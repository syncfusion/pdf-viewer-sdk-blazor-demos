#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Web;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorDemos.Shared
{
    /// <summary>
    /// A util class to perform common functionalities.
    /// </summary>
    public static class SampleUtils
    {
        #region common
        public const string SPACE = " ";
        public const string CONTENT = "sf-content";
        public const string VISIBLE = "sf-visible";
        public const string HIDDEN = "sf-hidden";
        public const string DisplayNone = "sb-hide";
        public const string ComponentsHide = "sb-components-hide";
        public const string ActiveClass = "active";

        #endregion

        #region SideBarComponent
        public const string SidebarClass = "sf-sidebar";
        public const string  SidebarLeft = "sf-sidebar-left";
        public const string SidebarRight = "sf-sidebar-right";
        public const string SidebarRightPane = "sf-sidebar-right-pane";
        public const string SidebarRightPaneCollapse = "sf-sidebar-right-pane-collapse";
        public const string SidebarRightPaneExpand = "sf-sidebar-right-pane-expand";
        #endregion

        #region ListComponent
        public const string ListClass = "sf-list";
        public const string ListUlClass = "sf-list-ul";
        public const string ListLiClass = "sf-list-li";
        public const string ListLiGroupClass = "sf-list-group-li";
        public const string ListActive = "sf-list-li-active";
        #endregion

        #region Spinner
        public const string ModalClass = "sf-modal";
        #endregion

        #region TreeComponent
        public const string TreeClass = "sf-tree";
        public const string TreeFullRow = "sf-tree-full-row";
        public const string TreeTextContent = "sf-tree-text-content";
        public const string TreeParent = "sf-tree-parent";
        public const string TreeParentLi = "sf-tree-parent-li";
        public const string TreeExpandIcon = "sf-tree-expand-icon";
        public const string TreeCollapseIcon = "sf-tree-collapse-icon";
        public const string TreeText = "sf-tree-text";
        public const string TreeActive = "sf-tree-active";
        public const string TreeHide = "sf-tree-hide";
        #endregion

        #region SearchComponent
        public const string SearchContainer = "sf-search-container";
        public const string SearchPopup = "sf-search-popup";
        public const string SearchInput = "sf-search-input";
        public const string SearchNoData = "sf-search-no-data";
        public const string ClearIcon = "sb-icons sf-clear-icon";
        public const string SearchIcon = "sb-icons sf-search-icon";
        public const string SearchOverlay = "sb-search-overlay";
        public const string SearchKeyNav = "sf-key-nav";
        #endregion

        #region AdStripComponent
        public const string AdContainer = "sb-ad-container";
        public const string AdContent = "sb-ad-content-area";
        public const string AdHeader = "sb-ad-header";
        public const string AdPointsDiv = "sb-ad-points-div";
        public const string AdPointDiv = "sb-ad-point-div";
        public const string AdPointTick = "sb-ad-img-div sb-icons sb-ad-tick";
        public const string AdPointText = "sb-ad-point-text";
        public const string AdLink = "sb-ad-link";
        public const string AdTry = "sb-ad-try-area";
        #endregion

        #region Preferences
        public const string DefaultMode = "mouse";
        public const string PreferencesPopupClass = "sf-preferences-popup";
        public const string PreferencesTouch = "sf-preference-btn sf-preference-touch-btn";
        public const string PreferencesMouse = "sf-preference-btn sf-preference-mouse-btn";
        #endregion

        #region HeaderComponent
        public const string HeaderSearchClass = "sb-search-btn";
        public const string HeaderPreferencesClass = "sf-preferences-button";
        #endregion

        #region DropdownComponent
        public const string DropdownPopup = "sf-dropdown-popup";
        #endregion

        #region NotificationComponent
        public const string NotificationPopupClass = "sb-notification-popup";
        #endregion

        /// <summary>
        /// Add a class to the existing string content.
        /// </summary>
        /// <param name="prevClass">Previous class list in string format.</param>
        /// <param name="className">Class name needs to be added in the string content.</param>
        /// <returns>Returns class string.</returns>
        internal static string AddClass(string prevClass, string className)
        {
            var finalClass = string.IsNullOrEmpty(prevClass) ? string.Empty : prevClass.Trim();
            finalClass = finalClass.Contains(className, StringComparison.Ordinal) ? finalClass : finalClass + SampleUtils.SPACE + className;
            return finalClass;
        }

        /// <summary>
        /// Remove a class from the existing string content.
        /// </summary>
        /// <param name="prevClass">Previous class list in string format.</param>
        /// <param name="className">Class name needs to be removed in the string content.</param>
        /// <returns>Returns class string.</returns>
        public static string RemoveClass(string prevClass, string className)
        {
            var finalClass = string.IsNullOrEmpty(prevClass) ? string.Empty : prevClass.Trim();
            finalClass = finalClass.Contains(className, StringComparison.Ordinal) ? prevClass.Replace(className, string.Empty, StringComparison.Ordinal) : finalClass;
            return finalClass;
        }

        public static bool IsLocalSample(string input)
        {
            if (input != null)
            {
                return input.Contains("localhost", StringComparison.Ordinal) ? true : false;
            }
            return false;
        }

        public static bool IsHomePage(NavigationManager uriHelper)
        {
            var currentUri = uriHelper?.Uri.Split("?")[0];
            return uriHelper?.BaseUri == currentUri;
        }

        /// <summary>
        /// DropDown data for Demos Sample  page switching button
        /// </summary>
        internal static List<DropDownData> Blazor_Platform = new List<DropDownData> {
        #if !SERVER
            new DropDownData { ID = "server", Text = "Web App Server" },
        #endif
        #if !WASM
            new DropDownData { ID = "wasm", Text = "Web App WASM" }
        #endif
        };

        /// <summary>
        /// List data for Demos Landing page switching button
        /// </summary>

        internal static List<ListData> BlazorPlatform = new List<ListData> {
            #if !SERVER
                        new ListData { ID = "server", Text = "Web App Server" },
            #endif
            #if !WASM
                        new ListData { ID = "wasm", Text = "Web App WASM" }
            #endif
        };

        /// <summary>
        /// Returns the url with current selected theme query string.
        /// </summary>
        /// <param name="UriHelper">Current url need to be validated.</param>
        /// <param name="themeName">Selected theme name to be added in the query string.</param>
        /// <returns>Returns class string.</returns>
        public static string GetThemeLink(NavigationManager UriHelper, string themeName)
        {
            string url = UriHelper?.Uri.TrimEnd('/')!;
#if !STAGING || DEBUG
            themeName = themeName != null && themeName.Equals("bootstrap5.3", StringComparison.OrdinalIgnoreCase) ? "bootstrap5" : themeName!;
#endif
            if (url.Contains("?theme=", StringComparison.OrdinalIgnoreCase))
            {
                string[] splittedUrl = url.Split("?theme=");
                url = splittedUrl[0];
            }
            url = UriHelper!.GetUriWithQueryParameters(url, new Dictionary<string, object?>
            {
                ["theme"] = themeName
            });
            return url;
        }

        /// <summary>
        /// Returns the current theme name from the url.
        /// </summary>
        /// <param name="input">Current url need to be parsed for getting the theme name.</param>
        public static string GetThemeName(string input)
        {
            var uri = new Uri(input);
            string themeName = HttpUtility.ParseQueryString(uri.Query).Get("theme")!;
            themeName = themeName != null ? themeName : "fluent2";
#if !STAGING || DEBUG
            themeName = themeName.Equals("bootstrap5", StringComparison.OrdinalIgnoreCase) ? "bootstrap5.3" : themeName.Equals("bootstrap5-dark", StringComparison.OrdinalIgnoreCase) ? "bootstrap5.3-dark" : themeName;
#endif
            return themeName;
        }

        internal static List<DropDownData> ThemeData = new List<DropDownData>
        {
#if RELEASE && STAGING
            new DropDownData { ID = "material", Text = "Material" },
#endif
            new DropDownData { ID = "material3", Text = "Material 3" },
            new DropDownData { ID = "fluent", Text = "Fluent" },
            new DropDownData { ID = "fluent2", Text = "Fluent 2" },
#if RELEASE && STAGING
            new DropDownData { ID = "bootstrap4", Text = "Bootstrap v4" },
            new DropDownData { ID = "bootstrap5", Text = "Bootstrap v5" },
            new DropDownData { ID = "bootstrap5.3", Text = "Bootstrap v5.3" },
#else
            new DropDownData { ID = "bootstrap5.3", Text = "Bootstrap 5" },
#endif
#if RELEASE && STAGING
            new DropDownData { ID = "tailwind", Text = "Tailwind CSS" },
            new DropDownData { ID = "tailwind3", Text = "Tailwind3 CSS" },
#else
            new DropDownData { ID = "tailwind3", Text = "Tailwind CSS" },
#endif
#if RELEASE && STAGING
           new DropDownData { ID = "fabric", Text = "Fabric" },
#endif
            new DropDownData { ID = "highcontrast", Text = "High Contrast" },
            new DropDownData { ID = "fluent2-highcontrast", Text = "Fluent 2 High Contrast" },
        };

        /// <summary>
        /// Returns the current theme mode from the url.
        /// </summary>
        /// <param name="url">Current url need to be parsed for getting the theme mode.</param>
        internal static string GetThemeMode(string url)
        {
            var uri = new Uri(url);
            string themeMode = HttpUtility.ParseQueryString(uri.Query).Get("theme")!;
            themeMode = themeMode == null ? "Light Mode" : !themeMode.Contains("-dark", StringComparison.OrdinalIgnoreCase) ? "Light Mode" : "Dark Mode";
            return themeMode;
        }

        internal static List<DropDownData> ThemeMode { get; set; } = new List<DropDownData> {
        new DropDownData { ID = "dark", Text = "Dark Mode" },
        new DropDownData { ID = "light", Text = "Light Mode" }
    };
//        /// <summary>
//        /// Returns list of JS resources need to be loaded.
//        /// </summary>
//        /// <returns></returns>
//        public static List<string> GetDynamicJSResources(NavigationManager uriHelper, SampleService sampleService)
//        {
//            var resourceList = new List<string>();
//            if (sampleService.SampleJSLoaded)
//            {
//                sampleService.SampleJSLoaded = false;
//                resourceList = new List<string>
//                {
//                   $"{sampleService.CommonScriptPath}/syncfusion-blazor.min.js",
//                   $"{sampleService.SBScriptPath}"
//                };
//                if (sampleService.ComponentName != null)
//                {
//                    if (sampleService.ComponentName.Equals("PDF Viewer") && !sampleService.IsPdfScript2Loaded)
//                    {
//                        sampleService.IsPdfScript2Loaded = true;
//                        resourceList.Add(sampleService.ViewerScriptPath);
//                        resourceList.Add(sampleService.PdfScriptPath2 + "/syncfusion-blazor-sfpdfviewer.min.js");
//                    }
//                    if (sampleService.ComponentName.Equals("Word Processor") && !sampleService.IsDocScriptLoaded)
//                    {
//                        sampleService.IsDocScriptLoaded = true;
//                        resourceList.Add(sampleService.DocScriptPath + "/syncfusion-blazor-documenteditor.min.js");
//                    }
//                    if (sampleService.ComponentName.Equals("Diagram") && !sampleService.IsDiagramScriptLoaded)
//                    {
//                        sampleService.IsDiagramScriptLoaded = true;
//                        resourceList.Add($"{sampleService.DiagramScriptPath}");
//                    }
//                    if (sampleService.ComponentName.Equals("Spreadsheet") && !sampleService.IsSpreadsheetScriptLoaded)
//                    {
//                        sampleService.IsSpreadsheetScriptLoaded = true;
//                        resourceList.Add($"{sampleService.SpreadsheetScriptPath}");
//                    }
//                }
//            }
//            return resourceList;
//        }

//        /// <summary>
//        /// Returns list of resources need to be loaded.
//        /// </summary>
//        /// <returns></returns>
//        public static List<string> GetDynamicResources(NavigationManager uriHelper, SampleService sampleService)
//        {
//            var resourceList = new List<string>();
//            if (!sampleService.IsHomeLoaded && SampleUtils.IsHomePage(uriHelper))
//            {
//                sampleService.IsHomeLoaded = true;
//#if DEBUG || STAGING
//                resourceList = new List<string> { $"{sampleService.WebAssetsPath}styles/common/home.css" };
//#else
//                resourceList = new List<string> { $"{sampleService.WebAssetsPath}styles/common/home.min.css" };
//#endif
//            }
//            else if (!sampleService.IsDemoLoaded)
//            {
//                sampleService.IsDemoLoaded = true;
//#if DEBUG || STAGING
//                resourceList = new List<string>
//                {
//                    $"{sampleService.WebAssetsPath}styles/common/roboto.css",
//                    $"{sampleService.WebAssetsPath}styles/common/highlight.css",
//                    $"{sampleService.WebAssetsPath}styles/common/demos.css",
//                };
//                if (uriHelper.Uri.Contains("theme=highcontrast"))
//                {
//                    resourceList.Add($"{sampleService.WebAssetsPath}styles/common/highcontrast.css");
//                }
//                if (new Regex(@"theme=.*-dark").IsMatch(uriHelper.Uri))
//                {
//                    resourceList.Add($"{sampleService.WebAssetsPath}styles/common/dark-theme.css");
//                }
//#else
//                resourceList = new List<string>
//                {
//                    $"{sampleService.WebAssetsPath}styles/common/demos.min.css",
//                };
//                if (uriHelper.Uri.Contains("theme=highcontrast") || new Regex(@"theme=.*-dark").IsMatch(uriHelper.Uri))
//                {
//                    resourceList.Add($"{sampleService.WebAssetsPath}styles/common/dark-theme.min.css");
//                }
//#endif
//            }
//            return resourceList;
//        }
    }

    /// <summary>
    /// Culture switcher datasource model class.
    /// </summary>
    public class DropDownData
    {
        public string? ID { get; set; }
        public string? Text { get; set; }
    }

    /// <summary>
    /// SplitButton datasource model class.
    /// </summary>
    public class ListData
    {
        public string? ID { get; set; }
        public string? Text { get; set; }
    }

#pragma warning disable
    
    /// <summary>
    /// Search Component datasource model class.
    /// </summary>

    public class SearchList
    {
        public string Category { get; set; }
        public List<SearchResult> SampleList { get; set; }
        public bool IsMultiSearch { get; set; }
#if SERVER
        public async Task<List<SearchList>> GetSearchListAsync()
#else
        public List<SearchList> GetSearchList()
#endif
        {
            bool isButtonsAdded = false;
            var searchlist = new List<SearchList>();
            var sampleList = SampleBrowser.SampleList;
#if SERVER
            await Task.Run(() => { 
#endif
            for (int i = 0; i < sampleList.Count; i++)
                {
                    if ((sampleList[i].Category == "Buttons" || sampleList[i].Category == "Inputs") && sampleList[i].ControllerName == "Buttons")
                    {
                        if (!isButtonsAdded)
                        {
                            isButtonsAdded = !isButtonsAdded;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    var samples = sampleList[i].Samples;
                    var searchResult = new List<SearchResult>();
                    for (int j = 0; j < samples.Count; j++)
                    {
                        var sample = samples[j];
                        searchResult.Add(new SearchResult { SampleName = sample.Name, SamplePath = sample.Url });
                    }
                    searchlist.Add(new SearchList { Category = sampleList[i].ControllerName, SampleList = searchResult });
                }
#if SERVER
            });
#endif
            return searchlist;
        }
    }

    public class SearchResult
    {
        public string SampleName { get; set; }
        public string SamplePath { get; set; }
    }
    /// <summary>
    /// Notification component's data model class.
    /// </summary
    public class NotificationList
    {
        public string Name { get; set; }
        public string DefaultSamplePath { get; set; }
        public List<NotificationData> SampleList { get; set; }
        public string[] NotificationContent { get; set; }
#if SERVER
        public async Task<List<NotificationList>> GetNotificationDataAsync()
#else
        public List<NotificationList> GetNotificationData()
#endif
        {
            var notificationlist = new List<NotificationList>();
            var sampleList = SampleBrowser.SampleList;
#if SERVER
            await Task.Run(() => { 
#endif
            for (int i = 0; i < sampleList.Count; i++)
                {
                    var samples = sampleList[i].Samples;
                    var notificationResultData = new List<NotificationData>();
                    for (int j = 0; j < samples.Count; j++)
                    {
                        var sample = samples[j];
                        if (sample.Type.ToString() == "New" || sample.Type.ToString() == "Updated" || sampleList[i].IsPreview)
                        {
                            if (sample.NotificationDescription != null)
                            {
                                notificationResultData.Add(new NotificationData { SampleName = sample.Name, SampleUrl = sample.Url, NotificationContent = sample.NotificationDescription });
                            }
                        }
                    }
                    if (notificationResultData.Count != 0)
                    {
                        notificationlist.Add(new NotificationList { Name = sampleList[i].Name, DefaultSamplePath = sampleList[i].DemoPath, SampleList = notificationResultData });
                    }
                }
#if SERVER
            });
#endif
            return notificationlist;
        }

#if SERVER
        public async Task<List<NotificationList>> GetComponentNotificationDataAsync()
#else
        public List<NotificationList> GetComponentNotificationData()
#endif
        {
            var listcomponentnotification = new List<NotificationList>();
            var notificationResultData = new List<NotificationData>();
            var sampleList = SampleBrowser.SampleList;
#if SERVER
            await Task.Run(() => { 
#endif
            for (int i = 0; i < sampleList.Count; i++)
                {
                    if (sampleList[i].NotificationDescription != null)
                    {
                        listcomponentnotification.Add(new NotificationList { Name = sampleList[i].Name, DefaultSamplePath = sampleList[i].DemoPath, NotificationContent = sampleList[i].NotificationDescription });
                    }
                }
#if SERVER
            });
#endif
            return listcomponentnotification;
        }

    }

    public class NotificationData
    {
        public string SampleName { get; set; }
        public string SampleUrl { get; set; }
        public string[] NotificationContent { get; set; }
    }

    /// <summary>
    /// Carousel component's data model class.
    /// </summary>
    public class ShowCaseItem
    {
        /// <summary>
        /// Constructor for updating properties.
        /// </summary>
        public ShowCaseItem(string header, string content, string imagePath, string demourl, string gitHubLink, string bgColor, bool isServer, bool isWasm)
        {
            this.Header = header;
            this.Content = content;
            this.ImagePath = imagePath;
            this.DemoUrl = demourl;
            this.GitHubLink = gitHubLink;
            this.BgColor = bgColor;
            this.isServer = isServer;
            this.isWasm = isWasm;
        }
        /// <summary>
        /// Specifies the header content of the item.
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// Specifies the description of the item.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Specifies the image path of the item.
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Specifies the hyper link of the item.
        /// </summary>
        public string DemoUrl { get; set; }
        /// <summary>
        /// Specifies the GitHub link of the item.
        /// </summary>
        public string GitHubLink { get; set; }
        /// <summary>
        /// Specifies the Background color of the item.
        /// </summary>
        public string BgColor { get; set; }
        /// <summary>
        /// Specifies the server mode.
        /// </summary>
        public bool isServer { get; set; }
        /// <summary>
        /// Specifies the wasm mode.
        /// </summary>
        public bool isWasm { get; set; }
    }

    public class CarouselModel
    {
        public int XValue { get; set; }
        public int LeftValue { get; set; }
        public bool IsDevice { get; set; }
    }

    /// <summary>
    /// AdStrip component's data model class.
    /// </summary>
    public class AdPoint
    {
        /// <summary>
        /// Constructor for updating property.
        /// </summary>
        public AdPoint(string Text)
        {
            this.AdPointText = Text;
        }
        /// <summary>
        /// Specifies the Ad point text content.
        /// </summary>
        public string AdPointText { get; set; }
    }

    /// <summary>
    /// Popular components model class.
    /// </summary>
    public class PopularComponents
    {
        public PopularComponents()
        {

        }

        public PopularComponents(string componentName, string imageName, string demoPath)
        {
            this.ComponentName = componentName;
            this.ImageName = imageName;
            this.DemoPath = demoPath;
        }
        /// <summary>
        /// Specifies the component name.
        /// </summary>
        public string ComponentName { get; set; }
        /// <summary>
        /// Specifies the component image path.
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// Specifies the demo path.
        /// </summary>
        public string DemoPath { get; set; }

        public List<PopularComponents> GetComponents()
        {
            List<PopularComponents> components = new List<PopularComponents>();
            {
                components.Add(new PopularComponents("Data Grid", "data-grid", "datagrid/overview"));
                components.Add(new PopularComponents("Charts", "charts", "chart/overview"));
                components.Add(new PopularComponents("Scheduler", "scheduler", "scheduler/overview"));
                components.Add(new PopularComponents("Diagram", "diagram", "diagram/flowchart"));
                components.Add(new PopularComponents("Rich Text Editor", "rich-text-editor", "rich-text-editor/overview"));
                components.Add(new PopularComponents("Tree Grid", "tree-grid", "tree-grid/overview"));
                components.Add(new PopularComponents("Gantt Chart", "gantt-chart", "gantt-chart/overview"));
            }
            return components;
        }
    }
    
#pragma warning restore

    /// <summary>
    /// Specifies the position of the SideBar component.
    /// </summary>
    public enum SidebarPlacement
    {   
        /// <summary>
        /// Renders the sidebar at left side.
        /// </summary>
        Left,
        /// <summary>
        /// Renders the sidebar at right side.
        /// </summary>
        Right
    }
}