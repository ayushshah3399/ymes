﻿@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "業務登録"
    ViewData("MSG") = "True"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
    Dim KarianacatList = DirectCast(ViewBag.KarianacatList, List(Of M0080))
    Dim M0060Kyukde = DirectCast(ViewBag.KyukDe, M0060)
    Dim strKey As String = ""
    Dim strId As String = ""
    Dim strVisibility As String = "hidden"

    Dim M0060KyukHouDe = DirectCast(ViewBag.KyukHouDe, M0060)
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

    .inputBoxAna {
        font-size: 14px;
        width: 120px;
        position: absolute;
    }

    .selectBoxAna {
        font-size: 14px;
        width: 145px;
        height: 33px;
    }
</style>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="row">
        <div class="col-md-1 col-md-push-11" style="padding-top:10px;">
            <ul class="nav nav-pills ">
                <li>
                    @Html.ActionLink("戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
                          .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
                          .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
                          .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
                          .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk"), .OAJKNST = Session("OAJKNST"), .OAJKNED = Session("OAJKNED"), .Biko = Session("Biko")},
                         htmlAttributes:=New With {.id = "btnModoru"})
                </li>
            </ul>
        </div>
        <div class="col-md-11 col-md-pull-1">
            <h3>複写</h3>
        </div>
    </div>

    @<div class="row">

        <div class="col-sm-6">

            <div class="form-horizontal">

                <hr style="margin-top:1px;" />

                <div id="divSummaryError" class="text-danger" style="visibility:hidden">
                </div>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                @Html.Hidden("GYOMNO", 0)
                @Html.Hidden("ACUSERID", ViewBag.ACUSERID)
                @Html.HiddenFor(Function(model) model.YOINUSERID)
                @Html.HiddenFor(Function(model) model.YOINUSERNM)
                @Html.HiddenFor(Function(model) model.YOINIDYES)
                @Html.Hidden("showkoho", ViewBag.ShowKoho)
                @Html.Hidden("ABWEEKSTARTDT", ViewBag.ABWEEKSTARTDT)

                <div class="form-group">
                    <div class="form-inline">
                        @Html.Label("業務期間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            <div class="row">
                                <div class="col-md-9">
                                    @Html.TextBox("GYOMYMD", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                                    ～
                                    @Html.TextBox("GYOMYMDED", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                                </div>
                                <div class="col-md-3">
                                    <label class="checkbox-inline" style="">
                                        @Html.EditorFor(Function(model) model.INDIVIDUAL, New With {.htmlAttributes = New With {.id = "chkIndividual"}}) 個別
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=" col-md-offset-3 col-md-9">
                        @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})
                        @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    <div class="form-inline">
                        @Html.Label("拘束時間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.KSKJKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                            ～
                            @Html.EditorFor(Function(model) model.KSKJKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                        </div>
                    </div>
                    <div class=" col-md-offset-3 col-md-9">
                        @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With {.class = "text-danger"})
                        @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.CATCD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                    <div class="col-md-9">
                        @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                        @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
                    </div>
                </div>

                @*<div class="form-group">
                    <div class="form-inline">
                        @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm", .list = "Bangumi"}})
                            @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
                            @If Bangumilst IsNot Nothing Then
                                @<datalist id="Bangumi">
                                    @For Each item In Bangumilst
                                        @<option>@Html.Encode(item.BANGUMINM) </option>
                                    Next
                                </datalist>
                            End If
                        </div>
                    </div>
                </div>*@

                <div class="form-group ">
                    @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                    <div class="col-md-9 comboField">
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

                <div class="form-group">
                    <div class="form-inline">
                        @Html.LabelFor(Function(model) model.OAJKNST, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.OAJKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                            ～
                            @Html.EditorFor(Function(model) model.OAJKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                        </div>
                        <div class=" col-md-offset-3 col-md-9">
                            @Html.ValidationMessageFor(Function(model) model.OAJKNST, "", New With {.class = "text-danger"})
                            @Html.ValidationMessageFor(Function(model) model.OAJKNED, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                </div>

                @*<div class="form-group">
                    <div class="form-inline">
                        @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control input-sm", .value = ViewData("NAIYO"), .list = "Naiyo"}})
                            @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
                            @If NaiyouList IsNot Nothing Then
                                @<datalist id="Naiyo">
                                    @For Each item In NaiyouList
                                        @<option>@Html.Encode(item.NAIYO) </option>
                                    Next
                                </datalist>
                            End If
                        </div>
                    </div>
                </div>*@

                <div class="form-group ">
                    @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9 comboField">
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
                    @Html.LabelFor(Function(model) model.BASYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9">
                        @Html.EditorFor(Function(model) model.BASYO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                        @Html.ValidationMessageFor(Function(model) model.BASYO, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.BANGUMITANTO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9">
                        @Html.EditorFor(Function(model) model.BANGUMITANTO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                        @Html.ValidationMessageFor(Function(model) model.BANGUMITANTO, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.BANGUMIRENRK, htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9">
                        @Html.EditorFor(Function(model) model.BANGUMIRENRK, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                        @Html.ValidationMessageFor(Function(model) model.BANGUMIRENRK, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.BIKO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9">
                        @Html.EditorFor(Function(model) model.BIKO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                        @Html.ValidationMessageFor(Function(model) model.BIKO, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group" id="divPattern">
                    @Html.LabelFor(Function(model) model.PATTERN, htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9">
                        <div class="checkbox">
                            <label>@Html.EditorFor(Function(model) model.PATTERN, New With {.htmlAttributes = New With {.id = "chkPattern", .onchange = "ShowDaysChkbox(this)"}}) 繰り返し登録</label>
                            @Html.ValidationMessageFor(Function(model) model.PATTERN, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                </div>

                @If Model IsNot Nothing AndAlso Model.PATTERN = True Then
                    strVisibility = "visible"
                End If

                <div class="form-group" id="checkbox" style="visibility:@strVisibility">
                    <div class="col-md-offset-3 col-md-9">
                        <lable class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.MON)月曜
                        </lable>
                        <label class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.TUE)火曜
                        </label>
                        <label class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.WED)水曜
                        </label>
                        <lable class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.TUR)木曜
                        </lable>
                        <label class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.FRI)金曜
                        </label>
                        <label class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.SAT)土曜
                        </label>
                        <label class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.SUN)日曜
                        </label>
                    </div>
                </div>
                <div class="form-group" id="checkboxAB" style="visibility:@strVisibility">
                    <div class="col-md-offset-3 col-md-9">
                        <label class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.WEEKA)A週
                        </label>
                        <label class="checkbox-inline">
                            @Html.EditorFor(Function(model) model.WEEKB)B週
                        </label>

                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        @Html.ValidationMessageFor(Function(model) model.MON, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div id="divHidden" style="visibility:hidden">
                    @Html.HiddenFor(Function(model) model.GYOMYMD)
                    @Html.HiddenFor(Function(model) model.GYOMYMDED)
                    @Html.HiddenFor(Function(model) model.KSKJKNST)
                    @Html.HiddenFor(Function(model) model.KSKJKNED)
                </div>

                <p></p>
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <input type="button" id="btnSearchKoho" value="候補検索" class="btn btn-info btn-sm " />
                        &nbsp | &nbsp
                        <input type="button" id="btnReEdit" value="再編集" class="btn btn-primary btn-sm " />
                        &nbsp | &nbsp
                        <input type="submit" value="登録" id="btnCreate" class="btn btn-default" />
                    </div>
                </div>
            </div>

                @*<p></p>

    <div>

            @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
            .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
            .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
            .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
            .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk")},
                     htmlAttributes:=New With {.id = "btnReturn"})
        </div>*@

        </div>


        <div class="col-sm-2">
            <table class="table table-bordered" id="myTable">
                <tr>
                    <th colspan="2" >
                        アナウンサー
                    </th>
                </tr>

                @If Model IsNot Nothing AndAlso Model.D0020 IsNot Nothing AndAlso Model.D0020.Count > 0 Then
                    @For i = 0 To Model.D0020.Count - 1
                        Dim j = i
                        strKey = String.Format("D0020[{0}].", i)
                        @<tr>
                            <td>

                                @Html.ValidationMessage(strKey & "USERID", New With {.class = "text-danger", .title = "アナウンサーの休暇、または業務情報がが変更されています。候補検索を行い、確認してください。"})

                                @(Model.D0020(i).USERNM)
                                @Html.Hidden(strKey & "USERNM", Model.D0020(i).USERNM)
                                @Html.Hidden(strKey & "USERID", Model.D0020(i).USERID)
                                @Html.Hidden(strKey & "YOINIDYES", Model.D0020(i).YOINIDYES)
                                @*@Html.Hidden(strKey & "YOINID", Model.D0020(i).YOINID)
                                @Html.Hidden(strKey & "DESKMEMO", Model.D0020(i).DESKMEMO)
                                @Html.Hidden(strKey & "MARKKYST", Model.D0020(i).MARKKYST)
                                @Html.Hidden(strKey & "MARKSYTK", Model.D0020(i).MARKSYTK)
                                @Html.Hidden(strKey & "ROWIDX", Model.D0020(i).ROWIDX)*@
                            </td>
                            <td>
                                <a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>
                            </td>
                        </tr>
Next
                End If
            </table>

                <select id="KarianacatList" style="visibility:hidden;">
                    @If KarianacatList IsNot Nothing Then
                        @For Each item In KarianacatList
                            @<option>@item.ANNACATNM</option>
Next
                    End If
                </select>

                <table class="table table-bordered" id="catTable">
                    <tr>
                        <th colspan="2" >
                            仮アナカテゴリー
                        </th>
                    </tr>
                    @If Model IsNot Nothing AndAlso Model.D0021 IsNot Nothing AndAlso Model.D0021.Count > 0 Then
                        @For i = 0 To Model.D0021.Count - 1
                            strKey = String.Format("D0021[{0}].", i)
                            strId = String.Format("D0021_{0}__", i)
                            @<tr>
                                <td>
                                    @Html.TextBox(strKey & "ANNACATNM", Model.D0021(i).ANNACATNM, htmlAttributes:=New With {.class = "form-control input-sm inputBoxAna"})
                                    @Html.Hidden(strKey & "SEQ", Model.D0021(i).SEQ)
                                    <select class="form-control input-sm selectBoxAna" onchange="AnnacatSelect(this, '@(strId & "ANNACATNM")')">
                                        @If KarianacatList IsNot Nothing Then
                                            @For Each item In KarianacatList
                                                @<option>@item.ANNACATNM</option>
Next
                                            End If
                                    </select>

                                </td>
                                <td>
                                    <a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>
                                </td>
                            </tr>
Next
                End If
                </table>

                <table id="tblRefAnalist" class="table table-bordered">
                    <tr>
                        <th colspan="2" style="background-color:#d2f4fa">
                            アナウンサー(呼出済)
                        </th>
                    </tr>
                    @If Model IsNot Nothing AndAlso Model.RefAnalist IsNot Nothing Then
                        @For i = 0 To Model.RefAnalist.Count - 1

                            strKey = String.Format("RefAnalist[{0}]", i)

                            @<tr>
                                <td>
                                    @Model.RefAnalist(i)
                                    @Html.Hidden(strKey, Model.RefAnalist(i))
                                </td>
                            </tr>
Next
                    End If

                </table>

                <table id="tblRefKariAnalist" class="table table-bordered">
                    <tr>
                        <th colspan="2" style="background-color:#d2f4fa">
                            仮アナカテゴリー(呼出済)
                        </th>
                    </tr>
                    @If Model IsNot Nothing AndAlso Model.RefKariAnalist IsNot Nothing Then
                        @For i = 0 To Model.RefKariAnalist.Count - 1

                            strKey = String.Format("RefKariAnalist[{0}]", i)

                            @<tr>
                                <td>
                                    @Model.RefKariAnalist(i)
                                    @Html.Hidden(strKey, Model.RefKariAnalist(i))
                                </td>
                            </tr>
Next
                    End If
                </table>

        </div>


        <div class="col-sm-4" style="padding-left:50px;">
            <h4 style="margin-top:-1px;"><b>候補者一覧</b></h4>
            <font style="background-color:#@M0060Kyukde.BACKCOLOR; color:#@M0060Kyukde.FONTCOLOR; border: 1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>&nbsp;&nbsp;&nbsp;公休出　
            <font style="background-color:#@M0060KyukHouDe.BACKCOLOR; color:#@M0060KyukHouDe.FONTCOLOR; border: 1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>&nbsp;&nbsp;&nbsp;法休出　
            <font style="background-color: yellow;">※</font>&nbsp;&nbsp;&nbsp;OT45h超え
            <div>
                <font style="color: red;">赤</font>&nbsp;&nbsp;&nbsp;OT+休日100h超え&nbsp;/&nbsp;総240h超え&nbsp;/&nbsp;月間休日4日以下
            </div>
            <div id="divKoho">
                @If ViewBag.Showkoho = True Then
                    @Html.Partial("_D0010_AnalistPartial")
End If
            </div>
            <label style="font-size:15px;color:orange;padding-top:10px;visibility:hidden;" id="lblInfo">処理中です。しばらくお待ち下さい。。。</label>
        </div>

    </div>

    @Html.Partial("_MsgDialog")

End Using


     <script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
    var sportShiftFlg;
    var ResetYoinUserAjax_url= '@Url.Action("ResetYoinUserData", "B0020")';
     </script>

<script type="text/javascript" src="~/Scripts/B0020-6.js"></script>

<script>

    //仮アナカテゴリーリストで選択した時
    function AnnacatSelect(select, inputid) {
        var input = document.getElementById(inputid);
        var val = select.value;
        input.value = val;
    }

    //番組リストで選択した時
    $("#selectBoxBangumi").change(function () {
        var val = this.value
        $('#BANGUMINM').val(val)
    });

    //内容リストで選択した時
    $("#selectBoxNaiyo").change(function () {
        var val = this.value
        $('#NAIYO').val(val)
    });

    //$(function () {

    //    $(function () {
    //        $(document).tooltip({
    //            items: ".input-validation-error",
    //            content: function () {
    //                return $(this).attr('data-val-required');
    //            }
    //        });
    //    });


    //});

    //繰り返し登録チェックの時
    function ShowDaysChkbox(checkboxElem) {
        var div = document.getElementById('checkbox');
        var divAB = document.getElementById('checkboxAB');
        if (checkboxElem.checked) {
            div.style.visibility = 'visible';
            divAB.style.visibility = 'visible';
        } else {
            div.style.visibility = 'hidden';
            divAB.style.visibility = 'hidden';
        }
    }

    //$(document).ready(function () {
    //    var div = document.getElementById('checkbox');
    //    if ($('#chkPattern').prop("checked")) {
    //        div.style.visibility = 'visible';
    //    } else {
    //        div.style.visibility = 'hidden';
    //    }
    //})


    $('#btnCreate').on('click', function (e) {

        var result = confirm("業務登録を行います。よろしいですか？")

        if (result == false) {
            return false
        }
    });


</script>

