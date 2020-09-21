@Imports Microsoft.AspNet.Identity
@ModelType IEnumerable(Of NTV_SHIFT.D0010)
@Code
    ViewData("Title") = "担当表"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim Categorys = DirectCast(ViewBag.CategList, List(Of M0020))
    Dim Kyukas = DirectCast(ViewBag.KyukaList, List(Of M0060))

    Dim Holidays = DirectCast(ViewBag.HolidayList, List(Of D0040))
    Dim bolDataHave = False
    Dim loopCount = 0
    Dim strKyuLink As String = ""
    Dim strCatLink As String = ""
End Code



<style>

    .table-bordered.tableCat {
        width: auto;
    }

    .colGYOMYMD {
        width: 150px;
    }

    .colGYOMYMD1, .colGYOMYMD2 {
        width: 75px;
    }

    .colKSKJKNST {
        width: 70px;
    }

    .colKSKJKNST1, .colKSKJKNST2 {
        width: 35px;
    }

    .colCATNM {
        width: 110px;
    }

    .colBANGUMINM {
        width: 130px;
    }

    .colBASYO {
        width: 100px;
    }

    .colBANGUMITANTO {
        width: 80px;
    }

    .colBIKO {
        width: 100px;
    }

    .colNAIYO {
        width: 100px;
    }
    .colAna {
        width: 120px;
    }

    .colLink {
        width: 50px;
    }

    .colBANGUMIRENRK {
        width: 105px;
    }


    .colOAJKNST {
        width: 70px;
    }

    .colOAJKNST1, .colOAJKNST2 {
        width: 35px;
    }


    .table-bordered.tableKyuKa {
        width: auto;
    }

    .colLink {
        width: 45px;
    }


    .colUSERNM {
        width: 120px;
    }

    .colSTTIME, .colEDTIME {
        width: 70px;
    }

    .nav > li.infoform > a:hover, .nav > li.infoform > a:focus {
        background-color: dodgerblue;
    }

    body {
        position: relative;
        overflow-x: hidden;
        overflow-y: auto;
    }

    html {
        overflow: auto;
        width: 100%
    }

    body {
        font-size: 12px;
    }
</style>
<div class="row " style="background-color:lightyellow; ">

    <div class="col-md-2" id="sidebar">
        <div class="affixdiv" data-spy="affix">
            <div style="padding-top:15px;">
                <a class="btn  btn-xs btn-info" role="button" data-toggle="collapse" href="#collapseExample" aria-expanded="false" aria-controls="collapseExample">
                    凡例表示/非表示
                </a>
                <div class="collapse" id="collapseExample">
                    @Html.Partial("_CategoryListPartial", ViewData.Item("ColorList"))
                </div>
            </div>

            <div style="padding-top:10px;">
                <label>アナウンサー一覧</label>
                @Html.Partial("_UserListParital", ViewData.Item("UserList"))
            </div>
        </div>
    </div>

    <div class="col-md-10" style="background-color:white; ">
        <div id="headermenus" class="affix" style="z-index:1; padding-top:15px;padding-right:15px; margin-left:-12px; background-color:white">
            @Using Html.BeginForm("Index", "C0030", routeValues:=Nothing, method:=FormMethod.Get, htmlAttributes:=New With {.id = "C0030Index"})
                @Html.Hidden("viewdatadate", ViewData("searchdt"))
                @*@Html.Hidden("msgShow", ViewBag.MsgBoxShowInfo)*@
                @Html.Hidden("msgShow", Session("msgShow"))
                @<div class="row">
                    <div class="col-md-6 col-md-push-5">
                        <ul class="nav nav-pills navbar-right">
                            <li><a href="javascript:PrintDiv();">印刷</a></li>
                            <li><a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>
                            <li><a href="#" id="EnDisColMsgBox1">伝言板表示/非表示</a></li>
                            @*<li><a href="#">戻る</a></li>*@
                            <li>@Html.ActionLink("戻る", Nothing, Nothing, Nothing, New With {.href = Request.UrlReferrer})</li>
                        </ul>
                    </div>

                    <div class="col-md-6 col-md-pull-6">
                        <ul class="nav nav-pills ">
                            <li>@*<a href="#" onclick="$('#Searchdt').val('@DateTime.Today.ToString("yyyy/MM/dd")')">本日</a>*@</li>
                            <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="$('#Searchdt').val('@DateTime.Today.ToString("yyyy/MM/dd")')">本日</button></li>
                            <li>@*<a href="#" onclick="SetDate(-1)">前日</a>*@</li>
                            <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(-1)">前日</button></li>
                            <li>
                                <div class="input-group">
                                    <input id="Searchdt" name="Searchdt" type="text" class="form-control input-sm date imedisabled" value=@Html.Encode(ViewData("searchdt")) onchange="KeyUpFunction()" style="width:120px;font-size:small;">
                                    @*<span class="input-group-addon input-sm">
                                            <span class="glyphicon glyphicon-th"></span>
                                        </span>*@
                                </div>
                            </li>
                            <li><div id="Day" style="font-size:20px; text-align:center; margin-top:4px;"></div></li>
                            <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(1)">翌日</button></li>
                            <li><button type="submit" class="btn btn-success btn-sm">表示</button></li>
                        </ul>
                    </div>


                </div>

            End Using

            @*リンクIDをカテゴリーか休暇の名前にすると文字化けの時エラーでるため、コードに変更。　カテゴリーと休暇コードで同じコードがあったら*@
            @*リンクをクリックの時うまく行かないため、カテゴリーにはC_休暇にはK_を前に付ける*@
            <ul class="breadcrumb " style="margin-bottom:5px;margin-top:5px;">
                @For Each item In Categorys
                    strCatLink = "C_" & item.CATCD
                    @<li style="cursor: pointer"><a class="catglink" id=@strCatLink>@item.CATNM</a></li>
                Next
                @For Each item In Kyukas
                    strKyuLink = "K_" & item.KYUKCD
                    @<li style="cursor: pointer"><a class="catglink" id=@strKyuLink>@item.KYUKNM</a></li>
Next
            </ul>

        </div>

        <div id="maincontent">
            <div class="col-md-12" id="mycontent" style="margin-left:-15px;">


                @For Each itemC In Categorys
                    @*@If Not (itemC.CATCD = "1" OrElse itemC.CATCD = "2") Then*@
                    @<div id="div_C_@itemC.CATCD"><b>@itemC.CATNM</b></div>
bolDataHave = False
                    loopCount = 0
                    @<table class="table table-hover table-striped table-bordered tableCat">

                        @For Each item In Model
                            @If itemC.CATCD = item.CATCD AndAlso (itemC.ANAHYOJ = True OrElse itemC.BANGUMIHYOJ = True OrElse itemC.BASYOHYOJ = True OrElse itemC.BIKOHYOJ = True OrElse
itemC.KKNHYOJ = True OrElse itemC.KSKHYOJ = True OrElse itemC.OATIMEHYOJ = True OrElse itemC.RENRKHYOJ = True OrElse
itemC.TANTOHYOJ = True) Then
                                bolDataHave = True
                            End If
                            @If bolDataHave = True AndAlso itemC.CATCD = item.CATCD Then


                                If loopCount = 0 Then
                                    @<tr>
                                        @If ViewData("Kanri") = "1" Then
                                            @<th class="colLink"></th>
End If

                                        @If itemC.OATIMEHYOJ = True Then
                                            @<th colspan="2" class="colOAJKNST">
                                                @Html.DisplayNameFor(Function(model) model.OAJKNST)
                                            </th>
End If

                                        @If itemC.BANGUMIHYOJ = True Then
                                            @<th class="colBANGUMINM">
                                                @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                                            </th>
End If


                                        @If itemC.KKNHYOJ = True Then
                                            @<th colspan="2" class="colGYOMYMD">
                                                業務期間
                                            </th>
End If

                                        @If itemC.KSKHYOJ = True Then
                                            @<th colspan="2" class="colKSKJKNST">
                                                拘束時間
                                            </th>
End If

                                        @If itemC.ANAHYOJ = True Then
                                            @<th class="colAna">
                                                担当アナ
                                            </th>
End If


                                        @If itemC.BASYOHYOJ = True Then
                                            @<th class="colBASYO">
                                                @Html.DisplayNameFor(Function(model) model.BASYO)
                                            </th>
End If

                                        @If itemC.BIKOHYOJ = True Then
                                            @<th class="colBIKO">
                                                @Html.DisplayNameFor(Function(model) model.BIKO)
                                            </th>
End If
                                        @If itemC.NAIYOHYOJ = True Then
                                            @<th class="colNAIYO">
                                                @Html.DisplayNameFor(Function(model) model.NAIYO)
                                            </th>
End If

                                        @If itemC.TANTOHYOJ = True Then
                                            @<th class="colBANGUMITANTO">
                                                @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                                            </th>
End If

                                        @If itemC.RENRKHYOJ = True Then
                                            @<th class="colBANGUMIRENRK">
                                                @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                                            </th>
End If

                                    </tr>
loopCount = 1
                                End If


                                @<tr>

                                    @If ViewData("Kanri") = "1" Then
                                        @<td class="colLink">
                                            @Html.ActionLink("修正", "Edit", "B0020", routeValues:=New With {.id = item.GYOMNO}, htmlAttributes:=Nothing)
                                        </td>
End If


                                    @If itemC.OATIMEHYOJ = True Then
                                        @If item.OAJKNST IsNot Nothing Then
                                            @<td class="colOAJKNST1">
                                                @Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(2, 2)
                                            </td>
Else
                                            @<td class="colOAJKNST1"></td>
End If
                                    End If


                                    @If itemC.OATIMEHYOJ = True Then
                                        @If item.OAJKNED IsNot Nothing Then
                                            @<td class="colOAJKNST2">
                                                @Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(2, 2)
                                            </td>
Else
                                            @<td class="colOAJKNST2"></td>
End If
                                    End If

                                    @If itemC.BANGUMIHYOJ = True Then
                                        @<td class="colBANGUMINM">
                                            @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                        </td>
End If

                                    @If itemC.KKNHYOJ = True Then


                                        @If item.GYOMYMD <> item.GYOMYMDED Then
                                            @<td class="colGYOMYMD1">
                                                @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
                                            </td>
                                            @<td class="colGYOMYMD2">
                                                @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
                                            </td>
Else
                                            @<td class="colGYOMYMD1">
                                                @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
                                            </td>
                                            @<td class="colGYOMYMD2"></td>
End If
                                    End If

                                    @If itemC.KSKHYOJ = True Then
                                        @<td class="colKSKJKNST1">
                                            @*@Html.DisplayFor(Function(modelItem) item.KSKJKNST)*@
                                            @Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(2, 2)


                                        </td>
                                        @<td class="colKSKJKNST2">
                                            @*@Html.DisplayFor(Function(modelItem) item.KSKJKNED)*@
                                            @Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(2, 2)


                                        </td>
End If

                                    @If itemC.ANAHYOJ = True Then
                                        @<td class="colAna">
                                            @For i = 0 To item.D0020.Count - 1
                                                If i > 0 Then
                                                    @Html.Encode(", ")
End If

                                                Dim j = i
                                                If item.D0020(j).CHK = "1" Then
                                                    @<font color="blue"> @Html.DisplayFor(Function(modelItem) item.D0020(j).M0010.USERNM)</font>
Else
                                                    @Html.DisplayFor(Function(modelItem) item.D0020(j).M0010.USERNM)
End If

                                            Next
                                            @If item.D0021.Count > 0 Then
                                                @<font color="red"> 仮アナ</font>
End If
                                        </td>
End If




                                    @If itemC.BASYOHYOJ = True Then
                                        @<td class="colBASYO">
                                            @Html.DisplayFor(Function(modelItem) item.BASYO)
                                        </td>
End If


                                    @If itemC.BIKOHYOJ = True Then
                                        @<td class="colBIKO">
                                            @Html.DisplayFor(Function(modelItem) item.BIKO)
                                        </td>
End If

                                    @If itemC.NAIYOHYOJ = True Then
                                        @<td class="colNAIYO">
                                            @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                        </td>
End If

                                    @If itemC.TANTOHYOJ = True Then
                                        @<td class="colBANGUMITANTO">
                                            @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                                        </td>
End If


                                    @If itemC.RENRKHYOJ = True Then
                                        @<td class="colBANGUMIRENRK">
                                            @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
                                        </td>
End If

                                </tr>
End If
                        Next

                    </table>
If bolDataHave = False Then
                        @<p>該当データがありません。</p>
End If

                    'End If
                Next
                @For Each itemK In Kyukas
                    @<div id="div_K_@itemK.KYUKCD"><b>@itemK.KYUKNM</b></div>
bolDataHave = False
                    loopCount = 0
                    @<table class="table table-hover table-striped table-bordered tableKyuKa">

                        @For Each item In Holidays
                            @If itemK.KYUKCD = item.KYUKCD Then
                                bolDataHave = True

                                If loopCount = 0 Then
                                    @<tr>
                                        @If ViewData("Kanri") = "1" Then
                                            If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                                @<th class="colLink"></th>
End If
                                        End If



                                        <th class="colUSERNM">
                                            氏名
                                        </th>
                                        @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                            @<th class="colSTTIME">
                                                開始時間
                                            </th>
                                            @<th class="colEDTIME">
                                                終了時間
                                            </th>
End If

                                    </tr>
loopCount = 1
                                End If


                                @<tr>


                                    @If ViewData("Kanri") = "1" Then
                                        If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                            @<td class="colLink">
                                                @Html.ActionLink("修正", "Index", "B0050", routeValues:=New With {.name = item.M0010.USERNM, .userid = item.USERID, .showdate = ViewData("searchdt").ToString.Substring(0, 7)}, htmlAttributes:=Nothing)
                                            </td>
End If

                                    End If



                                    <td class="colUSERNM">
                                        @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)

                                    </td>
                                    @If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
                                        @<td class="colSTTIME">

                                            @Html.DisplayFor(Function(modelItem) item.JKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.JKNST).ToString.Substring(2, 2)

                                        </td>

                                        @<td class="colEDTIME">
                                            @*@Html.DisplayFor(Function(modelItem) item.JKNED)*@
                                            @Html.DisplayFor(Function(modelItem) item.JKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.JKNED).ToString.Substring(2, 2)
                                        </td>
End If

                                </tr>
End If
                        Next

                    </table>
If bolDataHave = False Then
                        @<p>該当データがありません。</p>
End If

                Next
            </div>

            @*伝言板*@
             @If Session("msgShow") = "hide" Then
                @<div Class="col-md-2 col-md-offset-7 affix " id="ColMsgBox" style="z-index:1;background-color:lavender; width:320px;height:380px; overflow-y:scroll;display:none">
                    @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                    @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
                </div>
            Else
                 @<div Class="col-md-2 col-md-offset-7 affix " id="ColMsgBox" style="z-index:1;background-color:lavender; width:320px;height:380px; overflow-y:scroll">
                    @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                    @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
                  </div>
            End If
        </div>
    </div>

</div>


<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>
<script type="text/javascript">
    $(document).ready(function () {
        if ($(document).width() > 992) {
            var height = $('#headermenus').height() + 17
            $('#maincontent').css('margin-top', height + 'px')
        }

        $('.dropdown-toggle').dropdown();

        //伝言板非表示で検索する時は伝言板非表示のままにしたいため
        if ($("#msgShow").val() == 'hide') {
            $("#ColMsgBox").hide();
        }

        else {
            $("#ColMsgBox").show();
        }
    });

    $(window).resize(function () {
        if ($(document).width() > 992) {
            var height = $('#headermenus').height() + 17
            $('#maincontent').css('margin-top', height + 'px')
        }
        else {
            $('#maincontent').css('margin-top', '2px')
        }
    });

    $(".catglink").click(function () {
        var catglinkid = $(this).attr("id");
        var th = $("#div_" + catglinkid).position();

        var pos = th.top;
        $("html,body").animate({
            scrollTop: pos
        }, "slow", "swing");
    });


    function SetDate(days) {

        var searchdt = $('#Searchdt').val()
        if (searchdt != "") {

            var curdates = $('#Searchdt').val().split('/');
            var newdate = new Date(curdates[0], curdates[1] - 1, curdates[2]);
            newdate.setDate(newdate.getDate() + days);
            var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2) + '/' + ('0' + newdate.getDate()).slice(-2);
            $('#Searchdt').val(formattedNewDate);
        }

    }

    function KeyUpFunction() {

        var searchdt = $('#Searchdt').val()
        var viewdate = $('#viewdatadate').val()

        if (searchdt != "") {

            if (searchdt.length == 10) {

                //曜日をセットする
                var day_of_week = new Array('日', '月', '火', '水', '木', '金', '土');
                var curdates = searchdt.split('/');
                var dateObj = new Date(searchdt);
                //alert(dateObj)

                var day = dateObj.getDay()
                var yobi = day_of_week[day];
                $("#Day").text('(' + yobi + ')')


                if (searchdt != viewdate) {

                    $("#C0030Index").submit();
                }

            }
        }

    }

    function PrintDiv() {


        var divContents = document.getElementById("mycontent").innerHTML;
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


        var divContents = document.getElementById("mycontent").innerHTML;

        var url = myApp.Urls.baseUrl + 'Content/C0030.css';
        document.body.innerHTML = '<html><head><link rel="stylesheet"  type="text/css" media="print" href=' + url + '></head><body>' + divContents + '</body></html>'

        window.print();

        document.body.innerHTML = oldstr;

        afterPrint()
    }

    function afterPrint() {

        setTimeout(function () { document.location.href = window.location.href; }, 250);
    }


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
        $("#C0030Index").submit();
    });


</script>



