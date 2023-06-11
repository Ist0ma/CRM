function Copy(data) {
    navigator.clipboard.writeText(data);
    var input = document.getElementById("wallet");
    input.style.color = "green";

    setTimeout(() => input.style.color = "", 1000);
}