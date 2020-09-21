@ModelType NTV_SHIFT.W0040
@Code
    ViewData("Title") = "メール送信"
    Dim strKey As String = ""
End Code

<style>
    .table-scroll {
        width: 540px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-scroll tbody {
        height: 150px;
        width: 540px;
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
        width:190px;       
    }
     .colSENDKEITAI{
        width:200px;       
    }

     //ASI[25 Oct 2019] : belowed 3 consecutive classes are for design of table for Email address To & CC
    .colMailDropDown{
        width:200px;
        padding:3px;
    }
    .colDeleteBtn{
        width:50px;
        padding:3px;
    }
    .customeclass
    {
        width:380px; 
        margin-bottom: 21px;
    }
</style>

<h3>メールを送信しますか？</h3>


@Using (Html.BeginForm("SendMail", "B0020", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
    @Html.AntiForgeryToken()

    @<div class="row">
        <div class="col-sm-4">
            <table id="tblAna" class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th class="colAna">
                            アナウンサー
                        </th>
                        <th class="colSousian">
                        </th>
                        <th class="colSENDKEITAI">
                            携帯メール送信要
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
                                @Html.DropDownList(strKey & "MAILSOUSIAN", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .onChange = "SetSENDKEITAI(" & i & ")"})
                            </td>
                             <td class="colSENDKEITAI" style="text-align:center;">
                                 @Html.CheckBox(strKey & "SENDKEITAI", item.SENDKEITAI)
                             </td>
                        </tr>
                    Next
                </tbody>
            </table>
           
        </div>
    </div>

    @* //ASI[25 Oct 2019] START: belowed 2 consecutive div-row are added design of table for Email address To & CC *@
    @<div class="row">
        <div class="col-sm-3">
            
            <div class="table table-bordered customeclass" style ="margin-bottom:0px;background-color: #F0FFF0;padding: 3px;">         
                        
                             @Html.Label("To :", htmlAttributes:=New With {.class = "control-label" })
                             &nbsp;&nbsp;&nbsp;&nbsp;
                             <input id="btn_mailToAdd" type="button" value="追加" class="btn btn-success btn-xs" />
                         
                    
            </div>
            <table class="table table-bordered customeclass" id="mailToTable" >
                 <!-- <thead>
                     <tr>
                         <th style="text-align:left;" colspan="2">
                             @Html.Label("To :", htmlAttributes:=New With {.class = "control-label"})
                             &nbsp;&nbsp;&nbsp;
                             <input id="btn_mailToAdd" type="button" value="追加" class="btn btn-success btn-xs" />
                          </th>
                     </tr>
                 </thead>
                 <tbody>-->
                      @If Model IsNot Nothing AndAlso Model.ToMailList IsNot Nothing Then
                        @For i = 0 To Model.ToMailList.Count - 1
                                strKey = String.Format("ToMailList[{0}]", i)
                            @<tr>
                                <td class="colMailDropDown">
                                    @Html.DropDownList(strKey , New SelectList(ViewBag.MakorinList, "USERID", "USERNM", Model.ToMailList(i)), htmlAttributes:=New With {.class = "form-control input-sm"})
                                </td> 
                                <td class="colDeleteBtn">
                                    <input id="btn_mailToDelete" type="button" value="削除" class="btn btn-danger btn-sm" />
                                </td>
                            </tr>
                        Next
                                                                Else
                        @<tr>
                            <td class="colMailDropDown">
                                @Html.DropDownList("ToMailList[0]", New SelectList(ViewBag.MakorinList, "USERID", "USERNM", ""), htmlAttributes:=New With {.class = "form-control input-sm"})
                            </td>
                            <td class="colDeleteBtn">
                                <input id="btn_mailToDelete" type="button" value="削除" class="btn btn-danger btn-sm" />
                            </td>
                        </tr>
                    End If
               <!-- </tbody>-->
             </table>
        </div>
    </div>
    @<div class="row" >
         <div class="col-sm-3">
			
			<div class="table table-bordered customeclass" style ="margin-bottom:0px;background-color: #F0FFF0;padding: 3px;">         
                        
                             @Html.Label("CC :", htmlAttributes:=New With {.class = "control-label" })
                             &nbsp;&nbsp;&nbsp;&nbsp;
                             <input id="btn_mailCCAdd" type="button" value="追加" class="btn btn-success btn-xs" />
                         
                    
            </div>
			
            <table class="table table-bordered customeclass" id="mailCCTable">
				@If Model IsNot Nothing AndAlso Model.CcMailList IsNot Nothing Then
                        @For i = 0 To Model.CcMailList.Count - 1
                                strKey = String.Format("CcMailList[{0}]", i)
                            @<tr>
                                <td class="colMailDropDown">
                                    @Html.DropDownList(strKey , New SelectList(ViewBag.MakorinList, "USERID", "USERNM", Model.CcMailList(i)), htmlAttributes:=New With {.class = "form-control input-sm"})
                                </td> 
                                <td class="colDeleteBtn">
                                    <input id="btn_mailCcDelete" type="button" value="削除" class="btn btn-danger btn-sm" />
                                </td>
                            </tr>
                        Next
                                                                Else
                        @<tr>
                            <td class="colMailDropDown">
                                @Html.DropDownList("CcMailList[0]", New SelectList(ViewBag.MakorinList, "USERID", "USERNM", ""), htmlAttributes:=New With {.class = "form-control input-sm"})
                            </td>
                            <td class="colDeleteBtn">
                                <input id="btn_mailCcDelete" type="button" value="削除" class="btn btn-danger btn-sm" />
                            </td>
                        </tr>
                    End If
             </table>
        </div>
    </div>
    
    @* //ASI[25 Oct 2019] END*@

    @<div class="row" >
        <div class="col-sm-4" style="text-align:right">
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
             @Html.EditorFor(Function(model) model.MAILNOTE, New With {.htmlAttributes = New With {.class = "form-control", .style = "width:500px;max-width:800px;height:150px;"}})
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

<script>

    $(document).ready(function () {

        //送信しない場合、 携帯メール送信要選択不可
        var tblAna = document.getElementById("tblAna");
        var anarows = tblAna.getElementsByTagName("tr");

        for (var i = 0; i < anarows.length - 1; i += 1) {

            var id = "W0050_" + i + "__MAILSOUSIAN";
            var select1 = document.getElementById(id);
            var types = select1.options;
            var mailsousian = types.item(select1.selectedIndex).value;

            if (mailsousian == 'False') {
                var id = "W0050_" + i + "__SENDKEITAI";
                var checkbox = document.getElementById(id);
                checkbox.disabled = true;
                checkbox.checked = false;
            }
        }
    });

    //送信しない場合、 携帯メール送信要選択不可
    function SetSENDKEITAI(rowid) {

        var id = "W0050_" + rowid + "__MAILSOUSIAN";
        var select1 = document.getElementById(id);
        var types = select1.options;
        var mailsousian = types.item(select1.selectedIndex).value;

        var id = "W0050_" + rowid + "__SENDKEITAI";
        var checkbox = document.getElementById(id);

        if (mailsousian == 'False') {
            checkbox.disabled = true;
            checkbox.checked = false;
        }
        else {
            checkbox.disabled = false;
        }
    }


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

    //ASI ASI[25 Oct 2019] START : Write click event to add or remove Email address for To & CC
    $('#btn_mailToAdd').click(function (event) {
        
        var table = document.getElementById("mailToTable");
            var rows = table.getElementsByTagName("tr");
   
            var $table = $('#mailToTable');                  
            var $nrow = $table.find('tr:eq(0)').clone();
            var $ncell = $nrow.find('td:first');

            var $select = $ncell.find('select[name*="ToMailList"]');
            $select.val(0);
            $select.attr("name", "ToMailList[" + rows.length + "]");
            $select.attr("id", "ToMailList_" + rows.length + "__USERID");

            $table.append($nrow);
    });

    $("#mailToTable").on('click', '#btn_mailToDelete', function () {

        var table = document.getElementById("mailToTable");
        var rows = table.getElementsByTagName("tr");
    
        if (rows.length != 1) {
            $(this).closest('tr').remove();

            //行削除後、リストのIndexを振り直す
            for (var i = 0; i < rows.length  ; i += 1) {
                var $ncell = $('#mailToTable tr:eq(' + i + ') td:first');

                var $select = $ncell.find('select[name*="ToMailList"]');
                $select.attr("name", "ToMailList[" + i + "]")
                $select.attr("id", "ToMailList_" + i + "__USERID")
            }
        }

    });

    $('#btn_mailCCAdd').click(function (event) {
        var table = document.getElementById("mailCCTable");
        var rows = table.getElementsByTagName("tr");
   
        var $table = $('#mailCCTable');                  
        var $nrow = $table.find('tr:eq(0)').clone();
        var $ncell = $nrow.find('td:first');

        var $select = $ncell.find('select[name*="CcMailList"]');
        $select.val(0);
        $select.attr("name", "CcMailList[" + rows.length + "]");
        $select.attr("id", "CcMailList_" + rows.length + "__USERID");

        $table.append($nrow);
    });

    $("#mailCCTable").on('click', '#btn_mailCcDelete', function () {
        
        var table = document.getElementById("mailCCTable");
        var rows = table.getElementsByTagName("tr");
    
        if (rows.length != 1) {
            $(this).closest('tr').remove();

            //行削除後、リストのIndexを振り直す
            for (var i = 0; i < rows.length  ; i += 1) {
                var $ncell = $('#mailCCTable tr:eq(' + i + ') td:first');

                var $select = $ncell.find('select[name*="CcMailList"]');
                $select.attr("name", "CcMailList[" + i + "]")
                $select.attr("id", "CcMailList_" + i + "__USERID")
            }
        }

    });
    //ASI [30 July 2019] END
</script>

