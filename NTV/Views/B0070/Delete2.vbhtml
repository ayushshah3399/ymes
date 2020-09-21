@ModelType NTV_SHIFT.D0090

<h2>削除</h2>

<h3>削除してもよろしいですか？</h3>
<div>
    @*<h4>D0090</h4>*@
    <hr />
    <dl class="dl-horizontal">

        <dt>
            @Html.DisplayNameFor(Function(model) model.DATAKBN)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DATAKBN)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.HINAMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HINAMEMO)
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
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKJKNST)
            ～
            @Html.DisplayFor(Function(model) model.KSKJKNED)
        </dd>

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKJKNED)
        </dd>*@

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMINM)
        </dd>

        <dt>
            @*@Html.DisplayNameFor(Function(model) model.M0010.LOGINID)*@
            担当アナ
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0010.LOGINID)
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

        <dt>
            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0020.CATNM)
        </dd>
       
        <dt>
            @Html.DisplayNameFor(Function(model) model.OAJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OAJKNST)
            ～
            @Html.DisplayFor(Function(model) model.OAJKNED)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTDT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTDT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTNFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTNFLG)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTN1)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTN1)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTN2)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTN2)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTN3)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTN3)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTN4)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTN4)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTN5)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTN5)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTN6)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTN6)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTN7)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTN7)
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
    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="削除" class="btn btn-default" /> |
            @Html.ActionLink("一覧に戻る", "Index")
        </div>
    End Using
</div>
