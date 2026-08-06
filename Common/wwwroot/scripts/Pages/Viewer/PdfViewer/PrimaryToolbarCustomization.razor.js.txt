
export function hidePdfviewerSearchBox(viewerID) {
    var viewer = window.sfBlazor.getCompInstance(viewerID);
    if (viewer && viewer.textSearchModule && viewer.textSearchModule.searchBox && viewer.textSearchModule.searchBox.style.display == "block") {
        viewer.toolbarModule.isTextSearchBoxDisplayed = false;
        viewer.textSearchModule.showSearchBox(false);
    }
    // Close the search box in mobile/device mode by triggering the back button.
    if (viewer && viewer.textSearchModule && viewer.textSearchModule.searchInput) {
        const backwardBtn = document.getElementById(viewerID + "_backward");
        if (backwardBtn) {
            backwardBtn.click();
        }
    }
}