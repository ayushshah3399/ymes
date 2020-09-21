@ModelType IEnumerable(Of NTV_SHIFT.M0060)
@Code
    ViewData("Title") = "休暇コード設定"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div>
    @Html.Partial("_MenuPartial", "5")
</div>

<style>
  
    body {
        font-size: 12px;
    }
</style>
@*<h2>休暇コード設定</h2>*@

@*<p>
        @Html.ActionLink("新規作成", "Create")
    </p>*@
<br />
<table class="table table-bordered table-hover" >
    <tr>
        <th></th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KYUKNM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KYUKRYKNM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.HYOJJN)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.HYOJ)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.TNTHYOHYOJ)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KYUJITUHYOJ)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.SHINSEIHYOJ)
        </th>

        <th>
            @Html.DisplayNameFor(Function(model) model.BACKCOLOR)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.WAKUCOLOR)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.FONTCOLOR)
        </th>

        @*<th></th>*@
    </tr>

    @For Each item In Model

        @<tr>
             <td>
                 @Html.ActionLink("修正", "Edit", New With {.id = item.KYUKCD})
             </td>

            <td>
                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.HYOJJN)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.HYOJ)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.TNTHYOHYOJ)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.KYUJITUHYOJ)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.SHINSEIHYOJ)
            </td>

         
             <td style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">
                 @If item.BACKCOLOR <> "" Then
                     @<label>#</label>
                 End If
                 @Html.DisplayFor(Function(modelItem) item.BACKCOLOR)
             </td>


             <td style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">
                 @If item.WAKUCOLOR <> "" Then
                     @<label>#</label>
                 End If
                 @Html.DisplayFor(Function(modelItem) item.WAKUCOLOR)
             </td>


             <td style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">
                 @If item.FONTCOLOR <> "" Then
                     @<label>#</label>
                 End If
                 @Html.DisplayFor(Function(modelItem) item.FONTCOLOR)
             </td>

                @* <td>
                    @Html.ActionLink("参照", "Details", New With {.id = item.KYUKCD}) |
                    @Html.ActionLink("削除", "Delete", New With {.id = item.KYUKCD})
             </td>*@

        </tr>
    Next

</table>
