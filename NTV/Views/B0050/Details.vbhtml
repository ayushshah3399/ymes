@ModelType NTV_SHIFT.D0040
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>D0040</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.D0030.INSTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.D0030.INSTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.M0060.KYUKNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0060.KYUKNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JTJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JTJKNST)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JTJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JTJKNED)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.GYOMMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMMEMO)
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
</div>
<p>
    @*@Html.ActionLink("Edit", "Edit", New With {.id = Model.PrimaryKey}) |*@
    @Html.ActionLink("Back to List", "Index")
</p>
