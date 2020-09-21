@ModelType IEnumerable(Of NTV_SHIFT.M0080)

<style>
    
    .table-scroll {
        width: 240px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-scroll tbody {
        height:190px;
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
                @Html.DisplayFor(Function(modelItem) item.ANNACATNM)
                @Html.HiddenFor(Function(modelItem) item.ANNACATNO)
            </td>
        </tr>
    Next
</table>
End If

