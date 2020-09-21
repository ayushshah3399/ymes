@ModelType NTV_SHIFT.M0060
@Code
    ViewData("Title") = "休暇コード設定"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h3>削除</h3>

<h3>削除してもよろしいですか？</h3>
<div>
   
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.KYUKNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KYUKNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KYUKRYKNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KYUKRYKNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BACKCOLOR)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BACKCOLOR)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.WAKUCOLOR)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.WAKUCOLOR)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FONTCOLOR)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FONTCOLOR)
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
            @Html.DisplayNameFor(Function(model) model.TNTHYOHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TNTHYOHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KYUJITUHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KYUJITUHYOJ)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.SHINSEIHYOJ)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SHINSEIHYOJ)
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
