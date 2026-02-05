#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace BlazorDemos
{
    internal partial class SampleConfig
    {
        internal SampleConfig()
        {
            SampleBrowser.SampleList.Add(new SampleList
            {
                Name = "PDF Viewer",
                Category = "Viewer",
                Directory = "Viewer/PdfViewer",
                Samples = PDFViewer,
                ControllerName = "PdfViewer",
                CustomDocLink = "pdfviewer/getting-started",
                DemoPath = "pdf-viewer/default-functionalities",
                ComponentIconName = "pdfviewer",
                Type = SampleType.Updated
            });
#if SERVER
            SampleBrowser.SampleList.Add(new SampleList
            {
                Name = "Smart PDF Viewer",
                Category = "Smart Component",
                Directory = "SmartPdfViewer/SmartPdfViewer",
                Samples = SmartPdfViewer,
                ControllerName = "SmartPdfViewer",
                DemoPath = "smart-pdf-viewer/summarizer",
                ComponentIconName = "PdfViewer",
                Type = SampleType.Preview,
                IsPreview = true
            });
#endif
        }
    }
}
