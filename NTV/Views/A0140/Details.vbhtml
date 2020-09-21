@ModelType NTV_SHIFT.M0040
@Code
    ViewData("Title") = "内容設定"
End Code

<h3>参照</h3>

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
</div>
<p>
    @Html.ActionLink("修正", "Edit", New With {.id = Model.NAIYOCD}) |
    @Html.ActionLink("一覧に戻る", "Index")
</p>
