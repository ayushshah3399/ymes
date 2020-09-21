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
	Public Class C0030Controller
		Inherits System.Web.Mvc.Controller

		Private db As New Model1

		Function ReturnLoginPartial() As ActionResult
			ViewData!ID = "Login"
			Return PartialView("_LoginPartial")
		End Function

		' GET: D0010
        Function Index(ByVal id As String, ByVal Searchdt As String, ByVal msgShow As String) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If

            '伝言板表示・非表示
            If msgShow IsNot Nothing Then
                Session("msgShow") = msgShow
            End If

            '業務登録、修正で使用するSession値の初期化
            For intIdx As Integer = Session.Keys.Count - 1 To 0 Step -1
                Dim strKey As String = Session.Keys(intIdx).ToString
                If strKey.Contains("B0020") OrElse strKey.Contains("D0010") Then
                    Session.RemoveAt(intIdx)
                End If
            Next

            ViewData!LoginUsernm = Session("LoginUsernm")
            ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

            Dim loginUserKanri As Boolean = Session("LoginUserKanri")
            Dim loginUserSystem As Boolean = Session("LoginUserSystem")
            Dim loginUserAna As Boolean = Session("LoginUserAna")

            If (loginUserKanri OrElse loginUserSystem) Then
                ViewData("Kanri") = "1"
            Else
                ViewData("Kanri") = "0"
            End If

            If loginUserSystem Then
                ViewData("System") = "1"
            Else
                ViewData("System") = "0"
            End If

            Dim intUserid As Integer = Integer.Parse(loginUserId)
            Dim m0010KOKYU = db.M0010.Find(intUserid)
            ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
            ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) AndAlso Session("LoginUserDeskChief") = 1 Then
				ViewData("LoginUserDeskChief") = 1
			End If

			If String.IsNullOrEmpty(id) Then
                id = loginUserId
            End If

            Dim SearchdtD0010 As String = Searchdt
            If String.IsNullOrEmpty(SearchdtD0010) Then
                SearchdtD0010 = Today.ToString("yyyy/MM/dd")
            End If

            ViewData("searchdt") = SearchdtD0010
            ViewData("userid") = loginUserId
            ViewData("name") = Session("LoginUsernm")

            Dim d0010 = db.D0010.Where(Function(f) f.OYAGYOMFLG = 0).OrderBy(Function(f) f.CATCD).ThenBy(Function(f) f.GYOMYMD).ThenBy(Function(f) f.GYOMYMDED).ThenBy(Function(f) f.OAJKNST).ThenBy(Function(f) f.OAJKNED).ThenBy(Function(f) f.KSKJKNST).ThenBy(Function(f) f.KSKJKNED).Include(Function(d) d.M0020).Include(Function(d) d.M0090)

            'ASI [2019 Dec 17]to display sport category data
            'd0010 = d0010.Where(Function(d) d.PGYOMNO Is Nothing OrElse (d.SPORTFLG = True))
            d0010 = d0010.Where(Function(d1) (d1.SPORTFLG = True AndAlso d1.SPORT_OYAFLG = True) OrElse d1.PGYOMNO Is Nothing)
            'Dim d0010Search = db.D0010.Where(Function(d1) db.D0020.Any(Function(d2) d2.GYOMNO = d1.GYOMNO AndAlso d2.USERID = itemm0010.USERID))


            If Not String.IsNullOrEmpty(SearchdtD0010) Then
				d0010 = d0010.Where(Function(m) m.GYOMYMD <= SearchdtD0010 And SearchdtD0010 <= m.GYOMYMDED)
				'shited this condition with sport data
				'd0010 = d0010.Where(Function(m) m.PGYOMNO Is Nothing)
				'd0010 = d0010.Where(Function(m) m.GYOMYMD >= (SearchdtD0010) Or (m.GYOMYMDED <= (SearchdtD0010)))
			End If

            Dim m0020 = db.M0020.OrderBy(Function(f) f.HYOJJN)
            m0020 = m0020.Where(Function(m) m.HYOJ = True)

            Dim M0060 = db.M0060.OrderBy(Function(f) f.HYOJJN)
            M0060 = M0060.Where(Function(m) m.HYOJ = True)

            Dim M00602 = db.M0060.OrderBy(Function(f) f.HYOJJN)
            M00602 = M00602.Where(Function(m) m.TNTHYOHYOJ = True)

            Dim d0040 = db.D0040.OrderBy(Function(f) f.KYUKCD)
            If Not String.IsNullOrEmpty(Searchdt) Then
                Searchdt = Searchdt.ToString.Replace("/", "")
            Else
                Searchdt = Today.ToString.Replace("/", "")
            End If

            d0040 = d0040.Where(Function(m) m.NENGETU = (Searchdt.Substring(0, 6)) And m.HI = (Searchdt.Substring(6, 2)) And m.M0010.HYOJ = True)


            '業務申請で未処理のものがある場合、申請ありを表示するため
            Dim d0050 = db.D0050.Where(Function(m) m.SHONINFLG = False)
            If d0050.Count > 0 Then
                ViewBag.GYOMFLG = "1"
            End If

            '休暇申請で未処理のものがある場合、申請ありを表示するため
            Dim d0060 = db.D0060.Where(Function(m) m.SHONINFLG = False)
            If d0060.Count > 0 Then
                ViewBag.KYUKFLG = "1"
            End If

            '「ユーザーテーブル」で表示対象となっているものを、「表示順」に従って表示す
            Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True)
            M0010 = M0010.OrderBy(Function(f) f.HYOJJN)

            Dim lstData As New List(Of D0040)
            For Each itemd0040 In d0040
                lstData.Add(itemd0040)
            Next
            For Each itemm0010 In M0010

                Dim d0010Search = db.D0010.Where(Function(d1) db.D0020.Any(Function(d2) d2.GYOMNO = d1.GYOMNO AndAlso d2.USERID = itemm0010.USERID))
                d0010Search = d0010Search.Where(Function(m) m.GYOMYMD <= SearchdtD0010 And SearchdtD0010 <= m.GYOMYMDED)
                For Each itemd0010 In d0010Search
                    Dim m0020Color = db.M0020.Find(itemd0010.CATCD)
                    If m0020Color.SYUCHO = True Then
                        Dim data As New D0040
                        data.USERID = itemm0010.USERID
                        data.KYUKCD = "3"
                        Dim m0060add = db.M0060.Find(3)
                        data.M0060 = m0060add
                        lstData.Add(data)
                    End If
                Next

            Next

            Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            ViewData.Add("UserList", lstUSERID)

            ViewData("frompage") = "C0030"
            ViewBag.CategList = m0020.ToList
            ViewBag.KyukaList = M0060.ToList
            ViewData.Add("ColorList", M00602.ToList())

            lstData = lstData.OrderBy(Function(f) f.USERID).ToList
            ViewData.Add("UserColor", lstData)

            Dim KinmuColor = db.M0060.Find(1)
            ViewData.Add("KinmuColor", KinmuColor.FONTCOLOR)

            ViewBag.HolidayList = d0040.ToList
            ViewBag.USERID = id

            Dim item As New D0080
            item.USERID = id
            item.TOROKUYMD = Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
            item.DATAFLG = "0"
            ViewData.Add("Message", item)

            Dim d0080 = db.D0080.Where(Function(m) m.DATAFLG = "0")
            d0080 = d0080.OrderByDescending(Function(f) f.DNGNNO)
            ViewData.Add("MessageList", d0080.ToList())

            Dim intId As Integer = 0
            If id IsNot Nothing Then
                intId = Integer.Parse(id)
            End If
            Dim strNengetsu As String = SearchdtD0010.Replace("/", "")
            strNengetsu = strNengetsu.Substring(0, 6)
            Dim intNengetsu As Integer = Integer.Parse(strNengetsu)

            Dim d0030 = db.D0030.Where(Function(m) m.NENGETU = intNengetsu)
            ViewData.Add("TenkaiList", d0030.ToList())

			For Each d0010a In d0010
				d0010a.D0020 = d0010a.D0020.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
			Next

			Return View(d0010.ToList())
        End Function

		' GET: D0010/Details/5
		Function Details(ByVal id As Decimal) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0010 As D0010 = db.D0010.Find(id)
			If IsNothing(d0010) Then
				Return HttpNotFound()
			End If
			Return View(d0010)
		End Function

		' GET: D0010/Create
		Function Create() As ActionResult
			ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM")
			ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO")
			Return View()
		End Function

		' POST: D0010/Create
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Create(<Bind(Include:="GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,RNZK,PGYOMNO,IKTFLG,IKTUSERID,IKKATUNO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0010 As D0010) As ActionResult
			If ModelState.IsValid Then
				db.D0010.Add(d0010)
				db.SaveChanges()
				Return RedirectToAction("Index")
			End If
			ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0010.CATCD)
			ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO", d0010.IKKATUNO)
			Return View(d0010)
		End Function

		' GET: D0010/Edit/5
		Function Edit(ByVal id As Decimal) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0010 As D0010 = db.D0010.Find(id)
			If IsNothing(d0010) Then
				Return HttpNotFound()
			End If
			ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0010.CATCD)
			ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO", d0010.IKKATUNO)
			Return View(d0010)
		End Function

		' POST: D0010/Edit/5
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Edit(<Bind(Include:="GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,RNZK,PGYOMNO,IKTFLG,IKTUSERID,IKKATUNO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0010 As D0010) As ActionResult
			If ModelState.IsValid Then
				db.Entry(d0010).State = EntityState.Modified
				db.SaveChanges()
				Return RedirectToAction("Index")
			End If
			ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0010.CATCD)
			ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO", d0010.IKKATUNO)
			Return View(d0010)
		End Function

		' GET: D0010/Delete/5
		Function Delete(ByVal id As Decimal) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0010 As D0010 = db.D0010.Find(id)
			If IsNothing(d0010) Then
				Return HttpNotFound()
			End If
			Return View(d0010)
		End Function

        '' POST: D0010/Delete/5
        '<HttpPost()>
        '<ActionName("Delete")>
        '<ValidateAntiForgeryToken()>
        'Function DeleteConfirmed(ByVal id As Decimal) As ActionResult
        '	Dim d0010 As D0010 = db.D0010.Find(id)
        '	db.D0010.Remove(d0010)
        '	db.SaveChanges()
        '	Return RedirectToAction("Index")
        'End Function

        ' POST: D0080/Delete/5
        <HttpPost(), ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal DNGNNO As Decimal) As ActionResult
            Dim d0080 As D0080 = db.D0080.Find(DNGNNO)
            db.D0080.Remove(d0080)
            db.SaveChanges()
            'Return PartialView("ShowMessage", db.D0080.ToList())
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
