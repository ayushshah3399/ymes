@ModelType NTV_SHIFT.D0050
@Code
    ViewData("Title") = "業務承認"
End Code



@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <h4>D0050</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With { .class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(Function(model) model.GYOMSNSNO, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.GYOMSNSNO, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.GYOMSNSNO, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.USERID, "USERID", htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("USERID", Nothing, htmlAttributes:= New With { .class = "form-control" })
                @Html.ValidationMessageFor(Function(model) model.USERID, "", New With { .class = "text-danger" })
            </div>
        </div>

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
            @Html.LabelFor(Function(model) model.GYOMMEMO, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.GYOMMEMO, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.GYOMMEMO, "", New With { .class = "text-danger" })
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
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>
