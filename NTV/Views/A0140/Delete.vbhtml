@ModelType NTV_SHIFT.M0040
@Code
    ViewData("Title") = "内容設定"
End Code

<h3>削除</h3>

<h3>削除してもよろしいですか？</h3>
<div>
    
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.NAIYO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NAIYO)
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
