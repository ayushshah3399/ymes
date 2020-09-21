@ModelType NTV_SHIFT.M0090
@Code
    ViewData("Title") = "業務一括登録"
End Code

<h3>参照</h3>

<div>
   
    <hr />
    <dl class="dl-horizontal">
        @*<dt>
            @Html.DisplayNameFor(Function(model) model.M0010.LOGINID)
        </dt>*@

        @*<dd>
            @Html.DisplayFor(Function(model) model.M0010.LOGINID)
        </dd>*@

        <dt>
            @Html.DisplayNameFor(Function(model) model.IKKATUMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IKKATUMEMO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMYMD)
            ～
            @Html.DisplayFor(Function(model) model.GYOMYMDED)
        </dd>

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMYMDED)
        </dd>*@

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTNFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTNFLG)
            </dd>

            <dd>
              @Html.DisplayFor(Function(model) model.PTN1)
            @Html.DisplayNameFor(Function(model) model.PTN1)

            @Html.DisplayFor(Function(model) model.PTN2)
            @Html.DisplayNameFor(Function(model) model.PTN2)

            @Html.DisplayFor(Function(model) model.PTN3)
            @Html.DisplayNameFor(Function(model) model.PTN3)


            @Html.DisplayFor(Function(model) model.PTN4)
            @Html.DisplayNameFor(Function(model) model.PTN4)

            @Html.DisplayFor(Function(model) model.PTN5)
            @Html.DisplayNameFor(Function(model) model.PTN5)

            @Html.DisplayFor(Function(model) model.PTN6)
            @Html.DisplayNameFor(Function(model) model.PTN6)

            @Html.DisplayFor(Function(model) model.PTN7)
            @Html.DisplayNameFor(Function(model) model.PTN7)
        </dd>
   
        <dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKJKNST)
            ～
            @Html.DisplayFor(Function(model) model.KSKJKNED)
        </dd>

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
        </dt>*@

        @*<dd>
            @Html.DisplayFor(Function(model) model.KSKJKNED)
        </dd>*@

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
            @Html.DisplayNameFor(Function(model) model.OAJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OAJKNST)
            ～
            @Html.DisplayFor(Function(model) model.OAJKNED)
        </dd>

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.OAJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OAJKNED)
        </dd>*@

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

        <dt>
            アナウンサー
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0010.USERNM)
        </dd>
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
            @Html.DisplayNameFor(Function(model) model.BIKO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BIKO)
        </dd>









        @*<dt>
            @Html.DisplayNameFor(Function(model) model.INSTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTDT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTDT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTTERM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTTERM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTPRGNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTPRGNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTDT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTDT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTTERM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTTERM)
        </dd>*@

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.UPDTPRGNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTPRGNM)
        </dd>*@

    </dl>
</div>
<p>
    @Html.ActionLink("修正", "Edit", New With {.id = Model.IKKATUNO}) |
    @Html.ActionLink("一覧に戻る", "Index")
</p>
