@ModelType NTV_SHIFT.D0080
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>D0080</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.M0010.LOGINID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0010.LOGINID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.MESSAGE)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.MESSAGE)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DATAFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DATAFLG)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TOROKUYMD)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TOROKUYMD)
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
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.DNGNNO }) |
    @Html.ActionLink("Back to List", "Index")
</p>
