@ModelType NTV_SHIFT.D0110
@Code
    ViewData("Title") = "デスクメモ"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h3>参照</h3>

<div>
    @*<h4>デスクメモ</h4>*@
    <hr />
    <dl class="dl-horizontal">
        <dt>確認</dt>
        <dd>@Model.M0100.KAKUNINNM</dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.USERID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0010.USERNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0020.CATNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMINM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.NAIYO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NAIYO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BASYO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BASYO)
        </dd>
        <dt>シフト日</dt>
        @If Model.D0120.Count > 0 Then
            @For Each item In Model.D0120
                @<dd>
                    @Html.DisplayFor(Function(model) item.SHIFTYMDST)
                    @If item.SHIFTYMDED IsNot Nothing AndAlso item.SHIFTYMDST <> item.SHIFTYMDED Then
                        @Html.Encode("～")
                        @Html.DisplayFor(Function(model) item.SHIFTYMDED)
                    End If

                    @If item.KSKJKNST IsNot Nothing OrElse item.KSKJKNED IsNot Nothing Then
                        @Html.Encode("　")
                    If item.KSKJKNST IsNot Nothing Then
                        @Html.Encode(item.KSKJKNST.ToString.Substring(0, 2) & ":" & item.KSKJKNST.ToString.Substring(2, 2))
                    End If

                        @Html.Encode("～")
                    If item.KSKJKNED IsNot Nothing Then
                        @Html.Encode(item.KSKJKNED.ToString.Substring(0, 2) & ":" & item.KSKJKNED.ToString.Substring(2, 2))
                    End If
                    End If
                </dd>
            Next
                    Else
            @<dd></dd>
        End If


        <dt>担当アナ</dt>
        @If Model.D0130.Count > 0 Then
            @For Each item In Model.D0130
                @<dd>
                    @Html.DisplayFor(Function(model) item.M0010.USERNM)

                </dd>
            Next
            Else
            @<dd></dd>
        End If

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMITANTO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMIRENRK)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DESKMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DESKMEMO)
        </dd>


    </dl>
</div>
<p>
    @Html.ActionLink("修正", "Edit", New With {.id = Model.DESKNO}) |

    @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.CondDeskno = Session("CondDeskno"), .CondKakunin1 = Session("CondKakunin1"), .CondKakunin2 = Session("CondKakunin2"),
                        .CondInstst = Session("CondInstst"), .CondInsted = Session("CondInsted"), .CondDeskid = Session("CondDeskid"), .CondCatcd = Session("CondCatcd"),
                        .CondBanguminm = Session("CondBanguminm"), .CondNaiyo = Session("CondNaiyo"), .CondShiftst = Session("CondShiftst"), .CondShifted = Session("CondShifted"),
                        .CondAnaid = Session("CondAnaid"), .CondBangumitanto = Session("CondBangumitanto"), .CondBangumirenrk = Session("CondBangumirenrk"), .CondBasyo = Session("CondBasyo")})


</p>
