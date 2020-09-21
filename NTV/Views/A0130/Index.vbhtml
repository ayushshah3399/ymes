@ModelType IEnumerable(Of NTV_SHIFT.M0030)
@Code
    ViewData("Title") = "番組設定"
End Code

@*<h2>Index</h2>*@

<style>
    .table-scroll {
        width: 650px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 360px;
        width: 650px;
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

<div>
    @Html.Partial("_MenuPartial", "3")
</div>

<p>
    <ul class="nav nav-pills">
        <li>@Html.ActionLink("新規作成", "Create")</li>
    </ul>
</p>

<table class="tablecontainer">
    <tr>
        <td>

            <table class="table table table-bordered table-scroll table-hover ">
                <thead>
                    <tr>
                        <th style="width:50px">

                        </th>
                        <th style="width:200px">
                            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                        </th>
                        <th style="width:250px">
                            @Html.DisplayNameFor(Function(model) model.BANGUMIKN)
                        </th>
                        <th style="width:130px"></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model

                        @<tr>
                            <td style="width:50px">
                                @Html.ActionLink("修正", "Edit", New With {.id = item.BANGUMICD})
                            </td>

                            <td style="width:200px;max-width:200px;">
                                @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                            </td>
                            <td style="width:250px;max-width:250px;">
                                @Html.DisplayFor(Function(modelItem) item.BANGUMIKN)
                            </td>
                            <td style="width:130px">

                                @Html.ActionLink("参照", "Details", New With {.id = item.BANGUMICD}) |
                                @Html.ActionLink("削除", "Delete", New With {.id = item.BANGUMICD})
                            </td>
                        </tr>
                    Next
                </tbody>

            </table>

        </td>
    </tr>

</table>
