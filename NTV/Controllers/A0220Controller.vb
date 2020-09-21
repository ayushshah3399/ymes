Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
    Public Class A0220Controller
        Inherits Controller

        Private db As New Model1

        Private Function ReturnLoginPartial() As ActionResult
            ViewData!ID = "Login"
            Return PartialView("_LoginPartial")
        End Function

        Private Function CheckAccessLvl() As Boolean
            Dim loginUserKanri As Boolean = Session("LoginUserKanri")
            Dim loginUserSystem As Boolean = Session("LoginUserSystem")
            If Not loginUserKanri AndAlso Not loginUserSystem Then
                Return False
            End If

            Return True
        End Function

        ' GET: A0220
        Function Index() As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.AbsoluteUri      '休日設定画面から来た時アナ名が文字化けするので、Encodeされている元のUrlを取得
				If Not strUrlReferrer.Contains("/A0220") Then
					Session("UrlReferrer") = strUrlReferrer
				End If
			End If

			Dim lstSportCatNm = Nothing

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) Then
				If Session("LoginUserDeskChief") = 0 Then
					Return View("ErrorAccesslvl")
				Else
					lstSportCatNm = (From m In db.M0130 Join m160 In db.M0160 On m.SPORTCATCD Equals m160.SPORTCATCD
									 Where m.HYOJ = True And m160.USERID = loginUserId And m160.CHIEFFLG = True Select m).OrderBy(Function(m) m.HYOJJN).ToList
				End If
			Else
				lstSportCatNm = db.M0130.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			End If

			Dim blank_entry As New M0130
            blank_entry.SPORTCATCD = 0
            blank_entry.SPORTCATNM = ""
            lstSportCatNm.Insert(0, blank_entry)
            ViewBag.SportCatNmList = lstSportCatNm
            ViewBag.SportSubCatNmList = New SelectList("", "SPORTSUBCATCD", "SPORTSUBCATNM")
            Return View()

        End Function

        'POST: D0010
        <HttpPost()>
        Function Index(screenObjM0150 As M0150) As ActionResult

            Return RedirectToAction("Create", routeValues:=New With {.SPORTCATCD = screenObjM0150.SPORTCATCD, .SPORTSUBCATCD = screenObjM0150.SPORTSUBCATCD})
        End Function

        Function Create(SPORTCATCD As String, SPORTSUBCATCD As String) As ActionResult

            If IsNothing(SPORTCATCD) Or IsNothing(SPORTSUBCATCD) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
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

			Dim lstSportCatNm = db.M0130.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            Dim blank_entry As New M0130
            blank_entry.SPORTCATCD = 0
            blank_entry.SPORTCATNM = ""
            lstSportCatNm.Insert(0, blank_entry)
            ViewBag.SportCatNmList = lstSportCatNm

            Dim lstSportSubCatNm = db.M0140.Where(Function(m) m.HYOJ = True).OrderBy(Function(f) f.HYOJJN).ToList
            Dim blank_entry_subCatNm As New M0140
            blank_entry_subCatNm.SPORTSUBCATCD = 0
            blank_entry_subCatNm.SPORTSUBCATNM = ""
            lstSportSubCatNm.Insert(0, blank_entry_subCatNm)
            ViewBag.SportSubCatNmList = lstSportSubCatNm

            'Dim lstbangumi = db.M0030.OrderBy(Function(f) f.BANGUMIKN).ToList
            'Dim bangumiitem As New M0030
            'bangumiitem.BANGUMICD = "0"
            'bangumiitem.BANGUMINM = ""
            'lstbangumi.Insert(0, bangumiitem)
            'ViewBag.BangumiList = lstbangumi

            'Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            'Fetch userNm whose placed under KARIANA true and non deleted whose are registered with sportcatcd in M0160 tbl
            Dim m0010List = From m In db.M0010 Select m
            ViewBag.lstmsterm0010 = m0010List.ToList
            m0010List = m0010List.Where(Function(d1) db.M0160.Where(Function(m) m.SPORTCATCD = SPORTCATCD AndAlso d1.STATUS = True AndAlso d1.HYOJ = True).Select(Function(t2) t2.USERID).Contains(d1.USERID) Or (d1.KARIANA = True And d1.STATUS = True))

            'Dim lstUSERID = m0010List.OrderByDescending(Function(x) x.HYOJJN).ToList
            Dim lstUSERID = m0010List.OrderByDescending(Function(x) x.KARIANA = True).ThenBy(Function(x) x.HYOJJN).ToList
            Dim itemm0010 As New M0010
            itemm0010.USERID = "0"
            itemm0010.USERNM = ""
            lstUSERID.Insert(0, itemm0010)
            ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM", lstUSERID(1))
            ViewBag.lstM0010 = lstUSERID

            Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
            Dim naiyoitem As New M0040
            naiyoitem.NAIYOCD = "0"
            naiyoitem.NAIYO = ""
            lstNaiyo.Insert(0, naiyoitem)
            ViewBag.NaiyouList = lstNaiyo

            Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            Dim item1 As New M0020
            item1.CATCD = "0"
            item1.CATNM = ""
            lstm0020.Insert(0, item1)
            ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM")

            Dim d0010 As D0010 = New D0010()
            d0010.SPORTCATCD = SPORTCATCD
            d0010.SPORTSUBCATCD = SPORTSUBCATCD

            Dim listFreeItem As New List(Of String)
            Dim listAnaItem As New List(Of String)

            Dim m0150 As M0150 = db.M0150.Where(Function(m) m.SPORTCATCD = SPORTCATCD And m.SPORTSUBCATCD = SPORTSUBCATCD).FirstOrDefault

            If m0150 IsNot Nothing Then
                If m0150.COL01_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL01)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL01_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL01)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL02_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL02)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL02_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL02)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL03_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL03)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL03_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL03)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL04_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL04)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL04_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL04)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL05_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL05)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL05_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL05)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL06_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL06)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL06_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL06)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL07_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL07)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL07_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL07)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL08_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL08)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL08_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL08)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL09_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL09)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL09_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL09)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL10_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL10)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL10_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL10)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL11_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL11)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL11_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL11)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL12_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL12)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL12_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL12)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL13_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL13)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL13_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL13)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL14_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL14)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL14_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL14)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL15_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL15)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL15_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL15)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL16_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL16)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL16_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL16)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL17_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL17)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL17_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL17)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL18_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL18)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL18_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL18)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL19_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL19)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL19_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL19)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL20_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL20)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL20_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL20)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If


                If m0150.COL21_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL21)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL21_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL21)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If


                If m0150.COL22_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL22)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL22_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL22)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If


                If m0150.COL23_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL23)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL23_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL23)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If


                If m0150.COL24_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL24)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL24_TYPE = "2" Then
                    listAnaItem.Add(m0150.COL24)
                    listFreeItem.Add(Nothing)
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If


				If m0150.COL25_TYPE = "1" Then
					listFreeItem.Add(m0150.COL25)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL25_TYPE = "2" Then
					listAnaItem.Add(m0150.COL25)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				If m0150.COL26_TYPE = "1" Then
					listFreeItem.Add(m0150.COL26)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL26_TYPE = "2" Then
					listAnaItem.Add(m0150.COL26)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL27_TYPE = "1" Then
					listFreeItem.Add(m0150.COL27)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL27_TYPE = "2" Then
					listAnaItem.Add(m0150.COL27)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL28_TYPE = "1" Then
					listFreeItem.Add(m0150.COL28)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL28_TYPE = "2" Then
					listAnaItem.Add(m0150.COL28)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL29_TYPE = "1" Then
					listFreeItem.Add(m0150.COL29)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL29_TYPE = "2" Then
					listAnaItem.Add(m0150.COL29)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL30_TYPE = "1" Then
					listFreeItem.Add(m0150.COL30)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL30_TYPE = "2" Then
					listAnaItem.Add(m0150.COL30)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL31_TYPE = "1" Then
					listFreeItem.Add(m0150.COL31)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL31_TYPE = "2" Then
					listAnaItem.Add(m0150.COL31)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL32_TYPE = "1" Then
					listFreeItem.Add(m0150.COL32)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL32_TYPE = "2" Then
					listAnaItem.Add(m0150.COL32)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL33_TYPE = "1" Then
					listFreeItem.Add(m0150.COL33)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL33_TYPE = "2" Then
					listAnaItem.Add(m0150.COL33)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL34_TYPE = "1" Then
					listFreeItem.Add(m0150.COL34)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL34_TYPE = "2" Then
					listAnaItem.Add(m0150.COL34)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL35_TYPE = "1" Then
					listFreeItem.Add(m0150.COL35)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL35_TYPE = "2" Then
					listAnaItem.Add(m0150.COL35)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL36_TYPE = "1" Then
					listFreeItem.Add(m0150.COL36)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL36_TYPE = "2" Then
					listAnaItem.Add(m0150.COL36)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL37_TYPE = "1" Then
					listFreeItem.Add(m0150.COL37)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL37_TYPE = "2" Then
					listAnaItem.Add(m0150.COL37)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL38_TYPE = "1" Then
					listFreeItem.Add(m0150.COL38)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL38_TYPE = "2" Then
					listAnaItem.Add(m0150.COL38)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL39_TYPE = "1" Then
					listFreeItem.Add(m0150.COL39)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL39_TYPE = "2" Then
					listAnaItem.Add(m0150.COL39)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL40_TYPE = "1" Then
					listFreeItem.Add(m0150.COL40)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL40_TYPE = "2" Then
					listAnaItem.Add(m0150.COL40)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL41_TYPE = "1" Then
					listFreeItem.Add(m0150.COL41)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL41_TYPE = "2" Then
					listAnaItem.Add(m0150.COL41)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL42_TYPE = "1" Then
					listFreeItem.Add(m0150.COL42)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL42_TYPE = "2" Then
					listAnaItem.Add(m0150.COL42)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL43_TYPE = "1" Then
					listFreeItem.Add(m0150.COL43)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL43_TYPE = "2" Then
					listAnaItem.Add(m0150.COL43)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL44_TYPE = "1" Then
					listFreeItem.Add(m0150.COL44)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL44_TYPE = "2" Then
					listAnaItem.Add(m0150.COL44)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL45_TYPE = "1" Then
					listFreeItem.Add(m0150.COL45)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL45_TYPE = "2" Then
					listAnaItem.Add(m0150.COL45)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL46_TYPE = "1" Then
					listFreeItem.Add(m0150.COL46)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL46_TYPE = "2" Then
					listAnaItem.Add(m0150.COL46)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL47_TYPE = "1" Then
					listFreeItem.Add(m0150.COL47)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL47_TYPE = "2" Then
					listAnaItem.Add(m0150.COL47)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL48_TYPE = "1" Then
					listFreeItem.Add(m0150.COL48)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL48_TYPE = "2" Then
					listAnaItem.Add(m0150.COL48)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL49_TYPE = "1" Then
					listFreeItem.Add(m0150.COL49)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL49_TYPE = "2" Then
					listAnaItem.Add(m0150.COL49)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL50_TYPE = "1" Then
					listFreeItem.Add(m0150.COL50)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL50_TYPE = "2" Then
					listAnaItem.Add(m0150.COL50)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

			End If
            ViewBag.FreeItemList = listFreeItem
            ViewBag.AnaItemList = listAnaItem
            d0010.M0150 = m0150

            'Need to change below query to fetch CATCD from M0020 , once decided the way to retrive uniqu data from M0020
            'Dim m0020ForCATCD As M0020 = db.M0020.Where(Function(m) m.HYOJ = True And m.SPORT = True).OrderBy(Function(m) m.HYOJJN).FirstOrDefault
            'd0010.CATCD = m0020ForCATCD.CATCD

            TempData("CompleteMsg") = ""
            Return View(d0010)

        End Function

        <HttpPost()>
        Function Create(screenD0010 As D0010) As ActionResult

            If ModelState("MON") IsNot Nothing Then
                ModelState("MON").Errors.Clear()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")
            If ModelState.IsValid Then

                If screenD0010.GYOMYMDED Is Nothing Then
                    screenD0010.GYOMYMDED = screenD0010.GYOMYMD
                End If

                Dim Startdate As DateTime = screenD0010.GYOMYMD
                Dim Enddate As DateTime = screenD0010.GYOMYMDED

                'If screenD0010.PATTERN = True Then
                '    Enddate = screenD0010.GYOMYMDED
                'Else
                '    Enddate = screenD0010.GYOMYMD
                'End If

                Dim PatternType As String = ""
                'PatternType = "1" --同じ時間
                'PatternType = "2" --一日
                'PatternType = "3" --連続業務
                If screenD0010.PATTERN = True Then
                    PatternType = "1"
                ElseIf Startdate = Enddate Then
                    PatternType = "2"
                ElseIf Startdate <> Enddate Then
                    PatternType = "3"
                End If

                Dim count As Integer = 1
                Dim StrGYOMNO As String = ""
                Dim DaysCount As Integer = DateRange(screenD0010.GYOMYMD, Enddate).Count
                For Each day As DateTime In DateRange(screenD0010.GYOMYMD, Enddate)

                    Dim insertD0010 As D0010 = New D0010

                    'PatternType = "3" --連続業務
                    If count = 1 Then
                        StrGYOMNO = GetMaxGyomno() + count
                    End If

                    insertD0010.GYOMNO = GetMaxGyomno() + count

                    'Start Date
                    insertD0010.GYOMYMD = day

                    'PatternType = "1" --同じ時間
                    'PatternType = "2" --一日
                    'PatternType = "3" --連続業務
                    If PatternType = "1" Then

                        insertD0010.GYOMYMDED = day
                        insertD0010.KSKJKNST = ChangeToHHMM(screenD0010.KSKJKNST)
                        insertD0010.KSKJKNED = ChangeToHHMM(screenD0010.KSKJKNED)
                        insertD0010.SPORT_OYAFLG = True

                    ElseIf PatternType = "2" Then

                        insertD0010.GYOMYMDED = screenD0010.GYOMYMDED
                        insertD0010.KSKJKNST = ChangeToHHMM(screenD0010.KSKJKNST)
                        insertD0010.KSKJKNED = ChangeToHHMM(screenD0010.KSKJKNED)
                        insertD0010.SPORT_OYAFLG = True

                        'Parent
                    ElseIf PatternType = "3" AndAlso count = 1 Then

                        insertD0010.GYOMYMDED = screenD0010.GYOMYMDED
                        insertD0010.KSKJKNST = ChangeToHHMM(screenD0010.KSKJKNST)
                        insertD0010.KSKJKNED = ChangeToHHMM(screenD0010.KSKJKNED)
                        insertD0010.SPORT_OYAFLG = True

                        'Child
                    ElseIf PatternType = "3" Then

                        insertD0010.GYOMYMDED = day

                        If DaysCount = count Then
                            insertD0010.KSKJKNST = "0000"
                            insertD0010.KSKJKNED = ChangeToHHMM(screenD0010.KSKJKNED)
                        Else
                            insertD0010.KSKJKNST = "0000"
                            insertD0010.KSKJKNED = "2400"
                        End If

                    End If

                    insertD0010.JTJKNST = GetJtjkn(insertD0010.GYOMYMD, insertD0010.KSKJKNST)
                    insertD0010.JTJKNED = GetJtjkn(insertD0010.GYOMYMDED, insertD0010.KSKJKNED)
                    insertD0010.BANGUMINM = screenD0010.BANGUMINM
                    insertD0010.OAJKNST = ChangeToHHMM(screenD0010.OAJKNST)
                    insertD0010.OAJKNED = ChangeToHHMM(screenD0010.OAJKNED)
                    insertD0010.BASYO = screenD0010.BASYO
                    insertD0010.BIKO = screenD0010.BIKO

                    insertD0010.CATCD = screenD0010.CATCD
                    insertD0010.NAIYO = screenD0010.NAIYO
                    insertD0010.BANGUMITANTO = screenD0010.BANGUMITANTO
                    insertD0010.BANGUMIRENRK = screenD0010.BANGUMIRENRK

                    'PatternType = "1" --同じ時間
                    'PatternType = "2" --一日
                    'PatternType = "3" --連続業務
                    If PatternType = "3" Then
                        insertD0010.RNZK = True
                    Else
                        insertD0010.RNZK = False
                    End If

                    'PatternType = "3" --連続業務
                    If count <> 1 AndAlso PatternType = "3" Then
                        insertD0010.PGYOMNO = StrGYOMNO
                    End If

                    'UnComment below code of SPORTFLG,OYAGYOMFLG,SPORTCATCD,SPORTSUBCATCD,SAIJKNST,SAIJKNED,COL1,COL2......COL20
                    'Once db TBL D0010 altered.

                    insertD0010.SPORTFLG = True
                    insertD0010.OYAGYOMFLG = True
                    insertD0010.SPORTCATCD = screenD0010.SPORTCATCD
                    insertD0010.SPORTSUBCATCD = screenD0010.SPORTSUBCATCD
                    insertD0010.SAIJKNST = ChangeToHHMM(screenD0010.SAIJKNST)
                    insertD0010.SAIJKNED = ChangeToHHMM(screenD0010.SAIJKNED)

                    If screenD0010.FreeTxtBxList IsNot Nothing Then
                        Dim iterationCnt As Int16 = 0
                        For Each item In screenD0010.FreeTxtBxList
							If iterationCnt = 0 Then
								insertD0010.COL01 = item
							End If
							If iterationCnt = 1 Then
								insertD0010.COL02 = item
							End If
							If iterationCnt = 2 Then
								insertD0010.COL03 = item
							End If
							If iterationCnt = 3 Then
								insertD0010.COL04 = item
							End If
							If iterationCnt = 4 Then
								insertD0010.COL05 = item
							End If
							If iterationCnt = 5 Then
								insertD0010.COL06 = item
							End If
							If iterationCnt = 6 Then
								insertD0010.COL07 = item
							End If
							If iterationCnt = 7 Then
								insertD0010.COL08 = item
							End If
							If iterationCnt = 8 Then
								insertD0010.COL09 = item
							End If
							If iterationCnt = 9 Then
								insertD0010.COL10 = item
							End If
							If iterationCnt = 10 Then
								insertD0010.COL11 = item
							End If
							If iterationCnt = 11 Then
								insertD0010.COL12 = item
							End If
							If iterationCnt = 12 Then
								insertD0010.COL13 = item
							End If
							If iterationCnt = 13 Then
								insertD0010.COL14 = item
							End If
							If iterationCnt = 14 Then
								insertD0010.COL15 = item
							End If
							If iterationCnt = 15 Then
								insertD0010.COL16 = item
							End If
							If iterationCnt = 16 Then
								insertD0010.COL17 = item
							End If
							If iterationCnt = 17 Then
								insertD0010.COL18 = item
							End If
							If iterationCnt = 18 Then
								insertD0010.COL19 = item
							End If
							If iterationCnt = 19 Then
								insertD0010.COL20 = item
							End If
							If iterationCnt = 20 Then
								insertD0010.COL21 = item
							End If
							If iterationCnt = 21 Then
								insertD0010.COL22 = item
							End If
							If iterationCnt = 22 Then
								insertD0010.COL23 = item
							End If
							If iterationCnt = 23 Then
								insertD0010.COL24 = item
							End If
							If iterationCnt = 24 Then
								insertD0010.COL25 = item
							End If

							'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
							If iterationCnt = 25 Then
								insertD0010.COL26 = item
							End If
							If iterationCnt = 26 Then
								insertD0010.COL27 = item
							End If
							If iterationCnt = 27 Then
								insertD0010.COL28 = item
							End If
							If iterationCnt = 28 Then
								insertD0010.COL29 = item
							End If
							If iterationCnt = 29 Then
								insertD0010.COL30 = item
							End If
							If iterationCnt = 30 Then
								insertD0010.COL31 = item
							End If
							If iterationCnt = 31 Then
								insertD0010.COL32 = item
							End If
							If iterationCnt = 32 Then
								insertD0010.COL33 = item
							End If
							If iterationCnt = 33 Then
								insertD0010.COL34 = item
							End If
							If iterationCnt = 34 Then
								insertD0010.COL35 = item
							End If
							If iterationCnt = 35 Then
								insertD0010.COL36 = item
							End If
							If iterationCnt = 36 Then
								insertD0010.COL37 = item
							End If
							If iterationCnt = 37 Then
								insertD0010.COL38 = item
							End If
							If iterationCnt = 38 Then
								insertD0010.COL39 = item
							End If
							If iterationCnt = 39 Then
								insertD0010.COL40 = item
							End If
							If iterationCnt = 40 Then
								insertD0010.COL41 = item
							End If
							If iterationCnt = 41 Then
								insertD0010.COL42 = item
							End If
							If iterationCnt = 42 Then
								insertD0010.COL43 = item
							End If
							If iterationCnt = 43 Then
								insertD0010.COL44 = item
							End If
							If iterationCnt = 44 Then
								insertD0010.COL45 = item
							End If
							If iterationCnt = 45 Then
								insertD0010.COL46 = item
							End If
							If iterationCnt = 46 Then
								insertD0010.COL47 = item
							End If
							If iterationCnt = 47 Then
								insertD0010.COL48 = item
							End If
							If iterationCnt = 48 Then
								insertD0010.COL49 = item
							End If
							If iterationCnt = 49 Then
								insertD0010.COL50 = item
							End If

							iterationCnt = iterationCnt + 1
						Next
					End If

					If screenD0010.D0022 IsNot Nothing Then
						For Each item In screenD0010.D0022
							'If dropdown is not blank
							If item.USERID <> 0 Then
								If item.COLIDX = 1 Then
									item.COLNM = "COL01"
								End If
								If item.COLIDX = 2 Then
									item.COLNM = "COL02"
								End If
								If item.COLIDX = 3 Then
									item.COLNM = "COL03"
								End If
								If item.COLIDX = 4 Then
									item.COLNM = "COL04"
								End If
								If item.COLIDX = 5 Then
									item.COLNM = "COL05"
								End If
								If item.COLIDX = 6 Then
									item.COLNM = "COL06"
								End If
								If item.COLIDX = 7 Then
									item.COLNM = "COL07"
								End If
								If item.COLIDX = 8 Then
									item.COLNM = "COL08"
								End If
								If item.COLIDX = 9 Then
									item.COLNM = "COL09"
								End If
								If item.COLIDX = 10 Then
									item.COLNM = "COL10"
								End If
								If item.COLIDX = 11 Then
									item.COLNM = "COL11"
								End If
								If item.COLIDX = 12 Then
									item.COLNM = "COL12"
								End If
								If item.COLIDX = 13 Then
									item.COLNM = "COL13"
								End If
								If item.COLIDX = 14 Then
									item.COLNM = "COL14"
								End If
								If item.COLIDX = 15 Then
									item.COLNM = "COL15"
								End If
								If item.COLIDX = 16 Then
									item.COLNM = "COL16"
								End If
								If item.COLIDX = 17 Then
									item.COLNM = "COL17"
								End If
								If item.COLIDX = 18 Then
									item.COLNM = "COL18"
								End If
								If item.COLIDX = 19 Then
									item.COLNM = "COL19"
								End If
								If item.COLIDX = 20 Then
									item.COLNM = "COL20"
								End If
								If item.COLIDX = 21 Then
									item.COLNM = "COL21"
								End If
								If item.COLIDX = 22 Then
									item.COLNM = "COL22"
								End If
								If item.COLIDX = 23 Then
									item.COLNM = "COL23"
								End If
								If item.COLIDX = 24 Then
									item.COLNM = "COL24"
								End If
								If item.COLIDX = 25 Then
									item.COLNM = "COL25"
								End If

								'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
								If item.COLIDX = 26 Then
									item.COLNM = "COL26"
								End If
								If item.COLIDX = 27 Then
									item.COLNM = "COL27"
								End If
								If item.COLIDX = 28 Then
									item.COLNM = "COL28"
								End If
								If item.COLIDX = 29 Then
									item.COLNM = "COL29"
								End If
								If item.COLIDX = 30 Then
									item.COLNM = "COL30"
								End If
								If item.COLIDX = 31 Then
									item.COLNM = "COL31"
								End If
								If item.COLIDX = 32 Then
									item.COLNM = "COL32"
								End If
								If item.COLIDX = 33 Then
									item.COLNM = "COL33"
								End If
								If item.COLIDX = 34 Then
									item.COLNM = "COL34"
								End If
								If item.COLIDX = 35 Then
									item.COLNM = "COL35"
								End If
								If item.COLIDX = 36 Then
									item.COLNM = "COL36"
								End If
								If item.COLIDX = 37 Then
									item.COLNM = "COL37"
								End If
								If item.COLIDX = 38 Then
									item.COLNM = "COL38"
								End If
								If item.COLIDX = 39 Then
									item.COLNM = "COL39"
								End If
								If item.COLIDX = 40 Then
									item.COLNM = "COL40"
								End If
								If item.COLIDX = 41 Then
									item.COLNM = "COL41"
								End If
								If item.COLIDX = 42 Then
									item.COLNM = "COL42"
								End If
								If item.COLIDX = 43 Then
									item.COLNM = "COL43"
								End If
								If item.COLIDX = 44 Then
									item.COLNM = "COL44"
								End If
								If item.COLIDX = 45 Then
									item.COLNM = "COL45"
								End If
								If item.COLIDX = 46 Then
									item.COLNM = "COL46"
								End If
								If item.COLIDX = 47 Then
									item.COLNM = "COL47"
								End If
								If item.COLIDX = 48 Then
									item.COLNM = "COL48"
								End If
								If item.COLIDX = 49 Then
									item.COLNM = "COL49"
								End If
								If item.COLIDX = 50 Then
									item.COLNM = "COL50"
								End If

							End If
						Next
					End If
					db.D0010.Add(insertD0010)

					'Insert in D0022 table
					If screenD0010.D0022 IsNot Nothing Then
						For Each item In screenD0010.D0022
							If item.USERID <> 0 Then
								Dim insertD0022 As New D0022
								insertD0022.GYOMNO = insertD0010.GYOMNO
								insertD0022.COLNM = item.COLNM
								insertD0022.USERID = item.USERID
								db.D0022.Add(insertD0022)
							End If
						Next
					End If

					count += 1
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
						TempData("CompleteMsg") = "Update Complete"
					Catch ex As Exception
						Throw ex
						tran.Rollback()
					End Try
				End Using

			End If

			Dim lstSportCatNm = db.M0130.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim blank_entry As New M0130
			blank_entry.SPORTCATCD = 0
			blank_entry.SPORTCATNM = ""
			lstSportCatNm.Insert(0, blank_entry)
			ViewBag.SportCatNmList = lstSportCatNm

			Dim lstSportSubCatNm = db.M0140.Where(Function(m) m.HYOJ = True).OrderBy(Function(f) f.HYOJJN).ToList
			Dim blank_entry_subCatNm As New M0140
			blank_entry_subCatNm.SPORTSUBCATCD = 0
			blank_entry_subCatNm.SPORTSUBCATNM = ""
			lstSportSubCatNm.Insert(0, blank_entry_subCatNm)
			ViewBag.SportSubCatNmList = lstSportSubCatNm

			Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim item1 As New M0020
			item1.CATCD = "0"
			item1.CATNM = ""
			lstm0020.Insert(0, item1)
			ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM")

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			Dim m0010List = From m In db.M0010 Select m
			ViewBag.lstmsterm0010 = m0010List.ToList
			m0010List = m0010List.Where(Function(d1) db.M0160.Where(Function(m) m.SPORTCATCD = screenD0010.SPORTCATCD AndAlso d1.STATUS = True AndAlso d1.HYOJ = True).Select(Function(t2) t2.USERID).Contains(d1.USERID) Or (d1.KARIANA = True And d1.STATUS = True))
			'Dim lstUSERID = m0010List.ToList
			Dim lstUSERID = m0010List.OrderByDescending(Function(x) x.KARIANA = True).ThenBy(Function(x) x.HYOJJN).ToList
			Dim itemm0010 As New M0010
			itemm0010.USERID = "0"
			itemm0010.USERNM = ""
			lstUSERID.Insert(0, itemm0010)
			ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM", lstUSERID(0))
			ViewBag.lstM0010 = lstUSERID

			Dim listFreeItem As New List(Of String)
			Dim listAnaItem As New List(Of String)

			Dim m0150 As M0150 = db.M0150.Where(Function(m) m.SPORTCATCD = screenD0010.SPORTCATCD And m.SPORTSUBCATCD = screenD0010.SPORTSUBCATCD).FirstOrDefault

			If m0150 IsNot Nothing Then
				If m0150.COL01_TYPE = "1" Then
					listFreeItem.Add(m0150.COL01)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL01_TYPE = "2" Then
					listAnaItem.Add(m0150.COL01)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL02_TYPE = "1" Then
					listFreeItem.Add(m0150.COL02)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL02_TYPE = "2" Then
					listAnaItem.Add(m0150.COL02)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL03_TYPE = "1" Then
					listFreeItem.Add(m0150.COL03)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL03_TYPE = "2" Then
					listAnaItem.Add(m0150.COL03)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL04_TYPE = "1" Then
					listFreeItem.Add(m0150.COL04)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL04_TYPE = "2" Then
					listAnaItem.Add(m0150.COL04)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL05_TYPE = "1" Then
					listFreeItem.Add(m0150.COL05)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL05_TYPE = "2" Then
					listAnaItem.Add(m0150.COL05)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL06_TYPE = "1" Then
					listFreeItem.Add(m0150.COL06)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL06_TYPE = "2" Then
					listAnaItem.Add(m0150.COL06)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL07_TYPE = "1" Then
					listFreeItem.Add(m0150.COL07)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL07_TYPE = "2" Then
					listAnaItem.Add(m0150.COL07)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL08_TYPE = "1" Then
					listFreeItem.Add(m0150.COL08)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL08_TYPE = "2" Then
					listAnaItem.Add(m0150.COL08)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL09_TYPE = "1" Then
					listFreeItem.Add(m0150.COL09)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL09_TYPE = "2" Then
					listAnaItem.Add(m0150.COL09)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL10_TYPE = "1" Then
					listFreeItem.Add(m0150.COL10)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL10_TYPE = "2" Then
					listAnaItem.Add(m0150.COL10)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL11_TYPE = "1" Then
					listFreeItem.Add(m0150.COL11)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL11_TYPE = "2" Then
					listAnaItem.Add(m0150.COL11)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL12_TYPE = "1" Then
					listFreeItem.Add(m0150.COL12)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL12_TYPE = "2" Then
					listAnaItem.Add(m0150.COL12)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL13_TYPE = "1" Then
					listFreeItem.Add(m0150.COL13)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL13_TYPE = "2" Then
					listAnaItem.Add(m0150.COL13)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL14_TYPE = "1" Then
					listFreeItem.Add(m0150.COL14)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL14_TYPE = "2" Then
					listAnaItem.Add(m0150.COL14)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL15_TYPE = "1" Then
					listFreeItem.Add(m0150.COL15)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL15_TYPE = "2" Then
					listAnaItem.Add(m0150.COL15)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL16_TYPE = "1" Then
					listFreeItem.Add(m0150.COL16)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL16_TYPE = "2" Then
					listAnaItem.Add(m0150.COL16)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL17_TYPE = "1" Then
					listFreeItem.Add(m0150.COL17)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL17_TYPE = "2" Then
					listAnaItem.Add(m0150.COL17)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL18_TYPE = "1" Then
					listFreeItem.Add(m0150.COL18)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL18_TYPE = "2" Then
					listAnaItem.Add(m0150.COL18)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL19_TYPE = "1" Then
					listFreeItem.Add(m0150.COL19)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL19_TYPE = "2" Then
					listAnaItem.Add(m0150.COL19)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL20_TYPE = "1" Then
					listFreeItem.Add(m0150.COL20)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL20_TYPE = "2" Then
					listAnaItem.Add(m0150.COL20)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL21_TYPE = "1" Then
					listFreeItem.Add(m0150.COL21)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL21_TYPE = "2" Then
					listAnaItem.Add(m0150.COL21)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL22_TYPE = "1" Then
					listFreeItem.Add(m0150.COL22)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL22_TYPE = "2" Then
					listAnaItem.Add(m0150.COL22)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL23_TYPE = "1" Then
					listFreeItem.Add(m0150.COL23)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL23_TYPE = "2" Then
					listAnaItem.Add(m0150.COL23)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL24_TYPE = "1" Then
					listFreeItem.Add(m0150.COL24)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL24_TYPE = "2" Then
					listAnaItem.Add(m0150.COL24)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL25_TYPE = "1" Then
					listFreeItem.Add(m0150.COL25)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL25_TYPE = "2" Then
					listAnaItem.Add(m0150.COL25)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				If m0150.COL26_TYPE = "1" Then
					listFreeItem.Add(m0150.COL26)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL26_TYPE = "2" Then
					listAnaItem.Add(m0150.COL26)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL27_TYPE = "1" Then
					listFreeItem.Add(m0150.COL27)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL27_TYPE = "2" Then
					listAnaItem.Add(m0150.COL27)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL28_TYPE = "1" Then
					listFreeItem.Add(m0150.COL28)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL28_TYPE = "2" Then
					listAnaItem.Add(m0150.COL28)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL29_TYPE = "1" Then
					listFreeItem.Add(m0150.COL29)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL29_TYPE = "2" Then
					listAnaItem.Add(m0150.COL29)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL30_TYPE = "1" Then
					listFreeItem.Add(m0150.COL30)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL30_TYPE = "2" Then
					listAnaItem.Add(m0150.COL30)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL31_TYPE = "1" Then
					listFreeItem.Add(m0150.COL31)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL31_TYPE = "2" Then
					listAnaItem.Add(m0150.COL31)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL32_TYPE = "1" Then
					listFreeItem.Add(m0150.COL32)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL32_TYPE = "2" Then
					listAnaItem.Add(m0150.COL32)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL33_TYPE = "1" Then
					listFreeItem.Add(m0150.COL33)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL33_TYPE = "2" Then
					listAnaItem.Add(m0150.COL33)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL34_TYPE = "1" Then
					listFreeItem.Add(m0150.COL34)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL34_TYPE = "2" Then
					listAnaItem.Add(m0150.COL34)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL35_TYPE = "1" Then
					listFreeItem.Add(m0150.COL35)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL35_TYPE = "2" Then
					listAnaItem.Add(m0150.COL35)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL36_TYPE = "1" Then
					listFreeItem.Add(m0150.COL36)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL36_TYPE = "2" Then
					listAnaItem.Add(m0150.COL36)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL37_TYPE = "1" Then
					listFreeItem.Add(m0150.COL37)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL37_TYPE = "2" Then
					listAnaItem.Add(m0150.COL37)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL38_TYPE = "1" Then
					listFreeItem.Add(m0150.COL38)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL38_TYPE = "2" Then
					listAnaItem.Add(m0150.COL38)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL39_TYPE = "1" Then
					listFreeItem.Add(m0150.COL39)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL39_TYPE = "2" Then
					listAnaItem.Add(m0150.COL39)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL40_TYPE = "1" Then
					listFreeItem.Add(m0150.COL40)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL40_TYPE = "2" Then
					listAnaItem.Add(m0150.COL40)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL41_TYPE = "1" Then
					listFreeItem.Add(m0150.COL41)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL41_TYPE = "2" Then
					listAnaItem.Add(m0150.COL41)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL42_TYPE = "1" Then
					listFreeItem.Add(m0150.COL42)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL42_TYPE = "2" Then
					listAnaItem.Add(m0150.COL42)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL43_TYPE = "1" Then
					listFreeItem.Add(m0150.COL43)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL43_TYPE = "2" Then
					listAnaItem.Add(m0150.COL43)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL44_TYPE = "1" Then
					listFreeItem.Add(m0150.COL44)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL44_TYPE = "2" Then
					listAnaItem.Add(m0150.COL44)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL45_TYPE = "1" Then
					listFreeItem.Add(m0150.COL45)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL45_TYPE = "2" Then
					listAnaItem.Add(m0150.COL45)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL46_TYPE = "1" Then
					listFreeItem.Add(m0150.COL46)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL46_TYPE = "2" Then
					listAnaItem.Add(m0150.COL46)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL47_TYPE = "1" Then
					listFreeItem.Add(m0150.COL47)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL47_TYPE = "2" Then
					listAnaItem.Add(m0150.COL47)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL48_TYPE = "1" Then
					listFreeItem.Add(m0150.COL48)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL48_TYPE = "2" Then
					listAnaItem.Add(m0150.COL48)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL49_TYPE = "1" Then
					listFreeItem.Add(m0150.COL49)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL49_TYPE = "2" Then
					listAnaItem.Add(m0150.COL49)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL50_TYPE = "1" Then
					listFreeItem.Add(m0150.COL50)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL50_TYPE = "2" Then
					listAnaItem.Add(m0150.COL50)
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

			End If
			ViewBag.FreeItemList = listFreeItem
			ViewBag.AnaItemList = listAnaItem
			screenD0010.M0150 = m0150

			Return View(screenD0010)

		End Function

		'修正モッド
		Function Edit(ByVal id As Decimal, ByVal lastForm As String) As ActionResult

			'If Nothing Then
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			'If Session Time Out Then Return Back to Main Page
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

			Dim d0010_child As D0010 = db.D0010.Find(id)
			'業務テーブルからダ－タ取得


			If d0010_child.RNZK = True AndAlso d0010_child.PGYOMNO IsNot Nothing Then
				Dim d0010_parent As D0010 = db.D0010.Find(d0010_child.PGYOMNO)
				id = d0010_parent.GYOMNO
			End If

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.ToString
				If Not strUrlReferrer.Contains("A0220/Delete") Then
					Session("A0220EditRtnUrl" & id) = strUrlReferrer
				End If
			End If

			Dim d0010 As D0010 = db.D0010.Find(id)

			If d0010 Is Nothing Then
				'Need to Display Error
			End If

			Dim SPORTCATCD As String = d0010.SPORTCATCD
			Dim SPORTSUBCATCD As String = d0010.SPORTSUBCATCD

			'For DropDown Menu Of スポーツカテゴリーコード	(SPORTCATCD)
			Dim lstSportCatNm = db.M0130.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim blank_entry As New M0130
			blank_entry.SPORTCATCD = 0
			blank_entry.SPORTCATNM = ""
			lstSportCatNm.Insert(0, blank_entry)
			ViewBag.SportCatNmList = lstSportCatNm

			'For DropDown Menu Of スポーツサブカテゴリーコード	(SPORTSUBCATCD)
			Dim lstSportSubCatNm = db.M0140.Where(Function(m) m.HYOJ = True).OrderBy(Function(f) f.HYOJJN).ToList
			Dim blank_entry_subCatNm As New M0140
			blank_entry_subCatNm.SPORTSUBCATCD = 0
			blank_entry_subCatNm.SPORTSUBCATNM = ""
			lstSportSubCatNm.Insert(0, blank_entry_subCatNm)
			ViewBag.SportSubCatNmList = lstSportSubCatNm

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			'ユーザー
			Dim m0010List = From m In db.M0010 Select m
			ViewBag.lstmsterm0010 = m0010List.ToList
			m0010List = m0010List.Where(Function(d1) db.M0160.Where(Function(m) m.SPORTCATCD = SPORTCATCD AndAlso d1.STATUS = True AndAlso d1.HYOJ = True).Select(Function(t2) t2.USERID).Contains(d1.USERID) Or (d1.KARIANA = True And d1.STATUS = True))

			Dim d22UIDList = (From d22 In db.D0022
							  Join d10 In db.D0010 On d10.GYOMNO Equals d22.GYOMNO
							  Where d10.SPORTCATCD = SPORTCATCD Group By USERID = d22.USERID Into t = [Select](d22.USERID)).ToList
			Dim lstD22UserId As New List(Of Short)

			For Each item In d22UIDList
				Dim uidCnt = m0010List.Where(Function(m10) m10.USERID = item.USERID).Count
				If uidCnt = 0 Then
					lstD22UserId.Add(item.USERID)
				End If
			Next

			If lstD22UserId.Count > 0 Then
				m0010List = m0010List.Union(From m10 In db.M0010 Where lstD22UserId.Contains(m10.USERID) AndAlso m10.STATUS = True AndAlso m10.HYOJ = True)
			End If

			'Dim lstUSERID = m0010List.ToList
			Dim lstUSERID = m0010List.OrderByDescending(Function(x) x.KARIANA = True).ThenBy(Function(x) x.HYOJJN).ToList
			Dim itemm0010 As New M0010
			itemm0010.USERID = "0"
			itemm0010.USERNM = ""
			lstUSERID.Insert(0, itemm0010)
			ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM")
			ViewBag.lstM0010 = lstUSERID

			Dim listFreeItem As New List(Of String)
			'Dim listAnaItem As New List(Of String())
			Dim listAnaItem = {(New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "", .TBLNM = "", .HYOJJN = Short.Parse("0")})}.ToList
			Dim listitemvalue As New List(Of String)

			Dim m0150 As M0150 = db.M0150.Where(Function(m) m.SPORTCATCD = SPORTCATCD And m.SPORTSUBCATCD = SPORTSUBCATCD).FirstOrDefault
			If m0150 IsNot Nothing Then
				listAnaItem.RemoveAt(0)
				If m0150.COL01_TYPE = "1" Then
					listFreeItem.Add(m0150.COL01)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "1", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL01_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL01, .ANNCATNAME = "", .COLINDEX = "1", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL01_HYOJJN2 IsNot Nothing, m0150.COL01_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "1", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL02_TYPE = "1" Then
					listFreeItem.Add(m0150.COL02)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "2", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL02_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL02, .ANNCATNAME = "", .COLINDEX = "2", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL02_HYOJJN2 IsNot Nothing, m0150.COL02_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "2", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL03_TYPE = "1" Then
					listFreeItem.Add(m0150.COL03)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "3", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL03_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL03, .ANNCATNAME = "", .COLINDEX = "3", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL03_HYOJJN2 IsNot Nothing, m0150.COL03_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "3", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL04_TYPE = "1" Then
					listFreeItem.Add(m0150.COL04)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "4", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL04_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL04, .ANNCATNAME = "", .COLINDEX = "4", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL04_HYOJJN2 IsNot Nothing, m0150.COL04_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "4", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL05_TYPE = "1" Then
					listFreeItem.Add(m0150.COL05)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "5", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL05_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL05, .ANNCATNAME = "", .COLINDEX = "5", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL05_HYOJJN2 IsNot Nothing, m0150.COL05_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "5", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL06_TYPE = "1" Then
					listFreeItem.Add(m0150.COL06)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "6", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL06_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL06, .ANNCATNAME = "", .COLINDEX = "6", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL06_HYOJJN2 IsNot Nothing, m0150.COL06_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "6", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL07_TYPE = "1" Then
					listFreeItem.Add(m0150.COL07)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "7", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL07_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL07, .ANNCATNAME = "", .COLINDEX = "7", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL07_HYOJJN2 IsNot Nothing, m0150.COL07_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "7", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL08_TYPE = "1" Then
					listFreeItem.Add(m0150.COL08)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "8", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL08_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL08, .ANNCATNAME = "", .COLINDEX = "8", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL08_HYOJJN2 IsNot Nothing, m0150.COL08_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "8", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL09_TYPE = "1" Then
					listFreeItem.Add(m0150.COL09)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "9", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL09_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL09, .ANNCATNAME = "", .COLINDEX = "9", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL09_HYOJJN2 IsNot Nothing, m0150.COL09_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "9", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL10_TYPE = "1" Then
					listFreeItem.Add(m0150.COL10)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "10", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL10_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL10, .ANNCATNAME = "", .COLINDEX = "10", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL10_HYOJJN2 IsNot Nothing, m0150.COL10_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "10", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL11_TYPE = "1" Then
					listFreeItem.Add(m0150.COL11)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "11", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL11_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL11, .ANNCATNAME = "", .COLINDEX = "11", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL11_HYOJJN2 IsNot Nothing, m0150.COL11_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "11", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL12_TYPE = "1" Then
					listFreeItem.Add(m0150.COL12)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "12", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL12_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL12, .ANNCATNAME = "", .COLINDEX = "12", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL12_HYOJJN2 IsNot Nothing, m0150.COL12_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "12", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL13_TYPE = "1" Then
					listFreeItem.Add(m0150.COL13)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "13", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL13_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL13, .ANNCATNAME = "", .COLINDEX = "13", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL13_HYOJJN2 IsNot Nothing, m0150.COL13_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "13", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL14_TYPE = "1" Then
					listFreeItem.Add(m0150.COL14)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "14", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL14_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL14, .ANNCATNAME = "", .COLINDEX = "14", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL14_HYOJJN2 IsNot Nothing, m0150.COL14_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "14", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL15_TYPE = "1" Then
					listFreeItem.Add(m0150.COL15)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "15", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL15_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL15, .ANNCATNAME = "", .COLINDEX = "15", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL15_HYOJJN2 IsNot Nothing, m0150.COL15_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "15", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL16_TYPE = "1" Then
					listFreeItem.Add(m0150.COL16)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "16", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL16_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL16, .ANNCATNAME = "", .COLINDEX = "16", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL16_HYOJJN2 IsNot Nothing, m0150.COL16_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "16", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL17_TYPE = "1" Then
					listFreeItem.Add(m0150.COL17)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "17", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL17_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL17, .ANNCATNAME = "", .COLINDEX = "17", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL17_HYOJJN2 IsNot Nothing, m0150.COL17_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "17", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL18_TYPE = "1" Then
					listFreeItem.Add(m0150.COL18)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "18", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL18_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL18, .ANNCATNAME = "", .COLINDEX = "18", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL18_HYOJJN2 IsNot Nothing, m0150.COL18_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "18", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL19_TYPE = "1" Then
					listFreeItem.Add(m0150.COL19)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "19", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL19_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL19, .ANNCATNAME = "", .COLINDEX = "19", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL19_HYOJJN2 IsNot Nothing, m0150.COL19_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "19", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL20_TYPE = "1" Then
					listFreeItem.Add(m0150.COL20)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "20", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL20_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL20, .ANNCATNAME = "", .COLINDEX = "20", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL20_HYOJJN2 IsNot Nothing, m0150.COL20_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "20", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL21_TYPE = "1" Then
					listFreeItem.Add(m0150.COL21)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "21", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL21_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL21, .ANNCATNAME = "", .COLINDEX = "21", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL21_HYOJJN2 IsNot Nothing, m0150.COL21_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "21", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL22_TYPE = "1" Then
					listFreeItem.Add(m0150.COL22)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "22", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL22_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL22, .ANNCATNAME = "", .COLINDEX = "22", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL22_HYOJJN2 IsNot Nothing, m0150.COL22_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "22", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL23_TYPE = "1" Then
					listFreeItem.Add(m0150.COL23)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "23", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL23_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL23, .ANNCATNAME = "", .COLINDEX = "23", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL23_HYOJJN2 IsNot Nothing, m0150.COL23_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "23", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL24_TYPE = "1" Then
					listFreeItem.Add(m0150.COL24)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "24", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL24_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL24, .ANNCATNAME = "", .COLINDEX = "24", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL24_HYOJJN2 IsNot Nothing, m0150.COL24_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "24", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL25_TYPE = "1" Then
					listFreeItem.Add(m0150.COL25)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "25", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL25_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL25, .ANNCATNAME = "", .COLINDEX = "25", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL25_HYOJJN2 IsNot Nothing, m0150.COL25_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "25", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				If m0150.COL26_TYPE = "1" Then
					listFreeItem.Add(m0150.COL26)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "26", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL26_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL26, .ANNCATNAME = "", .COLINDEX = "26", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL26_HYOJJN2 IsNot Nothing, m0150.COL26_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "26", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL27_TYPE = "1" Then
					listFreeItem.Add(m0150.COL27)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "27", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL27_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL27, .ANNCATNAME = "", .COLINDEX = "27", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL27_HYOJJN2 IsNot Nothing, m0150.COL27_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "27", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL28_TYPE = "1" Then
					listFreeItem.Add(m0150.COL28)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "28", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL28_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL28, .ANNCATNAME = "", .COLINDEX = "28", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL28_HYOJJN2 IsNot Nothing, m0150.COL28_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "28", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL29_TYPE = "1" Then
					listFreeItem.Add(m0150.COL29)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "29", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL29_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL29, .ANNCATNAME = "", .COLINDEX = "29", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL29_HYOJJN2 IsNot Nothing, m0150.COL29_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "29", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL30_TYPE = "1" Then
					listFreeItem.Add(m0150.COL30)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "30", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL30_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL30, .ANNCATNAME = "", .COLINDEX = "30", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL30_HYOJJN2 IsNot Nothing, m0150.COL30_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "30", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL31_TYPE = "1" Then
					listFreeItem.Add(m0150.COL31)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "31", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL31_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL31, .ANNCATNAME = "", .COLINDEX = "31", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL31_HYOJJN2 IsNot Nothing, m0150.COL31_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "31", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL32_TYPE = "1" Then
					listFreeItem.Add(m0150.COL32)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "32", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL32_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL32, .ANNCATNAME = "", .COLINDEX = "32", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL32_HYOJJN2 IsNot Nothing, m0150.COL32_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "32", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL33_TYPE = "1" Then
					listFreeItem.Add(m0150.COL33)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "33", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL33_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL33, .ANNCATNAME = "", .COLINDEX = "33", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL33_HYOJJN2 IsNot Nothing, m0150.COL33_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "33", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL34_TYPE = "1" Then
					listFreeItem.Add(m0150.COL34)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "34", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL34_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL34, .ANNCATNAME = "", .COLINDEX = "34", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL34_HYOJJN2 IsNot Nothing, m0150.COL34_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "34", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL35_TYPE = "1" Then
					listFreeItem.Add(m0150.COL35)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "35", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL35_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL35, .ANNCATNAME = "", .COLINDEX = "35", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL35_HYOJJN2 IsNot Nothing, m0150.COL35_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "35", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL36_TYPE = "1" Then
					listFreeItem.Add(m0150.COL36)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "36", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL36_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL36, .ANNCATNAME = "", .COLINDEX = "36", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL36_HYOJJN2 IsNot Nothing, m0150.COL36_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "36", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL37_TYPE = "1" Then
					listFreeItem.Add(m0150.COL37)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "37", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL37_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL37, .ANNCATNAME = "", .COLINDEX = "37", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL37_HYOJJN2 IsNot Nothing, m0150.COL37_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "37", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL38_TYPE = "1" Then
					listFreeItem.Add(m0150.COL38)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "38", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL38_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL38, .ANNCATNAME = "", .COLINDEX = "38", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL38_HYOJJN2 IsNot Nothing, m0150.COL38_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "38", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL39_TYPE = "1" Then
					listFreeItem.Add(m0150.COL39)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "39", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL39_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL39, .ANNCATNAME = "", .COLINDEX = "39", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL39_HYOJJN2 IsNot Nothing, m0150.COL39_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "39", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL40_TYPE = "1" Then
					listFreeItem.Add(m0150.COL40)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "40", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL40_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL40, .ANNCATNAME = "", .COLINDEX = "40", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL40_HYOJJN2 IsNot Nothing, m0150.COL40_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "40", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL41_TYPE = "1" Then
					listFreeItem.Add(m0150.COL41)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "41", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL41_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL41, .ANNCATNAME = "", .COLINDEX = "41", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL41_HYOJJN2 IsNot Nothing, m0150.COL41_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "41", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL42_TYPE = "1" Then
					listFreeItem.Add(m0150.COL42)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "42", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL42_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL42, .ANNCATNAME = "", .COLINDEX = "42", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL42_HYOJJN2 IsNot Nothing, m0150.COL42_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "42", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL43_TYPE = "1" Then
					listFreeItem.Add(m0150.COL43)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "43", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL43_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL43, .ANNCATNAME = "", .COLINDEX = "43", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL43_HYOJJN2 IsNot Nothing, m0150.COL43_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "43", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL44_TYPE = "1" Then
					listFreeItem.Add(m0150.COL44)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "44", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL44_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL44, .ANNCATNAME = "", .COLINDEX = "44", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL44_HYOJJN2 IsNot Nothing, m0150.COL44_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "44", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL45_TYPE = "1" Then
					listFreeItem.Add(m0150.COL45)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "45", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL45_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL45, .ANNCATNAME = "", .COLINDEX = "45", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL45_HYOJJN2 IsNot Nothing, m0150.COL45_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "45", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL46_TYPE = "1" Then
					listFreeItem.Add(m0150.COL46)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "46", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL46_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL46, .ANNCATNAME = "", .COLINDEX = "46", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL46_HYOJJN2 IsNot Nothing, m0150.COL46_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "46", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL47_TYPE = "1" Then
					listFreeItem.Add(m0150.COL47)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "47", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL47_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL47, .ANNCATNAME = "", .COLINDEX = "47", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL47_HYOJJN2 IsNot Nothing, m0150.COL47_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "47", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL48_TYPE = "1" Then
					listFreeItem.Add(m0150.COL48)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "48", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL48_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL48, .ANNCATNAME = "", .COLINDEX = "48", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL48_HYOJJN2 IsNot Nothing, m0150.COL48_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "48", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL49_TYPE = "1" Then
					listFreeItem.Add(m0150.COL49)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "49", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL49_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL49, .ANNCATNAME = "", .COLINDEX = "49", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL49_HYOJJN2 IsNot Nothing, m0150.COL49_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "49", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL50_TYPE = "1" Then
					listFreeItem.Add(m0150.COL50)
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "50", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
				ElseIf m0150.COL50_TYPE = "2" Then
					listAnaItem.Add((New With {.COLNAME = m0150.COL50, .ANNCATNAME = "", .COLINDEX = "50", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL50_HYOJJN2 IsNot Nothing, m0150.COL50_HYOJJN2, 999))}))
					listFreeItem.Add(Nothing)
				Else
					listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "50", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					listFreeItem.Add(Nothing)
				End If

				listitemvalue.Add(d0010.COL01)
				listitemvalue.Add(d0010.COL02)
				listitemvalue.Add(d0010.COL03)
				listitemvalue.Add(d0010.COL04)
				listitemvalue.Add(d0010.COL05)
				listitemvalue.Add(d0010.COL06)
				listitemvalue.Add(d0010.COL07)
				listitemvalue.Add(d0010.COL08)
				listitemvalue.Add(d0010.COL09)
				listitemvalue.Add(d0010.COL10)
				listitemvalue.Add(d0010.COL11)
				listitemvalue.Add(d0010.COL12)
				listitemvalue.Add(d0010.COL13)
				listitemvalue.Add(d0010.COL14)
				listitemvalue.Add(d0010.COL15)
				listitemvalue.Add(d0010.COL16)
				listitemvalue.Add(d0010.COL17)
				listitemvalue.Add(d0010.COL18)
				listitemvalue.Add(d0010.COL19)
				listitemvalue.Add(d0010.COL20)
				listitemvalue.Add(d0010.COL21)
				listitemvalue.Add(d0010.COL22)
				listitemvalue.Add(d0010.COL23)
				listitemvalue.Add(d0010.COL24)
				listitemvalue.Add(d0010.COL25)

				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				listitemvalue.Add(d0010.COL26)
				listitemvalue.Add(d0010.COL27)
				listitemvalue.Add(d0010.COL28)
				listitemvalue.Add(d0010.COL29)
				listitemvalue.Add(d0010.COL30)
				listitemvalue.Add(d0010.COL31)
				listitemvalue.Add(d0010.COL32)
				listitemvalue.Add(d0010.COL33)
				listitemvalue.Add(d0010.COL34)
				listitemvalue.Add(d0010.COL35)
				listitemvalue.Add(d0010.COL36)
				listitemvalue.Add(d0010.COL37)
				listitemvalue.Add(d0010.COL38)
				listitemvalue.Add(d0010.COL39)
				listitemvalue.Add(d0010.COL40)
				listitemvalue.Add(d0010.COL41)
				listitemvalue.Add(d0010.COL42)
				listitemvalue.Add(d0010.COL43)
				listitemvalue.Add(d0010.COL44)
				listitemvalue.Add(d0010.COL45)
				listitemvalue.Add(d0010.COL46)
				listitemvalue.Add(d0010.COL47)
				listitemvalue.Add(d0010.COL48)
				listitemvalue.Add(d0010.COL49)
				listitemvalue.Add(d0010.COL50)

			End If
			ViewBag.FreeItemList = listFreeItem
			ViewBag.AnaItemList = listAnaItem.OrderBy(Function(m) m.HYOJJN)
			ViewBag.listitemvalue = listitemvalue
			d0010.M0150 = m0150

			Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList

			Dim listAnad0022 As New List(Of String())
			Dim listAnad0020 As New List(Of String())
			Dim listAnad0021 As New List(Of String())

			Dim d0022 = db.D0022.Where(Function(m) m.GYOMNO = id).OrderBy(Function(m) m.COLNM).ToList

			For Each d0020_item As D0022 In d0022

				Select Case d0020_item.COLNM

					Case "COL01"
						listAnad0022.Add({m0150.COL01, d0020_item.USERID, "1"})
					Case "COL02"
						listAnad0022.Add({m0150.COL02, d0020_item.USERID, "2"})
					Case "COL03"
						listAnad0022.Add({m0150.COL03, d0020_item.USERID, "3"})
					Case "COL04"
						listAnad0022.Add({m0150.COL04, d0020_item.USERID, "4"})
					Case "COL05"
						listAnad0022.Add({m0150.COL05, d0020_item.USERID, "5"})
					Case "COL06"
						listAnad0022.Add({m0150.COL06, d0020_item.USERID, "6"})
					Case "COL07"
						listAnad0022.Add({m0150.COL07, d0020_item.USERID, "7"})
					Case "COL08"
						listAnad0022.Add({m0150.COL08, d0020_item.USERID, "8"})
					Case "COL09"
						listAnad0022.Add({m0150.COL09, d0020_item.USERID, "9"})
					Case "COL10"
						listAnad0022.Add({m0150.COL10, d0020_item.USERID, "10"})
					Case "COL11"
						listAnad0022.Add({m0150.COL11, d0020_item.USERID, "11"})
					Case "COL12"
						listAnad0022.Add({m0150.COL12, d0020_item.USERID, "12"})
					Case "COL13"
						listAnad0022.Add({m0150.COL13, d0020_item.USERID, "13"})
					Case "COL14"
						listAnad0022.Add({m0150.COL14, d0020_item.USERID, "14"})
					Case "COL15"
						listAnad0022.Add({m0150.COL15, d0020_item.USERID, "15"})
					Case "COL16"
						listAnad0022.Add({m0150.COL16, d0020_item.USERID, "16"})
					Case "COL17"
						listAnad0022.Add({m0150.COL17, d0020_item.USERID, "17"})
					Case "COL18"
						listAnad0022.Add({m0150.COL18, d0020_item.USERID, "18"})
					Case "COL19"
						listAnad0022.Add({m0150.COL19, d0020_item.USERID, "19"})
					Case "COL20"
						listAnad0022.Add({m0150.COL20, d0020_item.USERID, "20"})
					Case "COL21"
						listAnad0022.Add({m0150.COL21, d0020_item.USERID, "21"})
					Case "COL22"
						listAnad0022.Add({m0150.COL22, d0020_item.USERID, "22"})
					Case "COL23"
						listAnad0022.Add({m0150.COL23, d0020_item.USERID, "23"})
					Case "COL24"
						listAnad0022.Add({m0150.COL24, d0020_item.USERID, "24"})
					Case "COL25"
						listAnad0022.Add({m0150.COL25, d0020_item.USERID, "25"})

						'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
					Case "COL26"
						listAnad0022.Add({m0150.COL26, d0020_item.USERID, "26"})
					Case "COL27"
						listAnad0022.Add({m0150.COL27, d0020_item.USERID, "27"})
					Case "COL28"
						listAnad0022.Add({m0150.COL28, d0020_item.USERID, "28"})
					Case "COL29"
						listAnad0022.Add({m0150.COL29, d0020_item.USERID, "29"})
					Case "COL30"
						listAnad0022.Add({m0150.COL30, d0020_item.USERID, "30"})
					Case "COL31"
						listAnad0022.Add({m0150.COL31, d0020_item.USERID, "31"})
					Case "COL32"
						listAnad0022.Add({m0150.COL32, d0020_item.USERID, "32"})
					Case "COL33"
						listAnad0022.Add({m0150.COL33, d0020_item.USERID, "33"})
					Case "COL34"
						listAnad0022.Add({m0150.COL34, d0020_item.USERID, "34"})
					Case "COL35"
						listAnad0022.Add({m0150.COL35, d0020_item.USERID, "35"})
					Case "COL36"
						listAnad0022.Add({m0150.COL36, d0020_item.USERID, "36"})
					Case "COL37"
						listAnad0022.Add({m0150.COL37, d0020_item.USERID, "37"})
					Case "COL38"
						listAnad0022.Add({m0150.COL38, d0020_item.USERID, "38"})
					Case "COL39"
						listAnad0022.Add({m0150.COL39, d0020_item.USERID, "39"})
					Case "COL40"
						listAnad0022.Add({m0150.COL40, d0020_item.USERID, "40"})
					Case "COL41"
						listAnad0022.Add({m0150.COL41, d0020_item.USERID, "41"})
					Case "COL42"
						listAnad0022.Add({m0150.COL42, d0020_item.USERID, "42"})
					Case "COL43"
						listAnad0022.Add({m0150.COL43, d0020_item.USERID, "43"})
					Case "COL44"
						listAnad0022.Add({m0150.COL44, d0020_item.USERID, "44"})
					Case "COL45"
						listAnad0022.Add({m0150.COL45, d0020_item.USERID, "45"})
					Case "COL46"
						listAnad0022.Add({m0150.COL46, d0020_item.USERID, "46"})
					Case "COL47"
						listAnad0022.Add({m0150.COL47, d0020_item.USERID, "47"})
					Case "COL48"
						listAnad0022.Add({m0150.COL48, d0020_item.USERID, "48"})
					Case "COL49"
						listAnad0022.Add({m0150.COL49, d0020_item.USERID, "49"})
					Case "COL50"
						listAnad0022.Add({m0150.COL50, d0020_item.USERID, "50"})
				End Select

			Next

			Dim d0020 = db.D0020.Where(Function(m) (From d0010_t In db.D0010
													Where d0010_t.PGYOMNO = id AndAlso
															d0010_t.OYAGYOMFLG = False
													Select d0010_t.GYOMNO).ToList.Contains(m.GYOMNO)).OrderBy(Function(x) x.COLNM).ToList

			For Each d0020_item As D0020 In d0020

				Select Case d0020_item.COLNM

					Case "COL01"
						listAnad0020.Add({m0150.COL01, d0020_item.USERID, "1"})
					Case "COL02"
						listAnad0020.Add({m0150.COL02, d0020_item.USERID, "2"})
					Case "COL03"
						listAnad0020.Add({m0150.COL03, d0020_item.USERID, "3"})
					Case "COL04"
						listAnad0020.Add({m0150.COL04, d0020_item.USERID, "4"})
					Case "COL05"
						listAnad0020.Add({m0150.COL05, d0020_item.USERID, "5"})
					Case "COL06"
						listAnad0020.Add({m0150.COL06, d0020_item.USERID, "6"})
					Case "COL07"
						listAnad0020.Add({m0150.COL07, d0020_item.USERID, "7"})
					Case "COL08"
						listAnad0020.Add({m0150.COL08, d0020_item.USERID, "8"})
					Case "COL09"
						listAnad0020.Add({m0150.COL09, d0020_item.USERID, "9"})
					Case "COL10"
						listAnad0020.Add({m0150.COL10, d0020_item.USERID, "10"})
					Case "COL11"
						listAnad0020.Add({m0150.COL11, d0020_item.USERID, "11"})
					Case "COL12"
						listAnad0020.Add({m0150.COL12, d0020_item.USERID, "12"})
					Case "COL13"
						listAnad0020.Add({m0150.COL13, d0020_item.USERID, "13"})
					Case "COL14"
						listAnad0020.Add({m0150.COL14, d0020_item.USERID, "14"})
					Case "COL15"
						listAnad0020.Add({m0150.COL15, d0020_item.USERID, "15"})
					Case "COL16"
						listAnad0020.Add({m0150.COL16, d0020_item.USERID, "16"})
					Case "COL17"
						listAnad0020.Add({m0150.COL17, d0020_item.USERID, "17"})
					Case "COL18"
						listAnad0020.Add({m0150.COL18, d0020_item.USERID, "18"})
					Case "COL19"
						listAnad0020.Add({m0150.COL19, d0020_item.USERID, "19"})
					Case "COL20"
						listAnad0020.Add({m0150.COL20, d0020_item.USERID, "20"})
					Case "COL21"
						listAnad0020.Add({m0150.COL21, d0020_item.USERID, "21"})
					Case "COL22"
						listAnad0020.Add({m0150.COL22, d0020_item.USERID, "22"})
					Case "COL23"
						listAnad0020.Add({m0150.COL23, d0020_item.USERID, "23"})
					Case "COL24"
						listAnad0020.Add({m0150.COL24, d0020_item.USERID, "24"})
					Case "COL25"
						listAnad0020.Add({m0150.COL25, d0020_item.USERID, "25"})

						'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
					Case "COL26"
						listAnad0020.Add({m0150.COL26, d0020_item.USERID, "26"})
					Case "COL27"
						listAnad0020.Add({m0150.COL27, d0020_item.USERID, "27"})
					Case "COL28"
						listAnad0020.Add({m0150.COL28, d0020_item.USERID, "28"})
					Case "COL29"
						listAnad0020.Add({m0150.COL29, d0020_item.USERID, "29"})
					Case "COL30"
						listAnad0020.Add({m0150.COL30, d0020_item.USERID, "30"})
					Case "COL31"
						listAnad0020.Add({m0150.COL31, d0020_item.USERID, "31"})
					Case "COL32"
						listAnad0020.Add({m0150.COL32, d0020_item.USERID, "32"})
					Case "COL33"
						listAnad0020.Add({m0150.COL33, d0020_item.USERID, "33"})
					Case "COL34"
						listAnad0020.Add({m0150.COL34, d0020_item.USERID, "34"})
					Case "COL35"
						listAnad0020.Add({m0150.COL35, d0020_item.USERID, "35"})
					Case "COL36"
						listAnad0020.Add({m0150.COL36, d0020_item.USERID, "36"})
					Case "COL37"
						listAnad0020.Add({m0150.COL37, d0020_item.USERID, "37"})
					Case "COL38"
						listAnad0020.Add({m0150.COL38, d0020_item.USERID, "38"})
					Case "COL39"
						listAnad0020.Add({m0150.COL39, d0020_item.USERID, "39"})
					Case "COL40"
						listAnad0020.Add({m0150.COL40, d0020_item.USERID, "40"})
					Case "COL41"
						listAnad0020.Add({m0150.COL41, d0020_item.USERID, "41"})
					Case "COL42"
						listAnad0020.Add({m0150.COL42, d0020_item.USERID, "42"})
					Case "COL43"
						listAnad0020.Add({m0150.COL43, d0020_item.USERID, "43"})
					Case "COL44"
						listAnad0020.Add({m0150.COL44, d0020_item.USERID, "44"})
					Case "COL45"
						listAnad0020.Add({m0150.COL45, d0020_item.USERID, "45"})
					Case "COL46"
						listAnad0020.Add({m0150.COL46, d0020_item.USERID, "46"})
					Case "COL47"
						listAnad0020.Add({m0150.COL47, d0020_item.USERID, "47"})
					Case "COL48"
						listAnad0020.Add({m0150.COL48, d0020_item.USERID, "48"})
					Case "COL49"
						listAnad0020.Add({m0150.COL49, d0020_item.USERID, "49"})
					Case "COL50"
						listAnad0020.Add({m0150.COL50, d0020_item.USERID, "50"})
				End Select

			Next

			Dim d0021 = db.D0021.Where(Function(m) (From d0010_t In db.D0010
													Where d0010_t.PGYOMNO = id AndAlso
													d0010_t.OYAGYOMFLG = False
													Select d0010_t.GYOMNO).ToList.Contains(m.GYOMNO)).OrderBy(Function(x) x.COLNM).ToList

			For Each d0021_item As D0021 In d0021
				'd0021_item.ANNACATNM = "仮アナ"
				Select Case d0021_item.COLNM

					Case "COL01"
						listAnad0021.Add({m0150.COL01, d0021_item.ANNACATNM, "1"})
					Case "COL02"
						listAnad0021.Add({m0150.COL02, d0021_item.ANNACATNM, "2"})
					Case "COL03"
						listAnad0021.Add({m0150.COL03, d0021_item.ANNACATNM, "3"})
					Case "COL04"
						listAnad0021.Add({m0150.COL04, d0021_item.ANNACATNM, "4"})
					Case "COL05"
						listAnad0021.Add({m0150.COL05, d0021_item.ANNACATNM, "5"})
					Case "COL06"
						listAnad0021.Add({m0150.COL06, d0021_item.ANNACATNM, "6"})
					Case "COL07"
						listAnad0021.Add({m0150.COL07, d0021_item.ANNACATNM, "7"})
					Case "COL08"
						listAnad0021.Add({m0150.COL08, d0021_item.ANNACATNM, "8"})
					Case "COL09"
						listAnad0021.Add({m0150.COL09, d0021_item.ANNACATNM, "9"})
					Case "COL10"
						listAnad0021.Add({m0150.COL10, d0021_item.ANNACATNM, "10"})
					Case "COL11"
						listAnad0021.Add({m0150.COL11, d0021_item.ANNACATNM, "11"})
					Case "COL12"
						listAnad0021.Add({m0150.COL12, d0021_item.ANNACATNM, "12"})
					Case "COL13"
						listAnad0021.Add({m0150.COL13, d0021_item.ANNACATNM, "13"})
					Case "COL14"
						listAnad0021.Add({m0150.COL14, d0021_item.ANNACATNM, "14"})
					Case "COL15"
						listAnad0021.Add({m0150.COL15, d0021_item.ANNACATNM, "15"})
					Case "COL16"
						listAnad0021.Add({m0150.COL16, d0021_item.ANNACATNM, "16"})
					Case "COL17"
						listAnad0021.Add({m0150.COL17, d0021_item.ANNACATNM, "17"})
					Case "COL18"
						listAnad0021.Add({m0150.COL18, d0021_item.ANNACATNM, "18"})
					Case "COL19"
						listAnad0021.Add({m0150.COL19, d0021_item.ANNACATNM, "19"})
					Case "COL20"
						listAnad0021.Add({m0150.COL20, d0021_item.ANNACATNM, "20"})
					Case "COL21"
						listAnad0021.Add({m0150.COL21, d0021_item.ANNACATNM, "21"})
					Case "COL22"
						listAnad0021.Add({m0150.COL22, d0021_item.ANNACATNM, "22"})
					Case "COL23"
						listAnad0021.Add({m0150.COL23, d0021_item.ANNACATNM, "23"})
					Case "COL24"
						listAnad0021.Add({m0150.COL24, d0021_item.ANNACATNM, "24"})
					Case "COL25"
						listAnad0021.Add({m0150.COL25, d0021_item.ANNACATNM, "25"})

						'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
					Case "COL26"
						listAnad0021.Add({m0150.COL26, d0021_item.ANNACATNM, "26"})
					Case "COL27"
						listAnad0021.Add({m0150.COL27, d0021_item.ANNACATNM, "27"})
					Case "COL28"
						listAnad0021.Add({m0150.COL28, d0021_item.ANNACATNM, "28"})
					Case "COL29"
						listAnad0021.Add({m0150.COL29, d0021_item.ANNACATNM, "29"})
					Case "COL30"
						listAnad0021.Add({m0150.COL30, d0021_item.ANNACATNM, "30"})
					Case "COL31"
						listAnad0021.Add({m0150.COL31, d0021_item.ANNACATNM, "31"})
					Case "COL32"
						listAnad0021.Add({m0150.COL32, d0021_item.ANNACATNM, "32"})
					Case "COL33"
						listAnad0021.Add({m0150.COL33, d0021_item.ANNACATNM, "33"})
					Case "COL34"
						listAnad0021.Add({m0150.COL34, d0021_item.ANNACATNM, "34"})
					Case "COL35"
						listAnad0021.Add({m0150.COL35, d0021_item.ANNACATNM, "35"})
					Case "COL36"
						listAnad0021.Add({m0150.COL36, d0021_item.ANNACATNM, "36"})
					Case "COL37"
						listAnad0021.Add({m0150.COL37, d0021_item.ANNACATNM, "37"})
					Case "COL38"
						listAnad0021.Add({m0150.COL38, d0021_item.ANNACATNM, "38"})
					Case "COL39"
						listAnad0021.Add({m0150.COL39, d0021_item.ANNACATNM, "39"})
					Case "COL40"
						listAnad0021.Add({m0150.COL40, d0021_item.ANNACATNM, "40"})
					Case "COL41"
						listAnad0021.Add({m0150.COL41, d0021_item.ANNACATNM, "41"})
					Case "COL42"
						listAnad0021.Add({m0150.COL42, d0021_item.ANNACATNM, "42"})
					Case "COL43"
						listAnad0021.Add({m0150.COL43, d0021_item.ANNACATNM, "43"})
					Case "COL44"
						listAnad0021.Add({m0150.COL44, d0021_item.ANNACATNM, "44"})
					Case "COL45"
						listAnad0021.Add({m0150.COL45, d0021_item.ANNACATNM, "45"})
					Case "COL46"
						listAnad0021.Add({m0150.COL46, d0021_item.ANNACATNM, "46"})
					Case "COL47"
						listAnad0021.Add({m0150.COL47, d0021_item.ANNACATNM, "47"})
					Case "COL48"
						listAnad0021.Add({m0150.COL48, d0021_item.ANNACATNM, "48"})
					Case "COL49"
						listAnad0021.Add({m0150.COL49, d0021_item.ANNACATNM, "49"})
					Case "COL50"
						listAnad0021.Add({m0150.COL50, d0021_item.ANNACATNM, "50"})
				End Select

			Next

			Dim ColNm As String = ""
			For int As Integer = listAnaItem.Count - 1 To 0 Step -1
				ColNm = listAnaItem.Item(int).COLNAME
				For Each listAnad0022_item As String() In listAnad0022
					If listAnaItem.Item(int) IsNot Nothing Then
						If listAnad0022_item(0) = listAnaItem.Item(int).COLNAME Then
							If listAnaItem.Where(Function(m) m.COLNAME = ColNm And m.ANNCATNAME <> "").Count > 0 Then
								listAnaItem.Add((New With {.COLNAME = listAnad0022_item(0), .ANNCATNAME = listAnad0022_item(1), .COLINDEX = listAnad0022_item(2), .TBLNM = "D0022", .HYOJJN = Short.Parse(listAnaItem.Item(int).HYOJJN)}))
							Else
								listAnaItem.Item(int).COLNAME = listAnad0022_item(0)
								listAnaItem.Item(int).ANNCATNAME = listAnad0022_item(1)
								listAnaItem.Item(int).COLINDEX = listAnad0022_item(2)
								listAnaItem.Item(int).TBLNM = "D0022"
							End If
						End If
					End If
				Next
				For Each listAnad0020_item As String() In listAnad0020
					If listAnaItem.Item(int) IsNot Nothing Then
						If listAnad0020_item(0) = listAnaItem.Item(int).COLNAME Then
							If listAnaItem.Where(Function(m) m.COLNAME = ColNm And m.ANNCATNAME <> "").Count > 0 Then
								listAnaItem.Add((New With {.COLNAME = listAnad0020_item(0), .ANNCATNAME = listAnad0020_item(1), .COLINDEX = listAnad0020_item(2), .TBLNM = "D0020", .HYOJJN = Short.Parse(listAnaItem.Item(int).HYOJJN)}))
							Else
								listAnaItem.Item(int).COLNAME = listAnad0020_item(0)
								listAnaItem.Item(int).ANNCATNAME = listAnad0020_item(1)
								listAnaItem.Item(int).COLINDEX = listAnad0020_item(2)
								listAnaItem.Item(int).TBLNM = "D0020"
							End If
						End If
					End If
				Next
				For Each listAnad0021_item As String() In listAnad0021
					If listAnaItem.Item(int) IsNot Nothing Then
						If listAnad0021_item(0) = listAnaItem.Item(int).COLNAME Then
							If listAnaItem.Where(Function(m) m.COLNAME = ColNm And m.ANNCATNAME <> "").Count > 0 Then
								listAnaItem.Add((New With {.COLNAME = listAnad0021_item(0), .ANNCATNAME = listAnad0021_item(1), .COLINDEX = listAnad0021_item(2), .TBLNM = "D0021", .HYOJJN = Short.Parse(listAnaItem.Item(int).HYOJJN)}))
							Else
								listAnaItem.Item(int).COLNAME = listAnad0021_item(0)
								listAnaItem.Item(int).ANNCATNAME = listAnad0021_item(1)
								listAnaItem.Item(int).COLINDEX = listAnad0021_item(2)
								listAnaItem.Item(int).TBLNM = "D0021"
							End If
						End If
					End If
				Next
			Next

			ViewBag.listAnad0022 = listAnad0022
			ViewBag.listAnad0020 = listAnad0020
			ViewBag.listAnad0021 = listAnad0021

			ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)
			If lastForm IsNot Nothing Then
				ViewBag.lastForm = lastForm
			End If

			Return View(d0010)

		End Function

		'修正モッド
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Edit(screenD0010 As D0010, ByVal lastForm As String) As ActionResult

			'If Nothing Then
			If IsNothing(screenD0010) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			'If Session Time Out Then Return Back to Main Page
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If lastForm IsNot Nothing Then
				ViewBag.lastForm = lastForm
			End If

			'If Error Is not there
			If ModelState.IsValid Then

				Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
				sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try

						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

						Dim d0010_old As D0010 = db.D0010.Find(screenD0010.GYOMNO)
						db.Entry(d0010_old).State = EntityState.Detached
						'業務テーブルからダ－タ取得
						Dim d0010 As D0010 = db.D0010.Find(screenD0010.GYOMNO)

						d0010.JTJKNST = screenD0010.JTJKNST
						d0010.JTJKNED = screenD0010.JTJKNED

						d0010.GYOMYMD = screenD0010.GYOMYMD
						If screenD0010.GYOMYMDED Is Nothing Then
							d0010.GYOMYMDED = screenD0010.GYOMYMD
						Else
							d0010.GYOMYMDED = screenD0010.GYOMYMDED
						End If

						d0010.KSKJKNST = ChangeToHHMM(screenD0010.KSKJKNST)
						d0010.KSKJKNED = ChangeToHHMM(screenD0010.KSKJKNED)
						d0010.JTJKNST = GetJtjkn(d0010.GYOMYMD, d0010.KSKJKNST)
						d0010.JTJKNED = GetJtjkn(d0010.GYOMYMDED, d0010.KSKJKNED)

						d0010.OAJKNST = ChangeToHHMM(screenD0010.OAJKNST)
						d0010.OAJKNED = ChangeToHHMM(screenD0010.OAJKNED)

						d0010.CATCD = screenD0010.CATCD
						d0010.BANGUMINM = screenD0010.BANGUMINM
						d0010.NAIYO = screenD0010.NAIYO
						d0010.BASYO = screenD0010.BASYO
						d0010.BIKO = screenD0010.BIKO
						d0010.BANGUMITANTO = screenD0010.BANGUMITANTO
						d0010.BANGUMIRENRK = screenD0010.BANGUMIRENRK


						If d0010.GYOMYMD <> d0010.GYOMYMDED Then
							d0010.RNZK = True
						Else
							d0010.RNZK = False
						End If
						'd0010.PGYOMNO = screenD0010.PGYOMNO
						'd0010.IKTFLG = screenD0010.IKTFLG
						'd0010.IKTUSERID = screenD0010.IKTUSERID
						'd0010.IKKATUNO = screenD0010.IKKATUNO
						'd0010.OYAGYOMFLG = True
						d0010.SAIJKNST = ChangeToHHMM(screenD0010.SAIJKNST)
						d0010.SAIJKNED = ChangeToHHMM(screenD0010.SAIJKNED)

						If screenD0010.FreeTxtBxList IsNot Nothing Then
							Dim iterationCnt As Int16 = 0
							For Each item In screenD0010.FreeTxtBxList
								If iterationCnt = 0 Then
									d0010.COL01 = item
								End If
								If iterationCnt = 1 Then
									d0010.COL02 = item
								End If
								If iterationCnt = 2 Then
									d0010.COL03 = item
								End If
								If iterationCnt = 3 Then
									d0010.COL04 = item
								End If
								If iterationCnt = 4 Then
									d0010.COL05 = item
								End If
								If iterationCnt = 5 Then
									d0010.COL06 = item
								End If
								If iterationCnt = 6 Then
									d0010.COL07 = item
								End If
								If iterationCnt = 7 Then
									d0010.COL08 = item
								End If
								If iterationCnt = 8 Then
									d0010.COL09 = item
								End If
								If iterationCnt = 9 Then
									d0010.COL10 = item
								End If
								If iterationCnt = 10 Then
									d0010.COL11 = item
								End If
								If iterationCnt = 11 Then
									d0010.COL12 = item
								End If
								If iterationCnt = 12 Then
									d0010.COL13 = item
								End If
								If iterationCnt = 13 Then
									d0010.COL14 = item
								End If
								If iterationCnt = 14 Then
									d0010.COL15 = item
								End If
								If iterationCnt = 15 Then
									d0010.COL16 = item
								End If
								If iterationCnt = 16 Then
									d0010.COL17 = item
								End If
								If iterationCnt = 17 Then
									d0010.COL18 = item
								End If
								If iterationCnt = 18 Then
									d0010.COL19 = item
								End If
								If iterationCnt = 19 Then
									d0010.COL20 = item
								End If
								If iterationCnt = 20 Then
									d0010.COL21 = item
								End If
								If iterationCnt = 21 Then
									d0010.COL22 = item
								End If
								If iterationCnt = 22 Then
									d0010.COL23 = item
								End If
								If iterationCnt = 23 Then
									d0010.COL24 = item
								End If
								If iterationCnt = 24 Then
									d0010.COL25 = item
								End If

								'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
								If iterationCnt = 25 Then
									d0010.COL26 = item
								End If
								If iterationCnt = 26 Then
									d0010.COL27 = item
								End If
								If iterationCnt = 27 Then
									d0010.COL28 = item
								End If
								If iterationCnt = 28 Then
									d0010.COL29 = item
								End If
								If iterationCnt = 29 Then
									d0010.COL30 = item
								End If
								If iterationCnt = 30 Then
									d0010.COL31 = item
								End If
								If iterationCnt = 31 Then
									d0010.COL32 = item
								End If
								If iterationCnt = 32 Then
									d0010.COL33 = item
								End If
								If iterationCnt = 33 Then
									d0010.COL34 = item
								End If
								If iterationCnt = 34 Then
									d0010.COL35 = item
								End If
								If iterationCnt = 35 Then
									d0010.COL36 = item
								End If
								If iterationCnt = 36 Then
									d0010.COL37 = item
								End If
								If iterationCnt = 37 Then
									d0010.COL38 = item
								End If
								If iterationCnt = 38 Then
									d0010.COL39 = item
								End If
								If iterationCnt = 39 Then
									d0010.COL40 = item
								End If
								If iterationCnt = 40 Then
									d0010.COL41 = item
								End If
								If iterationCnt = 41 Then
									d0010.COL42 = item
								End If
								If iterationCnt = 42 Then
									d0010.COL43 = item
								End If
								If iterationCnt = 43 Then
									d0010.COL44 = item
								End If
								If iterationCnt = 44 Then
									d0010.COL45 = item
								End If
								If iterationCnt = 45 Then
									d0010.COL46 = item
								End If
								If iterationCnt = 46 Then
									d0010.COL47 = item
								End If
								If iterationCnt = 47 Then
									d0010.COL48 = item
								End If
								If iterationCnt = 48 Then
									d0010.COL49 = item
								End If
								If iterationCnt = 49 Then
									d0010.COL50 = item
								End If

								iterationCnt = iterationCnt + 1
							Next
						End If

						If screenD0010.D0022 IsNot Nothing Then

							'Get All Data From D0022 Related to GYOMNO
							Dim Obj_D0022 = db.D0022.Where(Function(m) m.GYOMNO = screenD0010.GYOMNO).ToList

							For Each item In screenD0010.D0022
								'If dropdown is not blank
								If item.COLIDX = 1 Then
									item.COLNM = "COL01"
								End If
								If item.COLIDX = 2 Then
									item.COLNM = "COL02"
								End If
								If item.COLIDX = 3 Then
									item.COLNM = "COL03"
								End If
								If item.COLIDX = 4 Then
									item.COLNM = "COL04"
								End If
								If item.COLIDX = 5 Then
									item.COLNM = "COL05"
								End If
								If item.COLIDX = 6 Then
									item.COLNM = "COL06"
								End If
								If item.COLIDX = 7 Then
									item.COLNM = "COL07"
								End If
								If item.COLIDX = 8 Then
									item.COLNM = "COL08"
								End If
								If item.COLIDX = 9 Then
									item.COLNM = "COL09"
								End If
								If item.COLIDX = 10 Then
									item.COLNM = "COL10"
								End If
								If item.COLIDX = 11 Then
									item.COLNM = "COL11"
								End If
								If item.COLIDX = 12 Then
									item.COLNM = "COL12"
								End If
								If item.COLIDX = 13 Then
									item.COLNM = "COL13"
								End If
								If item.COLIDX = 14 Then
									item.COLNM = "COL14"
								End If
								If item.COLIDX = 15 Then
									item.COLNM = "COL15"
								End If
								If item.COLIDX = 16 Then
									item.COLNM = "COL16"
								End If
								If item.COLIDX = 17 Then
									item.COLNM = "COL17"
								End If
								If item.COLIDX = 18 Then
									item.COLNM = "COL18"
								End If
								If item.COLIDX = 19 Then
									item.COLNM = "COL19"
								End If
								If item.COLIDX = 20 Then
									item.COLNM = "COL20"
								End If
								If item.COLIDX = 21 Then
									item.COLNM = "COL21"
								End If
								If item.COLIDX = 22 Then
									item.COLNM = "COL22"
								End If
								If item.COLIDX = 23 Then
									item.COLNM = "COL23"
								End If
								If item.COLIDX = 24 Then
									item.COLNM = "COL24"
								End If
								If item.COLIDX = 25 Then
									item.COLNM = "COL25"
								End If

								'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
								If item.COLIDX = 26 Then
									item.COLNM = "COL26"
								End If
								If item.COLIDX = 27 Then
									item.COLNM = "COL27"
								End If
								If item.COLIDX = 28 Then
									item.COLNM = "COL28"
								End If
								If item.COLIDX = 29 Then
									item.COLNM = "COL29"
								End If
								If item.COLIDX = 30 Then
									item.COLNM = "COL30"
								End If
								If item.COLIDX = 31 Then
									item.COLNM = "COL31"
								End If
								If item.COLIDX = 32 Then
									item.COLNM = "COL32"
								End If
								If item.COLIDX = 33 Then
									item.COLNM = "COL33"
								End If
								If item.COLIDX = 34 Then
									item.COLNM = "COL34"
								End If
								If item.COLIDX = 35 Then
									item.COLNM = "COL35"
								End If
								If item.COLIDX = 36 Then
									item.COLNM = "COL36"
								End If
								If item.COLIDX = 37 Then
									item.COLNM = "COL37"
								End If
								If item.COLIDX = 38 Then
									item.COLNM = "COL38"
								End If
								If item.COLIDX = 39 Then
									item.COLNM = "COL39"
								End If
								If item.COLIDX = 40 Then
									item.COLNM = "COL40"
								End If
								If item.COLIDX = 41 Then
									item.COLNM = "COL41"
								End If
								If item.COLIDX = 42 Then
									item.COLNM = "COL42"
								End If
								If item.COLIDX = 43 Then
									item.COLNM = "COL43"
								End If
								If item.COLIDX = 44 Then
									item.COLNM = "COL44"
								End If
								If item.COLIDX = 45 Then
									item.COLNM = "COL45"
								End If
								If item.COLIDX = 46 Then
									item.COLNM = "COL46"
								End If
								If item.COLIDX = 47 Then
									item.COLNM = "COL47"
								End If
								If item.COLIDX = 48 Then
									item.COLNM = "COL48"
								End If
								If item.COLIDX = 49 Then
									item.COLNM = "COL49"
								End If
								If item.COLIDX = 50 Then
									item.COLNM = "COL50"
								End If

								'Update D0022
								Dim Obj_D0020_ByColName = Obj_D0022.Where(Function(m) m.COLNM = item.COLNM).ToList
								If item.USERID <> 0 AndAlso Obj_D0020_ByColName IsNot Nothing AndAlso Obj_D0020_ByColName.Count > 0 Then
									Obj_D0020_ByColName(0).USERID = item.USERID

								ElseIf item.USERID = 0 AndAlso Obj_D0020_ByColName IsNot Nothing AndAlso Obj_D0020_ByColName.Count > 0 Then

									db.D0022.Remove(Obj_D0020_ByColName(0))

								ElseIf item.USERID <> 0 Then
									Dim insertD0022 As New D0022
									insertD0022.GYOMNO = screenD0010.GYOMNO
									insertD0022.COLNM = item.COLNM
									insertD0022.USERID = item.USERID
									insertD0022.COLNM = item.COLNM
									'insertD0022.FIXED = False
									db.D0022.Add(insertD0022)

								End If

							Next
						End If



						Dim d0020 = db.D0020.Where(Function(m) (From d0010_t In db.D0010
																Where d0010_t.PGYOMNO = screenD0010.GYOMNO AndAlso
																		d0010_t.OYAGYOMFLG = False
																Select d0010_t.GYOMNO).ToList.Contains(m.GYOMNO)).ToList

						'If KakteiSori Has  done than Date is Disabled and do UPDATE PROCEDURE
						If d0020.Count > 0 Then
							'If False Then

							'Dim Obj_D0010 = (From d0010_t In db.D0010
							'                 Where d0010_t.PGYOMNO = screenD0010.GYOMNO AndAlso
							'                                        d0010_t.OYAGYOMFLG = True
							'                 Select d0010_t).ToList
							' db.D0010.RemoveRange(Obj_D0010)

							'For Each D0010_d0022 As D0010 In Obj_D0010

							'Next
							Dim d0010_mainChild = (From d0010_child In db.D0010
												   Where d0010_child.PGYOMNO = screenD0010.GYOMNO AndAlso
																	d0010_child.OYAGYOMFLG = True
												   Select d0010_child).ToList

							Dim d0010_kakteichild = (From d0010_child In db.D0010
													 Where d0010_child.PGYOMNO = screenD0010.GYOMNO AndAlso
																	d0010_child.OYAGYOMFLG = False AndAlso
															   d0010_child.SPORT_OYAFLG = True
													 Select d0010_child).ToList

							Dim d0010_kakteiChild_Child = db.D0010.Where(Function(x) (From d0010_child In db.D0010
																					  Where d0010_child.PGYOMNO = screenD0010.GYOMNO AndAlso
																							d0010_child.OYAGYOMFLG = False AndAlso
																						   d0010_child.SPORT_OYAFLG = True
																					  Select d0010_child.GYOMNO).ToList.Contains(x.PGYOMNO)).ToList

							d0010_mainChild.AddRange(d0010_kakteichild)
							d0010_mainChild.AddRange(d0010_kakteiChild_Child)

							Dim count As Integer = 1
							Dim start_dateChild As Date = screenD0010.GYOMYMD
							Dim DaysCount As Integer = DateRange(start_dateChild.AddDays(1), d0010.GYOMYMDED).Count

							For Each insertD0010 As D0010 In d0010_mainChild

								If insertD0010.OYAGYOMFLG = True Then
									Dim d0022_delete = db.D0022.Where(Function(x) x.GYOMNO = insertD0010.GYOMNO).ToList
									db.D0022.RemoveRange(d0022_delete)
								End If


								' Dim insertD0010 As D0010 = New D0010

								'insertD0010.GYOMNO = GetMaxGyomno() + count

								'Start Date
								'insertD0010.GYOMYMD = Day.
								'insertD0010.GYOMYMDED = Day()

								'If DaysCount = count Then
								'    insertD0010.KSKJKNST = "0000"
								'    insertD0010.KSKJKNED = ChangeToHHMM(screenD0010.KSKJKNED)
								'Else
								'    insertD0010.KSKJKNST = "0000"
								'    insertD0010.KSKJKNED = "2400"
								'End If


								insertD0010.JTJKNST = GetJtjkn(insertD0010.GYOMYMD, insertD0010.KSKJKNST)
								insertD0010.JTJKNED = GetJtjkn(insertD0010.GYOMYMDED, insertD0010.KSKJKNED)
								insertD0010.BANGUMINM = screenD0010.BANGUMINM
								insertD0010.OAJKNST = ChangeToHHMM(screenD0010.OAJKNST)
								insertD0010.OAJKNED = ChangeToHHMM(screenD0010.OAJKNED)
								insertD0010.BASYO = screenD0010.BASYO
								insertD0010.BIKO = screenD0010.BIKO

								insertD0010.CATCD = screenD0010.CATCD
								insertD0010.NAIYO = screenD0010.NAIYO
								insertD0010.BANGUMITANTO = screenD0010.BANGUMITANTO
								insertD0010.BANGUMIRENRK = screenD0010.BANGUMIRENRK
								'insertD0010.RNZK = True

								'insertD0010.PGYOMNO = d0010.GYOMNO

								'UnComment below code of SPORTFLG,OYAGYOMFLG,SPORTCATCD,SPORTSUBCATCD,SAIJKNST,SAIJKNED,COL1,COL2......COL20
								'Once db TBL D0010 altered.

								'insertD0010.SPORTFLG = True
								'insertD0010.OYAGYOMFLG = True
								'insertD0010.SPORTCATCD = screenD0010.SPORTCATCD
								'insertD0010.SPORTSUBCATCD = screenD0010.SPORTSUBCATCD
								insertD0010.SAIJKNST = ChangeToHHMM(screenD0010.SAIJKNST)
								insertD0010.SAIJKNED = ChangeToHHMM(screenD0010.SAIJKNED)

								If screenD0010.FreeTxtBxList IsNot Nothing Then
									Dim iterationCnt As Int16 = 0
									For Each item In screenD0010.FreeTxtBxList
										If iterationCnt = 0 Then
											insertD0010.COL01 = item
										End If
										If iterationCnt = 1 Then
											insertD0010.COL02 = item
										End If
										If iterationCnt = 2 Then
											insertD0010.COL03 = item
										End If
										If iterationCnt = 3 Then
											insertD0010.COL04 = item
										End If
										If iterationCnt = 4 Then
											insertD0010.COL05 = item
										End If
										If iterationCnt = 5 Then
											insertD0010.COL06 = item
										End If
										If iterationCnt = 6 Then
											insertD0010.COL07 = item
										End If
										If iterationCnt = 7 Then
											insertD0010.COL08 = item
										End If
										If iterationCnt = 8 Then
											insertD0010.COL09 = item
										End If
										If iterationCnt = 9 Then
											insertD0010.COL10 = item
										End If
										If iterationCnt = 10 Then
											insertD0010.COL11 = item
										End If
										If iterationCnt = 11 Then
											insertD0010.COL12 = item
										End If
										If iterationCnt = 12 Then
											insertD0010.COL13 = item
										End If
										If iterationCnt = 13 Then
											insertD0010.COL14 = item
										End If
										If iterationCnt = 14 Then
											insertD0010.COL15 = item
										End If
										If iterationCnt = 15 Then
											insertD0010.COL16 = item
										End If
										If iterationCnt = 16 Then
											insertD0010.COL17 = item
										End If
										If iterationCnt = 17 Then
											insertD0010.COL18 = item
										End If
										If iterationCnt = 18 Then
											insertD0010.COL19 = item
										End If
										If iterationCnt = 19 Then
											insertD0010.COL20 = item
										End If
										If iterationCnt = 20 Then
											insertD0010.COL21 = item
										End If
										If iterationCnt = 21 Then
											insertD0010.COL22 = item
										End If
										If iterationCnt = 22 Then
											insertD0010.COL23 = item
										End If
										If iterationCnt = 23 Then
											insertD0010.COL24 = item
										End If
										If iterationCnt = 24 Then
											insertD0010.COL25 = item
										End If

										'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
										If iterationCnt = 25 Then
											insertD0010.COL26 = item
										End If
										If iterationCnt = 26 Then
											insertD0010.COL27 = item
										End If
										If iterationCnt = 27 Then
											insertD0010.COL28 = item
										End If
										If iterationCnt = 28 Then
											insertD0010.COL29 = item
										End If
										If iterationCnt = 29 Then
											insertD0010.COL30 = item
										End If
										If iterationCnt = 30 Then
											insertD0010.COL31 = item
										End If
										If iterationCnt = 31 Then
											insertD0010.COL32 = item
										End If
										If iterationCnt = 32 Then
											insertD0010.COL33 = item
										End If
										If iterationCnt = 33 Then
											insertD0010.COL34 = item
										End If
										If iterationCnt = 34 Then
											insertD0010.COL35 = item
										End If
										If iterationCnt = 35 Then
											insertD0010.COL36 = item
										End If
										If iterationCnt = 36 Then
											insertD0010.COL37 = item
										End If
										If iterationCnt = 37 Then
											insertD0010.COL38 = item
										End If
										If iterationCnt = 38 Then
											insertD0010.COL39 = item
										End If
										If iterationCnt = 39 Then
											insertD0010.COL40 = item
										End If
										If iterationCnt = 40 Then
											insertD0010.COL41 = item
										End If
										If iterationCnt = 41 Then
											insertD0010.COL42 = item
										End If
										If iterationCnt = 42 Then
											insertD0010.COL43 = item
										End If
										If iterationCnt = 43 Then
											insertD0010.COL44 = item
										End If
										If iterationCnt = 44 Then
											insertD0010.COL45 = item
										End If
										If iterationCnt = 45 Then
											insertD0010.COL46 = item
										End If
										If iterationCnt = 46 Then
											insertD0010.COL47 = item
										End If
										If iterationCnt = 47 Then
											insertD0010.COL48 = item
										End If
										If iterationCnt = 48 Then
											insertD0010.COL49 = item
										End If
										If iterationCnt = 49 Then
											insertD0010.COL50 = item
										End If

										iterationCnt = iterationCnt + 1
									Next
								End If

								If insertD0010.OYAGYOMFLG = True Then

									If screenD0010.D0022 IsNot Nothing Then
										For Each item In screenD0010.D0022
											'If dropdown is not blank
											If item.USERID <> 0 Then
												If item.COLIDX = 1 Then
													item.COLNM = "COL01"
												End If
												If item.COLIDX = 2 Then
													item.COLNM = "COL02"
												End If
												If item.COLIDX = 3 Then
													item.COLNM = "COL03"
												End If
												If item.COLIDX = 4 Then
													item.COLNM = "COL04"
												End If
												If item.COLIDX = 5 Then
													item.COLNM = "COL05"
												End If
												If item.COLIDX = 6 Then
													item.COLNM = "COL06"
												End If
												If item.COLIDX = 7 Then
													item.COLNM = "COL07"
												End If
												If item.COLIDX = 8 Then
													item.COLNM = "COL08"
												End If
												If item.COLIDX = 9 Then
													item.COLNM = "COL09"
												End If
												If item.COLIDX = 10 Then
													item.COLNM = "COL10"
												End If
												If item.COLIDX = 11 Then
													item.COLNM = "COL11"
												End If
												If item.COLIDX = 12 Then
													item.COLNM = "COL12"
												End If
												If item.COLIDX = 13 Then
													item.COLNM = "COL13"
												End If
												If item.COLIDX = 14 Then
													item.COLNM = "COL14"
												End If
												If item.COLIDX = 15 Then
													item.COLNM = "COL15"
												End If
												If item.COLIDX = 16 Then
													item.COLNM = "COL16"
												End If
												If item.COLIDX = 17 Then
													item.COLNM = "COL17"
												End If
												If item.COLIDX = 18 Then
													item.COLNM = "COL18"
												End If
												If item.COLIDX = 19 Then
													item.COLNM = "COL19"
												End If
												If item.COLIDX = 20 Then
													item.COLNM = "COL20"
												End If
												If item.COLIDX = 21 Then
													item.COLNM = "COL21"
												End If
												If item.COLIDX = 22 Then
													item.COLNM = "COL22"
												End If
												If item.COLIDX = 23 Then
													item.COLNM = "COL23"
												End If
												If item.COLIDX = 24 Then
													item.COLNM = "COL24"
												End If
												If item.COLIDX = 25 Then
													item.COLNM = "COL25"
												End If

												'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
												If item.COLIDX = 26 Then
													item.COLNM = "COL26"
												End If
												If item.COLIDX = 27 Then
													item.COLNM = "COL27"
												End If
												If item.COLIDX = 28 Then
													item.COLNM = "COL28"
												End If
												If item.COLIDX = 29 Then
													item.COLNM = "COL29"
												End If
												If item.COLIDX = 30 Then
													item.COLNM = "COL30"
												End If
												If item.COLIDX = 31 Then
													item.COLNM = "COL31"
												End If
												If item.COLIDX = 32 Then
													item.COLNM = "COL32"
												End If
												If item.COLIDX = 33 Then
													item.COLNM = "COL33"
												End If
												If item.COLIDX = 34 Then
													item.COLNM = "COL34"
												End If
												If item.COLIDX = 35 Then
													item.COLNM = "COL35"
												End If
												If item.COLIDX = 36 Then
													item.COLNM = "COL36"
												End If
												If item.COLIDX = 37 Then
													item.COLNM = "COL37"
												End If
												If item.COLIDX = 38 Then
													item.COLNM = "COL38"
												End If
												If item.COLIDX = 39 Then
													item.COLNM = "COL39"
												End If
												If item.COLIDX = 40 Then
													item.COLNM = "COL40"
												End If
												If item.COLIDX = 41 Then
													item.COLNM = "COL41"
												End If
												If item.COLIDX = 42 Then
													item.COLNM = "COL42"
												End If
												If item.COLIDX = 43 Then
													item.COLNM = "COL43"
												End If
												If item.COLIDX = 44 Then
													item.COLNM = "COL44"
												End If
												If item.COLIDX = 45 Then
													item.COLNM = "COL45"
												End If
												If item.COLIDX = 46 Then
													item.COLNM = "COL46"
												End If
												If item.COLIDX = 47 Then
													item.COLNM = "COL47"
												End If
												If item.COLIDX = 48 Then
													item.COLNM = "COL48"
												End If
												If item.COLIDX = 49 Then
													item.COLNM = "COL49"
												End If
												If item.COLIDX = 50 Then
													item.COLNM = "COL50"
												End If
											End If
										Next
									End If
									'db.D0010.Add(insertD0010)

									'Insert in D0022 table
									If screenD0010.D0022 IsNot Nothing Then
										For Each item In screenD0010.D0022
											If item.USERID <> 0 Then
												Dim insertD0022 As New D0022
												insertD0022.GYOMNO = insertD0010.GYOMNO
												insertD0022.COLNM = item.COLNM
												insertD0022.USERID = item.USERID
												db.D0022.Add(insertD0022)
											End If
										Next
									End If
								End If
								'count += 1
							Next

							'If KakteiSori Has NOT done than Date is Disabled and do DELETE MAKE PROCEDURE
						ElseIf d0010.GYOMYMD <> d0010.GYOMYMDED OrElse d0010_old.GYOMYMD <> d0010_old.GYOMYMDED Then

							Dim Obj_D0010 = (From d0010_child In db.D0010
											 Where d0010_child.PGYOMNO = screenD0010.GYOMNO AndAlso
														d0010_child.OYAGYOMFLG = True
											 Select d0010_child).ToList

							db.D0010.RemoveRange(Obj_D0010)

							For Each D0010_d0022 As D0010 In Obj_D0010
								Dim d0022_delete = db.D0022.Where(Function(x) x.GYOMNO = D0010_d0022.GYOMNO).ToList
								db.D0022.RemoveRange(d0022_delete)
							Next

							Dim count As Integer = 1
							Dim start_dateChild As Date = screenD0010.GYOMYMD
							Dim DaysCount As Integer = DateRange(start_dateChild.AddDays(1), d0010.GYOMYMDED).Count

							For Each day As DateTime In DateRange(start_dateChild.AddDays(1), d0010.GYOMYMDED)

								Dim insertD0010 As D0010 = New D0010

								insertD0010.GYOMNO = GetMaxGyomno() + count

								'Start Date
								insertD0010.GYOMYMD = day
								insertD0010.GYOMYMDED = day

								If DaysCount = count Then
									insertD0010.KSKJKNST = "0000"
									insertD0010.KSKJKNED = ChangeToHHMM(screenD0010.KSKJKNED)
								Else
									insertD0010.KSKJKNST = "0000"
									insertD0010.KSKJKNED = "2400"
								End If


								insertD0010.JTJKNST = GetJtjkn(insertD0010.GYOMYMD, insertD0010.KSKJKNST)
								insertD0010.JTJKNED = GetJtjkn(insertD0010.GYOMYMDED, insertD0010.KSKJKNED)
								insertD0010.BANGUMINM = screenD0010.BANGUMINM
								insertD0010.OAJKNST = ChangeToHHMM(screenD0010.OAJKNST)
								insertD0010.OAJKNED = ChangeToHHMM(screenD0010.OAJKNED)
								insertD0010.BASYO = screenD0010.BASYO
								insertD0010.BIKO = screenD0010.BIKO

								insertD0010.CATCD = screenD0010.CATCD
								insertD0010.NAIYO = screenD0010.NAIYO
								insertD0010.BANGUMITANTO = screenD0010.BANGUMITANTO
								insertD0010.BANGUMIRENRK = screenD0010.BANGUMIRENRK
								insertD0010.RNZK = True

								insertD0010.PGYOMNO = d0010.GYOMNO

								'UnComment below code of SPORTFLG,OYAGYOMFLG,SPORTCATCD,SPORTSUBCATCD,SAIJKNST,SAIJKNED,COL1,COL2......COL20
								'Once db TBL D0010 altered.

								insertD0010.SPORTFLG = True
								insertD0010.OYAGYOMFLG = True
								insertD0010.SPORTCATCD = screenD0010.SPORTCATCD
								insertD0010.SPORTSUBCATCD = screenD0010.SPORTSUBCATCD
								insertD0010.SAIJKNST = ChangeToHHMM(screenD0010.SAIJKNST)
								insertD0010.SAIJKNED = ChangeToHHMM(screenD0010.SAIJKNED)

								If screenD0010.FreeTxtBxList IsNot Nothing Then
									Dim iterationCnt As Int16 = 0
									For Each item In screenD0010.FreeTxtBxList
										If iterationCnt = 0 Then
											insertD0010.COL01 = item
										End If
										If iterationCnt = 1 Then
											insertD0010.COL02 = item
										End If
										If iterationCnt = 2 Then
											insertD0010.COL03 = item
										End If
										If iterationCnt = 3 Then
											insertD0010.COL04 = item
										End If
										If iterationCnt = 4 Then
											insertD0010.COL05 = item
										End If
										If iterationCnt = 5 Then
											insertD0010.COL06 = item
										End If
										If iterationCnt = 6 Then
											insertD0010.COL07 = item
										End If
										If iterationCnt = 7 Then
											insertD0010.COL08 = item
										End If
										If iterationCnt = 8 Then
											insertD0010.COL09 = item
										End If
										If iterationCnt = 9 Then
											insertD0010.COL10 = item
										End If
										If iterationCnt = 10 Then
											insertD0010.COL11 = item
										End If
										If iterationCnt = 11 Then
											insertD0010.COL12 = item
										End If
										If iterationCnt = 12 Then
											insertD0010.COL13 = item
										End If
										If iterationCnt = 13 Then
											insertD0010.COL14 = item
										End If
										If iterationCnt = 14 Then
											insertD0010.COL15 = item
										End If
										If iterationCnt = 15 Then
											insertD0010.COL16 = item
										End If
										If iterationCnt = 16 Then
											insertD0010.COL17 = item
										End If
										If iterationCnt = 17 Then
											insertD0010.COL18 = item
										End If
										If iterationCnt = 18 Then
											insertD0010.COL19 = item
										End If
										If iterationCnt = 19 Then
											insertD0010.COL20 = item
										End If
										If iterationCnt = 20 Then
											insertD0010.COL21 = item
										End If
										If iterationCnt = 21 Then
											insertD0010.COL22 = item
										End If
										If iterationCnt = 22 Then
											insertD0010.COL23 = item
										End If
										If iterationCnt = 23 Then
											insertD0010.COL24 = item
										End If
										If iterationCnt = 24 Then
											insertD0010.COL25 = item
										End If

										'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
										If iterationCnt = 25 Then
											insertD0010.COL26 = item
										End If
										If iterationCnt = 26 Then
											insertD0010.COL27 = item
										End If
										If iterationCnt = 27 Then
											insertD0010.COL28 = item
										End If
										If iterationCnt = 28 Then
											insertD0010.COL29 = item
										End If
										If iterationCnt = 29 Then
											insertD0010.COL30 = item
										End If
										If iterationCnt = 30 Then
											insertD0010.COL31 = item
										End If
										If iterationCnt = 31 Then
											insertD0010.COL32 = item
										End If
										If iterationCnt = 32 Then
											insertD0010.COL33 = item
										End If
										If iterationCnt = 33 Then
											insertD0010.COL34 = item
										End If
										If iterationCnt = 34 Then
											insertD0010.COL35 = item
										End If
										If iterationCnt = 35 Then
											insertD0010.COL36 = item
										End If
										If iterationCnt = 36 Then
											insertD0010.COL37 = item
										End If
										If iterationCnt = 37 Then
											insertD0010.COL38 = item
										End If
										If iterationCnt = 38 Then
											insertD0010.COL39 = item
										End If
										If iterationCnt = 39 Then
											insertD0010.COL40 = item
										End If
										If iterationCnt = 40 Then
											insertD0010.COL41 = item
										End If
										If iterationCnt = 41 Then
											insertD0010.COL42 = item
										End If
										If iterationCnt = 42 Then
											insertD0010.COL43 = item
										End If
										If iterationCnt = 43 Then
											insertD0010.COL44 = item
										End If
										If iterationCnt = 44 Then
											insertD0010.COL45 = item
										End If
										If iterationCnt = 45 Then
											insertD0010.COL46 = item
										End If
										If iterationCnt = 46 Then
											insertD0010.COL47 = item
										End If
										If iterationCnt = 47 Then
											insertD0010.COL48 = item
										End If
										If iterationCnt = 48 Then
											insertD0010.COL49 = item
										End If
										If iterationCnt = 49 Then
											insertD0010.COL50 = item
										End If

										iterationCnt = iterationCnt + 1
									Next
								End If

								If screenD0010.D0022 IsNot Nothing Then
									For Each item In screenD0010.D0022
										'If dropdown is not blank
										If item.USERID <> 0 Then
											If item.COLIDX = 1 Then
												item.COLNM = "COL01"
											End If
											If item.COLIDX = 2 Then
												item.COLNM = "COL02"
											End If
											If item.COLIDX = 3 Then
												item.COLNM = "COL03"
											End If
											If item.COLIDX = 4 Then
												item.COLNM = "COL04"
											End If
											If item.COLIDX = 5 Then
												item.COLNM = "COL05"
											End If
											If item.COLIDX = 6 Then
												item.COLNM = "COL06"
											End If
											If item.COLIDX = 7 Then
												item.COLNM = "COL07"
											End If
											If item.COLIDX = 8 Then
												item.COLNM = "COL08"
											End If
											If item.COLIDX = 9 Then
												item.COLNM = "COL09"
											End If
											If item.COLIDX = 10 Then
												item.COLNM = "COL10"
											End If
											If item.COLIDX = 11 Then
												item.COLNM = "COL11"
											End If
											If item.COLIDX = 12 Then
												item.COLNM = "COL12"
											End If
											If item.COLIDX = 13 Then
												item.COLNM = "COL13"
											End If
											If item.COLIDX = 14 Then
												item.COLNM = "COL14"
											End If
											If item.COLIDX = 15 Then
												item.COLNM = "COL15"
											End If
											If item.COLIDX = 16 Then
												item.COLNM = "COL16"
											End If
											If item.COLIDX = 17 Then
												item.COLNM = "COL17"
											End If
											If item.COLIDX = 18 Then
												item.COLNM = "COL18"
											End If
											If item.COLIDX = 19 Then
												item.COLNM = "COL19"
											End If
											If item.COLIDX = 20 Then
												item.COLNM = "COL20"
											End If
											If item.COLIDX = 21 Then
												item.COLNM = "COL21"
											End If
											If item.COLIDX = 22 Then
												item.COLNM = "COL22"
											End If
											If item.COLIDX = 23 Then
												item.COLNM = "COL23"
											End If
											If item.COLIDX = 24 Then
												item.COLNM = "COL24"
											End If
											If item.COLIDX = 25 Then
												item.COLNM = "COL25"
											End If

											'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
											If item.COLIDX = 26 Then
												item.COLNM = "COL26"
											End If
											If item.COLIDX = 27 Then
												item.COLNM = "COL27"
											End If
											If item.COLIDX = 28 Then
												item.COLNM = "COL28"
											End If
											If item.COLIDX = 29 Then
												item.COLNM = "COL29"
											End If
											If item.COLIDX = 30 Then
												item.COLNM = "COL30"
											End If
											If item.COLIDX = 31 Then
												item.COLNM = "COL31"
											End If
											If item.COLIDX = 32 Then
												item.COLNM = "COL32"
											End If
											If item.COLIDX = 33 Then
												item.COLNM = "COL33"
											End If
											If item.COLIDX = 34 Then
												item.COLNM = "COL34"
											End If
											If item.COLIDX = 35 Then
												item.COLNM = "COL35"
											End If
											If item.COLIDX = 36 Then
												item.COLNM = "COL36"
											End If
											If item.COLIDX = 37 Then
												item.COLNM = "COL37"
											End If
											If item.COLIDX = 38 Then
												item.COLNM = "COL38"
											End If
											If item.COLIDX = 39 Then
												item.COLNM = "COL39"
											End If
											If item.COLIDX = 40 Then
												item.COLNM = "COL40"
											End If
											If item.COLIDX = 41 Then
												item.COLNM = "COL41"
											End If
											If item.COLIDX = 42 Then
												item.COLNM = "COL42"
											End If
											If item.COLIDX = 43 Then
												item.COLNM = "COL43"
											End If
											If item.COLIDX = 44 Then
												item.COLNM = "COL44"
											End If
											If item.COLIDX = 45 Then
												item.COLNM = "COL45"
											End If
											If item.COLIDX = 46 Then
												item.COLNM = "COL46"
											End If
											If item.COLIDX = 47 Then
												item.COLNM = "COL47"
											End If
											If item.COLIDX = 48 Then
												item.COLNM = "COL48"
											End If
											If item.COLIDX = 49 Then
												item.COLNM = "COL49"
											End If
											If item.COLIDX = 50 Then
												item.COLNM = "COL50"
											End If
										End If
									Next
								End If
								db.D0010.Add(insertD0010)

								'Insert in D0022 table
								If screenD0010.D0022 IsNot Nothing Then
									For Each item In screenD0010.D0022
										If item.USERID <> 0 Then
											Dim insertD0022 As New D0022
											insertD0022.GYOMNO = insertD0010.GYOMNO
											insertD0022.COLNM = item.COLNM
											insertD0022.USERID = item.USERID
											db.D0022.Add(insertD0022)
										End If
									Next
								End If

								count += 1
							Next
						End If


						db.Configuration.ValidateOnSaveEnabled = False
						db.SaveChanges()
						db.Configuration.ValidateOnSaveEnabled = True
						tran.Commit()

					Catch ex As Exception
						Throw ex
						tran.Rollback()
					End Try
				End Using

				Return Redirect(Session("A0220EditRtnUrl" & screenD0010.GYOMNO))
				'Return RedirectToAction("Index", "A0240", routeValues:=New With {.SportCatCd = screenD0010.SPORTCATCD, .Searchdt = screenD0010.GYOMYMD.ToString().Substring(0, 7), .lastForm = lastForm})

				'In Case Of Error Set The Value To The Control
			Else

				'業務テーブルからダ－タ取得
				Dim d0010 As D0010 = db.D0010.Find(screenD0010.GYOMNO)

                Dim SPORTCATCD As String = d0010.SPORTCATCD
                Dim SPORTSUBCATCD As String = d0010.SPORTSUBCATCD

                'For DropDown Menu Of スポーツカテゴリーコード	(SPORTCATCD)
                Dim lstSportCatNm = db.M0130.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
                Dim blank_entry As New M0130
                blank_entry.SPORTCATCD = 0
                blank_entry.SPORTCATNM = ""
                lstSportCatNm.Insert(0, blank_entry)
                ViewBag.SportCatNmList = lstSportCatNm

                'For DropDown Menu Of スポーツサブカテゴリーコード	(SPORTSUBCATCD)
                Dim lstSportSubCatNm = db.M0140.Where(Function(m) m.HYOJ = True).OrderBy(Function(f) f.HYOJJN).ToList
                Dim blank_entry_subCatNm As New M0140
                blank_entry_subCatNm.SPORTSUBCATCD = 0
                blank_entry_subCatNm.SPORTSUBCATNM = ""
                lstSportSubCatNm.Insert(0, blank_entry_subCatNm)
                ViewBag.SportSubCatNmList = lstSportSubCatNm

                Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
                Dim naiyoitem As New M0040
                naiyoitem.NAIYOCD = "0"
                naiyoitem.NAIYO = ""
                lstNaiyo.Insert(0, naiyoitem)
                ViewBag.NaiyouList = lstNaiyo

                'ユーザー
                Dim m0010List = From m In db.M0010 Select m
                ViewBag.lstmsterm0010 = m0010List.ToList
                m0010List = m0010List.Where(Function(d1) db.M0160.Where(Function(m) m.SPORTCATCD = SPORTCATCD AndAlso d1.STATUS = True AndAlso d1.HYOJ = True).Select(Function(t2) t2.USERID).Contains(d1.USERID) Or (d1.KARIANA = True And d1.STATUS = True))

                'Dim lstUSERID = m0010List.ToList
                Dim lstUSERID = m0010List.OrderByDescending(Function(x) x.KARIANA = True).ThenBy(Function(x) x.HYOJJN).ToList
                Dim itemm0010 As New M0010
                itemm0010.USERID = "0"
                itemm0010.USERNM = ""
                lstUSERID.Insert(0, itemm0010)
                ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM")
                ViewBag.lstM0010 = lstUSERID

                Dim listFreeItem As New List(Of String)
				'Dim listAnaItem As New List(Of String)
				Dim listAnaItem = {(New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "", .TBLNM = "", .HYOJJN = Short.Parse("0")})}.ToList
				Dim listitemvalue As New List(Of String)

                Dim m0150 As M0150 = db.M0150.Where(Function(m) m.SPORTCATCD = SPORTCATCD And m.SPORTSUBCATCD = SPORTSUBCATCD).FirstOrDefault

				If m0150 IsNot Nothing Then
					listAnaItem.RemoveAt(0)
					If m0150.COL01_TYPE = "1" Then
						listFreeItem.Add(m0150.COL01)
						'listAnaItem.Add(Nothing)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "1", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL01_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL01, .ANNCATNAME = "", .COLINDEX = "1", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL01_HYOJJN2 IsNot Nothing, m0150.COL01_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "1", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL02_TYPE = "1" Then
						listFreeItem.Add(m0150.COL02)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "2", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL02_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL02, .ANNCATNAME = "", .COLINDEX = "2", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL02_HYOJJN2 IsNot Nothing, m0150.COL02_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL02, m0150.COL02_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "2", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL03_TYPE = "1" Then
						listFreeItem.Add(m0150.COL03)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "3", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL03_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL03, .ANNCATNAME = "", .COLINDEX = "3", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL03_HYOJJN2 IsNot Nothing, m0150.COL03_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL03, m0150.COL03_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "3", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL04_TYPE = "1" Then
						listFreeItem.Add(m0150.COL04)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "4", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL04_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL04, .ANNCATNAME = "", .COLINDEX = "4", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL04_HYOJJN2 IsNot Nothing, m0150.COL04_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL04, m0150.COL04_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "4", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL05_TYPE = "1" Then
						listFreeItem.Add(m0150.COL05)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "5", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL05_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL05, .ANNCATNAME = "", .COLINDEX = "5", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL05_HYOJJN2 IsNot Nothing, m0150.COL05_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL05, m0150.COL05_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "5", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL06_TYPE = "1" Then
						listFreeItem.Add(m0150.COL06)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "6", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL06_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL06, .ANNCATNAME = "", .COLINDEX = "6", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL06_HYOJJN2 IsNot Nothing, m0150.COL06_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL06, m0150.COL06_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "6", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL07_TYPE = "1" Then
						listFreeItem.Add(m0150.COL07)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "7", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL07_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL07, .ANNCATNAME = "", .COLINDEX = "7", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL07_HYOJJN2 IsNot Nothing, m0150.COL07_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL07, m0150.COL07_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "7", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL08_TYPE = "1" Then
						listFreeItem.Add(m0150.COL08)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "8", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL08_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL08, .ANNCATNAME = "", .COLINDEX = "8", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL08_HYOJJN2 IsNot Nothing, m0150.COL08_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL08, m0150.COL08_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "8", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL09_TYPE = "1" Then
						listFreeItem.Add(m0150.COL09)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "9", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL09_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL09, .ANNCATNAME = "", .COLINDEX = "9", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL09_HYOJJN2 IsNot Nothing, m0150.COL09_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL09, m0150.COL09_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "9", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL10_TYPE = "1" Then
						listFreeItem.Add(m0150.COL10)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "10", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL10_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL10, .ANNCATNAME = "", .COLINDEX = "10", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL10_HYOJJN2 IsNot Nothing, m0150.COL10_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL10, m0150.COL10_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "10", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL11_TYPE = "1" Then
						listFreeItem.Add(m0150.COL11)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "11", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL11_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL11, .ANNCATNAME = "", .COLINDEX = "11", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL11_HYOJJN2 IsNot Nothing, m0150.COL11_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL11, m0150.COL11_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "11", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL12_TYPE = "1" Then
						listFreeItem.Add(m0150.COL12)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "12", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL12_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL12, .ANNCATNAME = "", .COLINDEX = "12", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL12_HYOJJN2 IsNot Nothing, m0150.COL12_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL12, m0150.COL12_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "12", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL13_TYPE = "1" Then
						listFreeItem.Add(m0150.COL13)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "13", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL13_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL13, .ANNCATNAME = "", .COLINDEX = "13", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL13_HYOJJN2 IsNot Nothing, m0150.COL13_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL13, m0150.COL13_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "13", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL14_TYPE = "1" Then
						listFreeItem.Add(m0150.COL14)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "14", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL14_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL14, .ANNCATNAME = "", .COLINDEX = "14", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL14_HYOJJN2 IsNot Nothing, m0150.COL14_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL14, m0150.COL14_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "14", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL15_TYPE = "1" Then
						listFreeItem.Add(m0150.COL15)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "15", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL15_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL15, .ANNCATNAME = "", .COLINDEX = "15", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL15_HYOJJN2 IsNot Nothing, m0150.COL15_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL15, m0150.COL15_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "15", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL16_TYPE = "1" Then
						listFreeItem.Add(m0150.COL16)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "16", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL16_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL16, .ANNCATNAME = "", .COLINDEX = "16", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL16_HYOJJN2 IsNot Nothing, m0150.COL16_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL16, m0150.COL16_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "16", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL17_TYPE = "1" Then
						listFreeItem.Add(m0150.COL17)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "17", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL17_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL17, .ANNCATNAME = "", .COLINDEX = "17", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL17_HYOJJN2 IsNot Nothing, m0150.COL17_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL17, m0150.COL17_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "17", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL18_TYPE = "1" Then
						listFreeItem.Add(m0150.COL18)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "18", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL18_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL18, .ANNCATNAME = "", .COLINDEX = "18", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL18_HYOJJN2 IsNot Nothing, m0150.COL18_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL18, m0150.COL18_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "18", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL19_TYPE = "1" Then
						listFreeItem.Add(m0150.COL19)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "19", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL19_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL19, .ANNCATNAME = "", .COLINDEX = "19", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL19_HYOJJN2 IsNot Nothing, m0150.COL19_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL19, m0150.COL19_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "19", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL20_TYPE = "1" Then
						listFreeItem.Add(m0150.COL20)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "20", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL20_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL20, .ANNCATNAME = "", .COLINDEX = "20", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL20_HYOJJN2 IsNot Nothing, m0150.COL20_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL20, m0150.COL20_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "20", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL21_TYPE = "1" Then
						listFreeItem.Add(m0150.COL21)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "21", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL21_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL21, .ANNCATNAME = "", .COLINDEX = "21", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL21_HYOJJN2 IsNot Nothing, m0150.COL21_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL21, m0150.COL21_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "21", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL22_TYPE = "1" Then
						listFreeItem.Add(m0150.COL22)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "22", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL22_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL22, .ANNCATNAME = "", .COLINDEX = "22", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL22_HYOJJN2 IsNot Nothing, m0150.COL22_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL22, m0150.COL22_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "22", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL23_TYPE = "1" Then
						listFreeItem.Add(m0150.COL23)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "23", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL23_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL23, .ANNCATNAME = "", .COLINDEX = "23", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL23_HYOJJN2 IsNot Nothing, m0150.COL23_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL23, m0150.COL23_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "23", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL24_TYPE = "1" Then
						listFreeItem.Add(m0150.COL24)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "24", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL24_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL24, .ANNCATNAME = "", .COLINDEX = "24", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL24_HYOJJN2 IsNot Nothing, m0150.COL24_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL24, m0150.COL24_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "24", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL25_TYPE = "1" Then
						listFreeItem.Add(m0150.COL25)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "25", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL25_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL25, .ANNCATNAME = "", .COLINDEX = "25", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL25_HYOJJN2 IsNot Nothing, m0150.COL25_HYOJJN2, 999))}))
						'listAnaItem.Add({m0150.COL25, m0150.COL25_HYOJJN2})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "25", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
					If m0150.COL26_TYPE = "1" Then
						listFreeItem.Add(m0150.COL26)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "26", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL26_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL26, .ANNCATNAME = "", .COLINDEX = "26", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL26_HYOJJN2 IsNot Nothing, m0150.COL26_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "26", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL27_TYPE = "1" Then
						listFreeItem.Add(m0150.COL27)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "27", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL27_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL27, .ANNCATNAME = "", .COLINDEX = "27", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL27_HYOJJN2 IsNot Nothing, m0150.COL27_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "27", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL28_TYPE = "1" Then
						listFreeItem.Add(m0150.COL28)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "28", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL28_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL28, .ANNCATNAME = "", .COLINDEX = "28", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL28_HYOJJN2 IsNot Nothing, m0150.COL28_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "28", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL29_TYPE = "1" Then
						listFreeItem.Add(m0150.COL29)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "29", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL29_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL29, .ANNCATNAME = "", .COLINDEX = "29", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL29_HYOJJN2 IsNot Nothing, m0150.COL29_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "29", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL30_TYPE = "1" Then
						listFreeItem.Add(m0150.COL30)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "30", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL30_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL30, .ANNCATNAME = "", .COLINDEX = "30", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL30_HYOJJN2 IsNot Nothing, m0150.COL30_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "30", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL31_TYPE = "1" Then
						listFreeItem.Add(m0150.COL31)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "31", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL31_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL31, .ANNCATNAME = "", .COLINDEX = "31", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL31_HYOJJN2 IsNot Nothing, m0150.COL31_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "31", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL32_TYPE = "1" Then
						listFreeItem.Add(m0150.COL32)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "32", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL32_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL32, .ANNCATNAME = "", .COLINDEX = "32", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL32_HYOJJN2 IsNot Nothing, m0150.COL32_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "32", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL33_TYPE = "1" Then
						listFreeItem.Add(m0150.COL33)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "33", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL33_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL33, .ANNCATNAME = "", .COLINDEX = "33", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL33_HYOJJN2 IsNot Nothing, m0150.COL33_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "33", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL34_TYPE = "1" Then
						listFreeItem.Add(m0150.COL34)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "34", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL34_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL34, .ANNCATNAME = "", .COLINDEX = "34", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL34_HYOJJN2 IsNot Nothing, m0150.COL34_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "34", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL35_TYPE = "1" Then
						listFreeItem.Add(m0150.COL35)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "35", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL35_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL35, .ANNCATNAME = "", .COLINDEX = "35", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL35_HYOJJN2 IsNot Nothing, m0150.COL35_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "35", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL36_TYPE = "1" Then
						listFreeItem.Add(m0150.COL36)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "36", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL36_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL36, .ANNCATNAME = "", .COLINDEX = "36", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL36_HYOJJN2 IsNot Nothing, m0150.COL36_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "36", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL37_TYPE = "1" Then
						listFreeItem.Add(m0150.COL37)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "37", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL37_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL37, .ANNCATNAME = "", .COLINDEX = "37", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL37_HYOJJN2 IsNot Nothing, m0150.COL37_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "37", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL38_TYPE = "1" Then
						listFreeItem.Add(m0150.COL38)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "38", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL38_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL38, .ANNCATNAME = "", .COLINDEX = "38", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL38_HYOJJN2 IsNot Nothing, m0150.COL38_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "38", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL39_TYPE = "1" Then
						listFreeItem.Add(m0150.COL39)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "39", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL39_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL39, .ANNCATNAME = "", .COLINDEX = "39", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL39_HYOJJN2 IsNot Nothing, m0150.COL39_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "39", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL40_TYPE = "1" Then
						listFreeItem.Add(m0150.COL40)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "40", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL40_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL40, .ANNCATNAME = "", .COLINDEX = "40", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL40_HYOJJN2 IsNot Nothing, m0150.COL40_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "40", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL41_TYPE = "1" Then
						listFreeItem.Add(m0150.COL41)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "41", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL41_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL41, .ANNCATNAME = "", .COLINDEX = "41", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL41_HYOJJN2 IsNot Nothing, m0150.COL41_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "41", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL42_TYPE = "1" Then
						listFreeItem.Add(m0150.COL42)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "42", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL42_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL42, .ANNCATNAME = "", .COLINDEX = "42", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL42_HYOJJN2 IsNot Nothing, m0150.COL42_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "42", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL43_TYPE = "1" Then
						listFreeItem.Add(m0150.COL43)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "43", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL43_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL43, .ANNCATNAME = "", .COLINDEX = "43", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL43_HYOJJN2 IsNot Nothing, m0150.COL43_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "43", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL44_TYPE = "1" Then
						listFreeItem.Add(m0150.COL44)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "44", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL44_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL44, .ANNCATNAME = "", .COLINDEX = "44", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL44_HYOJJN2 IsNot Nothing, m0150.COL44_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "44", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL45_TYPE = "1" Then
						listFreeItem.Add(m0150.COL45)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "45", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL45_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL45, .ANNCATNAME = "", .COLINDEX = "45", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL45_HYOJJN2 IsNot Nothing, m0150.COL45_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "45", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL46_TYPE = "1" Then
						listFreeItem.Add(m0150.COL46)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "46", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL46_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL46, .ANNCATNAME = "", .COLINDEX = "46", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL46_HYOJJN2 IsNot Nothing, m0150.COL46_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "46", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL47_TYPE = "1" Then
						listFreeItem.Add(m0150.COL47)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "47", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL47_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL47, .ANNCATNAME = "", .COLINDEX = "47", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL47_HYOJJN2 IsNot Nothing, m0150.COL47_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "47", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL48_TYPE = "1" Then
						listFreeItem.Add(m0150.COL48)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "48", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL48_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL48, .ANNCATNAME = "", .COLINDEX = "48", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL48_HYOJJN2 IsNot Nothing, m0150.COL48_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "48", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL49_TYPE = "1" Then
						listFreeItem.Add(m0150.COL49)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "49", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL49_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL49, .ANNCATNAME = "", .COLINDEX = "49", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL49_HYOJJN2 IsNot Nothing, m0150.COL49_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "49", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					If m0150.COL50_TYPE = "1" Then
						listFreeItem.Add(m0150.COL50)
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "50", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
					ElseIf m0150.COL50_TYPE = "2" Then
						listAnaItem.Add((New With {.COLNAME = m0150.COL50, .ANNCATNAME = "", .COLINDEX = "50", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(If(m0150.COL50_HYOJJN2 IsNot Nothing, m0150.COL50_HYOJJN2, 999))}))
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add((New With {.COLNAME = "", .ANNCATNAME = "", .COLINDEX = "50", .TBLNM = "ANAITEM", .HYOJJN = Short.Parse(999)}))
						listFreeItem.Add(Nothing)
					End If

					listitemvalue.Add(screenD0010.COL01)
					listitemvalue.Add(screenD0010.COL02)
					listitemvalue.Add(screenD0010.COL03)
					listitemvalue.Add(screenD0010.COL04)
					listitemvalue.Add(screenD0010.COL05)
					listitemvalue.Add(screenD0010.COL06)
					listitemvalue.Add(screenD0010.COL07)
					listitemvalue.Add(screenD0010.COL08)
					listitemvalue.Add(screenD0010.COL09)
					listitemvalue.Add(screenD0010.COL10)
					listitemvalue.Add(screenD0010.COL11)
					listitemvalue.Add(screenD0010.COL12)
					listitemvalue.Add(screenD0010.COL13)
					listitemvalue.Add(screenD0010.COL14)
					listitemvalue.Add(screenD0010.COL15)
					listitemvalue.Add(screenD0010.COL16)
					listitemvalue.Add(screenD0010.COL17)
					listitemvalue.Add(screenD0010.COL18)
					listitemvalue.Add(screenD0010.COL19)
					listitemvalue.Add(screenD0010.COL20)
					listitemvalue.Add(screenD0010.COL21)
					listitemvalue.Add(screenD0010.COL22)
					listitemvalue.Add(screenD0010.COL23)
					listitemvalue.Add(screenD0010.COL24)
					listitemvalue.Add(screenD0010.COL25)

					'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
					listitemvalue.Add(screenD0010.COL26)
					listitemvalue.Add(screenD0010.COL27)
					listitemvalue.Add(screenD0010.COL28)
					listitemvalue.Add(screenD0010.COL29)
					listitemvalue.Add(screenD0010.COL30)
					listitemvalue.Add(screenD0010.COL31)
					listitemvalue.Add(screenD0010.COL32)
					listitemvalue.Add(screenD0010.COL33)
					listitemvalue.Add(screenD0010.COL34)
					listitemvalue.Add(screenD0010.COL35)
					listitemvalue.Add(screenD0010.COL36)
					listitemvalue.Add(screenD0010.COL37)
					listitemvalue.Add(screenD0010.COL38)
					listitemvalue.Add(screenD0010.COL39)
					listitemvalue.Add(screenD0010.COL40)
					listitemvalue.Add(screenD0010.COL41)
					listitemvalue.Add(screenD0010.COL42)
					listitemvalue.Add(screenD0010.COL43)
					listitemvalue.Add(screenD0010.COL44)
					listitemvalue.Add(screenD0010.COL45)
					listitemvalue.Add(screenD0010.COL46)
					listitemvalue.Add(screenD0010.COL47)
					listitemvalue.Add(screenD0010.COL48)
					listitemvalue.Add(screenD0010.COL49)
					listitemvalue.Add(screenD0010.COL50)

				End If
				ViewBag.FreeItemList = listFreeItem
				ViewBag.AnaItemList = listAnaItem.OrderBy(Function(m) m.HYOJJN)
				ViewBag.listitemvalue = listitemvalue
                screenD0010.M0150 = m0150

                Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList

                Dim listAnad0022 As New List(Of String())
                Dim listAnad0020 As New List(Of String())
				Dim listAnad0021 As New List(Of String())

				Dim d0022 = db.D0022.Where(Function(m) m.GYOMNO = screenD0010.GYOMNO).ToList

                For Each d0020_item As D0022 In d0022

                    Select Case d0020_item.COLNM

                        Case "COL01"
                            listAnad0022.Add({m0150.COL01, d0020_item.USERID, "1"})
                        Case "COL02"
                            listAnad0022.Add({m0150.COL02, d0020_item.USERID, "2"})
                        Case "COL03"
                            listAnad0022.Add({m0150.COL03, d0020_item.USERID, "3"})
                        Case "COL04"
                            listAnad0022.Add({m0150.COL04, d0020_item.USERID, "4"})
                        Case "COL05"
                            listAnad0022.Add({m0150.COL05, d0020_item.USERID, "5"})
                        Case "COL06"
                            listAnad0022.Add({m0150.COL06, d0020_item.USERID, "6"})
                        Case "COL07"
                            listAnad0022.Add({m0150.COL07, d0020_item.USERID, "7"})
                        Case "COL08"
                            listAnad0022.Add({m0150.COL08, d0020_item.USERID, "8"})
                        Case "COL09"
                            listAnad0022.Add({m0150.COL09, d0020_item.USERID, "9"})
                        Case "COL10"
                            listAnad0022.Add({m0150.COL10, d0020_item.USERID, "10"})
                        Case "COL11"
                            listAnad0022.Add({m0150.COL11, d0020_item.USERID, "11"})
                        Case "COL12"
                            listAnad0022.Add({m0150.COL12, d0020_item.USERID, "12"})
                        Case "COL13"
                            listAnad0022.Add({m0150.COL13, d0020_item.USERID, "13"})
                        Case "COL14"
                            listAnad0022.Add({m0150.COL14, d0020_item.USERID, "14"})
                        Case "COL15"
                            listAnad0022.Add({m0150.COL15, d0020_item.USERID, "15"})
                        Case "COL16"
                            listAnad0022.Add({m0150.COL16, d0020_item.USERID, "16"})
                        Case "COL17"
                            listAnad0022.Add({m0150.COL17, d0020_item.USERID, "17"})
                        Case "COL18"
                            listAnad0022.Add({m0150.COL18, d0020_item.USERID, "18"})
                        Case "COL19"
                            listAnad0022.Add({m0150.COL19, d0020_item.USERID, "19"})
                        Case "COL20"
                            listAnad0022.Add({m0150.COL20, d0020_item.USERID, "20"})
                        Case "COL21"
                            listAnad0022.Add({m0150.COL21, d0020_item.USERID, "21"})
                        Case "COL22"
                            listAnad0022.Add({m0150.COL22, d0020_item.USERID, "22"})
                        Case "COL23"
                            listAnad0022.Add({m0150.COL23, d0020_item.USERID, "23"})
                        Case "COL24"
                            listAnad0022.Add({m0150.COL24, d0020_item.USERID, "24"})
                        Case "COL25"
							listAnad0022.Add({m0150.COL25, d0020_item.USERID, "25"})

						'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
						Case "COL26"
							listAnad0022.Add({m0150.COL26, d0020_item.USERID, "26"})
						Case "COL27"
							listAnad0022.Add({m0150.COL27, d0020_item.USERID, "27"})
						Case "COL28"
							listAnad0022.Add({m0150.COL28, d0020_item.USERID, "28"})
						Case "COL29"
							listAnad0022.Add({m0150.COL29, d0020_item.USERID, "29"})
						Case "COL30"
							listAnad0022.Add({m0150.COL30, d0020_item.USERID, "30"})
						Case "COL31"
							listAnad0022.Add({m0150.COL31, d0020_item.USERID, "31"})
						Case "COL32"
							listAnad0022.Add({m0150.COL32, d0020_item.USERID, "32"})
						Case "COL33"
							listAnad0022.Add({m0150.COL33, d0020_item.USERID, "33"})
						Case "COL34"
							listAnad0022.Add({m0150.COL34, d0020_item.USERID, "34"})
						Case "COL35"
							listAnad0022.Add({m0150.COL35, d0020_item.USERID, "35"})
						Case "COL36"
							listAnad0022.Add({m0150.COL36, d0020_item.USERID, "36"})
						Case "COL37"
							listAnad0022.Add({m0150.COL37, d0020_item.USERID, "37"})
						Case "COL38"
							listAnad0022.Add({m0150.COL38, d0020_item.USERID, "38"})
						Case "COL39"
							listAnad0022.Add({m0150.COL39, d0020_item.USERID, "39"})
						Case "COL40"
							listAnad0022.Add({m0150.COL40, d0020_item.USERID, "40"})
						Case "COL41"
							listAnad0022.Add({m0150.COL41, d0020_item.USERID, "41"})
						Case "COL42"
							listAnad0022.Add({m0150.COL42, d0020_item.USERID, "42"})
						Case "COL43"
							listAnad0022.Add({m0150.COL43, d0020_item.USERID, "43"})
						Case "COL44"
							listAnad0022.Add({m0150.COL44, d0020_item.USERID, "44"})
						Case "COL45"
							listAnad0022.Add({m0150.COL45, d0020_item.USERID, "45"})
						Case "COL46"
							listAnad0022.Add({m0150.COL46, d0020_item.USERID, "46"})
						Case "COL47"
							listAnad0022.Add({m0150.COL47, d0020_item.USERID, "47"})
						Case "COL48"
							listAnad0022.Add({m0150.COL48, d0020_item.USERID, "48"})
						Case "COL49"
							listAnad0022.Add({m0150.COL49, d0020_item.USERID, "49"})
						Case "COL50"
							listAnad0022.Add({m0150.COL50, d0020_item.USERID, "50"})
					End Select

                Next

                Dim d0020 = db.D0020.Where(Function(m) (From d0010_t In db.D0010
                                                        Where d0010_t.PGYOMNO = screenD0010.GYOMNO AndAlso
                                                            d0010_t.OYAGYOMFLG = False
                                                        Select d0010_t.GYOMNO).ToList.Contains(m.GYOMNO)).ToList

                For Each d0020_item As D0020 In d0020

                    Select Case d0020_item.COLNM

                        Case "COL01"
                            listAnad0020.Add({m0150.COL01, d0020_item.USERID, "1"})
                        Case "COL02"
                            listAnad0020.Add({m0150.COL02, d0020_item.USERID, "2"})
                        Case "COL03"
                            listAnad0020.Add({m0150.COL03, d0020_item.USERID, "3"})
                        Case "COL04"
                            listAnad0020.Add({m0150.COL04, d0020_item.USERID, "4"})
                        Case "COL05"
                            listAnad0020.Add({m0150.COL05, d0020_item.USERID, "5"})
                        Case "COL06"
                            listAnad0020.Add({m0150.COL06, d0020_item.USERID, "6"})
                        Case "COL07"
                            listAnad0020.Add({m0150.COL07, d0020_item.USERID, "7"})
                        Case "COL08"
                            listAnad0020.Add({m0150.COL08, d0020_item.USERID, "8"})
                        Case "COL09"
                            listAnad0020.Add({m0150.COL09, d0020_item.USERID, "9"})
                        Case "COL10"
                            listAnad0020.Add({m0150.COL10, d0020_item.USERID, "10"})
                        Case "COL11"
                            listAnad0020.Add({m0150.COL11, d0020_item.USERID, "11"})
                        Case "COL12"
                            listAnad0020.Add({m0150.COL12, d0020_item.USERID, "12"})
                        Case "COL13"
                            listAnad0020.Add({m0150.COL13, d0020_item.USERID, "13"})
                        Case "COL14"
                            listAnad0020.Add({m0150.COL14, d0020_item.USERID, "14"})
                        Case "COL15"
                            listAnad0020.Add({m0150.COL15, d0020_item.USERID, "15"})
                        Case "COL16"
                            listAnad0020.Add({m0150.COL16, d0020_item.USERID, "16"})
                        Case "COL17"
                            listAnad0020.Add({m0150.COL17, d0020_item.USERID, "17"})
                        Case "COL18"
                            listAnad0020.Add({m0150.COL18, d0020_item.USERID, "18"})
                        Case "COL19"
                            listAnad0020.Add({m0150.COL19, d0020_item.USERID, "19"})
                        Case "COL20"
                            listAnad0020.Add({m0150.COL20, d0020_item.USERID, "20"})
                        Case "COL21"
                            listAnad0020.Add({m0150.COL21, d0020_item.USERID, "21"})
                        Case "COL22"
                            listAnad0020.Add({m0150.COL22, d0020_item.USERID, "22"})
                        Case "COL23"
                            listAnad0020.Add({m0150.COL23, d0020_item.USERID, "23"})
                        Case "COL24"
                            listAnad0020.Add({m0150.COL24, d0020_item.USERID, "24"})
                        Case "COL25"
							listAnad0020.Add({m0150.COL25, d0020_item.USERID, "25"})

						'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
						Case "COL26"
							listAnad0020.Add({m0150.COL26, d0020_item.USERID, "26"})
						Case "COL27"
							listAnad0020.Add({m0150.COL27, d0020_item.USERID, "27"})
						Case "COL28"
							listAnad0020.Add({m0150.COL28, d0020_item.USERID, "28"})
						Case "COL29"
							listAnad0020.Add({m0150.COL29, d0020_item.USERID, "29"})
						Case "COL30"
							listAnad0020.Add({m0150.COL30, d0020_item.USERID, "30"})
						Case "COL31"
							listAnad0020.Add({m0150.COL31, d0020_item.USERID, "31"})
						Case "COL32"
							listAnad0020.Add({m0150.COL32, d0020_item.USERID, "32"})
						Case "COL33"
							listAnad0020.Add({m0150.COL33, d0020_item.USERID, "33"})
						Case "COL34"
							listAnad0020.Add({m0150.COL34, d0020_item.USERID, "34"})
						Case "COL35"
							listAnad0020.Add({m0150.COL35, d0020_item.USERID, "35"})
						Case "COL36"
							listAnad0020.Add({m0150.COL36, d0020_item.USERID, "36"})
						Case "COL37"
							listAnad0020.Add({m0150.COL37, d0020_item.USERID, "37"})
						Case "COL38"
							listAnad0020.Add({m0150.COL38, d0020_item.USERID, "38"})
						Case "COL39"
							listAnad0020.Add({m0150.COL39, d0020_item.USERID, "39"})
						Case "COL40"
							listAnad0020.Add({m0150.COL40, d0020_item.USERID, "40"})
						Case "COL41"
							listAnad0020.Add({m0150.COL41, d0020_item.USERID, "41"})
						Case "COL42"
							listAnad0020.Add({m0150.COL42, d0020_item.USERID, "42"})
						Case "COL43"
							listAnad0020.Add({m0150.COL43, d0020_item.USERID, "43"})
						Case "COL44"
							listAnad0020.Add({m0150.COL44, d0020_item.USERID, "44"})
						Case "COL45"
							listAnad0020.Add({m0150.COL45, d0020_item.USERID, "45"})
						Case "COL46"
							listAnad0020.Add({m0150.COL46, d0020_item.USERID, "46"})
						Case "COL47"
							listAnad0020.Add({m0150.COL47, d0020_item.USERID, "47"})
						Case "COL48"
							listAnad0020.Add({m0150.COL48, d0020_item.USERID, "48"})
						Case "COL49"
							listAnad0020.Add({m0150.COL49, d0020_item.USERID, "49"})
						Case "COL50"
							listAnad0020.Add({m0150.COL50, d0020_item.USERID, "50"})
					End Select

                Next

				Dim d0021 = db.D0021.Where(Function(m) (From d0010_t In db.D0010
														Where d0010_t.PGYOMNO = screenD0010.GYOMNO AndAlso
														d0010_t.OYAGYOMFLG = False
														Select d0010_t.GYOMNO).ToList.Contains(m.GYOMNO)).OrderBy(Function(x) x.COLNM).ToList

				For Each d0021_item As D0021 In d0021
					'd0021_item.ANNACATNM = "仮アナ"
					Select Case d0021_item.COLNM

						Case "COL01"
							listAnad0021.Add({m0150.COL01, d0021_item.ANNACATNM, "1"})
						Case "COL02"
							listAnad0021.Add({m0150.COL02, d0021_item.ANNACATNM, "2"})
						Case "COL03"
							listAnad0021.Add({m0150.COL03, d0021_item.ANNACATNM, "3"})
						Case "COL04"
							listAnad0021.Add({m0150.COL04, d0021_item.ANNACATNM, "4"})
						Case "COL05"
							listAnad0021.Add({m0150.COL05, d0021_item.ANNACATNM, "5"})
						Case "COL06"
							listAnad0021.Add({m0150.COL06, d0021_item.ANNACATNM, "6"})
						Case "COL07"
							listAnad0021.Add({m0150.COL07, d0021_item.ANNACATNM, "7"})
						Case "COL08"
							listAnad0021.Add({m0150.COL08, d0021_item.ANNACATNM, "8"})
						Case "COL09"
							listAnad0021.Add({m0150.COL09, d0021_item.ANNACATNM, "9"})
						Case "COL10"
							listAnad0021.Add({m0150.COL10, d0021_item.ANNACATNM, "10"})
						Case "COL11"
							listAnad0021.Add({m0150.COL11, d0021_item.ANNACATNM, "11"})
						Case "COL12"
							listAnad0021.Add({m0150.COL12, d0021_item.ANNACATNM, "12"})
						Case "COL13"
							listAnad0021.Add({m0150.COL13, d0021_item.ANNACATNM, "13"})
						Case "COL14"
							listAnad0021.Add({m0150.COL14, d0021_item.ANNACATNM, "14"})
						Case "COL15"
							listAnad0021.Add({m0150.COL15, d0021_item.ANNACATNM, "15"})
						Case "COL16"
							listAnad0021.Add({m0150.COL16, d0021_item.ANNACATNM, "16"})
						Case "COL17"
							listAnad0021.Add({m0150.COL17, d0021_item.ANNACATNM, "17"})
						Case "COL18"
							listAnad0021.Add({m0150.COL18, d0021_item.ANNACATNM, "18"})
						Case "COL19"
							listAnad0021.Add({m0150.COL19, d0021_item.ANNACATNM, "19"})
						Case "COL20"
							listAnad0021.Add({m0150.COL20, d0021_item.ANNACATNM, "20"})
						Case "COL21"
							listAnad0021.Add({m0150.COL21, d0021_item.ANNACATNM, "21"})
						Case "COL22"
							listAnad0021.Add({m0150.COL22, d0021_item.ANNACATNM, "22"})
						Case "COL23"
							listAnad0021.Add({m0150.COL23, d0021_item.ANNACATNM, "23"})
						Case "COL24"
							listAnad0021.Add({m0150.COL24, d0021_item.ANNACATNM, "24"})
						Case "COL25"
							listAnad0021.Add({m0150.COL25, d0021_item.ANNACATNM, "25"})

						'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
						Case "COL26"
							listAnad0021.Add({m0150.COL26, d0021_item.ANNACATNM, "26"})
						Case "COL27"
							listAnad0021.Add({m0150.COL27, d0021_item.ANNACATNM, "27"})
						Case "COL28"
							listAnad0021.Add({m0150.COL28, d0021_item.ANNACATNM, "28"})
						Case "COL29"
							listAnad0021.Add({m0150.COL29, d0021_item.ANNACATNM, "29"})
						Case "COL30"
							listAnad0021.Add({m0150.COL30, d0021_item.ANNACATNM, "30"})
						Case "COL31"
							listAnad0021.Add({m0150.COL31, d0021_item.ANNACATNM, "31"})
						Case "COL32"
							listAnad0021.Add({m0150.COL32, d0021_item.ANNACATNM, "32"})
						Case "COL33"
							listAnad0021.Add({m0150.COL33, d0021_item.ANNACATNM, "33"})
						Case "COL34"
							listAnad0021.Add({m0150.COL34, d0021_item.ANNACATNM, "34"})
						Case "COL35"
							listAnad0021.Add({m0150.COL35, d0021_item.ANNACATNM, "35"})
						Case "COL36"
							listAnad0021.Add({m0150.COL36, d0021_item.ANNACATNM, "36"})
						Case "COL37"
							listAnad0021.Add({m0150.COL37, d0021_item.ANNACATNM, "37"})
						Case "COL38"
							listAnad0021.Add({m0150.COL38, d0021_item.ANNACATNM, "38"})
						Case "COL39"
							listAnad0021.Add({m0150.COL39, d0021_item.ANNACATNM, "39"})
						Case "COL40"
							listAnad0021.Add({m0150.COL40, d0021_item.ANNACATNM, "40"})
						Case "COL41"
							listAnad0021.Add({m0150.COL41, d0021_item.ANNACATNM, "41"})
						Case "COL42"
							listAnad0021.Add({m0150.COL42, d0021_item.ANNACATNM, "42"})
						Case "COL43"
							listAnad0021.Add({m0150.COL43, d0021_item.ANNACATNM, "43"})
						Case "COL44"
							listAnad0021.Add({m0150.COL44, d0021_item.ANNACATNM, "44"})
						Case "COL45"
							listAnad0021.Add({m0150.COL45, d0021_item.ANNACATNM, "45"})
						Case "COL46"
							listAnad0021.Add({m0150.COL46, d0021_item.ANNACATNM, "46"})
						Case "COL47"
							listAnad0021.Add({m0150.COL47, d0021_item.ANNACATNM, "47"})
						Case "COL48"
							listAnad0021.Add({m0150.COL48, d0021_item.ANNACATNM, "48"})
						Case "COL49"
							listAnad0021.Add({m0150.COL49, d0021_item.ANNACATNM, "49"})
						Case "COL50"
							listAnad0021.Add({m0150.COL50, d0021_item.ANNACATNM, "50"})
					End Select

				Next

				Dim ColNm As String = ""
				For int As Integer = listAnaItem.Count - 1 To 0 Step -1
					ColNm = listAnaItem.Item(int).COLNAME
					For Each listAnad0022_item As String() In listAnad0022
						If listAnaItem.Item(int) IsNot Nothing Then
							If listAnad0022_item(0) = listAnaItem.Item(int).COLNAME Then
								If listAnaItem.Where(Function(m) m.COLNAME = ColNm And m.ANNCATNAME <> "").Count > 0 Then
									listAnaItem.Add((New With {.COLNAME = listAnad0022_item(0), .ANNCATNAME = listAnad0022_item(1), .COLINDEX = listAnad0022_item(2), .TBLNM = "D0022", .HYOJJN = Short.Parse(listAnaItem.Item(int).HYOJJN)}))
								Else
									listAnaItem.Item(int).COLNAME = listAnad0022_item(0)
									listAnaItem.Item(int).ANNCATNAME = listAnad0022_item(1)
									listAnaItem.Item(int).COLINDEX = listAnad0022_item(2)
									listAnaItem.Item(int).TBLNM = "D0022"
								End If
							End If
						End If
					Next
					For Each listAnad0020_item As String() In listAnad0020
						If listAnaItem.Item(int) IsNot Nothing Then
							If listAnad0020_item(0) = listAnaItem.Item(int).COLNAME Then
								If listAnaItem.Where(Function(m) m.COLNAME = ColNm And m.ANNCATNAME <> "").Count > 0 Then
									listAnaItem.Add((New With {.COLNAME = listAnad0020_item(0), .ANNCATNAME = listAnad0020_item(1), .COLINDEX = listAnad0020_item(2), .TBLNM = "D0020", .HYOJJN = Short.Parse(listAnaItem.Item(int).HYOJJN)}))
								Else
									listAnaItem.Item(int).COLNAME = listAnad0020_item(0)
									listAnaItem.Item(int).ANNCATNAME = listAnad0020_item(1)
									listAnaItem.Item(int).COLINDEX = listAnad0020_item(2)
									listAnaItem.Item(int).TBLNM = "D0020"
								End If
							End If
						End If
					Next
					For Each listAnad0021_item As String() In listAnad0021
						If listAnaItem.Item(int) IsNot Nothing Then
							If listAnad0021_item(0) = listAnaItem.Item(int).COLNAME Then
								If listAnaItem.Where(Function(m) m.COLNAME = ColNm And m.ANNCATNAME <> "").Count > 0 Then
									listAnaItem.Add((New With {.COLNAME = listAnad0021_item(0), .ANNCATNAME = listAnad0021_item(1), .COLINDEX = listAnad0021_item(2), .TBLNM = "D0021", .HYOJJN = Short.Parse(listAnaItem.Item(int).HYOJJN)}))
								Else
									listAnaItem.Item(int).COLNAME = listAnad0021_item(0)
									listAnaItem.Item(int).ANNCATNAME = listAnad0021_item(1)
									listAnaItem.Item(int).COLINDEX = listAnad0021_item(2)
									listAnaItem.Item(int).TBLNM = "D0021"
								End If
							End If
						End If
					Next
				Next

				ViewBag.listAnad0022 = listAnad0022
                ViewBag.listAnad0020 = listAnad0020
				ViewBag.listAnad0021 = listAnad0021

				ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", screenD0010.CATCD)

                If lastForm IsNot Nothing Then
                    ViewBag.lastForm = lastForm
                End If

                Return View(screenD0010)

            End If

            Return View("Index")
        End Function

        Function Delete(ByVal AV_GYOMNO As String, ByVal lastForm As String) As ActionResult
            'If Nothing Then
            If IsNothing(AV_GYOMNO) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            'If Session Time Out Then Return Back to Main Page
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

			'業務テーブルからダ－タ取得
			Dim d0010 As D0010 = db.D0010.Find(Convert.ToDecimal(AV_GYOMNO))

            Dim listFreeItem As New List(Of String)
            Dim listAnaItem As New List(Of String())
            Dim listitemvalue As New List(Of String)

            Dim m0150 As M0150 = db.M0150.Where(Function(m) m.SPORTCATCD = d0010.SPORTCATCD And m.SPORTSUBCATCD = d0010.SPORTSUBCATCD).FirstOrDefault

            If m0150 IsNot Nothing Then
                If m0150.COL01_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL01)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL01_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL01").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL01, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If

                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If


                If m0150.COL02_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL02)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL02_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL02").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL02, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL03_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL03)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL03_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL03").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL03, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL04_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL04)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL04_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL04").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL04, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL05_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL05)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL05_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL05").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL05, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL06_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL06)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL06_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL06").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL06, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL07_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL07)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL07_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL07").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL07, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL08_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL08)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL08_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL08").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL08, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL09_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL09)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL09_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL09").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL09, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL10_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL10)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL10_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL10").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL10, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL11_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL11)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL11_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL11").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL11, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL12_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL12)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL12_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL12").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL12, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL13_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL13)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL13_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL13").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL13, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL14_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL14)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL14_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL14").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL14, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL15_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL15)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL15_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL15").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL15, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL16_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL16)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL16_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL16").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL16, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL17_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL17)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL17_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL17").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL17, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL18_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL18)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL18_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL18").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL18, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL19_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL19)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL19_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL19").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL19, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL20_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL20)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL20_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL20").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL20, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL21_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL21)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL21_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL21").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL21, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL22_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL22)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL22_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL22").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL22, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL23_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL23)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL23_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL23").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL23, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

                If m0150.COL24_TYPE = "1" Then
                    listFreeItem.Add(m0150.COL24)
                    listAnaItem.Add(Nothing)
                ElseIf m0150.COL24_TYPE = "2" Then
                    Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL24").FirstOrDefault
                    If d0022 IsNot Nothing Then
                        listAnaItem.Add({m0150.COL24, d0022.USERID})
                        listFreeItem.Add(Nothing)
                    Else
                        listAnaItem.Add(Nothing)
                        listFreeItem.Add(Nothing)
                    End If
                Else
                    listAnaItem.Add(Nothing)
                    listFreeItem.Add(Nothing)
                End If

				If m0150.COL25_TYPE = "1" Then
					listFreeItem.Add(m0150.COL25)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL25_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL25").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL25, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				If m0150.COL26_TYPE = "1" Then
					listFreeItem.Add(m0150.COL26)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL26_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL26").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL26, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If

				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If


				If m0150.COL27_TYPE = "1" Then
					listFreeItem.Add(m0150.COL27)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL27_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL27").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL27, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL28_TYPE = "1" Then
					listFreeItem.Add(m0150.COL28)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL28_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL28").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL28, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL29_TYPE = "1" Then
					listFreeItem.Add(m0150.COL29)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL29_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL29").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL29, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL30_TYPE = "1" Then
					listFreeItem.Add(m0150.COL30)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL30_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL30").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL30, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL31_TYPE = "1" Then
					listFreeItem.Add(m0150.COL31)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL31_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL31").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL31, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL32_TYPE = "1" Then
					listFreeItem.Add(m0150.COL32)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL32_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL32").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL32, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL33_TYPE = "1" Then
					listFreeItem.Add(m0150.COL33)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL33_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL33").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL33, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL34_TYPE = "1" Then
					listFreeItem.Add(m0150.COL34)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL34_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL34").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL34, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL35_TYPE = "1" Then
					listFreeItem.Add(m0150.COL35)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL35_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL35").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL35, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL36_TYPE = "1" Then
					listFreeItem.Add(m0150.COL36)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL36_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL36").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL36, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL37_TYPE = "1" Then
					listFreeItem.Add(m0150.COL37)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL37_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL37").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL37, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL38_TYPE = "1" Then
					listFreeItem.Add(m0150.COL38)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL38_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL38").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL38, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL39_TYPE = "1" Then
					listFreeItem.Add(m0150.COL39)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL39_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL39").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL39, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL40_TYPE = "1" Then
					listFreeItem.Add(m0150.COL40)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL40_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL40").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL40, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL41_TYPE = "1" Then
					listFreeItem.Add(m0150.COL41)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL41_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL41").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL41, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL42_TYPE = "1" Then
					listFreeItem.Add(m0150.COL42)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL42_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL42").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL42, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL43_TYPE = "1" Then
					listFreeItem.Add(m0150.COL43)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL43_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL43").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL43, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL44_TYPE = "1" Then
					listFreeItem.Add(m0150.COL44)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL44_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL44").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL44, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL45_TYPE = "1" Then
					listFreeItem.Add(m0150.COL45)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL45_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL45").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL45, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL46_TYPE = "1" Then
					listFreeItem.Add(m0150.COL46)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL46_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL46").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL46, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL47_TYPE = "1" Then
					listFreeItem.Add(m0150.COL47)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL47_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL47").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL47, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL48_TYPE = "1" Then
					listFreeItem.Add(m0150.COL48)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL48_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL48").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL48, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL49_TYPE = "1" Then
					listFreeItem.Add(m0150.COL49)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL49_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL49").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL49, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				If m0150.COL50_TYPE = "1" Then
					listFreeItem.Add(m0150.COL50)
					listAnaItem.Add(Nothing)
				ElseIf m0150.COL50_TYPE = "2" Then
					Dim d0022 As D0022 = db.D0022.Where(Function(m) m.GYOMNO = AV_GYOMNO AndAlso m.COLNM = "COL50").FirstOrDefault
					If d0022 IsNot Nothing Then
						listAnaItem.Add({m0150.COL50, d0022.USERID})
						listFreeItem.Add(Nothing)
					Else
						listAnaItem.Add(Nothing)
						listFreeItem.Add(Nothing)
					End If
				Else
					listAnaItem.Add(Nothing)
					listFreeItem.Add(Nothing)
				End If

				listitemvalue.Add(d0010.COL01)
                listitemvalue.Add(d0010.COL02)
                listitemvalue.Add(d0010.COL03)
                listitemvalue.Add(d0010.COL04)
                listitemvalue.Add(d0010.COL05)
                listitemvalue.Add(d0010.COL06)
                listitemvalue.Add(d0010.COL07)
                listitemvalue.Add(d0010.COL08)
                listitemvalue.Add(d0010.COL09)
                listitemvalue.Add(d0010.COL10)
                listitemvalue.Add(d0010.COL11)
                listitemvalue.Add(d0010.COL12)
                listitemvalue.Add(d0010.COL13)
                listitemvalue.Add(d0010.COL14)
                listitemvalue.Add(d0010.COL15)
                listitemvalue.Add(d0010.COL16)
                listitemvalue.Add(d0010.COL17)
                listitemvalue.Add(d0010.COL18)
                listitemvalue.Add(d0010.COL19)
                listitemvalue.Add(d0010.COL20)
                listitemvalue.Add(d0010.COL21)
                listitemvalue.Add(d0010.COL22)
                listitemvalue.Add(d0010.COL23)
                listitemvalue.Add(d0010.COL24)
				listitemvalue.Add(d0010.COL25)

				'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
				listitemvalue.Add(d0010.COL26)
				listitemvalue.Add(d0010.COL27)
				listitemvalue.Add(d0010.COL28)
				listitemvalue.Add(d0010.COL29)
				listitemvalue.Add(d0010.COL30)
				listitemvalue.Add(d0010.COL31)
				listitemvalue.Add(d0010.COL32)
				listitemvalue.Add(d0010.COL33)
				listitemvalue.Add(d0010.COL34)
				listitemvalue.Add(d0010.COL35)
				listitemvalue.Add(d0010.COL36)
				listitemvalue.Add(d0010.COL37)
				listitemvalue.Add(d0010.COL38)
				listitemvalue.Add(d0010.COL39)
				listitemvalue.Add(d0010.COL40)
				listitemvalue.Add(d0010.COL41)
				listitemvalue.Add(d0010.COL42)
				listitemvalue.Add(d0010.COL43)
				listitemvalue.Add(d0010.COL44)
				listitemvalue.Add(d0010.COL45)
				listitemvalue.Add(d0010.COL46)
				listitemvalue.Add(d0010.COL47)
				listitemvalue.Add(d0010.COL48)
				listitemvalue.Add(d0010.COL49)
				listitemvalue.Add(d0010.COL50)

			End If

            ViewBag.FreeItemList = listFreeItem
            ViewBag.AnaItemList = listAnaItem
            ViewBag.listitemvalue = listitemvalue
            d0010.M0150 = m0150

            ViewBag.SPORTCATNM = db.M0130.Find(d0010.SPORTCATCD).SPORTCATNM
            ViewBag.SPORTSUBCATNM = db.M0140.Find(d0010.SPORTSUBCATCD).SPORTSUBCATNM

			Dim m0010List = From m In db.M0010 Select m
			'm0010List = m0010List.Where(Function(d1) db.M0160.Where(Function(m) m.SPORTCATCD = d0010.SPORTCATCD AndAlso d1.STATUS = True AndAlso d1.HYOJ = True).Select(Function(t2) t2.USERID).Contains(d1.USERID) Or (d1.KARIANA = True And d1.STATUS = True))

			Dim lstUSERID As New Dictionary(Of Integer, String)

            For Each m0010 As M0010 In m0010List.ToList
                lstUSERID.Add(m0010.USERID, m0010.USERNM)
            Next

            ViewBag.lstUSERID = lstUSERID
            If lastForm IsNot Nothing Then
                ViewBag.lastForm = lastForm
            End If

            Return View(d0010)

        End Function

        <HttpPost>
		<ActionName("Delete")>
		Function DeletePost(<Bind(Include:="GYOMNO,SPORTCATCD,GYOMYMD")> d0010 As D0010, ByVal lastForm As String) As ActionResult
			'If Nothing Then
			If IsNothing(d0010.GYOMNO) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			'If Session Time Out Then Return Back to Main Page
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If


			Using tran As DbContextTransaction = db.Database.BeginTransaction
				Try
					Dim d0010_mainChild = (From d0010_child In db.D0010
										   Where (d0010_child.PGYOMNO = d0010.GYOMNO OrElse d0010_child.GYOMNO = d0010.GYOMNO) AndAlso
														d0010_child.OYAGYOMFLG = True
										   Select d0010_child).ToList


					db.D0010.RemoveRange(d0010_mainChild)

					Dim gyom = d0010_mainChild.Select(Function(w) w.GYOMNO).ToList

					Dim d0022_delete = db.D0022.Where(Function(x) gyom.Contains(x.GYOMNO)).ToList
					db.D0022.RemoveRange(d0022_delete)

					db.Configuration.ValidateOnSaveEnabled = False
					db.SaveChanges()
					db.Configuration.ValidateOnSaveEnabled = True
					tran.Commit()

				Catch ex As Exception
					Throw ex
					tran.Rollback()
				End Try
			End Using
			'業務テーブルからダ－タ取得
			Return Redirect(Session("A0220EditRtnUrl" & d0010.GYOMNO))
			'Return RedirectToAction("Index", "A0240", routeValues:=New With {.SportCatCd = d0010.SPORTCATCD, .Searchdt = d0010.GYOMYMD.ToString().Substring(0, 7), .lastForm = lastForm})
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

        Private Function ChangeToHHMM(ByVal strTime As String)

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

        Private Function GetMaxGyomno() As Decimal
            '業務番号の最大値の取得
            Dim decMaxGyomno As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "0000")
            Dim lstgyomno = (From t In db.D0010 Where t.GYOMNO > decMaxGyomno Select t.GYOMNO).ToList
            If lstgyomno.Count > 0 Then
                decMaxGyomno = lstgyomno.Max
            End If

            Return decMaxGyomno
        End Function

        'this function call by AJAX call to fetch list of SportSubCatCd corresponding to selected SportCatCd
        Function getSportSubCatCdList(screenSportCatCd As String) As JsonResult
            If screenSportCatCd IsNot Nothing Then
                Dim lstSportSubCatNm = (From m1 In db.M0140
                                        Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
                                        Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
                                        Where m2.SPORTCATCD = screenSportCatCd And m1.HYOJ = True
                                        Order By m1.HYOJJN
                                        Select m1).ToList()
                Return Json(New With {.success = True, .sportSubCatCdList = lstSportSubCatNm})
            Else
                Return Json(New With {.success = True, .sportSubCatCdList = Nothing})
            End If

        End Function

        Public Shared Function DateRange(Start As DateTime, Thru As DateTime) As IEnumerable(Of Date)
            Return Enumerable.Range(0, (Thru.Date - Start.Date).Days + 1).Select(Function(i) Start.AddDays(i))
        End Function

    End Class
End Namespace