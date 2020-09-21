@ModelType IEnumerable(Of NTV_SHIFT.M0040)
@Code
    ViewData("Title") = "内容設定"
End Code

@*<h2>Index</h2>*@
<div>
    @Html.Partial("_MenuPartial", "4")
</div>


<style>
    .table-scroll {
        width: 250px;
    }


    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 360px;
        width: 280px;
        overflow-y: auto;
        overflow-x: hidden;
    }

       .table-scroll td {
          word-wrap:break-word;
    }

       body {
        font-size: 12px;
    }
</style>

<p>
    <ul class="nav nav-pills">
        <li>@Html.ActionLink("新規作成", "Create")</li>
    </ul>
</p>

<table class="tablecontainer">
    <tr>
        <td>

            <table class="table table-bordered table-scroll table-hover">
                <thead>
                    <tr>
                        <th style="width:47px"></th>
                        <th style="width:115px">
                            @Html.DisplayNameFor(Function(model) model.NAIYO)
                        </th>
                        <th style="width:100px"></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model

                        @<tr>
                            <td style="width:47px">
                                @Html.ActionLink("修正", "Edit", New With {.id = item.NAIYOCD})
                            </td>
                            <td style="width:115px; max-width:115px">
                                @Html.DisplayFor(Function(modelItem) item.NAIYO)
                            </td>
                            <td style="width:100px">
                                @Html.ActionLink("参照", "Details", New With {.id = item.NAIYOCD}) |
                                @Html.ActionLink("削除", "Delete", New With {.id = item.NAIYOCD})
                            </td>
                        </tr>

                    Next
                </tbody>
            </table>

        </td>
    </tr>

</table>
