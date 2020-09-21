
<h5>伝言板</h5>

@If ViewBag.USERID <> "a" Then
    @<div >
    <textarea class="form-control" rows="3" id="textArea"></textarea>
    <span class="help-block">伝言は2週間で消えます。</span>

    <input type="submit" value="送信" class="btn btn-primary" />

</div>
@<br />

    @*<div class="form-group">
            <div class="col-lg-10">
                <textarea class="form-control" rows="3" id="textArea"></textarea>
                <span class="help-block">伝言は2週間で消えます。</span>

            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="送信" class="btn btn-default" />
            </div>
        </div>
        <br />*@
End If

<div class="panel panel-success">
    @*<button type="button" class="close" data-dismiss="alert">&times;</button>*@
    <div class="panel-heading">
        2016/11/01<br />デスクA
    </div>
    <div class="panel-body">
        XXXXXXXX　XXXXXX　XXXXXXXXXX
    </div>
</div>


<div class="panel panel-success">
    @*<button type="button" class="close" data-dismiss="alert">&times;</button>*@
    <div class="panel-heading">
        2016/11/01<br />デスクA
    </div>
    <div class="panel-body">
       <p>あいおえおあいおえおあいおえお</p> 
    </div>
</div>

<div class="panel panel-success">
    @*<button type="button" class="close" data-dismiss="alert">&times;</button>*@
    <div class="panel-heading">
        2016/11/01<br />デスクC
    </div>
    <div class="panel-body">
        CCCCCCCC
    </div>
</div>

