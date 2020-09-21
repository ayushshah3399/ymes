@ModelType IEnumerable(Of NTV_SHIFT.WB0050)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.HI)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.STTIME)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.EDTIME)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.HI)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.STTIME)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.EDTIME)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.KYUKCD }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.KYUKCD }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.KYUKCD })
        </td>
    </tr>
Next

</table>
