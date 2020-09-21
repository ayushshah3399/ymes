@ModelType NTV_SHIFT.D0110
@Code
    ViewData("Title") = "デスクメモ"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
    Dim strKey As String = ""
End Code

<style>
    .comboField {
        position: relative;
    }

    .inputBox {
        font-size: 14px;
        width: 200px;
        position: absolute;
    }

    .selectBox {
        font-size: 14px;
        width: 225px;
    }
</style>

<h4>修正</h4>

@Using (Html.BeginForm("Edit", "A0200", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        @*<h4>デスクメモ</h4>*@
         @*<hr style="margin-top:1px;" />*@
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.HiddenFor(Function(model) model.DESKNO)
        @Html.HiddenFor(Function(model) model.INPUTDT)

        <div class="form-group">
            @Html.Label("確認", htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @For Each item In DirectCast(ViewBag.KAKUNINID, List(Of M0100))
                    @<label class="radio-inline">
                        @If item.KAKUNINID = Model.KAKUNINID Then
                            @Html.RadioButtonFor(Function(model) model.KAKUNINID, item.KAKUNINID, htmlAttributes:=New With {.checked = "true"})
                        Else
                            @Html.RadioButtonFor(Function(model) model.KAKUNINID, item.KAKUNINID)

                        End If

                        @item.KAKUNINNM
                    </label>
                Next
                @*ASI[25 July 2019]:If KAKUNINID is 'check' or '未入力' don't show on screen but in hiddedn it and checked accordingly*@
                @*ASI[27 Nov 2019]: index change due to order 未決、注意*@       
                <script>
                    if (@Model.KAKUNINID.toString() == 1 || 4){
                        document.getElementsByName('KAKUNINID')[0].parentElement.style.display = 'none';
                        document.getElementsByName('KAKUNINID')[4].parentElement.style.display = 'none';
                    }
                </script>

                @Html.ValidationMessageFor(Function(model) model.KAKUNINID, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.USERID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownList("USERID", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:130px;"})
                @Html.ValidationMessageFor(Function(model) model.USERID, "", New With {.class = "text-danger"})
            </div>
        </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.CATCD, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-9">
                 @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                 @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
             </div>
         </div>

        @*<div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
            </div>
        </div>*@

         <div class="form-group ">
             @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10 comboField">
                 @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox"}})
                 <select class="form-control input-sm selectBox" id="selectBoxBangumi">
                     @If Bangumilst IsNot Nothing Then
                         @For Each item In Bangumilst
                             @<option>@item.BANGUMINM</option>
                         Next
                    End If
                 </select>
                 @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
             </div>
         </div>

        @*<div class="form-group">
            <div class="form-inline">
                @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-2"})

                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control input-sm", .list = "naiyo"}})
                    <datalist id="naiyo">
                        @For Each naiyo In ViewBag.lstNAIYO
                            @<option>@Html.Encode(naiyo) </option>
                        Next
                    </datalist>
                    @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>*@

         <div class="form-group ">
             @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10 comboField">
                 @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox"}})
                 <select class="form-control input-sm selectBox" id="selectBoxNaiyo">
                     @If NaiyouList IsNot Nothing Then
                         @For Each item In NaiyouList
                             @<option>@item.NAIYO</option>
                         Next
                      End If
                 </select>
                 @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
            </div>
         </div>
         <div class="form-group">
             @Html.LabelFor(Function(model) model.BASYO, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.BASYO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                 @Html.ValidationMessageFor(Function(model) model.BASYO, "", New With {.class = "text-danger"})
             </div>
         </div>
        <div class="form-group form-inline ">
            <label class="col-md-2 control-label">シフト日と拘束時間</label>
            <div class="col-md-10">

                <table id="sdtTable" class="tbllayout">
                    @If Model.D0120.Count > 0 Then

                        @For i = 0 To Model.D0120.Count - 1
                                strKey = String.Format("D0120[{0}].", i)
                            @<tr>
                                <td>
                                    @Html.Hidden(strKey & "EDA", Model.D0120(i).EDA)
                                    @Html.TextBox(strKey & "SHIFTYMDST", Model.D0120(i).SHIFTYMDST, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:120px;"})
                                    ～
                                    @Html.TextBox(strKey & "SHIFTYMDED", Model.D0120(i).SHIFTYMDED, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:120px;"})
                                    &nbsp;&nbsp;
                                    @Html.TextBox(strKey & "KSKJKNST", Model.D0120(i).KSKJKNST, htmlAttributes:=New With {.class = "form-control input-sm time imedisabled", .style = "width:110px;"})
                                    ～
                                    @Html.TextBox(strKey & "KSKJKNED", Model.D0120(i).KSKJKNED, htmlAttributes:=New With {.class = "form-control input-sm time imedisabled", .style = "width:110px;"})
                                    <a href="#" id="del_btn_sdt" class="btn btn-danger btn-sm">削除</a>
                                    <div>@Html.ValidationMessage(strKey & "SHIFTYMDED", New With {.class = "text-danger"})</div>
                                </td>
                                <td style="text-align:right;width:150px;">
                                    @Html.ActionLink("業務登録", "Create", "B0020", New With {.deskno = Model.DESKNO, .eda = Model.D0120(i).EDA},
                                    htmlAttributes:=New With {.class = "btn btn-sm btn-primary btngyomu"})
                                </td>
                            </tr>
                        Next

                    Else
                        @<tr>
                            <td>
                                @Html.Hidden("D0120[0].EDA", "0")
                                @Html.TextBox("D0120[0].SHIFTYMDST", Nothing, htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:120px;"})
                                ～
                                @Html.TextBox("D0120[0].SHIFTYMDED", Nothing, htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:120px;"})
                                &nbsp;&nbsp;
                                @Html.TextBox("D0120[0].KSKJKNST", Nothing, htmlAttributes:=New With {.class = "form-control input-sm time imedisabled", .style = "width:110px;"})
                                ～
                                @Html.TextBox("D0120[0].KSKJKNED", Nothing, htmlAttributes:=New With {.class = "form-control input-sm time imedisabled", .style = "width:110px;"})
                                <a href="#" id="del_btn_sdt" class="btn btn-danger btn-sm">削除</a>
                                <div>@Html.ValidationMessage("D0120[0].SHIFTYMDED", New With {.class = "text-danger"})</div>
                            </td>
                            <td style="text-align:right;width:150px;">
                                    @Html.ActionLink("業務登録", "Create", "B0020", New With {.deskno = Model.DESKNO},
                                    htmlAttributes:=New With {.class = "btn btn-sm btn-primary btngyomu"})
                            </td>
                        </tr>
                    End If
                </table>

            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-1 col-md-1">
                <a href="#" id="add_btn_sdt" class="btn btn-success btn-xs ">追加</a>
            </div>
            <div class="col-md-10">
                <div id="divWarning" class="text-warning" style="visibility:hidden">
                </div>
            </div>
        </div>

        <div class="form-group form-inline">
            <label class="col-md-2 control-label"> 担当アナ</label>
            <div class="col-md-10">
                <table id="anaTable" class="tbllayout">

                    @If Model.D0130.Count > 0 Then
                        @For i = 0 To Model.D0130.Count - 1
                                strKey = String.Format("D0130[{0}].", i)
                            @<tr>
                                <td>
                                    @Html.Hidden(strKey & "EDA", Model.D0130(i).EDA)
                                    @Html.DropDownList(strKey & "USERID", New SelectList(ViewBag.lstM0010, "USERID", "USERNM", Model.D0130(i).USERID), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:130px"})
                                    <a href="#" id="del_btn_ana" class="btn btn-danger btn-sm">削除</a>
                                </td>
                            </tr>
                        Next
                     Else
                        @<tr>
                            <td>
                                @Html.Hidden("D0130[0].EDA", "0")
                                @Html.DropDownList("D0130[0].USERID", New SelectList(ViewBag.lstM0010, "USERID", "USERNM", ""), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:130px"})
                                <a href="#" id="del_btn_ana" class="btn btn-danger btn-sm">削除</a>
                            </td>
                        </tr>
                    End If

                </table>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-1 col-md-11">
                <a href="#" id="add_btn_ana" class="btn btn-success btn-xs ">追加</a>
            </div>
        </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.BANGUMITANTO, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.BANGUMITANTO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                 @Html.ValidationMessageFor(Function(model) model.BANGUMITANTO, "", New With {.class = "text-danger"})
             </div>
         </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.BANGUMIRENRK, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.BANGUMIRENRK, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                 @Html.ValidationMessageFor(Function(model) model.BANGUMIRENRK, "", New With {.class = "text-danger"})
             </div>
         </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.DESKMEMO, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.DESKMEMO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.DESKMEMO, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input id="btnMasterUpd" type="submit" value="更新" class="btn btn-default" />
            </div>
        </div>
    </div>

End Using

<div>
    @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.CondDeskno = Session("CondDeskno"), .CondKakunin1 = Session("CondKakunin1"), .CondKakunin2 = Session("CondKakunin2"),
                        .CondInstst = Session("CondInstst"), .CondInsted = Session("CondInsted"), .CondDeskid = Session("CondDeskid"), .CondCatcd = Session("CondCatcd"),
                        .CondBanguminm = Session("CondBanguminm"), .CondNaiyo = Session("CondNaiyo"), .CondShiftst = Session("CondShiftst"), .CondShifted = Session("CondShifted"),
                        .CondAnaid = Session("CondAnaid"), .CondBangumitanto = Session("CondBangumitanto"), .CondBangumirenrk = Session("CondBangumirenrk"), .CondBasyo = Session("CondBasyo")},
                        htmlAttributes:=New With {.id = "btnEditModoru"})
</div>

<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>

<script type="text/javascript" src="~/Scripts/A0200.js"></script>


<script>

    //業務登録ボタンを押した時
    $(document).on('click', '.btngyomu', (function () {
        var urlgyom = this.href;

        var deskno = $("#DESKNO").val();

        var $nrow = $(this).parent().parent();

        var $inputeda = $nrow.find('td:first').find('input[name*=EDA]');
        var eda = $inputeda.val();

        var $input1 = $nrow.find('td:first').find('input[name*=SHIFTYMDST]');
        var shiftymdst = $input1.val();

        var $input2 = $nrow.find('td:first').find('input[name*=SHIFTYMDED]');
        var shiftymded = $input2.val();

        var $input3 = $nrow.find('td:first').find('input[name*=KSKJKNST]');
        var kskjknst = $input3.val();

        var $input4 = $nrow.find('td:first').find('input[name*=KSKJKNED]');
        var kskjkned = $input4.val();

        if (shiftymdst.length > 0 && shiftymded.length > 0 && kskjknst.length > 0 && kskjkned.length > 0) {
            var url = myApp.Urls.baseUrl + 'A0200/CheckWBooking/?deskno=' + deskno + '&eda=' + eda;
            var bolRtn = true;

            $("#divWarning").load(url, function (response, status, xhr) {

                if (status == "error") {
                    var msg = "読み込みエラーが発生しました: ";
                    $("#divWarning").html(url + '<br />' + msg + xhr.status + " " + xhr.statusText);
                    return false;
                }
                else if (status == "success") {

                    var bolOk = true;
                    var msg = $("#divWarning").text();

                    if (msg.length > 0) {
                        if (confirm(msg) == false) {
                            bolOk = false;
                        }
                    }
                    if (bolOk == true) {
                        window.location.href = urlgyom;
                    }

                }
            });

            return false;
        }


    }));


    //修正モードで画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
    $(document).ready(function () {

        $("#myForm :input").change(function () {

            $("#myForm").data("MSG", true);

        });
    });
</script>

