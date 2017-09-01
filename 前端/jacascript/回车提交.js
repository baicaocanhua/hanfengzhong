
document.onkeydown = ban;
function ban(event) {
    event = (event) ? event : ((window.event) ? window.event : "")
    var code = event.keyCode ? event.keyCode : event.which;
    if (code == 13) {
        $("input[type='submit']").click();
    }

}


