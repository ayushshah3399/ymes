@ModelType IEnumerable(Of NTV_SHIFT.M0130)
@Code
    Dim index As Integer = 0
End Code
<style>

    .table-scroll {
        width: 320px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

</style>


@If Model IsNot Nothing Then
@<table id="tblSportCat" class="table table-hover table-bordered table-scroll">
    <tbody style="height:360px;width: 360px;overflow-y: auto;overflow-x: hidden;">
        @For Each item In Model
            index = index + 1
            Dim catId = "CHK1" & "_" & item.SPORTCATCD.ToString
            Dim flgId = "CHK2" & "_" & item.SPORTCATCD.ToString
            @<tr>
                <td style="width:40px;">
                    <input id="@catId" type="checkbox" value="@item.SPORTCATNM" code=@item.SPORTCATCD  onclick='CatcdCheckboxCliked(this);'/>
                </td>
                <td style="width:200px;">
                    @Html.DisplayFor(Function(modelItem) item.SPORTCATNM)
                    @Html.HiddenFor(Function(modelItem) item.SPORTCATCD)
                </td>
                <td style="width:120px;">
                    <label><input id="@flgId" type="checkbox" value="" disabled> チーフ</label>
                </td>
            </tr>
        Next
     </tbody>
</table>
End If