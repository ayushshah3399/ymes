<!DOCTYPE html>
<html>
<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - YAMAHA_MES</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

    @*This is For Common Class .CSS File*@
    @*Some Custom Classes are created by Raj*@
    <link rel="stylesheet" type="text/css" href="~/Content/Common-Class.css">

    @*This is For JQuery*@
    <script type="text/javascript" src="~/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="~/Scripts/jquery-3.3.1.slim.min.js"></script>
    <script type="text/javascript" src="~/Scripts/popper.min.js"></script>
    <script type="text/javascript" src="~/Scripts/jquery-3.3.1.min.js"></script>

</head>

<body>

    @*Navbar*@
    <div Class="navbar navbar-expand-sm navbar-dark bg-primary fixed-top ">

        <a class="navbar-brand" href="#">
            <b>
                @*Application name and Form Name*@
                @Session("webappnm")
                &nbsp;
                @ViewData.Item("ID")
            </b>
        </a>

        @*This is for toggle for logout button and Username*@
        @If ViewData.Item("ID") = LangResources.M1_01_Fn_Menu Then
            @<Button Class="navbar-toggler collapsed" type="button" data-toggle="collapse" data-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
                <span Class="navbar-toggler-icon"></span>
            </Button>
            @<div Class="navbar-collapse collapse" id="navbarColor01" style="">
                <ul Class="navbar-nav ml-auto">
                    <li Class="nav-item">
                        <label id=LoginUsername Class="navbar-brand mt-0 mb-0" style=" text-align:right">@Session("LoginUsernm")</label>
                    </li>
                    <li Class="nav-item">
                        <button id="btnlogout" type="button" name="btnlogout" Class="btn btn-secondary mt-0 mb-0" onclick="window.location.href = 'login/Logout';"><b>@LangResources.Common_Logout</b></button>
                    </li>
                </ul>
            </div>

            'This Is For password Change
        ElseIf ViewData.Item("ID") <> LangResources.L1_01_Login AndAlso ViewData.Item("ID") <> LangResources.L1_20_Fn_ChangePassword Then
            @<Button Class="navbar-toggler collapsed" type="button" data-toggle="collapse" data-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
                <span Class="navbar-toggler-icon"></span>
            </Button>
            @<div Class="navbar-collapse collapse" id="navbarColor01" style="">
                <ul Class="navbar-nav ml-auto">
                    <li Class="nav-item">
                        <label id=LoginUsername Class="navbar-brand mt-0 mb-0" style=" text-align:right">@Session("LoginUsernm")</label>
                    </li>
                </ul>
            </div>

        End If

    </div>

    @RenderBody()

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)

    @*When open screen Focus will on first textbox*@
    @*When open screen Focus will on first textbox*@
    <script type="text/javascript">
		$(function () {
			$('.focus :input:first').focus();
		});

        $(".SelectOnFocus").focusin(function (e) {
            var InputQty = $(this).val().replace(/,/g, '');
            $(this).val(InputQty)
            $(this).prop('type', 'number')
            $(this).prop('min', '0')    
            $(this).select();
        });

        $(".FormatLostFocus").focusout(function (e) {
            $(this).prop('type', 'text')
        });

        @*in Numeric Column keypress Event *@
        $('.NumericValidation').keypress(function (e) {

            // Tab = 9 // Shift = 16 // BackSpace = 8
            if (e.keyCode == '9' || e.keyCode == '16' || e.keyCode == '8') {
                return;
            }

            //Value
            var Inputqty = $(this).val();
            var QtyContainsDot = Inputqty.includes(".")

            //Dot = 9
            //Check Dot Can Input Only 1 Time
            if (e.keyCode == 46) {
                if (QtyContainsDot == true) {
                    return false;
                }
                else {
                    return true;
                }
            };

            //0-9
            if (e.keyCode < 48 || e.keyCode > 57)
                return false;
            else {
                ////Dot Contains
                //if (QtyContainsDot == false) {
                //    // Only Integer
                //    // for Input After . ==> 1234567890.1
                //    if (Inputqty.length > 9) {
                //        return false;
                //    }
                //    else {
                //        return true;
                //    }
                //}
            }
        });

        //Only Numeric Can Paste
        $(".PasteOnNumeric").bind("paste", function (e) {
            e.preventDefault()
            var clipboardData = e.originalEvent.clipboardData.getData('text');

            //If Textbox Contains
            if ($.isNumeric(clipboardData) == false) {
                return false;
            }
            else {
                var dataContainsDot = clipboardData.includes(".");

                //Contains Dot Or Not
                //Display 10 Digit
                if (dataContainsDot == false) {
                    $(this).val(clipboardData.replace(/\D/g, '').substring(0, 10));
                }
                else {
                    //Dot is After 10 Digit then Display 10 Integer
                    if (clipboardData.indexOf(".") > 9) {
                        $(this).val(clipboardData.substring(0, 10));
                    }
                    //Interger less then 10 Digit then
                    else {
                        clipboardData.indexOf(".")
                        $(this).val(clipboardData.substring(0, clipboardData.indexOf(".") + 4));
                    }
                }

            };

        });

    </script>

</body>
</html>
