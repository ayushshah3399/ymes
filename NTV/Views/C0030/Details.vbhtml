@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "Details"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Details</h2>

<div>
    <h4>D0010</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0020.CATNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.M0090.IKKATUMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0090.IKKATUMEMO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMYMD)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMYMDED)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKJKNST)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKJKNED)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JTJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JTJKNST)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JTJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JTJKNED)
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
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OAJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OAJKNED)
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
            @Html.DisplayNameFor(Function(model) model.BIKO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BIKO)
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
            @Html.DisplayNameFor(Function(model) model.RNZK)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RNZK)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PGYOMNO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PGYOMNO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.IKTFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IKTFLG)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.IKTUSERID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IKTUSERID)
        </dd>

        <dt>
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
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTPRGNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTPRGNM)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.GYOMNO }) |
    @Html.ActionLink("Back to List", "Index")
</p>
