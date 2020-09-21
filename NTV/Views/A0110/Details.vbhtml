@ModelType NTV_SHIFT.M0010
@Code
    ViewData("Title") = "ユーザー設定"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h3>参照</h3>

<div>
   
    <hr />
    <dl class="dl-horizontal">

        <dt>
            @Html.DisplayNameFor(Function(model) model.USERNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.USERNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.LOGINID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LOGINID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.USERSEX)
        </dt>

        <dd>
            @*@Html.DisplayFor(Function(model) model.USERSEX)*@
           
            <label class="radio-inline" disabled="disabled">
                @Html.RadioButtonFor(Function(model) model.USERSEX, "False")
                男
            </label>
            <label class="radio-inline" disabled="disabled">
                @Html.RadioButtonFor(Function(model) model.USERSEX, "True")
                女
            </label>
        </dd>

         <dt>
            @Html.Label("公休")
        </dt>
        <dd>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.KOKYU1)
                @Html.DisplayNameFor(Function(model) model.KOKYU1)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.KOKYU2)
                @Html.DisplayNameFor(Function(model) model.KOKYU2)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.KOKYU3)
                @Html.DisplayNameFor(Function(model) model.KOKYU3)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.KOKYU4)
                @Html.DisplayNameFor(Function(model) model.KOKYU4)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.KOKYU5)
                @Html.DisplayNameFor(Function(model) model.KOKYU5)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.KOKYU6)
                @Html.DisplayNameFor(Function(model) model.KOKYU6)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.KOKYU7)
                @Html.DisplayNameFor(Function(model) model.KOKYU7)
            </label>
        </dd>

        <!-- Havan[14 Oct 2019] : Added code for leagal Holidays -->
        <dt>
            @Html.Label("法休")
        </dt>
        <dd>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.HOKYU1)
                @Html.DisplayNameFor(Function(model) model.HOKYU1)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.HOKYU2)
                @Html.DisplayNameFor(Function(model) model.HOKYU2)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.HOKYU3)
                @Html.DisplayNameFor(Function(model) model.HOKYU3)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.HOKYU4)
                @Html.DisplayNameFor(Function(model) model.HOKYU4)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.HOKYU5)
                @Html.DisplayNameFor(Function(model) model.HOKYU5)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.HOKYU6)
                @Html.DisplayNameFor(Function(model) model.HOKYU6)
            </label>
            <label class="checkbox-inline">
                @Html.DisplayFor(Function(model) model.HOKYU7)
                @Html.DisplayNameFor(Function(model) model.HOKYU7)
            </label>
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.HYOJJN)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HYOJJN)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.HYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.M0050.HYOJNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0050.HYOJNM)
        </dd>

        @*<dt>
                @Html.DisplayNameFor(Function(model) model.STATUS)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.STATUS)
            </dd>*@

        <dt>
            @Html.DisplayNameFor(Function(model) model.MAILLADDESS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.MAILLADDESS)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KEITAIADDESS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KEITAIADDESS)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KOKYUTENKAI)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KOKYUTENKAI)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KOKYUTENKAIALL)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KOKYUTENKAIALL)
        </dd>
       
        <!--ASI[26 Nov 2019]: Added for SPORTCategory-->
        <dt>
            @Html.DisplayNameFor(Function(model) model.SportCatNmComaSeperatedString)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SportCatNmComaSeperatedString)
        </dd>
    </dl>
</div>
<p>
    @Html.ActionLink("修正", "Edit", New With {.id = Model.USERID}) |
    @Html.ActionLink("一覧に戻る", "Index")
</p>
