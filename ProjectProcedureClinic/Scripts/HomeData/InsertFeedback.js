$(document).ready(function () {
    var total = "";
    $("#s1").click(function () {
        total = $("#sp1").text();
        $("#s1").css("color", "red");
        $("#s2,#s3,#s4,#s5").css("color","green")
    })
    $("#s2").click(function () {
        total = $("#sp2").text();
        $("#s1,#s2").css("color", "orange");
        $("#s3,#s4,#s5").css("color", "green")
    })
    $("#s3").click(function () {
        total = $("#sp3").text();
        $("#s1,#s2,#s3").css("color", "yellow");
        $("#s4,#s5").css("color", "green")
    })
    $("#s4").click(function () {
        total = $("#sp4").text();
        $("#s1,#s2,#s3,#s4").css("color", "green");
        $("#s5").css("color", "green")
    })
    $("#s5").click(function () {
        total = $("#sp5").text();
        $("#s1,#s2,#s3,#s4,#s5").css("color", "red");
    })
    $("#btnFeedback").click(function () {
        var msg = $("#txtmessage").val();
        $.ajax({
            type: "Post",
            url: "/Patient/InsertFeedback",
            data: { Total: total, Msg: msg },
            success:function(data)
            {
                $("#result").text(data).hide(4000);
                $("#txtmessage").val("");
                total = "";
            },
            failure: function (data)
            {
                $("#result").text(data).hide(4000);
            }
        })
    })

})