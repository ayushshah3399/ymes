@ModelType NTV_SHIFT.WB0050
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
