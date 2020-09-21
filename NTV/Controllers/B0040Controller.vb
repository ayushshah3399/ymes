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
    Public Class B0040Controller
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

        ' GET: B0040
        Function Index(ByVal showdate As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

            Dim d0040 = db.D0040.Include(Function(d) d.D0030).Include(Function(d) d.M0060)
            ViewData("frompage") = "B0040"

            Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            ViewData.Add("List", lstUSERID)

            If String.IsNullOrEmpty(showdate) Then
                showdate = Today.ToString("yyyy/MM")
            End If

            ViewData("searchdt") = showdate
			Dim m0060 = db.M0060.Where(Function(a) a.KYUJITUHYOJ = True).OrderBy(Function(a) a.HYOJJN)


			ViewData.Add("ColorList", m0060.ToList())
            ViewBag.UserList = db.M0010.ToList
            ViewBag.KyujitsuList = db.D0040.ToList

            ViewBag.USERID = New SelectList(db.M0010.ToList, "USERID", "USERNM")

            Dim intUserid As Integer = Integer.Parse(loginUserId)
            Dim m0010KOKYU = db.M0010.Find(intUserid)
            ViewData("KOKYUTENKAI") = m0010KOKYU.KOKYUTENKAI
            ViewData("KOKYUTENKAIALL") = m0010KOKYU.KOKYUTENKAIALL

            If Not String.IsNullOrEmpty(showdate) Then
                showdate = showdate.Replace("/", "")
                d0040 = d0040.Where(Function(m) m.NENGETU = showdate)
            Else
                showdate = Today.ToString("yyyyMM")
                d0040 = d0040.Where(Function(m) m.NENGETU = showdate)
            End If

            '公休展開
            Dim d0030 = db.D0030.Where(Function(m) m.NENGETU = showdate)
            ViewData.Add("UserColor", d0030.ToList())

            Dim sqlpara2 As New SqlParameter("av_nengetsu", SqlDbType.VarChar, 6)
            sqlpara2.Value = showdate


            Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_b0040_getdata @av_nengetsu",
                                                     sqlpara2)

            'Dim wd0060 = db.WD0060
            'ViewBag.Timelist = wd0060.ToList

            Dim wd0050 = db.WD0050.Where(Function(m) m.NENGETU = showdate).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList

            For Each item In wd0050
                Dim decUserID As Decimal = item.USERID
                Dim decNengetsu As Decimal = item.NENGETU
                Dim wd0060Select = db.WD0060.Where(Function(m) m.USERID = decUserID And m.NENGETU = decNengetsu).OrderBy(Function(m) m.HI).ThenBy(Function(m) m.KYUKCD).ToList
                item.WD0060.Clear()
                For Each itemd0060 In wd0060Select
                    Dim newWD0060 As New WD0060
                    newWD0060.USERID = itemd0060.USERID
                    newWD0060.NENGETU = itemd0060.NENGETU
                    newWD0060.HI = itemd0060.HI
                    newWD0060.KYUKCD = itemd0060.KYUKCD
                    newWD0060.JKNST = itemd0060.JKNST
                    newWD0060.JKNED = itemd0060.JKNED
                    newWD0060.M0060 = itemd0060.M0060
                    item.WD0060.Add(newWD0060)
                Next

            Next

            Return View(wd0050)
        End Function

        ' GET: B0040/Details/5
        Function Details(ByVal id As Short?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0040 As D0040 = db.D0040.Find(id)
            If IsNothing(d0040) Then
                Return HttpNotFound()
            End If
            Return View(d0040)
        End Function

        ' GET: B0040/Create
        Function Create() As ActionResult
            ViewBag.USERID = New SelectList(db.D0030, "USERID", "INSTID")
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM")
            Return View()
        End Function

        ' POST: B0040/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="USERID,NENGETU,HI,JKNST,JKNED,JTJKNST,JTJKNED,KYUKCD,GYOMMEMO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0040 As D0040) As ActionResult
            If ModelState.IsValid Then
                db.D0040.Add(d0040)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.D0030, "USERID", "INSTID", d0040.USERID)
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0040.KYUKCD)
            Return View(d0040)
        End Function

        ' GET: B0040/Edit/5
        Function Edit(ByVal id As Short?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0040 As D0040 = db.D0040.Find(id)
            If IsNothing(d0040) Then
                Return HttpNotFound()
            End If
            ViewBag.USERID = New SelectList(db.D0030, "USERID", "INSTID", d0040.USERID)
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0040.KYUKCD)
            Return View(d0040)
        End Function

        ' POST: B0040/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="USERID,NENGETU,HI,JKNST,JKNED,JTJKNST,JTJKNED,KYUKCD,GYOMMEMO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0040 As D0040) As ActionResult
            If ModelState.IsValid Then
                db.Entry(d0040).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.D0030, "USERID", "INSTID", d0040.USERID)
            ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0040.KYUKCD)
            Return View(d0040)
        End Function

        ' GET: B0040/Delete/5
        Function Delete(ByVal id As Short?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim d0040 As D0040 = db.D0040.Find(id)
            If IsNothing(d0040) Then
                Return HttpNotFound()
            End If
            Return View(d0040)
        End Function

        ' POST: B0040/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Short) As ActionResult
            Dim d0040 As D0040 = db.D0040.Find(id)
            db.D0040.Remove(d0040)
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
