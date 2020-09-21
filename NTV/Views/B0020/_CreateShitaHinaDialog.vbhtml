<style>
    .modal-body dt,dd{
        padding:3px;
    }
</style>

<div class="modal fade" id="myModalShitaHina" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">下書保存確認</h4>
            </div>
            <div class="modal-body">
                <form>
                    <dl class="dl-horizontal">
                        <dt>
                            メモ
                        </dt>
                        <dd id="ddHINAMEMO">
                            <input type="text" class="form-control input-sm" id="modalHINAMEMO">
                            <div id="errorHinamemo" class="text-danger"></div>
                        </dd>
                        <dt>
                            区分
                        </dt>
                        <dd id="ddDATAKBN">
                            <label class="radio-inline">
                                @Html.RadioButton("modalDATAKBN", 1)
                                個人
                            </label>
                            <label class="radio-inline">
                                @Html.RadioButton("modalDATAKBN", 2, True)
                                共有
                            </label>
                        </dd>
                        <dt>
                            パターン登録
                        </dt>
                        <dd id="ddPATTERN">

                        </dd>
                        <dt>
                            業務期間
                        </dt>
                        <dd id="ddGYOMYMD">

                        </dd>
                        <dt>
                            拘束時間
                        </dt>
                        <dd id="ddKSKJKNST">

                        </dd>
                        <dt>
                            カテゴリー
                        </dt>
                        <dd id="ddCATNM">

                        </dd>
                        <dt>
                            番組名
                        </dt>
                        <dd id="ddBANGUMINM">

                        </dd>
                        <dt>
                            OA時間
                        </dt>
                        <dd id="ddOAJKNST">

                        </dd>
                        <dt>
                            内容
                        </dt>
                        <dd id="ddNAIYO">

                        </dd>
                        <dt>
                            場所
                        </dt>
                        <dd id="ddBASYO">

                        <dt>
                            担当アナ
                        </dt>
                        <dd>
                            <text id="modalAnalist"></text>
                            <input type="text" class="hidden" id="modalAnaidlist" />
                           <button type="button" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModalAna" data-kbn="1">選択</button>
                            @*<span style="float:right;">
                                </span>*@
                        </dd>

                        <dt>
                            仮アナ
                        </dt>
                        <dd>
                            <text id="modalKarianalist"></text>
                            <button type="button" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModalAna" data-kbn="2">選択</button>
                        </dd>

                        <dt>
                            番組担当者
                        </dt>
                        <dd id="ddBANGUMITANTO">

                        </dd>
                        <dt>
                            連絡先
                        </dt>
                        <dd id="ddBANGUMIRENRK">

                        </dd>
                        <dt>
                            備考
                        </dt>
                        <dd id="ddBIKO">

                        </dd>
                    </dl>

                </form>
            </div>
            <div class="modal-footer">
                <button type="submit" class="btn btn-default" id="modal-save" data-dismiss="modal" style="float: left;">新規保存</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal">キャンセル</button>

            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="myModalAna" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-sm" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">アナウンサー選択</h4>
                <input type="text" class="hidden" id="kbn" />
            </div>
            <div class="modal-body">

            </div>
            <div class="modal-footer">
                <button id="modal-selectana" type="button" class="btn btn-default" data-dismiss="modal" style="float: left;">選択</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal">閉じる</button>
            </div>
        </div>
    </div>
</div>

