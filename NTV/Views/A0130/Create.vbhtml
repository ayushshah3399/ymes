@ModelType NTV_SHIFT.M0030
@Code
    ViewData("Title") = "番組設定"
End Code

@*<div>
    @Html.Partial("_MenuPartial", "3")
</div>*@

<div class="container">
  @Using (Html.BeginForm("Create", "A0130", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
             <h3>新規</h3>
           
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            @*<div class="form-group">
                    @Html.LabelFor(Function(model) model.BANGUMICD, htmlAttributes:= New With { .class = "control-label col-md-2" })
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.BANGUMICD, New With { .htmlAttributes = New With { .class = "form-control" } })
                        @Html.ValidationMessageFor(Function(model) model.BANGUMICD, "", New With { .class = "text-danger" })
                    </div>
                </div>*@

            <div class="form-group">
                @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                    @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.BANGUMIKN, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.BANGUMIKN, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                    @Html.ValidationMessageFor(Function(model) model.BANGUMIKN, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input id="btnMasterUpd" type="submit" value="登録" class="btn btn-default" />
                </div>
            </div>
        </div>
    End Using
    <div>
        @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnNewModoru"})
    </div>
</div>


  <script>

      //画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
      //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
      $("#myForm :input").change(function () {
          var inputVal = $(this).val();

          if (inputVal != '') {
              $("#myForm").data("MSG", true);
          }
          else {
              $("#myForm").data("MSG", false);
          }

      });
  

</script>

   

    @Section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section

