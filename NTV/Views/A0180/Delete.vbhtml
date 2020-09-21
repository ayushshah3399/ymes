@ModelType NTV_SHIFT.D0060
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>D0060</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.M0010.LOGINID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0010.LOGINID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.M0060.KYUKNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0060.KYUKNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KKNST)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KKNED)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JKNST)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JKNED)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.GYOMMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMMEMO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.SHONINFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SHONINFLG)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTDT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTDT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTTERM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTTERM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTPRGNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTPRGNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTDT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTDT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTTERM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTTERM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTPRGNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTPRGNM)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
