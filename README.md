# Syncfusion® Blazor PDF Viewer SDK Demos
This repository contains the demos of Syncfusion® Blazor [**PDF Viewer**](https://www.syncfusion.com/pdf-viewer-sdk/blazor-pdf-viewer) and [**Smart PDF Viewer**](https://www.syncfusion.com/pdf-viewer-sdk/blazor-smart-pdf-viewer) Component samples.
The following topics can help you to use the Syncfusion Blazor PDF Viewer and Smart PDF Viewer Components and run this application in your local. 
* [Requirements to run the demo](#requirements-to-run-the-demo)
* [How to run the demo](#how-to-run-the-demo)
* [PDF Viewer Components Catalog](#pdf-viewer-components-catalog)
* [License](#license)
* [Support and feedback](#support-and-feedback)
## Requirements to run the demo
* [System requirements](https://help.syncfusion.com/document-processing/system-requirements)
* [NET 8 WebAssembly Workload / NET 9 WebAssembly Workload](https://learn.microsoft.com/en-us/aspnet/core/blazor/webassembly-build-tools-and-aot?view=aspnetcore-8.0#net-webassembly-build-tools) (For [PDF Viewer Component](https://help.syncfusion.com/document-processing/pdf/pdf-viewer/blazor/getting-started/web-assembly-application))
* Nodejs Version : [10.24.* or above](https://nodejs.org/download/release/v8.1.0/)
## How to run the demo
Clone the repository. This repository contains Blazor Server demos and Blazor WASM demos project and solution files for .NET 8 and .NET 9. This repository has Common, Blazor Server Demos, and Blazor WASM Demos folders.
* `Blazor-Server-Demos` folder has solution and project files to run Blazor server demos.
* `Blazor-WASM-Demos` folder has solution and project files to run Blazor WebAssembly demos.
* The Common folder contains all the common files (i.e., samples, static web assets, resources) which are applicable for both Blazor Server demos and Blazor WASM demos.
### Run the demo using .NET CLI
* Open the command prompt from the demo's directory.
* Run the demo using the following command.
   
   To run .NET 8 Blazor Server Demos project
   > `dotnet run --project Blazor-Server-Demos/Blazor_Server_Demos_NET8.csproj`

   To run .NET 9 Blazor Server Demos project
   > `dotnet run --project Blazor-Server-Demos/Blazor_Server_Demos_NET9.csproj`

   To run .NET 8 Blazor WASM Demos project
   > `dotnet run --project Blazor-WASM-Demos/Blazor_WASM_Demos_NET8.csproj`
   
   To run .NET 9 Blazor WASM Demos project
   > `dotnet run --project Blazor-WASM-Demos/Blazor_WASM_Demos_NET9.csproj`
### Run the demo using Visual Studio
* Open the solution file using Visual Studio.
* Press `Ctrl + F5` to run the demo.
### Run the demo using Visual Studio code
* Open the Visual Studio code from the demos directory where the project file is present.
    > Ensure the [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) extension is installed in your Visual Studio code before running the Blazor demos.
* Press `Ctrl + F5` to run the demo.
## PDF Viewer Components Catalog
This repository focuses on PDF Viewer components with comprehensive examples showcasing various features and functionalities.
### PDF Viewer Component
The **Syncfusion® Blazor PDF Viewer** component allows you to view, review, and print PDF files in web applications. It provides a rich set of features for working with PDF documents.
**Key Features:**
- View PDF documents with zoom, pan, and navigate
- Built-in toolbar with essential features
- Annotation support (Text markup, Shape, Sticky notes, etc.)
- Form filling and form designer
- Digital signature support
- Customizable toolbar
- Print and download functionality
- Responsive design
**Available Samples:**
- [Default Functionalities](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/default-functionalities)
- [Custom Toolbar](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/custom-toolbar) 
- [Annotations Toolbar](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/annotations-toolbar)
- [Form Designer](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/form-designer)
- [Form Filling](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/form-filling)
- [Handwritten Signature](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/handwritten-signature)
- [Digital Signature](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/invisible-digital-signature)
- [Document List](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/document-list)
- [Read Only](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/read-only)
- [Redaction](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/redaction)
- [Multi-Format Viewer](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/multi-format-viewer)
**Documentation:** [PDF Viewer Documentation](https://help.syncfusion.com/document-processing/pdf/pdf-viewer/blazor/getting-started/web-app)
---
### Smart PDF Viewer Component (Preview)
The **Smart PDF Viewer** is an AI-powered component that enhances the traditional PDF viewing experience with intelligent features powered by machine learning and AI technologies.
**Key Smart Features:**
- **Smart Summarizer:** AI-powered document summarization
- **Smart Fill:** Intelligent form field auto-completion
- **Smart Redact:** Automated sensitive content detection and redaction
**Available Smart Samples:**
- [Smart Summarizer](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/smart-pdf-viewer/summarizer)
- [Smart Fill](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/smart-pdf-viewer/smartfill)
- [Smart Redact](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/smart-pdf-viewer/smartredact)
**Documentation:** [Smart PDF Viewer Documentation](https://help.syncfusion.com/document-processing/pdf/smart-pdf-viewer/blazor/getting-started/web-app)
> **Note:** Smart PDF Viewer is currently in preview mode and available only in Blazor Server applications.
## Getting Started
### PDF Viewer
To get started with the PDF Viewer component:
1. Install the Syncfusion.Blazor.SfPdfViewer NuGet package
2. Configure the PDF Viewer in your application
3. Add the PDF Viewer component to your Blazor page
```csharp
@page "/pdfviewer"
@using Syncfusion.Blazor.SfPdfViewer
<SfPdfViewer2 @ref="@PdfViewer" 
              DocumentPath="@DocumentPath" 
              Height="100%" Width="100%">
</SfPdfViewer2>
```
### Smart PDF Viewer
To get started with the Smart PDF Viewer component:
1. Install the Syncfusion.Blazor.SmartPdfViewer NuGet package
2. Configure AI services and dependencies
3. Add the Smart PDF Viewer component with AI features enabled
```csharp
@page "/smartpdfviewer"
@using Syncfusion.Blazor.SmartPdfViewer;
<SfSmartPdfViewer @ref="@SmartPdfViewer" 
              DocumentPath="@DocumentPath" 
              Height="100%" Width="100%">
</SfSmartPdfViewer>
```
## License
Syncfusion Blazor Components is available under the Syncfusion Essential Studio program, and can be licensed either under the Syncfusion Community License Program or the Syncfusion commercial license.
To be qualified for the Syncfusion Community License Program, you must have gross revenue of less than one (1) million U.S. dollars (USD 1,000,000.00) per year and have less than five (5) developers in your organization, and agree to be bound by Syncfusion's terms and conditions.
Customers who do not qualify for the community license can contact sales@syncfusion.com for commercial licensing options.
You may not use this product without first purchasing a Community License or a Commercial License, as well as agreeing to and complying with Syncfusion's license terms and conditions.
The Syncfusion license that contains the terms and conditions can be found at
[https://www.syncfusion.com/content/downloads/syncfusion_license.pdf](https://www.syncfusion.com/content/downloads/syncfusion_license.pdf)
## Support and feedback
* For any other queries, reach the [Syncfusion support team](https://support.syncfusion.com/) or post the queries through the [community forums](https://www.syncfusion.com/forums?utm_source=github&utm_medium=listing&utm_campaign=blazor-samples).
* To renew the subscription, click [here](https://www.syncfusion.com/sales/products?utm_source=github&utm_medium=listing&utm_campaign=blazor-pdfviewer-samples) or contact our sales team at <salessupport@syncfusion.com>.
* Don't see what you need? Please request it in our [feedback portal](https://www.syncfusion.com/feedback/blazor-components).
## See also
* [Blazor PDF Viewer Documentation](https://help.syncfusion.com/document-processing/pdf/pdf-viewer/blazor/getting-started/web-app)
* [Blazor Smart PDF Viewer Documentation](https://help.syncfusion.com/document-processing/pdf/smart-pdf-viewer/blazor/getting-started/web-app)
* [Blazor PDF Viewer Live Demos](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/pdf-viewer/default-functionalities)
* [Blazor Smart PDF Viewer Live Demos](https://document.syncfusion.com/demos/pdf-viewer/blazor-server/smart-pdf-viewer/summarizer)
* [Syncfusion Blazor Components](https://www.syncfusion.com/blazor-components)
* [Blazor Documentation](https://blazor.syncfusion.com/documentation/introduction)
* [Blazor Smart/AI Samples](https://github.com/syncfusion/smart-ai-samples)