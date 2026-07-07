using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.SfPdfViewer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemos.Pages.Viewer
{
    public partial class CustomToolBarBase
    {
        //Method for handle the Zoom change
        internal void ZoomChanged(ZoomChangeEventArgs args)
        {
            if (args.CurrentZoomValue <= 10)
            {
                ZoomOutVisible = true;
            }
            else if (args.CurrentZoomValue >= 400)
            {
                ZoomInVisible = true;
            }
            else
            {
                ZoomOutVisible = false;
                ZoomInVisible = false;
            }
            ZoomValue = args.CurrentZoomValue.ToString(CultureInfo.CurrentCulture) + "%";
        }

        //Method for Zoom value change in the pdf viewer
        internal async void ZoomValueChange(ChangeEventArgs<string, AnnotationProperties> args)
        {
            if (args.Value != null || args.PreviousItemData != null)
            {
                if (args.Value == "Automatic" || args.Value == "Fit Height" || args.Value == "Fit Width" || args.Value == "Fit Page")
                {
                    IsZoomValueSelected = false;
                    switch (args.Value)
                    {
                        case "Automatic":
                            await ViewerInstance!.ZoomAsync(100).ConfigureAwait(true);
                            break;
                        case "Fit Height":
                            await ViewerInstance!.FitToHeightAsync().ConfigureAwait(true);
                            break;
                        case "Fit Width":
                            await ViewerInstance!.FitToWidthAsync().ConfigureAwait(true);
                            break;
                        case "Fit Page":
                            await ViewerInstance!.FitToPageAsync().ConfigureAwait(true);
                            break;
                    }
                    int zoom = await ViewerInstance!.GetZoomPercentageAsync().ConfigureAwait(true);
                    ZoomValue = zoom.ToString(CultureInfo.CurrentCulture) + "%";                  
                    StateHasChanged();

                }
                else
                {
                    if (IsZoomValueSelected)
                    {

                        string zoomText = args.Value!.Replace("%", "", StringComparison.Ordinal).Trim();
                        int zoom = int.Parse(zoomText, NumberStyles.Integer, CultureInfo.InvariantCulture);
                        await ViewerInstance!.ZoomAsync(zoom).ConfigureAwait(true);
                        IsZoomValueSelected = false;
                    }

                }
            }

        }
        //Method for set the true value to the when the zoom drop down is opened
        internal void ZoomValueSelected()
        {
            IsZoomValueSelected = true;
        }
        //Triggers when there is change in the current page number.
        internal void OnPageChanged(PageChangeEventArgs args)
        {
            CurrentPageNumber = (int)args.CurrentPageNumber;
            if (args.CurrentPageNumber == TotalPages)
            {
                NextPageDisable = true;
                PreviousPageDisable = false;
            }
            if (args.CurrentPageNumber < TotalPages && args.CurrentPageNumber != 1)
            {
                PreviousPageDisable = false;
                NextPageDisable = false;
            }
            if (args.CurrentPageNumber == 1)
            {
                NextPageDisable = false;
                PreviousPageDisable = true;
            }
        }

        //Triggers when the textbox is created.
        internal async void AddIcon()
        {
            await Sftextbox!.AddIconAsync("append", "e-icons e-search").ConfigureAwait(true);
        }

        //Function to close tooltip popup while scrolling
        [JSInvokable]
        public void CloseToolTip()
        {
            Sftooltip!.CloseAsync();
        }

        //Triggers when the search tooltip close.
        public async void SearchClose()
        {
            await ViewerInstance!.CancelTextSearchAsync().ConfigureAwait(true);
            Text = "";
        }

        //Triggers when match case checkbox is clicked.
        internal async void MatchCaseChanged(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            IsChecked = (bool)args.Value!;
            await ViewerInstance!.CancelTextSearchAsync().ConfigureAwait(true);
            if (Text != "") {
                if (!IsChecked)
                {
                    //Searches the target text in the PDF document and highlights the occurrences in the PDF pages.
                    await ViewerInstance!.SearchTextAsync(Text, false).ConfigureAwait(true);
                }
                else
                {
                    await ViewerInstance!.SearchTextAsync(Text, true).ConfigureAwait(true);
                }
            }
        }
        internal void OnInputText(Syncfusion.Blazor.Inputs.InputEventArgs args) {
            Text = args.Value;
        }

        //Triggers when search icon is clicked.
        internal async void SearchText(string args)
        {
            Text = args;
            await ViewerInstance!.CancelTextSearchAsync().ConfigureAwait(true);
            if (IsChecked)
            {
                //Searches the target text in the PDF document and highlights the occurrences in the PDF pages.
                await ViewerInstance!.SearchTextAsync(args, true).ConfigureAwait(true);
            }
            else
            {
                await ViewerInstance!.SearchTextAsync(args, false).ConfigureAwait(true);
            }
        }

        //Triggers when next button is clicked.
        internal async void NextClick()
        {
            //Searches the next occurrence of the searched text from the current occurrence of the SfPdfViewer.
            await ViewerInstance!.SearchNextAsync().ConfigureAwait(true);
        }

        //Triggers when previous button is clicked.
        internal async void PreviousClick()
        {
            //Searches the previous occurrence of the searched text from the current occurrence of the SfPdfViewer.
            await ViewerInstance!.SearchPreviousAsync().ConfigureAwait(true);
        }

        //Triggers when the value of the numeric textbox changes.
        public async void GoToPage(Syncfusion.Blazor.Inputs.ChangeEventArgs<int> args)
        {
            if (args != null)
            {
                int currentValue = args.Value;
                if (args.Event != null)
                {
                    Microsoft.AspNetCore.Components.ChangeEventArgs? changeEventArgs = args.Event as Microsoft.AspNetCore.Components.ChangeEventArgs;
                    if (changeEventArgs != null && changeEventArgs.Value != null)
                    {
                        currentValue = int.Parse(changeEventArgs.Value.ToString()!, NumberStyles.Integer, CultureInfo.InvariantCulture);
                    }
                }
                if (currentValue == args.Value && args.Value != 0)
                {
                    CurrentPageNumber = args.Value;
                    //Navigate to given page number in loaded document of the PDFViewer2.
                    await ViewerInstance!.GoToPageAsync(args.Value).ConfigureAwait(true);
                }
            }
            else
            {
                CurrentPageNumber = ViewerInstance!.CurrentPageNumber;
            }
        }
        internal async Task TooltipOpen()
        {
            if (module != null)
            {
                await module.InvokeVoidAsync("mapSearchValue", ViewerInstance!.ID).ConfigureAwait(true);
            }
            isToolTipOpened = true;
        }
        internal void TooltipClosed()
        {
            isToolTipOpened = false;
        }
    }
}
