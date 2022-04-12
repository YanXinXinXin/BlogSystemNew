$(function () {
    var f = false;
    $('#BtnReply').on('click', function () {
        if (f = !f) {
            $('#Reply').css('display', 'block');
        }
        else {
            $('#Reply').css('display', 'none');
        }
    })
})