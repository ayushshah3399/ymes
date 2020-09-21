Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports NTV_SHIFT

Namespace Controllers
    Public Class A0180Controller
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

        ' GET: A0180
        Function Index(ByVal searchdt As String, ByVal Kyuka As System.Nullable(Of Boolean), ByVal info As SortingPagingInfo) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")


            If CheckAccessLvl() = False Then
                Return View("ErrorAccesslvl")
            End If

            ViewData!LoginUsernm = Session("LoginUsernm")
            ViewBag.LoginUserSystem = Session("LoginUserSystem")
            ViewBag.LoginUserKanri = Session("LoginUserKanri")
            ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

            '一括業務メニューを表示可能な条件追加
            Dim intUserid As Integer = Integer.Parse(loginUserId)
            Dim m0010KOKYU = db.M0010.Find(intUserid)
            ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
            ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL

            If String.IsNullOrEmpty(searchdt) Then
                searchdt = Today.ToString("yyyy/MM/dd")
            End If

            ViewData("searchdt") = searchdt

            Dim D0150 = From m In db.D0150 Select m

            If Kyuka.HasValue = True Then
                If Kyuka.Value = True Then
                    D0150 = D0150.Where(Function(m) (searchdt) <= m.KKNED AndAlso (searchdt) >= m.KKNST).OrderByDescending(Function(f) f.KKNST).ThenByDescending(Function(f) f.JKNST)
                ElseIf Kyuka.Value = False Then
                    'd0070 = d0070.Where(Function(m) m.SYUSEIYMD.ToString.Substring(0, 10) = (searchdt))
                    'd0070 = d0070.Where(Function(m) Date.Parse(m.SYUSEIYMD).ToString("yyyy/MM/dd") = (searchdt))
                    Dim searchTimeSt As DateTime = DateTime.Parse(searchdt & " 00:00")
                    Dim dtEndDate As Date = Date.Parse(searchdt).AddDays(1)
                    Dim strEndDate As String = dtEndDate.ToString("yyyy/MM/dd")
                    Dim SearchTimeEd As DateTime = DateTime.Parse(strEndDate & " 00:00")
                    D0150 = D0150.Where(Function(m) (searchTimeSt) <= m.SYUSEIYMD AndAlso (SearchTimeEd) > m.SYUSEIYMD).OrderByDescending(Function(f) f.SYUSEIYMD)
                End If
            Else
                D0150 = D0150.Where(Function(m) (searchdt) <= m.KKNED AndAlso (searchdt) >= m.KKNST).OrderByDescending(Function(f) f.KKNST).ThenByDescending(Function(f) f.JKNST)
            End If

            Dim lstD0050 = D0150.ToList

            If info IsNot Nothing AndAlso info.SortField IsNot Nothing Then
                Select Case info.SortField

                    Case "HENKONAIYO"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.HENKONAIYO), lstD0050.OrderByDescending(Function(c) c.HENKONAIYO))).ToList

                    Case "USERNM"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.M0010.USERNM), lstD0050.OrderByDescending(Function(c) c.M0010.USERNM))).ToList

                    Case "UPDTDT"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.UPDTDT), lstD0050.OrderByDescending(Function(c) c.UPDTDT))).ToList

                    Case "KKNST"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.KKNST), lstD0050.OrderByDescending(Function(c) c.KKNST))).ToList

                    Case "JKNST"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.JKNST), lstD0050.OrderByDescending(Function(c) c.JKNST))).ToList

                    Case "SHINSEIUSER"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.SHINSEIUSER), lstD0050.OrderByDescending(Function(c) c.SHINSEIUSER))).ToList

                    Case "KYUKNM"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.KYUKNM), lstD0050.OrderByDescending(Function(c) c.KYUKNM))).ToList

                    Case "GYOMMEMO"
                        lstD0050 = (If(info.SortDirection = "ascending", lstD0050.OrderBy(Function(c) c.GYOMMEMO), lstD0050.OrderByDescending(Function(c) c.GYOMMEMO))).ToList

                End Select

                ViewBag.SortingPagingInfo = info
            Else
                Dim infoNew As New SortingPagingInfo()
                info.SortField = "New"
                info.SortDirection = "ascending"
                ViewBag.SortingPagingInfo = infoNew
            End If

            Return View(lstD0050)
        End Function

        ' GET: A0180/Details/5
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0060 As D0060 = db.D0060.Find(id)
            If IsNothing(d0060) Then
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

            Return View(d0060)
        End Function

        ' GET: A0180/Create
		Function Create() As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID")
			ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM")
			Return View()
		End Function

        ' POST: A0180/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0060 As D0060) As ActionResult
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If ModelState.IsValid Then
				db.D0060.Add(d0060)
				db.SaveChanges()
				Return RedirectToAction("Index")
			End If
			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
			ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
			Return View(d0060)
        End Function

        ' GET: A0180/Edit/5
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0060 As D0060 = db.D0060.Find(id)
            If IsNothing(d0060) Then
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

            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
            Return View(d0060)
        End Function

        ' POST: A0180/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0060 As D0060) As ActionResult
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If ModelState.IsValid Then
				db.Entry(d0060).State = EntityState.Modified
				db.SaveChanges()
				Return RedirectToAction("Index")
			End If
			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
			ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
			Return View(d0060)
        End Function

        ' GET: A0180/Delete/5
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0060 As D0060 = db.D0060.Find(id)
            If IsNothing(d0060) Then
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

            Return View(d0060)
        End Function

        ' POST: A0180/Delete/5
		<HttpPost()>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken()>
		Function DeleteConfirmed(ByVal id As Decimal) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim d0060 As D0060 = db.D0060.Find(id)
			db.D0060.Remove(d0060)
			db.SaveChanges()
			Return RedirectToAction("Index")
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
    End Class
End Namespace
