@ModelType NTV_SHIFT.M0030
@Code
    ViewData("Title") = "番組設定"
End Code

<h3>参照</h3>

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
</div>
<p>
    @Html.ActionLink("修正", "Edit", New With {.id = Model.BANGUMICD}) |
    @Html.ActionLink("一覧に戻る", "Index")
</p>
