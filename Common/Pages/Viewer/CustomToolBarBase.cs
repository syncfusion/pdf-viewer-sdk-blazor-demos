#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using BlazorDemos.Pages.Viewer.PdfViewer;
using BlazorDemos.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.SfPdfViewer;
using Syncfusion.Blazor.SplitButtons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDemos.Pages.Viewer
{
    public partial class CustomToolBarBase : SampleBaseComponent, IDisposable
    {
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }
        internal string DocumentPath { get; set; } = string.Empty;
        internal SfPdfViewer2? ViewerInstance;
        internal DotNetObjectReference<CustomToolbar>? dotnetObjectRef { get; set; }


        #region binded values
        internal SfTooltip? Sftooltip;
        internal SfTextBox? Sftextbox;
        internal SfUploader? uploadFiles;
        internal PdfViewerContextMenuSettings contextMenu { get; set; } = new PdfViewerContextMenuSettings();
        internal SfDropDownButton? FontColorDropdown;
        internal SfDropDownButton? FillColorDropdown;
        internal SfDropDownButton? StrokeColorDropdown;
        internal SfDropDownButton? ColorDropdown;
        internal bool IsFreeTextTool;
        internal bool IsShapeTool;
        internal bool IsHighlightTool;
        internal bool IsInkTool;
        internal bool IsImageTool;
        internal bool IsVisibleDialog;
        internal bool IsAnnotationSelected;
        internal bool IsBold;
        internal bool IsItalic;
        internal bool IsUnderline;
        internal bool IsStrikethrough { get; set; }
        internal bool IsToolbarIconClicked;
        internal bool isToolTipOpened;
        internal PdfViewerFreeTextSettings FreeAnnotationSetting { get; set; } = new PdfViewerFreeTextSettings();
        internal PdfViewerRectangleSettings RectangleAnnoSettings { get; set; } = new PdfViewerRectangleSettings();
        internal PdfViewerLineSettings LineAnnoSettings { get; set; } = new PdfViewerLineSettings();
        internal PdfViewerCircleSettings CircleAnnoSettings { get; set; } = new PdfViewerCircleSettings();
        internal PdfViewerInkAnnotationSettings InkAnnoSettings { get; set; } = new PdfViewerInkAnnotationSettings();
        internal PdfViewerCustomStampSettings CustomStampSettings { get; set; } = new PdfViewerCustomStampSettings();
        internal PdfViewerHighlightSettings HightLightSettings { get; set; } = new PdfViewerHighlightSettings();
        internal List<PdfViewerCustomStamp>? CustomStamps;
        internal InteractionMode ViewerInteractionMode { get; set; } = InteractionMode.TextSelection;
        internal Syncfusion.Blazor.SfPdfViewer.PdfAnnotation annotation = new Syncfusion.Blazor.SfPdfViewer.PdfAnnotation();
        internal string FontFamilyName { get; set; } = "Helvetica";
        internal string FontSize { get; set; } = "16px";
        internal string[] FontSizeList { get; set; } = { "8px", "10px", "12px", "16px", "22px", "36px", "48px", "72px", "96px" };
        internal int ShapeThickness { get; set; } = 1;
        internal string FillColor { get; set; } = "";
        internal string Color { get; set; } = "";
        internal string StrokeColor { get; set; } = "";
        internal string FontColor { get; set; } = "#000000";
        internal string CustomStampSource = "";
        internal string ShapeType { get; set; } = "";
        public bool PreviousPageDisable { get; set; } = true;
        public bool NextPageDisable { get; set; } = true;
        public bool PrintDisabled { get; set; } = true;
        public bool DownloadDisabled { get; set; } = true;
        public int TotalPages { get; set; }
        public int CurrentPageNumber { get; set; }
        public bool IsChecked { get; set; }
        public string SearchIconCss { get; set; } = "e-icons e-search";
        public string Text { get; set; } = "";
        public string ZoomValue { get; set; } = "100%";
        internal bool IsZoomValueSelected;
        internal bool IsDropdownSelected;
        internal bool IsImageSelected = true;
        internal bool ZoomInVisible;
        internal bool ZoomOutVisible;
        internal List<ImageDetails> Images = new List<ImageDetails>();
        private IJSObjectReference? module;
        private bool _disposed;
        #endregion

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                string jsPath = $"./{SampleService.AssetsPath}Pages/Viewer/PdfViewer/CustomToolbar.razor.js";
                module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", jsPath).ConfigureAwait(true);
                //Replace with your js file path. For Example: "./Components/Pages/CustomToolbar.razor.js".
                await module.InvokeVoidAsync("getInstance", dotnetObjectRef).ConfigureAwait(true);
                _ = module.InvokeVoidAsync("closeTooltipPopup", "CloseToolTip").AsTask();
                await module.InvokeVoidAsync("created").ConfigureAwait(true);
            }
        }
        // Synchronous dispose for managed IDisposable fields
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
       
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                if (module != null)
                {
                    _= module.InvokeVoidAsync("disposeDotnetTooltipRef").AsTask();
                }
                dotnetObjectRef?.Dispose();
                dotnetObjectRef = null;
                // Dispose your annotation‐settings objects:
                contextMenu?.Dispose();
                CircleAnnoSettings?.Dispose();
                LineAnnoSettings?.Dispose();
                FreeAnnotationSetting?.Dispose();
                RectangleAnnoSettings?.Dispose();
                HightLightSettings?.Dispose();
                InkAnnoSettings?.Dispose();
                CustomStampSettings?.Dispose();
            }
            _disposed = true;
        }


        //Class for the dropdown value
        internal sealed class AnnotationProperties
        {
            public string? ID { get; set; }
            public string? Text { get; set; }
        }
        internal List<AnnotationProperties> FontFamilyList = new List<AnnotationProperties>()
        {
            new AnnotationProperties(){ ID= "Helvetica", Text= "Helvetica" },
            new AnnotationProperties(){ ID= "Courier", Text= "Courier" },
            new AnnotationProperties(){ ID= "Symbol", Text= "Symbol" },
            new AnnotationProperties(){ ID= "Times New Roman", Text= "Times New Roman"}
        };
        internal  List<AnnotationProperties> ZoomList = new List<AnnotationProperties>()
        {
            new AnnotationProperties(){ ID= "10%", Text= "10%" },
            new AnnotationProperties(){ ID= "25%", Text= "25%" },
            new AnnotationProperties(){ ID= "50%", Text= "50%" },
            new AnnotationProperties(){ ID= "75%", Text= "75%" },
            new AnnotationProperties(){ ID= "100%", Text= "100%" },
            new AnnotationProperties(){ ID= "125%", Text= "125%" },
            new AnnotationProperties(){ ID= "150%", Text= "150%" },
            new AnnotationProperties(){ ID= "200%", Text= "200%" },
            new AnnotationProperties(){ ID= "400%", Text= "400%" },
            new AnnotationProperties(){ ID= "Automatic", Text= "Automatic" },
            new AnnotationProperties(){ ID= "Fit Height", Text= "Fit Height" },
            new AnnotationProperties(){ ID= "Fit Width", Text= "Fit Width" },
            new AnnotationProperties(){ ID= "Fit Page", Text= "Fit Page" },
        };
        internal List<CustomMenuItem> MenuItems = new List<CustomMenuItem>
        {
            new CustomMenuItem { Id = "Dynamic", Text = "Dynamic", Items = new List<CustomMenuItem>
            {
                new CustomMenuItem { Id = "DApproved", Text = "Approved"},
                new CustomMenuItem { Id = "DNotApproved", Text = "Not Approved"},
                new CustomMenuItem { Id = "DConfidential", Text = "Confidential" },
                new CustomMenuItem { Id = "DReceived", Text = "Received" },
                new CustomMenuItem { Id = "DReviewed", Text = "Reviewed" },
                new CustomMenuItem { Id = "DRevised", Text = "Revised" }
            } },
            new CustomMenuItem { Id = "SignHere", Text = "Sign Here", Items = new List<CustomMenuItem>
            {
                new CustomMenuItem { Id = "Accepted", Text = "Accepted" },
                new CustomMenuItem { Id = "InitialHere", Text = "Initial Here" },
                new CustomMenuItem { Id = "Rejected", Text = "Rejected"},
                new CustomMenuItem { Id = "Sign Here", Text = "Sign Here" },
                new CustomMenuItem { Id = "Witness", Text = "Witness" },
            } },
            new CustomMenuItem { Id = "StandardBusiness", Text = "Standard Business", Items = new List<CustomMenuItem>
            {
                new CustomMenuItem { Id = "Approved", Text = "Approved" },
                new CustomMenuItem { Id = "Completed", Text = "Completed"},
                new CustomMenuItem { Id = "Confidential", Text = "Confidential" },
                new CustomMenuItem { Id = "Draft", Text = "Draft"},
                new CustomMenuItem { Id = "Final", Text = "Final" },
                new CustomMenuItem { Id = "ForComment", Text = "For Comment" },
                new CustomMenuItem { Id = "ForPublicRelease", Text = "For Public Release" },
                new CustomMenuItem { Id = "InformationOnly", Text = "Information Only" },
                new CustomMenuItem { Id = "NotApproved", Text = "Not Approved" },
                new CustomMenuItem { Id = "NotForPublicRelease", Text = "Not For Public Release" },
                new CustomMenuItem { Id = "PreliminaryResults", Text = "Preliminary Results"},
                new CustomMenuItem { Id = "Void", Text = "Void" }
            } },
        };
        internal SfContextMenu<CustomMenuItem>? ContextMenuItem;
        //Method for the oen the context menu for the stamp
        internal void OnMenuCreated()
        {
            ContextMenuItem!.OpenAsync();
        }
        internal sealed class CustomMenuItem
        {
            public List<CustomMenuItem>? Items { get; set; }
            public string? Id { get; set; }
            public string? Text { get; set; }
        }
        //Method for handle the stamp annotation type set to the mode
        internal async void StampSelect(MenuEventArgs<CustomMenuItem> args)
        {
            if (args.Item != null)
            {
                RefreshButtons();
                await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.None).ConfigureAwait(true);
                if (args.Item.Id != "Stamp" && args.Item.Id != "Dynamic" && args.Item.Id != "SignHere" && args.Item.Id != "StandardBusiness")
                {
                    switch (args.Item.Id)
                    {
                        case "DApproved":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, DynamicStampItem.Approved, null, null).ConfigureAwait(true);
                            break;
                        case "DNotApproved":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, DynamicStampItem.NotApproved, null, null).ConfigureAwait(true);
                            break;
                        case "DConfidential":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, DynamicStampItem.Confidential, null, null).ConfigureAwait(true);
                            break;
                        case "DReceived":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, DynamicStampItem.Received, null, null).ConfigureAwait(true);
                            break;
                        case "DReviewed":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, DynamicStampItem.Reviewed, null, null).ConfigureAwait(true);
                            break;
                        case "DRevised":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, DynamicStampItem.Revised, null, null).ConfigureAwait(true);
                            break;
                        case "Accepted":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, SignStampItem.Accepted, null).ConfigureAwait(true);
                            break;
                        case "InitialHere":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, SignStampItem.InitialHere, null).ConfigureAwait(true);
                            break;
                        case "Rejected":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, SignStampItem.Rejected, null).ConfigureAwait(true);
                            break;
                        case "Sign Here":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, SignStampItem.SignHere, null).ConfigureAwait(true);
                            break;
                        case "Witness":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, SignStampItem.Witness, null).ConfigureAwait(true);
                            break;
                        case "Approved":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.Approved).ConfigureAwait(true);
                            break;
                        case "Completed":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.Completed).ConfigureAwait(true);
                            break;
                        case "Confidential":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.Confidential).ConfigureAwait(true);
                            break;
                        case "Draft":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.Draft).ConfigureAwait(true);
                            break;
                        case "Final":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.Final).ConfigureAwait(true);
                            break;
                        case "ForComment":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.ForComment).ConfigureAwait(true);
                            break;
                        case "ForPublicRelease":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.ForPublicRelease).ConfigureAwait(true);
                            break;
                        case "PreliminaryResults":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.PreliminaryResults).ConfigureAwait(true);
                            break;
                        case "NotApproved":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.NotApproved).ConfigureAwait(true);
                            break;
                        case "NotForPublicRelease":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.NotForPublicRelease).ConfigureAwait(true);
                            break;
                        case "Void":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.Void).ConfigureAwait(true);
                            break;
                        case "InformationOnly":
                            await ViewerInstance.SetAnnotationModeAsync(AnnotationType.Stamp, null, null, StandardBusinessStampItem.InformationOnly).ConfigureAwait(true);
                            break;
                    }
                }
            }

        }
        //Method for Identify the annotation shape from the dropdown
        internal async Task ShapeSelected(MenuEventArgs args)
        {
            RefreshButtons();
            IsShapeTool = true;
            if (args != null)
            {
                switch (args.Item.Text)
                {
                    case "Line":
                        ShapeType = "Line";
                        await AddLine().ConfigureAwait(true);
                        break;
                    case "Rectangle":
                        ShapeType = "Rectangle";
                        await AddRectangle().ConfigureAwait(true);
                        break;
                    case "Circle":
                        ShapeType = "Circle";
                        await AddCircle().ConfigureAwait(true);
                        break;
                }
                IsToolbarIconClicked = true;
            }
        }
        //Method for Identify the icon from the dropdown
        internal async Task IconSelected(MenuEventArgs args)
        {
            RefreshButtons();
            if (args != null)
            {
                switch (args.Item.Text)
                {
                    case "Check":
                        CustomStampSource = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAKtSURBVHjatFZbSBRRGP7OmduyFzPylqJhLFsYPfkURCERPthbBPXQQxZhvnQjgkAQJCJ8iCK6GBjUY0EFKV0fAh97KIQotRBMrVU313Fm3Jk553R2TVlos7XGA8Nc+Of7vv+f7//PECEE1nJRrPFS828IIf+GcgTlJIQBwVEPgi7cQtdSZQLJQIL3GJVqfaxR1yDQkSUMrkTHsZOoZE90s64JL8uGNOaRCobgABSqkN7SeChGKcHCqG+BoxMPwIIh2IBWLUyrIxUafJODmdzBD9wJxkXtiFJCLlc0RCJZc5gjmXnBcE6qdwMhoIyej5brRqhERSblw7dFCrO4v6JNV6G+CkKcqdoaDcuPitTwgil8cSq/9v+VgQJ6sWxTWDHCKqykC5bhE7Lyj4Lp5KOIA+JQdSJmENlLyU+WxZlUX0wn5xpEQZ2kddGDwYIvGORaTaJE13UFya8WmCdGcRvPiiKgBnkn/VxpGIrttPmaqtA+12N3JVlfLuAYGkHJ7pp4icKk4Sc+mln1p4ueRTK4hVDyPJEoK23YVqYPDaf2Dw9N7zXbPY/5vFPVlMPx7etDqkoxM26Du3xcqn+54hjJH9e5YScdooH2b6yObdnVVBfm8tH3pIUPg1NWenZB2dFcG/LlVHv7aty05rxWSfCwEPAS7u8EiyNA1yuVe5GItq+peXNE1Qk8xuBynjtPJ228H/g2yaZEbSFr5hMUdpHsRvc6O2jOuVf6nwzZju3nyLPBsnsxNpK2BBfdfwIv2qb+Td7hevzCi6efbcdaJHEzHDOTDpHJ9Aayo/Eb/KqX4Wdf939xXIdhYnSOSyM8ls5KF7VXFPwGhZS005O6Ti/JcJ6x/RbZuW9WAl7GzV4sHX9N9wS6aRsZK0b5MuZqCH7tYOtWQ0DW+rflpwADAPxaPdG3Dy+cAAAAAElFTkSuQmCC";
                        break;
                    case "Cross":
                        CustomStampSource = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAANoSURBVHja1JZdaBRnFIafb/anm50katQoFaShZROlFkUERS+MemVR2iuRImo1aeuNtvd70yh65ZUYdJOApYh6o6BQWgp6oRRCSwR/WqM1NZpofvxJsjub/Zuv58u4y2bdxCh44cDHzHw7877nvOc9Z1ZprXmXh8U7Pvz5C6UUMdhgWZzOuiwI+ji1O8eutwUuKGMuzGqFRe0W8b4IOrMCfT5MvMNH+0zA2iwOxhSpDotbbVBTjFuQyAcr59nkPqyStGR3cwS7MsA2IT00HXjMolWeO/B5hOB8mwafNTnrAkEO/hpO4B9NefciEVvqsUN+9gvJD1OBVwXYsb4OW4BJZARQ0fOKVnm9JNV9PwVIjC2TrZXein+GPuXHafOxsxT8zAfEBxrQQ0vQv81G/xzgZinuJBftdTmeyRE9/w9OPO3t2QHYWk+FJNR6Ui6LI2+UyCViup9C/xhPJIO1pVmqfPTGRfnDSBLw0fJlA+HKILjyyGACfrlLIqe5XBWkMQ/eNwZd/YyOuyz9Tm5LXVSWoJjki3rCFZJFzpUo49DzHGdZLWEDPpyEq72k5LdV38CNcjadkqBAYtEiDglLsZHoJ4jMMma48h/jrsv2vXBhqj6YtpP3uBzNuEQvdeNkXgJPkMjq7CMuzjtYDvyNRoVgLZHotUQ6UYv8eVE1FUqzrwPmvzWB2DYmBd2+QQpqEi5kIOfFs/DJqnUVV0/ArDcmKAY3Bc0K8LVenF/vkTQNZYjq5uCvreQjv+L3o1AxY4I8+EbTocqTpPMh40+TdGY0P/7xECeR9kg+nkOwOsSn1YqL52QAvJagGNxEbkC6HpMacPh7VLO5yeVIWhP9sx8nmfbIP6khZAdZ/UJxdloCseXhArjlgd8eItM7wiPRvVEGUtI812zcJSRdA0KS8UjqZmOH/WySadpetpOPK7UwZNGzNULI7/MKee8Z2VtDDCqX5V/DUGl0J6VPpD1aIvMIGynT8s79ZzLzcnzfpHVsUgaCWSOSuGYqGvAHI7g3BhmTj8+acuD5TLIQvTOMk8p6np4bxpae3fGKRN+KGqaW0p2ORM31J4yKROtkBPROZ2VDIg0X/VdGiCNyJVKkhKi77LAzLngu78htvey2GtKZfiJl0poPzTEUVwTyq2atRyYRvLf/Kv4XYAAnipiOY10VpQAAAABJRU5ErkJggg==";
                        break;
                    case "Star":
                        CustomStampSource = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAACXBIWXMAAAsTAAALEwEAmpwYAAACiklEQVR4nO2aTYhOURjHf6/P2WBQvjdKI58LJKVeG0XZkF4lTMrHAjVsSBaKlCxoFpRSmLITyUT5WJAM05SFj1BjQT7KAs0YGmOurp5br9v7Xue8c58z58S//st7zvO7H895znMu/JeR6oA24DZQIGCtByJxkYB1owzkLIFqGtBXBtINjCZAHSyDSLyVwFQAOiuA3CcwLa8AkXg2AelCBsgxAtEY4GsGyAdgOAFoZwZE4tUEoA4DkCt4rnkGELF/AJPxWM2GILH34qlGAB8tQF74Wkius4BIvBQPdb0GkDOug6yXInAWsFhW7rXAZmAXcCBVIJq6C9gH7AAagTUy9iJgJjDVptDcBrQAl4Cbkj6fA2+BLzUEp+XPwBvgGdAusV4EzgElpLyOAndnDLJSaAc7mIE8qRXJ6zUDeOpBUJGlX8p3+4dGAZc9CC4y9DVJQhVVkCzy04NAoyruB44CQ0wyWcnTJPAN2Iil5gOvPAg+Er8GFlKjxgO3PIC4C0xggBom7+RgQZzOe0e5AehxCPAd2IKSFsi7qg3xDliCsqZIraMF0S5zONEJRZDjONQTRZDHriAmyeqqBdLvqjHR6OBj3+QCpMUByHltiIKkRm2Q99odlrkOICLxHE2Q3Q5BmjRBWi2D6QWOiHstr23V7CR2WQTyKFV2x33hhxbXdwMjNUCWGQbQI7vMoRXGiHd12y1uSFED5LDBxHeABoOxpqeOrqv5kAbIg4wJP8mdtk2Zpb80vtvyhqjPaIleldZmrZqYscj2AWNz5Pjdk620aMX937y0qspeJ9djulOpoi6+g+PQOUhtTrWjTuY5wb2ywxkXP8cUZa7cfzKIB94vvy25Uh2wR9L+v6NfqcfPxTBhRuQAAAAASUVORK5CYII=";
                        break;
                }
                IsToolbarIconClicked = true;
                await AddStamp().ConfigureAwait(true);
            }
        }
    }
}
