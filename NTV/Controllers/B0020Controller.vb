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
Imports System.IO
Imports System.Net.Mail
Imports System.ComponentModel.DataAnnotations

Namespace Controllers
	Public Class B0020Controller
		Inherits System.Web.Mvc.Controller

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

		' GET: D0010
		Function Index(ByVal Gyost As String, ByVal Gyoend As String,
				ByVal PtnflgNo As System.Nullable(Of Boolean), ByVal Ptn1 As System.Nullable(Of Boolean),
				ByVal Ptn2 As System.Nullable(Of Boolean), ByVal Ptn3 As System.Nullable(Of Boolean),
				ByVal Ptn4 As System.Nullable(Of Boolean), ByVal Ptn5 As System.Nullable(Of Boolean),
				ByVal Ptn6 As System.Nullable(Of Boolean), ByVal Ptn7 As System.Nullable(Of Boolean),
				ByVal Kskjknst As String, ByVal Kskjkned As String,
				ByVal CATCD As String, ByVal ANAID As String,
				ByVal PtnAnaflgNo As System.Nullable(Of Boolean), ByVal PtnAna1 As System.Nullable(Of Boolean), ByVal PtnAna2 As System.Nullable(Of Boolean),
				ByVal Banguminm As String, ByVal Naiyo As String, ByVal Basyo As String, ByVal Bangumitanto As String, ByVal Bangumirenrk As String, ByVal OAJKNST As String, ByVal OAJKNED As String, ByVal Biko As String,
				ByVal confirmmsg As String, ByVal SPORTCATCD As String, ByVal SPORTSUBCATCD As String, ByVal SAIJKNST As String, ByVal SAIJKNED As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			For intIdx As Integer = Session.Keys.Count - 1 To 0 Step -1
				Dim strKey As String = Session.Keys(intIdx).ToString
				If strKey.Contains("B0020") OrElse strKey.Contains("D0010") Then
					Session.RemoveAt(intIdx)
				End If
			Next

			Session("Gyost") = Gyost
			Session("Gyoend") = Gyoend
			Session("PtnflgNo") = PtnflgNo
			Session("Ptn1") = Ptn1
			Session("Ptn2") = Ptn2
			Session("Ptn3") = Ptn3
			Session("Ptn4") = Ptn4
			Session("Ptn5") = Ptn5
			Session("Ptn6") = Ptn6
			Session("Ptn7") = Ptn7
			Session("Kskjknst") = Kskjknst
			Session("Kskjkned") = Kskjkned
			Session("CATCD") = CATCD
			Session("ANAID") = ANAID
			Session("PtnAnaflgNo") = PtnAnaflgNo
			Session("PtnAna1") = PtnAna1
			Session("PtnAna2") = PtnAna2
			Session("Banguminm") = Banguminm
			Session("Naiyo") = Naiyo
			Session("Basyo") = Basyo
			Session("Bangumitanto") = Bangumitanto
			Session("Bangumirenrk") = Bangumirenrk
			Session("OAJKNST") = OAJKNST
			Session("OAJKNED") = OAJKNED
			Session("SPORTSUBCATCD") = SPORTSUBCATCD
			Session("SPORTCATCD") = SPORTCATCD
			Session("SAIJKNST") = SAIJKNST
			Session("SAIJKNED") = SAIJKNED
			Session("Biko") = Biko

			Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim item As New M0020
			item.CATCD = "0"
			item.CATNM = ""
			lstCATCD.Insert(0, item)
			ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM")

			Dim lstSportCatNm = db.M0130.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim blank_entry As New M0130
			blank_entry.SPORTCATCD = 0
			blank_entry.SPORTCATNM = ""
			lstSportCatNm.Insert(0, blank_entry)
			ViewBag.SPORTCATCD = New SelectList(lstSportCatNm, "SPORTCATCD", "SPORTCATNM", "")

			Dim lstSportSubCatNm = (From m1 In db.M0140
									Join m2 In db.M0150 On m1.SPORTSUBCATCD Equals m2.SPORTSUBCATCD
									Join m3 In db.M0130 On m2.SPORTCATCD Equals m3.SPORTCATCD
									Where m2.SPORTCATCD = SPORTCATCD And m1.HYOJ = True
									Order By m1.HYOJJN
									Select m1).ToList
			Dim blank_entry_subCatNm As New M0140
			blank_entry_subCatNm.SPORTSUBCATCD = 0
			blank_entry_subCatNm.SPORTSUBCATNM = ""
			lstSportSubCatNm.Insert(0, blank_entry_subCatNm)
			ViewBag.SPORTSUBCATCD = New SelectList(lstSportSubCatNm, "SPORTSUBCATCD", "SPORTSUBCATNM", "")

			Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			Dim item1 As New M0010
			item1.USERID = "0"
			item1.USERNM = ""
			lstUSERID.Insert(0, item1)
			ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM")

			ViewBag.ANAID = New SelectList(lstUSERID, "USERID", "USERNM")

			Dim lstbangumi = db.M0030.OrderBy(Function(f) f.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			'画面開く時、検索しない
			If Gyost Is Nothing AndAlso Gyoend Is Nothing AndAlso Kskjknst Is Nothing AndAlso Kskjkned Is Nothing AndAlso CATCD Is Nothing AndAlso
				Banguminm Is Nothing AndAlso Naiyo Is Nothing AndAlso Basyo Is Nothing AndAlso Bangumitanto Is Nothing AndAlso Bangumirenrk Is Nothing Then
				'処理中ラベルのため色を渡す、画面開いて検索していない時、メッセージ表示したくないので、白にする
				ViewData("color") = "white"
				ViewData("disabled") = ""
				Return View()
			End If
			ViewData("color") = "orange"
			ViewData("disabled") = "disabled"

			Dim d0010 = db.D0010.Where(Function(f) f.OYAGYOMFLG = 0).Include(Function(d) d.M0020).Include(Function(d) d.M0090).Include(Function(d) d.M0130).Include(Function(d) d.M0140)

            'ASI [2019 Dec 17]to display sport category data
            'd0010 = d0010.Where(Function(d) d.PGYOMNO Is Nothing OrElse d.SPORTFLG = True)
            'd0010 = d0010.Where(Function(d1) db.D0010.Any(Function(d2) d2.PGYOMNO = d1.GYOMNO AndAlso d2.SPORTFLG = True) OrElse d1.PGYOMNO Is Nothing)
            d0010 = d0010.Where(Function(d1) (d1.SPORTFLG = True AndAlso d1.SPORT_OYAFLG = True) OrElse d1.PGYOMNO Is Nothing)

            If Not String.IsNullOrEmpty(Gyost) Then
				If String.IsNullOrEmpty(Gyoend) Then
					Gyoend = Gyost
				End If
				d0010 = d0010.Where(Function(m) (Gyost) <= m.GYOMYMDED AndAlso (Gyoend) >= m.GYOMYMD)
			End If

			If String.IsNullOrEmpty(Kskjknst) = False Then
				If Kskjknst.Contains(":") = False Then
					If Kskjknst.Length > 2 Then
						Dim strMM As String = Kskjknst.Substring(Kskjknst.Length - 2, 2)
						Dim strHH As String = Kskjknst.Remove(Kskjknst.Length - 2, 2)
						Kskjknst = strHH.PadLeft(2, "0") & strMM
					Else
						Kskjknst = Kskjknst.PadLeft(2, "0") & "00"
					End If
				Else
					Kskjknst = Kskjknst.Replace(":", "").PadLeft(4, "0")
				End If
			End If

			If String.IsNullOrEmpty(Kskjkned) = False Then
				If Kskjkned.Contains(":") = False Then
					If Kskjkned.Length > 2 Then
						Dim strMM As String = Kskjkned.Substring(Kskjkned.Length - 2, 2)
						Dim strHH As String = Kskjkned.Remove(Kskjkned.Length - 2, 2)
						Kskjkned = strHH.PadLeft(2, "0") & strMM
					Else
						Kskjkned = Kskjkned.PadLeft(2, "0") & "00"
					End If
				Else
					Kskjkned = Kskjkned.Replace(":", "").PadLeft(4, "0")
				End If
			End If


			If Not String.IsNullOrEmpty(Kskjknst) Then
				If String.IsNullOrEmpty(Kskjkned) Then
					Kskjkned = Kskjknst
				End If
				d0010 = d0010.Where(Function(m) (Kskjknst) <= m.KSKJKNED AndAlso (Kskjkned) >= m.KSKJKNST)
			End If

			If Not String.IsNullOrEmpty(CATCD) AndAlso CATCD <> "0" Then
				d0010 = d0010.Where(Function(m) m.CATCD = (CATCD))
			End If

			If Not String.IsNullOrEmpty(Banguminm) Then
				d0010 = d0010.Where(Function(m) m.BANGUMINM.Contains(Banguminm))
			End If

			If Not String.IsNullOrEmpty(Naiyo) Then
				d0010 = d0010.Where(Function(m) m.NAIYO.Contains(Naiyo))
			End If

			If Not String.IsNullOrEmpty(Basyo) Then
				d0010 = d0010.Where(Function(m) m.BASYO.Contains(Basyo))
			End If

			If Not String.IsNullOrEmpty(Bangumitanto) Then
				d0010 = d0010.Where(Function(m) m.BANGUMITANTO.Contains(Bangumitanto))
			End If

			If Not String.IsNullOrEmpty(Bangumirenrk) Then
				d0010 = d0010.Where(Function(m) m.BANGUMIRENRK.Contains(Bangumirenrk))
			End If

			'ASI[2019 Dec 17] SPORT condition added
			If Not String.IsNullOrEmpty(SPORTCATCD) AndAlso SPORTCATCD <> "0" Then
				d0010 = d0010.Where(Function(m) m.SPORTCATCD = (SPORTCATCD))
			End If

			If Not String.IsNullOrEmpty(SPORTSUBCATCD) AndAlso SPORTSUBCATCD <> "0" Then
				d0010 = d0010.Where(Function(m) m.SPORTSUBCATCD = (SPORTSUBCATCD))
			End If

			If String.IsNullOrEmpty(OAJKNST) = False Then
				If OAJKNST.Contains(":") = False Then
					If OAJKNST.Length > 2 Then
						Dim strMM As String = OAJKNST.Substring(OAJKNST.Length - 2, 2)
						Dim strHH As String = OAJKNST.Remove(OAJKNST.Length - 2, 2)
						OAJKNST = strHH.PadLeft(2, "0") & strMM
					Else
						OAJKNST = OAJKNST.PadLeft(2, "0") & "00"
					End If
				Else
					OAJKNST = OAJKNST.Replace(":", "").PadLeft(4, "0")
				End If
			End If

			If String.IsNullOrEmpty(OAJKNED) = False Then
				If OAJKNED.Contains(":") = False Then
					If OAJKNED.Length > 2 Then
						Dim strMM As String = OAJKNED.Substring(OAJKNED.Length - 2, 2)
						Dim strHH As String = OAJKNED.Remove(OAJKNED.Length - 2, 2)
						OAJKNED = strHH.PadLeft(2, "0") & strMM
					Else
						OAJKNED = OAJKNED.PadLeft(2, "0") & "00"
					End If
				Else
					OAJKNED = OAJKNED.Replace(":", "").PadLeft(4, "0")
				End If
			End If

			If Not String.IsNullOrEmpty(OAJKNST) Then
				If String.IsNullOrEmpty(OAJKNED) Then
					OAJKNED = OAJKNST
				End If
				d0010 = d0010.Where(Function(m) (OAJKNST) <= m.OAJKNED AndAlso (OAJKNED) >= m.OAJKNST)
			End If

			If String.IsNullOrEmpty(SAIJKNST) = False Then
				If SAIJKNST.Contains(":") = False Then
					If SAIJKNST.Length > 2 Then
						Dim strMM As String = SAIJKNST.Substring(SAIJKNST.Length - 2, 2)
						Dim strHH As String = SAIJKNST.Remove(SAIJKNST.Length - 2, 2)
						SAIJKNST = strHH.PadLeft(2, "0") & strMM
					Else
						SAIJKNST = SAIJKNST.PadLeft(2, "0") & "00"
					End If
				Else
					SAIJKNST = SAIJKNST.Replace(":", "").PadLeft(4, "0")
				End If
			End If

			If String.IsNullOrEmpty(SAIJKNED) = False Then
				If SAIJKNED.Contains(":") = False Then
					If SAIJKNED.Length > 2 Then
						Dim strMM As String = SAIJKNED.Substring(SAIJKNED.Length - 2, 2)
						Dim strHH As String = SAIJKNED.Remove(SAIJKNED.Length - 2, 2)
						SAIJKNED = strHH.PadLeft(2, "0") & strMM
					Else
						SAIJKNED = SAIJKNED.PadLeft(2, "0") & "00"
					End If
				Else
					SAIJKNED = SAIJKNED.Replace(":", "").PadLeft(4, "0")
				End If
			End If

			If Not String.IsNullOrEmpty(SAIJKNST) Then
				If String.IsNullOrEmpty(SAIJKNED) Then
					SAIJKNED = SAIJKNST
				End If
				d0010 = d0010.Where(Function(m) (SAIJKNST) <= m.SAIJKNED AndAlso (SAIJKNED) >= m.SAIJKNST)
			End If

			If Not String.IsNullOrEmpty(Biko) Then
				d0010 = d0010.Where(Function(m) m.BIKO.Contains(Biko))
			End If


			If Not (PtnAna1 AndAlso PtnAna2) AndAlso Not (PtnAna1 = False AndAlso PtnAna2 = False) Then
				If PtnAna1 = True Then
					d0010 = d0010.Where(Function(d1) db.D0020.Any(Function(d2) (d2.GYOMNO = d1.GYOMNO OrElse d2.GYOMNO = d1.PGYOMNO)))
				End If

				If PtnAna2 = True Then
					d0010 = From t In d0010 Where (From inq In db.D0021 Select inq.GYOMNO).Contains(t.GYOMNO)
				End If
			End If

			If Not String.IsNullOrEmpty(ANAID) AndAlso ANAID > "0" Then
				'd0010 = d0010.Where(Function(d1) db.D0020.Any(Function(d2) (d2.GYOMNO = d1.GYOMNO OrElse d2.GYOMNO = d1.PGYOMNO) AndAlso d2.USERID = ANAID))
				d0010 = d0010.Where(Function(d1) db.D0020.Any(Function(d2) d2.GYOMNO = d1.GYOMNO AndAlso d2.USERID = ANAID))
			End If

			If Not (Ptn1 AndAlso Ptn2 AndAlso Ptn3 AndAlso Ptn4 AndAlso Ptn5 AndAlso Ptn6 AndAlso Ptn7) AndAlso
			  Not (Ptn1 = False AndAlso Ptn2 = False AndAlso Ptn3 = False AndAlso Ptn4 = False AndAlso Ptn5 = False AndAlso Ptn6 = False AndAlso Ptn7 = False) Then

				Dim lstDayOfWeek As New List(Of DayOfWeek)
				If Ptn1 Then
					lstDayOfWeek.Add(DayOfWeek.Monday + 1)      'SqlのDatePart関数で、曜日は日～土＝１～７であるため、+1する
				End If
				If Ptn2 Then
					lstDayOfWeek.Add(DayOfWeek.Tuesday + 1)
				End If
				If Ptn3 Then
					lstDayOfWeek.Add(DayOfWeek.Wednesday + 1)
				End If
				If Ptn4 Then
					lstDayOfWeek.Add(DayOfWeek.Thursday + 1)
				End If
				If Ptn5 Then
					lstDayOfWeek.Add(DayOfWeek.Friday + 1)
				End If
				If Ptn6 Then
					lstDayOfWeek.Add(DayOfWeek.Saturday + 1)
				End If
				If Ptn7 Then
					lstDayOfWeek.Add(DayOfWeek.Sunday + 1)
				End If
				If lstDayOfWeek.Count > 0 Then
					'lstd0010 = lstd0010.Where(Function(m) lstDayOfWeek.Contains(Date.Parse(m.GYOMYMD).DayOfWeek)).ToList
					d0010 = d0010.Where(Function(m) lstDayOfWeek.Contains(SqlServer.SqlFunctions.DatePart("dw", m.GYOMYMD)))
				End If
			End If

			If String.IsNullOrEmpty(confirmmsg) = True Then
				Dim intSearchDataCnt As Integer = d0010.Count()
				If intSearchDataCnt > 2000 Then
					'TempData("message") = String.Format("検索結果が{0}件あります。" & vbCrLf & "2000件以内になるよう、条件を絞ってください。", intSearchDataCnt)
					TempData("warning") = String.Format("検索結果が{0}件あります。" & vbCrLf & "時間が掛かりますが、よろしいですか？", intSearchDataCnt)
					Return View()
				End If
			End If

			Dim lstd0010 = d0010.OrderBy(Function(f) f.GYOMYMD).ThenBy(Function(f) f.GYOMYMDED).ThenBy(Function(f) f.KSKJKNST).ThenBy(Function(f) f.KSKJKNED).ThenBy(Function(f) f.CATCD).ToList

			For Each d0010a In lstd0010
				d0010a.D0020 = d0010a.D0020.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
			Next

			Return View(lstd0010)
		End Function


		'POST: D0010
		<HttpPost()>
		Function Index(<Bind(Include:="FLGDEL,GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,RNZK,PGYOMNO,IKTFLG,IKTUSERID,IKKATUNO,PATTERN,MON,TUE,WED,TUR,FRI,SAT,SUN,ACUSERID,SPORTCATCD,SPORTSUBCATCD,SAIJKNST,SAIJKNED,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,D0020,D0021,M0020,M0130,M0140")> ByVal lstd0010 As List(Of D0010),
					   ByVal button As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If button = "downloadcsv" Then
				Return DownloadCsv(lstd0010)
			End If

			Dim dtNow As Date = Now
			Dim decNewHenkorrkcd As Decimal = GetMaxHenkorrkcd() + 1

			Dim lstD0010Copy As New List(Of D0010)
			Dim lstD0020Copy As New List(Of D0020)

			For Each item In lstd0010
				If item.FLGDEL = True Then

					Dim d0010 As D0010 = db.D0010.Find(item.GYOMNO)
					If d0010 Is Nothing Then
						Return HttpNotFound()
					End If

					db.D0010.Remove(d0010)

					Dim lstD0020 = db.D0020.Where(Function(d) d.GYOMNO = d0010.GYOMNO).ToList
					Dim lstD0021 = db.D0021.Where(Function(d) d.GYOMNO = d0010.GYOMNO).ToList

					'変更履歴の作成
					Dim d0070 As New D0070
					d0070.HENKORRKCD = decNewHenkorrkcd
					d0070.HENKONAIYO = "削除"
					d0070.USERID = loginUserId
					d0070.SYUSEIYMD = dtNow
					d0070.TNTNM = GetAllAnanm(lstD0020, lstD0021)
					CopyHenkorrk(d0070, d0010)
					db.D0070.Add(d0070)

					decNewHenkorrkcd += 1

					'連続業務の場合、子業務も削除。
					If d0010.RNZK Then
						Dim lstd0010child = (From t In db.D0010 Where t.PGYOMNO = d0010.GYOMNO).ToList
						For Each itemchild In lstd0010child
							db.D0010.Remove(itemchild)
						Next
					End If

					lstD0010Copy.Add(d0010)

					Dim d0020lst = db.D0020.Where(Function(f) f.GYOMNO = item.GYOMNO).ToList
					lstD0020Copy.AddRange(d0020lst)
				End If
			Next

			Dim decNewKyukHenkorrkcd As Decimal = GetMaxKyukHenkorrkcd() + 1

			Dim bolRtn As Boolean = True

			Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
			sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

			Using tran As DbContextTransaction = db.Database.BeginTransaction
				Try
					Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

					db.Configuration.ValidateOnSaveEnabled = False
					db.SaveChanges()
					db.Configuration.ValidateOnSaveEnabled = True

					For Each item In lstD0010Copy

						Dim pgyomno As Decimal = Nothing
						If (item.PGYOMNO IsNot Nothing AndAlso item.SPORTFLG = True) Then
							pgyomno = item.PGYOMNO
						End If

						'ASI[28 Feb 2020] : after fix 一括本登録, when delete fix sport shift in 業務削除 page, 
						'then delete the primary data(1stly created data) also if no other Ana exist.
						If (item.PGYOMNO IsNot Nothing AndAlso item.SPORTFLG = True) Then

							Dim anaCnt = (From d10 In db.D0010
										  Join d20 In db.D0020 On d20.GYOMNO Equals d10.GYOMNO
										  Where d10.GYOMNO <> item.GYOMNO And d10.PGYOMNO = pgyomno).Count
							If anaCnt = 0 Then
								anaCnt = (From d10 In db.D0010
										  Join d22 In db.D0022 On d22.GYOMNO Equals d10.GYOMNO
										  Where d10.GYOMNO = pgyomno Or d10.PGYOMNO = pgyomno).Count
								If anaCnt = 0 Then
									anaCnt = (From d10 In db.D0010
											  Join d21 In db.D0021 On d21.GYOMNO Equals d10.GYOMNO
											  Where d10.GYOMNO <> item.GYOMNO And d10.PGYOMNO = pgyomno).Count
								End If
							End If

							If anaCnt = 0 Then
								Dim lstd0010child = (From t In db.D0010 Where t.GYOMNO <> item.GYOMNO And (t.GYOMNO = pgyomno Or t.PGYOMNO = pgyomno)).ToList

								If lstd0010child.Count > 0 Then
									For Each itemchild In lstd0010child
										db.D0010.Remove(itemchild)
									Next

									db.Configuration.ValidateOnSaveEnabled = False
									db.SaveChanges()
									db.Configuration.ValidateOnSaveEnabled = True
								End If
							End If
						End If
					Next

					For Each item In lstD0010Copy
						Dim d0020lst = lstD0020Copy.Where(Function(f) f.GYOMNO = item.GYOMNO).ToList
						For Each d0020 In d0020lst

							'「10:24時超え休出」で24時を跨る業務を削除すると「4:公休」に変更される。(ただし、他に２４超えの業務が存在しない場合のみ)	
							'休出の業務が削除された場合、「4:公休」に戻す。
							CheckAndUpdateKoukyu(decNewKyukHenkorrkcd, item, d0020, dtNow)

							db.Configuration.ValidateOnSaveEnabled = False
							db.SaveChanges()
							db.Configuration.ValidateOnSaveEnabled = True
						Next
					Next

					tran.Commit()

				Catch ex As Exception
					bolRtn = False
					Throw ex
					tran.Rollback()
				End Try
			End Using


			Return RedirectToAction("Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
				.PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
				.Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
				.CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
				.Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk"),
				.SPORTCATCD = Session("SPORTCATCD"), .SPORTSUBCATCD = Session("SPORTSUBCATCD"), .OAJKNST = Session("OAJKNST"), .OAJKNED = Session("OAJKNED"), .SAIJKNST = Session("SAIJKNST"), .SAIJKNED = Session("SAIJKNED"), .Biko = Session("Biko")})

		End Function


		Private Function DownloadCsv(ByVal lstD0010 As List(Of D0010)) As ActionResult

			Dim builder As New StringBuilder()
			Dim strRecord As String = "業務期間,業務期間-終了,拘束時間-開始,拘束時間-終了,番組名,内容,アナウンサー,仮アナカテゴリー,場所,備考,番組担当者,連絡先,カテゴリー名,スポーツカテゴリ,スポーツサブカテゴリ,OA時間-開始,OA時間-終了,試合時間-開始,試合時間-終了"
			builder.AppendLine(strRecord)

			For Each d0010 As D0010 In lstD0010
				strRecord = Date.Parse(d0010.GYOMYMD).ToString("yyyy/MM/dd") & ","

				If d0010.GYOMYMDED IsNot Nothing Then
					strRecord = strRecord & Date.Parse(d0010.GYOMYMDED).ToString("yyyy/MM/dd")
				End If
				strRecord = strRecord & "," & d0010.KSKJKNST.ToString.Substring(0, 2) & ":" & d0010.KSKJKNST.ToString.Substring(2, 2) & "," &
					 d0010.KSKJKNED.ToString.Substring(0, 2) & ":" & d0010.KSKJKNED.ToString.Substring(2, 2) & ","

				If d0010.BANGUMINM IsNot Nothing Then
					strRecord = strRecord & d0010.BANGUMINM
				End If
				strRecord = strRecord & ","

				If d0010.NAIYO IsNot Nothing Then
					strRecord = strRecord & d0010.NAIYO
				End If
				strRecord = strRecord & ","

				Dim strAna As String = ""

				If d0010.D0020 IsNot Nothing Then
					For Each d0020 In d0010.D0020.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
						If String.IsNullOrEmpty(strAna) = False Then
							strAna = strAna & "，"           '全角カンマ
						End If
						strAna = strAna & d0020.M0010.USERNM
					Next
				End If

				strRecord = strRecord & strAna & ","

				Dim strKariAna As String = ""

				If d0010.D0021 IsNot Nothing Then
					For Each d0021 In d0010.D0021
						If String.IsNullOrEmpty(strKariAna) = False Then
							strKariAna = strKariAna & "，"           '全角カンマ
						End If
						strKariAna = strKariAna & d0021.ANNACATNM
					Next
				End If

				strRecord = strRecord & strKariAna & ","

				If d0010.BASYO IsNot Nothing Then
					strRecord = strRecord & d0010.BASYO
				End If
				strRecord = strRecord & ","

				If d0010.BIKO IsNot Nothing Then
					strRecord = strRecord & d0010.BIKO
				End If
				strRecord = strRecord & ","


				If d0010.BANGUMITANTO IsNot Nothing Then
					strRecord = strRecord & d0010.BANGUMITANTO
				End If
				strRecord = strRecord & ","

				If d0010.BANGUMIRENRK IsNot Nothing Then
					strRecord = strRecord & d0010.BANGUMIRENRK
				End If
				strRecord = strRecord & ","

				If d0010.M0020 IsNot Nothing AndAlso d0010.M0020.CATNM IsNot Nothing Then
					strRecord = strRecord & d0010.M0020.CATNM
				End If
				strRecord = strRecord & ","

				If d0010.M0130 IsNot Nothing AndAlso d0010.M0130.SPORTCATNM IsNot Nothing Then
					strRecord = strRecord & d0010.M0130.SPORTCATNM
				End If
				strRecord = strRecord & ","

				If d0010.M0140 IsNot Nothing AndAlso d0010.M0140.SPORTSUBCATNM IsNot Nothing Then
					strRecord = strRecord & d0010.M0140.SPORTSUBCATNM
				End If
				strRecord = strRecord & ","


				If d0010.OAJKNST IsNot Nothing Then
					strRecord = strRecord & d0010.OAJKNST.ToString.Substring(0, 2) & ":" & d0010.OAJKNST.ToString.Substring(2, 2)
				End If
				strRecord = strRecord & ","

				If d0010.OAJKNED IsNot Nothing Then
					strRecord = strRecord & d0010.OAJKNED.ToString.Substring(0, 2) & ":" & d0010.OAJKNED.ToString.Substring(2, 2)
				End If
				strRecord = strRecord & ","

				If d0010.SAIJKNST IsNot Nothing Then
					strRecord = strRecord & d0010.SAIJKNST.ToString.Substring(0, 2) & ":" & d0010.SAIJKNST.ToString.Substring(2, 2)
				End If
				strRecord = strRecord & ","

				If d0010.SAIJKNED IsNot Nothing Then
					strRecord = strRecord & d0010.SAIJKNED.ToString.Substring(0, 2) & ":" & d0010.SAIJKNED.ToString.Substring(2, 2)
				End If
				strRecord = strRecord & ","


				builder.AppendLine(strRecord)
			Next

			' 生成された文字列を「text/csv」形式（Shift_JIS）で出力
			Return File(System.Text.Encoding.GetEncoding("shift_jis").GetBytes(builder.ToString), "text/csv", "gyomdata.csv")
		End Function


		'番組名,担当者,連絡先,備考,拘束時間,カテゴリー,業務期間,OA時間,備考
		' GET: D0010/Create
		Function Create(deskno As String, eda As String, hinano As String, gyomsnsno As String, ikkatuno As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			ViewBag.ACUSERID = loginUserId

			Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim item1 As New M0020
			item1.CATCD = "0"
			item1.CATNM = ""
			lstm0020.Insert(0, item1)
			ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM")
			Dim s0010_Record As S0010 = db.S0010.Find(1)
			ViewBag.ABWEEKSTARTDT = s0010_Record.ABWEEKSTARTDT

			ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO")

			Dim lstbangumi = db.M0030.OrderBy(Function(m) m.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
			Dim anacatitem As New M0080
			anacatitem.ANNACATNO = "0"
			anacatitem.ANNACATNM = ""
			lstKarianacat.Insert(0, anacatitem)
			ViewBag.KarianacatList = lstKarianacat

			'休出
			ViewBag.KyukDe = db.M0060.Find(2)

			'ASI[23 Oct 2019]:法出
			ViewBag.KyukHouDe = db.M0060.Find(13)

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.ToString
                If Not strUrlReferrer.Contains("B0020/Create") AndAlso Not strUrlReferrer.Contains("B0020/SendMail") AndAlso Not strUrlReferrer.Contains("B0020/Edit") AndAlso Not strUrlReferrer.Contains("B0020/Delete") Then
                    Session("B0020CreateRtnUrl") = strUrlReferrer
                End If
            End If

			'デスクメモ/下書/雛形/業務承認から呼び出し場合、候補検索せず担当アナが設定されるので、その場合、更新する時要因チェックしたいため初期化する
			If String.IsNullOrEmpty(deskno) = False OrElse String.IsNullOrEmpty(hinano) = False OrElse String.IsNullOrEmpty(gyomsnsno) = False OrElse String.IsNullOrEmpty(ikkatuno) = False Then
				Dim lstW0010 = db.W0010.Where(Function(f) f.ACUSERID = loginUserId).ToList
				If lstW0010.Count > 0 Then
					For Each w0010 In lstW0010
						db.W0010.Remove(w0010)
					Next

					db.SaveChanges()
				End If
			End If

            'Wブッキング解除した後、業務登録に戻ってきた場合
            If Session("D0010_0") IsNot Nothing Then

                Dim d0010 As D0010 = Session("D0010_0")

                Dim strGYOMYMDED As String = d0010.GYOMYMD
                If d0010.GYOMYMDED IsNot Nothing Then
                    strGYOMYMDED = d0010.GYOMYMDED
                End If

                TempData("SearchKOHO") = True
                TempData("YOINUSERID") = d0010.YOINUSERID
                TempData("YOINIDYES") = d0010.YOINIDYES

                SearchKOHO(d0010.GYOMNO, d0010.GYOMYMD, strGYOMYMDED, d0010.KSKJKNST, d0010.KSKJKNED, d0010.PATTERN, d0010.MON, d0010.TUE, d0010.WED, d0010.TUR, d0010.FRI, d0010.SAT, d0010.SUN, d0010.WEEKA, d0010.WEEKB
                           )

                ViewBag.Showkoho = True

                ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)

                Session.Remove("D0010_0")

                Return View(d0010)


                'デスクメモから呼び出し
            ElseIf String.IsNullOrEmpty(deskno) = False Then

                Dim d0110 = db.D0110.Find(deskno)
                If d0110 Is Nothing Then
                    Return HttpNotFound()
                End If

                Dim d0010 As New D0010
                d0010.DESKNO = deskno
                If d0110.CATCD IsNot Nothing Then
                    d0010.CATCD = d0110.CATCD
                End If
                d0010.BANGUMINM = d0110.BANGUMINM
                d0010.NAIYO = d0110.NAIYO
                d0010.BANGUMITANTO = d0110.BANGUMITANTO
                d0010.BANGUMIRENRK = d0110.BANGUMIRENRK
                d0010.DESKMEMO = d0110.DESKMEMO
                d0010.BASYO = d0110.BASYO
                If String.IsNullOrEmpty(eda) = False Then
                    Dim intEda As Integer = Integer.Parse(eda)
                    Dim d0120 = db.D0120.Find(deskno, intEda)

                    If d0120 IsNot Nothing Then
                        d0010.GYOMYMD = d0120.SHIFTYMDST
                        d0010.GYOMYMDED = d0120.SHIFTYMDED
                        d0010.KSKJKNST = d0120.KSKJKNST
                        d0010.KSKJKNED = d0120.KSKJKNED
                    End If
                End If

                Dim d0130lst = db.D0130.Where(Function(f) f.DESKNO = deskno).ToList

                If d0130lst.Count > 0 Then
                    Dim lststrAnanm As New List(Of String)
                    For Each item In d0130lst.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
                        lststrAnanm.Add(item.M0010.USERNM)
                    Next

                    d0010.RefAnalist = lststrAnanm
                Else
                    Dim lststrKariAnanm As New List(Of String)
                    lststrKariAnanm.Add("仮アナ")

                    d0010.RefKariAnalist = lststrKariAnanm
                End If

                ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)

                Return View(d0010)

                '下書・雛形呼び出し
            ElseIf String.IsNullOrEmpty(hinano) = False Then

                Dim decHinano As Decimal = Decimal.Parse(hinano)
                Dim d0090 = db.D0090.Find(decHinano)
                If d0090 Is Nothing Then
                    Return HttpNotFound()
                End If

                Dim d0010 As New D0010

                d0010.HINANO = d0090.HINANO
                d0010.FMTKBN = d0090.FMTKBN
                d0010.GYOMYMD = d0090.GYOMYMD
                d0010.GYOMYMDED = d0090.GYOMYMDED
                d0010.KSKJKNST = d0090.KSKJKNST
                d0010.KSKJKNED = d0090.KSKJKNED
                d0010.CATCD = d0090.CATCD
                d0010.BANGUMINM = d0090.BANGUMINM
                d0010.OAJKNST = d0090.OAJKNST
                d0010.OAJKNED = d0090.OAJKNED
                d0010.NAIYO = d0090.NAIYO
                d0010.BASYO = d0090.BASYO
                d0010.BIKO = d0090.BIKO
                d0010.BANGUMITANTO = d0090.BANGUMITANTO
                d0010.BANGUMIRENRK = d0090.BANGUMIRENRK
                d0010.PATTERN = d0090.PTNFLG
                d0010.MON = d0090.PTN1
                d0010.TUE = d0090.PTN2
                d0010.WED = d0090.PTN3
                d0010.TUR = d0090.PTN4
                d0010.FRI = d0090.PTN5
                d0010.SAT = d0090.PTN6
                d0010.SUN = d0090.PTN7
                'ASI[24 Oct 2019]
                d0010.WEEKA = d0090.WEEKA
                d0010.WEEKB = d0090.WEEKB

                If d0090.D0100 IsNot Nothing AndAlso d0090.D0100.Count > 0 Then
                    Dim lststrAnanm As New List(Of String)
                    For Each item In d0090.D0100.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
                        lststrAnanm.Add(item.M0010.USERNM)
                    Next

                    d0010.RefAnalist = lststrAnanm
                End If

                If d0090.D0101 IsNot Nothing AndAlso d0090.D0101.Count > 0 Then
                    Dim lststrKariAnanm As New List(Of String)
                    For Each item In d0090.D0101
                        lststrKariAnanm.Add(item.ANNACATNM)
                    Next

                    d0010.RefKariAnalist = lststrKariAnanm
                End If

                ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0090.CATCD)

                Return View(d0010)

                '業務承認から呼び出し
            ElseIf String.IsNullOrEmpty(gyomsnsno) = False Then

                Dim decGyomsnsno As Decimal = Decimal.Parse(gyomsnsno)
                Dim d0050 = db.D0050.Find(decGyomsnsno)
                If d0050 Is Nothing Then
                    Return HttpNotFound()
                End If

                Dim d0010 As New D0010
                d0010.GYOMSNSNO = gyomsnsno
                d0010.GYOMYMD = d0050.GYOMYMD
                d0010.GYOMYMDED = d0050.GYOMYMDED
                d0010.KSKJKNST = d0050.KSKJKNST
                d0010.KSKJKNED = d0050.KSKJKNED
                d0010.CATCD = d0050.CATCD
                d0010.BANGUMINM = d0050.BANGUMINM
                d0010.NAIYO = d0050.NAIYO
                d0010.BASYO = d0050.BASYO
                d0010.BANGUMITANTO = d0050.BANGUMITANTO
                d0010.BANGUMIRENRK = d0050.BANGUMIRENRK
                d0010.BIKO = d0050.GYOMMEMO

                Dim lstD0020 As New List(Of D0020)

                Dim d0020 As New D0020
                d0020.USERID = d0050.USERID
                d0020.USERNM = d0050.M0010.USERNM
                lstD0020.Add(d0020)
                d0010.D0020 = lstD0020

                Dim lststrAnanm As New List(Of String)
                lststrAnanm.Add(d0050.M0010.USERNM)
                d0010.RefAnalist = lststrAnanm

                ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0050.CATCD)

                Return View(d0010)

                '一括業務から
            ElseIf String.IsNullOrEmpty(ikkatuno) = False Then

                Dim decIkkatuno As Decimal = Decimal.Parse(ikkatuno)
                Dim m0090 = db.M0090.Find(decIkkatuno)
                If m0090 Is Nothing Then
                    Return HttpNotFound()
                End If

                Dim d0010 As New D0010
                d0010.IKKATUNO = ikkatuno
                d0010.GYOMYMD = m0090.GYOMYMD
                d0010.GYOMYMDED = m0090.GYOMYMDED
                d0010.KSKJKNST = m0090.KSKJKNST
                d0010.KSKJKNED = m0090.KSKJKNED
                d0010.CATCD = m0090.CATCD
                d0010.BANGUMINM = m0090.BANGUMINM

                d0010.OAJKNST = m0090.OAJKNST
                d0010.OAJKNED = m0090.OAJKNED
                d0010.NAIYO = m0090.NAIYO
                d0010.BASYO = m0090.BASYO
                d0010.BIKO = m0090.BIKO
                d0010.BANGUMITANTO = m0090.BANGUMITANTO
                d0010.BANGUMIRENRK = m0090.BANGUMIRENRK
                d0010.PATTERN = m0090.PTNFLG
                d0010.MON = m0090.PTN1
                d0010.TUE = m0090.PTN2
                d0010.WED = m0090.PTN3
                d0010.TUR = m0090.PTN4
                d0010.FRI = m0090.PTN5
                d0010.SAT = m0090.PTN6
                d0010.SUN = m0090.PTN7
                'ASI[24 Oct 2019]
                d0010.WEEKA = m0090.WEEKA
                d0010.WEEKB = m0090.WEEKB

                If m0090.M0110 IsNot Nothing AndAlso m0090.M0110.Count > 0 Then
                    Dim lststrAnanm As New List(Of String)
                    For Each item In m0090.M0110.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
                        lststrAnanm.Add(item.M0010.USERNM)
                    Next

                    d0010.RefAnalist = lststrAnanm
                End If

                If m0090.M0120 IsNot Nothing AndAlso m0090.M0120.Count > 0 Then
                    Dim lststrKariAnanm As New List(Of String)
                    For Each item In m0090.M0120
                        lststrKariAnanm.Add(item.ANNACATNM)
                    Next

                    d0010.RefKariAnalist = lststrKariAnanm
                End If


                ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", m0090.CATCD)
                Return View(d0010)
            End If

            Return View()
        End Function

        ' POST: D0010/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="INDIVIDUAL,GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,RNZK,PGYOMNO,IKTFLG,IKTUSERID,IKKATUNO,PATTERN,MON,TUE,WED,TUR,FRI,SAT,SUN,WEEKA,WEEKB,ACUSERID,HINANO,FMTKBN,HINAMEMO,DATAKBN,ANAIDLIST,KARIANALIST,CONFIRMMSG,GYOMSNSNO,DESKNO,DESKMEMO,RefAnalist,RefCatAnalist,RefKariAnalist,RefCatKariAnalist,YOINUSERID,YOINUSERNM,YOINIDYES,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,D0020,D0021")> ByVal d0010 As D0010,
                        ByVal button As String, ByVal yoinid As String, ByVal userid As String, ByVal wgyomno As String, ByVal lastyoinflg As String) As ActionResult

            If button = "btnYes" Then

                Return UpdateYoinUser("Create", d0010, yoinid, userid, wgyomno, "")
            Else
                Return DoUpdateDB(d0010)
            End If

        End Function

        Private Function UpdateYoinUser(ByVal actionName As String, ByVal d0010 As D0010, ByVal yoinid As String, ByVal userid As String, ByVal wgyomno As String, ByVal copygyomno As String) As ActionResult

            Session("D0010_" & d0010.GYOMNO) = d0010

            'Wブッキングの場合、業務修正画面に遷移する
            If yoinid = 12 Then
                'Wブッキングの業務に移動する前の業務Noを保持する
                Session("B0020EditMotoGyomno" & d0010.GYOMNO) = d0010.GYOMNO
                Session("B0020WGYOMNO" & wgyomno) = wgyomno

                Return RedirectToAction("Edit", routeValues:=New With {.id = wgyomno})

            End If

            If actionName = "Create" Then
                '新規登録
                Return RedirectToAction(actionName)
            ElseIf actionName = "Copy" Then
                '複写
                Return RedirectToAction(actionName, routeValues:=New With {.id = copygyomno})
            Else
                '修正
                Return RedirectToAction(actionName, routeValues:=New With {.id = d0010.GYOMNO})
            End If

            'TempData("SearchKOHO") = True
            'TempData("YOINUSERID") = d0010.YOINUSERID
            'TempData("YOINIDYES") = d0010.YOINIDYES

            'Dim strGYOMYMDED As String = d0010.GYOMYMD
            'If d0010.GYOMYMDED IsNot Nothing Then
            '	strGYOMYMDED = d0010.GYOMYMDED
            'End If

            'SearchKOHO(d0010.GYOMNO, d0010.GYOMYMD, strGYOMYMDED, d0010.KSKJKNST, d0010.KSKJKNED, d0010.PATTERN, d0010.MON, d0010.TUR, d0010.WED, d0010.TUR, d0010.FRI, d0010.SAT, d0010.SUN)

            'ViewBag.Showkoho = True

            'Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            'Dim item1 As New M0020
            'item1.CATCD = "0"
            'item1.CATNM = ""
            'lstm0020.Insert(0, item1)
            'ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)

            'ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO")

            'Dim lstbangumi = db.M0030.OrderBy(Function(m) m.BANGUMIKN).ToList
            'Dim bangumiitem As New M0030
            'bangumiitem.BANGUMICD = "0"
            'bangumiitem.BANGUMINM = ""
            'lstbangumi.Insert(0, bangumiitem)
            'ViewBag.BangumiList = lstbangumi

            'Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
            'Dim naiyoitem As New M0040
            'naiyoitem.NAIYOCD = "0"
            'naiyoitem.NAIYO = ""
            'lstNaiyo.Insert(0, naiyoitem)
            'ViewBag.NaiyouList = lstNaiyo

            'Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
            'Dim anacatitem As New M0080
            'anacatitem.ANNACATNO = "0"
            'anacatitem.ANNACATNM = ""
            'lstKarianacat.Insert(0, anacatitem)
            'ViewBag.KarianacatList = lstKarianacat

            ''休出
            'ViewBag.KyukDe = db.M0060.Find(2)


            'Return View(d0010)
        End Function

        'ASI[25 Dec 2019]:Wブッキングは現状のまま。休日に関し、選択した時点では休日更新しない。当業務を更新する時に一緒に更新する。
        Private Sub UpdateYoinUser(ByRef decNewKyukHenkorrkcd As Decimal, ByVal d0010 As D0010)

            If d0010.D0020 IsNot Nothing Then

                For Each item In d0010.D0020

                    Dim YesYoinlist As New List(Of String)
                    If item.YOINIDYES IsNot Nothing Then
                        Dim strsYesYoin As String() = item.YOINIDYES.Split(",")
                        For Each strYoin As String In strsYesYoin
                            YesYoinlist.Add(strYoin)
                        Next
                    End If

                    If YesYoinlist.Contains("3567") OrElse YesYoinlist.Contains("11") OrElse YesYoinlist.Contains("39") OrElse YesYoinlist.Contains("0") Then

                        Dim lstkyukdata = (From w0020 In db.W0020 Join d0040 In db.D0040
                                                    On w0020.USERID Equals d0040.USERID And w0020.NENGETU Equals d0040.NENGETU And
                                                    w0020.HI Equals d0040.HI And w0020.JKNST Equals d0040.JKNST
                                           Where w0020.ACUSERID = d0010.ACUSERID And w0020.USERID = item.USERID).ToList

                        Dim dtNow As Date = Now
                        Dim strHENKONAIYO As String = ""

                        For Each row In lstkyukdata

                            '時間休
                            If row.w0020.YOINID = 3 Then
                                db.D0040.Remove(row.d0040)
                                strHENKONAIYO = "削除"

                                '公休→休出 'ASI[06 Nov 2019] : Added condition for YOINID 7
                            ElseIf row.w0020.YOINID = 5 OrElse row.w0020.YOINID = 7 Then
                                row.d0040.KYUKCD = 2
                                strHENKONAIYO = "変更"

                                'ASI[18 Oct 2019] : Added condition for YOINID 13 'ASI[06 Nov 2019] : Added condition for YOINID 14
                            ElseIf row.w0020.YOINID = 13 OrElse row.w0020.YOINID = 14 Then
                                row.d0040.KYUKCD = 13
                                strHENKONAIYO = "変更"

                                '24時間超え休出公休→24時間超え休出
                            ElseIf row.w0020.YOINID = 11 Then
                                row.d0040.KYUKCD = 10
                                strHENKONAIYO = "変更"

                                'ASI[18 Oct 2019] : Added condition for YOINID 15
                            ElseIf row.w0020.YOINID = 15 Then
                                row.d0040.KYUKCD = 14
                                strHENKONAIYO = "変更"

                                '代休→出勤、振替休→出勤

                            ElseIf row.w0020.YOINID = 6 Then
                                db.D0040.Remove(row.d0040)
                                row.d0040.KYUKCD = 1
                                strHENKONAIYO = "変更"

                                '2017/08/02不適合要因無しで業務の実終了時間が翌日になっていて、翌日に時間休、時間強休があったら、アラート出すため修正
                            ElseIf row.w0020.YOINID = 0 Then
                                '時間休
                                If row.w0020.KYUKCD = 3 Then
                                    db.D0040.Remove(row.d0040)
                                    strHENKONAIYO = "削除"

                                    '時間強休
                                ElseIf row.w0020.KYUKCD = 9 Then
                                    db.D0040.Remove(row.d0040)
                                    strHENKONAIYO = "削除"
                                End If

                            End If

                            '休暇変更履歴作成
                            If String.IsNullOrEmpty(strHENKONAIYO) = False Then
                                Dim d0150 As New D0150
                                d0150.HENKORRKCD = decNewKyukHenkorrkcd
                                d0150.HENKONAIYO = strHENKONAIYO
                                d0150.USERID = Session("LoginUserid")
                                d0150.SYUSEIYMD = dtNow
                                CopyKyukHenkorrk(d0150, row.d0040)
                                db.D0150.Add(d0150)

                                decNewKyukHenkorrkcd += 1
                                strHENKONAIYO = ""
                            End If

                        Next

                        'db.SaveChanges()

                    End If

                Next

            End If

        End Sub

        ''休日の場合、休日テーブルを更新する
        '<HttpPost()>
        'Function UpdateKyuka(ByVal userid As Integer) As JsonResult

        '	If Request.IsAjaxRequest() OrElse TempData("UpdateKyuka") = True Then
        '		Dim intAcuserid As Integer = Session("LoginUserid")

        '		Dim lstkyukdata = (From w0020 In db.W0020 Join d0040 In db.D0040
        '							On w0020.USERID Equals d0040.USERID And w0020.NENGETU Equals d0040.NENGETU And
        '							w0020.HI Equals d0040.HI And w0020.JKNST Equals d0040.JKNST
        '							Where w0020.ACUSERID = intAcuserid And w0020.USERID = userid).ToList

        '		For Each row In lstkyukdata
        '			'時間休
        '			If row.w0020.YOINID = 3 Then
        '				db.D0040.Remove(row.d0040)

        '				'公休→休出
        '			ElseIf row.w0020.YOINID = 5 Then
        '				row.d0040.KYUKCD = 2

        '				'24時間超え休出公休→24時間超え休出
        '			ElseIf row.w0020.YOINID = 11 Then
        '				row.d0040.KYUKCD = 10

        '				'代休→出勤、振替休→出勤
        '			ElseIf row.w0020.YOINID = 6 OrElse row.w0020.YOINID = 7 Then
        '				db.D0040.Remove(row.d0040)

        '			End If
        '		Next

        '		DoDBSaveChanges(db)
        '	End If

        '	Return Json(New With {.success = True})
        'End Function


        ' GET: D0010/Copy/5
        Function Copy(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim d0010 As D0010 = db.D0010.Find(id)
			If IsNothing(d0010) Then
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

			ViewBag.ACUSERID = loginUserId
			Dim s0010_Record As S0010 = db.S0010.Find(1)
			ViewBag.ABWEEKSTARTDT = s0010_Record.ABWEEKSTARTDT

			Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim item1 As New M0020
			item1.CATCD = "0"
			item1.CATNM = ""
			lstm0020.Insert(0, item1)

			'ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO", d0010.IKKATUNO)

			Dim lstbangumi = db.M0030.OrderBy(Function(m) m.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
			Dim anacatitem As New M0080
			anacatitem.ANNACATNO = "0"
			anacatitem.ANNACATNM = ""
			lstKarianacat.Insert(0, anacatitem)
			ViewBag.KarianacatList = lstKarianacat

			'休出
			ViewBag.KyukDe = db.M0060.Find(2)

			'ASI[23 Oct 2019]:法出
			ViewBag.KyukHouDe = db.M0060.Find(13)
			d0010.INDIVIDUAL = True
			If Session("D0010_0") IsNot Nothing Then
				d0010 = Session("D0010_0")

				Dim strGYOMYMDED As String = d0010.GYOMYMD
				If d0010.GYOMYMDED IsNot Nothing Then
					strGYOMYMDED = d0010.GYOMYMDED
				End If

				TempData("SearchKOHO") = True
				TempData("YOINUSERID") = d0010.YOINUSERID
				TempData("YOINIDYES") = d0010.YOINIDYES

				SearchKOHO(d0010.GYOMNO, d0010.GYOMYMD, strGYOMYMDED, d0010.KSKJKNST, d0010.KSKJKNED, d0010.PATTERN, d0010.MON, d0010.TUE, d0010.WED, d0010.TUR, d0010.FRI, d0010.SAT, d0010.SUN, d0010.WEEKA, d0010.WEEKB)

				ViewBag.Showkoho = True

				ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)

				Session.Remove("D0010_0")

				Return View(d0010)
			End If

			d0010.GYOMYMD = Nothing
			d0010.GYOMYMDED = Nothing
			d0010.KSKJKNST = Nothing
			d0010.KSKJKNED = Nothing

			If d0010.D0020 IsNot Nothing Then
				Dim lststrAnanm As New List(Of String)
				For Each item In d0010.D0020.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList
					lststrAnanm.Add(item.M0010.USERNM)
				Next

				d0010.RefAnalist = lststrAnanm
			End If

			If d0010.D0021 IsNot Nothing Then
				Dim lststrKariAnanm As New List(Of String)
				For Each item In d0010.D0021
					lststrKariAnanm.Add(item.ANNACATNM)
				Next

				d0010.RefKariAnalist = lststrKariAnanm
			End If

			d0010.D0020.Clear()
			d0010.D0021.Clear()

			ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)

			Return View(d0010)
		End Function

		' POST: D0010/Copy/5
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function COPY(<Bind(Include:="INDIVIDUAL,GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,RNZK,PGYOMNO,IKTFLG,IKTUSERID,IKKATUNO,PATTERN,MON,TUE,WED,TUR,FRI,SAT,SUN,WEEKA,WEEKB,ACUSERID,HINANO,FMTKBN,RefAnalist,RefCatAnalist,RefKariAnalist,RefCatKariAnalist,YOINUSERID,YOINUSERNM,YOINIDYES,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,D0020,D0021")> ByVal d0010 As D0010,
					ByVal id As Decimal, ByVal button As String, ByVal yoinid As String, ByVal userid As String, ByVal wgyomno As String, ByVal lastyoinflg As String) As ActionResult

			If button = "btnYes" Then

				Return UpdateYoinUser("Copy", d0010, yoinid, userid, wgyomno, id)
			Else
				Return DoUpdateDB(d0010, True)
			End If

		End Function


		'新規と複写の共通関数
		Private Function DoUpdateDB(ByVal d0010 As D0010, Optional ByVal bolCopy As Boolean = False) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			ViewBag.ACUSERID = loginUserId

			If d0010.FMTKBN Is Nothing Then
				d0010.FMTKBN = 0
			End If

			If d0010.FMTKBN = 0 AndAlso ModelState.IsValid Then

				'業務期間終了日が未設定の場合、開始日と同じ日を設定。
				If d0010.GYOMYMDED Is Nothing Then
					d0010.GYOMYMDED = d0010.GYOMYMD
				End If

				'各担当アナに対し、業務期間の終了年月が公休展開されているかチェックする
				CheckKokyutenkaiDone(d0010)

                '未確認の不適合要因があるかチェックする（候補検索から選択して追加した後、不適合要因が発生してしまう可能性があるため）
                If ModelState.IsValid Then
                    CheckYoin(d0010)
                End If

            End If

            Dim bolWarning As Boolean = False

			'「雛形」か「下書」を呼び出しているとき、登録する担当アナウンサーが「雛形」か「下書」から変更されていれば確認メッセージを表示する。
			If bolCopy = False AndAlso d0010.FMTKBN = 0 AndAlso ModelState.IsValid AndAlso d0010.HINANO > 0 Then

				Dim d0090 = db.D0090.Find(d0010.HINANO)

				If d0010.CONFIRMMSG Is Nothing OrElse d0010.CONFIRMMSG = False Then

					Dim strWarningMsg As String = "雛形・下書に登録されているアナウンサー、又は仮アナカテゴリーと異なっています。登録してよろしいですか？"

					If d0010.D0020 IsNot Nothing AndAlso d0010.D0020.Count > 0 Then

						Dim d0100list = db.D0100.Where(Function(f) f.HINANO = d0010.HINANO).ToList

						If d0100list.Count > 0 Then

							If d0010.D0020.Count <> d0100list.Count Then
								bolWarning = True
							Else
								For Each item In d0100list
									Dim bolFound As Boolean = False
									For Each d0020 In d0010.D0020
										If item.USERID = d0020.USERID Then
											bolFound = True
											Exit For
										End If
									Next
									If bolFound = False Then
										bolWarning = True
										Exit For
									End If
								Next
							End If

							If bolWarning = True Then
								TempData("warning") = strWarningMsg
							End If

						End If

					End If

					If bolWarning = False Then

						If d0010.D0021 IsNot Nothing AndAlso d0010.D0021.Count > 0 Then

							Dim d0101list = db.D0101.Where(Function(f) f.HINANO = d0010.HINANO).ToList

							If d0101list.Count > 0 Then

								If d0010.D0021.Count <> d0101list.Count Then
									bolWarning = True
								Else
									For Each item In d0101list
										Dim bolFound As Boolean = False
										For Each d0021 In d0010.D0021
											If item.ANNACATNM = d0021.ANNACATNM Then
												bolFound = True
												Exit For
											End If
										Next
										If bolFound = False Then
											bolWarning = True
											Exit For
										End If
									Next
								End If

								If bolWarning = True Then
									TempData("warning") = strWarningMsg
								End If

							End If

						End If

					End If

				End If

			End If

			If (d0010.FMTKBN = 0 AndAlso ModelState.IsValid AndAlso bolWarning = False) OrElse d0010.FMTKBN = 1 OrElse d0010.FMTKBN = 2 Then '1:下書、2:雛形の場合、D0090に登録するので、D0010のModelState.IsValidは無視。

				'OA時間をから：を除外して4桁化する。
				If d0010.OAJKNST IsNot Nothing Then
					d0010.OAJKNST = ChangeToHHMM(d0010.OAJKNST)
				End If
				If d0010.OAJKNED IsNot Nothing Then
					d0010.OAJKNED = ChangeToHHMM(d0010.OAJKNED)
				End If

				'拘束時間から：を除外して4桁化する。
				d0010.KSKJKNST = ChangeToHHMM(d0010.KSKJKNST)
				d0010.KSKJKNED = ChangeToHHMM(d0010.KSKJKNED)
			End If

			'業務登録
			If d0010.FMTKBN = 0 AndAlso ModelState.IsValid AndAlso bolWarning = False Then

                Dim decNewKyukHenkorrkcd As Decimal = GetMaxKyukHenkorrkcd() + 1

                '個別登録
                If d0010.INDIVIDUAL Then

                    Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
                    sqlpara1.Value = Session("LoginUsernm") & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

                    Using tran As DbContextTransaction = db.Database.BeginTransaction
                        Try

                            Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

                            'メール送信用のワークテーブルの初期化
                            Dim lstw0040 = db.W0040.Where(Function(w) w.ACUSERID = loginUserId)
                            If lstw0040.Count > 0 Then
                                For Each item In lstw0040
                                    db.W0040.Remove(item)
                                Next

                                db.SaveChanges()
                            End If

                            'If True Then
                            Dim lstD0020_d0010 As New List(Of D0020)
                            Dim lstD0021_d0010 As New List(Of D0021)

                            If d0010.D0020 IsNot Nothing Then
                                lstD0020_d0010 = d0010.D0020.ToList
                            End If

                            If d0010.D0021 IsNot Nothing Then
                                lstD0021_d0010 = d0010.D0021.ToList
                            End If

                            Dim Cnt_ana_Kari As Integer = lstD0020_d0010.Count + lstD0021_d0010.Count
                            Dim Cnt_Kari As Integer = 0
                            Dim intShorikbn As Integer = 1
                            'Dim Count As Integer = 1

                            For i As Integer = 0 To Cnt_ana_Kari - 1

                                If i < lstD0020_d0010.Count Then
                                    If d0010.D0020 IsNot Nothing Then
                                        d0010.D0020.Clear()
                                    End If
                                    If d0010.D0021 IsNot Nothing Then
                                        d0010.D0021.Clear()
                                    End If
                                    d0010.D0020.Add(lstD0020_d0010(i))
                                Else
                                    If d0010.D0020 IsNot Nothing Then
                                        d0010.D0020.Clear()
                                    End If
                                    If d0010.D0021 IsNot Nothing Then
                                        d0010.D0021.Clear()
                                    End If
                                    d0010.D0021.Add(lstD0021_d0010(Cnt_Kari))
                                    Cnt_Kari = Cnt_Kari + 1
                                End If

                                Dim lstD0010Updt As New List(Of D0010)
                                Dim decNewGyomno As Decimal = 0

                                '業務Noの採番
                                decNewGyomno = GetMaxGyomno() + 1

                                '繰り返し登録なし
                                If d0010.PATTERN = False Then

                                    d0010.JTJKNST = GetJtjkn(d0010.GYOMYMD, d0010.KSKJKNST)
                                    d0010.JTJKNED = GetJtjkn(d0010.GYOMYMDED, d0010.KSKJKNED)

                                    '期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
                                    Dim dtEnd As Date = Date.Parse(d0010.GYOMYMD).AddDays(1)
                                    If d0010.GYOMYMD <> d0010.GYOMYMDED AndAlso d0010.JTJKNED.ToString("yyyy/MM/dd") > d0010.JTJKNST.ToString("yyyy/MM/dd") AndAlso d0010.JTJKNED <> dtEnd Then

                                        AddRnzkGyom(lstD0010Updt, decNewGyomno, d0010.GYOMYMD, d0010.GYOMYMDED, d0010)

                                    Else
                                        '1日のみ登録

                                        Dim d0010New As New D0010
                                        d0010New.GYOMNO = decNewGyomno
                                        d0010New.GYOMYMD = d0010.GYOMYMD
                                        d0010New.GYOMYMDED = d0010.GYOMYMDED
                                        d0010New.KSKJKNST = d0010.KSKJKNST
                                        d0010New.KSKJKNED = d0010.KSKJKNED
                                        d0010New.JTJKNST = d0010.JTJKNST
                                        d0010New.JTJKNED = d0010.JTJKNED

                                        lstD0010Updt.Add(d0010New)
                                    End If

                                Else
                                    '繰り返し登録あり
                                    Dim dtymd As Date = d0010.GYOMYMD

                                    Do While dtymd <= d0010.GYOMYMDED

                                        Dim d0010New As New D0010
                                        Dim bolRnzkDay As Boolean = False       '曜日が連続しているかどうかのフラグ
                                        Dim bolIsUpdtDay As Boolean = False     '指定の曜日かどうかのフラグ
                                        Dim bolNG As Boolean = False

                                        'ASI[24 Oct 2019]:[START] toCheck day is in WeekA or WeekB. And condition to generate date of day in either week.
                                        Dim bolIsWeekDay As Boolean = False
                                        Dim s0010_Record As S0010 = db.S0010.Find(1)

                                        If d0010.WEEKA = True AndAlso Math.Truncate(DateDiff(DateInterval.Day, s0010_Record.ABWEEKSTARTDT, dtymd) / 7) Mod 2 = 0 Then
                                            bolIsWeekDay = True
                                        End If
                                        If d0010.WEEKB = True AndAlso Math.Truncate(DateDiff(DateInterval.Day, s0010_Record.ABWEEKSTARTDT, dtymd) / 7) Mod 2 = 1 Then
                                            bolIsWeekDay = True
                                        End If
                                        If d0010.WEEKA = False AndAlso d0010.WEEKB = False Then
                                            bolIsWeekDay = True
                                        End If

                                        If bolIsWeekDay = True Then 'ASI[24 Oct 2019]:[END]

                                            '曜日が全てチェックOFFの場合は、全ての曜日が登録
                                            If d0010.MON = False AndAlso d0010.TUE = False AndAlso d0010.WED = False AndAlso d0010.TUR = False AndAlso
                                            d0010.FRI = False AndAlso d0010.SAT = False AndAlso d0010.SUN = False Then
                                                bolIsUpdtDay = True
                                                bolRnzkDay = True

                                            Else
                                                If d0010.MON = True AndAlso dtymd.DayOfWeek = DayOfWeek.Monday Then
                                                    bolIsUpdtDay = True
                                                    If d0010.TUE = True Then
                                                        bolRnzkDay = True
                                                    End If
                                                ElseIf d0010.TUE = True AndAlso dtymd.DayOfWeek = DayOfWeek.Tuesday Then
                                                    bolIsUpdtDay = True
                                                    If d0010.WED = True Then
                                                        bolRnzkDay = True
                                                    End If
                                                ElseIf d0010.WED = True AndAlso dtymd.DayOfWeek = DayOfWeek.Wednesday Then
                                                    bolIsUpdtDay = True
                                                    If d0010.TUR = True Then
                                                        bolRnzkDay = True
                                                    End If
                                                ElseIf d0010.TUR = True AndAlso dtymd.DayOfWeek = DayOfWeek.Thursday Then
                                                    bolIsUpdtDay = True
                                                    If d0010.FRI = True Then
                                                        bolRnzkDay = True
                                                    End If
                                                ElseIf d0010.FRI = True AndAlso dtymd.DayOfWeek = DayOfWeek.Friday Then
                                                    bolIsUpdtDay = True
                                                    If d0010.SAT = True Then
                                                        bolRnzkDay = True
                                                    End If
                                                ElseIf d0010.SAT = True AndAlso dtymd.DayOfWeek = DayOfWeek.Saturday Then
                                                    bolIsUpdtDay = True
                                                    If d0010.SUN = True Then
                                                        bolRnzkDay = True
                                                    End If
                                                ElseIf d0010.SUN = True AndAlso dtymd.DayOfWeek = DayOfWeek.Sunday Then
                                                    bolIsUpdtDay = True
                                                    If d0010.MON = True Then
                                                        bolRnzkDay = True
                                                    End If
                                                End If
                                            End If
                                        End If

                                        If bolIsUpdtDay = True Then

                                            d0010New.GYOMYMD = dtymd

                                            '開始時間 > 終了時間の場合、開始日+1
                                            If d0010.KSKJKNST > d0010.KSKJKNED Then
                                                d0010New.GYOMYMDED = dtymd.AddDays(1)
                                            Else
                                                '開始時間 <= 終了時間
                                                d0010New.GYOMYMDED = dtymd
                                            End If

                                            d0010New.JTJKNST = GetJtjkn(d0010New.GYOMYMD, d0010.KSKJKNST)
                                            d0010New.JTJKNED = GetJtjkn(d0010New.GYOMYMDED, d0010.KSKJKNED)

                                            '終了日が対象業務期間の終了日を超えていないかチェック
                                            If d0010New.GYOMYMDED > d0010.GYOMYMDED Then
                                                bolNG = True

                                                '実時間前後関係チェック
                                            ElseIf d0010New.JTJKNST > d0010New.JTJKNED Then
                                                bolNG = True

                                            End If

                                            If bolNG = False Then
                                                '期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
                                                Dim dtEnd As Date = Date.Parse(d0010New.GYOMYMD).AddDays(1)
                                                If d0010New.GYOMYMD <> d0010New.GYOMYMDED AndAlso d0010New.JTJKNED.ToString("yyyy/MM/dd") > d0010New.JTJKNST.ToString("yyyy/MM/dd") AndAlso d0010New.JTJKNED <> dtEnd Then

                                                    AddRnzkGyom(lstD0010Updt, decNewGyomno, d0010New.GYOMYMD, d0010New.GYOMYMDED, d0010)

                                                Else
                                                    '1日登録

                                                    d0010New.GYOMNO = decNewGyomno
                                                    d0010New.KSKJKNST = d0010.KSKJKNST
                                                    d0010New.KSKJKNED = d0010.KSKJKNED

                                                    lstD0010Updt.Add(d0010New)

                                                    decNewGyomno += 1
                                                End If

                                            End If

                                        End If

                                        dtymd = dtymd.AddDays(1)

                                    Loop

                                End If

                                If lstD0010Updt.Count > 0 Then

                                    Dim dtNow As Date = Now
                                    Dim d0050 As D0050 = Nothing
                                    '「業務承認」からのデータを登録する場合、業務の「申請者」が「担当アナウンサー」と一致するときは承認フラグに１：承認、そうでない場合は２：却下を設定
                                    If d0010.GYOMSNSNO > 0 Then
                                        Dim bolExist As Boolean = False
                                        d0050 = db.D0050.Find(d0010.GYOMSNSNO)
                                        For Each d0020 In lstD0020_d0010
                                            If d0020.USERID = d0050.USERID Then
                                                bolExist = True
                                                Exit For
                                            End If
                                        Next
                                        If bolExist Then
                                            d0050.SHONINFLG = "1"
                                            intShorikbn = 4
                                        Else
                                            d0050.SHONINFLG = "2"
                                            intShorikbn = 5
                                        End If
                                    End If

                                    Dim decNewHenkorrkcd As Decimal = GetMaxHenkorrkcd() + 1
                                    Dim lstD0020 As New List(Of D0020)
                                    Dim lstD0021 As New List(Of D0021)

                                    If d0010.D0020 IsNot Nothing Then
                                        lstD0020 = d0010.D0020.ToList
                                    End If
                                    If d0010.D0021 IsNot Nothing Then
                                        lstD0021 = d0010.D0021.ToList
                                    End If

                                    Dim strAllAnanm As String = GetAllAnanm(lstD0020, lstD0021)

                                    For Each d0010updt In lstD0010Updt

                                        '業務登録
                                        CopyValue(d0010updt, d0010)
                                        db.D0010.Add(d0010updt)


                                        '担当アナを登録。それ以外は業務日ごとに担当アナを登録。ただし、連続登録の場合の子業務は除く。
                                        If d0010updt.PGYOMNO Is Nothing Then
                                            '担当アナ
                                            If d0010.D0020 IsNot Nothing Then
                                                For Each item In d0010.D0020
                                                    Dim d0020New As New D0020
                                                    d0020New.GYOMNO = d0010updt.GYOMNO
                                                    d0020New.USERID = item.USERID

                                                    'ASI[06 Jan 2020] : insert value in D0020 from d0010updt object instend of d0010 object
                                                    d0020New.GYOMYMD = d0010updt.GYOMYMD
                                                    d0020New.GYOMYMDED = d0010updt.GYOMYMDED
                                                    d0020New.KSKJKNST = d0010updt.KSKJKNST
                                                    d0020New.KSKJKNED = d0010updt.KSKJKNED
                                                    d0020New.JTJKNST = d0010updt.JTJKNST
                                                    d0020New.JTJKNED = d0010updt.JTJKNED
                                                    db.D0020.Add(d0020New)
                                                Next
                                            End If
                                            '仮アナ
                                            If d0010.D0021 IsNot Nothing Then
                                                Dim intSeq As Integer = 1
                                                For Each item In d0010.D0021
                                                    If String.IsNullOrEmpty(item.ANNACATNM) = False Then
                                                        Dim d0021New As New D0021
                                                        d0021New.GYOMNO = d0010updt.GYOMNO
                                                        d0021New.SEQ = intSeq
                                                        d0021New.ANNACATNM = item.ANNACATNM
                                                        db.D0021.Add(d0021New)
                                                        intSeq += 1
                                                    End If
                                                Next
                                            End If


                                            '変更履歴の作成（親業務のみ作成）
                                            Dim d0070 As New D0070
                                            d0070.HENKORRKCD = decNewHenkorrkcd
                                            d0070.HENKONAIYO = "登録"
                                            d0070.USERID = d0010.ACUSERID
                                            d0070.SYUSEIYMD = dtNow
                                            d0070.TNTNM = strAllAnanm
                                            CopyHenkorrk(d0070, d0010)
                                            db.D0070.Add(d0070)

                                            decNewHenkorrkcd += 1

                                        End If

                                    Next

                                    'メール送信用にデータを作成
                                    If i = 0 AndAlso lstD0020_d0010.Count > 0 Then
                                        Dim w0040 As New W0040
                                        w0040.ACUSERID = loginUserId
                                        w0040.SHORIKBN = intShorikbn
                                        w0040.GYOMNO = lstD0010Updt(0).GYOMNO
                                        w0040.UPDTDT = dtNow
                                        CopyGyom(w0040, d0010)
                                        db.W0040.Add(w0040)

                                        For Each d0020 In lstD0020_d0010
                                            Dim w0050 As New W0050
                                            w0050.ACUSERID = w0040.ACUSERID
                                            w0050.SHORIKBN = w0040.SHORIKBN
                                            w0050.GYOMNO = w0040.GYOMNO
                                            CopyAna(w0050, d0020)
                                            db.W0050.Add(w0050)
                                        Next

                                        '却下の場合、申請者もメール送信対象にする
                                        If intShorikbn = 5 Then
                                            Dim w0050 As New W0050
                                            w0050.ACUSERID = w0040.ACUSERID
                                            w0050.SHORIKBN = w0040.SHORIKBN
                                            w0050.GYOMNO = w0040.GYOMNO
                                            w0050.USERID = d0050.USERID
                                            w0050.DELFLG = True
                                            db.W0050.Add(w0050)
                                        End If
                                    End If

                                    'ASI[25 Dec 2019]:Wブッキングは現状のまま。休日に関し、選択した時点では休日更新しない。当業務を更新する時に一緒に更新する。
                                    UpdateYoinUser(decNewKyukHenkorrkcd, d0010)

                                    '雛形、下書呼び出しで登録する場合、雛形テーブルを更新
                                    If d0010.HINANO > 0 Then
                                        Dim d0090 = db.D0090.Find(d0010.HINANO)
                                        If d0090.FMTKBN = 1 Then
                                            '「下書」を呼び出している場合：ステータスに “２”を設定																			
                                            d0090.STATUS = 2
                                            d0090.SIYOUSERID = loginUserId
                                            d0090.SIYODATE = Now
                                        ElseIf d0090.FMTKBN = 2 Then
                                            '「雛形」を呼び出している場合：使用済み区分に“１”を設定
                                            d0090.SIYOFLG = True
                                            d0090.SIYOUSERID = loginUserId
                                            d0090.SIYODATE = Now
                                        End If

                                        '「デスクメモ」からのデータを登録する場合、デスクメモの「確認」を「5:済み」に変更する。
                                    ElseIf d0010.DESKNO > 0 Then
                                        Dim d0110 = db.D0110.Find(d0010.DESKNO)
                                        d0110.KAKUNINID = 5
                                    End If


                                    'DoDBSaveChanges(db)

                                    db.Configuration.ValidateOnSaveEnabled = False
                                    db.SaveChanges()
                                    db.Configuration.ValidateOnSaveEnabled = True

                                Else

                                    ModelState.AddModelError(String.Empty, "対象が一件もないため、業務が一件も登録されませんでした。")

                                End If

                            Next

                            tran.Commit()

                            d0010.D0020 = lstD0020_d0010
                            d0010.D0021 = lstD0021_d0010
                            If lstD0020_d0010.Count > 0 Then
                                Return RedirectToAction("SendMail", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn})
                            End If

                            '戻る
                            If bolCopy Then
                                Return RedirectToAction("Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
                            .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
                            .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
                            .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
                            .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk")})
                            Else
                                If String.IsNullOrEmpty(Session("B0020CreateRtnUrl")) = False Then
                                    Return Redirect(Session("B0020CreateRtnUrl"))
                                Else
                                    Return RedirectToAction("Create")
                                End If
                            End If

                        Catch ex As Exception
                            Throw ex
                            tran.Rollback()
                        End Try

                    End Using

                Else
                    '個別ではない登録

                    Dim lstD0010Updt As New List(Of D0010)
					Dim decNewGyomno As Decimal = 0

					'業務Noの採番
					decNewGyomno = GetMaxGyomno() + 1

					'繰り返し登録なし
					If d0010.PATTERN = False Then

						d0010.JTJKNST = GetJtjkn(d0010.GYOMYMD, d0010.KSKJKNST)
						d0010.JTJKNED = GetJtjkn(d0010.GYOMYMDED, d0010.KSKJKNED)

						'期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
						Dim dtEnd As Date = Date.Parse(d0010.GYOMYMD).AddDays(1)
						If d0010.GYOMYMD <> d0010.GYOMYMDED AndAlso d0010.JTJKNED.ToString("yyyy/MM/dd") > d0010.JTJKNST.ToString("yyyy/MM/dd") AndAlso d0010.JTJKNED <> dtEnd Then

							AddRnzkGyom(lstD0010Updt, decNewGyomno, d0010.GYOMYMD, d0010.GYOMYMDED, d0010)

						Else
							'1日のみ登録

							Dim d0010New As New D0010
							d0010New.GYOMNO = decNewGyomno
							d0010New.GYOMYMD = d0010.GYOMYMD
							d0010New.GYOMYMDED = d0010.GYOMYMDED
							d0010New.KSKJKNST = d0010.KSKJKNST
							d0010New.KSKJKNED = d0010.KSKJKNED
							d0010New.JTJKNST = d0010.JTJKNST
							d0010New.JTJKNED = d0010.JTJKNED

							lstD0010Updt.Add(d0010New)
						End If

					Else
						'繰り返し登録あり
						Dim dtymd As Date = d0010.GYOMYMD

						Do While dtymd <= d0010.GYOMYMDED

							Dim d0010New As New D0010
							Dim bolRnzkDay As Boolean = False       '曜日が連続しているかどうかのフラグ
							Dim bolIsUpdtDay As Boolean = False     '指定の曜日かどうかのフラグ
							Dim bolNG As Boolean = False

							'ASI[24 Oct 2019]:[START] toCheck day is in WeekA or WeekB. And condition to generate date of day in either week.
							Dim bolIsWeekDay As Boolean = False
							Dim s0010_Record As S0010 = db.S0010.Find(1)

							If d0010.WEEKA = True AndAlso Math.Truncate(DateDiff(DateInterval.Day, s0010_Record.ABWEEKSTARTDT, dtymd) / 7) Mod 2 = 0 Then
								bolIsWeekDay = True
							End If
							If d0010.WEEKB = True AndAlso Math.Truncate(DateDiff(DateInterval.Day, s0010_Record.ABWEEKSTARTDT, dtymd) / 7) Mod 2 = 1 Then
								bolIsWeekDay = True
							End If
							If d0010.WEEKA = False AndAlso d0010.WEEKB = False Then
								bolIsWeekDay = True
							End If

							If bolIsWeekDay = True Then 'ASI[24 Oct 2019]:[END]

								'曜日が全てチェックOFFの場合は、全ての曜日が登録
								If d0010.MON = False AndAlso d0010.TUE = False AndAlso d0010.WED = False AndAlso d0010.TUR = False AndAlso
								d0010.FRI = False AndAlso d0010.SAT = False AndAlso d0010.SUN = False Then
									bolIsUpdtDay = True
									bolRnzkDay = True

								Else
									If d0010.MON = True AndAlso dtymd.DayOfWeek = DayOfWeek.Monday Then
										bolIsUpdtDay = True
										If d0010.TUE = True Then
											bolRnzkDay = True
										End If
									ElseIf d0010.TUE = True AndAlso dtymd.DayOfWeek = DayOfWeek.Tuesday Then
										bolIsUpdtDay = True
										If d0010.WED = True Then
											bolRnzkDay = True
										End If
									ElseIf d0010.WED = True AndAlso dtymd.DayOfWeek = DayOfWeek.Wednesday Then
										bolIsUpdtDay = True
										If d0010.TUR = True Then
											bolRnzkDay = True
										End If
									ElseIf d0010.TUR = True AndAlso dtymd.DayOfWeek = DayOfWeek.Thursday Then
										bolIsUpdtDay = True
										If d0010.FRI = True Then
											bolRnzkDay = True
										End If
									ElseIf d0010.FRI = True AndAlso dtymd.DayOfWeek = DayOfWeek.Friday Then
										bolIsUpdtDay = True
										If d0010.SAT = True Then
											bolRnzkDay = True
										End If
									ElseIf d0010.SAT = True AndAlso dtymd.DayOfWeek = DayOfWeek.Saturday Then
										bolIsUpdtDay = True
										If d0010.SUN = True Then
											bolRnzkDay = True
										End If
									ElseIf d0010.SUN = True AndAlso dtymd.DayOfWeek = DayOfWeek.Sunday Then
										bolIsUpdtDay = True
										If d0010.MON = True Then
											bolRnzkDay = True
										End If
									End If
								End If
							End If

							If bolIsUpdtDay = True Then

								d0010New.GYOMYMD = dtymd

								'開始時間 > 終了時間の場合、開始日+1
								If d0010.KSKJKNST > d0010.KSKJKNED Then
									d0010New.GYOMYMDED = dtymd.AddDays(1)
								Else
									'開始時間 <= 終了時間
									d0010New.GYOMYMDED = dtymd
								End If

								d0010New.JTJKNST = GetJtjkn(d0010New.GYOMYMD, d0010.KSKJKNST)
								d0010New.JTJKNED = GetJtjkn(d0010New.GYOMYMDED, d0010.KSKJKNED)

								'終了日が対象業務期間の終了日を超えていないかチェック
								If d0010New.GYOMYMDED > d0010.GYOMYMDED Then
									bolNG = True

									'実時間前後関係チェック
								ElseIf d0010New.JTJKNST > d0010New.JTJKNED Then
									bolNG = True

								End If

								If bolNG = False Then
									'期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
									Dim dtEnd As Date = Date.Parse(d0010New.GYOMYMD).AddDays(1)
									If d0010New.GYOMYMD <> d0010New.GYOMYMDED AndAlso d0010New.JTJKNED.ToString("yyyy/MM/dd") > d0010New.JTJKNST.ToString("yyyy/MM/dd") AndAlso d0010New.JTJKNED <> dtEnd Then

										AddRnzkGyom(lstD0010Updt, decNewGyomno, d0010New.GYOMYMD, d0010New.GYOMYMDED, d0010)

									Else
										'1日登録

										d0010New.GYOMNO = decNewGyomno
										d0010New.KSKJKNST = d0010.KSKJKNST
										d0010New.KSKJKNED = d0010.KSKJKNED

										lstD0010Updt.Add(d0010New)

										decNewGyomno += 1
									End If

								End If

							End If

							dtymd = dtymd.AddDays(1)

						Loop

					End If

					If lstD0010Updt.Count > 0 Then

						Dim intShorikbn As Integer = 1
						Dim dtNow As Date = Now
                        Dim d0050 As D0050 = Nothing

                        '「業務承認」からのデータを登録する場合、業務の「申請者」が「担当アナウンサー」と一致するときは承認フラグに１：承認、そうでない場合は２：却下を設定
                        If d0010.GYOMSNSNO > 0 Then
							d0050 = db.D0050.Find(d0010.GYOMSNSNO)
							Dim lstTntAna = d0010.D0020.Where(Function(d) d.USERID = d0050.USERID).ToList
							If lstTntAna.Count > 0 Then
								d0050.SHONINFLG = "1"
								intShorikbn = 4
							Else
								d0050.SHONINFLG = "2"
								intShorikbn = 5
							End If
						End If

                        'メール送信用のワークテーブルの初期化
                        Dim lstw0040 = db.W0040.Where(Function(w) w.ACUSERID = loginUserId)
                        If lstw0040.Count > 0 Then
							For Each item In lstw0040
								db.W0040.Remove(item)
							Next

							db.SaveChanges()
						End If

                        Dim decNewHenkorrkcd As Decimal = GetMaxHenkorrkcd() + 1
						Dim lstD0020 As New List(Of D0020)
						Dim lstD0021 As New List(Of D0021)

						If d0010.D0020 IsNot Nothing Then
							lstD0020 = d0010.D0020.ToList
						End If
						If d0010.D0021 IsNot Nothing Then
							lstD0021 = d0010.D0021.ToList
						End If

						Dim strAllAnanm As String = GetAllAnanm(lstD0020, lstD0021)

						For Each d0010updt In lstD0010Updt

							'業務登録
							CopyValue(d0010updt, d0010)
							db.D0010.Add(d0010updt)


							'担当アナを登録。それ以外は業務日ごとに担当アナを登録。ただし、連続登録の場合の子業務は除く。
							If d0010updt.PGYOMNO Is Nothing Then
								'担当アナ
								If d0010.D0020 IsNot Nothing Then
									For Each item In d0010.D0020
										Dim d0020New As New D0020
										d0020New.GYOMNO = d0010updt.GYOMNO
										d0020New.USERID = item.USERID

										'ASI[06 Jan 2020] : insert value in D0020 from d0010updt object instend of d0010 object
										d0020New.GYOMYMD = d0010updt.GYOMYMD
										d0020New.GYOMYMDED = d0010updt.GYOMYMDED
										d0020New.KSKJKNST = d0010updt.KSKJKNST
										d0020New.KSKJKNED = d0010updt.KSKJKNED
										d0020New.JTJKNST = d0010updt.JTJKNST
										d0020New.JTJKNED = d0010updt.JTJKNED
										db.D0020.Add(d0020New)
									Next
								End If
								'仮アナ
								If d0010.D0021 IsNot Nothing Then
									Dim intSeq As Integer = 1
									For Each item In d0010.D0021
										If String.IsNullOrEmpty(item.ANNACATNM) = False Then
											Dim d0021New As New D0021
											d0021New.GYOMNO = d0010updt.GYOMNO
											d0021New.SEQ = intSeq
											d0021New.ANNACATNM = item.ANNACATNM
											db.D0021.Add(d0021New)
											intSeq += 1
										End If
									Next
								End If


								'変更履歴の作成（親業務のみ作成）
								Dim d0070 As New D0070
								d0070.HENKORRKCD = decNewHenkorrkcd
								d0070.HENKONAIYO = "登録"
								d0070.USERID = d0010.ACUSERID
								d0070.SYUSEIYMD = dtNow
								d0070.TNTNM = strAllAnanm
								CopyHenkorrk(d0070, d0010)
								db.D0070.Add(d0070)

								decNewHenkorrkcd += 1


								'メール送信用にデータを作成
								If lstD0020.Count > 0 Then
									Dim w0040 As New W0040
									w0040.ACUSERID = loginUserId
									w0040.SHORIKBN = intShorikbn
									w0040.GYOMNO = d0010updt.GYOMNO
									w0040.UPDTDT = dtNow
									CopyGyom(w0040, d0010)
									db.W0040.Add(w0040)

									For Each d0020 In d0010.D0020
										Dim w0050 As New W0050
										w0050.ACUSERID = w0040.ACUSERID
										w0050.SHORIKBN = w0040.SHORIKBN
										w0050.GYOMNO = w0040.GYOMNO
										CopyAna(w0050, d0020)
										db.W0050.Add(w0050)
									Next

									'却下の場合、申請者もメール送信対象にする
									If intShorikbn = 5 Then
										Dim w0050 As New W0050
										w0050.ACUSERID = w0040.ACUSERID
										w0050.SHORIKBN = w0040.SHORIKBN
										w0050.GYOMNO = w0040.GYOMNO
										w0050.USERID = d0050.USERID
										w0050.DELFLG = True
										db.W0050.Add(w0050)
									End If
								End If

							End If

						Next

						'ASI[25 Dec 2019]:Wブッキングは現状のまま。休日に関し、選択した時点では休日更新しない。当業務を更新する時に一緒に更新する。
						UpdateYoinUser(decNewKyukHenkorrkcd, d0010)

						'If d0010.D0020 IsNot Nothing Then
						'	For Each item In d0010.D0020

						'		Dim lstkyukdata = (From w0020 In db.W0020 Join d0040 In db.D0040
						'								On w0020.USERID Equals d0040.USERID And w0020.NENGETU Equals d0040.NENGETU And
						'								w0020.HI Equals d0040.HI And w0020.JKNST Equals d0040.JKNST
						'								Where w0020.ACUSERID = d0010.ACUSERID And w0020.USERID = item.USERID).ToList

						'		For Each row In lstkyukdata
						'			'時間休
						'			If row.w0020.YOINID = 3 Then
						'				db.D0040.Remove(row.d0040)

						'				'公休→休出
						'			ElseIf row.w0020.YOINID = 5 Then
						'				row.d0040.KYUKCD = 2

						'				'24時間超え休出公休→24時間超え休出
						'			ElseIf row.w0020.YOINID = 11 Then
						'				row.d0040.KYUKCD = 10

						'				'代休→出勤、振替休→出勤
						'			ElseIf row.w0020.YOINID = 6 OrElse row.w0020.YOINID = 7 Then
						'				db.D0040.Remove(row.d0040)

						'			End If
						'		Next

						'		'Wブッキングの解除
						'		Dim lstgyomdata = (From w0030 In db.W0030 Join d0020 In db.D0020
						'							On w0030.GYOMNO Equals d0020.GYOMNO And w0030.USERID Equals d0020.USERID
						'							 Where w0030.ACUSERID = d0010.ACUSERID And w0030.USERID = item.USERID).ToList

						'		For Each row In lstgyomdata
						'			db.D0020.Remove(row.d0020)
						'		Next

						'	Next
						'End If


						'雛形、下書呼び出しで登録する場合、雛形テーブルを更新
						If d0010.HINANO > 0 Then
							Dim d0090 = db.D0090.Find(d0010.HINANO)
							If d0090.FMTKBN = 1 Then
								'「下書」を呼び出している場合：ステータスに “２”を設定																			
								d0090.STATUS = 2
								d0090.SIYOUSERID = loginUserId
								d0090.SIYODATE = Now
							ElseIf d0090.FMTKBN = 2 Then
								'「雛形」を呼び出している場合：使用済み区分に“１”を設定
								d0090.SIYOFLG = True
								d0090.SIYOUSERID = loginUserId
								d0090.SIYODATE = Now
							End If

							'「デスクメモ」からのデータを登録する場合、デスクメモの「確認」を「5:済み」に変更する。
						ElseIf d0010.DESKNO > 0 Then
							Dim d0110 = db.D0110.Find(d0010.DESKNO)
							d0110.KAKUNINID = 5
						End If


						DoDBSaveChanges(db)


						If lstD0020.Count > 0 Then
							Return RedirectToAction("SendMail", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn})
						End If

						'戻る
						If bolCopy Then
							Return RedirectToAction("Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
								.PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
								.Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
								.CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
								.Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk")})
						Else
							If String.IsNullOrEmpty(Session("B0020CreateRtnUrl")) = False Then
								Return Redirect(Session("B0020CreateRtnUrl"))
							Else
								Return RedirectToAction("Create")
							End If
						End If

					Else

						ModelState.AddModelError(String.Empty, "対象が一件もないため、業務が一件も登録されませんでした。")

					End If
				End If



				'下書・雛形保存
			ElseIf d0010.FMTKBN = 1 OrElse d0010.FMTKBN = 2 Then

				Dim d0090 As New D0090
				d0090.FMTKBN = d0010.FMTKBN
				d0090.GYOMYMD = d0010.GYOMYMD
				d0090.GYOMYMDED = d0010.GYOMYMDED
				d0090.KSKJKNST = d0010.KSKJKNST
				d0090.KSKJKNED = d0010.KSKJKNED
				d0090.CATCD = d0010.CATCD
				d0090.BANGUMINM = d0010.BANGUMINM
				d0090.OAJKNST = d0010.OAJKNST
				d0090.OAJKNED = d0010.OAJKNED
				d0090.NAIYO = d0010.NAIYO
				d0090.BASYO = d0010.BASYO
				d0090.BIKO = d0010.BIKO
				d0090.BANGUMITANTO = d0010.BANGUMITANTO
				d0090.BANGUMIRENRK = d0010.BANGUMIRENRK
				d0090.PTNFLG = d0010.PATTERN
				d0090.PTN1 = d0010.MON
				d0090.PTN2 = d0010.TUE
				d0090.PTN3 = d0010.WED
				d0090.PTN4 = d0010.TUR
				d0090.PTN5 = d0010.FRI
				d0090.PTN6 = d0010.SAT
				d0090.PTN7 = d0010.SUN
				d0090.DATAKBN = d0010.DATAKBN
				d0090.HINAMEMO = d0010.HINAMEMO
				d0090.SIYOFLG = 0
				d0090.SIYOUSERID = loginUserId
				d0090.STATUS = 0
				'ASI[24 Oct 2019]
				d0090.WEEKA = d0010.WEEKA
				d0090.WEEKB = d0010.WEEKB

				Dim decTempHINANO As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "0000")
				Dim lstHINANO = (From t In db.D0090 Where t.HINANO > decTempHINANO Select t.HINANO).ToList
				If lstHINANO.Count > 0 Then
					decTempHINANO = lstHINANO.Max
				End If
				d0090.HINANO = decTempHINANO + 1

				db.D0090.Add(d0090)

				If String.IsNullOrEmpty(d0010.ANAIDLIST) = False Then
					If d0010.ANAIDLIST.Contains("，") Then
						Dim strAnaIds As String() = d0010.ANAIDLIST.Split("，")
						For Each strId In strAnaIds
							Dim d0100 As New D0100
							d0100.HINANO = d0090.HINANO
							d0100.USERID = strId
							db.D0100.Add(d0100)
						Next
					Else
						Dim d0100 As New D0100
						d0100.HINANO = d0090.HINANO
						d0100.USERID = d0010.ANAIDLIST
						db.D0100.Add(d0100)
					End If
				End If

				If String.IsNullOrEmpty(d0010.KARIANALIST) = False Then
					If d0010.KARIANALIST.Contains("，") Then
						Dim strAnaIds As String() = d0010.KARIANALIST.Split("，")
						Dim intSeq As Integer = 1
						For Each strAnaCat In strAnaIds
							Dim d0101 As New D0101
							d0101.HINANO = d0090.HINANO
							d0101.SEQ = intSeq
							d0101.ANNACATNM = Trim(strAnaCat.Replace(vbCrLf, " "))
							db.D0101.Add(d0101)
							intSeq += 1
						Next
					Else
						Dim d0101 As New D0101
						d0101.HINANO = d0090.HINANO
						d0101.SEQ = 1
						d0101.ANNACATNM = Trim(d0010.KARIANALIST.Replace(vbCrLf, " "))
						db.D0101.Add(d0101)
					End If
				End If


				If DoDBSaveChanges(db) Then
					Dim strfmt As String = ""
					If d0010.FMTKBN = 1 Then
						strfmt = "下書"
					Else
						strfmt = "雛形"
					End If

					TempData("success") = String.Format("{0}が新規に保存されました。", strfmt)
				End If

				Return RedirectToAction("Create")
			End If

			Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim item1 As New M0020
			item1.CATCD = "0"
			item1.CATNM = ""
			lstm0020.Insert(0, item1)
			ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM")

			ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO")

			Dim lstbangumi = db.M0030.OrderBy(Function(m) m.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
			Dim anacatitem As New M0080
			anacatitem.ANNACATNO = "0"
			anacatitem.ANNACATNM = ""
			lstKarianacat.Insert(0, anacatitem)
			ViewBag.KarianacatList = lstKarianacat

			'休出
			ViewBag.KyukDe = db.M0060.Find(2)

			'ASI[23 Oct 2019]:法出
			ViewBag.KyukHouDe = db.M0060.Find(13)

			Return View(d0010)
		End Function

		Private Sub AddRnzkGyom(ByRef lstD0010Updt As List(Of D0010), ByRef decNewGyomno As Decimal, ByVal dtSt As Date, ByVal dtEd As Date, ByVal d0010 As D0010)

			Dim dtymd As Date = dtSt
			Dim decPgyomno As Decimal = decNewGyomno

			Do While dtymd <= dtEd
				Dim d0010New As New D0010

				d0010New.GYOMNO = decNewGyomno
				d0010New.RNZK = True

				If dtymd = dtSt Then
					'親業務
					d0010New.PGYOMNO = Nothing
					d0010New.GYOMYMD = dtSt
					d0010New.GYOMYMDED = dtEd
					d0010New.KSKJKNST = d0010.KSKJKNST
					d0010New.KSKJKNED = d0010.KSKJKNED

				Else
					'子業務
					d0010New.PGYOMNO = decPgyomno
					d0010New.GYOMYMD = dtymd
					d0010New.GYOMYMDED = dtymd

					If dtymd < dtEd Then
						'中の日
						d0010New.KSKJKNST = "0000"
						d0010New.KSKJKNED = "2400"
					Else
						'終了日
						d0010New.KSKJKNST = "0000"
						d0010New.KSKJKNED = d0010.KSKJKNED
					End If

				End If

				d0010New.JTJKNST = GetJtjkn(d0010New.GYOMYMD, d0010New.KSKJKNST)
				d0010New.JTJKNED = GetJtjkn(d0010New.GYOMYMDED, d0010New.KSKJKNED)

				lstD0010Updt.Add(d0010New)

				decNewGyomno += 1

				dtymd = dtymd.AddDays(1)

			Loop

		End Sub

		Private Sub AddRnzkGyom(ByRef lstD0010Updt As List(Of D0010), ByVal dtSt As Date, ByVal dtEd As Date, ByVal kskjknst As String, ByVal kskjkned As String)

			Dim dtymd As Date = dtSt

			Do While dtymd <= dtEd
				Dim d0010New As New D0010

				If dtymd = dtSt Then
					'親業務
					d0010New.GYOMYMD = dtSt
					d0010New.GYOMYMDED = dtSt
					d0010New.KSKJKNST = kskjknst
					d0010New.KSKJKNED = "2400"

				Else
					'子業務
					d0010New.GYOMYMD = dtymd
					d0010New.GYOMYMDED = dtymd

					If dtymd < dtEd Then
						'中の日
						d0010New.KSKJKNST = "0000"
						d0010New.KSKJKNED = "2400"
					Else
						'終了日
						d0010New.KSKJKNST = "0000"
						d0010New.KSKJKNED = kskjkned
					End If

				End If

				d0010New.JTJKNST = GetJtjkn(d0010New.GYOMYMD, d0010New.KSKJKNST)
				d0010New.JTJKNED = GetJtjkn(d0010New.GYOMYMDED, d0010New.KSKJKNED)

				lstD0010Updt.Add(d0010New)

				dtymd = dtymd.AddDays(1)

			Loop

		End Sub


		' 候補検索
		'<ChildActionOnly>
		<OutputCache(Duration:=0)>
		Function SearchKOHO(ByVal gyomno As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal kskjknst As String, ByVal kskjkned As String,
							ByVal pattern As Boolean, ByVal mon As Boolean, ByVal tue As Boolean, ByVal wed As Boolean, ByVal tur As Boolean, ByVal fri As Boolean,
							ByVal sat As Boolean, ByVal sun As Boolean, ByVal weekA As Boolean, ByVal weekB As Boolean) As ActionResult


			If Request.IsAjaxRequest() OrElse TempData("SearchKOHO") = True Then

				'*0:不適合要因無し 1:超過勤務 2: 前後日10時間未満 3:時間休 4:当日時間休あり(業務期間と重ならないもの)
				' 5:公休 6:代休 7:振替休 8:強休 9:時間強休 10:当日時間強休あり 11: 24時超え休出 12: Wブッキング
				Dim intAcuserid As Integer = Session("LoginUserid")

				If gyomymded Is Nothing OrElse String.IsNullOrEmpty(gyomymded) Then
					gyomymded = gyomymd
				End If

				Dim dtgyomymd As Date = Date.Parse(gyomymd)
				Dim dtgyomymded As Date = Date.Parse(gyomymded)

				CreateKohoData(intAcuserid, gyomno, dtgyomymd, dtgyomymded, kskjknst, kskjkned, pattern, mon, tue, wed, tur, fri, sat, sun, weekA, weekB)

				Dim w0010 = db.W0010.OrderBy(Function(f) f.M00101.USERSEX).ThenBy(Function(f) f.M00101.HYOJJN)

				ViewBag.Futekinashi = (From t In w0010 Where t.YOINID = 0 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Chokakinmu = (From t In w0010 Where t.YOINID = 1 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Zengo10ji = (From t In w0010 Where t.YOINID = 2 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Jikankyu = (From t In w0010 Where t.YOINID = 3 And t.ACUSERID = intAcuserid).ToList
				ViewBag.TouJikankyu = (From t In w0010 Where t.YOINID = 4 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Koukyu = (From t In w0010 Where t.YOINID = 5 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Daikyu = (From t In w0010 Where t.YOINID = 6 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Furikyu = (From t In w0010 Where t.YOINID = 7 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Kyokyu = (From t In w0010 Where t.YOINID = 8 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Jikyokyu = (From t In w0010 Where t.YOINID = 9 And t.ACUSERID = intAcuserid).ToList
				ViewBag.TouJikyokyu = (From t In w0010 Where t.YOINID = 10 And t.ACUSERID = intAcuserid).ToList
				ViewBag.koekyude = (From t In w0010 Where t.YOINID = 11 And t.ACUSERID = intAcuserid).ToList
				ViewBag.WBooking = (From t In w0010 Where t.YOINID = 12 And t.ACUSERID = intAcuserid).ToList

				'ASI[17 Oct 2019]:Fetch employee under these 3 newly added leave and put it in ViewBag
				ViewBag.Houkyu = (From t In w0010 Where t.YOINID = 13 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Furikaehoukyu = (From t In w0010 Where t.YOINID = 14 And t.ACUSERID = intAcuserid).ToList
				ViewBag.Koehoukyu24 = (From t In w0010 Where t.YOINID = 15 And t.ACUSERID = intAcuserid).ToList

				ViewBag.Gyomymd = gyomymd
				ViewBag.Gyomymded = gyomymded

				'休出
				ViewBag.KyukDe = db.M0060.Find(2)
				'ASI[23 Oct 2019]:法出
				ViewBag.KyukHouDe = db.M0060.Find(13)

				If TempData("SearchKOHO") = False Then
					Return PartialView("_D0010_AnalistPartial")
				End If

			End If

			Return New EmptyResult
		End Function

		Private Sub CreateKohoData(ByVal intAcuserid As Integer, ByVal gyomno As String, ByVal dtGyomymd As Date, ByVal dtGyomymded As Date, ByVal strKskjknst As String, ByVal strKskjkned As String,
							ByVal bolPattern As Boolean, ByVal bolMon As Boolean, ByVal bolTue As Boolean, ByVal bolWed As Boolean, ByVal bolTur As Boolean,
							ByVal bolFri As Boolean, ByVal bolSat As Boolean, ByVal bolSun As Boolean, ByVal bolWeekA As Boolean, ByVal bolWeekB As Boolean)

			Dim lstD0010Updt As New List(Of D0010)

			strKskjknst = ChangeToHHMM(strKskjknst)
			strKskjkned = ChangeToHHMM(strKskjkned)

			'繰り返し登録なし
			If bolPattern = False Then

				Dim dtJTJKNST As Date = GetJtjkn(dtGyomymd, strKskjknst)
				Dim dtJTJKNED As Date = GetJtjkn(dtGyomymded, strKskjkned)

				'期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
				Dim dtEnd As Date = Date.Parse(dtGyomymd).AddDays(1)
				If dtGyomymd <> dtGyomymded AndAlso dtJTJKNED.ToString("yyyy/MM/dd") > dtJTJKNST.ToString("yyyy/MM/dd") AndAlso dtJTJKNED <> dtEnd Then

					'期間が2日以上の場合、連続登録
					AddRnzkGyom(lstD0010Updt, dtGyomymd, dtGyomymded, strKskjknst, strKskjkned)

				Else
					'1日のみ登録

					Dim d0010New As New D0010
					d0010New.GYOMYMD = dtGyomymd
					d0010New.GYOMYMDED = dtGyomymded
					d0010New.KSKJKNST = strKskjknst
					d0010New.KSKJKNED = strKskjkned
					d0010New.JTJKNST = dtJTJKNST
					d0010New.JTJKNED = dtJTJKNED

					lstD0010Updt.Add(d0010New)
				End If

			Else
				'繰り返し登録あり
				Dim dtymd As Date = dtGyomymd

				Do While dtymd <= dtGyomymded

					Dim d0010New As New D0010
					Dim bolRnzkDay As Boolean = False       '曜日が連続しているかどうかのフラグ
					Dim bolIsUpdtDay As Boolean = False     '指定の曜日かどうかのフラグ
					Dim bolNG As Boolean = False

					'ASI[24 Oct 2019]:[START] toCheck day is in WeekA or WeekB. And condition to generate date of day in either week.
					Dim bolIsWeekDay As Boolean = False
					Dim s0010_Record As S0010 = db.S0010.Find(1)

					If bolWeekA = True AndAlso Math.Truncate(DateDiff(DateInterval.Day, s0010_Record.ABWEEKSTARTDT, dtymd) / 7) Mod 2 = 0 Then
						bolIsWeekDay = True
					End If
					If bolWeekB = True AndAlso Math.Truncate(DateDiff(DateInterval.Day, s0010_Record.ABWEEKSTARTDT, dtymd) / 7) Mod 2 = 1 Then
						bolIsWeekDay = True
					End If
					If bolWeekA = False AndAlso bolWeekB = False Then
						bolIsWeekDay = True
					End If

					If bolIsWeekDay = True Then 'ASI[24 Oct 2019]:[END]

						'曜日が全てチェックOFFの場合は、全ての曜日が登録
						If bolMon = False AndAlso bolTue = False AndAlso bolWed = False AndAlso bolTur = False AndAlso
						bolFri = False AndAlso bolSat = False AndAlso bolSun = False Then
							bolIsUpdtDay = True
							bolRnzkDay = True

						Else
							If bolMon = True AndAlso dtymd.DayOfWeek = DayOfWeek.Monday Then
								bolIsUpdtDay = True
								If bolTue = True Then
									bolRnzkDay = True
								End If
							ElseIf bolTue = True AndAlso dtymd.DayOfWeek = DayOfWeek.Tuesday Then
								bolIsUpdtDay = True
								If bolWed = True Then
									bolRnzkDay = True
								End If
							ElseIf bolWed = True AndAlso dtymd.DayOfWeek = DayOfWeek.Wednesday Then
								bolIsUpdtDay = True
								If bolTur = True Then
									bolRnzkDay = True
								End If
							ElseIf bolTur = True AndAlso dtymd.DayOfWeek = DayOfWeek.Thursday Then
								bolIsUpdtDay = True
								If bolFri = True Then
									bolRnzkDay = True
								End If
							ElseIf bolFri = True AndAlso dtymd.DayOfWeek = DayOfWeek.Friday Then
								bolIsUpdtDay = True
								If bolSat = True Then
									bolRnzkDay = True
								End If
							ElseIf bolSat = True AndAlso dtymd.DayOfWeek = DayOfWeek.Saturday Then
								bolIsUpdtDay = True
								If bolSun = True Then
									bolRnzkDay = True
								End If
							ElseIf bolSun = True AndAlso dtymd.DayOfWeek = DayOfWeek.Sunday Then
								bolIsUpdtDay = True
								If bolMon = True Then
									bolRnzkDay = True
								End If
							End If
						End If

					End If

					If bolIsUpdtDay = True Then

						d0010New.GYOMYMD = dtymd

						'開始時間 > 終了時間の場合、開始日+1
						If strKskjknst > strKskjkned Then
							d0010New.GYOMYMDED = dtymd.AddDays(1)
						Else
							'開始時間 <= 終了時間
							d0010New.GYOMYMDED = dtymd
						End If

						d0010New.JTJKNST = GetJtjkn(d0010New.GYOMYMD, strKskjknst)
						d0010New.JTJKNED = GetJtjkn(d0010New.GYOMYMDED, strKskjkned)

						'終了日が対象業務期間の終了日を超えていないかチェック
						If d0010New.GYOMYMDED > dtGyomymded Then
							bolNG = True

							'実時間前後関係チェック
						ElseIf d0010New.JTJKNST > d0010New.JTJKNED Then
							bolNG = True

							'曜日が連続ありの場合、拘束時間が24時間以内かチェック
						ElseIf bolRnzkDay AndAlso DateDiff(DateInterval.Minute, d0010New.JTJKNST, d0010New.JTJKNED) > 1440 Then
							bolNG = True

						End If

						If bolNG = False Then
							'期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
							Dim dtEnd As Date = Date.Parse(d0010New.GYOMYMD).AddDays(1)
							If d0010New.GYOMYMD <> d0010New.GYOMYMDED AndAlso d0010New.JTJKNED.ToString("yyyy/MM/dd") > d0010New.JTJKNST.ToString("yyyy/MM/dd") AndAlso d0010New.JTJKNED <> dtEnd Then

								AddRnzkGyom(lstD0010Updt, d0010New.GYOMYMD, d0010New.GYOMYMDED, strKskjknst, strKskjkned)

							Else
								'1日登録

								d0010New.KSKJKNST = strKskjknst
								d0010New.KSKJKNED = strKskjkned

								lstD0010Updt.Add(d0010New)

							End If

						End If

					End If

					dtymd = dtymd.AddDays(1)

				Loop

			End If


			Dim bolFirst As Boolean = True
			Dim bolEnd As Boolean = False

			Using tran As DbContextTransaction = db.Database.BeginTransaction
				Try
					Dim intcnt As Integer = db.Database.ExecuteSqlCommand("DELETE FROM TeLAS.W0010 WHERE ACUSERID = " & intAcuserid)

					Dim d0030 = db.D0030.ToList.Max(Function(d) d.NENGETU)
					Dim dtKokyutenakaiEnd As Date = Date.Parse(d0030.ToString().Substring(0, 4) & "/" & d0030.ToString().Substring(4, 2) & "/01").AddMonths(1).AddDays(-1)

					For intIdx As Integer = 0 To lstD0010Updt.Count - 1

						If bolEnd = True Then
							Exit For
						End If

						Dim d0010 As D0010 = lstD0010Updt(intIdx)

						If intIdx = lstD0010Updt.Count - 1 OrElse lstD0010Updt(intIdx + 1).GYOMYMD > dtKokyutenakaiEnd Then
							bolEnd = True
						End If

						Exe_pr_b0020_inst_yoin(intAcuserid, gyomno, d0010.GYOMYMD, d0010.JTJKNST, d0010.JTJKNED, bolEnd)
					Next

					For intIdx As Integer = 0 To lstD0010Updt.Count - 1

						Dim d0010 As D0010 = lstD0010Updt(intIdx)

						Exe_pr_b0020_updt_mark(intAcuserid, d0010.GYOMYMD, d0010.JTJKNST, d0010.JTJKNED, bolFirst)

						If d0010.GYOMYMD > dtKokyutenakaiEnd Then
							Exit For
						End If

						bolFirst = False
					Next

					tran.Commit()

				Catch ex As Exception
					Throw ex
					tran.Rollback()
				End Try

			End Using


			lstD0010Updt = Nothing

		End Sub

		Private Function Exe_pr_b0020_inst_yoin(ByVal intAcuserid As Integer, ByVal strGyomno As String, ByVal strGyomymd As String, ByVal dtJtjknst As Date, ByVal dtJtjkned As Date, ByVal bolEnd As Boolean) As Boolean


			Dim sqlpara1 As New SqlParameter("asi_acuserid", SqlDbType.SmallInt)
			sqlpara1.Value = intAcuserid

			Dim sqlpara2 As New SqlParameter("av_gyomymd", SqlDbType.VarChar, 10)
			sqlpara2.Value = strGyomymd

			Dim sqlpara3 As New SqlParameter("ad_jtjknst", SqlDbType.DateTime)
			sqlpara3.Value = dtJtjknst

			Dim sqlpara5 As New SqlParameter("ad_jtjkned", SqlDbType.DateTime)
			sqlpara5.Value = dtJtjkned

			Dim sqlpara6 As New SqlParameter("av_gyomno", SqlDbType.VarChar, 12)
			sqlpara6.Value = strGyomno

			Dim sqlpara7 As New SqlParameter("ab_end", SqlDbType.Bit)
			sqlpara7.Value = bolEnd

			Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_b0020_inst_yoin @asi_acuserid, @av_gyomymd, @ad_jtjknst, @ad_jtjkned, @av_gyomno, @ab_end",
										sqlpara1, sqlpara2, sqlpara3, sqlpara5, sqlpara6, sqlpara7)

			Return True
		End Function

		Private Function Exe_pr_b0020_updt_mark(ByVal intAcuserid As Integer, ByVal strGyomymd As String, ByVal strJtjkst As Date, ByVal strJtjked As Date, ByVal bolFirst As Boolean) As Boolean

			Dim sqlpara1 As New SqlParameter("asi_acuserid", SqlDbType.SmallInt)
			sqlpara1.Value = intAcuserid

			Dim sqlpara2 As New SqlParameter("av_gyomymd", SqlDbType.VarChar, 10)
			sqlpara2.Value = strGyomymd

			Dim sqlpara3 As New SqlParameter("av_jtjkst", SqlDbType.DateTime)
			sqlpara3.Value = strJtjkst

			Dim sqlpara4 As New SqlParameter("av_jtjked", SqlDbType.DateTime)
			sqlpara4.Value = strJtjked

			Dim sqlpara5 As New SqlParameter("ab_first", SqlDbType.Bit)
			sqlpara5.Value = bolFirst


			Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_b0020_updt_mark @asi_acuserid, @av_gyomymd, @av_jtjkst, @av_jtjked, @ab_first", sqlpara1, sqlpara2, sqlpara3, sqlpara4, sqlpara5)

			Return True
		End Function


		<OutputCache(Duration:=0)>
		Function SearchYoinData(ByVal userid As Integer, ByVal usernm As String, ByVal yoinidyes As String) As ActionResult
			If Request.IsAjaxRequest() Then

				ViewBag.Usernm = db.M0010.Find(userid).USERNM
				ViewBag.LastYoinFlg = 0

				Dim lstYesYoin As New List(Of String)
				If yoinidyes IsNot Nothing Then
					Dim strsYesYoin As String() = yoinidyes.Split(",")
					For Each strYoin As String In strsYesYoin
						lstYesYoin.Add(strYoin)
					Next
				End If

				Dim intAcuserid As Integer = Session("LoginUserid")

				'12:Wブッキング
				Dim w0030 = (From t In db.W0030 Where t.ACUSERID = intAcuserid And t.YOINID = 12 And t.USERID = userid).OrderBy(Function(f) f.GYOMYMD).
					ThenBy(Function(f) f.GYOMYMDED).ThenBy(Function(f) f.KSKJKNST).ThenBy(Function(f) f.KSKJKNED).ThenBy(Function(f) f.CATCD).FirstOrDefault()

				If w0030 IsNot Nothing Then
					Dim lstW0030 As New List(Of W0030)
					lstW0030.Add(w0030)

					ViewBag.Yoinid = 12
					ViewBag.Title = "Wブッキングしている番組"
					ViewBag.Message = "この番組を解除しますか？"

					Return PartialView("_YoinGyomData", lstW0030)
				End If

				'3:時間休、5:公休、6:代休、7:振休、11:24時超え
				Dim w0020 = (From t In db.W0020 Where t.ACUSERID = intAcuserid And
							(t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or t.YOINID = 11 Or
													t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15) And t.USERID = userid).
							OrderBy(Function(f) f.NENGETU).ThenBy(Function(f) f.HI).ToList

				If w0020.Count > 0 Then

					ViewBag.Yoinid = Nothing

					Dim sbTitle As New StringBuilder
					Dim sbMsg As New StringBuilder

					'ASI[25 Dec 2019]:Wブッキングは現状のまま。休日に関し、選択した時点では休日更新しない。当業務を更新する時に一緒に更新する。
					If lstYesYoin.Contains("3567") = False Then
						If w0020.Where(Function(f) f.YOINID = 5).Count() > 0 Then
							sbTitle.AppendLine("公休出")
							sbMsg.AppendLine("登録すると 公休 → 公休出扱い になります。よろしいですか？")
						End If

						'ASI[18 Oct 2019] : Added If Block for YOINID 13
						If w0020.Where(Function(f) f.YOINID = 13).Count() > 0 Then
							sbTitle.AppendLine("法休出")
							sbMsg.AppendLine("登録すると 法休 → 法休出扱い になります。よろしいですか？")
						End If
					End If
					'ASI[18 Oct 2019] : Added condition in If for YOINID 15
					If (lstYesYoin.Contains("11") = False AndAlso (w0020.Where(Function(f) f.YOINID = 11).Count() > 0 Or w0020.Where(Function(f) f.YOINID = 15).Count() > 0)) Then

						ViewBag.Yoinid = "11"

						'ASI[18 Oct 2019] : condition for adding sbTitle when YOINID 15
						If w0020.Where(Function(f) f.YOINID = 11).Count() > 0 Then
							sbTitle.AppendLine("24時超え公休出")
						ElseIf w0020.Where(Function(f) f.YOINID = 15).Count() > 0 Then
							sbTitle.AppendLine("24時超え法休出")
						End If

						'2017/08/02  24時超え休出のメッセージで振休、代休、強休、公休分けて出す
						If w0020.Where(Function(f) f.YOINID = 11 AndAlso f.KYUKCD = 5).Count() Then
							sbMsg.AppendLine("登録すると 公休 → 24時超え公休出扱い になります。よろしいですか？")
						ElseIf w0020.Where(Function(f) f.YOINID = 11 AndAlso f.KYUKCD = 6).Count() > 0 Then
							sbMsg.AppendLine("登録すると 代休 → 24時超え公休出扱い になります。よろしいですか？")
						ElseIf w0020.Where(Function(f) f.YOINID = 11 AndAlso f.KYUKCD = 7).Count() > 0 Then
							sbMsg.AppendLine("登録すると 振公休 → 24時超え公休出扱い になります。よろしいですか？")
						ElseIf w0020.Where(Function(f) f.YOINID = 11 AndAlso f.KYUKCD = 8).Count() > 0 Then
							sbMsg.AppendLine("登録すると 強休 → 24時超え公休出扱い になります。よろしいですか？")
						End If

						'ASI[18 Oct 2019] : Added condition for prepare sbMsg when YOINID 15
						If w0020.Where(Function(f) f.YOINID = 15 AndAlso f.KYUKCD = 13).Count() Then
							sbMsg.AppendLine("登録すると 法休 → 24時超法休出扱い になります。よろしいですか？")
						ElseIf w0020.Where(Function(f) f.YOINID = 15 AndAlso f.KYUKCD = 14).Count() > 0 Then
							sbMsg.AppendLine("登録すると 振法休 → 24時超法休出扱い になります。よろしいですか？")
						End If

					End If

					'ASI[25 Dec 2019]:Wブッキングは現状のまま。休日に関し、選択した時点では休日更新しない。当業務を更新する時に一緒に更新する。
					If lstYesYoin.Contains("3567") = False Then

						' ASI [06 Nov 2019] : now 振休 → work inserted → 出勤 change to 振休 → work inserted → 休出
						If w0020.Where(Function(f) f.YOINID = 7).Count() > 0 Then
							sbTitle.AppendLine("公休出")
							sbMsg.AppendLine("登録すると 振公休 → 公休出扱い になります。よろしいですか？")
						End If

						'ASI[18 Oct 2019] : Added condition for YOINID 14 ' ASI [06 Nov 2019] 
						If w0020.Where(Function(f) f.YOINID = 14).Count() > 0 Then
							sbTitle.AppendLine("法休出")
							sbMsg.AppendLine("登録すると 振法休 → 法休出扱い になります。よろしいですか？")
						End If

						If w0020.Where(Function(f) f.YOINID = 6).Count() > 0 Then
							sbTitle.AppendLine("出勤")
							sbMsg.AppendLine("登録すると 代休 → 出勤扱い になります。よろしいですか？")
						End If

						If w0020.Where(Function(f) f.YOINID = 3).Count() > 0 Then
							sbTitle.AppendLine("時間休")
							sbMsg.AppendLine("登録すると、業務に重なる時間休は削除されます。よろしいですか？")
						End If
					End If

					If sbTitle.Length > 0 Then
						If ViewBag.Yoinid Is Nothing Then
							ViewBag.Yoinid = 3567
						Else
							ViewBag.Yoinid = 3567 & "," & ViewBag.Yoinid
						End If

						Dim strTitle As String = sbTitle.ToString.Replace(vbCrLf, "・")

						ViewBag.Title = strTitle.Remove(strTitle.LastIndexOf("・")) & "確認"
						ViewBag.Message = sbMsg.ToString

						'ASI[25 Dec 2019]:CHK value of table change which already check
						Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 (t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or
												 t.YOINID = 11 Or t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15) And t.CHK = False).ToList()
						For Each item In w0010List
							item.CHK = True
							db.Entry(item).State = EntityState.Modified
						Next
						DoDBSaveChanges(db)


						'ASI[18 Oct 2019] : Added YOINID 13,14,15 in condition
						Dim intOtherYoinCnt = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 t.YOINID <> 3 And t.YOINID <> 5 And t.YOINID <> 6 And t.YOINID <> 7 And
												 t.YOINID <> 11 And t.YOINID <> 13 And t.YOINID <> 14 And t.YOINID <> 15 And t.CHK = False).Count()
						If intOtherYoinCnt = 0 Then
							ViewBag.LastYoinFlg = 1
						End If

						sbTitle = Nothing
						sbMsg = Nothing

						Return PartialView("_YoinKyukData", w0020)
					End If

					sbTitle = Nothing
					sbMsg = Nothing
				End If


				'-- 0:不適合要因無し 1:超過勤務 2:前後日10時間未満 3:時間休 4:当日時間休あり(業務期間と重ならないもの) 
				'-- 5:公休 6:代休 7:振替休 8:強休 9:時間強休 10:当日時間強休あり 11: 24時超え公休出 12: Wブッキング
				Dim w0010 As W0010 = Nothing

				'11: 24時超え公休出
				If lstYesYoin.Contains("11") = False Then
					w0010 = db.W0010.Find(intAcuserid, 11, userid)
					If w0010 IsNot Nothing Then
						ViewBag.Yoinid = w0010.YOINID
						ViewBag.Title = "24時超え公休出確認"
						ViewBag.Message = "24時超え公休出です。よろしいですか？"

						'11: 24時超え公休出に更新する休日がある場合は、更新しに行きたいのでLastYoinFlgフラグを立てない。
						Dim w002011 = (From t In db.W0020 Where t.ACUSERID = intAcuserid And t.YOINID = 11 And t.USERID = userid).
									OrderBy(Function(f) f.NENGETU).ThenBy(Function(f) f.HI).ToList

						If w002011.Count = 0 Then

							'ASI[25 Dec 2019][START]:CHK value of table change which already check
							Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 (t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or
												 t.YOINID = 11 Or t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15) And t.CHK = False).ToList()
							For Each item In w0010List
								item.CHK = True
								db.Entry(item).State = EntityState.Modified
							Next
							DoDBSaveChanges(db)
							'ASI[25 Dec 2019][END]:CHK value of table change which already check

							Dim intOtherYoinCnt = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And t.YOINID <> 11 And t.CHK = False).Count()
							If intOtherYoinCnt = 0 Then
								ViewBag.LastYoinFlg = 1
							End If
						End If

						Return PartialView("_YoinOther")
					End If
				End If

				'ASI[18 Oct 2019] : Added Else block to manage YOINID 15
				If lstYesYoin.Contains("11") = False Then
					w0010 = db.W0010.Find(intAcuserid, 15, userid)
					If w0010 IsNot Nothing Then
						ViewBag.Yoinid = 11
						ViewBag.Title = "24時超え法休出確認"
						ViewBag.Message = "24時超え法休出です。よろしいですか？"

						'15: 24時超え法休出に更新する休日がある場合は、更新しに行きたいのでLastYoinFlgフラグを立てない。
						Dim w002011 = (From t In db.W0020 Where t.ACUSERID = intAcuserid And t.YOINID = 15 And t.USERID = userid).
									OrderBy(Function(f) f.NENGETU).ThenBy(Function(f) f.HI).ToList

						If w002011.Count = 0 Then

							'ASI[25 Dec 2019][START]:CHK value of table change which already check
							Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 (t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or
												 t.YOINID = 11 Or t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15) And t.CHK = False).ToList()
							For Each item In w0010List
								item.CHK = True
								db.Entry(item).State = EntityState.Modified
							Next
							DoDBSaveChanges(db)
							'ASI[25 Dec 2019][END]:CHK value of table change which already check

							Dim intOtherYoinCnt = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And t.YOINID <> 15 And t.CHK = False).Count()
							If intOtherYoinCnt = 0 Then
								ViewBag.LastYoinFlg = 1
							End If
						End If

						Return PartialView("_YoinOther")
					End If
				End If

				'2:前後日10時間未満
				If lstYesYoin.Contains("2") = False Then
					w0010 = db.W0010.Find(intAcuserid, 2, userid)
					If w0010 IsNot Nothing Then
						ViewBag.Yoinid = w0010.YOINID
						ViewBag.Title = "前後日勤務状況確認"
						ViewBag.Message = "登録すると前後日勤務の間隔が10時間未満になります。よろしいですか？"

						'ASI[25 Dec 2019][START]:CHK value of table change which already check
						Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 (t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or
												 t.YOINID = 11 Or t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15 Or
												 t.YOINID = 2) And t.CHK = False).ToList()
						For Each item In w0010List
							item.CHK = True
							db.Entry(item).State = EntityState.Modified
						Next
						DoDBSaveChanges(db)
						'ASI[25 Dec 2019][END]:CHK value of table change which already check

						'ASI[18 Oct 2019] : Added YOINID 15 in condition
						Dim intOtherYoinCnt = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And t.YOINID <> 2 And t.YOINID <> 11 And t.YOINID <> 15 And t.CHK = False).Count()
						If intOtherYoinCnt = 0 Then
							ViewBag.LastYoinFlg = 1
						End If

						Return PartialView("_YoinOther")
					End If
				End If

				'1:超過勤務
				If lstYesYoin.Contains("1") = False Then
					w0010 = db.W0010.Find(intAcuserid, 1, userid)
					If w0010 IsNot Nothing Then
						ViewBag.Yoinid = w0010.YOINID
						ViewBag.Title = "超過勤務"
						ViewBag.Message = "登録すると超過勤務になります。よろしいですか？"

						'ASI[25 Dec 2019][START]:CHK value of table change which already check
						Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 (t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or
												 t.YOINID = 11 Or t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15 Or
												 t.YOINID = 2 Or t.YOINID = 1) And t.CHK = False).ToList()
						For Each item In w0010List
							item.CHK = True
							db.Entry(item).State = EntityState.Modified
						Next
						DoDBSaveChanges(db)
						'ASI[25 Dec 2019][END]:CHK value of table change which already check

						'ASI[18 Oct 2019] : Added YOINID 15 in condition
						Dim intOtherYoinCnt = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And t.YOINID <> 1 And t.YOINID <> 2 And t.YOINID <> 11 And t.YOINID <> 15 And t.CHK = False).Count()
						If intOtherYoinCnt = 0 Then
							ViewBag.LastYoinFlg = 1
						End If

						Return PartialView("_YoinOther")
					End If
				End If

				'10:当日時間強休あり
				If lstYesYoin.Contains("10") = False Then
					w0010 = db.W0010.Find(intAcuserid, 10, userid)
					If w0010 IsNot Nothing Then
						ViewBag.Yoinid = w0010.YOINID
						ViewBag.Title = "出勤確認"
						ViewBag.Message = "当日時間強休があります。登録してもよろしいですか？"

						'ASI[25 Dec 2019][START]:CHK value of table change which already check
						Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 (t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or
												 t.YOINID = 11 Or t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15 Or
												 t.YOINID = 2 Or t.YOINID = 1 Or t.YOINID = 10) And t.CHK = False).ToList()
						For Each item In w0010List
							item.CHK = True
							db.Entry(item).State = EntityState.Modified
						Next
						DoDBSaveChanges(db)
						'ASI[25 Dec 2019][END]:CHK value of table change which already check

						'ASI[18 Oct 2019] : Added YOINID 15 in condition
						Dim intOtherYoinCnt = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And t.YOINID <> 10 And t.YOINID <> 1 And t.YOINID <> 2 And t.YOINID <> 11 And t.YOINID <> 15 And t.CHK = False).Count()
						If intOtherYoinCnt = 0 Then
							ViewBag.LastYoinFlg = 1
						End If

						Return PartialView("_YoinOther")
					End If
				End If

				'4:当日時間休あり(業務期間と重ならないもの)
				If lstYesYoin.Contains("4") = False Then
					w0010 = db.W0010.Find(intAcuserid, 4, userid)
					If w0010 IsNot Nothing Then
						ViewBag.Yoinid = w0010.YOINID
						ViewBag.Title = "出勤確認"
						ViewBag.Message = "当日時間休があります。登録してもよろしいですか？"

						'ASI[25 Dec 2019][START]:CHK value of table change which already check
						Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And
												 (t.YOINID = 3 Or t.YOINID = 5 Or t.YOINID = 6 Or t.YOINID = 7 Or
												 t.YOINID = 11 Or t.YOINID = 13 Or t.YOINID = 14 Or t.YOINID = 15 Or
												 t.YOINID = 2 Or t.YOINID = 1 Or t.YOINID = 10 Or t.YOINID = 4) And t.CHK = False).ToList()
						For Each item In w0010List
							item.CHK = True
							db.Entry(item).State = EntityState.Modified
						Next
						DoDBSaveChanges(db)
						'ASI[25 Dec 2019][END]:CHK value of table change which already check

						'ASI[18 Oct 2019] : Added YOINID 15 in condition
						Dim intOtherYoinCnt = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid And t.YOINID <> 4 And t.YOINID <> 10 And t.YOINID <> 1 And t.YOINID <> 2 And t.YOINID <> 11 And t.YOINID <> 15 And t.CHK = False).Count()
						If intOtherYoinCnt = 0 Then
							ViewBag.LastYoinFlg = 1
						End If

						Return PartialView("_YoinOther")
					End If
				End If

				'0:不適合要因無し
				w0010 = db.W0010.Find(intAcuserid, 0, userid)
				If w0010 IsNot Nothing Then

					'2017/08/02不適合要因無しで業務の実終了時間が翌日になっていて、翌日に業務と重なる時間休、時間強休があったら、アラート出すため修正
					Dim w00202 = (From t In db.W0020 Where t.ACUSERID = intAcuserid And
								(t.YOINID = 0) And t.USERID = userid).
								OrderBy(Function(f) f.NENGETU).ThenBy(Function(f) f.HI).ToList

					If w00202.Count > 0 Then

						ViewBag.Yoinid = Nothing

						Dim sbTitle As New StringBuilder
						Dim sbMsg As New StringBuilder

						If w00202.Where(Function(f) f.KYUKCD = 3).Count() > 0 Then
							sbTitle.AppendLine("時間休")
							sbMsg.AppendLine("登録すると、業務に重なる時間休は削除されます。よろしいですか？")
						End If

						If w00202.Where(Function(f) f.KYUKCD = 9).Count() > 0 Then
							sbTitle.AppendLine("時間強休")
							sbMsg.AppendLine("登録すると、業務に重なる時間強休は削除されます。よろしいですか？")
						End If

						If sbTitle.Length > 0 Then

							ViewBag.Yoinid = 39 & "," & w0010.YOINID

							Dim strTitle As String = sbTitle.ToString.Replace(vbCrLf, "・")

							ViewBag.Title = strTitle.Remove(strTitle.LastIndexOf("・")) & "確認"
							ViewBag.Message = sbMsg.ToString


							ViewBag.LastYoinFlg = 1

							sbTitle = Nothing
							sbMsg = Nothing

							Return PartialView("_YoinKyukData", w00202)
						End If

						sbTitle = Nothing
						sbMsg = Nothing

					Else

						ViewBag.Yoinid = w0010.YOINID
						ViewBag.Title = "出勤確認"
						ViewBag.Message = "登録してもよろしいですか？"
						ViewBag.LastYoinFlg = 1

						Return PartialView("_YoinOther")

					End If

				End If

			End If

			Return New EmptyResult
		End Function

		<OutputCache(Duration:=0)>
		Function SearchAna(ByVal kbn As Integer) As ActionResult
			If Request.IsAjaxRequest() Then
				If kbn = 1 Then
					Dim m0010 = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
					Return PartialView("_AnaList", m0010)

				ElseIf kbn = 2 Then
					Dim m0080 = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
					Return PartialView("_AnakariList", m0080)
				End If
			End If

			Return New EmptyResult
		End Function

		' GET: D0010/Edit/5
		Function Edit(ByVal id As Decimal, ByVal Form_name As String) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim d0010 As D0010 = db.D0010.Find(id)
			If IsNothing(d0010) Then
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

			ViewBag.ACUSERID = loginUserId

			ViewData("Form_name") = Form_name

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.ToString
				If Not strUrlReferrer.Contains("B0020/Edit/" & id) AndAlso Not strUrlReferrer.Contains("B0020/SendMail") AndAlso Not strUrlReferrer.Contains("B0020/Delete") Then
					'例え、一覧→A業務修正→WbookingのB業務修正→A業務修正と遷移した場合、A業務修正から一覧へ戻したいため、B業務からA業務に来た時はUrlを保持しない。
					If id <> Session("B0020EditMotoGyomno" & id) Then
						Session("B0020EditRtnUrl" & id) = strUrlReferrer
					End If
				End If
			End If

			If id = Session("B0020WGYOMNO" & id) Then
				ViewBag.AddTitle = "（Wブッキングしている番組）"
			End If

			Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList

			'ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO", d0010.IKKATUNO)

			Dim lstbangumi = db.M0030.OrderBy(Function(m) m.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
			Dim anacatitem As New M0080
			anacatitem.ANNACATNO = "0"
			anacatitem.ANNACATNM = ""
			lstKarianacat.Insert(0, anacatitem)
			ViewBag.KarianacatList = lstKarianacat

			'ASI[06 Dec 2019] :[START] To set list into SPORTCATCD and SPORTSUBCATCD Dropdown
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

			Dim listFreeItem As New List(Of String)
			Dim listAnaItem As New List(Of String)
			Dim listitemvalue As New List(Of String)

			Dim m0150 As M0150 = db.M0150.Where(Function(m) m.SPORTCATCD = d0010.SPORTCATCD And m.SPORTSUBCATCD = d0010.SPORTSUBCATCD).FirstOrDefault
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
			'[END]

			'休出
			ViewBag.KyukDe = db.M0060.Find(2)

			'ASI[23 Oct 2019]:法出
			ViewBag.KyukHouDe = db.M0060.Find(13)

			ViewBag.lsM0150AnaCol = GetM0150AnaCol(d0010)

			If Session("D0010_" & d0010.GYOMNO) IsNot Nothing Then
				Dim d0010Session As D0010 = Session("D0010_" & d0010.GYOMNO)

				'自分自身の業務の時。（Wブッキングの場合、他に業務の修正する時があるので、そのときは不要）
				If d0010Session.GYOMNO = id Then
					d0010 = d0010Session

					Dim strGYOMYMDED As String = d0010.GYOMYMD
					If d0010.GYOMYMDED IsNot Nothing Then
						strGYOMYMDED = d0010.GYOMYMDED
					End If

					TempData("SearchKOHO") = True
					TempData("YOINUSERID") = d0010.YOINUSERID
					TempData("YOINIDYES") = d0010.YOINIDYES

					SearchKOHO(d0010.GYOMNO, d0010.GYOMYMD, strGYOMYMDED, d0010.KSKJKNST, d0010.KSKJKNED, d0010.PATTERN, d0010.MON, d0010.TUE, d0010.WED, d0010.TUR, d0010.FRI, d0010.SAT, d0010.SUN, d0010.WEEKA, d0010.WEEKB)

					ViewBag.Showkoho = True

					ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)

					Session.Remove("D0010_" & d0010.GYOMNO)
					Return View(d0010)
				End If

			End If

			ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM", d0010.CATCD)

			d0010.D0020 = d0010.D0020.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList

			Return View(d0010)
		End Function

		' POST: D0010/Edit/5
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Edit(<Bind(Include:="GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,SAIJKNST,SAIJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,RNZK,PGYOMNO,IKTFLG,IKTUSERID,IKKATUNO,ACUSERID,FMTKBN,HINAMEMO,DATAKBN,ANAIDLIST,KARIANALIST,RefAnalist,RefCatAnalist,RefKariAnalist,RefCatKariAnalist,YOINUSERID,YOINUSERNM,YOINIDYES,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,D0020,D0021,SPORTCATCD,SPORTSUBCATCD,FreeTxtBxList,SPORT_OYAFLG,SPORTFLG")> ByVal d0010 As D0010,
					  ByVal button As String, ByVal yoinid As String, ByVal userid As String, ByVal wgyomno As String, ByVal lastyoinflg As String, ByVal Form_name As String) As ActionResult
			'ASI[06 Dec 2019] : COl01,COL01.....COL20, SPORTCATCD, SPORTSUBCATCD added in parameter of Action

			'GyomuToroku No toki
			If d0010.SPORTCATCD IsNot Nothing AndAlso d0010.SPORTCATCD = 0 Then
				d0010.SPORTCATCD = Nothing
			End If
			If d0010.SPORTSUBCATCD IsNot Nothing AndAlso d0010.SPORTSUBCATCD = 0 Then
				d0010.SPORTSUBCATCD = Nothing
			End If

			If button = "btnYes" Then

				Return UpdateYoinUser("Edit", d0010, yoinid, userid, wgyomno, "")
			End If

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If
			ViewData("Form_name") = Form_name
			ViewBag.ACUSERID = loginUserId

			'ASI[06 Dec 2019] : [START] Is FURI then set screen value of it to d0010 object which will be update otherwise set db value as it is for COL_TYPE 2 [ana]
			Dim d0010DBExistRec As D0010 = db.D0010.Find(d0010.GYOMNO)
			If d0010DBExistRec.SPORTFLG <> False Then
				If d0010.FreeTxtBxList IsNot Nothing Then
					Dim iterationCnt As Int16 = 0
					For Each item In d0010.FreeTxtBxList
						If iterationCnt = 0 AndAlso item <> "" Then
							d0010.COL01 = item
						End If
						If iterationCnt = 1 AndAlso item <> "" Then
							d0010.COL02 = item
						End If
						If iterationCnt = 2 AndAlso item <> "" Then
							d0010.COL03 = item
						End If
						If iterationCnt = 3 AndAlso item <> "" Then
							d0010.COL04 = item
						End If
						If iterationCnt = 4 AndAlso item <> "" Then
							d0010.COL05 = item
						End If
						If iterationCnt = 5 AndAlso item <> "" Then
							d0010.COL06 = item
						End If
						If iterationCnt = 6 AndAlso item <> "" Then
							d0010.COL07 = item
						End If
						If iterationCnt = 7 AndAlso item <> "" Then
							d0010.COL08 = item
						End If
						If iterationCnt = 8 AndAlso item <> "" Then
							d0010.COL09 = item
						End If
						If iterationCnt = 9 AndAlso item <> "" Then
							d0010.COL10 = item
						End If
						If iterationCnt = 10 AndAlso item <> "" Then
							d0010.COL11 = item
						End If
						If iterationCnt = 11 AndAlso item <> "" Then
							d0010.COL12 = item
						End If
						If iterationCnt = 12 AndAlso item <> "" Then
							d0010.COL13 = item
						End If
						If iterationCnt = 13 AndAlso item <> "" Then
							d0010.COL14 = item
						End If
						If iterationCnt = 14 AndAlso item <> "" Then
							d0010.COL15 = item
						End If
						If iterationCnt = 15 AndAlso item <> "" Then
							d0010.COL16 = item
						End If
						If iterationCnt = 16 AndAlso item <> "" Then
							d0010.COL17 = item
						End If
						If iterationCnt = 17 AndAlso item <> "" Then
							d0010.COL18 = item
						End If
						If iterationCnt = 18 AndAlso item <> "" Then
							d0010.COL19 = item
						End If
						If iterationCnt = 19 AndAlso item <> "" Then
							d0010.COL20 = item
						End If
						If iterationCnt = 20 AndAlso item <> "" Then
							d0010.COL21 = item
						End If
						If iterationCnt = 21 AndAlso item <> "" Then
							d0010.COL22 = item
						End If
						If iterationCnt = 22 AndAlso item <> "" Then
							d0010.COL23 = item
						End If
						If iterationCnt = 23 AndAlso item <> "" Then
							d0010.COL24 = item
						End If
						If iterationCnt = 24 AndAlso item <> "" Then
							d0010.COL25 = item
						End If

						'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
						If iterationCnt = 25 AndAlso item <> "" Then
							d0010.COL26 = item
						End If
						If iterationCnt = 26 AndAlso item <> "" Then
							d0010.COL27 = item
						End If
						If iterationCnt = 27 AndAlso item <> "" Then
							d0010.COL28 = item
						End If
						If iterationCnt = 28 AndAlso item <> "" Then
							d0010.COL29 = item
						End If
						If iterationCnt = 29 AndAlso item <> "" Then
							d0010.COL30 = item
						End If
						If iterationCnt = 30 AndAlso item <> "" Then
							d0010.COL31 = item
						End If
						If iterationCnt = 31 AndAlso item <> "" Then
							d0010.COL32 = item
						End If
						If iterationCnt = 32 AndAlso item <> "" Then
							d0010.COL33 = item
						End If
						If iterationCnt = 33 AndAlso item <> "" Then
							d0010.COL34 = item
						End If
						If iterationCnt = 34 AndAlso item <> "" Then
							d0010.COL35 = item
						End If
						If iterationCnt = 35 AndAlso item <> "" Then
							d0010.COL36 = item
						End If
						If iterationCnt = 36 AndAlso item <> "" Then
							d0010.COL37 = item
						End If
						If iterationCnt = 37 AndAlso item <> "" Then
							d0010.COL38 = item
						End If
						If iterationCnt = 38 AndAlso item <> "" Then
							d0010.COL39 = item
						End If
						If iterationCnt = 39 AndAlso item <> "" Then
							d0010.COL40 = item
						End If
						If iterationCnt = 40 AndAlso item <> "" Then
							d0010.COL41 = item
						End If
						If iterationCnt = 41 AndAlso item <> "" Then
							d0010.COL42 = item
						End If
						If iterationCnt = 42 AndAlso item <> "" Then
							d0010.COL43 = item
						End If
						If iterationCnt = 43 AndAlso item <> "" Then
							d0010.COL44 = item
						End If
						If iterationCnt = 44 AndAlso item <> "" Then
							d0010.COL45 = item
						End If
						If iterationCnt = 45 AndAlso item <> "" Then
							d0010.COL46 = item
						End If
						If iterationCnt = 46 AndAlso item <> "" Then
							d0010.COL47 = item
						End If
						If iterationCnt = 47 AndAlso item <> "" Then
							d0010.COL48 = item
						End If
						If iterationCnt = 48 AndAlso item <> "" Then
							d0010.COL49 = item
						End If
						If iterationCnt = 49 AndAlso item <> "" Then
							d0010.COL50 = item
						End If

						iterationCnt = iterationCnt + 1
					Next
				End If

				d0010.SPORTFLG = d0010DBExistRec.SPORTFLG
				d0010.OYAGYOMFLG = d0010DBExistRec.OYAGYOMFLG

				If d0010.SAIJKNST IsNot Nothing Then
					d0010.SAIJKNST = ChangeToHHMM(d0010.SAIJKNST)
				End If
				If d0010.SAIJKNED IsNot Nothing Then
					d0010.SAIJKNED = ChangeToHHMM(d0010.SAIJKNED)
				End If
			End If
			'[END]

			If d0010.FMTKBN Is Nothing Then
				d0010.FMTKBN = 0
			End If

			If d0010.FMTKBN = 0 AndAlso ModelState.IsValid Then

				'業務期間終了日が未設定の場合、開始日と同じ日を設定。
				If d0010.GYOMYMDED Is Nothing Then
					d0010.GYOMYMDED = d0010.GYOMYMD
				End If

				'各担当アナに対し、業務期間の終了年月が公休展開されているかチェックする
				CheckKokyutenkaiDone(d0010)

				'未確認の不適合要因があるかチェックする（候補検索から選択して追加した後、不適合要因が発生してしまう可能性があるため）
				'If ModelState.IsValid Then
				'	CheckYoin(d0010)
				'End If

				'ASI[03 Jan 2020]:アナの時間が違う場合、エラーメッセージを表示する。
				If ModelState.IsValid Then
					CheckEachAnaJkn(d0010)
				End If

			End If

			Dim intShorikbn As Integer = 2

			'メール送信用のワークテーブルの初期化
			Dim lstw0040 = db.W0040.Where(Function(w) w.ACUSERID = loginUserId And w.SHORIKBN = intShorikbn)
			If lstw0040.Count > 0 Then
				For Each item In lstw0040
					db.W0040.Remove(item)
				Next

				db.SaveChanges()
			End If

			If (d0010.FMTKBN = 0 AndAlso ModelState.IsValid) OrElse d0010.FMTKBN = 1 OrElse d0010.FMTKBN = 2 Then '1:下書、2:雛形の場合、D0090に登録するので、D0010のModelState.IsValidは無視。

				'OA時間をから：を除外して4桁化する。
				If d0010.OAJKNST IsNot Nothing Then
					d0010.OAJKNST = ChangeToHHMM(d0010.OAJKNST)
				End If
				If d0010.OAJKNED IsNot Nothing Then
					d0010.OAJKNED = ChangeToHHMM(d0010.OAJKNED)
				End If

				'拘束時間から：を除外して4桁化する。
				d0010.KSKJKNST = ChangeToHHMM(d0010.KSKJKNST)
				d0010.KSKJKNED = ChangeToHHMM(d0010.KSKJKNED)
			End If


			If d0010.FMTKBN = 0 AndAlso ModelState.IsValid Then

				Dim dtNow As Date = Now
				d0010.JTJKNST = GetJtjkn(d0010.GYOMYMD, d0010.KSKJKNST)
				d0010.JTJKNED = GetJtjkn(d0010.GYOMYMDED, d0010.KSKJKNED)

				'メール送信用にデータを作成
				Dim w0040 As W0040 = New W0040
				w0040.ACUSERID = loginUserId
				w0040.SHORIKBN = intShorikbn
				w0040.GYOMNO = d0010.GYOMNO
				w0040.UPDTDT = dtNow
				CopyGyom(w0040, d0010)
				db.W0040.Add(w0040)

				Dim decNewKyukHenkorrkcd As Decimal = GetMaxKyukHenkorrkcd() + 1

				'変更前の業務情報
				Dim d0010Old As D0010 = db.D0010.Find(d0010.GYOMNO)
				db.Entry(d0010Old).State = EntityState.Detached

				'削除されたアナの更新
				Dim listdbD0020 As List(Of D0020) = (From t In db.D0020 Where t.GYOMNO = d0010.GYOMNO).ToList
				For Each itemdb In listdbD0020
					Dim bolExist As Boolean = False
					If d0010.D0020 IsNot Nothing Then
						For Each item In d0010.D0020
							If itemdb.USERID = item.USERID Then
								db.Entry(itemdb).State = EntityState.Detached
								bolExist = True
								Exit For
							End If
						Next
					End If
					If bolExist = False Then
						db.D0020.Remove(itemdb)

						'「10:24時超え休出」で24時を跨る業務であれば、「4:公休」に変更される。(ただし、他に２４超えの業務が存在しない場合のみ)
						'休出の業務であれば、「4:公休」に戻す。
						'削除のユーザーのため、変更前の業務期間で判断。
						CheckAndUpdateKoukyu(decNewKyukHenkorrkcd, d0010Old, itemdb, dtNow)

						'メール送信用にデータを作成（削除された担当アナ）
						Dim w0050 As New W0050
						w0050.ACUSERID = w0040.ACUSERID
						w0050.SHORIKBN = w0040.SHORIKBN
						w0050.GYOMNO = w0040.GYOMNO
						w0050.DELFLG = True
						CopyAna(w0050, itemdb)
						db.W0050.Add(w0050)
					End If
				Next

				'追加されたアナの更新
				If d0010.D0020 IsNot Nothing Then
					For Each item In d0010.D0020
						item.GYOMNO = d0010.GYOMNO

						'業務期間が変更された場合、変更前と後期間を比べて、
						'「10:24時超え休出」で24時を跨る業務であれば、「4:公休」に変更される。(ただし、他に２４超えの業務が存在しない場合のみ)
						'休出の業務であれば、「4:公休」に戻す。
						If d0010.GYOMYMD <> d0010Old.GYOMYMD OrElse d0010.GYOMYMDED <> d0010Old.GYOMYMDED OrElse
							d0010.KSKJKNST <> d0010Old.KSKJKNST OrElse d0010.KSKJKNED <> d0010Old.KSKJKNED Then

							'変更後の実時間が２４時超えではない、又は、終了日が変更された場合、もし変更前が２４時超えなら、公休に戻す。ただし、変更後の業務期間内であれば、休出に変更する
							If d0010.JTJKNED <> d0010Old.JTJKNED Then
								Dim dtNext As Date = Date.Parse(d0010.GYOMYMDED).AddDays(1)
								If d0010.JTJKNED <= dtNext OrElse d0010.GYOMYMDED <> d0010Old.GYOMYMDED Then
									CheckAndUpdateKoukyu(decNewKyukHenkorrkcd, d0010Old, item, dtNow, True, False, d0010)
								End If
							End If

							'変更後と変更前を比べ、対象日付で休出があれば、公休に戻す
							If d0010.GYOMYMD <> d0010Old.GYOMYMD OrElse d0010.GYOMYMDED <> d0010Old.GYOMYMDED Then

								If d0010.GYOMYMD = d0010.GYOMYMDED AndAlso d0010Old.GYOMYMD = d0010Old.GYOMYMDED Then
									CheckAndUpdateKoukyu(decNewKyukHenkorrkcd, d0010Old, item, dtNow, False, True, d0010)

								Else
									Dim dtGYOMYMDEDOld As Date = d0010Old.GYOMYMDED

									If d0010.GYOMYMD > d0010Old.GYOMYMD Then
										d0010Old.GYOMYMDED = Date.Parse(d0010.GYOMYMD).AddDays(-1)
										If d0010Old.GYOMYMD <= d0010Old.GYOMYMDED Then
											CheckAndUpdateKoukyu(decNewKyukHenkorrkcd, d0010Old, item, dtNow, False, True)
										End If
									End If

									If d0010.GYOMYMDED < dtGYOMYMDEDOld Then
										d0010Old.GYOMYMD = Date.Parse(d0010.GYOMYMDED).AddDays(1)
										d0010Old.GYOMYMDED = dtGYOMYMDEDOld

										If d0010Old.GYOMYMD <= d0010Old.GYOMYMDED Then
											CheckAndUpdateKoukyu(decNewKyukHenkorrkcd, d0010Old, item, dtNow, False, True, d0010)
										End If
									End If
								End If

							End If

						End If

						'メール送信用にデータを作成
						Dim w0050 As New W0050
						w0050.ACUSERID = w0040.ACUSERID
						w0050.SHORIKBN = w0040.SHORIKBN
						w0050.GYOMNO = w0040.GYOMNO

						Dim d0020 As D0020 = db.D0020.Find(d0010.GYOMNO, item.USERID)
						If d0020 Is Nothing Then

							'ASI[26 Dec 2019]:Edit time col added
							item.GYOMYMD = d0010.GYOMYMD
							item.GYOMYMDED = d0010.GYOMYMDED
							item.KSKJKNST = d0010.KSKJKNST
							item.KSKJKNED = d0010.KSKJKNED
							item.JTJKNST = d0010.JTJKNST
							item.JTJKNED = d0010.JTJKNED
							db.D0020.Add(item)

							'メール送信用にデータを作成（新規担当アナ）
							CopyAna(w0050, item)

						Else
							item.CHK = False
							item.SHIFTMEMO = d0020.SHIFTMEMO
							item.SOUSIN = d0020.SOUSIN

							'ASI[26 Dec 2019]:Edit time col added
							item.GYOMYMD = d0010.GYOMYMD
							item.GYOMYMDED = d0010.GYOMYMDED
							item.KSKJKNST = d0010.KSKJKNST
							item.KSKJKNED = d0010.KSKJKNED
							item.JTJKNST = d0010.JTJKNST
							item.JTJKNED = d0010.JTJKNED
							db.Entry(d0020).State = EntityState.Detached
							db.Entry(item).State = EntityState.Modified

							'メール送信用にデータを作成（既に存在の担当アナ）
							CopyAna(w0050, d0020)
						End If

						db.W0050.Add(w0050)
					Next
				End If

				'ASI[26 Dec 2019]:Wブッキングは現状のまま。休日に関し、選択した時点では休日更新しない。当業務を更新する時に一緒に更新する。
				UpdateYoinUser(decNewKyukHenkorrkcd, d0010)

				Dim listdbD0021 As List(Of D0021) = (From t In db.D0021 Where t.GYOMNO = d0010.GYOMNO).ToList

				'追加された仮アナの更新
				If d0010.D0021 IsNot Nothing Then

					Dim maxseq As Integer = 0
					If listdbD0021.Count > 0 Then
						maxseq = listdbD0021.Max(Function(f) f.SEQ)
					End If

					For Each item In d0010.D0021
						If item.SEQ = 0 Then
							If String.IsNullOrEmpty(item.ANNACATNM) = False Then
								maxseq = maxseq + 1
								item.SEQ = maxseq
								item.GYOMNO = d0010.GYOMNO
								db.D0021.Add(item)
							End If
						End If
					Next
				End If

				'変更、又は削除された仮アナの更新
				For Each itemdb In listdbD0021
					Dim bolExist As Boolean = False
					If d0010.D0021 IsNot Nothing Then
						For Each item In d0010.D0021
							If itemdb.SEQ = item.SEQ Then
								If String.IsNullOrEmpty(item.ANNACATNM) = False Then
									bolExist = True
									db.Entry(itemdb).State = EntityState.Detached
									If itemdb.ANNACATNM <> item.ANNACATNM OrElse itemdb.COLNM <> item.COLNM Then
										item.GYOMNO = d0010.GYOMNO
										db.Entry(item).State = EntityState.Modified
									End If
								End If
								Exit For
							End If
						Next
					End If
					If bolExist = False Then
						db.D0021.Remove(itemdb)
					End If
				Next

				'連続登録の場合、子業務を再作成する
				If d0010.RNZK Then
					Dim lstd0010child = (From t In db.D0010 Where t.PGYOMNO = d0010.GYOMNO).ToList
					For Each itemchild In lstd0010child
						db.D0010.Remove(itemchild)
					Next
				End If


				'期間が2日以上の場合、連続登録（実時間の日付が違う場合。ただし、開始日の24時まで、つまり実時間は開始日の次の日の0時を除く。）
				Dim dtEnd As Date = Date.Parse(d0010.GYOMYMD).AddDays(1)
				If d0010.GYOMYMD <> d0010.GYOMYMDED AndAlso d0010.JTJKNED.ToString("yyyy/MM/dd") > d0010.JTJKNST.ToString("yyyy/MM/dd") AndAlso d0010.JTJKNED <> dtEnd Then

					d0010.RNZK = True

					'業務番号の最大値の取得
					Dim decTempGyomno As Decimal = Decimal.Parse(Date.Parse(d0010.GYOMYMD).ToString("yyyyMMdd") & "0000")
					Dim lstgyomno = (From t In db.D0010 Where t.GYOMNO > decTempGyomno Select t.GYOMNO).ToList
					If lstgyomno.Count > 0 Then
						decTempGyomno = lstgyomno.Max
					End If

					Dim dtGyomymd As Date = Date.Parse(d0010.GYOMYMD).AddDays(1)

					Do While dtGyomymd <= d0010.GYOMYMDED
						Dim newd0010ko As New D0010
						decTempGyomno += 1
						newd0010ko.GYOMNO = decTempGyomno
						newd0010ko.PGYOMNO = d0010.GYOMNO
						newd0010ko.GYOMYMD = dtGyomymd
						newd0010ko.GYOMYMDED = dtGyomymd

						If dtGyomymd < d0010.GYOMYMDED Then
							newd0010ko.KSKJKNST = "0000"
							newd0010ko.KSKJKNED = "2400"
						Else
							newd0010ko.KSKJKNST = "0000"
							newd0010ko.KSKJKNED = d0010.KSKJKNED
						End If

						newd0010ko.JTJKNST = GetJtjkn(newd0010ko.GYOMYMD, newd0010ko.KSKJKNST)
						newd0010ko.JTJKNED = GetJtjkn(newd0010ko.GYOMYMDED, newd0010ko.KSKJKNED)
						newd0010ko.RNZK = d0010.RNZK
						CopyValue(newd0010ko, d0010)
						db.D0010.Add(newd0010ko)

						'For Sport Record Create Child Record also in D0020 
						If d0010.SPORTFLG = True Then

							'追加されたアナの更新
							If d0010.D0020 IsNot Nothing Then
								For Each item In d0010.D0020
									Dim RecD0020 As New D0020
									RecD0020.GYOMNO = decTempGyomno
									RecD0020.USERID = item.USERID
									RecD0020.COLNM = item.COLNM

									'ASI[26 Dec 2019]:Edit time col added
									RecD0020.GYOMYMD = item.GYOMYMD
									RecD0020.GYOMYMDED = item.GYOMYMDED
									RecD0020.KSKJKNST = item.KSKJKNST
									RecD0020.KSKJKNED = item.KSKJKNED
									RecD0020.JTJKNST = item.JTJKNST
									RecD0020.JTJKNED = item.JTJKNED

									db.D0020.Add(RecD0020)
								Next
							End If

						End If

						dtGyomymd = dtGyomymd.AddDays(1)
					Loop

				Else
					d0010.RNZK = False
				End If

				Dim lstD0020 As New List(Of D0020)
				Dim lstD0021 As New List(Of D0021)

				If d0010.D0020 IsNot Nothing Then
					lstD0020 = d0010.D0020.ToList
				End If
				If d0010.D0021 IsNot Nothing Then
					lstD0021 = d0010.D0021.ToList
				End If

				'変更履歴の作成
				Dim d0070 As New D0070
				d0070.HENKORRKCD = GetMaxHenkorrkcd() + 1
				d0070.HENKONAIYO = "変更"
				d0070.USERID = loginUserId
				d0070.SYUSEIYMD = dtNow
				d0070.TNTNM = GetAllAnanm(lstD0020, lstD0021)
				CopyHenkorrk(d0070, d0010)
				db.D0070.Add(d0070)


				d0010.D0020 = Nothing
				d0010.D0021 = Nothing

				db.Entry(d0010).State = EntityState.Modified

				DoDBSaveChanges(db)

				If lstD0020.Count > 0 Then
					Return RedirectToAction("SendMail", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn})
				Else
					If String.IsNullOrEmpty(Session("B0020EditRtnUrl" & d0010.GYOMNO)) = False Then
						Return Redirect(Session("B0020EditRtnUrl" & d0010.GYOMNO))
					Else
						Return RedirectToAction("Index")
					End If
				End If


				'下書・雛形保存
			ElseIf d0010.FMTKBN = 1 OrElse d0010.FMTKBN = 2 Then

				Dim d0090 As New D0090
				d0090.FMTKBN = d0010.FMTKBN
				d0090.GYOMYMD = d0010.GYOMYMD
				d0090.GYOMYMDED = d0010.GYOMYMDED
				d0090.KSKJKNST = d0010.KSKJKNST
				d0090.KSKJKNED = d0010.KSKJKNED
				d0090.CATCD = d0010.CATCD
				d0090.BANGUMINM = d0010.BANGUMINM
				d0090.OAJKNST = d0010.OAJKNST
				d0090.OAJKNED = d0010.OAJKNED
				d0090.NAIYO = d0010.NAIYO
				d0090.BASYO = d0010.BASYO
				d0090.BIKO = d0010.BIKO
				d0090.BANGUMITANTO = d0010.BANGUMITANTO
				d0090.BANGUMIRENRK = d0010.BANGUMIRENRK
				d0090.PTNFLG = d0010.PATTERN
				d0090.PTN1 = d0010.MON
				d0090.PTN2 = d0010.TUE
				d0090.PTN3 = d0010.WED
				d0090.PTN4 = d0010.TUR
				d0090.PTN5 = d0010.FRI
				d0090.PTN6 = d0010.SAT
				d0090.PTN7 = d0010.SUN
				d0090.DATAKBN = d0010.DATAKBN
				d0090.HINAMEMO = d0010.HINAMEMO
				d0090.SIYOFLG = 0
				d0090.SIYOUSERID = loginUserId
				d0090.STATUS = 0

				'ASI[24 Oct 2019]
				d0090.WEEKA = d0010.WEEKA
				d0090.WEEKB = d0010.WEEKB

				Dim decTempHINANO As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "0000")
				Dim lstHINANO = (From t In db.D0090 Where t.HINANO > decTempHINANO Select t.HINANO).ToList
				If lstHINANO.Count > 0 Then
					decTempHINANO = lstHINANO.Max
				End If
				d0090.HINANO = decTempHINANO + 1

				db.D0090.Add(d0090)

				If String.IsNullOrEmpty(d0010.ANAIDLIST) = False Then
					If d0010.ANAIDLIST.Contains("，") Then
						Dim strAnaIds As String() = d0010.ANAIDLIST.Split("，")
						For Each strId In strAnaIds
							Dim d0100 As New D0100
							d0100.HINANO = d0090.HINANO
							d0100.USERID = strId
							db.D0100.Add(d0100)
						Next
					Else
						Dim d0100 As New D0100
						d0100.HINANO = d0090.HINANO
						d0100.USERID = d0010.ANAIDLIST
						db.D0100.Add(d0100)
					End If
				End If

				If String.IsNullOrEmpty(d0010.KARIANALIST) = False Then
					If d0010.KARIANALIST.Contains("，") Then
						Dim strAnaIds As String() = d0010.KARIANALIST.Split("，")
						Dim intSeq As Integer = 1
						For Each strAnaCat In strAnaIds
							Dim d0101 As New D0101
							d0101.HINANO = d0090.HINANO
							d0101.SEQ = intSeq
							d0101.ANNACATNM = Trim(strAnaCat.Replace(vbCrLf, " "))
							db.D0101.Add(d0101)
							intSeq += 1
						Next
					Else
						Dim d0101 As New D0101
						d0101.HINANO = d0090.HINANO
						d0101.SEQ = 1
						d0101.ANNACATNM = Trim(d0010.KARIANALIST.Replace(vbCrLf, " "))
						db.D0101.Add(d0101)
					End If
				End If

				If DoDBSaveChanges(db) Then
					Dim strfmt As String = ""
					If d0010.FMTKBN = 1 Then
						strfmt = "下書"
					Else
						strfmt = "雛形"
					End If

					TempData("success") = String.Format("{0}が新規に保存されました。", strfmt)
				End If

			End If

			ViewBag.CATCD = New SelectList(db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN), "CATCD", "CATNM", d0010.CATCD)
			ViewBag.IKKATUNO = New SelectList(db.M0090, "IKKATUNO", "IKKATUMEMO", d0010.IKKATUNO)

			Dim lstbangumi = db.M0030.OrderBy(Function(m) m.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(m) m.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
			Dim anacatitem As New M0080
			anacatitem.ANNACATNO = "0"
			anacatitem.ANNACATNM = ""
			lstKarianacat.Insert(0, anacatitem)
			ViewBag.KarianacatList = lstKarianacat

			'ASI[06 Dec 2019] :[START] To set list into SPORTCATCD and SPORTSUBCATCD Dropdown
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

			'ASI[10 Dec 2019] : START
			Dim listFreeItem As New List(Of String)
			Dim listAnaItem As New List(Of String)
			Dim listitemvalue As New List(Of String)

			Dim m0150 As M0150 = db.M0150.Where(Function(m) m.SPORTCATCD = d0010.SPORTCATCD And m.SPORTSUBCATCD = d0010.SPORTSUBCATCD).FirstOrDefault
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
			'[END]

			'休出
			ViewBag.KyukDe = db.M0060.Find(2)

			'ASI[23 Oct 2019]:法出
			ViewBag.KyukHouDe = db.M0060.Find(13)

			ViewBag.lsM0150AnaCol = GetM0150AnaCol(d0010)

			For Each item In d0010.D0020
				item.M0010 = db.M0010.Find(item.USERID)
			Next

			Return View(d0010)
		End Function


		'「24時超え休出」[休出]でなくなった時、もともと「公休」であれば、公休に戻す
		Sub CheckAndUpdateKoukyu(ByRef decNewKyukHenkorrkcd As Decimal, ByVal d0010 As D0010, ByVal d0020 As D0020, ByVal dtNow As Date,
				Optional ByVal bolReset24Koe As Boolean = True, Optional ByVal bolResetKyushutsu As Boolean = True, Optional ByVal d0010New As D0010 = Nothing)

			'「10:24時超え休出」で24時を跨る業務を削除すると「4:公休」に変更される。(ただし、他に２４超えの業務が存在しない場合のみ)
			If bolReset24Koe Then
				Dim strHENKONAIYO As String = ""
				Dim dtNext As Date = Date.Parse(d0010.GYOMYMDED).AddDays(1)
				If d0010.JTJKNED > dtNext Then
					Dim intNengetsu As Integer = Integer.Parse(dtNext.ToString("yyyyMM"))
					Dim intHi As Integer = Integer.Parse(dtNext.ToString("dd"))
					Dim d0040 = (db.D0040.Where(Function(t) t.USERID = d0020.USERID And (t.KYUKCD = 10 Or t.KYUKCD = 14) And t.NENGETU = intNengetsu And t.HI = intHi)).ToList
					If d0040.Count > 0 Then

						'他に24時を跨る業務かチェック
						Dim d0010lst24koe = (From d1 In db.D0010 Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
											 Where d1.GYOMNO <> d0010.GYOMNO And d1.GYOMYMD = d0010.GYOMYMD And d1.JTJKNED > dtNext And
										 d2.USERID = d0020.USERID).ToList

						If d0010lst24koe.Count = 0 Then

							Dim d0010lstgyom = (From d1 In db.D0010 Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
												Where d1.GYOMNO <> d0010.GYOMNO And d1.GYOMYMD = dtNext And d2.USERID = d0020.USERID).ToList

							If d0010lstgyom.Count > 0 Then
								'公休日に業務が登録されている場合、「休出」にする。
								If d0040(0).KYUKCD = 14 Then ' ASI 法休出
									d0040(0).KYUKCD = 13
								Else
									d0040(0).KYUKCD = 2
								End If
								strHENKONAIYO = "変更"
							ElseIf d0010New IsNot Nothing AndAlso dtNext >= d0010New.GYOMYMD AndAlso dtNext <= d0010New.GYOMYMDED Then
								'複数日業務の場合、変更後の業務期間内であれば、「休出」にする。
								If d0040(0).KYUKCD = 14 Then ' ASI 法休出
									d0040(0).KYUKCD = 13
								Else
									d0040(0).KYUKCD = 2
								End If
								strHENKONAIYO = "変更"
							Else


								'24時間超えは公休と振休だけではなく代休と強休も加わったため、全部を公休に戻せないので
								'２４時間超え休出を公休に戻す時に、ユーザー情報を見て、公休の日だけ公休に戻す、他は２４時間超えを削除する
								Dim m0010 = db.M0010.Find(d0020.USERID)
								Dim intDay As Integer = dtNext.DayOfWeek
								Dim bolUpdateKokyu As Boolean = False

								'[12 Nov 2019] :  no need to see user master. reset back 法休、公休 respectively 
								'If (intDay = 1 AndAlso (m0010.KOKYU1 OrElse m0010.HOKYU1)) OrElse (intDay = 2 AndAlso (m0010.KOKYU2 OrElse m0010.HOKYU2)) OrElse (intDay = 3 AndAlso (m0010.KOKYU3 OrElse m0010.HOKYU3)) OrElse
								'	(intDay = 4 AndAlso (m0010.KOKYU4 OrElse m0010.HOKYU4)) OrElse (intDay = 5 AndAlso (m0010.KOKYU5 OrElse m0010.HOKYU5)) OrElse (intDay = 6 AndAlso (m0010.KOKYU6 OrElse m0010.HOKYU6)) OrElse (intDay = 0 AndAlso (m0010.KOKYU7 OrElse m0010.HOKYU7)) Then
								'公休日に業務なしの場合、他に休暇(時間休など)が存在すれば、削除して公休にする
								Dim d0040Others = (db.D0040.Where(Function(t) t.USERID = d0020.USERID And t.KYUKCD <> 10 And t.KYUKCD <> 14 And t.NENGETU = intNengetsu And t.HI = intHi)).ToList
								If d0040Others.Count > 0 Then
									For Each d0040Other As D0040 In d0040Others
										db.D0040.Remove(d0040Other)
									Next
								End If

								'公休
								If d0040(0).KYUKCD = 14 Then ' ASI 法休
									d0040(0).KYUKCD = 11
								Else
									d0040(0).KYUKCD = 4
								End If
								strHENKONAIYO = "変更"
								'	Else
								'		'公休日に業務なしの場合、他に休暇(時間休など)が存在すれば、削除して公休にする
								'		Dim d004024Overs = (db.D0040.Where(Function(t) t.USERID = d0020.USERID And (t.KYUKCD = 10 Or t.KYUKCD = 14) And t.NENGETU = intNengetsu And t.HI = intHi)).ToList
								'		If d004024Overs.Count > 0 Then
								'			For Each d004024Over As D0040 In d004024Overs
								'				db.D0040.Remove(d004024Over)
								'			Next
								'		End If
								'		
								'		strHENKONAIYO = "削除"
								'	End If

							End If


							'休暇変更履歴作成
							Dim d0150 As New D0150
							d0150.HENKORRKCD = decNewKyukHenkorrkcd
							d0150.HENKONAIYO = strHENKONAIYO
							d0150.USERID = Session("LoginUserid")
							d0150.SYUSEIYMD = dtNow
							CopyKyukHenkorrk(d0150, d0040(0))
							db.D0150.Add(d0150)

							decNewKyukHenkorrkcd += 1

						End If
					End If
				End If
			End If


			'休出の業務が削除された場合、「4:公休」に戻す。
			If bolResetKyushutsu Then
				Dim dtymd As Date = d0010.GYOMYMD
				While dtymd <= d0010.GYOMYMDED
					Dim intNengetsu As Integer = Integer.Parse(dtymd.ToString("yyyyMM"))
					Dim intHi As Integer = Integer.Parse(dtymd.ToString("dd"))
					Dim d0040 = (db.D0040.Where(Function(t) t.USERID = d0020.USERID And (t.KYUKCD = 2 Or t.KYUKCD = 13) And t.NENGETU = intNengetsu And t.HI = intHi)).ToList
					If d0040.Count > 0 Then
						Dim gyom = (From d1 In db.D0010 Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
									Where d1.GYOMNO <> d0010.GYOMNO And d1.GYOMYMD <= dtymd And d1.GYOMYMDED >= dtymd And d2.USERID = d0020.USERID).FirstOrDefault
						If gyom Is Nothing Then

							' ASI [13 Nov 2019]: If block added	 : 
							'when delete work on 休出 day,If other work exist In that day, Then As it Is 休出.
							'ElseIf 24hr over work exist In prev day, Then 24超 type.
							'Else 公休/ 方法 holiday		
							Dim dtPrev As Date = Date.Parse(d0010.GYOMYMD).AddDays(-1)
							Dim gyomPrev = (From d1 In db.D0010 Join d2 In db.D0020 On d1.GYOMNO Equals d2.GYOMNO
											Where d1.GYOMNO <> d0010.GYOMNO And d1.GYOMYMD <= dtPrev And d1.JTJKNED > dtymd And d2.USERID = d0020.USERID).FirstOrDefault
							If gyomPrev IsNot Nothing Then
								If d0040(0).KYUKCD = 13 Then
									d0040(0).KYUKCD = 14
								Else
									d0040(0).KYUKCD = 10
								End If
							Else
								'業務なしの場合、他に休暇(時間休など)が存在すれば、削除して公休にする
								Dim d0040Others = (db.D0040.Where(Function(t) t.USERID = d0020.USERID And t.KYUKCD <> 2 And t.KYUKCD <> 13 And t.NENGETU = intNengetsu And t.HI = intHi)).ToList
								If d0040Others.Count > 0 Then
									For Each d0040Other As D0040 In d0040Others
										db.D0040.Remove(d0040Other)
									Next
								End If

								Dim isKYUKCDset = False

								'公休になる時、もし業務期間が変更され、変更後期間が24時超えであれば、24時超えに更新する
								If d0010New IsNot Nothing Then
									Dim dtNext As Date = Date.Parse(d0010New.GYOMYMDED).AddDays(1)
									If dtymd = dtNext AndAlso d0010New.JTJKNED > dtNext Then
										'休出→24時超え
										If d0040(0).KYUKCD = 13 Then ' ASI 法休
											d0040(0).KYUKCD = 14
										Else
											d0040(0).KYUKCD = 10
										End If
										isKYUKCDset = True
									End If
								End If

								If isKYUKCDset = False Then
									'休出→公休
									If d0040(0).KYUKCD = 13 Then ' ASI 法休
										d0040(0).KYUKCD = 11
									Else
										d0040(0).KYUKCD = 4
									End If
								End If
							End If

							'休暇変更履歴作成
							Dim d0150 As New D0150
							d0150.HENKORRKCD = decNewKyukHenkorrkcd
							d0150.HENKONAIYO = "変更"
							d0150.USERID = Session("LoginUserid")
							d0150.SYUSEIYMD = dtNow
							CopyKyukHenkorrk(d0150, d0040(0))
							db.D0150.Add(d0150)

							decNewKyukHenkorrkcd += 1
						End If
					End If

					dtymd = dtymd.AddDays(1)
				End While
			End If

		End Sub

		' GET: D0010/Delete/5
		Function Delete(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If

			Dim d0010 As D0010 = db.D0010.Find(id)
			If IsNothing(d0010) Then
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

			If Request.UrlReferrer IsNot Nothing AndAlso Not Request.UrlReferrer.ToString.Contains("B0020/Delete") Then
				Session("B0020DeleteRtnUrl") = Request.UrlReferrer.ToString
			End If

			Return View(d0010)
		End Function

		' POST: D0010/Delete/5
		<HttpPost()>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken()>
		Function DeleteConfirmed(ByVal id As Decimal) As ActionResult

			Dim d0010 As D0010 = db.D0010.Find(id)

			If d0010 Is Nothing Then
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

			Dim pgyomno As Decimal = Nothing
			If (d0010.PGYOMNO IsNot Nothing AndAlso d0010.SPORTFLG = True) Then
				pgyomno = d0010.PGYOMNO
			End If

			Dim intShorikbn As Integer = 3

			'メール送信用のワークテーブルの初期化
			Dim lstw0040 = db.W0040.Where(Function(w) w.ACUSERID = loginUserId And w.SHORIKBN = intShorikbn)
			If lstw0040.Count > 0 Then
				For Each item In lstw0040
					db.W0040.Remove(item)
				Next

				db.SaveChanges()
			End If

			Dim dtNow As Date = Now
			Dim decNewKyukHenkorrkcd As Decimal = GetMaxKyukHenkorrkcd() + 1

			Dim d0020lst = db.D0020.Where(Function(f) f.GYOMNO = d0010.GYOMNO).ToList
			For Each d0020 In d0020lst

				'「10:24時超え休出」で24時を跨る業務を削除すると「4:公休」に変更される。(ただし、他に２４超えの業務が存在しない場合のみ)	
				'休出の業務が削除された場合、「4:公休」に戻す。
				CheckAndUpdateKoukyu(decNewKyukHenkorrkcd, d0010, d0020, dtNow)

			Next

			Dim lstD0020 = db.D0020.Where(Function(d) d.GYOMNO = d0010.GYOMNO).ToList
			Dim lstD0021 = db.D0021.Where(Function(d) d.GYOMNO = d0010.GYOMNO).ToList

			'変更履歴の作成
			Dim d0070 As New D0070
			d0070.HENKORRKCD = GetMaxHenkorrkcd() + 1
			d0070.HENKONAIYO = "削除"
			d0070.USERID = loginUserId
			d0070.SYUSEIYMD = dtNow
			d0070.TNTNM = GetAllAnanm(lstD0020, lstD0021)
			CopyHenkorrk(d0070, d0010)
			db.D0070.Add(d0070)

			'メール送信用にデータを作成
			If lstD0020.Count > 0 AndAlso d0010.GYOMYMDED >= Today Then
				Dim w0040 As New W0040
				w0040.ACUSERID = loginUserId
				w0040.SHORIKBN = intShorikbn
				w0040.GYOMNO = d0010.GYOMNO
				w0040.UPDTDT = dtNow
				CopyGyom(w0040, d0010)
				db.W0040.Add(w0040)

				For Each d0020 In lstD0020
					Dim w0050 As New W0050
					w0050.ACUSERID = w0040.ACUSERID
					w0050.SHORIKBN = w0040.SHORIKBN
					w0050.GYOMNO = w0040.GYOMNO
					CopyAna(w0050, d0020)
					db.W0050.Add(w0050)
				Next
			End If

			db.D0010.Remove(d0010)

			'連続業務の場合、子業務も削除。
			If d0010.RNZK Then
				Dim lstd0010child = (From t In db.D0010 Where t.PGYOMNO = d0010.GYOMNO).ToList
				For Each itemchild In lstd0010child
					db.D0010.Remove(itemchild)
				Next
			End If

			'ASI[18 Feb 2020] : after fix 一括本登録, when delete fix sport shift in 業務削除 page, 
			'then delete the primary data(1stly created data) also if no other Ana exist.
			If (d0010.PGYOMNO IsNot Nothing AndAlso d0010.SPORTFLG = True) Then

				Dim anaCnt = (From d10 In db.D0010
							  Join d20 In db.D0020 On d20.GYOMNO Equals d10.GYOMNO
							  Where d10.GYOMNO <> id And d10.PGYOMNO = pgyomno).Count
				If anaCnt = 0 Then
					anaCnt = (From d10 In db.D0010
							  Join d22 In db.D0022 On d22.GYOMNO Equals d10.GYOMNO
							  Where d10.GYOMNO = pgyomno Or d10.PGYOMNO = pgyomno).Count
					If anaCnt = 0 Then
						anaCnt = (From d10 In db.D0010
								  Join d21 In db.D0021 On d21.GYOMNO Equals d10.GYOMNO
								  Where d10.GYOMNO <> id And d10.PGYOMNO = pgyomno).Count
					End If
				End If

				If anaCnt = 0 Then
					Dim lstd0010child = (From t In db.D0010 Where t.GYOMNO <> id And (t.GYOMNO = pgyomno Or t.PGYOMNO = pgyomno)).ToList
					For Each itemchild In lstd0010child
						db.D0010.Remove(itemchild)
					Next
				End If
			End If

			DoDBSaveChanges(db)

			If lstD0020.Count > 0 AndAlso d0010.GYOMYMDED >= Today Then
				Return RedirectToAction("SendMail", routeValues:=New With {.acuserid = loginUserId, .shorikbn = intShorikbn})

			Else
				'修正画面から削除された場合、修正画面を呼び出した画面に戻る
				If String.IsNullOrEmpty(Session("B0020DeleteRtnUrl")) = False AndAlso Session("B0020DeleteRtnUrl").ToString.Contains("B0020/Edit") Then
					If String.IsNullOrEmpty(Session("B0020EditRtnUrl" & id)) = False Then
						Return Redirect(Session("B0020EditRtnUrl" & id))
					End If
				End If

				Return RedirectToAction("Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
				.PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
				.Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
				.CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
				.Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk"), .OAJKNST = Session("OAJKNST"), .OAJKNED = Session("OAJKNED"), .Biko = Session("Biko")})

			End If

		End Function


		' GET: D0010/SendMail/5
		Function SendMail(ByVal acuserid As Integer, ByVal shorikbn As Integer) As ActionResult

			If IsNothing(acuserid) OrElse IsNothing(shorikbn) Then
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

			Dim strShori As String = ""

			'新規
			If shorikbn = 1 Then
				strShori = "登録"

				'修正
			ElseIf shorikbn = 2 Then
				strShori = "修正"

				'削除
			ElseIf shorikbn = 3 Then
				strShori = "削除"

				'承認
			ElseIf shorikbn = 4 Then
				strShori = "承認"

				'却下
			ElseIf shorikbn = 5 Then
				strShori = "却下"

			End If

			Dim query = (From p In db.W0050 Where p.ACUSERID = acuserid And p.SHORIKBN = shorikbn
						 Group By SubjectId = p.USERID Into GYOMNO = Min(p.GYOMNO)).ToList
			Dim lstGyomNo As New List(Of Decimal)
			For Each item In query
				lstGyomNo.Add(item.GYOMNO)
			Next

			Dim s0010 = db.S0010.Find(1)
			Dim w0040 As List(Of W0040) = db.W0040.Where(Function(w) w.ACUSERID = acuserid And w.SHORIKBN = shorikbn And lstGyomNo.Contains(w.GYOMNO)).ToList
			If w0040 Is Nothing Then
				Return HttpNotFound()
			End If

			'If Request.UrlReferrer IsNot Nothing Then
			'	Dim strUrlReferrer As String = Request.UrlReferrer.ToString
			'	If Not strUrlReferrer.Contains("B0020/SendMail") Then
			'		Session("B0020SendMailRtnUrl") = strUrlReferrer
			'	End If
			'End If

			'ASI[25 Oct 2019] : To load UseName in dropdown of SendMail screen, fetch list whose status 1 as well it's kariana not 1
			Dim MakorinList = db.M0010.Where(Function(m) m.STATUS = True And m.KARIANA <> True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ThenBy(Function(m) m.USERNM).ToList
			Dim blanckUserNmAdd As New M0010
			blanckUserNmAdd.USERID = "0"
			blanckUserNmAdd.USERNM = ""
			MakorinList.Insert(0, blanckUserNmAdd)
			ViewBag.MakorinList = MakorinList

			Dim strTitle As String = ""
			If shorikbn = 4 OrElse shorikbn = 5 Then
				strTitle = String.Format("業務申請{0}の連絡", strShori)
			Else
				strTitle = String.Format("業務{0}の連絡", strShori)
			End If

			w0040(0).MAILSUBJECT = String.Format(strTitle & "【{0}】", s0010.APPNM)
			w0040(0).MAILBODYHEAD = "＜" & strTitle & "＞" & vbCrLf

			Dim sbMailBody As New StringBuilder()
			sbMailBody.AppendLine(Session("LoginUsernm") & "さんによって " & w0040(0).UPDTDT.ToString("yyyy年MM月dd日 HH：mm") & " に" & vbCrLf &
								   String.Format("業務が{0}されました。", strShori))
			sbMailBody.AppendLine()

			'If d0010.GYOMYMD = d0010.GYOMYMDED Then
			'	sbMailBody.AppendLine("業務期間   ： " & d0010.GYOMYMD)
			'Else
			'	sbMailBody.AppendLine("業務期間   ： " & d0010.GYOMYMD & "～" & d0010.GYOMYMDED)
			'End If

			Dim strStartDay As String = "(" & w0040(0).GYOMYMD.ToString("ddd") & ")"
			'業務終了日付がDate?型なので、日付に変換する
			Dim strEndDay As String = "(" & Date.Parse(w0040(0).GYOMYMDED).ToString("ddd") & ")"

			sbMailBody.AppendLine("業務期間     ： " & w0040(0).GYOMYMD & strStartDay & "～" & w0040(0).GYOMYMDED & strEndDay)

			If shorikbn = 1 Then
				Dim sbPtn As New StringBuilder
				If w0040(0).PTNFLG = True Then
					If w0040(0).PTN1 Then
						sbPtn.AppendLine("月")
					End If
					If w0040(0).PTN2 Then
						sbPtn.AppendLine("火")
					End If
					If w0040(0).PTN3 Then
						sbPtn.AppendLine("水")
					End If
					If w0040(0).PTN4 Then
						sbPtn.AppendLine("木")
					End If
					If w0040(0).PTN5 Then
						sbPtn.AppendLine("金")
					End If
					If w0040(0).PTN6 Then
						sbPtn.AppendLine("土")
					End If
					If w0040(0).PTN7 Then
						sbPtn.AppendLine("日")
					End If
				End If
				Dim strPtn As String = ""
				If sbPtn.Length > 0 Then
					strPtn = sbPtn.ToString.Replace(vbCrLf, "，")
					strPtn = strPtn.Substring(0, strPtn.Length - 1)
				End If
				sbMailBody.AppendLine("繰り返し　　 ： " & strPtn)
			End If

			Dim strOAjknst As String = ""
			Dim strOAjknend As String = ""

			sbMailBody.AppendLine("拘束時間     ： " & w0040(0).KSKJKNST.Substring(0, 2) & ":" & w0040(0).KSKJKNST.Substring(2, 2) & "～" & w0040(0).KSKJKNED.Substring(0, 2) & ":" & w0040(0).KSKJKNED.Substring(2, 2))
			sbMailBody.AppendLine("カテゴリー 　： " & w0040(0).M0020.CATNM)
			sbMailBody.AppendLine("番組名       ： " & w0040(0).BANGUMINM)

			If w0040(0).OAJKNST IsNot Nothing Then
				strOAjknst = w0040(0).OAJKNST.Substring(0, 2) & ":" & w0040(0).OAJKNST.Substring(2, 2)
			End If
			If w0040(0).OAJKNED IsNot Nothing Then
				strOAjknend = "～" & w0040(0).OAJKNED.Substring(0, 2) & ":" & w0040(0).OAJKNED.Substring(2, 2)
			End If
			sbMailBody.AppendLine("OA時間      ： " & strOAjknst & strOAjknend)
			sbMailBody.AppendLine("内容         ： " & w0040(0).NAIYO)
			sbMailBody.AppendLine("場所         ： " & w0040(0).BASYO)
			sbMailBody.AppendLine("番組担当者   ： " & w0040(0).BANGUMITANTO)
			sbMailBody.AppendLine("連絡先       ： " & w0040(0).BANGUMIRENRK)
			sbMailBody.AppendLine("備考         ： " & w0040(0).BIKO)

			Dim dicList As New Dictionary(Of Boolean, String)
			dicList.Add(False, "送信しない")
			dicList.Add(True, "送信する")

			Dim sbTntAnanm As New StringBuilder
			Dim bolKikan As Boolean = False
			Dim inIdx As Integer = 0
			Dim strKey As String = ""

			'w0040(0).W0050 = w0040.W0050.OrderBy(Function(d) d.M00101.USERSEX).ThenBy(Function(d) d.M00101.HYOJJN).ToList
			Dim w0050_all As New List(Of W0050)
			For Each w0040_ind As W0040 In w0040
				w0050_all.AddRange(w0040_ind.W0050)
			Next

			For Each item In w0050_all.OrderBy(Function(d) d.M00101.USERSEX).ThenBy(Function(d) d.M00101.HYOJJN).ToList
				'メール送信するしないの初期設定
				If shorikbn = 4 OrElse shorikbn = 5 Then
					'承認、却下の場合、初期値は「送信する」
					item.MAILSOUSIAN = True
				Else
					'メール送信テーブルより初期値を設定
					If w0040(0).GYOMYMD <= Today.AddDays(s0010.SHHYOJDAYCNT) Then
						bolKikan = True
					Else
						bolKikan = False
					End If
					Dim m0070 = db.M0070.Where(Function(m) m.KIKAN = bolKikan And m.SOUSIN = item.SOUSIN And m.KAKUNIN = item.CHK).ToList
					If m0070.Count > 0 Then
						item.MAILSOUSIAN = m0070(0).MAILSOUSIAN
					End If
				End If

				strKey = String.Format("W0050[{0}].MAILSOUSIAN", inIdx)
				ViewData(strKey) = New SelectList(dicList.Select(Function(f) New With {.Value = f.Key, .Text = f.Value}), "Value", "Text", item.MAILSOUSIAN)
				inIdx += 1

				If item.DELFLG = False Then
					sbTntAnanm.AppendLine(item.M00101.USERNM)
				End If
			Next

			Dim strTntAnanm As String = sbTntAnanm.ToString().Replace(vbCrLf, "，")
			strTntAnanm = strTntAnanm.Substring(0, strTntAnanm.Length - 1)
			sbMailBody.AppendLine("担当アナ     ： " & strTntAnanm)

			w0040(0).MAILBODY = sbMailBody.ToString

			Dim strScheme As String = Request.Url.Scheme
			Dim strAuthority As String = Request.Url.Authority
			Dim strPath As String = HttpRuntime.AppDomainAppVirtualPath
			Dim strBaseUrl As String = String.Format("{0}://{1}{2}", strScheme, strAuthority, strPath)

			w0040(0).MAILAPPURL = String.Format(strBaseUrl & "/C0040/Index/{0}?stdt={1}", "xxx", w0040(0).GYOMYMD.ToString("yyyy/MM/dd"))
			w0040(0).W0050 = w0050_all
			Return View(w0040(0))
		End Function

		' POST: D0010/SendMail/5
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function SendMail(<Bind(Include:="ACUSERID,SHORIKBN,GYOMNO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,JTJKNST,JTJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,MAILSUBJECT,MAILBODYHEAD,MAILBODY,MAILNOTE,MAILAPPURL,UPDTDT,W0050,ToMailList,CcMailList")> ByVal w0040 As W0040) As ActionResult

			Dim loginUserId As Integer = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Dim strLoginUserMailAdd As String = db.M0010.Find(loginUserId).MAILLADDESS
			Dim strS0010CcAddress As String = db.S0010.Find(1).CCADDRESS

			Dim lstCcMailid As New List(Of String)
			If String.IsNullOrEmpty(strS0010CcAddress) = False Then
				lstCcMailid.Add(strS0010CcAddress)
			End If

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
					If String.IsNullOrEmpty(m0010.MAILLADDESS) = False Then
						lstToMailid.Add(m0010.MAILLADDESS)
					End If

					If item.SENDKEITAI = True AndAlso String.IsNullOrEmpty(m0010.KEITAIADDESS) = False Then
						lstToMailid.Add(m0010.KEITAIADDESS)
					End If

					If lstToMailid.Count > 0 Then
						If DoSendMail(lstToMailid.ToArray, w0040.MAILSUBJECT, strMyBody, lstCcMailid.ToArray, , strLoginUserMailAdd) = False Then
							Return View(w0040)
						Else
							'削除以外の場合、送信フラグを「済」に設定する
							If w0040.SHORIKBN <> 3 Then
								Dim lstw0050 = db.W0050.Where(Function(w) w.ACUSERID = w0040.ACUSERID And w.SHORIKBN = w0040.SHORIKBN And w.USERID = item.USERID And w.DELFLG = False).ToList
								For Each W0050 In lstw0050
									Dim d0020 = db.D0020.Find(W0050.GYOMNO, item.USERID)
									If d0020 IsNot Nothing Then
										d0020.SOUSIN = True
										db.Entry(d0020).State = EntityState.Modified
										bolUpdt = True
									End If
								Next
							End If
						End If
					End If

				End If

			Next


			'ASI[2019/11/05] Cc Mail List Added
			For Each emailCcUserID As Short In w0040.CcMailList
				If emailCcUserID <> 0 Then
					Dim m0010Cc = db.M0010.Find(emailCcUserID)
					If String.IsNullOrEmpty(m0010Cc.MAILLADDESS) = False Then
						lstCcMailid.Add(m0010Cc.MAILLADDESS)
					End If
				End If
			Next


			'ASI[2019/11/05] To Mail List Added
			For Each emailToUserID As Short In w0040.ToMailList
				Dim lstOnlyToMailids As New List(Of String)
				If emailToUserID <> 0 Then
					Dim m0010To = db.M0010.Find(emailToUserID)
					If String.IsNullOrEmpty(m0010To.MAILLADDESS) = False Then
						lstOnlyToMailids.Add(m0010To.MAILLADDESS)
					End If

					Dim strUrl As String = "システムでシフト表を参照" & vbCrLf & w0040.MAILAPPURL.Replace("xxx", emailToUserID)
					Dim strMyBody As String = strBody & vbCrLf & vbCrLf & vbCrLf & strUrl
					If lstOnlyToMailids.Count > 0 Then
						If DoSendMail(lstOnlyToMailids.ToArray, w0040.MAILSUBJECT, strMyBody, lstCcMailid.ToArray, , strLoginUserMailAdd) = False Then
							Return View(w0040)
						End If
					End If
				End If

			Next

			If bolUpdt = True Then
				DoDBSaveChanges(db)
			End If

			If w0040.SHORIKBN = 1 OrElse w0040.SHORIKBN = 4 Then
				'新規 OR 承認
				If String.IsNullOrEmpty(Session("B0020CreateRtnUrl")) = False Then
					Return Redirect(Session("B0020CreateRtnUrl"))
				End If

			ElseIf w0040.SHORIKBN = 2 Then
				'修正
				If String.IsNullOrEmpty(Session("B0020EditRtnUrl" & w0040.GYOMNO)) = False Then
					Return Redirect(Session("B0020EditRtnUrl" & w0040.GYOMNO))
				End If

			ElseIf w0040.SHORIKBN = 3 Then
				'削除
				If String.IsNullOrEmpty(Session("B0020DeleteRtnUrl")) = False Then
					'修正画面から削除された場合、修正画面を呼び出した画面に戻る
					If Session("B0020DeleteRtnUrl").ToString.Contains("B0020/Edit") AndAlso String.IsNullOrEmpty(Session("B0020EditRtnUrl" & w0040.GYOMNO)) = False Then
						Return Redirect(Session("B0020EditRtnUrl" & w0040.GYOMNO))
					Else
						'削除画面を呼び出した画面に戻る
						Return Redirect(Session("B0020DeleteRtnUrl"))
					End If
				End If

			ElseIf w0040.SHORIKBN = 5 Then
                '申請却下
                '申請業務を削除時
                If String.IsNullOrEmpty(Session("B0030DeleteRtnUrl")) = False Then
                    Return Redirect(Session("B0030DeleteRtnUrl"))
                End If
                '申請業務を申請者でないアナで登録時
                If String.IsNullOrEmpty(Session("B0020CreateRtnUrl")) = False Then
                    Return Redirect(Session("B0020CreateRtnUrl"))
                End If

            End If

			'If String.IsNullOrEmpty(Session("B0020SendMailRtnUrl")) = False Then
			'	Return Redirect(Session("B0020SendMailRtnUrl"))
			'Else
			'	Return RedirectToAction("Index")
			'End If

			Return RedirectToAction("Index")
		End Function

		''' <summary>
		''' 各担当アナに対し、業務期間の終了年月が公休展開されているかチェックする
		''' </summary>
		Private Function CheckKokyutenkaiDone(ByVal d0010 As D0010) As Boolean
			Dim bolRtn As Boolean = True

			If d0010.D0020 IsNot Nothing AndAlso d0010.D0020.Count > 0 Then

				Dim db As New Model1
				Dim bolNGUser As Boolean = False
				Dim strNGUsernms As String = ""
				Dim lstNGUsernm As New List(Of String)
				Dim lstNGUserColnm As New List(Of String)
				Dim lstD0020OK As New List(Of D0020)
				Dim intGyomNengetsu As Integer = Integer.Parse(Date.Parse(d0010.GYOMYMDED.ToString).ToString("yyyyMM"))
				Dim lstColNm = GetM0150AnaCol(d0010)

				For i As Integer = 0 To d0010.D0020.Count - 1
					Dim item = d0010.D0020(i)

					bolNGUser = False

					Dim d0030 = db.D0030.Find(item.USERID, intGyomNengetsu)
					If d0030 Is Nothing Then
						bolNGUser = True
					End If

					If bolNGUser = True Then
						lstNGUsernm.Add(item.USERNM)

						For Each itemColNm In lstColNm
							If itemColNm.COLNAME = item.COLNM Then
								lstNGUserColnm.Add(itemColNm.COLVALUE)
								Exit For
							End If
						Next

						If String.IsNullOrEmpty(strNGUsernms) = False Then
							strNGUsernms = strNGUsernms & "，"
						End If

						strNGUsernms = strNGUsernms & item.USERNM
					Else
						lstD0020OK.Add(item)
					End If
				Next

				If lstNGUsernm.Count > 0 Then
					d0010.D0020 = lstD0020OK
					d0010.RefAnalist = lstNGUsernm
					d0010.RefCatAnalist = lstNGUserColnm

					'D0020アナウンサー情報のエラー情報をクリアする（クリアしないと、Hidden項目のValue値に変更前の値が残ってしまうため）
					For intIdx As Integer = ModelState.Keys.Count - 1 To 0 Step -1
						Dim strKey As String = ModelState.Keys(intIdx).ToString
						If strKey.Contains("D0020[") Then
							ModelState.Remove(strKey)
						End If
					Next

					Dim strMsg As String = "「{0}」が公休展開されていません。"

					ModelState.AddModelError(String.Empty, String.Format(strMsg, strNGUsernms))

					bolRtn = False
				End If

				db.Dispose()
				db = Nothing

			End If

			Return bolRtn
		End Function

		''' <summary>
		''' 未確認の不適合要因があるかチェックする（候補検索から選択して追加した後、不適合要因が発生してしまう可能性があるため）
		''' </summary>
		Private Function CheckYoin(ByVal d0010 As D0010) As Boolean

			Dim bolRtn As Boolean = True

			If d0010.D0020 IsNot Nothing AndAlso d0010.D0020.Count > 0 Then

				Dim db As New Model1

				CreateKohoData(d0010.ACUSERID, d0010.GYOMNO, d0010.GYOMYMD, d0010.GYOMYMDED, d0010.KSKJKNST, d0010.KSKJKNED, d0010.PATTERN, d0010.MON, d0010.TUE, d0010.WED, d0010.TUR, d0010.FRI, d0010.SAT, d0010.SUN, d0010.WEEKA, d0010.WEEKB)

				Dim bolNGUser As Boolean = False
				Dim strNGUsernms As String = ""
				Dim lstNGUsernm As New List(Of String)
				Dim lstD0020OK As New List(Of D0020)
				Dim bolYOINIDYES As Boolean = False         '確認済みの不適合要因があるかどうかのフラグ

                For i As Integer = 0 To d0010.D0020.Count - 1
                    Dim item = d0010.D0020(i)

                    '業務修正の時、既存のユーザーは要因チェック不要
                    If d0010.GYOMNO <> 0 Then
						Dim d0020 = db.D0020.Find(d0010.GYOMNO, item.USERID)
						If d0020 IsNot Nothing Then
							lstD0020OK.Add(item)
							Continue For
						End If
					End If

					bolNGUser = False

                    '候補者一覧で選択時休日設定を更新していたので以下チェックをしているが、選択時更新ではなく、業務登録更新時更新になったので不要
                    ''未解消の3:時間休、5:公休、6:代休、7:振休が存在するかチェックする
                    'Dim lstW0020 = (From t In db.W0020 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList
                    'If lstW0020.Count > 0 Then
                    '    bolNGUser = True
                    'End If

                    '未解消のWブッキングが存在するかチェックする
                    If bolNGUser = False Then
						Dim lstW0030 = (From t In db.W0030 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList
						If lstW0030.Count > 0 Then
							bolNGUser = True
						End If
					End If

					'未確認の不適合要因があるかチェックする
					If bolNGUser = False Then
						Dim lstuserw0010 = (From t In db.W0010 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID And t.YOINID <> 0).ToList
						If lstuserw0010.Count > 0 Then
							'確認済みの要因なしの場合
							If item.YOINIDYES Is Nothing Then
								bolNGUser = True
							Else
                                '要因が8:強休、9:時間強休の場合、又は、確認済みの要因はあるが、他に未確認の要因が有る場合(13:法休、14:振替法休)
                                For Each w0010 As W0010 In lstuserw0010
                                    If w0010.YOINID = 8 OrElse w0010.YOINID = 9 OrElse
                                        (w0010.YOINID <> 13 AndAlso w0010.YOINID <> 14 AndAlso w0010.YOINID <> 15 AndAlso item.YOINIDYES.Contains(w0010.YOINID) = False) OrElse
                                         ((w0010.YOINID = 13 OrElse w0010.YOINID = 14) AndAlso item.YOINIDYES.Contains("3567") = False) OrElse
                                         (w0010.YOINID = 15 AndAlso item.YOINIDYES.Contains("11") = False) Then
                                        bolNGUser = True
                                        Exit For
                                    End If
                                Next
                            End If
						End If
					End If

					If bolNGUser = True Then
						lstNGUsernm.Add(item.USERNM)

						If String.IsNullOrEmpty(strNGUsernms) = False Then
							strNGUsernms = strNGUsernms & "，"
						End If

						strNGUsernms = strNGUsernms & item.USERNM

						'確認済みの不適合要因がある場合のメッセッジを出すためのフラグ設定
						If item.YOINIDYES IsNot Nothing Then
							bolYOINIDYES = True
						End If
					Else
						lstD0020OK.Add(item)
					End If

				Next

				If lstNGUsernm.Count > 0 Then
					d0010.D0020 = lstD0020OK
					d0010.RefAnalist = lstNGUsernm

					'D0020アナウンサー情報のエラー情報をクリアする（クリアしないと、Hidden項目のValue値に変更前の値が残ってしまうため）
					For intIdx As Integer = ModelState.Keys.Count - 1 To 0 Step -1
						Dim strKey As String = ModelState.Keys(intIdx).ToString
						If strKey.Contains("D0020[") Then
							ModelState.Remove(strKey)
						End If
					Next

					Dim strMsg As String = ""
					If bolYOINIDYES Then
                        '確認済みの不適合要因がある場合
                        strMsg = "「{0}」の情報が更新されています。個人シフトを確認してください。"
                    Else
						'確認済みの不適合要因がない場合（業務承認の場合）
						strMsg = "「{0}」に未解消の不適合要因があります。"
					End If
					ModelState.AddModelError(String.Empty, String.Format(strMsg, strNGUsernms))

					bolRtn = False
				End If

				db.Dispose()
				db = Nothing
			End If

			Return bolRtn
		End Function


		Private Function DoDBSaveChanges(ByVal db As Model1) As Boolean
			Dim bolRtn As Boolean = True

			'Hosting.HostingEnvironment.ApplicationHost.GetSiteName()
			'Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")

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
					bolRtn = False
					Throw ex
					tran.Rollback()
				End Try
			End Using

			Return bolRtn
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

		Private Function GetMaxHenkorrkcd() As Decimal
			'変更履歴コードの最大値の取得
			Dim decMaxHenkorrkcd As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "00000")
			Dim lstHENKORRKCD = (From t In db.D0070 Where t.HENKORRKCD > decMaxHenkorrkcd Select t.HENKORRKCD).ToList
			If lstHENKORRKCD.Count > 0 Then
				decMaxHenkorrkcd = lstHENKORRKCD.Max
			End If

			Return decMaxHenkorrkcd
		End Function

		Function GetMaxKyukHenkorrkcd() As Decimal
			'休暇変更履歴コードの最大値の取得
			Dim decMaxHenkorrkcd As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "00000")
			Dim lstHENKORRKCD = (From t In db.D0150 Where t.HENKORRKCD > decMaxHenkorrkcd Select t.HENKORRKCD).ToList
			If lstHENKORRKCD.Count > 0 Then
				decMaxHenkorrkcd = lstHENKORRKCD.Max
			End If

			Return decMaxHenkorrkcd
		End Function

		Private Function GetAllAnanm(ByVal lstD0020 As List(Of D0020), ByVal lstD0021 As List(Of D0021)) As String
			Dim sbAna As New StringBuilder
			Dim m0010 As M0010 = Nothing

			If lstD0020 IsNot Nothing Then

				For Each d0020 In lstD0020
					If d0020.M0010 Is Nothing Then
						d0020.M0010 = db.M0010.Find(d0020.USERID)
					End If
				Next
				lstD0020 = lstD0020.OrderBy(Function(d) d.M0010.USERSEX).ThenBy(Function(d) d.M0010.HYOJJN).ToList

				For Each item In lstD0020
					If String.IsNullOrEmpty(item.USERNM) = False Then
						sbAna.AppendLine(item.USERNM)
					Else
						sbAna.AppendLine(item.M0010.USERNM)
					End If
				Next

			End If

			If lstD0021 IsNot Nothing Then
				For Each item In lstD0021
					If String.IsNullOrEmpty(item.ANNACATNM) = False Then
						sbAna.AppendLine(item.ANNACATNM)
					End If
				Next
			End If

			Dim strAnanm As String = ""
			If sbAna.Length > 0 Then
				strAnanm = sbAna.ToString.Replace(vbCrLf, "，")
				strAnanm = strAnanm.Substring(0, strAnanm.Length - 1)
			End If

			m0010 = Nothing
			sbAna = Nothing

			Return strAnanm
		End Function

		Private Sub CopyValue(ByRef d0010New As D0010, ByVal d0010 As D0010)
			d0010New.CATCD = d0010.CATCD
			d0010New.BANGUMINM = d0010.BANGUMINM
			d0010New.OAJKNST = d0010.OAJKNST
			d0010New.OAJKNED = d0010.OAJKNED
			d0010New.NAIYO = d0010.NAIYO
			d0010New.BASYO = d0010.BASYO
			d0010New.BIKO = d0010.BIKO
			d0010New.BANGUMITANTO = d0010.BANGUMITANTO
			d0010New.BANGUMIRENRK = d0010.BANGUMIRENRK
			d0010New.SPORTCATCD = If(d0010.SPORTCATCD = 0, Nothing, d0010.SPORTCATCD)
			d0010New.SPORTSUBCATCD = If(d0010.SPORTSUBCATCD = 0, Nothing, d0010.SPORTSUBCATCD)
			d0010New.SPORTFLG = d0010.SPORTFLG
			d0010New.OYAGYOMFLG = d0010.OYAGYOMFLG
			d0010New.SPORT_OYAFLG = False
			d0010New.SAIJKNST = d0010.SAIJKNST
			d0010New.SAIJKNED = d0010.SAIJKNED

		End Sub

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
			d0070.SPORTCATCD = If(d0010.SPORTCATCD = 0, Nothing, d0010.SPORTCATCD)
			d0070.SPORTSUBCATCD = If(d0010.SPORTSUBCATCD = 0, Nothing, d0010.SPORTSUBCATCD)
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

		Private Sub CopyGyom(ByRef w0040 As W0040, ByVal d0010 As D0010)
			w0040.GYOMYMD = d0010.GYOMYMD
			w0040.GYOMYMDED = d0010.GYOMYMDED
			w0040.KSKJKNST = d0010.KSKJKNST
			w0040.KSKJKNED = d0010.KSKJKNED
			w0040.CATCD = d0010.CATCD
			w0040.BANGUMINM = d0010.BANGUMINM
			w0040.OAJKNST = d0010.OAJKNST
			w0040.OAJKNED = d0010.OAJKNED
			w0040.NAIYO = d0010.NAIYO
			w0040.BASYO = d0010.BASYO
			w0040.BIKO = d0010.BIKO
			w0040.BANGUMITANTO = d0010.BANGUMITANTO
			w0040.BANGUMIRENRK = d0010.BANGUMIRENRK
			w0040.PTNFLG = d0010.PATTERN
			w0040.PTN1 = d0010.MON
			w0040.PTN2 = d0010.TUE
			w0040.PTN3 = d0010.WED
			w0040.PTN4 = d0010.TUR
			w0040.PTN5 = d0010.FRI
			w0040.PTN6 = d0010.SAT
			w0040.PTN7 = d0010.SUN
		End Sub

		Private Sub CopyAna(ByRef w0050 As W0050, ByVal d0020 As D0020)
			w0050.USERID = d0020.USERID
			w0050.SHIFTMEMO = d0020.SHIFTMEMO
			w0050.CHK = d0020.CHK
			w0050.SOUSIN = d0020.SOUSIN
		End Sub

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


		Private Function DoSendMail(ByVal strToEmailIds As String(),
				Optional strSubject As String = Nothing,
				Optional strBody As String = Nothing,
				Optional strCcEmailIds As String() = Nothing,
				Optional strBccEmailIds As String() = Nothing,
				Optional strFromEmailId As String = "",
				Optional bolHtmlBody As Boolean = False,
				Optional strAattchments As String() = Nothing,
				Optional strSMTPServer As String = "",
				Optional intPORT As Integer = 0
				) As Boolean

			Dim bolResult As Boolean = True

			Using smtpClient As New SmtpClient()

				Using mailMessage As MailMessage = New MailMessage
					Try
						With mailMessage
							.Subject = If(strSubject = "", "", strSubject)
							.Body = strBody
							.IsBodyHtml = bolHtmlBody

							If String.IsNullOrEmpty(strFromEmailId) = False Then
								.From = New MailAddress(strFromEmailId)
							End If

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

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		'ASI[12 Dec 2019] : Created below Function to fetch displayName of Model property.Annotted on property in model.
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

		'Function CheckYoin(ByVal d0010 As D0010) As Boolean

		'	Dim bolRtn As Boolean = True

		'	If d0010.D0020 IsNot Nothing AndAlso d0010.D0020.Count > 0 Then

		'		Dim db As New Model1

		'		Dim lstw0010bf = (From t In db.W0010 Where t.ACUSERID = d0010.ACUSERID).ToList
		'		Dim lstw0020bf = (From t In db.W0020 Where t.ACUSERID = d0010.ACUSERID).ToList
		'		Dim lstw0030bf = (From t In db.W0030 Where t.ACUSERID = d0010.ACUSERID).ToList

		'		CreateKohoData(d0010.ACUSERID, d0010.GYOMNO, d0010.GYOMYMD, d0010.GYOMYMDED, d0010.KSKJKNST, d0010.KSKJKNED, d0010.PATTERN, d0010.MON, d0010.TUE, d0010.WED, d0010.TUR, d0010.FRI, d0010.SAT, d0010.SUN)

		'		If lstw0010bf.Count = 0 Then
		'			'業務承認で選択して業務登録を呼び出した時、候補検索を行っていない場合のチェック。
		'			Dim strUsernm As String = ""
		'			Dim bolErr As Boolean = False
		'			Dim lstD0020OK As New List(Of D0020)

		'			For i As Integer = 0 To d0010.D0020.Count - 1
		'				Dim item = d0010.D0020(i)

		'				'修正の時、追加のユーザーのみチェックする
		'				If d0010.GYOMNO <> 0 Then
		'					Dim d0020 = db.D0020.Find(d0010.GYOMNO, item.USERID)
		'					If d0020 IsNot Nothing Then
		'						Continue For
		'					End If
		'				End If

		'				Dim lstuserw0010 = (From t In db.W0010 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID And t.YOINID <> 0).ToList

		'				If lstuserw0010.Count > 0 Then
		'					bolErr = True

		'					If String.IsNullOrEmpty(strUsernm) = False Then
		'						strUsernm = strUsernm & "，"
		'					End If

		'					strUsernm = strUsernm & item.USERNM
		'					Continue For
		'				Else
		'					lstD0020OK.Add(item)
		'				End If
		'			Next

		'			If bolErr Then
		'				d0010.D0020 = lstD0020OK
		'				ModelState.AddModelError(String.Empty, String.Format("アナウンサー「{0}」が不適合要因無しではありません。候補検索を行って確認してください。", strUsernm))

		'				lstw0010bf = (From t In db.W0010 Where t.ACUSERID = d0010.ACUSERID).ToList

		'				If lstw0010bf.Count > 0 Then
		'					For Each item In lstw0010bf
		'						db.W0010.Remove(item)
		'					Next

		'					db.SaveChanges()
		'				End If

		'			End If
		'		Else
		'			Dim strErrMsg As String = "!!!"

		'			For i As Integer = 0 To d0010.D0020.Count - 1

		'				Dim item = d0010.D0020(i)
		'				Dim strKey As String = String.Format("D0020[{0}].USERID", i)

		'				'修正の時、追加のユーザーのみチェックする
		'				If d0010.GYOMNO <> 0 Then
		'					Dim d0020 = db.D0020.Find(d0010.GYOMNO, item.USERID)
		'					If d0020 IsNot Nothing Then
		'						Continue For
		'					End If
		'				End If

		'				Dim lstuserw0010bf = (From t In lstw0010bf Where t.USERID = item.USERID).ToList
		'				Dim lstuserw0010aft = (From t In db.W0010 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList

		'				If lstuserw0010bf.Count <> lstuserw0010aft.Count Then
		'					ModelState.AddModelError(strKey, strErrMsg)
		'					Continue For
		'				End If

		'				Dim bolErr As Boolean = False

		'				For Each W0010bf In lstuserw0010bf
		'					Dim bolFound As Boolean = False
		'					For Each w0010aft In lstuserw0010aft
		'						If W0010bf.YOINID = w0010aft.YOINID Then
		'							bolFound = True
		'							Exit For
		'						End If
		'					Next
		'					If bolFound = False Then
		'						bolErr = True
		'						Exit For
		'					End If
		'				Next

		'				If bolErr Then
		'					ModelState.AddModelError(strKey, strErrMsg)
		'					Continue For
		'				End If

		'				Dim lstuserw0020bf = (From t In lstw0020bf Where t.USERID = item.USERID).ToList
		'				Dim lstuserw0020aft = (From t In db.W0020 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList

		'				If lstuserw0020bf.Count <> lstuserw0020aft.Count Then
		'					ModelState.AddModelError(strKey, strErrMsg)
		'					Continue For
		'				End If

		'				For Each W0020bf In lstuserw0020bf
		'					Dim bolFound As Boolean = False
		'					For Each w0020aft In lstuserw0020aft
		'						If W0020bf.YOINID = w0020aft.YOINID AndAlso W0020bf.KYUKYMD = w0020aft.KYUKYMD AndAlso W0020bf.JKNST = w0020aft.JKNST AndAlso W0020bf.JKNED = w0020aft.JKNED Then
		'							bolFound = True
		'							Exit For
		'						End If
		'					Next
		'					If bolFound = False Then
		'						bolErr = True
		'						Exit For
		'					End If
		'				Next

		'				If bolErr Then
		'					ModelState.AddModelError(strKey, strErrMsg)
		'					Continue For
		'				End If

		'				Dim lstuserw0030bf = (From t In lstw0030bf Where t.USERID = item.USERID).ToList
		'				Dim lstuserw0030aft = (From t In db.W0030 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList

		'				If lstuserw0030bf.Count <> lstuserw0030aft.Count Then
		'					ModelState.AddModelError(strKey, strErrMsg)
		'					Continue For
		'				End If

		'				For Each W0030bf In lstuserw0030bf
		'					Dim bolFound As Boolean = False
		'					For Each w0030aft In lstuserw0030aft
		'						If W0030bf.YOINID = w0030aft.YOINID AndAlso W0030bf.GYOMNO = w0030aft.GYOMNO AndAlso
		'							W0030bf.GYOMYMD = w0030aft.GYOMYMD AndAlso W0030bf.GYOMYMDED = w0030aft.GYOMYMDED AndAlso
		'							W0030bf.KSKJKNST = w0030aft.KSKJKNST AndAlso W0030bf.KSKJKNED = w0030aft.KSKJKNED AndAlso
		'							W0030bf.CATCD = w0030aft.CATCD AndAlso W0030bf.BANGUMINM = w0030aft.BANGUMINM AndAlso
		'							W0030bf.OAJKNST = w0030aft.OAJKNST AndAlso W0030bf.OAJKNED = w0030aft.OAJKNED AndAlso
		'							W0030bf.NAIYO = w0030aft.NAIYO AndAlso W0030bf.BASYO = w0030aft.BASYO AndAlso W0030bf.BIKO = w0030aft.BIKO AndAlso
		'							W0030bf.BANGUMITANTO = w0030aft.BANGUMITANTO AndAlso W0030bf.BANGUMIRENRK = w0030aft.BANGUMIRENRK Then

		'							bolFound = True
		'							Exit For
		'						End If
		'					Next
		'					If bolFound = False Then
		'						bolErr = True
		'						Exit For
		'					End If
		'				Next

		'				If bolErr Then
		'					ModelState.AddModelError(strKey, strErrMsg)
		'					Continue For
		'				End If

		'			Next
		'		End If

		'	End If

		'	Return bolRtn
		'End Function

		'ASI[26 Dec 2019]:UnCheck value of CHK col from table which already check
		Function ResetYoinUserData(userid As String) As JsonResult
			If userid IsNot Nothing Then

				Dim intAcuserid As Integer = Session("LoginUserid")

				Dim w0010List = db.W0010.Where(Function(t) t.ACUSERID = intAcuserid And t.USERID = userid).ToList()
				For Each item In w0010List
					item.CHK = False
					db.Entry(item).State = EntityState.Modified
				Next

				If (DoDBSaveChanges(db)) Then
					Return Json(New With {.success = True})
				Else
					Return Json(New With {.success = False})
				End If

			Else
				Return Json(New With {.success = False})
			End If

		End Function

		'ASI[03 Jan 2020]:アナの時間が違う場合、エラーメッセージを表示する。
		Function CheckEachAnaJkn(ByVal d0010 As D0010) As Boolean
			If d0010 IsNot Nothing Then
				If d0010.D0020 IsNot Nothing AndAlso d0010.D0020.Count > 0 Then

					Dim d0010DbData = db.D0010.Find(d0010.GYOMNO)

					'実時間で前後関係チェック
					Dim jtjknst As Date = d0010DbData.JTJKNST
					Dim jtjkned As Date = d0010DbData.JTJKNED

					For Each item In d0010.D0020
						Dim d0020 = db.D0020.Find(d0010.GYOMNO, item.USERID)
						If d0020 IsNot Nothing Then
							If d0020.JTJKNST <> jtjknst OrElse d0020.JTJKNED <> jtjkned Then
								ModelState.AddModelError(String.Empty, String.Format("アナの時間が違うため、更新することはできません。"))
								Return False
							End If
						End If
					Next
				End If
			End If

			Return True
		End Function

		Function GetM0150AnaCol(ByVal d0010 As D0010)

			'ASI[12 Dec 2019]
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

			Dim lsColNmUni = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL01_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL01_TYPE, .HYOJJN = m1.COL01_HYOJJN2, .COLNAME = "COL01", .COLVALUE = If(m1.COL01 IsNot Nothing, m1.COL01, col01DsiplayName)}).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL02_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL02_TYPE, .HYOJJN = m1.COL02_HYOJJN2, .COLNAME = "COL02", .COLVALUE = If(m1.COL02 IsNot Nothing, m1.COL02, col02DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL03_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL03_TYPE, .HYOJJN = m1.COL03_HYOJJN2, .COLNAME = "COL03", .COLVALUE = If(m1.COL03 IsNot Nothing, m1.COL03, col03DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL04_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL04_TYPE, .HYOJJN = m1.COL04_HYOJJN2, .COLNAME = "COL04", .COLVALUE = If(m1.COL04 IsNot Nothing, m1.COL04, col04DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL05_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL05_TYPE, .HYOJJN = m1.COL05_HYOJJN2, .COLNAME = "COL05", .COLVALUE = If(m1.COL05 IsNot Nothing, m1.COL05, col05DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL06_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL06_TYPE, .HYOJJN = m1.COL06_HYOJJN2, .COLNAME = "COL06", .COLVALUE = If(m1.COL06 IsNot Nothing, m1.COL06, col06DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL07_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL07_TYPE, .HYOJJN = m1.COL07_HYOJJN2, .COLNAME = "COL07", .COLVALUE = If(m1.COL07 IsNot Nothing, m1.COL07, col07DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL08_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL08_TYPE, .HYOJJN = m1.COL08_HYOJJN2, .COLNAME = "COL08", .COLVALUE = If(m1.COL08 IsNot Nothing, m1.COL08, col08DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL09_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL09_TYPE, .HYOJJN = m1.COL09_HYOJJN2, .COLNAME = "COL09", .COLVALUE = If(m1.COL09 IsNot Nothing, m1.COL09, col09DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL10_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL10_TYPE, .HYOJJN = m1.COL10_HYOJJN2, .COLNAME = "COL10", .COLVALUE = If(m1.COL10 IsNot Nothing, m1.COL10, col10DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL11_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL11_TYPE, .HYOJJN = m1.COL11_HYOJJN2, .COLNAME = "COL11", .COLVALUE = If(m1.COL11 IsNot Nothing, m1.COL11, col11DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL12_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL12_TYPE, .HYOJJN = m1.COL12_HYOJJN2, .COLNAME = "COL12", .COLVALUE = If(m1.COL12 IsNot Nothing, m1.COL12, col12DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL13_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL13_TYPE, .HYOJJN = m1.COL13_HYOJJN2, .COLNAME = "COL13", .COLVALUE = If(m1.COL13 IsNot Nothing, m1.COL13, col13DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL14_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL14_TYPE, .HYOJJN = m1.COL14_HYOJJN2, .COLNAME = "COL14", .COLVALUE = If(m1.COL14 IsNot Nothing, m1.COL14, col14DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL15_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL15_TYPE, .HYOJJN = m1.COL15_HYOJJN2, .COLNAME = "COL15", .COLVALUE = If(m1.COL15 IsNot Nothing, m1.COL15, col15DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL16_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL16_TYPE, .HYOJJN = m1.COL16_HYOJJN2, .COLNAME = "COL16", .COLVALUE = If(m1.COL16 IsNot Nothing, m1.COL16, col16DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL17_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL17_TYPE, .HYOJJN = m1.COL17_HYOJJN2, .COLNAME = "COL17", .COLVALUE = If(m1.COL17 IsNot Nothing, m1.COL17, col17DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL18_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL18_TYPE, .HYOJJN = m1.COL18_HYOJJN2, .COLNAME = "COL18", .COLVALUE = If(m1.COL18 IsNot Nothing, m1.COL18, col18DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL19_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL19_TYPE, .HYOJJN = m1.COL19_HYOJJN2, .COLNAME = "COL19", .COLVALUE = If(m1.COL19 IsNot Nothing, m1.COL19, col19DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL20_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL20_TYPE, .HYOJJN = m1.COL20_HYOJJN2, .COLNAME = "COL20", .COLVALUE = If(m1.COL20 IsNot Nothing, m1.COL20, col20DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL21_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL21_TYPE, .HYOJJN = m1.COL21_HYOJJN2, .COLNAME = "COL21", .COLVALUE = If(m1.COL21 IsNot Nothing, m1.COL21, col21DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL22_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL22_TYPE, .HYOJJN = m1.COL22_HYOJJN2, .COLNAME = "COL22", .COLVALUE = If(m1.COL22 IsNot Nothing, m1.COL22, col22DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL23_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL23_TYPE, .HYOJJN = m1.COL23_HYOJJN2, .COLNAME = "COL23", .COLVALUE = If(m1.COL23 IsNot Nothing, m1.COL23, col23DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL24_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL24_TYPE, .HYOJJN = m1.COL24_HYOJJN2, .COLNAME = "COL24", .COLVALUE = If(m1.COL24 IsNot Nothing, m1.COL24, col24DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL25_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL25_TYPE, .HYOJJN = m1.COL25_HYOJJN2, .COLNAME = "COL25", .COLVALUE = If(m1.COL25 IsNot Nothing, m1.COL25, col25DsiplayName)})).ToList()

			'ASI CHANGE[2020/02/18] COL26 TO COL50 ADDED
			Dim lsColNmUni2 = db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL26_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL26_TYPE, .HYOJJN = m1.COL26_HYOJJN2, .COLNAME = "COL26", .COLVALUE = If(m1.COL26 IsNot Nothing, m1.COL26, COL26DsiplayName)}).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL27_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL27_TYPE, .HYOJJN = m1.COL27_HYOJJN2, .COLNAME = "COL27", .COLVALUE = If(m1.COL27 IsNot Nothing, m1.COL27, COL27DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL28_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL28_TYPE, .HYOJJN = m1.COL28_HYOJJN2, .COLNAME = "COL28", .COLVALUE = If(m1.COL28 IsNot Nothing, m1.COL28, COL28DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL29_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL29_TYPE, .HYOJJN = m1.COL29_HYOJJN2, .COLNAME = "COL29", .COLVALUE = If(m1.COL29 IsNot Nothing, m1.COL29, COL29DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL30_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL30_TYPE, .HYOJJN = m1.COL30_HYOJJN2, .COLNAME = "COL30", .COLVALUE = If(m1.COL30 IsNot Nothing, m1.COL30, COL30DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL31_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL31_TYPE, .HYOJJN = m1.COL31_HYOJJN2, .COLNAME = "COL31", .COLVALUE = If(m1.COL31 IsNot Nothing, m1.COL31, COL31DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL32_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL32_TYPE, .HYOJJN = m1.COL32_HYOJJN2, .COLNAME = "COL32", .COLVALUE = If(m1.COL32 IsNot Nothing, m1.COL32, COL32DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL33_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL33_TYPE, .HYOJJN = m1.COL33_HYOJJN2, .COLNAME = "COL33", .COLVALUE = If(m1.COL33 IsNot Nothing, m1.COL33, COL33DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL34_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL34_TYPE, .HYOJJN = m1.COL34_HYOJJN2, .COLNAME = "COL34", .COLVALUE = If(m1.COL34 IsNot Nothing, m1.COL34, COL34DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL35_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL35_TYPE, .HYOJJN = m1.COL35_HYOJJN2, .COLNAME = "COL35", .COLVALUE = If(m1.COL35 IsNot Nothing, m1.COL35, COL35DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL36_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL36_TYPE, .HYOJJN = m1.COL36_HYOJJN2, .COLNAME = "COL36", .COLVALUE = If(m1.COL36 IsNot Nothing, m1.COL36, COL36DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL37_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL37_TYPE, .HYOJJN = m1.COL37_HYOJJN2, .COLNAME = "COL37", .COLVALUE = If(m1.COL37 IsNot Nothing, m1.COL37, COL37DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL38_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL38_TYPE, .HYOJJN = m1.COL38_HYOJJN2, .COLNAME = "COL38", .COLVALUE = If(m1.COL38 IsNot Nothing, m1.COL38, COL38DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL39_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL39_TYPE, .HYOJJN = m1.COL39_HYOJJN2, .COLNAME = "COL39", .COLVALUE = If(m1.COL39 IsNot Nothing, m1.COL39, COL39DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL40_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL40_TYPE, .HYOJJN = m1.COL40_HYOJJN2, .COLNAME = "COL40", .COLVALUE = If(m1.COL40 IsNot Nothing, m1.COL40, COL40DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL41_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL41_TYPE, .HYOJJN = m1.COL41_HYOJJN2, .COLNAME = "COL41", .COLVALUE = If(m1.COL41 IsNot Nothing, m1.COL41, COL41DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL42_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL42_TYPE, .HYOJJN = m1.COL42_HYOJJN2, .COLNAME = "COL42", .COLVALUE = If(m1.COL42 IsNot Nothing, m1.COL42, COL42DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL43_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL43_TYPE, .HYOJJN = m1.COL43_HYOJJN2, .COLNAME = "COL43", .COLVALUE = If(m1.COL43 IsNot Nothing, m1.COL43, COL43DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL44_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL44_TYPE, .HYOJJN = m1.COL44_HYOJJN2, .COLNAME = "COL44", .COLVALUE = If(m1.COL44 IsNot Nothing, m1.COL44, COL44DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL45_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL45_TYPE, .HYOJJN = m1.COL45_HYOJJN2, .COLNAME = "COL45", .COLVALUE = If(m1.COL45 IsNot Nothing, m1.COL45, COL45DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL46_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL46_TYPE, .HYOJJN = m1.COL46_HYOJJN2, .COLNAME = "COL46", .COLVALUE = If(m1.COL46 IsNot Nothing, m1.COL46, COL46DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL47_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL47_TYPE, .HYOJJN = m1.COL47_HYOJJN2, .COLNAME = "COL47", .COLVALUE = If(m1.COL47 IsNot Nothing, m1.COL47, COL47DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL48_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL48_TYPE, .HYOJJN = m1.COL48_HYOJJN2, .COLNAME = "COL48", .COLVALUE = If(m1.COL48 IsNot Nothing, m1.COL48, COL48DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL49_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL49_TYPE, .HYOJJN = m1.COL49_HYOJJN2, .COLNAME = "COL49", .COLVALUE = If(m1.COL49 IsNot Nothing, m1.COL49, COL49DsiplayName)})).
						Union(db.M0150.Where(Function(m1) m1.SPORTSUBCATCD = d0010.SPORTSUBCATCD AndAlso ((m1.COL50_TYPE = 2))).Select(Function(m1) New With {.COLTYPE = m1.COL50_TYPE, .HYOJJN = m1.COL50_HYOJJN2, .COLNAME = "COL50", .COLVALUE = If(m1.COL50 IsNot Nothing, m1.COL50, COL50DsiplayName)})).ToList()

			For Each item In lsColNmUni2
				lsColNmUni.Add(item)
			Next

			Return lsColNmUni.OrderBy(Function(m1) m1.HYOJJN).ToList()

		End Function

	End Class

End Namespace
