$(document).ready(function () {
    //alert("OK")
    $("#btnsave").click(function () {
        var name = $("#txtname").val();
        var email = $("#txtemail").val();
        var mobile = $("#txtmobile").val();
        var message = $("#txtmsg").val();
        //alert(name + "" + email + "" + mobile + "" + message);
        $.ajax({
            type: "Post",
            url: "/Home/InsertEnquiry",
            data: { Name: name, Email: email, Mobile: mobile, Message: message },
            success: function (data) {
                $("#result").text(data).fadeOut(6000);
                $("#txtname").val("");
                $("#txtemail").val("");
                $("#txtmobile").val("");
                $("#txtmsg").val("");
            }
        })
    })
    //code for display data
    $("#btndisplay").click(function () {
        var i, tbl = "";
        $.ajax({
            type: "Get",
            url: "/Home/DisplayContact",
            success:function(data)
            {
                tbl += "<tr style='background:brown;color:white'><th>SN.</th><th>Name</th><th>Email</th><th>Mobile</th><th>Message</th></tr>";
                for (i = 0; i < data.length; i++) {
                    tbl += "<tr><td>" + (i+1) + "</td><td>" + data[i].Name + "</td><td>" + data[i].Email + "</td><td>" + data[i].Mobile + "</td><td>" + data[i].Message + "</td></tr>"
                }
                $("#tbldisplay").append(tbl);
            }
        })
    })
})