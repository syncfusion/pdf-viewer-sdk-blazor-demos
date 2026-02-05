import { data } from './version.js';
if (data.version == "net8.0") {
    var path = "_content/Blazor_WASM_Common_NET8";
    var isolatedCss = "Blazor_WASM_Demos_NET8.styles.css";
}
else if (data.version == "net9.0") {
    var path = "_content/Blazor_WASM_Common_NET9";
    var isolatedCss = "Blazor_WASM_Demos_NET9.styles.css";
}
else {
    var path = "_content/Blazor_WASM_Common_NET10";
    var isolatedCss = "Blazor_WASM_Demos_NET10.styles.css";
}

const homepagepath = ["/", "/pdf-viewer/wasm/demos/", "/development/pdf-viewer/wasm/net8/demos/", "/development/pdf-viewer/wasm/net9/demos/", "/development/pdf-viewer/wasm/net10/demos/", "/release/pdf-viewer/wasm/net8/demos/", "/release/pdf-viewer/wasm/net9/demos/", "/release/pdf-viewer/wasm/net10/demos/", "/hotfix/pdf-viewer/wasm/net8/demos/", "/hotfix/pdf-viewer/wasm/net9/demos/", "/hotfix/pdf-viewer/wasm/net10/demos/"];

function dynamicResources() {
    if (window.location.href.indexOf('pdf-viewer') != -1) {
        let newScript = document.createElement('script');
        newScript.setAttribute('src', "_content/Syncfusion.Blazor.SfPdfViewer/scripts/syncfusion-blazor-sfpdfviewer.min.js");
        document.getElementsByClassName('dynamic-resources')[0].appendChild(newScript);
    }
}

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
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/home-page/fluent2.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/bootstrap.min.css',
            'https://cdn.syncfusion.com/blazor/sb/favicon.ico',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/site.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/home.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/devices.min.css'
        ];
    }
    else if (data.configuration == "Staging") {
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
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/bootstrap.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/roboto.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/highlight.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/demos.min.css',
            'https://cdn.syncfusion.com/blazor/sb/styles/31.2.12/common/devices.min.css',
            'https://cdn.syncfusion.com/blazor/sb/scripts/31.2.12/highlight.min.js'
        ];
    }
    else if (data.configuration == "Staging") {
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
    loadAssets(isolatedCss);
    homePageAssets();
}
else {
    samplePageAssets();
    loadAssets(isolatedCss);
}
dynamicResources();
