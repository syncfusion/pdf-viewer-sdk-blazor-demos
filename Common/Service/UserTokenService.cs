#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorDemos.Service
{
    public class UserTokenService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string TokenFilePath = "user_tokens.json";
        private static readonly TimeZoneInfo IndianStandardTime = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public UserTokenService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetUserFingerprintAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("fingerPrint");
        }

        public async Task<int> GetRemainingTokensAsync(string userCode)
        {
            Dictionary<string, UserTokenInfo> tokens = await CheckAndResetTokensAsync(userCode);
            return tokens.ContainsKey(userCode) ? tokens[userCode].RemainingTokens : 15000 ;
        }

        public async Task UpdateTokensAsync(string userCode, int tokens)
        {
            Dictionary<string, UserTokenInfo> tokenData = await ReadTokensFromFileAsync();
            if (tokenData.ContainsKey(userCode))
            {
                tokenData[userCode].RemainingTokens = tokens;
            }
            else
            {
                tokenData[userCode] = new UserTokenInfo
                {
                    UserId = userCode,
                    DateOfLogin = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IndianStandardTime),
                    RemainingTokens = tokens
                };
            }
            await WriteTokensToFileAsync(tokenData);
        }

        public async Task<Dictionary<string, UserTokenInfo>> CheckAndResetTokensAsync(string userCode)
        {
            Dictionary<string, UserTokenInfo> tokenData = await ReadTokensFromFileAsync();
            if (tokenData.ContainsKey(userCode))
            {
                UserTokenInfo userTokenInfo = tokenData[userCode];
                DateTime currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IndianStandardTime);
                TimeSpan timeDifference = currentTime - userTokenInfo.DateOfLogin;

                if (timeDifference.TotalHours > 24)
                {
                    userTokenInfo.RemainingTokens = 15000; // Reset tokens
                    userTokenInfo.DateOfLogin = currentTime; // Update login time
                    await WriteTokensToFileAsync(tokenData);
                }
            }
            return tokenData;
        }


        private async Task<Dictionary<string, UserTokenInfo>> ReadTokensFromFileAsync()
        {
            if (!File.Exists(TokenFilePath))
            {
                Dictionary<string, UserTokenInfo> initialData = new Dictionary<string, UserTokenInfo>();
                await WriteTokensToFileAsync(initialData);
                return initialData;
            }

            string json = await File.ReadAllTextAsync(TokenFilePath);
            Dictionary<string, UserTokenInfo>? tokenData = JsonSerializer.Deserialize<Dictionary<string, UserTokenInfo>>(json);
            return tokenData ?? new Dictionary<string, UserTokenInfo>();
        }

        private async Task WriteTokensToFileAsync(Dictionary<string, UserTokenInfo> tokenData)
        {
            string json = JsonSerializer.Serialize(tokenData, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(TokenFilePath, json);
        }


        public async Task ShowAlert(string userCode)
        {
            string message = await ReturnAlertMessage(userCode);
            await _jsRuntime.InvokeVoidAsync("showBanner", message.ToString());
        }

        public async Task<string> ReturnAlertMessage(string userCode)
        {
            Dictionary<string, UserTokenInfo> tokenData = await ReadTokensFromFileAsync();
            if (tokenData.ContainsKey(userCode))
            {
                UserTokenInfo userTokenInfo = tokenData[userCode];
                string resetTime = userTokenInfo.DateOfLogin.AddHours(24).ToString("f");
                string message = $"You have reached your token limit. Your tokens will reset on {resetTime}. Download our <a href=\"https://github.com/SyncfusionExamples/blazor-smart-pdf-viewer-examples\" target=\"_blank\">Syncfusion Smart PDF Viewer Samples</a> from GitHub to explore this sample locally with your own API key.";
                return message;
            }
            return "";
        }

    }

    public class UserTokenInfo
    {
        public string UserId { get; set; }
        public DateTime DateOfLogin { get; set; }
        public int RemainingTokens { get; set; }
    }
}

