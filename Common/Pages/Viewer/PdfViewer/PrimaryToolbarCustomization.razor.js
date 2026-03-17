
export function hidePdfviewerSearchBox(viewerID) {
    var viewer = window.sfBlazor.getCompInstance(viewerID);
    if (viewer && viewer.textSearchModule && viewer.textSearchModule.searchBox.style.display == "block") {
        viewer.toolbarModule.isTextSearchBoxDisplayed = false;
        viewer.textSearchModule.showSearchBox(false);
    }
}