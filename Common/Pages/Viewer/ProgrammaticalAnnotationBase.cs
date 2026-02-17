#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using BlazorDemos.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.IO;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.SfPdfViewer;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemos.Pages.Viewer
{
    public partial class ProgrammaticalAnnotationBase: SampleBaseComponent
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        public SfPdfViewer2? PdfViewerInstance { get; set; }
        internal string DocumentPath { get; set; } = string.Empty;
        internal SfContextMenu<MenuItem>? ContextMenuObj { get; set; }
        internal string? SelectedItem { get; set; }
        internal SfUploader? uploadObj { get; set; }

    //Method for update the propertry value to the property panel
    internal async Task UpdatePropertiesInPropertyPanel(string SelectedAnnotationId)
    {
        List<PdfAnnotation> pdfAnnotationList = new List<PdfAnnotation>();
        PdfAnnotation annotation = new PdfAnnotation();
        pdfAnnotationList = await PdfViewerInstance!.GetAnnotationsAsync();
        selectedAnnotation.AnnotationId = SelectedAnnotationId;
        int selectedAnnotationIndex = pdfAnnotationList.FindIndex(item => item.Id == SelectedAnnotationId);
        if (selectedAnnotationIndex != -1)
        {
            annotation = pdfAnnotationList[selectedAnnotationIndex];
        }
        if (annotation != null)
        {
            if (annotation.Type.ToString() == "Image" || annotation.Type.ToString() == "Stamp")
            {
                if (annotation.Subject != null)
                {
                    if (annotation.Subject == "")
                    {
                        selectedAnnotation.AnnotationType = "CustomStamp";
                    }
                    else
                    {
                        selectedAnnotation.AnnotationType = "Stamp";
                    }

                }
                else
                {
                    selectedAnnotation.AnnotationType = "CustomStamp";
                }
            }
            else
            {
                selectedAnnotation.AnnotationType = annotation.Type.ToString();
            }
            ApplyCommonProperties(annotation);

            if (selectedAnnotation.AnnotationType == "Line" || selectedAnnotation.AnnotationType == "Arrow" || selectedAnnotation.AnnotationType == "Polygon" || selectedAnnotation.AnnotationType == "Distance" || selectedAnnotation.AnnotationType == "Area" || selectedAnnotation.AnnotationType == "Volume" || selectedAnnotation.AnnotationType == "Perimeter")
            {
                if (selectedAnnotation.AnnotationType == "Line" || selectedAnnotation.AnnotationType == "Arrow" || selectedAnnotation.AnnotationType == "Distance" || selectedAnnotation.AnnotationType == "Perimeter")
                {
                    if (selectedAnnotation.AnnotationType == "Distance")
                    {
                        selectedAnnotation.LeaderLength = annotation.LeaderLength;
                    }
                    ApplyLineProperties(annotation);
                    if (selectedAnnotation.AnnotationType == "Line" || selectedAnnotation.AnnotationType == "Arrow" || selectedAnnotation.AnnotationType == "Distance")
                    {
                        ApplyVertexProperties(annotation);
                    }
                }
                if (selectedAnnotation.AnnotationType == "Polygon" || selectedAnnotation.AnnotationType == "Area" || selectedAnnotation.AnnotationType == "Volume" || selectedAnnotation.AnnotationType == "Perimeter")
                {
                    if (annotation.VertexPoints.Count != 0)
                    {
                        ApplyPolygonProperties(annotation);
                    }
                }
            }
            else
            {
                ApplyShapeProperties(annotation);
            }
            if (selectedAnnotation.AnnotationType == "Line" || selectedAnnotation.AnnotationType == "Arrow" || selectedAnnotation.AnnotationType == "Polygon" || selectedAnnotation.AnnotationType == "Distance" || selectedAnnotation.AnnotationType == "Area" || selectedAnnotation.AnnotationType == "Volume" || selectedAnnotation.AnnotationType == "Perimeter" || selectedAnnotation.AnnotationType == "Rectangle" || selectedAnnotation.AnnotationType == "Circle" || selectedAnnotation.AnnotationType == "Ink" || selectedAnnotation.AnnotationType == "FreeText")
            {
                selectedAnnotation.Thickness = annotation.Thickness;
                selectedAnnotation.StrokeColor = annotation.StrokeColor;
                if (IsRGBAColor(selectedAnnotation.StrokeColor))
                {
                    selectedAnnotation.StrokeColor = RGBAtoHex(selectedAnnotation.StrokeColor, "stroke");
                }
            }
            if (selectedAnnotation.AnnotationType != "StickyNotes" && selectedAnnotation.AnnotationType != "Ink" && selectedAnnotation.AnnotationType != "Stamp" && selectedAnnotation.AnnotationType != "CustomStamp")
            {
                if (selectedAnnotation.AnnotationType == "Highlight" || selectedAnnotation.AnnotationType == "Underline" || selectedAnnotation.AnnotationType == "Strikethrough")
                {
                    selectedAnnotation.FillColor = annotation.Color;
                    selectedAnnotation.Bounds = annotation.Bounds;
                }
                else
                {
                    selectedAnnotation.FillColor = annotation.FillColor;
                    if (IsRGBAColor(selectedAnnotation.FillColor))
                    {
                        selectedAnnotation.FillColor = RGBAtoHex(selectedAnnotation.FillColor, "fill");
                    }
                }
            }
            if (selectedAnnotation.AnnotationType == "Stamp")
            {
                ApplyStampProperties(annotation);
            }
            if (selectedAnnotation.AnnotationType == "FreeText")
            {
                ApplyFreeTextProperties(annotation);
            }
            if (selectedAnnotation.AnnotationType == "CustomStamp")
            {
                selectedAnnotation.CustomStampSource = annotation.CustomStampSource;
            }
        }
        //StateHasChanged();
    }

    }
}
