@ModelType IEnumerable(Of NTV_SHIFT.D0050)
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
            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.NAIYO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BASYO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.GYOMMEMO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.SHONINFLG)
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
            @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.KSKJKNST)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.KSKJKNED)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.NAIYO)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BASYO)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.GYOMMEMO)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.SHONINFLG)
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
            @Html.ActionLink("Edit", "Edit", New With {.id = item.GYOMSNSNO }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.GYOMSNSNO }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.GYOMSNSNO })
        </td>
    </tr>
Next

</table>
