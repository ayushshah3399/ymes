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
Imports System.Net.Mail
Imports System.IO

Namespace Controllers

	Public Class B0050Controller
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

		' GET: B0050
		Function Index(name As String, ByVal userid As String, ByVal showdate As String, ByVal formname As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim loginUserNM As String = Session("LoginUsernm")

			ViewData!LoginUsernm = Session("LoginUsernm")
			ViewData("FormName") = formname

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.AbsoluteUri      '休日設定画面から来た時アナ名が文字化けするので、Encodeされている元のUrlを取得
				If strUrlReferrer.Contains("/B0040") OrElse strUrlReferrer.Contains("/C0030") OrElse strUrlReferrer.Contains("/C0050") OrElse strUrlReferrer.Contains("/A0200") Then
					Session("UrlReferrer") = strUrlReferrer
				End If
			End If

			If String.IsNullOrEmpty(showdate) Then
				showdate = Today.ToString("yyyy/MM")
			End If

			If String.IsNullOrEmpty(userid) Then
				userid = loginUserId
				name = loginUserNM
			End If

			ViewData("id") = userid
			ViewData("searchdt") = showdate

			Dim intDays As Integer = 0

			Dim d0040 = db.D0040.Include(Function(d) d.D0030).Include(Function(d) d.M0060)

			'休暇申請
			Dim strToday As String = DateTime.Today.ToString("yyyy/MM")
			Dim strSearchdate As String = strToday & "/01"

			Dim strLastMonthDate As String = DateTime.Now.AddMonths(-1).ToString("yyyy/MM")
			Dim strLstSearchDate As String = showdate & "/01"
			Dim lstDate As Date = Date.Parse(strLastMonthDate)
			Dim DaysInMonth As Integer = Date.DaysInMonth(lstDate.Year, lstDate.Month)
			Dim LastDayInMonthDate As Date = New Date(lstDate.Year, lstDate.Month, DaysInMonth)

			Dim d0060 = db.D0060.Include(Function(d) d.M0010).Include(Function(d) d.M0060)
			''「年月」が当月以降のときは、表示月以降の休暇申請を表示する。
			'If showdate >= strToday Then
			'    d0060 = d0060.Where(Function(m) m.KKNST >= strSearchdate)
			'End If

			'If showdate <= strLastMonthDate Then
			'    d0060 = d0060.Where(Function(m) m.KKNST >= strLstSearchDate And m.KKNST <= LastDayInMonthDate.ToString)
			'End If

			'日付関係なく未承認のもの全部見せる
			d0060 = d0060.Where(Function(m) m.USERID = userid And m.SHONINFLG = "0").OrderBy(Function(f) f.KKNST).ThenBy(Function(f) f.JKNST)

			'ASI [07 Nov 2019] : to show red color button next to 承認 button, if desk memo exist. when click, redirect to desk memo.
			Dim lstd0060Data As New List(Of D0060)
			For Each item In d0060
				Dim data As New D0060
				data = item
				data.GYOMSTDT = item.KKNST
				data.GYOMEDDT = item.KKNED
				data.DESKMEMO = CheckDeskMemoExist(item.KKNST, item.KKNED, item.JKNST, item.JKNED, Nothing, userid, Nothing)
				lstd0060Data.Add(data)
			Next


			ViewData("D0060") = lstd0060Data

			'休暇申請で未処理のものがある場合、申請ありを表示するため
			Dim d0060Chk = db.D0060.Where(Function(m) m.SHONINFLG = False)
			If d0060Chk.Count > 0 Then
				ViewData("KYUKFLG") = "1"
			End If

			showdate = showdate.Replace("/", "")

			d0040 = d0040.Where(Function(m) m.NENGETU = showdate And m.USERID = userid)
			d0040 = d0040.Where(Function(m) m.KYUKCD = "7" Or m.KYUKCD = "9")
			d0040 = d0040.OrderBy(Function(f) f.KYUKCD).ThenBy(Function(f) f.HI).ThenBy(Function(f) f.JKNST)

			Dim listdata As New List(Of D0040)
			For i = 1 To 31
				Dim intHi As Integer = i
				Dim lstD0040 = (From t In d0040 Where t.HI = intHi).ToList

				For Each item In lstD0040
					Dim data As New D0040
					data.HI = i
					data.KYUKCD = item.KYUKCD
					data.JKNST = item.JKNST
					data.JKNED = item.JKNED
					listdata.Add(data)

				Next
				Dim needCount As Integer = 0

				If lstD0040.Count > 0 Then

					If lstD0040.Count < 5 Then
						needCount = 5 - lstD0040.Count

						For intIndex As Integer = 0 To needCount - 1
							Dim dataExtra As New D0040
							dataExtra.HI = i
							dataExtra.KYUKCD = lstD0040(0).KYUKCD
							listdata.Add(dataExtra)
						Next
					End If

				End If

			Next

			Dim intUserid As Integer = Integer.Parse(loginUserId)
			Dim m0010KOKYU = db.M0010.Find(intUserid)
			ViewData("KOKYUTENKAI") = m0010KOKYU.KOKYUTENKAI
			ViewData("KOKYUTENKAIALL") = m0010KOKYU.KOKYUTENKAIALL

			'「ユーザーテーブル」で表示対象となっているものを、「表示順」に従って表示す
			Dim M0010 = db.M0010.OrderBy(Function(f) f.HYOJJN)
			M0010 = M0010.Where(Function(m) m.HYOJ = True)

			Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			Dim lstData As New List(Of M0010)
			For Each item In lstUSERID
				Dim data As New M0010
				data.USERSEX = item.USERSEX
				data.USERID = item.USERID
				data.USERNM = item.USERNM
				Dim d0060Search = db.D0060.Where(Function(m) m.SHONINFLG = "0" And m.USERID = item.USERID)
				Dim bolExist As Boolean = False
				For Each itemD0060 In d0060Search
					If String.IsNullOrEmpty(itemD0060.KYUKCD) = False Then
						bolExist = True
						Exit For
					End If
				Next
				If bolExist = True Then
					data.MAILLADDESS = "1"
				Else
					data.MAILLADDESS = ""
				End If
				lstData.Add(data)
			Next
			ViewData.Add("UserList", lstData)

			'休日コードの休日表表示ONのものを表示する。出張は出さないようにする
			Dim M0060 = From m In db.M0060 Select m
			M0060 = M0060.Where(Function(m) m.KYUJITUHYOJ = True And m.KYUKCD <> "3")
			M0060 = M0060.OrderBy(Function(f) f.HYOJJN)

			Dim sqlpara1 As New SqlParameter("av_userid", SqlDbType.SmallInt)
			sqlpara1.Value = Integer.Parse(userid)


			Dim sqlpara2 As New SqlParameter("av_nengetsu", SqlDbType.VarChar, 6)
			sqlpara2.Value = showdate


			Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_b0050_getdata @av_userid, @av_nengetsu",
													sqlpara1, sqlpara2)

			Dim wd0040 = db.WD0040.Where(Function(m) m.USERID = userid AndAlso m.NENGETU = showdate).ToList

			ViewData("name") = name
			ViewData("frompage") = "B0050"
			ViewBag.KyukaList = M0060.ToList

			'公休展開
			Dim d0030 = db.D0030.Where(Function(m) m.NENGETU = showdate)
			ViewData.Add("UserColor", d0030.ToList())


			Dim intID As Integer = Integer.Parse(userid)
			Dim intSearchdt As Integer = Integer.Parse(showdate)
			Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
			If d0030Status Is Nothing Then
				ViewData("status") = "1"
			Else
				ViewData("status") = ""
			End If


			ViewBag.datalist = wd0040.ToList
			ViewBag.D0040List = listdata.ToList
			Return View(wd0040)
		End Function

		Function Index2(name As String) As ActionResult
			Dim d0040 = db.D0040.Include(Function(d) d.D0030).Include(Function(d) d.M0060)
			ViewData.Add("UserList", db.M0010.ToList())
			ViewData("name") = name
			Return View(d0040.ToList())
		End Function

		' GET: B0050/Details/5
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

		' GET: B0050/Create
		Function Create() As ActionResult
			ViewBag.USERID = New SelectList(db.D0030, "USERID", "INSTID")
			ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM")
			Return View()
		End Function

		' POST: B0050/Create
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Index(<Bind(Include:="KYUKCD,HI1,HI2,HI3,HI4,HI5,HI6,HI7,HI8,HI9,HI10,HI11,HI12,HI13,HI14,HI15,HI16,HI17,HI18,HI19,HI20,HI21,HI22,HI23,HI24,HI25,HI26,HI27,HI28,HI29,HI30,HI31,D0040")> ByVal lstwd0040 As List(Of WD0040), ByVal name As String, ByVal userid As String, ByVal showdate As String) As ActionResult

			ViewData!LoginUsernm = Session("LoginUsernm")

			'Dim bolIndexUpdate As Boolean = ViewData("Boolean")

			Dim intCnt As Integer = 1
			Dim strUpdDate As String = ""
			Dim strNengetsu As String = ""
			Dim bolValidateFinish As Boolean = False
			If showdate IsNot Nothing Then
				strNengetsu = showdate.Replace("/", "")
			End If

			Dim strUpdateHi As String = ""
			Dim strErrorHI As String = ""
			Dim strErrorID As String = ""
			Dim lstError As New List(Of String)
			For Each item In lstwd0040
				If item.KYUKCD = "7" OrElse item.KYUKCD = "9" Then
					If item.D0040 IsNot Nothing Then
						If bolValidateFinish = False Then
							strUpdDate = showdate & "/"
							ValidateTime(lstwd0040, strUpdDate, userid, lstError, strErrorHI, strErrorID)
							bolValidateFinish = True
						End If

					End If

				End If


			Next

			ViewData("ErrorHI") = strErrorHI
			ViewData("ErrorID") = strErrorID

			If lstError.Count = 0 Then

				Dim intID As Integer = Integer.Parse(userid)
				Dim intSearchdt As Integer = Integer.Parse(strNengetsu)
				Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
				If d0030Status Is Nothing Then
					ViewData("status") = "1"
					Return View(lstwd0040)
				Else
					ViewData("status") = ""
				End If

				For Each item In lstwd0040


					If item.HI1 = "3" Then

						strUpdateHi = "1"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)
					End If

					If item.HI2 = "3" Then

						strUpdateHi = "2"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)
					End If

					If item.HI3 = "3" Then

						strUpdateHi = "3"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI4 = "3" Then

						strUpdateHi = "4"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)


					End If

					If item.HI5 = "3" Then

						strUpdateHi = "5"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)


					End If

					If item.HI6 = "3" Then

						strUpdateHi = "6"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI7 = "3" Then

						strUpdateHi = "7"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI8 = "3" Then

						strUpdateHi = "8"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI9 = "3" Then

						strUpdateHi = "9"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI10 = "3" Then

						strUpdateHi = "10"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI11 = "3" Then

						strUpdateHi = "11"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI12 = "3" Then

						strUpdateHi = "12"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI13 = "3" Then

						strUpdateHi = "13"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI14 = "3" Then

						strUpdateHi = "14"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI15 = "3" Then

						strUpdateHi = "15"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI16 = "3" Then

						strUpdateHi = "16"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI17 = "3" Then

						strUpdateHi = "17"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI18 = "3" Then

						strUpdateHi = "18"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI19 = "3" Then

						strUpdateHi = "19"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI20 = "3" Then

						strUpdateHi = "20"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI21 = "3" Then

						strUpdateHi = "21"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI22 = "3" Then

						strUpdateHi = "22"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI23 = "3" Then

						strUpdateHi = "23"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI24 = "3" Then

						strUpdateHi = "24"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI25 = "3" Then

						strUpdateHi = "25"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI26 = "3" Then

						strUpdateHi = "26"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI27 = "3" Then

						strUpdateHi = "27"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI28 = "3" Then

						strUpdateHi = "28"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI29 = "3" Then

						strUpdateHi = "29"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI30 = "3" Then

						strUpdateHi = "30"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI31 = "3" Then

						strUpdateHi = "31"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						UpdateD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item, lstwd0040)

					End If

					If item.HI1 = "2" Then

						strUpdateHi = "1"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)
					End If

					If item.HI2 = "2" Then

						strUpdateHi = "2"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)
					End If

					If item.HI3 = "2" Then

						strUpdateHi = "3"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI4 = "2" Then

						strUpdateHi = "4"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)


					End If

					If item.HI5 = "2" Then

						strUpdateHi = "5"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)


					End If

					If item.HI6 = "2" Then

						strUpdateHi = "6"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI7 = "2" Then

						strUpdateHi = "7"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI8 = "2" Then

						strUpdateHi = "8"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI9 = "2" Then

						strUpdateHi = "9"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI10 = "2" Then

						strUpdateHi = "10"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI11 = "2" Then

						strUpdateHi = "11"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI12 = "2" Then

						strUpdateHi = "12"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI13 = "2" Then

						strUpdateHi = "13"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI14 = "2" Then

						strUpdateHi = "14"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI15 = "2" Then

						strUpdateHi = "15"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI16 = "2" Then

						strUpdateHi = "16"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI17 = "2" Then

						strUpdateHi = "17"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI18 = "2" Then

						strUpdateHi = "18"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI19 = "2" Then

						strUpdateHi = "19"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI20 = "2" Then

						strUpdateHi = "20"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI21 = "2" Then

						strUpdateHi = "21"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI22 = "2" Then

						strUpdateHi = "22"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI23 = "2" Then

						strUpdateHi = "23"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI24 = "2" Then

						strUpdateHi = "24"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI25 = "2" Then

						strUpdateHi = "25"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI26 = "2" Then

						strUpdateHi = "26"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI27 = "2" Then

						strUpdateHi = "27"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI28 = "2" Then

						strUpdateHi = "28"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI29 = "2" Then

						strUpdateHi = "29"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI30 = "2" Then

						strUpdateHi = "30"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

					If item.HI31 = "2" Then

						strUpdateHi = "31"
						strUpdDate = showdate & "/" & strUpdateHi.PadLeft(2, "0"c)
						DeleteD0040(userid, strNengetsu, strUpdDate, strUpdateHi, item)

					End If

				Next
				db.SaveChanges()

				Return RedirectToAction("Index", New With {.name = name, .userid = userid, .showdate = showdate})
			Else


				If String.IsNullOrEmpty(showdate) Then
					showdate = Today.ToString("yyyy/MM")
				End If


				ViewData("id") = userid
				ViewData("searchdt") = showdate

				'休暇申請で未処理のものがある場合、申請ありを表示するため
				Dim d0060Chk = db.D0060.Where(Function(m) m.SHONINFLG = False)
				If d0060Chk.Count > 0 Then
					ViewData("KYUKFLG") = "1"
				End If

				'休暇申請
				Dim strToday As String = DateTime.Today.ToString("yyyy/MM")
				Dim strSearchdate As String = strToday & "/01"

				Dim strLastMonthDate As String = DateTime.Now.AddMonths(-1).ToString("yyyy/MM")
				Dim strLstSearchDate As String = showdate & "/01"
				Dim lstDate As Date = Date.Parse(strLastMonthDate)
				Dim DaysInMonth As Integer = Date.DaysInMonth(lstDate.Year, lstDate.Month)
				Dim LastDayInMonthDate As Date = New Date(lstDate.Year, lstDate.Month, DaysInMonth)

				Dim d0060 = db.D0060.Include(Function(d) d.M0010).Include(Function(d) d.M0060)
				'「年月」が当月以降のときは、表示月以降の休暇申請を表示する。
				If showdate >= strToday Then
					d0060 = d0060.Where(Function(m) m.KKNST >= strSearchdate)
				End If

				If showdate <= strLastMonthDate Then
					d0060 = d0060.Where(Function(m) m.KKNST >= strLstSearchDate And m.KKNST <= LastDayInMonthDate.ToString)
				End If

				d0060 = d0060.Where(Function(m) m.USERID = userid And m.SHONINFLG = "0")
				ViewData("D0060") = d0060.ToList


				showdate = showdate.Replace("/", "")


				'「ユーザーテーブル」で表示対象となっているものを、「表示順」に従って表示す
				Dim M0010 = db.M0010.OrderBy(Function(f) f.HYOJJN)
				M0010 = M0010.Where(Function(m) m.HYOJ = True)
				Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
				Dim lstData As New List(Of M0010)
				For Each item In lstUSERID
					Dim data As New M0010
					data.USERSEX = item.USERSEX
					data.USERID = item.USERID
					data.USERNM = item.USERNM
					Dim d0060Search = db.D0060.Where(Function(m) m.SHONINFLG = "0" And m.USERID = item.USERID)
					If d0060Search.Count > 0 Then
						data.MAILLADDESS = "1"
					Else
						data.MAILLADDESS = ""
					End If
					lstData.Add(data)
				Next
				ViewData.Add("UserList", lstData)

				'休日コードの休日表表示ONのものを表示する。出張は出さないようにする
				Dim M0060 = From m In db.M0060 Select m
				M0060 = M0060.Where(Function(m) m.KYUJITUHYOJ = True And m.KYUKCD <> "3")
				M0060 = M0060.OrderBy(Function(f) f.HYOJJN)


				ViewData("name") = name
				ViewData("frompage") = "B0050"
				ViewBag.KyukaList = M0060.ToList

				'公休展開
				Dim d0030 = db.D0030.Where(Function(m) m.NENGETU = showdate)
				ViewData.Add("UserColor", d0030.ToList())

				Dim wd0040 = db.WD0040

				ViewBag.datalist = wd0040.ToList



				ViewBag.Error = lstError

				Dim strJtjknSTHH As String = ""
				Dim strJtjknSTMM As String = ""
				Dim strJtjknEDHH As String = ""
				Dim strJtjknEDMM As String = ""

				Dim listdata As New List(Of D0040)
				For Each item In lstwd0040
					If item.D0040 IsNot Nothing Then
						For Each itemd0040 In item.D0040
							listdata.Add(itemd0040)

							If itemd0040.JKNST IsNot Nothing Then
								Dim strJiST As String() = itemd0040.JKNST.Split(":")
								strJtjknSTHH = strJiST(0).PadLeft(2, "0")
								strJtjknSTMM = strJiST(1).PadLeft(2, "0")

								itemd0040.JKNST = strJtjknSTHH & strJtjknSTMM
							End If


							If itemd0040.JKNED IsNot Nothing Then
								Dim strjiEND As String() = itemd0040.JKNED.Split(":")
								strJtjknEDHH = strjiEND(0).PadLeft(2, "0")
								strJtjknEDMM = strjiEND(1).PadLeft(2, "0")
								itemd0040.JKNED = strJtjknEDHH & strJtjknEDMM
							End If

						Next
					End If

				Next

				ViewBag.D0040List = listdata.ToList
				Return View(lstwd0040)
			End If

		End Function

		Private m_lstHI As New List(Of String)
		Private Sub UpdateD0040(ByVal userid As String, ByVal strNengetsu As String, ByVal strUpdDate As String, ByVal strUpdateHi As String, ByVal item As WD0040, ByVal lstWd0040 As List(Of WD0040))

			Dim JTDate As Date = Date.Parse(strUpdDate)
			Dim JTEndDate As Date = JTDate.AddDays(1)
			Dim strStjikan As String = ""
			Dim strEndjikan As String = ""
			Dim UpdFlg As Boolean = False
			Dim intUserID As Integer = Integer.Parse(userid)
			Dim intNengetsu As Integer = Integer.Parse(strNengetsu)
			Dim strJtjknSTHH As String = ""
			Dim strJtjknSTMM As String = ""
			Dim strJtjknEDHH As String = ""
			Dim strJtjknEDMM As String = ""
			Dim bolUpdate As Boolean = False
			Dim bolDelete As Boolean = False
			Dim d0040updt = From m In db.D0040 Select m
			d0040updt = d0040updt.Where(Function(m) m.USERID = intUserID And m.NENGETU = intNengetsu And m.HI = strUpdateHi)

			Dim d0040Check = d0040updt.Where(Function(m) m.USERID = intUserID And m.NENGETU = intNengetsu And m.HI = strUpdateHi And m.KYUKCD = item.KYUKCD)
			If d0040Check.Count = 0 Then
				bolUpdate = True
			End If

			For Each d0040item In d0040updt
				If d0040item.KYUKCD <> "7" AndAlso d0040item.KYUKCD <> "9" AndAlso d0040item.KYUKCD <> "10" AndAlso d0040item.KYUKCD <> "2" Then
					If item.KYUKCD <> d0040item.KYUKCD Then
						Dim D0040 As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
						db.D0040.Remove(D0040)
						bolUpdate = True
						bolDelete = True
					End If

				Else
					If item.KYUKCD <> "7" AndAlso item.KYUKCD <> "9" AndAlso item.KYUKCD <> "10" AndAlso item.KYUKCD <> "2" Then
						If item.KYUKCD <> d0040item.KYUKCD Then
							Dim D0040 As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
							bolUpdate = True
							db.D0040.Remove(D0040)
							bolDelete = True
						End If

					Else

						If item.KYUKCD = "7" OrElse item.KYUKCD = "9" Then
							'If item.KYUKCD = d0040item.KYUKCD Then
							'    Dim D0040 As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
							'    db.D0040.Remove(D0040)
							'    bolDelete = True
							'End If

						Else
							If item.KYUKCD = "2" Then
								If d0040item.KYUKCD = "10" Then
									Dim D0040 As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
									db.D0040.Remove(D0040)
									bolDelete = True
								End If
							ElseIf item.KYUKCD = "10" Then
								If d0040item.KYUKCD = "2" Then
									Dim D0040 As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
									db.D0040.Remove(D0040)
									bolDelete = True
								End If
							End If

						End If
					End If
				End If

			Next

			If item.KYUKCD <> "7" AndAlso item.KYUKCD <> "9" Then
				If bolUpdate = True Then
					Dim data As New D0040
					data.USERID = userid
					data.NENGETU = strNengetsu
					data.HI = strUpdateHi
					data.JKNST = "0000"
					data.JKNED = "2400"
					data.JTJKNST = JTDate

					If item.KYUKCD = "1" Then
						data.JTJKNED = JTDate
					Else
						data.JTJKNED = JTEndDate
					End If

					data.KYUKCD = item.KYUKCD
					db.D0040.Add(data)
					'End If
					AddHistoryData(bolDelete, intUserID, data)
				End If

			Else

				'同じ日のものは一回で処理したので、もう一回来たら同じ日は処理しないようにする
				If m_lstHI.Contains(strUpdateHi) = False Then
					Dim lstD0040 As New List(Of D0040)
					m_lstHI.Add(strUpdateHi)
					Dim strKyukacd As String = ""
					Dim strLoopKyukacd As String = ""
					Dim intTo As Integer = 1

					'一日にある時間休、時間強休を一括でチェック、削除、追加、更新したいため、時間休が来たら時間強休も取得して処理する、同じく時間強休来たら時間休取得して処理する
					If item.KYUKCD = "7" Then
						strKyukacd = "9"
					Else
						strKyukacd = "7"
					End If

					Dim lstOtherD0040 As New List(Of D0040)
					For Each itemWd0040 In lstWd0040
						If itemWd0040.KYUKCD = strKyukacd Then
							If itemWd0040.D0040 IsNot Nothing AndAlso itemWd0040.D0040.Count > 0 Then
								For Each itemD0040 In itemWd0040.D0040
									If itemD0040.HI = strUpdateHi Then
										lstOtherD0040.Add(itemD0040)
									End If
								Next

							End If
						End If
					Next

					'時間休か時間強休、他のものがあればループを2回する
					If lstOtherD0040 IsNot Nothing AndAlso lstOtherD0040.Count > 0 Then
						intTo = 2
					End If

					For intStart As Integer = 1 To intTo
						strLoopKyukacd = item.KYUKCD
						lstD0040 = item.D0040
						If intStart = 2 Then
							lstD0040 = lstOtherD0040
							strLoopKyukacd = strKyukacd
						End If


						If lstD0040 IsNot Nothing AndAlso lstD0040.Count > 0 Then
							Dim dtSyuseiymd As Date = Now
							Dim lstNew As New List(Of D0040)
							Dim lstOld As New List(Of D0040)

							'画面から来たデータ
							For Each itemNew In lstD0040
								If itemNew.HI = strUpdateHi AndAlso itemNew.JKNST IsNot Nothing Then
									Dim data As New D0040
									data.USERID = userid
									data.NENGETU = intNengetsu
									data.HI = itemNew.HI
									data.JKNST = ChangeToHHMM(itemNew.JKNST)
									data.JKNED = ChangeToHHMM(itemNew.JKNED)
									data.KYUKCD = itemNew.KYUKCD
									lstNew.Add(data)

								End If
							Next

							'すでにテーブルにあるデータ
							If d0040updt.Count > 0 Then
								For Each itemold As D0040 In d0040updt
									If itemold.KYUKCD = strLoopKyukacd Then
										Dim data As New D0040
										data.USERID = itemold.USERID
										data.NENGETU = itemold.NENGETU
										data.HI = itemold.HI
										data.JKNST = itemold.JKNST
										data.JKNED = itemold.JKNED
										data.KYUKCD = itemold.KYUKCD
										data.JTJKNST = itemold.JTJKNST
										data.JTJKNED = itemold.JTJKNED
										lstOld.Add(data)
									End If


								Next
							End If


							'画面から来た時間休の数がすでにあるデータより数が大きい時、
							If lstNew.Count > lstOld.Count Then

								For Each itemD0040 In lstNew

									Dim result = lstOld.Where(Function(x) x.KYUKCD = itemD0040.KYUKCD And x.HI = itemD0040.HI And x.JKNST = itemD0040.JKNST).ToList

									If result.Count = 0 Then
										bolDelete = False
										addTimeData(itemD0040, intUserID, strNengetsu, strUpdateHi, JTDate, strLoopKyukacd, bolDelete)


									Else
										If result.Item(0).JKNST <> itemD0040.JKNST OrElse result.Item(0).JKNED <> itemD0040.JKNED Then

											Dim D0040 As D0040 = db.D0040.Find(result.Item(0).USERID, result.Item(0).NENGETU, result.Item(0).HI, result.Item(0).JKNST)
											bolDelete = True
											db.D0040.Remove(D0040)

											addTimeData(itemD0040, intUserID, strNengetsu, strUpdateHi, JTDate, strLoopKyukacd, bolDelete)


										End If
									End If

								Next

							ElseIf lstNew.Count = lstOld.Count Then

								For intIndex As Integer = 0 To lstNew.Count - 1
									Dim itemD0040 As D0040 = lstNew(intIndex)
									Dim itemOld As D0040 = lstOld(intIndex)

									If itemD0040.JKNST <> itemOld.JKNST OrElse itemD0040.JKNED <> itemOld.JKNED Then
										Dim D0040 As D0040 = db.D0040.Find(itemOld.USERID, itemOld.NENGETU, itemOld.HI, itemOld.JKNST)
										bolDelete = True
										db.D0040.Remove(D0040)
										addTimeData(itemD0040, intUserID, strNengetsu, strUpdateHi, JTDate, strLoopKyukacd, bolDelete)
									End If


								Next
							Else

								For Each d0040item In lstOld
									Dim result = lstNew.Where(Function(x) x.KYUKCD = d0040item.KYUKCD And x.HI = d0040item.HI And x.JKNST = d0040item.JKNST).ToList
									If result.Count = 0 Then

										'テーブルにあって、新しく来たリストにない場合、削除される
										Dim D0040 As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
										bolUpdate = True
										db.D0040.Remove(D0040)

										'変更履歴の作成
										Dim m0010 = db.M0010.Find(intUserID)
										Dim m0060 = db.M0060.Find(d0040item.KYUKCD)
										Dim d0150 As New D0150
										Dim strNENGETU As String = d0040item.NENGETU.ToString()
										Dim dtYMD As Date = Date.Parse(strNENGETU.Substring(0, 4) & "/" & strNENGETU.Substring(4, 2) & "/" & d0040item.HI.ToString)
										d0150.HENKORRKCD = decNewHenkorrkcd + 1
										d0150.HENKONAIYO = "削除"
										d0150.USERID = Session("LoginUserid")
										d0150.SYUSEIYMD = dtSyuseiymd
										d0150.KKNST = dtYMD
										d0150.KKNED = dtYMD
										d0150.JKNST = d0040item.JKNST.Replace(":", "").PadLeft(4, "0")
										d0150.JKNED = d0040item.JKNED.Replace(":", "").PadLeft(4, "0")
										d0150.SHINSEIUSER = m0010.USERNM
										d0150.KYUKNM = m0060.KYUKNM
										d0150.GYOMMEMO = d0040item.GYOMMEMO
										db.D0150.Add(d0150)
										decNewHenkorrkcd += 1
									End If

								Next


							End If

						End If
					Next

				End If

			End If



		End Sub


		Private Sub addTimeData(ByVal itemD0040 As D0040, ByVal intUserID As Integer, ByVal strNengetsu As String, ByVal strUpdateHi As String, ByVal JTDate As Date, ByVal strKYUCD As String, ByVal bolDelete As Boolean)
			Dim strJtjknSTHH As String = ""
			Dim strJtjknSTMM As String = ""
			Dim strJtjknEDHH As String = ""
			Dim strJtjknEDMM As String = ""
			Dim data As New D0040
			data.USERID = intUserID
			data.NENGETU = strNengetsu
			data.HI = strUpdateHi
			If itemD0040.JKNST IsNot Nothing And itemD0040.JKNED IsNot Nothing Then
				If itemD0040.HI = strUpdateHi Then
					Dim strJiST As String = itemD0040.JKNST
					strJtjknSTHH = strJiST.Substring(0, 2)
					strJtjknSTMM = strJiST.Substring(2, 2)

					Dim strjiEND As String = itemD0040.JKNED
					strJtjknEDHH = strjiEND.Substring(0, 2)
					strJtjknEDMM = strjiEND.Substring(2, 2)

					data.JKNST = strJtjknSTHH & strJtjknSTMM
					data.JKNED = strJtjknEDHH & strJtjknEDMM

					'36:00まで登録可能なので、実時間を２４時間制度に変更する
					If strJtjknSTHH >= "24" Then
						Dim intHH As Integer = Integer.Parse(strJtjknSTHH) - 24
						strJtjknSTHH = intHH.ToString.PadLeft(2, "0")
						data.JTJKNST = JTDate.AddDays(1) & " " & strJtjknSTHH & ":" & strJtjknSTMM
					Else
						data.JTJKNST = JTDate & " " & strJtjknSTHH & ":" & strJtjknSTMM
					End If

					If strJtjknEDHH >= "24" Then
						Dim intHH As Integer = Integer.Parse(strJtjknEDHH) - 24
						strJtjknEDHH = intHH.ToString.PadLeft(2, "0")
						data.JTJKNED = JTDate.AddDays(1) & " " & strJtjknEDHH & ":" & strJtjknEDMM
					Else
						data.JTJKNED = JTDate & " " & strJtjknEDHH & ":" & strJtjknEDMM

					End If


					data.KYUKCD = strKYUCD

					'Dim d0040upd As D0040 = db.D0040.Find(itemD0040.USERID, itemD0040.NENGETU, itemD0040.HI, data.JKNST)

					'If d0040upd IsNot Nothing Then
					'    d0040upd.JKNST = data.JKNST
					'    d0040upd.JKNED = data.JKNED
					'Else
					db.D0040.Add(data)
					'End If
					AddHistoryData(bolDelete, intUserID, data)

				End If


			End If
		End Sub
		Function ChangeToHHMM(ByVal strTime As String)

			If String.IsNullOrEmpty(strTime) = False Then
				Dim strHH As String = ""
				Dim strMM As String = ""

				If strTime.Contains(":") Then
					Dim strs As String() = strTime.PadLeft(5, "0").Split(":")
					strHH = strs(0).PadLeft(2, "0")
					strMM = strs(1).PadLeft(2, "0")
					strTime = strHH & strMM
				Else
					If strTime.Length <= 2 Then
						strHH = strTime.PadLeft(2, "0")
						strMM = "00"
						strTime = strHH & strMM
					End If
				End If
			End If

			Return strTime
		End Function

		Private decNewHenkorrkcd As Decimal = GetMaxHenkorrkcd()
		Private Sub AddHistoryData(ByVal bolDelete As Boolean, ByVal intUserid As Integer, ByVal data As D0040)

			Dim dtSyuseiymd As Date = Now
			Dim M0060 = db.M0060.Find(data.KYUKCD)
			Dim m0010 = db.M0010.Find(intUserid)

			Dim strNENGETU As String = data.NENGETU.ToString()
			Dim dtYMD As Date = Date.Parse(strNENGETU.Substring(0, 4) & "/" & strNENGETU.Substring(4, 2) & "/" & data.HI.ToString)

			If bolDelete = False Then
				'変更履歴の作成

				Dim d0150 As New D0150
				d0150.HENKORRKCD = decNewHenkorrkcd + 1
				d0150.HENKONAIYO = "登録"
				d0150.USERID = Session("LoginUserid")
				d0150.SYUSEIYMD = dtSyuseiymd
				d0150.KKNST = dtYMD
				d0150.KKNED = dtYMD
				d0150.JKNST = data.JKNST.Replace(":", "").PadLeft(4, "0")
				d0150.JKNED = data.JKNED.Replace(":", "").PadLeft(4, "0")
				d0150.SHINSEIUSER = m0010.USERNM
				d0150.KYUKNM = M0060.KYUKNM
				d0150.GYOMMEMO = data.GYOMMEMO
				db.D0150.Add(d0150)
				decNewHenkorrkcd += 1
			Else
				'変更履歴の作成
				Dim d0150upt As New D0150
				d0150upt.HENKORRKCD = decNewHenkorrkcd + 1
				d0150upt.HENKONAIYO = "変更"
				d0150upt.USERID = Session("LoginUserid")
				d0150upt.SYUSEIYMD = dtSyuseiymd
				d0150upt.KKNST = dtYMD
				d0150upt.KKNED = dtYMD
				d0150upt.JKNST = data.JKNST.Replace(":", "").PadLeft(4, "0")
				d0150upt.JKNED = data.JKNED.Replace(":", "").PadLeft(4, "0")
				d0150upt.SHINSEIUSER = m0010.USERNM
				d0150upt.KYUKNM = M0060.KYUKNM
				d0150upt.GYOMMEMO = data.GYOMMEMO
				db.D0150.Add(d0150upt)
				decNewHenkorrkcd += 1
			End If

		End Sub

		Function GetMaxHenkorrkcd() As Decimal
			'変更履歴コードの最大値の取得
			Dim decMaxHenkorrkcd As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "00000")
			Dim lstHENKORRKCD = (From t In db.D0150 Where t.HENKORRKCD > decMaxHenkorrkcd Select t.HENKORRKCD).ToList
			If lstHENKORRKCD.Count > 0 Then
				decMaxHenkorrkcd = lstHENKORRKCD.Max
			End If

			Return decMaxHenkorrkcd
		End Function

		Private Sub DeleteD0040(ByVal userid As String, ByVal strNengetsu As String, ByVal strUpdDate As String, ByVal strUpdateHi As String, ByVal item As WD0040)

			Dim UpdFlg As Boolean = False
			Dim intUserID As Integer = Integer.Parse(userid)
			Dim intNengetsu As Integer = Integer.Parse(strNengetsu)

			Dim bolUpdate As Boolean = False
			Dim d0040updt = From m In db.D0040 Select m
			d0040updt = d0040updt.Where(Function(m) m.USERID = intUserID And m.NENGETU = intNengetsu And m.HI = strUpdateHi And m.KYUKCD = item.KYUKCD)


			Dim dtSyuseiymd As Date = Now

			'削除するのは時間休か時間強休と共存している休出と２４時間超えだけ
			For Each d0040item In d0040updt

				Dim D0040 As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)

				If D0040 IsNot Nothing Then
					db.D0040.Remove(D0040)

					If d0040item.KYUKCD = "2" OrElse d0040item.KYUKCD = "10" Then
						'変更履歴の作成
						Dim m0010 = db.M0010.Find(intUserID)
						Dim m0060 = db.M0060.Find(d0040item.KYUKCD)
						Dim strNENGETU As String = d0040item.NENGETU.ToString()
						Dim dtYMD As Date = Date.Parse(strNENGETU.Substring(0, 4) & "/" & strNENGETU.Substring(4, 2) & "/" & d0040item.HI.ToString)
						Dim d0150 As New D0150
						d0150.HENKORRKCD = decNewHenkorrkcd + 1
						d0150.HENKONAIYO = "変更"
						d0150.USERID = Session("LoginUserid")
						d0150.SYUSEIYMD = dtSyuseiymd
						d0150.KKNST = dtYMD
						d0150.KKNED = dtYMD
						d0150.JKNST = d0040item.JKNST.Replace(":", "").PadLeft(4, "0")
						d0150.JKNED = d0040item.JKNED.Replace(":", "").PadLeft(4, "0")
						d0150.SHINSEIUSER = m0010.USERNM
						d0150.KYUKNM = "勤務"
						d0150.GYOMMEMO = d0040item.GYOMMEMO
						db.D0150.Add(d0150)
						decNewHenkorrkcd += 1
					End If



				End If

			Next

		End Sub

		' GET: B0050/Edit/5
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

		' POST: B0050/Edit/5
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

		' GET: B0050/Delete/5
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

		' POST: B0050/Delete/5
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

		' GET: D0060/Edit/5
		<HttpPost()>
		Function CheckD00040(ByVal id As Decimal) As JsonResult
			'If IsNothing(id) Then
			'    Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			'End If
			TempData("success") = ""
			Dim d0060 As D0060 = db.D0060.Find(id)
			Dim strMessage As String = ""
			Dim strError As String = ""
			Dim strNengetsu = d0060.KKNST.ToString.Substring(0, 7)
			Dim strHI = d0060.KKNST.ToString.Substring(8, 2)
			Dim strDate = strNengetsu & "/" & strHI
			strNengetsu = strNengetsu.Replace("/", "")
			Dim intUserid As Integer = Integer.Parse(d0060.USERID)
			Dim intNengetsu As Integer = Integer.Parse(strNengetsu)
			Dim lstD0040 As New List(Of D0040)
			Dim d0040updt = From m In db.D0040 Select m
			d0040updt = d0040updt.Where(Function(m) m.USERID = intUserid And m.NENGETU = intNengetsu And m.HI = strHI)

			'公休展開のチェック
			Dim intID As Integer = Integer.Parse(intUserid)
			Dim strNenngetsuChk As String = d0060.KKNED.ToString.Substring(0, 7)
			strNenngetsuChk = strNenngetsuChk.Replace("/", "")
			Dim intSearchdt As Integer = Integer.Parse(strNenngetsuChk)
			Dim d0030Status As D0030 = db.D0030.Find(intID, intSearchdt)
			If d0030Status Is Nothing Then
				strError = String.Format("公休展開されていないため、承認できません。", strDate)
				Return Json(New With {.success = False, .text = strError})
			End If


			If d0060.KYUKCD = "6" OrElse d0060.KYUKCD = "8" Then
				Dim strKKNST As String = d0060.KKNST
				Dim strKKNED As String = d0060.KKNED
				Dim d0010 = From m In db.D0010 Select m
				d0010 = d0010.Where(Function(m) (strKKNST) <= m.GYOMYMDED AndAlso (strKKNED) >= m.GYOMYMD)
				d0010 = d0010.Where(Function(d1) db.D0020.Any(Function(d2) d2.GYOMNO = d1.GYOMNO AndAlso d2.USERID = d0060.USERID))

				For Each item In d0010
					If item IsNot Nothing Then
						strError = String.Format("シフト有りの日に対して、代休、強休は設定できません。", strDate)
						Return Json(New With {.success = False, .text = strError})
					End If
				Next

			End If


			If d0060.KYUKCD = "7" OrElse d0060.KYUKCD = "9" Then

				'時間休、時間強休の場合シフト時間とチェック
				Dim db As New Model1

				Dim sqlpara1 As New SqlParameter("av_userid", SqlDbType.SmallInt)
				sqlpara1.Value = d0060.USERID

				Dim strUpdateDate As String = d0060.KYUKCD
				Dim strjtjikan As String = GetJtjkn(d0060.KKNST, d0060.JKNST)
				Dim strjijikend As String = GetJtjkn(d0060.KKNED, d0060.JKNED)

				Dim sqlpara2 As New SqlParameter("ld_jtjknst", SqlDbType.DateTime)
				sqlpara2.Value = DateTime.Parse(strjtjikan).ToString("yyyy/MM/dd HH:mm:ss")

				Dim sqlpara3 As New SqlParameter("ld_jtjkned", SqlDbType.DateTime)
				sqlpara3.Value = DateTime.Parse(strjijikend).ToString("yyyy/MM/dd HH:mm:ss")

				Dim sqlpara4 As New SqlParameter("ln_retval", SqlDbType.Int)
				sqlpara4.Direction = ParameterDirection.Output
				sqlpara4.Value = ""

				Dim cnt = db.Database.ExecuteSqlCommand("Exec TeLAS.pr_b0050_chkoverlaptime @av_userid, @ld_jtjknst, @ld_jtjkned,@ln_retval OUTPUT ",
					sqlpara1, sqlpara2, sqlpara3, sqlpara4)

				Dim intResult As Integer = sqlpara4.Value
				If intResult = "1" Then

					strError = String.Format("{0}日に申請の時間がシフトの時間と重なっています。", strDate)
					Return Json(New With {.success = False, .text = strError})

				End If

			End If

			For Each d0040item In d0040updt

				Dim m0060 = db.M0060.Find(d0040item.KYUKCD)
				Dim strOldkyukanm As String = m0060.KYUKNM
				Dim m0060New = db.M0060.Find(d0060.KYUKCD)
				Dim strKyukanm As String = m0060New.KYUKNM

				'公休の場合、休暇申請できない。
				If d0040item.KYUKCD = "4" Then
					If d0060.KYUKCD <> 8 Then '強休の場合は公休の日でも申請を許すため
						strError = String.Format("{0}日は公休なので、休暇申請できません。", strDate)
						Return Json(New With {.success = False, .text = strError})
					End If
				End If


				If d0040item.KYUKCD <> "7" AndAlso d0040item.KYUKCD <> "9" AndAlso d0040item.KYUKCD <> "1" AndAlso d0040item.KYUKCD <> "2" AndAlso d0040item.KYUKCD <> "10" Then
					strMessage = String.Format("{0}から{1}に更新します。よろしいですか？", strOldkyukanm, strKyukanm)
					Return Json(New With {.success = True, .text = strMessage})
				Else
					If d0040item.KYUKCD = "7" OrElse d0040item.KYUKCD = "9" Then
						Dim d0040Old As New D0040
						d0040Old.KYUKCD = d0040item.KYUKCD
						d0040Old.JKNST = d0040item.JKNST
						d0040Old.JKNED = d0040item.JKNED
						lstD0040.Add(d0040Old)
						If d0060.KYUKCD <> "7" AndAlso d0060.KYUKCD <> "9" Then
							strMessage = String.Format("{0}から{1}に更新します。よろしいですか？", strOldkyukanm, strKyukanm)
							Return Json(New With {.success = True, .text = strMessage})
						End If

					End If
					If d0040item.KYUKCD = "2" OrElse d0040item.KYUKCD = "10" Then
						If d0060.KYUKCD <> "7" AndAlso d0060.KYUKCD <> "9" Then
							strMessage = String.Format("{0}から{1}に更新します。よろしいですか？", strOldkyukanm, strKyukanm)
							Return Json(New With {.success = True, .text = strMessage})
						End If
					End If
				End If

			Next


			'時間休、時間強休の場合　休日設定にすでにある 時間休、時間強休とチェック
			If d0060.KYUKCD = "7" OrElse d0060.KYUKCD = "9" Then
				Dim d0040 As New D0040
				d0040.KYUKCD = d0060.KYUKCD
				d0040.JKNST = d0060.JKNST
				d0040.JKNED = d0060.JKNED
				lstD0040.Add(d0040)
				strError = ""
				'CheckTimeDuplication(lstD0040, strError, strDate)

				Dim sorted = lstD0040.OrderBy(Function(f) f.JKNST.PadLeft(4, "0"))

				Dim lstChk As New List(Of D0040)
				For Each item In sorted.ToList


					Dim strJKNST As String = item.JKNST.PadLeft(4, "0")
					Dim strHH As String = strJKNST.Substring(0, 2)
					Dim strMM As String = strJKNST.Substring(2, 2)
					Dim strSTTIME As String = strHH & ":" & strMM

					item.JKNST = strSTTIME

					Dim strJKNED As String = item.JKNED.PadLeft(4, "0")
					Dim strHHEnd As String = strJKNED.Substring(0, 2)
					Dim strMMEnd As String = strJKNED.Substring(2, 2)
					Dim strSTTIMEEnd As String = strHHEnd & ":" & strMMEnd
					item.JKNED = strSTTIMEEnd

					lstChk.Add(item)

				Next
				Dim dtVALDTT As DateTime = Nothing
				Dim dtNextVALDTF As DateTime = Nothing
				'Dim rowIndex As Integer = 0
				For intIdx As Integer = 0 To lstChk.Count - 2

					If lstChk(intIdx).JKNST IsNot Nothing AndAlso lstChk(intIdx).JKNED IsNot Nothing Then

						Dim strSearchDate As String = strNengetsu.Substring(0, 4) & "/" & strNengetsu.Substring(4, 2) & "/" & strHI
						Dim strjtjikan As String = GetJtjkn(strSearchDate, lstChk(intIdx + 1).JKNST)
						Dim strjijikend As String = GetJtjkn(strSearchDate, lstChk(intIdx).JKNED)

						Date.TryParse(strjijikend, dtVALDTT)
						Date.TryParse(strjtjikan, dtNextVALDTF)


						If dtVALDTT > dtNextVALDTF Then
							strError = String.Format("{0}日に申請の時間と重複している時間休か時間強休があります。", strDate)
							Exit For
						End If

					End If

				Next


				If String.IsNullOrEmpty(strError) = False Then

					Return Json(New With {.success = False, .text = strError})

				End If

			End If

			If String.IsNullOrEmpty(strError) = True AndAlso String.IsNullOrEmpty(strMessage) = True Then
				Dim m0060New = db.M0060.Find(d0060.KYUKCD)
				Dim strKyukanm As String = m0060New.KYUKNM
				strMessage = String.Format("{0}に更新します。よろしいですか？", strKyukanm)
			End If

			Return Json(New With {.success = True, .text = strMessage})

		End Function

		Shared Function GetJtjkn(ByVal dtymd As Date, ByVal strHHMM As String) As Date
			Dim dtRtn As Date = Nothing
			strHHMM = strHHMM.Replace(":", "").PadLeft(4, "0")
			Dim strHH As String = strHHMM.Substring(0, 2)
			Dim strMM As String = strHHMM.Substring(2, 2)

			'36:00まで登録可能なので、実時間を２４時間制度に変更する
			If strHH >= "24" Then
				Dim intHH As Integer = Integer.Parse(strHH) - 24
				strHH = intHH.ToString.PadLeft(2, "0")
				dtymd = dtymd.AddDays(1)
			End If

			dtRtn = Date.Parse(dtymd.ToString("yyyy/MM/dd") & " " & strHH & ":" & strMM)

			Return dtRtn
		End Function


		Private Sub CheckTimeDuplication(ByVal lstD0040 As List(Of D0040), ByRef strError As String, ByVal strDate As String)
			Dim bolRet As Boolean = True

			Dim sorted = lstD0040.OrderBy(Function(f) f.JKNST.PadLeft(4, "0"))

			Dim lstChk As New List(Of D0040)
			For Each item In sorted.ToList


				Dim strJKNST As String = item.JKNST.PadLeft(4, "0")
				Dim strHH As String = strJKNST.Substring(0, 2)
				Dim strMM As String = strJKNST.Substring(2, 2)
				Dim strSTTIME As String = strHH & ":" & strMM

				item.JKNST = strSTTIME

				Dim strJKNED As String = item.JKNED.PadLeft(4, "0")
				Dim strHHEnd As String = strJKNED.Substring(0, 2)
				Dim strMMEnd As String = strJKNED.Substring(2, 2)
				Dim strSTTIMEEnd As String = strHHEnd & ":" & strMMEnd
				item.JKNED = strSTTIMEEnd

				lstChk.Add(item)

			Next
			Dim dtVALDTT As DateTime = Nothing
			Dim dtNextVALDTF As DateTime = Nothing
			'Dim rowIndex As Integer = 0
			For intIdx As Integer = 0 To lstChk.Count - 1

				If lstChk(intIdx).JKNST IsNot Nothing AndAlso lstChk(intIdx).JKNED IsNot Nothing Then

					DateTime.TryParse(lstChk(intIdx).JKNED.ToString, dtVALDTT)
					DateTime.TryParse(lstChk(intIdx + 1).JKNST.ToString, dtNextVALDTF)

					If dtVALDTT > dtNextVALDTF Then
						strError = String.Format("{0}日に申請の時間と重複している時間休か時間強休があります。", strDate)
						Exit For
					End If
				End If

			Next


		End Sub


		' GET: D0060/Edit/5
		<HttpPost()>
		Function UpdateD0060Shonin(ByVal id As Decimal, ByVal name As String, ByVal userid As String, ByVal showdate As String, ByVal kanrimemo As String) As ActionResult

			'If IsNothing(id) Then
			'    Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			'End If
			'If String.IsNullOrEmpty(name) = True Then
			'    Dim strError As String = String.Format("日は公休なので、休暇申請できません。")
			'    Return Json(New With {.success = True, .text = strError})
			'End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return Json(New With {.Url = New UrlHelper(Request.RequestContext).Action("Index", "Login")})
			End If

			Dim d0060 As D0060 = db.D0060.Find(id)

			''承認済み
			d0060.SHONINFLG = "1"
			'ASI[08 Jan 2020] : Update in D0060,even not executed and direct Approving
			d0060.KANRIMEMO = kanrimemo
			Dim decNewHenkorrkcd As Decimal = GetMaxHenkorrkcd() + 1
			Dim dtSyuseiymd As Date = Now

			Dim dtst As Date = d0060.KKNST
			Dim dtEd As Date = d0060.KKNED
			Dim lstD0040Updt As New List(Of D0040)

			Dim dtymd As Date = dtst

			Do While dtymd <= dtEd

				'申請で休暇テーブルにデータを作る
				Dim strNengetsu = dtymd.ToString.Substring(0, 7)
				Dim strHI = dtymd.ToString.Substring(8, 2)
				strNengetsu = strNengetsu.Replace("/", "")
				Dim intUserid As Integer = Integer.Parse(d0060.USERID)
				Dim intNengetsu As Integer = Integer.Parse(strNengetsu)
				Dim intHI As Integer = Integer.Parse(strHI)


				Dim d0040updt = db.D0040.Where(Function(m) m.USERID = intUserid And m.NENGETU = intNengetsu And m.HI = strHI).ToList

				For Each d0040item In d0040updt
					If d0040item.KYUKCD <> "7" AndAlso d0040item.KYUKCD <> "9" AndAlso d0040item.KYUKCD <> "2" AndAlso d0040item.KYUKCD <> "10" Then
						Dim d0040delete As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
						db.D0040.Remove(d0040delete)

					Else
						'新しく来たものが時間休、時間強休の場合、すでにあるのは休出、２４時間超え、時間休、時間強休じゃないものを時削除
						If d0060.KYUKCD <> "7" AndAlso d0060.KYUKCD <> "9" Then
							Dim d0040delete As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
							db.D0040.Remove(d0040delete)
						Else
							If d0040item.KYUKCD <> "7" AndAlso d0040item.KYUKCD <> "9" AndAlso d0040item.KYUKCD <> "2" AndAlso d0040item.KYUKCD <> "10" Then
								Dim d0040delete As D0040 = db.D0040.Find(d0040item.USERID, d0040item.NENGETU, d0040item.HI, d0040item.JKNST)
								db.D0040.Remove(d0040delete)
							End If
						End If

					End If

				Next

				Dim data As New D0040
				data.USERID = d0060.USERID
				data.NENGETU = strNengetsu
				data.HI = strHI
				data.JKNST = d0060.JKNST
				data.JKNED = d0060.JKNED
				data.KANRIMEMO = kanrimemo

				Dim strjtjikan As String = GetJtjkn(d0060.KKNST, d0060.JKNST)
				Dim strjijikend As String = GetJtjkn(d0060.KKNED, d0060.JKNED)

				data.JTJKNST = strjtjikan
				data.JTJKNED = strjijikend
				data.KYUKCD = d0060.KYUKCD
				data.BIKO = d0060.GYOMMEMO

				lstD0040Updt.Add(data)


				dtymd = dtymd.AddDays(1)

			Loop

			'変更履歴の作成
			Dim d0150 As New D0150
			d0150.HENKORRKCD = decNewHenkorrkcd
			d0150.HENKONAIYO = "承認"
			d0150.USERID = Session("LoginUserid")
			d0150.SYUSEIYMD = dtSyuseiymd
			d0150.KKNST = d0060.KKNST
			d0150.KKNED = d0060.KKNED
			d0150.JKNST = d0060.JKNST
			d0150.JKNED = d0060.JKNED
			d0150.SHINSEIUSER = d0060.M0010.USERNM
			d0150.KYUKNM = d0060.M0060.KYUKNM
			d0150.GYOMMEMO = d0060.GYOMMEMO
			db.D0150.Add(d0150)
			decNewHenkorrkcd += 1

			For Each d0040updt In lstD0040Updt
				db.D0040.Add(d0040updt)
			Next

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
					tran.Rollback()
					Throw ex
				End Try
			End Using


			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
			ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)
			'Return RedirectToAction("Index", "B0050", New With {.name = "test", .userid = "29", .showdate = "2017/03"})
			'Return Json(New With {.success = True})
			Dim intShorikbn As String = "1"
			Dim decKyukasnscd As Decimal = id
			'Return RedirectToAction("SendMail", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn, .kyukasnscd = decKyukasnscd})
			'Return Json(Url.Action("SendMail", "B0050", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn, .kyukasnscd = decKyukasnscd}))
			'ViewData("Boolean") = False
			Dim redirectUrl = New UrlHelper(Request.RequestContext).Action("SendMail", "B0050", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn, .kyukasnscd = decKyukasnscd, .name = name, .userid = userid, .showdate = showdate})
			Return Json(New With {.Url = redirectUrl})


		End Function

		Function SendMail(ByVal acuserid As Integer, ByVal shorikbn As Integer, ByVal kyukasnscd As Decimal, ByVal name As String, ByVal userid As String, ByVal showdate As String) As ActionResult

			If IsNothing(acuserid) OrElse IsNothing(shorikbn) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim loginUserId As String = Session("LoginUserid")
			Dim loginUserKanri As Boolean = Session("LoginUserKanri")
			Dim loginUserSystem As Boolean = Session("LoginUserSystem")

			ViewData!LoginUsernm = Session("LoginUsernm")

			Dim strShori As String = ""

			'承認
			If shorikbn = 1 Then
				strShori = "承認"

				'却下
			ElseIf shorikbn = 2 Then
				strShori = "却下"
			End If

			Dim s0010 = db.S0010.Find(1)
			Dim w0040 As New W0040
			Dim d0060 As D0060 = db.D0060.Find(kyukasnscd)
			If d0060 Is Nothing Then
				Return HttpNotFound()
			End If

			Dim strTitle As String = ""
			strTitle = String.Format("休暇申請{0}の連絡", strShori)

			w0040.MAILSUBJECT = String.Format(strTitle & "【{0}】", s0010.APPNM)
			w0040.MAILBODYHEAD = "＜" & strTitle & "＞" & vbCrLf

			Dim m00101 As M0010 = db.M0010.Find(d0060.USERID)
			Dim strShinseisha As String = m00101.USERNM

			Dim sbMailBody As New StringBuilder()
			sbMailBody.AppendLine(strShinseisha & "さんの以下の休暇申請が " & Session("LoginUsernm") & "さんによって " & d0060.UPDTDT.ToString("yyyy年MM月dd日 HH：mm") & " に" & vbCrLf &
								   String.Format("{0}されました。", strShori))
			sbMailBody.AppendLine()

			Dim strStartDay As String = "(" & Date.Parse(d0060.KKNST).ToString("ddd") & ")"
			Dim strEndDay As String = "(" & Date.Parse(d0060.KKNED).ToString("ddd") & ")"

			sbMailBody.AppendLine("期間 ： " & d0060.KKNST & strStartDay & "～" & d0060.KKNED & strEndDay)
			If d0060.KYUKCD = "7" OrElse d0060.KYUKCD = "9" Then
				sbMailBody.AppendLine("時間 ： " & d0060.JKNST.ToString.Substring(0, 2) & ":" & d0060.JKNST.ToString.Substring(2, 2) & "～" & d0060.JKNED.ToString.Substring(0, 2) & ":" & d0060.JKNED.ToString.Substring(2, 2))
			End If
			sbMailBody.AppendLine("種類 ： " & d0060.M0060.KYUKNM)
			sbMailBody.AppendLine("備考 ： " & d0060.GYOMMEMO)

			'ASI[25 Dec 2019]: After adding KANRIMEMO on personal shift screen, also include it in Mail content
			sbMailBody.AppendLine("管理者メモ ： " & d0060.KANRIMEMO)

			Dim dicList As New Dictionary(Of Boolean, String)
			dicList.Add(False, "送信しない")
			dicList.Add(True, "送信する")

			Dim sbTntAnanm As New StringBuilder
			Dim i As Integer = 0
			Dim strKey As String = ""

			ViewBag.MAILSOUSIAN = New SelectList(dicList.Select(Function(f) New With {.Value = f.Key, .Text = f.Value}), "Value", "Text", True)

			sbTntAnanm.AppendLine(d0060.M0010.USERNM)

			Dim strTntAnanm As String = sbTntAnanm.ToString().Replace(vbCrLf, "，")
			strTntAnanm = strTntAnanm.Substring(0, strTntAnanm.Length - 1)

			Dim w0050 As New W0050

			Dim lstw0050 As New List(Of W0050)

			w0050.USERID = d0060.USERID
			w0050.M00101 = m00101
			'ViewBag.MAILSOUSIAN = New SelectList(dicList.Select(Function(f) New With {.Value = f.Key, .Text = f.Value}), "Value", "Text", True)

			strKey = String.Format("W0050[{0}].MAILSOUSIAN", 0)
			ViewData(strKey) = New SelectList(dicList.Select(Function(f) New With {.Value = f.Key, .Text = f.Value}), "Value", "Text", True)

			w0050.MAILSOUSIAN = True

			lstw0050.Add(w0050)
			w0040.W0050 = lstw0050

			w0040.MAILBODY = sbMailBody.ToString

			Dim strScheme As String = Request.Url.Scheme
			Dim strAuthority As String = Request.Url.Authority
			Dim strPath As String = HttpRuntime.AppDomainAppVirtualPath
			Dim strBaseUrl As String = String.Format("{0}://{1}{2}", strScheme, strAuthority, strPath)

			w0040.MAILAPPURL = String.Format(strBaseUrl & "/C0040/Index/{0}?stdt={1}", "xxx", Date.Parse(d0060.KKNST).ToString("yyyy/MM/dd"))

			Return View("SendMail", w0040)
		End Function


		' POST: D0010/SendMail/5
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function SendMail(<Bind(Include:="ACUSERID,SHORIKBN,GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,MAILSUBJECT,MAILBODYHEAD,MAILBODY,MAILNOTE,MAILAPPURL,UPDTDT,W0050")> ByVal w0040 As W0040, ByVal name As String, ByVal userid As String, ByVal showdate As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			Dim loginUserKanri As Boolean = Session("LoginUserKanri")
			Dim loginUserSystem As Boolean = Session("LoginUserSystem")

			ViewData!LoginUsernm = Session("LoginUsernm")

			Dim strBody As String = w0040.MAILBODYHEAD

			If String.IsNullOrEmpty(w0040.MAILNOTE) = False Then
				strBody = strBody & vbCrLf & "コメント ： " & w0040.MAILNOTE & vbCrLf
			End If

			strBody = strBody & vbCrLf & w0040.MAILBODY


			Dim bolUpdt As Boolean = False

			For Each item In w0040.W0050

				If item.MAILSOUSIAN = False Then
					Continue For
				End If

				Dim m0010 = db.M0010.Find(item.USERID)

				If m0010 IsNot Nothing Then

					Dim strUrl As String = "システムでシフト表を参照" & vbCrLf & w0040.MAILAPPURL.Replace("xxx", item.USERID)

					Dim strMyBody As String = strBody & vbCrLf & vbCrLf & vbCrLf & strUrl

					Dim lstToMailid As New List(Of String)
					If m0010.MAILLADDESS IsNot Nothing Then
						If String.IsNullOrEmpty(m0010.MAILLADDESS.Trim) = False Then
							lstToMailid.Add(m0010.MAILLADDESS)
						End If
					End If

					If m0010.KEITAIADDESS IsNot Nothing Then
						If String.IsNullOrEmpty(m0010.KEITAIADDESS.Trim) = False Then
							lstToMailid.Add(m0010.KEITAIADDESS)
						End If
					End If


					If lstToMailid.Count > 0 Then
						If DoSendMail(lstToMailid.ToArray, w0040.MAILSUBJECT, strMyBody) = False Then
							Return View(w0040)
						End If
					End If

				End If
			Next

			Return RedirectToAction("Index", New With {.name = name, .userid = userid, .showdate = showdate})


		End Function


		' GET: D0060/Edit/5
		<HttpPost()>
		Function UpdateD0060Kyaka(ByVal kyuid As Decimal, ByVal name As String, ByVal userid As String, ByVal showdate As String, ByVal kanrimemo As String) As JsonResult
			'If IsNothing(kyuid) Then
			'    Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			'End If
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return Json(New With {.Url = New UrlHelper(Request.RequestContext).Action("Index", "Login")})
			End If

			Dim d0060 As D0060 = db.D0060.Find(kyuid)

			'却下
			d0060.SHONINFLG = "2"
			'ASI[08 Jan 2020] : Update in D0060,even not executed and direct Approving
			d0060.KANRIMEMO = kanrimemo

			'If IsNothing(d0060) Then
			'    Return HttpNotFound()
			'End If

			Dim decNewHenkorrkcd As Decimal = GetMaxHenkorrkcd() + 1
			Dim dtSyuseiymd As Date = Now

			'変更履歴の作成
			Dim d0150 As New D0150
			d0150.HENKORRKCD = decNewHenkorrkcd
			d0150.HENKONAIYO = "却下"
			d0150.USERID = Session("LoginUserid")
			d0150.SYUSEIYMD = dtSyuseiymd
			d0150.KKNST = d0060.KKNST
			d0150.KKNED = d0060.KKNED
			d0150.JKNST = d0060.JKNST
			d0150.JKNED = d0060.JKNED
			d0150.SHINSEIUSER = d0060.M0010.USERNM
			d0150.KYUKNM = d0060.M0060.KYUKNM
			d0150.GYOMMEMO = d0060.GYOMMEMO
			db.D0150.Add(d0150)


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
					tran.Rollback()
					Throw ex
				End Try
			End Using


			ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0060.USERID)
			ViewBag.KYUKCD = New SelectList(db.M0060, "KYUKCD", "KYUKNM", d0060.KYUKCD)

			Dim intShorikbn As String = "2"
			Dim decKyukasnscd As Decimal = kyuid
			Dim redirectUrl = New UrlHelper(Request.RequestContext).Action("SendMail", "B0050", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn, .kyukasnscd = decKyukasnscd, .name = name, .userid = userid, .showdate = showdate})
			Return Json(New With {.Url = redirectUrl})
		End Function

		'ASI[18 Oct 2019]: Add below Action to check desk memo exist for given date/time range
		Function CheckDeskMemoExist(ByVal kknst As String, ByVal kkned As String, ByVal jknst As Decimal, ByVal jkned As Decimal, ByVal name As String, ByVal userid As String, ByVal showdate As String) As Boolean
			Dim DeskMemoExist As Boolean = False
			Dim JTJKNST As Date = GetJtjkn(kknst, jknst)
			Dim JTJKNED As Date = GetJtjkn(kkned, jkned)
			Dim cnt = (From d1 In db.D0120 Join d2 In db.D0130
								On d1.DESKNO Equals d2.DESKNO
					   Join d3 In db.D0110 On d2.DESKNO Equals d3.DESKNO
					   Where d2.USERID = userid And d3.KAKUNINID <> 5 And
									(((kknst >= d1.SHIFTYMDST And kknst <= d1.SHIFTYMDED Or
										kkned >= d1.SHIFTYMDST And kkned <= d1.SHIFTYMDED Or
										d1.SHIFTYMDST >= kknst And d1.SHIFTYMDST <= kkned Or
										d1.SHIFTYMDED >= kknst And d1.SHIFTYMDED <= kkned) And d1.KSKJKNST = Nothing) Or
							   (
								JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED Or
										JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED Or
										d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED Or
										d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED
								)) Select d1.DESKNO).Count

			If cnt > 0 Then
				DeskMemoExist = True
			End If

			'Return Json(New With {DeskMemoExist})
			Return DeskMemoExist
		End Function

		Public Sub ValidateTime(ByVal lstwd0040 As List(Of WD0040), ByVal strUpdateDate As String, ByVal userid As String, ByRef lstError As List(Of String), ByRef strErrorHI As String, ByRef strErrorID As String)

			Dim strSearchDate As String = ""
			Dim chkDuplicate As Boolean = False

			Dim lstD0040 As New List(Of D0040)
			For Each item In lstwd0040
				If item.KYUKCD = "7" Or item.KYUKCD = "9" Then
					If item.D0040 IsNot Nothing AndAlso item.D0040.Count > 0 Then
						For Each itemD0040 In item.D0040
							If itemD0040.JKNST IsNot Nothing Then
								lstD0040.Add(itemD0040)
							End If
						Next

					End If
				End If
			Next

			For Each itemd0040 In lstD0040
				If itemd0040.JKNST IsNot Nothing AndAlso itemd0040.JKNED IsNot Nothing Then
					strSearchDate = strUpdateDate & itemd0040.HI

					Dim strError As String = ""
					Dim strJikan As String = itemd0040.JKNST & "～" & itemd0040.JKNED
					If itemd0040.JKNST.PadLeft(5, "0") > "24:00" OrElse itemd0040.JKNED.PadLeft(5, "0") > "24:00" Then
						If itemd0040.KYUKCD = "7" Then
							strError = String.Format(" {0}日で時間休の時間が24時を超えています。", strSearchDate, strJikan)
						Else
							strError = String.Format(" {0}日で時間強休時間が24時を超えています。", strSearchDate, strJikan)
						End If
						lstError.Add(strError)
						If strErrorHI = "" Then
							strErrorHI = itemd0040.HI
							strErrorID = itemd0040.KYUKCD
						End If
					ElseIf itemd0040.JKNST.PadLeft(5, "0") > itemd0040.JKNED.PadLeft(5, "0") Then
						If itemd0040.KYUKCD = "7" Then
							strError = String.Format(" {0}日で時間休の時間-開始と終了の前後関係が誤っています。", strSearchDate, strJikan)
						Else
							strError = String.Format(" {0}日で時間強休の時間-開始と終了の前後関係が誤っています。", strSearchDate, strJikan)
						End If

						lstError.Add(strError)
						If strErrorHI = "" Then
							strErrorHI = itemd0040.HI
							strErrorID = itemd0040.KYUKCD
						End If
					Else
						strSearchDate = strUpdateDate & itemd0040.HI

						Dim db As New Model1

						Dim sqlpara1 As New SqlParameter("av_userid", SqlDbType.SmallInt)
						sqlpara1.Value = userid

						Dim strjtjikan As String = GetJtjkn(strSearchDate, itemd0040.JKNST)
						Dim strjijikend As String = GetJtjkn(strSearchDate, itemd0040.JKNED)

						Dim sqlpara2 As New SqlParameter("ld_jtjknst", SqlDbType.DateTime)
						sqlpara2.Value = DateTime.Parse(strjtjikan).ToString("yyyy/MM/dd HH:mm:ss")

						Dim sqlpara3 As New SqlParameter("ld_jtjkned", SqlDbType.DateTime)
						sqlpara3.Value = DateTime.Parse(strjijikend).ToString("yyyy/MM/dd HH:mm:ss")

						Dim sqlpara4 As New SqlParameter("ln_retval", SqlDbType.Int)
						sqlpara4.Direction = ParameterDirection.Output
						sqlpara4.Value = ""

						Dim cnt = db.Database.ExecuteSqlCommand("Exec TeLAS.pr_b0050_chkoverlaptime @av_userid, @ld_jtjknst, @ld_jtjkned,@ln_retval OUTPUT ",
							sqlpara1, sqlpara2, sqlpara3, sqlpara4)

						Dim intResult As Integer = sqlpara4.Value
						If intResult = "1" Then


							If itemd0040.KYUKCD = "7" Then
								strError = String.Format(" {0}日で時間休{1}がシフトの時間と重なっています。", strSearchDate, strJikan)
							Else
								strError = String.Format(" {0}日で時間強休{1}がシフトの時間と重なっています。", strSearchDate, strJikan)
							End If

							If strErrorHI = "" Then
								strErrorHI = itemd0040.HI
								strErrorID = itemd0040.KYUKCD
							End If

							lstError.Add(strError)
						End If
					End If

				End If

			Next

			If lstError.Count = 0 Then
				CheckDateDuplication(strUpdateDate, lstD0040, lstError, strErrorHI, strErrorID)
				chkDuplicate = True
			End If

		End Sub


		Public Shared Function CheckDateDuplication(ByVal strUpdateDate As String, ByVal lstD0040 As List(Of D0040), ByRef lstError As List(Of String), ByRef strErrorHI As String, ByRef strErrorID As String) As Boolean
			Dim bolRet As Boolean = True

			Dim lstChk As New List(Of D0040)
			Dim sorted = lstD0040.OrderBy(Function(f) f.HI).ThenBy(Function(f) f.JKNST.PadLeft(5, "0"))

			For Each item In sorted.ToList
				lstChk.Add(item)
			Next

			'Dim rowIndex As Integer = 0
			For intIdx As Integer = 0 To lstChk.Count - 2


				Dim bolSameKeyColumn As Boolean = True

				'If lstChk.Count <= (rowIndex + 1) Then
				'    Return False
				'End If

				If lstChk(intIdx).HI <> lstChk(intIdx + 1).HI Then
					bolSameKeyColumn = False
				End If

				'If lstChk.Count <= (rowIndex + 1) Then
				'    Return False
				'End If

				If bolSameKeyColumn = False Then
					Continue For
				End If

				If lstChk(intIdx).JKNST IsNot Nothing AndAlso lstChk(intIdx).JKNED IsNot Nothing Then
					Dim strSearchDate As String = strUpdateDate & lstChk(intIdx).HI
					Dim strjtjikan As String = GetJtjkn(strSearchDate, lstChk(intIdx + 1).JKNST)
					Dim strjijikend As String = GetJtjkn(strSearchDate, lstChk(intIdx).JKNED)

					Dim dtVALDTT As Date = Nothing
					Date.TryParse(strjijikend, dtVALDTT)

					Dim dtNextVALDTF As Date = Nothing
					Date.TryParse(strjtjikan, dtNextVALDTF)

					Dim strError As String = ""


					If dtVALDTT > dtNextVALDTF Then
						strError = String.Format(" {0}日で時間が重複しています。", strSearchDate)
						lstError.Add(strError)
						If strErrorHI = "" Then
							strErrorHI = lstChk(intIdx).HI
							strErrorID = lstChk(intIdx).KYUKCD
						End If

						bolRet = False
						'Exit For
					End If
				End If

			Next

			Return bolRet
		End Function



		Function DoSendMail(ByVal strToEmailIds As String(),
Optional strSubject As String = Nothing,
Optional strBody As String = Nothing,
Optional strCcEmailIds As String() = Nothing,
Optional strBccEmailIds As String() = Nothing,
Optional bolHtmlBody As Boolean = False,
Optional strAattchments As String() = Nothing,
Optional strSMTPServer As String = "",
Optional intPORT As Integer = 0) As Boolean

			Dim bolResult As Boolean = True

			Using smtpClient As New SmtpClient()

				Using mailMessage As MailMessage = New MailMessage
					Try
						With mailMessage
							.Subject = If(strSubject = "", "", strSubject)
							.Body = strBody
							.IsBodyHtml = bolHtmlBody

							'ＴＯメールIDを　MailMessageのオブジェクトに　追加する
							For Each emailToAdr As String In strToEmailIds
								If Not String.IsNullOrEmpty(emailToAdr) AndAlso emailToAdr.Trim.Length <> 0 Then
									.To.Add(emailToAdr)
								End If
							Next

							'CCメールIDを　MailMessageのオブジェクトに　追加する
							If (Not strCcEmailIds Is Nothing) AndAlso (strCcEmailIds.Length > 0) Then
								For Each emailCc As String In strCcEmailIds
									If Not String.IsNullOrEmpty(emailCc) AndAlso emailCc.Trim.Length <> 0 Then
										.CC.Add(emailCc)
									End If
								Next
							End If

							'BCCメールIDを　MailMessageのオブジェクトに　追加する
							If (Not strBccEmailIds Is Nothing) AndAlso (strBccEmailIds.Length > 0) Then
								For Each emailBcc As String In strBccEmailIds
									If Not String.IsNullOrEmpty(emailBcc) AndAlso emailBcc.Trim.Length <> 0 Then
										.Bcc.Add(emailBcc)
									End If
								Next
							End If

							'添付ファイルを　MailMessageのオブジェクトに　追加する
							If (Not strAattchments Is Nothing) AndAlso strAattchments.Length > 0 Then
								For Each FileAttachment As String In strAattchments
									If Not String.IsNullOrEmpty(FileAttachment) AndAlso FileAttachment.Trim.Length <> 0 Then
										Dim AttachMent As System.Net.Mail.Attachment = New System.Net.Mail.Attachment(FileAttachment)
										.Attachments.Add(AttachMent)
									End If
								Next
							End If
						End With

						'メールを送信
						smtpClient.Send(mailMessage)
						'Threading.Thread.Sleep(5000)
						bolResult = True

					Catch ex As FormatException
						bolResult = False
						Throw ex

					Catch ex As FileNotFoundException
						bolResult = False
						Throw ex

					Catch ex As SmtpException
						bolResult = False
						Throw ex

					Catch ex As InvalidOperationException
						bolResult = False
						Throw ex

					Finally
						mailMessage.Dispose()
						smtpClient.Dispose()
					End Try
				End Using
			End Using

			Return bolResult

		End Function

		<HttpPost()>
		Function UpdateD0060(<Bind(Include:="KYUKSNSCD,USERID,KYUKCD,KKNST,KKNED,JKNST,JKNED,GYOMMEMO,SHONINFLG,KANRIMEMO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal lstd0060 As List(Of D0060), ByVal name As String, ByVal userid As String, ByVal showdate As String) As ActionResult

			'休暇申請の備考の更新
			If lstd0060 IsNot Nothing Then
				For Each item In lstd0060

					Dim d0060updt As D0060 = db.D0060.Find(item.KYUKSNSCD)

					If d0060updt IsNot Nothing Then

						'ASI[25 Dec 2019]: After change to made GYOMMEMO only for display and add KANRIMEMO as a editable on screen.
						'So no need to update GYOMMEMO, now update KANRIMEMO in db tabl
						'd0060updt.GYOMMEMO = item.GYOMMEMO
						d0060updt.KANRIMEMO = item.KANRIMEMO

					End If

				Next
			End If


			db.SaveChanges()

			Return RedirectToAction("Index", New With {.name = name, .userid = userid, .showdate = showdate})

		End Function
	End Class

End Namespace
