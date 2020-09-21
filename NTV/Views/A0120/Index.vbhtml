@ModelType IEnumerable(Of NTV_SHIFT.M0020)
@Code
    ViewData("Title") = "カテゴリー設定"
End Code

@*<h2>Index</h2>*@

<style>
    .table-scroll {
        width: 1422px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 360px;
        width: 1422px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .col1 {
        width: 50px;
    }

    .catnm {
        width: 160px;
           max-width: 160px;
    }

    .hyoj, .hyojjin {
        width: 70px;
    }

    .oatimehyoj {
        width: 80px;
    }

    .kskhyoj, .anahyoj {
        width: 90px;
    }

    .tantohyoj {
        width: 110px;
    }
    .alertflg {
        width: 140px;
    }


      .table-scroll td {
          word-wrap:break-word;
    }

        body {
        font-size: 12px;
    }
</style>


<div>
    @Html.Partial("_MenuPartial", "2")
</div>
<p>
    <ul class="nav nav-pills">
        <li>@Html.ActionLink("新規作成", "Create")</li>
    </ul>
</p>


<table class="tablecontainer">
    <tr>
        <td>

            <table class="table table-bordered table-scroll table-hover ">
                <thead>
                    <tr>
                        <th class="col1">
                        </th>
                        <th class="catnm">
                            @Html.DisplayNameFor(Function(model) model.CATNM)
                        </th>
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.HYOJ)
                        </th>
                        <th class="hyojjin">
                            @Html.DisplayNameFor(Function(model) model.HYOJJN)
                        </th>
                        <th class="oatimehyoj">
                            @Html.DisplayNameFor(Function(model) model.OATIMEHYOJ)
                        </th>
                        <th class="hyojjin">
                            @Html.DisplayNameFor(Function(model) model.BANGUMIHYOJ)
                        </th>
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.KKNHYOJ)
                        </th>
                        <th class="kskhyoj">
                            @Html.DisplayNameFor(Function(model) model.KSKHYOJ)
                        </th>

                        <th class="anahyoj">
                            @Html.DisplayNameFor(Function(model) model.ANAHYOJ)
                        </th>
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.BASYOHYOJ)
                        </th>
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.BIKOHYOJ)
                        </th>
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.NAIYOHYOJ)
                        </th>
                        <th class="tantohyoj">
                            @Html.DisplayNameFor(Function(model) model.TANTOHYOJ)
                        </th>
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.RENRKHYOJ)
                        </th>                       
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.SYUCHO)
                        </th>
                        <th class="alertflg">
                            @Html.DisplayNameFor(Function(model) model.ALERTFLG)
                        </th>
                        @*<th >
                        @Html.DisplayNameFor(Function(model) model.STATUS)
                    </th>
                        *@
                        <th class="col1">
                        </th>
                    </tr>

                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td class="col1">
                                @Html.ActionLink("修正", "Edit", New With {.id = item.CATCD})
                            </td>
                            <td class="catnm">
                                @Html.DisplayFor(Function(modelItem) item.CATNM)
                            </td>
                            <td class="hyoj">
                                @Html.DisplayFor(Function(modelItem) item.HYOJ)
                            </td>
                            <td class="hyojjin">
                                @Html.DisplayFor(Function(modelItem) item.HYOJJN)
                            </td>
                            <td class="oatimehyoj">
                                @Html.DisplayFor(Function(modelItem) item.OATIMEHYOJ)
                            </td>
                            <td class="hyojjin">
                                @Html.DisplayFor(Function(modelItem) item.BANGUMIHYOJ)
                            </td>
                            <td class="hyoj">
                                @Html.DisplayFor(Function(modelItem) item.KKNHYOJ)
                            </td>
                            <td class="kskhyoj">
                                @Html.DisplayFor(Function(modelItem) item.KSKHYOJ)
                            </td>
                            <td class="anahyoj">
                                @Html.DisplayFor(Function(modelItem) item.ANAHYOJ)
                            </td>
                            <td class="hyoj">
                                @Html.DisplayFor(Function(modelItem) item.BASYOHYOJ)
                            </td>
                             <td class="hyoj">
                                 @Html.DisplayFor(Function(modelItem) item.BIKOHYOJ)
                             </td>
                             <td class="hyoj">
                                 @Html.DisplayFor(Function(modelItem) item.NAIYOHYOJ)
                             </td>
                            <td class="tantohyoj">
                                @Html.DisplayFor(Function(modelItem) item.TANTOHYOJ)
                            </td>
                            <td class="hyoj">
                                @Html.DisplayFor(Function(modelItem) item.RENRKHYOJ)
                            </td>                         
                            <td class="hyoj">
                                @Html.DisplayFor(Function(modelItem) item.SYUCHO)
                            </td>
                             <td class="alertflg">
                                 @Html.DisplayFor(Function(modelItem) item.ALERTFLG)
                             </td>
                            @*<td>
                            @Html.DisplayFor(Function(modelItem) item.STATUS)
                        </td>
                            *@
                            <td class="col1">
                                @Html.ActionLink("参照", "Details", New With {.id = item.CATCD})
                            </td>

                        </tr>
                    Next

                </tbody>

            </table>

        </td>
    </tr>

</table>
