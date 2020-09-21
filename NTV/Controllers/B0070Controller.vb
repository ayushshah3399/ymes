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
    Public Class B0070Controller
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

		' GET: D0090
        Function Index(ByVal siyoflg As String, ByVal formname As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

            ViewData("FormName") = formname
            Dim d0090 = db.D0090.Where(Function(f) f.FMTKBN = 2 And ((f.DATAKBN = 1 And f.SIYOUSERID = loginUserId) Or f.DATAKBN = 2)).OrderBy(Function(f) f.DATAKBN).ThenBy(Function(f) f.M0020.HYOJJN).ThenBy(Function(f) f.OAJKNST).ThenBy(Function(f) f.OAJKNED).ThenBy(Function(f) f.BANGUMINM).Include(Function(d) d.M0010).Include(Function(d) d.M0020)

            If Not String.IsNullOrEmpty(siyoflg) Then
                If siyoflg = "2" Then
                    d0090 = d0090.Where(Function(m) m.SIYOFLG = True)
                ElseIf siyoflg = "3" Then
                    d0090 = d0090.Where(Function(m) m.SIYOFLG = False)
                End If
            End If

            Dim dicSIYOFLG As New Dictionary(Of Integer, String)
            dicSIYOFLG.Add(1, "全て表示")
            dicSIYOFLG.Add(2, "登録済み")
            dicSIYOFLG.Add(3, "未登録")

            ViewBag.SIYOFLG = New SelectList(dicSIYOFLG.Select(Function(f) New With {.Value = f.Key, .Text = f.Value}), "Value", "Text")

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

        Function D0010_Shitagaki() As ActionResult
            Dim d0090 = db.D0090.Include(Function(d) d.M0010).Include(Function(d) d.M0020)
            Return View(d0090.ToList())
        End Function

        ' GET: D0090/Details/5
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

        ' GET: D0090/Create
        Function Create(para1 As String, para2 As String, para3 As String, para4 As String, para5 As String, para6 As String, para7 As String, _
                        para8 As String, para9 As String, para10 As String, para11 As String, para12 As String, para13 As String, ByVal ptnflg As System.Nullable(Of Boolean), _
                        ByVal ptn1 As System.Nullable(Of Boolean), ByVal ptn2 As System.Nullable(Of Boolean), ByVal ptn3 As System.Nullable(Of Boolean), _
                        ByVal ptn4 As System.Nullable(Of Boolean), ByVal ptn5 As System.Nullable(Of Boolean), ByVal ptn6 As System.Nullable(Of Boolean), _
                        ByVal ptn7 As System.Nullable(Of Boolean), ByVal fmtkbn As String, ByVal userid As String) As ActionResult
            ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID")
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM")

            If Not String.IsNullOrEmpty(para1) Then

                Dim d0090 As New D0090
                Dim d0100 As New D0100
                d0090.BANGUMINM = para1
                d0090.BANGUMITANTO = para2
                d0090.BANGUMIRENRK = para3
                d0090.BIKO = para4
                d0090.KSKJKNST = para5
                d0090.KSKJKNED = para6
                d0090.CATCD = para7
                d0090.OAJKNST = para10
                d0090.OAJKNED = para11
                d0090.NAIYO = para12
                d0090.BASYO = para13

                If Not String.IsNullOrEmpty(para8) Then
                    d0090.GYOMYMD = para8
                End If

                If Not String.IsNullOrEmpty(para9) Then
                    d0090.GYOMYMDED = para9
                End If

                d0090.PTNFLG = ptnflg
                d0090.PTN1 = ptn1
                d0090.PTN2 = ptn2
                d0090.PTN3 = ptn3
                d0090.PTN4 = ptn4
                d0090.PTN5 = ptn5
                d0090.PTN6 = ptn6
                d0090.PTN7 = ptn7
                d0100.USERID = userid
                d0090.D0100.Add(d0100)
                Return View(d0090)

            End If

            Return View()
        End Function

        ' POST: D0090/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="HINANO,HINAMEMO,FMTKBN,DATAKBN,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,SIYOFLG,SIYOUSERID,SIYODATE,STATUS,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,D0100")> ByVal d0090 As D0090) As ActionResult

            If ModelState.IsValid Then

                Dim decTempHINANO As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "000")
                Dim lstHINANO = (From t In db.D0090 Where t.HINANO > decTempHINANO Select t.HINANO).ToList
                If lstHINANO.Count > 0 Then
                    decTempHINANO = lstHINANO.Max
                End If
                d0090.HINANO = decTempHINANO + 1

                db.D0090.Add(d0090)

                For Each item In d0090.D0100
                    item.HINANO = d0090.HINANO
                Next

                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID", d0090.SIYOUSERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0090.CATCD)
            Return View(d0090)
        End Function

        ' GET: D0090/Edit/5
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

        ' POST: D0090/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="HINANO,HINAMEMO,FMTKBN,DATAKBN,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,SIYOFLG,SIYOUSERID,SIYODATE,STATUS,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0090 As D0090) As ActionResult
            If ModelState.IsValid Then
                db.Entry(d0090).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID", d0090.SIYOUSERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0090.CATCD)
            Return View(d0090)
        End Function

        ' GET: D0090/Delete/5
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

            Return View(d0090)
        End Function

        ' GET: D0090/Delete/5
        Function Delete2(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0090 As D0090 = db.D0090.Find(id)
            If IsNothing(d0090) Then
                Return HttpNotFound()
            End If
            Return View(d0090)
        End Function

        ' POST: D0090/Delete/5
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
