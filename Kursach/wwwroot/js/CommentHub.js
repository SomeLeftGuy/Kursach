let hubUrl = 'https://localhost:44332/Comments';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();
hubConnection.on('Send', function (message, userName, comment) {
    let messages = document.querySelectorAll(".author");
    let dublicate = false;
    for (let i = 0; i < messages.length; i++) {
        if (messages[i].textContent == userName) {
            dublicate = true;
            if (message == "")
                messages[i].parentNode.remove();
            else
                messages[i].parentNode.querySelector(".text-context").textContent = message;
        }
    }
    if (!dublicate) {
        let container = document.createElement("div");
        container.classList.add("border");
        container.classList.add("rounded");
        container.classList.add("p-3");
        container.classList.add("mb-3");

        let userNameElem = document.createElement("h2");
        userNameElem.appendChild(document.createTextNode(userName));
        userNameElem.classList.add("author");

        let elem = document.createElement("p");
        elem.classList.add("text-context")
        elem.appendChild(document.createTextNode(message));
        let commentId = document.createElement("input");
        commentId.classList.add("comment-id");
        commentId.type = "hidden";
        commentId.value = comment;

        container.appendChild(userNameElem);
        container.appendChild(elem);
        container.appendChild(commentId);

        var firstElem = document.getElementById("comments-list").firstChild;
        document.getElementById("comments-list").insertBefore(container, firstElem);
    }

});
document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("comment").value;
    hubConnection.invoke('Send', message, userName, company);
});
document.addEventListener("keydown", function (event) {
    if (event.code == "Enter") {
        let message = document.getElementById("comment").value;
        hubConnection.invoke('Send', message, userName, company);
    }
});
hubConnection.start();