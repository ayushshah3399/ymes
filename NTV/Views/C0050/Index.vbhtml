@Imports Microsoft.AspNet.Identity

@Code
    ViewData("Title") = "ガントチャート"
    Layout = "~/Views/Shared/_Layout.vbhtml"

	@Functions
        Const cnsShiftBackColor As String = "lightblue"     '業務（シフト）時間のセルの背景色
        Const cnsPngUrl As String = "/Content/kosoku.png"
        Private m_Kosoku1 As Integer = 0
        Private m_Kosoku2 As Integer = 0
        Class clsCell
            Private _BackColor As String = ""
            Private _KosokuColor As String = ""
            Private _SpanPrv As Integer = 0
            Private _Span As Integer = 0
            Private _SpanAft As Integer = 0
            Private _BorderLeft As String = ""
            Private _BorderRight As String = ""

            Property BackColor As String
                Get
                    Return _BackColor
                End Get
                Set(value As String)
                    _BackColor = value
                End Set
            End Property

            Property KosokuColor As String
                Get
                    Return _KosokuColor
                End Get
                Set(value As String)
                    _KosokuColor = value
                End Set
            End Property

            Property SpanPrv As Integer
                Get
                    Return _SpanPrv
                End Get
                Set(value As Integer)
                    _SpanPrv = value
                End Set
            End Property

            Property Span As Integer
                Get
                    Return _Span
                End Get
                Set(value As Integer)
                    _Span = value
                End Set
            End Property

            Property SpanAft As Integer
                Get
                    Return _SpanAft
                End Get
                Set(value As Integer)
                    _SpanAft = value
                End Set
            End Property

            Property BorderLeft As String
                Get
                    Return _BorderLeft
                End Get
                Set(value As String)
                    _BorderLeft = value
                End Set
            End Property

            Property BorderRight As String
                Get
                    Return _BorderRight
                End Get
                Set(value As String)
                    _BorderRight = value
                End Set
            End Property

            Sub New()
            End Sub

            Sub New(ByVal strBgColor As String, ByVal intSpan As Integer)
                Dim strRet As String = "rgba({0},{1},{2}, 0.5)"
                Dim objColor As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(strBgColor)

                _BackColor = strBgColor
                _Span = intSpan
                '背景色を透過してtableタグの背景画像を見せるための色設定（RGBA）
                If strBgColor = "white" Then
                    _KosokuColor = "transparent"
                Else
                    _KosokuColor = String.Format(strRet, New String() {objColor.R.ToString, objColor.G.ToString, objColor.B.ToString})
                End If
            End Sub
        End Class

        'セル情報を作成する
        Function GetCellInfo(ByVal intHour2 As Integer, ByVal intSpan As Integer, ByVal strBgColor As String, ByVal strUSERID As Short, ByVal intModelIndex As Integer) As clsCell
            Dim retCell = New clsCell(strBgColor, intSpan)

            Dim objDesk As Object = Nothing
            If intModelIndex > 0 Then
                objDesk = Model(intModelIndex - 1)
                If strUSERID = objDesk.USERID AndAlso String.IsNullOrEmpty(objDesk.DESKNO) = False AndAlso objDesk.TM + objDesk.SPAN = intHour2 Then
                    retCell.BorderLeft = "border-left-width:0px"
                End If
            End If
            If intModelIndex < Model.Count - 1 Then
                objDesk = Model(intModelIndex + 1)
                If strUSERID = objDesk.USERID AndAlso String.IsNullOrEmpty(objDesk.DESKNO) = False AndAlso objDesk.TM = intHour2 + intSpan Then
                    retCell.BorderRight = "border-right-width:0px"
                End If
            End If

            '拘束時間内ならSpanの部分を網掛けにする（背景を透明にしてtableに貼り付けた画像を見せる）
            If m_Kosoku1 > 0 Then
                If intHour2 >= m_Kosoku1 AndAlso intHour2 < m_Kosoku2 Then   '開始時刻が拘束時間内で始まる（拘束終了時間と開始時間がイコールは含めてはいけない）
                    '終了時間が拘束時間外
                    If intHour2 + intSpan > m_Kosoku2 Then
                        retCell.Span = m_Kosoku2 - intHour2    '拘束時間内部分
                        retCell.SpanAft = intSpan - retCell.Span  '拘束時間外部分
                    End If
                ElseIf intHour2 + intSpan > m_Kosoku1 AndAlso intHour2 + intSpan <= m_Kosoku2 Then  '開始時間は拘束時間外で終了時間が拘束開始時間内に終わる（拘束開始時間と終了時間がイコールは含めてはいけない）
                    retCell.SpanPrv = m_Kosoku1 - intHour2       '拘束時間外部分
                    retCell.Span = intSpan - retCell.SpanPrv '拘束時間内部分
                ElseIf intHour2 <= m_Kosoku1 AndAlso intHour2 + intSpan >= m_Kosoku2 Then    '開始時間と終了時間が拘束時間内にある
                    retCell.SpanPrv = m_Kosoku1 - intHour2       '拘束時間外部分
                    retCell.Span = m_Kosoku2 - m_Kosoku1      '拘束時間内部分
                    retCell.SpanAft = intSpan - retCell.SpanPrv - retCell.Span   '拘束時間外部分
                End If
            End If

            Return retCell
        End Function
	End Functions
End Code

<style>

	div.c0050div3 {
		margin-top: 10px;
	}

	div.c0050div4 {
		margin-top: 10px;
	}

	div.c0050div5 {
		margin-top: 10px;
        width:25%;
	}

	div.c0050div6 {
		position: relative;
		min-height: 1px;
		padding-right: 15px;
		padding-left: 15px;
		padding: 0px 0px;
		width: 100%;
	}

    div#scheduleheddiv {
        border: 0;
        padding: 0px 0px;
    }

    table#schedulehed {
        table-layout: fixed;
        border-collapse: separate;
        width: 1486px;
        font-size: 10pt;
    }

	/* 時刻 */
        table#schedulehed td.HOUR {
            border: solid 1px #808080;
            padding: 3px 3px;
            height: 24px;
            color: black;
            background-color: white;
        }

    div#scheduleditdiv {
        margin-top: 1px;
        border: 0;
        height: 98%;
        width: 1505px;
        overflow: auto;
        border-bottom: 1px solid #ecf0f1;
    }

    table#schedule {
        table-layout: fixed;
        border-collapse: separate;
        width: 1486px;
        font-size: 10pt;
    }

        table#schedule td {
            border: solid 1px #808080;
            height: 24px;
        }

	table#schedule line {
		stroke: red;
		stroke-width: 3px;
	}

	/* 何もなし */
        table#schedule td.Def {
            border: solid 1px #808080;
            padding: 3px 3px;
            height: 24px;
            background-color: white;
        }

	/* エラー（シフト重複） */
        table#schedule td.err {
            border: solid 1px #808080;
            height: 24px;
            background-color: pink;
        }

    /* エラー（シフト重複） */
        table#schedule td.errtransbk {
            border: solid 1px #808080;
            height: 24px;
            background-color: rgba(255,192,203,0.5);
        }

	/* 休日 */
        table#schedule td.kyuka {
            border: solid 1px #808080;
            padding: 3px 3px;
            height: 24px;
        }

	/* 休日（デスクメモあり） */
	table#schedule td.kyukadesk {
		border: solid 3px fuchsia;
		padding: 3px 3px;
		height: 24px;
	}

	/* アナウンサー名 */
        td.USER, table#schedule td.USER {
            border: solid 1px #808080;
            padding-left: 3px;
            width: 70px;
            height: 24px;
            color: black;
            background-color: white;
        }

	/* アナウンサー名 */
	table#schedule td.USER > a {
		text-decoration: none;
	}

	/* アナウンサー名 */
	table#schedule td.USER > a:hover {
		text-decoration: underline;
	}

	/* 業務（シフト） */
        td.inshift, table#schedule td.inshift {
            border: solid 1px #808080;
            padding: 3px 1px;
            height: 24px;
            color: black;
            background-color: lightblue;
        }
    
	/* 業務（シフト） */
	table#schedule td.inshift {
		cursor: pointer;
	}

	/* 業務（シフト・デスクメモあり） */
	table#schedule td.inshiftdesk {
		border: solid 3px fuchsia;
		padding: 3px 1px;
		height: 24px;
		color: black;
		background-color: lightblue;
		cursor: pointer;
	}
        td.inshiftana, table#schedule td.inshiftana {
            border: solid 1px #808080;
            padding: 3px 1px;
            height: 24px;
            color: black;
            background-color: lightblue;
        }
    table#schedule td.inshiftana {
		cursor: auto;
	}
    table#schedule td.inshiftdeskana {
		border: solid 3px fuchsia;
		padding: 3px 1px;
		height: 24px;
		color: black;
		background-color: lightblue;
		cursor: auto;
	}

	/* デスクメモ */
	td.desk, table#schedule td.desk {
		border: solid 3px fuchsia;
		padding: 3px 3px;
		height: 24px;
		color: black;
		background-color: white;
	}

    /* Wブッキング */
    td.wbooking {
        border: solid 1px #808080;
        padding: 0px;
        color: black;
        background-color: pink;
    }

	/* 休暇サンプル 大枠 */
	div.c0050div5_1 {
		height: 146px;
		width: 800px;
		overflow-x: scroll;
		padding: 0px 0px;
		-ms-overflow-style: none;
	}

	/* 休暇サンプル テーブル枠 */
	table#holiday {
		border-collapse: collapse;
		width: 500px;
		font-size: 10pt;
		position: absolute;
		z-index: 0;
	}

	/* 休暇サンプル 1マス */
	table#holiday td {
		border: solid 1px black;
		padding: 2px 5px;
		width: 50px;
		height: 60px;
		text-align: center;
	}

	/* 休暇サンプル 文字スペース */
	table#holiday span {
		writing-mode: vertical-lr;
	}

	html {
		height: 100%;
		width: 100%;
	}

	body {
		position: relative;
		height: 100%;
		font-size: 12px;
	}

    #headermenus {
        margin-left: -12px;
        padding-top: 15px;
        padding-right: 15px;
    }

    #mycontent {
        height: 100%;
        padding: 0px 0px 26px 0px;
        overflow-x: auto;
        overflow-y: hidden;
    }

</style>

<div class="row" style="background-color:lightyellow;margin-bottom: -15px;">

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

    <div class="col-md-10" style="background-color:white;">
        <div id="headermenus" class="affix">
            <!-- 管理職ユーザー用にメニューを表示する -->
            @If ViewData("Kanri") = "1" Then
                @<div class="c0050div1" style="float:left;">
                    <ul class="nav nav-pills">
                        <li>@Html.ActionLink("担当表", "Index", "C0030")</li>
                        <li>@Html.ActionLink("休日表", "Index", "B0040")</li>
                        <li>
                            @Html.ActionLink("休日設定", "Index", "B0050")
                            @If ViewBag.KYUKFLG = "1" Then
                                @<span>
                                    @Html.ActionLink("休日申請あり", "Index", "B0050", New With {.name = ViewData("name"), .userid = ViewData("userid"), .showdate = ViewData("searchdt").ToString.Substring(0, 7)}, htmlAttributes:=New With {.class = "btn btn-danger btn-xs"})
                                </span>
                            End If
                        </li>
                        <li>@Html.ActionLink("業務登録", "Create", "B0020")</li>
                        <li>
                            @Html.ActionLink("業務承認", "Index", "B0030")
                            @If ViewBag.GYOMFLG = "1" Then
                                @<span>
                                    @Html.ActionLink("業務申請あり", "Index", "B0030", Nothing, htmlAttributes:=New With {.class = "btn btn-warning btn-xs"})
                                </span>
                            End If
                        </li>
                        <li>@Html.ActionLink("デスクメモ", "Index", "A0200")</li>

                        <li class="dropdown">
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                                管理メニュー <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu" style="font-size:12px;">
                                @If ViewBag.LoginUserACCESSLVLCD <> "3" OrElse (ViewBag.LoginUserACCESSLVLCD = "3" AndAlso Session("LoginUserDeskChief") = 1) Then
                                    @<li>@Html.ActionLink("ユーザー設定", "Index", "A0110")</li>
                                End If
                                @If ViewBag.LoginUserACCESSLVLCD <> "3" Then
                                    @<li>@Html.ActionLink("カテゴリー設定", "Index", "A0120")</li>
                                End If

                                <li>@Html.ActionLink("番組設定", "Index", "A0130")</li>

                                @If ViewBag.LoginUserACCESSLVLCD <> "3" Then
                                    @<li>@Html.ActionLink("内容設定", "Index", "A0140")</li>
                                End If

                                @If ViewData("Kanri") = "1" AndAlso ViewBag.LoginUserACCESSLVLCD <> "3" Then
                                    @<li>@Html.ActionLink("休暇コード設定", "Index", "A0150")</li>
                                End If

                                <li>@Html.ActionLink("業務変更履歴", "Index", "A0160")</li>
                                <li>@Html.ActionLink("休暇申請履歴", "Index", "A0180")</li>

                                @If ViewBag.KOKYUTENKAI = "1" OrElse ViewBag.KOKYUTENKAIALL = "1" Then
                                    @<li>@Html.ActionLink("業務一括登録", "Index", "A0170")</li>
                                End If
                                @If ViewBag.LoginUserACCESSLVLCD <> "3" OrElse (ViewBag.LoginUserACCESSLVLCD = "3" AndAlso Session("LoginUserDeskChief") = 1) Then
                                    @<li>@Html.ActionLink("スポーツカテゴリー設定", "Index", "A0210")</li>
                                End If
                            </ul>
                        </li>

                        <li class="dropdown">
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                                スポーツ <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu" style="font-size:12px;">
                                @If ViewBag.LoginUserACCESSLVLCD <> "3" OrElse (ViewBag.LoginUserACCESSLVLCD = "3" AndAlso Session("LoginUserDeskChief") = 1) Then
                                    @<li>@Html.ActionLink("スポーツシフト登録(仮登録)", "Index", "A0220")</li>
                                End If
                                <li>@Html.ActionLink("全体スポーツシフト表", "Index", "A0230")</li>
                                <li>@Html.ActionLink("種目別スポーツシフト表", "Index", "A0240", New With {.searchdt = ViewData("searchdt").ToString.Substring(0, 7)}, htmlAttributes:=Nothing)</li>
                            </ul>
                        </li>

                        <li class="pull-right infoform" style="margin-left:40px;background-color:lightskyblue;">@Html.ActionLink("業務検索", "Index", "B0020", Nothing, htmlAttributes:=New With {.style = "color:black"})</li>
                    </ul>
                </div>
            End If

            @Using Html.BeginForm("Index", "C0050", routeValues:=Nothing, method:=FormMethod.Get, htmlAttributes:=New With {.id = "C0050Form"})
                @<!-- 項目値を保持するだけのアイテムは非表示にしておく -->
                @Html.Hidden("msgShow", Session("msgShow"))
                @Html.Hidden("viewdatadate", ViewData("searchdt"))
                @Html.Hidden("cndradio", ViewData("cndradio"))
                @Html.Hidden("cndman", ViewData("cndman"))
                @Html.Hidden("cndwoman", ViewData("cndwoman"))
                @Html.Hidden("viewkosoku1", ViewData("kosoku1"))
                @Html.Hidden("viewkosoku2", ViewData("kosoku2"))
                @Html.Hidden("cnduserid", ViewData("cnduserid"))
                @Html.Hidden("cndusernm", ViewData("cndusernm"))

                @Html.Partial("AnaListDialog", ViewData("UserList"))

                @If ViewData("Kanri") = "1" Then
                    @<div class="c0050div2" style="float:right;">
                        <ul class="nav nav-pills">
                            <!--<li><a href="javascript:PrintDiv();">印刷</a></li>-->
                            <li><a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>
                            <li><a href="#" id="EnDisColMsgBox1">伝言板表示/非表示</a></li>
                            <!--<li>@Html.ActionLink("戻る", Nothing, Nothing, Nothing, New With {.href = Request.UrlReferrer})</li>-->
                        </ul>
                    </div>
                End If
                @<div Class="c0050div3" style="clear:both;float:left;margin-left:15px;">

                    <!-- 日付指定 -->
                    <div Class="c0050div3_1">
                        <ul Class="nav nav-pills">
                            <li> <Button type="submit" Class="btn btn-success btn-sm" style="background:white; color:green" onclick="$('#Searchdt').val('@DateTime.Today.ToString("yyyy/MM/dd")')">本日</Button></li>
                            <li> <Button type="submit" Class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(-1)">前日</Button></li>
                            <li>
                                <div Class="input-group">
                                    <input id="Searchdt" name="Searchdt" type="text" Class="form-control input-sm date imedisabled" value=@Html.Encode(ViewData("searchdt")) onchange="KeyUpFunction()" style="width:120px;font-size:small;">
                                </div>
                            </li>
                            <li> <div id="Day" style="font-size:20px; text-align:center; margin-top:4px;"></div></li>
                            <li> <Button type="submit" Class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(1)">翌日</Button></li>
                            <li> <Button type="submit" Class="btn btn-success btn-sm">表示</Button></li>
                        </ul>
                    </div>
                    <!-- 拘束時間 -->
                    <div Class="c0050div3_2" style="margin-top:10px;">
                        <ul Class="nav nav-pills">
                            <li><div style="float:left; text-align:center; margin-top:9px; font-size:13px;"><label class="control-label">拘束時間</label> </div></li>
                            <li>
                                <div style="float:left; margin-left:13px;">
                                    <select id="kosoku1" class="form-control input-sm imedisabled" style=" width:90px; height:34px; padding:2px 5px;">
                                        <option value="0"></option>
                                    </select>
                                </div>
                            </li>
                            <li>
                                <div style="float:left; margin-left:10px; margin-top:10px;">～</div>
                            </li>
                            <li>
                                <div style="float:left; margin-left:10px;">
                                    <select id="kosoku2" class="form-control input-sm imedisabled" style="width:90px; height:34px; padding:2px 5px;">
                                        <option value="0"></option>
                                    </select>
                                </div>
                            </li>
                            <li>
                                <div style="float:left; margin-left:8px;margin-top:4px;">
                                    <button type="submit" class="btn btn-info btn-xs" style="width:61px;" onclick="ClearKosoku()">クリア</button>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <!-- その他条件 -->
                    <div Class="c0050div3_3" style="clear:both;height:50px;margin-top:10px;">
                        <form>
                            <Label Class="radio-inline">
                                <input type="radio" name="rdoAna" value="All"> すべてのアナ
                            </Label>							<Label Class="radio-inline">
                                <input type="radio" name="rdoAna" value="Only"> 勤務日のアナのみ
                            </Label>
                            <Label Class="radio-inline">
                                <input type="radio" name="rdoAna" value="In" data-toggle="modal" data-target="#myModalAna"> 担当アナ
                            </Label>
                            <Label Class="checkbox-inline" style="margin-left:30px;">
                                <input type="checkbox" name="chkMan" value="Man"> 男性アナ
                            </Label>
                            <Label Class="checkbox-inline">
                                <input type="checkbox" name="chkWoman" value="Woman"> 女性アナ
                            </Label>
                        </form>
                        <div style="width:480px;margin-top:10px;">
                            <table id="selectana" style="margin-top:10px; table-layout:fixed; width:100%; visibility:hidden;font-size:13px;">
                                <tr>
                                    <td align="right" style="width:70px;">担当アナ：</td>
                                    <td id="modalAnalist" style="max-width: 380px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            End Using
            <!-- シフト、デスクメモ、Wブッキングのサンプルのセル表示 -->
            <div Class="c0050div4" style="float:left;">
                <div Class="c0050div4_1" style="margin-left:50px;">
                    <Table style="width:150px;">
                        <tr>
                            <td class="inshift" style="height:30px; width:30px;"></td>
                            <td style="padding-left:5px;">シフト有り</td>
                        </tr>
                        <!--一般ユーザーの場合、デスクメモ有枠を表示しない-->
                        @if ViewBag.LoginUserACCESSLVLCD <> "4" Then
                            @<tr style="height:15px;"></tr>
                            @<tr>
                                <td Class="desk" style="height:30px; width:30px;"></td>
                                <td style="padding-left:5px;">デスクメモ有り</td>
                            </tr>
                        End If
                        <tr style="height:15px;"></tr>
                        <tr>
                            <td class="wbooking"><svg height="24px" width="30px"><line x1="100%" y1="4" x2="0" y2="100%" style="stroke:red;stroke-width:3px" /></svg></td>
                            <td style="padding-left:5px;">Wブッキング</td>
                        </tr>
                    </Table>
        </div>
    </div>
    @If ViewData("Kanri") = "0" Then
        @<div class="c0050div2" style="float:right;">
            <ul class="nav nav-pills">
                <!--<li><a href="javascript:PrintDiv();">印刷</a></li>-->
                        <li><a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>
                        <li><a href="#" id="EnDisColMsgBox1">伝言板表示/非表示</a></li>
                        <!--<li>@Html.ActionLink("戻る", Nothing, Nothing, Nothing, New With {.href = Request.UrlReferrer})</li>-->
                        </ul>
</div>
            End If
            <!-- 休暇のサンプルのセル表示 -->
            <div Class="c0050div5" style="float:left;margin-left:15px;">
                <div Class="c0050div5_1">
                    <Table id="holiday">
                        @code
                            Dim intCnt As Integer = ViewData("ColorList").Count
                            Dim intMax As Integer = 0
                            Dim strKYUKNM As String = ""
                            Dim strBACKCOLOR As String = ""
                            Dim strWAKUCOLOR As String = ""
                            Dim strFONTCOLOR As String = ""

                            '休暇情報を２段に分けて表示する
                            @<tr>
                                @For intRow As Integer = 1 To intCnt
                                    intMax = intRow

                                    strBACKCOLOR = ViewData("ColorList")(intRow - 1).BACKCOLOR
                                    strWAKUCOLOR = ViewData("ColorList")(intRow - 1).WAKUCOLOR
                                    strFONTCOLOR = ViewData("ColorList")(intRow - 1).FONTCOLOR

                                    If strWAKUCOLOR IsNot Nothing AndAlso strWAKUCOLOR.Length > 0 Then
                                        strWAKUCOLOR = "#" & strWAKUCOLOR
                                        @<td style="box-shadow: 0px 0px 0px 3px @strWAKUCOLOR inset;"><span>@ViewData("ColorList")(intRow - 1).KYUKRYKNM</span></td>
                                    Else
                                        strBACKCOLOR = "#" & strBACKCOLOR
                                        strFONTCOLOR = "#" & strFONTCOLOR
                                        @<td style="background-color: @strBACKCOLOR;color: @strFONTCOLOR;"><span>@ViewData("ColorList")(intRow - 1).KYUKRYKNM</span></td>
                                    End If

                                    strKYUKNM = ViewData("ColorList")(intRow - 1).KYUKNM
                                    If Not strKYUKNM Is Nothing Then
                                        '６バイトを超える休暇名のときはセルの幅を少し大きくする
                                        If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(strKYUKNM) > 6 Then
                                            @<td style="width:140px;"><span>@strKYUKNM</span></td>
                                        Else
                                            @<td><span>@strKYUKNM</span></td>
                                        End If
                                    End If

                                    '１段目の表示が終わったら抜ける
                                    If intRow >= (intCnt / 2) Then
                                        Exit For
                                    End If
                                Next
                            </tr>

                            '２段目の表示を開始する
                            If intCnt > 1 Then
                                @<tr>
                                    @For intRow As Integer = intMax + 1 To intCnt

                                        strBACKCOLOR = ViewData("ColorList")(intRow - 1).BACKCOLOR
                                        strWAKUCOLOR = ViewData("ColorList")(intRow - 1).WAKUCOLOR
                                        strFONTCOLOR = ViewData("ColorList")(intRow - 1).FONTCOLOR

                                        If strWAKUCOLOR IsNot Nothing AndAlso strWAKUCOLOR.Length > 0 Then
                                            strWAKUCOLOR = "#" & strWAKUCOLOR
                                            @<td style="box-shadow: 0px 0px 0px 3px @strWAKUCOLOR inset;"><span>@ViewData("ColorList")(intRow - 1).KYUKRYKNM</span></td>
                                        Else
                                            strBACKCOLOR = "#" & strBACKCOLOR
                                            strFONTCOLOR = "#" & strFONTCOLOR
                                            @<td style="background-color: @strBACKCOLOR;color: @strFONTCOLOR;"><span>@ViewData("ColorList")(intRow - 1).KYUKRYKNM</span></td>
                                        End If

                                        strKYUKNM = ViewData("ColorList")(intRow - 1).KYUKNM
                                        If Not strKYUKNM Is Nothing Then
                                            '６バイトを超える休暇名のときはセルの幅を少し大きくする
                                            If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(strKYUKNM) > 6 Then
                                                @<td style="width:140px;"><span>@strKYUKNM</span></td>
                                            Else
                                                @<td><span>@strKYUKNM</span></td>
                                            End If
                                        End If
                                    Next
                                </tr>
                            End If
                        End Code
                    </Table>
                </div>
            </div>
        </div>

        <div id="maincontent">
            <!-- スクリプトのreadyでTOPの位置を変更しているがそれだと遅くてちらつくので先にここで位置をずらす -->
            <script>
                var intWinH = $(window).height();
                var intMenuH = $('#headermenus').height();
                intMenuH = intMenuH + 15;

                $('#maincontent').css('margin-top', intMenuH + 'px')
            </script>

            @code
                '拘束時間の取得
                m_Kosoku1 = Integer.Parse(ViewData("kosoku1").ToString)
                m_Kosoku2 = Integer.Parse(ViewData("kosoku2").ToString)
                Dim strUrl As String = ""

                '拘束時間のどちらかが未設定の場合は無視する
                If (m_Kosoku1 <> 0 AndAlso m_Kosoku2 = 0) OrElse (m_Kosoku1 = 0 AndAlso m_Kosoku2 <> 0) Then
                    m_Kosoku1 = 0
                    m_Kosoku2 = 0
                End If

                If m_Kosoku1 > 0 Then
                    '相対パスのままだとダメ？ http～のアドレスにしてみる
                    strUrl = Request.Url.Scheme & "://" & Request.Url.Authority & HttpRuntime.AppDomainAppVirtualPath & cnsPngUrl
                End If
            End Code

            <div class="col-md-12" id="mycontent">
                <div id="scheduleheddiv">
                    <table id="schedulehed" style="background-image:url(@strUrl); background-repeat:round; background-size:38px;">
                        <tr>
                            <!-- 時刻タイトル -->
                            <td class="USER" style="text-align:right;">時</td>
                            @Code
                                Dim strBgColor As String = ""

                                '30分刻みでセルを追加する
                                For intMin As Integer = 1 To 288 Step 6
                                    Dim strHour As String = ""

                                    If m_Kosoku1 > 0 AndAlso intMin >= m_Kosoku1 AndAlso intMin < m_Kosoku2 Then
                                        '拘束時間が指定されていればその間は網掛けにする
                                        strBgColor = "transparent"
                                    Else
                                        strBgColor = "white"
                                    End If
                                    If (intMin - 1) Mod 12 = 0 Then
                                        strHour = (Math.Floor((intMin - 1) / 12)).ToString  '表示する時刻
                                    End If

                                    @<td class="HOUR" colspan="6" style="background-color:@strBgColor;"> @strHour </td>
                                Next
                            End Code
                        </tr>
                    </table>
                </div>

                <div id="scheduleditdiv">
                    <table id="schedule" style="background-image:url(@strUrl); background-repeat:round; background-size:38px;">
                        @Code
                            Dim intModel As Integer = 0

                            Do While intModel <= Model.Count - 1    'FN_C0050_MAINDATAで取得したデータをループする（ここのループはユーザーが変わるたびに通ります）
                                Dim objItem As Object = Model(intModel)
                                Dim shUSERID As Short = objItem.USERID
                                Dim strUserBd As String = objItem.USERBDCL
                                Dim intHour2 As Integer = 1
                                Dim intLast As Integer = 0
                                Dim bolKyukrnmExist As Boolean = False

                                @<tr>
                                    @*ユーザー名を表示（枠あり休日指定もセット）*@
                                    <td class="USER" style="box-shadow: 0px 0px 0px 3px @strUserBd inset;">
                                        @Html.ActionLink(objItem.USERNM, "Index", "C0040", routeValues:=New With {.name = objItem.USERNM.ToString, .id = shUSERID, .stdt = ViewData("searchdt").ToString}, htmlAttributes:=New With {.style = "color:black;"})
                                    </td>

                                    @*以下、ユーザーが変わるまでレコードをループ（１レコードに業務と休暇とデスクメモの時間範囲が入ります　１日に同じ種類の実績が複数存在するときはレコードが複数になります）*@
                                    @Do While intModel <= Model.Count - 1
                                        Dim strClass As String = ""
                                        Dim strGYOMNO As String = objItem.GYOMNO
                                        Dim intSYUCHO As Integer = objItem.SYUCHO
                                        Dim shERRKBN As Short = objItem.ERRKBN
                                        Dim strDESKNO As String = objItem.DESKNO
                                        Dim strBANGUMINM As String = objItem.BANGUMINM
                                        Dim intShiftSt As Integer = Integer.Parse(objItem.SHIFTTM.ToString)
                                        Dim intShiftSpan As Integer = Integer.Parse(objItem.SHIFTSPAN.ToString)
                                        Dim intKyukSt As Integer = Integer.Parse(objItem.KYUKTM.ToString)
                                        Dim intKyukSpan As Integer = Integer.Parse(objItem.KYUKSPAN.ToString)
                                        Dim strKyukrnm As String = objItem.KYUKRNM
                                        Dim strKyukBk As String = objItem.KYUKBKCL
                                        Dim strKyukFt As String = objItem.KYUKFTCL
                                        Dim intDeskSt As Integer = Integer.Parse(objItem.DESKTM.ToString)
                                        Dim intDeskSpan As Integer = Integer.Parse(objItem.DESKSPAN.ToString)
                                        Dim objCell As clsCell = Nothing

                                        @If intShiftSt = -1 AndAlso intKyukSt = -1 AndAlso intDeskSt = -1 Then
                                            'このアナウンサーは今日なんの予定も入っていない
                                            For intMin As Integer = 1 To 288 Step 6
                                                If m_Kosoku1 > 0 AndAlso intMin >= m_Kosoku1 AndAlso intMin < m_Kosoku2 Then
                                                    '拘束時間内なら網掛けにする（背景を透明にしてtableに貼り付けた画像を見せる）
                                                    @<td class="Def" colspan="6" style="background-color:transparent;"> </td>
                                                Else
                                                    @<td class="Def" colspan="6"> </td>
                                                End If
                                            Next
                                        Else
                                            '０時から２４時までの間に業務開始、休日開始、デスクメモ開始があればセルを追加する
                                            @do While intHour2 <= 288    '5分単位（５分×２４時間＝２８８）で各開始時間を確認する

                                                '業務、休暇、デスクメモの開始が見つかった・・・
                                                'この前に追加したセルのお尻から今回追加するセルの頭までの間に「空き」が出たならば、そこを埋めるセルを追加する
                                                '何も予定のないところの３０分刻みのラインに達しても同じように埋める
                                                If intLast > 0 AndAlso intLast < intHour2 Then
                                                    Dim blnUmeru As Boolean = False
                                                    If intHour2 = intShiftSt OrElse intHour2 = intKyukSt OrElse intHour2 = intDeskSt Then
                                                        blnUmeru = True
                                                    ElseIf (intHour2 - 1) Mod 6 = 0 Then
                                                        blnUmeru = True
                                                    End If
                                                    If blnUmeru = True Then
                                                        @<td class="Def" colspan=@(intHour2 - intLast)> </td>
                                                        intLast = 0
                                                    End If
                                                End If

                                                If intHour2 = intShiftSt Then   '業務（シフト）開始
                                                    'シフト開始～終了（intShiftSpan）までをcolspanで囲う

                                                    'セル情報を作成する（出張の場合は背景色が異なる）
                                                    If intSYUCHO = 1 Then
                                                        objCell = GetCellInfo(intHour2, intShiftSpan, strKyukBk, shUSERID, intModel)
                                                    Else
                                                        objCell = GetCellInfo(intHour2, intShiftSpan, cnsShiftBackColor, shUSERID, intModel)
                                                    End If

                                                    If shERRKBN = 0 Then
                                                        'デスクメモがあるなしでスタイルを変更するのでクラス名を変更する
                                                        If ViewBag.LoginUserACCESSLVLCD <> "4" Then
                                                            If strDESKNO = "" Then
                                                                strClass = "inshift"
                                                            Else
                                                                strClass = "inshiftdesk"
                                                            End If
                                                        Else
                                                            If strDESKNO = "" Then
                                                                strClass = "inshiftana"
                                                            Else
                                                                strClass = "inshiftdeskana"
                                                            End If
                                                        End If


                                                        If objCell.SpanPrv > 0 AndAlso objCell.SpanAft > 0 Then
                                                            @<td class=@strClass colspan=@objCell.SpanPrv title="@strBANGUMINM" style="border-right-width:0px; @objCell.BorderLeft;" onclick="EditGyomu(@strGYOMNO)"> </td>
                                                            @<td class=@strClass colspan=@objCell.Span title="@strBANGUMINM" style="background-color:@objCell.KosokuColor; border-left-width:0px; border-right-width:0px;" onclick="EditGyomu(@strGYOMNO)"> </td>
                                                            @<td class=@strClass colspan=@objCell.SpanAft title="@strBANGUMINM" style="border-left-width:0px; @objCell.BorderRight;" onclick="EditGyomu(@strGYOMNO)"> </td>
                                                        ElseIf objCell.SpanPrv > 0 Then
                                                            @<td class=@strClass colspan=@objCell.SpanPrv title="@strBANGUMINM" style="border-right-width:0px; @objCell.BorderLeft;" onclick="EditGyomu(@strGYOMNO)">@strKyukrnm</td>
                                                            @<td class=@strClass colspan=@objCell.Span title="@strBANGUMINM" style="background-color:@objCell.KosokuColor; border-left-width:0px; @objCell.BorderRight;" onclick="EditGyomu(@strGYOMNO)"> </td>
                                                        ElseIf objCell.SpanAft > 0 Then
                                                            @<td class=@strClass colspan=@objCell.Span title="@strBANGUMINM" style="background-color:@objCell.KosokuColor; border-right-width:0px; @objCell.BorderLeft;" onclick="EditGyomu(@strGYOMNO)"> </td>
                                                            @<td class=@strClass colspan=@objCell.SpanAft title="@strBANGUMINM" style="border-left-width:0px; @objCell.BorderRight;" onclick="EditGyomu(@strGYOMNO)"> </td>
                                                        Else
                                                            @<td class=@strClass colspan=@objCell.Span title="@strBANGUMINM" style="background-color:@objCell.BackColor; @objCell.BorderLeft; @objCell.BorderRight;" onclick="EditGyomu(@strGYOMNO)"> </td>
                                                        End If
                                                    Else
                                                        If objCell.SpanPrv > 0 AndAlso objCell.SpanAft > 0 Then
                                                            @<td class=err colspan=@objCell.SpanPrv style="border-right-width:0px; @objCell.BorderLeft;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                            @<td class=errtransbk colspan=@objCell.Span style="border-left-width:0px; border-right-width:0px;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                            @<td class=err colspan=@objCell.SpanAft style="border-left-width:0px; @objCell.BorderRight;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                        ElseIf objCell.SpanPrv > 0 Then
                                                            @<td class=err colspan=@objCell.SpanPrv style="border-right-width:0px; @objCell.BorderLeft;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                            @<td class=errtransbk colspan=@objCell.Span style="border-left-width:0px; @objCell.BorderRight;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                        ElseIf objCell.SpanAft > 0 Then
                                                            @<td class=errtransbk colspan=@objCell.Span style="border-right-width:0px; @objCell.BorderLeft;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                            @<td class=err colspan=@objCell.SpanAft style="border-left-width:0px; @objCell.BorderRight;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                        Else
                                                            @<td class="err" colspan=@objCell.Span style="@objCell.BorderLeft; @objCell.BorderRight;"><svg height="20px"><line x1="100%" y1="4" x2="0" y2="20px" /></svg></td>
                                                        End If
                                                    End If
                                                    intHour2 = intShiftSt + objCell.SpanPrv + objCell.Span + objCell.SpanAft - 1
                                                    If intHour2 Mod 6 <> 0 Then
                                                        intLast = intHour2 + 1
                                                    Else
                                                        '2020/03/12 変数の初期化の場所が悪かったため３０分刻みで割り切れているはずなのにintLastが0でない状態があった
                                                        intLast = 0
                                                    End If

                                                    intShiftSt = -1 'このレコードの業務情報は表示し終わったので不要になります

                                                ElseIf intHour2 = intKyukSt Then    '休暇開始
                                                    '休暇開始～終了（intKyukSpan）までをcolspanで囲う

                                                    'セル情報を作成する
                                                    objCell = GetCellInfo(intHour2, intKyukSpan, strKyukBk, shUSERID, intModel)

                                                    'デスクメモがあるなしでスタイルを変更するのでクラス名を変更する
                                                    If strDESKNO = "" Then
                                                        strClass = "kyuka"
                                                    Else
                                                        strClass = "kyukadesk"
                                                    End If

                                                    '「公」など休暇略称は1回表示すればいい。デスクメモがある場合、デスクメモの枠中も表示されてしまうのを防ぐため。
                                                    If bolKyukrnmExist = True Then
                                                        strKyukrnm = ""
                                                    End If

                                                    If objCell.SpanPrv > 0 AndAlso objCell.SpanAft > 0 Then
                                                        @<td class=@strClass colspan=@objCell.SpanPrv style="background-color:@objCell.BackColor;color:@strKyukFt; border-right-width:0px;">@strKyukrnm</td>
                                                        @<td class=@strClass colspan=@objCell.Span style="background-color:@objCell.KosokuColor;color:@strKyukFt; border-left-width:0px; border-right-width:0px;"> </td>
                                                        @<td class=@strClass colspan=@objCell.SpanAft style="background-color:@objCell.BackColor;color:@strKyukFt; border-left-width:0px;"> </td>
                                                    ElseIf objCell.SpanPrv > 0 Then
                                                        @<td class=@strClass colspan=@objCell.SpanPrv style="background-color:@objCell.BackColor;color:@strKyukFt; border-right-width:0px;">@strKyukrnm</td>
                                                        @<td class=@strClass colspan=@objCell.Span style="background-color:@objCell.KosokuColor;color:@strKyukFt; border-left-width:0px"> </td>
                                                    ElseIf objCell.SpanAft > 0 Then
                                                        @<td class=@strClass colspan=@objCell.Span style="background-color:@objCell.KosokuColor;color:@strKyukFt; border-right-width:0px;">@strKyukrnm</td>
                                                        @<td class=@strClass colspan=@objCell.SpanAft style="background-color:@objCell.BackColor;color:@strKyukFt; border-left-width:0px;"> </td>
                                                    Else
                                                        @<td class=@strClass colspan=@objCell.Span style="background-color:@objCell.BackColor;color:@strKyukFt;">@strKyukrnm</td>
                                                    End If
                                                    bolKyukrnmExist = True

                                                    intHour2 = intKyukSt + objCell.SpanPrv + objCell.Span + objCell.SpanAft - 1
                                                    If intHour2 Mod 6 <> 0 Then
                                                        intLast = intHour2 + 1
                                                    Else
                                                        '2020/03/12 変数の初期化の場所が悪かったため３０分刻みで割り切れているはずなのにintLastが0でない状態があった
                                                        intLast = 0
                                                    End If

                                                    intKyukSt = -1 'このレコードの休日情報は表示し終わったので不要になります

                                                ElseIf intHour2 = intDeskSt Then    'デスクメモ開始
                                                    'デスクメモ開始～終了（intDeskSpan）までをcolspanで囲う

                                                    'セル情報を作成する
                                                    objCell = GetCellInfo(intHour2, intDeskSpan, "white", shUSERID, intModel)

                                                    If objCell.SpanPrv > 0 AndAlso objCell.SpanAft > 0 Then
                                                        @<td class="desk" colspan=@objCell.SpanPrv style="border-right-width:0px; @objCell.BorderLeft;"> </td>
                                                        @<td class"desk" colspan=@objCell.Span style="background-color:@objCell.KosokuColor; border-left-width:0px; border-right-width:0px;"> </td>
                                                        @<td class="desk" colspan=@objCell.SpanAft style="border-left-width:0px; @objCell.BorderRight;"> </td>
                                                    ElseIf objCell.SpanPrv > 0 Then
                                                        @<td class="desk" colspan=@objCell.SpanPrv style="border-right-width:0px; @objCell.BorderLeft;">@strKyukrnm</td>
                                                        @<td class="desk" colspan=@objCell.Span style="background-color:@objCell.KosokuColor; border-left-width:0px; @objCell.BorderRight;"> </td>
                                                    ElseIf objCell.SpanAft > 0 Then
                                                        @<td class="desk" colspan=@objCell.Span style="background-color:@objCell.KosokuColor; border-right-width:0px; @objCell.BorderLeft;"> </td>
                                                        @<td class="desk" colspan=@objCell.SpanAft style="border-left-width:0px; @objCell.BorderRight;"> </td>
                                                    Else
                                                        @<td class="desk" colspan=@objCell.Span style="background-color:@objCell.BackColor; @objCell.BorderLeft; @objCell.BorderRight;"> </td>
                                                    End If

                                                    intHour2 = intDeskSt + objCell.SpanPrv + objCell.Span + objCell.SpanAft - 1
                                                    If intHour2 Mod 6 <> 0 Then
                                                        intLast = intHour2 + 1
                                                    Else
                                                        '2020/03/12 変数の初期化の場所が悪かったため３０分刻みで割り切れているはずなのにintLastが0でない状態があった
                                                        intLast = 0
                                                    End If

                                                    intDeskSt = -1 'このレコードの休日情報は表示し終わったので不要になります

                                                Else
                                                    'この時間は何も予定なし（30分刻みで枠を追加）
                                                    If (intHour2 - 1) Mod 6 = 0 Then
                                                        'ここから３０分以内に次の予定があればそこまでのspanのセルを追加
                                                        Dim intMostNearSt As Integer = intShiftSt
                                                        Dim intSpan As Integer = 6

                                                        If intKyukSt > 0 AndAlso intMostNearSt > intKyukSt Then
                                                            intMostNearSt = intKyukSt
                                                        End If
                                                        If intDeskSt > 0 AndAlso intMostNearSt > intDeskSt Then
                                                            intMostNearSt = intDeskSt
                                                        End If

                                                        If intMostNearSt > intHour2 AndAlso intMostNearSt - intHour2 < 6 Then
                                                            intSpan = intMostNearSt - intHour2
                                                        End If

                                                        If m_Kosoku1 > 0 AndAlso intHour2 >= m_Kosoku1 AndAlso intHour2 < m_Kosoku2 Then
                                                            '拘束時間内なら網掛けにする（背景を透明にしてtableに貼り付けた画像を見せる）
                                                            @<td class="Def" colspan=@intSpan style="background-color:transparent;"> </td>
                                                        Else
                                                            @<td class="Def" colspan=@intSpan> </td>
                                                        End If
                                                        intHour2 += intSpan - 1
                                                    End If
                                                End If

                                                intHour2 += 1

                                                'このユーザーのスケジュールをすべて表示したら２４時までの残りのセルを追加する
                                                @If intShiftSt = -1 AndAlso intKyukSt = -1 AndAlso intDeskSt = -1 Then
                                                    'このユーザーの最後のレコードか調べる（次レコードのユーザーが異なれば最後となる）
                                                    Dim blnLastRec As Boolean = (intModel = Model.Count - 1)
                                                    If blnLastRec = False AndAlso shUSERID <> Model(intModel + 1).USERID Then
                                                        blnLastRec = True
                                                    End If
                                                    '２４時までの残りのセルを追加するためintShiftSt、intKyukSt、intDeskStをすべてゼロにすれば「この時間は何も予定なし」の中に入ってセルを追加してくれる
                                                    If blnLastRec = True Then
                                                        intShiftSt = 0
                                                        intKyukSt = 0
                                                        intDeskSt = 0
                                                    Else
                                                        'このユーザーの次のスケジュールがあるのでループを抜ける
                                                        Exit Do
                                                    End If
                                                End If
                                            Loop
                                        End If

                                        intModel += 1

                                        @If intModel <= Model.Count - 1 Then
                                            objItem = Model(intModel)
                                            If shUSERID <> objItem.USERID Then
                                                '次のユーザーのスケジュールをセットするためループを抜けます
                                                Exit Do
                                            End If
                                        End If
                                    Loop
                                </tr>
                            Loop
                        End Code
                    </table>

                </div>
            </div>
            <!-- 伝言板 -->
            @if Session("msgShow") = "hide" Then
                @<div Class="col-md-2 col-md-offset-7 affix " id="ColMsgBox" style="z-index:2;background-color:lavender; width:320px;height:380px; overflow-y:scroll; display:none">
                    @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                    @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
                 </div>
            Else
                @<div Class="col-md-2 col-md-offset-7 affix " id="ColMsgBox" style="z-index:2;background-color:lavender; width:320px;height:380px; overflow-y:scroll;">
                    @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                    @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
                </div>
            End If
        </div>
    </div>

</div>

<script type="text/javascript">
	var myApp = myApp || {};
	myApp.Urls = myApp.Urls || {};
	myApp.Urls.baseUrl = '@Url.Content("~")';

	$(document).ready(function () {
		// 画面上部の内容が動的に高さが変わるのでそれに合わせてスケジュールを表示するDIVタグのTOPを変更する
		var intWinH = $(window).height();
		var intMenuH = $('#headermenus').height();
		intMenuH = intMenuH + 15;
		var intMainH = intWinH - intMenuH - 100;

		$('#maincontent').css('margin-top', intMenuH + 'px')
        $('#mycontent').css('height', intMainH + 22 + 'px');
        $('#scheduleditdiv').css('height', intMainH - 23 + 'px');

        //伝言板非表示で検索する時は伝言板非表示のままにしたいため
        if ($("#msgShow").val() == 'hide') {
            $("#ColMsgBox").hide();
        }
        else {
            $("#ColMsgBox").show();
        }

		$('.dropdown-toggle').dropdown();

		// 拘束時間コンボボックスの内容をここでセットする
		setSelect();

		// 条件の値をセットする
		var strradio = $('#cndradio').val()
		$('input[name=rdoAna]').val([strradio]);

		var strman = $('#cndman').val()
		var strwoman = $('#cndwoman').val()
		if (strman == "1") {
			$('input[name=chkMan]').val(['Man']);
		}
		if (strwoman == "1") {
			$('input[name=chkWoman]').val(['Woman']);
		}

		// 選択アナの名称
		var strselectusernm = $('#cndusernm').val();
		if (strselectusernm != '') {
			$('#modalAnalist').text(strselectusernm);
			$('#selectana').css("visibility", "visible");
		}
	});

	$(window).resize(function () {
		// 画面上部の内容が動的に高さが変わるのでそれに合わせてスケジュールを表示するDIVタグのTOPを変更する
		var intWinH = $(window).height();
		var intMenuH = $('#headermenus').height();
		intMenuH = intMenuH + 15;
		var intMainH = intWinH - intMenuH - 100;

		$('#maincontent').css('margin-top', intMenuH + 'px')
        $('#mycontent').css('height', intMainH + 22 + 'px');
        $('#scheduleditdiv').css('height', intMainH - 23 + 'px');

});

	// 日付の前後移動
	function SetDate(days) {
		var searchdt = $('#Searchdt').val()
		if (searchdt!= "") {

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

		// 日付の変更があれば曜日を表示して再表示
		if (searchdt != "") {

			if (searchdt.length == 10) {
				//曜日をセットする
				var day_of_week = new Array('日', '月', '火', '水', '木', '金', '土');
				var curdates = searchdt.split('/');
				var dateObj = new Date(searchdt);

				var day = dateObj.getDay()
				var yobi = day_of_week[day];
				$("#Day").text('(' + yobi + ')')

				if (searchdt!= viewdate) {
					$("#C0050Form").submit();
				}
			}
		}
	}

	// 拘束時間コンボボックスに30分刻みの時刻を追加する
	function setSelect() {

		for (i = 1; i <= 294; i += 6) {
			var strVal = i;
			var strTxt = ('00' + Math.floor((i - 1) / 12)).slice(-2) + ':';
			if ((i - 1) % 12 == 0) {
				strTxt = strTxt + '00';
			}
			else {
				strTxt = strTxt + '30';
			}

			// 開始時刻には２４：００はセットしないので不要
			if (i < 288) {
				$("#kosoku1").append('<option value="' + strVal + '">' + strTxt + '</option>');
			}
			// 終了時刻には００：００はセットしないので不要
			if (i > 1) {
				$("#kosoku2").append('<option value="' + strVal + '">' + strTxt + '</option>');
			}
		}

		$('#kosoku1').val($('#viewkosoku1').val());
		$('#kosoku2').val($('#viewkosoku2').val());
	}

	// 拘束時間コンボボックスを初期表示に戻す
	function ClearKosoku() {
		$("#kosoku1").val(0);
		$("#kosoku2").val(0);
		$('#viewkosoku1').val("0");
		$('#viewkosoku2').val("0");
	}

	// 拘束開始時間を変更されたら変更された値を隠しコントロールにセットする
	$(function () {
		$('#kosoku1').change(function () {
			var kosoku1 = parseInt($(this).val(), 10);
			var kosoku2 = parseInt($('#kosoku2').val(), 10);

			$('#viewkosoku1').val($(this).val());

			// 開始と終了が逆になってしまう場合は終了をクリア
			if (kosoku1 >= kosoku2) {
				$('#kosoku2').val("0");
				$('#viewkosoku2').val("0");
			}
		});
	});

	// 拘束終了時間を変更されたら変更された値を隠しコントロールにセットする
	$(function () {
		$('#kosoku2').change(function () {
			var kosoku1 = parseInt($('#kosoku1').val(), 10);
			var kosoku2 = parseInt($(this).val(), 10);

			$('#viewkosoku2').val($(this).val());

			// 開始と終了が逆になってしまう場合は開始をクリア
			if (kosoku1 >= kosoku2) {
				$('#kosoku1').val("0");
				$('#viewkosoku1').val("0");
			}
		});
	});

	// 条件の表示対象アナウンサーのラジオボタンの有効な値を隠しコントロールにセットする
	$(function () {
		$('input[name="rdoAna"]:radio').change(function () {
			if ($(this).val() != "In") {
				$('#selectana').css("visibility", "hidden");

				$('#modalAnalist').text("");
				$('#cnduserid').val("");

				$('#cndradio').val($(this).val());
			}
		});
	});

	//アナウンサー選択ダイアログ
	$('#myModalAna').on('shown.bs.modal', function (e) {

		var analist = '';

		// 担当アナが指定されていればそのアナウンサーのチェックをONにしてダイアログを表示する
		analist = $('#cnduserid').val();

		var tblAna = document.getElementById("tblAna");
		if (tblAna != null) {
			var anarows = tblAna.getElementsByTagName("tr");

			for (var i = 0; i < anarows.length; i += 1) {
				var anaid = jQuery.trim($('#tblAna tr:eq(' + i + ') td:eq(2)').text().replace("\n", ""))

				if (analist.indexOf(anaid) != -1) {
					$('#tblAna tr:eq(' + i + ') td:first').find('input:first').prop('checked', true);
				}
				else {
					$('#tblAna tr:eq(' + i + ') td:first').find('input:first').prop('checked', false);
				}
			}
		}
	});

	//アナウンサー「選択」クリック
	$("#modal-selectana").click(function () {

		var tblAna = document.getElementById("tblAna");
		var anarows = tblAna.getElementsByTagName("tr");
		var analist = '';
		var anaidlist = '';
		for (var i = 0; i < anarows.length; i += 1) {
			// チェックがONのアナウンサーの名称、USERIDを取得してmodalAnalist、cnduseridにそれぞれセットする
			var chk = $('#tblAna tr:eq(' + i + ') td:first').find('input:first').is(':checked');
			if (chk) {
				var ananm = $('#tblAna tr:eq(' + i + ') td:eq(1)').text().replace(/\r?\n/g, '').replace(/\t/g, '');
				var anaid = $('#tblAna tr:eq(' + i + ') td:eq(2)').text().replace(/\r?\n/g, '').replace(/\t/g, '');
				if (analist != '') {
					analist = analist + ', ';
					anaidlist = anaidlist + ',';
				}
				analist = analist + ananm;
				anaidlist = anaidlist + anaid;

				$('#tblAna tr:eq(' + i + ') td:first').find('input:first').prop('checked', false);
			}
		}

		if (anaidlist != '') {
			$('#selectana').css("visibility", "visible");

			$('#modalAnalist').text(analist);
			$('#cnduserid').val(anaidlist);

			$('#cndradio').val('In');
			$('input[name="rdoAna"]:radio').val(['In']);
		}
	});

	//アナウンサー「閉じる」クリック
	$("#modal-close").click(function () {

		var tblAna = document.getElementById("tblAna");
		var anarows = tblAna.getElementsByTagName("tr");
		for (var i = 0; i < anarows.length; i += 1) {
			var chk = $('#tblAna tr:eq(' + i + ') td:first').find('input:first').is(':checked');
			if (chk) {
				$('#tblAna tr:eq(' + i + ') td:first').find('input:first').prop('checked', false);
			}
		}

		// 前回選択中のラジオボタンの値に変更する（「担当アナ」だったらそのままになる）
		var strradio = $('#cndradio').val()
		$('input[name="rdoAna"]:radio').val([strradio]);
	});

	// 条件の表示対象男女アナウンサーのチェックボックスの有効な値を隠しコントロールにセットする
	$(function () {
		$('[name="chkMan"]').change(function () {
			if ($(this).prop('checked')) {
				$('#cndman').val("1");
			} else {
				$('#cndman').val("0");
			}
		});
	});
	$(function () {
		$('[name="chkWoman"]').change(function () {
			if ($(this).prop('checked')) {
				$('#cndwoman').val("1");
			} else {
				$('#cndwoman').val("0");
			}
		});
	});

	// 業務内容修正画面へジャンプ
    function EditGyomu(gyomuno) {
        var accessLvl = '@Session("LoginUserACCESSLVLCD")'
        if (accessLvl != "4") {
            window.location.href = '@Url.Action("Edit", "B0020", routeValues:=New With {.id = "GYOMNO"})'.replace("GYOMNO",gyomuno)
        }
	}

	// 伝言板を表示・非表示にする
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
        $("#C0050Form").submit();

	});
</script>
