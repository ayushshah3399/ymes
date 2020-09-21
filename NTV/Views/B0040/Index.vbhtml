@ModelType IEnumerable(Of NTV_SHIFT.WD0050)
@Code
    ViewData("Title") = "休日表"
    Dim UserList = DirectCast(ViewBag.UserList, List(Of M0010))
    'Dim item.WD0060 = DirectCast(ViewBag.item.WD0060, List(Of WD0060))
End Code

<style>
    .table-scroll td {
        border: 1px solid darkgray;
        /*color: blue;*/
        text-align: center;
       
    }

    .table-scroll th {
        border: 1px solid darkgray;        
        text-align: center;
        border-collapse :separate;
       
    }


    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
         border-collapse :separate;
         
    }



    table.table-scroll tbody {
        height: 350px;
        width: 780px;
        overflow-y: auto;
        overflow-x: hidden;
    }


       .table-scroll th {
        width: 21px;
        border-collapse:separate;
    }


        .table-scroll th.th_head {
            width: 101px;
            text-align: right;
             border-collapse :separate;
        }

          
        

    .table-scroll td.td_body {
        width: 21px;
         border-collapse:separate;
    }

        .table-scroll td.td_head {
            width: 101px;
            text-align: left;
             border-collapse :separate;
        }

      

    .td_body {
        position:relative;
       
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
        top: 0px;
        left:-70px;
        width: 110px;
        height: auto;
        text-align: left;
    }


    .td_body:hover span.CellComment {
        display: block;
    }

   
</style>





<div class="container-fluid">
    @Using Html.BeginForm("Index", "B0040", routeValues:=Nothing, method:=FormMethod.Get, htmlAttributes:=New With {.id = "B0040Index"})
        @<div class="row" style="padding-top:10px;">

            <div class="col-md-6 col-md-push-6" style="background-color:white;">
                <ul class="nav nav-pills navbar-right">
                    <li><a href="javascript:PrintDiv();">印刷</a></li>
                    <li><a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>                  
                    <li>@Html.ActionLink("戻る", "Index", "C0050")</li>
                </ul>
            </div>
            <div class="col-md-6 col-md-pull-6">
                @*<h4>休日表</h4>*@
            </div>

        </div>


@Html.Hidden("viewdatadate", ViewData("searchdt"))

        @<p>
            <div class="row">

                <div class="col-sm-2">
                     @If ViewData("KOKYUTENKAIALL") = True Then
                        @Html.ActionLink("公休展開（全員）", "Create", "WB0050", routeValues:=New With {.searchdt = ViewData("searchdt")}, htmlAttributes:=New With {.class = "btn btn-success btn-xs"})
                    End If
                     <br /><br />
                    黒：未設定<br />
                    <font color=" blue">青</font>：設定済
                </div>

                <div class="col-sm-4">

                    <ul class="nav nav-pills ">

                        @*<li><a href="#" onclick="SetDate(-1)">前月</a></li>*@
                        <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(-1)">前月</button></li>
                        <li>
                            <div class="input-group">
                                <input type="text" id="showdate" name="showdate" class="form-control input-sm " value=@viewdata("searchdt").ToString onchange="KeyUpFunction()" style="width:80px;font-size:small;">

                            </div>
                        </li>

                        <li>@*<a href="#" onclick="SetDate(1)">翌月</a>*@</li>
                        <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(1)">翌月</button></li>
                        <li> <button id="hyoji" type="submit" class="btn btn-success btn-sm">表示</button></li>
                    </ul>
                </div>

                <div class="col-sm-6">
                    @Html.Partial("_ColorView", ViewData.Item("ColorList"))
                </div>

            </div>
        </p>

    End Using



    <div class="row">
        <div class="col-sm-2">

           
            <font color="blue"> @Html.Partial("_UserListParital", ViewData.Item("List"))</font>

            @*<div style="padding-top:50px;">
                @If ViewData("KOKYUTENKAI") = True Then
                    @Html.ActionLink("公休展開（個人）", "Create", "WB0050", routeValues:=New With {.searchdt = ViewData("searchdt"), .userid = ViewData("id")}, htmlAttributes:=New With {.class = "btn btn-success btn-xs"})
                End If
            </div>*@
        </div>

        <div class="col-sm-10" id="div_print" >
            @*@Html.Partial("_CalendarView", ViewData.Item("List"))*@
            <table class="table-scroll" id="myTableData">
                <thead>
                    <tr id="firstrow">
                        <th class="th_head" style="width:101px; text-align:right">
                            日付
                        </th>
                        @For i = 1 To 31
                            If i = 15 Then
                            @<td id=@i style="border-right-width :4px;"></td>
                            Else
                            @<td id=@i></td>
                            End If

                        Next
                    </tr>
                    <tr id="secondrow">
                        <th class="th_head" style="text-align:right">
                            曜日
                        </th>
                        @For i = 1 To 31
                            If i = 15 Then
                            @<th id="@(i)yobi" style="border-right-width :4px;"></th>
                            Else
                            @<th id="@(i)yobi"></th>
                            End If

                        Next
                    </tr>
                </thead>
                <tbody>
                    @For Each item In Model
                        Dim strRowName As String = "datarow"
                        Dim strDisplay As String = ""
                        @<tr id=@strRowName>
                            <td class="td_head" style="color:#@item.TENKAIFONTCOLOR;">
                                @Html.DisplayFor(Function(modelItem) item.USERNM)
                            </td>

                            <td class="td_body" style="background-color:#@item.BACKCOLORHI1;  border: 1px solid #@item.WAKUCOLORHI1; color:#@item.FONTCOLORHI1;">
                                @Html.DisplayFor(Function(modelItem) item.HI1)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "1" AndAlso itemTime.M0060.KYUKRYKNM = item.HI1 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If
                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If

                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI2;  border: 1px solid #@item.WAKUCOLORHI2; color:#@item.FONTCOLORHI2;">
                                @Html.DisplayFor(Function(modelItem) item.HI2)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "2" AndAlso itemTime.M0060.KYUKRYKNM = item.HI2 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI3;  border: 1px solid #@item.WAKUCOLORHI3; color:#@item.FONTCOLORHI3;">
                                @Html.DisplayFor(Function(modelItem) item.HI3)
                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If

                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "3" AndAlso itemTime.M0060.KYUKRYKNM = item.HI3 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI4;  border: 1px solid #@item.WAKUCOLORHI4; color:#@item.FONTCOLORHI4;">
                                @Html.DisplayFor(Function(modelItem) item.HI4)
                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "4" AndAlso itemTime.M0060.KYUKRYKNM = item.HI4 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI5;  border: 1px solid #@item.WAKUCOLORHI5; color:#@item.FONTCOLORHI5;">
                                @Html.DisplayFor(Function(modelItem) item.HI5)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "5" AndAlso itemTime.M0060.KYUKRYKNM = item.HI5 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI6;  border: 1px solid #@item.WAKUCOLORHI6; color:#@item.FONTCOLORHI6;">
                                @Html.DisplayFor(Function(modelItem) item.HI6)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "6" AndAlso itemTime.M0060.KYUKRYKNM = item.HI6 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI7;  border: 1px solid #@item.WAKUCOLORHI7; color:#@item.FONTCOLORHI7;">
                                @Html.DisplayFor(Function(modelItem) item.HI7)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "7" AndAlso itemTime.M0060.KYUKRYKNM = item.HI7 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI8;  border: 1px solid #@item.WAKUCOLORHI8; color:#@item.FONTCOLORHI8;">

                                @Html.DisplayFor(Function(modelItem) item.HI8)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "8" AndAlso itemTime.M0060.KYUKRYKNM = item.HI8 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI9;  border: 1px solid #@item.WAKUCOLORHI9; color:#@item.FONTCOLORHI9;">
                                @Html.DisplayFor(Function(modelItem) item.HI9)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "9" AndAlso itemTime.M0060.KYUKRYKNM = item.HI9 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI10;  border: 1px solid #@item.WAKUCOLORHI10; color:#@item.FONTCOLORHI10;">
                                @Html.DisplayFor(Function(modelItem) item.HI10)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "10" AndAlso itemTime.M0060.KYUKRYKNM = item.HI10 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI11;  border: 1px solid #@item.WAKUCOLORHI11; color:#@item.FONTCOLORHI11;">
                                @Html.DisplayFor(Function(modelItem) item.HI11)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "11" AndAlso itemTime.M0060.KYUKRYKNM = item.HI11 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI12;  border: 1px solid #@item.WAKUCOLORHI12; color:#@item.FONTCOLORHI12;">
                                @Html.DisplayFor(Function(modelItem) item.HI12)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If

                                @For Each itemTime In item.WD0060
                                If itemTime.HI = "12" AndAlso itemTime.M0060.KYUKRYKNM = item.HI12 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI13;  border: 1px solid #@item.WAKUCOLORHI13; color:#@item.FONTCOLORHI13;">
                                @Html.DisplayFor(Function(modelItem) item.HI13)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "13" AndAlso itemTime.M0060.KYUKRYKNM = item.HI13 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI14;  border: 1px solid #@item.WAKUCOLORHI14; color:#@item.FONTCOLORHI14;">
                                @Html.DisplayFor(Function(modelItem) item.HI14)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "14" AndAlso itemTime.M0060.KYUKRYKNM = item.HI14 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                        End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI15;  border: 1px solid #@item.WAKUCOLORHI15; color:#@item.FONTCOLORHI15; border-right-width :4px; width:22px">
                                @Html.DisplayFor(Function(modelItem) item.HI15)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "15" AndAlso itemTime.M0060.KYUKRYKNM = item.HI15 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI16;  border: 1px solid #@item.WAKUCOLORHI16; color:#@item.FONTCOLORHI16;">
                                @Html.DisplayFor(Function(modelItem) item.HI16)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "16" AndAlso itemTime.M0060.KYUKRYKNM = item.HI16 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI17;  border: 1px solid #@item.WAKUCOLORHI17; color:#@item.FONTCOLORHI17;">
                                @Html.DisplayFor(Function(modelItem) item.HI17)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "17" AndAlso itemTime.M0060.KYUKRYKNM = item.HI17 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI18;  border: 1px solid #@item.WAKUCOLORHI18; color:#@item.FONTCOLORHI18;">
                                @Html.DisplayFor(Function(modelItem) item.HI18)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "18" AndAlso itemTime.M0060.KYUKRYKNM = item.HI18 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI19;  border: 1px solid #@item.WAKUCOLORHI19; color:#@item.FONTCOLORHI19;">
                                @Html.DisplayFor(Function(modelItem) item.HI19)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "19" AndAlso itemTime.M0060.KYUKRYKNM = item.HI19 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI20;  border: 1px solid #@item.WAKUCOLORHI20; color:#@item.FONTCOLORHI20;">
                                @Html.DisplayFor(Function(modelItem) item.HI20)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "20" AndAlso itemTime.M0060.KYUKRYKNM = item.HI20 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI21;  border: 1px solid #@item.WAKUCOLORHI21; color:#@item.FONTCOLORHI21;">
                                @Html.DisplayFor(Function(modelItem) item.HI21)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "21" AndAlso itemTime.M0060.KYUKRYKNM = item.HI21 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI22;  border: 1px solid #@item.WAKUCOLORHI22; color:#@item.FONTCOLORHI22;">
                                @Html.DisplayFor(Function(modelItem) item.HI22)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "22" AndAlso itemTime.M0060.KYUKRYKNM = item.HI22 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI23;  border: 1px solid #@item.WAKUCOLORHI23; color:#@item.FONTCOLORHI23;">
                                @Html.DisplayFor(Function(modelItem) item.HI23)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "23" AndAlso itemTime.M0060.KYUKRYKNM = item.HI23 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI24;  border: 1px solid #@item.WAKUCOLORHI24; color:#@item.FONTCOLORHI24;">
                                @Html.DisplayFor(Function(modelItem) item.HI24)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "24" AndAlso itemTime.M0060.KYUKRYKNM = item.HI24 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI25;  border: 1px solid #@item.WAKUCOLORHI25; color:#@item.FONTCOLORHI25;">
                                @Html.DisplayFor(Function(modelItem) item.HI25)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "25" AndAlso itemTime.M0060.KYUKRYKNM = item.HI25 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI26;  border: 1px solid #@item.WAKUCOLORHI26; color:#@item.FONTCOLORHI26;">
                                @Html.DisplayFor(Function(modelItem) item.HI26)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "26" AndAlso itemTime.M0060.KYUKRYKNM = item.HI26 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI27;  border: 1px solid #@item.WAKUCOLORHI27; color:#@item.FONTCOLORHI27;">
                                @Html.DisplayFor(Function(modelItem) item.HI27)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "27" AndAlso itemTime.M0060.KYUKRYKNM = item.HI27 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI28;  border: 1px solid #@item.WAKUCOLORHI28; color:#@item.FONTCOLORHI28;">
                                @Html.DisplayFor(Function(modelItem) item.HI28)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "28" AndAlso itemTime.M0060.KYUKRYKNM = item.HI28 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI29;  border: 1px solid #@item.WAKUCOLORHI29; color:#@item.FONTCOLORHI29;">
                                @Html.DisplayFor(Function(modelItem) item.HI29)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "29" AndAlso itemTime.M0060.KYUKRYKNM = item.HI29 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI30;  border: 1px solid #@item.WAKUCOLORHI30; color:#@item.FONTCOLORHI30;">
                                @Html.DisplayFor(Function(modelItem) item.HI30)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "30" AndAlso itemTime.M0060.KYUKRYKNM = item.HI30 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>
                            <td class="td_body" style="background-color:#@item.BACKCOLORHI31;  border: 1px solid #@item.WAKUCOLORHI31; color:#@item.FONTCOLORHI31;">
                                @Html.DisplayFor(Function(modelItem) item.HI31)

                                @If strDisplay <> "" Then
                                strDisplay = ""
                                End If
                                @For Each itemTime In item.WD0060
                                If itemTime.USERID = item.USERID AndAlso itemTime.HI = "31" AndAlso itemTime.M0060.KYUKRYKNM = item.HI31 Then
                                strDisplay = strDisplay & vbCrLf & itemTime.JKNST.ToString.Substring(0, 2) & ":" & itemTime.JKNST.ToString.Substring(2, 2) & "～" & itemTime.JKNED.ToString.Substring(0, 2) & ":" & itemTime.JKNED.ToString.Substring(2, 2)
                                End If

                                Next
                                @If strDisplay <> "" Then
                                    @<span class="CellComment" id="spanClass">@strDisplay</span>
                                End If
                            </td>


                        </tr>


                    Next
                </tbody>
            </table>
        </div>

    </div>



</div>



<script src="js/bootstrap-datepicker.min.js"></script>
<script src="js/bootstrap-datepicker.ja.js"></script>
<script>
    $('#date').datepicker({
        format: "yyyy/mm",
        language: "ja",
        autoclose: true,
        minViewMode: 'months'
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

        var formattedNewDate1 = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2) + '/' + ('0' + newdate.getDate()).slice(-2);

        var d = new Date(newdate.getFullYear(), (newdate.getMonth() + 1), 0).getDate();
        //alert(d)

        var day_of_week = new Array('日', '月', '火', '水', '木', '金', '土');
        week_day = newdate.getDay();
        var table = document.getElementById("myTableData");

       
        var row = document.getElementById("firstrow");

        var row2 = document.getElementById("secondrow");
        var int = 0
       
        var needcol = 31 - d
        var startcol;
        for (index = 1; index <= d; index++) {
                                  
            var cell = document.getElementById(index)
            cell.innerHTML = index;
            var dateObj = new Date(newdate.getFullYear(), newdate.getMonth(), index)
            var day = dateObj.getDay()
                       
            var cell2 = document.getElementById(index + 'yobi')
            cell2.innerHTML = day_of_week[day];
            cell.style.display = "table-cell";
            cell2.style.display = "table-cell";


        }
        var table = document.getElementById('myTableData'),
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
               
                cell.style.display = "none";
                cell2.style.display = "none";

            }

            var table = document.getElementById('myTableData'),
                     rows = table.getElementsByTagName('tr');


            for (i = 2, j = rows.length; i < j; ++i) {
                cells = rows[i].getElementsByTagName('td');

                for (index = 1; index <= needcol; index++) {
                    startcol = d + index
                    cells[startcol].style.display = "none";

                }
            }
        }

      

        var table = document.getElementById('myTableData'),
        rows = table.getElementsByTagName('tr');

      
        for (i = 2, j = rows.length; i < j; ++i) {
            cells = rows[i].getElementsByTagName('td');

            for (index = 1; index <= 31; index++) {
                cells[index].innerHTML = '<td class="td_body" style="width: 21px; border-collapse:separate;"></td>'
                cells[index].style.backgroundColor = "white";
                            
                cells[index].style.borderColor = "darkgray";
              
            }
        }


    }



    $(document).ready(function () {
        var showdate = $('#showdate').val().split('/');
        var curdates = new Date(showdate[0], showdate[1] - 1, '01');

        if (curdates == '') {
            curdates = new Date();
        }


        var d = new Date(curdates.getFullYear(), (curdates.getMonth() + 1), 0).getDate();
        //alert(d)

        var day_of_week = new Array('日', '月', '火', '水', '木', '金', '土');
        week_day = curdates.getDay();
        var table = document.getElementById("myTableData");
           

        var row = document.getElementById("firstrow");
        var row2 = document.getElementById("secondrow");
        

        var needcol = 31 - d
        var startcol;
     
        for (index = 1; index <= d; index++) {

            var cell = document.getElementById(index)
           
            cell.innerHTML = index;

            var dateObj = new Date(curdates.getFullYear(), curdates.getMonth(), index)
            var day = dateObj.getDay()
                       
            var cell2 = document.getElementById(index + 'yobi')
            cell2.innerHTML = day_of_week[day];
            cell.style.display = "table-cell";
            cell2.style.display = "table-cell";
            
        }

        var table = document.getElementById('myTableData'),
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
            var table = document.getElementById('myTableData'),
             rows = table.getElementsByTagName('tr');


            for (i = 2, j = rows.length; i < j; ++i) {
                cells = rows[i].getElementsByTagName('td');

                for (index = 1; index <= needcol; index++) {
                    startcol = d + index
                    cells[startcol].style.display = "none";
               
                }
            }
        }
       

        var table = document.getElementById('myTableData'),
            rows = table.getElementsByTagName('tr');
        for (i = 2, j = rows.length; i < j; ++i) {
            cells = rows[i].getElementsByTagName('td');

            for (index = 1; index <= needcol; index++) {
                startcol = d + index
                cells[startcol].innerHTML = '<td class="td_body" style="width: 21px; border-collapse:separate;"></td>'
                cells[startcol].style.backgroundColor = "white";

                //cells[index].removeAttr('style');
                cells[startcol].style.borderColor = "darkgray";
                //cells[index].style.borderStyle = "";
            }
        }



    });


    $('#add_btn_ana').click(function (event) {
        var table = document.getElementById("anaTable");
        var rows = table.getElementsByTagName("tr");

        var $table = $('#anaTable');
        var $nrow = $table.find('tr:eq(0)').clone();

        var $select = $nrow.find('td:first').find('select');
        $select.val(0);
        $select.attr("name", "D0130[" + rows.length + "].USERID");
        $select.attr("id", "D0130_" + rows.length + "__USERID");

        $table.append($nrow);
    });

       

    function PrintDiv() {
       
        var headstr = "<html><head><title></title></head><body>";
        var footstr = "</body>";
        //var newstr = document.all.item(printpage).innerHTML;
        var oldstr = document.body.innerHTML;

        $(".CellComment").hide();

        var myTable = document.getElementById('myTableData');

        myTable.style.border = "1px solid darkgray";

        var allTableCells = document.getElementsByTagName("td");

        for (var i = 0, max = allTableCells.length; i < max; i++) {
            var node = allTableCells[i];
           
            node.style.border = "1px solid darkgray";
                                   
        }


        var table = document.getElementById('myTableData'),
             rows = table.getElementsByTagName('tr');
        cellheadder = rows[0].getElementsByTagName('td');
        cellheadder[14].style.borderRightWidth = "4px";
        for (i = 2, j = rows.length; i < j; ++i) {
            cells = rows[i].getElementsByTagName('td');

            for (index = 1; index <= 31; index++) {
                if (index == 15) {
                    cells[index].style.borderRightWidth = "4px";
                }


            }

        }


        var allTableHeaders = document.getElementsByTagName("th");

        for (var i = 0, max = allTableHeaders.length; i < max; i++) {
            var nodeheader = allTableHeaders[i];

            nodeheader.style.border = "1px solid darkgray";

            if (i == 16) {
                nodeheader.style.borderRightWidth = "4px";
            }
        }

              

        var divContents = document.getElementById("div_print").innerHTML;
        //document.body.innerHTML = headstr+newstr+footstr;
        document.body.innerHTML = headstr + divContents + footstr;
        window.print();
        document.body.innerHTML = oldstr;

        afterPrint();
        
    }

    function afterPrint() {

        setTimeout(function () { document.location.href = window.location.href; }, 250);
    }

    function KeyUpFunction() {

        var searchdt = $('#showdate').val()
        var viewdate = $('#viewdatadate').val()
              
        if (searchdt != "") {

            if (searchdt.length == 7) {
                if (searchdt != viewdate) {                 
                    $("#B0040Index").submit();
                }

            }
        }

    }
</script>

