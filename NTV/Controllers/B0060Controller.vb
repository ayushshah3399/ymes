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
    Public Class B0060Controller
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

        ' GET: B0060
        Function Index() As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim d0090 = db.D0090.Where(Function(f) f.FMTKBN = 1 And f.STATUS = 0 And ((f.DATAKBN = 1 And f.SIYOUSERID = loginUserId) Or f.DATAKBN = 2)).OrderBy(Function(f) f.DATAKBN).ThenBy(Function(f) f.GYOMYMD).ThenBy(Function(f) f.KSKJKNST).ThenBy(Function(f) f.KSKJKNED).ThenBy(Function(f) f.BANGUMINM).Include(Function(d) d.M0010).Include(Function(d) d.M0020)

			For Each d9 In d0090
				d9.D0100 = d9.D0100.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
			Next

            Return View(d0090.ToList())
        End Function

		'POST: B0060
		<HttpPost()>
		Function Index(<Bind(Include:="FLGDEL,HINANO,HINAMEMO,FMTKBN,DATAKBN,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,SIYOFLG,SIYOUSERID,SIYODATE,STATUS,INSTID,INSTDT")> ByVal lstd0090 As List(Of D0090)) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			For Each item In lstd0090
				If item.FLGDEL = True Then
					Dim d0090 As D0090 = db.D0090.Find(item.HINANO)
					db.D0090.Remove(d0090)
				End If
			Next

			db.SaveChanges()

			Return RedirectToAction("Index")
		End Function

        ' GET: B0060/Details/5
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0090 As D0090 = db.D0090.Find(id)
            If IsNothing(d0090) Then
                Return HttpNotFound()
            End If
            Return View(d0090)
        End Function

        ' GET: B0060/Create
        Function Create() As ActionResult
            ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID")
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM")
            Return View()
        End Function

        ' POST: B0060/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="HINANO,HINAMEMO,FMTKBN,DATAKBN,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,SIYOFLG,SIYOUSERID,SIYODATE,STATUS,INSTID,INSTDT")> ByVal d0090 As D0090) As ActionResult
            If ModelState.IsValid Then
                db.D0090.Add(d0090)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID", d0090.SIYOUSERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0090.CATCD)
            Return View(d0090)
        End Function

        ' GET: B0060/Edit/5
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0090 As D0090 = db.D0090.Find(id)
            If IsNothing(d0090) Then
                Return HttpNotFound()
            End If
            ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID", d0090.SIYOUSERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0090.CATCD)
            Return View(d0090)
        End Function

        ' POST: B0060/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="HINANO,HINAMEMO,FMTKBN,DATAKBN,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,SIYOFLG,SIYOUSERID,SIYODATE,STATUS,INSTID,INSTDT")> ByVal d0090 As D0090) As ActionResult
            If ModelState.IsValid Then
                db.Entry(d0090).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID", d0090.SIYOUSERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0090.CATCD)
            Return View(d0090)
        End Function

        ' GET: B0060/Delete/5
		Function Delete(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim d0090 As D0090 = db.D0090.Find(id)
			If IsNothing(d0090) Then
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

			ViewData!LoginUsernm = Session("LoginUsernm")

			Return View(D0090)
		End Function

        ' POST: B0060/Delete/5
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

			Dim d0090 As D0090 = db.D0090.Find(id)
			db.D0090.Remove(d0090)
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
