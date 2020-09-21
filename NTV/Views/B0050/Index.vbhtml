@ModelType IEnumerable(Of NTV_SHIFT.WD0040)
@Code
    ViewData("Title") = "休日設定"
    Dim Kyukas = DirectCast(ViewBag.KyukaList, List(Of M0060))
    Dim colName As String = ""
    Dim radioName As String = ""
    Dim Bordercolor As String = ""

    Dim datalist = DirectCast(ViewBag.datalist, List(Of WD0040))
    Dim D0040List = DirectCast(ViewBag.D0040List, List(Of D0040))
    Dim strKey As String = ""
    Dim strLabelName As String = ""
    Dim strCommonName As String = ""
    Dim strD0040Name As String = ""
    Dim strD0040ID As String = ""
    Dim strColName As String = ""
    Dim strColID As String = ""
    Dim strStartID As String = ""
    Dim strEndID As String = ""
    Dim strStartName As String = ""
    Dim strEndName As String = ""

    Dim intColIndex As Integer = 0
    Dim strHI As String = ""
    Dim strRadioValue As String = ""
    Dim rowscnt As Integer = -1

    Dim rowscnt2 As Integer = -1
    Dim intColIndex2 As Integer = 0
    Dim intKeyindex As Integer = -1
    Dim strHI2 As String = ""

    Dim strMaru As String = "◯"
    Dim strBlank As String = ""
    Dim lstError As List(Of String) = ViewBag.Error
    Dim strDisplayLabel As String = ""
    Dim bolFirst As Boolean = False
    Dim strBackColor As String = "B0E0E6"
End Code


<style>
    label {
        font-size: 13px;
    }

    #tbl_wkkbn  {        
     border-collapse: separate;
     border:1px solid gray;
    }

         #tbl_wkkbn td  {
        border: 1px solid gray;        
        text-align: center;
        border-collapse :separate;
       
    }
     #tbl_wkkbn tbody.td_body  {
        /*border: 1px solid black;*/        
        text-align: center;
        border-collapse :separate;
       
    }

       #tbl_wkkbn tr  {
        /*border: 1px solid black;*/        
        text-align: center;
        border-collapse :separate;
       
    }

         #tbl_wkkbn th  {
        border: 1px solid gray;        
        text-align: center;
        border-collapse :separate;
       
    }

    
      #tbl_wkkbn tr {
 
  border-collapse:separate;
   border:1px solid black;
}

    input[type=button]:focus {
        background-color: lightgreen;
        outline: 0;
    }

    /*.table-condensed tr.td {
      padding:0px;
    }

    .table-condensed td.td_body {
        text-align: center;
         font-size: 15px;
    }


    .table-condensed td.td_head {
        vertical-align: middle;
        font-size: 10px;
    }

    .table-condensed th {
        text-align: center;
        font-size: 15px;
    }

    .scroll-content.myscroll {
        margin-top: 100px;
    }*/

    .button-selected {
        border: 4px solid red;
    }

    .td_body {
        position: relative;
    }

    .CellComment {
        display: none;
        position: absolute;
        z-index: 100;
        border: 1px;
        background-color: lavender;
        border-style: solid;
        border-width: 1px;
        border-color: blueviolet;
        padding: 3px;
        color: black;
        top: 20px;
        left: 20px;
        width: 110px;
        height: auto;
        text-align: left;
    }

    .td_body:hover span.CellComment {
        display: block;
    }

    .td_head {
        text-align: center;
    }
</style>
@Using Html.BeginForm("Index", "B0050", routeValues:=Nothing, method:=FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})
    @<div class="row" style="padding-top:10px">
        <div class="col-md-3" style="padding-top:10px">
            @If ViewData("KOKYUTENKAIALL") = True Then
                @Html.ActionLink("公休展開（全員）", "Create", "WB0050", routeValues:=New With {.searchdt = ViewData("searchdt")}, htmlAttributes:=New With {.class = "btn btn-success btn-xs"})
            End If


        </div>

        <div class="col-md-5" style="padding-top:10px">

            <ul class="nav nav-pills ">
                @*<li>
                        @If ViewData("name") IsNot Nothing Then
                            @<label style="font-size:20px; background-color:lightgreen">  @Html.Encode(ViewData("name"))さん</label>
                        End If
                    </li>*@
                <li>@*<a href="#" onclick="SetDateForm(-1)">前月</a>*@</li>
                <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(-1)">前月</button></li>
                <li>
                    <div class="input-group">
                        <input type="text" id="showdate" name="showdate" class="form-control input-sm " value=@ViewData("searchdt") onchange="KeyUpFunction()" style="width:80px;font-size:small;">
                        @*<input class="date" id="showdate" name="showdate" value=@DateTime.Today.ToString("yyyy/MM") type="text" style="width:80px;margin-top:4px;font-size:small;" >*@
                    </div>
                </li>
                <li>@*<a href="#" onclick="SetDateForm(1)">翌月</a>*@</li>
                <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(1)">翌月</button></li>
                <li><button type="submit" class="btn btn-success btn-sm">表示</button></li>
            </ul>
        </div>

        @Html.Hidden("name", ViewData("name"))
        @Html.Hidden("userid", ViewData("id"))
        @Html.Hidden("showdate", ViewData("searchdt"))
        @Html.Hidden("KYUKFLG", ViewData("KYUKFLG"))
         @Html.Hidden("viewdatadate", ViewData("searchdt"))
        <div class="col-md-4">
            <ul class="nav nav-pills navbar-right">
                <li>@Html.ActionLink("休日表", "Index", "B0040")</li>
                @*<li><a href="#" id="alEnDisGyomu">休暇申請表示/非表示</a></li>
                    <li><a href="#" id="alEnDisTime">時間休入力表示/非表示</a></li>*@
                <li><a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>
                <li>@*<a href="#" id="EnDisColMsgBox">伝言板表示/非表示</a>*@</li>
                @*<li><a href="#">戻る</a></li>*@

                @If Session("UrlReferrer") IsNot Nothing Then
                    @<li><a href="@Session("UrlReferrer")">戻る</a></li>
End If

            </ul>
        </div>


    </div>
End Using

<div class="row MainRow">
    @*@Html.ValidationSummary(True, "", New With {.class = "text-danger"})*@

    @Using Html.BeginForm("Index", "B0050", Nothing, FormMethod.Post)
        @Html.AntiForgeryToken()
        @Html.Hidden("name", ViewData("name"))
        @Html.Hidden("userid", ViewData("id"))
        @Html.Hidden("showdate", ViewData("searchdt"))
        @<div class="row">
            <div class="col-md-2" style="padding-left:20px">
                黒：未設定&nbsp;&nbsp;<font color=" blue">
                    青
                </font>：設定済 <br />
                赤：休暇申請あり
                <br />
                <a href="#" id="EnDisColMsgBox">アナウンサー一覧表示/非表示</a>


            </div>


            <div class="col-md-9" style="padding-left:75px">

                @For Each itemColor In Kyukas
                radioName = itemColor.KYUKCD & "radio"
                strRadioValue = itemColor.KYUKCD
                    @If itemColor.KYUKCD = "1" Then

                        @<input type="radio" id=@radioName name="Colradio" value=@strRadioValue />  @<label style="background-color:#@itemColor.BACKCOLOR; border:solid; border-color:#@itemColor.WAKUCOLOR; border-width: 2px; color:#@itemColor.FONTCOLOR;"> @itemColor.KYUKNM &nbsp; </label>
                    Else

                        @<input type="radio" id=@radioName name="Colradio" value=@strRadioValue />  @<label style="background-color:#@itemColor.BACKCOLOR; border:solid;  border-color:#@itemColor.WAKUCOLOR; border-width: 2px; color:#@itemColor.FONTCOLOR;"> @itemColor.KYUKNM &nbsp; </label>

                    End If



                Next
                @Html.Hidden("status", ViewData("status"))
                <input id="btnUpd" type="submit" value="更新" class="btn btn-default btn-sm" />

                <ul class="nav nav-pills ">
                    <li>
                        @If ViewData("name") IsNot Nothing Then
                            @<label style="font-size:20px; background-color:lightgreen">@ViewData("name")さん</label> @<label style="font-size:15px;">    ※休暇の種類を選んでから設定したい日をクリックしてください。</label>
        End If
                    </li>
                    <li class="nav nav-pills navbar-right"><a href="#" id="alEnDisTime">時間休入力表示/非表示</a></li>
                    <li class="nav nav-pills navbar-right"><a href="#" id="alEnDisGyomu">休暇申請表示/非表示</a></li>

                </ul>
            </div>

        </div>
        @<div class="col-md-2 " id="UserBox">
            @Html.Partial("_UserListParital", ViewData.Item("UserList"))

            <div style="padding-top:5px;">
                @If ViewData("KOKYUTENKAI") = True Then
                    @Html.ActionLink("公休展開（個人）", "Create", "WB0050", routeValues:=New With {.searchdt = ViewData("searchdt"), .userid = ViewData("id")}, htmlAttributes:=New With {.class = "btn btn-success btn-xs"})
                End If
            </div>
        </div>
        @<div class="col-md-10" id="tablecontent">

            <table id="tbl_wkkbn">
                <thead>
                    <tr id="firstrow" style="border-style:solid; border-width:1px;">
                        <th style="border-style:solid; border-width:1px; text-align:center">
                            日付
                        </th>
                        @For i = 1 To 31
                        If i = 15 Then
                            @<td id=@i style="border-style:solid; border-width:1px; border-right-width :3px;">
                                <input type="button" id="hibtn" value=@i class="btn btn-info btn-xs " style="font-size:15px; width:30px;" />
                            </td>
                        Else
                            @<td id=@i style="border-style:solid; border-width:1px;">
                                <input type="button" id="hibtn" value=@i class="btn btn-info btn-xs " style="font-size:15px; width:30px;" />
                            </td>

                        End If

                        Next
                    </tr>
                    <tr id="secondrow" style="border-style:solid; border-width:1px;">
                        <th style="border-style:solid; border-width:1px; text-align:center">
                            曜日
                        </th>
                        @For i = 1 To 31
                        If i = 15 Then
                            @<th id="@(i)yobi" style="border-style:solid; border-width:1px;text-align: center; border-right-width :3px;"></th>
                        Else
                            @<th id="@(i)yobi" style="border-style:solid; border-width:1px; text-align: center;"></th>
                        End If

                        Next
                    </tr>
                </thead>

                <tbody>
                    @For Each itemColor In Kyukas

                        @For i As Integer = 0 To Model.Count - 1
                        Dim key As String = String.Format("lstWD0040[{0}].", i)
                        Dim item = Model(i)
                            @Html.Hidden(key + "KYUKCD", item.KYUKCD)
                            @Html.Hidden(key + "HI1", item.HI1)
                            @Html.Hidden(key + "HI2", item.HI2)
                            @Html.Hidden(key + "HI3", item.HI3)
                            @Html.Hidden(key + "HI4", item.HI4)
                            @Html.Hidden(key + "HI5", item.HI5)
                            @Html.Hidden(key + "HI6", item.HI6)
                            @Html.Hidden(key + "HI7", item.HI7)
                            @Html.Hidden(key + "HI8", item.HI8)
                            @Html.Hidden(key + "HI9", item.HI9)
                            @Html.Hidden(key + "HI10", item.HI10)
                            @Html.Hidden(key + "HI11", item.HI11)
                            @Html.Hidden(key + "HI12", item.HI12)
                            @Html.Hidden(key + "HI13", item.HI13)
                            @Html.Hidden(key + "HI14", item.HI14)
                            @Html.Hidden(key + "HI15", item.HI15)
                            @Html.Hidden(key + "HI16", item.HI16)
                            @Html.Hidden(key + "HI17", item.HI17)
                            @Html.Hidden(key + "HI18", item.HI18)
                            @Html.Hidden(key + "HI19", item.HI19)
                            @Html.Hidden(key + "HI20", item.HI20)
                            @Html.Hidden(key + "HI21", item.HI21)
                            @Html.Hidden(key + "HI22", item.HI22)
                            @Html.Hidden(key + "HI23", item.HI23)
                            @Html.Hidden(key + "HI24", item.HI24)
                            @Html.Hidden(key + "HI25", item.HI25)
                            @Html.Hidden(key + "HI26", item.HI26)
                            @Html.Hidden(key + "HI27", item.HI27)
                            @Html.Hidden(key + "HI28", item.HI28)
                            @Html.Hidden(key + "HI29", item.HI29)
                            @Html.Hidden(key + "HI30", item.HI30)
                            @Html.Hidden(key + "HI31", item.HI31)

                            @If item.KYUKCD = itemColor.KYUKCD Then
                            'intKeyindex = intKeyindex + 1

                            'MsgBox(itemColor.KYUKCD)
                            Dim strRowName As String = item.KYUKCD & "Row"

                                @<tr id=@strRowName style="border-style:solid; border-width:1px;">

                                    @If itemColor.WAKUCOLOR IsNot Nothing Then
                                    Bordercolor = "#" & itemColor.WAKUCOLOR
                                    Else
                                    Bordercolor = "#000000"
                                    End If

                                    @If item.KYUKCD = "1" Then

                                        @<td class="td_head" style="border-style:solid;  background-color:#@itemColor.BACKCOLOR;  border-color:@Bordercolor; border-width: 1px; color:#@itemColor.FONTCOLOR;">
                                            勤務
                                        </td>

                                    Else

                                        @<td class="td_head" style="border-style:solid;  background-color:#@itemColor.BACKCOLOR;  border-color:@Bordercolor; border-width: 1px; color:#@itemColor.FONTCOLOR;">
                                            @itemColor.KYUKRYKNM
                                        </td>

                                    End If

                                        @If item.HI1 = "1" OrElse item.HI1 = "4" Then
                                        strBackColor = "B0E0E6"
                                        Else
                                        strBackColor = ""
                                        End If
                                        
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor;">
                                         @If item.HI1 = "1" OrElse item.HI1 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "1" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI1 = "2" OrElse item.HI1 = "4" Then

                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI1)
                                         End If


                                     </td>

                                     @If item.HI2 = "1" OrElse item.HI2 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ">
                                         @If item.HI2 = "1" OrElse item.HI2 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "2" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI2 = "2" OrElse item.HI2 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI2)
                                         End If

                                     </td>

                                     @If item.HI3 = "1" OrElse item.HI3 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If

                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI3 = "1" OrElse item.HI3 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "3" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If
                                         ElseIf item.HI3 = "2" OrElse item.HI3 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI3)
                                         End If
                                     </td>

                                     @If item.HI4 = "1" OrElse item.HI4 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI4 = "1" OrElse item.HI4 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "4" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI4 = "2" OrElse item.HI4 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI4)
                                         End If

                                     </td>


                                     @If item.HI5 = "1" OrElse item.HI5 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI5 = "1" OrElse item.HI5 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "5" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI5 = "2" OrElse item.HI5 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI5)
                                         End If

                                     </td>

                                     @If item.HI6 = "1" OrElse item.HI6 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI6 = "1" OrElse item.HI6 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "6" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI6 = "2" OrElse item.HI6 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI6)
                                         End If
                                     </td>


                                     @If item.HI7 = "1" OrElse item.HI7 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI7 = "1" OrElse item.HI7 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "7" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI7 = "2" OrElse item.HI7 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI7)
                                         End If
                                     </td>


                                     @If item.HI8 = "1" OrElse item.HI8 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI8 = "1" OrElse item.HI8 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "8" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI8 = "2" OrElse item.HI8 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI8)
                                         End If
                                     </td>


                                     @If item.HI9 = "1" OrElse item.HI9 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI9 = "1" OrElse item.HI9 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "9" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI9 = "2" OrElse item.HI9 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI9)
                                         End If
                                     </td>


                                     @If item.HI10 = "1" OrElse item.HI10 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI10 = "1" OrElse item.HI10 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "10" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI10 = "2" OrElse item.HI10 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI10)
                                         End If
                                     </td>

                                     @If item.HI11 = "1" OrElse item.HI11 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI11 = "1" OrElse item.HI11 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "11" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI11 = "2" OrElse item.HI11 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI11)
                                         End If
                                     </td>

                                     @If item.HI12 = "1" OrElse item.HI12 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI12 = "1" OrElse item.HI12 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "12" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI12 = "2" OrElse item.HI12 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI12)
                                         End If
                                     </td>

                                     @If item.HI13 = "1" OrElse item.HI13 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI13 = "1" OrElse item.HI13 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "13" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI13 = "2" OrElse item.HI13 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI13)
                                         End If
                                     </td>


                                     @If item.HI14 = "1" OrElse item.HI14 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI14 = "1" OrElse item.HI14 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "14" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI14 = "2" OrElse item.HI14 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI14)
                                         End If

                                     </td>

                                     @If item.HI15 = "1" OrElse item.HI15 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="border-style:solid; border-width:1px;  border-right-width :3px; text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI15 = "1" OrElse item.HI15 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "15" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI15 = "2" OrElse item.HI15 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI15)
                                         End If
                                     </td>

                                     @If item.HI16 = "1" OrElse item.HI16 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI16 = "1" OrElse item.HI16 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "16" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI16 = "2" OrElse item.HI16 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI16)
                                         End If
                                     </td>


                                     @If item.HI17 = "1" OrElse item.HI17 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI17 = "1" OrElse item.HI17 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "17" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI17 = "2" OrElse item.HI17 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI17)
                                         End If
                                     </td>

                                     @If item.HI18 = "1" OrElse item.HI18 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI18 = "1" OrElse item.HI18 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "18" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI18 = "2" OrElse item.HI18 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI18)
                                         End If
                                     </td>


                                     @If item.HI19 = "1" OrElse item.HI19 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI19 = "1" OrElse item.HI19 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "19" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI19 = "2" OrElse item.HI19 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI19)
                                         End If
                                     </td>

                                     @If item.HI20 = "1" OrElse item.HI20 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI20 = "1" OrElse item.HI20 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "20" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI20 = "2" OrElse item.HI20 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI20)
                                         End If
                                     </td>


                                     @If item.HI21 = "1" OrElse item.HI21 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI21 = "1" OrElse item.HI21 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "21" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If
                                         ElseIf item.HI21 = "2" OrElse item.HI21 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI21)
                                         End If
                                     </td>


                                     @If item.HI22 = "1" OrElse item.HI22 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI22 = "1" OrElse item.HI22 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "22" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI22 = "2" OrElse item.HI22 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI22)
                                         End If
                                     </td>


                                     @If item.HI23 = "1" OrElse item.HI23 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI23 = "1" OrElse item.HI23 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "23" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI23 = "2" OrElse item.HI23 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI23)
                                         End If
                                     </td>


                                     @If item.HI24 = "1" OrElse item.HI24 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI24 = "1" OrElse item.HI24 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "24" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI24 = "2" OrElse item.HI24 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI24)
                                         End If
                                     </td>

                                     @If item.HI25 = "1" OrElse item.HI25 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI25 = "1" OrElse item.HI25 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "25" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If
                                         ElseIf item.HI25 = "2" OrElse item.HI25 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI25)
                                         End If
                                     </td>

                                     @If item.HI26 = "1" OrElse item.HI26 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI26 = "1" OrElse item.HI26 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "26" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI26 = "2" OrElse item.HI26 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI26)
                                         End If
                                     </td>


                                     @If item.HI27 = "1" OrElse item.HI27 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI27 = "1" OrElse item.HI27 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "27" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI27 = "2" OrElse item.HI27 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI27)
                                         End If
                                     </td>


                                     @If item.HI28 = "1" OrElse item.HI28 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI28 = "1" OrElse item.HI28 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "28" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI28 = "2" OrElse item.HI28 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI28)
                                         End If
                                     </td>


                                     @If item.HI29 = "1" OrElse item.HI29 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI29 = "1" OrElse item.HI29 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "29" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI29 = "2" OrElse item.HI29 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI29)
                                         End If
                                     </td>


                                     @If item.HI30 = "1" OrElse item.HI30 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI30 = "1" OrElse item.HI30 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "30" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI30 = "2" OrElse item.HI30 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI30)
                                         End If
                                     </td>

                                     @If item.HI31 = "1" OrElse item.HI31 = "4" Then
                                     strBackColor = "B0E0E6"
                                     Else
                                     strBackColor = ""
                                     End If
                                     <td class="td_body" style="text-align:center; background-color:#@strBackColor; ;">
                                         @If item.HI31 = "1" OrElse item.HI31 = "3" Then
                                             @strMaru
                                             @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                             Dim strDisplay As String = ""
                                             If item.D0040 IsNot Nothing Then
                                             For Each itemd0040 In item.D0040
                                             If itemd0040.HI = "31" AndAlso itemd0040.JKNST IsNot Nothing Then
                                             strDisplay = strDisplay & vbCrLf & itemd0040.JKNST.ToString.Substring(0, 2) & ":" & itemd0040.JKNST.ToString.Substring(2, 2) & "～" & itemd0040.JKNED.ToString.Substring(0, 2) & ":" & itemd0040.JKNED.ToString.Substring(2, 2)
                                             End If
                                             Next
                                             End If
                                                 @<span class="CellComment">@strDisplay</span>
                                             End If

                                         ElseIf item.HI31 = "2" OrElse item.HI31 = "4" Then
                                         Else
                                             @Html.DisplayFor(Function(modelItem) item.HI31)
                                         End If
                                     </td>



                                </tr>
                            Exit For
                            End If


                        Next



                    Next
                    @For Each item In Model
                        @*ASI [16 Oct 2019] : replaced KYUKCD "11" with "99" *@
                        @If item.KYUKCD = "99" Then

                        Dim key As String = String.Format("lstWD0040[{0}].", Kyukas.Count + 1)
                        Dim strRowName As String = item.KYUKCD & "Row"
                            @Html.Hidden(key + "KYUKCD", item.KYUKCD)
                            @Html.Hidden(key + "HI1", item.HI1)
                            @Html.Hidden(key + "HI2", item.HI2)
                            @Html.Hidden(key + "HI3", item.HI3)
                            @Html.Hidden(key + "HI4", item.HI4)
                            @Html.Hidden(key + "HI5", item.HI5)
                            @Html.Hidden(key + "HI6", item.HI6)
                            @Html.Hidden(key + "HI7", item.HI7)
                            @Html.Hidden(key + "HI8", item.HI8)
                            @Html.Hidden(key + "HI9", item.HI9)
                            @Html.Hidden(key + "HI10", item.HI10)
                            @Html.Hidden(key + "HI11", item.HI11)
                            @Html.Hidden(key + "HI12", item.HI12)
                            @Html.Hidden(key + "HI13", item.HI13)
                            @Html.Hidden(key + "HI14", item.HI14)
                            @Html.Hidden(key + "HI15", item.HI15)
                            @Html.Hidden(key + "HI16", item.HI16)
                            @Html.Hidden(key + "HI17", item.HI17)
                            @Html.Hidden(key + "HI18", item.HI18)
                            @Html.Hidden(key + "HI19", item.HI19)
                            @Html.Hidden(key + "HI20", item.HI20)
                            @Html.Hidden(key + "HI21", item.HI21)
                            @Html.Hidden(key + "HI22", item.HI22)
                            @Html.Hidden(key + "HI23", item.HI23)
                            @Html.Hidden(key + "HI24", item.HI24)
                            @Html.Hidden(key + "HI25", item.HI25)
                            @Html.Hidden(key + "HI26", item.HI26)
                            @Html.Hidden(key + "HI27", item.HI27)
                            @Html.Hidden(key + "HI28", item.HI28)
                            @Html.Hidden(key + "HI29", item.HI29)
                            @Html.Hidden(key + "HI30", item.HI30)
                            @Html.Hidden(key + "HI31", item.HI31)
                            @<tr id=@strRowName style="border-style:solid; border-width:1px;">
                                <td class="td_head" style="border-style:solid;border-width:1px;">シフト</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI1)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI2)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI3)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI4)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI5)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI6)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI7)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI8)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI9)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI10)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI11)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI12)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI13)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI14)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px;  border-right-width :3px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI15)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI16)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI17)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI18)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI19)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI20)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI21)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI22)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI23)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI24)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI25)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI26)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI27)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI28)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI29)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI30)</td>

                                <td class="td_body" style="border-style:solid; border-width:1px; text-align:center;">@Html.DisplayFor(Function(modelItem) item.HI31)</td>
                            </tr>
                        Exit For
                        End If
                    Next




                </tbody>

            </table>

        </div>


        @<div class="col-md-4" id="ColTime" style="padding-top:10px;">

            <div id="ColTimeError" style="padding-top:10px">
                @If lstError IsNot Nothing Then
                    @For Each item In lstError
                        @<p id="errorLogin" style="color:red">@item</p>
                    Next
                                                                                Else
                    @<p id="errorLogin" style="color:red"></p>
                End If
            </div>

            <div class="TimeTable" style="width:330px;max-height:115px;overflow-y:auto;">

                <table id="sdtTable" class="tbllayout">
                    @*<tr>
                            <td></td>
                        </tr>*@

                    @If D0040List IsNot Nothing Then
                        @For Each item In D0040List

                        If item.KYUKCD = "7" Then

                        rowscnt = rowscnt + 1
                        If strHI <> item.HI.ToString Then
                        intColIndex = -1
                        strHI = item.HI.ToString

                        'If item.HI <> "1" Then
                        '    Dim intHI As Integer = Integer.Parse(item.HI.ToString)
                        '    rowscnt = (intHI - 1) * 5
                        'End If
                        End If



                        intColIndex = intColIndex + 1
                        Dim intKyukacd As Integer = Integer.Parse(item.KYUKCD) - 1

                        strLabelName = "JIKYU_" & item.HI & intColIndex & "__HI"
                        strCommonName = String.Format("lstWD0040[{0}].", intKyukacd)
                        strD0040Name = String.Format("D0040[{0}].", rowscnt)
                        strD0040ID = "D0040_" & rowscnt
                        strColName = strCommonName & strD0040Name
                        strColID = strCommonName & strD0040ID

                        strStartID = strColID & "__JKNSTt"
                        strEndID = strColID & "__JKNED"
                        strStartName = strColName & "JKNST"
                        strEndName = strColName & "JKNED"

                        strKey = strCommonName & strD0040Name
                        strDisplayLabel = item.HI & "日-"

                        If lstError IsNot Nothing AndAlso lstError.Count > 0 AndAlso bolFirst = False AndAlso item.HI = ViewData("ErrorHI") AndAlso item.KYUKCD = ViewData("ErrorID") Then

                            @<tr>

                                <td>
                                    <label style="background-color:lavender">@strDisplayLabel</label>
                                    <label id=@strLabelName>時間休</label>



                                    @Html.Hidden(strKey & "USERID", item.USERID)
                                    @Html.Hidden(strKey & "NENGETU", item.NENGETU)
                                    @Html.Hidden(strKey & "HI", item.HI)
                                    @Html.Hidden(strKey & "KYUKCD", item.KYUKCD)
                                </td>
                                <td>
                                    <input id=@strStartID name=@strStartName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNST></input>
                                </td>
                                <td>
                                    <input id=@strEndID name=@strEndName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNED></input>
                                </td>
                                <td>
                                    <input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>
                                </td>
                            </tr>

                        Else
                            @<tr style="display:none">

                                <td>
                                    <label style="background-color:lavender">@strDisplayLabel</label>
                                    <label id=@strLabelName>時間休</label>



                                    @Html.Hidden(strKey & "USERID", item.USERID)
                                    @Html.Hidden(strKey & "NENGETU", item.NENGETU)
                                    @Html.Hidden(strKey & "HI", item.HI)
                                    @Html.Hidden(strKey & "KYUKCD", item.KYUKCD)
                                </td>
                                <td>
                                    <input id=@strStartID name=@strStartName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNST></input>
                                </td>
                                <td>
                                    <input id=@strEndID name=@strEndName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNED></input>
                                </td>
                                <td>
                                    <input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>
                                </td>
                            </tr>
                        End If



                        End If


                        Next

                    End If


                </table>

                <table id="sdtTable2" class="tbllayout">
                    @*<tr>
                            <td></td>
                        </tr>*@


                    @If D0040List IsNot Nothing Then
                        @For Each item In D0040List


                        If item.KYUKCD = "9" Then
                        rowscnt2 = rowscnt2 + 1
                        If strHI2 <> item.HI.ToString Then
                        intColIndex2 = -1
                        strHI2 = item.HI.ToString

                        'If item.HI <> "1" Then
                        '    Dim intHI As Integer = Integer.Parse(item.HI.ToString)
                        '    rowscnt = (intHI - 1) * 5
                        'End If
                        End If



                        intColIndex2 = intColIndex2 + 1
                        Dim intKyukacd As Integer = Integer.Parse(item.KYUKCD) - 1

                        strLabelName = "KYOKYU_" & item.HI & intColIndex2 & "__HI"
                        strCommonName = String.Format("lstWD0040[{0}].", intKyukacd)
                        strD0040Name = String.Format("D0040[{0}].", rowscnt2)
                        strD0040ID = "D0040_" & rowscnt2
                        strColName = strCommonName & strD0040Name
                        strColID = strCommonName & strD0040ID

                        strStartID = strColID & "__JKNSTt"
                        strEndID = strColID & "__JKNED"
                        strStartName = strColName & "JKNST"
                        strEndName = strColName & "JKNED"

                        strKey = strCommonName & strD0040Name
                        strDisplayLabel = item.HI & "日-"

                        If lstError IsNot Nothing AndAlso lstError.Count > 0 AndAlso bolFirst = False AndAlso item.HI = ViewData("ErrorHI") AndAlso item.KYUKCD = ViewData("ErrorID") Then

                            @<tr>

                                <td>
                                    <label style="background-color:lavender">@strDisplayLabel</label>
                                    <label id=@strLabelName>時強休</label>



                                    @Html.Hidden(strKey & "USERID", item.USERID)
                                    @Html.Hidden(strKey & "NENGETU", item.NENGETU)
                                    @Html.Hidden(strKey & "HI", item.HI)
                                    @Html.Hidden(strKey & "KYUKCD", item.KYUKCD)
                                </td>
                                <td>
                                    <input id=@strStartID name=@strStartName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNST></input>
                                </td>
                                <td>
                                    <input id=@strEndID name=@strEndName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNED></input>
                                </td>
                                <td>
                                    <input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>
                                </td>
                            </tr>
                        Else
                            @<tr style="display:none">

                                <td>
                                    <label style="background-color:lavender">@strDisplayLabel</label>
                                    <label id=@strLabelName>時強休</label>



                                    @Html.Hidden(strKey & "USERID", item.USERID)
                                    @Html.Hidden(strKey & "NENGETU", item.NENGETU)
                                    @Html.Hidden(strKey & "HI", item.HI)
                                    @Html.Hidden(strKey & "KYUKCD", item.KYUKCD)
                                </td>
                                <td>
                                    <input id=@strStartID name=@strStartName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNST></input>
                                </td>
                                <td>
                                    <input id=@strEndID name=@strEndName class="form-control input-sm timecustom imedisabled" style="width:100px;" value=@item.JKNED></input>
                                </td>
                                <td>
                                    <input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>
                                </td>
                            </tr>

                        End If

                        End If



                        Next

                    End If





                </table>

            </div>


        </div>

    End Using

    <div class="col-md-6" id="ColGyomu" style="padding-top:10px">
        @Html.Partial("_D0060Partial", ViewData.Item("D0060"))
    </div>
</div>



<script type="text/javascript">


    $('#alEnDisGyomu').on('click', function (e) {

        if ($("#ColGyomu").is(':hidden')) {


            $("#ColGyomu").show();
        }
        else {
            $("#ColGyomu").hide();


        }
    });


    $('#alEnDisTime').on('click', function (e) {

        if ($("#ColTime").is(':hidden')) {


            $("#ColTime").show();
            $("#ColTimeError").show();
        }
        else {
            $("#ColTime").hide();
            $("#ColTimeError").hide();

        }
    });


    $('#EnDisColMsgBox').on('click', function (e) {

        if ($("#UserBox").is(':hidden')) {
            //$("#ColMsgBox").removeClass("invisible");
            $("#tablecontent").removeClass("col-md-12");
            $("#tablecontent").addClass("col-md-10");
            $("#UserBox").show();

        }
        else {

            //$("#ColMsgBox").last().addClass("invisible");
            $("#UserBox").hide();
            $("#tablecontent").removeClass("col-md-10");
            $("#tablecontent").addClass("col-md-12");

        }
    });


    $(document).ready(function () {

        //申請がない場合ユーザー一覧を非表示にする
        var kyukakbn = $("#KYUKFLG").val();
        if (kyukakbn != '1') {
            $("#UserBox").hide();
        }

        var showdate = $('#showdate').val().split('/');
        var curdates = new Date(showdate[0], showdate[1] - 1, '01');

        if (curdates == '') {
            curdates = new Date();
        }

        var formattedNewDate = curdates.getFullYear() + '/' + ('0' + (curdates.getMonth() + 1)).slice(-2);

        var today = new Date();
        var todaydate = today.getFullYear() + '/' + ('0' + (today.getMonth() + 1)).slice(-2);

        //過去日でも登録したいという要望があったため、ラジオボタンの使用不可をやめる 2018/03/07
        //if (formattedNewDate < todaydate) {

        //    var radioList = document.getElementsByName("Colradio");

        //    for (var i = 0; i < radioList.length; i++) {
        //        radioList[i].disabled = true;
        //    }
        //}
        //else {
        //    var radioList = document.getElementsByName("Colradio");

        //    for (var i = 0; i < radioList.length; i++) {
        //        radioList[i].disabled = false;
        //    }
        //}


        var d = new Date(curdates.getFullYear(), (curdates.getMonth() + 1), 0).getDate();
        //alert(d)

        var day_of_week = new Array('日', '月', '火', '水', '木', '金', '土');
        week_day = curdates.getDay();
        var table = document.getElementById("tbl_wkkbn");



        var row = document.getElementById("firstrow");
        var row2 = document.getElementById("secondrow");


        var needcol = 31 - d
        //alert (needcol)

        //var value = 0
        for (index = 1; index <= d; index++) {

            var cell = document.getElementById(index)
            //row.insertCell(index).innerHTML = index;

            //cell.innerHTML = '<input type="submit" value=1 class="btn btn-info btn-xs" style="width:23px; font-size:9px;" />';

            cell.innerHTML = '<input type="button" id="hibtn" value=' + index + ' class="btn btn-info btn-xs " style="font-size:15px; width:30px;" />'

            var dateObj = new Date(curdates.getFullYear(), curdates.getMonth(), index)
            var day = dateObj.getDay()


            //row2.insertCell(index).innerHTML = day_of_week[day];
            var cell2 = document.getElementById(index + 'yobi')
            cell2.innerHTML = day_of_week[day];
            cell.style.display = "table-cell";
            cell2.style.display = "table-cell";

        }


        var table = document.getElementById('tbl_wkkbn'),
  rows = table.getElementsByTagName('tr');


        for (i = 2, j = rows.length; i < j; ++i) {
            cells = rows[i].getElementsByTagName('td');

            for (index = 1; index <= d; index++) {
                cells[index].style.display = "table-cell";

            }
        }

        if (needcol > 0) {

            for (index = 1; index <= needcol; index++) {
                startcol = d + index
                var cell = document.getElementById(startcol)
                var cell2 = document.getElementById(startcol + 'yobi')
                //cell.innerHTML = ''
                //cell2.innerHTML = ''
                cell.style.display = "none";
                cell2.style.display = "none";
            }

            var table = document.getElementById('tbl_wkkbn'),
              rows = table.getElementsByTagName('tr');


            for (i = 2, j = rows.length; i < j; ++i) {
                cells = rows[i].getElementsByTagName('td');

                for (index = 1; index <= needcol; index++) {
                    startcol = d + index
                    cells[startcol].style.display = "none";

                }
            }
        }


        var table = document.getElementById("tbl_wkkbn");
        var kohorows = table.getElementsByTagName("tr");
        for (var i = 2; i < kohorows.length; i += 1) {
            var row = table.rows[i]
            for (index = 1; index <= needcol; index++) {
                startcol = row.children.length - index

                row.cells(startcol).innerHTML = '';

            }
        }



    });

    function SetDateForm(months) {


        var curdates = $('#showdate').val().split('/');
        var newdate = new Date(curdates[0], curdates[1] - 1, '01');

        newdate.setMonth(newdate.getMonth() + months);

        var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2);

        $('#showdate').val(formattedNewDate);

        var formattedNewDate1 = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2) + '/' + ('0' + newdate.getDate()).slice(-2);

        var d = new Date(newdate.getFullYear(), (newdate.getMonth() + 1), 0).getDate();
        //alert(d)

        var day_of_week = new Array('日', '月', '火', '水', '木', '金', '土');
        week_day = newdate.getDay();
        var table = document.getElementById("myTableData");

        var today = new Date();

        var curdate = today.getFullYear() + '/' + ('0' + (today.getMonth() + 1)).slice(-2);

        //過去日でも登録したいという要望があったため、ラジオボタンの使用不可をやめる 2018/03/07
        //if (formattedNewDate < curdate) {

        //    var radioList = document.getElementsByName("Colradio");

        //    for (var i = 0; i < radioList.length; i++) {
        //        radioList[i].disabled = true;
        //    }
        //}
        //else {
        //    var radioList = document.getElementsByName("Colradio");

        //    for (var i = 0; i < radioList.length; i++) {
        //        radioList[i].disabled = false;
        //    }
        //}
        //var rowCount = table.rows.length;
        //if (rowCount > 1) {
        //    table.deleteRow(1)
        //    table.deleteRow(0)
        //}


        var row = document.getElementById("firstrow");

        var row2 = document.getElementById("secondrow");
        var int = 0
        //row.insertCell(0).innerHTML = '日付';
        //row2.insertCell(0).innerHTML = '曜日';
        //var value = 0
        var needcol = 31 - d
        var startcol;
        for (index = 1; index <= d; index++) {


            //row.insertCell(index).innerHTML = index;
            var cell = document.getElementById(index)
            //cell.innerHTML = index;
            cell.innerHTML = '<input type="button" id="hibtn" value=' + index + ' class="btn btn-info btn-xs" style="font-size:15px; width:30px;" />'
            var dateObj = new Date(newdate.getFullYear(), newdate.getMonth(), index)
            var day = dateObj.getDay()

            //row2.insertCell(index).innerHTML = day_of_week[day];
            var cell2 = document.getElementById(index + 'yobi')
            cell2.innerHTML = day_of_week[day];
            cell.style.display = "table-cell";
            cell2.style.display = "table-cell";

        }



        var table = document.getElementById('tbl_wkkbn'),
            rows = table.getElementsByTagName('tr');


        for (i = 2, j = rows.length; i < j; ++i) {
            cells = rows[i].getElementsByTagName('td');

            for (index = 1; index <= d; index++) {
                cells[index].style.display = "table-cell";

            }
        }

        if (needcol > 0) {

            for (index = 1; index <= needcol; index++) {
                startcol = d + index
                //row.insertCell(startcol).innerHTML = '';
                //row2.insertCell(startcol).innerHTML = '';
                var cell = document.getElementById(startcol)
                var cell2 = document.getElementById(startcol + 'yobi')
                //cell.innerHTML = ''
                //cell2.innerHTML = ''
                cell.style.display = "none";
                cell2.style.display = "none";
            }


            var table = document.getElementById('tbl_wkkbn'),
                rows = table.getElementsByTagName('tr');


            for (i = 2, j = rows.length; i < j; ++i) {
                cells = rows[i].getElementsByTagName('td');

                for (index = 1; index <= needcol; index++) {
                    startcol = d + index
                    cells[startcol].style.display = "none";

                }
            }

        }


        //var table = document.getElementById("tbl_wkkbn");
        //var kohorows = table.getElementsByTagName("tr");
        //for (var i = 2; i < kohorows.length  ; i += 1) {
        //    var row = table.rows[i]
        //    for (index = 1; index <= 31; index++) {
        //        startcol = row.children.length - index

        //        row.cells(startcol).innerHTML = '';

        //    }
        //}

        var table = document.getElementById('tbl_wkkbn'),
            rows = table.getElementsByTagName('tr');


        for (i = 2, j = rows.length; i < j; ++i) {
            cells = rows[i].getElementsByTagName('td');

            for (index = 1; index <= 31; index++) {
                //cells[index].innerHTML = '<td class="td_body" style="width: 21px; border-collapse:separate;"></td>'
                cells[index].innerHTML = '';
                cells[index].style.backgroundColor = "white";
                //cells[index].style.borderColor = "darkgray";

            }
        }



    }




    //tdクリックにするとParitalのテーブルボタンクルックも走ってしまうため

    $('td').on('click', function (e) {

        //過去の月に修正出来ないようにするため、日ボタンクリックされても◯を付けない
        var today = new Date();

        var curdate = today.getFullYear() + '/' + ('0' + (today.getMonth() + 1)).slice(-2);
        var txtdate = document.getElementById("showdate").value;

        //過去日でも登録したいという要望があったため、ラジオボタンの使用不可をやめる 2018/03/07
        //if (txtdate < curdate) {

        //    return false
        //}


        var intRadioValue = 0
        evID = $(this).closest('tr').attr("id");
        if (evID == "firstrow") {

            //セルがブラングの場合、◯付けないようにする
            var cellbutton = document.getElementById($(this).index())
            if (cellbutton.innerHTML == '') {
                return false
            }

            var radioList = document.getElementsByName("Colradio");
            var intindex = 0;
            var strIndex = ''
            var strCellName = ''
            for (var i = 0; i < radioList.length; i++) {
                if (radioList[i].checked) {

                    //strIndex = radioList[i].value.split(':')
                    intindex = radioList[i].value
                    intRadioValue = intindex
                    //strCellName = strIndex[1];
                    strCellName = intindex + 'Row'
                    //alert(strCellName)
                    break;
                }
            }

            //ラジオボタンを選ばずに日ボタンクリックした時
            if (strCellName == '') {
                alert('休暇の種類を選んでから設定したい日をクリックしてください。')
                return;
            }

            intindex = parseInt(intindex);

            var intHiddenIndex = 0
            intHiddenIndex = intindex - 1;


            //intindex += 1;
            intindex++;
            //intCellIndex++;

            //myRowIndex = $(this).parent().index();
            myColIndex = $(this).index();

            var table = document.getElementById("tbl_wkkbn");

            var strShiftCellName = '99Row'
            var shiftrow = document.getElementById(strShiftCellName);
            var shiftcell = shiftrow.cells[myColIndex];
            var shiftval = shiftcell.innerHTML;


            if (shiftval != '') {
                intRadioValue
                if (intRadioValue == '4' || intRadioValue == '6' || intRadioValue == '8' || intRadioValue == '5' || intRadioValue == '11' || intRadioValue == '12') {
                    alert('シフト有りの日に対して、公休、法休、振公休、振法休、代休、強休は設定できません。')
                    return false
                }

            }

            var kohorows = table.getElementsByTagName("tr");

            //メッセージ出すため休出と２４超えあるかチェック
            var bolHave = false
            var msg = ''
            for (var i = 2; i < kohorows.length; i += 1) {

                var x = 0
                var cellloop = table.rows[i].cells[myColIndex];


                var row = table.rows[i]
                var rowid = row.id

                if (rowid != undefined) {
                    var introwidIndex = 0;

                    if (rowid == '10Row' || rowid == '11Row' || rowid == '12Row' || rowid == '13Row' || rowid == '14Row' || rowid == '99Row') {
                        introwidIndex = rowid.substring(0, 2);
                    }
                    else {
                        introwidIndex = rowid.substring(0, 1);
                    }
                    var intHIndex = 0
                    intHIndex = introwidIndex - 1

                    //alert(intHIndex)
                }

                var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex

                if (rowid == '10Row' || rowid == '2Row' || rowid == '13Row' || rowid == '14Row') {
                    if ($(strHicol).val() != '') {
                        bolHave = true
                    }
                }

                var strHicolJikan = "#lstWD0040_" + 6 + "__HI" + myColIndex
                var strHicolJikanKyo = "#lstWD0040_" + 8 + "__HI" + myColIndex
                if ($(strHicolJikan).val() != '' && $(strHicolJikanKyo).val() != '') {
                    bolHave = true
                }

            }



            for (var i = 2; i < kohorows.length; i += 1) {
                var x = 0
                var cellloop = table.rows[i].cells[myColIndex];

                var intHIndex = 0
                var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex
                //alert('clear')
                $(strHicol).val('');

                var row = table.rows[i]
                var rowid = row.id

                if (rowid != undefined) {
                    var introwidIndex = 0;

                    if (rowid == '10Row' || rowid == '11Row' || rowid == '12Row' || rowid == '13Row' || rowid == '14Row' || rowid == '99Row') {
                        introwidIndex = rowid.substring(0, 2);
                    }
                    else {
                        introwidIndex = rowid.substring(0, 1);
                    }
                    var intHIndex = 0
                    intHIndex = introwidIndex - 1

                    //alert(intHIndex)
                }



                if (intRadioValue == '7' || intRadioValue == '9' || intRadioValue == '10' || intRadioValue == '2' || intRadioValue == '13' || intRadioValue == '14') {



                    if (intRadioValue == '7' || intRadioValue == '9') {

                        if (rowid == '7Row' || rowid == '9Row' || rowid == '10Row' || rowid == '2Row' || rowid == '13Row' || rowid == '14Row') {
                            if (cellloop.innerHTML == '' || cellloop.innerText == '') {



                                var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex
                                //alert('clear')
                                $(strHicol).val('2');
                            }
                        }

                    }

                    //if (rowid != '7Row' && rowid != '9Row' && rowid != '10Row' && rowid != '2Row') {

                    //       //var intHIndex = 0
                    //    //intHIndex = i - 1
                    //    var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex
                    //    //alert('clear')
                    //    $(strHicol).val('');
                    //}


                    if (cellloop.innerHTML != '' || cellloop.innerText != '') {


                        //alert(rowid)
                        if (rowid != '7Row' && rowid != '9Row' && rowid != '10Row' && rowid != '2Row' && rowid != '13Row' && rowid != '14Row') {

                            //x = 1;
                            if (cellloop != shiftcell) {
                                cellloop.innerHTML = ''
                            }

                            //var intHIndex = 0
                            //intHIndex = i - 1
                            var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex
                            //alert('clear')
                            //alert(strHicol)
                            if ($(strHicol).val() != '1') {
                                $(strHicol).val('');
                            }
                            else {
                                $(strHicol).val('4');
                            }


                        }
                        else {
                            if ((intRadioValue == '10' && rowid == '2Row') || (intRadioValue == '2' && rowid == '10Row') || (intRadioValue == '14' && rowid == '13Row') || (intRadioValue == '13' && rowid == '14Row')) {
                                //x = 1;
                                if (cellloop != shiftcell) {
                                    cellloop.innerHTML = ''
                                }


                                //var intHIndex = 0
                                //intHIndex = i - 1
                                //alert(intHIndex)
                                var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex
                                //alert('clear')
                                //alert(strHicol)
                                if ($(strHicol).val() != '1') {
                                    $(strHicol).val('');
                                }
                                else {
                                    $(strHicol).val('4');
                                }

                            }
                        }
                    }
                }
                else {

                    //休出と２４超えがある、時間休時間強休もある時に他の休暇コードに変えられたら、時間休も消えることをメッセージ出す

                    var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex


                    if (rowid == '7Row') {
                        if ($(strHicol).val() != '' && bolHave == true) {
                            if (intRadioValue == '1') {
                                msg = '時間休'
                            }

                        }

                    }

                    if (rowid == '9Row') {

                        if ($(strHicol).val() == '1' && bolHave == true) {
                            if (intRadioValue == '1') {
                                if (msg != '') {

                                    msg = msg + ',時間強休'
                                }
                                else {
                                    msg = '時間強休'
                                }
                            }

                        }
                    }

                    //--------------------
                    if (cellloop != shiftcell) {
                        cellloop.innerHTML = ''
                    }

                    //var intHIndex = 0
                    //intHIndex = i - 1
                    var strHicol = "#lstWD0040_" + intHIndex + "__HI" + myColIndex
                    //alert('clear')
                    //alert(strHicol)
                    if ($(strHicol).val() != '1') {
                        $(strHicol).val('');
                    }
                    else {
                        $(strHicol).val('4');
                    }
                }


            }


            if (msg != '') {
                alert(msg + 'も消えます。必要な場合再び設定してください。')
            }

            //var row = table.rows[strCellName];
            var row = document.getElementById(strCellName);
            var cell = row.cells[myColIndex];
            var strHiddenColName = "#lstWD0040_" + intHiddenIndex + "__HI" + myColIndex
            //alert(strHiddenColName)
            $(strHiddenColName).val('3');

            //alert(strHiddenColName)


            var sevenrow = document.getElementById('7Row');
            var ninerow = document.getElementById('9Row');
            var sevencell = sevenrow.cells[myColIndex];
            var ninecell = ninerow.cells[myColIndex];


            if (intRadioValue == '10' || intRadioValue == '2' || intRadioValue == '13' || intRadioValue == '14') {

                if (cell.innerHTML.indexOf('◯') != -1) {

                    if ((sevencell.innerHTML.indexOf('◯') != -1) || (ninecell.innerHTML.indexOf('◯') != -1)) {
                        cell.innerHTML = ''
                        $(strHiddenColName).val('2');

                    }

                }
                else {

                    cell.innerHTML = '◯'
                }
            }
            else {

                cell.innerHTML = '◯'
            }

            var rowscnt = $('#sdtTable tr').length;

            //if (myColIndex != '1') {

            //    rowscnt = (myColIndex - 1) * 5;
            //    //alert(rowscnt)
            //  }


            //if (rowscnt != 0) {
            //    rowscnt = rowscnt - 1
            //}

            if (intRadioValue != '7') {
                $('#sdtTable tr').each(function () {
                    //alert('reach')
                    $(this).hide();
                });


            }

            if (intRadioValue != '9') {

                $('#sdtTable2 tr').each(function () {
                    //alert('reach')
                    $(this).hide();
                });
            }



            if (shiftval != '') {

                //シフトありのセルをクリックする時にセルに黒丸をもう一回付ける、そうしないとエラーチェックで帰って来た時黒丸が消えているため
                var rowCount = $('#tbl_wkkbn tr').length;
                var shiftIndex = (rowCount - 2);
                var strHiddenshift = "#lstWD0040_" + shiftIndex + "__HI" + myColIndex
                $(strHiddenshift).val(shiftval);
            }



            //alert(rowscnt)
            if (intRadioValue == '7') {

                var table = document.getElementById("sdtTable");
                var rows = table.getElementsByTagName("tr");

                //var rowcount = rows.length - 1;

                var JikyuElement = document.getElementById("JIKYU_" + myColIndex + "0__HI");

                if (JikyuElement == null) {

                    //$("tr").each(function () {
                    //    $(this).find("input.form-control").hide;
                    //    $(this).find("input.form-control").hide;
                    //});

                    $('#sdtTable tr').each(function () {
                        //alert('reach')
                        $(this).hide();
                    });

                    var i = 0

                    for (rowcount = rowscnt; i < 5; rowcount++) {
                        //alert(rowcount)
                        var row = table.insertRow();
                        var cell1 = row.insertCell(0);
                        var cell2 = row.insertCell(1);
                        var cell3 = row.insertCell(2);
                        var cell4 = row.insertCell(3);
                        var cellname = myColIndex + '日-'


                        cell1.innerHTML = ' <label style="background-color:lavender">' + cellname + '</label><label  id ="JIKYU_' + myColIndex + i + '__HI">時間休</label>'

                        //alert('JIKYU_' + myColIndex + rowcount + '__HI')




                        i++;

                        cell2.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__HI" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].HI" type="hidden" value="' + myColIndex + '" ></input>' +
                            '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__KYUKCD" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].KYUKCD" type="hidden" value="' + intRadioValue + '" ></input>' +
                            '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNST" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNST"  class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                        cell3.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNED" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNED" class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                        cell4.innerHTML = '<input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>';

                        $('.timecustom').timepicker({
                            showMeridian: false,
                            maxHours: 25,
                            minuteStep: 5,
                            defaultTime: false
                        });

                        $(document).on('focus', 'input', function () {
                            $('.timecustom').not(this).timepicker('hideWidget');
                        });

                    }




                }
                else {

                    $('#sdtTable tr').each(function () {
                        //alert('reach')
                        $(this).hide();
                    });

                    //alert(rowscnt)
                    var i = 0
                    for (intindex = 0; intindex < 5; intindex++) {

                        var colName = "JIKYU_" + myColIndex + intindex + "__HI"
                        var lbl = document.getElementById(colName)
                        $(lbl).closest("tr").show();

                        if (lbl != null) {


                            document.getElementById(colName).innerHTML = '時間休';




                        }


                        //alert(colName)


                        if (lbl == null) {
                            rowcount = rowscnt
                            //alert(rowscnt)
                            var row = table.insertRow();
                            var cell1 = row.insertCell(0);
                            var cell2 = row.insertCell(1);
                            var cell3 = row.insertCell(2);
                            var cell4 = row.insertCell(3);
                            var cellname = myColIndex + '日-'


                            cell1.innerHTML = '<label style="background-color:lavender">' + cellname + '</label><label  id ="JIKYU_' + myColIndex + intindex + '__HI">時間休</label>'

                            //alert('JIKYU_' + myColIndex + rowcount + '__HI')





                            cell2.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__USERID" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].USERID" type="hidden" value="' + myColIndex + '" ></input>' +
                                '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__HI" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].HI" type="hidden" value="' + myColIndex + '" ></input>' +
                                '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__KYUKCD" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].KYUKCD" type="hidden" value="' + intRadioValue + '" ></input>' +
                                '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNST" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNST"  class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                            cell3.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNED" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNED" class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                            cell4.innerHTML = '<input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>';

                            rowscnt++;

                            $('.timecustom').timepicker({
                                showMeridian: false,
                                maxHours: 25,
                                minuteStep: 5,
                                defaultTime: false
                            });

                            $(document).on('focus', 'input', function () {
                                $('.timecustom').not(this).timepicker('hideWidget');
                            });
                        }

                    }

                }






            }



            var rowscnt2 = $('#sdtTable2 tr').length;

            //alert(rowscnt)
            if (intRadioValue == '9') {

                var table = document.getElementById("sdtTable2");
                var rows = table.getElementsByTagName("tr");

                //var rowcount = rows.length - 1;

                var JikyuElement = document.getElementById("KYOKYU_" + myColIndex + "0__HI");

                if (JikyuElement == null) {

                    //$("tr").each(function () {
                    //    $(this).find("input.form-control").hide;
                    //    $(this).find("input.form-control").hide;
                    //});

                    $('#sdtTable2 tr').each(function () {
                        //alert('reach')
                        $(this).hide();
                    });

                    var i = 0

                    for (rowcount = rowscnt2; i < 5; rowcount++) {
                        //alert(rowcount)
                        var row = table.insertRow();
                        var cell1 = row.insertCell(0);
                        var cell2 = row.insertCell(1);
                        var cell3 = row.insertCell(2);
                        var cell4 = row.insertCell(3);
                        var cellname = myColIndex + '日-'

                        cell1.innerHTML = '<label style="background-color:lavender">' + cellname + '</label><label  id ="KYOKYU_' + myColIndex + i + '__HI">時強休</label>'



                        i++;

                        cell2.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__HI" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].HI" type="hidden" value="' + myColIndex + '" ></input>' +
                            '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__KYUKCD" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].KYUKCD" type="hidden" value="' + intRadioValue + '" ></input>' +
                            '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNST" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNST"  class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                        cell3.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNED" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNED" class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                        cell4.innerHTML = '<input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>';

                        $('.timecustom').timepicker({
                            showMeridian: false,
                            maxHours: 25,
                            minuteStep: 5,
                            defaultTime: false
                        });

                        $(document).on('focus', 'input', function () {
                            $('.timecustom').not(this).timepicker('hideWidget');
                        });

                    }




                }
                else {

                    $('#sdtTable2 tr').each(function () {
                        //alert('reach')
                        $(this).hide();
                    });

                    //alert(rowscnt)
                    var i = 0
                    for (intindex = 0; intindex < 5; intindex++) {

                        var colName = "KYOKYU_" + myColIndex + intindex + "__HI"
                        var lbl = document.getElementById(colName)
                        $(lbl).closest("tr").show();

                        if (lbl != null) {




                            document.getElementById(colName).innerHTML = '時強休';


                        }


                        //alert(colName)


                        if (lbl == null) {
                            rowcount = rowscnt2
                            //alert(rowscnt)
                            var row = table.insertRow();
                            var cell1 = row.insertCell(0);
                            var cell2 = row.insertCell(1);
                            var cell3 = row.insertCell(2);
                            var cell4 = row.insertCell(3);
                            var cellname = myColIndex + '日-'


                            cell1.innerHTML = '<label style="background-color:lavender">' + cellname + '</label><label  id ="KYOKYU_' + myColIndex + intindex + '__HI">時強休</label>'




                            cell2.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__USERID" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].USERID" type="hidden" value="' + myColIndex + '" ></input>' +
                                '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__HI" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].HI" type="hidden" value="' + myColIndex + '" ></input>' +
                                '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__KYUKCD" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].KYUKCD" type="hidden" value="' + intRadioValue + '" ></input>' +
                                '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNST" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNST"  class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                            cell3.innerHTML = '<input id ="lstWD0040[' + intHiddenIndex + '].D0040_' + rowcount + '__JKNED" name ="lstWD0040[' + intHiddenIndex + '].D0040[' + rowcount + '].JKNED" class = "form-control input-sm timecustom imedisabled" style="width:100px"></input>'

                            cell4.innerHTML = '<input type="button" id="del_btn_jikan" class="btn btn-default btn-xs" value="X"></input>';

                            rowscnt2++;

                            $('.timecustom').timepicker({
                                showMeridian: false,
                                maxHours: 25,
                                minuteStep: 5,
                                defaultTime: false
                            });

                            $(document).on('focus', 'input', function () {
                                $('.timecustom').not(this).timepicker('hideWidget');
                            });
                        }

                    }

                }






            }



        }



    });


    $('#showdate').datepicker({
        format: "yyyy/mm",
        language: "ja",
        autoclose: true,
        minViewMode: 'months'
    });



    function SetDate(months) {

        var curdates = $('#showdate').val().split('/');
        var newdate = new Date(curdates[0], curdates[1] - 1, '01');

        newdate.setMonth(newdate.getMonth() + months);

        var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2);

        $('#showdate').val(formattedNewDate);
    }



    $("#sdtTable").on('click', '#del_btn_jikan', function () {


        var table = document.getElementById("sdtTable");
        var rows = table.getElementsByTagName("tr");
        if (rows.length != 1) {
            var row = $(this).closest('tr');

            row.find('.form-control').val('')
            row.find('.form-control').removeAttr('readonly');

        }


    });

    $("#sdtTable2").on('click', '#del_btn_jikan', function () {


        var table = document.getElementById("sdtTable");
        var rows = table.getElementsByTagName("tr");
        if (rows.length != 1) {
            var row = $(this).closest('tr');

            row.find('.form-control').val('')
            row.find('.form-control').removeAttr('readonly');

        }


    });


    $(function () {
        $('#btnUpd').click(function () {

            var lblstatus = document.getElementById("status");

            if (lblstatus.value == "1") {
                alert('公休展開されていないため、休日設定できません。')
                return false
            }

            var result = confirm("休日設定を更新します。よろしいですか?")

            if (result == false) {
                return false
            }

            //時間テーブルの行を非表に示する
            $('#sdtTable tr').each(function () {
                //alert('reach')
                $(this).hide();
            });



            $('#sdtTable2 tr').each(function () {
                //alert('reach')
                $(this).hide();
            });

        });
    });



    $('.timecustom').timepicker({
        showMeridian: false,
        maxHours: 25,
        minuteStep: 5,
        defaultTime: false
    });

    $(document).on('focus', 'input', function () {
        $('.timecustom').not(this).timepicker('hideWidget');
    });


    function KeyUpFunction() {

        var searchdt = $('#showdate').val()
        var viewdate = $('#viewdatadate').val()

        if (searchdt != "") {

            if (searchdt.length == 7) {
                if (searchdt != viewdate) {

                    $("#myForm").submit();
                }

            }
        }

    }


    //画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
    $('input[type=radio][name=Colradio]').change(function () {
        var inputVal = $(this).val();
        if (inputVal != '') {
            $("#myForm").data("MSG", true);
        }
        else {
            $("#myForm").data("MSG", false);
        }
    });



</script>



