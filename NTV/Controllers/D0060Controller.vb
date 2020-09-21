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
    Public Class D0060Controller
        Inherits System.Web.Mvc.Controller

        Private db As New Model1

        ' GET: D0060
        Function Index(userid As String, name As String) As ActionResult
            ViewData("name") = name

            Dim d0060 = db.D0060.Include(Function(d) d.M0010).Include(Function(d) d.M0060)
            d0060 = d0060.Where(Function(m) m.USERID = userid)

            Return View(d0060.ToList())
        End Function

        ' GET: D0060/Details/5
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0060 As D0060 = db.D0060.Find(id)
            If IsNothing(d0060) Then
                Return HttpNotFound()
            End If
            Return View(d0060)
        End Function

        ' GET: D0060/Create
        Function Create() As ActionResult
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID")
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM")
            Return View()
        End Function

        ' POST: D0060/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0060 As D0060) As ActionResult
            If ModelState.IsValid Then
                db.D0060.Add(d0060)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
            Return View(d0060)
        End Function

        ' GET: D0060/Edit/5
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0060 As D0060 = db.D0060.Find(id)
            If IsNothing(d0060) Then
                Return HttpNotFound()
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
            Return View(d0060)
        End Function

        ' POST: D0060/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0060 As D0060) As ActionResult
            If ModelState.IsValid Then
                db.Entry(d0060).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
            Return View(d0060)
        End Function

        ' GET: D0060/Delete/5
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0060 As D0060 = db.D0060.Find(id)
            If IsNothing(d0060) Then
                Return HttpNotFound()
            End If
            Return View(d0060)
        End Function

        ' POST: D0060/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Decimal) As ActionResult
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
