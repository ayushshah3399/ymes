@ModelType NTV_SHIFT.D0090
@Code
    ViewData("Title") = "Create"
End Code


<div class="container-fluid">
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
             <h3>新規</h3>
            <h4>雛形</h4>
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

            <div class="form-group">
                @Html.LabelFor(Function(model) model.HINAMEMO, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.HINAMEMO, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.HINAMEMO, "", New With {.class = "text-danger"})
                </div>
            </div>

            @*<div class="form-group">
                    @Html.LabelFor(Function(model) model.DATAKBN, htmlAttributes:= New With { .class = "control-label col-md-2" })
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.DATAKBN, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.DATAKBN, "", New With { .class = "text-danger" })
                    </div>
                </div>*@
            <div class="form-group">
                @Html.LabelFor(Function(model) model.DATAKBN, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    <label class="radio-inline">
                        @Html.RadioButtonFor(Function(model) model.DATAKBN, "1", htmlAttributes:=New With {.checked = "true"})
                        個人
                    </label>
                    <label class="radio-inline">
                        @Html.RadioButtonFor(Function(model) model.DATAKBN, "2")
                        共有
                    </label>
                    @Html.ValidationMessageFor(Function(model) model.DATAKBN, "", New With {.class = "text-danger"})
                </div>
            </div>
             @*@Html.HiddenFor(Function(model) model.FMTKBN, New With {.value = "2"})*@
           
            @Html.HiddenFor(Function(model) model.GYOMYMD)
            @Html.HiddenFor(Function(model) model.GYOMYMDED)

            @Html.HiddenFor(Function(model) model.KSKJKNST)
            @Html.HiddenFor(Function(model) model.KSKJKNED)

            @Html.HiddenFor(Function(model) model.CATCD)
            @Html.HiddenFor(Function(model) model.BANGUMINM)

            @Html.HiddenFor(Function(model) model.OAJKNST)
            @Html.HiddenFor(Function(model) model.OAJKNED)


            @Html.HiddenFor(Function(model) model.NAIYO)
            @Html.HiddenFor(Function(model) model.BASYO)
            @Html.HiddenFor(Function(model) model.BIKO)
            @Html.HiddenFor(Function(model) model.BANGUMITANTO)

            @Html.HiddenFor(Function(model) model.BANGUMIRENRK)

            @Html.HiddenFor(Function(model) model.PTNFLG)
            @Html.HiddenFor(Function(model) model.PTN1)
            @Html.HiddenFor(Function(model) model.PTN2)
            @Html.HiddenFor(Function(model) model.PTN3)
            @Html.HiddenFor(Function(model) model.PTN4)
            @Html.HiddenFor(Function(model) model.PTN5)
            @Html.HiddenFor(Function(model) model.PTN6)
            @Html.HiddenFor(Function(model) model.PTN7)
            @*@Html.HiddenFor(Function(model) model.D0100(0).USERID,htmlAttributes:= New With {})*@
             @Html.Hidden("D0100[0].USERID", Model.D0100(0).USERID)

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="更新" class="btn btn-default" />
                </div>
            </div>
        </div>
    End Using
</div>

   

    <div>
        @Html.ActionLink("戻る", "Create", "A0170")
    </div>

    @Section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section
