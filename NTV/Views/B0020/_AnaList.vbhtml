@ModelType IEnumerable(Of NTV_SHIFT.M0010)

<style>
    
    .table-scroll {
        width: 240px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-scroll tbody {
        height: 500px;
        width:240px;
        overflow-y: auto;
        overflow-x: hidden;
    }
        
</style>


@If Model IsNot Nothing Then
   @<table id="tblAna" class="table table-hover table-bordered table-scroll">

    @For Each item In Model
        @<tr>
            <td style="width:40px;">
                <input type="checkbox" />
            </td>
            <td style="width:200px;">
                @Html.DisplayFor(Function(modelItem) item.USERNM)
                @Html.HiddenFor(Function(modelItem) item.USERID)
            </td>
        </tr>
    Next
</table>
End If

