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
using System.Globalization;
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
                    await Sftooltip!.CloseAsync().ConfigureAwait(true);
                }
                // Switch case based on the CSS class of the clicked item
                switch (args.Item.CssClass)
                {
                    // Case when the 'Select' tool is clicked
                    case "e-pv-select-container":
                        RefreshButtons();
                        // Set the interaction mode to 'TextSelection'
                        ViewerInteractionMode = InteractionMode.TextSelection;
                        await ResetMode().ConfigureAwait(true);
                        break;
                    case "e-pv-zoom-in-container":
                        //Zoom in  the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.ZoomInAsync().ConfigureAwait(true);
                        zoomPercentage = await ViewerInstance.GetZoomPercentageAsync().ConfigureAwait(true);
                        ZoomValue = zoomPercentage.ToString(CultureInfo.CurrentCulture) + "%";
                        break;
                    case "e-pv-zoom-out-container":
                        //Zoom out the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.ZoomOutAsync().ConfigureAwait(true);
                        zoomPercentage = await ViewerInstance.GetZoomPercentageAsync().ConfigureAwait(true);
                        ZoomValue = zoomPercentage.ToString(CultureInfo.CurrentCulture) + "%";
                        break;
                    case "e-pv-fit-page-container":
                        //Fit page the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.FitToPageAsync().ConfigureAwait(true);
                        zoomPercentage = await ViewerInstance.GetZoomPercentageAsync().ConfigureAwait(true);
                        ZoomValue = zoomPercentage.ToString(CultureInfo.CurrentCulture) + "%";
                        break;
                    // Case when the 'Free text' tool is clicked
                    case "e-pv-font-container":
                        RefreshButtons();
                        IsFreeTextTool = true;
                        await AddFreeText().ConfigureAwait(true);
                        break;
                    case "e-pv-print-container":
                        //Print the PDF document being loaded in the PDFViewer2.
                        await ViewerInstance!.PrintAsync().ConfigureAwait(true);
                        break;
                    // Case when the 'Image' tool is clicked
                    case "e-pv-image-container":
                        RefreshButtons();
                        IsVisibleDialog = true;
                        IsImageTool = true;
                        await ResetMode().ConfigureAwait(true);
                        break;
                    // Case when the 'Signature' tool is clicked
                    case "e-pv-signature-container":
                        RefreshButtons();
                        //set the mode to signature annotation
                        await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.HandWrittenSignature).ConfigureAwait(true);
                        break;
                    // Case when the 'Ink annotation' tool is clicked
                    case "e-pv-ink-container":
                        RefreshButtons();
                        if (IsInkTool)
                        {
                            ViewerInteractionMode = InteractionMode.TextSelection;
                        }
                        else
                        {
                            IsInkTool = true;
                            ShapeType = "Ink";
                            await AddInk().ConfigureAwait(true);
                        }
                        break;
                    // Case when the 'Highlight' tool is clicked
                    case "e-pv-highlight-container":
                        RefreshButtons();
                        IsHighlightTool = true;
                        ShapeType = "Heighlight";
                        await AddHighlight().ConfigureAwait(true);
                        break;
                    // Case when the 'Previous' button is clicked
                    case "e-pv-previous-container":
                        // Go to the previous page
                        await ViewerInstance!.GoToPreviousPageAsync().ConfigureAwait(true);
                        break;
                    // Case when the 'Next' button is clicked
                    case "e-pv-next-container":
                        // Go to the next page
                        await ViewerInstance!.GoToNextPageAsync().ConfigureAwait(true);
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

                        await ModifiedStyle().ConfigureAwait(true);
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

                        await ModifiedStyle().ConfigureAwait(true);
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
                        await ModifiedStyle().ConfigureAwait(true);
                        break;
                    case "e-pv-search-container":
                        if (isToolTipOpened)
                        {
                            await Sftooltip!.CloseAsync().ConfigureAwait(true);
                        }
                        else
                        {
                            await Sftooltip!.OpenAsync().ConfigureAwait(true);
                        }
                        isToolTipOpened = !isToolTipOpened;
                        break;
                }
            }
        }

        //Method for applying download the document 
        internal async Task DownloadDocument()
        {
            await ViewerInstance!.DownloadAsync().ConfigureAwait(true);
            // Refresh the buttons
            RefreshButtons();
        }
    }

}
