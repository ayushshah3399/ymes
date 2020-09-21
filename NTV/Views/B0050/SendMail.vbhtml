@ModelType NTV_SHIFT.W0040
@Code
    ViewData("Title") = "メール送信"
    Dim strKey As String = ""
End Code

<style>
    .table-scroll {
        width: 310px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-scroll tbody {
        height: 150px;
        width: 310px;
        overflow-y: auto;
        overflow-x: hidden;
    }

      .table > thead > tr > th{
       padding:3px;
   }

    .colAna{
        width:150px;
    }
    .colSousian{
        width:130px;       
    }
</style>

<h3>メールを送信しますか？</h3>

@Using (Html.BeginForm("SendMail", "B0050", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
    @Html.AntiForgeryToken()

@Html.Hidden("name", ViewData("name"))
@Html.Hidden("userid", ViewData("id"))
@Html.Hidden("showdate", ViewData("searchdt"))
    @<div class="row">
        <div class="col-sm-3">
            <table id="tblAna" class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th class="colAna">
                            アナウンサー
                        </th>
                        <th class="colSousian">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    @For i = 0 To Model.W0050.Count - 1
                            strKey = String.Format("W0050[{0}].", i)
                            Dim item = Model.W0050(i)
                        @<tr>
                            <td class="colAna">
                                @item.M00101.USERNM
                                @Html.Hidden(strKey & "USERID", item.USERID)
                          </td>
                            <td class="colSousian" style="padding: 5px;">
                                @Html.DropDownList(strKey & "MAILSOUSIAN", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                            </td>
                        </tr>
                    Next
                </tbody>
            </table>
        </div>
    </div>


    @<div class="row">
        <div class="col-sm-3" style="text-align:right">
            <button type="submit" id="btnOK"  class="btn btn-default btn-sm" data-dismiss="modal">OK</button>
        </div>
    </div>
    @<hr />

    @<div>
         @Html.HiddenFor(Function(model) model.ACUSERID)
         @Html.HiddenFor(Function(model) model.SHORIKBN)
         @Html.HiddenFor(Function(model) model.GYOMNO)
         @Html.HiddenFor(Function(model) model.GYOMYMD)
         @Html.HiddenFor(Function(model) model.MAILSUBJECT)
         @Html.HiddenFor(Function(model) model.MAILBODYHEAD)
         @Html.HiddenFor(Function(model) model.MAILBODY)
         @Html.HiddenFor(Function(model) model.MAILAPPURL)
     

         <p>subject @Html.Encode("：") @Html.DisplayFor(Function(model) model.MAILSUBJECT)</p>
         <hr />

         <p>@Html.Raw(Model.MAILBODYHEAD.ToString.Replace(vbCrLf, "<br />"))</p>

         <div class="form-inline">
             @Html.DisplayNameFor(Function(model) model.MAILNOTE)
             @Html.Encode(" ： ")
             @Html.EditorFor(Function(model) model.MAILNOTE, New With {.htmlAttributes = New With {.class = "form-control input-sm", .style = "width:500px;max-width:800px;height:150px;"}})
             @Html.ValidationMessageFor(Function(model) model.MAILNOTE, "", New With {.class = "text-danger"})
         </div>
         <br />

         <p>@Html.Raw(Html.Encode(Model.MAILBODY.ToString).Replace(" ", "&ensp;").Replace(vbCrLf, "<br />"))</p>

         <br />
         <br />

         
    <div>@Html.DisplayNameFor(Function(model) model.MAILAPPURL)</div>
    <u>@Model.MAILAPPURL</u>


       @* <div class="body">
            ログインユーザー @Session("LoginUsernm") さんによって&nbsp;&nbsp;@Model.UPDTDT.ToString("yyyy年MM月dd日 HH：mm ")に <br />
            業務が登録されました。
        </div>
        <br />


        <div>
        業務期間
        @Html.Encode("：")
        @Html.DisplayFor(Function(model) model.GYOMYMD)
        @Html.Encode("～")
        @Html.DisplayFor(Function(model) model.GYOMYMDED)
    </div>

    <div>
        繰り返し
        @Html.Encode("：")
    </div>

    <div>
        拘束時間
        @Html.Encode("：")
        @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(2, 2)
        @Html.Encode("～")
        @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(2, 2)
    </div>

    <div>
        @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        @Html.Encode("：")
        @Html.DisplayFor(Function(model) model.M0020.CATNM)
    </div>
    <div>
        @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        @Html.Encode("：")
        @Html.DisplayFor(Function(model) model.BANGUMINM)
    </div>

    <div>
        @Html.DisplayNameFor(Function(model) model.NAIYO)
        @Html.Encode("：")
        @Html.DisplayFor(Function(model) model.NAIYO)
    </div>

    <div>
        @Html.DisplayNameFor(Function(model) model.BASYO)
        @Html.Encode("：")
        @Html.DisplayFor(Function(model) model.BASYO)
    </div>

   *@

    </div>


End Using



<script type="text/javascript">
    //OKボタン押した時、メールのコメントの桁数チェック
    $(function () {
        $('#btnOK').click(function (e) {
            e.preventDefault();
            var err = '';
            $('div span[data-valmsg-for="MAILNOTE"]').text("");
            var MAILNOTE = $('#MAILNOTE').val();
          
            if (getByteCount(MAILNOTE) > 1000) {
              
                $('div span[data-valmsg-for="MAILNOTE"]').text("文字数がオーバーしています。");
                err = '1';
            }

            if (err == '1') {
                return false
            } else {

                $("#myForm").submit();

            }
        });

    });

    function getByteCount(str) {
        var b = str.match(/[^\x00-\xff]/g);
        return (str.length + (!b ? 0 : b.length));
    }

</script>