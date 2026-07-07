namespace BlazorDemos
{
    internal sealed partial class SampleConfig
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
                DemoPath = "default",
                ComponentIconName = "pdfviewer"
            });
#if SERVER
            SampleBrowser.SampleList.Add(new SampleList
            {
                Name = "Smart PDF Viewer",
                Category = "Smart Component",
                Directory = "SmartPdfViewer/SmartPdfViewer",
                Samples = SmartPdfViewer,
                ControllerName = "SmartPdfViewer",
                DemoPath = "summarizer",
                ComponentIconName = "PdfViewer"
            });
#endif
        }
    }
}
