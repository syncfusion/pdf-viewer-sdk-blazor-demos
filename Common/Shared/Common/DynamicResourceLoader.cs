#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorDemos.Shared
{
    /// <summary>
    /// A component to dynamically load CSS styles and scripts based on the current sample.
    /// </summary>
    public class DynamicResourceLoader : ComponentBase
    {
        [Inject]
        protected NavigationManager? UriHelper { get; set; }

        [Inject]
        protected SampleService? SampleService { get; set; }

        // Defines the structure for holding resource information (CSS directory and JS files) for a component.
        private class ComponentSamplesResources
        {
            public string? CssDirectory { get; set; }
            public string[]? JsFiles { get; set; }
        }

        // A static dictionary that maps component names (keys) to their required resources (ComponentSamplesResources objects).
        // This allows for efficient lookup of CSS and JS dependencies for each component.
        private readonly Dictionary<string, ComponentSamplesResources> CompSamplesResourcesMappings = new()
        {
            {"pdf-viewer", new() { CssDirectory= "pdfviewer", JsFiles= new[] {"syncfusion-blazor-sfpdfviewer", "sf-grid", "sf-uploader", "sf-multiselect", "sf-accordion", "sf-sidebar", "sf-dropdownlist" } } },
            {"smart-pdf-viewer", new() { CssDirectory= "smartpdfviewer", JsFiles= new[] {"syncfusion-blazor-sfsmartpdfviewer" } } }
        };

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            #if !WASM
                builder.AddMarkupContent(0, GetResourcesMarkup(true));
            #else
                if (!SampleService.IsScriptReinitialized)
                {
                    builder.AddMarkupContent(0, GetResourcesMarkup(false));
                    SampleService.IsScriptReinitialized = true;
                }
                else
                {
                    builder.AddMarkupContent(0, GetResourcesMarkup(true));
                }
            #endif
        }

        private string GetResourcesMarkup(bool isAddScript)
        {
            var theme = SampleUtils.GetThemeName(UriHelper.Uri);
            if (SampleService != null && SampleService.IsThemeChangeOnBrowserNav)
            {
                theme = SampleService.BrowserNavTheme;
                SampleService.IsThemeChangeOnBrowserNav = false;
            }
            StringBuilder sb = new StringBuilder();
            // Use CDN links for Release builds
            var cdnScriptPath = "https://cdn.syncfusion.com/blazor/31.2.12/";
            var cdnStylePath = "https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/";
            var demoPageCompsScript = new string[] { "sf-tab", "sf-tooltip", "sf-toast" };
            if (SampleUtils.IsHomePage(UriHelper))
            {
                sb.AppendLine($"<script src=\"{cdnScriptPath}sf-drop-down-button.min.js\"></script>");
            }
            else 
            {
                if(theme != "fluent2")
                {
                    sb.AppendLine($"<link rel=\"stylesheet\" href=\"https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/demo-page-comps/{theme}.min.css\" />");
                }
                // Get the current component page details
                var compKey = GetComponentKey()?.ToLower();
                // Retrieve the two values (e.g., CssDirectory and JsFiles) based on compName
                ComponentSamplesResources resources = CompSamplesResourcesMappings.GetValueOrDefault(compKey);
                // sf-tab, sf-toast, and sf-tooltip - Since these resources are loaded commonly in every page for the SB's common layout, they are not loaded during comp to comp navigation.
                resources.JsFiles = resources.JsFiles != null ? resources.JsFiles.Except(demoPageCompsScript).ToArray() : null;
                if (SampleService?.SampleName == null)
                {
                    if (!string.IsNullOrEmpty(resources.CssDirectory))
                    {
                        if (resources.CssDirectory == "overall")
                        {
                            sb.AppendLine($"<link rel=\"stylesheet\" href=\"https://cdn.syncfusion.com/blazor/31.2.12/styles/{theme}.css\" />");
                        }
                        else
                        {
                            sb.AppendLine($"<link rel=\"stylesheet\" href=\"{cdnStylePath}{resources.CssDirectory}/{theme}.min.css\" />");
                        }
                    }
                    if (resources.JsFiles != null && isAddScript == true)
                    {
                        foreach (var file in resources.JsFiles)
                        {
                            var jsPath = file == "syncfusion-blazor" ? $"{cdnScriptPath}syncfusion-blazor.min.js" : $"{cdnScriptPath}{file}.min.js";
                            sb.AppendLine($"<script src=\"{jsPath}\"></script>");
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(resources.CssDirectory))
                    {
                        if (resources.CssDirectory == "overall")
                        {
                            sb.AppendLine($"<link rel=\"stylesheet\" href=\"https://cdn.syncfusion.com/blazor/31.2.12/styles/{theme}.css\" />");
                        }
                        else if (resources.JsFiles == null)
                        {
                            sb.AppendLine($"<link rel=\"stylesheet\" href=\"{cdnStylePath}{resources.CssDirectory}/{theme}.min.css\" onload=\"onCompStylesLoaded(false)\" />");
                        }
                        else
                        {
                            sb.AppendLine($"<link rel=\"stylesheet\" href=\"{cdnStylePath}{resources.CssDirectory}/{theme}.min.css\" onload=\"onCompStylesLoaded(true)\" />");
                        }
                    }
                    if (resources.JsFiles != null)
                    {
                        foreach (var file in resources.JsFiles)
                        {
                            var jsPath = file == "syncfusion-blazor" ? $"{cdnScriptPath}syncfusion-blazor.min.js" : $"{cdnScriptPath}{file}.min.js";
                            if (resources.JsFiles[resources.JsFiles.Length - 1] == file)
                            {
                                sb.AppendLine($"<script src=\"{jsPath}\" onload=\"onCompScriptsLoaded()\"></script>");
                            }
                            else
                            {
                                sb.AppendLine($"<script src=\"{jsPath}\"></script>");
                            }
                        }
                    }
                }

                if (theme.Contains("dark") || theme.Contains("fluent2-highcontrast") || theme.Contains("highcontrast"))
                {
                    sb.AppendLine($"<link rel=\"stylesheet\" href=\"https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/highcontrast.min.css\" />");
                    sb.AppendLine($"<link rel=\"stylesheet\" href=\"https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/dark-theme.min.css\" />");
                }
                // Add sf-tab, sf-toast, and sf-tooltip for the SB's common layout if syncfusion-blazor.js is not already being loaded.
                if (resources.JsFiles == null || !resources.JsFiles.Contains("syncfusion-blazor"))
                {
                    sb.AppendLine($"<script src=\"{cdnScriptPath}sf-tab.min.js\"></script>");
                    sb.AppendLine($"<script src=\"{cdnScriptPath}sf-tooltip.min.js\"></script>");
                    sb.AppendLine($"<script src=\"{cdnScriptPath}sf-toast.min.js\"></script>");
                }
            }
            return sb.ToString();
        }

        private string GetComponentKey()
        {
            string demoPath;
            // Url is often like "Component/SampleName" e.g., "datagrid/overview"
            if (SampleService?.SampleName == null) {
                demoPath = SampleService?.SampleInfo?.Url;
            }
            else
            {
                demoPath = SampleService.SampleName;
            }
            var componentName = demoPath?.Split('/')[0];
            return componentName;
        }

        public void Refresh()
        {
            StateHasChanged();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            // Assign reference to sample service for outside usage.
            if (firstRender && SampleService != null)
            {
                SampleService.DynamicResourceLoader = this;
            }
        }
    }
}