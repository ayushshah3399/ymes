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
    Public Class A0120Controller
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

        ' GET: A0120
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

            Dim M0020 = db.M0020.OrderBy(Function(f) f.HYOJJN)

            Return View(M0020.ToList())
        End Function

        ' GET: A0120/Details/5
        Function Details(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0020 As M0020 = db.M0020.Find(id)
			If IsNothing(m0020) Then
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

            Return View(m0020)
        End Function

        ' GET: A0120/Create
        Function Create() As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim M0020 = db.M0020.OrderBy(Function(f) f.HYOJJN)
            ViewData.Add("List", M0020.ToList())
            Return View()
        End Function

        ' POST: A0120/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="CATCD,CATNM,HYOJJN,HYOJ,OATIMEHYOJ,BANGUMIHYOJ,KSKHYOJ,ANAHYOJ,BASYOHYOJ,KKNHYOJ,BIKOHYOJ,TANTOHYOJ,RENRKHYOJ,SYUCHO,STATUS,ALERTFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,NAIYOHYOJ")> ByVal m0020 As M0020) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

            If CheckAccessLvl() = False Then
                Return View("ErrorAccesslvl")
            End If

            'If ModelState.IsValid Then
            '    db.M0020.Add(m0020)
            '    db.SaveChanges()
            '    Return RedirectToAction("Index")
            'End If

            If ModelState.IsValid Then
                'Dim maxid = (From c In db.M0020.ToList Select c.CATCD).Max()
                'm0020.CATCD = maxid + 1

                If m0020.HYOJ.Equals(False) Then
                    m0020.HYOJJN = "999"
                End If
                db.M0020.Add(m0020)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewData.Add("List", db.M0020.ToList())
            Return View(m0020)
        End Function

        ' GET: A0120/Edit/5
        Function Edit(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0020 As M0020 = db.M0020.Find(id)
			If IsNothing(m0020) Then
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

            Dim m0020list = db.M0020.OrderBy(Function(f) f.HYOJJN)
            ViewData.Add("List", m0020list.ToList())
            Return View(m0020)
        End Function

        ' POST: A0120/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="CATCD,CATNM,HYOJJN,HYOJ,OATIMEHYOJ,BANGUMIHYOJ,KSKHYOJ,ANAHYOJ,BASYOHYOJ,KKNHYOJ,BIKOHYOJ,TANTOHYOJ,RENRKHYOJ,SYUCHO,STATUS,ALERTFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,NAIYOHYOJ")> ByVal m0020 As M0020) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

            If CheckAccessLvl() = False Then
                Return View("ErrorAccesslvl")
            End If

            If ModelState.IsValid Then
                If m0020.HYOJ.Equals(False) Then
                    m0020.HYOJJN = "999"
                End If
                db.Entry(m0020).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewData.Add("List", db.M0020.ToList())
            Return View(m0020)
        End Function

        ' GET: A0120/Delete/5
		Function Delete(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0020 As M0020 = db.M0020.Find(id)
			If IsNothing(m0020) Then
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

			Return View(m0020)
		End Function

        ' POST: A0120/Delete/5
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

			Dim m0020 As M0020 = db.M0020.Find(id)
            db.M0020.Remove(m0020)
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
