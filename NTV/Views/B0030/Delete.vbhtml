@ModelType NTV_SHIFT.D0050
@Code
    ViewData("Title") = "業務承認"
End Code

<h2>削除</h2>

<h3>削除してもよろしいですか？</h3>
<div>
  
    <hr />
    <dl class="dl-horizontal">
        <dt>
          申請者
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0010.USERNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.CATCD)
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
            @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(2, 2)
            
            ～
           
            @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(2, 2)
        </dd>

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKJKNED)
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

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.GYOMMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMMEMO)
        </dd>*@

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
            @Html.DisplayNameFor(Function(model) model.GYOMMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMMEMO)
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
