var isFormValid = false;

$('#question1').on('click', function () {
    $('#question1span').html('');
});
$('#s1c1,s1c2,s1c2,s1c2').on('click', function () {
    $('#question1Answers').html('');
});
$('#selectTrue1').on('click', function () {
    $('#question1Correct').html('');
});

$('#s2c1,s2c2,s2c2,s2c2').on('click', function () {
    $('#question2Answers').html('');
});
$('#selectTrue2').on('click', function () {
    $('#question2Correct').html('');
});

$('#s3c1,s3c2,s3c2,s3c2').on('click', function () {
    $('#question3Answers').html('');
});
$('#selectTrue3').on('click', function () {
    $('#question3Correct').html('');
});

$('#s4c1,s4c2,s4c2,s4c2').on('click', function () {
    $('#question4Answers').html('');
});
$('#selectTrue4').on('click', function () {
    $('#question4Correct').html('');
});



//get blog data
$('.blogdropdown').on("click", function () {
    var selectedVal = $('#Blog_Id').find(":selected").val();
    var id = Number(selectedVal);

    $.ajax({
        url: '/Exam/GetBlogContent/' + id,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.length > 0) {
                $('#textarea').removeClass('dnone');
                $('#blogcontent').html('');
                $('#blogcontent').html(data);
            } else {
                alert('Birşeyler ters gitti');
            }
        }
    }).fail(function () {
        alert('Birşeyler ters gitti');
    });
});


//validate form
function Validateform() {
    if ($('#Blog_Id').find(":selected").val() == "") {
        $('#s1Dropdown').html('Bir blog seçiniz');
        isFormValid = false;
    }
    else if ($('#question1').val() == "") {
        $('#question1span').html('Soru1´i doldurunuz');
        isFormValid = false;
    }
    else if ($('#s1c1').val() == "" || $('#s1c2').val() == "" || $('#s1c3').val() == "" || $('#s1c4').val() == "") {
        $('#question1Answers').html('Tüm cevapları doldurunuz');
        isFormValid = false;
    }
    else if ($('#selectTrue1').find(":selected").val() == "default") {
        $('#question1Correct').html('Doğru cevabı belirleyiniz');
        isFormValid = false;
    }
    else if ($('#question2').val() == "") {
        $('#question2span').html('Soru2´i doldurunuz');
        isFormValid = false;
    }
    else if ($('#s2c1').val() == "" || $('#s2c2').val() == "" || $('#s2c3').val() == "" || $('#s2c4').val() == "") {
        $('#question2Answers').html('Tüm cevapları doldurunuz');
        isFormValid = false;
    }
    else if ($('#selectTrue2').find(":selected").val() == "default") {
        $('#question2Correct').html('Doğru cevabı belirleyiniz');
        isFormValid = false;
    }
    else if ($('#question3').val() == "") {
        $('#question3span').html('Soru3´i doldurunuz');
        isFormValid = false;
    }
    else if ($('#s3c1').val() == "" || $('#s3c2').val() == "" || $('#s3c3').val() == "" || $('#s3c4').val() == "") {
        $('#question3Answers').html('Tüm cevapları doldurunuz');
        isFormValid = false;
    }
    else if ($('#selectTrue3').find(":selected").val() == "default") {
        $('#question3Correct').html('Doğru cevabı belirleyiniz');
        isFormValid = false;
    }
    else if ($('#question4').val() == "") {
        $('#question4span').html('Soru4´i doldurunuz');
        isFormValid = false;
    }
    else if ($('#s4c1').val() == "" || $('#s4c2').val() == "" || $('#s4c3').val() == "" || $('#s4c4').val() == "") {
        $('#question4Answers').html('Tüm cevapları doldurunuz');
        isFormValid = false;
    }
    else if ($('#selectTrue4').find(":selected").val() == "default") {
        $('#question4Correct').html('Doğru cevabı belirleyiniz');
        isFormValid = false;
    }
    else {
        isFormValid = true;
    }
}



$('#examform').on("submit", function (e) {
    Validateform();
    e.preventDefault();
    e.stopPropagation();
    var blogVal = $('#Blog_Id').find(":selected").val();
    var blogid = Number(blogVal);
    var blogTitle = $('#Blog_Id').find(":selected").text();
    var blogContent = $('#blogcontent').text();

    //question1
    var query1_selectedTrue = $('#selectTrue1').find(":selected").val();
    var q1a1 = $('#s1c1').val();
    var q1a2 = $('#s1c2').val();
    var q1a3 = $('#s1c3').val();
    var q1a4 = $('#s1c4').val();

    var query1_answer1 = { Id: 0, AnswerContent: q1a1, IsCorrect: false };
    var query1_answer2 = { Id: 0, AnswerContent: q1a2, IsCorrect: false };
    var query1_answer3 = { Id: 0, AnswerContent: q1a3, IsCorrect: false };
    var query1_answer4 = { Id: 0, AnswerContent: q1a4, IsCorrect: false };

    if (query1_selectedTrue == "s1c1") {
        query1_answer1 = { Id: 0, AnswerContent: q1a1, IsCorrect: true };
    }
    else if (query1_selectedTrue == "s1c2") {
        query1_answer2 = { Id: 0, AnswerContent: q1a2, IsCorrect: true };
    }
    else if (query1_selectedTrue == "s1c3") {
        query1_answer3 = { Id: 0, AnswerContent: q1a3, IsCorrect: true };
    }
    else if (query1_selectedTrue == "s1c4") {
        query1_answer4 = { Id: 0, AnswerContent: q1a4, IsCorrect: true };
    }

    var q1 = $('#question1').val();

    //question2
    var query2_selectedTrue = $('#selectTrue2').find(":selected").val();
    var q2a1 = $('#s2c1').val();
    var q2a2 = $('#s2c2').val();
    var q2a3 = $('#s2c3').val();
    var q2a4 = $('#s2c4').val();

    var query2_answer1 = { Id: 0, AnswerContent: q2a1, IsCorrect: false };
    var query2_answer2 = { Id: 0, AnswerContent: q2a2, IsCorrect: false };
    var query2_answer3 = { Id: 0, AnswerContent: q2a3, IsCorrect: false };
    var query2_answer4 = { Id: 0, AnswerContent: q2a4, IsCorrect: false };
    if (query2_selectedTrue == "s2c1") {
        query2_answer1 = { Id: 0, AnswerContent: q2a1, IsCorrect: true };
    }
    else if (query2_selectedTrue == "s2c2") {
        query2_answer2 = { Id: 0, AnswerContent: q2a2, IsCorrect: true };
    }
    else if (query2_selectedTrue == "s2c3") {
        query2_answer3 = { Id: 0, AnswerContent: q2a3, IsCorrect: true };
    }
    else if (query2_selectedTrue == "s2c4") {
        query2_answer4 = { Id: 0, AnswerContent: q2a4, IsCorrect: true };
    }

    var q2 = $('#question2').val();


    //question3
    var query3_selectedTrue = $('#selectTrue3').find(":selected").val();
    var q3a1 = $('#s3c1').val();
    var q3a2 = $('#s3c2').val();
    var q3a3 = $('#s3c3').val();
    var q3a4 = $('#s3c4').val();

    var query3_answer1 = { Id: 0, AnswerContent: q3a1, IsCorrect: false };
    var query3_answer2 = { Id: 0, AnswerContent: q3a2, IsCorrect: false };
    var query3_answer3 = { Id: 0, AnswerContent: q3a3, IsCorrect: false };
    var query3_answer4 = { Id: 0, AnswerContent: q3a4, IsCorrect: false };

    if (query3_selectedTrue == "s3c1") {
        query3_answer1 = { Id: 0, AnswerContent: q3a1, IsCorrect: true };
    }
    else if (query3_selectedTrue == "s3c2") {
        query3_answer2 = { Id: 0, AnswerContent: q3a2, IsCorrect: true };
    }
    else if (query3_selectedTrue == "s3c3") {
        query3_answer3 = { Id: 0, AnswerContent: q3a3, IsCorrect: true };
    }
    else if (query3_selectedTrue == "s3c4") {
        query3_answer4 = { Id: 0, AnswerContent: q3a4, IsCorrect: true };
    }

    var q3 = $('#question3').val();

    //question4
    var query4_selectedTrue = $('#selectTrue4').find(":selected").val();
    var q4a1 = $('#s4c1').val();
    var q4a2 = $('#s4c2').val();
    var q4a3 = $('#s4c3').val();
    var q4a4 = $('#s4c4').val();

    var query4_answer1 = { Id: 0, AnswerContent: q4a1, IsCorrect: false };
    var query4_answer2 = { Id: 0, AnswerContent: q4a2, IsCorrect: false };
    var query4_answer3 = { Id: 0, AnswerContent: q4a3, IsCorrect: false };
    var query4_answer4 = { Id: 0, AnswerContent: q4a4, IsCorrect: false };

    if (query4_selectedTrue == "s4c1") {
        query4_answer1 = { Id: 0, AnswerContent: q4a1, IsCorrect: true };
    }
    else if (query4_selectedTrue == "s4c2") {
        query4_answer2 = { Id: 0, AnswerContent: q4a2, IsCorrect: true };
    }
    else if (query4_selectedTrue == "s4c3") {
        query4_answer3 = { Id: 0, AnswerContent: q4a3, IsCorrect: true };
    }
    else if (query4_selectedTrue == "s4c4") {
        query4_answer4 = { Id: 0, AnswerContent: q4a4, IsCorrect: true };
    }

    var q4 = $('#question4').val();

    var model = {
        Blog: {
            Id: blogid,
            Title: blogTitle,
            Content: blogContent,
        },
        Questions: [
            {
                Id: 0,
                QuestionContent: q1,
                AnswerId: 0,
                Answers: [query1_answer1, query1_answer2, query1_answer3, query1_answer4]
            },
            {
                Id: 0,
                QuestionContent: q2,
                AnswerId: 0,
                Answers: [query2_answer1, query2_answer2, query2_answer3, query2_answer4]
            },
            {
                Id: 0,
                QuestionContent: q3,
                AnswerId: 0,
                Answers: [query3_answer1, query3_answer2, query3_answer3, query3_answer4]
            },
            {
                Id: 0,
                QuestionContent: q4,
                AnswerId: 0,
                Answers: [query4_answer1, query4_answer2, query4_answer3, query4_answer4]
            },
        ]
    };

    if (isFormValid == true) {
        $.post("/Exam/SaveExam", model, function (res) {
          
            if (res==1) {
                alert('Sınav başarılı bir şekilde kaydedildi.');
                location.href = "/Sinav-Hazirla";
            }
            else {
                alert('Sınav kaydedilemedi.');
                setTimeout(() => location.href = "/Sinav-Hazirla", 2000);
                
            }
        });
    } else {
        alert('Formda doldurulmamış alan var');
    }
});
