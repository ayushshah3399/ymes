@ModelType NTV_SHIFT.M0030
@Code
    ViewData("Title") = "番組設定"
End Code

<div class="container">
   @Using (Html.BeginForm("Edit", "A0130", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
             <h3>修正</h3>
         
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            @Html.HiddenFor(Function(model) model.BANGUMICD)

            <div class="form-group">
                @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.BANGUMIKN, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.BANGUMIKN, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.BANGUMIKN, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input id="btnMasterUpd"  type="submit" value="更新" class="btn btn-default" />
                </div>
            </div>
        </div>
    End Using

    <div>
        @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnEditModoru"})
    </div>
</div>


   
<script>

    //修正モードで画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
    $("#myForm :input").change(function () {

        $("#myForm").data("MSG", true);

    });


</script>
 

    @Section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section
