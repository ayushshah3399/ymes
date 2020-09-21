<style>
    .modal-body dt,dd{
        padding:3px;
    }
    .dl-horizontal dt {
        width:300px
    }
</style>
<div class="modal fade" id="myModalCnt" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">一括本登録時の休日設定変更結果</h4>
            </div>
            <div class="modal-body">

                <form>
                                <dl class="dl-horizontal">
                                    <dt>強休、又は時間強休のため、未登録のもの ：
                                    </dt>
                                    <dd >「@TempData("Cntkyokyu")」件
                                    </dd>
                                    <dt>公休出に変更のもの ：
                                    </dt>
                                    <dd >「@TempData("Cntkoukyu")」件
                                    </dd>
                                    <dt>法休出に変更のもの ：
                                    </dt>
                                    <dd >「@TempData("Cnthoukyu")」件
                                    </dd>
                                    <dt>削除した代休 ：
                                    </dt>
                                    <dd >「@TempData("Cntdaikyu")」件
                                    </dd>
                                    <dt>削除した時間休 ：
                                    </dt>
                                    <dd >「@TempData("Cntfurikyu")」件
                                    </dd>
                                    <dt>24時超え公休出に変更のもの ：
                                    </dt>
                                    <dd >「@TempData("Cnt24koe")」件
                                    </dd>
                                    <dt>24時超え法休出に変更のもの ：
                                    </dt>
                                    <dd >「@TempData("Cnt24koehoukyu")」件
                                    </dd>
                                     </dl>
                           </form>
                
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>
