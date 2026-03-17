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
using System.Globalization;
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

        private readonly JsonSerializerOptions s_jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public UserTokenService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetUserFingerprintAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("fingerPrint").ConfigureAwait(true);
        }

        public async Task<int> GetRemainingTokensAsync(string userCode)
        {
            Dictionary<string, UserTokenInfo> tokens = await CheckAndResetTokensAsync(userCode).ConfigureAwait(true);
            return tokens.TryGetValue(userCode, out UserTokenInfo? info) ? info.RemainingTokens : 15000;
        }

        public async Task UpdateTokensAsync(string userCode, int tokens)
        {
            Dictionary<string, UserTokenInfo> tokenData = await ReadTokensFromFileAsync().ConfigureAwait(true);
            if (tokenData.TryGetValue(userCode, out UserTokenInfo? info))
            {
                info.RemainingTokens = tokens;
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
            await WriteTokensToFileAsync(tokenData).ConfigureAwait(true);
        }

        public async Task<Dictionary<string, UserTokenInfo>> CheckAndResetTokensAsync(string userCode)
        {
            Dictionary<string, UserTokenInfo> tokenData = await ReadTokensFromFileAsync().ConfigureAwait(true);
            if (tokenData.TryGetValue(userCode, out UserTokenInfo? userTokenInfo))
            {
                DateTime currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IndianStandardTime);
                TimeSpan timeDifference = currentTime - userTokenInfo.DateOfLogin;

                if (timeDifference.TotalHours > 24)
                {
                    userTokenInfo.RemainingTokens = 15000;
                    userTokenInfo.DateOfLogin = currentTime;
                    await WriteTokensToFileAsync(tokenData).ConfigureAwait(true);
                }
            }
            return tokenData;
        }


        private async Task<Dictionary<string, UserTokenInfo>> ReadTokensFromFileAsync()
        {
            if (!File.Exists(TokenFilePath))
            {
                Dictionary<string, UserTokenInfo> initialData = new Dictionary<string, UserTokenInfo>();
                await WriteTokensToFileAsync(initialData).ConfigureAwait(true);
                return initialData;
            }

            string json = await File.ReadAllTextAsync(TokenFilePath).ConfigureAwait(true);
            Dictionary<string, UserTokenInfo>? tokenData = JsonSerializer.Deserialize<Dictionary<string, UserTokenInfo>>(json);
            return tokenData ?? new Dictionary<string, UserTokenInfo>();
        }

        private async Task WriteTokensToFileAsync(Dictionary<string, UserTokenInfo> tokenData)
        {
            string json = JsonSerializer.Serialize(tokenData, s_jsonOptions);
            await File.WriteAllTextAsync(TokenFilePath, json).ConfigureAwait(true);
        }


        public async Task ShowAlert(string userCode)
        {
            string message = await ReturnAlertMessage(userCode).ConfigureAwait(true);
            await _jsRuntime.InvokeVoidAsync("showBanner", message.ToString()).ConfigureAwait(true);
        }

        public async Task<string> ReturnAlertMessage(string userCode)
        {
            Dictionary<string, UserTokenInfo> tokenData = await ReadTokensFromFileAsync().ConfigureAwait(true);
            if (tokenData.TryGetValue(userCode, out UserTokenInfo? userTokenInfo))
            {
                string resetTime = userTokenInfo.DateOfLogin.AddHours(24).ToString("f", CultureInfo.CurrentCulture);
                string message = $"You have reached your token limit. Your tokens will reset on {resetTime}. Download our <a href=\"https://github.com/SyncfusionExamples/blazor-smart-pdf-viewer-examples\" target=\"_blank\">Syncfusion Smart PDF Viewer Samples</a> from GitHub to explore this sample locally with your own API key.";
                return message;
            }
            return string.Empty;
        }

    }

    public class UserTokenInfo
    {
        public string? UserId { get; set; }
        public DateTime DateOfLogin { get; set; }
        public int RemainingTokens { get; set; }
    }
}

