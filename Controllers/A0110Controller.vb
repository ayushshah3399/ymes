Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports NTV_SHIFT
Imports System.Data.Entity.Validation
Imports System.Data.SqlClient

Namespace Controllers
	Public Class A0110Controller
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

		' GET: M0010
		Function Index() As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")


			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) Then
				If Session("LoginUserDeskChief") = 0 Then
					Return View("ErrorAccesslvl")
				End If
			End If

			Dim loginUserSystem As Boolean = Session("LoginUserSystem")
			Dim lstM0010 As List(Of M0010) = Nothing

			ViewBag.LoginUserSystem = Session("LoginUserSystem")
			ViewBag.LoginUserKanri = Session("LoginUserKanri")
			ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

			'一括業務メニューを表示可能な条件追加
			Dim intUserid As Integer = Integer.Parse(loginUserId)
			Dim m0010KOKYU = db.M0010.Find(intUserid)
			ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
			ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL

			'システム管理者の場合は全員表示
			If loginUserSystem Then
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			Else
				'管理職、デスクでログインの場合、システム担当者をユーザー設定画面に表示しない
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True And m.M0050.SYSTEM = False).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			End If

			If lstM0010 IsNot Nothing Then
				For intIdx As Integer = 0 To lstM0010.Count - 1
					If lstM0010(intIdx).KOKYU1 Then
						lstM0010(intIdx).KYU1 = "公"
					End If
					If lstM0010(intIdx).KOKYU2 Then
						lstM0010(intIdx).KYU2 = "公"
					End If
					If lstM0010(intIdx).KOKYU3 Then
						lstM0010(intIdx).KYU3 = "公"
					End If
					If lstM0010(intIdx).KOKYU4 Then
						lstM0010(intIdx).KYU4 = "公"
					End If
					If lstM0010(intIdx).KOKYU5 Then
						lstM0010(intIdx).KYU5 = "公"
					End If
					If lstM0010(intIdx).KOKYU6 Then
						lstM0010(intIdx).KYU6 = "公"
					End If
					If lstM0010(intIdx).KOKYU7 Then
						lstM0010(intIdx).KYU7 = "公"
					End If
					If lstM0010(intIdx).HOKYU1 Then
						lstM0010(intIdx).KYU1 = "法"
					End If
					If lstM0010(intIdx).HOKYU2 Then
						lstM0010(intIdx).KYU2 = "法"
					End If
					If lstM0010(intIdx).HOKYU3 Then
						lstM0010(intIdx).KYU3 = "法"
					End If
					If lstM0010(intIdx).HOKYU4 Then
						lstM0010(intIdx).KYU4 = "法"
					End If
					If lstM0010(intIdx).HOKYU5 Then
						lstM0010(intIdx).KYU5 = "法"
					End If
					If lstM0010(intIdx).HOKYU6 Then
						lstM0010(intIdx).KYU6 = "法"
					End If
					If lstM0010(intIdx).HOKYU7 Then
						lstM0010(intIdx).KYU7 = "法"
					End If

					'ASI[26 Nov 2019]: [START] Fetch SportCategory for Each user and set in property of M0010 object ana pass that list to screen
					Dim strCommaSperateStrings As String() = GetCommaSeparatedStrings(lstM0010(intIdx).USERID)
					lstM0010(intIdx).SportCatNmComaSeperatedString = strCommaSperateStrings(1) 'allSportCatNmString
					'ASI[26 Nov 2019]: [End]
				Next
			End If
			Return View(lstM0010)
		End Function

		Function EditHYOJJN() As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) Then
				If Session("LoginUserDeskChief") = 0 Then
					Return View("ErrorAccesslvl")
				End If
			End If

			Dim loginUserSystem As Boolean = Session("LoginUserSystem")
			Dim lstM0010 As List(Of M0010) = Nothing

			'システム管理者の場合は全員表示
			If loginUserSystem Then
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			Else
				'管理職、デスクでログインの場合、システム担当者をユーザー設定画面に表示しない
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True And m.M0050.SYSTEM = False).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			End If

			Return View(lstM0010)
		End Function

		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function EditHYOJJN(<Bind(Include:="USERID,LOGINID,USERPWD,USERPWDCONFRIM,USERNM,USERSEX,KOKYU1,KOKYU2,KOKYU3,KOKYU4,KOKYU5,KOKYU6,KOKYU7,HOKYU1,HOKYU2,HOKYU3,HOKYU4,HOKYU5,HOKYU6,HOKYU7,HYOJJN,HYOJ,STATUS,ACCESSLVLCD,MAILLADDESS,KEITAIADDESS,KOKYUTENKAI,KOKYUTENKAIALL,KARIANA,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")>
							ByVal list As IList(Of NTV_SHIFT.M0010)) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")


			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim key As String = ""
			Dim bolErr As Boolean = False
			Dim strChofukuErr As String = "表示順が重複しています。"

			Dim lstM0010 = db.M0010.Where(Function(m) m.STATUS = True).ToList

			'システム管理者か、管理職・デスクかにより、表示されるユーザーで、全てユーザーを検索し、表示順の新しい値を設定してからチェックする
			For Each item In list
				For Each m0010 In lstM0010
					If m0010.USERID = item.USERID Then
						m0010.HYOJJN = item.HYOJJN
						Exit For
					End If
				Next
			Next

			For i As Integer = 0 To list.Count - 1
				bolErr = False
				For Each item In lstM0010
					If list(i).USERID <> item.USERID AndAlso list(i).USERSEX = item.USERSEX AndAlso list(i).HYOJJN = item.HYOJJN Then
						bolErr = True
						Exit For
					End If
				Next

				key = String.Format("list[{0}].", i) & "HYOJJN"

				If bolErr Then
					ModelState.AddModelError(key, strChofukuErr)
				Else
					'ModelのCustomValidationでエラーになった重複チェックをクリアする
					For Each merror In ModelState(key).Errors
						If merror.ErrorMessage = strChofukuErr Then
							ModelState(key).Errors.Remove(merror)
							Exit For
						End If
					Next
				End If
			Next

			If ModelState.IsValid Then
				Dim m0010 As M0010 = Nothing
				For Each item In list
					m0010 = db.M0010.Find(item.USERID)
					If IsNothing(m0010) Then
						Return HttpNotFound()
					End If
					If m0010.HYOJJN <> item.HYOJJN Then
						m0010.HYOJJN = item.HYOJJN
						db.Entry(m0010).State = EntityState.Modified
					End If
				Next

				db.Configuration.ValidateOnSaveEnabled = False
				db.SaveChanges()
				db.Configuration.ValidateOnSaveEnabled = True

				Return RedirectToAction("Index")
			End If
			Return View(list)
		End Function

		' GET: M0010/Details/5
		Function Details(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0010 As M0010 = db.M0010.Find(id)
			If IsNothing(m0010) Then
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

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) Then
				If Session("LoginUserDeskChief") = 0 Then
					Return View("ErrorAccesslvl")
				End If
			End If

			'ASI[26 Nov 2019]: [Start] fetch SportCategory for user and make string list of it by comma seperation
			Dim strCommaSperateStrings As String() = GetCommaSeparatedStrings(id)
			m0010.SportCatNmComaSeperatedString = strCommaSperateStrings(1) 'allSportCatNmString
			'ASI[26 Nov 2019]: [End]

			Return View(m0010)
		End Function

		' GET: M0010/Create
		Function Create() As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")


			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) Then
				If Session("LoginUserDeskChief") = 0 Then
					Return View("ErrorAccesslvl")
				End If
			End If

			ViewBag.ACCESSLVLCD = New SelectList(db.M0050, "ACCESSLVLCD", "HYOJNM")

			Dim loginUserSystem As Boolean = Session("LoginUserSystem")
			Dim lstM0010 As List(Of M0010) = Nothing

			'システム管理者の場合は全員表示
			If loginUserSystem Then
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			Else
				'管理職、デスクでログインの場合、システム担当者をユーザー設定画面に表示しない
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True And m.M0050.SYSTEM = False).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			End If

			ViewData.Add("List", lstM0010)
			ViewData("frompage") = "A0110"

			Return View()
		End Function

		' POST: M0010/Create
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Create(<Bind(Include:="USERID,LOGINID,USERPWD,USERPWDCONFRIM,USERNM,USERSEX,KOKYU1,KOKYU2,KOKYU3,KOKYU4,KOKYU5,KOKYU6,KOKYU7,HOKYU1,HOKYU2,HOKYU3,HOKYU4,HOKYU5,HOKYU6,HOKYU7,HYOJJN,HYOJ,STATUS,ACCESSLVLCD,MAILLADDESS,KEITAIADDESS,KOKYUTENKAI,KOKYUTENKAIALL,KARIANA,SportCatCdComaSeperatedString,SportCatNmComaSeperatedString,ChiefFlgsComaSeperatedString,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal m0010 As M0010) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If ModelState.IsValid Then
				'Dim maxid = (From c In db.M0010.ToList Select c.USERID).Max()
				'm0010.USERID = maxid + 1
				m0010.STATUS = True
				db.M0010.Add(m0010)

				'ASI[25 Nov 2019] :[START] To make insertion of sportCatCd selected by user to M0160 tbl, handeled Transaction mgt also.
				If m0010.SportCatCdComaSeperatedString IsNot Nothing Then
					Dim allSportCatCds = m0010.SportCatCdComaSeperatedString.Split(",")
					Dim allChiefFlgs = m0010.ChiefFlgsComaSeperatedString.Split(",")
					Dim index = 0
					For Each sportCatCd In allSportCatCds
						Dim bolChiefFlg As Boolean = If(allChiefFlgs(index) = "1", True, False)
						Dim m0160InsertObj As M0160 = New M0160()
						m0160InsertObj.USERID = (From c In db.M0010.ToList Select c.USERID).Max() + 1
						m0160InsertObj.SPORTCATCD = sportCatCd
						m0160InsertObj.CHIEFFLG = bolChiefFlg
						db.M0160.Add(m0160InsertObj)
						index = index + 1
					Next
				End If

				Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
				sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try
						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)
						db.Configuration.ValidateOnSaveEnabled = False
						db.SaveChanges()
						db.Configuration.ValidateOnSaveEnabled = True
						tran.Commit()
					Catch ex As Exception
						Throw ex
						tran.Rollback()
					End Try
				End Using
				'ASI[25 Nov 2019] :[END]
				Return RedirectToAction("Index")

			End If
			ViewBag.ACCESSLVLCD = New SelectList(db.M0050, "ACCESSLVLCD", "HYOJNM", m0010.ACCESSLVLCD)

			Dim loginUserSystem As Boolean = Session("LoginUserSystem")
			Dim lstM0010 As List(Of M0010) = Nothing

			'システム管理者の場合は全員表示
			If loginUserSystem Then
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			Else
				'管理職、デスクでログインの場合、システム担当者をユーザー設定画面に表示しない
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True And m.M0050.SYSTEM = False).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			End If

			ViewData.Add("List", lstM0010)
			ViewData("frompage") = "A0110"

			Return View(m0010)
		End Function

		' GET: M0010/Edit/5
		Function Edit(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0010 As M0010 = db.M0010.Find(id)
			If IsNothing(m0010) Then
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

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) Then
				If Session("LoginUserDeskChief") = 0 Then
					Return View("ErrorAccesslvl")
				End If
			End If

			ViewBag.ACCESSLVLCD = New SelectList(db.M0050, "ACCESSLVLCD", "HYOJNM", m0010.ACCESSLVLCD)

			Dim loginUserSystem As Boolean = Session("LoginUserSystem")
			Dim lstM0010 As List(Of M0010) = Nothing

			'システム管理者の場合は全員表示
			If loginUserSystem Then
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			Else
				'管理職、デスクでログインの場合、システム担当者をユーザー設定画面に表示しない
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True And m.M0050.SYSTEM = False).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			End If

			ViewData.Add("List", lstM0010)
			ViewData("frompage") = "A0110"
			m0010.USERPWDCONFRIM = m0010.USERPWD

			'ASI[25 Nov 2019]: [Start] fetch SportCategory for user and make string list of it by comma seperation
			Dim strCommaSperateStrings As String() = GetCommaSeparatedStrings(id)
			m0010.SportCatCdComaSeperatedString = strCommaSperateStrings(0) 'allSportCatCdString
			m0010.SportCatNmComaSeperatedString = strCommaSperateStrings(1) 'allSportCatNmString
			m0010.ChiefFlgsComaSeperatedString = strCommaSperateStrings(2) 'allChiefFlgString
			'ASI[25 Nov 2019]: [End]

			Return View(m0010)
		End Function

		' POST: M0010/Edit/5
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Edit(<Bind(Include:="USERID,LOGINID,USERPWD,USERPWDCONFRIM,USERNM,USERSEX,KOKYU1,KOKYU2,KOKYU3,KOKYU4,KOKYU5,KOKYU6,KOKYU7,HOKYU1,HOKYU2,HOKYU3,HOKYU4,HOKYU5,HOKYU6,HOKYU7,HYOJJN,HYOJ,STATUS,ACCESSLVLCD,MAILLADDESS,KEITAIADDESS,KOKYUTENKAI,KOKYUTENKAIALL,KARIANA,SportCatCdComaSeperatedString,SportCatNmComaSeperatedString,ChiefFlgsComaSeperatedString,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal m0010 As M0010) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If ModelState.IsValid Then
				m0010.STATUS = True     '有効
				db.Entry(m0010).State = EntityState.Modified

				'ASI[26 Nov 2019]:[START] to Update sportcategory in M0160 tbl, first perform Delete and then Insert in M0160 tbl
				'Code for Remove
				Dim lstM0160 As List(Of M0160) = Nothing
				lstM0160 = db.M0160.Where(Function(m) m.USERID = m0010.USERID).ToList()

				For Each m0160 In lstM0160
					db.M0160.Remove(m0160)
				Next
				'Code for insert
				If m0010.SportCatCdComaSeperatedString IsNot Nothing Then
					Dim allSportCatCds = m0010.SportCatCdComaSeperatedString.Split(",")
					Dim allChiefFlgs = m0010.ChiefFlgsComaSeperatedString.Split(",")
					Dim index = 0
					For Each sportCatCd In allSportCatCds
						Dim bolChiefFlg As Boolean = If(allChiefFlgs(index) = "1", True, False)
						Dim m0160InsertObj As M0160 = New M0160()
						m0160InsertObj.USERID = m0010.USERID
						m0160InsertObj.SPORTCATCD = sportCatCd
						m0160InsertObj.CHIEFFLG = bolChiefFlg
						db.M0160.Add(m0160InsertObj)
						index = index + 1
					Next
				End If

				Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
				sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try
						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)
						db.Configuration.ValidateOnSaveEnabled = False
						db.SaveChanges()
						db.Configuration.ValidateOnSaveEnabled = True
						tran.Commit()
					Catch ex As Exception
						Throw ex
						tran.Rollback()
					End Try
				End Using
				'ASI[26 Nov 2019]: [END]
				Return RedirectToAction("Index")
			End If

			ViewBag.ACCESSLVLCD = New SelectList(db.M0050, "ACCESSLVLCD", "HYOJNM", m0010.ACCESSLVLCD)

			Dim loginUserSystem As Boolean = Session("LoginUserSystem")
			Dim lstM0010 As List(Of M0010) = Nothing

			'システム管理者の場合は全員表示
			If loginUserSystem Then
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			Else
				'管理職、デスクでログインの場合、システム担当者をユーザー設定画面に表示しない
				lstM0010 = db.M0010.Where(Function(m) m.STATUS = True And m.M0050.SYSTEM = False).OrderBy(Function(f) f.USERSEX).ThenBy(Function(f) f.HYOJJN).Include(Function(m) m.M0050).ToList()
			End If

			ViewData.Add("List", lstM0010)
			ViewData("frompage") = "A0110"

			Return View(m0010)
		End Function

		' GET: M0010/Delete/5
		Function Delete(ByVal id As Short?) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0010 As M0010 = db.M0010.Find(id)
			If IsNothing(m0010) Then
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

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) Then
				If Session("LoginUserDeskChief") = 0 Then
					Return View("ErrorAccesslvl")
				End If
			End If

			'ASI[26 Nov 2019]
			Dim strCommaSperateStrings As String() = GetCommaSeparatedStrings(id)
			m0010.SportCatNmComaSeperatedString = strCommaSperateStrings(1) 'allSportCatNmString
			Return View(m0010)
		End Function

		' POST: M0010/Delete/5
		<HttpPost()>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken()>
		Function DeleteConfirmed(<Bind(Include:="USERID,CONFIRMMSG")> ByVal m0010 As M0010) As ActionResult

			Dim item As M0010 = db.M0010.Find(m0010.USERID)
			If IsNothing(item) Then
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

			If m0010.CONFIRMMSG Is Nothing OrElse m0010.CONFIRMMSG = False Then
				Dim d0010 = db.D0010.Where(Function(d) d.GYOMYMD > Today).Where(Function(d1) db.D0020.Any(Function(d2) d2.GYOMNO = d1.GYOMNO AndAlso d2.USERID = m0010.USERID)).ToList
				If d0010.Count > 0 Then
					TempData("warning") = "未来日に業務があります。削除してもよろしいですか？"
					Return View(item)
				End If
			End If

			item.USERPWDCONFRIM = item.USERPWD      '必須エラーを防ぐため
			item.HYOJ = False                       '非表示
			item.STATUS = False                     '無効

			'ASI[26 Nov 2019]:[START] Code for Remove from M0160 tbl
			Dim lstM0160 As List(Of M0160) = Nothing
			lstM0160 = db.M0160.Where(Function(m) m.USERID = m0010.USERID).ToList()
			For Each m0160 In lstM0160
				db.M0160.Remove(m0160)
			Next

			Using tran As DbContextTransaction = db.Database.BeginTransaction
				Try
					db.Configuration.ValidateOnSaveEnabled = False
					db.SaveChanges()
					db.Configuration.ValidateOnSaveEnabled = True
					tran.Commit()
				Catch ex As Exception
					Throw ex
					tran.Rollback()
				End Try
			End Using
			'ASI[26 Nov 2019]: [END]

			'db.M0010.Remove(m0010)

			'Try
			'	db.SaveChanges()

			'Catch ex As DbEntityValidationException
			'	For Each e In ex.EntityValidationErrors
			'		For Each i In e.ValidationErrors
			'			Dim str As String = i.ErrorMessage
			'		Next
			'	Next
			'End Try

			Return RedirectToAction("Index")
		End Function


		Function ChangePassword(ByVal id As String) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim m0010 As M0010 = db.M0010.Find(Integer.Parse(id))
			If IsNothing(m0010) Then
				Return HttpNotFound()
			End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			m0010.USERPWD = ""

			Return View(m0010)
		End Function

		<HttpPost>
		Function ChangePassword(<Bind(Include:="USERID,LOGINID,USERPWD,USERPWDCONFRIM,USERNM,USERSEX,KOKYU1,KOKYU2,KOKYU3,KOKYU4,KOKYU5,KOKYU6,KOKYU7,HOKYU1,HOKYU2,HOKYU3,HOKYU4,HOKYU5,HOKYU6,HOKYU7,HYOJJN,HYOJ,STATUS,ACCESSLVLCD,MAILLADDESS,KEITAIADDESS,KOKYUTENKAI,KOKYUTENKAIALL,KARIANA")> ByVal m0010 As M0010) As ActionResult

			Dim item As M0010 = db.M0010.Find(Integer.Parse(m0010.USERID))
			If IsNothing(m0010) Then
				Return HttpNotFound()
			End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If ModelState.IsValid Then

				'If m0010.USERPWDOLD <> item.USERPWD Then
				'	ModelState.AddModelError("USERPWDOLD", "旧パスワードが正しくありません。")
				'	Return View(m0010)
				'End If

				item.USERPWD = m0010.USERPWD

				db.Configuration.ValidateOnSaveEnabled = False
				db.SaveChanges()
				db.Configuration.ValidateOnSaveEnabled = True

				Return RedirectToAction("Index", "C0030")
			End If

			'm0010.USERPWD = ""
			'm0010.USERPWDCONFRIM = ""

			Return View(m0010)
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		'ASI[26 Nov 2019] : Function which return all sportCategories as a comma seperated string for perticular user 
		'Function GetAllSportCatNmString(ByVal id As Short?) As String
		'	Dim m0160List = (From m1 In db.M0130
		'					 Join m2 In db.M0160 On m1.SPORTCATCD Equals m2.SPORTCATCD
		'					 Where m2.USERID = id
		'					 Select m1).ToList()
		'	Dim allSportCatNmString As String = Nothing

		'	If m0160List.Count > 0 Then
		'		For Each m0160 In m0160List
		'			If allSportCatNmString <> "" Then
		'				allSportCatNmString = allSportCatNmString + "," + m0160.SPORTCATNM
		'			Else
		'				allSportCatNmString = m0160.SPORTCATNM
		'			End If
		'		Next
		'	End If
		'	Return allSportCatNmString
		'End Function

		'ASI[22 Nov 2019] : Added Action for load SPORTCATEGORY DIALOGBOX
		<OutputCache(Duration:=0)>
		Function SearchSportCategory() As ActionResult
			If Request.IsAjaxRequest() Then
				Dim m0130 = db.M0130.OrderBy(Function(m) m.HYOJJN).ToList
				Return PartialView("_SportCategoryList", m0130)
			End If

			Return New EmptyResult
		End Function

		'Get comma seprated string for the sportcatcd,name and chiefflgs
		Private Function GetCommaSeparatedStrings(ByVal id As Short?) As String()
			Dim m0160List = (From m1 In db.M0130
							 Join m2 In db.M0160 On m1.SPORTCATCD Equals m2.SPORTCATCD
							 Where m2.USERID = id
							 Order By m1.HYOJJN
							 Select m2).ToList()
			Dim allSportCatCdString As String = Nothing
			Dim allSportCatNmString As String = Nothing
			Dim allChiefFlgString As String = Nothing

			If m0160List.Count > 0 Then
				For Each m0160 In m0160List

					'Get Sportcatname
					Dim sportCatName As String = (From m1 In db.M0130 Where m1.SPORTCATCD = m0160.SPORTCATCD Select m1.SPORTCATNM).FirstOrDefault()

					If allSportCatCdString <> "" Then
						Dim strChiefFlg = If(m0160.CHIEFFLG = True, "1", "0")
						allSportCatCdString = allSportCatCdString + "," + m0160.SPORTCATCD.ToString()
						allChiefFlgString = allChiefFlgString + "," + strChiefFlg
						If strChiefFlg = "1" Then
							allSportCatNmString = allSportCatNmString + ", " + sportCatName & "(チーフ)"
						Else
							allSportCatNmString = allSportCatNmString + ", " + sportCatName
						End If
					Else
						Dim strChiefFlg = If(m0160.CHIEFFLG = True, "1", "0")
						allSportCatCdString = m0160.SPORTCATCD.ToString()
						allChiefFlgString = strChiefFlg
						If strChiefFlg = "1" Then
							allSportCatNmString = sportCatName & "(チーフ)"
						Else
							allSportCatNmString = sportCatName
						End If
					End If
				Next
			End If

			Dim separatedString As String() = {allSportCatCdString, allSportCatNmString, allChiefFlgString}
			Return separatedString

		End Function

	End Class
End Namespace
