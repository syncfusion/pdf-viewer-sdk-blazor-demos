#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemos
{
    internal partial class SampleConfig
    {
        public List<Sample> SmartPdfViewer { get; set; } = new List<Sample>{

            new Sample
            {
                Name = "Document Summaries",
                Category = "Smart PDF Viewer",
                Directory = "SmartPdfViewer/SmartPdfViewer",
                Url = "smart-pdf-viewer/summarizer",
                FileName = "SmartSummarizer.razor",
                MetaTitle = "Blazor Smart PDF Viewer Summarizer Example - Syncfusion AI Demos",
                HeaderText = "Blazor Smart PDF Viewer Example - AI-Powered PDF Summarization",
                MetaDescription = "This Blazor Smart PDF Viewer example demonstrates AI-powered summarization with the PDF document loaded in the PDF Viewer for quick document comprehension.",
                NotificationDescription = new string[]{ @" This demo showcases the Smart PDF Viewer AI feature." }
            },
             new Sample
            {
                Name = "Redaction",
                Category = "Smart PDF Viewer",
                Directory = "SmartPdfViewer/SmartPdfViewer",
                Url = "smart-pdf-viewer/smartredact",
                FileName = "SmartRedact.razor",
                MetaTitle = "Blazor Smart PDF Viewer Smart Redact Example - Syncfusion AI Demos",
                HeaderText = "Blazor Smart PDF Viewer Example - AI-Powered Smart Redaction",
                MetaDescription = "This Blazor Smart PDF Viewer Smart Redact demo showcases intelligent document redaction. Automatically identify and obscure sensitive information for enhanced security.",
                NotificationDescription = new string[]{ @" This demo showcases the Smart PDF Viewer AI feature." }
            },
            new Sample
            {
                Name = "Form Filling",
                Category = "Smart PDF Viewer",
                Directory = "SmartPdfViewer/SmartPdfViewer",
                Url = "smart-pdf-viewer/smartfill",
                FileName = "SmartFill.razor",
                MetaTitle = "Blazor Smart PDF Viewer Smart Fill Example - Syncfusion AI Demos",
                HeaderText = "Blazor Smart PDF Viewer Example - AI-Powered Smart Fill",
                MetaDescription = "This Blazor Smart PDF Viewer Smart Fill demo showcases AI-powered form completion. Automatically detect and populate form fields with the relevant information.",
                NotificationDescription = new string[]{ @" This demo showcases the Smart PDF Viewer AI feature." },
                SourceFiles = new List<SourceCollection>()
                {
                    new SourceCollection
                    {
                        Id="SmartFill",
                        FileName="SmartFill.razor"
                    },
                    new SourceCollection
                    {
                        Id="SmartFillPdfViewer",
                        FileName="Pdfviewer.razor"
                    },
                }
            },
        };
    }
}
