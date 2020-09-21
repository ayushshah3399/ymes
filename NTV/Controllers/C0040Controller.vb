Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
	Public Class C0040Controller
		Inherits Controller

		Private db As New Model1
		Private Days As String() = {"日", "月", "火", "水", "木", "金", "土"}

		Function ReturnLoginPartial() As ActionResult
			ViewData!ID = "Login"
			Return PartialView("_LoginPartial")
		End Function

        ' GET: C0020
        Function Index(ByVal name As String, ByVal id As String, ByVal stdt As String, ByVal formname As String, sportdata As String, ByVal msgShow As String) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

            ViewData("frompage") = formname

            '伝言板表示・非表示
            If msgShow IsNot Nothing Then
                Session("msgShow") = msgShow
            End If

            If String.IsNullOrEmpty(stdt) Then
                stdt = Today.ToString("yyyy/MM/dd")
            Else
                stdt = Date.Parse(stdt).ToString("yyyy/MM/dd")
            End If

            Dim loginUserKanri As Boolean = Session("LoginUserKanri")
            Dim loginUserSystem As Boolean = Session("LoginUserSystem")
            Dim loginUserAna As Boolean = Session("LoginUserAna")

            If String.IsNullOrEmpty(id) = True Then
                id = loginUserId
            End If

            If id = loginUserId Then
                ViewData("Self") = "1"
            Else
                ViewData("Self") = "0"
            End If

            If loginUserSystem = False AndAlso loginUserKanri = False Then
                ViewData("Kanri") = "0"
            Else
                ViewData("Kanri") = "1"
            End If

            If Request.UrlReferrer IsNot Nothing Then
                Dim strUrlReferrer As String = Request.UrlReferrer.AbsoluteUri
                Dim strFormName As String = Request.QueryString("formname")
                '休日設定画面から来た時アナ名が文字化けするので、Encodeされている元のUrlを取得
                If strFormName = "B0020" OrElse strUrlReferrer.Contains("/C0030") OrElse strUrlReferrer.Contains("/C0050") OrElse strUrlReferrer.Contains("/A0200") Then
                    Session("UrlReferrer") = strUrlReferrer
                End If
            End If

            Dim listdata As New List(Of C0040)

            If String.IsNullOrEmpty(name) Then
                Dim decID As Decimal = Decimal.Parse(id)
                Dim m0010Name = db.M0010.Find(decID)
                name = m0010Name.USERNM
            End If

            ViewData("name") = name
            ViewData("id") = id

            '一般ユーザーも管理者と同様に表示。
            'If loginUserSystem = False AndAlso loginUserKanri = False Then
            '	If stdt > Today.ToString("yyyy/MM/dd") Then
            '		ViewData("searchdt") = Today.ToString("yyyy/MM/dd")
            '		stdt = Today.ToString("yyyy/MM/dd")
            '	Else
            '		ViewData("searchdt") = stdt
            '	End If

            'Else
            '	ViewData("searchdt") = stdt
            'End If
            ViewData("searchdt") = stdt

            If sportdata = "" Then
                ViewData("sportdata") = "0"
                sportdata = 0
            ElseIf sportdata = "1" Then
                ViewData("sportdata") = "1"
            ElseIf sportdata = "0" Then
                ViewData("sportdata") = "0"
            End If
            ViewBag.curDate = Date.Today.ToString("yyyy/MM/dd")

            Dim d0050 As New D0050
            ViewData.Add("D0050", d0050)

            ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO")

            Dim d0080 As New D0080
            d0080.USERID = id
            d0080.TOROKUYMD = Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
            d0080.DATAFLG = "0"
            ViewData.Add("Message", d0080)

            Dim d00801 = db.D0080.Where(Function(m) m.DATAFLG = "0")
            d00801 = d00801.OrderByDescending(Function(f) f.DNGNNO)
            ViewData.Add("MessageList", d00801.ToList())

            ViewData("D0060") = New D0060
            Dim m0060lst = db.M0060.ToList
            Dim m0060item As New M0060
            m0060item.KYUKCD = "0"
            m0060item.KYUKNM = ""

            Dim selectlst As New List(Of M0060)
            selectlst.Add(m0060item)
            For Each item In m0060lst
                Select Case item.KYUKCD
                    Case "7", "6", "9", "8"
                        selectlst.Add(item)
                End Select
            Next
            ViewBag.KYUKCD = New SelectList(selectlst, "KYUKCD", "KYUKNM")
            ViewData("KYUKCD") = New SelectList(selectlst, "KYUKCD", "KYUKNM")

            'ASI[26 Dec 2019]:[START] Prepare KYUKCD list for dropdown in screen which open on link 時間休登録
            Dim lstKYUKCD = db.M0060.Where(Function(m) m.KYUKCD = 7 Or m.KYUKCD = 9).ToList
            Dim kyukaitem As New M0060
            kyukaitem.KYUKCD = "0"
            kyukaitem.KYUKNM = ""
            lstKYUKCD.Insert(0, kyukaitem)
            ViewBag.KYUKCDforPartialView = lstKYUKCD
            '[END]

            Dim M0030lst = db.M0030.ToList
            Dim newM0030lst As New List(Of M0030)
            newM0030lst.Add(New M0030)
            For Each item In M0030lst
                newM0030lst.Add(item)
            Next
            ViewBag.BANGUMICD = New SelectList(newM0030lst, "BANGUMICD", "BANGUMINM")

            Dim M0040lst = db.M0040.ToList
            Dim newM0040lst As New List(Of M0040)
            newM0040lst.Add(New M0040)
            For Each item In M0040lst
                newM0040lst.Add(item)
            Next
            ViewBag.NAIYOCD = New SelectList(newM0040lst, "NAIYOCD", "NAIYO")

            Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            Dim cataitem As New M0020
            cataitem.CATCD = "0"
            cataitem.CATNM = ""
            lstCATCD.Insert(0, cataitem)
            ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM")


            Dim lstbangumi = db.M0030.OrderBy(Function(m) m.BANGUMIKN).ToList
            Dim bangumiitem As New M0030
            bangumiitem.BANGUMICD = "0"
            bangumiitem.BANGUMINM = ""
            lstbangumi.Insert(0, bangumiitem)
            ViewBag.BangumiList = lstbangumi

            Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
            Dim naiyoitem As New M0040
            naiyoitem.NAIYOCD = "0"
            naiyoitem.NAIYO = ""
            lstNaiyo.Insert(0, naiyoitem)
            ViewBag.NaiyouList = lstNaiyo
            ViewBag.ColorList = db.M0060.OrderBy(Function(f) f.HYOJJN).ToList

            'アクセスコード取得
            Dim m0010 = From m In db.M0010 Select m
            m0010 = m0010.Where(Function(m) m.USERID = (id))
            Dim m0010lst = db.M0010.ToList
            Dim intAccessLevel As Integer = m0010lst(0).ACCESSLVLCD

            Dim strSearchdt As String = stdt.Replace("/", "")
            strSearchdt = strSearchdt.Substring(0, 6)
            Dim strSearchDay As String = stdt.Replace("/", "")
            strSearchDay = strSearchDay.Substring(6, 2)
            Dim intSearchdt As Integer = Integer.Parse(strSearchdt)
            Dim intId As Integer = 0
            If id IsNot Nothing Then
                intId = Integer.Parse(id)
            End If

            '公休展開されているかチェック
            Dim strNengetsu As String = ""
            Dim d0030 As D0030 = db.D0030.Find(intId, intSearchdt)
            Dim intDays As Integer = 0
            If d0030 Is Nothing Then
                ViewData("status") = "1"

                Return View(listdata)
            Else
                ViewData("status") = ""

                Dim d0030Day = db.D0030.Where(Function(m) m.USERID = intId And m.NENGETU >= intSearchdt)
                For Each item In d0030Day
                    strNengetsu = item.NENGETU

                    If String.IsNullOrEmpty(strNengetsu) = False Then
                        If item.NENGETU = intSearchdt Then
                            intDays = intDays + (DateTime.DaysInMonth(strNengetsu.Substring(0, 4), strNengetsu.Substring(4, 2)))
                            intDays = intDays - Integer.Parse(strSearchDay)
                        Else
                            intDays = intDays + (DateTime.DaysInMonth(strNengetsu.Substring(0, 4), strNengetsu.Substring(4, 2)))
                        End If

                    End If
                Next

            End If

            intDays = intDays + 1

            ViewData("DayCount") = intDays

            '一般ユーザーも管理者と同様に表示。
            '         'システム担当者、管理職以外は31
            '         If loginUserSystem = False AndAlso loginUserKanri = False Then
            '	Dim s0010 = db.S0010.Find(1)
            '	intDays = s0010.SHHYOJDAYCNT
            'End If

            Dim ParaDt As Date = Date.Parse(stdt)
            Dim SearchDt As Date = Nothing

            '業務登録から来た時過去のデータを一日分見せる
            If formname = "B0020" Then
                '画面の日より過去1日を見せる
                ParaDt = ParaDt.AddDays(-1)
                intDays = intDays + 1
                ViewData("searchdt") = ParaDt.ToString("yyyy/MM/dd")
            End If

            '[ASI:05 Dec 2019] To make common for Sportshift module also
            Dim commonController As CommonController = New CommonController()
            listdata = commonController.GetPersonalShiftData(intDays, ParaDt, id, sportdata)

            'Dim lstCATCD = db.M0020.ToList
            'Dim cataitem As New M0020
            'cataitem.CATCD = "0"
            'cataitem.CATNM = ""
            'lstCATCD.Insert(0, cataitem)
            'ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM")
            'ViewData("CATCD") = New SelectList(lstCATCD, "CATCD", "CATNM")


            '総シフト時間などを取得　----------
            Dim dtStdt As Date = Date.Parse(stdt)
            Dim dtSearchdt As Date = dtStdt

            'Dim dtStdt_Nextmm As Date = dtStdt.AddMonths(1)
            'If DateDiff(DateInterval.Day, dtStdt, Date.Parse(dtStdt_Nextmm.ToString("yyyy/MM") & "/01")) < 28 Then
            '	dtSearchdt = dtStdt_Nextmm
            'End If

            Dim sqlpara1 As New SqlParameter("an_userid", SqlDbType.SmallInt)
            sqlpara1.Value = id
            Dim sqlpara2 As New SqlParameter("ad_searchdt", SqlDbType.Date)


            sqlpara2.Value = dtSearchdt

            Dim sqlparaout1 As New SqlParameter("an_othour", SqlDbType.Decimal)
            sqlparaout1.Precision = 6
            sqlparaout1.Scale = 2
            sqlparaout1.Direction = ParameterDirection.Output

            Dim sqlparaout2 As New SqlParameter("an_othour_all", SqlDbType.Decimal)
            sqlparaout2.Precision = 6
            sqlparaout2.Scale = 2
            sqlparaout2.Direction = ParameterDirection.Output

            Dim sqlparaout3 As New SqlParameter("an_wkhour", SqlDbType.Decimal)
            sqlparaout3.Precision = 6
            sqlparaout3.Scale = 2
            sqlparaout3.Direction = ParameterDirection.Output

            Dim sqlparaout4 As New SqlParameter("an_holiday_cnt", SqlDbType.Int)
            sqlparaout4.Direction = ParameterDirection.Output

            'Dim intWkHour As Integer = db.Database.SqlQuery(Of Integer)("SELECT TeLAS.fn_get_wkhour(@an_userid, @ad_searchdt)", sqlpara1, sqlpara2).FirstOrDefault
            'ViewData("wkhour") = intWkHour

            Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_c0040_getdata @an_userid, @ad_searchdt, @an_othour OUTPUT, @an_othour_all OUTPUT, @an_wkhour OUTPUT, @an_holiday_cnt OUTPUT",
                                                    sqlpara1, sqlpara2, sqlparaout1, sqlparaout2, sqlparaout3, sqlparaout4)

            ViewData("othour") = Decimal.Parse(sqlparaout1.Value).ToString("###0.##")
            ViewData("othour_all") = Decimal.Parse(sqlparaout2.Value).ToString("###0.##")
            ViewData("wkhour") = Decimal.Parse(sqlparaout3.Value).ToString("###0.##")
            ViewData("holiday_cnt") = sqlparaout4.Value
            ViewData("month") = Integer.Parse(dtSearchdt.ToString("MM"))
            '-------------



            Return View(listdata)
        End Function


        ' POST: D0060/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Index(<Bind(Include:="GYOMNO,GYOMDT,KAKUNIN,STTIME,EDTIME,CATEGORY,BANGUMINM,NAIYO,BASHO,MEMO,MYMEMO,TITLEKBN,DATAKBN,USERID,USERNAME,C0050")> ByVal c0040 As C0040, name As String, id As String, searchdt As String, sportdata As String, ByVal msgShow As String) As ActionResult

            ViewData!LoginUsernm = Session("LoginUsernm")
            If ModelState.IsValid Then

                Dim strUserid As String = ""
                Dim strUsername As String = ""
                Dim strShiftMemo As String = ""
                Dim strPGYOMNO As String = ""
                Dim strGYOMDT As String = ""

                Dim memoExistDaysErr As String = ""

                For Each item In c0040.C0050

                    If item.DATAKBN = "1" Then

                        Dim d0020upt As D0020 = db.D0020.Find(item.GYOMNO, item.USERID)

                        If d0020upt IsNot Nothing Then

                            If d0020upt.SHIFTMEMO IsNot Nothing Then
                                strShiftMemo = d0020upt.SHIFTMEMO.ToString
                            End If

                            'd0020upt.SHIFTMEMO = item.MYMEMO
                            d0020upt.CHK = item.KAKUNIN

                        End If

                        '連続の場合
                        Dim d0020uptP As D0020 = db.D0020.Find(item.PGYOMNO, item.USERID)


                        If d0020uptP IsNot Nothing Then

                            If strPGYOMNO <> item.PGYOMNO.ToString Then
                                strPGYOMNO = item.PGYOMNO

                            End If


                            If strShiftMemo <> item.MYMEMO Then
                                'd0020uptP.SHIFTMEMO = item.MYMEMO
                                d0020uptP.CHK = item.KAKUNIN
                            End If

                        End If

                        'ASI[01 Jan 2020] : Update KSKJKNST and KSKJKNED in D0020 table
                        If item.STTIME IsNot Nothing Or item.EDTIME IsNot Nothing Then

                            Dim AV_GYOMNO = item.GYOMNO
                            If item.PGYOMNO <> 0 AndAlso item.SPORT_OYAFLG = False Then
                                AV_GYOMNO = item.PGYOMNO
                            End If

                            'Dim d0020DbLst = (From d2 In db.D0020 Join d1 In db.D0010 On d2.GYOMNO Equals d1.GYOMNO Where d2.GYOMNO = AV_GYOMNO AndAlso d2.JTJKNST = d1.JTJKNST AndAlso d2.JTJKNED = d1.JTJKNED Select d2.USERID).ToList
                            Dim d0020DbLst = (From d2 In db.D0020 Join d1 In db.D0010 On d2.GYOMNO Equals d1.GYOMNO Where d2.GYOMNO = AV_GYOMNO Select d2.USERID).ToList

                            If d0020DbLst IsNot Nothing Then
                                If (d0020DbLst.Count > 1 OrElse (d0020DbLst.Count = 1 And d0020DbLst.Contains(item.USERID) = False)) Then
                                    Dim d0020update As D0020 = db.D0020.Find(AV_GYOMNO, item.USERID)
                                    If d0020update IsNot Nothing Then
                                        If item.STTIME IsNot Nothing Then
                                            d0020update.KSKJKNST = ChangeToHHMM(item.STTIME)
                                            d0020update.JTJKNST = GetJtjkn(d0020update.GYOMYMD, d0020update.KSKJKNST)
                                        End If
                                        If item.EDTIME IsNot Nothing Then
                                            d0020update.KSKJKNED = ChangeToHHMM(item.EDTIME)
                                            d0020update.JTJKNED = GetJtjkn(d0020update.GYOMYMDED, d0020update.KSKJKNED)
                                        End If
                                        db.Entry(d0020update).State = EntityState.Modified
                                    End If
                                    If item.SPORTFLG = True Then
                                        Dim d0020_1update As D0020 = db.D0020.Find(item.GYOMNO, item.USERID)
                                        If d0020_1update IsNot Nothing Then
                                            If item.EDTIME IsNot Nothing Then
                                                d0020_1update.KSKJKNED = ChangeToHHMM(item.EDTIME)
                                                d0020_1update.JTJKNED = GetJtjkn(d0020_1update.GYOMYMDED, d0020_1update.KSKJKNED)
                                                db.Entry(d0020_1update).State = EntityState.Modified
                                            End If
                                        End If
                                    End If

                                ElseIf (d0020DbLst.Count = 1 And d0020DbLst.Contains(item.USERID)) Then
                                    Dim d0010upt As D0010 = db.D0010.Find(item.GYOMNO)
                                    Dim d0020update As D0020 = db.D0020.Find(AV_GYOMNO, item.USERID)

                                    If d0010upt IsNot Nothing Then
                                        If item.STTIME IsNot Nothing Then
                                            d0010upt.KSKJKNST = ChangeToHHMM(item.STTIME)
                                            d0010upt.JTJKNST = GetJtjkn(d0010upt.GYOMYMD, d0010upt.KSKJKNST)
                                        End If
                                        If item.EDTIME IsNot Nothing Then
                                            If item.RNZK <> 0 Then
                                                Dim d0010DBRecord As D0010 = db.D0010.Find(AV_GYOMNO)
                                                If d0010DBRecord IsNot Nothing Then
                                                    d0010DBRecord.KSKJKNED = ChangeToHHMM(item.EDTIME)
                                                    d0010DBRecord.JTJKNED = GetJtjkn(d0010DBRecord.GYOMYMDED, d0010DBRecord.KSKJKNED)
                                                    db.Entry(d0010DBRecord).State = EntityState.Modified
                                                End If
                                            End If
                                            d0010upt.KSKJKNED = ChangeToHHMM(item.EDTIME)
                                            d0010upt.JTJKNED = GetJtjkn(d0010upt.GYOMYMDED, d0010upt.KSKJKNED)
                                        End If
                                        db.Entry(d0010upt).State = EntityState.Modified
                                    End If

                                    If d0020update IsNot Nothing Then
                                        If item.STTIME IsNot Nothing Then
                                            d0020update.KSKJKNST = ChangeToHHMM(item.STTIME)
                                            d0020update.JTJKNST = GetJtjkn(d0020update.GYOMYMD, d0020update.KSKJKNST)
                                        End If
                                        If item.EDTIME IsNot Nothing Then
                                            d0020update.KSKJKNED = ChangeToHHMM(item.EDTIME)
                                            d0020update.JTJKNED = GetJtjkn(d0020update.GYOMYMDED, d0020update.KSKJKNED)
                                        End If
                                        If item.SPORTFLG = True Then
                                            Dim d0020_1update As D0020 = db.D0020.Find(item.GYOMNO, item.USERID)
                                            If d0020_1update IsNot Nothing Then
                                                If item.EDTIME IsNot Nothing Then
                                                    d0020_1update.KSKJKNED = ChangeToHHMM(item.EDTIME)
                                                    d0020_1update.JTJKNED = GetJtjkn(d0020_1update.GYOMYMDED, d0020_1update.KSKJKNED)
                                                    db.Entry(d0020_1update).State = EntityState.Modified
                                                End If
                                            End If
                                        End If
                                        db.Entry(d0020update).State = EntityState.Modified
                                    End If
                                End If

                            End If
                        End If
                    End If

                    'When KYUKCD 7 then it stored in GYOMNO from commonController during list preparation
                    'Use these DataKbn and KYUKCD to delete and insert D0040 record for purpose of JKNST-JKNED updation
                    If item.DATAKBN = "3" AndAlso (item.GYOMNO = 7) Then
                        Dim strNengetsu = item.GYOMDT.Substring(0, 7)
                        Dim strHI = item.GYOMDT.Substring(8, 2)
                        strNengetsu = strNengetsu.Replace("/", "")

                        Dim intNengetsu As Integer = Integer.Parse(strNengetsu)
                        Dim intHI As Integer = Integer.Parse(strHI)

                        Dim isDeskMemoExist As Boolean = False
                        Dim zeroPadSTTIME As String = ChangeToHHMM(item.STTIME)
                        If item.EDTIME Is Nothing Then
                            item.EDTIME = item.STTIME
                        End If
                        Dim zeroPadEDTIME As String = ChangeToHHMM(item.EDTIME)

                        isDeskMemoExist = CheckDeskMemoExist(item.GYOMDT, item.GYOMDT, zeroPadSTTIME, zeroPadEDTIME, Nothing, item.USERID, Nothing)
                        If isDeskMemoExist = True Then
                            If memoExistDaysErr IsNot "" Then
                                memoExistDaysErr = memoExistDaysErr + ", " + "[" + item.GYOMDT.Substring(5) + " " + item.STTIME + " " + item.EDTIME + "]"
                            Else
                                memoExistDaysErr = "[" + item.GYOMDT.Substring(5) + "  " + item.STTIME + " " + item.EDTIME + "]"
                            End If
                            Continue For
                        End If


                        Dim d0040delete As D0040 = db.D0040.Find(item.USERID, intNengetsu, intHI, item.STTIMEupdt)
                        If d0040delete IsNot Nothing Then
                            Dim d0040DelInsert As New D0040
                            d0040DelInsert.USERID = d0040delete.USERID
                            d0040DelInsert.NENGETU = d0040delete.NENGETU
                            d0040DelInsert.HI = d0040delete.HI

                            If String.IsNullOrEmpty(item.STTIME) = False Then
                                d0040DelInsert.JKNST = item.STTIME.Replace(":", "").PadLeft(4, "0")
                            End If
                            If String.IsNullOrEmpty(item.EDTIME) = False Then
                                d0040DelInsert.JKNED = item.EDTIME.Replace(":", "").PadLeft(4, "0")
                            End If

                            If item.STTIME IsNot Nothing Then
                                d0040DelInsert.JTJKNST = GetJtjkn(Date.Parse(item.GYOMDT), ChangeToHHMM(item.STTIME))
                            End If
                            If item.EDTIME IsNot Nothing Then
                                d0040DelInsert.JTJKNED = GetJtjkn(Date.Parse(item.GYOMDT), ChangeToHHMM(item.EDTIME))
                            End If

                            'd0040DelInsert.JTJKNST = d0040delete.JTJKNST
                            'd0040DelInsert.JTJKNED = d0040delete.JTJKNED
                            d0040DelInsert.KYUKCD = d0040delete.KYUKCD
                            d0040DelInsert.GYOMMEMO = d0040delete.GYOMMEMO
                            d0040DelInsert.BIKO = d0040delete.BIKO

                            db.D0040.Remove(d0040delete)
                            db.D0040.Add(d0040DelInsert)

                        End If
                    End If

                    If item.GYOMDT <> strGYOMDT Then
                        strGYOMDT = item.GYOMDT
                        Dim d0140upt As D0140 = db.D0140.Find(item.USERID, Date.Parse(item.GYOMDT))
                        If d0140upt IsNot Nothing Then
                            d0140upt.USERMEMO = item.MYMEMO
                        Else
                            If String.IsNullOrEmpty(item.MYMEMO) = False Then
                                Dim data As New D0140
                                data.USERMEMO = item.MYMEMO
                                data.USERID = id
                                data.YMD = item.GYOMDT
                                db.D0140.Add(data)
                            End If
                        End If
                    End If

                Next

                db.Configuration.ValidateOnSaveEnabled = False
                db.SaveChanges()
                db.Configuration.ValidateOnSaveEnabled = True

                ViewData("memoExistDaysErr") = memoExistDaysErr

                Return Index(name, id, searchdt, "", sportdata, msgShow)
            End If

            Dim lstC0040 = Index(name, id, searchdt, "", sportdata, msgShow)

            Return lstC0040
        End Function


        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function CreateD0050(<Bind(Include:="GYOMSNSNO,USERID,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,NAIYO,BASYO,GYOMMEMO,BANGUMITANTO,BANGUMIRENRK,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,CONFIRMMSG")> ByVal d0050 As D0050, name As String, id As String, searchdt As String) As ActionResult

			If ModelState.IsValid Then
				TempData("warning") = ""

				Dim strKskjknST As String() = d0050.KSKJKNST.Split(":")
				strKskjknST(0) = strKskjknST(0).PadLeft(2, "0")
				strKskjknST(1) = strKskjknST(1).PadLeft(2, "0")
				d0050.KSKJKNST = strKskjknST(0) & strKskjknST(1)

				Dim strKskjknED As String() = d0050.KSKJKNED.Split(":")
				strKskjknED(0) = strKskjknED(0).PadLeft(2, "0")
				strKskjknED(1) = strKskjknED(1).PadLeft(2, "0")
				d0050.KSKJKNED = strKskjknED(0) & strKskjknED(1)

				'業務番号の最大値の取得
				Dim decTempGyomno As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "000")
				Dim lstgyomno = (From t In db.D0050 Where t.GYOMSNSNO > decTempGyomno Select t.GYOMSNSNO).ToList
				If lstgyomno.Count > 0 Then
					decTempGyomno = lstgyomno.Max
				End If


				decTempGyomno += 1

				d0050.GYOMSNSNO = decTempGyomno
				d0050.SHONINFLG = "0"
				d0050.USERID = id
				db.D0050.Add(d0050)

				db.SaveChanges()
				Return RedirectToAction("Index", New With {.id = id, .name = name, .stdt = searchdt})
			End If


			ViewBag.BangumiList = db.M0030.ToList
			ViewBag.NaiyouList = db.M0040.ToList
			Return View(d0050)
			'Return View("_GyomuPartial", d0050)
		End Function

        <OutputCache(Duration:=0)>
        Function SearchKOKYUTENKAI(ByVal gyomymd As String, ByVal gyomymded As String, ByVal id As String) As ActionResult
            If Request.IsAjaxRequest() Then

                If gyomymd Is Nothing OrElse String.IsNullOrEmpty(gyomymd) Then
                    Return New EmptyResult
                End If

                If gyomymded Is Nothing OrElse String.IsNullOrEmpty(gyomymded) Then
                    gyomymded = gyomymd
                End If

                Dim Gyost As String = gyomymd
                Dim Gyoend As String = gyomymded

                Dim intID As Integer = Integer.Parse(id)
                Dim strYYMM As String = Date.Parse(gyomymded).ToString("yyyy/MM")
                strYYMM = strYYMM.Replace("/", "")
                Dim intSearchdt As Integer = Integer.Parse(strYYMM)
                Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
                If d0030Status Is Nothing Then
                    ViewData("warning") = "2"
                    Return PartialView("_EmptyPartial")
                End If

            End If

            Return New EmptyResult
        End Function

        <OutputCache(Duration:=0)>
        Function CheckKyukaShinsei(ByVal gyomymd As String, ByVal gyomymded As String, ByVal id As String, ByVal jknst As String, ByVal jkned As String, ByVal kyukacdcode As String,
                                ByVal pattern As String, ByVal mon As String, ByVal tue As String, ByVal wed As String, ByVal tur As String, ByVal fri As String, ByVal sat As String, ByVal sun As String) As ActionResult

            If Request.IsAjaxRequest() Then

                If gyomymd Is Nothing OrElse String.IsNullOrEmpty(gyomymd) Then
                    Return New EmptyResult
                End If

                If gyomymded Is Nothing OrElse String.IsNullOrEmpty(gyomymded) Then
                    gyomymded = gyomymd
                End If

                Dim Gyost As String = gyomymd
                Dim Gyoend As String = gyomymded

                Dim intUSERID As Integer = Integer.Parse(id)
                Dim strYYMM As String = Date.Parse(gyomymded).ToString("yyyy/MM")
                strYYMM = strYYMM.Replace("/", "")
                Dim intSearchdt As Integer = Integer.Parse(strYYMM)
                Dim d0030Status As D0030 = db.D0030.Find(intUSERID, intSearchdt)
                If d0030Status Is Nothing Then
                    ViewData("warning") = "2"
                    Return PartialView("_EmptyPartial")
                End If

                '時間休の場合
                If kyukacdcode = "7" AndAlso String.IsNullOrEmpty(jknst) = False AndAlso String.IsNullOrEmpty(jkned) = False Then

                    Dim zeroPadJKNST As String = ChangeToHHMM(jknst)
                    If jkned Is Nothing Then
                        jkned = jknst
                    End If
                    Dim zeroPadJKNED As String = ChangeToHHMM(jkned)

                    Const STR_TRUE As String = "true"
                    If pattern = STR_TRUE Then
                        Dim dtymd As Date = gyomymd
                        Do While dtymd <= gyomymded
                            If (dtymd.DayOfWeek = DayOfWeek.Monday AndAlso mon = STR_TRUE) OrElse
                                    (dtymd.DayOfWeek = DayOfWeek.Tuesday AndAlso tue = STR_TRUE) OrElse
                                    (dtymd.DayOfWeek = DayOfWeek.Wednesday AndAlso wed = STR_TRUE) OrElse
                                    (dtymd.DayOfWeek = DayOfWeek.Thursday AndAlso tur = STR_TRUE) OrElse
                                    (dtymd.DayOfWeek = DayOfWeek.Friday AndAlso fri = STR_TRUE) OrElse
                                    (dtymd.DayOfWeek = DayOfWeek.Saturday AndAlso sat = STR_TRUE) OrElse
                                    (dtymd.DayOfWeek = DayOfWeek.Sunday AndAlso sun = STR_TRUE) Then

                                Dim intResult As Integer = CheckOverLap(intUSERID, dtymd, dtymd, zeroPadJKNST, zeroPadJKNED)
                                If intResult <> "0" Then
                                    'その時間帯は業務が登録されています。
                                    ViewData("warning") = "3"
                                    Return PartialView("_EmptyPartial")
                                End If

                            End If
                            dtymd = dtymd.AddDays(1)
                        Loop
                    Else
                        Dim intResult As Integer = CheckOverLap(intUSERID, gyomymd, gyomymded, zeroPadJKNST, zeroPadJKNED)
                        If intResult <> "0" Then
                            'その時間帯は業務が登録されています。
                            ViewData("warning") = "3"
                            Return PartialView("_EmptyPartial")
                        End If
                    End If
                End If

            End If

            Return New EmptyResult
        End Function

        <OutputCache(Duration:=0)>
        Function CheckGyomuShinsei(ByVal gyomymd As String, ByVal gyomymded As String, ByVal id As String, ByVal jknst As String, ByVal jkned As String) As ActionResult
            If Request.IsAjaxRequest() Then

                If gyomymd Is Nothing OrElse String.IsNullOrEmpty(gyomymd) Then
                    Return New EmptyResult
                End If

                If gyomymded Is Nothing OrElse String.IsNullOrEmpty(gyomymded) Then
                    gyomymded = gyomymd
                End If

                Dim Gyost As String = gyomymd
                Dim Gyoend As String = gyomymded

                Dim intID As Integer = Integer.Parse(id)
                Dim strYYMM As String = Date.Parse(gyomymded).ToString("yyyy/MM")
                strYYMM = strYYMM.Replace("/", "")
                Dim intSearchdt As Integer = Integer.Parse(strYYMM)
                Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
                If d0030Status Is Nothing Then
                    ViewData("warning") = "2"
                    Return PartialView("_EmptyPartial")
                End If

                Dim zeroPadJKNST As String = ChangeToHHMM(jknst)
                If jkned Is Nothing Then
                    jkned = jknst
                End If
                Dim zeroPadJKNED As String = ChangeToHHMM(jkned)

                Dim intResult As Integer = CheckJikan_TokiLeaveOverLap(intID, gyomymd, gyomymded, zeroPadJKNST, zeroPadJKNED)
                If intResult <> "0" Then
                    'その時間帯は時間休が登録されています。
                    ViewData("warning") = "3"
                    Return PartialView("_EmptyPartial")
                End If
            End If

            Return New EmptyResult
        End Function

        '[ASI:02 JAN 2020] FOR _KYUTOROKUPARTIAL
        <OutputCache(Duration:=0)>
        Function SearchKYUTOROKU(ByVal gyomymd As String, ByVal gyomymded As String, ByVal id As String, ByVal jknst As String, ByVal jkned As String,
                                  ByVal pattern As String, ByVal mon As String, ByVal tue As String, ByVal wed As String, ByVal tur As String, ByVal fri As String, ByVal sat As String, ByVal sun As String) As ActionResult
            If Request.IsAjaxRequest() Then

                If gyomymd Is Nothing OrElse String.IsNullOrEmpty(gyomymd) Then
                    Return New EmptyResult
                End If

                If jknst Is Nothing OrElse String.IsNullOrEmpty(jknst) Then
                    Return New EmptyResult
                End If

                If jkned Is Nothing OrElse String.IsNullOrEmpty(jkned) Then
                    Return New EmptyResult
                End If

                If gyomymded Is Nothing OrElse String.IsNullOrEmpty(gyomymded) Then
                    gyomymded = gyomymd
                End If

                Dim intUSERID As Integer = Integer.Parse(id)
                Dim strYYMM As String = Date.Parse(gyomymded).ToString("yyyy/MM")
                strYYMM = strYYMM.Replace("/", "")
                Dim intSearchdt As Integer = Integer.Parse(strYYMM)
                Dim d0030Status As D0030 = db.D0030.Find(intUSERID, intSearchdt)

                If d0030Status Is Nothing Then
                    '公休展開していない月の申請です。デスクにご連絡ください。
                    ViewData("warning") = "2"
                    Return PartialView("_EmptyPartial")
                End If

                Dim zeroPadJKNST As String = ChangeToHHMM(jknst)
                If jkned Is Nothing Then
                    jkned = jknst
                End If
                Dim zeroPadJKNED As String = ChangeToHHMM(jkned)

                Const STR_TRUE As String = "true"
                If pattern = STR_TRUE Then
                    Dim dtymd As Date = gyomymd
                    Do While dtymd <= gyomymded
                        If (dtymd.DayOfWeek = DayOfWeek.Monday AndAlso mon = STR_TRUE) OrElse
                                (dtymd.DayOfWeek = DayOfWeek.Tuesday AndAlso tue = STR_TRUE) OrElse
                                (dtymd.DayOfWeek = DayOfWeek.Wednesday AndAlso wed = STR_TRUE) OrElse
                                (dtymd.DayOfWeek = DayOfWeek.Thursday AndAlso tur = STR_TRUE) OrElse
                                (dtymd.DayOfWeek = DayOfWeek.Friday AndAlso fri = STR_TRUE) OrElse
                                (dtymd.DayOfWeek = DayOfWeek.Saturday AndAlso sat = STR_TRUE) OrElse
                                (dtymd.DayOfWeek = DayOfWeek.Sunday AndAlso sun = STR_TRUE) Then

                            If CheckDuplicate_KYUTOROKU(ViewData("warning"), intUSERID, dtymd, dtymd, zeroPadJKNST, zeroPadJKNED) = False Then
                                Return PartialView("_EmptyPartial")
                            End If
                        End If
                        dtymd = dtymd.AddDays(1)
                    Loop
                Else
                    If CheckDuplicate_KYUTOROKU(ViewData("warning"), intUSERID, gyomymd, gyomymded, zeroPadJKNST, zeroPadJKNED) = False Then
                        Return PartialView("_EmptyPartial")
                    End If
                End If

            End If

            Return New EmptyResult
        End Function

        Function CheckDuplicate_KYUTOROKU(ByRef strWarning As String, ByVal userid As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal jknst As String, ByVal jkned As String) As Boolean

            Dim intResult As Integer = CheckOverLap(userid, gyomymd, gyomymded, jknst, jkned)
            If intResult <> "0" Then
                'その時間帯は業務が登録されています。
                strWarning = "3"
                Return False
            End If

            intResult = CheckLeaveOverLap(userid, gyomymd, gyomymded, jknst, jkned)
            If intResult <> "0" Then
                '休日に対して、時間休登録できません。
                strWarning = "5"
                Return False
            End If

            intResult = CheckJikan_TokiLeaveOverLap(userid, gyomymd, gyomymded, jknst, jkned)
            If intResult <> "0" Then
                '他の時間休、又は時強休と重複するため、登録できません。
                strWarning = "6"
                Return False
            End If

            Dim bolIsDeskMemoExist As Boolean = CheckDeskMemoExist(gyomymd, gyomymded, jknst, jkned, Nothing, userid, Nothing)
            If bolIsDeskMemoExist = True Then
                'この日の時間休登録はデスクに問い合わせて下さい。
                strWarning = "4"
                Return False
            End If

            Return True
        End Function

        'ASI[09 Jan 2020]: Add below Action to check desk memo exist for given date/time range
        Function CheckDeskMemoExist(ByVal kknst As String, ByVal kkned As String, ByVal jknst As Decimal, ByVal jkned As Decimal, ByVal name As String, ByVal userid As String, ByVal showdate As String) As Boolean
			Dim DeskMemoExist As Boolean = False
			Dim JTJKNST As Date = GetJtjkn(kknst, jknst).AddSeconds(1)
			Dim JTJKNED As Date = GetJtjkn(kkned, jkned).AddSeconds(-1)
			Dim cnt = (From d1 In db.D0120 Join d2 In db.D0130
								On d1.DESKNO Equals d2.DESKNO
					   Join d3 In db.D0110 On d2.DESKNO Equals d3.DESKNO
					   Where d2.USERID = userid And d3.KAKUNINID <> 5 And
									(((kknst >= d1.SHIFTYMDST And kknst <= d1.SHIFTYMDED Or
										kkned >= d1.SHIFTYMDST And kkned <= d1.SHIFTYMDED Or
										d1.SHIFTYMDST >= kknst And d1.SHIFTYMDST <= kkned Or
										d1.SHIFTYMDED >= kknst And d1.SHIFTYMDED <= kkned) And d1.KSKJKNST = Nothing) Or
							   (
								JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED Or
										JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED Or
										d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED Or
										d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED
								)) Select d1.DESKNO).Count

			If cnt > 0 Then
				DeskMemoExist = True
			End If

			'Return Json(New With {DeskMemoExist})
			Return DeskMemoExist
		End Function

		'[ASI:02 JAN 2020] FOR CHECK 時間と重なっています。
		Function CheckOverLap(ByVal userid As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal jknst As String, ByVal jkned As String) As Integer

			Dim Gyost As String = GetJtjkn(gyomymd, ChangeToHHMM(jknst))
			Dim Gyoend As String = GetJtjkn(gyomymded, ChangeToHHMM(jkned))

			Dim sqlpara1 As New SqlParameter("av_userid", SqlDbType.SmallInt)
			sqlpara1.Value = userid

			Dim sqlpara2 As New SqlParameter("ld_jtjknst", SqlDbType.DateTime)
			sqlpara2.Value = DateTime.Parse(Gyost).ToString("yyyy/MM/dd HH:mm:ss")

			Dim sqlpara3 As New SqlParameter("ld_jtjkned", SqlDbType.DateTime)
			sqlpara3.Value = DateTime.Parse(Gyoend).ToString("yyyy/MM/dd HH:mm:ss")

			Dim sqlpara4 As New SqlParameter("ln_retval", SqlDbType.Int)
			sqlpara4.Direction = ParameterDirection.Output
			sqlpara4.Value = ""

			Dim cnt = db.Database.ExecuteSqlCommand("Exec TeLAS.pr_b0050_chkoverlaptime @av_userid, @ld_jtjknst, @ld_jtjkned,@ln_retval OUTPUT ",
				sqlpara1, sqlpara2, sqlpara3, sqlpara4)

			Return sqlpara4.Value

		End Function

		'[ASI:06 FEB 2020] FOR CHECK 公休の場合、休暇申請できない。
		Function CheckLeaveOverLap(ByVal userid As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal jknst As String, ByVal jkned As String) As Integer

			'Dim JTJKNST As Date = GetJtjkn(gyomymd, ChangeToHHMM(jknst)).AddSeconds(1)
			'Dim JTJKNED As Date = GetJtjkn(gyomymded, ChangeToHHMM(jkned)).AddSeconds(-1)

			Dim JTJKNST As Date = Date.Parse(gyomymd).ToString("yyyy/MM/dd")
			Dim JTJKNED As Date = Date.Parse(gyomymded).ToString("yyyy/MM/dd")

			Date.Parse(gyomymded).ToString("yyyy/MM/dd")

            Dim cnt = (From d40 In db.D0040
                       Where d40.USERID = userid And
                       (d40.KYUKCD = 4 Or d40.KYUKCD = 11 Or d40.KYUKCD = 12 Or d40.KYUKCD = 5 Or d40.KYUKCD = 6 Or d40.KYUKCD = 8) And
                       (
                            (JTJKNST >= d40.JTJKNST And JTJKNST < d40.JTJKNED) Or
                               (JTJKNED >= d40.JTJKNST And JTJKNED < d40.JTJKNED) Or
                               (d40.JTJKNST >= JTJKNST And d40.JTJKNST <= JTJKNED) Or
                               (d40.JTJKNED > JTJKNST And d40.JTJKNED < JTJKNED)
                       )).Count

            Return cnt

		End Function

        '[ASI:06 FEB 2020] FOR CHECK 時間休, 時強休 Overlap
        Function CheckJikan_TokiLeaveOverLap(ByVal userid As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal jknst As String, ByVal jkned As String) As Integer

            Dim JTJKNST As Date = GetJtjkn(gyomymd, ChangeToHHMM(jknst)).AddSeconds(1)
            Dim JTJKNED As Date = GetJtjkn(gyomymded, ChangeToHHMM(jkned)).AddSeconds(-1)

            Date.Parse(gyomymded).ToString("yyyy/MM/dd")

            Dim cnt = (From d40 In db.D0040
                       Where d40.USERID = userid And
                       (d40.KYUKCD = 7 Or d40.KYUKCD = 9) And
                       (
                            (JTJKNST >= d40.JTJKNST And JTJKNST <= d40.JTJKNED) Or
                               (JTJKNED >= d40.JTJKNST And JTJKNED <= d40.JTJKNED) Or
                               (d40.JTJKNST >= JTJKNST And d40.JTJKNST <= JTJKNED) Or
                               (d40.JTJKNED >= JTJKNST And d40.JTJKNED <= JTJKNED)
                       )).Count

            Return cnt

        End Function

        'For CHECK 時間休 Overlap
        Function CheckJikanLeaveOverLap(ByVal userid As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal jknst As String, ByVal jkned As String) As Integer

            Dim JTJKNST As Date = GetJtjkn(gyomymd, ChangeToHHMM(jknst)).AddSeconds(1)
            Dim JTJKNED As Date = GetJtjkn(gyomymded, ChangeToHHMM(jkned)).AddSeconds(-1)

            Date.Parse(gyomymded).ToString("yyyy/MM/dd")

            Dim cnt = (From d40 In db.D0040
                       Where d40.USERID = userid And
                       (d40.KYUKCD = 7) And
                       (
                            (JTJKNST >= d40.JTJKNST And JTJKNST <= d40.JTJKNED) Or
                               (JTJKNED >= d40.JTJKNST And JTJKNED <= d40.JTJKNED) Or
                               (d40.JTJKNST >= JTJKNST And d40.JTJKNST <= JTJKNED) Or
                               (d40.JTJKNED >= JTJKNST And d40.JTJKNED <= JTJKNED)
                       )).Count

            Return cnt
        End Function

        <OutputCache(Duration:=0)>
		Function SearchGYOMU(ByVal gyomno As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal kskjknst As String, ByVal kskjkned As String, ByVal id As String) As ActionResult
			If Request.IsAjaxRequest() Then

				If gyomymded Is Nothing OrElse String.IsNullOrEmpty(gyomymded) Then
					gyomymded = gyomymd
				End If

				Dim Gyost As String = gyomymd
				Dim Gyoend As String = gyomymded

				Dim intID As Integer = Integer.Parse(id)
				Dim strYYMM As String = Date.Parse(gyomymded).ToString("yyyy/MM")
				strYYMM = strYYMM.Replace("/", "")
				Dim intSearchdt As Integer = Integer.Parse(strYYMM)
				Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
				If d0030Status Is Nothing Then
					ViewData("warning") = "2"
					Return PartialView("_EmptyPartial")
				End If

				kskjknst = ChangeToHHMM(kskjknst)
				kskjkned = ChangeToHHMM(kskjkned)

				Dim JTJKNST As Date = GetJtjkn(gyomymd, kskjknst)
				Dim JTJKNED As Date = GetJtjkn(gyomymded, kskjkned)

				Dim d0010 = From m In db.D0010 Select m
				d0010 = d0010.Where(Function(m) JTJKNST < m.JTJKNED AndAlso m.JTJKNST < JTJKNED)

				d0010 = d0010.Where(Function(d1) db.D0020.Any(Function(d2) d2.GYOMNO = d1.GYOMNO AndAlso d2.USERID = id))

				For Each item In d0010
					If item IsNot Nothing Then

						ViewData("warning") = "1"
						Dim lstCATCD = db.M0020.ToList
						Dim cataitem As New M0020
						cataitem.CATCD = "0"
						cataitem.CATNM = ""
						lstCATCD.Insert(0, cataitem)
						ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM")

						ViewBag.BangumiList = db.M0030.ToList
						ViewBag.NaiyouList = db.M0040.ToList
						Return PartialView("_EmptyPartial")
						'Return PartialView("_GyomuPartial")

					End If
				Next

			End If

			Return New EmptyResult
		End Function


		<OutputCache(Duration:=0)>
		Function SearchKYUKA(ByVal gyomno As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal id As String) As ActionResult
			If Request.IsAjaxRequest() Then

				If gyomymded Is Nothing OrElse String.IsNullOrEmpty(gyomymded) Then
					gyomymded = gyomymd
				End If

				Dim Gyost As String = gyomymd
				Dim Gyoend As String = gyomymded

				Dim intID As Integer = Integer.Parse(id)
				Dim strYYMM As String = Date.Parse(gyomymded).ToString("yyyy/MM")
				strYYMM = strYYMM.Replace("/", "")
				Dim intSearchdt As Integer = Integer.Parse(strYYMM)
				Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
				If d0030Status Is Nothing Then
					ViewData("warning") = "2"
					Return PartialView("_EmptyPartial")
				End If

			End If

			Return New EmptyResult
		End Function

		Function GetJtjkn(ByVal dtymd As Date, ByVal strHHMM As String) As Date
			Dim dtRtn As Date = Nothing
			strHHMM = strHHMM.Replace(":", "").PadLeft(4, "0")
			Dim strHH As String = strHHMM.Substring(0, 2)
			Dim strMM As String = strHHMM.Substring(2, 2)

			'36:00まで登録可能なので、実時間を２４時間制度に変更する
			If strHH >= "24" Then
				Dim intHH As Integer = Integer.Parse(strHH) - 24
				strHH = intHH.ToString.PadLeft(2, "0")
				dtymd = dtymd.AddDays(1)
			End If

			dtRtn = Date.Parse(dtymd.ToString("yyyy/MM/dd") & " " & strHH & ":" & strMM)

			Return dtRtn
		End Function

		Shared Function ChangeToHHMM(ByVal strTime As String)

			If String.IsNullOrEmpty(strTime) = False Then
				Dim strHH As String = ""
				Dim strMM As String = ""

				If strTime.Contains(":") Then
					Dim strs As String() = strTime.PadLeft(5, "0").Split(":")
					strHH = strs(0).PadLeft(2, "0")
					strMM = strs(1).PadLeft(2, "0")
					strTime = strHH & strMM
				Else
					If strTime.Length <= 2 Then
						strHH = strTime.PadLeft(2, "0")
						strMM = "00"
						strTime = strHH & strMM
					End If
				End If
			End If

			Return strTime
		End Function

		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function CreateD0060(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,PATTERN,MON,TUE,WED,TUR,FRI,SAT,SUN,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0060 As D0060, name As String, id As String, searchdt As String) As ActionResult
			If ModelState.IsValid Then


				Dim lstD0060Updt As New List(Of D0060)


				'繰り返し登録なし
				If d0060.PATTERN = True Then

                    If String.IsNullOrEmpty(d0060.JKNST) = False Then
                        d0060.JKNST = d0060.JKNST.Replace(":", "").PadLeft(4, "0")
                    End If

                    If String.IsNullOrEmpty(d0060.JKNED) = False Then
                        d0060.JKNED = d0060.JKNED.Replace(":", "").PadLeft(4, "0")
                    End If

                    '繰り返し登録あり
                    Dim dtymd As Date = d0060.KKNST

                    Do While dtymd <= d0060.KKNED

						Dim d0060New As New D0060
						Dim bolRnzkDay As Boolean = False       '曜日が連続しているかどうかのフラグ
						Dim bolIsUpdtDay As Boolean = False     '指定の曜日かどうかのフラグ
						Dim bolNG As Boolean = False

						'曜日が全てチェックOFFの場合は、全ての曜日が登録
						If d0060.MON = False AndAlso d0060.TUE = False AndAlso d0060.WED = False AndAlso d0060.TUR = False AndAlso
							d0060.FRI = False AndAlso d0060.SAT = False AndAlso d0060.SUN = False Then
							bolIsUpdtDay = True
							bolRnzkDay = True

						Else
							If d0060.MON = True AndAlso dtymd.DayOfWeek = DayOfWeek.Monday Then
								bolIsUpdtDay = True
								If d0060.TUE = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.TUE = True AndAlso dtymd.DayOfWeek = DayOfWeek.Tuesday Then
								bolIsUpdtDay = True
								If d0060.WED = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.WED = True AndAlso dtymd.DayOfWeek = DayOfWeek.Wednesday Then
								bolIsUpdtDay = True
								If d0060.TUR = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.TUR = True AndAlso dtymd.DayOfWeek = DayOfWeek.Thursday Then
								bolIsUpdtDay = True
								If d0060.FRI = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.FRI = True AndAlso dtymd.DayOfWeek = DayOfWeek.Friday Then
								bolIsUpdtDay = True
								If d0060.SAT = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.SAT = True AndAlso dtymd.DayOfWeek = DayOfWeek.Saturday Then
								bolIsUpdtDay = True
								If d0060.SUN = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.SUN = True AndAlso dtymd.DayOfWeek = DayOfWeek.Sunday Then
								bolIsUpdtDay = True
								If d0060.MON = True Then
									bolRnzkDay = True
								End If
							End If
						End If

						If bolIsUpdtDay = True Then

                            d0060New.KKNST = dtymd

                            '開始時間 > 終了時間の場合、開始日+1
                            If d0060.JKNST > d0060.JKNED Then
								d0060New.KKNED = dtymd.AddDays(1)
							Else
								'開始時間 <= 終了時間
								d0060New.KKNED = dtymd
							End If

							'終了日が対象業務期間の終了日を超えていないかチェック
							If d0060New.KKNED > d0060.KKNED Then
								bolNG = True
							End If

							If bolNG = False Then
								'期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
								Dim dtEnd As Date = Date.Parse(d0060New.KKNST).AddDays(1)
								If d0060New.KKNST <> d0060New.KKNED Then

									AddRnzkGyom(lstD0060Updt, d0060New.KKNST, d0060New.KKNED, d0060)

								Else
									'1日登録


									d0060New.KYUKCD = d0060.KYUKCD
									If String.IsNullOrEmpty(d0060.JKNST) = False Then
                                        d0060New.JKNST = d0060.JKNST
                                    End If

									If String.IsNullOrEmpty(d0060.JKNED) = False Then
                                        d0060New.JKNED = d0060.JKNED
                                    End If


                                    '代休、公休の場合 00:00と24:00で更新
                                    If d0060New.KYUKCD <> "7" AndAlso d0060New.KYUKCD <> "9" Then
										d0060New.JKNST = "0000"
										d0060New.JKNED = "2400"
									End If

									lstD0060Updt.Add(d0060New)


								End If

							End If

						End If

						dtymd = dtymd.AddDays(1)

					Loop
				Else
					Dim d0060New As New D0060

					d0060New.KKNST = d0060.KKNST
					If d0060.KKNED Is Nothing Then
						d0060.KKNED = d0060.KKNST
					End If
					d0060New.KKNED = d0060.KKNED
					d0060New.KYUKCD = d0060.KYUKCD

					'繰り返しなし
					If String.IsNullOrEmpty(d0060.JKNST) = False Then
						d0060New.JKNST = d0060.JKNST.Replace(":", "").PadLeft(4, "0")
					End If

					If String.IsNullOrEmpty(d0060.JKNED) = False Then
						d0060New.JKNED = d0060.JKNED.Replace(":", "").PadLeft(4, "0")
					End If

					'代休、公休の場合 00:00と24:00で更新
					If d0060.KYUKCD <> "7" AndAlso d0060.KYUKCD <> "9" Then
						d0060New.JKNST = "0000"
						d0060New.JKNED = "2400"
					End If

					lstD0060Updt.Add(d0060New)
				End If

				Dim decMaxKyuksnscd As Decimal = 0
				decMaxKyuksnscd = GetKyuksncd()
				For Each d0060updt In lstD0060Updt
					d0060updt.KYUKSNSCD = decMaxKyuksnscd + 1
					d0060updt.KYUKCD = d0060.KYUKCD
					d0060updt.GYOMMEMO = d0060.GYOMMEMO
					d0060updt.USERID = id
					d0060updt.SHONINFLG = "0"
					db.D0060.Add(d0060updt)
					decMaxKyuksnscd += 1
				Next


				db.SaveChanges()
				Return RedirectToAction("Index", New With {.id = id, .name = name, .stdt = searchdt})
			End If


			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
			ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)

			Return View(d0060)
		End Function

		Function GetMaxHenkorrkcd() As Decimal
			'変更履歴コードの最大値の取得
			Dim decMaxHenkorrkcd As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "00000")
			Dim lstHENKORRKCD = (From t In db.D0150 Where t.HENKORRKCD > decMaxHenkorrkcd Select t.HENKORRKCD).ToList
			If lstHENKORRKCD.Count > 0 Then
				decMaxHenkorrkcd = lstHENKORRKCD.Max
			End If

			Return decMaxHenkorrkcd
		End Function

		'ASI[08 Jan 2020]:Action to Insert in D0040 tbl, 
		'to reach the purpose of display leave in Personal shift data without sending it to confirmation to admin
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function CreateD0040(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,PATTERN,MON,TUE,WED,TUR,FRI,SAT,SUN,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0060 As D0060, name As String, id As String, searchdt As String) As ActionResult
			If ModelState.IsValid Then

				Dim decNewHenkorrkcd As Decimal = GetMaxHenkorrkcd() + 1
				Dim dtSyuseiymd As Date = Now
				Dim M0060 = db.M0060.Find(d0060.KYUKCD)

				Dim dtst As Date = d0060.KKNST
				Dim dtEd As Date
				If d0060.KKNED Is Nothing Then
					d0060.KKNED = d0060.KKNST
				End If
				dtEd = d0060.KKNED
				Dim lstD0040Insert As New List(Of D0040)

				Dim dtymd As Date = dtst

				Do While dtymd <= dtEd

					Dim strNengetsu
					Dim strHI
					Dim intNengetsu As Integer
					Dim intHI As Integer

					'繰り返し登録なし
					If d0060.PATTERN = True Then

						Dim d0040 As New D0040
						Dim bolRnzkDay As Boolean = False       '曜日が連続しているかどうかのフラグ
						Dim bolIsUpdtDay As Boolean = False     '指定の曜日かどうかのフラグ
						Dim bolNG As Boolean = False

						'曜日が全てチェックOFFの場合は、全ての曜日が登録
						If d0060.MON = False AndAlso d0060.TUE = False AndAlso d0060.WED = False AndAlso d0060.TUR = False AndAlso
							d0060.FRI = False AndAlso d0060.SAT = False AndAlso d0060.SUN = False Then
							bolIsUpdtDay = True
							bolRnzkDay = True
						Else
							If d0060.MON = True AndAlso dtymd.DayOfWeek = DayOfWeek.Monday Then
								bolIsUpdtDay = True
								If d0060.TUE = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.TUE = True AndAlso dtymd.DayOfWeek = DayOfWeek.Tuesday Then
								bolIsUpdtDay = True
								If d0060.WED = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.WED = True AndAlso dtymd.DayOfWeek = DayOfWeek.Wednesday Then
								bolIsUpdtDay = True
								If d0060.TUR = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.TUR = True AndAlso dtymd.DayOfWeek = DayOfWeek.Thursday Then
								bolIsUpdtDay = True
								If d0060.FRI = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.FRI = True AndAlso dtymd.DayOfWeek = DayOfWeek.Friday Then
								bolIsUpdtDay = True
								If d0060.SAT = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.SAT = True AndAlso dtymd.DayOfWeek = DayOfWeek.Saturday Then
								bolIsUpdtDay = True
								If d0060.SUN = True Then
									bolRnzkDay = True
								End If
							ElseIf d0060.SUN = True AndAlso dtymd.DayOfWeek = DayOfWeek.Sunday Then
								bolIsUpdtDay = True
								If d0060.MON = True Then
									bolRnzkDay = True
								End If
							End If
						End If

						If bolIsUpdtDay = True Then
							strNengetsu = dtymd.ToString.Substring(0, 7)
							strHI = dtymd.ToString.Substring(8, 2)
							strNengetsu = strNengetsu.Replace("/", "")

							intNengetsu = Integer.Parse(strNengetsu)
							intHI = Integer.Parse(strHI)

							d0040.USERID = id
							d0040.NENGETU = intNengetsu
							d0040.HI = intHI
							d0040.JKNST = ChangeToHHMM(d0060.JKNST)
							If d0060.JKNED Is Nothing Then
								d0060.JKNED = d0060.JKNST
							End If
							d0040.JKNED = ChangeToHHMM(d0060.JKNED)
							d0040.JTJKNST = GetJtjkn(dtymd, ChangeToHHMM(d0060.JKNST))
							d0040.JTJKNED = GetJtjkn(dtymd, ChangeToHHMM(d0060.JKNED))
							d0040.KYUKCD = d0060.KYUKCD
							d0040.BIKO = d0060.GYOMMEMO

							lstD0040Insert.Add(d0040)


						End If

					Else
						strNengetsu = dtymd.ToString.Substring(0, 7)
						strHI = dtymd.ToString.Substring(8, 2)
						strNengetsu = strNengetsu.Replace("/", "")

						intNengetsu = Integer.Parse(strNengetsu)
						intHI = Integer.Parse(strHI)

						Dim data As New D0040
						data.USERID = id
						data.NENGETU = intNengetsu
						data.HI = intHI
						Dim strJKNST = ChangeToHHMM(d0060.JKNST)


						data.JKNST = strJKNST
						If d0060.JKNED Is Nothing Then
							d0060.JKNED = d0060.JKNST
						End If
						Dim strJKNED = ChangeToHHMM(d0060.JKNED)
						data.JKNED = strJKNED

						Dim strjtjikan As String = GetJtjkn(dtymd, ChangeToHHMM(d0060.JKNST))
						Dim strjijikend As String = GetJtjkn(dtymd, ChangeToHHMM(d0060.JKNED))

						data.JTJKNST = strjtjikan
						data.JTJKNED = strjijikend
						data.KYUKCD = d0060.KYUKCD
						data.BIKO = d0060.GYOMMEMO

						lstD0040Insert.Add(data)
					End If

					dtymd = dtymd.AddDays(1)
				Loop

				Dim d0150 As New D0150
				d0150.HENKORRKCD = decNewHenkorrkcd + 1
				d0150.HENKONAIYO = "登録"
				d0150.USERID = Session("LoginUserid")
				d0150.SYUSEIYMD = dtSyuseiymd
				d0150.KKNST = dtymd
				d0150.KKNED = dtymd
				d0150.JKNST = d0060.JKNST.Replace(":", "").PadLeft(4, "0")
				d0150.JKNED = d0060.JKNED.Replace(":", "").PadLeft(4, "0")
				d0150.SHINSEIUSER = name
				d0150.KYUKNM = M0060.KYUKNM
				d0150.GYOMMEMO = d0060.GYOMMEMO
				db.D0150.Add(d0150)
				decNewHenkorrkcd += 1

				For Each d0040updt In lstD0040Insert
					db.D0040.Add(d0040updt)
				Next

				Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
				sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try

						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

						db.Configuration.ValidateOnSaveEnabled = False
						db.SaveChanges()
						db.Configuration.ValidateOnSaveEnabled = True

						tran.Commit()
						Return RedirectToAction("Index", New With {.id = id, .name = name, .stdt = searchdt})

					Catch ex As Exception
						tran.Rollback()
						Throw ex
					End Try
				End Using

			End If

			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
			'ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)

			''ASI[26 Dec 2019]:[START] If ModelState Validation false then Prepare KYUKCD list for dropdown in screen which open on link 時間休登録
			'Dim lstKYUKCD = db.M0060.Where(Function(m) m.KYUKCD = 7 Or m.KYUKCD = 9).ToList
			'Dim kyukaitem As New M0060
			'kyukaitem.KYUKCD = "0"
			'kyukaitem.KYUKNM = ""
			'lstKYUKCD.Insert(0, kyukaitem)
			'ViewBag.KYUKCDforPartialView = lstKYUKCD
			'[END]

			Return View(d0060)
		End Function


		Function GetKyuksncd() As Decimal
			'業務番号の最大値の取得
			Dim decTempKyuksnscd As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "000")
			Dim lstsnscd = (From t In db.D0060 Where t.KYUKSNSCD > decTempKyuksnscd Select t.KYUKSNSCD).ToList
			If lstsnscd.Count > 0 Then
				decTempKyuksnscd = lstsnscd.Max
			End If

			Return decTempKyuksnscd
		End Function

		Sub AddRnzkGyom(ByRef lstD0060Updt As List(Of D0060), ByVal dtSt As Date, ByVal dtEd As Date, ByVal d0060 As D0060)

			Dim dtymd As Date = dtSt


			Do While dtymd <= dtEd
				Dim d0060New As New D0060
				d0060New.KKNST = dtymd
				d0060New.KKNED = dtymd
				d0060New.KYUKCD = d0060.KYUKCD
				d0060New.JKNST = d0060New.JKNST.Replace(":", "").PadLeft(4, "0")
				d0060New.JKNED = d0060New.JKNST.Replace(":", "").PadLeft(4, "0")

				'代休、公休の場合 00:00と24:00で更新
				If d0060New.KYUKCD <> "7" AndAlso d0060New.KYUKCD <> "9" Then
					d0060New.JKNST = "0000"
					d0060New.JKNED = "2400"
				End If

				lstD0060Updt.Add(d0060New)


				dtymd = dtymd.AddDays(1)

			Loop

		End Sub

		'   ' POST: D0060/Create
		''過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		''詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		'<HttpPost()>
		'<ValidateAntiForgeryToken()>
		'Function Create(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0060 As D0060) As ActionResult
		'    If ModelState.IsValid Then
		'        db.D0060.Add(d0060)
		'        db.SaveChanges()
		'        Return RedirectToAction("Index")
		'    End If
		'    ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
		'    ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
		'    Return View(d0060)
		'End Function

		' GET: D0010/Delete/5
		Function Delete(ByVal id As String, ByVal gyomdt As Date, ByVal jknst As String) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			If IsNothing(gyomdt) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If

			If loginUserId <> id Then
				Return View("ErrorAccesslvl")
			End If

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.ToString
				If Not strUrlReferrer.Contains("C0040/Delete/" & id) Then
					Session("C0040DeleteRtnUrl" & id) = strUrlReferrer
				End If
			End If

			Dim strNengetsu = gyomdt.ToString.Substring(0, 7)
			strNengetsu = strNengetsu.Replace("/", "")

			Dim strHI = gyomdt.ToString.Substring(8, 2)

			Dim intUserid As Integer = Integer.Parse(id)
			Dim intNengetsu As Integer = Integer.Parse(strNengetsu)

			Dim d0040 = (From m In db.D0040 Where m.USERID = intUserid And m.NENGETU = intNengetsu And m.HI = strHI And m.JKNST = jknst).SingleOrDefault
			ViewBag.YasumiHi = d0040.JTJKNST.ToString("yyyy/MM/dd")
			ViewBag.NENGETU = strNengetsu
			ViewBag.HI = strHI
            ViewData!LoginUsernm = Session("LoginUsernm")

            If d0040.KANRIMEMO IsNot Nothing Then
                d0040.BIKO = d0040.BIKO & " (" & d0040.KANRIMEMO & ")"
            End If

            Return View(d0040)
		End Function

		' POST: C0040/Delete
		<HttpPost()>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken()>
		Function DeleteConfirmed(ByVal id As Integer, ByVal nengetsu As String, ByVal HI As String, ByVal JKNST As String) As ActionResult
			Dim intNengetsu As Integer = Integer.Parse(nengetsu)
			Dim d0040 = (From m In db.D0040 Where m.USERID = id And m.NENGETU = intNengetsu And m.HI = HI And m.JKNST = JKNST).SingleOrDefault

			db.D0040.Remove(d0040)
			db.SaveChanges()
			Return Redirect(Session("C0040DeleteRtnUrl" & id))
		End Function

	End Class



End Namespace