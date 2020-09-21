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
    Public Class D0080Controller
        Inherits System.Web.Mvc.Controller

        Private db As New Model1

        ' GET: D0080
        Function Index() As ActionResult
            Dim d0080 = db.D0080.Include(Function(d) d.M0010)
            Return View(d0080.ToList())
        End Function

        ' GET: D0080/Details/5
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0080 As D0080 = db.D0080.Find(id)
            If IsNothing(d0080) Then
                Return HttpNotFound()
            End If
            Return View(d0080)
        End Function

        ' GET: D0080/Create
        Function Create() As ActionResult
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID")
            Dim d0080List = db.D0080.OrderByDescending(Function(f) f.DNGNNO)
            ViewData.Add("MessageList", d0080List.ToList())
            Return View()
        End Function

        ' POST: D0080/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Create(<Bind(Include:="DNGNNO,USERID,MESSAGE,DATAFLG,TOROKUYMD,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0080 As D0080, ByVal formnm As String) As ActionResult
			If ModelState.IsValid Then
				Dim decTempIKKATUNO As Decimal = Integer.Parse(DateTime.Today.ToString("yyyyMM") & "000")
				Dim lstIKKATUNO = (From t In db.D0080 Where t.DNGNNO > decTempIKKATUNO Select t.DNGNNO).ToList
				If lstIKKATUNO.Count > 0 Then
					decTempIKKATUNO = lstIKKATUNO.Max
				End If
				d0080.DNGNNO = decTempIKKATUNO + 1

				db.D0080.Add(d0080)
				db.SaveChanges()
				Return RedirectToAction("Index", formnm)
			End If
			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0080.USERID)
			Return View(d0080)
		End Function

		' POST: D0080/Create
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
        <ValidateAntiForgeryToken()>
        Function CreateB0030(<Bind(Include:="DNGNNO,USERID,MESSAGE,DATAFLG,TOROKUYMD,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0080 As D0080) As ActionResult
            If ModelState.IsValid Then
                Dim decTempIKKATUNO As Decimal = Integer.Parse(DateTime.Today.ToString("yyyyMM") & "000")
                Dim lstIKKATUNO = (From t In db.D0080 Where t.DNGNNO > decTempIKKATUNO Select t.DNGNNO).ToList
                If lstIKKATUNO.Count > 0 Then
                    decTempIKKATUNO = lstIKKATUNO.Max
                End If
                d0080.DNGNNO = decTempIKKATUNO + 1

                db.D0080.Add(d0080)
                db.SaveChanges()
                Return RedirectToAction("Index", "B0030")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0080.USERID)
            Return View(d0080)
        End Function

        ' GET: D0080/Edit/5
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0080 As D0080 = db.D0080.Find(id)
            If IsNothing(d0080) Then
                Return HttpNotFound()
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0080.USERID)
            Return View(d0080)
        End Function

        ' POST: D0080/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="DNGNNO,USERID,MESSAGE,DATAFLG,TOROKUYMD,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0080 As D0080) As ActionResult
            If ModelState.IsValid Then
                db.Entry(d0080).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0080.USERID)
            Return View(d0080)
        End Function

        ' GET: D0080/Delete/5

        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0080 As D0080 = db.D0080.Find(id)
            If IsNothing(d0080) Then
                Return HttpNotFound()
            End If
            Return View(d0080)
        End Function

        ' POST: D0080/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Decimal) As ActionResult
            Dim d0080 As D0080 = db.D0080.Find(id)
            db.D0080.Remove(d0080)
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
