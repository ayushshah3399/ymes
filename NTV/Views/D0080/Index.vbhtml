@ModelType IEnumerable(Of NTV_SHIFT.D0080)
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
            @Html.DisplayNameFor(Function(model) model.M0010.LOGINID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.MESSAGE)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DATAFLG)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.TOROKUYMD)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.INSTID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.INSTDT)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.INSTTERM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.INSTPRGNM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.UPDTID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.UPDTDT)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.UPDTTERM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.UPDTPRGNM)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.M0010.LOGINID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.MESSAGE)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.DATAFLG)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.TOROKUYMD)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.INSTID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.INSTDT)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.INSTTERM)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.INSTPRGNM)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.UPDTID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.UPDTDT)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.UPDTTERM)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.UPDTPRGNM)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.DNGNNO }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.DNGNNO }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.DNGNNO })
        </td>
    </tr>
Next

</table>
