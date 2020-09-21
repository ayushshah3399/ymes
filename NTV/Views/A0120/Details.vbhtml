@ModelType NTV_SHIFT.M0020
@Code
    ViewData("Title") = "カテゴリー設定"
End Code

<h3>参照</h3>

<div>
    
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.CATNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.CATNM)
        </dd>

      
        <dt>
            @Html.DisplayNameFor(Function(model) model.HYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HYOJ)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.HYOJJN)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HYOJJN)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OATIMEHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OATIMEHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMIHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMIHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KKNHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KKNHYOJ)
        </dd>


        <dt>
            @Html.DisplayNameFor(Function(model) model.KSKHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ANAHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ANAHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BASYOHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BASYOHYOJ)
        </dd>
            

        <dt>
            @Html.DisplayNameFor(Function(model) model.BIKOHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BIKOHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.NAIYOHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NAIYOHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TANTOHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TANTOHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.RENRKHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RENRKHYOJ)
        </dd>
      
        <dt>
            @Html.DisplayNameFor(Function(model) model.SYUCHO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SYUCHO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ALERTFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ALERTFLG)
        </dd>

        @*<dt>
        @Html.DisplayNameFor(Function(model) model.STATUS)
    </dt>

    <dd>
        @Html.DisplayFor(Function(model) model.STATUS)
    </dd>*@

    </dl>
</div>
<p>
    @Html.ActionLink("修正", "Edit", New With {.id = Model.CATCD}) |
    @Html.ActionLink("一覧に戻る", "Index")
</p>
