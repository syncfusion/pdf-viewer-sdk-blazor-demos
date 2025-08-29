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
