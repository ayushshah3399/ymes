@ModelType IEnumerable(Of NTV_SHIFT.M0130)
@Code
    ViewData("Title") = "スポーツカテゴリー設定"
End Code

<style>
    .table-scroll {
        width: 468px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 360px;
        width: 468px;
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
    @Html.Partial("_MenuPartial", "9")
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
                            @Html.DisplayNameFor(Function(model) model.SPORTCATNM)
                        </th>
                        <th style="width:60px">
                            @Html.DisplayNameFor(Function(model) model.HYOJJN)
                        </th>
                        <th style="width:60px">
                            @Html.DisplayNameFor(Function(model) model.HYOJ)
                        </th>
                        <th style="width:80px"></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model

                        @<tr>
                            <td style="width:50px">
                                @Html.ActionLink("修正", "Edit", New With {.id = item.SPORTCATCD})
                            </td>

                            <td style="width:200px;max-width:200px;">
                                @Html.DisplayFor(Function(modelItem) item.SPORTCATNM)
                            </td>
                            <td style="width:60px;max-width:60px;">
                                @Html.DisplayFor(Function(modelItem) item.HYOJJN)
                            </td>
                            <td style="width:60px;max-width:60px;">
                                @Html.DisplayFor(Function(modelItem) item.HYOJ)
                            </td>
                            <td style="width:80px">

                                @Html.ActionLink("参照", "Details", New With {.id = item.SPORTCATCD}) |
                                @Html.ActionLink("削除", "Delete", New With {.id = item.SPORTCATCD})
                            </td>
                        </tr>
Next
                </tbody>

            </table>

        </td>
    </tr>

</table>
