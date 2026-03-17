#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Spinner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorDemos.Shared
{
    /// <summary>
    /// The injectable service class used to handle common functionalities all over the application.
    /// </summary>
    public class SampleService
    {
        /// <summary>
        /// Specifies the app is rendering in device or not.
        /// </summary>
        public bool IsDevice { get; set; }
        /// <summary>
        /// Specifies spinner component reference.
        /// </summary>
        public SpinnerComponent? Spinner { get; set; }
        /// <summary>
        /// Specifies the current component name.
        /// </summary>
        public string? ComponentName { get; set; }
        /// <summary>
        /// Specifies the current sample details.
        /// </summary>
        public Sample? SampleInfo { get; set; }
        /// <summary>
        /// Specifies the meta data component reference.
        /// </summary>
        ///         /// <summary>
        /// Specifies the DynamicResourceLoader component reference.
        /// </summary>
        public DynamicResourceLoader? DynamicResourceLoader { get; set; }
        /// <summary>
        /// Specifies the sample name of the selected component for the DynamicResourceLoader.
        /// </summary>
		public string? SampleName { get; set; }
        /// <summary>
        /// Indicates whether the theme has changed due to browser navigation.
        /// Returns true if the theme was updated during navigation; otherwise, false.
        /// </summary>
        public bool IsThemeChangeOnBrowserNav { get; set; }
        /// <summary>
        /// Indicates whether dynamic resource loading has started. 
        /// Returns true when loading begins and false once the resources are fully loaded.
        /// </summary>
        public bool IsResourcesLoadedOnInternalNav { get; set; }
        /// <summary>
        /// Stores the theme applied during browser navigation.
        /// This value represents the theme name.
        /// </summary
        public string? BrowserNavTheme { get; set; }
        /// Indicates whether component samples-specific resources should be loaded during browser forward/backward navigation.
        /// </summary>
        public bool IsBrowserNav { get; set; }
        /// <summary>
        /// Specifies whether to hide the spinner once the component samples-specific styles have been loaded. 
        /// </summary>
        public bool HideSpinnerOnStylesLoad { get; set; }
        /// <summary>
        /// Specifies whether to hide the spinner once the component samples-specific scripts have been loaded.
        /// </summary>
        public bool HideSpinnerOnScriptsLoad { get; set; }
        /// <summary>
        /// Specifies the meta data component reference.
        /// </summary>
        public SampleMetaData? MetaData { get; set; }
        /// <summary>
        /// Specifies the very first sample url.
        /// </summary>
        internal string? FirstSampleUrl { get; set; }
        /// <summary>
        /// Specifies the last sample url.
        /// </summary>
        internal string? LastSampleUrl { get; set; }
        /// <summary>
        /// Specifies the current sample url.
        /// </summary>
        internal string? CurrentSampleUrl { get; set; }

        /// <summary>
        /// Specifies the documentation link.
        /// </summary>
        public string? DocumentLink { get; set; }

        /// <summary>
        /// Specifies the image url starts path.
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Specifies the showcase image url starts path.
        /// </summary>
        public string ShowCaseImagePath { get; set; }
        /// <summary>
        /// Specifies the home page loaded or not.
        /// </summary>
        public bool IsHomeLoaded { get; set; }
        /// <summary>
        /// Specifies the demo page loaded or not.
        /// </summary>
        public bool IsDemoLoaded { get; set; }
        /// <summary>
        /// Specifies the demo page js loaded or not.
        /// </summary>
        public bool SampleJSLoaded { get; set; }
        /// <summary>
        /// Specifies the Demos Type whether its WASM or Server.
        /// </summary>
        public string DemoType { get; set; }
        ///// <summary>
        ///// Specifies the Common base script path..
        ///// </summary>
        //public string CommonScriptPath { get; set; }

        ///// <summary>
        ///// Specifies the Common base script path..
        ///// </summary>
        //public string SBScriptPath { get; set; }

        ///// <summary>
        ///// Specifies the diagram component script path.
        ///// </summary>
        //public string DiagramScriptPath { get; set; }
        ///// <summary>
        ///// Specifies the pdfviewer script path.
        ///// </summary>
        //public string PdfScriptPath2 { get; set; }
        ///// <summary>
        ///// Specifies the document viewer script path.
        ///// </summary>
        //public string ViewerScriptPath { get; set; }
        ///// <summary>
        ///// Specifies the document editor script path.
        ///// </summary>
        //public string DocScriptPath { get; set; }
        ///// <summary>
        ///// Specifies the spreadsheet script path.
        ///// </summary>
        //public string SpreadsheetScriptPath { get; set; }
        /// <summary>
        /// Specifies the pdfviewer Script loaded or not.
        /// </summary>
        public bool IsPdfScript2Loaded { get; set; }
        /// <summary>
        /// Specifies the document editor script loaded or not.
        /// </summary>
        public bool IsDocScriptLoaded { get; set; }
        /// <summary>
        /// Specifies the spreadsheet script loaded or not.
        /// </summary>
        public bool IsSpreadsheetScriptLoaded { get; set; }
        /// <summary>
        /// Specifies the diagram interop script loaded or not.
        /// </summary>
        public bool IsDiagramScriptLoaded { get; set; }
        /// <summary>
        /// Specifies the Blazor Samples Common Static Web Assets location based on the project.
        /// </summary>
        public string WebAssetsPath { get; set; }
        /// <summary>
        /// Specifies the current sample is PDF Viewer.
        /// </summary>
        public bool IsPdfViewerSample { get; set; }
        /// <summary>
        /// Specifies the current sample is Document Editor/Word Editor.
        /// </summary>
        public bool IsWordEditorSample { get; set; }
        /// <summary>
        /// Specifies the current sample is Spreadsheet.
        /// </summary>
        public bool IsSpreadsheetSample { get; set; }
        /// <summary>
        /// Specifies the current sample is DiagramComponent.
        /// </summary>
        public bool IsDiagramSample { get; set; }
        /// <summary>
        /// Specifies whether script is reinitialized
        /// </summary>
        public static bool IsScriptReinitialized { get; set; }
        public static bool IsServer { get; }
#if !WASM
    = true;
#endif
        public static string AssetsPath { get; } =
#if WASM
#if NET8_0
            "_content/Blazor_WASM_Common_NET8/";
#elif NET9_0
            "_content/Blazor_WASM_Common_NET9/";
#else
            "_content/Blazor_WASM_Common_NET10/";
#endif
#else
#if NET8_0
            "_content/Blazor_Server_Common_NET8/";
#elif NET9_0
            "_content/Blazor_Server_Common_NET9/";
#else
            "_content/Blazor_Server_Common_NET10/";
#endif
#endif
        public SampleService()
        {
#if WASM
            DemoType = "Blazor Web App WASM Demos";
#else
            DemoType = "Blazor Web App Server Demos";
#endif
            WebAssetsPath = AssetsPath;
#if DEBUG || STAGING
            ImagePath = WebAssetsPath + "images/common/";
            ShowCaseImagePath = WebAssetsPath + "images/showcase/";
            //PdfScriptPath2 = "_content/Syncfusion.Blazor.SfPdfViewer/scripts";
            //DocScriptPath = "_content/Syncfusion.Blazor.WordProcessor/scripts";
            //SpreadsheetScriptPath = "_content/Syncfusion.Blazor.Spreadsheet/scripts";
            //CommonScriptPath = "_content/Syncfusion.Blazor.Core/scripts";
            //DiagramScriptPath = WebAssetsPath + "scripts/diagram/interop.js";
            //SBScriptPath = WebAssetsPath + "scripts/common/highlight.min.js";
            //ViewerScriptPath = WebAssetsPath + "scripts/pdfviewer/interop.js";
#else
            ImagePath = "https://cdn.syncfusion.com/blazor/images/demos/";
            ShowCaseImagePath = "https://cdn.syncfusion.com/blazor/images/showcase/";
            //PdfScriptPath2 = "https://cdn.syncfusion.com/blazor/27.1.48";
            //DocScriptPath = "https://cdn.syncfusion.com/blazor/27.1.48";
            //SpreadsheetScriptPath = "_content/Syncfusion.Blazor.Spreadsheet/scripts";
            //CommonScriptPath = "https://cdn.syncfusion.com/blazor/27.1.48";
            //DiagramScriptPath = WebAssetsPath + "scripts/diagram/interop.min.js";
            //SBScriptPath = WebAssetsPath + "scripts/common/demos.min.js";
            //ViewerScriptPath = WebAssetsPath + "scripts/pdfviewer/interop.min.js";
#endif
        }

        public static async void SwicthToDemo(string id, NavigationManager UriHelper, IJSRuntime JsRuntime)
        {
            var Navigation_Url = SampleUtils.IsHomePage(UriHelper) ?  UriHelper.BaseUri + SampleBrowser.SampleList[0].DemoPath : UriHelper.Uri;
#if DEBUG || STAGING
#if NET8_0
#if SERVER
            Navigation_Url = id == "server" ? Navigation_Url : Navigation_Url.Replace("blazor-server", "blazor-wasm", StringComparison.Ordinal);
#endif
#if WASM
            Navigation_Url =  id == "wasm" ? Navigation_Url : Navigation_Url.Replace("blazor-wasm", "blazor-server", StringComparison.Ordinal);
#endif
#elif NET9_0
#if SERVER
            Navigation_Url = id == "server" ? Navigation_Url : Navigation_Url.Replace("blazor-server", "blazor-wasm", StringComparison.Ordinal);
#endif
#if WASM
            Navigation_Url =  id == "wasm" ? Navigation_Url : Navigation_Url.Replace("blazor-wasm", "blazor-server", StringComparison.Ordinal);
#endif
#else
#if SERVER
            Navigation_Url = id == "server" ? Navigation_Url : Navigation_Url.Replace("blazor-server", "blazor-wasm", StringComparison.Ordinal);
#endif
#if WASM
            Navigation_Url =  id == "wasm" ? Navigation_Url : Navigation_Url.Replace("blazor-wasm", "blazor-server", StringComparison.Ordinal);
#endif
#endif
#else
#if SERVER
        Navigation_Url = id == "server" ? Navigation_Url : Navigation_Url.Replace("blazor-server", "blazor-wasm", StringComparison.Ordinal);
#endif
#if WASM
        Navigation_Url = id == "wasm" ? Navigation_Url : Navigation_Url.Replace("blazor-wasm", "blazor-server", StringComparison.Ordinal);
#endif
#endif
            await JsRuntime.InvokeVoidAsync("open", Navigation_Url, "_blank").ConfigureAwait(true);
        }

        // Updates the SampleInfo and ComponentName based on current loaded uri.
        internal void Update(NavigationManager urlHelper)
        {
            if (urlHelper.Uri.Contains("?theme", StringComparison.OrdinalIgnoreCase))
            {
                var themeName = System.Web.HttpUtility.ParseQueryString(new Uri(urlHelper.Uri).Query).Get("theme");
                var queryString = urlHelper.Uri.Substring(urlHelper.Uri.IndexOf(themeName!, StringComparison.OrdinalIgnoreCase) + themeName!.Length);
                if (!string.IsNullOrEmpty(queryString))
                {
                    string[] themeurl = urlHelper.Uri.Split("?");
                    var url = themeurl[0] + "?theme=" + themeName;
                    urlHelper.NavigateTo(url);
                }
            }
            string updatedUrl = urlHelper.ToBaseRelativePath(urlHelper.Uri);
             if (updatedUrl.Contains('?', StringComparison.Ordinal))
            {
                updatedUrl = updatedUrl.Substring(0, updatedUrl.IndexOf('?', StringComparison.Ordinal));
            }
            if (!string.IsNullOrEmpty(updatedUrl))
                {
                if (updatedUrl.LastIndexOf('/') == updatedUrl.Length - 1)
                {
                    updatedUrl = updatedUrl.Substring(0, updatedUrl.LastIndexOf('/'));
                }
                updatedUrl = updatedUrl.Replace("//", "/", StringComparison.Ordinal);
                string[] splittedUrl = updatedUrl.Split("/");
                if (splittedUrl.Length >= 2)
                {
                    try
                    {
                        string categoryName = splittedUrl[splittedUrl.Length - 2];
                        categoryName = categoryName.Replace("-", string.Empty, StringComparison.Ordinal);
                        SampleList controlInfo;
                        string sampleName = splittedUrl[splittedUrl.Length - 1];
                        if (categoryName == "buttons" && sampleName != "default-functionalities" && sampleName != "keyboard-navigation" || categoryName == "colorpicker" && sampleName == "inline")
                        {
                            controlInfo = SampleBrowser.SampleList.First<SampleList>(control => control!.DemoPath!.Contains(sampleName, StringComparison.OrdinalIgnoreCase));
                        }
                        else
                        {
                            controlInfo = SampleBrowser.SampleList.First<SampleList>(control => control!.ControllerName!.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                        }
                        this.ComponentName = controlInfo.Name;
                        List<Sample> sampleInfo = controlInfo.Samples!.Where(control => string.Equals(control!.Url, updatedUrl, StringComparison.OrdinalIgnoreCase)).ToList();
                        if (sampleInfo.Count > 0)
                        {
                            this.SampleInfo = sampleInfo.First();
                        }
                        else
                        {
                            sampleName = sampleName.Replace("-", string.Empty, StringComparison.Ordinal);
                            this.SampleInfo = controlInfo.Samples!.Where(control => string.Equals(control!.FileName, sampleName + ".razor", StringComparison.OrdinalIgnoreCase)).First();
                        }
                        this.CurrentSampleUrl = this.SampleInfo.Url;
                    }
                    catch (InvalidOperationException invOpEx)
                    {
                        Console.Error.WriteLine($"SampleService.Update: no sample found for URL {invOpEx.Message}");
                    }
                }
                // Navigate a Sample using Component Name
                else if (splittedUrl.Length < 2 && splittedUrl.Length > 0)
                {
                    string categoryName = splittedUrl[splittedUrl.Length - 1];
                    categoryName = categoryName.Replace("-", string.Empty, StringComparison.Ordinal);
                    var controlInfo = SampleBrowser.SampleList.FirstOrDefault<SampleList>(control => control!.ControllerName!.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                    if (controlInfo != null)
                    {
                        if (controlInfo.Samples?.Count > 0)
                        {
                            this.SampleInfo = controlInfo.Samples.First();
                        }
                        this.ComponentName = controlInfo.Name;
                        this.CurrentSampleUrl = this.SampleInfo?.Url;
                        var newUri = urlHelper.GetUriWithQueryParameters(SampleInfo!.Url!, new Dictionary<string, object?>
                        {
                            ["theme"] = "fluent2"
                        });
                        urlHelper.NavigateTo(newUri);
                    }

                }
            }
        }

        // Updates the documentation link based on current loaded uri.
        internal void UpdateFooter(string currentUrl)
        {
            
            currentUrl = Uri.UnescapeDataString(currentUrl);
            var queryString = System.Web.HttpUtility.ParseQueryString(new Uri(currentUrl).Query);
            // Handled condition to avoid a Culture switching navigation problem.
            if (queryString.Get("redirectUri") != null)
            {
                currentUrl = queryString.Get("redirectUri")!;
                currentUrl = currentUrl!.Split('?')[currentUrl.Split('?').Length - 2];
            }
            else
            {
                currentUrl = queryString.Count != 0 ? currentUrl.Replace(queryString.ToString()!, "", StringComparison.Ordinal) : currentUrl;
            }
            currentUrl = ((currentUrl.LastIndexOf('/') != -1)  || (currentUrl.LastIndexOf('?') != -1) || (currentUrl.LastIndexOf('&') != -1)) ? currentUrl.Remove(currentUrl.Length - 1) : currentUrl;
            currentUrl = currentUrl.EndsWith('/') ? currentUrl.TrimEnd('/') : currentUrl;
            var splittedUrl = currentUrl.Split("/");
            var ComponentName = splittedUrl[splittedUrl.Length - 2];
            var ComponentInfo = SampleBrowser.SampleList.First<SampleList>(control => control!.ControllerName!.Equals(ComponentName.Replace("-", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase));
            if (ComponentName == "pdf-viewer")
            {
                this.DocumentLink = "https://help.syncfusion.com/document-processing/pdf/pdf-viewer/blazor/overview";
            }
            else
            {
                this.DocumentLink = "https://help.syncfusion.com/document-processing/pdf/smart-pdf-viewer/blazor/overview";
            }
        }
        // Method for checking current Sample is PDF / Word / Diagram		
        public void checkSampleType(string currentLink)
        {
            if (currentLink != null)
            {
                if (currentLink.Contains("pdf-", StringComparison.OrdinalIgnoreCase)
                    || currentLink.Contains("powerpoint", StringComparison.OrdinalIgnoreCase))
                {
                    IsPdfViewerSample = true;
                }
                else if (currentLink.Contains("document-", StringComparison.OrdinalIgnoreCase))
                {
                    IsWordEditorSample = true;
                }
                else if (currentLink.Contains("spreadsheet", StringComparison.OrdinalIgnoreCase))
                {
                    IsSpreadsheetSample = true;
                }
                else if (currentLink.Contains("diagramcomponent", StringComparison.OrdinalIgnoreCase))
                {
                    IsDiagramSample = true;
                }
            }
        }
    }
}
