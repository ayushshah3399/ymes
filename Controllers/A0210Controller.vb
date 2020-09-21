Imports System.Net
Imports System.Web.Mvc
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Globalization

Namespace Controllers
	Public Class A0210Controller
		Inherits Controller

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

		' GET: M0130
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

			ViewBag.LoginUserSystem = Session("LoginUserSystem")
			ViewBag.LoginUserKanri = Session("LoginUserKanri")
			ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

			'一括業務メニューを表示可能な条件追加
			Dim intUserid As Integer = Integer.Parse(loginUserId)
			Dim m0010KOKYU = db.M0010.Find(intUserid)
			ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
			ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL

			If Session("m0150") IsNot Nothing Then
				Session("m0150") = Nothing
			End If
			If Session("m0130") IsNot Nothing Then
				Session("m0130") = Nothing
			End If
			If Session("Mode") IsNot Nothing Then
				Session("Mode") = Nothing
			End If
			If Session("IsDataChanged") IsNot Nothing Then
				Session("IsDataChanged") = Nothing
			End If
			If Session("IsDataChangedCreate") IsNot Nothing Then
				Session("IsDataChangedCreate") = Nothing
			End If

			Dim M0130List = db.M0130.OrderBy(Function(f) f.HYOJJN).ToList()
			Return View(M0130List)

		End Function

		' GET: M0130/Create
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

			Dim m0130 = New M0130
			Dim m0150List = db.M0150

			Session("Mode") = "新規"

			ViewData("M0150List") = m0150List

			If Session("m0130") IsNot Nothing Then
				m0130 = Session("m0130")
			End If

			Return View(m0130)
		End Function

		<HttpPost()>
		Function EditM0150(<Bind(Include:="SPORTCATCD,SPORTCATNM,HYOJJN,HYOJ,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,M0140,M0150")> ByVal m0130 As M0130, ByVal opType As String, ByVal m0150SelectedIndex As String, ByVal IsDataChanged As Boolean, ByVal IsDataChangedCreate As Boolean) As ActionResult

			'OpType is 1 indicate a operation on when press detail link
			If opType = "1" Then

				Dim loginUserId As String = Session("LoginUserid")
				If loginUserId = Nothing Then
					Return ReturnLoginPartial()
				End If
				ViewData!LoginUsernm = Session("LoginUsernm")

				If CheckAccessLvl() = False Then
					Return View("ErrorAccesslvl")
				End If

				If ModelState.IsValid Then

					'Put the value in session
					Session("m0130") = m0130

					Dim m0150Object As M0150 = New M0150()

					Dim m0140 As M0140 = New M0140()

					'Case of Edit
					If m0130.SPORTCATCD <> 0 Then

						'Loop to get matching record of M0140
						For i As Integer = 0 To m0130.M0140.Count - 1

							If m0130.M0140(i).SELECTEDINDEX = m0150SelectedIndex Then

								'Find which record of m0140 has selected
								m0140 = m0130.M0140(i)

								'Check record is not new on selected index
								If m0140.SPORTSUBCATCD <> 0 Then

									'Since there is no change then get data from DB
									m0150Object = (From m150 In db.M0150
												   Where m150.SPORTCATCD = m0130.SPORTCATCD AndAlso m150.SPORTSUBCATCD = m0140.SPORTSUBCATCD
												   Select m150).FirstOrDefault()

									'Check session has data for M0150
									If Session("m0150") IsNot Nothing Then

										Dim m0150List As ICollection(Of M0150) = Session("m0150")

										For k As Integer = 0 To m0150List.Count - 1

											'If match found then get data from session
											If m0150List(k).SELECTINDEX = m0150SelectedIndex Then
												m0150Object = m0150List(k)
												Exit For
											End If

										Next

									End If

								Else

									'Record is a new entry so get M0150 object from session
									'Fetch data from session
									If Session("m0150") IsNot Nothing Then

										Dim m0150List As ICollection(Of M0150) = Session("m0150")

										For j As Integer = 0 To m0150List.Count - 1

											'To check matching between M0140 and M0150
											If m0150List(j).SELECTINDEX = m0150SelectedIndex Then
												m0150Object = m0150List(j)
												Exit For
											End If

										Next

									End If

								End If

								Exit For

							End If

						Next

						'Case of New
					Else

						'Fetch data from session
						If Session("m0150") IsNot Nothing Then

							Dim m0150List As ICollection(Of M0150) = Session("m0150")

							For j As Integer = 0 To m0150List.Count - 1

								'To check matching between M0140 and M0150
								If m0150List(j).SELECTINDEX = m0150SelectedIndex Then
									m0150Object = m0150List(j)
									Exit For
								End If

							Next

						End If

					End If

					'Loop to get matching record of M0140
					For i As Integer = 0 To m0130.M0140.Count - 1
						If m0130.M0140(i).SELECTEDINDEX = m0150SelectedIndex Then
							'Find which record of m0140 has selected
							m0140 = m0130.M0140(i)
						End If
					Next

					'When we have not put data session then create a new object of M0150 to send to view
					If m0150Object Is Nothing Then

						m0150Object = New M0150()

						m0150Object.SPORTCATNM = m0130.SPORTCATNM
						m0150Object.SPORTSUBCATNM = m0140.SPORTSUBCATNM
						m0150Object.SELECTINDEX = m0150SelectedIndex

					Else

						'We have already a model and add extra information here
						m0150Object.SPORTCATNM = m0130.SPORTCATNM
						m0150Object.SPORTSUBCATNM = m0140.SPORTSUBCATNM
						m0150Object.SELECTINDEX = m0150SelectedIndex

					End If

					Session("IsDataChanged") = IsDataChanged
					Session("IsDataChangedCreate") = IsDataChangedCreate

					Return View("Edit", m0150Object)

				Else

					Return View("Create", m0130)

				End If

				'OpType is 2 indicate a operation on when press save button
			ElseIf opType = "2" Then

				Dim bolAlreadyExists As Boolean = False

				Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
				sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try
						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

						Dim loginUserId As String = Session("LoginUserid")
						If loginUserId = Nothing Then
							Return ReturnLoginPartial()
						End If
						ViewData!LoginUsernm = Session("LoginUsernm")

						If CheckAccessLvl() = False Then
							Return View("ErrorAccesslvl")
						End If

						'Check Model is valid i.e. no error
						If ModelState.IsValid Then

							'Create M0130 Model With M0150 Data
							If Session("m0150") Is Nothing Then
								'create all data from M0140 if it is new mode
								For i As Integer = 0 To m0130.M0140.Count - 1
									Dim sportsubcatcd = m0130.M0140(i).SPORTSUBCATCD
									If sportsubcatcd = 0 Then
										Dim m0150Data As New M0150
										m0150Data.SELECTINDEX = m0130.M0140(i).SELECTEDINDEX
										m0130.M0150.Add(m0150Data)
									End If
								Next
							Else
								'Get data from session
								Dim m0150SessioData As ICollection(Of M0150) = Session("m0150")
								Dim count As Integer = 0
								'Now check is there any data that is not in session
								For i As Integer = 0 To m0130.M0140.Count - 1
									Dim index = m0130.M0140(i).SELECTEDINDEX
									Dim M0150SelectedIndexData = m0150SessioData.Where(Function(m) m.SELECTINDEX = index).ToList()
									If M0150SelectedIndexData.Count = 0 Then
										'Add remaining data to list
										Dim sportsubcatcd = m0130.M0140(i).SPORTSUBCATCD
										If sportsubcatcd = 0 Then
											Dim m0150Data As New M0150
											m0150Data.SELECTINDEX = m0130.M0140(i).SELECTEDINDEX
											m0130.M0150.Add(m0150Data)
										End If
									Else
										'Add session data to list
										m0130.M0150.Add(m0150SessioData(count))
										count = count + 1
									End If
								Next
							End If

							If m0130.HYOJ.Equals(False) Then
								m0130.HYOJJN = "999"
							End If

							'Add M0130 model object to model for save
							db.M0130.Add(m0130)

							Dim m0140 As M0140 = New M0140()

							'Add M0140 model object to model for save
							For i As Integer = 0 To m0130.M0140.Count - 1
								db.M0140.Add(m0130.M0140(i))
							Next

							db.Configuration.ValidateOnSaveEnabled = False

							Dim m0140DelList As New List(Of M0140)()

							'Case of update
							If m0130.SPORTCATCD <> 0 Then

								'Since Sportcatcd is exists so entry will update
								db.Entry(m0130).State = EntityState.Modified

								'Also change state of M0140 enrty list to updated
								For i As Integer = 0 To m0130.M0140.Count - 1

									'In M0140 List entry which has sportsubcatcd is modified entry
									If m0130.M0140(i).SPORTSUBCATCD <> 0 Then
										db.Entry(m0130.M0140(i)).State = EntityState.Modified
									End If

								Next

								'when enrty is deleted fatch record on code from db and compare it with list
								'check it is missing or not
								Dim m0140List = (From m In db.M0150
												 Join m2 In db.M0140 On m.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
												 Where m.SPORTCATCD = m0130.SPORTCATCD
												 Select m2).ToList()

								For i As Integer = 0 To m0140List.Count - 1

									Dim bolFound As Boolean = False

									For j As Integer = 0 To m0130.M0140.Count - 1

										'To match data from db to data of list 
										If m0140List(i).SPORTSUBCATCD = m0130.M0140(j).SPORTSUBCATCD Then
											bolFound = True
										End If

									Next

									'if entry is found make a delete record list
									If bolFound = False Then

										m0140DelList.Add(m0140List(i))

									End If

								Next

								'If deleted entry then add it to the model for save
								If m0140DelList.Count > 0 Then

									For i As Integer = 0 To m0140DelList.Count - 1
										db.M0140.Add(m0140DelList(i))
										db.Entry(m0140DelList(i)).State = EntityState.Deleted
									Next

								End If

							End If

							db.SaveChanges()

							db.Configuration.ValidateOnSaveEnabled = True

							'Manage Insert,update and delete in M0150

							Dim sportcatcode1 As String = m0130.SPORTCATCD

							For i As Integer = 0 To m0130.M0140.Count - 1

								For j As Integer = 0 To m0130.M0150.Count - 1

									'Get matching record from list and add in M0150 model
									'If m0130.M0140(i).SPORTSUBCATNM = m0130.M0150(j).SPORTSUBCATNM AndAlso m0130.SPORTCATNM = m0130.M0150(j).SPORTCATNM Then
									If m0130.M0140(i).SELECTEDINDEX = m0130.M0150(j).SELECTINDEX Then

										Dim m0140Local As M0140 = m0130.M0140(i)

										Dim sportcatcode2 As String = m0140Local.SPORTSUBCATCD

										Dim bolM0150Exists As Boolean = False

										'Checck entry in M0150 is edit or new
										If m0130.M0150(j).SPORTSUBCATCD <> 0 Then
											bolM0150Exists = True
										End If

										m0130.M0150(j).SPORTCATCD = sportcatcode1

										m0130.M0150(j).SPORTSUBCATCD = sportcatcode2

										db.M0150.Add(m0130.M0150(j))

										'make record to eligible for update
										If bolM0150Exists = True Then

											db.Entry(m0130.M0150(j)).State = EntityState.Modified

										End If

									End If

								Next

							Next

							'In case when records are deleted from parent table M0140
							'It is reuired to delete table from M0150
							Dim m0150List = (From m In db.M0150
											 Where m.SPORTCATCD = m0130.SPORTCATCD
											 Select m).ToList()

							'Prepare a delete list from M0150
							Dim m0150DelList As New List(Of M0150)()

							For i As Integer = 0 To m0150List.Count - 1

								Dim bolFound As Boolean = False

								For j As Integer = 0 To m0140DelList.Count - 1

									'Match code to deleted list of M0140 so that we can get record to be deleted from M0150
									If m0150List(i).SPORTSUBCATCD = m0140DelList(j).SPORTSUBCATCD Then
										bolFound = True
									End If

								Next

								If bolFound = True Then

									m0150DelList.Add(m0150List(i))

								End If

							Next

							'Change state to delete
							If m0150DelList.Count > 0 Then

								For i As Integer = 0 To m0150DelList.Count - 1
									db.M0150.Add(m0150DelList(i))
									db.Entry(m0150DelList(i)).State = EntityState.Deleted
								Next

							End If

							db.Configuration.ValidateOnSaveEnabled = False
							db.SaveChanges()
							db.Configuration.ValidateOnSaveEnabled = True

							tran.Commit()

							Session("m0150") = Nothing
							Session("m0130") = Nothing

							Return RedirectToAction("Index")

						End If

					Catch ex As Exception
						Throw ex
						Session("m0150") = Nothing
						Session("m0130") = Nothing
						tran.Rollback()
					End Try

				End Using

			End If

			Return View("Create", m0130)

		End Function

		<HttpPost()>
		Function CreateM0130(<Bind(Include:="SPORTCATCD,SPORTSUBCATCD,BANGUMIHYOJ1,KSKJKNHYOJ1,OAJKNHYOJ1,SAIKNHYOJ1,BASYOHYOJ1,BIKOHYOJ1,BANGUMIHYOJNM1,KSKJKNHYOJNM1,OAJKNHYOJNM1,
										SAIKNHYOJNM1,BASYOHYOJNM1,BIKOHYOJNM1,BANGUMIHYOJJN1,KSKJKNHYOJJN1,OAJKNHYOJJN1,SAIKNHYOJJN1,BASYOHYOJJN1,BIKOHYOJJN1,BANGUMIHYOJ2,KSKJKNHYOJ2,
										OAJKNHYOJ2,SAIKNHYOJ2,BASYOHYOJ2,BIKOHYOJ2,BANGUMIHYOJNM2,KSKJKNHYOJNM2,OAJKNHYOJNM2,SAIKNHYOJNM2,BASYOHYOJNM2,BIKOHYOJNM2,BANGUMIHYOJJN2,KSKJKNHYOJJN2,OAJKNHYOJJN2,SAIKNHYOJJN2,BASYOHYOJJN2,BIKOHYOJJN2,
										COL01,COL02,COL03,COL04,COL05,COL06,COL07,COL08,COL09,COL10,COL11,COL12,COL13,COL14,COL15,COL16,COL17,COL18,COL19,COL20,COL21,COL22,COL23,COL24,COL25,
										COL26,COL27,COL28,COL29,COL30,COL31,COL32,COL33,COL34,COL35,COL36,COL37,COL38,COL39,COL40,COL41,COL42,COL43,COL44,COL45,COL46,COL47,COL48,COL49,COL50,
										COL01_TYPE,COL02_TYPE,COL03_TYPE,COL04_TYPE,COL05_TYPE,COL06_TYPE,COL07_TYPE,COL08_TYPE,COL09_TYPE,COL10_TYPE,COL11_TYPE,COL12_TYPE,COL13_TYPE,COL14_TYPE,COL15_TYPE,COL16_TYPE,COL17_TYPE,COL18_TYPE,COL19_TYPE,COL20_TYPE,COL21_TYPE,COL22_TYPE,COL23_TYPE,COL24_TYPE,COL25_TYPE,
										COL26_TYPE,COL27_TYPE,COL28_TYPE,COL29_TYPE,COL30_TYPE,COL31_TYPE,COL32_TYPE,COL33_TYPE,COL34_TYPE,COL35_TYPE,COL36_TYPE,COL37_TYPE,COL38_TYPE,COL39_TYPE,COL40_TYPE,COL41_TYPE,COL42_TYPE,COL43_TYPE,COL44_TYPE,COL45_TYPE,COL46_TYPE,COL47_TYPE,COL48_TYPE,COL49_TYPE,COL50_TYPE,
										COL01_HYOJ1,COL02_HYOJ1,COL03_HYOJ1,COL04_HYOJ1,COL05_HYOJ1,COL06_HYOJ1,COL07_HYOJ1,COL08_HYOJ1,COL09_HYOJ1,COL10_HYOJ1,COL11_HYOJ1,COL12_HYOJ1,COL13_HYOJ1,COL14_HYOJ1,COL15_HYOJ1,COL16_HYOJ1,COL17_HYOJ1,COL18_HYOJ1,COL19_HYOJ1,COL20_HYOJ1,COL21_HYOJ1,COL22_HYOJ1,COL23_HYOJ1,COL24_HYOJ1,COL25_HYOJ1,
										COL26_HYOJ1,COL27_HYOJ1,COL28_HYOJ1,COL29_HYOJ1,COL30_HYOJ1,COL31_HYOJ1,COL32_HYOJ1,COL33_HYOJ1,COL34_HYOJ1,COL35_HYOJ1,COL36_HYOJ1,COL37_HYOJ1,COL38_HYOJ1,COL39_HYOJ1,COL40_HYOJ1,COL41_HYOJ1,COL42_HYOJ1,COL43_HYOJ1,COL44_HYOJ1,COL45_HYOJ1,COL46_HYOJ1,COL47_HYOJ1,COL48_HYOJ1,COL49_HYOJ1,COL50_HYOJ1,
										COL01_HYOJNM1,COL02_HYOJNM1,COL03_HYOJNM1,COL04_HYOJNM1,COL05_HYOJNM1,COL06_HYOJNM1,COL07_HYOJNM1,COL08_HYOJNM1,COL09_HYOJNM1,COL10_HYOJNM1,COL11_HYOJNM1,COL12_HYOJNM1,COL13_HYOJNM1,COL14_HYOJNM1,COL15_HYOJNM1,COL16_HYOJNM1,COL17_HYOJNM1,COL18_HYOJNM1,COL19_HYOJNM1,COL20_HYOJNM1,COL21_HYOJNM1,COL22_HYOJNM1,COL23_HYOJNM1,COL24_HYOJNM1,COL25_HYOJNM1,
										COL26_HYOJNM1,COL27_HYOJNM1,COL28_HYOJNM1,COL29_HYOJNM1,COL30_HYOJNM1,COL31_HYOJNM1,COL32_HYOJNM1,COL33_HYOJNM1,COL34_HYOJNM1,COL35_HYOJNM1,COL36_HYOJNM1,COL37_HYOJNM1,COL38_HYOJNM1,COL39_HYOJNM1,COL40_HYOJNM1,COL41_HYOJNM1,COL42_HYOJNM1,COL43_HYOJNM1,COL44_HYOJNM1,COL45_HYOJNM1,COL46_HYOJNM1,COL47_HYOJNM1,COL48_HYOJNM1,COL49_HYOJNM1,COL50_HYOJNM1,
										COL01_HYOJJN1,COL02_HYOJJN1,COL03_HYOJJN1,COL04_HYOJJN1,COL05_HYOJJN1,COL06_HYOJJN1,COL07_HYOJJN1,COL08_HYOJJN1,COL09_HYOJJN1,COL10_HYOJJN1,COL11_HYOJJN1,COL12_HYOJJN1,COL13_HYOJJN1,COL14_HYOJJN1,COL15_HYOJJN1,COL16_HYOJJN1,COL17_HYOJJN1,COL18_HYOJJN1,COL19_HYOJJN1,COL20_HYOJJN1,COL21_HYOJJN1,COL22_HYOJJN1,COL23_HYOJJN1,COL24_HYOJJN1,COL25_HYOJJN1,
										COL26_HYOJJN1,COL27_HYOJJN1,COL28_HYOJJN1,COL29_HYOJJN1,COL30_HYOJJN1,COL31_HYOJJN1,COL32_HYOJJN1,COL33_HYOJJN1,COL34_HYOJJN1,COL35_HYOJJN1,COL36_HYOJJN1,COL37_HYOJJN1,COL38_HYOJJN1,COL39_HYOJJN1,COL40_HYOJJN1,COL41_HYOJJN1,COL42_HYOJJN1,COL43_HYOJJN1,COL44_HYOJJN1,COL45_HYOJJN1,COL46_HYOJJN1,COL47_HYOJJN1,COL48_HYOJJN1,COL49_HYOJJN1,COL50_HYOJJN1,
										COL01_HYOJ2,COL02_HYOJ2,COL03_HYOJ2,COL04_HYOJ2,COL05_HYOJ2,COL06_HYOJ2,COL07_HYOJ2,COL08_HYOJ2,COL09_HYOJ2,COL10_HYOJ2,COL11_HYOJ2,COL12_HYOJ2,COL13_HYOJ2,COL14_HYOJ2,COL15_HYOJ2,COL16_HYOJ2,COL17_HYOJ2,COL18_HYOJ2,COL19_HYOJ2,COL20_HYOJ2,COL21_HYOJ2,COL22_HYOJ2,COL23_HYOJ2,COL24_HYOJ2,COL25_HYOJ2,
										COL26_HYOJ2,COL27_HYOJ2,COL28_HYOJ2,COL29_HYOJ2,COL30_HYOJ2,COL31_HYOJ2,COL32_HYOJ2,COL33_HYOJ2,COL34_HYOJ2,COL35_HYOJ2,COL36_HYOJ2,COL37_HYOJ2,COL38_HYOJ2,COL39_HYOJ2,COL40_HYOJ2,COL41_HYOJ2,COL42_HYOJ2,COL43_HYOJ2,COL44_HYOJ2,COL45_HYOJ2,COL46_HYOJ2,COL47_HYOJ2,COL48_HYOJ2,COL49_HYOJ2,COL50_HYOJ2,
										COL01_HYOJNM2,COL02_HYOJNM2,COL03_HYOJNM2,COL04_HYOJNM2,COL05_HYOJNM2,COL06_HYOJNM2,COL07_HYOJNM2,COL08_HYOJNM2,COL09_HYOJNM2,COL10_HYOJNM2,COL11_HYOJNM2,COL12_HYOJNM2,COL13_HYOJNM2,COL14_HYOJNM2,COL15_HYOJNM2,COL16_HYOJNM2,COL17_HYOJNM2,COL18_HYOJNM2,COL19_HYOJNM2,COL20_HYOJNM2,COL21_HYOJNM2,COL22_HYOJNM2,COL23_HYOJNM2,COL24_HYOJNM2,COL25_HYOJNM2,
										COL26_HYOJNM2,COL27_HYOJNM2,COL28_HYOJNM2,COL29_HYOJNM2,COL30_HYOJNM2,COL31_HYOJNM2,COL32_HYOJNM2,COL33_HYOJNM2,COL34_HYOJNM2,COL35_HYOJNM2,COL36_HYOJNM2,COL37_HYOJNM2,COL38_HYOJNM2,COL39_HYOJNM2,COL40_HYOJNM2,COL41_HYOJNM2,COL42_HYOJNM2,COL43_HYOJNM2,COL44_HYOJNM2,COL45_HYOJNM2,COL46_HYOJNM2,COL47_HYOJNM2,COL48_HYOJNM2,COL49_HYOJNM2,COL50_HYOJNM2,
										COL01_HYOJJN2,COL02_HYOJJN2,COL03_HYOJJN2,COL04_HYOJJN2,COL05_HYOJJN2,COL06_HYOJJN2,COL07_HYOJJN2,COL08_HYOJJN2,COL09_HYOJJN2,COL10_HYOJJN2,COL11_HYOJJN2,COL12_HYOJJN2,COL13_HYOJJN2,COL14_HYOJJN2,COL15_HYOJJN2,COL16_HYOJJN2,COL17_HYOJJN2,COL18_HYOJJN2,COL19_HYOJJN2,COL20_HYOJJN2,COL21_HYOJJN2,COL22_HYOJJN2,COL23_HYOJJN2,COL24_HYOJJN2,COL25_HYOJJN2,
										COL26_HYOJJN2,COL27_HYOJJN2,COL28_HYOJJN2,COL29_HYOJJN2,COL30_HYOJJN2,COL31_HYOJJN2,COL32_HYOJJN2,COL33_HYOJJN2,COL34_HYOJJN2,COL35_HYOJJN2,COL36_HYOJJN2,COL37_HYOJJN2,COL38_HYOJJN2,COL39_HYOJJN2,COL40_HYOJJN2,COL41_HYOJJN2,COL42_HYOJJN2,COL43_HYOJJN2,COL44_HYOJJN2,COL45_HYOJJN2,COL46_HYOJJN2,COL47_HYOJJN2,COL48_HYOJJN2,COL49_HYOJJN2,COL50_HYOJJN2,SPORTCATNM,SPORTSUBCATNM,SELECTINDEX,FREEZE_LSTCOLNM")> ByVal m0150 As M0150, ByVal IsDataChanged As Boolean, ByVal FREEZE_LSTCOLNM As String, ByVal IsDataChangedCreate As Boolean) As ActionResult

			m0150.FREEZE_LSTCOLNM = FREEZE_LSTCOLNM
			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim m0130Store As M0130 = Session("m0130")

			If m0130Store Is Nothing Then
				Return RedirectToAction("Index")
			End If

			'Now you can check model state before saving data to session
			If ModelState.IsValid Then

				'Change CheckBox value in Model that has set to false
				If m0150.BANGUMIHYOJ1 = "false" Then
					m0150.BANGUMIHYOJ1 = "0"
				End If
				If m0150.BANGUMIHYOJ2 = "false" Then
					m0150.BANGUMIHYOJ2 = "0"
				End If
				If m0150.KSKJKNHYOJ1 = "false" Then
					m0150.KSKJKNHYOJ1 = "0"
				End If
				If m0150.KSKJKNHYOJ2 = "false" Then
					m0150.KSKJKNHYOJ2 = "0"
				End If
				If m0150.OAJKNHYOJ1 = "false" Then
					m0150.OAJKNHYOJ1 = "0"
				End If
				If m0150.OAJKNHYOJ2 = "false" Then
					m0150.OAJKNHYOJ2 = "0"
				End If
				If m0150.SAIKNHYOJ1 = "false" Then
					m0150.SAIKNHYOJ1 = "0"
				End If
				If m0150.SAIKNHYOJ2 = "false" Then
					m0150.SAIKNHYOJ2 = "0"
				End If
				If m0150.BASYOHYOJ1 = "false" Then
					m0150.BASYOHYOJ1 = "0"
				End If
				If m0150.BASYOHYOJ2 = "false" Then
					m0150.BASYOHYOJ2 = "0"
				End If
				If m0150.BIKOHYOJ1 = "false" Then
					m0150.BIKOHYOJ1 = "0"
				End If
				If m0150.BIKOHYOJ2 = "false" Then
					m0150.BIKOHYOJ2 = "0"
				End If

				Session("IsDataChanged") = IsDataChanged
				Session("IsDataChangedCreate") = IsDataChangedCreate

				'Check session is available if it has means it has record
				'Further you can add record in this session
				If Session("m0150") IsNot Nothing Then

					Dim m0150SessionList As List(Of M0150) = Session("m0150")

					If m0150SessionList.Count > 0 Then

						Dim bolm0150ExistsInSession As Boolean = False

						For i As Integer = 0 To m0150SessionList.Count - 1

							'Check is entry already done in M0150?
							'if record is there then simply replace
							If m0150SessionList(i).SELECTINDEX = m0150.SELECTINDEX Then
								bolm0150ExistsInSession = True
								m0150SessionList(i) = m0150
								Exit For
							End If

						Next

						'If record is not in session add them
						If bolm0150ExistsInSession = False Then
							m0150SessionList.Add(m0150)
						End If

						Session("m0150") = m0150SessionList

					End If

				Else

					'When record come first time to save in session
					Dim m0150List As New List(Of M0150)()

					m0150List.Add(m0150)

					Session("m0150") = m0150List

				End If

				Dim selectIndex As Integer = Convert.ToInt32(m0150.SELECTINDEX)

				'Get actual index of row in model
				Dim index As Integer
				For i = 0 To m0130Store.M0140.Count - 1
					If m0130Store.M0140(i).SELECTEDINDEX = selectIndex Then
						index = i
						Exit For
					End If
				Next

				If CheckUserSettingCompleted(m0150) Then

					m0130Store.M0140(index).USERSETTINGSTATUS = " "
				Else
					m0130Store.M0140(index).USERSETTINGSTATUS = "未"
				End If

				Return View("Create", m0130Store)

			Else

				If m0150.BANGUMIHYOJ1 <> "false" Then
					ModelState.SetModelValue("BANGUMIHYOJ1", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("BANGUMIHYOJ1", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.KSKJKNHYOJ1 <> "false" Then
					ModelState.SetModelValue("KSKJKNHYOJ1", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("KSKJKNHYOJ1", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.OAJKNHYOJ1 <> "false" Then
					ModelState.SetModelValue("OAJKNHYOJ1", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("OAJKNHYOJ1", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.SAIKNHYOJ1 <> "false" Then
					ModelState.SetModelValue("SAIKNHYOJ1", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("SAIKNHYOJ1", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.BASYOHYOJ1 <> "false" Then
					ModelState.SetModelValue("BASYOHYOJ1", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("BASYOHYOJ1", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.BIKOHYOJ1 <> "false" Then
					ModelState.SetModelValue("BIKOHYOJ1", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("BIKOHYOJ1", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.BANGUMIHYOJ2 <> "false" Then
					ModelState.SetModelValue("BANGUMIHYOJ2", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("BANGUMIHYOJ2", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.KSKJKNHYOJ2 <> "false" Then
					ModelState.SetModelValue("KSKJKNHYOJ2", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("KSKJKNHYOJ2", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.OAJKNHYOJ2 <> "false" Then
					ModelState.SetModelValue("OAJKNHYOJ2", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("OAJKNHYOJ2", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.SAIKNHYOJ2 <> "false" Then
					ModelState.SetModelValue("SAIKNHYOJ2", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("SAIKNHYOJ2", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.BASYOHYOJ2 <> "false" Then
					ModelState.SetModelValue("BASYOHYOJ2", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("BASYOHYOJ2", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If
				If m0150.BIKOHYOJ2 <> "false" Then
					ModelState.SetModelValue("BIKOHYOJ2", New ValueProviderResult("true", "true", CultureInfo.InvariantCulture))
				Else
					ModelState.SetModelValue("BIKOHYOJ2", New ValueProviderResult("false", "false", CultureInfo.InvariantCulture))
				End If

				Return View("Edit", m0150)

			End If

		End Function

		' GET: M0150/Edit/5
		Function Edit(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
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

			Dim m0130 As M0130 = db.M0130.Find(id)

			If m0130 IsNot Nothing Then
				Dim m0140List = (From m In db.M0150
								 Join m2 In db.M0140 On m.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
								 Where m.SPORTCATCD = m0130.SPORTCATCD
								 Order By m2.HYOJJN
								 Select m2).ToList()

				'Again fill the value of selected index
				For i As Integer = 0 To m0140List.Count - 1
					m0140List(i).SELECTEDINDEX = i
					Dim subcatcd = m0140List(i).SPORTSUBCATCD
					Dim m0150Data = (From m150 In db.M0150
									 Where m150.SPORTCATCD = m0130.SPORTCATCD AndAlso m150.SPORTSUBCATCD = subcatcd
									 Select m150).FirstOrDefault()

					If CheckUserSettingCompleted(m0150Data) Then
						m0140List(i).USERSETTINGSTATUS = " "
					Else
						m0140List(i).USERSETTINGSTATUS = "未"
					End If

				Next

				m0130.M0140 = m0140List

			End If

			Session("Mode") = "修正"

			If Session("m0130") IsNot Nothing Then
				m0130 = Session("m0130")
			End If

			Return View("Create", m0130)

		End Function

		' GET: A0210/Delete/5
		Function Delete(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
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

			Dim m0130 As M0130 = db.M0130.Find(id)

			If m0130 IsNot Nothing Then
				Dim m0140List = (From m In db.M0150
								 Join m2 In db.M0140 On m.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
								 Where m.SPORTCATCD = m0130.SPORTCATCD
								 Select m2).ToList()

				'Again fill the value of selected index
				For i As Integer = 0 To m0140List.Count - 1
					m0140List(i).SELECTEDINDEX = i
				Next

				m0130.M0140 = m0140List

			End If

			Return View("Delete", m0130)

		End Function

		' POST: A0210/Delete/5
		<HttpPost()>
		<ActionName("Delete")>
		Function DeleteConfirmed(ByVal id As Short) As ActionResult

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

					Dim m0130 As M0130 = db.M0130.Find(id)
					db.M0130.Remove(m0130)


					Dim m0140List = (From m1 In db.M0140
									 Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
									 Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
									 Where m3.SPORTCATCD = id
									 Select m1).ToList()

					For i As Integer = 0 To m0140List.Count - 1

						db.M0140.Remove(m0140List(i))

					Next

					Dim m0150List = (From m1 In db.M0150
									 Where m1.SPORTCATCD = id
									 Select m1).ToList()

					For i As Integer = 0 To m0150List.Count - 1

						db.M0150.Remove(m0150List(i))

					Next

					db.SaveChanges()

					tran.Commit()

				Catch ex As Exception
					tran.Rollback()
                    'Throw ex
                    Return View("ErrorSportAccesslvl")
                End Try

			End Using

			Return RedirectToAction("Index")

		End Function

		' GET: A0210/Details/5
		Function Details(ByVal id As Short?) As ActionResult

			If IsNothing(id) Then
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

			Dim m0130 As M0130 = db.M0130.Find(id)

			If m0130 IsNot Nothing Then
				Dim m0140List = (From m In db.M0150
								 Join m2 In db.M0140 On m.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
								 Where m.SPORTCATCD = m0130.SPORTCATCD
								 Select m2).ToList()

				'Again fill the value of selected index
				For i As Integer = 0 To m0140List.Count - 1
					m0140List(i).SELECTEDINDEX = i
				Next

				m0130.M0140 = m0140List

			End If

			Return View("Details", m0130)

		End Function

		Function CheckUserSettingCompleted(ByVal m0150 As M0150) As Boolean

			Dim isChanged As Boolean = False

			If m0150.BANGUMIHYOJ1 <> "0" OrElse m0150.KSKJKNHYOJ1 <> "0" OrElse m0150.OAJKNHYOJ1 <> "0" OrElse m0150.SAIKNHYOJ1 <> "0" OrElse m0150.BASYOHYOJ1 <> "0" OrElse m0150.BIKOHYOJ1 <> "0" OrElse
			  m0150.BANGUMIHYOJNM1 IsNot Nothing OrElse m0150.KSKJKNHYOJNM1 IsNot Nothing OrElse m0150.OAJKNHYOJNM1 IsNot Nothing OrElse m0150.SAIKNHYOJNM1 IsNot Nothing OrElse m0150.BASYOHYOJNM1 IsNot Nothing OrElse m0150.BIKOHYOJNM1 IsNot Nothing OrElse
			  m0150.BANGUMIHYOJJN1 IsNot Nothing OrElse m0150.KSKJKNHYOJJN1 IsNot Nothing OrElse m0150.OAJKNHYOJJN1 IsNot Nothing OrElse m0150.SAIKNHYOJJN1 IsNot Nothing OrElse m0150.BASYOHYOJJN1 IsNot Nothing OrElse m0150.BIKOHYOJJN1 IsNot Nothing OrElse
			  m0150.BANGUMIHYOJ2 <> "0" OrElse m0150.KSKJKNHYOJ2 <> "0" OrElse m0150.OAJKNHYOJ2 <> "0" OrElse m0150.SAIKNHYOJ2 <> "0" OrElse m0150.BASYOHYOJ2 <> "0" OrElse m0150.BIKOHYOJ2 <> "0" OrElse
			  m0150.BANGUMIHYOJNM2 IsNot Nothing OrElse m0150.KSKJKNHYOJNM2 IsNot Nothing OrElse m0150.OAJKNHYOJNM2 IsNot Nothing OrElse m0150.SAIKNHYOJNM2 IsNot Nothing OrElse m0150.BASYOHYOJNM2 IsNot Nothing OrElse m0150.BIKOHYOJNM2 IsNot Nothing OrElse
			  m0150.BANGUMIHYOJJN2 IsNot Nothing OrElse m0150.KSKJKNHYOJJN2 IsNot Nothing OrElse m0150.OAJKNHYOJJN2 IsNot Nothing OrElse m0150.SAIKNHYOJJN2 IsNot Nothing OrElse m0150.BASYOHYOJJN2 IsNot Nothing OrElse m0150.BIKOHYOJJN2 IsNot Nothing OrElse
			  m0150.COL01 IsNot Nothing OrElse m0150.COL02 IsNot Nothing OrElse m0150.COL03 IsNot Nothing OrElse m0150.COL04 IsNot Nothing OrElse m0150.COL05 IsNot Nothing OrElse m0150.COL06 IsNot Nothing OrElse m0150.COL07 IsNot Nothing OrElse m0150.COL08 IsNot Nothing OrElse m0150.COL09 IsNot Nothing OrElse m0150.COL10 IsNot Nothing OrElse m0150.COL11 IsNot Nothing OrElse m0150.COL12 IsNot Nothing OrElse m0150.COL13 IsNot Nothing OrElse m0150.COL14 IsNot Nothing OrElse m0150.COL15 IsNot Nothing OrElse m0150.COL16 IsNot Nothing OrElse m0150.COL17 IsNot Nothing OrElse m0150.COL18 IsNot Nothing OrElse m0150.COL19 IsNot Nothing OrElse m0150.COL20 IsNot Nothing OrElse m0150.COL21 IsNot Nothing OrElse m0150.COL22 IsNot Nothing OrElse m0150.COL23 IsNot Nothing OrElse m0150.COL24 IsNot Nothing OrElse m0150.COL25 IsNot Nothing OrElse
			  m0150.COL26 IsNot Nothing OrElse m0150.COL27 IsNot Nothing OrElse m0150.COL28 IsNot Nothing OrElse m0150.COL29 IsNot Nothing OrElse m0150.COL30 IsNot Nothing OrElse m0150.COL31 IsNot Nothing OrElse m0150.COL32 IsNot Nothing OrElse m0150.COL33 IsNot Nothing OrElse m0150.COL34 IsNot Nothing OrElse m0150.COL35 IsNot Nothing OrElse m0150.COL36 IsNot Nothing OrElse m0150.COL37 IsNot Nothing OrElse m0150.COL38 IsNot Nothing OrElse m0150.COL39 IsNot Nothing OrElse m0150.COL40 IsNot Nothing OrElse m0150.COL41 IsNot Nothing OrElse m0150.COL42 IsNot Nothing OrElse m0150.COL43 IsNot Nothing OrElse m0150.COL44 IsNot Nothing OrElse m0150.COL45 IsNot Nothing OrElse m0150.COL46 IsNot Nothing OrElse m0150.COL47 IsNot Nothing OrElse m0150.COL48 IsNot Nothing OrElse m0150.COL49 IsNot Nothing OrElse m0150.COL50 IsNot Nothing OrElse
			  m0150.COL01_TYPE <> "0" OrElse m0150.COL02_TYPE <> "0" OrElse m0150.COL03_TYPE <> "0" OrElse m0150.COL04_TYPE <> "0" OrElse m0150.COL05_TYPE <> "0" OrElse m0150.COL06_TYPE <> "0" OrElse m0150.COL07_TYPE <> "0" OrElse m0150.COL08_TYPE <> "0" OrElse m0150.COL09_TYPE <> "0" OrElse m0150.COL10_TYPE <> "0" OrElse m0150.COL11_TYPE <> "0" OrElse m0150.COL12_TYPE <> "0" OrElse m0150.COL13_TYPE <> "0" OrElse m0150.COL14_TYPE <> "0" OrElse m0150.COL15_TYPE <> "0" OrElse m0150.COL16_TYPE <> "0" OrElse m0150.COL17_TYPE <> "0" OrElse m0150.COL18_TYPE <> "0" OrElse m0150.COL19_TYPE <> "0" OrElse m0150.COL20_TYPE <> "0" OrElse m0150.COL21_TYPE <> "0" OrElse m0150.COL22_TYPE <> "0" OrElse m0150.COL23_TYPE <> "0" OrElse m0150.COL24_TYPE <> "0" OrElse m0150.COL25_TYPE <> "0" OrElse
			  m0150.COL26_TYPE <> "0" OrElse m0150.COL27_TYPE <> "0" OrElse m0150.COL28_TYPE <> "0" OrElse m0150.COL29_TYPE <> "0" OrElse m0150.COL30_TYPE <> "0" OrElse m0150.COL31_TYPE <> "0" OrElse m0150.COL32_TYPE <> "0" OrElse m0150.COL33_TYPE <> "0" OrElse m0150.COL34_TYPE <> "0" OrElse m0150.COL35_TYPE <> "0" OrElse m0150.COL36_TYPE <> "0" OrElse m0150.COL37_TYPE <> "0" OrElse m0150.COL38_TYPE <> "0" OrElse m0150.COL39_TYPE <> "0" OrElse m0150.COL40_TYPE <> "0" OrElse m0150.COL41_TYPE <> "0" OrElse m0150.COL42_TYPE <> "0" OrElse m0150.COL43_TYPE <> "0" OrElse m0150.COL44_TYPE <> "0" OrElse m0150.COL45_TYPE <> "0" OrElse m0150.COL46_TYPE <> "0" OrElse m0150.COL47_TYPE <> "0" OrElse m0150.COL48_TYPE <> "0" OrElse m0150.COL49_TYPE <> "0" OrElse m0150.COL50_TYPE <> "0" OrElse
			  m0150.COL01_HYOJ1 <> False OrElse m0150.COL02_HYOJ1 <> False OrElse m0150.COL03_HYOJ1 <> False OrElse m0150.COL04_HYOJ1 <> False OrElse m0150.COL05_HYOJ1 <> False OrElse m0150.COL06_HYOJ1 <> False OrElse m0150.COL07_HYOJ1 <> False OrElse m0150.COL08_HYOJ1 <> False OrElse m0150.COL09_HYOJ1 <> False OrElse m0150.COL10_HYOJ1 <> False OrElse m0150.COL11_HYOJ1 <> False OrElse m0150.COL12_HYOJ1 <> False OrElse m0150.COL13_HYOJ1 <> False OrElse m0150.COL14_HYOJ1 <> False OrElse m0150.COL15_HYOJ1 <> False OrElse m0150.COL16_HYOJ1 <> False OrElse m0150.COL17_HYOJ1 <> False OrElse m0150.COL18_HYOJ1 <> False OrElse m0150.COL19_HYOJ1 <> False OrElse m0150.COL20_HYOJ1 <> False OrElse m0150.COL21_HYOJ1 <> False OrElse m0150.COL22_HYOJ1 <> False OrElse m0150.COL23_HYOJ1 <> False OrElse m0150.COL24_HYOJ1 <> False OrElse m0150.COL25_HYOJ1 <> False OrElse
			  m0150.COL26_HYOJ1 <> False OrElse m0150.COL27_HYOJ1 <> False OrElse m0150.COL28_HYOJ1 <> False OrElse m0150.COL29_HYOJ1 <> False OrElse m0150.COL30_HYOJ1 <> False OrElse m0150.COL31_HYOJ1 <> False OrElse m0150.COL32_HYOJ1 <> False OrElse m0150.COL33_HYOJ1 <> False OrElse m0150.COL34_HYOJ1 <> False OrElse m0150.COL35_HYOJ1 <> False OrElse m0150.COL36_HYOJ1 <> False OrElse m0150.COL37_HYOJ1 <> False OrElse m0150.COL38_HYOJ1 <> False OrElse m0150.COL39_HYOJ1 <> False OrElse m0150.COL40_HYOJ1 <> False OrElse m0150.COL41_HYOJ1 <> False OrElse m0150.COL42_HYOJ1 <> False OrElse m0150.COL43_HYOJ1 <> False OrElse m0150.COL44_HYOJ1 <> False OrElse m0150.COL45_HYOJ1 <> False OrElse m0150.COL46_HYOJ1 <> False OrElse m0150.COL47_HYOJ1 <> False OrElse m0150.COL48_HYOJ1 <> False OrElse m0150.COL49_HYOJ1 <> False OrElse m0150.COL50_HYOJ1 <> False OrElse
			  m0150.COL01_HYOJNM1 IsNot Nothing OrElse m0150.COL02_HYOJNM1 IsNot Nothing OrElse m0150.COL03_HYOJNM1 IsNot Nothing OrElse m0150.COL04_HYOJNM1 IsNot Nothing OrElse m0150.COL05_HYOJNM1 IsNot Nothing OrElse m0150.COL06_HYOJNM1 IsNot Nothing OrElse m0150.COL07_HYOJNM1 IsNot Nothing OrElse m0150.COL08_HYOJNM1 IsNot Nothing OrElse m0150.COL09_HYOJNM1 IsNot Nothing OrElse m0150.COL10_HYOJNM1 IsNot Nothing OrElse m0150.COL11_HYOJNM1 IsNot Nothing OrElse m0150.COL12_HYOJNM1 IsNot Nothing OrElse m0150.COL13_HYOJNM1 IsNot Nothing OrElse m0150.COL14_HYOJNM1 IsNot Nothing OrElse m0150.COL15_HYOJNM1 IsNot Nothing OrElse m0150.COL16_HYOJNM1 IsNot Nothing OrElse m0150.COL17_HYOJNM1 IsNot Nothing OrElse m0150.COL18_HYOJNM1 IsNot Nothing OrElse m0150.COL19_HYOJNM1 IsNot Nothing OrElse m0150.COL20_HYOJNM1 IsNot Nothing OrElse m0150.COL21_HYOJNM1 IsNot Nothing OrElse m0150.COL22_HYOJNM1 IsNot Nothing OrElse m0150.COL23_HYOJNM1 IsNot Nothing OrElse m0150.COL24_HYOJNM1 IsNot Nothing OrElse m0150.COL25_HYOJNM1 IsNot Nothing OrElse
			  m0150.COL26_HYOJNM1 IsNot Nothing OrElse m0150.COL27_HYOJNM1 IsNot Nothing OrElse m0150.COL28_HYOJNM1 IsNot Nothing OrElse m0150.COL29_HYOJNM1 IsNot Nothing OrElse m0150.COL30_HYOJNM1 IsNot Nothing OrElse m0150.COL31_HYOJNM1 IsNot Nothing OrElse m0150.COL32_HYOJNM1 IsNot Nothing OrElse m0150.COL33_HYOJNM1 IsNot Nothing OrElse m0150.COL34_HYOJNM1 IsNot Nothing OrElse m0150.COL35_HYOJNM1 IsNot Nothing OrElse m0150.COL36_HYOJNM1 IsNot Nothing OrElse m0150.COL37_HYOJNM1 IsNot Nothing OrElse m0150.COL38_HYOJNM1 IsNot Nothing OrElse m0150.COL39_HYOJNM1 IsNot Nothing OrElse m0150.COL40_HYOJNM1 IsNot Nothing OrElse m0150.COL41_HYOJNM1 IsNot Nothing OrElse m0150.COL42_HYOJNM1 IsNot Nothing OrElse m0150.COL43_HYOJNM1 IsNot Nothing OrElse m0150.COL44_HYOJNM1 IsNot Nothing OrElse m0150.COL45_HYOJNM1 IsNot Nothing OrElse m0150.COL46_HYOJNM1 IsNot Nothing OrElse m0150.COL47_HYOJNM1 IsNot Nothing OrElse m0150.COL48_HYOJNM1 IsNot Nothing OrElse m0150.COL49_HYOJNM1 IsNot Nothing OrElse m0150.COL50_HYOJNM1 IsNot Nothing OrElse
			  m0150.COL01_HYOJJN1 IsNot Nothing OrElse m0150.COL02_HYOJJN1 IsNot Nothing OrElse m0150.COL03_HYOJJN1 IsNot Nothing OrElse m0150.COL04_HYOJJN1 IsNot Nothing OrElse m0150.COL05_HYOJJN1 IsNot Nothing OrElse m0150.COL06_HYOJJN1 IsNot Nothing OrElse m0150.COL07_HYOJJN1 IsNot Nothing OrElse m0150.COL08_HYOJJN1 IsNot Nothing OrElse m0150.COL09_HYOJJN1 IsNot Nothing OrElse m0150.COL10_HYOJJN1 IsNot Nothing OrElse m0150.COL11_HYOJJN1 IsNot Nothing OrElse m0150.COL12_HYOJJN1 IsNot Nothing OrElse m0150.COL13_HYOJJN1 IsNot Nothing OrElse m0150.COL14_HYOJJN1 IsNot Nothing OrElse m0150.COL15_HYOJJN1 IsNot Nothing OrElse m0150.COL16_HYOJJN1 IsNot Nothing OrElse m0150.COL17_HYOJJN1 IsNot Nothing OrElse m0150.COL18_HYOJJN1 IsNot Nothing OrElse m0150.COL19_HYOJJN1 IsNot Nothing OrElse m0150.COL20_HYOJJN1 IsNot Nothing OrElse m0150.COL21_HYOJJN1 IsNot Nothing OrElse m0150.COL22_HYOJJN1 IsNot Nothing OrElse m0150.COL23_HYOJJN1 IsNot Nothing OrElse m0150.COL24_HYOJJN1 IsNot Nothing OrElse m0150.COL25_HYOJJN1 IsNot Nothing OrElse
			  m0150.COL26_HYOJJN1 IsNot Nothing OrElse m0150.COL27_HYOJJN1 IsNot Nothing OrElse m0150.COL28_HYOJJN1 IsNot Nothing OrElse m0150.COL29_HYOJJN1 IsNot Nothing OrElse m0150.COL30_HYOJJN1 IsNot Nothing OrElse m0150.COL31_HYOJJN1 IsNot Nothing OrElse m0150.COL32_HYOJJN1 IsNot Nothing OrElse m0150.COL33_HYOJJN1 IsNot Nothing OrElse m0150.COL34_HYOJJN1 IsNot Nothing OrElse m0150.COL35_HYOJJN1 IsNot Nothing OrElse m0150.COL36_HYOJJN1 IsNot Nothing OrElse m0150.COL37_HYOJJN1 IsNot Nothing OrElse m0150.COL38_HYOJJN1 IsNot Nothing OrElse m0150.COL39_HYOJJN1 IsNot Nothing OrElse m0150.COL40_HYOJJN1 IsNot Nothing OrElse m0150.COL41_HYOJJN1 IsNot Nothing OrElse m0150.COL42_HYOJJN1 IsNot Nothing OrElse m0150.COL43_HYOJJN1 IsNot Nothing OrElse m0150.COL44_HYOJJN1 IsNot Nothing OrElse m0150.COL45_HYOJJN1 IsNot Nothing OrElse m0150.COL46_HYOJJN1 IsNot Nothing OrElse m0150.COL47_HYOJJN1 IsNot Nothing OrElse m0150.COL48_HYOJJN1 IsNot Nothing OrElse m0150.COL49_HYOJJN1 IsNot Nothing OrElse m0150.COL50_HYOJJN1 IsNot Nothing OrElse
			  m0150.COL01_HYOJ2 <> False OrElse m0150.COL02_HYOJ2 <> False OrElse m0150.COL03_HYOJ2 <> False OrElse m0150.COL04_HYOJ2 <> False OrElse m0150.COL05_HYOJ2 <> False OrElse m0150.COL06_HYOJ2 <> False OrElse m0150.COL07_HYOJ2 <> False OrElse m0150.COL08_HYOJ2 <> False OrElse m0150.COL09_HYOJ2 <> False OrElse m0150.COL10_HYOJ2 <> False OrElse m0150.COL11_HYOJ2 <> False OrElse m0150.COL12_HYOJ2 <> False OrElse m0150.COL13_HYOJ2 <> False OrElse m0150.COL14_HYOJ2 <> False OrElse m0150.COL15_HYOJ2 <> False OrElse m0150.COL16_HYOJ2 <> False OrElse m0150.COL17_HYOJ2 <> False OrElse m0150.COL18_HYOJ2 <> False OrElse m0150.COL19_HYOJ2 <> False OrElse m0150.COL20_HYOJ2 <> False OrElse m0150.COL21_HYOJ2 <> False OrElse m0150.COL22_HYOJ2 <> False OrElse m0150.COL23_HYOJ2 <> False OrElse m0150.COL24_HYOJ2 <> False OrElse m0150.COL25_HYOJ2 <> False OrElse
			  m0150.COL26_HYOJ2 <> False OrElse m0150.COL27_HYOJ2 <> False OrElse m0150.COL28_HYOJ2 <> False OrElse m0150.COL29_HYOJ2 <> False OrElse m0150.COL30_HYOJ2 <> False OrElse m0150.COL31_HYOJ2 <> False OrElse m0150.COL32_HYOJ2 <> False OrElse m0150.COL33_HYOJ2 <> False OrElse m0150.COL34_HYOJ2 <> False OrElse m0150.COL35_HYOJ2 <> False OrElse m0150.COL36_HYOJ2 <> False OrElse m0150.COL37_HYOJ2 <> False OrElse m0150.COL38_HYOJ2 <> False OrElse m0150.COL39_HYOJ2 <> False OrElse m0150.COL40_HYOJ2 <> False OrElse m0150.COL41_HYOJ2 <> False OrElse m0150.COL42_HYOJ2 <> False OrElse m0150.COL43_HYOJ2 <> False OrElse m0150.COL44_HYOJ2 <> False OrElse m0150.COL45_HYOJ2 <> False OrElse m0150.COL46_HYOJ2 <> False OrElse m0150.COL47_HYOJ2 <> False OrElse m0150.COL48_HYOJ2 <> False OrElse m0150.COL49_HYOJ2 <> False OrElse m0150.COL50_HYOJ2 <> False OrElse
			  m0150.COL01_HYOJNM2 IsNot Nothing OrElse m0150.COL02_HYOJNM2 IsNot Nothing OrElse m0150.COL03_HYOJNM2 IsNot Nothing OrElse m0150.COL04_HYOJNM2 IsNot Nothing OrElse m0150.COL05_HYOJNM2 IsNot Nothing OrElse m0150.COL06_HYOJNM2 IsNot Nothing OrElse m0150.COL07_HYOJNM2 IsNot Nothing OrElse m0150.COL08_HYOJNM2 IsNot Nothing OrElse m0150.COL09_HYOJNM2 IsNot Nothing OrElse m0150.COL10_HYOJNM2 IsNot Nothing OrElse m0150.COL11_HYOJNM2 IsNot Nothing OrElse m0150.COL12_HYOJNM2 IsNot Nothing OrElse m0150.COL13_HYOJNM2 IsNot Nothing OrElse m0150.COL14_HYOJNM2 IsNot Nothing OrElse m0150.COL15_HYOJNM2 IsNot Nothing OrElse m0150.COL16_HYOJNM2 IsNot Nothing OrElse m0150.COL17_HYOJNM2 IsNot Nothing OrElse m0150.COL18_HYOJNM2 IsNot Nothing OrElse m0150.COL19_HYOJNM2 IsNot Nothing OrElse m0150.COL20_HYOJNM2 IsNot Nothing OrElse m0150.COL21_HYOJNM2 IsNot Nothing OrElse m0150.COL22_HYOJNM2 IsNot Nothing OrElse m0150.COL23_HYOJNM2 IsNot Nothing OrElse m0150.COL24_HYOJNM2 IsNot Nothing OrElse m0150.COL25_HYOJNM2 IsNot Nothing OrElse
			  m0150.COL26_HYOJNM2 IsNot Nothing OrElse m0150.COL27_HYOJNM2 IsNot Nothing OrElse m0150.COL28_HYOJNM2 IsNot Nothing OrElse m0150.COL29_HYOJNM2 IsNot Nothing OrElse m0150.COL30_HYOJNM2 IsNot Nothing OrElse m0150.COL31_HYOJNM2 IsNot Nothing OrElse m0150.COL32_HYOJNM2 IsNot Nothing OrElse m0150.COL33_HYOJNM2 IsNot Nothing OrElse m0150.COL34_HYOJNM2 IsNot Nothing OrElse m0150.COL35_HYOJNM2 IsNot Nothing OrElse m0150.COL36_HYOJNM2 IsNot Nothing OrElse m0150.COL37_HYOJNM2 IsNot Nothing OrElse m0150.COL38_HYOJNM2 IsNot Nothing OrElse m0150.COL39_HYOJNM2 IsNot Nothing OrElse m0150.COL40_HYOJNM2 IsNot Nothing OrElse m0150.COL41_HYOJNM2 IsNot Nothing OrElse m0150.COL42_HYOJNM2 IsNot Nothing OrElse m0150.COL43_HYOJNM2 IsNot Nothing OrElse m0150.COL44_HYOJNM2 IsNot Nothing OrElse m0150.COL45_HYOJNM2 IsNot Nothing OrElse m0150.COL46_HYOJNM2 IsNot Nothing OrElse m0150.COL47_HYOJNM2 IsNot Nothing OrElse m0150.COL48_HYOJNM2 IsNot Nothing OrElse m0150.COL49_HYOJNM2 IsNot Nothing OrElse m0150.COL50_HYOJNM2 IsNot Nothing OrElse
			  m0150.COL01_HYOJJN2 IsNot Nothing OrElse m0150.COL02_HYOJJN2 IsNot Nothing OrElse m0150.COL03_HYOJJN2 IsNot Nothing OrElse m0150.COL04_HYOJJN2 IsNot Nothing OrElse m0150.COL05_HYOJJN2 IsNot Nothing OrElse m0150.COL06_HYOJJN2 IsNot Nothing OrElse m0150.COL07_HYOJJN2 IsNot Nothing OrElse m0150.COL08_HYOJJN2 IsNot Nothing OrElse m0150.COL09_HYOJJN2 IsNot Nothing OrElse m0150.COL10_HYOJJN2 IsNot Nothing OrElse m0150.COL11_HYOJJN2 IsNot Nothing OrElse m0150.COL12_HYOJJN2 IsNot Nothing OrElse m0150.COL13_HYOJJN2 IsNot Nothing OrElse m0150.COL14_HYOJJN2 IsNot Nothing OrElse m0150.COL15_HYOJJN2 IsNot Nothing OrElse m0150.COL16_HYOJJN2 IsNot Nothing OrElse m0150.COL17_HYOJJN2 IsNot Nothing OrElse m0150.COL18_HYOJJN2 IsNot Nothing OrElse m0150.COL19_HYOJJN2 IsNot Nothing OrElse m0150.COL20_HYOJJN2 IsNot Nothing OrElse m0150.COL21_HYOJJN2 IsNot Nothing OrElse m0150.COL22_HYOJJN2 IsNot Nothing OrElse m0150.COL23_HYOJJN2 IsNot Nothing OrElse m0150.COL24_HYOJJN2 IsNot Nothing OrElse m0150.COL25_HYOJJN2 IsNot Nothing OrElse
			  m0150.COL26_HYOJJN2 IsNot Nothing OrElse m0150.COL27_HYOJJN2 IsNot Nothing OrElse m0150.COL28_HYOJJN2 IsNot Nothing OrElse m0150.COL29_HYOJJN2 IsNot Nothing OrElse m0150.COL30_HYOJJN2 IsNot Nothing OrElse m0150.COL31_HYOJJN2 IsNot Nothing OrElse m0150.COL32_HYOJJN2 IsNot Nothing OrElse m0150.COL33_HYOJJN2 IsNot Nothing OrElse m0150.COL34_HYOJJN2 IsNot Nothing OrElse m0150.COL35_HYOJJN2 IsNot Nothing OrElse m0150.COL36_HYOJJN2 IsNot Nothing OrElse m0150.COL37_HYOJJN2 IsNot Nothing OrElse m0150.COL38_HYOJJN2 IsNot Nothing OrElse m0150.COL39_HYOJJN2 IsNot Nothing OrElse m0150.COL40_HYOJJN2 IsNot Nothing OrElse m0150.COL41_HYOJJN2 IsNot Nothing OrElse m0150.COL42_HYOJJN2 IsNot Nothing OrElse m0150.COL43_HYOJJN2 IsNot Nothing OrElse m0150.COL44_HYOJJN2 IsNot Nothing OrElse m0150.COL45_HYOJJN2 IsNot Nothing OrElse m0150.COL46_HYOJJN2 IsNot Nothing OrElse m0150.COL47_HYOJJN2 IsNot Nothing OrElse m0150.COL48_HYOJJN2 IsNot Nothing OrElse m0150.COL49_HYOJJN2 IsNot Nothing OrElse m0150.COL50_HYOJJN2 IsNot Nothing Then

				isChanged = True
			End If

			Return isChanged

		End Function

		Function CheckSubCatExistsInD0010(sportcat As String, sportsubcat As String) As JsonResult
			Dim strCatPresent As String = ""
			If sportcat IsNot Nothing AndAlso sportsubcat IsNot Nothing Then
				Dim lstD0010 As List(Of D0010)
				Dim lstD0070 As List(Of D0070)
				Dim lstD0010_COUNT As Integer = 0
				Dim lstD0070_COUNT As Integer = 0
				lstD0010 = (From d1 In db.D0010
							Where d1.SPORTCATCD = sportcat AndAlso d1.SPORTFLG = "1"
							Select d1).ToList()

				If lstD0010 IsNot Nothing AndAlso lstD0010.Count > 0 AndAlso sportsubcat <> "" Then
					lstD0010_COUNT = lstD0010.Where(Function(d1) (d1.SPORTSUBCATCD = sportsubcat)).Count
				Else
					lstD0010_COUNT = lstD0010.Count
				End If

				strCatPresent = If(lstD0010_COUNT > 0, "D0010", "")

				If strCatPresent = "" Then

					lstD0070 = (From d1 In db.D0070 Where d1.SPORTCATCD = sportcat Select d1).ToList()

					If lstD0070 IsNot Nothing AndAlso lstD0070.Count > 0 AndAlso sportsubcat <> "" Then
						lstD0070_COUNT = lstD0070.Where(Function(d1) (d1.SPORTSUBCATCD = sportsubcat)).Count
					Else
						lstD0070_COUNT = lstD0070.Count
					End If

					strCatPresent = If(lstD0070_COUNT > 0, "D0070", "")
				End If

				Return Json(New With {.success = True, .catExists = strCatPresent})
			Else
				Return Json(New With {.success = True, .catExists = strCatPresent})
			End If

		End Function

	End Class
End Namespace