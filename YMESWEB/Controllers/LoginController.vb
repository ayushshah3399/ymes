Imports System.Data.Entity
Imports System.Web.Mvc
Imports Npgsql
Imports System.Threading
Imports System.Globalization
Imports MES_WEB.My.Resources

Namespace Controllers
	Public Class LoginController
		Inherits Controller

		'Create instance of model
		Dim db As New Model1
		Dim Appid As String = My.Settings.Item("ApplicationID")
		Dim CompCd As String = My.Settings.Item("CompCd")

		'GET: Login
		Function Index() As ActionResult

			'Get Webappnm name Form S0010
			Dim s0010 = (From t In db.s0010 Where t.appid = Appid AndAlso t.compcd = CompCd).ToList
			If s0010.Count > 0 Then
				Session("webappnm") = s0010(0).webappnm
				Session("StrPlantCode") = s0010(0).plant_code
			Else
				'Will Display error that No data or no plant Code
				Return RedirectToAction("Index", "Comm_Error")
			End If

			'Assemby Version Set
			'This Is Published File Version To Set The Version On Login And Menu Screen
			Dim Assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
			Dim FileVersioninfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location)
			Dim AssembliVersion = FileVersioninfo.FileVersion
			Session("AssembliVersion") = AssembliVersion

			'If user didnt logout then it will acotomatically login
			Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

			'Check If User Already Login Or not
			Dim sy010 = (From t In db.sy010 Where t.ipaddress = Ipaddress AndAlso t.logouttime Is Nothing).ToList

			If sy010.Count > 0 Then

				'Set Laungage to Culture Info
				'Set to Session variable also 
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sy010(0).lcid)
				Thread.CurrentThread.CurrentUICulture = New CultureInfo(sy010(0).lcid)
				Session("language") = sy010(0).lcid
				'Set To Cookie Variable
				Response.Cookies.Add(New HttpCookie("language") With {.Value = sy010(0).lcid})

				'Get Date From S014
				'Set Format To Session variable
				Dim laungage As String = Session("language")
				Dim objs0140_Format = (From t In db.s0140 Where t.lcid = laungage AndAlso t.appid = "TeLAS").ToList
				Session("language_Frmt") = "{0:" & objs0140_Format(0).datefmt & "}"
                Session("DateFormat_Original") = objs0140_Format(0).datefmt
                'Add To CultureInfo For Date Format
                Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern = objs0140_Format(0).datefmt
				Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongDatePattern = objs0140_Format(0).datefmt
				Thread.CurrentThread.CurrentUICulture.DateTimeFormat.DateSeparator = objs0140_Format(0).dateseparator
				'Add To Cookie And Add to Global.asax File
				Response.Cookies.Add(New HttpCookie("Cookie_datefmt") With {.Value = objs0140_Format(0).datefmt})
				Response.Cookies.Add(New HttpCookie("Cookie_dateseparator") With {.Value = objs0140_Format(0).dateseparator})

				'Return
				Session("LoginUserid") = sy010(0).userid
				Session("LoginUsernm") = sy010(0).usernm
				Return RedirectToAction("Index", "Menu")

				'When logout and when 1st time login
			Else

				'Get laungage from S0140 
				Dim objs0140 = db.s0140.OrderBy(Function(m) m.showorder).ToList()
				Dim Clones0140 = objs0140

				'Set native name
				For i As Integer = 0 To Clones0140.Count - 1
					Dim nativname As String
					nativname = CultureInfo.CreateSpecificCulture(Clones0140(i).lcid).NativeName
					Clones0140(i).lcnm = nativname
				Next

				'After Logout No Session Is there So Get Data From Cookie
				If Request.Cookies("language") IsNot Nothing Then
					Session("language") = Request.Cookies("language").Value
				End If

				'It Is Checking When Back Button Or Logout Button Is Pressed.
				If Session("language") Is Nothing Then
					'This is for dropdown menu.
					ViewBag.language = New SelectList(Clones0140, "lcid", "lcnm", "en-US")
					Session("language") = "en-US"
				Else
					'This is for dropdown menu.
					'When user lougout and want to set same laungage from cookie
					ViewBag.language = New SelectList(Clones0140, "lcid", "lcnm", Session("language"))
				End If

				'From laungage Get Format and set to Session Variable
				'No need to Set CultureInfo. It will set from globax.asax
				Dim laungage As String = Session("language")
				Dim objs0140_Format = (From t In db.s0140 Where t.lcid = laungage AndAlso t.appid = "TeLAS").ToList
				Session("language_Frmt") = "{0:" & objs0140_Format(0).datefmt & "}"
                Session("DateFormat_Original") = objs0140_Format(0).datefmt

                'Return
                ViewData!ID = LangResources.L1_01_Login
				Return View()

			End If

		End Function

		'Post: Login
		<HttpPost>
		Function Index(ByVal LoinId As String, ByVal Password As String, ByVal btnLogin As String, ByVal language As String) As ActionResult

			'Laungage Change Combox click
			If btnLogin = "1" Then

				Dim objs0140 = db.s0140.OrderBy(Function(m) m.showorder).ToList()
				Dim Clones0140 = objs0140
				'This is for set native name of laungage
				For i As Integer = 0 To Clones0140.Count - 1
					Dim nativname As String
					nativname = CultureInfo.CreateSpecificCulture(Clones0140(i).lcid).NativeName
					Clones0140(i).lcnm = nativname
				Next

				'This is for dropdown menu.
				ViewBag.language = New SelectList(Clones0140, "lcid", "lcnm", language)
				ViewData!ID = LangResources.L1_01_Login

				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language)
				Thread.CurrentThread.CurrentUICulture = New CultureInfo(language)
				'It Will Save Language In Session variable.
				Session("language") = language
				Dim cookie As HttpCookie = New HttpCookie("language")
				cookie.Value = language
				Response.Cookies.Add(cookie)

				'Get Date format from laungage 
				Dim Lang_For_Format As String = Session("language")
				Dim objs0140_Format = (From t In db.s0140 Where t.lcid = Lang_For_Format AndAlso t.appid = "TeLAS").ToList
				Session("language_Frmt") = "{0:" & objs0140_Format(0).datefmt & "}"
                Session("DateFormat_Original") = objs0140_Format(0).datefmt
                'Set Cultureinfo
                Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern = objs0140_Format(0).datefmt
				Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongDatePattern = objs0140_Format(0).datefmt
				Thread.CurrentThread.CurrentUICulture.DateTimeFormat.DateSeparator = objs0140_Format(0).dateseparator
				'Add To Cookie And Add to Global.asax File
				Response.Cookies.Add(New HttpCookie("Cookie_datefmt") With {.Value = objs0140_Format(0).datefmt})
				Response.Cookies.Add(New HttpCookie("Cookie_dateseparator") With {.Value = objs0140_Format(0).dateseparator})

				'Return
				ViewData!ID = LangResources.L1_01_Login
				Return View()

				'When click on login button
			Else

				' Check Userid regiser Or not?
				Dim s0050 = (From t In db.s0050 Where t.userid = LoinId).ToList

				'ログイン
				If s0050.Count > 0 Then

					'Encrption Of password and Check Registered Or not
					If s0050(0).pass = db.Encryptsyspass(Password) Then

						'If user want to login and want to access menu form
						Session("LoginUserid") = s0050(0).userid
						Session("LoginPass") = s0050(0).pass
						Session("LoginUsernm") = s0050(0).usernm
						ViewData!LoginUsernm = s0050(0).usernm

						Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
						Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
						Dim Tras As DbContextTransaction = db.Database.BeginTransaction

						Try
							Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")
							Dim sy010 = (From t In db.sy010 Where t.ipaddress = Ipaddress AndAlso t.logouttime Is Nothing).ToList

							'If New User Or loginTime
							If sy010.Count = 0 Then
								Dim InsertNewRecord As New sy010
								InsertNewRecord.ipaddress = Ipaddress
								InsertNewRecord.logintime = DateTime.Now
								InsertNewRecord.sessionid = Session.SessionID
								InsertNewRecord.userid = s0050(0).userid
								InsertNewRecord.usernm = s0050(0).usernm
								InsertNewRecord.lcid = language
								'Call Function for Set Client info
								Dim cnt = db.Database.ExecuteSqlCommand("SELECT TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")
								db.sy010.Add(InsertNewRecord)
								db.Configuration.ValidateOnSaveEnabled = False
								db.SaveChanges()
								db.Configuration.ValidateOnSaveEnabled = True
								Tras.Commit()
							End If

						Catch ex As Exception
							Tras.Rollback()

                            '倉庫棚卸中のため、登録できません。
                            '工程棚卸中のため、登録できません。
                            '完成・不良倉庫棚卸中のため、登録できません。
                            '棚卸中のため、登録できません。
                            Dim ObjTransactionLockStockController As New TransactionLockStockController
                            Dim ObjException = ObjTransactionLockStockController.Check_Transaction_lock_Exception(ex)

                            If ObjException IsNot Nothing Then
                                Return ObjException
                            End If

                            'Get stack trace for the exception with source file information
                            Dim st = New StackTrace(ex, True)
							'Get the top stack frame
							Dim frame = st.GetFrame(st.GetFrames.Count - 1)
							'Get the line number from the stack frame
							Dim line = frame.GetFileLineNumber()

							TempData("Line") = line
							Throw ex
						End Try

						'Return
						Return RedirectToAction("Index", "Menu")

						'If password Invalid Then Display error message
					Else

						TempData("LoinId") = LoinId
						TempData("LoginErrMsg") = LangResources.MSG_L1_21_InvalidUserPsw

						If Request.UrlReferrer IsNot Nothing Then
							If Not Request.UrlReferrer.LocalPath = "/" AndAlso Not Request.UrlReferrer.LocalPath.Contains("Login") Then
								Return Redirect(Request.UrlReferrer.ToString)
							End If
						End If

					End If

					'If Userid is not registerd then display error message
				Else

					TempData("LoinId") = LoinId
					TempData("LoginErrMsg") = LangResources.MSG_L1_21_InvalidUserPsw

					If Request.UrlReferrer IsNot Nothing Then
						If Not Request.UrlReferrer.LocalPath = "/" AndAlso Not Request.UrlReferrer.LocalPath.Contains("Login") Then
							Return Redirect(Request.UrlReferrer.ToString)
						End If
					End If

				End If

				'Set DropDownmenu
				Dim objs0140 = db.s0140.OrderBy(Function(m) m.showorder).ToList()
				Dim Clones0140 = objs0140
				'Set Native Name for laungage
				For i As Integer = 0 To Clones0140.Count - 1
					Dim nativname As String
					nativname = CultureInfo.CreateSpecificCulture(Clones0140(i).lcid).NativeName
					Clones0140(i).lcnm = nativname
				Next

				'Return
				'This is for dropdown menu.
				ViewBag.language = New SelectList(Clones0140, "lcid", "lcnm", language)
				ViewData!ID = LangResources.L1_01_Login
				Return View()

			End If

		End Function

		'When Click On Logout Button
		Function Logout() As ActionResult

			'This is for Clientinfo
			Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
			Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
			Dim Tras As DbContextTransaction = db.Database.BeginTransaction

			Try
				Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")
				Dim sy010 = (From t In db.sy010 Where t.ipaddress = Ipaddress AndAlso t.logouttime Is Nothing).ToList
				If sy010.Count > 0 Then

					'Add logout time 
					sy010(0).logouttime = DateTime.Now
					'Set Clientinfo
					Dim cnt = db.Database.ExecuteSqlCommand("SELECT TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")
					db.Configuration.ValidateOnSaveEnabled = False
					db.SaveChanges()
					db.Configuration.ValidateOnSaveEnabled = True
					Tras.Commit()

					'Clear Session
					Session.Clear()

				End If

			Catch ex As Exception
				Tras.Rollback()

                '倉庫棚卸中のため、登録できません。
                '工程棚卸中のため、登録できません。
                '完成・不良倉庫棚卸中のため、登録できません。
                '棚卸中のため、登録できません。
                Dim ObjTransactionLockStockController As New TransactionLockStockController
                Dim ObjException = ObjTransactionLockStockController.Check_Transaction_lock_Exception(ex)

                If ObjException IsNot Nothing Then
                    Return ObjException
                End If

                'Get stack trace for the exception with source file information
                Dim st = New StackTrace(ex, True)
				'Get the top stack frame
				Dim frame = st.GetFrame(0)
				'Get the line number from the stack frame
				Dim line = frame.GetFileLineNumber()

				TempData("Line") = line
				Throw ex
			End Try

			'Return
			Return RedirectToAction("Index", "Login")

		End Function

	End Class
End Namespace