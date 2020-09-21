@ModelType NTV_SHIFT.WB0050
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>WB0050</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.HI)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HI)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.STTIME)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.STTIME)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EDTIME)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EDTIME)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.KYUKCD }) |
    @Html.ActionLink("Back to List", "Index")
</p>
