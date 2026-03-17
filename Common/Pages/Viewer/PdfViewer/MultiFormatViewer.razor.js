
export function createdBrowsebutton() {
    document.getElementById('browseButton').addEventListener('click', function () {
        document.querySelector('.e-upload-browse-btn').click()
    })
}

export function showMessage() {

    setTimeout(() => {
        document.getElementById("linearProgressBar").style.display = "none";
        document.getElementById("uploadedMessage").style.display = "block";
    }, 1000);

}

export function onSelect(args) {
    let extensions = ['doc', 'docx', 'rtf', 'docm', 'dotm', 'dotx', 'dot', 'xls', 'xlsx', 'pptx', 'pptm', 'potx', 'potm', 'jpeg', 'png', 'bmp', 'pdf'];
    var progressRef = document.getElementById("progress-container");
    progressRef.value = 0;
    let progressBarContainer = document.getElementById("progressbarContainer");
    let progressBar = document.getElementById("linearProgressBar");
    let progressMessage = document.getElementById("uploadedMessage");
    let fileSizeValidation = document.getElementById("fileSizeValidation");
    document.getElementById("fileDetails").style.display = "block";
    document.getElementById("FailedMessage").style.display = "none";
    progressBarContainer.style.display = "block";
    progressBar.style.display = "flex";
    progressMessage.style.display = "none";
    fileSizeValidation.style.display = "none";
    document.getElementById("pdfviewer").style.display = "block";
    var validFiles = args.filesData;
    if (validFiles.length === 0) {
        progressBarContainer.style.display = "block";
        progressBar.style.display = "none";
        progressMessage.style.display = "block";
        args.cancel = true;
        return true;
    }
    if (!extensions.includes(validFiles[0].type)) {
        document.getElementById("FailedMessage").style.display = "block";
        document.getElementById("fileDetails").style.display = "none";
        document.getElementById("pdfviewer").style.display = "none";
        document.getElementById("uploadedMessage").style.display = "none";
        progressBar.style.display = "none";
        progressMessage.style.display = "none";
        args.cancel = true;
        return true;
    }
    if (validFiles[0].type != "pdf" && validFiles[0].size > 4000000) {
        fileSizeValidation.style.display = "block";
        document.getElementById("fileDetails").style.display = "none";
        document.getElementById("pdfviewer").style.display = "none";
        document.getElementById("uploadedMessage").style.display = "none";
        progressBar.style.display = "none";
        progressMessage.style.display = "none";
        args.cancel = true;
        return true;
    }
    document.getElementById("fileName").innerHTML = args.filesData[0].name;
    let size = document.getElementById("fileSize");
    if ((args.filesData[0].size.toString()).length <= 6) {
        size.innerHTML = ((args.filesData[0].size / 1024).toFixed(1)).toString() + " KB";
    } else {
        let kbsize = args.filesData[0].size / 1024;
        size.innerHTML = ((kbsize / 1024).toFixed(1)).toString() + " MB";
    }
    return false;
}
