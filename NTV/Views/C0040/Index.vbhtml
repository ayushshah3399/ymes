@ModelType IEnumerable(Of NTV_SHIFT.C0040)
@Code
    ViewData("Title") = ViewData("name") & "さん_" & "個人シフト"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim i = 0
    Dim Kyukas = DirectCast(ViewBag.ColorList, List(Of M0060))

    Dim memoExistDaysErr = ViewData("memoExistDaysErr")

End Code
<style>
    #D0010_GYOMYMD, #D0010_GYOMYMDED, #D0060_KKNST, #D0060_KKNED {
        width: 100px;
    }

    #D0010_KSKJKNST, #D0010_KSKJKNED, #D0060_JKNST, #D0060_JKNED {
        width: 70px;
    }


    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }


    table.table-scroll tbody {
        height: 460px;
        width: 1320px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .table > thead > tr > th, .table > tbody > tr > th, .table > thead > tr > td, .table > tbody > tr > td {
        padding: 3px;
        vertical-align: middle;
    }

    .colkakunin {
        width: 60px;
        max-width: 60px;
    }

    .colsttime {
        width: 60px;
        max-width: 60px;
    }

    .edtime {
        width: 60px;
        max-width: 60px;
    }

    .showsttime {
        width: 60px;
        max-width: 60px;
    }

    .showedtime {
        width: 60px;
        max-width: 60px;
    }

    .catcd {
        width: 100px;
        max-width: 100px;
        word-wrap: break-word;
    }


    .banguminm {
        width: 200px;
        max-width: 200px;
        word-wrap: break-word;
    }

    .naiyo {
        width: 100px;
        max-width: 100px;
        word-wrap: break-word;
    }

    .basho {
        width: 150px;
        max-width: 150px;
        word-wrap: break-word;
    }

    .memo {
        width: 200px;
        max-width: 200px;
        word-wrap: break-word;
    }

    .mymemo {
        width: 200px;
        max-width: 200px;
    }

    .deskmemo_and_editlink {
        width: 80px;
        max-width: 80px;
    }

    .colHI {
        width: 50px;
        max-width: 50px;
    }

    .colYOBI {
        width: 40px;
        max-width: 40px;
    }

    .timeEdit {
        width: 50px;
        max-width: 50px;
    }


    .CellComment {
        display: none;
        position: relative;
        z-index: 100;
        border: 1px;
        background-color: white;
        border-style: solid;
        border-width: 1px;
        border-color: black;
        padding: 3px;
        color: black;
        /*top: 50px;*/
        /*left: 20px;*/
        width: 210px;
        max-width: 210px;
        height: auto;
        word-wrap: break-word;
        text-align: left;
    }

    .mymemo:hover span.CellComment {
        display: block;
    }

    body {
        font-size: 12px;
    }

    input {
        line-height: normal
    }
</style>


@Using Html.BeginForm("Index", "C0040", routeValues:=Nothing, method:=FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})

    @<div class="row">

        @If ViewData("Kanri") <> "1" Then
            @<div Class="col-md-8" style="padding-top:10px;margin-top:5px;">
                <ul class="nav nav-pills">
                    <li>@Html.ActionLink("担当表", "Index", "C0030")</li>
                    <li>@Html.ActionLink("全体スポーツシフト表", "Index", "A0230")</li>
                    <li>@Html.ActionLink("種目別スポーツシフト表", "Index", "A0240", New With {.searchdt = ViewData("searchdt").ToString.Substring(0, 7)}, htmlAttributes:=Nothing)</li>
                </ul>
            </div>
            @<div Class="col-md-3" style="padding-top:10px;margin-top:5px;">
                <ul Class="nav nav-pills navbar-right">
                    <li> <a href="javascript:PrintDiv();"> 印刷</a></li>
                    <li> <a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>
                    @if Session("LoginUserKanri") = "False" AndAlso Session("LoginUserSystem") = "False" AndAlso Session("LoginUserAna") = "True" Then
                        @<li><a href="#" id="EnDisColMsgBox1">伝言板表示/非表示</a></li>
                    End If
                    @If Session("UrlReferrer") IsNot Nothing Then
                        @<li><a href="@Session("UrlReferrer")" id="btnEditModoru">戻る</a></li>
                    End If
                </ul>
            </div>
        End If
    </div>
    @<div Class="row">

        @If ViewData("Kanri") = "1" Then
            @<div Class="col-md-12" style="padding-top:5px;height:25px;"></div>
        End If
        <div Class="col-md-3" style="margin-top:-20px;">
            <h3>
                @*個人シフト表*@
                @If ViewData.Item("name") IsNot Nothing Then
                    @<label>(</label>@ViewData("name")@<label>さん)</label>
                End If
            </h3>
        </div>
        <div class="col-md-3">
            <label>開始日付 &nbsp;</label>
            <input class="date imedisabled" id="stdt" name="stdt" value=@Html.Encode(ViewData("searchdt")) type="text" onchange="KeyUpFunction()" style="width:120px;height:30px;font-size:small;margin-top:0px;">
            <button type="submit" name="btnSearch" id="btnSearch" class="btn btn-default btn-xs">検索</button>
            @*@If ViewData("Kanri") = "0" Then
                    @<label> &nbsp; から31日間</label>
                End If*@
            @Html.Hidden("accesslavel", ViewData("Kanri"))
        </div>
        <div class="col-md-2" style="margin-top:0px;">
            @If ViewData("sportdata") = "0" Then
                @<button type="submit" name="btnSportShow" id="btnSportShow" class="btn btn-info btn-xs">スポーツ予定表示</button>
            Else
                @<button type="submit" name="btnSportHide" id="btnSportHide" class="btn btn-info btn-xs">スポーツ予定非表示</button>
            End If
        </div>

        @If ViewData("Kanri") <> "0" Then
            @<div class="col-md-3" style="margin-top:-5px;">
                <ul class="nav nav-pills navbar-right">
                    <li><a href="javascript:PrintDiv();">印刷</a></li>
                    <li><a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>
                    @if Session("LoginUserKanri") = "False" AndAlso Session("LoginUserSystem") = "False" AndAlso Session("LoginUserAna") = "True" Then
                        @<li><a href="#" id="EnDisColMsgBox1">伝言板表示/非表示</a></li>
                    End If
                    @If Session("UrlReferrer") IsNot Nothing Then
                        @<li><a href="@Session("UrlReferrer")" id="btnEditModoru">戻る</a></li>
                    End If
                </ul>
            </div>
        End If
    </div>
    @Html.Hidden("viewdatadate", ViewData("searchdt"))
    @Html.Hidden("name", ViewData("name"))
    @Html.Hidden("id", ViewData("id"))
    @Html.Hidden("sportdata", ViewData("sportdata"))
    @Html.Hidden("msgShow", Session("msgShow"))


    @If (ViewData("status") = "1") Then

        @<script language="javascript">
             alert("公休展開されていません。");
        </script>

    End If

    @*<div class="row">

            @Html.Hidden("name", ViewData("name"))
            @Html.Hidden("id", ViewData("id"))

           @If ViewData("Self") = "1" Then
                    @<div class="col-md-5 col-md-push-7">
                        <ul class="nav nav-pills navbar-right">
                            <li><a href="#" id="alEnDisGyomu">業務申請表示/非表示</a></li>
                            <li><a href="#" id="alEnDisKyuka">休暇申請表示/非表示</a></li>
                        </ul>
                    </div>
                Else
                    @<div class="col-md-5 col-md-push-7">
                    </div>
                End If

           <div class="col-md-5  col-md-pull-4">
                    <label>開始日付 &nbsp;</label>
                    <input class="date imedisabled" id="stdt" name="stdt" value=@Html.Encode(ViewData("searchdt")) type="text" style="width:120px;height:35px;font-size:small;">
                    <button type="submit" name="btnSearch" id="btnSearch" class="btn btn-default btn-sm">検索</button>
                    @If ViewData("Kanri") = "0" Then
                        @<label> &nbsp; から31日間</label>
                    End If
                    @Html.Hidden("accesslavel", ViewData("Kanri"))
                </div>

        </div>*@
End Using

         <div class="row">

             @Using (Html.BeginForm("Index", "C0040", Nothing, FormMethod.Post))
                 @Html.AntiForgeryToken()

                 @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                 @<div class="row" style="padding-bottom:2px;">

                     <div class="col-md-1" style="padding-left:30px">
                         @If ViewData("Self") = "1" Then
                             @<input type="submit" name="btnUpdate" id="btnUpdate" value="更新" class="btn btn-default btn-xs" />
                         End If
                     </div>

                     <div Class="col-md-7" style="padding-left:30px;padding-top:5px;font-size:small;">
                         <label class="control-label">（@ViewData("month")月）</label>

                         <label class="control-label" style="margin-right:50px;color:tomato">
                             時間外シフト時間&nbsp;(h)：@ViewData("othour")
                         </label>

                         <Label Class="control-label" style="margin-right:50px;color:mediumvioletred">
                             時間外+法休シフト時間&nbsp;(h)： @ViewData("othour_all")
                         </Label>

                         <Label Class="control-label" style="margin-right:50px;color:blue">
                             総シフト時間&nbsp;(h)： @ViewData("wkhour")
                         </Label>

                         <Label Class="control-label" style="margin-right:50px;color:forestgreen">
                             休日数&nbsp;： @ViewData("holiday_cnt")
                         </Label>
                     </div>

                     @If ViewData("Self") = "1" Then
                         @<div class="col-md-4 pull-right">
                             <ul class="nav nav-pills navbar-right" style="padding-right:30px;">
                                 <!--Havan[26 Dev 2019]: added new link 時間休登録 on screen-->
                                 <li><a href="#" id="alEnDisKyuToroku">時間休登録</a></li>
                                 <li><a href="#" id="alEnDisGyomu">業務申請表示/非表示</a></li>
                                 <li><a href="#" id="alEnDisKyuka">休暇申請表示/非表示</a></li>
                             </ul>
                         </div>
                     Else
                         @<div class="col-md-4">
                         </div>
                     End If
                 </div>

                 @<div id="idErrorDiv" class="row" style="padding-bottom:5px;" hidden>
                     <div class="col-md-9" style="padding-left:30px; color:red; font-size:15px; font-weight: bold;">
                         <label id="Error" style="color:red; font-size:15px"></label>
                     </div>
                 </div>

                 @Html.Hidden("name", ViewData("name"))
                 @Html.Hidden("id", ViewData("id"))
                 @Html.Hidden("searchdt", ViewData("searchdt"))
                 @Html.Hidden("sportdata", ViewData("sportdata"))


                 @<div class="col-sm-6" id="mycontent">

                     <table class="table table-hover  table-bordered table-scroll" id="mytable" style="border-collapse:separate">

                         <thead>
                             <tr>
                                 <th class="colHI">
                                     @Html.DisplayNameFor(Function(model) model.HI)
                                 </th>
                                 <th class="colYOBI">
                                     @Html.DisplayNameFor(Function(model) model.YOBI)
                                 </th>

                                 <th class="colkakunin">
                                     @Html.DisplayNameFor(Function(model) model.KAKUNIN)
                                 </th>
                                 <th class="colsttime">
                                     @Html.DisplayNameFor(Function(model) model.STTIME)
                                 </th>
                                 <th class="showsttime" hidden="hidden">
                                     @Html.DisplayNameFor(Function(model) model.STTIME)
                                 </th>
                                 <th class="edtime">
                                     @Html.DisplayNameFor(Function(model) model.EDTIME)
                                 </th>
                                 <th class="showedtime" hidden="hidden">
                                     @Html.DisplayNameFor(Function(model) model.EDTIME)
                                 </th>
                                 <th class="banguminm">
                                     @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                                 </th>
                                 <th class="naiyo">
                                     @Html.DisplayNameFor(Function(model) model.NAIYO)
                                 </th>
                                 <th class="basho">
                                     @Html.DisplayNameFor(Function(model) model.BASHO)
                                 </th>
                                 <th class="memo">
                                     @Html.DisplayNameFor(Function(model) model.MEMO)
                                 </th>
                                 <th class="catcd">
                                     @Html.DisplayNameFor(Function(model) model.CATCD)
                                 </th>

                                 @If ViewData("Self") = "1" Then
                                     @<th class="mymemo">
                                         @Html.DisplayNameFor(Function(model) model.MYMEMO)
                                     </th>
                                     @<th class="showmemo" hidden="hidden">
                                         @Html.DisplayNameFor(Function(model) model.MYMEMO)
                                     </th>
                                 End If
                                 @If Session("LoginUserACCESSLVLCD") <> "4" OrElse ViewData("Self") = "1" Then
                                     @<th class="deskmemo_and_editlink"></th>
                                 End If

                             </tr>
                         </thead>

                         <tbody>
                             @For intIndex As Integer = 0 To Model.Count - 1
                                 Dim item = Model(intIndex)
                                 Dim isTimeEditable = "0"
                                 'Havan[09 Jan 2020] 開始 time editable before current date validation check
                                 If Convert.ToDateTime(Date.Today.ToString("yyyy/MM/dd")) > Convert.ToDateTime(item.GYOMDT) Then
                                     isTimeEditable = "1"
                                 End If

                                 Dim topwaku As String = ""
                                 Dim bottomwaku As String = ""
                                 Dim bottomblackwaku As String = ""
                                 If intIndex > 0 Then
                                     Dim itemBefore = Model(intIndex - 1)
                                     Dim itemAfter = Model(intIndex + 1)

                                     If itemBefore.KYUSHUTSU <> "" AndAlso itemBefore.KYUSHUTSU = item.KYUSHUTSU Then
                                         topwaku = Nothing
                                     Else
                                         topwaku = item.ROWWAKUCOLOR
                                     End If

                                     If itemAfter IsNot Nothing Then
                                         If itemAfter.KYUSHUTSU <> "" AndAlso itemAfter.KYUSHUTSU = item.KYUSHUTSU Then
                                             If itemAfter.HI <> "" Then
                                                 bottomwaku = "808080"
                                             Else
                                                 bottomwaku = Nothing
                                             End If

                                         Else
                                             bottomwaku = item.ROWWAKUCOLOR
                                         End If
                                     Else
                                         bottomwaku = item.ROWWAKUCOLOR
                                     End If
                                     If itemAfter IsNot Nothing Then
                                         If itemAfter.HI <> "" Then
                                             bottomblackwaku = "808080"
                                         Else
                                             bottomblackwaku = Nothing
                                         End If
                                     End If
                                 Else
                                     Dim itemAfter = Model(intIndex + 1)
                                     If item.KYUSHUTSU <> "" Then
                                         topwaku = item.ROWWAKUCOLOR
                                     End If
                                     If itemAfter IsNot Nothing Then
                                         If itemAfter.KYUSHUTSU <> "" AndAlso itemAfter.KYUSHUTSU = item.KYUSHUTSU Then
                                             If itemAfter.HI <> "" Then
                                                 bottomwaku = "808080"
                                             Else
                                                 bottomwaku = Nothing
                                             End If
                                         Else
                                             bottomwaku = item.ROWWAKUCOLOR
                                         End If
                                     End If
                                     If itemAfter IsNot Nothing Then
                                         If itemAfter.HI <> "" Then
                                             bottomblackwaku = "808080"
                                         Else
                                             bottomblackwaku = Nothing
                                         End If
                                     End If


                                 End If

                                 Dim timePickerCSSCls As String = ""
                                 If item.DATAKBN = "3" And (item.GYOMNO = "7") Then
                                     timePickerCSSCls = "timeEdit timecustom"
                                 Else
                                     timePickerCSSCls = "timeEdit time"
                                 End If

                                 @If item.KYUSHUTSU <> "" Then
                                     @<tr class="kyushutsu">

                                         <td class="colHI" style="border-left:2px solid #@item.ROWWAKUCOLOR; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                             @Html.DisplayFor(Function(modelItem) item.HI)
                                         </td>

                                         <td class="colYOBI" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                             @Html.DisplayFor(Function(modelItem) item.YOBI)
                                         </td>

                                         <td class="colkakunin" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                             @If item.KAKUNIN = "0" OrElse item.KAKUNIN = "1" Then

                                                 If item.KAKUNIN = "0" Then
                                                     @If ViewData.Item("Self") = "1" Then
                                                         Dim strName As String = "C0050[" & i & "].KAKUNIN"
                                                         Dim strID As String = "C0050_" & i & "__KAKUNIN"
                                                         @<input type="checkbox" name=@strName value="0" id=@strID onclick='handleClick(this)' ;>

                                                     End If

                                                 End If
                                             Else
                                                 @Html.DisplayFor(Function(modelItem) item.KAKUNIN)
                                             End If
                                         </td>


                                         @If item.BACKCOLOR IsNot Nothing Then
                                             @<td class="colsttime" style="background-color:#@item.BACKCOLOR ;  border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR;">
                                                 @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.PGYOMNO = 0 OrElse (item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = True))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                     @Html.TextBox("C0050[" & i & "].STTIME", item.STTIME, New With {.class = timePickerCSSCls})
                                                     @Html.ValidationMessage("C0050[" & i & "].STTIME", "", New With {.class = "text-danger"})
                                                 Else
                                                     @Html.DisplayFor(Function(modelItem) item.STTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                 End If
                                             </td>
                                             @<td class="showsttime" hidden="hidden" style="background-color:#@item.BACKCOLOR ;  border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR;">
                                                 <div>@item.STTIME</div>
                                             </td>
                                         Else

                                             @<td class="colsttime" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                 @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.PGYOMNO = 0 OrElse (item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = True))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                     @Html.TextBox("C0050[" & i & "].STTIME", item.STTIME, New With {.class = timePickerCSSCls})
                                                     @Html.ValidationMessage("C0050[" & i & "].STTIME", "", New With {.class = "text-danger"})
                                                 Else
                                                     @Html.DisplayFor(Function(modelItem) item.STTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                 End If
                                             </td>
                                             @<td class="showsttime" hidden="hidden" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                 <div>@item.STTIME</div>
                                             </td>End If


                                         @If item.TITLEKBN = "1" Then

                                             @If item.BACKCOLOR IsNot Nothing Then
                                                 @<td class="edtime" colspan="6" style="background-color:#@item.BACKCOLOR ;  border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; ">
                                                     @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.RNZK = "0" OrElse (item.RNZK = "1" AndAlso item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False AndAlso item.JTJKNED.ToString("HH:mm") <> "00:00"))) Or (item.DATAKBN = "3" And item.GYOMNO = "7")) Then
                                                         @Html.TextBox("C0050[" & i & "].EDTIME", item.EDTIME, New With {.class = timePickerCSSCls})
                                                         @Html.ValidationMessage("C0050[" & i & "].EDTIME", "", New With {.class = "text-danger"})
                                                     Else
                                                         @Html.DisplayFor(Function(modelItem) item.EDTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                     End If
                                                 </td>
                                                 @<td class="showedtime" hidden="hidden" colspan="6" style="background-color:#@item.BACKCOLOR ;  border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; ">
                                                     <div>@item.EDTIME</div>
                                                 </td>
                                             Else
                                                 @<td class="edtime" colspan="6" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                     @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.RNZK = "0" OrElse (item.RNZK = "1" AndAlso item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False AndAlso item.JTJKNED.ToString("HH:mm") <> "00:00"))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                         @Html.TextBox("C0050[" & i & "].EDTIME", item.EDTIME, New With {.class = timePickerCSSCls})
                                                         @Html.ValidationMessage("C0050[" & i & "].EDTIME", "", New With {.class = "text-danger"})
                                                     Else
                                                         @Html.DisplayFor(Function(modelItem) item.EDTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                     End If

                                                 </td>

                                                 @<td class="showedtime" hidden="hidden" colspan="6" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                     <div>@item.EDTIME</div>
                                                 </td>
                                             End If
                                         Else

                                             @If item.BACKCOLOR IsNot Nothing Then
                                                 @<td class="edtime" style="background-color:#@item.BACKCOLOR ;  border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR;">
                                                     @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.RNZK = "0" OrElse (item.RNZK = "1" AndAlso item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False AndAlso item.JTJKNED.ToString("HH:mm") <> "00:00"))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                         @Html.TextBox("C0050[" & i & "].EDTIME", item.EDTIME, New With {.class = timePickerCSSCls})
                                                         @Html.ValidationMessage("C0050[" & i & "].EDTIME", "", New With {.class = "text-danger"})
                                                     Else
                                                         @Html.DisplayFor(Function(modelItem) item.EDTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                     End If
                                                 </td>
                                                 @<td class="showedtime" hidden="hidden" style="background-color:#@item.BACKCOLOR ;  border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR;">
                                                     <div>@item.EDTIME</div>
                                                 </td> Else

                                                 @<td class="edtime" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                     @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.RNZK = "0" OrElse (item.RNZK = "1" AndAlso item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False AndAlso item.JTJKNED.ToString("HH:mm") <> "00:00"))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                         @Html.TextBox("C0050[" & i & "].EDTIME", item.EDTIME, New With {.class = timePickerCSSCls})
                                                         @Html.ValidationMessage("C0050[" & i & "].EDTIME", "", New With {.class = "text-danger"})
                                                     Else
                                                         @Html.DisplayFor(Function(modelItem) item.EDTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                     End If
                                                 </td>
                                                 @<td class="showedtime" hidden="hidden" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                     <div>@item.EDTIME</div>
                                                 </td>End If


                                             @If item.KAKUNIN <> "申請中" AndAlso (Html.Encode(item.BANGUMINM) = "時間休" OrElse Html.Encode(item.BANGUMINM) = "時間強休") Then


                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="banguminm" colspan="3" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku ;  border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; ">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>
                                                     @<td class="memo" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku ; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; ">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                     @<td class="catcd" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku ;  border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; border-right:2px solid #@item.ROWWAKUCOLOR;"></td>
                                                 Else
                                                     @<td class="banguminm" colspan="3" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;  ">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>
                                                     @<td class="memo" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;  ">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                     @<td class="catcd" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;  border-right:2px solid #@item.ROWWAKUCOLOR;"></td>
                                                 End If



                                             Else



                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="banguminm" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;; border-width: 2px; color:#@item.FONTCOLOR;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>
                                                 Else

                                                     @<td class="banguminm" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>
                                                 End If

                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="naiyo" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR;">
                                                         @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                                     </td>
                                                 Else

                                                     @<td class="naiyo" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                                     </td>
                                                 End If

                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="basho" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku; border-width: 2px; color:#@item.FONTCOLOR;">
                                                         @Html.DisplayFor(Function(modelItem) item.BASHO)
                                                     </td>
                                                 Else

                                                     @<td class="basho" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BASHO)
                                                     </td>
                                                 End If



                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="memo" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;   border-width: 2px; color:#@item.FONTCOLOR;">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                 Else

                                                     @<td class="memo" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;  ">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                 End If



                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="catcd" style="background-color:#@item.BACKCOLOR ; border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku ; border-right:2px solid #@item.ROWWAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR;">
                                                         @Html.DisplayFor(Function(modelItem) item.CATNM)
                                                     </td>
                                                 Else

                                                     @<td class="catcd" style="border-top:2px solid #@topwaku;  border-bottom:3px solid #@bottomwaku;border-right:2px solid #@item.ROWWAKUCOLOR;">
                                                         @Html.DisplayFor(Function(modelItem) item.CATNM)
                                                     </td>
                                                 End If


                                             End If
                                         End If

                                         @Html.Hidden("C0050[" & i & "].GYOMNO", item.GYOMNO)
                                         @Html.Hidden("C0050[" & i & "].PGYOMNO", item.PGYOMNO)
                                         @Html.Hidden("C0050[" & i & "].KAKUNIN", item.KAKUNIN)
                                         @Html.Hidden("C0050[" & i & "].DATAKBN", item.DATAKBN)
                                         @Html.Hidden("C0050[" & i & "].GYOMDT", item.GYOMDT)
                                         @Html.Hidden("C0050[" & i & "].USERID", item.USERID)
                                         @Html.Hidden("C0050[" & i & "].STTIMEupdt", item.STTIMEupdt)
                                         @Html.Hidden("C0050[" & i & "].EDTIMEupdt", item.EDTIMEupdt)
                                         @Html.Hidden("C0050[" & i & "].KYUKCD", item.KYUKCD)
                                         @Html.Hidden("C0050[" & i & "].KKNST", item.KKNST)
                                         @Html.Hidden("C0050[" & i & "].KKNED", item.KKNED)

                                         @*ASI[02 Jan 2020]: to update STTIME EDTIME in D0010*@
                                         @Html.Hidden("C0050[" & i & "].SPORT_OYAFLG", item.SPORT_OYAFLG)
                                         @Html.Hidden("C0050[" & i & "].RNZK", item.RNZK)
                                         @Html.Hidden("C0050[" & i & "].SPORTFLG", item.SPORTFLG)


                                         @If ViewData("Self") = "1" Then
                                             @<td class="mymemo" style="border-bottom:3px solid #@bottomblackwaku;">

                                                 @If item.MYMEMOFLG = "1" Then
                                                     @Html.TextBox("C0050[" & i & "].MYMEMO", item.MYMEMO, htmlAttributes:=New With {.style = "width : 190px;", .id = "C0050_" & i & "__MYMEMO"})
                                                     @If item.MYMEMO IsNot Nothing Then
                                                         @<span class="CellComment" id="spanClass">@item.MYMEMO</span>
                                                     End If
                                                     Dim strKey As String = String.Format("C0050[{0}].", i)
                                                     @Html.ValidationMessage(strKey & "MYMEMO", "", New With {.class = "text-danger"})
                                                 End If



                                             </td>


                                             @*@<td class="showmemo" >
                                            @item.MYMEMO
                                        </td>*@
                                             @<td class="showmemo" hidden="hidden">
                                                 <div class="showmemolabel" style="max-width: 150px; word-wrap: break-word">@item.MYMEMO</div>
                                             </td>
                                         End If
                                         @If Session("LoginUserACCESSLVLCD") <> "4" OrElse ViewData("Self") = "1" Then
                                             @<td class="deskmemo_and_editlink" style="border-bottom:3px solid #@bottomblackwaku;">
                                                 @If item.DATAKBN = "1" AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
                                                     Dim AV_GYOMNO = item.GYOMNO
                                                     @if item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False Then
                                                         AV_GYOMNO = item.PGYOMNO
                                                     End If
                                                     @Html.ActionLink("修正", "Edit", "B0020", routeValues:=New With {.id = AV_GYOMNO, .Form_name = "C0040"}, htmlAttributes:=New With {.class = ""})
                                                 End If
                                                 @If item.DATAKBN = "3" AndAlso ViewData("Self") = "1" AndAlso (item.GYOMNO = "7") Then

                                                     Dim AV_USERID = item.USERID
                                                     Dim AV_GYOMDT = item.GYOMDT
                                                     Dim AV_JKNST = item.STTIMEupdt
                                                     @Html.ActionLink("削除", "Delete", "C0040", routeValues:=New With {.id = AV_USERID, .gyomdt = AV_GYOMDT, .jknst = AV_JKNST}, htmlAttributes:=New With {.class = ""})
                                                 End If
                                                 @If item.DESKMEMOEXISTFLG = True AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
                                                     @If item.DATAKBN = "1" OrElse (item.DATAKBN = "3" AndAlso ViewData("Self") = "1" AndAlso (item.GYOMNO = "7")) Then
                                                         @<span> | </span>
                                                     End If
                                                     @*ASI[02 Aug 2019]:Based on value of that flag display link of DeskMemo in _個人シフト screen*@
                                                     @*@Html.ActionLink("デスク", "Index", "A0200", routeValues:=New With {.CondShiftst = item.GYOMDT, .CondAnaid = ViewData("id")}, htmlAttributes:=New With {.class = "", .target = "_blank", .color = "red"})*@
                                                     @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = item.GYOMDT, .CondAnaid = ViewData("id"), .CondKakunin1 = "false", .CondKakunin2 = "true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:-2px;"})
                                                 End If
                                             </td>
                                         End If
                                     </tr>
                                 Else
                                     @<tr>

                                         <td class="colHI" style="border-bottom:3px solid #@bottomblackwaku;">
                                             @Html.DisplayFor(Function(modelItem) item.HI)
                                         </td>

                                         <td class="colYOBI" style="border-bottom:3px solid #@bottomblackwaku;">
                                             @Html.DisplayFor(Function(modelItem) item.YOBI)
                                         </td>

                                         <td class="colkakunin" style="border-bottom:3px solid #@bottomblackwaku;">
                                             @If item.KAKUNIN = "0" OrElse item.KAKUNIN = "1" Then

                                                 If item.KAKUNIN = "0" Then
                                                     @If ViewData.Item("Self") = "1" Then
                                                         Dim strName As String = "C0050[" & i & "].KAKUNIN"
                                                         Dim strID As String = "C0050_" & i & "__KAKUNIN"
                                                         @<input type="checkbox" name=@strName value="0" id=@strID onclick='handleClick(this)' ;>
                                                     End If

                                                 End If
                                             Else
                                                 @Html.DisplayFor(Function(modelItem) item.KAKUNIN)
                                             End If
                                         </td>


                                         @If item.BACKCOLOR IsNot Nothing Then
                                             @<td class="colsttime" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-bottom:3px solid #@bottomblackwaku; ">
                                                 @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.PGYOMNO = 0 OrElse (item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = True))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                     @Html.TextBox("C0050[" & i & "].STTIME", item.STTIME, New With {.class = timePickerCSSCls})
                                                     @Html.ValidationMessage("C0050[" & i & "].STTIME", "", New With {.class = "text-danger"})
                                                 Else
                                                     @Html.DisplayFor(Function(modelItem) item.STTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                 End If
                                             </td>
                                             @<td class="showsttime" hidden="hidden" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-bottom:3px solid #@bottomblackwaku; ">
                                                 <div>@item.STTIME</div>
                                             </td>Else

                                             @<td class="colsttime" style="border-bottom:3px solid #@bottomblackwaku;">
                                                 @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.PGYOMNO = 0 OrElse (item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = True))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                     @Html.TextBox("C0050[" & i & "].STTIME", item.STTIME, New With {.class = timePickerCSSCls})
                                                     @Html.ValidationMessage("C0050[" & i & "].STTIME", "", New With {.class = "text-danger"})
                                                 Else
                                                     @Html.DisplayFor(Function(modelItem) item.STTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                 End If
                                             </td>
                                             @<td class="showsttime" hidden="hidden" style="border-bottom:3px solid #@bottomblackwaku;">
                                                 <div>@item.STTIME</div>
                                             </td>
                                         End If

                                         @If item.BACKCOLOR IsNot Nothing Then
                                             @<td class="edtime" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-bottom:3px solid #@bottomblackwaku;">
                                                 @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.RNZK = "0" OrElse (item.RNZK = "1" AndAlso item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False AndAlso item.JTJKNED.ToString("HH:mm") <> "00:00"))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                     @Html.TextBox("C0050[" & i & "].EDTIME", item.EDTIME, New With {.class = timePickerCSSCls})
                                                     @Html.ValidationMessage("C0050[" & i & "].EDTIME", "", New With {.class = "text-danger"})
                                                 Else
                                                     @Html.DisplayFor(Function(modelItem) item.EDTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                 End If
                                             </td>
                                             @<td class="showedtime" hidden="hidden" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-bottom:3px solid #@bottomblackwaku;">
                                                 <div>@item.EDTIME</div>
                                             </td> Else

                                             @<td class="edtime" style="border-bottom:3px solid #@bottomblackwaku;">
                                                 @If ViewData("Self") = "1" And ((item.DATAKBN = "1" And isTimeEditable = "1" And (item.RNZK = "0" OrElse (item.RNZK = "1" AndAlso item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False AndAlso item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False AndAlso item.JTJKNED.ToString("HH:mm") <> "00:00"))) Or (item.DATAKBN = "3" And (item.GYOMNO = "7"))) Then
                                                     @Html.TextBox("C0050[" & i & "].EDTIME", item.EDTIME, New With {.class = timePickerCSSCls})
                                                     @Html.ValidationMessage("C0050[" & i & "].EDTIME", "", New With {.class = "text-danger"})
                                                 Else
                                                     @Html.DisplayFor(Function(modelItem) item.EDTIME, New With {.htmlAttributes = New With {.class = "timeEdit"}})
                                                 End If
                                             </td>
                                             @<td class="showedtime" hidden="hidden" style="border-bottom:3px solid #@bottomblackwaku;">
                                                 <div>@item.EDTIME</div>
                                             </td>End If
                                         @If item.TITLEKBN = "1" Then
                                             @If item.BANGUMINM = "休暇申請中" Then
                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="banguminm" colspan="3" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>Else
                                                     @<td class="banguminm" colspan="3" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)

                                                     </td>End If
                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="memo" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>Else

                                                     @<td class="memo" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>End If
                                                 @<td></td>

                                             Else
                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="banguminm" colspan="3" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate;border-bottom:3px solid #@bottomblackwaku; ">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>
                                                     @<td class="memo" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate;border-bottom:3px solid #@bottomblackwaku; ">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                     @<td class="catcd" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; border-bottom:3px solid #@bottomblackwaku;"></td>
                                                 Else
                                                     @<td class="banguminm" colspan="3" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)

                                                     </td>
                                                     @<td class="memo" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                     @<td class="catcd" style="border-bottom:3px solid #@bottomblackwaku;"></td>
                                                 End If

                                             End If

                                         Else




                                             @If item.KAKUNIN <> "申請中" AndAlso (Html.Encode(item.BANGUMINM) = "時間休" OrElse Html.Encode(item.BANGUMINM) = "時間強休") Then


                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="banguminm" colspan="3" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate; border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>
                                                     @<td class="memo" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate;border-bottom:3px solid #@bottomblackwaku; ">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                     @<td class="catcd" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR; border-collapse:separate;border-bottom:3px solid #@bottomblackwaku; "></td>
                                                 Else
                                                     @<td class="banguminm" colspan="3" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>
                                                     @<td class="banguminm" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>
                                                     @<td class="catcd" style="border-bottom:3px solid #@bottomblackwaku;"></td>
                                                 End If

                                             Else

                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="banguminm" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR;border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>Else

                                                     @<td class="banguminm" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                                     </td>End If

                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="naiyo" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR;border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                                     </td>Else

                                                     @<td class="naiyo" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                                     </td>End If

                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="basho" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR;border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BASHO)
                                                     </td>Else

                                                     @<td class="basho" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.BASHO)
                                                     </td>End If



                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="memo" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR;border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>Else

                                                     @<td class="memo" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.MEMO)
                                                     </td>End If

                                                 @If item.BACKCOLOR IsNot Nothing Then
                                                     @<td class="catcd" style="background-color:#@item.BACKCOLOR ;  border-color:#@item.WAKUCOLOR; border-width: 2px; color:#@item.FONTCOLOR;border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.CATNM)
                                                     </td>Else

                                                     @<td class="catcd" style="border-bottom:3px solid #@bottomblackwaku;">
                                                         @Html.DisplayFor(Function(modelItem) item.CATNM)
                                                     </td>End If
                                             End If

                                         End If

                                         @Html.Hidden("C0050[" & i & "].GYOMNO", item.GYOMNO)
                                         @Html.Hidden("C0050[" & i & "].PGYOMNO", item.PGYOMNO)
                                         @Html.Hidden("C0050[" & i & "].KAKUNIN", item.KAKUNIN)
                                         @Html.Hidden("C0050[" & i & "].DATAKBN", item.DATAKBN)
                                         @Html.Hidden("C0050[" & i & "].GYOMDT", item.GYOMDT)
                                         @Html.Hidden("C0050[" & i & "].USERID", item.USERID)
                                         @Html.Hidden("C0050[" & i & "].STTIMEupdt", item.STTIMEupdt)
                                         @Html.Hidden("C0050[" & i & "].EDTIMEupdt", item.EDTIMEupdt)
                                         @Html.Hidden("C0050[" & i & "].KYUKCD", item.KYUKCD)
                                         @Html.Hidden("C0050[" & i & "].KKNST", item.KKNST)
                                         @Html.Hidden("C0050[" & i & "].KKNED", item.KKNED)

                                         @*ASI[02 Jan 2020]: to update STTIME EDTIME in D0010*@
                                         @Html.Hidden("C0050[" & i & "].SPORT_OYAFLG", item.SPORT_OYAFLG)
                                         @Html.Hidden("C0050[" & i & "].RNZK", item.RNZK)
                                         @Html.Hidden("C0050[" & i & "].SPORTFLG", item.SPORTFLG)

                                         @If ViewData("Self") = "1" Then
                                             @<td class="mymemo" style="border-bottom:3px solid #@bottomblackwaku;">

                                                 @If item.MYMEMOFLG = "1" Then
                                                     @Html.TextBox("C0050[" & i & "].MYMEMO", item.MYMEMO, htmlAttributes:=New With {.style = "width : 190px;", .id = "C0050_" & i & "__MYMEMO"})
                                                     @If item.MYMEMO IsNot Nothing Then
                                                         @<span class="CellComment" id="spanClass">@item.MYMEMO</span>
                                                     End If

                                                     Dim strKey As String = String.Format("C0050[{0}].", i)
                                                     @Html.ValidationMessage(strKey & "MYMEMO", "", New With {.class = "text-danger"})
                                                 End If



                                             </td>


                                             @<td class="showmemo" hidden="hidden">
                                                 <div class="showmemolabel" style="max-width: 150px; word-wrap: break-word">@item.MYMEMO</div>
                                             </td>
                                         End If
                                         @If Session("LoginUserACCESSLVLCD") <> "4" OrElse ViewData("Self") = "1" Then
                                             @<td class="deskmemo_and_editlink" style="border-bottom:3px solid #@bottomblackwaku;">
                                                 @If item.DATAKBN = "1" AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
                                                     Dim AV_GYOMNO = item.GYOMNO
                                                     @if item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False Then
                                                         AV_GYOMNO = item.PGYOMNO
                                                     End If
                                                     @Html.ActionLink("修正", "Edit", "B0020", routeValues:=New With {.id = AV_GYOMNO, .Form_name = "C0040"}, htmlAttributes:=New With {.class = ""})
                                                 End If
                                                 @If item.DATAKBN = "3" AndAlso ViewData("Self") = "1" AndAlso (item.GYOMNO = "7") Then
                                                     Dim AV_USERID = item.USERID
                                                     Dim AV_GYOMDT = item.GYOMDT
                                                     Dim AV_JKNST = item.STTIMEupdt
                                                     @Html.ActionLink("削除", "Delete", "C0040", routeValues:=New With {.id = AV_USERID, .gyomdt = AV_GYOMDT, .jknst = AV_JKNST}, htmlAttributes:=New With {.class = ""})
                                                 End If
                                                 @*ASI[02 Aug 2019]:Based on value of that flag display link of DeskMemo in _個人シフト screen*@
                                                 @If item.DESKMEMOEXISTFLG = True AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
                                                     @If item.DATAKBN = "1" OrElse (item.DATAKBN = "3" AndAlso ViewData("Self") = "1" AndAlso (item.GYOMNO = "7")) Then
                                                         @<span> | </span>
                                                     End If
                                                     @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = item.GYOMDT, .CondAnaid = ViewData("id"), .CondKakunin1 = "false", .CondKakunin2 = "true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:-2px;"})
                                                 End If
                                             </td>
                                         End If
                                     </tr>
                                 End If

                                 i = i + 1
                             Next
                         </tbody>


                     </table>


                 </div>

             End Using
             
             @*伝言板*@
             @if Session("LoginUserKanri") = "False" AndAlso Session("LoginUserSystem") = "False" AndAlso Session("LoginUserAna") = "True" Then
                 @if Session("msgShow") = "hide" Then
                     @<div Class="col-md-2 col-md-offset-9 affix " id="ColMsgBox" style="z-index:1;background-color:lavender; width:320px;height:380px; overflow-y:scroll; display:none;">
                         @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                         @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
                     </div>
                 Else
                     @<div Class="col-md-2 col-md-offset-9 affix " id="ColMsgBox" style="z-index:1;background-color:lavender; width:320px;height:380px; overflow-y:scroll">
                         @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                         @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
                     </div>
                 End If
             End If


             @*Havan[26 Dec 2019]: Div for newly Added link 時間休登録*@
             <div Class="col-sm-6" id="ColKyuTotoku" hidden="hidden" style="padding-left:0px;">
                 @Html.Partial("_KyuTorokuPartial", ViewData("D0060"))
             </div>
             <div class="col-sm-6" id="ColGyomu" hidden="hidden" style="padding-left:0px;">
                 @*@Html.Partial("_GyomuPartial", Model(0))*@
                 @Html.Partial("_GyomuPartial", ViewData("D0050"))
             </div>
             <div class="col-sm-6" id="ColKyuka" hidden="hidden" style="padding-left:0px;">
                 @Html.Partial("_KyukaPartial", ViewData("D0060"))
                 @*@Html.Partial("_KyukaPartial", New ViewDataDictionary(Of D0060))*@
             </div>
         </div>


<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>

<script type="text/javascript">

    $(document).ready(function () {
        //画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
        //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
        $('#mycontent').on('change', 'input', function () {
            //var kakunin = $(this).closest('tr').find('td input:first').val();
            //var note = $(this).closest('tr').find('td input').eq(1).val();
            $("#myForm").data("MSG", true);
        });
    });


    $("#Error").text("")
    $("#ErrorTitle").text("")
    if ('@memoExistDaysErr' != "") {
        $("#idErrorDiv").show();
        $("#Error").html("デスクメモが存在するため登録できません。デスクに連絡してください。<br />@memoExistDaysErr");
    }

    //Havan[26 Dec 2019] : Hide-UnHide Div on click of link 時間休登録
    $('#alEnDisKyuToroku').on('click', function (e) {
        if ($("#ColKyuTotoku").is(':hidden')) {
            $("#mycontent").removeClass("col-sm-12");
            $("#mycontent").addClass("col-sm-8");
            $("#ColKyuka").hide();
            $("#ColGyomu").hide();
            $("#ColKyuTotoku").show();
        }
        else {
            $("#ColKyuTotoku").hide();
            $("#mycontent").removeClass("col-sm-8");
            $("#mycontent").addClass("col-sm-12");
        }
    });

    $('#alEnDisGyomu').on('click', function (e) {

        if ($("#ColGyomu").is(':hidden')) {

            $("#mycontent").removeClass("col-sm-12");
            $("#mycontent").addClass("col-sm-8");
            $("#ColKyuTotoku").hide();
            $("#ColKyuka").hide();
            $("#ColGyomu").show();
        }
        else {
            $("#ColGyomu").hide();
            $("#mycontent").removeClass("col-sm-8");
            $("#mycontent").addClass("col-sm-12");

        }
    });

    $('#alEnDisKyuka').on('click', function (e) {

        if ($("#ColKyuka").is(':hidden')) {
            $("#ColKyuTotoku").hide();
            $("#ColGyomu").hide();
            $("#ColKyuka").show();
            $("#mycontent").removeClass("col-sm-12");
            $("#mycontent").addClass("col-sm-8");
        }
        else {
            $("#ColKyuka").hide();
            $("#mycontent").removeClass("col-sm-8");
            $("#mycontent").addClass("col-sm-12");

        }
    });


    $('#KAKUNIN').on('click', function (e) {

        if ($("#KAKUNIN").is(':checked')) {
            //$("#ColMsgBox").removeClass("invisible");
            this.value = 1;
        }
        else {
            //$("#ColMsgBox").last().addClass("invisible");
            this.value = 0;
        }
    });

    function handleClick(cb) {

        if (cb.checked) {
            //$("#ColMsgBox").removeClass("invisible");

            cb.value = 1;

        }
        else {
            //$("#ColMsgBox").last().addClass("invisible");
            cb.value = 0;

        }

    }

    function KeyUpFunction() {

        var searchdt = $('#stdt').val()
        var viewdate = $('#viewdatadate').val()


        if (searchdt != "") {
            if (searchdt.length == 10) {
                if (searchdt != viewdate) {
                    $("#myForm").submit();
                }

            }
        }

    }

    function PrintDiv() {

        var divContents = document.getElementById("mycontent").innerHTML;
        var oldstr = document.body.innerHTML;


        $(".mymemo").hide();
        $(".CellComment").hide();
        $(".colsttime").hide();
        $(".edtime").hide();
        $(".showmemo").show();
        $(".showsttime").show();
        $(".showedtime").show();

        var ths = document.getElementsByTagName("th");
        for (var i = 0; i < ths.length; i++) {

            if (ths[i].className == "deskmemo_and_editlink") {
                ths[i].hidden = true;
            }
        }

        var tds = document.getElementsByTagName("td");
        for (var i = 0; i < tds.length; i++) {

            if (tds[i].className == "deskmemo_and_editlink") {
                tds[i].hidden = true;
            }
        }


        var divContents = document.getElementById("mycontent").innerHTML;
        var url = myApp.Urls.baseUrl + 'Content/C0040.css';


        document.body.innerHTML = '<html><head><link rel="stylesheet"  type="text/css" media="print" href=' + url + '></head><body>' + divContents + '</body></html>'

        window.print();

        document.body.innerHTML = oldstr;

        afterPrint();

    }


    function afterPrint() {

        setTimeout(function () { document.location.href = window.location.href; }, 250);
    }

    function getStringDate(gyomdt, i) {
        var gyomdtdt = new Date(gyomdt);
        gyomdtdt = new Date(gyomdtdt.setDate(gyomdtdt.getDate() + i));
        var mm = gyomdtdt.getMonth() + 1;
        var dd = gyomdtdt.getDate();
        gyomdt = [gyomdtdt.getFullYear(), (mm > 9 ? '' : '0') + mm, (dd > 9 ? '' : '0') + dd].join('/');

        return gyomdt;
    }

    function isOverlap(currrow, sttimeOuter, edtimeOuter) {
        var overlap = false;
        var sttimeInner;
        var edtimeInner;

        var datakbn = $(currrow).find('input[id$=__DATAKBN]').val();
        var gyomno = $(currrow).find('input[id$=__GYOMNO]').val();

        if (datakbn == "3" && gyomno != "7" && gyomno != "9") {
            sttimeInner = 0;
            edtimeInner = 2400;
        }
        else {
            sttimeInner = $(currrow).find('input[id$=__STTIME]').val();
            edtimeInner = $(currrow).find('input[id$=__EDTIME]').val();

            if (sttimeInner == undefined) {
                sttimeInner = $(currrow).find('.colsttime').text().trim();
            }
            if (edtimeInner == undefined) {
                edtimeInner = $(currrow).find('.edtime').text().trim();
            }

            sttimeInner = parseInt(sttimeInner.replace(":", ""));
            edtimeInner = parseInt(edtimeInner.replace(":", ""));
        }

        if (((sttimeOuter > sttimeInner && sttimeOuter < edtimeInner) ||
            (edtimeOuter > sttimeInner && edtimeOuter < edtimeInner) ||
            (sttimeInner > sttimeOuter && sttimeInner < edtimeOuter) ||
            (edtimeInner > sttimeOuter && edtimeInner < edtimeOuter)) ||
            (sttimeOuter == sttimeInner && edtimeOuter == edtimeInner)) {
            overlap = true;
        }

        return overlap;
    }

    function validateRequiredEntry() {
        var trs = $("#mytable > tbody > tr");
        var requireFail = false;
        var errorcount = 0;
        trs.each(function () {
            var sttimeShift = $(this).find('input[id$=__STTIME]').val();
            var edtimeShift = $(this).find('input[id$=__EDTIME]').val();
            if (sttimeShift == "") {
                errorcount = errorcount + 1;
                $(this).find('span[data-valmsg-for$=STTIME]').text("必要です。");
            } else {
                $(this).find('span[data-valmsg-for$=STTIME]').text("");
            }
            if (edtimeShift == "") {
                errorcount = errorcount + 1;
                $(this).find('span[data-valmsg-for$=EDTIME]').text("必要です。");
            } else {
                $(this).find('span[data-valmsg-for$=EDTIME]').text("");
            }
        });

        if (errorcount > 0) {
            requireFail = true;
        }

        return requireFail;
    }

    //Check is overlapping check is required if false we will not proceed further
    function isOverlapCheckRequired(currrow, sttimeOuter, edtimeOuter) {
        var checkRequired = true;
        var sttimeDefaultValue = ""
        var edtimeDefaultValue = ""
        if ($(currrow).find('.colsttime').find('input').length > 0) {
            sttimeDefaultValue = $(currrow).find('.colsttime').find('input')[0].defaultValue;
            sttimeDefaultValue = parseInt(sttimeDefaultValue.replace(":", ""));
        }
        if ($(currrow).find('.edtime').find('input').length > 0) {
            edtimeDefaultValue = $(currrow).find('.edtime').find('input')[0].defaultValue;
            edtimeDefaultValue = parseInt(edtimeDefaultValue.replace(":", ""));
        }
        //When both are readonly then no change
        if (sttimeDefaultValue == "" && edtimeDefaultValue == "") {
            checkRequired = false;
        }
        //When starttime readonly and endtime is same as previous then no change
        else if (sttimeDefaultValue == "") {
            if (edtimeOuter == edtimeDefaultValue) {
                checkRequired = false;
            }
        }
        //When endtime readonly and starttime is same as previous then no change
        else if (edtimeDefaultValue == "") {
            if (sttimeOuter == sttimeDefaultValue) {
                checkRequired = false;
            }
        }
        //Whenboth time is editable but starttime and endtime are same as previous then no change
        else {
            if (sttimeOuter == sttimeDefaultValue && edtimeOuter == edtimeDefaultValue) {
                checkRequired = false;
            }
        }
        return checkRequired;
    }


    function getStartEndTimes(currrow) {
        var startendtimes = [];
        var sttimeOuter = $(currrow).find('input[id$=__STTIME]').val();
        var edtimeOuter = $(currrow).find('input[id$=__EDTIME]').val();

        if (sttimeOuter == undefined) {
            sttimeOuter = $(currrow).find('.colsttime').text().trim();
        }
        if (edtimeOuter == undefined) {
            edtimeOuter = $(currrow).find('.edtime').text().trim();
        }
        startendtimes.push({
            sttimeOuter: sttimeOuter,
            edtimeOuter: edtimeOuter
        });
        return startendtimes;
    }

    function shiftLeaveOvelapCheck() {

        var days = '@ViewData("DayCount")'
        var daycount = parseInt(days);
        var startdt = '@ViewData("searchdt")'
        var strMessage = "";
        var prevdaytimes = [];
        var isPreviousChange = false;

        loop1:
        for (var i = 0; i < daycount; i++) {

            var gyomdt = getStringDate(startdt, i);

            //Get row based on gyomdate from start to end one by one
            var trs = $("#mytable > tbody > tr input[id$='__GYOMDT'][value='" + gyomdt + "']").closest("tr");

            //filter rows
            var filterrows = trs.find('td[class=colkakunin]').filter(function () {
                return this.textContent.trim() != "申請中" && this.textContent.trim() != "仮"
            }).closest("tr");

            var currrow;

            //check overlapping with next day
            for (var idx = 0; idx < prevdaytimes.length; idx++) {
                var sttime = prevdaytimes[idx].sttimeOuterPrevious;
                var edtime = prevdaytimes[idx].endtimeOuterPrevious;
                var startformattime = prevdaytimes[idx].starttime;
                var endformattime = prevdaytimes[idx].endtime;
                var gyomdate = prevdaytimes[idx].gyomdate;
                for (var cnt = 0; cnt < filterrows.length; cnt++) {
                    currrow = filterrows[cnt];
                    var startendTimes = getStartEndTimes(currrow)
                    var sttimeOuter = parseInt(startendTimes[0].sttimeOuter.replace(":", ""));
                    var edtimeOuter = parseInt(startendTimes[0].edtimeOuter.replace(":", ""));
                    if (isOverlapCheckRequired(currrow, sttimeOuter, edtimeOuter) || isPreviousChange == true) {
                        if (isOverlap(currrow, sttime, edtime)) {
                            strMessage = gyomdate + "日で時間(" + startformattime + "～" + endformattime + ")が重複しています。"
                            break loop1;
                        }
                    }
                }
            }

            prevdaytimes = [];
            isPreviousChange = false;

            loop2:
            for (var j = 0; j < filterrows.length; j++) {
                currrow = filterrows[j];
                var currentRowIndex = j;
                var startendTimes = getStartEndTimes(currrow)

                var sttime = startendTimes[0].sttimeOuter;
                var edtime = startendTimes[0].edtimeOuter;

                if (sttime != "" && sttime.indexOf(":") == -1) {
                    if (sttime.length <= 2) {
                        sttime = sttime + ":00";
                    }
                    else if (sttime.length == 3) {
                        sttime = "0" + sttime;
                    }

                    if (sttime.length == 4 && sttime.indexOf(":") == -1) {
                        sttime = sttime.substring(0, 2).concat(":").concat(sttime.substring(2, 4));
                    }
                }

                if (edtime != "" && edtime.indexOf(":") == -1){
                    if (edtime.length <= 2) {
                        edtime = edtime + ":00";
                    }
                    else if (edtime.length == 3) {
                        edtime = "0" + edtime;
                    }

                    if (edtime.length == 4 && edtime.indexOf(":") == -1) {
                        edtime = edtime.substring(0, 2).concat(":").concat(edtime.substring(2, 4));
                    }
                }

                var sttimeOuter = parseInt(sttime.replace(":", ""));
                var edtimeOuter = parseInt(edtime.replace(":", ""));

                if (edtimeOuter > 2400) {
                    var endtimeOuterPrevious = edtimeOuter - 2400;
                    var sttimeOuterPrevious = 0000;
                    if (sttimeOuter > 2400)
                        sttimeOuterPrevious = sttimeOuter - 2400;
                    prevdaytimes.push({
                        sttimeOuterPrevious: sttimeOuterPrevious,
                        endtimeOuterPrevious: endtimeOuterPrevious,
                        starttime: sttime,
                        endtime: edtime,
                        gyomdate: gyomdt
                    });
                }

                if (!isOverlapCheckRequired(currrow, sttimeOuter, edtimeOuter)) {
                    continue loop2;
                } else {
                    if (isPreviousChange == false) {
                        isPreviousChange = true;
                    }
                }

                loop3:
                for (var k = 0; k < filterrows.length; k++) {
                    if (k == j)
                        continue loop3;
                    currrow = filterrows[k];
                    if (isOverlap(currrow, sttimeOuter, edtimeOuter)) {
                        strMessage = gyomdt + "日で時間(" + sttime + "～" + edtime + ")が重複しています。"
                        break loop1;
                    }
                }
            }
        }

        return strMessage
    }

    $(function () {
        $('#btnUpdate').click(function () {

            var gyoymd = $('#GYOMYMD').val();

            var validateRequired = validateRequiredEntry();

            if (validateRequired == true) {
                return false;
            }

            var strMsg = shiftLeaveOvelapCheck();

            if (strMsg != "") {
                alert(strMsg);
                return false;
            }

            var result = confirm("更新処理を行います。よろしいですか?")

            if (result == false) {
                return false
            }

        });

    });


    $(function () {
        $('#btnSearch').click(function () {
            var lavel = $('#accesslavel').val()
            var curdate = $('#stdt').val()

            var newdate = new Date();
            var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2) + '/' + ('0' + newdate.getDate()).slice(-2);



            if (lavel == 0) {
                if (curdate > formattedNewDate) {
                    alert('今日以降の日付は設定できません。')
                    $('#stdt').val(formattedNewDate);
                }
            }


        });


    });

    $(function () {
        $('#btnSportShow').click(function () {
            $('#sportdata').val("1");
        });
    });

    $(function () {
        $('#btnSportHide').click(function () {
            $('#sportdata').val("0");
        });
    });



    $('#EnDisColMsgBox1').on('click', function (e) {
        if ($("#ColMsgBox").is(':hidden')) {
            $("#ColMsgBox").show();
            $("#msgShow").val("show");           
        } 
        else {
            $("#ColMsgBox").hide();
            $("#msgShow").val("hide");
        }
        $("body").css("cursor", "progress");

        //非表示にしたら、ログオフするまで非表示にするため、submitしController側で現在の設定を保存している
        $("#myForm").submit();
    });
</script>





