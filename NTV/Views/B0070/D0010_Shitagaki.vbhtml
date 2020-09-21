@ModelType IEnumerable(Of NTV_SHIFT.D0090)
@Code
ViewData("Title") = "D0010_Shitagaki"
End Code

<h3>下書一覧</h3>

<style>
    .table-bordered th {
        white-space: nowrap;
    }

    .table-bordered td {
        /*width:1%;*/
        /*min-width:100px;*/
        white-space: nowrap;
    }
</style>



<div class="container-fluid">
    <div class="row" style="padding-top:5px;overflow-x:auto">
        <table class="table table-bordered">
            <tr>
                <th>

                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.DATAKBN)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.HINAMEMO)
                </th>
                <th colspan="2">
                    @Html.DisplayNameFor(Function(model) model.GYOMYMD)
                </th>
                @*<th>
                        @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
                    </th>*@

                <th colspan="2">
                    @Html.DisplayNameFor(Function(model) model.KSKJKNST)
                </th>
                @*<th>
                        @Html.DisplayNameFor(Function(model) model.KSKJKNED)
                    </th>*@

                <th>
                    @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                </th>

                <th>
                    担当アナ
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.NAIYO)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.BASYO)
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
                </th>

                <th colspan="2">
                    @Html.DisplayNameFor(Function(model) model.OAJKNST)
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.INSTDT)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.INSTID)
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.PTNFLG)
                </th>


                <th>
                    @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.BIKO)
                </th>


                <th>

                </th>

            </tr>

            @For Each item In Model
                @<tr>
                    <td>
                        @*@Html.ActionLink("選択", "Create", "B0020")*@
                        @Html.ActionLink("選択", "Create", "B0020", routeValues:=New With {.para1 = item.BANGUMINM.ToString, .para2 = item.BANGUMITANTO, .para3 = item.BANGUMIRENRK, _
                          .para4 = item.BIKO, .para5 = item.KSKJKNST, .para6 = item.KSKJKNED, .para7 = item.CATCD, _
                         .para8 = item.GYOMYMD, .para9 = item.GYOMYMDED, .para10 = item.OAJKNST, .para11 = item.OAJKNED, _
                                      .para12 = item.NAIYO, .para13 = item.BASYO}, htmlAttributes:=Nothing)


                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.DATAKBN)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.HINAMEMO)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.GYOMYMD)

                    </td>
                    <td>

                        @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
                    </td>
                    @*<td>
                            @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
                        </td>*@

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.KSKJKNST)

                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.KSKJKNED)
                    </td>

                    @*<td>
                            @Html.DisplayFor(Function(modelItem) item.KSKJKNED)
                        </td>*@

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                    </td>

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.NAIYO)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.BASYO)
                    </td>

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                    </td>

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.OAJKNST)

                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.OAJKNED)

                    </td>

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.INSTDT)

                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.INSTID)

                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.PTNFLG)
                    </td>

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)

                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)

                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.BIKO)

                    </td>

                    <td>

                        @Html.ActionLink("削除", "Delete2", New With {.id = item.HINANO})
                    </td>
                </tr>
            Next

        </table>

        <div class="form-group">
            @Html.ActionLink("閉じる", "Create", "B0020")
            @*<div class="form-inline">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="選択" class="btn btn-default" />
                        &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
                        <input type="submit" value="削除" class="btn btn-default" />
                        &nbsp &nbsp
                       <input type="submit" value="閉じる" class="btn btn-default" />
                        @Html.ActionLink("閉じる", "Create", "D0010")
                    </div>

                </div>*@
        </div>
    </div>
</div>
    



