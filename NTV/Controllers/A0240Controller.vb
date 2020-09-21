'ASI[10 Dec 2019]: Description: 
'COL_TYPE = 1 means FURI
'COL_TYPE = 2 means ANA
'COL_TYPE = 3 means User Personal Shift Data same as C0040
'COL_TYPE = 4 means it is DateHeader and DayHeader Column

Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Web.Mvc

Namespace Controllers
	Public Class A0240Controller
		Inherits Controller

		Private db As New Model1
		Public Const LINK_COLOR1 As String = "#1e90ff"
		Public Const LINK_COLOR2 As String = "red"
		Public Const LINK_COLOR3 As String = "black"
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

		' GET: A0230
		Function Index(ByVal SearchType As String, ByVal SportCatCd As String, ByVal Searchdt As String, ByVal button As String, ByVal lastForm As String, ByVal ShiftTableRadioType As String) As ActionResult
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			'If CheckAccessLvl() = False Then
			'    Return View("ErrorAccesslvl")
			'End If

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.AbsoluteUri      '休日設定画面から来た時アナ名が文字化けするので、Encodeされている元のUrlを取得
				If Not strUrlReferrer.Contains("/A0240") AndAlso Not strUrlReferrer.Contains("/A0220") Then
					Session("UrlReferrer") = strUrlReferrer
				End If
			End If

			If lastForm IsNot Nothing Then
				ViewBag.lastForm = lastForm
			End If

			ViewBag.LoginUserSystem = Session("LoginUserSystem")
			ViewBag.LoginUserKanri = Session("LoginUserKanri")
			ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")
			ViewBag.LoginUserDeskChief = Session("LoginUserDeskChief")

			Dim sportCatList = db.M0130.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			'Dim blankCat As New M0130
			'blankCat.SPORTCATCD1 = "0"
			'blankCat.SPORTCATNM1 = ""
			'sportCatList.Insert(0, blankCat)
			ViewBag.SPORTCATCD_LIST = sportCatList

			'When screen loads from menu then use the list's first sportcatcd
			If SportCatCd Is Nothing Then
				Dim userAssignCat = (From m160 In db.M0160 Where m160.USERID = loginUserId Select m160).ToList
				If userAssignCat IsNot Nothing AndAlso userAssignCat.Count > 0 Then
					For i As Integer = 0 To sportCatList.Count - 1
						For j As Integer = 0 To userAssignCat.Count - 1
							If sportCatList(i).SPORTCATCD = userAssignCat(j).SPORTCATCD Then
								SportCatCd = sportCatList(i).SPORTCATCD
								Exit For
							End If
						Next
						If SportCatCd IsNot Nothing Then
							Exit For
						End If
					Next
				ElseIf sportCatList.Count > 0 Then
					SportCatCd = sportCatList(0).SPORTCATCD
				Else
					SportCatCd = 0
				End If
			End If

			Dim DeskChief_CatData = (From m160 In db.M0160 Where m160.USERID = loginUserId And m160.CHIEFFLG = True And m160.SPORTCATCD = SportCatCd Select m160).Count
			ViewBag.CHIEF_CAT = 0
			If (DeskChief_CatData > 0) Then
				ViewBag.CHIEF_CAT = 1
			End If

			'Dim m0010List = db.M0010.OrderBy(Function(m) m.USERID).ToList
			'Fetch userNm whose placed under KARIANA true and non deleted whose are registered with sportcatcd in M0160 tbl
			Dim m0010List = From m In db.M0010 Select m
			m0010List = m0010List.Where(Function(d1) db.M0160.Where(Function(m) m.SPORTCATCD = SportCatCd AndAlso d1.STATUS = True AndAlso d1.HYOJ = True).Select(Function(t2) t2.USERID).Contains(d1.USERID) Or (d1.KARIANA = True And d1.STATUS = True))
			Dim ana_TempAnaList = m0010List.OrderByDescending(Function(x) x.KARIANA = True).ThenBy(Function(x) x.HYOJJN).ToList

			Dim itemm0010 As New M0010
			itemm0010.USERID = "0"
			itemm0010.USERNM = ""
			ana_TempAnaList.Insert(0, itemm0010)
			ViewBag.ana_TempAnaList = ana_TempAnaList

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


			Dim m0140List = (From m1 In db.M0140
							 Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
							 Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
							 Where m2.SPORTCATCD = SportCatCd And m1.HYOJ = True And m3.HYOJ = True
							 Order By m3.HYOJJN, m3.SPORTCATCD, m1.HYOJJN, m1.SPORTSUBCATCD
							 Select m1).ToList()

			Dim m0130List = (From m1 In db.M0140
							 Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
							 Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
							 Where m2.SPORTCATCD = SportCatCd And m1.HYOJ = True And m3.HYOJ = True
							 Order By m3.HYOJJN, m3.SPORTCATCD, m1.HYOJJN, m1.SPORTSUBCATCD
							 Select m3).ToList()


			Dim m0150 As New M0150
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

			For i As Integer = 0 To m0140List.Count - 1
				m0140List(i).SPORTCATCD = m0130List(i).SPORTCATCD

				Dim SportSubCatCD As Short = m0140List(i).SPORTSUBCATCD

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

				Dim lstCATCDUn = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BANGUMIHYOJJN2 Is Nothing, hyojjnShort011, m1.BANGUMIHYOJJN2), .COLNAME = "BANGUMINM", .COLVALUE = If(m1.BANGUMIHYOJNM2 IsNot Nothing, m1.BANGUMIHYOJNM2, bangumiHyojNm2DsiplayName), .HYOJ = (m1.BANGUMIHYOJ2 <> "0")}).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.KSKJKNHYOJJN2 Is Nothing, hyojjnShort022, m1.KSKJKNHYOJJN2), .COLNAME = "KSKJKNST", .COLVALUE = If(m1.KSKJKNHYOJNM2 IsNot Nothing, m1.KSKJKNHYOJNM2, ksjnkHyojNM2DsiplayName), .HYOJ = (m1.KSKJKNHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.OAJKNHYOJJN2 Is Nothing, hyojjnShort033, m1.OAJKNHYOJJN2), .COLNAME = "OAJKNST", .COLVALUE = If(m1.OAJKNHYOJNM2 IsNot Nothing, m1.OAJKNHYOJNM2, oajnkHyojNm2DsiplayName), .HYOJ = (m1.OAJKNHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.SAIKNHYOJJN2 Is Nothing, hyojjnShort044, m1.SAIKNHYOJJN2), .COLNAME = "SAIJKNST", .COLVALUE = If(m1.SAIKNHYOJNM2 IsNot Nothing, m1.SAIKNHYOJNM2, saiknHyojNm2DsiplayName), .HYOJ = (m1.SAIKNHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BASYOHYOJJN2 Is Nothing, hyojjnShort055, m1.BASYOHYOJJN2), .COLNAME = "BASYO", .COLVALUE = If(m1.BASYOHYOJNM2 IsNot Nothing, m1.BASYOHYOJNM2, bashyoHyojNM2DsiplayName), .HYOJ = (m1.BASYOHYOJ2 <> "0")})).
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD).Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = If(m1.BIKOHYOJJN2 Is Nothing, hyojjnShort066, m1.BIKOHYOJJN2), .COLNAME = "BIKO", .COLVALUE = If(m1.BIKOHYOJNM2 IsNot Nothing, m1.BIKOHYOJNM2, bikoHyojNm2DsiplayName), .HYOJ = (m1.BIKOHYOJ2 <> "0")})).
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
					Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL25_TYPE = 1) Or (m1.COL25_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL25_TYPE, .HYOJJN = If(m1.COL25_HYOJJN2 Is Nothing, hyojjnShort25, m1.COL25_HYOJJN2), .COLNAME = "COL25", .COLVALUE = If(m1.COL25_HYOJNM2 IsNot Nothing, m1.COL25_HYOJNM2, If(m1.COL25 IsNot Nothing, m1.COL25, col25DsiplayName)), .HYOJ = m1.COL25_HYOJ2})).ToList()

				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				Dim lstCATCDUn2 = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL26_TYPE = 1) Or (m1.COL26_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL26_TYPE, .HYOJJN = If(m1.COL26_HYOJJN2 Is Nothing, hyojjnShort26, m1.COL26_HYOJJN2), .COLNAME = "COL26", .COLVALUE = If(m1.COL26_HYOJNM2 IsNot Nothing, m1.COL26_HYOJNM2, If(m1.COL26 IsNot Nothing, m1.COL26, COL26DsiplayName)), .HYOJ = m1.COL26_HYOJ2}).
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

				For Each item In lstCATCDUn2
					lstCATCDUn.Add(item)
				Next

				m0140List(i).M0150LIST = lstCATCDUn.OrderBy(Function(m1) m1.HYOJJN).ToList()
				'Else
				'Dim lstCATCDUn = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso m1.BANGUMIHYOJ2 <> "0").Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = m1.BANGUMIHYOJJN2, .COLNAME = "BANGUMINM", .COLVALUE = If(m1.BANGUMIHYOJNM2 IsNot Nothing, m1.BANGUMIHYOJNM2, bangumiHyojNm2DsiplayName), .HYOJ = False}).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso m1.KSKJKNHYOJ2 <> "0").Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = m1.KSKJKNHYOJJN2, .COLNAME = "KSKJKNST", .COLVALUE = If(m1.KSKJKNHYOJNM2 IsNot Nothing, m1.KSKJKNHYOJNM2, ksjnkHyojNM2DsiplayName), .HYOJ = False})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso m1.OAJKNHYOJ2 <> "0").Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = m1.OAJKNHYOJJN2, .COLNAME = "OAJKNST", .COLVALUE = If(m1.OAJKNHYOJNM2 IsNot Nothing, m1.OAJKNHYOJNM2, oajnkHyojNm2DsiplayName), .HYOJ = False})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso m1.SAIKNHYOJ2 <> "0").Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = m1.SAIKNHYOJJN2, .COLNAME = "SAIJKNST", .COLVALUE = If(m1.SAIKNHYOJNM2 IsNot Nothing, m1.SAIKNHYOJNM2, saiknHyojNm2DsiplayName), .HYOJ = False})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso m1.BASYOHYOJ2 <> "0").Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = m1.BASYOHYOJJN2, .COLNAME = "BASYO", .COLVALUE = If(m1.BASYOHYOJNM2 IsNot Nothing, m1.BASYOHYOJNM2, bashyoHyojNM2DsiplayName), .HYOJ = False})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso m1.BIKOHYOJ2 <> "0").Select(Function(m1) New With {.COLTYPE = "", .HYOJJN = m1.BIKOHYOJJN2, .COLNAME = "BIKO", .COLVALUE = If(m1.BIKOHYOJNM2 IsNot Nothing, m1.BIKOHYOJNM2, bikoHyojNm2DsiplayName), .HYOJ = False})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL01_TYPE = 1 And m1.COL01_HYOJ2 <> 0) Or (m1.COL01_TYPE = 2 And m1.COL01_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL01_TYPE, .HYOJJN = m1.COL01_HYOJJN2, .COLNAME = "COL01", .COLVALUE = If(m1.COL01_HYOJNM2 IsNot Nothing, m1.COL01_HYOJNM2, If(m1.COL01 IsNot Nothing, m1.COL01, col01DsiplayName)), .HYOJ = m1.COL01_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL02_TYPE = 1 And m1.COL02_HYOJ2 <> 0) Or (m1.COL02_TYPE = 2 And m1.COL02_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL02_TYPE, .HYOJJN = m1.COL02_HYOJJN2, .COLNAME = "COL02", .COLVALUE = If(m1.COL02_HYOJNM2 IsNot Nothing, m1.COL02_HYOJNM2, If(m1.COL02 IsNot Nothing, m1.COL02, col02DsiplayName)), .HYOJ = m1.COL02_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL03_TYPE = 1 And m1.COL03_HYOJ2 <> 0) Or (m1.COL03_TYPE = 2 And m1.COL03_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL03_TYPE, .HYOJJN = m1.COL03_HYOJJN2, .COLNAME = "COL03", .COLVALUE = If(m1.COL03_HYOJNM2 IsNot Nothing, m1.COL03_HYOJNM2, If(m1.COL03 IsNot Nothing, m1.COL03, col03DsiplayName)), .HYOJ = m1.COL03_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL04_TYPE = 1 And m1.COL04_HYOJ2 <> 0) Or (m1.COL04_TYPE = 2 And m1.COL04_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL04_TYPE, .HYOJJN = m1.COL04_HYOJJN2, .COLNAME = "COL04", .COLVALUE = If(m1.COL04_HYOJNM2 IsNot Nothing, m1.COL04_HYOJNM2, If(m1.COL04 IsNot Nothing, m1.COL04, col04DsiplayName)), .HYOJ = m1.COL04_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL05_TYPE = 1 And m1.COL05_HYOJ2 <> 0) Or (m1.COL05_TYPE = 2 And m1.COL05_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL05_TYPE, .HYOJJN = m1.COL05_HYOJJN2, .COLNAME = "COL05", .COLVALUE = If(m1.COL05_HYOJNM2 IsNot Nothing, m1.COL05_HYOJNM2, If(m1.COL05 IsNot Nothing, m1.COL05, col05DsiplayName)), .HYOJ = m1.COL05_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL06_TYPE = 1 And m1.COL06_HYOJ2 <> 0) Or (m1.COL06_TYPE = 2 And m1.COL06_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL06_TYPE, .HYOJJN = m1.COL06_HYOJJN2, .COLNAME = "COL06", .COLVALUE = If(m1.COL06_HYOJNM2 IsNot Nothing, m1.COL06_HYOJNM2, If(m1.COL06 IsNot Nothing, m1.COL06, col06DsiplayName)), .HYOJ = m1.COL06_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL07_TYPE = 1 And m1.COL07_HYOJ2 <> 0) Or (m1.COL07_TYPE = 2 And m1.COL07_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL07_TYPE, .HYOJJN = m1.COL07_HYOJJN2, .COLNAME = "COL07", .COLVALUE = If(m1.COL07_HYOJNM2 IsNot Nothing, m1.COL07_HYOJNM2, If(m1.COL07 IsNot Nothing, m1.COL07, col07DsiplayName)), .HYOJ = m1.COL07_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL08_TYPE = 1 And m1.COL08_HYOJ2 <> 0) Or (m1.COL08_TYPE = 2 And m1.COL08_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL08_TYPE, .HYOJJN = m1.COL08_HYOJJN2, .COLNAME = "COL08", .COLVALUE = If(m1.COL08_HYOJNM2 IsNot Nothing, m1.COL08_HYOJNM2, If(m1.COL08 IsNot Nothing, m1.COL08, col08DsiplayName)), .HYOJ = m1.COL08_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL09_TYPE = 1 And m1.COL09_HYOJ2 <> 0) Or (m1.COL09_TYPE = 2 And m1.COL09_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL09_TYPE, .HYOJJN = m1.COL09_HYOJJN2, .COLNAME = "COL09", .COLVALUE = If(m1.COL09_HYOJNM2 IsNot Nothing, m1.COL09_HYOJNM2, If(m1.COL09 IsNot Nothing, m1.COL09, col09DsiplayName)), .HYOJ = m1.COL09_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL10_TYPE = 1 And m1.COL10_HYOJ2 <> 0) Or (m1.COL10_TYPE = 2 And m1.COL10_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL10_TYPE, .HYOJJN = m1.COL10_HYOJJN2, .COLNAME = "COL10", .COLVALUE = If(m1.COL10_HYOJNM2 IsNot Nothing, m1.COL10_HYOJNM2, If(m1.COL10 IsNot Nothing, m1.COL10, col10DsiplayName)), .HYOJ = m1.COL10_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL11_TYPE = 1 And m1.COL11_HYOJ2 <> 0) Or (m1.COL11_TYPE = 2 And m1.COL11_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL11_TYPE, .HYOJJN = m1.COL11_HYOJJN2, .COLNAME = "COL11", .COLVALUE = If(m1.COL11_HYOJNM2 IsNot Nothing, m1.COL11_HYOJNM2, If(m1.COL11 IsNot Nothing, m1.COL11, col11DsiplayName)), .HYOJ = m1.COL11_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL12_TYPE = 1 And m1.COL12_HYOJ2 <> 0) Or (m1.COL12_TYPE = 2 And m1.COL12_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL12_TYPE, .HYOJJN = m1.COL12_HYOJJN2, .COLNAME = "COL12", .COLVALUE = If(m1.COL12_HYOJNM2 IsNot Nothing, m1.COL12_HYOJNM2, If(m1.COL12 IsNot Nothing, m1.COL12, col12DsiplayName)), .HYOJ = m1.COL12_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL13_TYPE = 1 And m1.COL13_HYOJ2 <> 0) Or (m1.COL13_TYPE = 2 And m1.COL13_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL13_TYPE, .HYOJJN = m1.COL13_HYOJJN2, .COLNAME = "COL13", .COLVALUE = If(m1.COL13_HYOJNM2 IsNot Nothing, m1.COL13_HYOJNM2, If(m1.COL13 IsNot Nothing, m1.COL13, col13DsiplayName)), .HYOJ = m1.COL13_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL14_TYPE = 1 And m1.COL14_HYOJ2 <> 0) Or (m1.COL14_TYPE = 2 And m1.COL14_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL14_TYPE, .HYOJJN = m1.COL14_HYOJJN2, .COLNAME = "COL14", .COLVALUE = If(m1.COL14_HYOJNM2 IsNot Nothing, m1.COL14_HYOJNM2, If(m1.COL14 IsNot Nothing, m1.COL14, col14DsiplayName)), .HYOJ = m1.COL14_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL15_TYPE = 1 And m1.COL15_HYOJ2 <> 0) Or (m1.COL15_TYPE = 2 And m1.COL15_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL15_TYPE, .HYOJJN = m1.COL15_HYOJJN2, .COLNAME = "COL15", .COLVALUE = If(m1.COL15_HYOJNM2 IsNot Nothing, m1.COL15_HYOJNM2, If(m1.COL15 IsNot Nothing, m1.COL15, col15DsiplayName)), .HYOJ = m1.COL15_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL16_TYPE = 1 And m1.COL16_HYOJ2 <> 0) Or (m1.COL16_TYPE = 2 And m1.COL16_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL16_TYPE, .HYOJJN = m1.COL16_HYOJJN2, .COLNAME = "COL16", .COLVALUE = If(m1.COL16_HYOJNM2 IsNot Nothing, m1.COL16_HYOJNM2, If(m1.COL16 IsNot Nothing, m1.COL16, col16DsiplayName)), .HYOJ = m1.COL16_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL17_TYPE = 1 And m1.COL17_HYOJ2 <> 0) Or (m1.COL17_TYPE = 2 And m1.COL17_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL17_TYPE, .HYOJJN = m1.COL17_HYOJJN2, .COLNAME = "COL17", .COLVALUE = If(m1.COL17_HYOJNM2 IsNot Nothing, m1.COL17_HYOJNM2, If(m1.COL17 IsNot Nothing, m1.COL17, col17DsiplayName)), .HYOJ = m1.COL17_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL18_TYPE = 1 And m1.COL18_HYOJ2 <> 0) Or (m1.COL18_TYPE = 2 And m1.COL18_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL18_TYPE, .HYOJJN = m1.COL18_HYOJJN2, .COLNAME = "COL18", .COLVALUE = If(m1.COL18_HYOJNM2 IsNot Nothing, m1.COL18_HYOJNM2, If(m1.COL18 IsNot Nothing, m1.COL18, col18DsiplayName)), .HYOJ = m1.COL18_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL19_TYPE = 1 And m1.COL19_HYOJ2 <> 0) Or (m1.COL19_TYPE = 2 And m1.COL19_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL19_TYPE, .HYOJJN = m1.COL19_HYOJJN2, .COLNAME = "COL19", .COLVALUE = If(m1.COL19_HYOJNM2 IsNot Nothing, m1.COL19_HYOJNM2, If(m1.COL19 IsNot Nothing, m1.COL19, col19DsiplayName)), .HYOJ = m1.COL19_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL20_TYPE = 1 And m1.COL20_HYOJ2 <> 0) Or (m1.COL20_TYPE = 2 And m1.COL20_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL20_TYPE, .HYOJJN = m1.COL20_HYOJJN2, .COLNAME = "COL20", .COLVALUE = If(m1.COL20_HYOJNM2 IsNot Nothing, m1.COL20_HYOJNM2, If(m1.COL20 IsNot Nothing, m1.COL20, col20DsiplayName)), .HYOJ = m1.COL20_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL21_TYPE = 1 And m1.COL21_HYOJ2 <> 0) Or (m1.COL21_TYPE = 2 And m1.COL21_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL21_TYPE, .HYOJJN = m1.COL21_HYOJJN2, .COLNAME = "COL21", .COLVALUE = If(m1.COL21_HYOJNM2 IsNot Nothing, m1.COL21_HYOJNM2, If(m1.COL21 IsNot Nothing, m1.COL21, col21DsiplayName)), .HYOJ = m1.COL21_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL22_TYPE = 1 And m1.COL22_HYOJ2 <> 0) Or (m1.COL22_TYPE = 2 And m1.COL22_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL22_TYPE, .HYOJJN = m1.COL22_HYOJJN2, .COLNAME = "COL22", .COLVALUE = If(m1.COL22_HYOJNM2 IsNot Nothing, m1.COL22_HYOJNM2, If(m1.COL22 IsNot Nothing, m1.COL22, col22DsiplayName)), .HYOJ = m1.COL22_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL23_TYPE = 1 And m1.COL23_HYOJ2 <> 0) Or (m1.COL23_TYPE = 2 And m1.COL23_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL23_TYPE, .HYOJJN = m1.COL23_HYOJJN2, .COLNAME = "COL23", .COLVALUE = If(m1.COL23_HYOJNM2 IsNot Nothing, m1.COL23_HYOJNM2, If(m1.COL23 IsNot Nothing, m1.COL23, col23DsiplayName)), .HYOJ = m1.COL23_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL24_TYPE = 1 And m1.COL24_HYOJ2 <> 0) Or (m1.COL24_TYPE = 2 And m1.COL24_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL24_TYPE, .HYOJJN = m1.COL24_HYOJJN2, .COLNAME = "COL24", .COLVALUE = If(m1.COL24_HYOJNM2 IsNot Nothing, m1.COL24_HYOJNM2, If(m1.COL24 IsNot Nothing, m1.COL24, col24DsiplayName)), .HYOJ = m1.COL24_HYOJ2})).
				'				Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = SportSubCatCD AndAlso ((m1.COL25_TYPE = 1 And m1.COL25_HYOJ2 <> 0) Or (m1.COL25_TYPE = 2 And m1.COL25_HYOJ2 <> 0))).Select(Function(m1) New With {.COLTYPE = m1.COL25_TYPE, .HYOJJN = m1.COL25_HYOJJN2, .COLNAME = "COL25", .COLVALUE = If(m1.COL25_HYOJNM2 IsNot Nothing, m1.COL25_HYOJNM2, If(m1.COL25 IsNot Nothing, m1.COL25, col25DsiplayName)), .HYOJ = m1.COL25_HYOJ2})).OrderBy(Function(m1) m1.HYOJJN).ToList()
				'm0140List(i).M0150LIST = lstCATCDUn
				'End If
			Next

			'Logic for day create
			Dim SearchDtYerarMonth As Date

			Dim edateValue As Date
			If Date.TryParseExact(SearchdtD0010, "yyyy/MM", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, edateValue) Then
				SearchDtYerarMonth = edateValue.ToString("yyyy/MM", Globalization.CultureInfo.InvariantCulture)
			Else
				SearchDtYerarMonth = Date.Today.ToString("yyyy/MM", Globalization.CultureInfo.InvariantCulture)
				SearchdtD0010 = Date.Today.ToString("yyyy/MM", Globalization.CultureInfo.InvariantCulture)
			End If


			Dim year As Integer = SearchDtYerarMonth.Year
			Dim month As Integer = SearchDtYerarMonth.Month
			Dim latDateOfMonth As Integer = Date.DaysInMonth(year, month)
			Dim userListPersonalShiftData As New Dictionary(Of String, List(Of C0040))

			'User personalshift data do not require in csv
			If button <> "downloadcsv" Then

				'ASI[09 Dec 2019]: to create and append Date Header and Day Header in header caption list
				m0140List.Insert(0, New M0140())
				m0140List(0).M0150LIST = {(New With {.COLTYPE = "4", .HYOJJN = 0, .COLNAME = "DateHeader", .COLVALUE = "日付", .HYOJ = False})}.ToList
				m0140List.Insert(1, New M0140())
				m0140List(1).M0150LIST = {(New With {.COLTYPE = "4", .HYOJJN = 0, .COLNAME = "DayHeader", .COLVALUE = "曜", .HYOJ = False})}.ToList

				'ASI[11 Dec 20198]: [START] Fetch freeze column name from M0150 tbl
				Dim m0150Freeze_LstColNm = (From m1 In db.M0150
											Join m2 In db.M0140 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
											Where m1.SPORTCATCD = SportCatCd
											Order By m2.HYOJJN
											Select m1.FREEZE_LSTCOLNM).FirstOrDefault()

				'Always Display first 2 columns[Date&Day Header] freeze even there is no any subcategory data present
				Dim NoOfFreezeCol_DbValue As Int16 = 2
				Dim FreezeColIsExist As Boolean = False

				'First 2 records in m0140List is for Date&Day Header. So if any subcategory is there 
				'then counting numbers of column upto FREEZE_LSTCOLNM which will be freez on screen
				If m0150Freeze_LstColNm IsNot Nothing And m0150Freeze_LstColNm <> "" And m0140List.Count > 2 Then
					For Each item In m0140List(2).M0150LIST
						If item.COLNAME = m0150Freeze_LstColNm Then
							NoOfFreezeCol_DbValue = NoOfFreezeCol_DbValue + 1
							FreezeColIsExist = True
							Exit For
						Else
							NoOfFreezeCol_DbValue = NoOfFreezeCol_DbValue + 1
						End If
					Next
				End If
				If FreezeColIsExist = False Then
					NoOfFreezeCol_DbValue = 2
				End If
				ViewBag.NoOfFreezeCol = NoOfFreezeCol_DbValue
				'[End]
			End If
			'Fetch list of Anauncer whose have sport Category as selected on screen
			Dim anaNmList = (From m1 In db.M0010
							 Join m2 In db.M0160 On m1.USERID Equals m2.USERID
							 Where m2.SPORTCATCD = SportCatCd And m1.HYOJ = True And m1.STATUS = True And m1.M0050.ANA = True And m1.KARIANA = False
							 Select m1.USERNM, m1.USERID, m1.HYOJJN, m1.USERSEX).Distinct().ToList().OrderBy(Function(f) f.HYOJJN).ThenBy(Function(f) f.USERSEX)
			Dim commonController As CommonController = New CommonController()
			Dim anaIdList As List(Of String) = Nothing
			Dim c0040List1 As List(Of C0040) = Nothing
			If anaNmList IsNot Nothing AndAlso anaNmList.Count > 0 Then
				anaIdList = anaNmList.Select(Function(m) m.USERID.ToString).ToList
				c0040List1 = commonController.GetPersonalShiftData_OneMonth(latDateOfMonth, SearchDtYerarMonth.ToString("yyyy/MM/dd"), anaIdList, "0")
			End If

			For i As Integer = 0 To anaNmList.Count - 1
				If anaNmList(i).USERNM <> "" Then
					Dim m0140Obj As M0140 = New M0140()
					m0140Obj.SPORTSUBCATNM = ""
					m0140Obj.SELECTEDINDEX = anaNmList(i).USERID 'We are setting userid name of ananmList in M0140 object's SELECTEDINDEX

					'ASI[09 Dec 2019] : set COL_TYPE = 3 , to identify Date Header and Day Header
					m0140Obj.M0150LIST = {(New With {.COLTYPE = "3", .HYOJJN = i, .COLNAME = "", .COLVALUE = anaNmList(i).USERNM, .HYOJ = False})}.ToList
					m0140List.Add(m0140Obj)

					'Create List of Personal Shift Data of UserList
					'sportdata flag passed = 0 to exclude kari sport data from personal shit data of user
					'Dim commonController As CommonController = New CommonController()
					'Dim c0040List = commonController.GetPersonalShiftData(latDateOfMonth, SearchDtYerarMonth.ToString("yyyy/MM/dd"), anaNmList(i).USERID, "0")

					Dim c0040List = c0040List1.Where(Function(f) f.USERID = m0140Obj.SELECTEDINDEX).ToList()
					c0040List = c0040List.OrderBy(Function(f) Date.Parse(f.GYOMDT)).ThenBy(Function(f) f.STTIME).ToList()
					For Each item In c0040List
						item.HI = Date.Parse(item.HI).ToString("MM/dd")
					Next
					Dim strHI As String = ""
					Dim strYobi As String = ""
					For Each item In c0040List
						If item.HI <> strHI Then
							strHI = item.HI
							strYobi = item.YOBI

							If String.IsNullOrEmpty(item.MYMEMOFLG) = True Then
								item.MYMEMOFLG = "1"
							End If

						Else
							item.HI = ""
							item.YOBI = ""
							item.MYMEMO = ""
						End If

					Next
					'Remove the next month data from list
					Dim lstDateofMonth As String = SearchDtYerarMonth.AddDays(latDateOfMonth - 1).ToString("yyyy/MM/dd")
					For indx As Integer = c0040List.Count - 1 To 0 Step -1
						If c0040List(indx).GYOMDT > lstDateofMonth Then
							c0040List.RemoveAt(indx)
						End If
					Next

					'To set top bottom border color same as C0040 Index view page
					For intIndex As Integer = 0 To c0040List.Count - 1
						Dim item = c0040List(intIndex)

						Dim topwaku As String = ""
						Dim bottomwaku As String = ""
						Dim bottomblackwaku As String = ""
						If intIndex > 0 Then
							Dim itemBefore = c0040List(intIndex - 1)
							Dim itemAfter = Nothing
							If intIndex < c0040List.Count - 1 Then
								itemAfter = c0040List(intIndex + 1)
							End If

							If itemBefore.KYUSHUTSU <> "" AndAlso itemBefore.KYUSHUTSU = item.KYUSHUTSU Then
								topwaku = Nothing
							Else
								topwaku = item.ROWWAKUCOLOR
							End If

							If itemAfter IsNot Nothing Then
								If itemAfter.KYUSHUTSU <> "" AndAlso itemAfter.KYUSHUTSU = item.KYUSHUTSU Then
									'If itemAfter.HI <> "" Then
									'	bottomwaku = "808080"
									'Else
									bottomwaku = Nothing
									'End If

								Else
									bottomwaku = item.ROWWAKUCOLOR
								End If
							Else
								bottomwaku = item.ROWWAKUCOLOR
							End If
							If itemAfter IsNot Nothing Then
								'If itemAfter.HI <> "" Then
								'	bottomblackwaku = "808080"
								'Else
								bottomblackwaku = Nothing
								'End If
							End If
						Else
							Dim itemAfter = Nothing
							If intIndex < c0040List.Count - 1 Then
								itemAfter = c0040List(intIndex + 1)
							End If
							If item.KYUSHUTSU <> "" Then
								topwaku = item.ROWWAKUCOLOR
							End If
							If itemAfter IsNot Nothing Then
								If itemAfter.KYUSHUTSU <> "" AndAlso itemAfter.KYUSHUTSU = item.KYUSHUTSU Then
									'If itemAfter.HI <> "" Then
									'	bottomwaku = "808080"
									'Else
									bottomwaku = Nothing
									'End If
								Else
									bottomwaku = item.ROWWAKUCOLOR
								End If
							End If
							If itemAfter IsNot Nothing Then
								'If itemAfter.HI <> "" Then
								'	bottomblackwaku = "808080"
								'Else
								bottomblackwaku = Nothing
								'End If
							End If
						End If
						item.TOPWAKU = topwaku
						item.BOTTOMWAKU = bottomwaku
						item.BOTTOMBLACKWAKU = bottomblackwaku

					Next

					userListPersonalShiftData.Add(anaNmList(i).USERID, c0040List)

				End If
			Next


			ViewBag.sportCatList2 = m0140List

			Dim wholeRowsList As New Dictionary(Of ICollection, Integer)
			For d = 0 To latDateOfMonth - 1
				'Create a date for where
				Dim GYOYMD As Date = SearchDtYerarMonth.AddDays(d).ToString("yyyy/MM/dd")

				Dim records_subcat As New List(Of ICollection)
				Dim maxRowSpan As Integer = 1
				For Each m0140SubCatList In m0140List
					Dim d0010_Records = db.D0010.Where(Function(m) m.SPORTCATCD = m0140SubCatList.SPORTCATCD And
														   m.SPORTSUBCATCD = m0140SubCatList.SPORTSUBCATCD And
														   m.GYOMYMD = GYOYMD And
														   m.SPORTFLG = True And
														   m.OYAGYOMFLG = True).ToList()

					If maxRowSpan < d0010_Records.Count Then
						maxRowSpan = d0010_Records.Count
					End If

					Dim records_d0010 As New List(Of ICollection)

					If d0010_Records.Count > 0 AndAlso d0010_Records IsNot Nothing Then
						For Each d0010_EachRecord In d0010_Records
							Dim colValueList As New List(Of A0230)

							For Each orderedM0150 In m0140SubCatList.M0150LIST
								Dim a0230 As New A0230
								If orderedM0150.COLNAME = "BANGUMINM" Then
									a0230.ITEMNM = d0010_EachRecord.BANGUMINM
									a0230.CATCD = -1
									a0230.COLORSTATUS = -1
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "KSKJKNST" Then
									If d0010_EachRecord.RNZK = True AndAlso d0010_EachRecord.PGYOMNO Is Nothing Then
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.KSKJKNST) & "~" & "24:00"
									Else
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.KSKJKNST) & "~" & GetFormatTime(d0010_EachRecord.KSKJKNED)
									End If
									a0230.CATCD = -1
									a0230.COLORSTATUS = -1
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "OAJKNST" Then
									If d0010_EachRecord.OAJKNST IsNot Nothing And d0010_EachRecord.OAJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.OAJKNST) & "~" & GetFormatTime(d0010_EachRecord.OAJKNED)
									ElseIf d0010_EachRecord.OAJKNST IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.OAJKNST)
									ElseIf d0010_EachRecord.OAJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.OAJKNED)
									End If
									a0230.CATCD = -1
									a0230.COLORSTATUS = -1
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "SAIJKNST" Then
									If d0010_EachRecord.SAIJKNST IsNot Nothing And d0010_EachRecord.SAIJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.SAIJKNST) & "~" & GetFormatTime(d0010_EachRecord.SAIJKNED)
									ElseIf d0010_EachRecord.SAIJKNST IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.SAIJKNST)
									ElseIf d0010_EachRecord.SAIJKNED IsNot Nothing Then
										a0230.ITEMNM = GetFormatTime(d0010_EachRecord.SAIJKNED)
									End If
									a0230.CATCD = -1
									a0230.COLORSTATUS = -1
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "BASYO" Then
									a0230.ITEMNM = d0010_EachRecord.BASYO
									a0230.CATCD = -1
									a0230.COLORSTATUS = -1
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "BIKO" Then
									a0230.ITEMNM = d0010_EachRecord.BIKO
									a0230.CATCD = -1
									a0230.COLORSTATUS = -1
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL01" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL01
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL02" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL02
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL03" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL03
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL04" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL04
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL05" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL05
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL06" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL06
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL07" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL07
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL08" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL08
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									colValueList.Add(a0230)
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL09" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL09
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									colValueList.Add(a0230)
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL10" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL10
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL11" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL11
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL12" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL12
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If

								If orderedM0150.COLNAME = "COL13" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL13
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL14" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL14
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL15" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL15
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL16" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL16
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL17" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL17
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL18" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL18
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL19" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL19
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL20" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL20
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If

								If orderedM0150.COLNAME = "COL21" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL21
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL22" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL22
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL23" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL23
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL24" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL24
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL25" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL25
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If

								'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
								If orderedM0150.COLNAME = "COL26" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL26
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL27" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL27
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL28" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL28
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL29" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL29
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL30" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL30
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL31" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL31
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL32" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL32
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL33" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL33
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									colValueList.Add(a0230)
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL34" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL34
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									colValueList.Add(a0230)
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL35" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL35
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL36" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL36
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL37" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL37
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If

								If orderedM0150.COLNAME = "COL38" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL38
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL39" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL39
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL40" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL40
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL41" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL41
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL42" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL42
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL43" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL43
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL44" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL44
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL45" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL45
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If

								If orderedM0150.COLNAME = "COL46" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL46
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL47" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL47
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL48" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL48
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									colValueList.Add(a0230)
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
								End If
								If orderedM0150.COLNAME = "COL49" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL49
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If
								If orderedM0150.COLNAME = "COL50" Then
									If orderedM0150.COLTYPE = "2" Then
										GetD0010FixColData(orderedM0150, d0010_EachRecord, a0230)
									Else
										a0230.ITEMNM = d0010_EachRecord.COL50
									End If
									a0230.COL_TYPE = orderedM0150.COLTYPE
									a0230.COL_NAME = orderedM0150.COLNAME
									a0230.GYOMNO = d0010_EachRecord.GYOMNO
									a0230.SPORTSUBCATCD = d0010_EachRecord.SPORTSUBCATCD
									a0230.HYOJ2 = orderedM0150.HYOJ
									a0230.RNZK = d0010_EachRecord.RNZK
									a0230.PGYOMNO = IIf(d0010_EachRecord.PGYOMNO Is Nothing, 0, d0010_EachRecord.PGYOMNO)
									colValueList.Add(a0230)
								End If

							Next
							records_d0010.Add(colValueList)
						Next
					Else
						'ASI[09 Dec 2019] : to create and append Date Header and Day Header in header caption list
						If m0140SubCatList.SPORTSUBCATCD = 0 AndAlso m0140SubCatList.M0150LIST(0).COLNAME = "DateHeader" Then
							Dim records_cols As New List(Of A0230)
							Dim a0230 As New A0230
							a0230.ITEMNM = SearchDtYerarMonth.AddDays(d).ToString("MM/dd")
							a0230.SPORTSUBCATCD = 0
							a0230.COL_NAME = "DateHeader"
							a0230.COL_TYPE = 4
							records_cols.Add(a0230)
							records_d0010.Add(records_cols)

						ElseIf m0140SubCatList.SPORTSUBCATCD = 0 AndAlso m0140SubCatList.M0150LIST(0).COLNAME = "DayHeader" Then
							Dim records_cols As New List(Of A0230)
							Dim a0230 As New A0230
							a0230.ITEMNM = SearchDtYerarMonth.AddDays(d).ToString("ddd")
							a0230.SPORTSUBCATCD = 0
							a0230.COL_NAME = "DayHeader"
							a0230.COL_TYPE = 4
							records_cols.Add(a0230)
							records_d0010.Add(records_cols)

						ElseIf m0140SubCatList.SPORTSUBCATCD = 0 Then

							Dim userId As String = m0140SubCatList.SELECTEDINDEX
							Dim c0040List = userListPersonalShiftData.Item(userId)
							Dim lstC0040DayWise = c0040List.Where(Function(m) m.GYOMDT = GYOYMD).OrderBy(Function(f) f.STTIME).ToList()
							Dim intCounter As Integer = 0
							For Each c0040_EachRecord In lstC0040DayWise
								intCounter = intCounter + 1
								Dim records_cols As New List(Of A0230)
								Dim a0230 As New A0230
								a0230.COL_TYPE = "3"
								a0230.SPORTSUBCATCD = 0
								a0230.BACKCOLOR = c0040_EachRecord.BACKCOLOR
								a0230.FONTCOLOR = c0040_EachRecord.FONTCOLOR
								a0230.KYUSHUTSU = c0040_EachRecord.KYUSHUTSU
								a0230.TITLEKBN = c0040_EachRecord.TITLEKBN
								a0230.KAKUNIN = c0040_EachRecord.KAKUNIN
								If c0040_EachRecord.KAKUNIN = "申請中" Then
									a0230.ITEMNM = "(申請中) " & c0040_EachRecord.BANGUMINM
								Else
									a0230.ITEMNM = c0040_EachRecord.BANGUMINM
								End If
								a0230.BANGUMINM = c0040_EachRecord.BANGUMINM
								a0230.WAKUCOLOR = c0040_EachRecord.WAKUCOLOR
								a0230.TOPWAKU = c0040_EachRecord.TOPWAKU
								a0230.BOTTOMWAKU = c0040_EachRecord.BOTTOMWAKU
								a0230.BOTTOMBLACKWAKU = c0040_EachRecord.BOTTOMBLACKWAKU
								a0230.ROWWAKUCOLOR = c0040_EachRecord.ROWWAKUCOLOR
								a0230.ITEMCD = c0040_EachRecord.USERID
								a0230.GYOMDT = c0040_EachRecord.GYOMDT

								If Session("LoginUserACCESSLVLCD") <> "4" Then
									a0230.DESKMEMOEXISTFLG = c0040_EachRecord.DESKMEMOEXISTFLG
								Else
									a0230.DESKMEMOEXISTFLG = False
								End If

								If a0230.DESKMEMOEXISTFLG = True Then
									If a0230.ROWWAKUCOLOR IsNot Nothing Then
										If intCounter = lstC0040DayWise.Count AndAlso a0230.BOTTOMWAKU Is Nothing Then
											a0230.DESKMEMOBOTTOMWAKU = a0230.ROWWAKUCOLOR
										End If
										If intCounter = 1 AndAlso a0230.TOPWAKU Is Nothing Then
											a0230.DESKMEMOTOPWAKU = a0230.ROWWAKUCOLOR
										End If
									End If
								End If

								'a0230.COL_TYPE = "3" So It will not Affect anywhere
								a0230.HYOJ2 = False
								a0230.HYOJJN = Short.Parse(m0140SubCatList.M0150LIST(0).HYOJJN.ToString)
								records_cols.Add(a0230)
								records_d0010.Add(records_cols)
							Next

							If lstC0040DayWise.Count > maxRowSpan Then
								maxRowSpan = lstC0040DayWise.Count
							End If

						Else
							Dim records_cols As New List(Of A0230)
							For j As Integer = 0 To m0140SubCatList.M0150LIST.COUNT - 1
								Dim a0230 As New A0230
								If (m0140SubCatList.M0150LIST(j).COLTYPE IsNot Nothing AndAlso m0140SubCatList.M0150LIST(j).COLTYPE <> "") Then a0230.COL_TYPE = m0140SubCatList.M0150LIST(j).COLTYPE
								a0230.ITEMNM = ""
								a0230.SPORTSUBCATCD = 0
								a0230.HYOJ2 = m0140SubCatList.M0150LIST(j).HYOJ
								records_cols.Add(a0230)
							Next
							records_d0010.Add(records_cols)
						End If

					End If
					records_subcat.Add(records_d0010)
				Next
				wholeRowsList.Add(records_subcat, maxRowSpan)
			Next

			If button = "downloadcsv" Then
				Return DownloadCsv(wholeRowsList, m0140List, SearchDtYerarMonth)
			End If

			ViewBag.tbodyData = wholeRowsList

			'To keep selected SPORTCATCD in DropDown
			ViewBag.SelectedSportCatCd = SportCatCd

			'Maintain state of radio after re search
			If String.IsNullOrEmpty(SearchType) Then
				ViewBag.SearchType = "1"
			Else
				ViewBag.SearchType = SearchType
			End If

			'Maintain state of radio after re search
			If String.IsNullOrEmpty(ShiftTableRadioType) Then
				ViewBag.ShiftTableRadioType = "0"
			Else
				ViewBag.ShiftTableRadioType = ShiftTableRadioType
			End If

			Return View()
		End Function

		<HttpPost()>
		Function Index(<Bind(Include:="GYOMNO,BANGUMINM,KSKJKNST,KSKJKNED,OAJKNST,OAJKNED,SAIJKNST,SAIJKNED,BASYO,BIKO,RNZK,PGYOMNO,COL01,COL02,COL03,COL04,COL05,COL06,COL07,COL08,COL09,COL10,
							COL11,COL12,COL13,COL14,COL15,COL16,COL17,COL18,COL19,COL20,COL21,COL22,COL23,COL24,COL25,COL26,COL27,COL28,COL29,COL30,COL31,COL32,COL33,COL34,COL35,COL36,COL37,
							COL38,COL39,COL40,COL41,COL42,COL43,COL44,COL45,COL46,COL47,COL48,COL49,COL50")> ByVal lstd0010 As List(Of D0010),
							ByVal SearchType As String, ByVal SportCatCd As String, ByVal Searchdt As String, ByVal BASYO As String, ByVal forUpdateOrInsert As String, ByVal lastForm As String, ByVal ShiftTableRadioType As String) As ActionResult

			If forUpdateOrInsert = "Update" Or forUpdateOrInsert = "Insert" Then
				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try
						Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
						sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version
						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

						If lstd0010 IsNot Nothing AndAlso lstd0010.Count > 0 Then
							For Each d0010ScreenObj In lstd0010
								Dim dbRecord As D0010 = db.D0010.Find(d0010ScreenObj.GYOMNO)
								Dim lstKarianaD0022 As New List(Of D0022)
								lstKarianaD0022 = db.D0022.Where(Function(d1) d1.GYOMNO = d0010ScreenObj.GYOMNO).ToList()
								For Each anaItem In lstKarianaD0022
									Dim dBUpdateOrDelete As String = ""
									If anaItem.COLNM = "COL01" Then
										If d0010ScreenObj.COL01 <> Nothing And d0010ScreenObj.COL01 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL01
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL02" Then
										If d0010ScreenObj.COL02 <> Nothing And d0010ScreenObj.COL02 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL02
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL03" Then
										If d0010ScreenObj.COL03 <> Nothing And d0010ScreenObj.COL03 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL03
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL04" Then
										If d0010ScreenObj.COL04 <> Nothing And d0010ScreenObj.COL04 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL04
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL05" Then
										If d0010ScreenObj.COL05 <> Nothing And d0010ScreenObj.COL05 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL05
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL06" Then
										If d0010ScreenObj.COL06 <> Nothing And d0010ScreenObj.COL06 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL06
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL07" Then
										If d0010ScreenObj.COL07 <> Nothing And d0010ScreenObj.COL07 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL07
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL08" Then
										If d0010ScreenObj.COL08 <> Nothing And d0010ScreenObj.COL08 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL08
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL09" Then
										If d0010ScreenObj.COL09 <> Nothing And d0010ScreenObj.COL09 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL09
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL10" Then
										If d0010ScreenObj.COL10 <> Nothing And d0010ScreenObj.COL10 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL10
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL11" Then
										If d0010ScreenObj.COL11 <> Nothing And d0010ScreenObj.COL11 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL11
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL12" Then
										If d0010ScreenObj.COL12 <> Nothing And d0010ScreenObj.COL12 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL12
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL13" Then
										If d0010ScreenObj.COL13 <> Nothing And d0010ScreenObj.COL13 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL13
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL14" Then
										If d0010ScreenObj.COL14 <> Nothing And d0010ScreenObj.COL14 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL14
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL15" Then
										If d0010ScreenObj.COL15 <> Nothing And d0010ScreenObj.COL15 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL15
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL16" Then
										If d0010ScreenObj.COL16 <> Nothing And d0010ScreenObj.COL16 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL16
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL17" Then
										If d0010ScreenObj.COL17 <> Nothing And d0010ScreenObj.COL17 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL17
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL18" Then
										If d0010ScreenObj.COL18 <> Nothing And d0010ScreenObj.COL18 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL18
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL19" Then
										If d0010ScreenObj.COL19 <> Nothing And d0010ScreenObj.COL19 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL19
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL20" Then
										If d0010ScreenObj.COL20 <> Nothing And d0010ScreenObj.COL20 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL20
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL21" Then
										If d0010ScreenObj.COL21 <> Nothing And d0010ScreenObj.COL21 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL21
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL22" Then
										If d0010ScreenObj.COL22 <> Nothing And d0010ScreenObj.COL22 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL22
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL23" Then
										If d0010ScreenObj.COL23 <> Nothing And d0010ScreenObj.COL23 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL23
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL24" Then
										If d0010ScreenObj.COL24 <> Nothing And d0010ScreenObj.COL24 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL24
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL25" Then
										If d0010ScreenObj.COL25 <> Nothing And d0010ScreenObj.COL25 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL25
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If

									'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
									If anaItem.COLNM = "COL26" Then
										If d0010ScreenObj.COL26 <> Nothing And d0010ScreenObj.COL26 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL26
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL27" Then
										If d0010ScreenObj.COL27 <> Nothing And d0010ScreenObj.COL27 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL27
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL28" Then
										If d0010ScreenObj.COL28 <> Nothing And d0010ScreenObj.COL28 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL28
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL29" Then
										If d0010ScreenObj.COL29 <> Nothing And d0010ScreenObj.COL29 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL29
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL30" Then
										If d0010ScreenObj.COL30 <> Nothing And d0010ScreenObj.COL30 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL30
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL31" Then
										If d0010ScreenObj.COL31 <> Nothing And d0010ScreenObj.COL31 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL31
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL32" Then
										If d0010ScreenObj.COL32 <> Nothing And d0010ScreenObj.COL32 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL32
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL33" Then
										If d0010ScreenObj.COL33 <> Nothing And d0010ScreenObj.COL33 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL33
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL34" Then
										If d0010ScreenObj.COL34 <> Nothing And d0010ScreenObj.COL34 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL34
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL35" Then
										If d0010ScreenObj.COL35 <> Nothing And d0010ScreenObj.COL35 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL35
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL36" Then
										If d0010ScreenObj.COL36 <> Nothing And d0010ScreenObj.COL36 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL36
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL37" Then
										If d0010ScreenObj.COL37 <> Nothing And d0010ScreenObj.COL37 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL37
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL38" Then
										If d0010ScreenObj.COL38 <> Nothing And d0010ScreenObj.COL38 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL38
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL39" Then
										If d0010ScreenObj.COL39 <> Nothing And d0010ScreenObj.COL39 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL39
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL40" Then
										If d0010ScreenObj.COL40 <> Nothing And d0010ScreenObj.COL40 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL40
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL41" Then
										If d0010ScreenObj.COL41 <> Nothing And d0010ScreenObj.COL41 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL41
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL42" Then
										If d0010ScreenObj.COL42 <> Nothing And d0010ScreenObj.COL42 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL42
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL43" Then
										If d0010ScreenObj.COL43 <> Nothing And d0010ScreenObj.COL43 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL43
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL44" Then
										If d0010ScreenObj.COL44 <> Nothing And d0010ScreenObj.COL44 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL44
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL45" Then
										If d0010ScreenObj.COL45 <> Nothing And d0010ScreenObj.COL45 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL45
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL46" Then
										If d0010ScreenObj.COL46 <> Nothing And d0010ScreenObj.COL46 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL46
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL47" Then
										If d0010ScreenObj.COL47 <> Nothing And d0010ScreenObj.COL47 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL47
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL48" Then
										If d0010ScreenObj.COL48 <> Nothing And d0010ScreenObj.COL48 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL48
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL49" Then
										If d0010ScreenObj.COL49 <> Nothing And d0010ScreenObj.COL49 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL49
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If
									If anaItem.COLNM = "COL50" Then
										If d0010ScreenObj.COL50 <> Nothing And d0010ScreenObj.COL50 <> "0" Then
											anaItem.USERID = d0010ScreenObj.COL50
											dBUpdateOrDelete = "update"
										Else
											db.D0022.Remove(anaItem)
										End If
									End If

									'D0022 Update, if dBUpdateOrDelete variable set means need to update otherwise it been remodev and need to saveChanges()
									If dBUpdateOrDelete = "update" Then
										db.Entry(anaItem).State = EntityState.Modified
									End If

									db.Configuration.ValidateOnSaveEnabled = False
									db.SaveChanges()
									db.Configuration.ValidateOnSaveEnabled = True
								Next
							Next
							tran.Commit()
						End If
					Catch ex As Exception
						Throw ex
						tran.Rollback()
					End Try
				End Using

			End If

			If forUpdateOrInsert = "Insert" Then
				Dim Cntkoukyu As Decimal = 0
				Dim Cnthoukyu As Decimal = 0
				Dim Cntdaikyu As Decimal = 0
				Dim Cntfurikyu As Decimal = 0
				Dim Cnt24koe As Decimal = 0
				Dim Cnt24koehoukyu As Decimal = 0
				Dim Cntkyokyu As Decimal = 0
				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try
						Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
						sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version
						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

						If lstd0010 IsNot Nothing Then

							lstd0010 = lstd0010.Where(Function(x) (x.RNZK = True AndAlso x.PGYOMNO <> 0) = False).ToList

							If lstd0010.Count > 0 Then

								For Each d0010ScreenObj In lstd0010

									Dim Count As Integer = 1
									Dim StrKoteiParentGyom As Decimal = 0

									Dim lstKarianaD0022 As New List(Of D0022)
									lstKarianaD0022 = db.D0022.Where(Function(d1) d1.GYOMNO = d0010ScreenObj.GYOMNO).ToList()

									For Each anaItem In lstKarianaD0022
										Count = 1
										Dim assssss = db.D0010.Where(Function(x) (x.GYOMNO = d0010ScreenObj.GYOMNO OrElse x.PGYOMNO = d0010ScreenObj.GYOMNO) AndAlso x.OYAGYOMFLG = True)
										For Each d0010parentChildRecord In db.D0010.Where(Function(x) (x.GYOMNO = d0010ScreenObj.GYOMNO OrElse x.PGYOMNO = d0010ScreenObj.GYOMNO) AndAlso x.OYAGYOMFLG = True).ToList

											Dim dbRecord As D0010 = db.D0010.Find(d0010parentChildRecord.GYOMNO)

											Dim m0010AnaTypeCheck As M0010 = db.M0010.Find(Integer.Parse(anaItem.USERID))
											'Dim d0030DataCheck = db.D0030.Find(Integer.Parse(anaItem.USERID), Integer.Parse(Date.Parse(dbRecord.GYOMYMD).ToString("yyyyMM")))
											If m0010AnaTypeCheck IsNot Nothing AndAlso m0010AnaTypeCheck.KARIANA <> True Then

												Dim intNengetsu As Integer = Integer.Parse(Date.Parse(dbRecord.GYOMYMD).ToString("yyyyMM"))
												Dim intHi As Integer = Integer.Parse(Date.Parse(dbRecord.GYOMYMD).ToString("dd"))

												'8:強休 9:時間強休
												Dim d0040Cnt = (db.D0040.Where(Function(t) t.USERID = anaItem.USERID And t.NENGETU = intNengetsu And t.HI = intHi And (t.KYUKCD = 8 Or t.KYUKCD = 9))).Count
												If d0040Cnt = 0 Then
													'D0010 Insert
													Dim newD0010Record As D0010 = New D0010()
													newD0010Record.GYOMNO = GetMaxGyomno() + Count
													newD0010Record.GYOMYMD = dbRecord.GYOMYMD
													newD0010Record.GYOMYMDED = dbRecord.GYOMYMDED
													newD0010Record.KSKJKNST = dbRecord.KSKJKNST
													newD0010Record.KSKJKNED = dbRecord.KSKJKNED
													newD0010Record.JTJKNST = dbRecord.JTJKNST
													newD0010Record.JTJKNED = dbRecord.JTJKNED
													newD0010Record.CATCD = dbRecord.CATCD
													newD0010Record.BANGUMINM = dbRecord.BANGUMINM
													newD0010Record.OAJKNST = dbRecord.OAJKNST
													newD0010Record.OAJKNED = dbRecord.OAJKNED
													newD0010Record.NAIYO = dbRecord.NAIYO
													newD0010Record.BASYO = dbRecord.BASYO
													newD0010Record.BIKO = dbRecord.BIKO
													newD0010Record.BANGUMITANTO = dbRecord.BANGUMITANTO
													newD0010Record.BANGUMIRENRK = dbRecord.BANGUMIRENRK
													newD0010Record.RNZK = dbRecord.RNZK
													newD0010Record.SPORT_OYAFLG = dbRecord.SPORT_OYAFLG
													If Count = 1 Then
														newD0010Record.PGYOMNO = d0010ScreenObj.GYOMNO
														StrKoteiParentGyom = newD0010Record.GYOMNO
													Else
														newD0010Record.PGYOMNO = StrKoteiParentGyom
													End If
													newD0010Record.IKTFLG = dbRecord.IKTFLG
													newD0010Record.IKTUSERID = dbRecord.IKTUSERID
													newD0010Record.IKKATUNO = dbRecord.IKKATUNO
													newD0010Record.SPORTFLG = dbRecord.SPORTFLG
													newD0010Record.OYAGYOMFLG = False
													newD0010Record.SPORTCATCD = dbRecord.SPORTCATCD
													newD0010Record.SPORTSUBCATCD = dbRecord.SPORTSUBCATCD
													newD0010Record.SAIJKNST = dbRecord.SAIJKNST
													newD0010Record.SAIJKNED = dbRecord.SAIJKNED

													newD0010Record.COL01 = dbRecord.COL01
													newD0010Record.COL02 = dbRecord.COL02
													newD0010Record.COL03 = dbRecord.COL03
													newD0010Record.COL04 = dbRecord.COL04
													newD0010Record.COL05 = dbRecord.COL05
													newD0010Record.COL06 = dbRecord.COL06
													newD0010Record.COL07 = dbRecord.COL07
													newD0010Record.COL08 = dbRecord.COL08
													newD0010Record.COL09 = dbRecord.COL09
													newD0010Record.COL10 = dbRecord.COL10
													newD0010Record.COL11 = dbRecord.COL11
													newD0010Record.COL12 = dbRecord.COL12
													newD0010Record.COL13 = dbRecord.COL13
													newD0010Record.COL14 = dbRecord.COL14
													newD0010Record.COL15 = dbRecord.COL15
													newD0010Record.COL16 = dbRecord.COL16
													newD0010Record.COL17 = dbRecord.COL17
													newD0010Record.COL18 = dbRecord.COL18
													newD0010Record.COL19 = dbRecord.COL19
													newD0010Record.COL20 = dbRecord.COL20
													newD0010Record.COL21 = dbRecord.COL21
													newD0010Record.COL22 = dbRecord.COL22
													newD0010Record.COL23 = dbRecord.COL23
													newD0010Record.COL24 = dbRecord.COL24
													newD0010Record.COL25 = dbRecord.COL25

													'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
													newD0010Record.COL26 = dbRecord.COL26
													newD0010Record.COL27 = dbRecord.COL27
													newD0010Record.COL28 = dbRecord.COL28
													newD0010Record.COL29 = dbRecord.COL29
													newD0010Record.COL30 = dbRecord.COL30
													newD0010Record.COL31 = dbRecord.COL31
													newD0010Record.COL32 = dbRecord.COL32
													newD0010Record.COL33 = dbRecord.COL33
													newD0010Record.COL34 = dbRecord.COL34
													newD0010Record.COL35 = dbRecord.COL35
													newD0010Record.COL36 = dbRecord.COL36
													newD0010Record.COL37 = dbRecord.COL37
													newD0010Record.COL38 = dbRecord.COL38
													newD0010Record.COL39 = dbRecord.COL39
													newD0010Record.COL40 = dbRecord.COL40
													newD0010Record.COL41 = dbRecord.COL41
													newD0010Record.COL42 = dbRecord.COL42
													newD0010Record.COL43 = dbRecord.COL43
													newD0010Record.COL44 = dbRecord.COL44
													newD0010Record.COL45 = dbRecord.COL45
													newD0010Record.COL46 = dbRecord.COL46
													newD0010Record.COL47 = dbRecord.COL47
													newD0010Record.COL48 = dbRecord.COL48
													newD0010Record.COL49 = dbRecord.COL49
													newD0010Record.COL50 = dbRecord.COL50

													db.D0010.Add(newD0010Record)

													'D0022 Delete
													Dim del_D0022 = db.D0022.Where(Function(d1) d1.GYOMNO = d0010parentChildRecord.GYOMNO AndAlso d1.COLNM = anaItem.COLNM).ToList()
													db.D0022.RemoveRange(del_D0022)

													'D0020 Insert
													Dim insertObjD0020 As D0020 = New D0020()
													insertObjD0020.GYOMNO = newD0010Record.GYOMNO
													insertObjD0020.USERID = anaItem.USERID
													insertObjD0020.COLNM = anaItem.COLNM
													insertObjD0020.CHK = False
													insertObjD0020.SOUSIN = False
													insertObjD0020.GYOMYMD = newD0010Record.GYOMYMD
													insertObjD0020.GYOMYMDED = newD0010Record.GYOMYMDED
													insertObjD0020.KSKJKNST = newD0010Record.KSKJKNST
													insertObjD0020.KSKJKNED = newD0010Record.KSKJKNED
													insertObjD0020.JTJKNST = newD0010Record.JTJKNST
													insertObjD0020.JTJKNED = newD0010Record.JTJKNED
													db.D0020.Add(insertObjD0020)

													Dim dtNow As Date = Now

													If newD0010Record.SPORT_OYAFLG = True Then

														'変更履歴の作成
														Dim d0070 As New D0070
														d0070.HENKORRKCD = GetMaxHenkorrkcd() + Count
														d0070.HENKONAIYO = "登録"
														d0070.USERID = Session("LoginUserid")
														d0070.SYUSEIYMD = dtNow
														d0070.TNTNM = m0010AnaTypeCheck.USERNM
														CopyHenkorrk(d0070, newD0010Record)
														db.D0070.Add(d0070)

													End If

													'D0040 Update 公休 → 公休出 etc

													Dim d0040 = (db.D0040.Where(Function(t) t.USERID = anaItem.USERID And t.NENGETU = intNengetsu And t.HI = intHi)).ToList


													Dim decNewKyukHenkorrkcd As Decimal = 0
													Dim strHENKONAIYO As String = ""

													If d0040.Count > 0 Then
														decNewKyukHenkorrkcd = GetMaxKyukHenkorrkcd() + Count
													End If

													If d0040.Count > 0 Then
														For Each row In d0040
															If row.KYUKCD = 4 Or row.KYUKCD = 5 Then
																row.KYUKCD = 2
																strHENKONAIYO = "変更"
																Cntkoukyu += 1

															ElseIf row.KYUKCD = 6 Then
																db.D0040.Remove(row)
																strHENKONAIYO = "削除"
																Cntdaikyu += 1

															ElseIf row.KYUKCD = 7 Then
																db.D0040.Remove(row)
																strHENKONAIYO = "削除"
																Cntfurikyu += 1

															ElseIf row.KYUKCD = 11 Or row.KYUKCD = 12 Then
																row.KYUKCD = 13
																strHENKONAIYO = "変更"
																Cnthoukyu += 1
															End If

															'休暇変更履歴作成
															If String.IsNullOrEmpty(strHENKONAIYO) = False Then
																Dim d0150 As New D0150
																d0150.HENKORRKCD = decNewKyukHenkorrkcd
																d0150.HENKONAIYO = strHENKONAIYO
																d0150.USERID = Session("LoginUserid")
																d0150.SYUSEIYMD = dtNow
																CopyKyukHenkorrk(d0150, row)
																db.D0150.Add(d0150)

																decNewKyukHenkorrkcd += 1
																strHENKONAIYO = ""
															End If
														Next
													End If

													'公休、振公休		→ 24時超え公休出 ,法休、振法休		→ 24時超え法休出
													strHENKONAIYO = ""
													Dim dtNext As Date = Date.Parse(dbRecord.GYOMYMDED).AddDays(1)
													If dbRecord.JTJKNED > dtNext Then
														intNengetsu = Integer.Parse(dtNext.ToString("yyyyMM"))
														intHi = Integer.Parse(dtNext.ToString("dd"))
														Dim d0040Next = (db.D0040.Where(Function(t) t.USERID = anaItem.USERID And t.NENGETU = intNengetsu And t.HI = intHi)).ToList

														If d0040Next.Count > 0 Then
															decNewKyukHenkorrkcd = GetMaxKyukHenkorrkcd() + Count
														End If

														If d0040Next.Count > 0 Then
															For Each row In d0040Next

																If row.KYUKCD = 4 Or row.KYUKCD = 5 Then
																	row.KYUKCD = 10
																	strHENKONAIYO = "変更"
																	Cnt24koe += 1
																ElseIf row.KYUKCD = 11 Or row.KYUKCD = 12 Then
																	row.KYUKCD = 14
																	strHENKONAIYO = "変更"
																	Cnt24koehoukyu += 1
																End If

																'休暇変更履歴作成
																If String.IsNullOrEmpty(strHENKONAIYO) = False Then
																	Dim d0150 As New D0150
																	d0150.HENKORRKCD = decNewKyukHenkorrkcd
																	d0150.HENKONAIYO = strHENKONAIYO
																	d0150.USERID = Session("LoginUserid")
																	d0150.SYUSEIYMD = dtNow
																	CopyKyukHenkorrk(d0150, row)
																	db.D0150.Add(d0150)

																	decNewKyukHenkorrkcd += 1
																	strHENKONAIYO = ""
																End If
															Next

														End If
													End If
												Else
													Cntkyokyu += 1
												End If
												If Count = 1 AndAlso d0040Cnt <> 0 Then
													Exit For
												End If
											End If

											Count = Count + 1
										Next
										db.Configuration.ValidateOnSaveEnabled = False
										db.SaveChanges()
										db.Configuration.ValidateOnSaveEnabled = True
									Next
								Next
								tran.Commit()
							End If
						End If
					Catch ex As Exception
						Throw ex
						tran.Rollback()
					End Try
				End Using

				TempData("Cntkoukyu") = Cntkoukyu
				TempData("Cntdaikyu") = Cntdaikyu
				TempData("Cntfurikyu") = Cntfurikyu
				TempData("Cnthoukyu") = Cnthoukyu
				TempData("Cnt24koe") = Cnt24koe
				TempData("Cnt24koehoukyu") = Cnt24koehoukyu
				TempData("Cntkyokyu") = Cntkyokyu
				If (Cntkoukyu > 0 OrElse Cntdaikyu > 0 OrElse Cntfurikyu > 0 OrElse Cnthoukyu > 0 OrElse Cnt24koe > 0 OrElse Cnt24koehoukyu > 0 OrElse Cntkyokyu > 0) Then
					TempData("saveSuccess") = 1
				Else
					TempData("saveSuccess") = 0
				End If



				'Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
				'sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

				'Using tran As DbContextTransaction = db.Database.BeginTransaction
				'	Try
				'		Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)
				'		db.Configuration.ValidateOnSaveEnabled = False
				'		db.SaveChanges()
				'		db.Configuration.ValidateOnSaveEnabled = True
				'		tran.Commit()
				'	Catch ex As Exception
				'		Throw ex
				'		tran.Rollback()
				'	End Try
				'End Using
			Else
				'CSV

			End If

			Return RedirectToAction("Index", New With {.SearchType = SearchType, .SportCatCd = SportCatCd, .Searchdt = Searchdt, .lastForm = lastForm, .ShiftTableRadioType = ShiftTableRadioType})
		End Function

		Private Function GetUserName(id As Short) As String

			Dim name As String = ""
			name = (From m1 In db.M0010
					Where m1.USERID = id
					Select m1.USERNM).SingleOrDefault

			Return name

		End Function

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

		'Private Function GetFixGyomno(number As String, colnm As String) As String
		'	Dim FIXGYOMNO As String = ""
		'	Dim d0022 = (From m1 In db.D0022
		'				 Where m1.GYOMNO = number AndAlso
		'							m1.COLNM = colnm
		'				 Select m1).FirstOrDefault
		'	If d0022 IsNot Nothing Then
		'		FIXGYOMNO = d0022.FIX_GYOMNO.ToString
		'	End If
		'	Return FIXGYOMNO.ToString
		'End Function

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

		Private Function GetMaxGyomno() As Decimal
			'業務番号の最大値の取得
			Dim decMaxGyomno As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "0000")
			Dim lstgyomno = (From t In db.D0010 Where t.GYOMNO > decMaxGyomno Select t.GYOMNO).ToList
			If lstgyomno.Count > 0 Then
				decMaxGyomno = lstgyomno.Max
			End If

			Return decMaxGyomno
		End Function
		Private Function GetWBookingStatus(fixGyomNO As Decimal, gyoYMD As Date, userID As Short) As Short

			Dim childConfirmGyomno = (From d1 In db.D0010 Where (d1.GYOMNO = fixGyomNO Or d1.PGYOMNO = fixGyomNO) And d1.GYOMYMD = gyoYMD Select d1.GYOMNO).FirstOrDefault

			Dim fixd0010 = db.D0010.Find(childConfirmGyomno)
			Dim JTJKNST As Date = fixd0010.JTJKNST
			Dim JTJKNED As Date = fixd0010.JTJKNED

			If fixd0010.RNZK = True And ((fixd0010.SPORTFLG = True And fixd0010.SPORT_OYAFLG = True) Or (fixd0010.SPORTFLG = False And fixd0010.PGYOMNO Is Nothing)) Then
				JTJKNED = GetJtjkn(fixd0010.GYOMYMD, "2400")
			End If

			JTJKNST = JTJKNST.AddSeconds(1)
			JTJKNED = JTJKNED.AddSeconds(-1)

			Dim d0010lstWbook = (From d1 In db.D0010 Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
								 Where (d1.GYOMNO <> childConfirmGyomno And d1.GYOMNO <> fixGyomNO) And
									   ((JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED) OrElse
										(JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED) OrElse
										(d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED) OrElse
										(d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED)) And
										d2.USERID = userID And d1.OYAGYOMFLG = False And
									 (d1.SPORTFLG = False Or (d1.SPORTFLG = True And d1.SPORT_OYAFLG = True))).ToList

			Return d0010lstWbook.Count

		End Function

		Private Function DownloadCsv(ByVal datalist As Dictionary(Of ICollection, Integer), headerlist As List(Of M0140), ByVal SearchDtYerarMonth As Date) As ActionResult

			Dim builder As New StringBuilder()
			Dim Days As String() = {"日", "月", "火", "水", "木", "金", "土"}
			Dim SearchDt As Date = Nothing
			Dim strRecord As String = "日付,曜,"
			Dim strFirstRecord As String = ",,"
			Dim Countervar As Integer = 0
			If headerlist IsNot Nothing AndAlso headerlist.Count > 0 Then
				Dim bolUserHeaderWrite As Boolean = False
				For Each item In headerlist
					Dim bolSubCatWrite As Boolean = False
					If item.SPORTSUBCATCD <> 0 Then
						For Each item2 In item.M0150LIST
							If bolSubCatWrite = False Then
								strFirstRecord = strFirstRecord & item.SPORTSUBCATNM & ","
								bolSubCatWrite = True
							Else
								strFirstRecord = strFirstRecord & ","
							End If
						Next
					Else
						If bolUserHeaderWrite = False Then
							strFirstRecord = strFirstRecord & "シフト表" & ","
							bolUserHeaderWrite = True
						Else
							strFirstRecord = strFirstRecord & ","
						End If
					End If
				Next
			End If
			strFirstRecord = strFirstRecord.Substring(0, strFirstRecord.Length - 1)
			builder.AppendLine(strFirstRecord)

			If headerlist IsNot Nothing AndAlso headerlist.Count > 0 Then
				For Each item In headerlist
					'If item.SPORTSUBCATCD <> 0 Then
					For Each item2 In item.M0150LIST
						strRecord = strRecord & item2.COLVALUE & ","
					Next
					'End If
				Next
			End If
			strRecord = strRecord.Substring(0, strRecord.Length - 1)
			builder.AppendLine(strRecord)

			For Each dayItem In datalist

				SearchDt = SearchDtYerarMonth.AddDays(Countervar).ToString("yyyy/MM/dd")
				Dim HI_Date As String
				Dim YOBI As String
				Dim rowSpan = 0
				HI_Date = SearchDt.ToString("MM/dd")
				YOBI = Days(SearchDt.DayOfWeek)
				'<tr>
				strRecord = HI_Date & "," & YOBI & ","
				If dayItem.Key IsNot Nothing AndAlso dayItem.Key.Count > 0 Then
					rowSpan = dayItem.Value
					Dim d0010Cnt = 0
					Dim isRowSpan = 0
					Dim Cnt = 1
					For i = 0 To dayItem.Key.Count - 1
						Dim subCatItem As ICollection = dayItem.Key(i)
						Dim d0010Item As ICollection = subCatItem(d0010Cnt)
						If d0010Item IsNot Nothing AndAlso d0010Item.Count > 0 Then

							Dim maxRow = subCatItem.Count
							If maxRow > 1 Then
								isRowSpan = 1
							End If

							If i = 0 AndAlso d0010Cnt > 0 AndAlso Cnt < rowSpan Then
								'Html.Raw("<tr>")
								strRecord = HI_Date & "," & YOBI & ","
								Cnt = Cnt + 1
							End If

							For Each colItem As A0230 In d0010Item
								'If colItem.COL_TYPE <> "3" Then
								If colItem.ITEMNM <> "" Then
									strRecord = strRecord & colItem.ITEMNM & ","
								Else
									strRecord = strRecord & "" & ","
								End If
								'End If
							Next

							If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 Then
								'Html.Raw("</tr>")
								strRecord = strRecord.Substring(0, strRecord.Length - 1)
								builder.AppendLine(strRecord)
							End If
						ElseIf isRowSpan = 1 Then
							'Toset blnk cell same as row span
							Dim d0010ItemEmpty As ICollection = subCatItem(0)
							If d0010ItemEmpty IsNot Nothing AndAlso d0010ItemEmpty.Count > 0 Then
								If i = 0 AndAlso d0010Cnt > 0 AndAlso Cnt < rowSpan Then
									'Html.Raw("<tr>")
									strRecord = HI_Date & "," & YOBI & ","
									Cnt = Cnt + 1
								End If
								For Each colItem As A0230 In d0010ItemEmpty
									'If colItem.COL_TYPE <> "3" Then
									strRecord = strRecord & "" & ","
									'End If
								Next
								If i = dayItem.Key.Count - 1 Then
									'Html.Raw("</tr>")
									strRecord = strRecord.Substring(0, strRecord.Length - 1)
									builder.AppendLine(strRecord)
								End If
							End If
						End If
						If isRowSpan > 0 AndAlso i = dayItem.Key.Count - 1 AndAlso d0010Cnt < rowSpan - 1 Then
							i = -1
							d0010Cnt = d0010Cnt + 1
						End If
					Next
				End If
				'</tr>
				If rowSpan < 2 Then
					strRecord = strRecord.Substring(0, strRecord.Length - 1)
					builder.AppendLine(strRecord)
				End If
				Countervar = Countervar + 1
			Next

			' 生成された文字列を「text/csv」形式（Shift_JIS）で出力
			Return File(System.Text.Encoding.GetEncoding("shift_jis").GetBytes(builder.ToString), "text/csv", "sportshiftdata.csv")
		End Function
		Private Sub CopyKyukHenkorrk(ByRef d0150 As D0150, ByVal d0040 As D0040)
			Dim strNENGETU As String = d0040.NENGETU.ToString()
			Dim dtYMD As Date = Date.Parse(strNENGETU.Substring(0, 4) & "/" & strNENGETU.Substring(4, 2) & "/" & d0040.HI.ToString)
			d0150.KKNST = dtYMD
			d0150.KKNED = dtYMD
			d0150.JKNST = d0040.JKNST
			d0150.JKNED = d0040.JKNED
			d0150.SHINSEIUSER = db.M0010.Find(d0040.USERID).USERNM
			d0150.KYUKNM = db.M0060.Find(d0040.KYUKCD).KYUKNM
			d0150.GYOMMEMO = d0040.GYOMMEMO
		End Sub

		Function GetMaxKyukHenkorrkcd() As Decimal
			'休暇変更履歴コードの最大値の取得
			Dim decMaxHenkorrkcd As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "00000")
			Dim lstHENKORRKCD = (From t In db.D0150 Where t.HENKORRKCD > decMaxHenkorrkcd Select t.HENKORRKCD).ToList
			If lstHENKORRKCD.Count > 0 Then
				decMaxHenkorrkcd = lstHENKORRKCD.Max
			End If

			Return decMaxHenkorrkcd
		End Function
		Private Sub CopyHenkorrk(ByRef d0070 As D0070, ByVal d0010 As D0010)
			d0070.GYOMYMD = d0010.GYOMYMD
			d0070.GYOMYMDED = d0010.GYOMYMDED
			d0070.KSKJKNST = d0010.KSKJKNST
			d0070.KSKJKNED = d0010.KSKJKNED
			d0070.CATCD = d0010.CATCD
			d0070.BANGUMINM = d0010.BANGUMINM
			d0070.OAJKNST = d0010.OAJKNST
			d0070.OAJKNED = d0010.OAJKNED
			d0070.NAIYO = d0010.NAIYO
			d0070.BASYO = d0010.BASYO
			d0070.BIKO = d0010.BIKO
			d0070.BANGUMITANTO = d0010.BANGUMITANTO
			d0070.BANGUMIRENRK = d0010.BANGUMIRENRK
			d0070.GYOMNO = d0010.GYOMNO
			d0070.SPORTCATCD = d0010.SPORTCATCD
			d0070.SPORTSUBCATCD = d0010.SPORTSUBCATCD
			d0070.SAIJKNST = d0010.SAIJKNST
			d0070.SAIJKNED = d0010.SAIJKNED
			d0070.COL01 = d0010.COL01
			d0070.COL02 = d0010.COL02
			d0070.COL03 = d0010.COL03
			d0070.COL04 = d0010.COL04
			d0070.COL05 = d0010.COL05
			d0070.COL06 = d0010.COL06
			d0070.COL07 = d0010.COL07
			d0070.COL08 = d0010.COL08
			d0070.COL09 = d0010.COL09
			d0070.COL10 = d0010.COL10
			d0070.COL11 = d0010.COL11
			d0070.COL12 = d0010.COL12
			d0070.COL13 = d0010.COL13
			d0070.COL14 = d0010.COL14
			d0070.COL15 = d0010.COL15
			d0070.COL16 = d0010.COL16
			d0070.COL17 = d0010.COL17
			d0070.COL18 = d0010.COL18
			d0070.COL19 = d0010.COL19
			d0070.COL20 = d0010.COL20
			d0070.COL21 = d0010.COL21
			d0070.COL22 = d0010.COL22
			d0070.COL23 = d0010.COL23
			d0070.COL24 = d0010.COL24
			d0070.COL25 = d0010.COL25

			'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
			d0070.COL26 = d0010.COL26
			d0070.COL27 = d0010.COL27
			d0070.COL28 = d0010.COL28
			d0070.COL29 = d0010.COL29
			d0070.COL30 = d0010.COL30
			d0070.COL31 = d0010.COL31
			d0070.COL32 = d0010.COL32
			d0070.COL33 = d0010.COL33
			d0070.COL34 = d0010.COL34
			d0070.COL35 = d0010.COL35
			d0070.COL36 = d0010.COL36
			d0070.COL37 = d0010.COL37
			d0070.COL38 = d0010.COL38
			d0070.COL39 = d0010.COL39
			d0070.COL40 = d0010.COL40
			d0070.COL41 = d0010.COL41
			d0070.COL42 = d0010.COL42
			d0070.COL43 = d0010.COL43
			d0070.COL44 = d0010.COL44
			d0070.COL45 = d0010.COL45
			d0070.COL46 = d0010.COL46
			d0070.COL47 = d0010.COL47
			d0070.COL48 = d0010.COL48
			d0070.COL49 = d0010.COL49
			d0070.COL50 = d0010.COL50

		End Sub
		Private Function GetMaxHenkorrkcd() As Decimal
			'変更履歴コードの最大値の取得
			Dim decMaxHenkorrkcd As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "00000")
			Dim lstHENKORRKCD = (From t In db.D0070 Where t.HENKORRKCD > decMaxHenkorrkcd Select t.HENKORRKCD).ToList
			If lstHENKORRKCD.Count > 0 Then
				decMaxHenkorrkcd = lstHENKORRKCD.Max
			End If

			Return decMaxHenkorrkcd
		End Function

		Private Function GetBGColor(gyomNO As Decimal, gyoYMD As Date, userID As Short, colnm As String, JTJKNST As Date, JTJKNED As Date) As String
			Dim recCnt As Integer = 0
			Dim BGColor As String = ""
			JTJKNST = JTJKNST.AddSeconds(1)
			JTJKNED = JTJKNED.AddSeconds(-1)
			If (Session("LoginUserACCESSLVLCD") <> "3") Then
				Dim d0010SubSelect = (From d1 In db.D0010
									  Join d2 In db.D0022 On d1.GYOMNO Equals d2.GYOMNO
									  Where (d1.GYOMNO <> gyomNO OrElse (d1.GYOMNO = gyomNO AndAlso d2.COLNM <> colnm)) AndAlso
											  d1.SPORTFLG = True AndAlso
											  d1.OYAGYOMFLG = True AndAlso
											  d2.USERID = userID
									  Select New With {d1.JTJKNST, .JTJKNED = If(d1.RNZK = True And d1.PGYOMNO Is Nothing, DbFunctions.AddDays(d1.GYOMYMD, 1), d1.JTJKNED)})

				recCnt = (From d1 In d0010SubSelect
						  Where ((JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED) Or
								(JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED) Or
								(d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED) Or
								(d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED))
						  Select d1).Count()
			End If

			If recCnt > 0 Then
				BGColor = "ffad33"  'Orange color
			Else
				If (Session("LoginUserACCESSLVLCD") <> "3" OrElse (Session("LoginUserACCESSLVLCD") = "3" AndAlso ViewBag.CHIEF_CAT = 1)) Then

					recCnt = (From d1 In db.D0010
							  Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
							  Where ((JTJKNST >= d1.JTJKNST And JTJKNST <= d1.JTJKNED) Or
									  (JTJKNED >= d1.JTJKNST And JTJKNED <= d1.JTJKNED) Or
									  (d1.JTJKNST >= JTJKNST And d1.JTJKNST <= JTJKNED) Or
									  (d1.JTJKNED >= JTJKNST And d1.JTJKNED <= JTJKNED)) AndAlso
										d1.OYAGYOMFLG = False AndAlso
								   d2.USERID = userID
							  Select d1).Count()
					If recCnt > 0 Then
						BGColor = "fec5e5"  'Pink color
					Else
						Dim strGyomYmd As String = gyoYMD.ToString.Replace("/", "")
						Dim intGyomYmd_YM As Integer = Integer.Parse(strGyomYmd.Substring(0, 6))

						recCnt = (From d40 In db.D0040
								  Where d40.USERID = userID AndAlso
								  d40.NENGETU = intGyomYmd_YM AndAlso
								  d40.HI = strGyomYmd.Substring(6, 2) AndAlso
								  d40.KYUKCD = 8).Count
						If recCnt > 0 Then
							BGColor = "fec5e5"  'Pink color
						Else

							recCnt = (From d40 In db.D0040
									  Where d40.USERID = userID AndAlso
									  d40.NENGETU = intGyomYmd_YM AndAlso
									  d40.HI = strGyomYmd.Substring(6, 2) AndAlso
									  d40.KYUKCD = 9 AndAlso
									  ((JTJKNST >= d40.JTJKNST And JTJKNST <= d40.JTJKNED) Or
									  (JTJKNED >= d40.JTJKNST And JTJKNED <= d40.JTJKNED) Or
									  (d40.JTJKNST >= JTJKNST And d40.JTJKNST <= JTJKNED) Or
									  (d40.JTJKNED >= JTJKNST And d40.JTJKNED <= JTJKNED))).Count

							If recCnt > 0 Then
								BGColor = "fec5e5"  'Pink color
							End If
						End If
					End If
				End If
			End If

			Return BGColor

		End Function

		Private Function GetD0010FixColData(orderedM0150 As Object, d0010_EachRecord As D0010, ByRef a0230 As A0230) As A0230

			Dim userid As Short = 0
			Dim FIX_GYOMNO = ""
			Dim kariUserNm As String = ""
			userid = GetD0022Ana(d0010_EachRecord.GYOMNO, orderedM0150.COLNAME)
			Dim anaList As New List(Of A0240ANALIST)
			Dim cnt = 0
			Dim d0020 = GetD0020Ana(If(d0010_EachRecord.PGYOMNO IsNot Nothing, d0010_EachRecord.PGYOMNO, d0010_EachRecord.GYOMNO), orderedM0150.COLNAME)
			If d0020 IsNot Nothing AndAlso d0020.Count > 0 Then
				If userid = 0 Then
					userid = d0020(0).USERID
					FIX_GYOMNO = d0020(0).GYOMNO
					cnt = 1
				End If
				If d0020.Count > cnt Then
					For i = cnt To d0020.Count - 1
						Dim a0240ANALIST30 As New A0240ANALIST
						Dim userid1 As Short = d0020(i).USERID
						Dim UserName1 As String = GetUserName(userid1)
						Dim WBookingStatus1 As Short = 0
						Dim LINKCOLOR1 As String = ""
						Dim FIX_GYOMNO1 As String = d0020(i).GYOMNO.ToString
						If FIX_GYOMNO1 <> "" AndAlso ((Session("LoginUserACCESSLVLCD") <> "4" AndAlso Session("LoginUserACCESSLVLCD") <> "3") OrElse (Session("LoginUserACCESSLVLCD") = "3" AndAlso ViewBag.CHIEF_CAT = 1)) Then
							WBookingStatus1 = GetWBookingStatus(FIX_GYOMNO1, d0010_EachRecord.GYOMYMD, userid1)
						End If
						If FIX_GYOMNO1 = "" Then
							LINKCOLOR1 = LINK_COLOR1
						ElseIf WBookingStatus1 > 0 Then
							LINKCOLOR1 = LINK_COLOR2
						Else
							LINKCOLOR1 = LINK_COLOR3
						End If
						a0240ANALIST30.DESKMEMOEXISTFLG = False
						If userid1 <> 0 Then
							a0240ANALIST30.DESKMEMOEXISTFLG = CheckDeskMemo(userid1, d0010_EachRecord.GYOMYMD)
							a0240ANALIST30.GYOMDT = d0010_EachRecord.GYOMYMD
						End If

						a0240ANALIST30.ITEMCD = userid1.ToString
						a0240ANALIST30.ITEMNM = UserName1
						a0240ANALIST30.LINKCOLOR = LINKCOLOR1
						a0240ANALIST30.FIX_GYOMNO = FIX_GYOMNO1
						anaList.Add(a0240ANALIST30)
					Next
				End If
			End If
			cnt = 0
			Dim d0021 = GetD0021KariAna(If(d0010_EachRecord.PGYOMNO IsNot Nothing, d0010_EachRecord.PGYOMNO, d0010_EachRecord.GYOMNO), orderedM0150.COLNAME)
			If d0021 IsNot Nothing AndAlso d0021.Count > 0 Then
				If userid = 0 Then
					FIX_GYOMNO = d0021(0).GYOMNO
					kariUserNm = d0021(0).ANNACATNM
					cnt = 1
				End If
				If d0021.Count > cnt Then
					For i = cnt To d0021.Count - 1
						Dim a0240ANALIST30 As New A0240ANALIST
						a0240ANALIST30.ITEMCD = 0
						a0240ANALIST30.ITEMNM = d0021(i).ANNACATNM
						a0240ANALIST30.LINKCOLOR = "black"
						a0240ANALIST30.FIX_GYOMNO = d0021(i).GYOMNO.ToString
						anaList.Add(a0240ANALIST30)
					Next
				End If
			End If
			a0230.ITEMCD = userid

			' Perform Buiness when type is anna
			Dim m0010 = db.M0010.Find(userid)
			Dim UserName As String = ""
			If m0010 IsNot Nothing Then
				UserName = m0010.USERNM
			End If
			a0230.DESKMEMOEXISTFLG = False
			If FIX_GYOMNO = "" AndAlso (Session("LoginUserACCESSLVLCD") = "4" OrElse (Session("LoginUserACCESSLVLCD") = "3" AndAlso ViewBag.CHIEF_CAT = 0)) Then
				a0230.ITEMNM = If(UserName <> "", "仮アナ", "")
			ElseIf userid = 0 AndAlso d0021 IsNot Nothing AndAlso d0021.Count > 0 Then
				a0230.ITEMNM = kariUserNm
			Else
				a0230.ITEMNM = UserName
				If userid <> 0 Then
					a0230.DESKMEMOEXISTFLG = CheckDeskMemo(userid, d0010_EachRecord.GYOMYMD)
					a0230.GYOMDT = d0010_EachRecord.GYOMYMD
				End If
			End If

			Dim BGCOLOR As String = ""
			If FIX_GYOMNO = "" AndAlso m0010 IsNot Nothing AndAlso m0010.KARIANA = False AndAlso Session("LoginUserACCESSLVLCD") <> "4" Then
				Dim JTJKNED As Date = d0010_EachRecord.JTJKNED
				If d0010_EachRecord.RNZK = True AndAlso d0010_EachRecord.PGYOMNO Is Nothing Then
					JTJKNED = GetJtjkn(d0010_EachRecord.GYOMYMD, "2400")
				End If
				BGCOLOR = GetBGColor(d0010_EachRecord.GYOMNO, d0010_EachRecord.GYOMYMD, userid, orderedM0150.COLNAME, d0010_EachRecord.JTJKNST, JTJKNED)
			End If
			a0230.BGCOLOR = BGCOLOR
			'Dim FIX_GYOMNO = GetFixGyomno(d0010_EachRecord.GYOMNO, orderedM0150.COLNAME)
			Dim WBookingStatus As Short = 0
			If FIX_GYOMNO <> "" AndAlso userid <> 0 AndAlso ((Session("LoginUserACCESSLVLCD") <> "4" AndAlso Session("LoginUserACCESSLVLCD") <> "3") OrElse (Session("LoginUserACCESSLVLCD") = "3" AndAlso ViewBag.CHIEF_CAT = 1)) Then
				WBookingStatus = GetWBookingStatus(FIX_GYOMNO, d0010_EachRecord.GYOMYMD, userid)
			End If
			If FIX_GYOMNO = "" Then
				a0230.LINKCOLOR = LINK_COLOR1
			ElseIf WBookingStatus > 0 Then
				a0230.LINKCOLOR = LINK_COLOR2
			Else
				a0230.LINKCOLOR = LINK_COLOR3
			End If
			a0230.FIX_GYOMNO = FIX_GYOMNO
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

		Private Function CheckDeskMemo(ByVal userid As Short, ByVal HI_Date As Date) As Boolean

			Dim DESKMEMOEXISTFLG As Boolean = False

			Dim deskMemoCount = (From d1 In db.D0120 Join d2 In db.D0130
								On d1.DESKNO Equals d2.DESKNO
								 Join d3 In db.D0110 On d2.DESKNO Equals d3.DESKNO
								 Where d2.USERID = userid And d3.KAKUNINID <> 5 And
								(HI_Date >= d1.SHIFTYMDST And HI_Date <= d1.SHIFTYMDED
								) Select d1.DESKNO).Count

			If deskMemoCount > 0 Then
				DESKMEMOEXISTFLG = True
			Else
				DESKMEMOEXISTFLG = False
			End If

			Return DESKMEMOEXISTFLG
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