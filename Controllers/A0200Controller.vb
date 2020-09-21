Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports NTV_SHIFT
Imports System.Data.SqlClient


Namespace Controllers
	Public Class A0200Controller
		Inherits System.Web.Mvc.Controller

		Private db As New Model1

		Function ReturnLoginPartial() As ActionResult
			ViewData!ID = "Login"
			Return PartialView("_LoginPartial")
		End Function

		Function CheckAccessLvl() As Boolean
			Dim loginUserKanri As Boolean = Session("LoginUserKanri")
			Dim loginUserSystem As Boolean = Session("LoginUserSystem")
			If Not loginUserKanri AndAlso Not loginUserSystem Then
				Return False
			End If

			Return True
		End Function

		' GET: A0200co
        Function Index(ByVal CondDeskno As String, ByVal CondKakunin1 As String, ByVal CondKakunin2 As String, ByVal CondInstst As String, ByVal CondInsted As String,
         ByVal CondDeskid As String, ByVal CondCatcd As String, ByVal CondBanguminm As String, ByVal CondNaiyo As String, ByVal CondShiftst As String, ByVal CondShifted As String,
         ByVal CondAnaid As String, ByVal CondBangumitanto As String, ByVal CondBangumirenrk As String, ByVal CondBasyo As String, ByVal info As SortingPagingInfo) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.AbsoluteUri      '休日設定画面から来た時アナ名が文字化けするので、Encodeされている元のUrlを取得
				If strUrlReferrer.Contains("/B0020") OrElse strUrlReferrer.Contains("/B0050") OrElse strUrlReferrer.Contains("/C0040") OrElse strUrlReferrer.Contains("/C0050") OrElse strUrlReferrer.Contains("/A0240") Then
					Session("UrlReferrer") = strUrlReferrer
				End If
			End If

			ViewBag.LoginUserSystem = Session("LoginUserSystem")

            Session("CondDeskno") = CondDeskno
            Session("CondKakunin1") = CondKakunin1
            Session("CondKakunin2") = CondKakunin2
            Session("CondInstst") = CondInstst
            Session("CondInsted") = CondInsted
            Session("CondDeskid") = CondDeskid
            Session("CondCatcd") = CondCatcd
            Session("CondBanguminm") = CondBanguminm
            Session("CondNaiyo") = CondNaiyo
            Session("CondShiftst") = CondShiftst
            Session("CondShifted") = CondShifted
            Session("CondAnaid") = CondAnaid
            Session("CondBangumitanto") = CondBangumitanto
            Session("CondBangumirenrk") = CondBangumirenrk
            Session("CondBasyo") = CondBasyo

            Dim item1 As New M0010
            item1.USERID = "0"
            item1.USERNM = ""

            Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            lstUSERID.Insert(0, item1)
            If CondAnaid IsNot Nothing AndAlso CondAnaid > "0" Then
                ViewBag.CondAnaid = New SelectList(lstUSERID, "USERID", "USERNM", CondAnaid)
            Else
                ViewBag.CondAnaid = New SelectList(lstUSERID, "USERID", "USERNM")
            End If

            Dim lstAdmin = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.KANRI = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            lstAdmin.Insert(0, item1)
            If CondDeskid IsNot Nothing AndAlso CondDeskid > "0" Then
                ViewBag.CondDeskid = New SelectList(lstAdmin, "USERID", "USERNM", CondDeskid)
            Else
                ViewBag.CondDeskid = New SelectList(lstAdmin, "USERID", "USERNM")
            End If

            Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            Dim item As New M0020
            item.CATCD = "0"
            item.CATNM = ""
            lstCATCD.Insert(0, item)
            ViewBag.CondCatcd = New SelectList(lstCATCD, "CATCD", "CATNM")

            Dim lstbangumi = db.M0030.OrderBy(Function(f) f.BANGUMIKN).ToList
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

            If CondDeskno Is Nothing AndAlso CondKakunin1 Is Nothing AndAlso CondKakunin2 Is Nothing AndAlso CondInstst Is Nothing AndAlso CondInsted Is Nothing AndAlso
                CondDeskid Is Nothing AndAlso CondCatcd Is Nothing AndAlso CondBanguminm Is Nothing AndAlso CondNaiyo Is Nothing AndAlso CondShiftst Is Nothing AndAlso
                CondShifted Is Nothing AndAlso CondAnaid Is Nothing AndAlso CondBangumitanto Is Nothing AndAlso CondBangumirenrk Is Nothing Then

                Dim infoNew As New SortingPagingInfo()
                info.SortField = "New"
                info.SortDirection = "ascending"
                ViewBag.SortingPagingInfo = infoNew
                Return View()
            End If

            Dim d0110 = db.D0110.Include(Function(d) d.M0010).Include(Function(d) d.M0100)

            If Not String.IsNullOrEmpty(CondDeskno) Then
                d0110 = d0110.Where(Function(m) m.DESKNO.Contains(CondDeskno))
            End If

            If CondKakunin1 = "true" And CondKakunin2 = "false" Then
                d0110 = d0110.Where(Function(m) m.KAKUNINID = 5)
            End If

            If CondKakunin2 = "true" And CondKakunin1 = "false" Then
                d0110 = d0110.Where(Function(m) m.KAKUNINID <> 5)
            End If

            Dim searchTimeSt As DateTime = Nothing
            Dim SearchTimeEd As DateTime = Nothing
            If Not String.IsNullOrEmpty(CondInstst) Then
                searchTimeSt = DateTime.Parse(CondInstst & " 00:00")
            End If

            If Not String.IsNullOrEmpty(CondInsted) Then
                Dim dtEndDate As Date = Date.Parse(CondInsted).AddDays(1)
                Dim strEndDate As String = dtEndDate.ToString("yyyy/MM/dd")
                SearchTimeEd = DateTime.Parse(strEndDate & " 00:00")
            End If

            If Not String.IsNullOrEmpty(CondInstst) AndAlso Not String.IsNullOrEmpty(CondInsted) Then
                d0110 = d0110.Where(Function(m) m.UPDTDT >= searchTimeSt And m.UPDTDT < SearchTimeEd)
            ElseIf Not String.IsNullOrEmpty(CondInstst) Then
                d0110 = d0110.Where(Function(m) m.UPDTDT >= searchTimeSt)
            ElseIf Not String.IsNullOrEmpty(CondInsted) Then
                d0110 = d0110.Where(Function(m) m.UPDTDT < SearchTimeEd)
            End If

            If Not String.IsNullOrEmpty(CondDeskid) AndAlso CondDeskid > "0" Then
                d0110 = d0110.Where(Function(m) m.USERID = CondDeskid)
            End If

            If Not String.IsNullOrEmpty(CondCatcd) AndAlso CondCatcd > "0" Then
                d0110 = d0110.Where(Function(m) m.CATCD = CondCatcd)
            End If

            If Not String.IsNullOrEmpty(CondBanguminm) Then
                d0110 = d0110.Where(Function(m) m.BANGUMINM.Contains(CondBanguminm))
            End If

            If Not String.IsNullOrEmpty(CondNaiyo) Then
                d0110 = d0110.Where(Function(m) m.NAIYO.Contains(CondNaiyo))
            End If

            If Not String.IsNullOrEmpty(CondBasyo) Then
                d0110 = d0110.Where(Function(m) m.BASYO.Contains(CondBasyo))
            End If

            If Not String.IsNullOrEmpty(CondShiftst) Then
                If String.IsNullOrEmpty(CondShifted) Then
                    CondShifted = CondShiftst
                End If

                d0110 = d0110.Where(Function(d1) db.D0120.Where(Function(m) CondShiftst <= m.SHIFTYMDED AndAlso CondShifted >= m.SHIFTYMDST).Select(Function(t2) t2.DESKNO).Contains(d1.DESKNO))
            End If

            If Not String.IsNullOrEmpty(CondAnaid) AndAlso CondAnaid > "0" Then
                'd0110 = d0110.Where(Function(d1) db.D0130.Where(Function(m) m.M0010.USERNM.Contains(CondAnanm)).Select(Function(t2) t2.DESKNO).Contains(d1.DESKNO))
                d0110 = d0110.Where(Function(d1) db.D0130.Any(Function(d2) d2.DESKNO = d1.DESKNO AndAlso d2.USERID = CondAnaid))
            End If

            If Not String.IsNullOrEmpty(CondBangumitanto) Then
                d0110 = d0110.Where(Function(m) m.BANGUMITANTO.Contains(CondBangumitanto))
            End If

            If Not String.IsNullOrEmpty(CondBangumirenrk) Then
                d0110 = d0110.Where(Function(m) m.BANGUMIRENRK.Contains(CondBangumirenrk))
            End If

            Dim lstd0110 = d0110.OrderBy(Function(f) f.D0120.Select(Function(t) t.SHIFTYMDST).FirstOrDefault()).ThenBy(Function(f) f.UPDTDT).ToList


            If info IsNot Nothing AndAlso info.SortField IsNot Nothing Then
                Select Case info.SortField

                    Case "DESKNO"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.DESKNO), lstd0110.OrderByDescending(Function(c) c.DESKNO))).ToList

                    Case "UPDTDT"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.UPDTDT), lstd0110.OrderByDescending(Function(c) c.UPDTDT))).ToList

                    Case "BANGUMINM"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.BANGUMINM), lstd0110.OrderByDescending(Function(c) c.BANGUMINM))).ToList

                    Case "NAIYO"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.NAIYO), lstd0110.OrderByDescending(Function(c) c.NAIYO))).ToList

                    Case "DESKMEMO"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.DESKMEMO), lstd0110.OrderByDescending(Function(c) c.DESKMEMO))).ToList

                    Case "KAKUNINNM"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.M0100.KAKUNINNM), lstd0110.OrderByDescending(Function(c) c.M0100.KAKUNINNM))).ToList

                    Case "USERID"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.M0010.USERNM), lstd0110.OrderByDescending(Function(c) c.M0010.USERNM))).ToList

                    Case "CATNM"

                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) If(c.M0020 Is Nothing, String.Empty, c.M0020.CATNM)), lstd0110.OrderByDescending(Function(c) If(c.M0020 Is Nothing, String.Empty, c.M0020.CATNM)))).ToList

                    Case "SHIFTDT"
                        lstd0110 = (If(info.SortDirection = "ascending",
                                      lstd0110.OrderBy(Function(f) f.D0120.Select(Function(t) t.SHIFTYMDST).FirstOrDefault()),
                                      lstd0110.OrderByDescending(Function(f) f.D0120.Select(Function(t) t.SHIFTYMDST).FirstOrDefault()))).ToList()

                    Case "ANA"
                        lstd0110 = (If(info.SortDirection = "ascending",
                                  lstd0110.OrderBy(Function(f) f.D0130.Select(Function(t) t.M0010.USERNM).FirstOrDefault()),
                                  lstd0110.OrderByDescending(Function(f) f.D0130.Select(Function(t) t.M0010.USERNM).FirstOrDefault()))).ToList()

                    Case "BANGUMITANTO"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.BANGUMITANTO), lstd0110.OrderByDescending(Function(c) c.BANGUMITANTO))).ToList

                    Case "BANGUMIRENRK"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.BANGUMIRENRK), lstd0110.OrderByDescending(Function(c) c.BANGUMIRENRK))).ToList

                    Case "BASYO"
                        lstd0110 = (If(info.SortDirection = "ascending", lstd0110.OrderBy(Function(c) c.BASYO), lstd0110.OrderByDescending(Function(c) c.BASYO))).ToList

                End Select

                ViewBag.SortingPagingInfo = info
            Else
                Dim infoNew As New SortingPagingInfo()
                info.SortField = "New"
                info.SortDirection = "ascending"
                ViewBag.SortingPagingInfo = infoNew
			End If

			For Each d0110a In lstd0110
				d0110a.D0130 = d0110a.D0130.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList()
			Next

            Return View(lstd0110)
        End Function

		' GET: A0200/Details/5
		Function Details(ByVal id As String) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim d0110 As D0110 = db.D0110.Find(id)
			If IsNothing(d0110) Then
				Return HttpNotFound()
			End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			d0110.D0130 = d0110.D0130.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList()

			Return View(d0110)
		End Function

		' GET: A0200/Create
		Function Create() As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			'ASI[18 Oct 2019]:To print only these 注意,未決 and 済 radio buttons on デスクメモ create screen,
			'Add where clause which selects only these 3.
			'ASI[27 Nov 2019]:order by added : for order 未決、注意
			ViewBag.KAKUNINID = db.M0100.Where(Function(m) m.KAKUNINID = 2 OrElse m.KAKUNINID = 3 OrElse m.KAKUNINID = 5).OrderByDescending(Function(m1) m1.KAKUNINNM).ToList

			'ViewBag.lstBANGUMINM = (From m In db.M0030 Select m.BANGUMINM).ToList
			'ViewBag.lstNAIYO = (From m In db.M0040 Select m.NAIYO).ToList

			Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim itemM0020 As New M0020
			itemM0020.CATCD = "0"
			itemM0020.CATNM = ""
			lstCATCD.Insert(0, itemM0020)
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

			Dim itemM0010 As New M0010
			itemM0010.USERID = "0"
			itemM0010.USERNM = ""

			Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			lstUSERID.Insert(0, itemM0010)
			ViewBag.lstM0010 = lstUSERID

			'ViewBag.D0130USERID = New SelectList(lstUSERID, "USERID", "USERNM")

			Dim lstAdmin = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.KANRI = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			lstAdmin.Insert(0, itemM0010)

			'管理者の場合、入力者にログインユーザーIDを初期設定する
			Dim loginUserKanri As Boolean = Session("LoginUserKanri")
			If loginUserKanri = True Then
				ViewBag.USERID = New SelectList(lstAdmin, "USERID", "USERNM", loginUserId)
			Else
				ViewBag.USERID = New SelectList(lstAdmin, "USERID", "USERNM", lstAdmin(0).USERID)
			End If

			Return View()
		End Function

		' POST: A0200/Create
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="DESKNO,KAKUNINID,USERID,CATCD,BANGUMINM,NAIYO,BANGUMITANTO,BANGUMIRENRK,DESKMEMO,INPUTDT,BASYO,D0120,D0130")> ByVal d0110 As D0110) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

            If CheckAccessLvl() = False Then
                Return View("ErrorAccesslvl")
            End If

            If ModelState.IsValid Then

                d0110.INPUTDT = DateTime.Today

                Dim decTempDeskno As Decimal = Integer.Parse(DateTime.Today.ToString("yyyyMM") & "000")
                Dim lstdeskno = (From t In db.D0110 Where t.DESKNO > decTempDeskno Select t.DESKNO).ToList
                If lstdeskno.Count > 0 Then
                    decTempDeskno = lstdeskno.Max
                End If

                d0110.DESKNO = decTempDeskno + 1

                Dim lsteda = (From t In db.D0120 Where t.DESKNO = d0110.DESKNO Select t.EDA).ToList
                Dim nexteda As Integer = 0
                If lsteda.Count > 0 Then
                    nexteda = lsteda.Max
                End If
                For Each item In d0110.D0120
                    If item.SHIFTYMDST IsNot Nothing Then
                        item.DESKNO = d0110.DESKNO
                        If item.SHIFTYMDED Is Nothing Then
                            item.SHIFTYMDED = item.SHIFTYMDST
                        End If

                        '拘束時間から：を除外して4桁化する。
                        item.KSKJKNST = ChangeToHHMM(item.KSKJKNST)
                        item.KSKJKNED = ChangeToHHMM(item.KSKJKNED)

                        If String.IsNullOrEmpty(item.KSKJKNST) = False Then
                            item.JTJKNST = GetJtjkn(item.SHIFTYMDST, item.KSKJKNST)
                        End If
                        If String.IsNullOrEmpty(item.KSKJKNED) = False Then
                            item.JTJKNED = GetJtjkn(item.SHIFTYMDED, item.KSKJKNED)
                        End If

                        nexteda = nexteda + 1
                        item.EDA = nexteda
                        db.D0120.Add(item)
                    End If
                Next

                lsteda = (From t In db.D0130 Where t.DESKNO = d0110.DESKNO Select t.EDA).ToList
                nexteda = 0
                If lsteda.Count > 0 Then
                    nexteda = lsteda.Max
                End If
                For Each item In d0110.D0130
                    If item.USERID <> 0 Then
                        item.DESKNO = d0110.DESKNO
                        nexteda = nexteda + 1
                        item.EDA = nexteda
                        db.D0130.Add(item)
                    End If
                Next

                d0110.D0120 = Nothing
                d0110.D0130 = Nothing

                If d0110.CATCD = 0 Then
                    d0110.CATCD = Nothing
                End If

                db.D0110.Add(d0110)

                db.SaveChanges()

                Return RedirectToAction("Index")
            End If

			'ASI[18 Oct 2019]:To print only these 注意,未決 and 済 radio buttons on デスクメモ create screen,
			'Add where clause which selects only these 3.
			'ASI[27 Nov 2019]:order by added : for order 未決、注意
			ViewBag.KAKUNINID = db.M0100.Where(Function(m) m.KAKUNINID = 2 OrElse m.KAKUNINID = 3 OrElse m.KAKUNINID = 5).OrderByDescending(Function(m1) m1.KAKUNINNM).ToList

			'ViewBag.lstBANGUMINM = (From m In db.M0030 Select m.BANGUMINM).ToList
			'ViewBag.lstNAIYO = (From m In db.M0040 Select m.NAIYO).ToList

			Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            Dim itemM0020 As New M0020
            itemM0020.CATCD = "0"
            itemM0020.CATNM = ""
            lstCATCD.Insert(0, itemM0020)
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

            Dim itemM0010 As New M0010
            itemM0010.USERID = "0"
            itemM0010.USERNM = ""

            Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            lstUSERID.Insert(0, itemM0010)
            ViewBag.lstM0010 = lstUSERID

            'ViewBag.D0130USERID = New SelectList(lstUSERID, "USERID", "USERNM")

            Dim lstAdmin = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.KANRI = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            lstAdmin.Insert(0, itemM0010)

            '管理者の場合、入力者にログインユーザーIDを初期設定する
            Dim loginUserKanri As Boolean = Session("LoginUserKanri")
            If loginUserKanri = True Then
                ViewBag.USERID = New SelectList(lstAdmin, "USERID", "USERNM", loginUserId)
            Else
                ViewBag.USERID = New SelectList(lstAdmin, "USERID", "USERNM", lstAdmin(0).USERID)
            End If

            Return View(d0110)
        End Function

		' GET: A0200/Edit/5
		Function Edit(ByVal id As String) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim d0110 As D0110 = db.D0110.Find(id)
			If IsNothing(d0110) Then
				Return HttpNotFound()
			End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			'd0110.D0120 = db.D0120.Where(Function(f) f.DESKNO = d0110.DESKNO).ToList
			'd0110.D0130 = db.D0130.Include(Function(d) d.M0010).Where(Function(f) f.DESKNO = d0110.DESKNO).ToList

			'ASI[27 Nov 2019]:order by added : for order 未決、注意
			ViewBag.KAKUNINID = db.M0100.OrderByDescending(Function(m1) m1.KAKUNINNM).ToList
			'ViewBag.lstBANGUMINM = (From m In db.M0030 Select m.BANGUMINM).ToList
			'ViewBag.lstNAIYO = (From m In db.M0040 Select m.NAIYO).ToList

			Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim itemM0020 As New M0020
			itemM0020.CATCD = "0"
			itemM0020.CATNM = ""
			lstCATCD.Insert(0, itemM0020)
			ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM", d0110.CATCD)


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

			Dim itemM0010 As New M0010
			itemM0010.USERID = "0"
			itemM0010.USERNM = ""

			Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			lstUSERID.Insert(0, itemM0010)
			ViewBag.lstM0010 = lstUSERID

			'ViewBag.D0130USERID = New SelectList(lstUSERID, "USERID", "USERNM")

			Dim lstAdmin = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.KANRI = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			lstAdmin.Insert(0, itemM0010)
			ViewBag.USERID = New SelectList(lstAdmin, "USERID", "USERNM", d0110.USERID)

			d0110.D0130 = d0110.D0130.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList()

			Return View(d0110)
		End Function

		' POST: A0200/Edit/5
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="DESKNO,KAKUNINID,USERID,CATCD,BANGUMINM,NAIYO,BANGUMITANTO,BANGUMIRENRK,DESKMEMO,BASYO,INPUTDT,D0120,D0130")> ByVal d0110 As D0110) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

            If CheckAccessLvl() = False Then
                Return View("ErrorAccesslvl")
            End If

            If ModelState.IsValid Then
                Dim nexteda As Integer = 0
                Dim listdbD0120 = (From t In db.D0120 Where t.DESKNO = d0110.DESKNO).ToList
                If listdbD0120.Count > 0 Then
                    nexteda = (From d In listdbD0120 Select d.EDA).Max
                End If

                For Each itemdb In listdbD0120
                    Dim bolExist As Boolean = False
                    For Each item In d0110.D0120
                        If itemdb.EDA = item.EDA Then
                            If item.SHIFTYMDST IsNot Nothing OrElse item.SHIFTYMDED IsNot Nothing Then
                                bolExist = True
                                db.Entry(itemdb).State = EntityState.Detached

                                If item.SHIFTYMDED Is Nothing Then
                                    item.SHIFTYMDED = item.SHIFTYMDST
                                End If

                                '拘束時間から：を除外して4桁化する。
                                item.KSKJKNST = ChangeToHHMM(item.KSKJKNST)
                                item.KSKJKNED = ChangeToHHMM(item.KSKJKNED)

                                If itemdb.SHIFTYMDST <> item.SHIFTYMDST OrElse itemdb.SHIFTYMDED <> item.SHIFTYMDED OrElse
                                    itemdb.KSKJKNST <> item.KSKJKNST OrElse itemdb.KSKJKNED <> item.KSKJKNED OrElse
                                    (itemdb.SHIFTYMDST Is Nothing AndAlso item.SHIFTYMDST IsNot Nothing) OrElse (itemdb.SHIFTYMDST IsNot Nothing AndAlso item.SHIFTYMDST Is Nothing) OrElse
                                    (itemdb.SHIFTYMDED Is Nothing AndAlso item.SHIFTYMDED IsNot Nothing) OrElse (itemdb.SHIFTYMDED IsNot Nothing AndAlso item.SHIFTYMDED Is Nothing) OrElse
                                    (itemdb.KSKJKNST Is Nothing AndAlso item.KSKJKNST IsNot Nothing) OrElse (itemdb.KSKJKNST IsNot Nothing AndAlso item.KSKJKNST Is Nothing) OrElse
                                    (itemdb.KSKJKNED Is Nothing AndAlso item.KSKJKNED IsNot Nothing) OrElse (itemdb.KSKJKNED IsNot Nothing AndAlso item.KSKJKNED Is Nothing) Then

                                    item.DESKNO = d0110.DESKNO
                                    If String.IsNullOrEmpty(item.KSKJKNST) = False Then
                                        item.JTJKNST = GetJtjkn(item.SHIFTYMDST, item.KSKJKNST)
                                    End If
                                    If String.IsNullOrEmpty(item.KSKJKNED) = False Then
                                        item.JTJKNED = GetJtjkn(item.SHIFTYMDED, item.KSKJKNED)
                                    End If
                                    db.Entry(item).State = EntityState.Modified
                                End If
                            End If
                            Exit For
                        End If
                    Next
                    If bolExist = False Then
                        db.D0120.Remove(itemdb)
                    End If
                Next

                For Each item In d0110.D0120
                    If item.EDA = 0 Then
                        If item.SHIFTYMDST IsNot Nothing OrElse item.SHIFTYMDED IsNot Nothing Then
                            nexteda = nexteda + 1
                            item.EDA = nexteda
                            item.DESKNO = d0110.DESKNO

                            If item.SHIFTYMDED Is Nothing Then
                                item.SHIFTYMDED = item.SHIFTYMDST
                            End If

                            '拘束時間から：を除外して4桁化する。
                            item.KSKJKNST = ChangeToHHMM(item.KSKJKNST)
                            item.KSKJKNED = ChangeToHHMM(item.KSKJKNED)

                            If String.IsNullOrEmpty(item.KSKJKNST) = False Then
                                item.JTJKNST = GetJtjkn(item.SHIFTYMDST, item.KSKJKNST)
                            End If
                            If String.IsNullOrEmpty(item.KSKJKNED) = False Then
                                item.JTJKNED = GetJtjkn(item.SHIFTYMDED, item.KSKJKNED)
                            End If
                            db.D0120.Add(item)
                        End If
                    End If
                Next

                nexteda = 0
                Dim dblistD0130 = (From t In db.D0130 Where t.DESKNO = d0110.DESKNO).ToList
                If dblistD0130.Count > 0 Then
                    nexteda = (From d In dblistD0130 Select d.EDA).Max
                End If

                For Each itemdb In dblistD0130
                    Dim bolExist As Boolean = False
                    For Each item In d0110.D0130
                        If itemdb.EDA = item.EDA Then
                            If item.USERID IsNot Nothing AndAlso item.USERID > 0 Then
                                bolExist = True
                                db.Entry(itemdb).State = EntityState.Detached
                                If itemdb.USERID <> item.USERID Then
                                    item.DESKNO = d0110.DESKNO
                                    db.Entry(item).State = EntityState.Modified
                                End If
                            End If
                            Exit For
                        End If
                    Next
                    If bolExist = False Then
                        db.D0130.Remove(itemdb)
                    End If
                Next

                For Each item In d0110.D0130
                    If item.EDA = 0 Then
                        If item.USERID IsNot Nothing AndAlso item.USERID > 0 Then
                            nexteda = nexteda + 1
                            item.EDA = nexteda
                            item.DESKNO = d0110.DESKNO
                            db.D0130.Add(item)
                        End If
                    End If
                Next

                d0110.D0120 = Nothing
                d0110.D0130 = Nothing

                If d0110.CATCD = 0 Then
                    d0110.CATCD = Nothing
                End If

                db.Entry(d0110).State = EntityState.Modified

                db.SaveChanges()

				Return RedirectToAction("Index", routeValues:=New With {.CondDeskno = Session("CondDeskno"), .CondKakunin1 = Session("CondKakunin1"), .CondKakunin2 = Session("CondKakunin2"),
						.CondInstst = Session("CondInstst"), .CondInsted = Session("CondInsted"), .CondDeskid = Session("CondDeskid"), .CondCatcd = Session("CondCatcd"),
						.CondBanguminm = Session("CondBanguminm"), .CondNaiyo = Session("CondNaiyo"), .CondShiftst = Session("CondShiftst"), .CondShifted = Session("CondShifted"),
						.CondAnaid = Session("CondAnaid"), .CondBangumitanto = Session("CondBangumitanto"), .CondBangumirenrk = Session("CondBangumirenrk"), .CondBasyo = Session("CondBasyo")})
            End If

			'ASI[27 Nov 2019]:order by added : for order 未決、注意
			ViewBag.KAKUNINID = db.M0100.OrderByDescending(Function(m1) m1.KAKUNINNM).ToList
			'ViewBag.lstBANGUMINM = (From m In db.M0030 Select m.BANGUMINM).ToList
			'ViewBag.lstNAIYO = (From m In db.M0040 Select m.NAIYO).ToList

			Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            Dim itemM0020 As New M0020
            itemM0020.CATCD = "0"
            itemM0020.CATNM = ""
            lstCATCD.Insert(0, itemM0020)
            ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM", d0110.CATCD)

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

            Dim itemM0010 As New M0010
            itemM0010.USERID = "0"
            itemM0010.USERNM = ""

            Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            lstUSERID.Insert(0, itemM0010)
            ViewBag.lstM0010 = lstUSERID

            'ViewBag.D0130USERID = New SelectList(lstUSERID, "USERID", "USERNM")

            Dim lstAdmin = db.M0010.Where(Function(m) m.HYOJ = True And m.STATUS = True And m.KARIANA = False AndAlso m.M0050.KANRI = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            lstAdmin.Insert(0, itemM0010)
            ViewBag.USERID = New SelectList(lstAdmin, "USERID", "USERNM", d0110.USERID)

            Return View(d0110)
        End Function

		' GET: A0200/Delete/5
		Function Delete(ByVal id As String) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0110 As D0110 = db.D0110.Find(id)
			If IsNothing(d0110) Then
				Return HttpNotFound()
			End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			d0110.D0130 = d0110.D0130.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList()

			Return View(d0110)
		End Function


		' POST: A0200/Delete/5
		<HttpPost()>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken()>
		Function DeleteConfirmed(ByVal id As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim d0110 As D0110 = db.D0110.Find(id)
			db.D0110.Remove(d0110)
			For Each item In d0110.D0120
				db.D0120.Remove(item)
			Next
			For Each item In d0110.D0130
				db.D0130.Remove(item)
			Next
			db.SaveChanges()

			Return RedirectToAction("Index", routeValues:=New With {.CondDeskno = Session("CondDeskno"), .CondKakunin1 = Session("CondKakunin1"), .CondKakunin2 = Session("CondKakunin2"),
					.CondInstst = Session("CondInstst"), .CondInsted = Session("CondInsted"), .CondDeskid = Session("CondDeskid"), .CondCatcd = Session("CondCatcd"),
					.CondBanguminm = Session("CondBanguminm"), .CondNaiyo = Session("CondNaiyo"), .CondShiftst = Session("CondShiftst"), .CondShifted = Session("CondShifted"),
					.CondAnaid = Session("CondAnaid"), .CondBangumitanto = Session("CondBangumitanto"), .CondBangumirenrk = Session("CondBangumirenrk"), .CondBasyo = Session("CondBasyo")})
		End Function

		<OutputCache(Duration:=0)>
		Function CheckWBooking(ByVal deskno As String, ByVal eda As Integer) As String
			Dim strRtn As String = ""

			If Request.IsAjaxRequest() Then
				Dim strTntnm As String = ""

				Exe_pr_a0200_chkwbooking(deskno, eda, strTntnm)

				If String.IsNullOrEmpty(strTntnm) = False Then
					strRtn = String.Format("担当アナ「{0}」がWブッキングになります。" & vbCrLf & "よろしいですか？", strTntnm)
				End If
			End If

			Return strRtn
		End Function

		Function Exe_pr_a0200_chkwbooking(ByVal strDeskno As String, ByVal intEda As Integer, ByRef strTntnm As String) As Boolean

			Dim sqlpara1 As New SqlParameter("av_deskno", SqlDbType.VarChar, 9)
			sqlpara1.Value = strDeskno

			Dim sqlpara2 As New SqlParameter("an_eda", SqlDbType.SmallInt)
			sqlpara2.Value = intEda

			Dim sqlpara3 As New SqlParameter("av_tntnm", SqlDbType.VarChar, 100)
			sqlpara3.Direction = ParameterDirection.Output

			Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_a0200_chkwbooking @av_deskno, @an_eda, @av_tntnm OUTPUT", sqlpara1, sqlpara2, sqlpara3)

			strTntnm = sqlpara3.Value

			Return True
		End Function

		Function ChangeToHHMM(ByVal strTime As String)

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

		Function GetJtjkn(ByVal dtymd As Date, ByVal strHHMM As String) As Date

			Dim dtRtn As Date = Nothing
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

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace
