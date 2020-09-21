@ModelType NTV_SHIFT.M0030
@Code
    ViewData("Title") = "番組設定"
End Code

<h3>削除</h3>

<h3>削除してもよろしいですか？</h3>
<div>
   
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMINM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMIKN)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMIKN)
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
