@ModelType IEnumerable(Of NTV_SHIFT.W0030)

<div class="modal-header">
    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
    <div>
        <label class="control-label">担当アナ : </label>
        <label class="control-label" id="lblusernm">@ViewBag.Usernm</label>
    </div>
    <h4 class="modal-title">@ViewBag.Title </h4>
</div>

<div class="modal-body">
    @Html.Hidden("yoinid", ViewBag.Yoinid)
    @Html.Hidden("lastyoinflg", ViewBag.LastYoinFlg)


    <div class="panel panel-default">
        <table class="table">
            @For Each item In Model
                @<tr>
                    <td>
                        <dl class="dl-horizontal">
                            <dt>
                                @Html.DisplayNameFor(Function(model) model.GYOMYMD)
                            </dt>
                            <dd>
                                @Html.Hidden("wgyomno", item.GYOMNO)
                                @Html.DisplayFor(Function(model) item.GYOMYMD)
                                @If item.GYOMYMDED IsNot Nothing Then
                                    @Html.Encode("～")
                                    @Html.DisplayFor(Function(model) item.GYOMYMDED)
                                End If
                            </dd>
                            <dt>
                                @Html.DisplayNameFor(Function(model) model.KSKJKNST)
                            </dt>
                            <dd>
                                @item.KSKJKNST.Substring(0, 2)@Html.Encode(":")@item.KSKJKNST.Substring(2, 2)
                                ～
                                @item.KSKJKNED.Substring(0, 2)@Html.Encode(":")@item.KSKJKNED.Substring(2, 2)
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
                            </dt>
                            <dd>
                                @Html.DisplayFor(Function(model) item.M0020.CATNM)
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                            </dt>
                            <dd>
                                @Html.DisplayFor(Function(model) item.BANGUMINM)
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.OAJKNST)
                            </dt>
                            <dd>
                                @If item.OAJKNST IsNot Nothing Then
                                    @item.OAJKNST.Substring(0, 2)@Html.Encode(":")@item.OAJKNST.Substring(2, 2)
                                End If
                                @If item.OAJKNED IsNot Nothing Then
                                    @Html.Encode("～")
                                    @item.OAJKNED.Substring(0, 2)@Html.Encode(":")@item.OAJKNED.Substring(2, 2)
                                End If
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.NAIYO)
                            </dt>
                            <dd>
                                @Html.DisplayFor(Function(model) item.NAIYO)
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.BASYO)
                            </dt>
                            <dd>
                                @Html.DisplayFor(Function(model) item.BASYO)
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                            </dt>
                            <dd>
                                @Html.DisplayFor(Function(model) item.BANGUMITANTO)
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                            </dt>
                            <dd>
                                @Html.DisplayFor(Function(model) item.BANGUMIRENRK)
                            </dd>

                            <dt>
                                @Html.DisplayNameFor(Function(model) model.BIKO)
                            </dt>
                            <dd>
                                @Html.DisplayFor(Function(model) item.BIKO)
                            </dd>

                        </dl>

                    </td>
                </tr>

                @*@<tr>
                        <td style="text-align:right">
                            <button type="button" id="btnEditWGyom" class="btn btn-success btn-sm" data-gyomno="@item.GYOMNO" onclick="btnEditWGyomClicked()">修正</button>
                        </td>
                    </tr>*@

            Next

        </table>

    </div>

    <div class="message">
        @Html.Raw(ViewBag.Message.ToString.Replace(vbCrLf, "<br />"))
    </div>
</div>


