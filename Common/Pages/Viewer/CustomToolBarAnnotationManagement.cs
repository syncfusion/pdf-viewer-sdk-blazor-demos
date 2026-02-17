#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.SfPdfViewer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemos.Pages.Viewer
{
    public partial class CustomToolBarBase
    {
        //Triggers while loading document in to the PDFViewer.
        internal void DocumentLoaded(LoadEventArgs args)
        {
            TotalPages = ViewerInstance!.PageCount;
            CurrentPageNumber = ViewerInstance.CurrentPageNumber;
            NextPageDisable = false;
            if (TotalPages == 1)
            {
                PreviousPageDisable = true;
                NextPageDisable = true;
            }
        }

        //Method for converting the Font style from the buttons
        public Syncfusion.Blazor.SfPdfViewer.FontStyle ConvertToFontStyle(bool isBold, bool isItalic, bool isUnderline, bool isStrikeout)
        {
            if (isBold)
            {
                if (isItalic)
                {
                    return isUnderline
                        ? Syncfusion.Blazor.SfPdfViewer.FontStyle.Bold | Syncfusion.Blazor.SfPdfViewer.FontStyle.Italic | Syncfusion.Blazor.SfPdfViewer.FontStyle.Underline
                        : Syncfusion.Blazor.SfPdfViewer.FontStyle.Bold | Syncfusion.Blazor.SfPdfViewer.FontStyle.Italic;
                }

                return isUnderline
                    ? Syncfusion.Blazor.SfPdfViewer.FontStyle.Bold | Syncfusion.Blazor.SfPdfViewer.FontStyle.Underline
                    : Syncfusion.Blazor.SfPdfViewer.FontStyle.Bold;
            }
            else
            {
                if (isItalic)
                {
                    return isUnderline
                        ? Syncfusion.Blazor.SfPdfViewer.FontStyle.Italic | Syncfusion.Blazor.SfPdfViewer.FontStyle.Underline
                        : Syncfusion.Blazor.SfPdfViewer.FontStyle.Italic;
                }

                return isUnderline
                    ? Syncfusion.Blazor.SfPdfViewer.FontStyle.Underline
                    : Syncfusion.Blazor.SfPdfViewer.FontStyle.None;
            }
        }

        //Method for add the free text annotation
        internal async Task AddFreeText()
        {
            FreeAnnotationSetting = new PdfViewerFreeTextSettings()
            {
                FontFamily = FontFamilyName,
                FontColor = FontColor,
                FontSize = Convert.ToDouble(FontSize.Replace("px", "")),
                FontStyle = ConvertToFontStyle(IsBold, IsItalic, IsUnderline, IsStrikethrough)
            };
            StateHasChanged();
            await ResetMode();
            IsToolbarIconClicked = true;
            //Set the mode for the Add free text annotation
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.FreeText);
        }
        //Method for add the Highlight annotation
        internal async Task AddHighlight()
        {
            HightLightSettings = new PdfViewerHighlightSettings()
            {
                Color = Color
            };
            StateHasChanged();
            await ResetMode();
            IsToolbarIconClicked = true;
            //Set the mode for the Highlight annotation
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.Highlight);
        }
        //Method for the Add the Line Annotation
        internal async Task AddLine()
        {
            LineAnnoSettings = new PdfViewerLineSettings()
            {
                StrokeColor = StrokeColor,
                Thickness = ShapeThickness,
            };
            StateHasChanged();
            await ResetMode();
            IsToolbarIconClicked = true;
            //Set the mode for the line annotation
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.Line);
        }
        //Method for the Add the Rectangle Annotation
        internal async Task AddRectangle()
        {
            RectangleAnnoSettings = new PdfViewerRectangleSettings()
            {
                StrokeColor = StrokeColor,
                Thickness = ShapeThickness,
                FillColor = FillColor

            };
            StateHasChanged();
            await ResetMode();
            IsToolbarIconClicked = true;
            //Set the mode for the Rectangle annotation
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.Rectangle);

        }
        //Method for the Add the Circle Annotation
        internal async Task AddCircle()
        {
            CircleAnnoSettings = new PdfViewerCircleSettings()
            {
                StrokeColor = StrokeColor,
                Thickness = ShapeThickness,
                FillColor = FillColor
            };
            StateHasChanged();
            await ResetMode();
            IsToolbarIconClicked = true;
            //Set the mode for the Circle annotation
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.Circle);

        }
        //Method for the Add the stamp Annotation
        internal async Task AddStamp()
        {
            CustomStamps = new List<PdfViewerCustomStamp>() { new PdfViewerCustomStamp { CustomStampImageSource = CustomStampSource } };
            if (IsImageTool)
            {
                CustomStampSettings = new PdfViewerCustomStampSettings();
            }
            else
            {
                CustomStampSettings = new PdfViewerCustomStampSettings()
                {
                    Height = 16,
                    Width = 16
                };
            }
            StateHasChanged();
            IsToolbarIconClicked = true;
            //Set the mode for the stamp annotation annotation
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.Stamp);
        }
        //Method for the Add the Ink Annotation
        internal async Task AddInk()
        {
            InkAnnoSettings = new PdfViewerInkAnnotationSettings()
            {
                Thickness = ShapeThickness,
                StrokeColor = StrokeColor
            };
            StateHasChanged();
            IsToolbarIconClicked = true;
            //Set the mode for the Ink annotation
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.Ink);
        }
        //Method for reset the annotation mode
        internal async Task ResetMode()
        {
            //Set the mode for the none
            await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.None);
        }
        //Method for Adding the annotation continuously 
        internal async Task OnAnnotationAdd(AnnotationAddEventArgs args)
        {

            List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation> pdfAnnotationList = new List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation>();
            pdfAnnotationList = await ViewerInstance!.GetAnnotationsAsync();
            int selectedAnnotationIndex = pdfAnnotationList.FindIndex(item => item.Id == args.AnnotationId);
            if (selectedAnnotationIndex != -1)
            {
                annotation = pdfAnnotationList[selectedAnnotationIndex];
            }
            if (annotation.Type == AnnotationType.FreeText)
            {
                IsAnnotationSelected = false;
            }
            else
            {
                IsAnnotationSelected = true;
            }
            IsToolbarIconClicked = false;

        }

        //Method for decide the annotation type After annotation properties changed
        internal async void OnAnnotationPropertiesChanged(AnnotationPropertiesChangeEventArgs args)
        {
            List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation> pdfAnnotationList = new List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation>();
            //Get the details about the annotations in the pdf viewer
            pdfAnnotationList = await ViewerInstance!.GetAnnotationsAsync();
            int selectedAnnotationIndex = pdfAnnotationList.FindIndex(item => item.Id == args.AnnotationId);
            if (selectedAnnotationIndex != -1)
            {
                annotation = pdfAnnotationList[selectedAnnotationIndex];
            }
        }
        //Method for decide the annotation type After annotation select
        internal async Task OnAnnotationSelect(AnnotationSelectEventArgs annotationSelectEventArgs)
        {
            List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation> pdfAnnotationList = new List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation>();
            //Get the details about the annotations in the pdf viewer
            pdfAnnotationList = await ViewerInstance!.GetAnnotationsAsync();
            int selectedAnnotationIndex = pdfAnnotationList.FindIndex(item => item.Id == annotationSelectEventArgs.AnnotationId);
            if (selectedAnnotationIndex != -1)
            {
                annotation = pdfAnnotationList[selectedAnnotationIndex];
            }
            if (annotation != null)
            {
                RefreshToolbarItems();
                RefreshIcon();
                IsAnnotationSelected = true;
                //Set the annotations type for update the annotation type after select the annotation
                if (annotation.Type == AnnotationType.FreeText)
                {
                    FontFamilyName = annotation.FontFamily;
                    FontColor = annotation.FontColor;
                    FontSize = annotation.FontSize.ToString() + "px";

                    if (annotation.FontStyle.ToString().Contains("Bold"))
                    {
                        IsBold = true;
                    }
                    if (annotation.FontStyle.ToString().Contains("Italic"))
                    {
                        IsItalic = true;
                    }
                    if (annotation.FontStyle.ToString().Contains("Underline"))
                    {
                        IsUnderline = true;
                    }
                    IsFreeTextTool = true;
                }
                else if (annotation.Type == AnnotationType.Line || annotation.Type == AnnotationType.Rectangle || annotation.Type == AnnotationType.Circle || annotation.Type == AnnotationType.Arrow || annotation.Type == AnnotationType.Polygon || annotation.Type == AnnotationType.Distance || annotation.Type == AnnotationType.Perimeter || annotation.Type == AnnotationType.Radius || annotation.Type == AnnotationType.Volume || annotation.Type == AnnotationType.Area)
                {
                    ShapeThickness = annotation.Thickness;
                    IsShapeTool = true;
                }
                else if (annotation.Type == AnnotationType.Ink)
                {
                    ShapeThickness = annotation.Thickness;
                    IsInkTool = true;
                }
                else if (annotation.Type == AnnotationType.Highlight || annotation.Type == AnnotationType.Strikethrough || annotation.Type == AnnotationType.Underline)
                {
                    IsHighlightTool = true;
                }
            }
        }
        //Method for the handle the annotation unselect 
        internal void OnAnnotationUnSelect()
        {
            RefreshIcon();
            IsAnnotationSelected = false;
            bool temp = IsHighlightTool;
            if (!IsToolbarIconClicked)
            {
                RefreshToolbarItems();
            }
            if (temp)
            {
                IsHighlightTool = true;
                ShapeType = "Heighlight";
            }
        }
        //Method for handle the free text annotation unselect
        public void ChangeInFile(DocumentEditedEventArgs args)
        {
            if (args.EditingAction == EditingAction.AnnotationAdded && IsFreeTextTool)
            {
                RefreshButtons();
            }
        }
        //Method for the set annotation when the annotation moved
        internal async Task OnAnnotationMoved(AnnotationMoveEventArgs annotationSelectEventArgs)
        {
            List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation> pdfAnnotationList = new List<Syncfusion.Blazor.SfPdfViewer.PdfAnnotation>();
            pdfAnnotationList = await ViewerInstance!.GetAnnotationsAsync();
            int selectedAnnotationIndex = pdfAnnotationList.FindIndex(item => item.Id == annotationSelectEventArgs.AnnotationId);
            if (selectedAnnotationIndex != -1)
            {
                annotation = pdfAnnotationList[selectedAnnotationIndex];
            }
        }

        //Method for the handle the annotation remove 
        internal void OnAnnotationRemove(AnnotationRemoveEventArgs args)
        {
            RefreshIcon();
            RefreshButtons();
            IsAnnotationSelected = false;
        }

        internal void RefreshIcon()
        {
            IsBold = false;
            IsUnderline = false;
            IsItalic = false;
        }
        //Refresh annotation select
        internal void ResetToolbarState()
        {
            ClosePopUp();
            IsFreeTextTool = false;
            IsShapeTool = false;
            IsHighlightTool = false;
            IsInkTool = false;
            IsImageTool = false;
            IsVisibleDialog = false;
            ShapeType = "";
            FillColor = "";
            FontColor = "";
            Color = "#ffff00";
            StrokeColor = "";
            ShapeThickness = 1;
            FontFamilyName = "Helvetica";
            FontSize = "16px";
            IsAnnotationSelected = false;
            IsToolbarIconClicked = false;
        }
        //Refresh toolbar Items
        internal void RefreshToolbarItems()
        {
            ResetToolbarState();
        }
        //Method for refresh the buttons in the toolbar
        internal void RefreshButtons()
        {
            ResetToolbarState();
            CustomStampSettings = new PdfViewerCustomStampSettings();
            InkAnnoSettings = new PdfViewerInkAnnotationSettings();
            LineAnnoSettings = new PdfViewerLineSettings();
            RectangleAnnoSettings = new PdfViewerRectangleSettings();
            LineAnnoSettings = new PdfViewerLineSettings();
            FreeAnnotationSetting = new PdfViewerFreeTextSettings();
        }
        //Method for the Font size is changing
        internal async Task FontSizeChange(ChangeEventArgs<string, string> args)
        {
            if (IsDropdownSelected)
            {
                if (IsAnnotationSelected)
                {
                    annotation.FontSize = int.Parse(FontSize.Replace("px", ""));
                    await ViewerInstance!.EditAnnotationAsync(annotation);
                }
                else
                {
                    if (IsFreeTextTool)
                    {
                        await AddFreeText();
                    }
                }
                IsDropdownSelected = false;
            }
        }
        //Method for the Font family changing
        internal async Task OnFontFamilyChange()
        {
            if (IsDropdownSelected)
            {
                if (IsAnnotationSelected)
                {
                    annotation.FontFamily = FontFamilyName;
                    await ViewerInstance!.EditAnnotationAsync(annotation);
                    IsDropdownSelected = false;
                }
                else
                {
                    if (IsFreeTextTool)
                    {
                        await AddFreeText();
                    }
                }
                IsDropdownSelected = false;
            }
        }
        //Method for the Font color changing
        internal async Task ColorChanged()
        {
            FontColorDropdown!.Toggle();
            if (IsAnnotationSelected)
            {
                annotation.FontColor = FontColor;
                await ViewerInstance!.EditAnnotationAsync(annotation);
            }
            else
            {
                await AddFreeText();
            }
        }
        //Method for change the font style
        internal async Task ModifiedStyle()
        {
            if (IsAnnotationSelected)
            {
                annotation.FontStyle = ConvertToFontStyle(IsBold, IsItalic, IsUnderline, IsStrikethrough);
                await ViewerInstance!.EditAnnotationAsync(annotation);
            }
            else
            {
                await AddFreeText();
            }
        }
        //Method for shape thickness changing
        internal async Task ThicknessChange()
        {
            if (IsAnnotationSelected)
            {
                annotation.Thickness = ShapeThickness;
                await ViewerInstance!.EditAnnotationAsync(annotation);
            }
            else
            {
                if (ShapeType == "Line")
                {
                    await AddLine();
                }
                else if (ShapeType == "Rectangle")
                {
                    await AddRectangle();
                }
                else if (ShapeType == "Circle")
                {
                    await AddCircle();
                }
                else if (ShapeType == "Ink")
                {
                    await AddInk();
                }
            }
        }
        //Method for shape stroke color changing
        internal async Task StrokeColorChanged()
        {
            StrokeColorDropdown!.Toggle();
            if (IsAnnotationSelected)
            {
                annotation.StrokeColor = StrokeColor;
                await ViewerInstance!.EditAnnotationAsync(annotation);
            }
            else
            {
                if (ShapeType == "Line")
                {
                    await AddLine();
                }
                else if (ShapeType == "Rectangle")
                {
                    await AddRectangle();
                }
                else if (ShapeType == "Circle")
                {
                    await AddCircle();
                }
                else if (ShapeType == "Ink")
                {
                    await AddInk();
                }
            }
        }
        //Method for shape fill color changing
        internal async Task FillColorChanged(ColorPickerEventArgs args)
        {
            FillColor = args.CurrentValue.Hex;
            FillColorDropdown!.Toggle();
            if (IsAnnotationSelected)
            {
                annotation.FillColor = FillColor;
                await ViewerInstance!.EditAnnotationAsync(annotation);
            }
            else
            {
                if (ShapeType == "Line")
                {
                    await AddLine();
                }
                else if (ShapeType == "Rectangle")
                {
                    await AddRectangle();
                }
                else if (ShapeType == "Circle")
                {
                    await AddCircle();
                }
            }
        }
        //Method for Highlight color changing
        internal async Task HighlightColorChanged(ColorPickerEventArgs args)
        {
            Color = args.CurrentValue.Hex;
            ColorDropdown!.Toggle();
            if (IsAnnotationSelected)
            {
                annotation.Color = Color;
                await ViewerInstance!.EditAnnotationAsync(annotation);
            }
            else
            {
                await ResetMode();
                StateHasChanged();
                //Set the mode for the Highlight annotation
                await ViewerInstance!.SetAnnotationModeAsync(AnnotationType.Highlight);
            }
        }

        //Method to set the dropdown selection flag to true when a font family is selected
        internal void FontFamilySelected()
        {
            IsDropdownSelected = true;
        }
        //Method to set the dropdown selection flag to true when a font size is selected
        internal void FontSizeSelected()
        {
            IsDropdownSelected = true;
        }
    }
}
