@ModelType IEnumerable(Of NTV_SHIFT.M0020)


<style>
  
    
    table.table-bordered.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }



    table.table-bordered.table-scroll tbody {
        height: 500px;
        width:290px;
        overflow-y: auto;
        overflow-x: hidden;
    }
    
    
 

  
</style>

<table class="table table-bordered table-scroll">
    <thead>
        <tr>
            <th width="200" >
                @Html.DisplayNameFor(Function(model) model.CATNM)
            </th>
            <th width = "70" >
                @Html.DisplayNameFor(Function(model) model.HYOJJN)
            </th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td style="width:200px;">
                    @Html.DisplayFor(Function(modelItem) item.CATNM)
                </td>
                 <td style="width:70px";>
                     @Html.DisplayFor(Function(modelItem) item.HYOJJN)
                 </td>
            </tr>
        Next
    </tbody>




</table>
