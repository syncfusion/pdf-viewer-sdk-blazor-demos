#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.JSInterop;
using Syncfusion.Blazor.SfPdfViewer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemos.Pages.Viewer
{
    public partial class CustomToolBarBase
    {
        //Method for handle the Toolbar items click
        internal async Task OnToolbarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            int zoomPercentage = 0;
            if (args.Item != null)
            {
                if (isToolTipOpened)
                {
                    await Sftooltip!.CloseAsync();
                }
                // Switch case based on the CSS class of the clicked item
                switch (args.Item.CssClass)
                {
                    // Case when the 'Select' tool is clicked
                    case "e-pv-select-container":
                        RefreshButtons();
                        // Set the interaction mode to 'TextSelection'
                        ViewerInstance!.InteractionMode = InteractionMode.TextSelection;
                        await ResetMode();
                        break;
                    case "e-pv-zoom-in-container":
                        //Zoom in  the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.ZoomInAsync();
                        zoomPercentage = await ViewerInstance.GetZoomPercentageAsync();
                        ZoomValue = zoomPercentage.ToString() + "%";
                        break;
                    case "e-pv-zoom-out-container":
                        //Zoom out the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.ZoomOutAsync();
                        zoomPercentage = await ViewerInstance.GetZoomPercentageAsync();
                        ZoomValue = zoomPercentage.ToString() + "%";
                        break;
                    case "e-pv-fit-page-container":
                        //Fit page the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.FitToPageAsync();
                        zoomPercentage = await ViewerInstance.GetZoomPercentageAsync();
                        ZoomValue = zoomPercentage.ToString() + "%";
                        break;
                    // Case when the 'Free text' tool is clicked
                    case "e-pv-font-container":
                        RefreshButtons();
                        IsFreeTextTool = true;
                        await AddFreeText();
                        break;
                    case "e-pv-print-container":
                        //Print the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.PrintAsync();
                        break;
                    // Case when the 'Image' tool is clicked
                    case "e-pv-image-container":
                        RefreshButtons();
                        IsVisibleDialog = true;
                        IsImageTool = true;
                        await ResetMode();
                        break;
                    // Case when the 'Signature' tool is clicked
                    case "e-pv-signature-container":
                        RefreshButtons();
                        //set the mode to signature annotation
                        await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.HandWrittenSignature);
                        break;
                    // Case when the 'Ink annotation' tool is clicked
                    case "e-pv-ink-container":
                        RefreshButtons();
                        if (IsInkTool)
                        {
                            ViewerInstance!.InteractionMode = InteractionMode.TextSelection;
                        }
                        else
                        {
                            IsInkTool = true;
                            ShapeType = "Ink";
                            await AddInk();
                        }
                        break;
                    // Case when the 'Highlight' tool is clicked
                    case "e-pv-highlight-container":
                        RefreshButtons();
                        IsHighlightTool = true;
                        ShapeType = "Heighlight";
                        await AddHighlight();
                        break;
                    // Case when the 'Previous' button is clicked
                    case "e-pv-previous-container":
                        // Go to the previous page
                        await ViewerInstance!.GoToPreviousPageAsync();
                        break;
                    // Case when the 'Next' button is clicked
                    case "e-pv-next-container":
                        // Go to the next page
                        await ViewerInstance!.GoToNextPageAsync();
                        break;
                    case "e-pv-bold-container":
                    case "e-pv-bold-container e-select-icon":
                        if (IsBold)
                        {
                            IsBold = false;
                        }
                        else
                        {
                            IsBold = true;
                        }

                        await ModifiedStyle();
                        break;
                    case "e-pv-italic-container":
                    case "e-pv-italic-container e-select-icon":
                        if (IsItalic)
                        {
                            IsItalic = false;
                        }
                        else
                        {
                            IsItalic = true;
                        }

                        await ModifiedStyle();
                        break;
                    case "e-pv-underline-container":
                    case "e-pv-underline-container e-select-icon":
                        if (IsUnderline)
                        {
                            IsUnderline = false;
                        }
                        else
                        {
                            IsUnderline = true;
                        }
                        await ModifiedStyle();
                        break;
                    case "e-pv-search-container":
                        if (isToolTipOpened)
                        {
                            await Sftooltip!.CloseAsync();
                        }
                        else
                        {
                            await Sftooltip!.OpenAsync();
                        }
                        isToolTipOpened = !isToolTipOpened;
                        break;
                }
            }
        }

        //Method for applying download the document 
        internal async Task DownloadDocument()
        {
            await ViewerInstance!.DownloadAsync();
            // Refresh the buttons
            RefreshButtons();
        }
    }

}
