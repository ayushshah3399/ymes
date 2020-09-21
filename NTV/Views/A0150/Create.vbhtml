@ModelType NTV_SHIFT.M0060
@Code
    ViewData("Title") = "休暇コード設定"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h3>新規</h3>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
      
        <hr />
        @Html.ValidationSummary(True, "", New With { .class = "text-danger" })
        @*<div class="form-group">
            @Html.LabelFor(Function(model) model.KYUKCD, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.KYUKCD, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.KYUKCD, "", New With { .class = "text-danger" })
            </div>
        </div>*@

        <div class="form-group">
            @Html.LabelFor(Function(model) model.KYUKNM, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.KYUKNM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.KYUKNM, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.KYUKRYKNM, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.KYUKRYKNM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.KYUKRYKNM, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BACKCOLOR, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BACKCOLOR, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.BACKCOLOR, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.WAKUCOLOR, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.WAKUCOLOR, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.WAKUCOLOR, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.FONTCOLOR, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.FONTCOLOR, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.FONTCOLOR, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.HYOJJN, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.HYOJJN, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.HYOJJN, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.HYOJ, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.HYOJ)
                    @Html.ValidationMessageFor(Function(model) model.HYOJ, "", New With { .class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.TNTHYOHYOJ, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.TNTHYOHYOJ)
                    @Html.ValidationMessageFor(Function(model) model.TNTHYOHYOJ, "", New With { .class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.KYUJITUHYOJ, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.KYUJITUHYOJ)
                    @Html.ValidationMessageFor(Function(model) model.KYUJITUHYOJ, "", New With { .class = "text-danger" })
                </div>
            </div>
        </div>

        @*<div class="form-group">
            @Html.LabelFor(Function(model) model.SHINSEIHYOJ, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.SHINSEIHYOJ)
                    @Html.ValidationMessageFor(Function(model) model.SHINSEIHYOJ, "", New With { .class = "text-danger" })
                </div>
            </div>
        </div>*@

      
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input id="btnMasterUpd" type="submit" value="登録" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnReturn"})
</div>
