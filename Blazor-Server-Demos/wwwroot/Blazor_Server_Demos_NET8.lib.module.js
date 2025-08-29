import { data } from './version.js';

var path = "_content/Blazor_Server_Common_NET8";

const homepagepath = ["/pdf-viewer/demos/", "/development/pdf-viewer/net8/demos/", "/release/pdf-viewer/net8/demos/", "/hotfix/pdf-viewer/net8/demos/", "/"];

// Predefined mappings for component names to their corresponding values
const compNameMappings = {
    'pdf-viewer': { cssDirectory: 'pdfviewer', jsFile: ['syncfusion-blazor-sfpdfviewer','sf-grid','sf-uploader','sf-multiselect','sf-accordion', 'sf-sidebar', 'sf-dropdownlist' ] },
    'smart-pdf-viewer': { cssDirectory: 'smartpdfviewer', jsFile: ['syncfusion-blazor-sfsmartpdfviewer'] }
};

let theme = new URL(window.location.href).searchParams.get("theme");
theme = theme ? theme : "fluent2";
if (window.location.href.indexOf('blazor.syncfusion.com') != -1 || window.location.href.indexOf('localhost') != -1) {
   theme = theme === "bootstrap5" ? "bootstrap5.3" : theme === "bootstrap5-dark" ? "bootstrap5.3-dark" : theme;
}

function getCompNameFromURL() {
    var pathSegments = window.location.pathname;
    pathSegments = pathSegments.split('?')[0];
    pathSegments = pathSegments.split('/')
        .filter(segment => segment.length > 0);
    const specialCaseComponents = ['split-button', 'button-group', 'dropdown-menu', 'progress-button', 'toggle-switch-button', 'checkbox','radio-button'];
    for (const componentName of specialCaseComponents) {
        if (pathSegments.includes(componentName)) {
            return componentName;
        }
    }
    return pathSegments[pathSegments.length - 2];
}

window.sfBlazorSBStaticAssets = {
    dynamicResources: function () {
        if (data.configuration == "Release") {
            //Get the current component page details
            var compName = getCompNameFromURL();
            // Retrieve the two values (e.g., cssDirectory and jsFile) based on compName
            const values = compNameMappings[compName];
            //Dynamically patch the resources to the DOM
            if (values.cssDirectory === "overall") {
                loadAssets("https://cdn.syncfusion.com/blazor/29.2.4/styles/" + theme + ".css");
            }
            else {
                loadAssets("https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/" + values.cssDirectory + "/" + theme + ".min.css");
            }
            if (!values.jsFile || !values.jsFile.includes('syncfusion-blazor')) {
                // Dynamically import necessary scripts for the common layout components
                import("https://cdn.syncfusion.com/blazor/29.2.4/sf-tab.min.js");
                import("https://cdn.syncfusion.com/blazor/29.2.4/sf-tooltip.min.js");
            }
            if (values.jsFile) {
                for (const file of values.jsFile) {
                    if (file === "syncfusion-blazor") {
                        import("https://cdn.syncfusion.com/blazor/29.2.4/syncfusion-blazor.min.js");
                    }
                    else {
                        import("https://cdn.syncfusion.com/blazor/29.2.4/" + file + ".min.js");
                    }
                }
            }
            if (window.location.href.indexOf('dark') != -1 || window.location.href.indexOf('fluent2-highcontrast') != -1 || window.location.href.indexOf('highcontrast') != -1) {
                loadAssets("https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/highcontrast.min.css");
                loadAssets("https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/dark-theme.min.css");
            }
        }
        else {
            loadAssets("_content/Syncfusion.Blazor.Themes/" + theme + ".css");
            loadAssets("_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js");
            if(window.location.href.indexOf('smart-pdf-viewer') != -1 ){
                loadAssets("_content/Syncfusion.Blazor.SfSmartPdfViewer/scripts/syncfusion-blazor-sfsmartpdfviewer.min.js");
            }
            else {
                loadAssets("_content/Syncfusion.Blazor.SfPdfViewer/scripts/syncfusion-blazor-sfpdfviewer.min.js");
            }
            
        }    
    }
};
function loadAssets(file) {
    if (file.indexOf(".css") >= 0) {
        const link = document.createElement('link');
        link.setAttribute('rel', 'stylesheet');
        link.setAttribute('type', 'text/css');
        link.setAttribute('href', file);
        if (file.indexOf("device") >= 0) {
            link.setAttribute('media', "(max-width: 1024px)");
        }
        document.head.appendChild(link);
    }
    else if (file.indexOf(".js") >= 0) {
        const script = document.createElement('script');
        script.setAttribute('src', file);
        document.body.appendChild(script);
    }
    else {
        const link = document.createElement('link');
        link.setAttribute('rel', 'shortcut icon');
        link.setAttribute('type', 'image/x-icon');
        link.setAttribute('href', file);
        document.head.appendChild(link);
    }
}

function homePageAssets() {
    var assetFiles;
    if (data.configuration == "Release") {
        assetFiles = [
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/home-page/fluent2.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/bootstrap.min.css',
            'https://cdn.syncfusion.com/blazor/sb/favicon.ico',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/site.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/home.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/devices.min.css'
        ];
    }
    else if(data.configuration == "Staging"){
        assetFiles = [
            '_content/Syncfusion.Blazor.Themes/fluent2.css',
            '/styles/bootstrap.min.css',
            '/favicon.ico',
            '/styles/site.min.css',
            '/styles/common/home.min.css',
            '/styles/common/devices.min.css'
        ];
    } 
    else {
        assetFiles = [
            '_content/Syncfusion.Blazor.Themes/fluent2.css',
            '/styles/bootstrap.min.css',
            '/favicon.ico',
            '/styles/site.css',
            '/styles/common/home.css',
            '/styles/common/devices.css'
        ];
    }
    assetFiles.forEach((file) => {
        if (data.configuration == "Release") {
            loadAssets(file);
        }
        else {
            if (file.includes('_content')) {
                loadAssets(file);
            }
            else {
                loadAssets(path + file);
            }
        }
    });
}

function samplePageAssets() {
    var assetFiles;
    if (data.configuration == "Release") {
        assetFiles = [
            'https://cdn.syncfusion.com/blazor/sb/favicon.ico',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/bootstrap.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/roboto.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/highlight.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/demos.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/29.2.4/common/devices.min.css',
            'https://cdn.syncfusion.com/blazor/sb/scripts/29.2.4/highlight.min.js'
        ];
    } 
    else if(data.configuration == "Staging"){
        assetFiles = [
            '/styles/common/highcontrast.min.css',
            '/favicon.ico',
            '/styles/common/roboto.min.css',
            '/styles/bootstrap.min.css',
            '/styles/common/highlight.min.css',
            '/styles/common/demos.min.css',
            '/styles/common/devices.min.css',
            '/scripts/common/highlight.min.js',
            '/styles/common/dark-theme.min.css',
        ];
    }
    else {
        assetFiles = [
            '/styles/common/highcontrast.css',
            '/favicon.ico',
            '/styles/common/roboto.css',
            '/styles/bootstrap.min.css',
            '/styles/common/highlight.css',
            '/styles/common/demos.css',
            '/styles/common/devices.css',
            '/scripts/common/highlight.min.js',
            '/styles/common/dark-theme.css',
        ];
    }
    assetFiles.forEach((file) => {
        if (data.configuration == "Release") {
            loadAssets(file);
        }
        else {
            loadAssets(path + file);
        }
    });
}

if (homepagepath.indexOf(window.location.pathname) !== -1) {
    homePageAssets();
    if (data.configuration == "Release") {
        // Dynamically import necessary scripts for the homepage
        import("https://cdn.syncfusion.com/blazor/29.2.4/sf-carousel.min.js");
        import("https://cdn.syncfusion.com/blazor/29.2.4/sf-drop-down-button.min.js");
    }
    else {
        loadAssets("_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js");
    }
}
else {
    loadAssets("Blazor_Server_Demos_NET8.styles.css");
    sfBlazorSBStaticAssets.dynamicResources();
    samplePageAssets();
}