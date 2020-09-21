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
    Public Class B0030Controller
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

        ' GET: D0050
        Function Index(ByVal msgShow As String) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

            '伝言板表示・非表示
            If msgShow IsNot Nothing Then
                Session("B0030msgShow") = msgShow
            End If

            If CheckAccessLvl() = False Then
                Return View("ErrorAccesslvl")
            End If

            Dim d0050 = db.D0050.Include(Function(d) d.M0010).Include(Function(d) d.M0020).OrderBy(Function(m) m.GYOMYMD)
            d0050 = d0050.Where(Function(m) m.SHONINFLG = "0")

            Dim item As New D0080
            item.USERID = loginUserId
            item.TOROKUYMD = Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
            item.DATAFLG = "1"
            ViewData.Add("Message", item)

            Dim d0080 = db.D0080.Where(Function(m) m.DATAFLG = "1")
            d0080 = d0080.OrderByDescending(Function(f) f.DNGNNO)
            ViewData.Add("MessageList", d0080.ToList())

            Return View(d0050.ToList())
        End Function

        <HttpPost()>
        Function CheckD0050(ByVal id As Decimal) As JsonResult

            '承認する年月が公休展開されているかチェック
            TempData("success") = ""
            Dim d0050 As D0050 = db.D0050.Find(id)
            Dim strMessage As String = ""
            Dim strError As String = ""
            Dim strNengetsu = d0050.GYOMYMDED.ToString.Substring(0, 7)
          
            strNengetsu = strNengetsu.Replace("/", "")
            Dim intUserid As Integer = Integer.Parse(d0050.USERID)

            '公休展開のチェック
            Dim intID As Integer = Integer.Parse(intUserid)
            Dim intSearchdt As Integer = Integer.Parse(strNengetsu)
            Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
            If d0030Status Is Nothing Then
                strError = String.Format("公休展開されていないため、承認できません。")
                Return Json(New With {.success = False, .text = strError})
            End If

            Return Json(New With {.success = True, .text = strMessage})

        End Function

        ' GET: D0050/Details/5
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0050 As D0050 = db.D0050.Find(id)
            If IsNothing(d0050) Then
                Return HttpNotFound()
            End If
            Return View(d0050)
        End Function

        ' GET: D0050/Create
        Function Create() As ActionResult
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID")
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM")
            Return View()
        End Function

        ' POST: D0050/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="GYOMSNSNO,USERID,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,NAIYO,BASYO,GYOMMEMO,BANGUMITANTO,BANGUMIRENRK,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0050 As D0050) As ActionResult
            If ModelState.IsValid Then
                db.D0050.Add(d0050)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0050.USERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0050.CATCD)
            Return View(d0050)
        End Function

        ' GET: D0050/Edit/5
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0050 As D0050 = db.D0050.Find(id)
            If IsNothing(d0050) Then
                Return HttpNotFound()
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0050.USERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0050.CATCD)
            Return View(d0050)
        End Function

        ' POST: D0050/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="GYOMSNSNO,USERID,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,NAIYO,BASYO,GYOMMEMO,BANGUMITANTO,BANGUMIRENRK,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0050 As D0050) As ActionResult
            If ModelState.IsValid Then
                db.Entry(d0050).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0050.USERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0050.CATCD)
            Return View(d0050)
        End Function

        ' GET: D0050/Delete/5
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0050 As D0050 = db.D0050.Find(id)
			If IsNothing(d0050) Then
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

			If Request.UrlReferrer IsNot Nothing AndAlso Not Request.UrlReferrer.ToString.Contains("B0030/Delete") Then
				Session("B0030DeleteRtnUrl") = Request.UrlReferrer.ToString
			End If

			Return View(d0050)
        End Function

        ' POST: D0050/Delete/5
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

			Dim intShorikbn As Integer = 5

			'メール送信用のワークテーブルの初期化
			Dim lstw0040 = db.W0040.Where(Function(w) w.ACUSERID = loginUserId And w.SHORIKBN = intShorikbn)
			If lstw0040.Count > 0 Then
				For Each item In lstw0040
					db.W0040.Remove(item)
				Next

				db.SaveChanges()
			End If

			Dim d0050 As D0050 = db.D0050.Find(id)
			'却下
			d0050.SHONINFLG = "2"


			'メール送信用にデータを作成
			Dim w0040 As New W0040
			w0040.ACUSERID = loginUserId
			w0040.SHORIKBN = intShorikbn
			w0040.GYOMNO = d0050.GYOMSNSNO
			w0040.UPDTDT = Now
			CopyGyom(w0040, d0050)
			db.W0040.Add(w0040)

			Dim w0050 As New W0050
			w0050.ACUSERID = w0040.ACUSERID
			w0050.SHORIKBN = w0040.SHORIKBN
			w0050.GYOMNO = w0040.GYOMNO
			w0050.USERID = d0050.USERID
			db.W0050.Add(w0050)

			db.SaveChanges()

			Return RedirectToAction("SendMail", "B0020", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn})

			'Return RedirectToAction("Index")
		End Function

		Sub CopyGyom(ByRef w0040 As W0040, ByVal d0050 As D0050)
			w0040.GYOMYMD = d0050.GYOMYMD
			w0040.GYOMYMDED = d0050.GYOMYMDED
			w0040.KSKJKNST = d0050.KSKJKNST
			w0040.KSKJKNED = d0050.KSKJKNED
			w0040.CATCD = d0050.CATCD
			w0040.BANGUMINM = d0050.BANGUMINM
			w0040.NAIYO = d0050.NAIYO
			w0040.BASYO = d0050.BASYO
			w0040.BANGUMITANTO = d0050.BANGUMITANTO
			w0040.BANGUMIRENRK = d0050.BANGUMIRENRK
		End Sub


        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <HttpPost()>
       <ValidateAntiForgeryToken()>
        Function DeleteD0080(ByVal DNGNNO As Decimal) As ActionResult
            Dim d0080 As D0080 = db.D0080.Find(DNGNNO)
            db.D0080.Remove(d0080)
            db.SaveChanges()
            'Return PartialView("ShowMessage", db.D0080.ToList())
            Return RedirectToAction("Index")
        End Function

    End Class
End Namespace
