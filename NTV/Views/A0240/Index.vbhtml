@*@ModelType NTV_SHIFT.D0010*@
@ModelType IEnumerable(Of NTV_SHIFT.D0010)

@Code
    Dim Countervar As Integer = 0
    Dim SubCatCnt As Integer = -1
    Dim strKey As String = ""
    ViewData("Title") = "種目別スポーツシフト表"


    Dim ana_TempAnaList = DirectCast(ViewBag.ana_TempAnaList, List(Of M0010))
    Dim sportCatList2 = DirectCast(ViewBag.sportCatList2, List(Of M0140))
    Dim tbodyData = DirectCast(ViewBag.tbodyData, Dictionary(Of ICollection, Integer))

    Dim SearchDate As Date = Date.ParseExact(ViewData("searchdt").ToString(), "yyyy/MM", Nothing)
    Dim days As Integer = Date.DaysInMonth(SearchDate.Year, SearchDate.Month)

    Dim searchType = DirectCast(ViewBag.SearchType, String)
    Dim ShiftTableRadioType = DirectCast(ViewBag.ShiftTableRadioType, String)
    Dim lastForm As String = ViewBag.lastForm

    Dim NoOfFreezeCol = ViewBag.NoOfFreezeCol
    Dim leftHeaderColFreezCounter As Integer = 0
    Dim leftDataColFreezCounter As Integer = 0
    Dim rightHeaderColFreezCounter As Integer = 0
    Dim rightDataColFreezCounter As Integer = 0
    Dim saveSuccess = 0
    If TempData("saveSuccess") IsNot Nothing Then
        saveSuccess = TempData("saveSuccess")
    End If

    Dim leftHeaderColFreezCounterRow1 As Integer = 0
    Dim rightHeaderColFreezCounterRow1 As Integer = 0
    Dim subcategoryDrawn As Boolean = False
    Dim cnt01Left = 1
    Dim cnt02Left = 1
    Dim prevCatCdLeft = 0
    Dim cnt01Right = 1
    Dim cnt02Right = 1
    Dim prevCatCdRight = 0

    Dim leftHeaderColFreezCounterColGroup As Integer = 0
    Dim rightHeaderColFreezCounterColgroup As Integer = 0

    Dim KARIANA_LINK_COLOR As String = "#1e90ff"

End Code

@*ASI[29 Nov 2019]: If No SPORTCATCD available in Master table*@
@If ViewBag.SelectedSportCatCd = 0 Then
    @<script language="javascript">
         alert("スポーツカテゴリはありません。");
    </script>
End If

<style>

    .table > thead > tr > th,
    .table > tbody > tr > th,
    .table > thead > tr > td,
    .table > tbody > tr > td {
        padding-left: 2px;
        padding-right: 2px;
        padding-top: 2px;
        padding-bottom: 2px;
    }

    table a:not(.btn), .table a:not(.btn) {
        text-decoration: none;
    }

    table a:hover, .table a:hover {
        text-decoration: underline;
    }

    body {
        font-size: 12px;
    }

    .myDiv {
        max-width: 1765px;
        max-height: 670px;
        overflow-y: scroll;
        overflow-x: scroll;
        position: relative;
        padding-left: 0px;
    }

    .myHeaderDiv {
        max-width: 1765px;
        overflow-x: hidden;
        overflow-y: scroll;
        z-index: 1;
        padding-left: 0px;
    }

    #tbl-header.table {
        position: relative;
        vertical-align: middle;
    }

    .my-left-HeaderDiv {
        max-width: 100px;
        width: 85px;
        overflow: hidden;
        z-index: 1;
    }

    .my-left-dataDiv {
        max-width: 100px;
        width: 85px;
        max-height: 670px;
        overflow-x: scroll;
        overflow-y: hidden;
        position: relative;
    }

    th, td {
        vertical-align: middle;
        word-wrap: break-word;
    }

    #left-fixed-header th {
        vertical-align: middle;
    }

    #left-fixed-data th {
        vertical-align: middle;
    }

    #tbl-header th {
    }

    .table > tbody > tr > td {
        vertical-align: middle;
        text-align: center;
    }

    .table > thead > tr > th {
        vertical-align: middle;
    }

    .col-1 {
        width: 50px;
    }

    /*.col-2 for ! sign btn in personalshift data, when deskmemo exist*/
    .col-2 {
        width: 42px;
        max-width: 42px;
    }
    /*.col-12 for ANA (4 Kanji width)*/
    .col-12 {
        width: 60px;
        max-width: 60px;
    }
    /*.col-13 for FURI (6 Kanji width)*/
    .col-13 {
        width: 85px;
        max-width: 85px;
    }
    /*.col-14 for banguminame in personalshift data, when deskmemo exist*/
    .col-14 {
        width: 43px;
        max-width: 43px;
    }
    /*.col-20 for only banguminame in personalshift data, no deskmemo*/
    .col-20 {
        width: 85px;
        max-width: 85px;
    }

    body {
        font-size: 12px;
    }

    .anaTHHide {
        display: none;
    }

    .dateHeaderWidth {
        width: 55px;
        max-width: 55px;
    }

    .dayHeaderWidth {
        width: 30px;
        max-width: 30px;
    }
</style>

<div class="col-md-12">
    <div class="row">
        <div class="col-md-6" style="width :400px">
            <h3 style="margin-bottom:0px;">種目別スポーツシフト表</h3>
        </div>
        <div class="col-md-6" style="background-color:white;font-size:14px;margin-top:25px; width :400px;">
            <label style="font-size:15px;text-align:left; color:orange; width :400px" id="lblInfo" hidden="hidden">処理中です。しばらくお待ち下さい。。。</label>
        </div>

        <div class="col-md-6" style="background-color:white;font-size:14px;margin-top:15px; width :auto ;position:absolute; right:0px;">
            <ul class="nav nav-pills navbar-right" style="padding-right:15px">
                <!--Havan[20 Dec 2019]: Hide PrintBtn for 2nd Release. after release uncomment this line and start implementation.-->
                <!--<li><a href="javascript:PrintDiv();">印刷</a></li>-->
                <li><a href="#" onclick="document.getElementById('lblInfo').hidden = false;document.forms[0].submit();">最新情報</a></li>
                @If Session("UrlReferrer") IsNot Nothing Then
                    @<li><a href="@Session("UrlReferrer")">戻る</a></li>
End If

            </ul>

        </div>
    </div>

    <div Class="container-fluid" style="padding-left: 0px;">
        <div Class="row" style="display: inline-flex;">
            @Using Html.BeginForm("Index", "A0240", FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})
                @Html.Hidden("SearchType", "0")
                @Html.Hidden("ShiftTableRadioType", "0")
                @<div class="col-md-12" style="padding-left:0px;">
                    <ul class="nav nav-pills">
                        <li style="padding-right:20px; padding-bottom:5px;">
                            <div class="col-md-12" style="width: 180px;">
                                @Html.DropDownList("SPORTCATCD", New SelectList(ViewBag.SPORTCATCD_LIST, "SPORTCATCD", "SPORTCATNM", ViewBag.SelectedSportCatCd), htmlAttributes:=New With {.class = "form-control input-sm", .id = "SPORTCATCD"})
                            </div>
                        </li>
                        <li style="padding-right:30px;padding-bottom:5px;">
                            <ul class="nav nav-pills ">
                                <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(-1)">前月</button></li>
                                <li>
                                    <div class="input-group">
                                        <input id="Searchdt" name="Searchdt" type="text" style="width:80px" class="form-control input-sm date imedisabled" value=@Html.Encode(ViewData("searchdt")) onchange="KeyUpFunction()" style="width:120px;font-size:small;">
                                    </div>
                                </li>
                                <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(1)">翌月</button></li>
                                <li><button id="searchBtn" type="submit" class="btn btn-success btn-sm">表示</button></li>
                                <li>
                                    <div class="col-md-12" style="padding-top:10px;">
                                        <label class="control-label" style="width:90px;text-align :right;padding-right :25px ">項目</label>
                                        @if searchType = "0" Then
                                            @<Label Class="radio-inline">
                                                @Html.RadioButton("SearchAll", "False", True) 全表示
                                            </Label>
Else
                                            @<Label Class="radio-inline">
                                                @Html.RadioButton("SearchAll", "False", False) 全表示
                                            </Label>
End If
                                        @if searchType = "1" Then
                                            @<label class="radio-inline">
                                                @Html.RadioButton("SearchFilter", "True", True)
                                                絞り込み表示
                                            </label>
Else
                                            @<label class="radio-inline">
                                                @Html.RadioButton("SearchFilter", "False", False)
                                                絞り込み表示
                                            </label>
End If
                                    </div>
                                </li>
                            </ul>
                        </li>
                        <li style="padding-right:20px; padding-bottom:5px;">
                            <button id="btnDownloadCsv" type="submit" class="btn btn-success btn-sm" name="button" value="downloadcsv">CSV出力</button>
                        </li>
                    </ul>
                </div>
                @Html.Hidden("viewdatadate", ViewBag.searchdt)
End Using

            @Using (Html.BeginForm("Index", "A0240", FormMethod.Post))
                @Html.Hidden("forUpdateOrInsert")
                @Html.Hidden("SearchType", "0")
                 @Html.Hidden("ShiftTableRadioType", "0")
                @Html.Hidden("SportCatCd")
                @Html.Hidden("Searchdt")
                @<div class="col-md-12" style="padding-left:0px;">
                    <ul class="nav nav-pills">
                        @If Session("LoginUserACCESSLVLCD") <> "4" Then
                            @If Session("LoginUserACCESSLVLCD") <> "3" OrElse (Session("LoginUserACCESSLVLCD") = "3" And ViewBag.CHIEF_CAT = 1) Then
                                @<li style="padding-bottom:5px;">
                                    <div class="col-md-12">
                                        @Html.Hidden("lastForm", lastForm, New With {.id = "lastForm"})
                                        <input id="btnUpdate" type="submit" value="更新" class="btn btn-default btn-sm" />
                                    </div>
                                </li>
                                @<li style="padding-right:10px;padding-bottom:5px;">
                                    <div class="col-md-12">
                                        <button id="btnCollectiveReg" type="submit" class="btn btn-success btn-sm" name="button" value="collectiveReg">一括本登録</button>
                                    </div>
                                </li>
                            End If
                        End If
                        @*<li style="padding-right:30px;padding-bottom:5px;">
                                <div class="col-md-12">
                                    <button id="btnDownloadCsv" type="submit" class="btn btn-success btn-sm" name="button" value="downloadcsv">CSV出力</button>
                                </div>
                            </li>*@
                        <li>
                            <div class="col-md-12" style="padding-top:10px;">
                                <label class="control-label" style="width:70px;">シフト表</label>
                                @if ShiftTableRadioType = "0" Then
                                    @<label Class="radio-inline">
		                                @Html.RadioButton("ShiftTblRadio", "True", True, New With {.id = "showRd"})
		                                表示&nbsp;
	                                </label>
Else
                                        @<label Class="radio-inline">
		                                @Html.RadioButton("ShiftTblRadio", "False", False, New With {.id = "showRd"})
		                                表示&nbsp;
	                                </label>
End If
                                @If ShiftTableRadioType = "1" Then
                                    @<label Class="radio-inline">
		                                @Html.RadioButton("ShiftTblRadio", "True", True, New With {.id = "hideRd"})
		                                非表示&nbsp;
	                                </label>
Else
                                     @<label Class="radio-inline">
		                                @Html.RadioButton("ShiftTblRadio", "False", False, New With {.id = "hideRd"})
		                                非表示&nbsp;
	                                </label>
End If
                            </div>
                        </li>
                    </ul>
                </div>
                @Html.Raw("</div>")
                @<div>
                    <div class="col-md-12" style="display:inline-flex; padding-left:0px;padding-right:0px">
                        <div>
                            <div class="my-left-HeaderDiv" style="padding-right:0px;padding-left:0px;">
                                <table id="left-fixed-header" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout: fixed;border-collapse:separate;">
                                    @*ASI[20 Jan 2020]: do create column of specific width, generate col before Th Td print*@
                                    @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                        Dim headerWidthCssClass As String = ""
                                        Dim dynamicColId As String = ""
                                        For Each item In sportCatList2
                                            For Each item2 In item.M0150LIST
                                                If leftHeaderColFreezCounterColGroup < NoOfFreezeCol Then
                                                    If item2.COLNAME = "DateHeader" Then
                                                        headerWidthCssClass = "dateHeaderWidth"
                                                    ElseIf item2.COLNAME = "DayHeader" Then
                                                        headerWidthCssClass = "dayHeaderWidth"
                                                    Else
                                                        If item2.COLTYPE = "2" Then
                                                            headerWidthCssClass = "col-12"
                                                        Else
                                                            headerWidthCssClass = "col-13"
                                                        End If
                                                    End If

                                                    If (item2.COLTYPE = "1" OrElse item2.COLTYPE = "2" OrElse item2.COLTYPE = "") And item2.HYOJ = False Then
                                                        dynamicColId = "Col_anaTHHY"
                                                    Else
                                                        dynamicColId = ""
                                                    End If
                                                    @<col id="@dynamicColId" class="@headerWidthCssClass"></col>
leftHeaderColFreezCounterColGroup = leftHeaderColFreezCounterColGroup + 1
                                                End If
                                            Next
                                        Next
                                    End If

                                    <tr id="MainLeftHeaderRowUpper">
                                        @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                            Dim partitionCSSStyle As String = ""
                                            Dim headerWidthCssClass As String = ""
                                            Dim thID = ""
                                            @For Each item In sportCatList2
                                                Dim Colid = ""
                                                If item.SPORTSUBCATCD <> 0 Then
                                                    thID = "R1_" & cnt01Left
                                                    Colid = thID
                                                    'cnt01Left = cnt01Left + 1
                                                End If
                                                @For Each item2 In item.M0150LIST
                                                    'Want to Display Or Not

                                                    Dim freezecolSubcatColspan = NoOfFreezeCol - 2
                                                    Dim freezecolSubcatWidth = freezecolSubcatColspan * 100 & "px"
                                                    @If leftHeaderColFreezCounterRow1 < NoOfFreezeCol Then
                                                        @If item2.COLTYPE = "4" And item2.COLNAME = "DateHeader" Then
                                                            partitionCSSStyle = "background-color:#F0FEEF;"
                                                            headerWidthCssClass = "dateHeaderWidth"
                                                        ElseIf item2.COLTYPE = "4" And item2.COLNAME = "DayHeader" Then
                                                            partitionCSSStyle = "background-color:#F0FEEF;"
                                                            headerWidthCssClass = "dayHeaderWidth"
                                                        Else
                                                            Colid = thID
                                                            partitionCSSStyle = ""
                                                            headerWidthCssClass = "col-13"
                                                        End If
                                                        If item2.COLNAME = "DateHeader" OrElse item2.COLNAME = "DayHeader" Then
                                                            @<th class="@headerWidthCssClass" id="@Colid" style="@partitionCSSStyle"></th>
Else
                                                            If subcategoryDrawn = False Then
                                                                partitionCSSStyle = "background-color:#F0FEEF;max-width:" & freezecolSubcatWidth & ";width:" & freezecolSubcatWidth
                                                                @<th id="@Colid" style="@partitionCSSStyle" colspan="@freezecolSubcatColspan">@item.SPORTSUBCATNM</th>
                                                                subcategoryDrawn = True
                                                                prevCatCdRight = item.SPORTSUBCATCD
                                                            End If
                                                        End If
                                                        leftHeaderColFreezCounterRow1 = leftHeaderColFreezCounterRow1 + 1
                                                    End If
                                                Next
                                            Next
                                        End If
                                    </tr>

                                    <tr id="MainLeftHeaderRow">
                                        @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                            Dim partitionCSSStyle As String = ""
                                            Dim headerWidthCssClass As String = ""
                                            'cnt01Left = 0
                                            Dim thID = ""
                                            @For Each item In sportCatList2
                                                Dim Colid = ""
                                                @For Each item2 In item.M0150LIST
                                                    'Want to Display Or Not

                                                    @If leftHeaderColFreezCounter < NoOfFreezeCol Then
                                                        If item.SPORTSUBCATCD <> 0 Then
                                                            If prevCatCdLeft <> item.SPORTSUBCATCD Then
                                                                'cnt01Left = cnt01Left + 1
                                                                cnt02Left = 1
                                                            End If

                                                            prevCatCdLeft = item.SPORTSUBCATCD
                                                            thID = "R2_" & cnt01Left & "_" & cnt02Left
                                                            Colid = thID

                                                            cnt02Left = cnt02Left + 1
                                                        End If
                                                        @If item2.COLTYPE = "4" And item2.COLNAME = "DateHeader" Then
                                                            partitionCSSStyle = "background-color:#F0FEEF;"
                                                            headerWidthCssClass = "dateHeaderWidth"
                                                        ElseIf item2.COLTYPE = "4" And item2.COLNAME = "DayHeader" Then
                                                            partitionCSSStyle = "background-color:#F0FEEF;"
                                                            headerWidthCssClass = "dayHeaderWidth"
                                                        Else
                                                            @If item2.COLTYPE = "2" Then
                                                                headerWidthCssClass = "col-12"
                                                            Else
                                                                headerWidthCssClass = "col-13"
                                                            End If
                                                            If item2.HYOJ = False Then
                                                                Colid = thID & "_" & "anaTHHY"
                                                            Else
                                                                Colid = thID
                                                            End If
                                                            partitionCSSStyle = ""
                                                        End If
                                                        @<th class="@headerWidthCssClass" id="@Colid" style="@partitionCSSStyle">@item2.COLVALUE</th>
leftHeaderColFreezCounter = leftHeaderColFreezCounter + 1
                                                    End If
                                                Next
                                            Next
                                        End If
                                    </tr>

                                </table>
                            </div>
                            <div class="my-left-dataDiv" style="padding-right:0px;padding-left:0px;">
                                <table id="left-fixed-data" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout: fixed;border-collapse:separate;">
                                    <tbody id="myTable1">
                                        <!--START: dynamic value display block-->
                                        @For Each dayItem In tbodyData
                                            Dim row As String = "ROW:"
                                            Countervar = Countervar + 1
                                            row = row & Countervar.ToString()
                                            Dim subCatColCnt As Integer = 0

                                            @<tr id=@row>
                                                @If dayItem.Key IsNot Nothing AndAlso dayItem.Key.Count > 0 Then
                                                    Dim rowSpan = dayItem.Value
                                                    Dim d0010Cnt = 0
                                                    Dim isRowSpan = 0
                                                    Dim isNewRow = 0
                                                    Dim Cnt = 1
                                                    leftDataColFreezCounter = 0
                                                    For i = 0 To dayItem.Key.Count - 1
                                                        Dim subCatItem As ICollection = dayItem.Key(i)
                                                        Dim d0010Item As ICollection = subCatItem(d0010Cnt)
                                                        Dim subcatRowSpan = 1
                                                        If i = 0 Then
                                                            isNewRow = 0
                                                        End If
                                                        If d0010Item IsNot Nothing AndAlso d0010Item.Count > 0 Then
                                                            If d0010Item(0).SPORTSUBCATCD <> 0 Then
                                                                SubCatCnt = SubCatCnt + 1
                                                            End If
                                                            Dim maxRow = subCatItem.Count
                                                            If maxRow > 1 Then
                                                                isRowSpan = 1
                                                            End If
                                                            If maxRow > 1 AndAlso d0010Cnt > 0 AndAlso Cnt < maxRow AndAlso isNewRow = 0 Then
                                                                @Html.Raw("<tr id=" & row & ">")
Cnt = Cnt + 1
                                                                isNewRow = 1
                                                            End If
                                                            If maxRow = Cnt Then
                                                                subcatRowSpan = rowSpan - maxRow + 1
                                                            Else
                                                                subcatRowSpan = 1
                                                            End If
                                                            subCatColCnt = 0
                                                            For Each colItem As A0230 In d0010Item
                                                                subCatColCnt = subCatColCnt + 1
                                                                Dim Colid = "R4_" & i - 1 & "_" & subCatColCnt
                                                                If leftDataColFreezCounter < NoOfFreezeCol Then
                                                                    If colItem.COL_TYPE <> 3 AndAlso colItem.SPORTSUBCATCD <> 0 Then
                                                                        strKey = String.Format("lstd0010[{0}].", SubCatCnt)
                                                                        @Html.Hidden(strKey & "GYOMNO", colItem.GYOMNO)
                                                                        @Html.Hidden(strKey & "RNZK", colItem.RNZK)
                                                                        @Html.Hidden(strKey & "PGYOMNO", colItem.PGYOMNO)
If colItem.COL_NAME = "KSKJKNST" Then
                                                                            If colItem.ITEMNM IsNot Nothing Then
                                                                                Dim values As String() = colItem.ITEMNM.Split("~")
                                                                                If values.Count = 0 Then
                                                                                    @Html.Hidden(strKey & "KSKJKNST", "")
                                                                                    @Html.Hidden(strKey & "KSKJKNED", "")
ElseIf values.Count = 1 Then
                                                                                    @Html.Hidden(strKey & "KSKJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "KSKJKNED", "")
ElseIf values.Count = 2 Then
                                                                                    @Html.Hidden(strKey & "KSKJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "KSKJKNED", values(1))
End If
                                                                            End If

                                                                        ElseIf colItem.COL_NAME = "OAJKNST" Then
                                                                            If colItem.ITEMNM IsNot Nothing Then
                                                                                Dim values As String() = colItem.ITEMNM.Split("~")
                                                                                If values.Count = 0 Then
                                                                                    @Html.Hidden(strKey & "OAJKNST", "")
                                                                                    @Html.Hidden(strKey & "OAJKNED", "")
ElseIf values.Count = 1 Then
                                                                                    @Html.Hidden(strKey & "OAJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "OAJKNED", "")
ElseIf values.Count = 2 Then
                                                                                    @Html.Hidden(strKey & "OAJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "OAJKNED", values(1))
End If
                                                                            End If

                                                                        ElseIf colItem.COL_NAME = "SAIJKNST" Then
                                                                            If colItem.ITEMNM IsNot Nothing Then
                                                                                Dim values As String() = colItem.ITEMNM.Split("~")
                                                                                If values.Count = 0 Then
                                                                                    @Html.Hidden(strKey & "SAIJKNST", "")
                                                                                    @Html.Hidden(strKey & "SAIJKNED", "")
ElseIf values.Count = 1 Then
                                                                                    @Html.Hidden(strKey & "SAIJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "SAIJKNED", "")
ElseIf values.Count = 2 Then
                                                                                    @Html.Hidden(strKey & "SAIJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "SAIJKNED", values(1))
End If
                                                                            End If

                                                                        Else
                                                                            If colItem.COL_TYPE = 2 Then
                                                                                @Html.Hidden(strKey & colItem.COL_NAME, colItem.ITEMCD)
Else
                                                                                @Html.Hidden(strKey & colItem.COL_NAME, colItem.ITEMNM)
End If
                                                                        End If
                                                                    End If

                                                                    If colItem.ITEMNM <> "" Then
                                                                        If colItem.COL_TYPE = 2 Then

                                                                            'Want to Display Or Not
                                                                            Dim dataWidthCssClass As String = "col-12"
                                                                            @If colItem.HYOJ2 = False Then
                                                                                Colid = Colid & "_anaTDHY"
                                                                            Else
                                                                                Colid = Colid
                                                                            End If
                                                                            @<td class="@dataWidthCssClass" id='@Colid' style="background-color:#@colItem.BGCOLOR ;" rowspan='@subcatRowSpan'>
                                                                                @If Session("LoginUserACCESSLVLCD") = "4" Then
                                                                                    @<font color="@colItem.LINKCOLOR">@colItem.ITEMNM</font>
                                                                                Else
                                                                                    @If colItem.LINKCOLOR = KARIANA_LINK_COLOR Then
                                                                                        @If (Session("LoginUserACCESSLVLCD") = "3" And ViewBag.CHIEF_CAT = 0) Then
                                                                                            @<font color="@colItem.LINKCOLOR">@colItem.ITEMNM</font>
                                                                                        Else
                                                                                            @<a href="#" data-toggle="modal" data-target="#announcerDialog" col-name=@colItem.COL_NAME model-index=@SubCatCnt data-kbn=@colItem.ITEMCD style="color:@colItem.LINKCOLOR;" onclick="tempTD=$(this)">@colItem.ITEMNM</a>
                                                                                        End If

                                                                                    ElseIf colItem.LINKCOLOR = "red" Then
                                                                                        @Html.ActionLink(colItem.ITEMNM, "Edit", "B0020", New With {.id = colItem.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:red;"})
                                                                                    Else
                                                                                        @Html.ActionLink(colItem.ITEMNM, "Edit", "B0020", New With {.id = colItem.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:black;"})
                                                                                    End If
                                                                                    @If colItem.DESKMEMOEXISTFLG = True Then
                                                                                        @<br>
                                                                                        @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = colItem.GYOMDT, .CondAnaid = colItem.ITEMCD, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:1px;"})
End If
                                                                                End If
                                                                                @If colItem.AnaLIST IsNot Nothing AndAlso colItem.AnaLIST.Count > 0 Then
                                                                                    For Each item2 In colItem.AnaLIST
                                                                                        If item2 IsNot Nothing Then
                                                                                            @<br>@<br>
                                                                                            @If Session("LoginUserACCESSLVLCD") = "4" Then
                                                                                                @<font color="@item2.LINKCOLOR">@item2.ITEMNM</font>
                                                                                            Else
                                                                                                If item2.LINKCOLOR = KARIANA_LINK_COLOR Then
                                                                                                    @Html.Raw("Nothing")
                                                                                                ElseIf item2.LINKCOLOR = "red" Then
                                                                                                    @Html.ActionLink(item2.ITEMNM, "Edit", "B0020", New With {.id = item2.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:red;"})
                                                                                                Else
                                                                                                    @Html.ActionLink(item2.ITEMNM, "Edit", "B0020", New With {.id = item2.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:black;"})
                                                                                                End If
                                                                                                @If item2.DESKMEMOEXISTFLG = True Then
                                                                                                    @<br>
                                                                                                    @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = item2.GYOMDT, .CondAnaid = item2.ITEMCD, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:1px;"})
End If
                                                                                            End If
                                                                                        End If
                                                                                    Next
                                                                                End If
                                                                            </td>
                                                                        ElseIf colItem.COL_TYPE = 3 Then
                                                                            If colItem.KYUSHUTSU <> "" Then
                                                                                If colItem.TITLEKBN = "1" Then
                                                                                    @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan">@colItem.ITEMNM</td>
Else
                                                                                    If colItem.KAKUNIN <> "申請中" AndAlso (Html.Encode(colItem.BANGUMINM) = "時間休" OrElse Html.Encode(colItem.BANGUMINM) = "時間強休") Then
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ;border-collapse:separate;  border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td> End If

                                                                                    Else
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ;border-collapse:separate;  border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td> End If
                                                                                    End If
                                                                                End If
                                                                            Else
                                                                                If colItem.TITLEKBN = "1" Then
                                                                                    If colItem.BANGUMINM = "休暇申請中" Then
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate;  border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td> End If

                                                                                    Else
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate; border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td> End If

                                                                                    End If
                                                                                Else
                                                                                    If colItem.KAKUNIN <> "申請中" AndAlso (Html.Encode(colItem.BANGUMINM) = "時間休" OrElse Html.Encode(colItem.BANGUMINM) = "時間強休") Then
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate; border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td> End If
                                                                                    Else
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate; border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="col-13" rowspan="@subcatRowSpan" style="border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td> End If

                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        ElseIf colItem.COL_TYPE = 4 Then
                                                                            If colItem.COL_NAME = "DateHeader" Then
                                                                                @<td class="dateHeaderWidth" rowspan="@subcatRowSpan" style="background-color: #F0FEEF;">@colItem.ITEMNM</td>
ElseIf colItem.COL_NAME = "DayHeader" Then
                                                                                @<td class="dayHeaderWidth" rowspan="@subcatRowSpan" style="background-color: #F0FEEF;">@colItem.ITEMNM</td>
End If
                                                                        Else
                                                                            @If colItem.HYOJ2 = False Then
                                                                                Colid = Colid & "_anaTDHY"
                                                                            Else
                                                                                Colid = Colid
                                                                            End If
                                                                            @If Session("LoginUserACCESSLVLCD") = "4" OrElse (Session("LoginUserACCESSLVLCD") = "3" And ViewBag.CHIEF_CAT = 0) Then
                                                                                @<td class="col-13" id="@Colid" rowspan="@subcatRowSpan"><font color="black">@colItem.ITEMNM</font></td>
Else
                                                                                @<td class="col-13" id="@Colid" rowspan="@subcatRowSpan">@Html.ActionLink(colItem.ITEMNM, "Edit", "A0220", New With {.id = colItem.GYOMNO, .lastForm = "A0240"}, htmlAttributes:=New With {.style = "color:black;"})</td>
End If
                                                                        End If
                                                                    Else
                                                                        Dim dataWidthCssClass As String = "col-13"
                                                                        If colItem.COL_TYPE = 2 Then
                                                                            dataWidthCssClass = "col-12"
                                                                        End If
                                                                        @If (colItem.COL_TYPE = 2 OrElse colItem.COL_TYPE Is Nothing OrElse colItem.COL_TYPE = 1) AndAlso colItem.HYOJ2 = False Then
                                                                            Colid = Colid & "_anaTDHY"
                                                                            @<td class="@dataWidthCssClass" id="@Colid" rowspan="@subcatRowSpan">&nbsp;</td>
Else
                                                                            @<td class="@dataWidthCssClass" id="@Colid" rowspan="@subcatRowSpan">&nbsp;</td>
End If

                                                                    End If

                                                                End If
                                                                leftDataColFreezCounter = leftDataColFreezCounter + 1
                                                            Next

                                                            If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 Then
                                                                @Html.Raw("</tr>")
End If
                                                        Else
                                                            d0010Item = subCatItem(0)
                                                            leftDataColFreezCounter = leftDataColFreezCounter + d0010Item.Count
                                                        End If
                                                        If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 AndAlso d0010Cnt < rowSpan - 1 Then
                                                            i = -1
                                                            leftDataColFreezCounter = 0
                                                            d0010Cnt = d0010Cnt + 1
                                                        End If
                                                    Next
                                                End If
                                            </tr>Next
                                        <!--END: dynamic value display block-->
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div>
                            <div id="headerDiv" class="myHeaderDiv col-md-12" style="padding-right:0px;padding-left:0px;">
                                <table id="tbl-header" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout: fixed;border-collapse:separate;">
                                    @*ASI[20 Jan 2020]: do create column of specific width, generate col before Th Td print*@
                                    @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                        Dim headerWidthCssClass As String = ""
                                        Dim dynamicColId As String = ""
                                        @For Each item In sportCatList2
                                            @For Each item2 In item.M0150LIST
                                                @If rightHeaderColFreezCounterColgroup >= NoOfFreezeCol Then
                                                    If item2.COLTYPE <> "3" Then
                                                        If item2.COLTYPE = "2" Then
                                                            headerWidthCssClass = "col-12"
                                                        Else
                                                            headerWidthCssClass = "col-13"
                                                        End If

                                                        If (item2.COLTYPE = "1" OrElse item2.COLTYPE = "2" OrElse item2.COLTYPE = "") And item2.HYOJ = False Then
                                                            dynamicColId = "Col_anaTHHY"
                                                        Else
                                                            dynamicColId = ""
                                                        End If
                                                        @<col id="@dynamicColId" class="@headerWidthCssClass"></col>
End If

                                                End If
                                                rightHeaderColFreezCounterColgroup = rightHeaderColFreezCounterColgroup + 1
                                            Next
                                        Next
                                    End If

                                    <thead>
                                        <tr id="MainHeaderRowUpper">
                                            @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                                cnt01Right = cnt01Left
                                                If prevCatCdRight = 0 Then
                                                    cnt01Right = 0
                                                End If
                                                Dim flgAnaNmPartition As Boolean = False
                                                Dim partitionCSSStyle As String = ""
                                                Dim dynamicId As String = ""
                                                Dim thID = ""
                                                Dim subCatColspan = 0
                                                Dim subCatWidth = ""
                                                Dim cnt = 1
                                                Dim counter = 0
                                                @For Each item In sportCatList2
                                                    If cnt = 3 Then
                                                        subCatColspan = item.M0150LIST.COUNT - (NoOfFreezeCol - 2)
                                                        subCatWidth = subCatColspan * 100 & "px"
                                                    Else
                                                        If item.SPORTSUBCATCD = 0 Then
                                                            subCatColspan = 1
                                                            subCatWidth = subCatColspan * 130 & "px"
                                                        Else
                                                            subCatColspan = item.M0150LIST.COUNT
                                                            subCatWidth = subCatColspan * 100 & "px"
                                                        End If
                                                    End If

                                                    cnt = cnt + 1

                                                    If item.SPORTSUBCATCD <> 0 Then
                                                        If prevCatCdRight <> item.SPORTSUBCATCD Then
                                                            cnt01Right = cnt01Right + 1
                                                        End If
                                                        thID = "R1_" & cnt01Right
                                                        dynamicId = thID
                                                        'cnt01Right = cnt01Right + 1
                                                    End If

                                                    Dim sportsubcatcd = item.SPORTSUBCATCD
                                                    Dim sportSubCatDrawn = False
                                                    @For Each item2 In item.M0150LIST

                                                        @If rightHeaderColFreezCounterRow1 >= NoOfFreezeCol Then
                                                            @if item2.COLTYPE = "2" And item2.HYOJ = False Then
                                                                dynamicId = thID
                                                            Else
                                                                partitionCSSStyle = ""
                                                                dynamicId = thID
                                                            End If
                                                            @If item2.COLTYPE = "3" Then
                                                                dynamicId = "anaTH"
                                                                If sportSubCatDrawn = False Then
                                                                    If flgAnaNmPartition = False Then
                                                                        Dim userdatacount = sportCatList2.Count - counter
                                                                        Dim userdatacolspan As Integer = (sportCatList2.Count - counter) * 2
                                                                        Dim userdatawidth = 85 * userdatacount & "px"
                                                                        partitionCSSStyle = "border-left:double #babebf;width:" & userdatawidth & ";max-width:" & userdatawidth
                                                                        flgAnaNmPartition = True
                                                                        @<th id="@dynamicId" colspan="@userdatacolspan" style="@partitionCSSStyle">シフト表</th>
End If

                                                                    sportSubCatDrawn = True
                                                                End If

                                                            Else
                                                                @if item2.HYOJ = False Then
                                                                    dynamicId = thID
                                                                End If
                                                                If sportSubCatDrawn = False Then
                                                                    partitionCSSStyle = "background-color:#F0FEEF;max-width:" & subCatWidth & ";width:" & subCatWidth
                                                                    @<th id="@dynamicId" colspan="@subCatColspan" class="col-13" style="@partitionCSSStyle">@item.SPORTSUBCATNM</th>
sportSubCatDrawn = True
                                                                End If
                                                            End If
                                                        End If
                                                        rightHeaderColFreezCounterRow1 = rightHeaderColFreezCounterRow1 + 1
                                                    Next
                                                    counter = counter + 1
                                                Next
                                            End If

                                        </tr>
                                        <tr id="MainHeaderRow">
                                            @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                                cnt01Right = cnt01Left
                                                cnt02Right = (NoOfFreezeCol - 2) + 1
                                                'Dim cntInit As Boolean = True
                                                Dim flgAnaNmPartition As Boolean = False
                                                Dim partitionCSSStyle As String = ""
                                                Dim dynamicId As String = ""
                                                Dim headerWidthCssClass As String = ""
                                                Dim thID = ""
                                                @For Each item In sportCatList2
                                                    @For Each item2 In item.M0150LIST
                                                        @If rightHeaderColFreezCounter >= NoOfFreezeCol Then
                                                            If item.SPORTSUBCATCD <> 0 Then
                                                                If prevCatCdRight <> 0 AndAlso prevCatCdRight <> item.SPORTSUBCATCD Then
                                                                    cnt01Right = cnt01Right + 1
                                                                    'If cntInit = False Then
                                                                    cnt02Right = 1
                                                                    'End If
                                                                End If

                                                                'cntInit = False
                                                                prevCatCdRight = item.SPORTSUBCATCD
                                                                dynamicId = thID
                                                                thID = "R2_" & cnt01Right & "_" & cnt02Right
                                                                cnt02Right = cnt02Right + 1
                                                            End If
                                                            @If item2.COLTYPE = "3" Then
                                                                @If flgAnaNmPartition = False Then
                                                                    flgAnaNmPartition = True
                                                                    partitionCSSStyle = "border-left:double #babebf;"
                                                                Else
                                                                    partitionCSSStyle = ""
                                                                End If
                                                                dynamicId = "anaTH"
                                                            Else
                                                                @If item2.COLTYPE = "2" Then
                                                                    headerWidthCssClass = "col-12"
                                                                Else
                                                                    headerWidthCssClass = "col-13"
                                                                End If

                                                                If item2.COLTYPE = "2" And item2.HYOJ = False Then
                                                                    dynamicId = thID & "_" & "anaTHHY"
                                                                Else
                                                                    partitionCSSStyle = ""
                                                                    dynamicId = thID
                                                                End If

                                                            End If
                                                            @If item2.COLTYPE = "3" Then
                                                                @<th id="@dynamicId" colspan="2" class="col-20" style="@partitionCSSStyle">@item2.COLVALUE</th>
Else
                                                                @if item2.HYOJ = False Then
                                                                    dynamicId = thID & "_" & "anaTHHY"
                                                                End If
                                                                @<th id="@dynamicId" class="@headerWidthCssClass" style="@partitionCSSStyle">@item2.COLVALUE</th>
End If
                                                        End If
                                                        rightHeaderColFreezCounter = rightHeaderColFreezCounter + 1
                                                    Next
                                                Next
                                            End If

                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <div id="dataDiv" class="myDiv col-sm-12" style="padding-right:0px;padding-left:0px;">
                                <table id="tbl-data" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout: fixed;border-collapse:separate;">
                                    <tbody id="myTable">
                                        <!--START: dynamic value display block-->
                                        @Code
                                            Countervar = 0
                                            SubCatCnt = -1
                                        End Code
                                        @For Each dayItem In tbodyData
                                            Dim row As String = "ROW:"
                                            Countervar = Countervar + 1
                                            row = row & Countervar.ToString()
                                            Dim partitionCSSStyle As String = ""
                                            Dim subCatColCnt As Integer = 0

                                            @<tr id=@row>
                                                @If dayItem.Key IsNot Nothing AndAlso dayItem.Key.Count > 0 Then
                                                    Dim rowSpan = dayItem.Value
                                                    Dim d0010Cnt = 0
                                                    Dim isRowSpan = 0
                                                    Dim isNewRow = 0
                                                    Dim Cnt = 1
                                                    rightDataColFreezCounter = 0
                                                    For i = 0 To dayItem.Key.Count - 1
                                                        Dim subCatItem As ICollection = dayItem.Key(i)
                                                        Dim d0010Item As ICollection = subCatItem(d0010Cnt)
                                                        Dim subcatRowSpan = 1
                                                        Dim colSpan = 1

                                                        Dim colClass = ""
                                                        If i = 0 Then
                                                            isNewRow = 0
                                                        End If
                                                        If d0010Item IsNot Nothing AndAlso d0010Item.Count > 0 Then
                                                            If d0010Item(0).SPORTSUBCATCD <> 0 Then
                                                                SubCatCnt = SubCatCnt + 1
                                                            End If
                                                            Dim maxRow = subCatItem.Count
                                                            If maxRow > 1 Then
                                                                isRowSpan = 1
                                                            End If
                                                            If maxRow > 1 AndAlso d0010Cnt > 0 AndAlso Cnt < maxRow AndAlso isNewRow = 0 Then
                                                                @Html.Raw("<tr id=" & row & ">")
Cnt = Cnt + 1
                                                                isNewRow = 1
                                                            End If
                                                            If maxRow = Cnt Then
                                                                subcatRowSpan = rowSpan - maxRow + 1
                                                            Else
                                                                subcatRowSpan = 1
                                                            End If
                                                            subCatColCnt = 0
                                                            For Each colItem As A0230 In d0010Item
                                                                subCatColCnt = subCatColCnt + 1
                                                                Dim Colid = "R4_" & i - 1 & "_" & subCatColCnt
                                                                If rightDataColFreezCounter >= NoOfFreezeCol Then
                                                                    If colItem.COL_TYPE <> 3 AndAlso colItem.SPORTSUBCATCD <> 0 Then
                                                                        strKey = String.Format("lstd0010[{0}].", SubCatCnt)
                                                                        @Html.Hidden(strKey & "GYOMNO", colItem.GYOMNO)
                                                                        @Html.Hidden(strKey & "RNZK", colItem.RNZK)
                                                                        @Html.Hidden(strKey & "PGYOMNO", colItem.PGYOMNO)
If colItem.COL_NAME = "KSKJKNST" Then
                                                                            If colItem.ITEMNM IsNot Nothing Then
                                                                                Dim values As String() = colItem.ITEMNM.Split("~")
                                                                                If values.Count = 0 Then
                                                                                    @Html.Hidden(strKey & "KSKJKNST", "")
                                                                                    @Html.Hidden(strKey & "KSKJKNED", "")
ElseIf values.Count = 1 Then
                                                                                    @Html.Hidden(strKey & "KSKJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "KSKJKNED", "")
ElseIf values.Count = 2 Then
                                                                                    @Html.Hidden(strKey & "KSKJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "KSKJKNED", values(1))
End If
                                                                            End If

                                                                        ElseIf colItem.COL_NAME = "OAJKNST" Then
                                                                            If colItem.ITEMNM IsNot Nothing Then
                                                                                Dim values As String() = colItem.ITEMNM.Split("~")
                                                                                If values.Count = 0 Then
                                                                                    @Html.Hidden(strKey & "OAJKNST", "")
                                                                                    @Html.Hidden(strKey & "OAJKNED", "")
ElseIf values.Count = 1 Then
                                                                                    @Html.Hidden(strKey & "OAJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "OAJKNED", "")
ElseIf values.Count = 2 Then
                                                                                    @Html.Hidden(strKey & "OAJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "OAJKNED", values(1))
End If
                                                                            End If

                                                                        ElseIf colItem.COL_NAME = "SAIJKNST" Then
                                                                            If colItem.ITEMNM IsNot Nothing Then
                                                                                Dim values As String() = colItem.ITEMNM.Split("~")
                                                                                If values.Count = 0 Then
                                                                                    @Html.Hidden(strKey & "SAIJKNST", "")
                                                                                    @Html.Hidden(strKey & "SAIJKNED", "")
ElseIf values.Count = 1 Then
                                                                                    @Html.Hidden(strKey & "SAIJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "SAIJKNED", "")
ElseIf values.Count = 2 Then
                                                                                    @Html.Hidden(strKey & "SAIJKNST", values(0))
                                                                                    @Html.Hidden(strKey & "SAIJKNED", values(1))
End If
                                                                            End If

                                                                        Else
                                                                            If colItem.COL_TYPE = 2 Then
                                                                                @Html.Hidden(strKey & colItem.COL_NAME, colItem.ITEMCD)
Else
                                                                                @Html.Hidden(strKey & colItem.COL_NAME, colItem.ITEMNM)
End If
                                                                        End If
                                                                    End If
                                                                    If colItem.ITEMNM <> "" Then
                                                                        If colItem.COL_TYPE = 2 Then

                                                                            'Want to Display Or Not
                                                                            Dim dataWidthCssClass As String = "col-12"
                                                                            @If colItem.HYOJ2 = False Then
                                                                                Colid = Colid & "_anaTDHY"
                                                                            Else
                                                                                Colid = Colid
                                                                            End If
                                                                            @<td class="@dataWidthCssClass" id='@Colid' style="background-color:#@colItem.BGCOLOR ;" rowspan='@subcatRowSpan'>
                                                                                @If Session("LoginUserACCESSLVLCD") = "4" Then
                                                                                    @<font color="@colItem.LINKCOLOR">@colItem.ITEMNM</font>
                                                                                Else
                                                                                    @If colItem.LINKCOLOR = KARIANA_LINK_COLOR Then
                                                                                        @If (Session("LoginUserACCESSLVLCD") = "3" And ViewBag.CHIEF_CAT = 0) Then
                                                                                            @<font color="@colItem.LINKCOLOR">@colItem.ITEMNM</font>
                                                                                        Else
                                                                                            @<a href="#" data-toggle="modal" data-target="#announcerDialog" col-name=@colItem.COL_NAME model-index=@SubCatCnt data-kbn=@colItem.ITEMCD style="color:@colItem.LINKCOLOR;" onclick="tempTD=$(this)">@colItem.ITEMNM</a>
                                                                                        End If

                                                                                    ElseIf colItem.LINKCOLOR = "red" Then
                                                                                        @Html.ActionLink(colItem.ITEMNM, "Edit", "B0020", New With {.id = colItem.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:red;"})
                                                                                    Else
                                                                                        @Html.ActionLink(colItem.ITEMNM, "Edit", "B0020", New With {.id = colItem.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:black;"})
                                                                                    End If
                                                                                    @If colItem.DESKMEMOEXISTFLG = True Then
                                                                                        @<br>
                                                                                        @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = colItem.GYOMDT, .CondAnaid = colItem.ITEMCD, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:1px;"})
End If
                                                                                End If
                                                                                @If colItem.AnaLIST IsNot Nothing AndAlso colItem.AnaLIST.Count > 0 Then
                                                                                    For Each item2 In colItem.AnaLIST
                                                                                        If item2 IsNot Nothing Then
                                                                                            @<br>@<br>
                                                                                            @If Session("LoginUserACCESSLVLCD") = "4" Then
                                                                                                @<font color="@item2.LINKCOLOR">@item2.ITEMNM</font>
                                                                                            Else
                                                                                                If item2.LINKCOLOR = KARIANA_LINK_COLOR Then
                                                                                                    @Html.Raw("Nothing")
                                                                                                ElseIf item2.LINKCOLOR = "red" Then
                                                                                                    @Html.ActionLink(item2.ITEMNM, "Edit", "B0020", New With {.id = item2.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:red;"})
                                                                                                Else    
                                                                                                    @Html.ActionLink(item2.ITEMNM, "Edit", "B0020", New With {.id = item2.FIX_GYOMNO, .Form_name = "A0240"}, htmlAttributes:=New With {.style = "color:black;"})
                                                                                                End If
                                                                                                @If item2.DESKMEMOEXISTFLG = True Then
                                                                                                    @<br>
                                                                                                    @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = item2.GYOMDT, .CondAnaid = item2.ITEMCD, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:1px;"})
End If
                                                                                            End If
                                                                                        End If
                                                                                    Next
                                                                                End If
                                                                            </td>
                                                                        ElseIf colItem.COL_TYPE = 3 Then
                                                                            If colItem.HYOJJN = 0 Then
                                                                                partitionCSSStyle = "border-left:double #babebf;"
                                                                            Else
                                                                                partitionCSSStyle = ""
                                                                            End If

                                                                            If colItem.DESKMEMOEXISTFLG = True AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
                                                                                colSpan = 1
                                                                                colClass = "col-14"
                                                                            Else
                                                                                colSpan = 2
                                                                                colClass = "col-20"
                                                                            End If
                                                                            If colItem.KYUSHUTSU <> "" Then
                                                                                If colItem.TITLEKBN = "1" Then
                                                                                    @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan">@colItem.ITEMNM</td>
Else
                                                                                    If colItem.KAKUNIN <> "申請中" AndAlso (Html.Encode(colItem.BANGUMINM) = "時間休" OrElse Html.Encode(colItem.BANGUMINM) = "時間強休") Then
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ;border-collapse:separate;  border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
End If

                                                                                    Else
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ;border-collapse:separate;  border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;border-top:2px solid #@colItem.TOPWAKU;  border-bottom:2px solid #@colItem.BOTTOMWAKU ; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
End If
                                                                                    End If
                                                                                End If
                                                                            Else
                                                                                If colItem.TITLEKBN = "1" Then
                                                                                    If colItem.BANGUMINM = "休暇申請中" Then
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate;  border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
End If

                                                                                    Else
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate; border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
End If

                                                                                    End If
                                                                                Else
                                                                                    If colItem.KAKUNIN <> "申請中" AndAlso (Html.Encode(colItem.BANGUMINM) = "時間休" OrElse Html.Encode(colItem.BANGUMINM) = "時間強休") Then
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate;
                                                                                                 1px;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
End If
                                                                                    Else
                                                                                        If colItem.BACKCOLOR IsNot Nothing Then
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;background-color:#@colItem.BACKCOLOR ; color:#@colItem.FONTCOLOR; border-color:#@colItem.WAKUCOLOR; border-collapse:separate; border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR; ">@colItem.ITEMNM</td>
Else
                                                                                            @<td id="anaTD" class="@colClass" colspan="@colSpan" rowspan="@subcatRowSpan" style="@partitionCSSStyle border-left:2px solid #@colItem.ROWWAKUCOLOR;border-bottom:2px solid #@colItem.BOTTOMBLACKWAKU; border-right:2px solid #@colItem.ROWWAKUCOLOR;">@colItem.ITEMNM</td>
End If
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            If colItem.DESKMEMOEXISTFLG = True AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
                                                                                @<td id="anaTD" class="col-2" rowspan="@subcatRowSpan" style="border-top:2px solid #@colItem.DESKMEMOTOPWAKU;  border-bottom:2px solid #@colItem.DESKMEMOBOTTOMWAKU ;">@Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = colItem.GYOMDT, .CondAnaid = colItem.ITEMCD, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:-2px;"})</td>
End If
                                                                        Else
                                                                            'Want to Display Or Not
                                                                            @if colItem.HYOJ2 = False Then
                                                                                Colid = Colid & "_anaTDHY"
                                                                            End If
                                                                            @If Session("LoginUserACCESSLVLCD") = "4" OrElse (Session("LoginUserACCESSLVLCD") = "3" And ViewBag.CHIEF_CAT = 0) Then
                                                                                @<td class="col-13" id="@Colid" rowspan="@subcatRowSpan"><font color="black">@colItem.ITEMNM</font></td>
Else
                                                                                @<td class="col-13" id="@Colid" rowspan="@subcatRowSpan">@Html.ActionLink(colItem.ITEMNM, "Edit", "A0220", New With {.id = colItem.GYOMNO, .lastForm = "A0240"}, htmlAttributes:=New With {.style = "color:black;"})</td>
End If
                                                                        End If
                                                                    Else
                                                                        Dim dataWidthCssClass As String = "col-13"
                                                                        If colItem.COL_TYPE = 2 Then
                                                                            dataWidthCssClass = "col-12"
                                                                        End If
                                                                        If colItem.COL_TYPE = 3 Then
                                                                            If colItem.HYOJJN = 0 Then
                                                                                partitionCSSStyle = "border-left:double #babebf;"
                                                                            Else
                                                                                partitionCSSStyle = ""
                                                                            End If
                                                                            If colItem.DESKMEMOEXISTFLG = True AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
                                                                                @<td id="anaTD" class="col-14" style="@partitionCSSStyle" rowspan="@subcatRowSpan">&nbsp;</td>
                                                                                @<td id="anaTD" class="col-2" rowspan="@subcatRowSpan" style="border-top:2px solid #@colItem.DESKMEMOTOPWAKU;  border-bottom:2px solid #@colItem.DESKMEMOBOTTOMWAKU ;">@Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondShiftst = colItem.GYOMDT, .CondAnaid = colItem.ITEMCD, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs", .target = "_blank", .style = "font-weight:bold;height: 23px;margin-top:-2px;"})</td>
Else
                                                                                @<td id="anaTD" class="col-20" style="@partitionCSSStyle" colspan="2" rowspan="@subcatRowSpan">&nbsp;</td>
End If
                                                                        ElseIf (colItem.COL_TYPE = 2 OrElse colItem.COL_TYPE Is Nothing OrElse colItem.COL_TYPE = 1) AndAlso colItem.HYOJ2 = False Then
                                                                            Colid = Colid & "_anaTDHY"
                                                                            @<td class="@dataWidthCssClass" id="@Colid" rowspan="@subcatRowSpan">&nbsp;</td>
Else
                                                                            @<td class="@dataWidthCssClass" id="@Colid" rowspan="@subcatRowSpan">&nbsp;</td>
End If
                                                                    End If
                                                                End If
                                                                rightDataColFreezCounter = rightDataColFreezCounter + 1
                                                            Next

                                                            If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 Then
                                                                @Html.Raw("</tr>")
End If
                                                        Else
                                                            d0010Item = subCatItem(0)
                                                            rightDataColFreezCounter = rightDataColFreezCounter + d0010Item.Count
                                                        End If
                                                        If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 AndAlso d0010Cnt < rowSpan - 1 Then
                                                            i = -1
                                                            d0010Cnt = d0010Cnt + 1
                                                            rightDataColFreezCounter = 0
                                                        End If
                                                    Next
                                                End If
                                            </tr>Next
                                        <!--END: dynamic value display block-->
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            End Using

        </div>

        @*<button id="btn-announcer" type="button" class="btn btn-info col-md-offset-0" data-toggle="modal" data-target="#announcerDialog" data-kbn="1">Temp Announcer</button>*@
        <div class="modal fade" id="announcerDialog" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">アナウンサー選択</h4>
                        <input type="text" class="hidden" id="kbn" />
                    </div>
                    <div class="modal-body" style="height:75px;">
                        <div class="col-md-9">
                            @Html.DropDownList("ANNOUNCER", New SelectList(ViewBag.ana_TempAnaList, "USERID", "USERNM", "@kbn"), htmlAttributes:=New With {.class = "form-control input-sm"})
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnselectcategory" type="button" class="btn btn-default" data-dismiss="modal" style="float: left;" onclick="btnAnnouncerSelectClick()">選択</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">閉じる</button>
                    </div>
                </div>
            </div>
        </div>

        @Html.Partial("_MsgDialog")


        <script>
        var myApp = myApp || {};
        myApp.Urls = myApp.Urls || {};
        myApp.Urls.baseUrl = '@Url.Content("~")';
        </script>
        <script type="text/javascript">
        var tempTD;
        var leftDataDivWidth;
        var leftDataDivWidth1;
        var rightDataDivWidth;
        var rightDataFilterDivWidth;
        var leftDataFilterDivWidth;
        var rdSearchFilter = 0;
        var rdShiftTableFilter = 0;
        var MainHeaderRowHeight;
        var TotalShowAllWidth=0;

        $("form").submit(function () {
            if (jQuery("#TheForm").context.activeElement != null) {
                if (jQuery("#TheForm").context.activeElement.value != "downloadcsv") {
                    $('#lblInfo').show();
                }
            } else {
                $('#lblInfo').show();
            }
        });

        //内容リストで選択した時
        $("#SPORTCATCD").change(function () {
            $('#searchBtn').click();
            });

        $('#Searchdt').datepicker({
            format: "yyyy/mm",
            language: "ja",
            autoclose: true,
            minViewMode: 'months'
        });

        function SetDate(months) {
            var searchdt = $('#Searchdt').val()
            if (searchdt != "") {
                var curdates = $('#Searchdt').val().split('/');
                var newdate = new Date(curdates[0], curdates[1] - 1, '01');
                newdate.setMonth(newdate.getMonth() + months);
                var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2);
                $('#Searchdt').val(formattedNewDate);
            }
        }

        $(document).ready(function () {
            //$('#btn-announcer').hide();
            //ASI[13 Jan 2020]
            if ($('#SearchFilter')[0].checked) {
                rdSearchFilter = 1;
                $('#SearchAll')[0].checked = true
                $('#SearchFilter')[0].checked = false;
            }

            //ASI[13 Dec 2019][START]
            if ($('#SearchAll')[0].checked) {
                $('#SearchAll').attr('disabled', true);
                $('#SearchFilter').attr('disabled', false);
                document.forms[0].SearchType.value = "0";
                document.forms[1].SearchType.value = "0";
                SetBorder_SearchAll();
                setDynamicHeaderTr_DataTrHeight(0);
            }
            //[END]
            if ($('#hideRd')[0].checked) {
                rdShiftTableFilter = 1;
                 $("#showRd").prop("checked", true).trigger("click");
            } else {
                 $("#showRd").prop("checked", true).trigger("click");
            }

            adjustSubcatWidth();

            $(".header").click(function (evt) {
                //$("#lblInfo").text("処理中です。しばらくお待ち下さい。。。")
                //document.getElementById('lblInfo').style.color = 'orange';
                var table = document.getElementById("tbl-header");
                var rows = table.getElementsByTagName("tr");

                if (rows.length < 3) {
                    return
                }

                evt.preventDefault();
                $("#myForm").submit();
            });



            //$('#myTable tr').find('td:nth-child(6)').click(function () {
            //    $('#btn-announcer').click();
            //    tempTD = this;
            //});

            //dynamic table CELL height set [left-header]

            //ASI[09 Dec 2019]: After Col Freez Logic, now both side list printing, so no need to calculate cell height, so comment this function
            // And write new function and call it to set Height of <tr> of both side table
            //setDynamicCellHeight();
            var DateHeaderColWidth = parseInt($(".dateHeaderWidth").css("max-width").toString().substring(0, $(".dateHeaderWidth").css("max-width").toString().length - 2));
            var DayHeaderColWidth = parseInt($(".dayHeaderWidth").css("max-width").toString().substring(0, $(".dayHeaderWidth").css("max-width").toString().length - 2));
            var DateDayHeaderColCombinedWidth = DateHeaderColWidth + DayHeaderColWidth;

            var col_13ClassWidth = 0;
            if ($(".col-13").css("max-width") != undefined) {
                col_13ClassWidth = parseInt($(".col-13").css("max-width").toString().substring(0, $(".col-13").css("max-width").toString().length - 2));
            }

            var myHeaderDivWidth = 0;
            if ($(".myHeaderDiv").css("max-width") != undefined) {
                myHeaderDivWidth = parseInt($(".myHeaderDiv").css("max-width").toString().substring(0, $(".myHeaderDiv").css("max-width").toString().length - 2));
            }
            var screenTotalMaxWidth = parseInt($(".my-left-HeaderDiv").css("width").toString().substring(0, $(".my-left-HeaderDiv").css("width").toString().length - 2)) + myHeaderDivWidth;

            $($('.my-left-HeaderDiv')).css('max-width', (DateDayHeaderColCombinedWidth+2 + (col_13ClassWidth * ('@NoOfFreezeCol' - 2))) + 'px');
            $($('.my-left-dataDiv')).css('max-width', (DateDayHeaderColCombinedWidth+2 + (col_13ClassWidth * ('@NoOfFreezeCol' - 2))) + 'px');

            $($('.my-left-HeaderDiv')).css('width', (DateDayHeaderColCombinedWidth+2 + (col_13ClassWidth * ('@NoOfFreezeCol' - 2))) + 'px');
            $($('.my-left-dataDiv')).css('width', (DateDayHeaderColCombinedWidth+2 + (col_13ClassWidth * ('@NoOfFreezeCol' - 2))) + 'px');

            var dataTblWidth = screenTotalMaxWidth - $('.my-left-HeaderDiv').width();
            $('#headerDiv').css('max-width', dataTblWidth + 'px');
            $('#dataDiv').css('max-width', dataTblWidth + 'px');

            leftDataDivWidth = parseInt($('.my-left-HeaderDiv').width());
            rightDataDivWidth = parseInt($('#dataDiv').width());
            MainHeaderRowHeight = $('.my-left-HeaderDiv tr[id="MainLeftHeaderRow"]')[0].offsetHeight;
            AdjustHeaderHeight(0.5);
            TotalShowAllWidth = screenTotalMaxWidth;
            if (rdSearchFilter == 1) {
                $('#SearchAll')[0].checked = false
                $("#SearchFilter").prop("checked", true).trigger("click");
            }

            if (rdSearchFilter == 1) {
                rightDataFilterDivWidth = parseInt($('#dataDiv').width());
            }
            if (rdShiftTableFilter == 1) {
                $("#hideRd").prop("checked", true).trigger("click");
            }

            scrollHideUnhide();
            setDynamicHeaderTr_DataTrHeight(2.5);

            if ('@saveSuccess' == '1')
                $('#myModalCnt').modal('show');
        });

        function dynamicallySetFixedHeaderWidth(totalFixedDisplayColumns) {
            var DateHeaderColWidth = parseInt($(".dateHeaderWidth").css("max-width").toString().substring(0, $(".dateHeaderWidth").css("max-width").toString().length - 2));
            var DayHeaderColWidth = parseInt($(".dayHeaderWidth").css("max-width").toString().substring(0, $(".dayHeaderWidth").css("max-width").toString().length - 2));
            var DateDayHeaderColCombinedWidth = DateHeaderColWidth + DayHeaderColWidth;
            var col_13ClassWidth = 0;
            if ($(".col-13").css("max-width") != undefined) {
                col_13ClassWidth = parseInt($(".col-13").css("max-width").toString().substring(0, $(".col-13").css("max-width").toString().length - 2));
            }
            leftDataDivWidth1 = (DateDayHeaderColCombinedWidth + 2 + (col_13ClassWidth * (totalFixedDisplayColumns - 2)));
            $($('.my-left-HeaderDiv')).css('width', (DateDayHeaderColCombinedWidth+ 2 + (col_13ClassWidth * (totalFixedDisplayColumns - 2))) + 'px');
            $($('.my-left-dataDiv')).css('width', (DateDayHeaderColCombinedWidth + 2 + (col_13ClassWidth * (totalFixedDisplayColumns - 2))) + 'px');
        }

        function setDynamicHeaderTr_DataTrHeight(addedHeight) {

            //ASI[09 Dec 2019]: Script to set Height of both side's Header Row(Tr) with greater amonth Left Header Row or Right Header Row
            var maxHeightFromBothHeaderTr = 0
            //var LEFT_TABLE_HEADER_ROW = $('#left-fixed-header tr');
            //var RIGHT_TABLE_HEADER_ROW = $('#tbl-header tr');
            //var leftHeaderTrHeight = LEFT_TABLE_HEADER_ROW.height();
            //var rightHeaderTrHeight = RIGHT_TABLE_HEADER_ROW.height();

            //Preserve Actual height of both Data div before row height calculation logic
            var LEFT_TABLE_DATA_DIV_HEIGHT = parseInt($(".my-left-dataDiv").css("max-height").toString().substring(0, $(".my-left-dataDiv").css("max-height").toString().length - 2));
            var RIGHT_TABLE_DATA_DIV_HEIGHT = parseInt($(".myDiv").css("max-height").toString().substring(0, $(".myDiv").css("max-height").toString().length - 2));

            //if (leftHeaderTrHeight > rightHeaderTrHeight)
            //    maxHeightFromBothHeaderTr = leftHeaderTrHeight;
            //else
            //    maxHeightFromBothHeaderTr = rightHeaderTrHeight;

            //LEFT_TABLE_HEADER_ROW.css('height', maxHeightFromBothHeaderTr);
            //RIGHT_TABLE_HEADER_ROW.css('height', maxHeightFromBothHeaderTr);


            //ASI[09 Dec 2019]: Script to set Height of both side's Data Row(Tr) with greater amonth Left Data Row or Right Data Row
            var maxHeightFromBothDataTr = 0
            var ALL_ROW_TABLE_DATA = $('#dataDiv tr');
            var ALL_ROW_TABLE_LEFT_DATA = $('#left-fixed-data tr');

            ALL_ROW_TABLE_DATA.each(function (index, row) {
                var leftTrHeight = $(ALL_ROW_TABLE_LEFT_DATA[index]).height();
                var rightTrHeight = $(ALL_ROW_TABLE_DATA[index]).height();

                if (leftTrHeight > rightTrHeight)
                    maxHeightFromBothDataTr = leftTrHeight;
                else
                    maxHeightFromBothDataTr = rightTrHeight;

                //ASI[17 Dec 2019]:Height of both side's Tr not exact in IE, FireFox, Edge.
                //To do so, add addiional 0.1px in each Tr height
                $(ALL_ROW_TABLE_DATA[index]).css('height', maxHeightFromBothDataTr + addedHeight);
                $(ALL_ROW_TABLE_LEFT_DATA[index]).css('height', maxHeightFromBothDataTr + addedHeight);

            });

            //Set Actual height of both Data div after row height calculation completed
            $('.my-left-dataDiv').css('height', LEFT_TABLE_DATA_DIV_HEIGHT + 'px');
            $('.myDiv').css('height', RIGHT_TABLE_DATA_DIV_HEIGHT + 'px');

            }

        //ASI[13 Jan 2020]: Hide scroll when no data in detail part
        function scrollHideUnhide() {
            var dataDivWidth = parseInt($('#dataDiv').css("width").toString().substring(0, $('#dataDiv').css("width").toString().length - 2));
            var tbodyCellCount = $('#tbl-data tbody td:visible').length;

            //Detail Not Avilable
            if (tbodyCellCount == 0 ) {

                $('#headerDiv').css('overflow-x', 'hidden');
                $('#headerDiv').css('overflow-y', 'hidden');
                $('#dataDiv').css('overflow-x', 'hidden');
                $('#dataDiv').css('overflow-y', 'hidden');

                $('.my-left-HeaderDiv').css('overflow-x', 'hidden');
                $('.my-left-HeaderDiv').css('overflow-y', 'scroll');
                $('.my-left-dataDiv').css('overflow-x', 'scroll');
                $('.my-left-dataDiv').css('overflow-y', 'scroll');

                if ($('#SearchAll')[0].checked) {
                    $($('.my-left-HeaderDiv')).css('width', (leftDataDivWidth + 18) + 'px');
                    $($('.my-left-dataDiv')).css('width', (leftDataDivWidth + 18) + 'px');

                    $($('.my-left-HeaderDiv')).css('max-width', (leftDataDivWidth + 18) + 'px');
                    $($('.my-left-dataDiv')).css('max-width', (leftDataDivWidth + 18) + 'px');

                } else {

                    var totalHideColumns = $('#left-fixed-header tr th[id$="_anaTHHY"]').length;
                    var totalFixedDisplayColumns = ('@NoOfFreezeCol' - totalHideColumns);
                    dynamicallySetFixedHeaderWidth(totalFixedDisplayColumns);

                    $($('.my-left-HeaderDiv')).css('width', (leftDataDivWidth1+18) + 'px');
                    $($('.my-left-dataDiv')).css('width', (leftDataDivWidth1+18) + 'px');

                    $($('.my-left-HeaderDiv')).css('max-width', (leftDataDivWidth1+18) + 'px');
                    $($('.my-left-dataDiv')).css('max-width', (leftDataDivWidth1+18) + 'px');
                }

                $('#headerDiv').css('max-width', 0 + 'px');
                $('#dataDiv').css('max-width', 0 + 'px');

                //var MainHeaderRowHeight = $('.my-left-HeaderDiv tr[id="MainLeftHeaderRow"]')[0].offsetHeight;
                $('.my-left-HeaderDiv tr').eq(0).css('height', (MainHeaderRowHeight + 1) + 'px');

            }
            else
            {
                $('#headerDiv').css('overflow-x', 'hidden');
                $('#headerDiv').css('overflow-y', 'scroll');
                $('#dataDiv').css('overflow-x', 'scroll');
                $('#dataDiv').css('overflow-y', 'scroll');

                $('.my-left-HeaderDiv').css('overflow-x', 'hidden');
                $('.my-left-HeaderDiv').css('overflow-y', 'hidden');
                $('.my-left-dataDiv').css('overflow-x', 'scroll');
                $('.my-left-dataDiv').css('overflow-y', 'hidden');

                if ($('#SearchAll')[0].checked) {
                    if (rdSearchFilter == 1) {
                        $('#headerDiv').css('width', (TotalShowAllWidth - leftDataDivWidth) + 'px');
                        $('#headerDiv').css('max-width', (TotalShowAllWidth - leftDataDivWidth) + 'px');
                        $('#dataDiv').css('width', (TotalShowAllWidth - leftDataDivWidth) + 'px');
                        $('#dataDiv').css('max-width', (TotalShowAllWidth - leftDataDivWidth) + 'px');
                        $($('.my-left-HeaderDiv')).css('max-width', (leftDataDivWidth + 0) + 'px');
                        $($('.my-left-dataDiv')).css('max-width', (leftDataDivWidth + 0) + 'px');

                    } else {
                        var totalHideColumns = $('#left-fixed-header tr th[id$="_anaTHHY"]').length;
                        var totalFixedDisplayColumns = ('@NoOfFreezeCol' - totalHideColumns);
                        dynamicallySetFixedHeaderWidth(totalFixedDisplayColumns);
                        $($('.my-left-HeaderDiv')).css('width', (leftDataDivWidth + 0) + 'px');
                        $($('.my-left-dataDiv')).css('width', (leftDataDivWidth + 0) + 'px');
                        $($('.my-left-HeaderDiv')).css('max-width', (leftDataDivWidth + 0) + 'px');
                        $($('.my-left-dataDiv')).css('max-width', (leftDataDivWidth + 0) + 'px');

                        $('#headerDiv').css('width', (TotalShowAllWidth - leftDataDivWidth ) + 'px');
                        $('#headerDiv').css('max-width', (TotalShowAllWidth - leftDataDivWidth) + 'px');
                        $('#dataDiv').css('width', (TotalShowAllWidth - leftDataDivWidth) + 'px');
                        $('#dataDiv').css('max-width', (TotalShowAllWidth - leftDataDivWidth ) + 'px');
                    }

                } else {
                    $('#headerDiv').css('width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');
                    $('#dataDiv').css('width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');
                    $('#headerDiv').css('max-width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');
                    $('#dataDiv').css('max-width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');

                    //if (rdSearchFilter == 1) {
                    //    $('#headerDiv').css('width', (leftDataDivWidth - parseInt($('.my-left-HeaderDiv').width())) + rightDataFilterDivWidth + 'px');
                    //    $('#dataDiv').css('width', (leftDataDivWidth - parseInt($('.my-left-HeaderDiv').width())) + rightDataFilterDivWidth + 'px');
                    //    $('#headerDiv').css('max-width', (leftDataDivWidth-parseInt($('.my-left-HeaderDiv').width())) + rightDataFilterDivWidth + 'px');
                    //    $('#dataDiv').css('max-width', (leftDataDivWidth - parseInt($('.my-left-HeaderDiv').width())) + rightDataFilterDivWidth + 'px');

                    //} else {
                    //    $('#headerDiv').css('width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');
                    //    $('#dataDiv').css('width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');
                    //    $('#headerDiv').css('max-width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');
                    //    $('#dataDiv').css('max-width', (TotalShowAllWidth - parseInt($('.my-left-HeaderDiv').width())) + 'px');
                    //}
                }

            }

        }

        function setDynamicCellHeight() {

            var ALL_ROW_TABLE_DATA = $('#dataDiv tr');
            var ALL_ROW_TABLE_LEFT_HEADER = $('#left-fixed-data tr');
            var LEFT_HEADER_DIV_HEIGHT = $('.my-left-dataDiv').height();

            var MainHeaderRowHeight = $('#tbl-header tr[id="MainHeaderRow"]')[0].offsetHeight;
            var ALL_ROW_TABLE_LEFT_LABEL_HEADER = $('#left-fixed-header tr');
            var TotalHeaderHeight = MainHeaderRowHeight;

            ALL_ROW_TABLE_LEFT_LABEL_HEADER.each(function (index, row) {
                $(ALL_ROW_TABLE_LEFT_LABEL_HEADER[index]).css('height', MainHeaderRowHeight);
            });

            $($('.my-left-HeaderDiv')).css('height', TotalHeaderHeight + 'px');
            $($('.myHeaderDiv')).css('height', (TotalHeaderHeight + 1) + 'px');

            //Logic for create dynamic left data header height
            var lastID = "0";
            var thIndex = 0;
            ALL_ROW_TABLE_DATA.each(function (index, row) {
                var val = this.id
                if (val != lastID) {
                    var rows = $('#dataDiv tr[id="' + val + '"]')
                    var heightSum = 0;

                    rows.each(function (index, row) {
                        heightSum = heightSum + $(this).height();
                    });

                    $(ALL_ROW_TABLE_LEFT_HEADER[thIndex]).css('height', heightSum);
                    thIndex = thIndex + 1;
                }

                lastID = val;

            });

            $($('.my-left-dataDiv')).css('height', LEFT_HEADER_DIV_HEIGHT + 'px');

        }

        function KeyUpFunction() {
            var searchdt = $('#Searchdt').val()
            var viewdate = $('#viewdatadate').val()

            if (searchdt != "") {
                if (searchdt.length == 7) {
                    if (searchdt != viewdate) {
                        $("#myForm").submit();
                    }
                }
            }
        }

        /**ASI[27 Nov 2019]: Ana selection dialog **/
        $('#announcerDialog').on('show.bs.modal', function (event) {
            var modal = $(this);
            //var button = $(event.relatedTarget);
            // var kbn = button.data('kbn');
            var kbn = tempTD.attr("data-kbn");
            modal.find('#ANNOUNCER').val(kbn);
        });

        /**ASI[27 Nov 2019]: Ana selection dialog **/
        $('#announcerDialog').on('shown.bs.modal', function (e) {
            //logic which execute after dialog box load
        });

        $('#announcerDialog').on('hidden.bs.modal', function (event) {
            //Logic after Closed DialogBox
        });

        function btnAnnouncerSelectClick() {
            var announcerVal = $("#ANNOUNCER").val();
            var announcerName = $("#ANNOUNCER option:selected").text();
            tempTD.context.text = announcerName;
            tempTD.attr("data-kbn", $("#ANNOUNCER").val());

            //replace value in model when name change by dailog
            var colname = tempTD.attr("col-name");
            var index = tempTD.attr("model-index");
            var id = "lstd0010_" + index + "__" + colname;

            //Set Selected Record Value
            document.getElementById(id).value = announcerVal;

            //Set to All Parant Child Data
            var GYOMNO = ""

            if (document.getElementById("lstd0010_" + index + "__" + "PGYOMNO").value !== "0") {
                GYOMNO = document.getElementById("lstd0010_" + index + "__" + "PGYOMNO").value;
            }
            else {
                GYOMNO = document.getElementById("lstd0010_" + index + "__" + "GYOMNO").value;
            }

            var datanotfound = false;
            var Count = 0;
            while (datanotfound == false) {

                var Chekdataexist = document.getElementById("lstd0010_" + Count + "__" + "PGYOMNO");
                if (Chekdataexist != null) {

                    if (document.getElementById("lstd0010_" + Count + "__" + "PGYOMNO").value == GYOMNO ||
                        document.getElementById("lstd0010_" + Count + "__" + "GYOMNO").value == GYOMNO) {
                        document.getElementById("lstd0010_" + Count + "__" + colname).value = announcerVal;
                        $('[col-name=' + colname + '][model-index=' + Count + ']').attr('data-kbn', announcerVal);
                        $('[col-name=' + colname + '][model-index=' + Count + ']').text(announcerName);
                    };

                }
                else {
                    datanotfound = true;
                };
                Count++;
            }

            //document.getElementById("lstd0010_1__COL03").value = announcerVal;
            //$('[col-name=COL03]').eq(1).attr('data-kbn', announcerVal);
            //$('[col-name=COL03]').eq(1).text(announcerName);
        }

        //Havan[20 Dec 2019]: Hide PrintBtn for 2nd Release. after release uncomment this line and start implementation.
        //Comment PrintDiv() and afterPrint() functions.
        /*function PrintDiv() {
            var divContents = document.getElementById("tblSportChartByItem").innerHTML;
            var headstr = "<html><head><title></title></head><body>";
            var footstr = "</body>";
            var oldhead = document.head.innerHTML;
            var oldstr = document.body.innerHTML;


            var ths = document.getElementsByTagName("th");
            for (var i = 0; i < ths.length; i++) {

                if (ths[i].className == "colLink") {
                    ths[i].hidden = true;
                }
            }

            var tds = document.getElementsByTagName("td");
            for (var i = 0; i < tds.length; i++) {

                if (tds[i].className == "colLink") {
                    tds[i].hidden = true;
                }
            }


            var divContents = document.getElementById("tblSportChartByItem").innerHTML;

            //var url = myApp.Urls.baseUrl + 'Content/C0030.css';
            //document.body.innerHTML = '<html><head><link rel="stylesheet"  type="text/css" media="print" href=' + url + '></head><body>' + divContents + '</body></html>'

            window.print();

            document.body.innerHTML = oldstr;

            afterPrint()
        }

        function afterPrint() {

            setTimeout(function () { document.location.href = window.location.href; }, 250);
        }*/

        //ASI[05 Aug 2019] : script to Scroll horizontally with header
        $("#dataDiv").scroll(function () {
            $(".my-left-dataDiv").scrollTop($("#dataDiv").scrollTop());
            $("#headerDiv").scrollLeft($("#dataDiv").scrollLeft());
        });

        //ASI[19 Dec 2019] : script to Scroll left and Right both header vertically with right header scroll
        $("#headerDiv").scroll(function () {
            $(".my-left-HeaderDiv").scrollTop($("#headerDiv").scrollTop());
        });

        ////検索終わったら、処理中メッセージをクリア
        //jQuery(window).load(function () {
        //    setTimeout(function () {
        //        document.getElementById('lblInfo').style.color = 'white';
        //    }, 1000);
        //});

        //ASI[28 Nov 2019] : On selection of シフト表 radio, hide unhide announcer table [ie. Ana name and it's data table]
        $('#showRd').on('click', function (e) {
            /* $('#tbl-header tr th[id="anaTH"]').each(function (index) {
                 $(this).show();
                 //$("#tbl-data td:nth-child(" + $(this).context.cellIndex + ")").show();
                 //$("#tbl-data td:nth-child(" + (($(this).context.cellIndex) + 1).toString() + ")").show();
                 $("#tbl-data td:last-child").show();

                 //After show anaTH, it can't showing without scroll left and right ,
                 //so dynamically scroll left 10 px and then move in original position
                 //$("#dataDiv").scrollLeft($("#dataDiv").scrollLeft() + 10);
                 //$("#dataDiv").scrollLeft($("#dataDiv").scrollLeft() - 10);

             });*/
            document.forms[0].ShiftTableRadioType.value = "0";
            document.forms[1].ShiftTableRadioType.value = "0";
            $('#tbl-header tr th[id="anaTH"]').show();
            $('#tbl-data td[id="anaTD"]').show();
            scrollHideUnhide();
            setDynamicHeaderTr_DataTrHeight(0);
        });
        $('#hideRd').on('click', function (e) {
            /*  $('#tbl-header tr th[id="anaTH"]').each(function (index) {
                  $(this).hide();
                  //$("#tbl-data td:nth-child(" + $(this).context.cellIndex + ")").hide();
                  //$("#tbl-data td:nth-child(" + (($(this).context.cellIndex) + 1).toString() + ")").hide();
                  $("#tbl-data td:last-child").hide();
              });*/
            document.forms[0].ShiftTableRadioType.value = "1";
            document.forms[1].ShiftTableRadioType.value = "1";
            $('#tbl-header tr th[id="anaTH"]').hide();
            $('#tbl-data td[id="anaTD"]').hide();
            scrollHideUnhide();
            setDynamicHeaderTr_DataTrHeight(0);
            /*var lastChilds = $('#tbl-header td:last-child');
            lastChilds.each(function (i) {
                var rowSpan = $(this).attr('rowspan');
                if (rowSpan !== undefined) {
                    lastChilds.splice(i + 1, rowSpan - 1);
                }
                $(this).hide();
            });*/


        });

        //ASI[07 Dec 2019]: ANA Fetching SELECTION RADIO Setting
        $('#SearchFilter').on('click', function (e) {
            $(this).attr('disabled', true);
            $('#SearchAll').attr('disabled', false);
            $('#SearchAll')[0].checked = false
            document.forms[0].SearchType.value = "1";
            document.forms[1].SearchType.value = "1";

            var totalHideColumns = $('#left-fixed-header tr th[id$="anaTHHY"]').length;
            var totalFixedDisplayColumns = ('@NoOfFreezeCol' - totalHideColumns);
            dynamicallySetFixedHeaderWidth(totalFixedDisplayColumns);

            $('#tbl-header tr th[id$="anaTHHY"]').hide();

            //ASI[20 Jan 2020]: Hide <col>
            $('#tbl-header [id="Col_anaTHHY"]').hide();
            $('#left-fixed-header [id="Col_anaTHHY"]').hide();


            $('#tbl-data td[id$="anaTDHY"]').hide();
            $('#left-fixed-header tr th[id$="anaTHHY"]').hide();
            $('#left-fixed-data td[id$="anaTDHY"]').hide();
            headerHideWithAdjustWidth();
            scrollHideUnhide();
            setDynamicHeaderTr_DataTrHeight(0);
            AdjustHeaderHeight(0);
        });

        $('#SearchAll').on('click', function (e) {
            $(this).attr('disabled', true);
            $('#SearchFilter').attr('disabled', false);
            $('#SearchFilter')[0].checked = false
            document.forms[0].SearchType.value = "0";
            document.forms[1].SearchType.value = "0";

            var totalFixedDisplayColumns = '@NoOfFreezeCol';
            dynamicallySetFixedHeaderWidth(totalFixedDisplayColumns);

            $('#tbl-header tr th[id$="anaTHHY"]').show();

            //ASI[20 Jan 2020]: Show <col>
            $('#tbl-header [id="Col_anaTHHY"]').show();
            $('#left-fixed-header [id="Col_anaTHHY"]').show();

            $('#tbl-data td[id$="anaTDHY"]').show();
            $('#left-fixed-header tr th[id$="anaTHHY"]').show();
            $('#left-fixed-data td[id$="anaTDHY"]').show();
            headerShowWithAdjustWidth();
            scrollHideUnhide();
            setDynamicHeaderTr_DataTrHeight(0);
            AdjustHeaderHeight(0);
        });

        function set_Flag_Update() {
            var result = confirm("更新します。よろしいですか？");

            if (result == false) {
                return false;
            }
            $('#forUpdateOrInsert').val("Update");

        }

        function headerHideWithAdjustWidth() {

            $('#left-fixed-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);
                var TotlHideColumnsPerCat = $('#left-fixed-header tr th[id^="R2_' + counter1 + '_"][id$="_anaTHHY"]').length;
                if (TotlHideColumnsPerCat > 0) {
                    var reducedTd = parseInt($(this)[0].colSpan) - parseInt(TotlHideColumnsPerCat);
                    //var tdWidthBefore = parseInt($(this).css("width").toString().substring(0, $(this).css("width").toString().length - 2));
                    if (reducedTd == 0) {
                        $(this)[0].colSpan = 1;
                    } else {
                        $(this)[0].colSpan = reducedTd;
                    }

                    var tdWidthAfter = 0;
                    if (reducedTd == 0) {
                        $(this).hide();
                    } else {
                        tdWidthAfter = $(this)[0].colSpan * 100;
                    }
                    $(this).css('width', tdWidthAfter + 'px');
                }
            });

            $('#tbl-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);
                var TotlHideColumnsPerCat = $('#tbl-header tr th[id^="R2_' + counter1 + '_"][id$="_anaTHHY"]').length;
                if (TotlHideColumnsPerCat > 0) {
                    var reducedTd = parseInt($(this)[0].colSpan) - parseInt(TotlHideColumnsPerCat);
                    //var tdWidthBefore = parseInt($(this).css("width").toString().substring(0, $(this).css("width").toString().length - 2));
                    if (reducedTd == 0) {
                        $(this)[0].colSpan = 1;
                    } else {
                        $(this)[0].colSpan = reducedTd;
                    }

                    var tdWidthAfter = 0;
                    if (reducedTd == 0) {
                        $(this).hide();
                    } else {
                        tdWidthAfter = $(this)[0].colSpan * 100;
                    }
                    $(this).css('width', tdWidthAfter + 'px');
                }
            });

            SetBorder_SearchFilter();
        }

       function headerShowWithAdjustWidth() {

            $('#left-fixed-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);
                var HiddenCount = $('#left-fixed-header tr th[id^="R2_' + counter1 + '_"][id$="_anaTHHY"]').length;
                var TotalCount = $('#left-fixed-header tr th[id^="R2_' + counter1 + '_"]').length;
                if (TotalCount > 0 && HiddenCount > 0) {
                    var addedTd = 0;
                    if (parseInt(TotalCount) == parseInt($(this)[0].colSpan)) {
                        addedTd = 1;
                    } else {
                        addedTd = parseInt(TotalCount) - parseInt($(this)[0].colSpan);
                    }
                    if ($(this).css('display') == "none") {
                        $(this).show();
                    }
                    $(this)[0].colSpan = TotalCount;
                    var tdWidthAfter = $(this)[0].colSpan * 100;
                    $(this).css('width', tdWidthAfter + 'px');
                }
            });

            $('#tbl-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);
                var HiddenCount = $('#tbl-header tr th[id^="R2_' + counter1 + '_"][id$="_anaTHHY"]').length;
                var TotalCount = $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').length;

                if (TotalCount > 0 && HiddenCount > 0) {
                    var addedTd = 0;
                    if (parseInt(TotalCount) == parseInt($(this)[0].colSpan)) {
                        addedTd = 1;
                    } else {
                        addedTd = parseInt(TotalCount) - parseInt($(this)[0].colSpan);
                    }
                    if ($(this).css('display') == "none") {
                        $(this).show();
                    }
                    $(this)[0].colSpan = TotalCount;
                    var tdWidthAfter = $(this)[0].colSpan * 100;
                    $(this).css('width', tdWidthAfter + 'px');
                }
            });
            SetBorder_SearchAll();
        }

        function adjustSubcatWidth() {

            $('#left-fixed-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);
                var totalWidth = 0;
                $('#left-fixed-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                    var widthStr = $(this).css('max-width');
                    var widthInt = widthStr.substring(0, widthStr.length - 2);
                    totalWidth += parseInt(widthInt);
                });
                $(this).css('max-width', totalWidth + 'px');
                $(this).css('width', totalWidth  + 'px');
            });

            $('#tbl-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);
                var totalWidth = 0;
                $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                    var widthStr = $(this).css('max-width');
                    var widthInt = widthStr.substring(0, widthStr.length - 2);
                    totalWidth += parseInt(widthInt);
                });
                $(this).css('max-width', totalWidth + 'px');
                $(this).css('width', totalWidth  + 'px');
            });

        }

        function AdjustHeaderHeight(addedHeight) {

            var maxHeightFromBothDataTr = 0
            var Left_Fixed_Header = $('#left-fixed-header tr');
            var Right_Fixed_Header = $('#tbl-header tr');

            Right_Fixed_Header.each(function (index, row) {
                var leftTrHeight = $(Left_Fixed_Header[index]).height();
                var rightTrHeight = $(Right_Fixed_Header[index]).height();

                if (leftTrHeight > rightTrHeight)
                    maxHeightFromBothDataTr = leftTrHeight;
                else
                    maxHeightFromBothDataTr = rightTrHeight;

                //ASI[17 Dec 2019]:Height of both side's Tr not exact in IE, FireFox, Edge.
                //To do so, add addiional 0.1px in each Tr height
                $(Right_Fixed_Header[index]).css('height', maxHeightFromBothDataTr + addedHeight);
                $(Left_Fixed_Header[index]).css('height', maxHeightFromBothDataTr + addedHeight);
            });
        }

        function SetBorder_SearchFilter() {

            $('#tbl-header tr th[id^="R1_"]').each(function () {
                $(this).css('border-right', '1px');
            });
            $('#tbl-header tr th[id^="R2_"]').each(function () {
                $(this).css('border-right', '1px');
            });
            $('#tbl-data tr td[id^="R4_"]').each(function () {
                $(this).css('border-right', '1px');
            });

            //Total display sub category
            var lastCatLength = $('#tbl-header tr th[id^="R1_"]').filter(function () {
                return $(this).css('display') != "none"
            }).length;

            var catCount = 0;
            var TotalSubCatDispCol = 0;
            var counter1 = 1;

            $('#tbl-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);

                //Per category total display col1
                TotalSubCatDispCol = $('#tbl-header tr th[id^="R2_' + counter1 + '_"]:not([id$="_anaTHHY"])').length;

                if (TotalSubCatDispCol > 0) {
                    catCount = catCount + 1;
                }

                if (lastCatLength != catCount) {
                    $(this).css('border-right', '2px solid gray');
                    $('#tbl-header tr th[id^="R2_' + counter1 + '_"]:not([id$="_anaTHHY"])').each(function (index) {
                        if (index + 1 == TotalSubCatDispCol) {
                            $(this).css('border-right', '2px solid gray');
                            $('#tbl-data tr td[id^="' + $(this).attr("id").replace('R2', 'R4').replace('anaTHHY', 'anaTDHY') + '"]').css('border-right', '2px solid gray');
                        }
                    });
                }
            });
        }

        function SetBorder_SearchAll() {

            $('#tbl-header tr th[id^="R1_"]').each(function () {
                $(this).css('border-right', '1px');
            });
            $('#tbl-header tr th[id^="R2_"]').each(function () {
                $(this).css('border-right', '1px');
            });
            $('#tbl-data tr td[id^="R4_"]').each(function () {
                $(this).css('border-right', '1px');
            });

            //Total display sub category
            var lastCatLength = $('#tbl-header tr th[id^="R1_"]').filter(function () {
                return $(this).css('display') != "none"
            }).length;

            var catCount = 0;
            var TotalSubCatDispCol = 0;
            var counter1 = 1;

            $('#tbl-header tr th[id^="R1_"]').each(function () {
                counter1 = $(this).attr("id").substr(3);

                //Per category total display col1
                TotalSubCatDispCol = $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').length;

                if (TotalSubCatDispCol > 0) {
                    catCount = catCount + 1;
                }

                if (lastCatLength != catCount) {
                    $(this).css('border-right', '2px solid gray');
                    $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function (index) {
                        if (index + 1 == TotalSubCatDispCol) {
                            $(this).css('border-right', '2px solid gray');
                            $('#tbl-data tr td[id^="' + $(this).attr("id").replace('R2', 'R4').replace('anaTHHY', 'anaTDHY') + '"]').css('border-right', '2px solid gray');
                        }
                    });
                }
            });
            }

        $('#btnUpdate').on('click', function (e) {

            var result = confirm("更新します。よろしいですか？");

            if (result == false) {
                return false;
            }
            $('#forUpdateOrInsert').val("Update");
        });
        $('#btnCollectiveReg').on('click', function (e) {

            var result = confirm("仮アナ以外の担当アナに対し、本登録します。よろしいですか？");

            if (result == false) {
                return false;
            }
            $('#forUpdateOrInsert').val("Insert");
        });


        </script>
