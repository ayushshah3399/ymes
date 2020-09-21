@ModelType NTV_SHIFT.D0080
@Code
    ViewData("Title") = "Create"
End Code

<h5>伝言板</h5>


@Using (Html.BeginForm("Create", "D0080", FormMethod.Post))
    @Html.AntiForgeryToken()

@<div class="form-horizontal">

    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})


    <div class="form-group">

        <div class="col-md-12">
            @If ViewData("Kanri") = "1" Then
                @<textarea class="form-control" id="MESSAGE" name="MESSAGE" rows="3"></textarea>
            End If

            <label id="Error"></label>
            <span class="help-block">伝言は2週間で消えます。</span>

            @If ViewData("Kanri") = "1" Then
                @<input id="update" type="submit" value="送信" class="btn btn-primary" />
                  End If

        </div>

    </div>
    @Html.Hidden("formnm", "C0040")
    @Html.HiddenFor(Function(model) model.USERID)
    @Html.HiddenFor(Function(model) model.TOROKUYMD)
    @Html.HiddenFor(Function(model) model.DATAFLG)

</div>
End Using


<script type="text/javascript">


    $(function () {
        $('#update').click(function () {
            var message = $('#MESSAGE').val()
            $("#Error").text("")
            var result = confirm("更新します。よろしいですか?")

            if (result == false) {
                return false
            }

            if (message.length == 0) {


                $("#Error").text("メッセージが必要です。");
                document.getElementById('Error').style.color = 'red';
                return false
            }
            else {
                if (getByteCount(message) > 256) {
                    $("#Error").text("文字数がオーバーしています。");
                    document.getElementById('Error').style.color = 'red';
                    return false
                }
                
            }

        
           

        });
    });

    function getByteCount(str)
    {
        var b = str.match(/[^\x00-\xff]/g);
        return (str.length + (!b ? 0: b.length));
    }
    </script>