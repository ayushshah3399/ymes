
@Code
    ViewData("Title") = "全体スポーツシフト表"
    Dim sportCatList = DirectCast(ViewBag.sportCatList, List(Of M0130))
    Dim sportCatList2 = DirectCast(ViewBag.sportCatList2, List(Of M0140))
    Dim sportCatList3 = DirectCast(ViewBag.D0010MainList, Dictionary(Of ICollection, Integer))
    Dim sportCatCheckedList = DirectCast(ViewBag.SportCatCheckedList, List(Of M0130))
    Dim SearchDate As Date = Date.ParseExact(ViewData("searchdt").ToString(), "yyyy/MM", Nothing)
    Dim days As Integer = Date.DaysInMonth(SearchDate.Year, SearchDate.Month)
    Dim Countervar As Integer = 0
    Dim searchType = DirectCast(ViewBag.SearchType, String)
    Dim cnt1 = 1
    Dim cnt2 = 1
    Dim cnt3 = 1
    Dim thID = ""
    Dim prevCatCd = 0
    Dim KARIANA_LINK_COLOR As String = "#1e90ff"

End Code


<style>
    .table > thead > tr > th, 
    .table > tbody > tr > th, 
    .table > thead > tr > td, 
    .table > tbody > tr > td  {
        padding-left:2px;
        padding-right:2px;
        padding-top:2px;
        padding-bottom:2px;
    }

    table a:not(.btn), .table a:not(.btn) {
        text-decoration: none;
    }

    table a:hover, .table a:hover {
        text-decoration: underline;
    }

    .myDiv {
        max-width: 1765px;
        max-height: 648px;
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
        height: 66px;
        padding-left: 0px;
    }

    #tbl-data.table {
        position: relative;
        border-collapse: collapse;
        margin-bottom: 0px;
    }

    .my-left-HeaderDiv {
        overflow: hidden;
        z-index: 1;
        height: 66px;
        width: 84px;
        max-width: 84px;
    }

    .my-left-dataDiv {
        max-height: 648px;
        overflow-x: scroll;
        overflow-y: hidden;
        position: relative;
        width: 84px;
        max-width: 84px;
    }

    th, td {
        width: 50px;
        vertical-align: middle;
        word-wrap: break-word;
    }

    #left-fixed-header th {
        vertical-align: middle;
    }

    #tbl-header th {
        vertical-align: middle;
    }

    #left-fixed-data th {
        vertical-align: middle;
    }

    .table > tbody > tr > td {
        vertical-align: middle;
        text-align: center;
    }

    .row-1 {
        height: 34px;
    }

    .row-2 {
        height: 68px;
    }

    .col-1 {
        width: 50px;
    }

    .col-2 {
        width: 30px;
    }

    .cat149_col {
        width: 60px;
    }

    .cat150_col {
        width: 85px;
    }


    .col1 {
        width: 50px;
    }

    .col2 {
        width: 30px;
    }

    .baseball_cat1_head {
        width: 1520px;
    }

    .baseball_cat2_head {
        width: 640px;
    }

    .baseball_cat_col1 {
        width: 90px;
    }

    .baseball_cat_col2 {
        width: 50px;
    }

    .baseball_cat_col3 {
        width: 60px;
    }

    .baseball_cat_col4 {
        width: 50px;
    }

    .baseball_cat_col5 {
        width: 70px;
    }

    .baseball_cat_col6 {
        width: 70px;
    }

    .baseball_cat_col7 {
        width: 70px;
    }

    .baseball_cat_col8 {
        width: 50px;
    }

    .baseball_cat_col9 {
        width: 50px;
    }

    .baseball_cat_col10 {
        width: 80px;
    }

    body {
        font-size: 12px;
    }
</style>

<div class="col-md-12">
    <div class="row">
        <div class="col-md-6" style="width :400px"><h3>全体スポーツシフト表</h3></div>
        <div class="col-md-6" style="background-color:white;font-size:14px;margin-top:25px; width :400px;">
            <label style="font-size:15px;text-align:left; color:orange; width :400px" id="lblInfo" hidden="hidden">処理中です。しばらくお待ち下さい。。。</label>
        </div>
        <div class="col-md-6" style="background-color:white;font-size:14px;margin-top:15px;width :auto ;position:absolute; right:0px;">
            <ul class="nav nav-pills navbar-right" style="padding-right:15px">
                @If ViewData("Kanri") = "1" Then
                    @<li>@Html.ActionLink("スポーツシフト登録(仮登録)", "Index", "A0220", Nothing, htmlAttributes:=New With {.target = "_blank"})</li>
                    @<li>@Html.ActionLink("種目別スポーツシフト表", "Index", "A0240", New With {.searchdt = ViewData("searchdt").ToString.Substring(0, 7)}, htmlAttributes:=New With {.target = "_blank"})</li>
End If
                <!--Havan[20 Dec 2019]: Hide PrintBtn for 2nd Release. after release uncomment this line and start implementation.-->
                <!-- <li><a href="javascript:PrintDiv();">印刷</a></li>-->
                <li><a href="#" onclick="document.getElementById('lblInfo').hidden = false;document.getElementById('myForm').submit();">最新情報</a></li>
                @If Session("UrlReferrer") IsNot Nothing Then
                    @<li><a href="@Session("UrlReferrer")">戻る</a></li>
End If
            </ul>
        </div>
    </div>
</div>

<div class="container-fluid">
    <div class="row">
        @Using Html.BeginForm("Index", "A0230", FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})
            @Html.Hidden("CatCodes", Nothing)
            @Html.Hidden("SearchType", "0")
            @<div class="col-md-12">
                <ul class="nav nav-pills">
                    <li><input type="submit" class="btn btn-success btn-sm btnForward" style="background:white; color:green" value="前月" onclick="SetDate(-1)"></li>
                    <li>
                        <div class="input-group">
                            <input type="text" id="Searchdt" name="Searchdt" class="form-control input-sm date imedisabled" value=@ViewData("searchdt") onchange="KeyUpFunction()" style="width:80px;font-size:small;">
                        </div>
                    </li>
                    <li><input type="submit" class="btn btn-success btn-sm btnNext" style="background:white; color:green" value="翌月" onclick="SetDate(1)"></li>
                    <li style="padding-right:50px;"><input type="submit" class="btn btn-success btn-sm btnSearch" value="表示"></li>
                    <li>
                        <div "col-md-9">
                            <div>
                                <label class="control-label" style="width:150px;">表示スポーツカテゴリ</label>
                                <label class="checkbox-inline">
                                    @Html.CheckBox("flgSportCatAll")  全て
                                </label>
                                @If sportCatCheckedList IsNot Nothing AndAlso sportCatCheckedList.Count > 0 Then
                                    @For Each item In sportCatCheckedList
                                        @<label class="checkbox-inline">
                                            @Html.CheckBox("sportCat", item.CHECKEDSTATUS, htmlAttributes:=New With {.value = item.SPORTCATCD}) @item.SPORTCATNM
                                        </label>
Next
                                End If
                            </div>
                            <div>
                                <label class="control-label" style="width:150px;text-align :right;padding-right :30px;">項目</label>
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
                        </div>
                    </li>
                </ul>
            </div>
            @Html.Hidden("viewdatadate", ViewBag.searchdt)
        End Using
    </div>

    @Using (Html.BeginForm())
        @Html.Hidden("Searchdt")
        @*@<p>
                <label style="font-size:15px;text-align:right; color:orange;padding-left:20px;" id="lblInfo">処理中です。しばらくお待ち下さい。。。</label>
            </p>*@

        @<div>
            <div class="col-md-12" style="display:table;padding-left:0px;">
                <div style="width:84px;display:table-cell;vertical-align:top;">
                    <div id="leftHeaderDiv" class="my-left-HeaderDiv">
                        <table id="left-fixed-header" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout:fixed;">
                            <tr id="leftHeaderRow1">
                                <th class="col-1">&nbsp;</th>
                                <th class="col-2">&nbsp;</th>
                            </tr>
                            <tr id="leftHeaderRow2">
                                <th class="col-1">&nbsp;</th>
                                <th class="col-2">&nbsp;</th>
                            </tr>
                            <tr id="leftHeaderRow3">
                                <th class="col-1">日付</th>
                                <th class="col-2">曜</th>
                            </tr>
                        </table>
                    </div>
                    <div class="my-left-dataDiv">
                        <table id="left-fixed-data" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout:fixed;">
                            @For i = 0 To days - 1
                                @<tr>
                                    <td class="col-1" style="vertical-align:middle;background-color:#F0FEEF;">@SearchDate.AddDays(i).ToString("MM/dd")</td>
                                    <td class="col-2" style="vertical-align:middle;background-color:#F0FEEF;">@SearchDate.AddDays(i).ToString("ddd")</td>
                                </tr>
Next
                        </table>
                    </div>
                </div>
                <div id="mycontent">
                    <div id="headerDiv" class="myHeaderDiv col-md-12">
                        <table id="tbl-header" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout:fixed;">
                            @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                Dim headerWidthCssClass = ""
                                Dim thIDs = ""
	                            @For Each item In sportCatList2
		                            @For Each item2 In item.M0150LIST
                                        Dim anaThID = "Col_anaTHHY"
                                        If item2.COLTYPE = "2" Then
                                            headerWidthCssClass = "cat149_col"
                                        Else
                                            headerWidthCssClass = "cat150_col"
                                        End If
			                            @if item2.HYOJ = False Then
				                            @<col id="@anaThID" class="@headerWidthCssClass"></col>
Else
				                            @<col id="@thIDs" class="@headerWidthCssClass"></col>
End IF
		                            Next
	                            Next
                            End If
                            <thead>
                                <tr id="MainHeaderRow1">
                                    @If sportCatList IsNot Nothing AndAlso sportCatList.Count > 0 Then
                                        @For Each item In sportCatList
                                            Dim sportCat2 = sportCatList2.Where(Function(m) m.SPORTCATCD = item.SPORTCATCD)
                                            Dim M0140Count = sportCat2.Count
                                            Dim M0150Count = 0
                                            thID = "R1_" & cnt1
                                            @For Each item2 In sportCat2
                                                M0150Count = M0150Count + item2.M0150LIST.COUNT
                                            Next

                                            Dim colspanCount = M0150Count
                                            Dim colWidth = colspanCount * 100 & "px"

                                            @If colspanCount > 0 Then
                                                @<th id="@thID" style="width:@colWidth" colspan="@colspanCount">@item.SPORTCATNM</th>
End If
                                            cnt1 = cnt1 + 1
                                        Next
                                    End If
                                </tr>
                                <tr id="MainHeaderRow2">

                                    @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                        cnt1 = 0
                                        @For Each item In sportCatList2
                                            Dim colspanCount = item.M0150LIST.COUNT
                                            Dim colWidth = colspanCount * 100 & "px"

                                            If prevCatCd <> item.SPORTCATCD Then
                                                cnt1 = cnt1 + 1
                                                cnt2 = 1
                                            End If

                                            prevCatCd = item.SPORTCATCD

                                            thID = "R2_" & cnt1 & "_" & cnt2

                                            @If colspanCount > 0 Then
                                                @<th id="@thID" style="width:@colWidth" colspan="@colspanCount">@item.SPORTSUBCATNM</th>
End If
                                            cnt2 = cnt2 + 1
                                        Next
                                    End If
                                </tr>
                                <tr id="MainHeaderRow3">
                                    @If sportCatList2 IsNot Nothing AndAlso sportCatList2.Count > 0 Then
                                        cnt1 = 0
                                        cnt2 = 1
                                        prevCatCd = 0
                                        Dim headerWidthCssClass = ""
                                        @For Each item In sportCatList2
                                            If prevCatCd <> item.SPORTCATCD Then
                                                cnt1 = cnt1 + 1
                                                cnt2 = 1
                                            End If

                                            prevCatCd = item.SPORTCATCD
                                            cnt3 = 1
                                            @For Each item2 In item.M0150LIST
                                                thID = "R3_" & cnt1 & "_" & cnt2 & "_" & cnt3
                                                Dim colValue = item2.COLVALUE
                                                Dim anaThID = thID & "_anaTHHY"
                                                If item2.COLTYPE = "2" Then
                                                    headerWidthCssClass = "cat149_col"
                                                Else
                                                    headerWidthCssClass = "cat150_col"
                                                End If
                                                @if item2.HYOJ = False Then
                                                    @<th id="@anaThID" class="@headerWidthCssClass">@colValue</th>
Else
                                                    @<th id="@thID" class="@headerWidthCssClass">@colValue</th>
End IF
                                                cnt3 = cnt3 + 1
                                            Next
                                            cnt2 = cnt2 + 1
                                        Next
                                    End If
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="dataDiv" class="myDiv col-sm-12">
                        <table id="tbl-data" class="table table-bordered table-hover table-scroll" style="margin-bottom:0px;table-layout:fixed;">
                            <tbody id="myTable">
                                <!--START: dynamic value display block-->
                                @For Each dayItem In sportCatList3

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
                                            Dim headerWidthCssClass = ""
                                            For i = 0 To dayItem.Key.Count - 1
                                                Dim subCatItem As ICollection = dayItem.Key(i)
                                                Dim d0010Item As ICollection = subCatItem(d0010Cnt)
                                                Dim subcatRowSpan = 1
                                                If i = 0 Then
                                                    isNewRow = 0
                                                End If
                                                If d0010Item IsNot Nothing Then
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
                                                        'Want to Display Or Not
                                                        Dim Colid = "R4_" & i + 1 & "_" & subCatColCnt
                                                        If colItem.HYOJ2 = False Then
                                                            Colid = Colid & "_anaTDHY"
                                                        Else
                                                            Colid = Colid
                                                        End If

                                                        If colItem.COL_TYPE = "2" Then
                                                            headerWidthCssClass = "cat149_col"
                                                        Else
                                                            headerWidthCssClass = "cat150_col"
                                                        End If

                                                        If colItem.CATCD <> -1 Then

                                                            If colItem.ITEMNM <> "" Then
                                                                Dim bgcolor = ""
                                                                If colItem.COLORSTATUS > 0 Then
                                                                    bgcolor = "background-color:#ffad33;"
                                                                Else
                                                                    bgcolor = ""
                                                                End If
                                                                @<td id="@Colid" class="@headerWidthCssClass" style="@bgcolor" rowspan="@subcatRowSpan">
    @If Session("LoginUserACCESSLVLCD") = "4" Then
        @<font color="@colItem.LINKCOLOR">@colItem.ITEMNM</font>
    Else
        @If colItem.LINKCOLOR = KARIANA_LINK_COLOR Then
            @If (Session("LoginUserACCESSLVLCD") = "3" And colItem.Desk_Chief_Cat = 0) Then
                @<font color="@colItem.LINKCOLOR">@colItem.ITEMNM</font>
Else
                @Html.ActionLink(colItem.ITEMNM, "Edit", "A0220", New With {.id = colItem.GYOMNO, .lastForm = "A0230"}, htmlAttributes:=New With {.style = "color:" & colItem.LINKCOLOR})
End If

        Else
            @Html.ActionLink(colItem.ITEMNM, "Edit", "B0020", New With {.id = colItem.FIX_GYOMNO, .Form_name = "A0230"}, htmlAttributes:=New With {.style = "color:" & colItem.LINKCOLOR})
        End If
    End If
    @If colItem.AnaLIST IsNot Nothing AndAlso colItem.AnaLIST.Count > 0 Then
        For Each item2 In colItem.AnaLIST
            If item2 IsNot Nothing Then
                @<br>@<br>
                @If Session("LoginUserACCESSLVLCD") = "4" Then
                    @<font color="@item2.LINKCOLOR">@item2.ITEMNM</font>
Else
                    @Html.ActionLink(item2.ITEMNM, "Edit", "B0020", New With {.id = item2.FIX_GYOMNO, .Form_name = "A0230"}, htmlAttributes:=New With {.style = "color:" & item2.LINKCOLOR})
End If
            End If
        Next
    End if
</td>
                                                            Else
                                                                @<td id="@Colid" class="@headerWidthCssClass" rowspan="@subcatRowSpan">&nbsp;</td>
                                                            End If
                                                        Else
                                                            If colItem.ITEMNM <> "" Then
                                                                @If Session("LoginUserACCESSLVLCD") = "4" OrElse (Session("LoginUserACCESSLVLCD") = "3" And colItem.Desk_Chief_Cat = 0) Then
														            @<td id="@Colid" class="@headerWidthCssClass" rowspan="@subcatRowSpan">@colItem.ITEMNM</td>
Else
														            @<td id="@Colid" class="@headerWidthCssClass" rowspan="@subcatRowSpan">
															            @Html.ActionLink(colItem.ITEMNM, "Edit", "A0220", New With {.id = colItem.GYOMNO, .lastForm = "A0230"}, htmlAttributes:=New With {.style = "color:black"})
														            </td>   End If
                                                            Else
                                                                @<td id="@Colid" class="@headerWidthCssClass" rowspan="@subcatRowSpan">&nbsp;</td>
End If
                                                        End If
                                                    Next

                                                    If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 Then
                                                        @Html.Raw("</tr>")
End If
                                                End If
                                                If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 AndAlso d0010Cnt < rowSpan - 1 Then
                                                    i = -1
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
            </div>
        </div>End Using

</div>

<script>
                                                                    var myApp = myApp || {};
                                                        myApp.Urls = myApp.Urls || {};
                                                        myApp.Urls.baseUrl = '@Url.Content("~")';
</script>

<script type="text/javascript">



    $("form").submit(function () {
        $('#lblInfo').show();
    });

    $('#Searchdt').datepicker({
        format: "yyyy/mm",
        language: "ja",
        autoclose: true,
        minViewMode: 'months'
    });

    function SetDate(months) {
        var searchdt = $('#Searchdt').val();
        if (searchdt != "") {
            var curdates = $('#Searchdt').val().split('/');
            var newdate = new Date(curdates[0], curdates[1] - 1, '01');
            newdate.setMonth(newdate.getMonth() + months);
            var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2);
            //document.getElementById("Searchdt").value = formattedNewDate;
            $('#Searchdt').val(formattedNewDate);
        }
    }

    $('.checkbox-inline').on('click', function (e) {
        var codes = [];
        $.each($("input[name='sportCat']:checked"), function () {
            codes.push($(this).val());
        });

        document.getElementById("CatCodes").value = codes;
        //alert("My favourite sports are: " + favorite.join(", "));
        var control = this.querySelector("input[type='checkbox']");
        if (control.id == 'sportCat') {
            if (control.checked) {
                var unChkCnt = $("input[type='checkbox']:input[name='sportCat']:not(:checked)");
                if (unChkCnt.length == 0)
                    $("#flgSportCatAll").prop("checked", true);
            } else {
                $("#flgSportCatAll").prop("checked", false);
            }
        }
    });

    $('#flgSportCatAll').on('click', function (e) {
        if (!this.checked) {
            $.each($("input[name='sportCat']:checked"), function () {
                $(this).prop("checked", false);
            });
        } else {
            $.each($("input[name='sportCat']:not(:checked)"), function () {
                $(this).prop("checked", true);
            });
        }
    });

    $('#SearchFilter').on('click', function (e) {
        $(this).attr('disabled', true);
        $('#SearchAll').attr('disabled', false);
        $('#SearchAll')[0].checked = false;
        document.getElementById("SearchType").value = "1";
        $('#tbl-header tr th[id$="anaTHHY"]').hide();
        $('#tbl-data td[id$="anaTDHY"]').hide();
        $('#tbl-header [id="Col_anaTHHY"]').hide();
        var counter1 = 1;
        var counter2 = 1;
        $('#tbl-header tr th[id^="R1_"]').each(function () {
            counter1 = $(this).attr("id").substr(3);
            var TotalCountSum = 0;
            $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                counter2 = $(this).attr("id").substr(('R2_' + counter1 + '_').length);
                var TotalCount = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"][id$="_anaTHHY"]').length;
                var TotalCol = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"]').length;
                if (TotalCount > 0) {

                    var reducedTd = TotalCol - parseInt(TotalCount);

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
                    TotalCountSum = TotalCountSum + TotalCount;
                }
            });
            if (TotalCountSum > 0) {
                var reducedTd = parseInt($(this)[0].colSpan) - parseInt(TotalCountSum);
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
        setDataTrHeight_LeftToRight(0);
        SetBorder_SearchFilter(true);
        
    });

    $('#SearchAll').on('click', function (e) {
        $(this).attr('disabled', true);
        $('#SearchFilter').attr('disabled', false);
        $('#SearchFilter')[0].checked = false
        document.getElementById("SearchType").value = "0";
        $('#tbl-header [id="Col_anaTHHY"]').show();
        var counter1 = 1;
        var counter2 = 1
        $('#tbl-header tr th[id^="R1_"]').each(function () {
            counter1 = $(this).attr("id").substr(3);
            var TotalCountSum = 0;
            $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                counter2 = $(this).attr("id").substr(('R2_' + counter1 + '_').length);
                var HiddenCount = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"][id$="_anaTHHY"]').length;
                var TotalCount = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"]').length;
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
                    TotalCountSum = TotalCountSum + HiddenCount;
                }
            });
            if (TotalCountSum > 0) {
                var addedTd = TotalCountSum;
                if ($(this).css('display') == "none") {
                    addedTd = parseInt(TotalCountSum);
                } else {
                    addedTd = parseInt(TotalCountSum) + parseInt($(this)[0].colSpan);
                }

                if ($(this).css('display') == "none") {
                    $(this).show();
                }
                $(this)[0].colSpan = addedTd;
                var tdWidthAfter = $(this)[0].colSpan * 100;
                $(this).css('width', tdWidthAfter + 'px');
            }
        });
        $('#tbl-header tr th[id$="anaTHHY"]').show();
        $('#tbl-data td[id$="anaTDHY"]').show();
        setDataTrHeight_LeftToRight(0);
        SetBorder_SearchAll();
    });

    function SetBorder_SearchAll() {        

        //reset border
        $('#tbl-header tr th').each(function () {
            $(this).css('border-right', '1px');
        });
        $('#tbl-data tr td').each(function () {
            $(this).css('border-right', '1px');
        });

        var counter1 = 1;
        var counter2 = 1;
        var subCatCount = 0;
        var lastCatLength = $('#tbl-header tr th[id^="R1_"]').filter(function () {
            return $(this).css('display') != "none"
        }).length;

        $('#tbl-header tr th[id^="R1_"]').each(function () {

            var subCatId = 1;
            var SumCatDispCol = 0;
            var TotalCatDispCol = 0;

            counter1 = $(this).attr("id").substr(3);

            $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                subCatId = $(this).attr("id").substr(('R2_' + counter1 + '_').length);
                TotalCatDispCol = TotalCatDispCol + $('#tbl-header tr th[id^="R3_' + counter1 + '_' + subCatId + '_"]').length;
            });

            $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                counter2 = $(this).attr("id").substr(('R2_' + counter1 + '_').length);
                var TotalCount = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"]').length;
                var HiddenCount = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"][id$="_anaTHHY"]').length;

                SumCatDispCol = SumCatDispCol + TotalCount;
                subCatCount = subCatCount + 1;

                if (SumCatDispCol == TotalCatDispCol) {

                    if (lastCatLength != counter1) {
                        $(this).css('border-right', '3px solid gray');
                        if (HiddenCount == 0) {
                            $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_' + TotalCount + '"]').css('border-right', '3px solid gray');
                            $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + TotalCount + '"]').css('border-right', '3px solid gray');
                        } else {
                            $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_' + $(this)[0].colSpan + '"]').css('border-right', '3px solid gray');
                            $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + $(this)[0].colSpan + '"]').css('border-right', '3px solid gray');
                        }
                    }

                } else {
                    $(this).css('border-right', '2px solid gray');
                    if (HiddenCount == 0) {
                        $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_' + TotalCount + '"]').css('border-right', '2px solid gray');
                        $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + TotalCount + '"]').css('border-right', '2px solid gray');
                    } else {
                        $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_' + $(this)[0].colSpan + '"]').css('border-right', '2px solid gray');
                        $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + $(this)[0].colSpan + '"]').css('border-right', '2px solid gray');
                    }
                }
            });
            if (counter1 != lastCatLength) {
                $(this).css('border-right', '3px solid gray');
            }
        });
        AdjustHeaderHeight();
    }

    function SetBorder_SearchFilter(searchFilterselected) {        

        //reset border
        $('#tbl-header tr th').each(function () {
            $(this).css('border-right', '1px');
        });
        $('#tbl-data tr td').each(function () {
            $(this).css('border-right', '1px');
        });

        var counter1 = 1;
        var counter2 = 1;
        var subCatCount = 0;
        var lastCatLength = $('#tbl-header tr th[id^="R1_"]').filter(function () {
            return $(this).css('display') != "none"
        }).length;
        var catCount = 0;
        $('#tbl-header tr th[id^="R1_"]').each(function () {

            counter1 = $(this).attr("id").substr(3);
            var TotalHideCountSum = 0;
            var TotalCountSum = 0;
            var TotalCatDispCol = 0;
            var SumCatDispCol = 0;
            var subCatId = 1;

            $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                subCatId = $(this).attr("id").substr(('R2_' + counter1 + '_').length);
                TotalCatDispCol = TotalCatDispCol + $('#tbl-header tr th[id^="R3_' + counter1 + '_' + subCatId + '_"]:not([id$="_anaTHHY"])').length;
            });

            if (TotalCatDispCol > 0){
                catCount = catCount + 1;
            }

            $('#tbl-header tr th[id^="R2_' + counter1 + '_"]').each(function () {
                counter2 = $(this).attr("id").substr(('R2_' + counter1 + '_').length);

                var TotalCount = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"][id$="_anaTHHY"]').length;
                var TotalCol = $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"]').length;

                if (TotalCount > 0) {
                    var reducedTd = TotalCol - parseInt(TotalCount);
                }

                subCatCount = subCatCount + 1;
                TotalCountSum = TotalCountSum + TotalCol;
                SumCatDispCol = SumCatDispCol + (TotalCol - TotalCount);

                if (SumCatDispCol == TotalCatDispCol) {
                    if (lastCatLength != catCount) {
                        $(this).css('border-right', '3px solid gray');
                    }
                } else {
                    $(this).css('border-right', '2px solid gray');
                    //$('#tbl-data tr td[id^="R4_' + subCatCount + '_' + (TotalCol - TotalCount) + '"]').css('border-right', '2px solid gray');
                }

                if (TotalCount == 0) {
                    reducedTd = TotalCol;
                    if (SumCatDispCol == TotalCatDispCol) {
                        if (lastCatLength != catCount) {
                            $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_' + reducedTd + '"]').css('border-right', '3px solid gray');
                            $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + reducedTd + '"]').css('border-right', '3px solid gray');
                        }
                    } else {
                        $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_' + reducedTd + '"]').css('border-right', '2px solid gray');
                        $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + reducedTd + '"]').css('border-right', '2px solid gray');
                    }
                } else {
                    $('#tbl-header tr th[id^="R3_' + counter1 + '_' + counter2 + '_"]:not([id$="_anaTHHY"])').each(function (index) {
                        if (index + 1 == reducedTd) {
                            if (SumCatDispCol == TotalCatDispCol) {
                                if (lastCatLength != catCount) {
                                    $(this).css('border-right', '3px solid gray');
                                    $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + $(this).attr("id").substr(('R3_' + counter1 + '_' + counter2 + '_').length) + '"]').css('border-right', '3px solid gray');
                                }
                            } else {
                                $(this).css('border-right', '2px solid gray');
                                $('#tbl-data tr td[id^="R4_' + subCatCount + '_' + $(this).attr("id").substr(('R3_' + counter1 + '_' + counter2 + '_').length) + '"]').css('border-right', '2px solid gray');
                            }
                        }
                    });
                }
            });

            if (lastCatLength != catCount) {
                $(this).css('border-right', '3px solid gray');
            }

        });
        AdjustHeaderHeight(searchFilterselected);
    }

    function AdjustHeaderHeight(searchFilterselected) {

        //Dynamic set left Header height
        var MainHeaderRow1Height = $('#tbl-header tr[id="MainHeaderRow1"]')[0].offsetHeight;
        var MainHeaderRow2Height = $('#tbl-header tr[id="MainHeaderRow2"]')[0].offsetHeight;
        var MainHeaderRow3Height = $('#tbl-header tr[id="MainHeaderRow3"]')[0].offsetHeight;
        var ALL_ROW_TABLE_LEFT_LABEL_HEADER = $('#left-fixed-header tr');
        var TotalHeaderHeight = MainHeaderRow1Height + MainHeaderRow2Height + MainHeaderRow3Height;

        ALL_ROW_TABLE_LEFT_LABEL_HEADER.each(function (index, row) {
            if (index == 0) {
                $(ALL_ROW_TABLE_LEFT_LABEL_HEADER[index]).css('height', MainHeaderRow1Height);
            } else if (index == 1) {
                $(ALL_ROW_TABLE_LEFT_LABEL_HEADER[index]).css('height', MainHeaderRow2Height);
            } else {
                $(ALL_ROW_TABLE_LEFT_LABEL_HEADER[index]).css('height', MainHeaderRow3Height);
            }
        });

        if (TotalHeaderHeight == 0) {
            if(searchFilterselected)
                addHeightInLeftHeader = 0;
            else
                addHeightInLeftHeader = 2;

            var leftheaderHeightNumeric = parseInt($('.my-left-HeaderDiv').css('height').toString().substring(0, $('.my-left-HeaderDiv').css('height').toString().length - 2));
            var headerHeightNumeric = parseInt($('.myHeaderDiv').css('height').toString().substring(0, $('.myHeaderDiv').css('height').toString().length - 2));
            $($('.my-left-HeaderDiv')).css('height', (leftheaderHeightNumeric + addHeightInLeftHeader) + 'px');
            $($('.myHeaderDiv')).css('height', (headerHeightNumeric + addHeightInLeftHeader) + 'px');
        }
        else {
            $($('.my-left-HeaderDiv')).css('height', (TotalHeaderHeight + 2) + 'px');
            $($('.myHeaderDiv')).css('height', (TotalHeaderHeight + 2) + 'px');
        }
    }

    $(document).ready(function () {

        var unChkCnt = $("input[type='checkbox']:input[name='sportCat']:not(:checked)");
        if (unChkCnt.length == 0)
            $("#flgSportCatAll").prop("checked", true);

        if ($("input[type=radio][name='SearchFilter']").prop('checked') == true) {
            $('#SearchFilter').click();
        }

        if ($("input[type=radio][name='SearchAll']").prop('checked') == true) {
            $('#SearchAll').attr('disabled', true);
            $('#SearchFilter').attr('disabled', false);
            $('#SearchFilter')[0].checked = false
            document.getElementById("SearchType").value = "0";
            SetBorder_SearchAll();
        }

        $(".header").click(function (evt) {

            //$("#lblInfo").text("処理中です。しばらくお待ち下さい。。。")
            //document.getElementById('lblInfo').style.color = 'orange';
            var table = document.getElementById("tbl-data");
            var rows = table.getElementsByTagName("tr");

            if (rows.length < 3) {
                return
            }

            evt.preventDefault();

            $("#myForm").submit();
        });

        //表示スポーツカテゴリ by default check all
        $('#sportflgNo').click();

        setDataTrHeight_LeftToRight(0);

    });

    function setDataTrHeight_LeftToRight(addHeight) {
        //dynamic table CELL height set [left-header]
        var ALL_ROW_TABLE_DATA = $('#dataDiv tr');
        var ALL_ROW_TABLE_LEFT_HEADER = $('#left-fixed-data tr');
        /*var LEFT_HEADER_DIV_HEIGHT = $('.my-left-dataDiv').height();
         var RIGHT_TABLE_DATA_DIV_HEIGHT = $('.myDiv').height();*/
        var LEFT_HEADER_DIV_HEIGHT = parseInt($(".my-left-dataDiv").css("max-height").toString().substring(0, $(".my-left-dataDiv").css("max-height").toString().length - 2));
        var RIGHT_TABLE_DATA_DIV_HEIGHT = parseInt($(".myDiv").css("max-height").toString().substring(0, $(".myDiv").css("max-height").toString().length - 2));

        //Logic for create dynamic left data header height
        var lastID = "0";
        var thIndex = 0;
        ALL_ROW_TABLE_DATA.each(function (index, row) {
            var val = this.id
            if (val != lastID) {
                var rows = $('#dataDiv tr[id="' + val + '"]')
                var heightSum = 0;

                // Internet Explorer 6-11
                var isIE = !!document.documentMode;
                // Firefox 1.0+
                var isFirefox = typeof InstallTrigger !== 'undefined';
                // Edge 20+
                var isEdge = !isIE && !!window.StyleMedia;

                rows.each(function (index, row) {
                    if (isIE || isFirefox || isEdge) {
                        //ASI[17 Dec 2019]:Height of both side's Tr not exact in IE, FireFox, Edge.
                        //To do so, add addiional 0.17px in each Tr height
                        if (rows.length > 1) {
                            addHeight = 0.17
                        }
                        heightSum = heightSum + $(this).height() + addHeight;
                    } else {
                        heightSum = heightSum + $(this).height();
                    }
                });

                if ($('#left-fixed-data tr').eq(thIndex)[0].offsetHeight != heightSum) {
                    $(ALL_ROW_TABLE_LEFT_HEADER[thIndex]).css('height', heightSum);
                }
                thIndex = thIndex + 1;
            }

            lastID = val;

        });

        $($('.my-left-dataDiv')).css('height', LEFT_HEADER_DIV_HEIGHT + 'px');
        $($('.myDiv')).css('height', RIGHT_TABLE_DATA_DIV_HEIGHT + 'px');
    }

    function KeyUpFunction() {
        var searchdt = $('#Searchdt').val()
        var viewdate = $('#viewdatadate').val()

        //document.getElementById("Searchdt").value = searchdt;

        if (searchdt != "") {
            if (searchdt.length == 7) {
                if (searchdt != viewdate) {
                    $("#myForm").submit();
                }
            }
        }
    }

    //Havan[20 Dec 2019]: Hide PrintBtn for 2nd Release. after release uncomment this line and start implementation.
    //Comment PrintDiv() and afterPrint() functions.

    /*function PrintDiv() {
        var divContents = document.getElementById("mycontent").innerHTML;

        //Create left header div
        $("#MainHeaderRow1 th:nth-child(1)").before("<th width=50px></th><th width=30px></th>");
        $("#MainHeaderRow2 th:nth-child(1)").before("<th width=50px></th><th width=30px></th>");
        $("#MainHeaderRow3 th:nth-child(1)").before("<th width=50px>日付</th><th width=30px>曜</th>");

        //Getting left detail part data
        var dateArray = [];
        var dayArray = [];
        $('#left-fixed-data tr').each(function() {
            var date = $(this).find(".col-1").html();
            dateArray.push(date);
            var day = $(this).find(".col-2").html();
            dayArray.push(day);
        });

        for (var i = 0; i < $("#myTable tr").length; i++) {

            var month = dateArray[i];
            var day = dayArray[i];
            var addHtml = ""
            var id = 'ROW:' + (i + 1);

            //Create left data div correspond to data div
            if ($('#dataDiv tr[id="' + id + '"]').length > 1) {
                var rowspan = $('#dataDiv tr[id="' + id + '"]').length
                var addHtml = "<th width=50px rowspan="+rowspan+">"+month+"</th><th width=30px rowspan="+rowspan+">"+day+"</th>"
                $('#dataDiv tr[id="' + id + '"]:first > td:nth-child(1)').before(addHtml);
            } else {
                addHtml = "<th width=50px>"+month+"</th><th width=30px>"+day+"</th>"
                $('#dataDiv tr[id="' + id + '"] > td:nth-child(1)').before(addHtml);
            }

            //Remove llink content
            $('#dataDiv tr[id="' + id + '"] > td > a').each(function(){
                $(this).removeAttr("href");
            });

            $('#dataDiv tr[id="' + id + '"] > td').each(function(){
                $(this).css('width', 100 + 'px');
            });
        }



        var divContents = document.getElementById("mycontent").innerHTML;

        var url = myApp.Urls.baseUrl + 'Content/C0040.css';
        document.body.innerHTML = '<html><head><link rel="stylesheet"  type="text/css" media="print" href=' + url + '></head><body>' + divContents + '</body></html>'

        window.print();

        afterPrint()
    }


    function afterPrint() {

        setTimeout(function () { document.location.href = window.location.href; }, 250);
    }*/

    //Scroll
    $("#dataDiv").scroll(function () {
        $(".my-left-dataDiv").scrollTop($("#dataDiv").scrollTop());
        $("#headerDiv").scrollLeft($("#dataDiv").scrollLeft());
    });


                                                        ////検索終わったら、処理中メッセージをクリア
                                                        //jQuery(window).load(function () {
                                                        //    setTimeout(function () {
                                                        //        document.getElementById('lblInfo').style.color = 'white';
                                                        //    }, 1000);
                                                        //});

</script>
