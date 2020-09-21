@ModelType NTV_SHIFT.M0010
@Code
    ViewData("Title") = "パスワード変更"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h3>ログインユーザー
    @Html.Encode(" ： ")
    @ViewData("LoginUsernm")さん</h3>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">

        <hr />
        @Html.ValidationSummary(True, "", New With { .class = "text-danger" })

         @Html.HiddenFor(Function(model) model.USERID)
         @Html.HiddenFor(Function(model) model.LOGINID)
         @Html.HiddenFor(Function(model) model.USERNM)
         @Html.HiddenFor(Function(model) model.MAILLADDESS)
         @Html.HiddenFor(Function(model) model.HYOJJN)

         @*<div class="form-group">
             @Html.LabelFor(Function(model) model.USERPWDOLD, htmlAttributes:=New With {.class = "control-label col-md-2 text-warning"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.USERPWDOLD, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.USERPWDOLD, "", New With {.class = "text-danger"})
             </div>
         </div>*@

          <div class="form-group">
             @Html.LabelFor(Function(model) model.USERPWD, htmlAttributes:=New With {.class = "control-label col-md-2 text-warning"})
              <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.USERPWD, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.USERPWD, "", New With {.class = "text-danger"})
             </div>
         </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.USERPWDCONFRIM, htmlAttributes:=New With {.class = "control-label col-md-2 text-warning"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.USERPWDCONFRIM, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.USERPWDCONFRIM, "", New With {.class = "text-danger"})
             </div>
         </div>

            <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <p id="errorLogin" style="color:red">@ViewBag.LoginError</p>

                <div class="row">
                    <div class="col-md-1">
                        <input id="btnUpdate" type="submit" value="更新" class="btn btn-default" />
                    </div>
                    <div class="col-md-11">
                        @Html.ActionLink("キャンセル", "Logout", "Login", routeValues:=Nothing, htmlAttributes:=New With {.class = "btn btn-primary"})
                    </div>
                </div>

            </div>
        </div>
    </div>
End Using

@*<script>

    $('#btnUpdate').on('click', function (e) {

        var err = '';
        var userpwdold = $('#USERPWDOLD').val();

        $('div span[data-valmsg-for="USERPWDOLD"]').text("");
        
        if (userpwdold == '') {
            err = '1';
            $('div span[data-valmsg-for="USERPWDOLD"]').text("旧パスワードが必要です。");
        }

        if (err = '1') {
            return false;
        }

    });

</script>*@