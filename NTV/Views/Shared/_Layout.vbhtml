<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - アナウンサーシフトシステム（テスト環境）</title>
  
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
     
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap-datepicker.min.css">
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap-timepicker.min.css">

    <link rel="stylesheet" type="text/css" href="~/Content/jquery.minicolors.css">
    <link rel="stylesheet" type="text/css" href="~/Content/MyCSS.css">

    <script type="text/javascript" src="~/Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="~/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="~/Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="~/Scripts/bootstrap-datepicker.ja.min.js"></script>
    <script type="text/javascript" src="~/Scripts/bootstrap-timepicker.js"></script>

</head>

<body >

    <div class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            @*<div class="navbar-header">
                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                     <span class="icon-bar"></span>
                     <span class="icon-bar"></span>
                     <span class="icon-bar"></span>
                 </button>
                @Html.ActionLink("アナウンサーシフトシステム", "", "", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>*@

            <div class="navbar-collapse collapse">
                <div class="navbar-text" style="font-size:19px;line-height:1"><b>アナウンサーシフトシステム（テスト環境）</b></div>
                <div class="navbar-text" style="font-size: 14px;">@ViewData.Item("Title")</div>

                <!-- DEMO CODE -->
                @If ViewData.Item("ID") = "Login" Then
                Else

                    @<ul class="nav navbar-nav">
                        @*<li>@Html.ActionLink("詳細", "About", "Home")</li>
                    <li>@Html.ActionLink("連絡先", "Contact", "Home")</li>*@
                    </ul>
                    @*@Html.Partial("_LoginPartial")*@
                    @<ul class="nav navbar-nav navbar-right" style="margin-right:5px;">
                        @*@If Session("LoginUserACCESSLVLCD") = 4 Then
                            @<li>@Html.ActionLink("トップページへ", "Index", "C0040", Nothing, htmlAttributes:=New With {.id = "btnKojinshifuto", .style = "font-size: 14px;"})</li>
                            @<li>@Html.ActionLink("ガントチャートへ", "Index", "C0050", Nothing, htmlAttributes:=New With {.id = "btnGanttChart", .style = "font-size: 14px;"})</li>
                        Else
                            @<li>@Html.ActionLink("トップページへ", "Index", "C0050", Nothing, htmlAttributes:=New With {.id = "btnGanttChart", .style = "font-size: 14px;"})</li>
                        End If*@

                        <li>@Html.ActionLink("個人シフトへ", "Index", "C0040", routeValues:=New With {.id = Session("LoginUserid")}, htmlAttributes:=New With {.id = "btnKojinshifuto", .style = "font-size: 14px;"})</li>    
                        <li>@Html.ActionLink("ガントチャートへ", "Index", "C0050", Nothing, htmlAttributes:=New With {.id = "btnGanttChart", .style = "font-size: 14px;"})</li>
                        <li>@Html.ActionLink("担当表へ", "Index", "C0030", Nothing, htmlAttributes:=New With {.id = "btnTantouHyo", .style = "font-size: 14px;"})</li>
                        <li>
                            <div class="navbar-text" style="font-size: 14px;">@ViewData.Item("LoginUsernm")さん</div>
                        </li>
                        @*<li><a href="javascript:document.getElementById('logoutForm').submit()">ログオフ</a></li>*@
                        <li>@Html.ActionLink("ログオフ", "Logout", "Login", routeValues:=Nothing, htmlAttributes:=New With {.id = "btnLogout", .style = "font-size: 14px;"})</li>
                    </ul>
                End If
            </div>
        </div>
    </div>
    <div class="container-fluid body-content">

        @RenderBody()
        <hr />
        <footer>
            @*<p>&copy; @DateTime.Now.Year - アナウンサーシフトシステム</p>*@
        </footer>

        <script type="text/javascript">

            $('.datepicker').datepicker({
                language: 'ja',
                format: 'yyyy/mm/dd',
                autoclose: true,
                //clearBtn: true,
                //keyboardNavigation: false,
                todayHighlight: true
            });

            //$("#stdt").datepicker("update", new Date());

            $('.date').datepicker({
                language: 'ja',
                format: 'yyyy/mm/dd',
                autoclose: true,
                clearBtn: true,
                //keyboardNavigation: false,
                todayHighlight: true,
            });

           
            $('.time').timepicker({
                showMeridian: false,
                maxHours: 37,
                minuteStep: 5,
                defaultTime: false
            });

            $(document).on('focus', 'input', function () {
                $('.time').not(this).timepicker('hideWidget');
            });
               
            $(document).on('focus', 'select', function () {
                $('.time').not(this).timepicker('hideWidget');
            });

            $('.timecustom').timepicker({
                showMeridian: false,
                maxHours: 25,
                minuteStep: 5,
                defaultTime: false
            });

            $(document).on('focus', 'input', function () {
                $('.timecustom').not(this).timepicker('hideWidget');
            });

            $(document).on('focus', 'select', function () {
                $('.timecustom').not(this).timepicker('hideWidget');
            });
         
        </script>

        <script type="text/javascript">

            $('#EnDisColMsgBox').on('click', function (e) {

                if ($("#ColMsgBox").is(':hidden')) {
                    //$("#ColMsgBox").removeClass("invisible");
                    $("#ColMsgBox").show();
                    //$("#mycontent").removeClass("col-md-12");
                    //$("#mycontent").addClass("col-md-10");
                }
                else {
                    //$("#ColMsgBox").last().addClass("invisible");
                    $("#ColMsgBox").hide();
                    //$("#mycontent").removeClass("col-md-10");
                    //$("#mycontent").addClass("col-md-12");

                }
            });


            $('#EnDisCondition').on('click', function (e) {
                if ($("#conditionrow").is(':hidden')) {
                    $("#conditionrow").show();
                }
                else {
                    $("#conditionrow").hide();
                }
            });


            $(function () {
                $('#btnMasterUpd').click(function () {

                    var result = confirm("更新します。よろしいですか?")

                    if (result == false) {
                        return false
                    }
                });
            });

            //一覧に戻るボタンのため
            $(function () {
                $('#btnReturn').click(function () {

                    var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                    if (result == false) {
                        return false
                    }
                });
            });

            //業務登録の戻るボタンのため(業務登録では一覧へ戻ると戻るボタンがあり、同じ名前だと動かないので、別々の名前にする)
            $(function () {
                $('#btnModoru').click(function () {

                    var strMsgFlg = '@Html.Encode(ViewData.Item("MSG"))'
                    if (strMsgFlg != '' || $("#myForm").data("MSG")) {
                        var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                    }

                });
            });

            //担当表へ戻るボタンのため
            $(function () {
                $('#btnTantouHyo').click(function () {
                    //登録画面だけ確認メッセージを出すため、登録画面かどうかの判断フラグ
                    var strMsgFlg = '@Html.Encode(ViewData.Item("MSG"))'
                    if (strMsgFlg != '' || $("#myForm").data("MSG")) {
                        var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                    }


                });
            }); 

            //ガントチャートへボタンのため
            $(function () {
                $('#btnKojinshifuto').click(function () {
                    //登録画面だけ確認メッセージを出すため、登録画面かどうかの判断フラグ
                    var strMsgFlg = '@Html.Encode(ViewData.Item("MSG"))'
                    if (strMsgFlg != '' || $("#myForm").data("MSG")) {
                        var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                    }


                });
            });

            //ガントチャートへボタンのため
            $(function () {
                $('#btnGanttChart').click(function () {
                    //登録画面だけ確認メッセージを出すため、登録画面かどうかの判断フラグ
                    var strMsgFlg = '@Html.Encode(ViewData.Item("MSG"))'
                    if (strMsgFlg != '' || $("#myForm").data("MSG")) {
                        var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                    }


                });
            });

            //ログオフボタンのため
            $(function () {
                $('#btnLogout').click(function () {
                    //登録画面だけ確認メッセージを出すため、登録画面かどうかの判断フラグ
                    var strMsgFlg = '@Html.Encode(ViewData.Item("MSG"))'
                    if (strMsgFlg != '' || $("#myForm").data("MSG")) {

                        var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                    }

                });
            });

            //新規画面の戻るボタンのため
            $(function () {
                $('#btnNewModoru').click(function () {
                    //登録画面だけ確認メッセージを出すため、登録画面かどうかの判断フラグ

                    if ($("#myForm").data("MSG")) {

                        var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                    }

                });
            });

            //修正画面の戻るボタンのため
            $(function () {
                $('#btnEditModoru').click(function () {

                    //登録画面だけ確認メッセージを出すため、登録画面かどうかの判断フラグ
                    if ($("#myForm").data("MSG")) {

                        var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                    }

                });
            });

</script>

    </div>


    @*@Scripts.Render("~/bundles/jquery")*@
    @*@Scripts.Render("~/bundles/bootstrap")*@
    @RenderSection("scripts", required:=False)

    @*<script type="text/javascript" src="~/Scripts/tinycolor.js"></script>*@
    <script type="text/javascript" src="~/Scripts/jquery.minicolors.min.js"></script>

    <script>

        $("input.backcolor").minicolors({
            change: function (value, opicity) {
                $('.colorsample').css("background-color", value);
            }
        });

        $("input.bordercolor").minicolors({
            change: function (value, opicity) {
                var color = $(this).val();
                if (color != '') {
                    $('.colorsample').css("border", "2px solid " + color);
                }
                else {
                    $('.colorsample').css("border", "none" );
                }
            }

        });

        $("input.fontcolor").minicolors({
            defaultValue: '#000000',

            change: function (value, opicity) {
                $('.colorsample').css("color", value);
            }

        });
               
    </script>

    <script>
        $(document).ready(function () {
            var $affix = $('*[data-spy="affix"]');
            $affix.width($affix.parent().width());
        });

        $(window).resize(function () {
            var $affix = $('*[data-spy="affix"]');
            $affix.width($affix.parent().width());
        });

          </script>

</body>



</html>


  