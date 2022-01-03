
function choiseFunction(ele) {
    var parentId = ele.parentNode.id;
    var list_items = document.getElementById(parentId).getElementsByTagName('li');
    for (var i = 0; i < list_items.length; i++) {
        list_items[i].classList.remove("checkbefore");
    }


    document.getElementById(ele.id).classList.add("checkbefore");
};

$('#finishexam').on("click", function (e) {
    var Id = [];
    var examId = $('#exam').attr('value');
    $('.checkbefore').each(function (n) {
        var i = $(this).attr('id');
        Id[n] = i;
    });

    var data = { examid: JSON.stringify(examId), id: JSON.stringify(Id) };
    $.post("/Sinav-Sonucu", data, function (datas) {
        document.all[0].innerHTML = datas;
    });
});