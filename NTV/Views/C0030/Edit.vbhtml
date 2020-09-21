@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "Edit"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Edit</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <h4>D0010</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With { .class = "text-danger" })
        @Html.HiddenFor(Function(model) model.GYOMNO)

        <div class="form-group">
            @Html.LabelFor(Function(model) model.GYOMYMD, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.GYOMYMD, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.GYOMYMDED, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.GYOMYMDED, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.KSKJKNST, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.KSKJKNST, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.KSKJKNED, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.KSKJKNED, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.JTJKNST, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.JTJKNST, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.JTJKNST, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.JTJKNED, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.JTJKNED, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.JTJKNED, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.CATCD, "CATCD", htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("CATCD", Nothing, htmlAttributes:= New With { .class = "form-control" })
                @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BANGUMINM, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.OAJKNST, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.OAJKNST, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.OAJKNST, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.OAJKNED, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.OAJKNED, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.OAJKNED, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.NAIYO, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BASYO, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BASYO, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.BASYO, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BIKO, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BIKO, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.BIKO, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMITANTO, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BANGUMITANTO, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.BANGUMITANTO, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMIRENRK, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BANGUMIRENRK, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.BANGUMIRENRK, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.RNZK, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.RNZK)
                    @Html.ValidationMessageFor(Function(model) model.RNZK, "", New With { .class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.PGYOMNO, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.PGYOMNO, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.PGYOMNO, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.IKTFLG, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.IKTFLG)
                    @Html.ValidationMessageFor(Function(model) model.IKTFLG, "", New With { .class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.IKTUSERID, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.IKTUSERID, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.IKTUSERID, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.IKKATUNO, "IKKATUNO", htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("IKKATUNO", Nothing, htmlAttributes:= New With { .class = "form-control" })
                @Html.ValidationMessageFor(Function(model) model.IKKATUNO, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.INSTID, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.INSTID, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.INSTID, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.INSTDT, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.INSTDT, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.INSTDT, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.INSTTERM, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.INSTTERM, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.INSTTERM, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.INSTPRGNM, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.INSTPRGNM, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.INSTPRGNM, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.UPDTID, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.UPDTID, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.UPDTID, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.UPDTDT, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.UPDTDT, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.UPDTDT, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.UPDTTERM, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.UPDTTERM, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.UPDTTERM, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.UPDTPRGNM, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.UPDTPRGNM, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.UPDTPRGNM, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>
