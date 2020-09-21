@ModelType IEnumerable(Of NTV_SHIFT.W0060)
@Code
    ViewData("Title") = "公休展開"

    Dim daysInMonth As Integer = ViewData("Days")
    Dim strDate As String = ViewData("searchdt").ToString.Substring(0, 4) & "年" & ViewData("searchdt").ToString.Substring(5, 2) & "月"


    Dim strTableDate As String = ViewData("searchdt").ToString & "/01"
    Dim FirstDate As Date = Date.Parse(strTableDate)
    Dim intWeekday As Integer = Weekday(FirstDate)
    Dim start As Integer = intWeekday - 1
    Dim loopStart As Integer = 7 - start
    Dim loop2 As Integer = loopStart + 7
    Dim loop3 As Integer = loop2 + 7
    Dim loop4 As Integer = loop3 + 7
    Dim loop5 As Integer = loop4 + 7
    Dim loop5start As Integer = loop4 + 7
    Dim needCol As Integer = 42 - 35
End Code

<style>
    .black {
        background-color: black;
        color: white;
    }

    .holiday-tab {
        border-collapse: separate;
        border-spacing: 1px;
        padding-top: 10px;
    }

        .holiday-tab th {
            width: 100px;
            text-align: center;
        }

        .holiday-tab > thead > tr > th {
            border-style: none;
        }

        .holiday-tab td {
            width: 100px;
        }

        .holiday-tab > tbody > tr > td {
            border: solid 2px;
        }

    .daily {
        height: 50px;
    }

        .daily > td {
            position: relative;
            border-style: none;
        }

            .daily > td > time {
                font-size: 15pt;
            }

            .daily > td > p {
                font-size: 12pt;
            }

                .daily > td > p > dv {
                    font-size: 8pt;
                }


    .wkkbn-4 {
        color: black;
        text-align: right;
        font-size: 100pt;
    }
</style>

    @Using Html.BeginForm("Create", "WB0050", routeValues:=New With {.searchdt = ViewData("searchdt"), .userid = ViewData("userid")}, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"})
    @Html.AntiForgeryToken()

    @<div class="row">
        <div class="col-md-2 col-md-push-9">
            <p>
                <ul class="nav nav-pills navbar-right">
                    @If Session("UrlReferrer") IsNot Nothing Then
                        @<li><a href="@Session("WB0050UrlReferrer")">戻る</a></li>
Else
                        @<li>@Html.ActionLink("戻る", "Index", "B0040")</li>
                    End If
                </ul>
            </p>
        </div>

        <div class="col-md-9 col-md-pull-2">
            <h3>
                公休展開
                @If ViewData("name") IsNot Nothing Then
                    @Html.Encode("（" & ViewData("name").ToString & "さん）")
                End If
            </h3>
            <h4>祝日を設定してください。</h4>
        </div>
    </div>


    @<div class="row">
        <div class="col-md-5  col-md-offset-1">

            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

            <label style="font-size:20px;">@strDate</label>
            &nbsp;
            @*<input id="btnUpdt" type="submit" value="更新" class="btn btn-default btn-sm btnUpdt" />*@
            <button type="button" class="btn btn-default btn-sm" id="btnUpdt">更新</button>

            <label style="font-size:15px;text-align:right; color:orange;padding-left:20px;" id="lblInfo"></label>
            @For intIndex As Integer = 0 To Model.Count - 1
                    Dim intHi As Integer = intIndex + 1
                    Dim key As String = String.Format("W0060[{0}].", intIndex)
                    Dim item = Model(intIndex)
                @Html.Hidden(key + "NENGETU", item.NENGETU)
                @Html.Hidden(key + "HI", item.HI)
                @Html.Hidden(key + "KOUKYU", item.KOUKYU)
            Next


            <table class="table holiday-tab" id="tblCalendar">
                <thead class="yobi">
                    <tr class="success">
                        <th style="background-color:red">日</th>
                        <th>月</th>
                        <th>火</th>
                        <th>水</th>
                        <th>木</th>
                        <th>金</th>
                        <th class="info">土</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="daily">
                        @For i = 0 To start - 1
                            @<td class="active"></td>
                        Next
                        @For i = 1 To loopStart
                                Dim strColName As String = i & "yobi"
                                Dim strButtonName As String = i & "btn"
                            @<td id=@i>@i</td>

                        Next

                    </tr>
                    <tr class="daily">


                        @For i = loopStart + 1 To loop2
                                Dim strColName As String = i & "yobi"
                            @<td id=@i>@i</td>
                        Next

                    </tr>
                    <tr class="daily">
                        @For i = loop2 + 1 To loop3
                                Dim strColName As String = i & "yobi"
                            @<td id=@i>@i</td>
                        Next

                    </tr>
                    <tr class="daily">

                        @For i = loop3 + 1 To loop4
                                Dim strColName As String = i & "yobi"
                            @<td id=@i>@i</td>
                        Next
                    </tr>

                    <tr class="daily">

                        @If daysInMonth < loop5 Then
                                loop5 = daysInMonth

                            End If
                        @For i = loop4 + 1 To loop5
                                Dim strColName As String = i & "yobi"
                            @<td id=@i>@i</td>
                        Next
                        @For intindex = 1 To loop5start - daysInMonth
                            @<td class="active"></td>
                        Next

                    </tr>

                    <tr class="daily">
                        @For i = loop5 + 1 To (daysInMonth)
                                Dim strColName As String = i & "yobi"
                            @<td id=@i>@i</td>
                            needCol = needCol - 1
                        Next
                        @For intindex = 0 To needCol - 1
                            @<td class="active"></td>
                        Next

                    </tr>
                </tbody>
            </table>

        </div>

    </div>


End Using




<script>

    $(document).ready(function () {
        var rows = $("table")[0].rows;

        jQuery.each(rows, function (i) {
            if (i == 0) {
                return true;  //continue
            }

            var cells = rows[i].cells;

            jQuery.each(cells, function () {
                var day = $(this).text();

                if (day != '') {
                    var hiddenindex = day - 1;
                    var strHiddenColName = "#W0060_" + hiddenindex + "__KOUKYU";
                    var kokyu = $(strHiddenColName).val();

                    if (kokyu == 'true') {
                        $(this).addClass("black");
                    }
                }

            });
        });

       
    });

    $('td').on('click', function (e) {
        var day = $(this).text();

        //空のセルの場合処理しないようにする
        if (day == '') {
            return false
        }

        var cell = $(this);
        var hiddenindex = day - 1;
        var strHiddenColName = "#W0060_" + hiddenindex + "__KOUKYU"

        if (cell.hasClass("black") == false) {
            cell.addClass("black");
            //var cellvalue = $(this).val();
            $(strHiddenColName).val(true);
        }
        else {
            cell.removeClass("black");
            $(strHiddenColName).val(false);
        }


    })

    $('#btnUpdt').on('click', function (e) {
        
        $('#FMTKBN').val(0);
        
        var result = confirm("公休展開処理を行います。よろしいですか？")

        if (result == true) {

            $("#lblInfo").text("処理中です。しばらくお待ち下さい。。。")
            //$("body").css("cursor", "wait");
            $('#btnUpdt').attr("disabled", "disabled");
            
            setTimeout(function () {

                $("#myForm").submit();

            }, 50);
        }
        
    });
   
</script>
