$(document).ready(function () {
    // File input
    let fileInput = $('.form-group .file-input');
    let fileLabel = $('.form-group .fileName');
    let fileLabelValue = fileLabel.text();
    console.log(fileLabelValue)

    fileInput.change(function(x){
        let fileName = x.target.value.split(/(\\|\/)/g).pop();

        if (fileName) {
            fileLabel.text(fileName);
        } else {
            fileLabel.text(fileLabelValue);
        }
    })
})