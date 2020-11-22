document.getElementById("markdown-text").innerHTML = markdown.toHTML(text);
//let markChecks = document.querySelectorAll(".mark-check");
let chapterText = document.querySelectorAll(".chapter-text");
for (let i = 0; i < chapterText.length; i++) {
    let buf = chapterText[i].textContent;
    chapterText[i].textContent = "";
    chapterText[i].innerHTML = markdown.toHTML(buf);
}
let starChecks = document.querySelectorAll(".star-img");
let info = document.querySelectorAll(".main-section");
let select = 0;
document.getElementById("info").addEventListener("click", function () { SwitchSection(0); });
document.getElementById("chapter").addEventListener("click", function () { SwitchSection(1); });
document.getElementById("comments").addEventListener("click", function () { SwitchSection(2); });
function SwitchSection(num) {
    if (info[num].classList.contains("d-none")) {
        info[num].classList.remove("d-none");
        if (select != null)
            info[select].classList.add("d-none");
        select = num;
    } else {
        info[num].classList.add("d-none");
        select = null;
    }
}

document.ondragover = function (e) { e.preventDefault() }
document.ondrop = function (e) { e.preventDefault(); upload(e.dataTransfer.files[0]); }
function upload(file) {

    if (!file || !file.type.match(/image.*/)) return;

    let spanUploading = document.createElement('div');
    spanUploading.classList.add("fixed-bottom");
    spanUploading.innerHTML = "Uploading";
    document.body.appendChild(spanUploading);

    var fd = new FormData();
    fd.append("image", file);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "https://api.imgur.com/3/image.json");
    xhr.onload = function () {
        let linkImage = JSON.parse(xhr.responseText).data.link;
        document.querySelector('.chapter-image').src = linkImage;

        document.getElementById("chapter-image").setAttribute('value', linkImage);

        spanUploading.innerHTML = "Uploaded";
    }

    xhr.setRequestHeader('Authorization', 'Client-ID ');

    xhr.send(fd);
}