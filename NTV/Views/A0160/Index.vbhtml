@ModelType IEnumerable(Of NTV_SHIFT.D0070)
@Code
    ViewData("Title") = "業務変更履歴"
    Dim bolfirst As Boolean = False
    Dim bolIKTFLG As Boolean = False
    Dim info = ViewBag.SortingPagingInfo
End Code

<div>
    @Html.Partial("_MenuPartial", "6")
</div>

<style>
    /*.table-bordered th {
        white-space: nowrap;
    }

    .table-bordered td {
        white-space: nowrap;
    }*/

    /*.table-scroll {
        width: 2155px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 300px;
        width: 2155px;
        overflow-y: auto;
        overflow-x: auto;
    }*/


    table.table-scroll {
        display: block;
    }


    .colHENKONAIYO {
        width: 90px;
        max-width: 90px;
    }

    .colUSERID {
        width: 80px;
        max-width: 80px;
    }

    .colSYUSEIYMD {
        width: 100px;
        max-width: 100px;
    }

    .colGYOMYMD {
        width: 170px;
        max-width: 170px;
    }

    .colGYOMYMD1, .colGYOMYMD2 {
        width: 85px;
        max-width: 85px;
    }

    .colKSKJKNST {
        width: 100px;
        max-width: 100px;
    }

    .colKSKJKNST1, .colKSKJKNST2 {
        width: 50px;
        max-width: 50px;
    }

    .colOAJKNST, .colSAIJKNST {
        width: 100px;
        max-width: 100px;
    }

    .colOAJKNST1, .colOAJKNST2, .colSAIJKNST1, .colSAIJKNST2 {
        width: 50px;
        max-width: 50px;
    }

    .colCATCD{
        width: 110px;
        max-width: 110px;
    }

     .colSPORTCATCD {
        width: 120px;
        max-width: 120px;
    }

    .colSPORTSUBCATCD {
        width: 140px;
        max-width: 140px;
    }

    .colBANGUMINM, .colNAIYO {
        width: 100px;
        max-width: 100px;
    }

    .colBASYO {
        width: 150px;
        max-width: 150px;
    }

    .colTNTNM {
        width: 200px;
        max-width: 200px;
    }


    .colBANGUMITANTO {
        width: 150px;
        max-width: 150px;
    }

    .colBANGUMIRENRK {
        width: 150px;
        max-width: 150px;
    }

    .colBIKO {
        width: 100px;
        max-width: 100px;
    }

    .colIKTFLG {
        width: 160px;
        max-width: 160px;
    }

    .table-scroll td {
        word-wrap: break-word;
    }

    body {
        font-size: 12px;
    }

    table.scroll tbody,
    table.scroll thead {
        display: block;
    }

    table.scroll thead {
        width: 2196px;
    }

    table.scroll tbody {
        height: 370px;
        width: 2198px;
        overflow-y: auto;
    }

    .myHeaderDiv {
        max-width: 1890px;
        overflow-x: hidden;
        overflow-y: scroll;
        z-index: 1;
        padding-left: 0px;
        height: 35px;
        max-height: 35px;
    }

    .myDiv {
        max-width: 1890px;
        height: 400px;
        overflow-y: scroll;
        overflow-x: scroll;
        position: relative;
        padding-left: 0px;
    }

    #tblSearchResult.table {
        position: relative;
        border-collapse: collapse;
        margin-bottom: 0px;
    }
</style>

<div class="container-fluid">
    @*<div class="row">
            <div class="col-md-12 col-md-push-5" style="padding-top:10px">
                <label style="font-size:15px;">※8日以前のシフトの変更履歴はありません。</label>
            </div>

        </div>*@

    <div class="row">

        @Using Html.BeginForm("Index", "A0160", FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})
            @Html.Hidden("SortField", info.SortField)
            @Html.Hidden("SortDirection", info.SortDirection)
            @Html.Hidden("viewdatadate", ViewData("searchdt"))
            @<p>

                <div class="col-md-4" style="padding-top:5px;">

                    <label class="radio-inline">
                        @Html.RadioButton("Shiftdt", "True", True)
                        <label>シフトの日付で検索&nbsp;</label>
                    </label>
                    <label class="radio-inline">
                        @Html.RadioButton("Shiftdt", "False")
                        <label>作業日で検索&nbsp;</label>
                    </label>

                </div>
                <ul class="nav nav-pills ">

                    @*<li><a href="#" onclick="SetDate(-1)">前日</a></li>*@
                    <li>@*<button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(-1)">前日</button>*@</li>
                    <li><input type="button" class="btn btn-success btn-sm btnForward" disabled="disabled" style="background:white; color:green" value="前日" onclick="SetDate(-1)"></li>
                    <li>
                        <div class="input-group">
                            <input id="searchdt" name="searchdt" type="text" class="form-control input-sm date imedisabled" value=@ViewData("searchdt") onchange="KeyUpFunction()" style="width:120px;font-size:small;">

                        </div>
                    </li>
                    @*<li><a href="#" onclick="SetDate(1)">翌日</a></li>*@
                    <li>@*<button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(1)">翌日</button>*@</li>
                    <li><input type="button" class="btn btn-success btn-sm btnNext" disabled="disabled" style="background:white; color:green" value="翌日" onclick="SetDate(1)"></li>
                    <li>@*<button type="submit" class="btn btn-success btn-sm btnSearch" id="btnSearch1">表示</button>*@</li>
                    <li><input type="button" class="btn btn-success btn-sm btnSearch" disabled="disabled" value="表示"></li>
                    <li><label style="font-size:15px; padding-left:20px; padding-top:5px;">※8日以前のシフトの変更履歴はありません。</label></li>

                </ul>

            </p>

        End Using

    </div>

    @Using (Html.BeginForm())
        @<p>
            @*@Html.ActionLink("CSV出力", "DownloadCsv", "B0020", Nothing, htmlAttributes:=New With {.name = "DownloadCsv", .class = "btn btn-success btn-sm"})*@
            <button id="btnDownloadCsv" type="submit" class="btn btn-success btn-sm" name="button" value="downloadcsv" style="margin-top:-10px;" disabled="disabled">CSV出力</button>

            <label style="font-size:15px;text-align:right; color:orange;padding-left:20px;" id="lblInfo">処理中です。しばらくお待ち下さい。。。</label>
        </p>
        @<p></p>

        @<p>
             <div Class="row" style="max-width: 1850px;padding-left:0px;padding-right:0px;border:1px solid #ecf0f1;">
                 <div id="headerDiv" class="myHeaderDiv col-md-12">
                     <table id="tbl-header" class="table table-bordered table-hover" style="margin-bottom:0px;table-layout:fixed;">
                         <thead>
                             <tr>
                                 <th class="colHENKONAIYO">
                                     <a href="#" data-sortfield="HENKONAIYO"
                                        class="header">@Html.DisplayNameFor(Function(model) model.HENKONAIYO)</a>
                                 </th>
                                 <th class="colUSERID">
                                     <a href="#" data-sortfield="USERNM"
                                        class="header">@Html.DisplayNameFor(Function(model) model.USERID)</a>
                                 </th>
                                 <th class="colSYUSEIYMD">
                                     <a href="#" data-sortfield="SYUSEIYMD"
                                        class="header">@Html.DisplayNameFor(Function(model) model.SYUSEIYMD)</a>
                                 </th>
                                 <th class="colGYOMYMD">
                                     <a href="#" data-sortfield="GYOMYMD"
                                        class="header">@Html.DisplayNameFor(Function(model) model.GYOMYMD)</a>
                                 </th>

                                 <th class="colKSKJKNST">
                                     <a href="#" data-sortfield="KSKJKNST"
                                        class="header">@Html.DisplayNameFor(Function(model) model.KSKJKNST)</a>
                                 </th>
                                 <th class="colCATCD">
                                     <a href="#" data-sortfield="CATNM"
                                        class="header">@Html.DisplayNameFor(Function(model) model.CATCD)</a>
                                 </th>

                                 <th class="colBANGUMINM">
                                     <a href="#" data-sortfield="BANGUMINM"
                                        class="header">@Html.DisplayNameFor(Function(model) model.BANGUMINM)</a>
                                 </th>
                                 <th class="colOAJKNST">
                                     <a href="#" data-sortfield="OAJKNST"
                                        class="header">@Html.DisplayNameFor(Function(model) model.OAJKNST)</a>
                                 </th>

                                 <th class="colNAIYO">
                                     <a href="#" data-sortfield="NAIYO"
                                        class="header">@Html.DisplayNameFor(Function(model) model.NAIYO)</a>
                                 </th>
                                 <th class="colBASYO">
                                     <a href="#" data-sortfield="BASYO"
                                        class="header">@Html.DisplayNameFor(Function(model) model.BASYO)</a>
                                 </th>
                                 <th class="colTNTNM">
                                     <a href="#" data-sortfield="TNTNM"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.TNTNM)</a>
                                 </th>

                                 <th class="colBANGUMITANTO">
                                     <a href="#" data-sortfield="BANGUMITANTO"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.BANGUMITANTO)</a>
                                 </th>
                                 <th class="colBANGUMIRENRK">
                                     <a href="#" data-sortfield="BANGUMIRENRK"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)</a>
                                 </th>

                                 <th class="colBIKO">
                                     <a href="#" data-sortfield="BIKO"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.BIKO)</a>
                                 </th>

                                 <th class="colIKTFLG">
                                     <a href="#" data-sortfield="IKTFLG"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.IKTFLG)</a>
                                 </th>
                                 <th class="colSPORTCATCD">
                                     <a href="#" data-sortfield="SPORTCATCD"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.SPORTCATCD)</a>
                                 </th>
                                 <th class="colSPORTSUBCATCD">
                                     <a href="#" data-sortfield="SPORTSUBCATCD"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.SPORTSUBCATCD)</a>
                                 </th>
                                 <th class="colSAIJKNST">
                                     <a href="#" data-sortfield="SAIJKNST"
                                        class=" header">@Html.DisplayNameFor(Function(model) model.SAIJKNST)</a>
                                 </th>
                             </tr>
                         </thead>
                     </table>
                 </div>
                 <div id="dataDiv" class="myDiv col-sm-12">
                     <table id="tblSearchResult" class="table table-bordered table-hover" style="margin-bottom:0px;table-layout:fixed;">
                         <tbody id="myTable">

                             @For i As Integer = 0 To Model.Count - 1
                                 Dim key As String = String.Format("lstd0070[{0}].", i)
                                 Dim item = Model(i)
                                 bolIKTFLG = False
                 @If item.IKTFLG IsNot Nothing AndAlso item.IKTFLG = True Then
                     bolIKTFLG = True
                 End If
                                 Dim firstrowid As String = ""
                                 If i = 0 OrElse i Mod 50 = 0 Then
                                     firstrowid = "firstrow"
                                 End If
             @<tr class=@firstrowid>
                 <td class="colHENKONAIYO">
                     @Html.DisplayFor(Function(modelItem) item.HENKONAIYO)
                     @Html.Hidden(key + "HENKONAIYO", item.HENKONAIYO)
                 </td>
                 <td class="colUSERID">
                     @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                     @Html.Hidden(key + "M0010.USERNM", item.M0010.USERNM)
                 </td>

                 <td class="colSYUSEIYMD">
                     @Html.DisplayFor(Function(modelItem) item.SYUSEIYMD)
                     @Html.Hidden(key + "SYUSEIYMD", item.SYUSEIYMD)
                 </td>
                 <td class="colGYOMYMD1">
                     @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
                     @Html.Hidden(key + "GYOMYMD", item.GYOMYMD)
                 </td>
                 <td class="colGYOMYMD2">
                     @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
                     @Html.Hidden(key + "GYOMYMDED", item.GYOMYMDED)
                 </td>

                 <td class="colKSKJKNST1">
                     @Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(2, 2)
                     @Html.Hidden(key + "KSKJKNST", item.KSKJKNST)
                 </td>
                 <td class="colKSKJKNST2">
                     @Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(2, 2)
                     @Html.Hidden(key + "KSKJKNED", item.KSKJKNED)
                 </td>

                 <td class="colCATCD">
                     @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                     @Html.Hidden(key + "M0020.CATNM", item.M0020.CATNM)

                 </td>

                 <td class="colBANGUMINM">
                     @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                     @Html.Hidden(key + "BANGUMINM", item.BANGUMINM)
                 </td>
                 @If item.OAJKNST IsNot Nothing Then
             @<td class="colOAJKNST1">
                 @Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(2, 2)
                 @Html.Hidden(key + "OAJKNST", item.OAJKNST)
             </td>
Else
             @<td class="colOAJKNST1"></td>
End If
                 @If item.OAJKNED IsNot Nothing Then
             @<td class="colOAJKNST2">
                 @Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(2, 2)
                 @Html.Hidden(key + "OAJKNED", item.OAJKNED)
             </td>
Else
             @<td class="colOAJKNST2"></td>
End If
                 <td class="colNAIYO">
                     @Html.DisplayFor(Function(modelItem) item.NAIYO)
                     @Html.Hidden(key + "NAIYO", item.NAIYO)
                 </td>
                 <td class="colBASYO">
                     @Html.DisplayFor(Function(modelItem) item.BASYO)
                     @Html.Hidden(key + "BASYO", item.BASYO)
                 </td>
                 <td class="colTNTNM">
                     @Html.DisplayFor(Function(modelItem) item.TNTNM)
                     @Html.Hidden(key + "TNTNM", item.TNTNM)
                 </td>
                 <td class="colBANGUMITANTO">
                     @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                     @Html.Hidden(key + "BANGUMITANTO", item.BANGUMITANTO)
                 </td>
                 <td class="colBANGUMIRENRK">
                     @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
                     @Html.Hidden(key + "BANGUMIRENRK", item.BANGUMIRENRK)
                 </td>
                 <td class="colBIKO">
                     @Html.DisplayFor(Function(modelItem) item.BIKO)
                     @Html.Hidden(key + "BIKO", item.BIKO)
                 </td>

                 <td class="colIKTFLG">
                     @*@Html.CheckBox("IKTFLG", bolIKTFLG, htmlAttributes:=New With {.disabled = "disabled"})*@
                     @Html.DisplayFor(Function(modelItem) item.IKTFLG)
                     @Html.Hidden(key + "IKTFLG", item.IKTFLG)
                 </td>
                 @If item.M0130 IsNot Nothing Then
             @<td class="colSPORTCATCD">
                 @Html.DisplayFor(Function(modelItem) item.M0130.SPORTCATNM)
                 @Html.Hidden(key + "M0130.SPORTCATNM", item.M0130.SPORTCATNM)
             </td>
Else
             @<td class="colSPORTCATCD"></td>
End If
                 @If item.M0140 IsNot Nothing Then
             @<td class="colSPORTSUBCATCD">
                 @Html.DisplayFor(Function(modelItem) item.M0140.SPORTSUBCATNM)
                 @Html.Hidden(key + "M0140.SPORTSUBCATNM", item.M0140.SPORTSUBCATNM)
             </td>
Else
             @<td class="colSPORTSUBCATCD"></td>
End If
                 @If item.SAIJKNST IsNot Nothing Then
             @<td class="colSAIJKNST1">
                 @Html.DisplayFor(Function(modelItem) item.SAIJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.SAIJKNST).ToString.Substring(2, 2)
                 @Html.Hidden(key + "SAIJKNST", item.SAIJKNST)
             </td>
Else
             @<td class="colSAIJKNST1"></td>
End If
                 @If item.SAIJKNED IsNot Nothing Then
             @<td class="colSAIJKNST2">
                 @Html.DisplayFor(Function(modelItem) item.SAIJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.SAIJKNED).ToString.Substring(2, 2)
                 @Html.Hidden(key + "SAIJKNED", item.SAIJKNED)
             </td>
Else
             @<td class="colSAIJKNST2"></td>
End If

             </tr>
Next
                         </tbody>                       
                    </table>
                </div>
            </div>
        </p>
    End Using


    <div class="row" style="margin-top:-30px;">

        <div class="col-md-12  col-md-pull-1 text-center">
            <ul class="pagination" id="myPager">
                @*<li><a href="#" id="NoOne" style="visibility:visible">1</a></li>*@
            </ul>
        </div>

    </div>

</div>


<script type="text/javascript">

    //Scroll
    $("#dataDiv").scroll(function () {
        $("#headerDiv").scrollLeft($("#dataDiv").scrollLeft());
    });

    function SetDate(days) {

        $("#lblInfo").text("処理中です。しばらくお待ち下さい。。。")
        document.getElementById('lblInfo').style.color = 'orange';

        var curdates = $('#searchdt').val().split('/');
        var newdate = new Date(curdates[0], curdates[1] - 1, curdates[2]);
        newdate.setDate(newdate.getDate() + days);
        var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2) + '/' + ('0' + newdate.getDate()).slice(-2);
        $('#searchdt').val(formattedNewDate);
        setTimeout(function () {
            $("#myForm").submit();
        }, 100);
    }


    $('#btnDownloadCsv').on('click', function (e) {

        var len = $("#tblSearchResult tbody").children().length;
        if (len == 0) {
            alert("対象データが一件も存在しません。検索を行ってください。");
            return false
        }

    });


    $(document).on('click', '.btnSearch', function () {

        $('.btnSearch').attr("disabled", "disabled");
        $('.btnForward').attr("disabled", "disabled");
        $('.btnNext').attr("disabled", "disabled");
        $('#btnDownloadCsv').attr("disabled", "disabled");
        document.getElementById('lblInfo').style.color = 'orange';

        setTimeout(function () {
            $("#myForm").submit();
        }, 100);
    });

    $(document).on('click', '.btnForward', function () {

        $('.btnSearch').attr("disabled", "disabled");
        $('.btnForward').attr("disabled", "disabled");
        $('.btnNext').attr("disabled", "disabled");
        $('#btnDownloadCsv').attr("disabled", "disabled");

    });

    $(document).on('click', '.btnNext', function () {

        $('.btnSearch').attr("disabled", "disabled");
        $('.btnForward').attr("disabled", "disabled");
        $('.btnNext').attr("disabled", "disabled");
        $('#btnDownloadCsv').attr("disabled", "disabled");

    });

    $(document).ready(function () {

        $(".header").click(function (evt) {

            $("#lblInfo").text("処理中です。しばらくお待ち下さい。。。")
            document.getElementById('lblInfo').style.color = 'orange';
            var headerTable = document.getElementById("tbl-header");
            var dataTable = document.getElementById("tblSearchResult");
            var rows = dataTable.getElementsByTagName("tr");

            if (rows.length < 2) {
                return
            }

            var sortfield = $(evt.target).data("sortfield");

            if ($("#SortField").val() == sortfield) {
                if ($("#SortDirection").val() == "ascending") {
                    $("#SortDirection").val("descending");
                }
                else {
                    $("#SortDirection").val("ascending");
                }
            }
            else {
                $("#SortField").val(sortfield);
                $("#SortDirection").val("ascending");
            }
            evt.preventDefault();

            $("#myForm").submit();
        });

        $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 50 });


    });

    function KeyUpFunction() {

        var searchdt = $('#searchdt').val()
        var viewdate = $('#viewdatadate').val()

        if (searchdt != "") {

            if (searchdt.length == 10) {

                if (searchdt != viewdate) {
                    $("#lblInfo").text("処理中です。しばらくお待ち下さい。。。")
                    document.getElementById('lblInfo').style.color = 'orange';
                    $("#myForm").submit();
                    $('.btnSearch').attr("disabled", "disabled");
                    $('.btnForward').attr("disabled", "disabled");
                    $('.btnNext').attr("disabled", "disabled");
                    $('#btnDownloadCsv').attr("disabled", "disabled");
                }

            }
        }

    }

    //検索終わったら、処理中メッセージをクリア
    jQuery(window).load(function () {


        setTimeout(function () {
            document.getElementById('lblInfo').style.color = 'white';
            $('.btnSearch').removeAttr("disabled");
            $('.btnForward').removeAttr("disabled");
            $('.btnNext').removeAttr("disabled");
            $('#btnDownloadCsv').removeAttr("disabled", "disabled");
        }, 1000);
    });


    $.fn.pageMe = function (opts) {
        var $this = this,
            defaults = {
                perPage: 7,
                showPrevNext: false,
                numbersPerPage: 5,
                hidePageNumbers: false
            },
            settings = $.extend(defaults, opts);

        var listElement = $this;
        var perPage = settings.perPage;
        var children = listElement.children();
        var pager = $('.pagination');

        if (typeof settings.childSelector != "undefined") {
            children = listElement.find(settings.childSelector);
        }

        if (typeof settings.pagerSelector != "undefined") {
            pager = $(settings.pagerSelector);
        }

        var numItems = children.size();
        var numPages = Math.ceil(numItems / perPage);

        pager.data("curr", 0);

        if (settings.showPrevNext) {
            $('<li><a href="#" class="first_link">＜＜最初</a></li>').appendTo(pager);
            //$('<li><a href="#" class="prev_link">前へ</a></li>').appendTo(pager);
        }

        var curr = 0;
        while (numPages > curr && (settings.hidePageNumbers == false)) {
            $('<li><a href="#" class="page_link">' + (curr + 1) + '</a></li>').appendTo(pager);
            curr++;
        }

        if (settings.numbersPerPage > 1) {
            $('.page_link').hide();
            $('.page_link').slice(pager.data("curr"), settings.numbersPerPage).show();
            //$('.page_link').slice(pager.data("curr"), pager.data("curr") + 1).show();
        }

        if (settings.showPrevNext) {
            if (numPages > settings.numbersPerPage) {
                //$('<li><a href="#" class="next_link">次へ</a></li>').appendTo(pager);
                $('<li><a href="#" class="last_link">最後＞＞</a></li>').appendTo(pager);
            }
        }

        pager.find('.page_link:first').addClass('active');
        pager.find('.prev_link').hide();
        pager.find('.first_link').hide();
        if (numPages <= 1) {
            pager.find('.next_link').hide();
            pager.find('.last_link').hide();
        }
        pager.children().eq(1).addClass("active");

        children.hide();
        children.slice(0, perPage).show();

        pager.find('li .page_link').click(function () {
            var clickedPage = $(this).html().valueOf() - 1;
            goTo(clickedPage, perPage);
            return false;
        });
        pager.find('li .prev_link').click(function () {
            previous();
            return false;
        });
        pager.find('li .next_link').click(function () {
            next();
            return false;
        });

        pager.find('li .first_link').click(function () {
            first();
            return false;
        });

        pager.find('li .last_link').click(function () {
            last();
            return false;
        });

        function previous() {
            var goToPage = parseInt(pager.data("curr")) - 1;
            goTo(goToPage);
        }

        function next() {
            goToPage = parseInt(pager.data("curr")) + 1;
            goTo(goToPage);
        }

        function first() {
            var goToPage = 0;
            goTo(goToPage);
        }

        function last() {
            var goToPage = numPages - 1;
            goTo(goToPage);
        }

        function goTo(page) {
            var startAt = page * perPage,
                endOn = startAt + perPage;

            children.css('display', 'none').slice(startAt, endOn).show();

            pager.data("curr", page);

            if (numPages > settings.numbersPerPage) {
                if (page >= 1) {
                    pager.find('.prev_link').show();
                    pager.find('.first_link').show();
                }
                else {
                    pager.find('.prev_link').hide();
                    pager.find('.first_link').hide();
                }

                if (page < (numPages - 1)) {
                    pager.find('.next_link').show();
                    pager.find('.last_link').show();
                }
                else {
                    pager.find('.next_link').hide();
                    pager.find('.last_link').hide();
                }

                if (page == 0) {
                    $('.page_link').hide();
                    $('.page_link').slice(page, settings.numbersPerPage + page).show();
                }
                else {
                    if (settings.numbersPerPage > 1) {
                        $('.page_link').hide();

                        if (page >= 2) {
                            $('.page_link').slice(page, settings.numbersPerPage - 2 + page).show();
                            $('.page_link').slice(page - 2, page).show();
                        }
                        else {
                            $('.page_link').slice(page, settings.numbersPerPage - 1 + page).show();
                            $('.page_link').slice(page - 1, page).show();
                        }
                    }
                }

                var total = settings.numbersPerPage + page

                if (total > numPages) {
                    var extrashow = total - numPages
                    $('.page_link').slice(page - extrashow, page).show();
                }


            }


            pager.children().removeClass("active");
            pager.children().eq(page + 1).addClass("active");

            //ページをクリックする時、そのページの一番目の行にフォーカスする
            var row = $('.firstrow');
            row.find('td:first').focus();

        }
    };
</script>