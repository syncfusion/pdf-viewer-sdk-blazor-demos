using Microsoft.Extensions.AI;
using Syncfusion.Blazor.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemos.Service
{
    public class CustomAIService : IChatInferenceService
    {
        private readonly UserTokenService _userTokenService;
        private ChatParameters chatParameters_history = new ChatParameters();
        private IChatClient _chatClient;

        public CustomAIService(UserTokenService userTokenService, IChatClient client)
        {
            _userTokenService = userTokenService;
            this._chatClient = client ?? throw new ArgumentNullException(nameof(client));
        }


        /// <summary>
        /// Gets a text completion from the Azure OpenAI service.
        /// </summary>
        /// <param name="prompt">The user prompt to send to the AI service.</param>
        /// <param name="returnAsJson">Indicates whether the response should be returned in JSON format. Defaults to <c>true</c></param>
        /// <param name="appendPreviousResponse">Indicates whether to append previous responses to the conversation history. Defaults to <c>false</c></param>
        /// <param name="systemRole">Specifies the systemRole that is sent to AI Clients. Defaults to <c>null</c></param>
        /// <returns>The AI-generated completion as a string.</returns>
        public async Task<string> GetCompletionAsync(string prompt, bool returnAsJson = true, bool appendPreviousResponse = false, string systemRole = null, int outputTokens = 2000)
        {
            string systemMessage = returnAsJson ? "You are a helpful assistant that only returns and replies with valid, iterable RFC8259 compliant JSON in your responses unless I ask for any other format. Do not provide introductory words such as 'Here is your result' or '```json', etc. in the response" : !string.IsNullOrEmpty(systemRole) ? systemRole : "You are a helpful assistant";
            try
            {
                ChatParameters chatParameters = appendPreviousResponse ? chatParameters_history : new ChatParameters();
                if (appendPreviousResponse)
                {
                    if (chatParameters.Messages == null)
                    {
                        chatParameters.Messages = new List<ChatMessage>() {
                            new ChatMessage(ChatRole.System,systemMessage),
                        };
                    }
                    chatParameters.Messages.Add(new ChatMessage(ChatRole.User, prompt));
                }
                else
                {
                    chatParameters.Messages = new List<ChatMessage>(2) {
                        new ChatMessage (ChatRole.System, systemMessage),
                        new ChatMessage(ChatRole.User,prompt),
                    };
                }
                chatParameters.MaxTokens = outputTokens;
                string completion = await GenerateResponseAsync(chatParameters);
                if (appendPreviousResponse)
                {
                    chatParameters_history?.Messages?.Add(new ChatMessage(ChatRole.Assistant, completion));
                }
                return completion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex.Message}");
                return "";
            }
        }

        public async Task<string> GenerateResponseAsync(ChatParameters options)
        {
            string userCode = await _userTokenService.GetUserFingerprintAsync();
            int remainingTokens = await _userTokenService.GetRemainingTokensAsync(userCode);
            int inputTokens = options.Messages.Sum(message => message.Text.Length / 4);

            if (remainingTokens <= inputTokens)
            {
                await _userTokenService.ShowAlert(userCode);
                return null;
            }
            // Create a completion request with the provided parameters
            ChatOptions completionRequest = new ChatOptions
            {
                Temperature = options.Temperature ?? 0.5f,
                TopP = options.TopP ?? 1.0f,
                MaxOutputTokens = options.MaxTokens ?? 2000,
                FrequencyPenalty = options.FrequencyPenalty ?? 0.0f,
                PresencePenalty = options.PresencePenalty ?? 0.0f,
                StopSequences = options.StopSequences
            };
            try
            {
                ChatResponse completion = await _chatClient.GetResponseAsync(options.Messages, completionRequest);
                await _userTokenService.UpdateTokensAsync(userCode, (int)(remainingTokens - completion.Usage.TotalTokenCount));
                return completion.Text.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
