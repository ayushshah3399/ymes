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
    Public Class D0050Controller
        Inherits System.Web.Mvc.Controller

        Private db As New Model1

        ' GET: D0050
        Function Index() As ActionResult
            Dim d0050 = db.D0050.Include(Function(d) d.M0010).Include(Function(d) d.M0020)
            Return View(d0050.ToList())
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
        Function Create(<Bind(Include:="GYOMSNSNO,USERID,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,NAIYO,BASYO,GYOMMEMO,BANGUMITANTO,BANGUMIRENRK,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0050 As D0050) As ActionResult
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
        Function Edit(<Bind(Include:="GYOMSNSNO,USERID,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,NAIYO,BASYO,GYOMMEMO,BANGUMITANTO,BANGUMIRENRK,SHONINFLG,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0050 As D0050) As ActionResult
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
            Return View(d0050)
        End Function

        ' POST: D0050/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Decimal) As ActionResult
            Dim d0050 As D0050 = db.D0050.Find(id)
            db.D0050.Remove(d0050)
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
