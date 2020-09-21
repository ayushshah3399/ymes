Imports System.Net
Imports System.Web.Mvc
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel.DataAnnotations

Namespace Controllers
	Public Class A0230Controller
		Inherits Controller

		Private db As New Model1
		Public Const LINK_COLOR1 As String = "#1e90ff"
		Public Const LINK_COLOR2 As String = "black"

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

		' GET: M0130
		Function Index(ByVal SearchType As String, ByVal Searchdt As String, ByVal CatCodes As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			'If CheckAccessLvl() = False Then
			'	Return View("ErrorAccesslvl")
			'End If

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.AbsoluteUri      '休日設定画面から来た時アナ名が文字化けするので、Encodeされている元のUrlを取得
				If Not strUrlReferrer.Contains("/A0230") AndAlso Not strUrlReferrer.Contains("/A0220/Edit") Then
					Session("UrlReferrer") = strUrlReferrer
				End If
			End If

			Dim loginUserKanri As Boolean = Session("LoginUserKanri")
			Dim loginUserSystem As Boolean = Session("LoginUserSystem")

			If (loginUserKanri OrElse loginUserSystem) Then
				ViewData("Kanri") = "1"
			Else
				ViewData("Kanri") = "0"
			End If

			ViewBag.LoginUserSystem = loginUserSystem
			ViewBag.LoginUserKanri = loginUserKanri
			ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

			'一括業務メニューを表示可能な条件追加
			Dim intUserid As Integer = Integer.Parse(loginUserId)
			Dim m0010KOKYU = db.M0010.Find(intUserid)
			ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
			ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL


			Dim m0140List = Nothing 'List contains sportsubcat data
			Dim m0130List = Nothing 'List contains sportsubcat data
			Dim m0130RecList = Nothing 'List contains sportcat data

			If String.IsNullOrEmpty(CatCodes) AndAlso (Session("LoginUserACCESSLVLCD") = "4" OrElse Session("LoginUserACCESSLVLCD") = "3") Then
				Dim m0160 = (From m1 In db.M0160 Where m1.USERID = loginUserId Select m1.SPORTCATCD).ToList

				For i = 0 To m0160.Count - 1
					CatCodes = CatCodes & m0160(i)
					If i < m0160.Count - 1 Then
						CatCodes = CatCodes & ","
					End If
				Next
				'm0130RecList = (From m1 In db.M0130
				'				Where m1.HYOJ = True
				'				Select m1).OrderBy(Function(f) f.HYOJJN).ThenBy(Function(f) f.SPORTCATCD).ToList
				'Session("m0130List") = m0130RecList
			End If

			'When screen loads intially all data comes without filter
			If String.IsNullOrEmpty(CatCodes) Then
				m0140List = (From m1 In db.M0140
							 Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
							 Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
							 Where m1.HYOJ = True AndAlso m3.HYOJ = True
							 Order By m3.HYOJJN, m3.SPORTCATCD, m1.HYOJJN, m1.SPORTSUBCATCD
							 Select m1).ToList()

				m0130List = (From m1 In db.M0140
							 Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
							 Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
							 Where m1.HYOJ = True AndAlso m3.HYOJ = True
							 Order By m3.HYOJJN, m3.SPORTCATCD, m1.HYOJJN, m1.SPORTSUBCATCD
							 Select m3).ToList()

				m0130RecList = (From m1 In db.M0130
								Where m1.HYOJ = True
								Select m1).OrderBy(Function(f) f.HYOJJN).ThenBy(Function(f) f.SPORTCATCD).ToList

				'Send list to view for M0130 data
				ViewBag.sportCatList = m0130RecList

				'Preserve the M0130 data so that checkbox state can be known
				'Session("m0130List") = m0130RecList

				'Send all sportcatcode list to create checkbox in initial sate
				ViewBag.SportCatCheckedList = m0130RecList

				'When some checkbox is checked or unchecked and search then this code execute
			Else

				'Get the list of sportcatcd according to checked checkbox
				Dim CatCodesList As String() = CatCodes.Split(",")

				'Get Filter data of M0130 and M0140
				m0140List = (From m1 In db.M0140
							 Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
							 Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
							 Where CatCodesList.Contains(m3.SPORTCATCD) AndAlso m1.HYOJ = True AndAlso m3.HYOJ = True
							 Order By m3.HYOJJN, m3.SPORTCATCD, m1.HYOJJN, m1.SPORTSUBCATCD
							 Select m1).ToList

				m0130List = (From m1 In db.M0140
							 Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
							 Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
							 Where CatCodesList.Contains(m3.SPORTCATCD) AndAlso m1.HYOJ = True AndAlso m3.HYOJ = True
							 Order By m3.HYOJJN, m3.SPORTCATCD, m1.HYOJJN, m1.SPORTSUBCATCD
							 Select m3).ToList().ToList

				m0130RecList = (From m1 In db.M0130
								Where CatCodesList.Contains(m1.SPORTCATCD) AndAlso m1.HYOJ = True
								Select m1).OrderBy(Function(f) f.HYOJJN).ThenBy(Function(f) f.SPORTCATCD).ToList

				ViewBag.sportCatList = m0130RecList

				Dim m0130Items As List(Of M0130) = (From m1 In db.M0130
													Where m1.HYOJ = True
													Select m1).OrderBy(Function(f) f.HYOJJN).ThenBy(Function(f) f.SPORTCATCD).ToList

				'Maintain checkbox state i.e. wil it be checked or uncecked as sate before search
				For i = 0 To m0130Items.Count - 1

					If Not CatCodesList.Contains(m0130Items(i).SPORTCATCD) Then
						m0130Items(i).CHECKEDSTATUS = False 'To show checkbox unchecked
					Else
						m0130Items(i).CHECKEDSTATUS = True 'To show checkbox checked
					End If

				Next

				'Send M0130 filter record to view
				ViewBag.SportCatCheckedList = m0130Items

			End If

			'Get Display name of M0150 field from Model
			Dim m0150 As New M0150
			Dim bangumiHyoj1NmDsiplayName As String = GetDisplayName(GetType(M0150), "BANGUMIHYOJNM1")
			Dim ksjnkHyoj1NmDsiplayName As String = GetDisplayName(GetType(M0150), "KSKJKNHYOJNM1")
			Dim oajnkHyoj1NmDsiplayName As String = GetDisplayName(GetType(M0150), "OAJKNHYOJNM1")
			Dim saiknHyoj1NmDsiplayName As String = GetDisplayName(GetType(M0150), "SAIKNHYOJNM1")
			Dim bashyoHyoj1NmDsiplayName As String = GetDisplayName(GetType(M0150), "BASYOHYOJNM1")
			Dim bikoHyoj1NmDsiplayName As String = GetDisplayName(GetType(M0150), "BIKOHYOJNM1")
			Dim bangumiHyojNm2DsiplayName As String = GetDisplayName(GetType(M0150), "BANGUMIHYOJNM2")
			Dim ksjnkHyojNM2DsiplayName As String = GetDisplayName(GetType(M0150), "KSKJKNHYOJNM2")
			Dim oajnkHyojNm2DsiplayName As String = GetDisplayName(GetType(M0150), "OAJKNHYOJNM2")
			Dim saiknHyojNm2DsiplayName As String = GetDisplayName(GetType(M0150), "SAIKNHYOJNM2")
			Dim bashyoHyojNM2DsiplayName As String = GetDisplayName(GetType(M0150), "BASYOHYOJNM2")
			Dim bikoHyojNm2DsiplayName As String = GetDisplayName(GetType(M0150), "BIKOHYOJNM2")
			Dim col01DsiplayName As String = GetDisplayName(GetType(M0150), "COL01")
			Dim col02DsiplayName As String = GetDisplayName(GetType(M0150), "COL02")
			Dim col03DsiplayName As String = GetDisplayName(GetType(M0150), "COL03")
			Dim col04DsiplayName As String = GetDisplayName(GetType(M0150), "COL04")
			Dim col05DsiplayName As String = GetDisplayName(GetType(M0150), "COL05")
			Dim col06DsiplayName As String = GetDisplayName(GetType(M0150), "COL06")
			Dim col07DsiplayName As String = GetDisplayName(GetType(M0150), "COL07")
			Dim col08DsiplayName As String = GetDisplayName(GetType(M0150), "COL08")
			Dim col09DsiplayName As String = GetDisplayName(GetType(M0150), "COL09")
			Dim col10DsiplayName As String = GetDisplayName(GetType(M0150), "COL10")
			Dim col11DsiplayName As String = GetDisplayName(GetType(M0150), "COL11")
			Dim col12DsiplayName As String = GetDisplayName(GetType(M0150), "COL12")
			Dim col13DsiplayName As String = GetDisplayName(GetType(M0150), "COL13")
			Dim col14DsiplayName As String = GetDisplayName(GetType(M0150), "COL14")
			Dim col15DsiplayName As String = GetDisplayName(GetType(M0150), "COL15")
			Dim col16DsiplayName As String = GetDisplayName(GetType(M0150), "COL16")
			Dim col17DsiplayName As String = GetDisplayName(GetType(M0150), "COL17")
			Dim col18DsiplayName As String = GetDisplayName(GetType(M0150), "COL18")
			Dim col19DsiplayName As String = GetDisplayName(GetType(M0150), "COL19")
			Dim col20DsiplayName As String = GetDisplayName(GetType(M0150), "COL20")
			Dim col21DsiplayName As String = GetDisplayName(GetType(M0150), "COL21")
			Dim col22DsiplayName As String = GetDisplayName(GetType(M0150), "COL22")
			Dim col23DsiplayName As String = GetDisplayName(GetType(M0150), "COL23")
			Dim col24DsiplayName As String = GetDisplayName(GetType(M0150), "COL24")
			Dim col25DsiplayName As String = GetDisplayName(GetType(M0150), "COL25")

			'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
			Dim COL26DsiplayName As String = GetDisplayName(GetType(M0150), "COL26")
			Dim COL27DsiplayName As String = GetDisplayName(GetType(M0150), "COL27")
			Dim COL28DsiplayName As String = GetDisplayName(GetType(M0150), "COL28")
			Dim COL29DsiplayName As String = GetDisplayName(GetType(M0150), "COL29")
			Dim COL30DsiplayName As String = GetDisplayName(GetType(M0150), "COL30")
			Dim COL31DsiplayName As String = GetDisplayName(GetType(M0150), "COL31")
			Dim COL32DsiplayName As String = GetDisplayName(GetType(M0150), "COL32")
			Dim COL33DsiplayName As String = GetDisplayName(GetType(M0150), "COL33")
			Dim COL34DsiplayName As String = GetDisplayName(GetType(M0150), "COL34")
			Dim COL35DsiplayName As String = GetDisplayName(GetType(M0150), "COL35")
			Dim COL36DsiplayName As String = GetDisplayName(GetType(M0150), "COL36")
			Dim COL37DsiplayName As String = GetDisplayName(GetType(M0150), "COL37")
			Dim COL38DsiplayName As String = GetDisplayName(GetType(M0150), "COL38")
			Dim COL39DsiplayName As String = GetDisplayName(GetType(M0150), "COL39")
			Dim COL40DsiplayName As String = GetDisplayName(GetType(M0150), "COL40")
			Dim COL41DsiplayName As String = GetDisplayName(GetType(M0150), "COL41")
			Dim COL42DsiplayName As String = GetDisplayName(GetType(M0150), "COL42")
			Dim COL43DsiplayName As String = GetDisplayName(GetType(M0150), "COL43")
			Dim COL44DsiplayName As String = GetDisplayName(GetType(M0150), "COL44")
			Dim COL45DsiplayName As String = GetDisplayName(GetType(M0150), "COL45")
			Dim COL46DsiplayName As String = GetDisplayName(GetType(M0150), "COL46")
			Dim COL47DsiplayName As String = GetDisplayName(GetType(M0150), "COL47")
			Dim COL48DsiplayName As String = GetDisplayName(GetType(M0150), "COL48")
			Dim COL49DsiplayName As String = GetDisplayName(GetType(M0150), "COL49")
			Dim COL50DsiplayName As String = GetDisplayName(GetType(M0150), "COL50")

			'Loop through subcat list
			For i As Integer = 0 To m0140List.Count - 1

				'get corresponding sportcat in M0150 model
				m0140List(i).SPORTCATCD = m0130List(i).SPORTCATCD

				Dim SportSubCatCD As Short = m0140List(i).SPORTSUBCATCD

				'Dim lstSportCatDetailsNames = Nothing

				'When Option is searchAll the data
				'Create a list of M0150 names that will be diaplay for each subcat in view
				'This list contains all ana names whether it's HYOJ is checked or unchecked
				'If SearchType Is Nothing OrElse SearchType = "0" Then
				Dim hyojjnShort As Short = Short.Parse("999")

				Dim hyojjnShort011 As Short = Short.Parse("9944")
				Dim hyojjnShort022 As Short = Short.Parse("9945")
				Dim hyojjnShort033 As Short = Short.Parse("9946")
				Dim hyojjnShort044 As Short = Short.Parse("9947")
				Dim hyojjnShort055 As Short = Short.Parse("9948")
				Dim hyojjnShort066 As Short = Short.Parse("9949")

				Dim hyojjnShort01 As Short = Short.Parse("9950")
				Dim hyojjnShort02 As Short = Short.Parse("9951")
				Dim hyojjnShort03 As Short = Short.Parse("9952")
				Dim hyojjnShort04 As Short = Short.Parse("9953")
				Dim hyojjnShort05 As Short = Short.Parse("9954")
				Dim hyojjnShort06 As Short = Short.Parse("9955")
				Dim hyojjnShort07 As Short = Short.Parse("9956")
				Dim hyojjnShort08 As Short = Short.Parse("9957")
				Dim hyojjnShort09 As Short = Short.Parse("9958")
				Dim hyojjnShort10 As Short = Short.Parse("9959")
				Dim hyojjnShort11 As Short = Short.Parse("9960")
				Dim hyojjnShort12 As Short = Short.Parse("9961")
				Dim hyojjnShort13 As Short = Short.Parse("9962")
				Dim hyojjnShort14 As Short = Short.Parse("9963")
				Dim hyojjnShort15 As Short = Short.Parse("9964")
				Dim hyojjnShort16 As Short = Short.Parse("9965")
				Dim hyojjnShort17 As Short = Short.Parse("9966")
				Dim hyojjnShort18 As Short = Short.Parse("9967")
				Dim hyojjnShort19 As Short = Short.Parse("9968")
				Dim hyojjnShort20 As Short = Short.Parse("9969")
				Dim hyojjnShort21 As Short = Short.Parse("9970")
				Dim hyojjnShort22 As Short = Short.Parse("9971")
				Dim hyojjnShort23 As Short = Short.Parse("9972")
				Dim hyojjnShort24 As Short = Short.Parse("9973")
				Dim hyojjnShort25 As Short = Short.Parse("9974")

				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				Dim hyojjnShort26 As Short = Short.Parse("9975")
				Dim hyojjnShort27 As Short = Short.Parse("9976")
				Dim hyojjnShort28 As Short = Short.Parse("9977")
				Dim hyojjnShort29 As Short = Short.Parse("9978")
				Dim hyojjnShort30 As Short = Short.Parse("9979")
				Dim hyojjnShort31 As Short = Short.Parse("9980")
				Dim hyojjnShort32 As Short = Short.Parse("9981")
				Dim hyojjnShort33 As Short = Short.Parse("9982")
				Dim hyojjnShort34 As Short = Short.Parse("9983")
				Dim hyojjnShort35 As Short = Short.Parse("9984")
				Dim hyojjnShort36 As Short = Short.Parse("9985")
				Dim hyojjnShort37 As Short = Short.Parse("9986")
				Dim hyojjnShort38 As Short = Short.Parse("9987")
				Dim hyojjnShort39 As Short = Short.Parse("9988")
				Dim hyojjnShort40 As Short = Short.Parse("9989")
				Dim hyojjnShort41 As Short = Short.Parse("9990")
				Dim hyojjnShort42 As Short = Short.Parse("9991")
				Dim hyojjnShort43 As Short = Short.Parse("9992")
				Dim hyojjnShort44 As Short = Short.Parse("9993")
				Dim hyojjnShort45 As Short = Short.Parse("9994")
				Dim hyojjnShort46 As Short = Short.Parse("9995")
				Dim hyojjnShort47 As Short = Short.Parse("9996")
				Dim hyojjnShort48 As Short = Short.Parse("9997")
				Dim hyojjnShort49 As Short = Short.Parse("9998")
				Dim hyojjnShort50 As Short = Short.Parse("9999")

				If Session("LoginUserACCESSLVLCD") = "4" Then

					Dim lstSportCatDetailsNames = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BANGUMIHYOJJN2 Is Nothing, hyojjnShort011, m1.BANGUMIHYOJJN2), .COLNAME = "BANGUMIHYOJNM1", .COLVALUE = If(m1.BANGUMIHYOJNM2 IsNot Nothing, m1.BANGUMIHYOJNM2, bangumiHyojNm2DsiplayName), .HYOJ = (m1.BANGUMIHYOJ2 <> "0")}).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.KSKJKNHYOJJN2 Is Nothing, hyojjnShort022, m1.KSKJKNHYOJJN2), .COLNAME = "KSKJKNHYOJNM1", .COLVALUE = If(m1.KSKJKNHYOJNM2 IsNot Nothing, m1.KSKJKNHYOJNM2, ksjnkHyojNM2DsiplayName), .HYOJ = (m1.KSKJKNHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.OAJKNHYOJJN2 Is Nothing, hyojjnShort033, m1.OAJKNHYOJJN2), .COLNAME = "OAJKNHYOJNM1", .COLVALUE = If(m1.OAJKNHYOJNM2 IsNot Nothing, m1.OAJKNHYOJNM2, oajnkHyojNm2DsiplayName), .HYOJ = (m1.OAJKNHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.SAIKNHYOJJN2 Is Nothing, hyojjnShort044, m1.SAIKNHYOJJN2), .COLNAME = "SAIKNHYOJNM1", .COLVALUE = If(m1.SAIKNHYOJNM2 IsNot Nothing, m1.SAIKNHYOJNM2, saiknHyojNm2DsiplayName), .HYOJ = (m1.SAIKNHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BASYOHYOJJN2 Is Nothing, hyojjnShort055, m1.BASYOHYOJJN2), .COLNAME = "BASYOHYOJNM1", .COLVALUE = If(m1.BASYOHYOJNM2 IsNot Nothing, m1.BASYOHYOJNM2, bashyoHyojNM2DsiplayName), .HYOJ = (m1.BASYOHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BIKOHYOJJN2 Is Nothing, hyojjnShort066, m1.BIKOHYOJJN2), .COLNAME = "BIKOHYOJNM1", .COLVALUE = If(m1.BIKOHYOJNM2 IsNot Nothing, m1.BIKOHYOJNM2, bikoHyojNm2DsiplayName), .HYOJ = (m1.BIKOHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL01_TYPE = 1) Or (m1.COL01_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL01_TYPE, .HYOJJN = If(m1.COL01_HYOJJN2 Is Nothing, hyojjnShort01, m1.COL01_HYOJJN2), .COLNAME = "COL01", .COLVALUE = If(m1.COL01_HYOJNM2 IsNot Nothing, m1.COL01_HYOJNM2, If(m1.COL01 IsNot Nothing, m1.COL01, col01DsiplayName)), .HYOJ = m1.COL01_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL02_TYPE = 1) Or (m1.COL02_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL02_TYPE, .HYOJJN = If(m1.COL02_HYOJJN2 Is Nothing, hyojjnShort02, m1.COL02_HYOJJN2), .COLNAME = "COL02", .COLVALUE = If(m1.COL02_HYOJNM2 IsNot Nothing, m1.COL02_HYOJNM2, If(m1.COL02 IsNot Nothing, m1.COL02, col02DsiplayName)), .HYOJ = m1.COL02_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL03_TYPE = 1) Or (m1.COL03_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL03_TYPE, .HYOJJN = If(m1.COL03_HYOJJN2 Is Nothing, hyojjnShort03, m1.COL03_HYOJJN2), .COLNAME = "COL03", .COLVALUE = If(m1.COL03_HYOJNM2 IsNot Nothing, m1.COL03_HYOJNM2, If(m1.COL03 IsNot Nothing, m1.COL03, col03DsiplayName)), .HYOJ = m1.COL03_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL04_TYPE = 1) Or (m1.COL04_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL04_TYPE, .HYOJJN = If(m1.COL04_HYOJJN2 Is Nothing, hyojjnShort04, m1.COL04_HYOJJN2), .COLNAME = "COL04", .COLVALUE = If(m1.COL04_HYOJNM2 IsNot Nothing, m1.COL04_HYOJNM2, If(m1.COL04 IsNot Nothing, m1.COL04, col04DsiplayName)), .HYOJ = m1.COL04_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL05_TYPE = 1) Or (m1.COL05_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL05_TYPE, .HYOJJN = If(m1.COL05_HYOJJN2 Is Nothing, hyojjnShort05, m1.COL05_HYOJJN2), .COLNAME = "COL05", .COLVALUE = If(m1.COL05_HYOJNM2 IsNot Nothing, m1.COL05_HYOJNM2, If(m1.COL05 IsNot Nothing, m1.COL05, col05DsiplayName)), .HYOJ = m1.COL05_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL06_TYPE = 1) Or (m1.COL06_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL06_TYPE, .HYOJJN = If(m1.COL06_HYOJJN2 Is Nothing, hyojjnShort06, m1.COL06_HYOJJN2), .COLNAME = "COL06", .COLVALUE = If(m1.COL06_HYOJNM2 IsNot Nothing, m1.COL06_HYOJNM2, If(m1.COL06 IsNot Nothing, m1.COL06, col06DsiplayName)), .HYOJ = m1.COL06_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL07_TYPE = 1) Or (m1.COL07_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL07_TYPE, .HYOJJN = If(m1.COL07_HYOJJN2 Is Nothing, hyojjnShort07, m1.COL07_HYOJJN2), .COLNAME = "COL07", .COLVALUE = If(m1.COL07_HYOJNM2 IsNot Nothing, m1.COL07_HYOJNM2, If(m1.COL07 IsNot Nothing, m1.COL07, col07DsiplayName)), .HYOJ = m1.COL07_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL08_TYPE = 1) Or (m1.COL08_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL08_TYPE, .HYOJJN = If(m1.COL08_HYOJJN2 Is Nothing, hyojjnShort08, m1.COL08_HYOJJN2), .COLNAME = "COL08", .COLVALUE = If(m1.COL08_HYOJNM2 IsNot Nothing, m1.COL08_HYOJNM2, If(m1.COL08 IsNot Nothing, m1.COL08, col08DsiplayName)), .HYOJ = m1.COL08_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL09_TYPE = 1) Or (m1.COL09_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL09_TYPE, .HYOJJN = If(m1.COL09_HYOJJN2 Is Nothing, hyojjnShort09, m1.COL09_HYOJJN2), .COLNAME = "COL09", .COLVALUE = If(m1.COL09_HYOJNM2 IsNot Nothing, m1.COL09_HYOJNM2, If(m1.COL09 IsNot Nothing, m1.COL09, col09DsiplayName)), .HYOJ = m1.COL09_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL10_TYPE = 1) Or (m1.COL10_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL10_TYPE, .HYOJJN = If(m1.COL10_HYOJJN2 Is Nothing, hyojjnShort10, m1.COL10_HYOJJN2), .COLNAME = "COL10", .COLVALUE = If(m1.COL10_HYOJNM2 IsNot Nothing, m1.COL10_HYOJNM2, If(m1.COL10 IsNot Nothing, m1.COL10, col10DsiplayName)), .HYOJ = m1.COL10_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL11_TYPE = 1) Or (m1.COL11_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL11_TYPE, .HYOJJN = If(m1.COL11_HYOJJN2 Is Nothing, hyojjnShort11, m1.COL11_HYOJJN2), .COLNAME = "COL11", .COLVALUE = If(m1.COL11_HYOJNM2 IsNot Nothing, m1.COL11_HYOJNM2, If(m1.COL11 IsNot Nothing, m1.COL11, col11DsiplayName)), .HYOJ = m1.COL11_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL12_TYPE = 1) Or (m1.COL12_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL12_TYPE, .HYOJJN = If(m1.COL12_HYOJJN2 Is Nothing, hyojjnShort12, m1.COL12_HYOJJN2), .COLNAME = "COL12", .COLVALUE = If(m1.COL12_HYOJNM2 IsNot Nothing, m1.COL12_HYOJNM2, If(m1.COL12 IsNot Nothing, m1.COL12, col12DsiplayName)), .HYOJ = m1.COL12_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL13_TYPE = 1) Or (m1.COL13_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL13_TYPE, .HYOJJN = If(m1.COL13_HYOJJN2 Is Nothing, hyojjnShort13, m1.COL13_HYOJJN2), .COLNAME = "COL13", .COLVALUE = If(m1.COL13_HYOJNM2 IsNot Nothing, m1.COL13_HYOJNM2, If(m1.COL13 IsNot Nothing, m1.COL13, col13DsiplayName)), .HYOJ = m1.COL13_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL14_TYPE = 1) Or (m1.COL14_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL14_TYPE, .HYOJJN = If(m1.COL14_HYOJJN2 Is Nothing, hyojjnShort14, m1.COL14_HYOJJN2), .COLNAME = "COL14", .COLVALUE = If(m1.COL14_HYOJNM2 IsNot Nothing, m1.COL14_HYOJNM2, If(m1.COL14 IsNot Nothing, m1.COL14, col14DsiplayName)), .HYOJ = m1.COL14_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL15_TYPE = 1) Or (m1.COL15_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL15_TYPE, .HYOJJN = If(m1.COL15_HYOJJN2 Is Nothing, hyojjnShort15, m1.COL15_HYOJJN2), .COLNAME = "COL15", .COLVALUE = If(m1.COL15_HYOJNM2 IsNot Nothing, m1.COL15_HYOJNM2, If(m1.COL15 IsNot Nothing, m1.COL15, col15DsiplayName)), .HYOJ = m1.COL15_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL16_TYPE = 1) Or (m1.COL16_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL16_TYPE, .HYOJJN = If(m1.COL16_HYOJJN2 Is Nothing, hyojjnShort16, m1.COL16_HYOJJN2), .COLNAME = "COL16", .COLVALUE = If(m1.COL16_HYOJNM2 IsNot Nothing, m1.COL16_HYOJNM2, If(m1.COL16 IsNot Nothing, m1.COL16, col16DsiplayName)), .HYOJ = m1.COL16_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL17_TYPE = 1) Or (m1.COL17_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL17_TYPE, .HYOJJN = If(m1.COL17_HYOJJN2 Is Nothing, hyojjnShort17, m1.COL17_HYOJJN2), .COLNAME = "COL17", .COLVALUE = If(m1.COL17_HYOJNM2 IsNot Nothing, m1.COL17_HYOJNM2, If(m1.COL17 IsNot Nothing, m1.COL17, col17DsiplayName)), .HYOJ = m1.COL17_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL18_TYPE = 1) Or (m1.COL18_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL18_TYPE, .HYOJJN = If(m1.COL18_HYOJJN2 Is Nothing, hyojjnShort18, m1.COL18_HYOJJN2), .COLNAME = "COL18", .COLVALUE = If(m1.COL18_HYOJNM2 IsNot Nothing, m1.COL18_HYOJNM2, If(m1.COL18 IsNot Nothing, m1.COL18, col18DsiplayName)), .HYOJ = m1.COL18_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL19_TYPE = 1) Or (m1.COL19_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL19_TYPE, .HYOJJN = If(m1.COL19_HYOJJN2 Is Nothing, hyojjnShort19, m1.COL19_HYOJJN2), .COLNAME = "COL19", .COLVALUE = If(m1.COL19_HYOJNM2 IsNot Nothing, m1.COL19_HYOJNM2, If(m1.COL19 IsNot Nothing, m1.COL19, col19DsiplayName)), .HYOJ = m1.COL19_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL20_TYPE = 1) Or (m1.COL20_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL20_TYPE, .HYOJJN = If(m1.COL20_HYOJJN2 Is Nothing, hyojjnShort20, m1.COL20_HYOJJN2), .COLNAME = "COL20", .COLVALUE = If(m1.COL20_HYOJNM2 IsNot Nothing, m1.COL20_HYOJNM2, If(m1.COL20 IsNot Nothing, m1.COL20, col20DsiplayName)), .HYOJ = m1.COL20_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL21_TYPE = 1) Or (m1.COL21_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL21_TYPE, .HYOJJN = If(m1.COL21_HYOJJN2 Is Nothing, hyojjnShort21, m1.COL21_HYOJJN2), .COLNAME = "COL21", .COLVALUE = If(m1.COL21_HYOJNM2 IsNot Nothing, m1.COL21_HYOJNM2, If(m1.COL21 IsNot Nothing, m1.COL21, col21DsiplayName)), .HYOJ = m1.COL21_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL22_TYPE = 1) Or (m1.COL22_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL22_TYPE, .HYOJJN = If(m1.COL22_HYOJJN2 Is Nothing, hyojjnShort22, m1.COL22_HYOJJN2), .COLNAME = "COL22", .COLVALUE = If(m1.COL22_HYOJNM2 IsNot Nothing, m1.COL22_HYOJNM2, If(m1.COL22 IsNot Nothing, m1.COL22, col22DsiplayName)), .HYOJ = m1.COL22_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL23_TYPE = 1) Or (m1.COL23_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL23_TYPE, .HYOJJN = If(m1.COL23_HYOJJN2 Is Nothing, hyojjnShort23, m1.COL23_HYOJJN2), .COLNAME = "COL23", .COLVALUE = If(m1.COL23_HYOJNM2 IsNot Nothing, m1.COL23_HYOJNM2, If(m1.COL23 IsNot Nothing, m1.COL23, col23DsiplayName)), .HYOJ = m1.COL23_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL24_TYPE = 1) Or (m1.COL24_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL24_TYPE, .HYOJJN = If(m1.COL24_HYOJJN2 Is Nothing, hyojjnShort24, m1.COL24_HYOJJN2), .COLNAME = "COL24", .COLVALUE = If(m1.COL24_HYOJNM2 IsNot Nothing, m1.COL24_HYOJNM2, If(m1.COL24 IsNot Nothing, m1.COL24, col24DsiplayName)), .HYOJ = m1.COL24_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL25_TYPE = 1) Or (m1.COL25_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL25_TYPE, .HYOJJN = If(m1.COL25_HYOJJN2 Is Nothing, hyojjnShort25, m1.COL25_HYOJJN2), .COLNAME = "COL25", .COLVALUE = If(m1.COL25_HYOJNM2 IsNot Nothing, m1.COL25_HYOJNM2, If(m1.COL25 IsNot Nothing, m1.COL25, col25DsiplayName)), .HYOJ = m1.COL25_HYOJ2})).OrderBy(Function(m1) m1.HYOJJN).ToList()

					'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
					Dim lstSportCatDetailsNames2 = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL26_TYPE = 1) Or (m1.COL26_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL26_TYPE, .HYOJJN = If(m1.COL26_HYOJJN2 Is Nothing, hyojjnShort26, m1.COL26_HYOJJN2), .COLNAME = "COL26", .COLVALUE = If(m1.COL26_HYOJNM2 IsNot Nothing, m1.COL26_HYOJNM2, If(m1.COL26 IsNot Nothing, m1.COL26, COL26DsiplayName)), .HYOJ = m1.COL26_HYOJ2}).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL27_TYPE = 1) Or (m1.COL27_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL27_TYPE, .HYOJJN = If(m1.COL27_HYOJJN2 Is Nothing, hyojjnShort27, m1.COL27_HYOJJN2), .COLNAME = "COL27", .COLVALUE = If(m1.COL27_HYOJNM2 IsNot Nothing, m1.COL27_HYOJNM2, If(m1.COL27 IsNot Nothing, m1.COL27, COL27DsiplayName)), .HYOJ = m1.COL27_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL28_TYPE = 1) Or (m1.COL28_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL28_TYPE, .HYOJJN = If(m1.COL28_HYOJJN2 Is Nothing, hyojjnShort28, m1.COL28_HYOJJN2), .COLNAME = "COL28", .COLVALUE = If(m1.COL28_HYOJNM2 IsNot Nothing, m1.COL28_HYOJNM2, If(m1.COL28 IsNot Nothing, m1.COL28, COL28DsiplayName)), .HYOJ = m1.COL28_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL29_TYPE = 1) Or (m1.COL29_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL29_TYPE, .HYOJJN = If(m1.COL29_HYOJJN2 Is Nothing, hyojjnShort29, m1.COL29_HYOJJN2), .COLNAME = "COL29", .COLVALUE = If(m1.COL29_HYOJNM2 IsNot Nothing, m1.COL29_HYOJNM2, If(m1.COL29 IsNot Nothing, m1.COL29, COL29DsiplayName)), .HYOJ = m1.COL29_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL30_TYPE = 1) Or (m1.COL30_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL30_TYPE, .HYOJJN = If(m1.COL30_HYOJJN2 Is Nothing, hyojjnShort30, m1.COL30_HYOJJN2), .COLNAME = "COL30", .COLVALUE = If(m1.COL30_HYOJNM2 IsNot Nothing, m1.COL30_HYOJNM2, If(m1.COL30 IsNot Nothing, m1.COL30, COL30DsiplayName)), .HYOJ = m1.COL30_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL31_TYPE = 1) Or (m1.COL31_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL31_TYPE, .HYOJJN = If(m1.COL31_HYOJJN2 Is Nothing, hyojjnShort31, m1.COL31_HYOJJN2), .COLNAME = "COL31", .COLVALUE = If(m1.COL31_HYOJNM2 IsNot Nothing, m1.COL31_HYOJNM2, If(m1.COL31 IsNot Nothing, m1.COL31, COL31DsiplayName)), .HYOJ = m1.COL31_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL32_TYPE = 1) Or (m1.COL32_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL32_TYPE, .HYOJJN = If(m1.COL32_HYOJJN2 Is Nothing, hyojjnShort32, m1.COL32_HYOJJN2), .COLNAME = "COL32", .COLVALUE = If(m1.COL32_HYOJNM2 IsNot Nothing, m1.COL32_HYOJNM2, If(m1.COL32 IsNot Nothing, m1.COL32, COL32DsiplayName)), .HYOJ = m1.COL32_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL33_TYPE = 1) Or (m1.COL33_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL33_TYPE, .HYOJJN = If(m1.COL33_HYOJJN2 Is Nothing, hyojjnShort33, m1.COL33_HYOJJN2), .COLNAME = "COL33", .COLVALUE = If(m1.COL33_HYOJNM2 IsNot Nothing, m1.COL33_HYOJNM2, If(m1.COL33 IsNot Nothing, m1.COL33, COL33DsiplayName)), .HYOJ = m1.COL33_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL34_TYPE = 1) Or (m1.COL34_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL34_TYPE, .HYOJJN = If(m1.COL34_HYOJJN2 Is Nothing, hyojjnShort34, m1.COL34_HYOJJN2), .COLNAME = "COL34", .COLVALUE = If(m1.COL34_HYOJNM2 IsNot Nothing, m1.COL34_HYOJNM2, If(m1.COL34 IsNot Nothing, m1.COL34, COL34DsiplayName)), .HYOJ = m1.COL34_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL35_TYPE = 1) Or (m1.COL35_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL35_TYPE, .HYOJJN = If(m1.COL35_HYOJJN2 Is Nothing, hyojjnShort35, m1.COL35_HYOJJN2), .COLNAME = "COL35", .COLVALUE = If(m1.COL35_HYOJNM2 IsNot Nothing, m1.COL35_HYOJNM2, If(m1.COL35 IsNot Nothing, m1.COL35, COL35DsiplayName)), .HYOJ = m1.COL35_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL36_TYPE = 1) Or (m1.COL36_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL36_TYPE, .HYOJJN = If(m1.COL36_HYOJJN2 Is Nothing, hyojjnShort36, m1.COL36_HYOJJN2), .COLNAME = "COL36", .COLVALUE = If(m1.COL36_HYOJNM2 IsNot Nothing, m1.COL36_HYOJNM2, If(m1.COL36 IsNot Nothing, m1.COL36, COL36DsiplayName)), .HYOJ = m1.COL36_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL37_TYPE = 1) Or (m1.COL37_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL37_TYPE, .HYOJJN = If(m1.COL37_HYOJJN2 Is Nothing, hyojjnShort37, m1.COL37_HYOJJN2), .COLNAME = "COL37", .COLVALUE = If(m1.COL37_HYOJNM2 IsNot Nothing, m1.COL37_HYOJNM2, If(m1.COL37 IsNot Nothing, m1.COL37, COL37DsiplayName)), .HYOJ = m1.COL37_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL38_TYPE = 1) Or (m1.COL38_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL38_TYPE, .HYOJJN = If(m1.COL38_HYOJJN2 Is Nothing, hyojjnShort38, m1.COL38_HYOJJN2), .COLNAME = "COL38", .COLVALUE = If(m1.COL38_HYOJNM2 IsNot Nothing, m1.COL38_HYOJNM2, If(m1.COL38 IsNot Nothing, m1.COL38, COL38DsiplayName)), .HYOJ = m1.COL38_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL39_TYPE = 1) Or (m1.COL39_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL39_TYPE, .HYOJJN = If(m1.COL39_HYOJJN2 Is Nothing, hyojjnShort39, m1.COL39_HYOJJN2), .COLNAME = "COL39", .COLVALUE = If(m1.COL39_HYOJNM2 IsNot Nothing, m1.COL39_HYOJNM2, If(m1.COL39 IsNot Nothing, m1.COL39, COL39DsiplayName)), .HYOJ = m1.COL39_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL40_TYPE = 1) Or (m1.COL40_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL40_TYPE, .HYOJJN = If(m1.COL40_HYOJJN2 Is Nothing, hyojjnShort40, m1.COL40_HYOJJN2), .COLNAME = "COL40", .COLVALUE = If(m1.COL40_HYOJNM2 IsNot Nothing, m1.COL40_HYOJNM2, If(m1.COL40 IsNot Nothing, m1.COL40, COL40DsiplayName)), .HYOJ = m1.COL40_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL41_TYPE = 1) Or (m1.COL41_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL41_TYPE, .HYOJJN = If(m1.COL41_HYOJJN2 Is Nothing, hyojjnShort41, m1.COL41_HYOJJN2), .COLNAME = "COL41", .COLVALUE = If(m1.COL41_HYOJNM2 IsNot Nothing, m1.COL41_HYOJNM2, If(m1.COL41 IsNot Nothing, m1.COL41, COL41DsiplayName)), .HYOJ = m1.COL41_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL42_TYPE = 1) Or (m1.COL42_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL42_TYPE, .HYOJJN = If(m1.COL42_HYOJJN2 Is Nothing, hyojjnShort42, m1.COL42_HYOJJN2), .COLNAME = "COL42", .COLVALUE = If(m1.COL42_HYOJNM2 IsNot Nothing, m1.COL42_HYOJNM2, If(m1.COL42 IsNot Nothing, m1.COL42, COL42DsiplayName)), .HYOJ = m1.COL42_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL43_TYPE = 1) Or (m1.COL43_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL43_TYPE, .HYOJJN = If(m1.COL43_HYOJJN2 Is Nothing, hyojjnShort43, m1.COL43_HYOJJN2), .COLNAME = "COL43", .COLVALUE = If(m1.COL43_HYOJNM2 IsNot Nothing, m1.COL43_HYOJNM2, If(m1.COL43 IsNot Nothing, m1.COL43, COL43DsiplayName)), .HYOJ = m1.COL43_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL44_TYPE = 1) Or (m1.COL44_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL44_TYPE, .HYOJJN = If(m1.COL44_HYOJJN2 Is Nothing, hyojjnShort44, m1.COL44_HYOJJN2), .COLNAME = "COL44", .COLVALUE = If(m1.COL44_HYOJNM2 IsNot Nothing, m1.COL44_HYOJNM2, If(m1.COL44 IsNot Nothing, m1.COL44, COL44DsiplayName)), .HYOJ = m1.COL44_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL45_TYPE = 1) Or (m1.COL45_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL45_TYPE, .HYOJJN = If(m1.COL45_HYOJJN2 Is Nothing, hyojjnShort45, m1.COL45_HYOJJN2), .COLNAME = "COL45", .COLVALUE = If(m1.COL45_HYOJNM2 IsNot Nothing, m1.COL45_HYOJNM2, If(m1.COL45 IsNot Nothing, m1.COL45, COL45DsiplayName)), .HYOJ = m1.COL45_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL46_TYPE = 1) Or (m1.COL46_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL46_TYPE, .HYOJJN = If(m1.COL46_HYOJJN2 Is Nothing, hyojjnShort46, m1.COL46_HYOJJN2), .COLNAME = "COL46", .COLVALUE = If(m1.COL46_HYOJNM2 IsNot Nothing, m1.COL46_HYOJNM2, If(m1.COL46 IsNot Nothing, m1.COL46, COL46DsiplayName)), .HYOJ = m1.COL46_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL47_TYPE = 1) Or (m1.COL47_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL47_TYPE, .HYOJJN = If(m1.COL47_HYOJJN2 Is Nothing, hyojjnShort47, m1.COL47_HYOJJN2), .COLNAME = "COL47", .COLVALUE = If(m1.COL47_HYOJNM2 IsNot Nothing, m1.COL47_HYOJNM2, If(m1.COL47 IsNot Nothing, m1.COL47, COL47DsiplayName)), .HYOJ = m1.COL47_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL48_TYPE = 1) Or (m1.COL48_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL48_TYPE, .HYOJJN = If(m1.COL48_HYOJJN2 Is Nothing, hyojjnShort48, m1.COL48_HYOJJN2), .COLNAME = "COL48", .COLVALUE = If(m1.COL48_HYOJNM2 IsNot Nothing, m1.COL48_HYOJNM2, If(m1.COL48 IsNot Nothing, m1.COL48, COL48DsiplayName)), .HYOJ = m1.COL48_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL49_TYPE = 1) Or (m1.COL49_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL49_TYPE, .HYOJJN = If(m1.COL49_HYOJJN2 Is Nothing, hyojjnShort49, m1.COL49_HYOJJN2), .COLNAME = "COL49", .COLVALUE = If(m1.COL49_HYOJNM2 IsNot Nothing, m1.COL49_HYOJNM2, If(m1.COL49 IsNot Nothing, m1.COL49, COL49DsiplayName)), .HYOJ = m1.COL49_HYOJ2})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL50_TYPE = 1) Or (m1.COL50_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL50_TYPE, .HYOJJN = If(m1.COL50_HYOJJN2 Is Nothing, hyojjnShort50, m1.COL50_HYOJJN2), .COLNAME = "COL50", .COLVALUE = If(m1.COL50_HYOJNM2 IsNot Nothing, m1.COL50_HYOJNM2, If(m1.COL50 IsNot Nothing, m1.COL50, COL50DsiplayName)), .HYOJ = m1.COL50_HYOJ2})).ToList()

					For Each item In lstSportCatDetailsNames2
						lstSportCatDetailsNames.Add(item)
					Next

					m0140List(i).M0150LIST = lstSportCatDetailsNames.OrderBy(Function(m1) m1.HYOJJN).ToList()

				Else

					Dim lstSportCatDetailsNames = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BANGUMIHYOJJN1 Is Nothing, hyojjnShort011, m1.BANGUMIHYOJJN1), .COLNAME = "BANGUMIHYOJNM1", .COLVALUE = If(m1.BANGUMIHYOJNM1 IsNot Nothing, m1.BANGUMIHYOJNM1, bangumiHyoj1NmDsiplayName), .HYOJ = (m1.BANGUMIHYOJ1 <> "0")}).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.KSKJKNHYOJJN1 Is Nothing, hyojjnShort022, m1.KSKJKNHYOJJN1), .COLNAME = "KSKJKNHYOJNM1", .COLVALUE = If(m1.KSKJKNHYOJNM1 IsNot Nothing, m1.KSKJKNHYOJNM1, ksjnkHyoj1NmDsiplayName), .HYOJ = (m1.KSKJKNHYOJ1 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.OAJKNHYOJJN1 Is Nothing, hyojjnShort033, m1.OAJKNHYOJJN1), .COLNAME = "OAJKNHYOJNM1", .COLVALUE = If(m1.OAJKNHYOJNM1 IsNot Nothing, m1.OAJKNHYOJNM1, oajnkHyoj1NmDsiplayName), .HYOJ = (m1.OAJKNHYOJ1 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.SAIKNHYOJJN1 Is Nothing, hyojjnShort044, m1.SAIKNHYOJJN1), .COLNAME = "SAIKNHYOJNM1", .COLVALUE = If(m1.SAIKNHYOJNM1 IsNot Nothing, m1.SAIKNHYOJNM1, saiknHyoj1NmDsiplayName), .HYOJ = (m1.SAIKNHYOJ1 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BASYOHYOJJN1 Is Nothing, hyojjnShort055, m1.BASYOHYOJJN1), .COLNAME = "BASYOHYOJNM1", .COLVALUE = If(m1.BASYOHYOJNM1 IsNot Nothing, m1.BASYOHYOJNM1, bashyoHyoj1NmDsiplayName), .HYOJ = (m1.BASYOHYOJ1 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BIKOHYOJJN1 Is Nothing, hyojjnShort066, m1.BIKOHYOJJN1), .COLNAME = "BIKOHYOJNM1", .COLVALUE = If(m1.BIKOHYOJNM1 IsNot Nothing, m1.BIKOHYOJNM1, bikoHyoj1NmDsiplayName), .HYOJ = (m1.BIKOHYOJ1 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL01_TYPE = 1) Or (m1.COL01_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL01_TYPE, .HYOJJN = If(m1.COL01_HYOJJN1 Is Nothing, hyojjnShort01, m1.COL01_HYOJJN1), .COLNAME = "COL01", .COLVALUE = If(m1.COL01_HYOJNM1 IsNot Nothing, m1.COL01_HYOJNM1, If(m1.COL01 IsNot Nothing, m1.COL01, col01DsiplayName)), .HYOJ = m1.COL01_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL02_TYPE = 1) Or (m1.COL02_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL02_TYPE, .HYOJJN = If(m1.COL02_HYOJJN1 Is Nothing, hyojjnShort02, m1.COL02_HYOJJN1), .COLNAME = "COL02", .COLVALUE = If(m1.COL02_HYOJNM1 IsNot Nothing, m1.COL02_HYOJNM1, If(m1.COL02 IsNot Nothing, m1.COL02, col02DsiplayName)), .HYOJ = m1.COL02_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL03_TYPE = 1) Or (m1.COL03_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL03_TYPE, .HYOJJN = If(m1.COL03_HYOJJN1 Is Nothing, hyojjnShort03, m1.COL03_HYOJJN1), .COLNAME = "COL03", .COLVALUE = If(m1.COL03_HYOJNM1 IsNot Nothing, m1.COL03_HYOJNM1, If(m1.COL03 IsNot Nothing, m1.COL03, col03DsiplayName)), .HYOJ = m1.COL03_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL04_TYPE = 1) Or (m1.COL04_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL04_TYPE, .HYOJJN = If(m1.COL04_HYOJJN1 Is Nothing, hyojjnShort04, m1.COL04_HYOJJN1), .COLNAME = "COL04", .COLVALUE = If(m1.COL04_HYOJNM1 IsNot Nothing, m1.COL04_HYOJNM1, If(m1.COL04 IsNot Nothing, m1.COL04, col04DsiplayName)), .HYOJ = m1.COL04_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL05_TYPE = 1) Or (m1.COL05_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL05_TYPE, .HYOJJN = If(m1.COL05_HYOJJN1 Is Nothing, hyojjnShort05, m1.COL05_HYOJJN1), .COLNAME = "COL05", .COLVALUE = If(m1.COL05_HYOJNM1 IsNot Nothing, m1.COL05_HYOJNM1, If(m1.COL05 IsNot Nothing, m1.COL05, col05DsiplayName)), .HYOJ = m1.COL05_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL06_TYPE = 1) Or (m1.COL06_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL06_TYPE, .HYOJJN = If(m1.COL06_HYOJJN1 Is Nothing, hyojjnShort06, m1.COL06_HYOJJN1), .COLNAME = "COL06", .COLVALUE = If(m1.COL06_HYOJNM1 IsNot Nothing, m1.COL06_HYOJNM1, If(m1.COL06 IsNot Nothing, m1.COL06, col06DsiplayName)), .HYOJ = m1.COL06_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL07_TYPE = 1) Or (m1.COL07_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL07_TYPE, .HYOJJN = If(m1.COL07_HYOJJN1 Is Nothing, hyojjnShort07, m1.COL07_HYOJJN1), .COLNAME = "COL07", .COLVALUE = If(m1.COL07_HYOJNM1 IsNot Nothing, m1.COL07_HYOJNM1, If(m1.COL07 IsNot Nothing, m1.COL07, col07DsiplayName)), .HYOJ = m1.COL07_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL08_TYPE = 1) Or (m1.COL08_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL08_TYPE, .HYOJJN = If(m1.COL08_HYOJJN1 Is Nothing, hyojjnShort08, m1.COL08_HYOJJN1), .COLNAME = "COL08", .COLVALUE = If(m1.COL08_HYOJNM1 IsNot Nothing, m1.COL08_HYOJNM1, If(m1.COL08 IsNot Nothing, m1.COL08, col08DsiplayName)), .HYOJ = m1.COL08_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL09_TYPE = 1) Or (m1.COL09_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL09_TYPE, .HYOJJN = If(m1.COL09_HYOJJN1 Is Nothing, hyojjnShort09, m1.COL09_HYOJJN1), .COLNAME = "COL09", .COLVALUE = If(m1.COL09_HYOJNM1 IsNot Nothing, m1.COL09_HYOJNM1, If(m1.COL09 IsNot Nothing, m1.COL09, col09DsiplayName)), .HYOJ = m1.COL09_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL10_TYPE = 1) Or (m1.COL10_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL10_TYPE, .HYOJJN = If(m1.COL10_HYOJJN1 Is Nothing, hyojjnShort10, m1.COL10_HYOJJN1), .COLNAME = "COL10", .COLVALUE = If(m1.COL10_HYOJNM1 IsNot Nothing, m1.COL10_HYOJNM1, If(m1.COL10 IsNot Nothing, m1.COL10, col10DsiplayName)), .HYOJ = m1.COL10_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL11_TYPE = 1) Or (m1.COL11_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL11_TYPE, .HYOJJN = If(m1.COL11_HYOJJN1 Is Nothing, hyojjnShort11, m1.COL11_HYOJJN1), .COLNAME = "COL11", .COLVALUE = If(m1.COL11_HYOJNM1 IsNot Nothing, m1.COL11_HYOJNM1, If(m1.COL11 IsNot Nothing, m1.COL11, col11DsiplayName)), .HYOJ = m1.COL11_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL12_TYPE = 1) Or (m1.COL12_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL12_TYPE, .HYOJJN = If(m1.COL12_HYOJJN1 Is Nothing, hyojjnShort12, m1.COL12_HYOJJN1), .COLNAME = "COL12", .COLVALUE = If(m1.COL12_HYOJNM1 IsNot Nothing, m1.COL12_HYOJNM1, If(m1.COL12 IsNot Nothing, m1.COL12, col12DsiplayName)), .HYOJ = m1.COL12_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL13_TYPE = 1) Or (m1.COL13_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL13_TYPE, .HYOJJN = If(m1.COL13_HYOJJN1 Is Nothing, hyojjnShort13, m1.COL13_HYOJJN1), .COLNAME = "COL13", .COLVALUE = If(m1.COL13_HYOJNM1 IsNot Nothing, m1.COL13_HYOJNM1, If(m1.COL13 IsNot Nothing, m1.COL13, col13DsiplayName)), .HYOJ = m1.COL13_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL14_TYPE = 1) Or (m1.COL14_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL14_TYPE, .HYOJJN = If(m1.COL14_HYOJJN1 Is Nothing, hyojjnShort14, m1.COL14_HYOJJN1), .COLNAME = "COL14", .COLVALUE = If(m1.COL14_HYOJNM1 IsNot Nothing, m1.COL14_HYOJNM1, If(m1.COL14 IsNot Nothing, m1.COL14, col14DsiplayName)), .HYOJ = m1.COL14_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL15_TYPE = 1) Or (m1.COL15_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL15_TYPE, .HYOJJN = If(m1.COL15_HYOJJN1 Is Nothing, hyojjnShort15, m1.COL15_HYOJJN1), .COLNAME = "COL15", .COLVALUE = If(m1.COL15_HYOJNM1 IsNot Nothing, m1.COL15_HYOJNM1, If(m1.COL15 IsNot Nothing, m1.COL15, col15DsiplayName)), .HYOJ = m1.COL15_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL16_TYPE = 1) Or (m1.COL16_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL16_TYPE, .HYOJJN = If(m1.COL16_HYOJJN1 Is Nothing, hyojjnShort16, m1.COL16_HYOJJN1), .COLNAME = "COL16", .COLVALUE = If(m1.COL16_HYOJNM1 IsNot Nothing, m1.COL16_HYOJNM1, If(m1.COL16 IsNot Nothing, m1.COL16, col16DsiplayName)), .HYOJ = m1.COL16_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL17_TYPE = 1) Or (m1.COL17_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL17_TYPE, .HYOJJN = If(m1.COL17_HYOJJN1 Is Nothing, hyojjnShort17, m1.COL17_HYOJJN1), .COLNAME = "COL17", .COLVALUE = If(m1.COL17_HYOJNM1 IsNot Nothing, m1.COL17_HYOJNM1, If(m1.COL17 IsNot Nothing, m1.COL17, col17DsiplayName)), .HYOJ = m1.COL17_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL18_TYPE = 1) Or (m1.COL18_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL18_TYPE, .HYOJJN = If(m1.COL18_HYOJJN1 Is Nothing, hyojjnShort18, m1.COL18_HYOJJN1), .COLNAME = "COL18", .COLVALUE = If(m1.COL18_HYOJNM1 IsNot Nothing, m1.COL18_HYOJNM1, If(m1.COL18 IsNot Nothing, m1.COL18, col18DsiplayName)), .HYOJ = m1.COL18_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL19_TYPE = 1) Or (m1.COL19_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL19_TYPE, .HYOJJN = If(m1.COL19_HYOJJN1 Is Nothing, hyojjnShort19, m1.COL19_HYOJJN1), .COLNAME = "COL19", .COLVALUE = If(m1.COL19_HYOJNM1 IsNot Nothing, m1.COL19_HYOJNM1, If(m1.COL19 IsNot Nothing, m1.COL19, col19DsiplayName)), .HYOJ = m1.COL19_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL20_TYPE = 1) Or (m1.COL20_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL20_TYPE, .HYOJJN = If(m1.COL20_HYOJJN1 Is Nothing, hyojjnShort20, m1.COL20_HYOJJN1), .COLNAME = "COL20", .COLVALUE = If(m1.COL20_HYOJNM1 IsNot Nothing, m1.COL20_HYOJNM1, If(m1.COL20 IsNot Nothing, m1.COL20, col20DsiplayName)), .HYOJ = m1.COL20_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL21_TYPE = 1) Or (m1.COL21_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL21_TYPE, .HYOJJN = If(m1.COL21_HYOJJN1 Is Nothing, hyojjnShort21, m1.COL21_HYOJJN1), .COLNAME = "COL21", .COLVALUE = If(m1.COL21_HYOJNM1 IsNot Nothing, m1.COL21_HYOJNM1, If(m1.COL21 IsNot Nothing, m1.COL21, col21DsiplayName)), .HYOJ = m1.COL21_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL22_TYPE = 1) Or (m1.COL22_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL22_TYPE, .HYOJJN = If(m1.COL22_HYOJJN1 Is Nothing, hyojjnShort22, m1.COL22_HYOJJN1), .COLNAME = "COL22", .COLVALUE = If(m1.COL22_HYOJNM1 IsNot Nothing, m1.COL22_HYOJNM1, If(m1.COL22 IsNot Nothing, m1.COL22, col22DsiplayName)), .HYOJ = m1.COL22_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL23_TYPE = 1) Or (m1.COL23_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL23_TYPE, .HYOJJN = If(m1.COL23_HYOJJN1 Is Nothing, hyojjnShort23, m1.COL23_HYOJJN1), .COLNAME = "COL23", .COLVALUE = If(m1.COL23_HYOJNM1 IsNot Nothing, m1.COL23_HYOJNM1, If(m1.COL23 IsNot Nothing, m1.COL23, col23DsiplayName)), .HYOJ = m1.COL23_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL24_TYPE = 1) Or (m1.COL24_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL24_TYPE, .HYOJJN = If(m1.COL24_HYOJJN1 Is Nothing, hyojjnShort24, m1.COL24_HYOJJN1), .COLNAME = "COL24", .COLVALUE = If(m1.COL24_HYOJNM1 IsNot Nothing, m1.COL24_HYOJNM1, If(m1.COL24 IsNot Nothing, m1.COL24, col24DsiplayName)), .HYOJ = m1.COL24_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL25_TYPE = 1) Or (m1.COL25_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL25_TYPE, .HYOJJN = If(m1.COL25_HYOJJN1 Is Nothing, hyojjnShort25, m1.COL25_HYOJJN1), .COLNAME = "COL25", .COLVALUE = If(m1.COL25_HYOJNM1 IsNot Nothing, m1.COL25_HYOJNM1, If(m1.COL25 IsNot Nothing, m1.COL25, col25DsiplayName)), .HYOJ = m1.COL25_HYOJ1})).ToList()

					'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
					Dim lstSportCatDetailsNames2 = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL26_TYPE = 1) Or (m1.COL26_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL26_TYPE, .HYOJJN = If(m1.COL26_HYOJJN1 Is Nothing, hyojjnShort26, m1.COL26_HYOJJN1), .COLNAME = "COL26", .COLVALUE = If(m1.COL26_HYOJNM1 IsNot Nothing, m1.COL26_HYOJNM1, If(m1.COL26 IsNot Nothing, m1.COL26, COL26DsiplayName)), .HYOJ = m1.COL26_HYOJ1}).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL27_TYPE = 1) Or (m1.COL27_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL27_TYPE, .HYOJJN = If(m1.COL27_HYOJJN1 Is Nothing, hyojjnShort27, m1.COL27_HYOJJN1), .COLNAME = "COL27", .COLVALUE = If(m1.COL27_HYOJNM1 IsNot Nothing, m1.COL27_HYOJNM1, If(m1.COL27 IsNot Nothing, m1.COL27, COL27DsiplayName)), .HYOJ = m1.COL27_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL28_TYPE = 1) Or (m1.COL28_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL28_TYPE, .HYOJJN = If(m1.COL28_HYOJJN1 Is Nothing, hyojjnShort28, m1.COL28_HYOJJN1), .COLNAME = "COL28", .COLVALUE = If(m1.COL28_HYOJNM1 IsNot Nothing, m1.COL28_HYOJNM1, If(m1.COL28 IsNot Nothing, m1.COL28, COL28DsiplayName)), .HYOJ = m1.COL28_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL29_TYPE = 1) Or (m1.COL29_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL29_TYPE, .HYOJJN = If(m1.COL29_HYOJJN1 Is Nothing, hyojjnShort29, m1.COL29_HYOJJN1), .COLNAME = "COL29", .COLVALUE = If(m1.COL29_HYOJNM1 IsNot Nothing, m1.COL29_HYOJNM1, If(m1.COL29 IsNot Nothing, m1.COL29, COL29DsiplayName)), .HYOJ = m1.COL29_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL30_TYPE = 1) Or (m1.COL30_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL30_TYPE, .HYOJJN = If(m1.COL30_HYOJJN1 Is Nothing, hyojjnShort30, m1.COL30_HYOJJN1), .COLNAME = "COL30", .COLVALUE = If(m1.COL30_HYOJNM1 IsNot Nothing, m1.COL30_HYOJNM1, If(m1.COL30 IsNot Nothing, m1.COL30, COL30DsiplayName)), .HYOJ = m1.COL30_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL31_TYPE = 1) Or (m1.COL31_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL31_TYPE, .HYOJJN = If(m1.COL31_HYOJJN1 Is Nothing, hyojjnShort31, m1.COL31_HYOJJN1), .COLNAME = "COL31", .COLVALUE = If(m1.COL31_HYOJNM1 IsNot Nothing, m1.COL31_HYOJNM1, If(m1.COL31 IsNot Nothing, m1.COL31, COL31DsiplayName)), .HYOJ = m1.COL31_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL32_TYPE = 1) Or (m1.COL32_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL32_TYPE, .HYOJJN = If(m1.COL32_HYOJJN1 Is Nothing, hyojjnShort32, m1.COL32_HYOJJN1), .COLNAME = "COL32", .COLVALUE = If(m1.COL32_HYOJNM1 IsNot Nothing, m1.COL32_HYOJNM1, If(m1.COL32 IsNot Nothing, m1.COL32, COL32DsiplayName)), .HYOJ = m1.COL32_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL33_TYPE = 1) Or (m1.COL33_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL33_TYPE, .HYOJJN = If(m1.COL33_HYOJJN1 Is Nothing, hyojjnShort33, m1.COL33_HYOJJN1), .COLNAME = "COL33", .COLVALUE = If(m1.COL33_HYOJNM1 IsNot Nothing, m1.COL33_HYOJNM1, If(m1.COL33 IsNot Nothing, m1.COL33, COL33DsiplayName)), .HYOJ = m1.COL33_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL34_TYPE = 1) Or (m1.COL34_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL34_TYPE, .HYOJJN = If(m1.COL34_HYOJJN1 Is Nothing, hyojjnShort34, m1.COL34_HYOJJN1), .COLNAME = "COL34", .COLVALUE = If(m1.COL34_HYOJNM1 IsNot Nothing, m1.COL34_HYOJNM1, If(m1.COL34 IsNot Nothing, m1.COL34, COL34DsiplayName)), .HYOJ = m1.COL34_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL35_TYPE = 1) Or (m1.COL35_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL35_TYPE, .HYOJJN = If(m1.COL35_HYOJJN1 Is Nothing, hyojjnShort35, m1.COL35_HYOJJN1), .COLNAME = "COL35", .COLVALUE = If(m1.COL35_HYOJNM1 IsNot Nothing, m1.COL35_HYOJNM1, If(m1.COL35 IsNot Nothing, m1.COL35, COL35DsiplayName)), .HYOJ = m1.COL35_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL36_TYPE = 1) Or (m1.COL36_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL36_TYPE, .HYOJJN = If(m1.COL36_HYOJJN1 Is Nothing, hyojjnShort36, m1.COL36_HYOJJN1), .COLNAME = "COL36", .COLVALUE = If(m1.COL36_HYOJNM1 IsNot Nothing, m1.COL36_HYOJNM1, If(m1.COL36 IsNot Nothing, m1.COL36, COL36DsiplayName)), .HYOJ = m1.COL36_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL37_TYPE = 1) Or (m1.COL37_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL37_TYPE, .HYOJJN = If(m1.COL37_HYOJJN1 Is Nothing, hyojjnShort37, m1.COL37_HYOJJN1), .COLNAME = "COL37", .COLVALUE = If(m1.COL37_HYOJNM1 IsNot Nothing, m1.COL37_HYOJNM1, If(m1.COL37 IsNot Nothing, m1.COL37, COL37DsiplayName)), .HYOJ = m1.COL37_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL38_TYPE = 1) Or (m1.COL38_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL38_TYPE, .HYOJJN = If(m1.COL38_HYOJJN1 Is Nothing, hyojjnShort38, m1.COL38_HYOJJN1), .COLNAME = "COL38", .COLVALUE = If(m1.COL38_HYOJNM1 IsNot Nothing, m1.COL38_HYOJNM1, If(m1.COL38 IsNot Nothing, m1.COL38, COL38DsiplayName)), .HYOJ = m1.COL38_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL39_TYPE = 1) Or (m1.COL39_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL39_TYPE, .HYOJJN = If(m1.COL39_HYOJJN1 Is Nothing, hyojjnShort39, m1.COL39_HYOJJN1), .COLNAME = "COL39", .COLVALUE = If(m1.COL39_HYOJNM1 IsNot Nothing, m1.COL39_HYOJNM1, If(m1.COL39 IsNot Nothing, m1.COL39, COL39DsiplayName)), .HYOJ = m1.COL39_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL40_TYPE = 1) Or (m1.COL40_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL40_TYPE, .HYOJJN = If(m1.COL40_HYOJJN1 Is Nothing, hyojjnShort40, m1.COL40_HYOJJN1), .COLNAME = "COL40", .COLVALUE = If(m1.COL40_HYOJNM1 IsNot Nothing, m1.COL40_HYOJNM1, If(m1.COL40 IsNot Nothing, m1.COL40, COL40DsiplayName)), .HYOJ = m1.COL40_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL41_TYPE = 1) Or (m1.COL41_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL41_TYPE, .HYOJJN = If(m1.COL41_HYOJJN1 Is Nothing, hyojjnShort41, m1.COL41_HYOJJN1), .COLNAME = "COL41", .COLVALUE = If(m1.COL41_HYOJNM1 IsNot Nothing, m1.COL41_HYOJNM1, If(m1.COL41 IsNot Nothing, m1.COL41, COL41DsiplayName)), .HYOJ = m1.COL41_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL42_TYPE = 1) Or (m1.COL42_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL42_TYPE, .HYOJJN = If(m1.COL42_HYOJJN1 Is Nothing, hyojjnShort42, m1.COL42_HYOJJN1), .COLNAME = "COL42", .COLVALUE = If(m1.COL42_HYOJNM1 IsNot Nothing, m1.COL42_HYOJNM1, If(m1.COL42 IsNot Nothing, m1.COL42, COL42DsiplayName)), .HYOJ = m1.COL42_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL43_TYPE = 1) Or (m1.COL43_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL43_TYPE, .HYOJJN = If(m1.COL43_HYOJJN1 Is Nothing, hyojjnShort43, m1.COL43_HYOJJN1), .COLNAME = "COL43", .COLVALUE = If(m1.COL43_HYOJNM1 IsNot Nothing, m1.COL43_HYOJNM1, If(m1.COL43 IsNot Nothing, m1.COL43, COL43DsiplayName)), .HYOJ = m1.COL43_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL44_TYPE = 1) Or (m1.COL44_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL44_TYPE, .HYOJJN = If(m1.COL44_HYOJJN1 Is Nothing, hyojjnShort44, m1.COL44_HYOJJN1), .COLNAME = "COL44", .COLVALUE = If(m1.COL44_HYOJNM1 IsNot Nothing, m1.COL44_HYOJNM1, If(m1.COL44 IsNot Nothing, m1.COL44, COL44DsiplayName)), .HYOJ = m1.COL44_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL45_TYPE = 1) Or (m1.COL45_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL45_TYPE, .HYOJJN = If(m1.COL45_HYOJJN1 Is Nothing, hyojjnShort45, m1.COL45_HYOJJN1), .COLNAME = "COL45", .COLVALUE = If(m1.COL45_HYOJNM1 IsNot Nothing, m1.COL45_HYOJNM1, If(m1.COL45 IsNot Nothing, m1.COL45, COL45DsiplayName)), .HYOJ = m1.COL45_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL46_TYPE = 1) Or (m1.COL46_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL46_TYPE, .HYOJJN = If(m1.COL46_HYOJJN1 Is Nothing, hyojjnShort46, m1.COL46_HYOJJN1), .COLNAME = "COL46", .COLVALUE = If(m1.COL46_HYOJNM1 IsNot Nothing, m1.COL46_HYOJNM1, If(m1.COL46 IsNot Nothing, m1.COL46, COL46DsiplayName)), .HYOJ = m1.COL46_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL47_TYPE = 1) Or (m1.COL47_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL47_TYPE, .HYOJJN = If(m1.COL47_HYOJJN1 Is Nothing, hyojjnShort47, m1.COL47_HYOJJN1), .COLNAME = "COL47", .COLVALUE = If(m1.COL47_HYOJNM1 IsNot Nothing, m1.COL47_HYOJNM1, If(m1.COL47 IsNot Nothing, m1.COL47, COL47DsiplayName)), .HYOJ = m1.COL47_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL48_TYPE = 1) Or (m1.COL48_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL48_TYPE, .HYOJJN = If(m1.COL48_HYOJJN1 Is Nothing, hyojjnShort48, m1.COL48_HYOJJN1), .COLNAME = "COL48", .COLVALUE = If(m1.COL48_HYOJNM1 IsNot Nothing, m1.COL48_HYOJNM1, If(m1.COL48 IsNot Nothing, m1.COL48, COL48DsiplayName)), .HYOJ = m1.COL48_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL49_TYPE = 1) Or (m1.COL49_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL49_TYPE, .HYOJJN = If(m1.COL49_HYOJJN1 Is Nothing, hyojjnShort49, m1.COL49_HYOJJN1), .COLNAME = "COL49", .COLVALUE = If(m1.COL49_HYOJNM1 IsNot Nothing, m1.COL49_HYOJNM1, If(m1.COL49 IsNot Nothing, m1.COL49, COL49DsiplayName)), .HYOJ = m1.COL49_HYOJ1})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL50_TYPE = 1) Or (m1.COL50_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL50_TYPE, .HYOJJN = If(m1.COL50_HYOJJN1 Is Nothing, hyojjnShort50, m1.COL50_HYOJJN1), .COLNAME = "COL50", .COLVALUE = If(m1.COL50_HYOJNM1 IsNot Nothing, m1.COL50_HYOJNM1, If(m1.COL50 IsNot Nothing, m1.COL50, COL50DsiplayName)), .HYOJ = m1.COL50_HYOJ1})).ToList()

					For Each item In lstSportCatDetailsNames2
						lstSportCatDetailsNames.Add(item)
					Next

					m0140List(i).M0150LIST = lstSportCatDetailsNames.OrderBy(Function(m1) m1.HYOJJN).ToList()

				End If

			Next

			ViewBag.sportCatList2 = m0140List

			'Intiallly data of current month will display
			Dim SearchdtD0010 As String = Searchdt
			If String.IsNullOrEmpty(SearchdtD0010) Then
				SearchdtD0010 = Today.ToString("yyyy/MM")
			End If

			ViewData("searchdt") = SearchdtD0010
			ViewBag.searchdt = SearchdtD0010

			Dim ds As Date
			If Not (Date.TryParseExact(SearchdtD0010, "yyyy/MM", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, ds)) Then
				SearchdtD0010 = Date.Today.ToString("yyyy/MM", Globalization.CultureInfo.InvariantCulture)
				ViewData("searchdt") = SearchdtD0010
			End If

			'Logic for create data for D0010 as per it diaplay in view
			Dim CurrentYear As Integer = DateTime.Today.Year
			Dim CurrentMonth As Integer = DateTime.Today.Month

			Dim startOfCurrentMonthDate As New DateTime(CurrentYear, CurrentMonth, 1)
			Dim startOfNextMonthDate As DateTime = startOfCurrentMonthDate.AddMonths(1)
			Dim SearchDtYerarMonth As Date = Date.ParseExact(SearchdtD0010, "yyyy/MM", Nothing)
			Dim days As Integer = Date.DaysInMonth(SearchDtYerarMonth.Year, SearchDtYerarMonth.Month)

			'<key,value> list where key has D0010 data and value has maxrowspan
			Dim D0010MainList As New Dictionary(Of ICollection, Integer)

			'Loop through the number of days
			For i = 0 To days - 1

				'Create a date as per day
				Dim GYOYMD As Date = SearchDtYerarMonth.AddDays(i).ToString("yyyy/MM/dd")

				'List with list of D0010 list
				Dim subcatWithD001DataList As New List(Of ICollection)

				'Max rowspan  of each row
				Dim maxRowSpan As Integer = 1

				'Loop through the M0140 item
				For Each item As M0140 In ViewBag.sportCatList2

					'Get D0010 data by datewise
					Dim d0010L = (From d In db.D0010
								  Where d.GYOMYMD = GYOYMD AndAlso
										 d.SPORTCATCD = item.SPORTCATCD AndAlso
										 d.SPORTSUBCATCD = item.SPORTSUBCATCD AndAlso
										 d.SPORTFLG = True AndAlso
										 d.OYAGYOMFLG = True).ToList

					'Set max rowspan
					If maxRowSpan < d0010L.Count Then
						maxRowSpan = d0010L.Count
					End If

					'make a list so that column can be re arrange according to M0150 list
					Dim d0010ColOrderChangedList As New List(Of ICollection)

					'If D0010 data is found then re arrange thye data so it can be display in according to M0150 Names
					If d0010L.Count > 0 Then

						For Each itemd0010 As D0010 In d0010L
							'Create a list of A0230 data which contains the name and other info that will be used for a cell
							Dim records_cols As New List(Of A0230)
							For j As Integer = 0 To item.M0150LIST.COUNT - 1

								Dim a0230 As New A0230

								Dim DeskChief_CatData = (From m160 In db.M0160 Where m160.USERID = loginUserId And m160.CHIEFFLG = True And m160.SPORTCATCD = itemd0010.SPORTCATCD Select m160).Count
								a0230.Desk_Chief_Cat = 0
								If (DeskChief_CatData > 0) Then
									a0230.Desk_Chief_Cat = 1
								End If

								If item.M0150LIST(j).COLNAME = "BANGUMIHYOJNM1" Then
									a0230.ITEMNM = itemd0010.BANGUMINM
									a0230.CATCD = -1
									a0230.SEARCHDT = Nothing
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
									a0230.COLORSTATUS = -1
									a0230.GYOMNO = itemd0010.GYOMNO
									records_cols.Add(a0230)
								End If
								If item.M0150LIST(j).COLNAME = "KSKJKNHYOJNM1" Then
									If itemd0010.RNZK = True AndAlso itemd0010.PGYOMNO Is Nothing Then
										a0230.ITEMNM = GetFormatTime(itemd0010.KSKJKNST) & "~" & GetFormatTime("2400")
									Else
										a0230.ITEMNM = GetFormatTime(itemd0010.KSKJKNST) & "~" & GetFormatTime(itemd0010.KSKJKNED)
									End If

									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
									a0230.CATCD = -1
									a0230.SEARCHDT = Nothing
									a0230.COLORSTATUS = -1
									a0230.GYOMNO = itemd0010.GYOMNO
									records_cols.Add(a0230)
								End If
								If item.M0150LIST(j).COLNAME = "OAJKNHYOJNM1" Then
									If itemd0010.OAJKNST IsNot Nothing And itemd0010.OAJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(itemd0010.OAJKNST) & "~" & GetFormatTime(itemd0010.OAJKNED)
									ElseIf itemd0010.OAJKNST IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(itemd0010.OAJKNST)
									ElseIf itemd0010.OAJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(itemd0010.OAJKNED)
									End If
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
									a0230.CATCD = -1
									a0230.SEARCHDT = Nothing
									a0230.COLORSTATUS = -1
									a0230.GYOMNO = itemd0010.GYOMNO
									records_cols.Add(a0230)
								End If
								If item.M0150LIST(j).COLNAME = "SAIKNHYOJNM1" Then
									If itemd0010.SAIJKNST IsNot Nothing And itemd0010.SAIJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(itemd0010.SAIJKNST) & "~" & GetFormatTime(itemd0010.SAIJKNED)
									ElseIf itemd0010.SAIJKNST IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(itemd0010.SAIJKNST)
									ElseIf itemd0010.SAIJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(itemd0010.SAIJKNED)
									End If
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
									a0230.CATCD = -1
									a0230.SEARCHDT = Nothing
									a0230.COLORSTATUS = -1
									a0230.GYOMNO = itemd0010.GYOMNO
									records_cols.Add(a0230)
								End If
								If item.M0150LIST(j).COLNAME = "BASYOHYOJNM1" Then
									a0230.ITEMNM = itemd0010.BASYO
									a0230.CATCD = -1
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
									a0230.SEARCHDT = Nothing
									a0230.COLORSTATUS = -1
									a0230.GYOMNO = itemd0010.GYOMNO
									records_cols.Add(a0230)
								End If
								If item.M0150LIST(j).COLNAME = "BIKOHYOJNM1" Then
									a0230.ITEMNM = itemd0010.BIKO
									a0230.CATCD = -1
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
									a0230.SEARCHDT = Nothing
									a0230.COLORSTATUS = -1
									a0230.GYOMNO = itemd0010.GYOMNO
									records_cols.Add(a0230)
								End If
								If item.M0150LIST(j).COLNAME = "COL01" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL01
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL02" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL02
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL03" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL03
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL04" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL04
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL05" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL05
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL06" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL06
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL07" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL07
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL08" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL08
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL09" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL09
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL10" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL10
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL11" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL11
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL12" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL12
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL13" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL13
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL14" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL14
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL15" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL15
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL16" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL16
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL17" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL17
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL18" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL18
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL19" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL19
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL20" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL20
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL21" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL21
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL22" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL22
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL23" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL23
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL24" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL24
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL25" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL25
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If

								'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
								If item.M0150LIST(j).COLNAME = "COL26" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL26
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL27" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL27
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL28" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL28
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL29" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL29
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL30" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL30
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL31" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL31
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL32" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL32
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL33" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL33
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL34" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL34
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL35" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL35
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL36" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL36
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL37" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL37
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL38" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL38
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL39" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL39
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL40" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL40
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL41" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL41
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL42" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL42
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL43" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL43
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL44" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL44
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL45" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL45
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL46" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL46
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL47" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL47
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL48" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL48
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL49" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL49
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
								If item.M0150LIST(j).COLNAME = "COL50" Then
									If item.M0150LIST(j).COLTYPE = "2" Then
										records_cols.Add(GetD0010FixColData(item.M0150LIST(j), itemd0010, GYOYMD, a0230.Desk_Chief_Cat, a0230))
									Else
										a0230.CATCD = -1
										a0230.SEARCHDT = Nothing
										a0230.ITEMNM = itemd0010.COL50
										a0230.COLORSTATUS = -1
										a0230.GYOMNO = itemd0010.GYOMNO
										records_cols.Add(a0230)
									End If
									a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
									a0230.HYOJ2 = item.M0150LIST(j).HYOJ
								End If
							Next
							d0010ColOrderChangedList.Add(records_cols)
						Next

					Else
						'When there is no data found in D0010 then create a list of A0230 with empty data 
						'So that a blank cell can be created
						Dim records_cols As New List(Of A0230)
						For j As Integer = 0 To item.M0150LIST.COUNT - 1
							Dim a0230 As New A0230
							a0230.ITEMNM = ""
							a0230.COL_TYPE = item.M0150LIST(j).COLTYPE
							a0230.HYOJ2 = item.M0150LIST(j).HYOJ
							records_cols.Add(a0230)
						Next

						d0010ColOrderChangedList.Add(records_cols)

					End If
					subcatWithD001DataList.Add(d0010ColOrderChangedList)
				Next
				D0010MainList.Add(subcatWithD001DataList, maxRowSpan)
			Next

			'Send a D0010 create dictionary to view
			ViewBag.D0010MainList = D0010MainList

			'Maintain state of radio after re search
			If String.IsNullOrEmpty(SearchType) Then
				ViewBag.SearchType = "1"
			Else
				ViewBag.SearchType = SearchType
			End If

			Return View()

		End Function

		''' <summary>
		''' ASI[27 Nov 2019] :Get the color status i.e. will color should display for a cell or not
		''' </summary>
		''' <param name="gyomNO"></param>
		''' <param name="gyoYMD"></param>
		''' <param name="userID"></param>
		''' <returns></returns>
		Private Function GetColorStatus(gyomNO As Decimal, gyoYMD As Date, userID As Short, colnm As String, JTJKNST As Date, JTJKNED As Date) As Short

			Dim recCnt As Integer = 0
			JTJKNST = JTJKNST.AddSeconds(1)
			JTJKNED = JTJKNED.AddSeconds(-1)
			'recCnt = (From d1 In db.D0010
			'          Join d2 In db.D0022 On d1.GYOMNO Equals d2.GYOMNO
			'          Where (d1.GYOMNO <> gyomNO OrElse (d1.GYOMNO = gyomNO AndAlso d2.COLNM <> colnm)) AndAlso
			'                d1.GYOMYMD = gyoYMD AndAlso
			'                d1.SPORTFLG = True AndAlso
			'                d1.OYAGYOMFLG = True AndAlso
			'                d2.USERID = userID
			'          Select d1).Count()

			'If kakteiGyom = 0 Then

			'    recCnt = recCnt + (From d1 In db.D0010
			'                       Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
			'                       Where (d1.GYOMNO <> gyomNO OrElse (d1.GYOMNO = gyomNO AndAlso d2.COLNM <> colnm)) AndAlso
			'                            d1.GYOMYMD = gyoYMD AndAlso
			'                            d1.SPORTFLG = True AndAlso
			'                            d1.OYAGYOMFLG = False AndAlso d2.USERID = userID Select d1).Count()

			'Else

			'    Dim kakteigym = (From d1 In db.D0010
			'                     Where (d1.PGYOMNO = gyomNO OrElse
			'                    d1.GYOMNO = gyomNO) AndAlso
			'                    d1.SPORT_OYAFLG = False AndAlso
			'                    d1.GYOMYMD = gyoYMD AndAlso
			'                    d1.OYAGYOMFLG = False AndAlso
			'                    d1.SPORTFLG = True).ToList

			'    If kakteigym IsNot Nothing AndAlso kakteigym.Count > 0 Then
			'        Dim kgyom = kakteigym(0).GYOMNO
			'        recCnt = recCnt + (From d1 In db.D0010
			'                           Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
			'                           Where (d1.GYOMNO <> kgyom OrElse (d1.GYOMNO = kgyom AndAlso d2.COLNM <> colnm)) AndAlso
			'                            d1.GYOMYMD = gyoYMD AndAlso
			'                            d1.SPORTFLG = True AndAlso
			'                            d1.OYAGYOMFLG = False AndAlso d2.USERID = userID Select d1).Count()

			'    End If

			'End If
			Dim d0010SubSelect = (From d1 In db.D0010
								  Join d2 In db.D0022 On d1.GYOMNO Equals d2.GYOMNO
								  Where d1.SPORTFLG = True AndAlso
								  d1.OYAGYOMFLG = True AndAlso
								  d2.USERID = userID
								  Select New With {d1.JTJKNST, .JTJKNED = If(d1.RNZK = True And d1.PGYOMNO Is Nothing, DbFunctions.AddDays(d1.GYOMYMD, 1), d1.JTJKNED)})

			recCnt = (From d1 In d0010SubSelect
					  Where ((JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED) Or
							(JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED) Or
							(d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED) Or
							(d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED))
					  Select d1).Count()

			'recCnt = (From d1 In db.D0010
			'		  Join d2 In db.D0022 On d1.GYOMNO Equals d2.GYOMNO
			'		  Where '(d1.GYOMNO <> gyomNO OrElse (d1.GYOMNO = gyomNO AndAlso d2.COLNM <> colnm)) AndAlso
			'				((d1.RNZK = 1 And d1.GYOMYMD = d1.GYOMYMDED) Or (d1.RNZK = 0)) AndAlso
			'				((JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED) Or
			'				(JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED) Or
			'				(d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED) Or
			'				(d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED)) AndAlso
			'				d1.SPORTFLG = True AndAlso
			'				d1.OYAGYOMFLG = True AndAlso
			'				d2.USERID = userID
			'		  Select d1).Count()

			d0010SubSelect = (From d1 In db.D0010
							  Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
							  Where d1.SPORTFLG = True AndAlso
								  d1.OYAGYOMFLG = False AndAlso
								  d2.USERID = userID
							  Select New With {d1.JTJKNST, .JTJKNED = If(d1.RNZK = True And d1.SPORT_OYAFLG = True, DbFunctions.AddDays(d1.GYOMYMD, 1), d1.JTJKNED)})

			recCnt = recCnt + (From d1 In d0010SubSelect
							   Where ((JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED) Or
									  (JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED) Or
									  (d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED) Or
									  (d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED)) Select d1).Count()

			If recCnt < 2 Then
				recCnt = 0
			End If

			Return recCnt

		End Function

		''' <summary>
		''' ASI[27 Nov 2019] :Get the username for userid
		''' </summary>
		''' <param name="id"></param>
		''' <returns></returns>
		'Private Function GetUserName(id As String) As String

		'	Dim name As String = ""
		'	Dim userid As Short = Convert.ToInt16(id)
		'	name = (From m1 In db.M0010
		'			Where m1.USERID = userid
		'			Select m1.USERNM).SingleOrDefault

		'	Return name

		'End Function

		''' <summary>
		''' 'ASI[27 Nov 2019] : Get the type of link color that should display on link
		''' </summary>
		''' <param name="number"></param>
		''' <param name="id"></param>
		''' <returns></returns>
		Private Function GetD0022Ana(number As String, colnm As String) As Short
			Dim Ana As Short = 0
			'Dim userid As Short = Convert.ToInt16(id)
			Ana = (From m1 In db.D0022
				   Where m1.GYOMNO = number AndAlso
									m1.COLNM = colnm
				   Select m1.USERID).FirstOrDefault

			Return Ana
		End Function

		Private Function GetD0020Ana(number As String, colnm As String) As List(Of D0020)
			Dim Ana As List(Of D0020)
			'Dim userid As Short = Convert.ToInt16(id)
			Ana = (From m1 In db.D0020
				   Where (From m2 In db.D0010 Where m2.PGYOMNO = number AndAlso m2.OYAGYOMFLG = False Select m2.GYOMNO).Contains(m1.GYOMNO) AndAlso
									m1.COLNM = colnm
				   Select m1).ToList

			Return Ana
		End Function

		'ASI[19 Nov 2019] : Created below Function to fetch displayName of Model property.Annotted on property in model.
		Public Function GetDisplayName(dataType As Type, fieldName As String) As String
			Dim attr As DisplayAttribute
			attr = DirectCast(dataType.GetProperty(fieldName).GetCustomAttributes(GetType(DisplayAttribute), True).SingleOrDefault(), DisplayAttribute)
			If attr Is Nothing Then
				Dim metadataType As MetadataTypeAttribute = DirectCast(dataType.GetCustomAttributes(GetType(MetadataTypeAttribute), True).FirstOrDefault(), MetadataTypeAttribute)
				If metadataType IsNot Nothing Then
					Dim tt = metadataType.MetadataClassType.GetProperty(fieldName)
					If tt IsNot Nothing Then
						attr = DirectCast(tt.GetCustomAttributes(GetType(DisplayAttribute), True).SingleOrDefault(), DisplayAttribute)
					End If
				End If
			End If
			Return If(attr Is Nothing, String.Empty, attr.Name)

		End Function

		Private Function GetFormatTime(jkn As String) As String
			If String.IsNullOrEmpty(jkn) = False Then
				If jkn.Contains(":") = False Then
					If jkn.Length > 2 Then
						Dim strMM As String = jkn.Substring(jkn.Length - 2, 2)
						Dim strHH As String = jkn.Remove(jkn.Length - 2, 2)
						jkn = strHH & ":" & strMM
					End If
				End If
			End If

			Return jkn
		End Function

		Private Function GetD0010FixColData(m0150LIST As Object, itemd0010 As D0010, GYOYMD As Date, Desk_Chief_Cat As Boolean, ByRef a0230 As A0230) As A0230

			' Perform Buiness when type is anna
			Dim userid As Short = 0
			Dim colorValue As Boolean = False
			userid = GetD0022Ana(itemd0010.GYOMNO, m0150LIST.COLNAME)
			Dim anaList As New List(Of A0240ANALIST)
			Dim cnt = 0
			Dim fix_gyomno As Decimal = 0
			Dim fix_colnm As String = ""
			Dim ColorStatus As Short = 0
			Dim kariUserNm As String = ""

			Dim d0020 = GetD0020Ana(If(itemd0010.PGYOMNO IsNot Nothing, itemd0010.PGYOMNO, itemd0010.GYOMNO), m0150LIST.COLNAME)

			If d0020 IsNot Nothing AndAlso d0020.Count > 0 Then
				If userid = 0 Then
					userid = d0020(0).USERID
					cnt = 1
					fix_gyomno = d0020(0).GYOMNO
					fix_colnm = d0020(0).COLNM
					colorValue = True
				End If
				If d0020.Count > cnt Then
					For k = cnt To d0020.Count - 1
						Dim a0240ANALIST30 As New A0240ANALIST
						Dim m00101 = db.M0010.Find(d0020(k).USERID)
						Dim UserName1 As String = ""
						If m00101 IsNot Nothing Then
							UserName1 = m00101.USERNM
						End If
						If ColorStatus = 0 AndAlso
							Session("LoginUserACCESSLVLCD") <> "4" AndAlso Session("LoginUserACCESSLVLCD") <> "3" Then
							Dim JTJKNED As Date = itemd0010.JTJKNED
							If itemd0010.RNZK = True AndAlso itemd0010.PGYOMNO Is Nothing Then
								JTJKNED = GetJtjkn(itemd0010.GYOMYMD, "2400")
							End If
							ColorStatus = GetColorStatus(itemd0010.GYOMNO, GYOYMD, d0020(k).USERID, d0020(k).COLNM, itemd0010.JTJKNST, JTJKNED)
						End If
						a0240ANALIST30.FIX_GYOMNO = d0020(k).GYOMNO.ToString
						a0240ANALIST30.ITEMNM = UserName1
						a0240ANALIST30.LINKCOLOR = "black"
						anaList.Add(a0240ANALIST30)
					Next
				End If
			End If

			cnt = 0
			Dim d0021 = GetD0021KariAna(If(itemd0010.PGYOMNO IsNot Nothing, itemd0010.PGYOMNO, itemd0010.GYOMNO), m0150LIST.COLNAME)
			If d0021 IsNot Nothing AndAlso d0021.Count > 0 Then
				If userid = 0 Then
					cnt = 1
					fix_gyomno = d0021(0).GYOMNO
					colorValue = True
					kariUserNm = d0021(0).ANNACATNM
				End If
				If d0021.Count > cnt Then
					For k = cnt To d0021.Count - 1
						Dim a0240ANALIST30 As New A0240ANALIST
						a0240ANALIST30.FIX_GYOMNO = d0021(k).GYOMNO.ToString
						a0240ANALIST30.ITEMNM = d0021(k).ANNACATNM
						a0240ANALIST30.LINKCOLOR = "black"
						anaList.Add(a0240ANALIST30)
					Next
				End If
			End If

			Dim m0010 = db.M0010.Find(userid)
			Dim UserName As String = ""
			If m0010 IsNot Nothing Then
				UserName = m0010.USERNM
			End If

			If ColorStatus = 0 AndAlso m0010 IsNot Nothing AndAlso m0010.KARIANA = False AndAlso
				(Session("LoginUserACCESSLVLCD") <> "4" AndAlso Session("LoginUserACCESSLVLCD") <> "3") Then
				Dim JTJKNED As Date = itemd0010.JTJKNED
				If itemd0010.RNZK = True AndAlso itemd0010.PGYOMNO Is Nothing Then
					JTJKNED = GetJtjkn(itemd0010.GYOMYMD, "2400")
				End If
				ColorStatus = GetColorStatus(itemd0010.GYOMNO, GYOYMD, userid, If(fix_colnm <> "", fix_colnm, m0150LIST.COLNAME), itemd0010.JTJKNST, JTJKNED)
			End If
			a0230.COLORSTATUS = ColorStatus
			a0230.CATCD = itemd0010.SPORTCATCD
			a0230.SEARCHDT = GYOYMD
			'Dim colorValue As Boolean = GetLinkColor(itemd0010.GYOMNO, m0150LIST.COLNAME)
			If colorValue = False AndAlso (Session("LoginUserACCESSLVLCD") = "4" OrElse (Session("LoginUserACCESSLVLCD") = "3" AndAlso Desk_Chief_Cat = 0)) Then
				a0230.ITEMNM = If(UserName <> "", "仮アナ", "")
			ElseIf userid = 0 AndAlso d0021 IsNot Nothing AndAlso d0021.Count > 0 Then
				a0230.ITEMNM = kariUserNm
			Else
				a0230.ITEMNM = UserName
			End If
			a0230.FIX_GYOMNO = fix_gyomno
			a0230.GYOMNO = itemd0010.GYOMNO
			a0230.LINKCOLOR = If(colorValue = False, LINK_COLOR1, LINK_COLOR2)
			a0230.AnaLIST = anaList

			Return a0230

		End Function

		Private Function GetD0021KariAna(number As String, colnm As String) As List(Of D0021)
			Dim Ana As List(Of D0021)
			'Dim userid As Short = Convert.ToInt16(id)
			Ana = (From m1 In db.D0021
				   Where (From m2 In db.D0010 Where m2.PGYOMNO = number AndAlso m2.OYAGYOMFLG = False Select m2.GYOMNO).Contains(m1.GYOMNO) AndAlso
									m1.COLNM = colnm
				   Select m1).ToList

			Return Ana
		End Function

		Private Function GetJtjkn(ByVal dtymd As Date, ByVal strHHMM As String) As Date
			Dim dtRtn As Date = Nothing
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
	End Class
End Namespace