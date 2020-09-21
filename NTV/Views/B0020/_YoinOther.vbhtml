
<div class="modal-header">
    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
    <div>
        <label class="control-label">担当アナ : </label>
        <label class="control-label" id="lblusernm">@ViewBag.Usernm</label>
    </div>
    <h4 class="modal-title">@ViewBag.Title </h4>
</div>

<div class="modal-body">
    @Html.Hidden("yoinid", ViewBag.Yoinid)
    @Html.Hidden("lastyoinflg", ViewBag.LastYoinFlg)


    <div class="message">
        @Html.Raw(ViewBag.Message.ToString.Replace(vbCrLf, "<br />"))
    </div>

</div>

