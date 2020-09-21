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
    Public Class A0130Controller
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

        ' GET: M0030
        Function Index() As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")


			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			ViewBag.LoginUserSystem = Session("LoginUserSystem")
            ViewBag.LoginUserKanri = Session("LoginUserKanri")
            ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

            '一括業務メニューを表示可能な条件追加
            Dim intUserid As Integer = Integer.Parse(loginUserId)
            Dim m0010KOKYU = db.M0010.Find(intUserid)
            ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
            ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL

            Dim M0030 = db.M0030.OrderBy(Function(f) f.BANGUMIKN)
            Return View(M0030.ToList())

        End Function

        ' GET: M0030/Details/5
        Function Details(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0030 As M0030 = db.M0030.Find(id)
			If IsNothing(m0030) Then
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

            Return View(m0030)
        End Function

        ' GET: M0030/Create
        Function Create() As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If
			Return View()
        End Function

        ' POST: M0030/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="BANGUMICD,BANGUMINM,BANGUMIKN")> ByVal m0030 As M0030) As ActionResult
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If ModelState.IsValid Then
				db.M0030.Add(m0030)
				db.SaveChanges()
				Return RedirectToAction("Index")
			End If
			'If ModelState.IsValid Then
			'    Dim maxid = (From c In db.M0030.ToList Select c.BANGUMICD).Max()
			'    m0030.BANGUMICD = maxid + 1
			'    db.M0030.Add(m0030)
			'    db.SaveChanges()
			'    Return RedirectToAction("Index")
			'End If

            Return View(m0030)
        End Function

        ' GET: M0030/Edit/5
        Function Edit(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0030 As M0030 = db.M0030.Find(id)
			If IsNothing(m0030) Then
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

			Return View(m0030)
        End Function

        ' POST: M0030/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="BANGUMICD,BANGUMINM,BANGUMIKN")> ByVal m0030 As M0030) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If ModelState.IsValid Then
				db.Entry(m0030).State = EntityState.Modified
				db.SaveChanges()
				Return RedirectToAction("Index")
			End If
            Return View(m0030)
        End Function

        ' GET: M0030/Delete/5
        Function Delete(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0030 As M0030 = db.M0030.Find(id)
			If IsNothing(m0030) Then
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

            Return View(m0030)
        End Function

        ' POST: M0030/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Short) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim m0030 As M0030 = db.M0030.Find(id)
            db.M0030.Remove(m0030)
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
