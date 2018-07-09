<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaptchaTest.aspx.cs" Inherits="CaptchaTestWebApp.CaptchaTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="captcha_div"></div>
        <input type="button" id="btn_refresh" value="Refresh" />
        <input type="button" id="btn" value="Submit" />
    </form>
    <script src="http://localhost:1867/Scripts/jquery-1.10.2.min.js"></script>
    <script>
        $(document).ready(() => {
            GETCAPTCHA();

            $('#btn').click(function () {
                if ($('#txt_captcha').val() !== undefined &&
                    $('#hdn_captcha').val() !== undefined) {
                    console.log($('#txt_captcha').val());
                    console.log($('#hdn_captcha').val());
                } else {
                    alert('Unable to submit form since captcha was not loaded.')
                }
            });

            function GETCAPTCHA() {
                $.ajax({
                    url: "http://localhost:1867/api/values",
                    method: "GET",
                    contentType: "application/json",
                    success: function (e) {
                        $('#captcha_div').html('');
                        console.log(e)
                        var input = document.createElement('input');
                        input.type = 'text';
                        input.id = "txt_captcha";
                        if (e.num1 === "blank") {
                            $('#captcha_div').append(input);
                        } else {
                            $('#captcha_div').append(e.num1);
                        }
                        $('#captcha_div').append(e.opr);
                        if (e.num2 === "blank") {
                            $('#captcha_div').append(input);
                        } else {
                            $('#captcha_div').append(e.num2);
                        }
                        $('#captcha_div').append("=");
                        if (e.sum === "blank") {
                            $('#captcha_div').append(input);
                        } else {
                            $('#captcha_div').append(e.sum);
                        }
                        var hidden = document.createElement('input');
                        hidden.type = 'hidden';
                        hidden.id = "hdn_captcha";
                        hidden.value = e.val;
                        $('#captcha_div').append(hidden);
                    },
                    error: function (e) {
                        console.log('error')
                        console.log(e)
                    }
                });
            }
            $('#btn_refresh').click(function () {
                GETCAPTCHA();
            });
        });
    </script>
</body>
</html>
