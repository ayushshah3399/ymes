@Code
    ViewData("Title") = "ログイン"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>ログイン</h2>

<hr />

<div class="row">
    <div class="col-md-8">
        <section id="loginForm">
            @Using Html.BeginForm("Index", "Login", FormMethod.Post, New With {.class = "form-horizontal"})
                @Html.AntiForgeryToken()
                @<text>


                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                    <div class="form-group">

                        @Html.Label("ユーザーID", htmlAttributes:=New With {.class = "col-md-2 control-label"})

                        <div class="col-md-10">
                            @Html.TextBox("LoinId", TempData("LoinId"), htmlAttributes:=New With {.class = "form-control", .autocomplete = "off", .style = "ime-mode: disabled;"})
                            @Html.ValidationMessage("LoinId", htmlAttributes:=New With {.class = "text-danger"})
                            <div id="errorLoinId" style="color:red"></div>
                        </div>
                    </div>
                    <div class="form-group">
                        @Html.Label("パスワード", htmlAttributes:=New With {.class = "col-md-2 control-label"})

                        <div class="col-md-10">
                            @Html.Password("Password", Nothing, htmlAttributes:=New With {.class = "form-control", .autocomplete = "off"})
                            <span id="errorPassword" style="color:red"></span>
                        </div>

                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <p id="errorLogin" style="color:red">@TempData("LoginErrMsg")</p>

                            <div class="row">
                                <div class="col-md-2">
                                    <button id="btnLogin" type="submit" class="btn btn-default">ログイン</button>
                                </div>
                                <div class="col-md-10">
                                    <button id="btnChangePsw" name="changepsw" value="1" type="submit" class="btn btn-primary">パスワード変更</button>
                                </div>
                             </div>
                        </div>

                       
                    </div>

                </text>
            End Using
        </section>
    </div>

</div>

<script>
    $('#btnLogin').on('click', function (e) {

        var LoinId = $('#LoinId').val();
        var Password = $('#Password').val();
        var errflg = '';

        $("#errorLogin").text("");
        $("#errorLoinId").text("");
        $("#errorPassword").text("");

        if (LoinId == '') {
            $("#errorLoinId").text("ユーザーIDが必須入力です。 ");
            errflg = '1';
        }

        if (Password == '') {
            $("#errorPassword").text("パスワードが必須入力です。 ");
            errflg = '1';
        }
       

        if (errflg != '') {
            return false
        }

    });

    $('#btnChangePsw').on('click', function (e) {

        var LoinId = $('#LoinId').val();
        var Password = $('#Password').val();
        var errflg = '';

        $("#errorLogin").text("");
        $("#errorLoinId").text("");
        $("#errorPassword").text("");

        if (LoinId == '') {
            $("#errorLoinId").text("ユーザーIDが必須入力です。 ");
            errflg = '1';
        }

        if (Password == '') {
            $("#errorPassword").text("パスワードが必須入力です。 ");
            errflg = '1';
        }


        if (errflg != '') {
            return false
        }

    });

</script>
