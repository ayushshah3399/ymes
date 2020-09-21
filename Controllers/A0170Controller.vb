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
    Public Class A0170Controller
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

		' GET: M0090
		'Function Index() As ActionResult
		'    Dim m0090 = db.M0090.Include(Function(m) m.M0010).Include(Function(m) m.M0020)
		'    Return View(m0090.ToList())
		'End Function

		'ASI[21 Oct 2019] : Added to param[WeekA & WeekB] in this Action.
		Function Index(ByVal Naiyo As String, ByVal Basyo As String, ByVal Bangumitanto As String, ByVal Renraku As String,
						 ByVal Gyost As String, ByVal Gyoend As String, ByVal GyoendTo As String,
						  ByVal Kskjknst As String, ByVal Kskjkned As String,
						  ByVal Oajknst As String, ByVal Oajkned As String,
						  ByVal Banguminm As String, ByVal Biko As String, ByVal CATCD As String, ByVal USERID As String, ByVal ANAID As String,
						  ByVal PtnAna2 As System.Nullable(Of Boolean), ByVal PtnflgYes As System.Nullable(Of Boolean), ByVal PtnflgNo As System.Nullable(Of Boolean),
						  ByVal Ptn1 As System.Nullable(Of Boolean), ByVal Ptn2 As System.Nullable(Of Boolean), ByVal Ptn3 As System.Nullable(Of Boolean),
						   ByVal Ptn4 As System.Nullable(Of Boolean), ByVal Ptn5 As System.Nullable(Of Boolean), ByVal Ptn6 As System.Nullable(Of Boolean),
						   ByVal Ptn7 As System.Nullable(Of Boolean), ByVal WeekA As System.Nullable(Of Boolean), ByVal WeekB As System.Nullable(Of Boolean), ByVal TaishoNo As System.Nullable(Of Boolean), ByVal TaishoYes As System.Nullable(Of Boolean)) As ActionResult
			Dim strQuery As String = ""

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")


			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			ViewBag.LoginUserSystem = Session("LoginUserSystem")
			ViewBag.LoginUserKanri = Session("LoginUserKanri")
			ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

			Dim intUserid As Integer = Integer.Parse(loginUserId)
			Dim m0010KOKYU = db.M0010.Find(intUserid)
			ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
			ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL


			Session("Gyost") = Gyost
			Session("Gyoend") = Gyoend
			Session("Kskjknst") = Kskjknst
			Session("Kskjkned") = Kskjkned
			Session("Oajknst") = Oajknst
			Session("Oajkned") = Oajkned
			Session("Banguminm") = Banguminm
			Session("Biko") = Biko
			Session("CATCD") = CATCD
			Session("USERID") = USERID
			Session("ANAID") = ANAID
			Session("PtnAna2") = PtnAna2


			Session("PtnflgYes") = PtnflgYes
			Session("PtnflgNo") = PtnflgNo
			Session("Ptn1") = Ptn1
			Session("Ptn2") = Ptn2
			Session("Ptn3") = Ptn3
			Session("Ptn4") = Ptn4
			Session("Ptn5") = Ptn5
			Session("Ptn6") = Ptn6
			Session("Ptn7") = Ptn7

			'ASI[21 Oct 2019] : Put WeekA & WeekB in Session
			Session("WeekA") = WeekA
			Session("WeekB") = WeekB

			Session("Naiyo") = Naiyo
			Session("Basyo") = Basyo
			Session("Bangumitanto") = Bangumitanto
			Session("Renraku") = Renraku

			Session("TaishoNo") = TaishoNo
			Session("TaishoYes") = TaishoYes

			Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim item As New M0020
			item.CATCD = "0"
			item.CATNM = ""
			lstCATCD.Insert(0, item)
			ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM")

			'Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True And m.KARIANA = False And m.STATUS = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
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

			Dim lstNaiyo = db.M0040.OrderBy(Function(f) f.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			'ASI[21 Oct 2019]: Added check of WeekA & WeekB in this condition 
			If Gyost Is Nothing AndAlso Gyoend Is Nothing AndAlso Kskjknst Is Nothing AndAlso Kskjkned Is Nothing AndAlso Oajknst Is Nothing AndAlso
			Oajkned Is Nothing AndAlso Banguminm Is Nothing AndAlso Biko Is Nothing AndAlso CATCD Is Nothing AndAlso USERID Is Nothing AndAlso ANAID Is Nothing AndAlso
			 PtnflgYes Is Nothing AndAlso PtnflgNo Is Nothing AndAlso Ptn1 Is Nothing AndAlso Ptn2 Is Nothing AndAlso Ptn3 Is Nothing AndAlso
			  Ptn4 Is Nothing AndAlso Ptn5 Is Nothing AndAlso Ptn6 Is Nothing AndAlso Ptn7 Is Nothing AndAlso WeekA Is Nothing AndAlso WeekB Is Nothing AndAlso Naiyo Is Nothing AndAlso Basyo Is Nothing AndAlso
			 Bangumitanto Is Nothing AndAlso Renraku Is Nothing AndAlso TaishoNo Is Nothing AndAlso TaishoYes Is Nothing Then
				Return View()
			End If


			Dim M0090 = From m In db.M0090 Select m

			If Not String.IsNullOrEmpty(Gyost) Then
				If String.IsNullOrEmpty(Gyoend) Then
					Gyoend = Gyost
				End If
				M0090 = M0090.Where(Function(m) (Gyost) <= m.GYOMYMDED AndAlso (Gyoend) >= m.GYOMYMD)
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
				M0090 = M0090.Where(Function(m) (Kskjknst) <= m.KSKJKNED AndAlso (Kskjkned) >= m.KSKJKNST)
			End If


			If Not String.IsNullOrEmpty(CATCD) AndAlso CATCD <> "0" Then
				M0090 = M0090.Where(Function(m) m.CATCD = (CATCD))
			End If


			If Not String.IsNullOrEmpty(Banguminm) Then
				M0090 = M0090.Where(Function(m) m.BANGUMINM.Contains(Banguminm))
			End If

			If Not String.IsNullOrEmpty(Naiyo) Then
				M0090 = M0090.Where(Function(m) m.NAIYO.Contains(Naiyo))
			End If
			If Not String.IsNullOrEmpty(Basyo) Then
				M0090 = M0090.Where(Function(m) m.BASYO.Contains(Basyo))
			End If

			If Not String.IsNullOrEmpty(Bangumitanto) Then
				M0090 = M0090.Where(Function(m) m.BANGUMITANTO.Contains(Bangumitanto))
			End If

			If Not String.IsNullOrEmpty(Renraku) Then
				M0090 = M0090.Where(Function(m) m.BANGUMIRENRK.Contains(Renraku))
			End If

			If Not String.IsNullOrEmpty(Biko) Then
				M0090 = M0090.Where(Function(m) m.BIKO.Contains(Biko))
			End If

			If Not String.IsNullOrEmpty(USERID) AndAlso USERID <> "0" Then
				Dim decUserid As Decimal = Decimal.Parse(USERID)
				Dim M0010 = db.M0010.Find(decUserid)
				M0090 = M0090.Where(Function(m) m.UPDTID = (M0010.USERNM))
			End If

			If Not String.IsNullOrEmpty(ANAID) AndAlso ANAID <> "0" Then
				M0090 = M0090.Where(Function(d1) db.M0110.Any(Function(d2) d2.IKKATUNO = d1.IKKATUNO AndAlso d2.USERID = ANAID))
			End If


			If PtnAna2 = True Then
				M0090 = From t In M0090 Where (From inq In db.M0120 Select inq.IKKATUNO).Contains(t.IKKATUNO)
			End If



			If String.IsNullOrEmpty(Oajknst) = False Then
				If Oajknst.Contains(":") = False Then
					If Oajknst.Length > 2 Then
						Dim strMM As String = Oajknst.Substring(Oajknst.Length - 2, 2)
						Dim strHH As String = Oajknst.Remove(Oajknst.Length - 2, 2)
						Oajknst = strHH.PadLeft(2, "0") & strMM
					Else
						Oajknst = Oajknst.PadLeft(2, "0") & "00"
					End If
				Else
					Oajknst = Oajknst.Replace(":", "").PadLeft(4, "0")
				End If
			End If

			If String.IsNullOrEmpty(Oajkned) = False Then
				If Oajkned.Contains(":") = False Then
					If Oajkned.Length > 2 Then
						Dim strMM As String = Oajkned.Substring(Oajkned.Length - 2, 2)
						Dim strHH As String = Oajkned.Remove(Oajkned.Length - 2, 2)
						Oajkned = strHH.PadLeft(2, "0") & strMM
					Else
						Oajkned = Oajkned.PadLeft(2, "0") & "00"
					End If
				Else
					Oajkned = Oajkned.Replace(":", "").PadLeft(4, "0")
				End If
			End If

			If Not String.IsNullOrEmpty(Oajknst) Then
				If String.IsNullOrEmpty(Oajkned) Then
					Oajkned = Oajknst
				End If
				M0090 = M0090.Where(Function(m) (Oajknst) <= m.OAJKNED AndAlso (Oajkned) >= m.OAJKNST)

			End If

			'Dim query = Entity.M_Employee.SelectMany(Function(e) Entity.M_Position.Where(Function(p) e.PostionId >= p.PositionId))

			If PtnflgYes.HasValue AndAlso PtnflgYes.Value AndAlso PtnflgNo.HasValue AndAlso PtnflgNo.Value Then
				M0090 = M0090.Where(Function(m) m.PTNFLG = True Or m.PTNFLG = False)
			ElseIf PtnflgYes.HasValue AndAlso PtnflgYes.Value Then
				M0090 = M0090.Where(Function(m) m.PTNFLG = True)
			ElseIf PtnflgNo.HasValue AndAlso PtnflgNo.Value Then
				M0090 = M0090.Where(Function(m) m.PTNFLG = False)
			End If

			If PtnflgNo.HasValue AndAlso PtnflgNo.Value Then
				If Ptn1.HasValue AndAlso Ptn1.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.PTN1 = True)
				End If
				If Ptn2.HasValue AndAlso Ptn2.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.PTN2 = True)
				End If
				If Ptn3.HasValue AndAlso Ptn3.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.PTN3 = True)
				End If
				If Ptn4.HasValue AndAlso Ptn4.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.PTN4 = True)
				End If
				If Ptn5.HasValue AndAlso Ptn5.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.PTN5 = True)
				End If
				If Ptn6.HasValue AndAlso Ptn6.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.PTN6 = True)
				End If
				If Ptn7.HasValue AndAlso Ptn7.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.PTN7 = True)
				End If

				'ASI[21 Oct 2019] : Added condition for WeekA & WeekB
				If WeekA.HasValue AndAlso WeekA.Value AndAlso WeekB.Value = False Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.WEEKA = True)
				End If
				If WeekB.HasValue AndAlso WeekB.Value AndAlso WeekA.Value = False Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or m.WEEKB = True)
				End If
				If WeekB.HasValue AndAlso WeekA.Value AndAlso WeekB.Value Then
					M0090 = M0090.Where(Function(m) m.PTNFLG = False Or (m.WEEKA = True Or m.WEEKB = True))
				End If
			Else

				'M0090 = M0090.Where(Function(m) m.PTNFLG = True)
				If Ptn1.HasValue AndAlso Ptn1.Value Then
					M0090 = M0090.Where(Function(m) m.PTN1 = Ptn1.Value)
				End If

				If Ptn2.HasValue AndAlso Ptn2.Value Then
					M0090 = M0090.Where(Function(m) m.PTN2 = Ptn2.Value)
				End If

				If Ptn3.HasValue AndAlso Ptn3.Value Then
					M0090 = M0090.Where(Function(m) m.PTN3 = Ptn3.Value)
				End If
				If Ptn4.HasValue AndAlso Ptn4.Value Then
					M0090 = M0090.Where(Function(m) m.PTN4 = Ptn4.Value)
				End If
				If Ptn5.HasValue AndAlso Ptn5.Value Then
					M0090 = M0090.Where(Function(m) m.PTN5 = Ptn5.Value)
				End If
				If Ptn6.HasValue AndAlso Ptn6.Value Then
					M0090 = M0090.Where(Function(m) m.PTN6 = Ptn6.Value)
				End If
				If Ptn7.HasValue AndAlso Ptn7.Value Then
					M0090 = M0090.Where(Function(m) m.PTN7 = Ptn7.Value)
				End If

				'ASI[21 Oct 2019] : Added condition for WeekA & WeekB
				If WeekA.HasValue AndAlso WeekA.Value AndAlso WeekB.Value = False Then
					M0090 = M0090.Where(Function(m) m.WEEKA = WeekA.Value)
				End If
				If WeekB.HasValue AndAlso WeekB.Value AndAlso WeekA.Value = False Then
					M0090 = M0090.Where(Function(m) m.WEEKB = WeekB.Value)
				End If
				If WeekA.HasValue AndAlso WeekA.Value AndAlso WeekB.Value Then
					M0090 = M0090.Where(Function(m) (m.WEEKA = WeekA.Value Or m.WEEKB = WeekB.Value))
				End If

			End If


			If TaishoYes.HasValue AndAlso TaishoYes.Value AndAlso TaishoNo.HasValue AndAlso TaishoNo.Value Then
				M0090 = M0090.Where(Function(m) m.IKTTAISHO = True Or m.IKTTAISHO = False)
			ElseIf TaishoYes.HasValue AndAlso TaishoYes.Value Then
				M0090 = M0090.Where(Function(m) m.IKTTAISHO = True)
			ElseIf TaishoNo.HasValue AndAlso TaishoNo.Value Then
				M0090 = M0090.Where(Function(m) m.IKTTAISHO = False)
			End If

			Dim lstm0090 = M0090.OrderBy(Function(f) f.M0020.HYOJJN).ThenBy(Function(f) f.CATCD).ThenBy(Function(f) f.GYOMYMD).ThenBy(Function(f) f.KSKJKNST).ToList

			For Each m0090a In lstm0090
				m0090a.M0110 = m0090a.M0110.OrderBy(Function(m) m.M0010.USERSEX).ThenBy(Function(m) m.M0010.HYOJJN).ToList()
			Next

			Return View(lstm0090)

		End Function

		'ASI[21 Oct 2019]: Added WEEKA & WEEKB param in Action
		'POST: M0090
		<HttpPost()>
		Function Index(<Bind(Include:="FLGDEL,IKKATUNO,IKKATUMEMO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,USERID,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,WEEKA,WEEKB,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,HINANO,FMTKBN,ANAIDLIST,KARIANALIST,DATAKBN,HINAMEMO,IKTTAISHO,M0110,M0120,M0020")> ByVal lstm0090 As List(Of M0090), ByVal button As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If button = "downloadcsv" Then
				Return DownloadCsv(lstm0090)
			End If

			For Each item In lstm0090
				If item.FLGDEL = True Then

					Dim m0090 As M0090 = db.M0090.Find(item.IKKATUNO)
					If m0090 Is Nothing Then
						Return HttpNotFound()
					End If

					db.M0090.Remove(m0090)
				End If
			Next

			db.SaveChanges()

			'ASI[21 Oct 2019]: Pass WEEKA & WEEKB param in Action
			Return RedirectToAction("Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
						  .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"), .Oajknst = Session("Oajknst"), .Oajkned = Session("Oajkned"), .Banguminm = Session("Banguminm"),
						  .Biko = Session("Biko"), .CATCD = Session("CATCD"), .USERID = Session("USERID"), .ANAID = Session("ANAID"), .PtnAna2 = Session("PtnAna2"), .PtnflgNo = Session("PtnflgNo"), .PtnflgYes = Session("PtnflgYes"),
						  .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"), .Ptn5 = Session("Ptn5"),
						  .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .WeekA = Session("WeekA"), .WeekB = Session("WeekB"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Renraku = Session("Renraku"), .TaishoNo = Session("TaishoNo"), .TaishoYes = Session("TaishoYes")})
		End Function

		Private Function DownloadCsv(ByVal lstm0090 As List(Of M0090)) As ActionResult

            Dim builder As New StringBuilder()
            Dim strRecord As String = "業務期間,業務期間-終了,パターン設定,拘束時間-開始,拘束時間-終了,カテゴリー名,番組名,OA時間-開始,OA時間-終了,内容,場所,アナウンサー,仮アナカテゴリー,番組担当者,連絡先,備考,最終更新者,一括登録対象"
            
            builder.AppendLine(strRecord)

            For Each m0090 As M0090 In lstm0090
                strRecord = Date.Parse(m0090.GYOMYMD).ToString("yyyy/MM/dd") & ","

                If m0090.GYOMYMDED IsNot Nothing Then
                    strRecord = strRecord & Date.Parse(m0090.GYOMYMDED).ToString("yyyy/MM/dd")
                End If

                strRecord = strRecord & ","

                Dim strPattern As String = ""

                If m0090.PTNFLG = True Then
                  
                    If m0090.PTN1 = True Then
                        strPattern = "月"
                    End If

                    If m0090.PTN2 = True Then
                        If String.IsNullOrEmpty(strPattern) = False Then
                            strPattern = strPattern & "，"           '全角カンマ
                        End If

                        strPattern = strPattern & "火"
                    End If


                    If m0090.PTN3 = True Then
                        If String.IsNullOrEmpty(strPattern) = False Then
                            strPattern = strPattern & "，"           '全角カンマ
                        End If

                        strPattern = strPattern & "水"
                    End If

                    If m0090.PTN4 = True Then
                        If String.IsNullOrEmpty(strPattern) = False Then
                            strPattern = strPattern & "，"           '全角カンマ
                        End If

                        strPattern = strPattern & "木"
                    End If

                    If m0090.PTN5 = True Then
                        If String.IsNullOrEmpty(strPattern) = False Then
                            strPattern = strPattern & "，"           '全角カンマ
                        End If

                        strPattern = strPattern & "金"
                    End If

                    If m0090.PTN6 = True Then
                        If String.IsNullOrEmpty(strPattern) = False Then
                            strPattern = strPattern & "，"           '全角カンマ
                        End If

                        strPattern = strPattern & "土"
                    End If

                    If m0090.PTN7 = True Then
                        If String.IsNullOrEmpty(strPattern) = False Then
                            strPattern = strPattern & "，"           '全角カンマ
                        End If
                        strPattern = strPattern & "日"
                    End If

                Else
                    strPattern = "パターンなし"
                End If

                strRecord = strRecord & strPattern

                strRecord = strRecord & "," & m0090.KSKJKNST.ToString.Substring(0, 2) & ":" & m0090.KSKJKNST.ToString.Substring(2, 2) & "," &
                     m0090.KSKJKNED.ToString.Substring(0, 2) & ":" & m0090.KSKJKNED.ToString.Substring(2, 2) & ","


                If m0090.M0020 IsNot Nothing AndAlso m0090.M0020.CATNM IsNot Nothing Then
                    strRecord = strRecord & m0090.M0020.CATNM
                End If
                strRecord = strRecord & ","

                If m0090.BANGUMINM IsNot Nothing Then
                    strRecord = strRecord & m0090.BANGUMINM
                End If
                strRecord = strRecord & ","

                If m0090.OAJKNST IsNot Nothing Then
                    strRecord = strRecord & m0090.OAJKNST.ToString.Substring(0, 2) & ":" & m0090.OAJKNST.ToString.Substring(2, 2)
                End If
                strRecord = strRecord & ","

                If m0090.OAJKNED IsNot Nothing Then
                    strRecord = strRecord & m0090.OAJKNED.ToString.Substring(0, 2) & ":" & m0090.OAJKNED.ToString.Substring(2, 2)
                End If
                strRecord = strRecord & ","

                If m0090.NAIYO IsNot Nothing Then
                    strRecord = strRecord & m0090.NAIYO
                End If
                strRecord = strRecord & ","

                If m0090.BASYO IsNot Nothing Then
                    strRecord = strRecord & m0090.BASYO
                End If
                strRecord = strRecord & ","

                Dim strAna As String = ""

                If m0090.M0110 IsNot Nothing Then
					For Each m0110 In m0090.M0110.OrderBy(Function(m) m.M0010.USERSEX).ThenBy(Function(m) m.M0010.HYOJJN).ToList
						If String.IsNullOrEmpty(strAna) = False Then
							strAna = strAna & "，"			'全角カンマ
						End If
						strAna = strAna & m0110.M0010.USERNM
					Next
                End If

                strRecord = strRecord & strAna & ","

                Dim strKariAna As String = ""

                If m0090.M0120 IsNot Nothing Then
                    For Each M0120 In m0090.M0120
                        If String.IsNullOrEmpty(strKariAna) = False Then
                            strKariAna = strKariAna & "，"           '全角カンマ
                        End If
                        strKariAna = strKariAna & M0120.ANNACATNM
                    Next
                End If

                strRecord = strRecord & strKariAna & ","

                If m0090.BANGUMITANTO IsNot Nothing Then
                    strRecord = strRecord & m0090.BANGUMITANTO
                End If
                strRecord = strRecord & ","

                If m0090.BANGUMIRENRK IsNot Nothing Then
                    strRecord = strRecord & m0090.BANGUMIRENRK
                End If
                strRecord = strRecord & ","

                If m0090.BIKO IsNot Nothing Then
                    strRecord = strRecord & m0090.BIKO
                End If
                strRecord = strRecord & ","


                If m0090.UPDTID IsNot Nothing Then
                    strRecord = strRecord & m0090.UPDTID
                End If
                strRecord = strRecord & ","


                Dim strIktflg As String = ""
                If m0090.IKTTAISHO IsNot Nothing Then
                    If m0090.IKTTAISHO = True Then
                        strIktflg = "対象"
                    Else
                        strIktflg = "対象外"
                    End If
                    strRecord = strRecord & strIktflg
                End If


                builder.AppendLine(strRecord)
            Next

            ' 生成された文字列を「text/csv」形式（Shift_JIS）で出力
            Return File(System.Text.Encoding.GetEncoding("shift_jis").GetBytes(builder.ToString), "text/csv", "ikatsugyomdata.csv")
        End Function


        ' GET: M0090/Details/5
        Function Details(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0090 As M0090 = db.M0090.Find(id)
			If IsNothing(m0090) Then
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

            Return View(m0090)
        End Function

        ' GET: M0090/Create
        Function Create(hinano As String) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

            Dim lstbangumi = db.M0030.OrderBy(Function(f) f.BANGUMIKN).ToList
            Dim bangumiitem As New M0030
            bangumiitem.BANGUMICD = "0"
            bangumiitem.BANGUMINM = ""
            lstbangumi.Insert(0, bangumiitem)
            ViewBag.BangumiList = lstbangumi

            Dim lstNaiyo = db.M0040.OrderBy(Function(f) f.NAIYO).ToList
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

            ViewData.Add("List", db.M0020.ToList())
            TempData("test") = "test temp data"

            Dim lstCATCD = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
            Dim item As New M0020
            item.CATCD = "0"
            item.CATNM = ""
            lstCATCD.Insert(0, item)
            ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM")

            'Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True And m.KARIANA = False And m.STATUS = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN)
            Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
            Dim lstUSERID = M0010.ToList
            Dim itemm0010 As New M0010
            itemm0010.USERID = "0"
            itemm0010.USERNM = ""
            lstUSERID.Insert(0, itemm0010)
            ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM")
            ViewBag.lstM0010 = lstUSERID


            If String.IsNullOrEmpty(hinano) = False Then

                Dim decHinano As Decimal = Decimal.Parse(hinano)
                Dim d0090 = db.D0090.Find(decHinano)
                If d0090 Is Nothing Then
                    Return HttpNotFound()
                End If

                Dim m0090 As New M0090

                m0090.HINANO = d0090.HINANO
                m0090.FMTKBN = d0090.FMTKBN
                m0090.GYOMYMD = d0090.GYOMYMD
                m0090.GYOMYMDED = d0090.GYOMYMDED
                m0090.KSKJKNST = d0090.KSKJKNST
                m0090.KSKJKNED = d0090.KSKJKNED
                m0090.CATCD = d0090.CATCD
                m0090.BANGUMINM = d0090.BANGUMINM
                m0090.OAJKNST = d0090.OAJKNST
                m0090.OAJKNED = d0090.OAJKNED
                m0090.NAIYO = d0090.NAIYO
                m0090.BASYO = d0090.BASYO
                m0090.BIKO = d0090.BIKO
                m0090.BANGUMITANTO = d0090.BANGUMITANTO
                m0090.BANGUMIRENRK = d0090.BANGUMIRENRK
                m0090.PTNFLG = d0090.PTNFLG
                m0090.PTN1 = d0090.PTN1
                m0090.PTN2 = d0090.PTN2
                m0090.PTN3 = d0090.PTN3
                m0090.PTN4 = d0090.PTN4
                m0090.PTN5 = d0090.PTN5
                m0090.PTN6 = d0090.PTN6
				m0090.PTN7 = d0090.PTN7

				'ASI[21 Oct 2019]: put from d0090 to m0090
				m0090.WEEKA = d0090.WEEKA
				m0090.WEEKB = d0090.WEEKB

				If d0090.D0100 IsNot Nothing AndAlso d0090.D0100.Count > 0 Then
                    Dim lstM0110 As New List(Of M0110)
                    For Each itemd0100 In d0090.D0100
                        Dim M0110 As New M0110
                        M0110.USERID = itemd0100.USERID
                        lstM0110.Add(M0110)
                    Next
                    m0090.M0110 = lstM0110
                End If

                If d0090.D0101 IsNot Nothing AndAlso d0090.D0101.Count > 0 Then
                    Dim lstM0120 As New List(Of M0120)
                    For Each itemd0101 In d0090.D0101
                        Dim M0120 As New M0120
                        M0120.SEQ = itemd0101.SEQ
                        M0120.ANNACATNM = itemd0101.ANNACATNM
                        lstM0120.Add(M0120)
                    Next
                    m0090.M0120 = lstM0120
                End If


                ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM", m0090.CATCD)
                Return View(m0090)

            End If

          

            Return View()
        End Function

        Function Search() As ActionResult
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID")
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM")
            ViewBag.BANGUMICD = New SelectList(db.M0030, "BANGUMICD", "BANGUMINM")
            ViewBag.NAIYOCD = New SelectList(db.M0040, "NAIYOCD", "NAIYO")
            Return View()
        End Function

		'ASI[21 Oct 2019]:Added WEEKA & WEEKB in Action params
		' POST: M0090/Create
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Create(<Bind(Include:="IKKATUNO,IKKATUMEMO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,USERID,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,WEEKA,WEEKB,IKTTAISHO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,HINANO,FMTKBN,ANAIDLIST,KARIANALIST,DATAKBN,HINAMEMO,M0110,M0120")> ByVal m0090 As M0090) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If m0090.FMTKBN Is Nothing Then
				m0090.FMTKBN = 0
			End If

			'「雛形」
			If ModelState.IsValid AndAlso m0090.HINANO > 0 Then

				Dim d0090 = db.D0090.Find(m0090.HINANO)

				'「雛形」を呼び出している場合：使用済み区分に“１”を設定
				d0090.SIYOFLG = True
				d0090.SIYOUSERID = loginUserId
				d0090.SIYODATE = Now
			End If

			If (m0090.FMTKBN = 0 AndAlso ModelState.IsValid) OrElse m0090.FMTKBN = 1 OrElse m0090.FMTKBN = 2 Then
				'OA時間をから：を除外して4桁化する。
				If m0090.OAJKNST IsNot Nothing Then
					m0090.OAJKNST = ChangeToHHMM(m0090.OAJKNST)
				End If
				If m0090.OAJKNED IsNot Nothing Then
					m0090.OAJKNED = ChangeToHHMM(m0090.OAJKNED)
				End If

				'拘束時間から：を除外して4桁化する。
				m0090.KSKJKNST = ChangeToHHMM(m0090.KSKJKNST)
				m0090.KSKJKNED = ChangeToHHMM(m0090.KSKJKNED)
			End If

			'一括業務
			If m0090.FMTKBN = 0 AndAlso ModelState.IsValid Then
				'Dim maxid = (From c In db.M0090.ToList Select c.IKKATUNO).Max()
				'm0090.IKKATUNO = maxid + 1

				Dim decTempIKKATUNO As Decimal = Integer.Parse(DateTime.Today.ToString("yyyyMM") & "000")
				Dim lstIKKATUNO = (From t In db.M0090 Where t.IKKATUNO > decTempIKKATUNO Select t.IKKATUNO).ToList
				If lstIKKATUNO.Count > 0 Then
					decTempIKKATUNO = lstIKKATUNO.Max
				End If
				m0090.IKKATUNO = decTempIKKATUNO + 1

				If m0090.GYOMYMDED Is Nothing Then
					m0090.GYOMYMDED = m0090.GYOMYMD
				End If

				'担当アナ
				If m0090.M0110 IsNot Nothing Then
					For Each itemM0110 In m0090.M0110
						If itemM0110.USERID > 0 Then
							'itemM0110.IKKATUNO = decTempIKKATUNO + 1
							'db.M0110.Add(itemM0110)

							Dim m0110New As New M0110
							m0110New.IKKATUNO = decTempIKKATUNO + 1
							m0110New.USERID = itemM0110.USERID

							db.M0110.Add(m0110New)
						End If

					Next
				End If

				'仮アナ
				If m0090.M0120 IsNot Nothing Then
					Dim intSeq As Integer = 1
					For Each itemM0120 In m0090.M0120
						If String.IsNullOrEmpty(itemM0120.ANNACATNM) = False Then

							'itemM0120.IKKATUNO = decTempIKKATUNO + 1
							'itemM0120.SEQ = intSeq
							'db.M0120.Add(itemM0120)
							'intSeq += 1

							Dim m0120New As New M0120
							m0120New.IKKATUNO = decTempIKKATUNO + 1
							m0120New.SEQ = intSeq
							m0120New.ANNACATNM = itemM0120.ANNACATNM

							db.M0120.Add(m0120New)
							intSeq += 1
						End If
					Next

				End If

				m0090.M0110 = Nothing
				m0090.M0120 = Nothing

				db.M0090.Add(m0090)



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



				Return RedirectToAction("Index")

				'雛形保存
			ElseIf m0090.FMTKBN = 2 Then

				Dim d0090 As New D0090
				d0090.FMTKBN = m0090.FMTKBN
				d0090.GYOMYMD = m0090.GYOMYMD
				d0090.GYOMYMDED = m0090.GYOMYMDED
				d0090.KSKJKNST = m0090.KSKJKNST
				d0090.KSKJKNED = m0090.KSKJKNED
				d0090.CATCD = m0090.CATCD
				d0090.BANGUMINM = m0090.BANGUMINM
				d0090.OAJKNST = m0090.OAJKNST
				d0090.OAJKNED = m0090.OAJKNED
				d0090.NAIYO = m0090.NAIYO
				d0090.BASYO = m0090.BASYO
				d0090.BIKO = m0090.BIKO
				d0090.BANGUMITANTO = m0090.BANGUMITANTO
				d0090.BANGUMIRENRK = m0090.BANGUMIRENRK
				d0090.PTNFLG = m0090.PTNFLG
				d0090.PTN1 = m0090.PTN1
				d0090.PTN2 = m0090.PTN2
				d0090.PTN3 = m0090.PTN3
				d0090.PTN4 = m0090.PTN4
				d0090.PTN5 = m0090.PTN5
				d0090.PTN6 = m0090.PTN6
				d0090.PTN7 = m0090.PTN7

				'ASI[21 Oct 2019]: save from m0090 to d0090
				d0090.WEEKA = m0090.WEEKA
				d0090.WEEKB = m0090.WEEKB

				d0090.DATAKBN = m0090.DATAKBN
				d0090.HINAMEMO = m0090.HINAMEMO
				d0090.SIYOFLG = 0
				d0090.SIYOUSERID = loginUserId
				d0090.STATUS = 0

				Dim decTempHINANO As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "0000")
				Dim lstHINANO = (From t In db.D0090 Where t.HINANO > decTempHINANO Select t.HINANO).ToList
				If lstHINANO.Count > 0 Then
					decTempHINANO = lstHINANO.Max
				End If
				d0090.HINANO = decTempHINANO + 1

				db.D0090.Add(d0090)

				If String.IsNullOrEmpty(m0090.ANAIDLIST) = False Then
					If m0090.ANAIDLIST.Contains("，") Then
						Dim strAnaIds As String() = m0090.ANAIDLIST.Split("，")
						For Each strId In strAnaIds
							Dim d0100 As New D0100
							d0100.HINANO = d0090.HINANO
							d0100.USERID = strId
							db.D0100.Add(d0100)
						Next
					Else
						If m0090.ANAIDLIST > 0 Then
							Dim d0100 As New D0100
							d0100.HINANO = d0090.HINANO
							d0100.USERID = m0090.ANAIDLIST
							db.D0100.Add(d0100)
						End If

					End If
				End If

				If String.IsNullOrEmpty(m0090.KARIANALIST) = False Then
					If m0090.KARIANALIST.Contains("，") Then
						Dim strAnaIds As String() = m0090.KARIANALIST.Split("，")
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
						d0101.ANNACATNM = Trim(m0090.KARIANALIST.Replace(vbCrLf, " "))
						db.D0101.Add(d0101)
					End If
				End If

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

						Dim strfmt As String = ""
						If m0090.FMTKBN = 1 Then
							strfmt = "下書"
						Else
							strfmt = "雛形"
						End If

						TempData("success") = String.Format("{0}が新規に保存されました。", strfmt)

					Catch ex As Exception
						tran.Rollback()
						Throw ex
					End Try
				End Using

				Return RedirectToAction("Create")

				'ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0090.CATCD)
				'ViewBag.SIYOUSERID = New SelectList(db.M0010, "USERID", "LOGINID", d0090.SIYOUSERID)

				'Return View("CreateShita", d0090)
			End If

			ViewBag.BangumiList = db.M0030.ToList
			ViewBag.NaiyouList = db.M0040.ToList


			Dim lstm0020 = db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN).ToList
			Dim item1 As New M0020
			item1.CATCD = "0"
			item1.CATNM = ""
			lstm0020.Insert(0, item1)
			ViewBag.CATCD = New SelectList(lstm0020, "CATCD", "CATNM")

			'Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True And m.KARIANA = False And m.STATUS = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN)
			Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			Dim lstUSERID = M0010.ToList
			Dim itemm0010 As New M0010
			itemm0010.USERID = "0"
			itemm0010.USERNM = ""
			lstUSERID.Insert(0, itemm0010)
			ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM")
			ViewBag.lstM0010 = lstUSERID

			Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
			Dim anacatitem As New M0080
			anacatitem.ANNACATNO = "0"
			anacatitem.ANNACATNM = ""
			lstKarianacat.Insert(0, anacatitem)
			ViewBag.KarianacatList = lstKarianacat

			Return View(m0090)
		End Function

		' GET: M0090/Edit/5
		Function Edit(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0090 As M0090 = db.M0090.Find(id)
			If IsNothing(m0090) Then
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

			Dim lstbangumi = db.M0030.OrderBy(Function(f) f.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(f) f.NAIYO).ToList
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

			ViewBag.CATCD = New SelectList(db.M0020.Where(Function(m) m.HYOJ = True).OrderBy(Function(m) m.HYOJJN), "CATCD", "CATNM", m0090.CATCD)

			Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			Dim lstUSERID = M0010.ToList
			Dim itemm0010 As New M0010
			itemm0010.USERID = "0"
			itemm0010.USERNM = ""
			lstUSERID.Insert(0, itemm0010)
			ViewBag.USERID = New SelectList(db.M0010, "USERID", "USERNM")
			ViewBag.lstM0010 = lstUSERID

			m0090.M0110 = m0090.M0110.OrderBy(Function(m) m.M0010.USERSEX).ThenBy(Function(m) m.M0010.HYOJJN).ToList()

			Return View(m0090)
		End Function

		'ASI[21 Oct 2019]: Added params WEEKA & WEEKB in Edit Action
		' POST: M0090/Edit/5
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Edit(<Bind(Include:="IKKATUNO,IKKATUMEMO,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,USERID,BIKO,BANGUMITANTO,BANGUMIRENRK,PTNFLG,PTN1,PTN2,PTN3,PTN4,PTN5,PTN6,PTN7,WEEKA,WEEKB,IKTTAISHO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM,HINANO,FMTKBN,ANAIDLIST,KARIANALIST,DATAKBN,HINAMEMO,M0110,M0120")> ByVal m0090 As M0090) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			If (m0090.FMTKBN = 0 AndAlso ModelState.IsValid) OrElse m0090.FMTKBN = 1 OrElse m0090.FMTKBN = 2 Then
				'OA時間をから：を除外して4桁化する。
				If m0090.OAJKNST IsNot Nothing Then
					m0090.OAJKNST = ChangeToHHMM(m0090.OAJKNST)
				End If
				If m0090.OAJKNED IsNot Nothing Then
					m0090.OAJKNED = ChangeToHHMM(m0090.OAJKNED)
				End If

				'拘束時間から：を除外して4桁化する。
				m0090.KSKJKNST = ChangeToHHMM(m0090.KSKJKNST)
				m0090.KSKJKNED = ChangeToHHMM(m0090.KSKJKNED)
			End If

			If m0090.FMTKBN = 0 AndAlso ModelState.IsValid Then

				If m0090.GYOMYMDED Is Nothing Then
					m0090.GYOMYMDED = m0090.GYOMYMD
				End If

				'削除されたアナの更新
				Dim listdbM0110 As List(Of M0110) = (From t In db.M0110 Where t.IKKATUNO = m0090.IKKATUNO).ToList
				For Each itemdb In listdbM0110
					Dim bolExist As Boolean = False
					If m0090.M0110 IsNot Nothing Then
						For Each itemM0110 In m0090.M0110
							If itemM0110.USERID = itemdb.USERID Then
								db.Entry(itemdb).State = EntityState.Detached
								bolExist = True
								Exit For
							End If
						Next
					End If
					If bolExist = False Then
						db.M0110.Remove(itemdb)
					End If
				Next

				'追加されたアナの更新
				If m0090.M0110 IsNot Nothing Then
					For Each itemM0110 In m0090.M0110

						itemM0110.IKKATUNO = m0090.IKKATUNO

						Dim m0110 As M0110 = db.M0110.Find(m0090.IKKATUNO, itemM0110.USERID)
						If m0110 Is Nothing Then
							If itemM0110.USERID > 0 Then
								db.M0110.Add(itemM0110)
							End If


						Else

							db.Entry(m0110).State = EntityState.Detached
							db.Entry(itemM0110).State = EntityState.Modified
						End If
					Next
				End If


				Dim listdbM0120 As List(Of M0120) = (From t In db.M0120 Where t.IKKATUNO = m0090.IKKATUNO).ToList

				'追加された仮アナの更新
				If m0090.M0120 IsNot Nothing Then

					Dim maxseq As Integer = 0
					If listdbM0120.Count > 0 Then
						maxseq = listdbM0120.Max(Function(f) f.SEQ)
					End If

					For Each itemM0120 In m0090.M0120
						If itemM0120.SEQ = 0 Then
							If String.IsNullOrEmpty(itemM0120.ANNACATNM) = False Then
								maxseq = maxseq + 1
								itemM0120.SEQ = maxseq
								itemM0120.IKKATUNO = m0090.IKKATUNO
								db.M0120.Add(itemM0120)
							End If
						End If
					Next
				End If

				'変更、又は削除された仮アナの更新
				For Each itemdb In listdbM0120
					Dim bolExist As Boolean = False
					If m0090.M0120 IsNot Nothing Then
						For Each itemM0120 In m0090.M0120
							If itemdb.SEQ = itemM0120.SEQ Then
								If String.IsNullOrEmpty(itemM0120.ANNACATNM) = False Then
									bolExist = True
									db.Entry(itemdb).State = EntityState.Detached
									If itemdb.ANNACATNM <> itemM0120.ANNACATNM Then
										itemM0120.IKKATUNO = m0090.IKKATUNO
										db.Entry(itemM0120).State = EntityState.Modified
									End If
								End If
								Exit For
							End If
						Next
					End If

					If bolExist = False Then
						db.M0120.Remove(itemdb)
					End If
				Next

				m0090.M0110 = Nothing
				m0090.M0120 = Nothing

				db.Entry(m0090).State = EntityState.Modified

				'db.Configuration.ValidateOnSaveEnabled = False
				'db.SaveChanges()
				'db.Configuration.ValidateOnSaveEnabled = True

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

				'ASI[21 Oct 2019]: Pass WEEKA & WEEKB param in Action
				Return RedirectToAction("Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
					.Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"), .Oajknst = Session("Oajknst"), .Oajkned = Session("Oajkned"), .Banguminm = Session("Banguminm"),
					.Biko = Session("Biko"), .CATCD = Session("CATCD"), .USERID = Session("USERID"), .ANAID = Session("ANAID"), .PtnAna2 = Session("PtnAna2"), .PtnflgNo = Session("PtnflgNo"), .PtnflgYes = Session("PtnflgYes"),
					.Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"), .Ptn5 = Session("Ptn5"),
					.Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .WeekA = Session("WeekA"), .WeekB = Session("WeekB"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Renraku = Session("Renraku"), .TaishoNo = Session("TaishoNo"), .TaishoYes = Session("TaishoYes")})
				'雛形保存
			ElseIf m0090.FMTKBN = 2 Then

				Dim d0090 As New D0090
				d0090.FMTKBN = m0090.FMTKBN
				d0090.GYOMYMD = m0090.GYOMYMD
				d0090.GYOMYMDED = m0090.GYOMYMDED
				d0090.KSKJKNST = m0090.KSKJKNST
				d0090.KSKJKNED = m0090.KSKJKNED
				d0090.CATCD = m0090.CATCD
				d0090.BANGUMINM = m0090.BANGUMINM
				d0090.OAJKNST = m0090.OAJKNST
				d0090.OAJKNED = m0090.OAJKNED
				d0090.NAIYO = m0090.NAIYO
				d0090.BASYO = m0090.BASYO
				d0090.BIKO = m0090.BIKO
				d0090.BANGUMITANTO = m0090.BANGUMITANTO
				d0090.BANGUMIRENRK = m0090.BANGUMIRENRK
				d0090.PTNFLG = m0090.PTNFLG
				d0090.PTN1 = m0090.PTN1
				d0090.PTN2 = m0090.PTN2
				d0090.PTN3 = m0090.PTN3
				d0090.PTN4 = m0090.PTN4
				d0090.PTN5 = m0090.PTN5
				d0090.PTN6 = m0090.PTN6
				d0090.PTN7 = m0090.PTN7

				'ASI[21 Oct 2019]: fill from m0090
				d0090.WEEKA = m0090.WEEKA
				d0090.WEEKB = m0090.WEEKB

				d0090.DATAKBN = m0090.DATAKBN
				d0090.HINAMEMO = m0090.HINAMEMO
				d0090.SIYOFLG = 0
				d0090.SIYOUSERID = loginUserId
				d0090.STATUS = 0

				Dim decTempHINANO As Decimal = Decimal.Parse(DateTime.Today.ToString("yyyyMMdd") & "0000")
				Dim lstHINANO = (From t In db.D0090 Where t.HINANO > decTempHINANO Select t.HINANO).ToList
				If lstHINANO.Count > 0 Then
					decTempHINANO = lstHINANO.Max
				End If
				d0090.HINANO = decTempHINANO + 1

				db.D0090.Add(d0090)

				If String.IsNullOrEmpty(m0090.ANAIDLIST) = False Then
					If m0090.ANAIDLIST.Contains("，") Then
						Dim strAnaIds As String() = m0090.ANAIDLIST.Split("，")
						For Each strId In strAnaIds
							Dim d0100 As New D0100
							d0100.HINANO = d0090.HINANO
							d0100.USERID = strId
							db.D0100.Add(d0100)
						Next
					Else
						If m0090.ANAIDLIST > 0 Then
							Dim d0100 As New D0100
							d0100.HINANO = d0090.HINANO
							d0100.USERID = m0090.ANAIDLIST
							db.D0100.Add(d0100)
						End If

					End If
				End If

				If String.IsNullOrEmpty(m0090.KARIANALIST) = False Then
					If m0090.KARIANALIST.Contains("，") Then
						Dim strAnaIds As String() = m0090.KARIANALIST.Split("，")
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
						d0101.ANNACATNM = Trim(m0090.KARIANALIST.Replace(vbCrLf, " "))
						db.D0101.Add(d0101)
					End If
				End If

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

						Dim strfmt As String = ""
						If m0090.FMTKBN = 1 Then
							strfmt = "下書"
						Else
							strfmt = "雛形"
						End If

						TempData("success") = String.Format("{0}が新規に保存されました。", strfmt)

					Catch ex As Exception
						tran.Rollback()
						Throw ex
					End Try
				End Using


			End If

			Dim lstbangumi = db.M0030.OrderBy(Function(f) f.BANGUMIKN).ToList
			Dim bangumiitem As New M0030
			bangumiitem.BANGUMICD = "0"
			bangumiitem.BANGUMINM = ""
			lstbangumi.Insert(0, bangumiitem)
			ViewBag.BangumiList = lstbangumi

			Dim lstNaiyo = db.M0040.OrderBy(Function(f) f.NAIYO).ToList
			Dim naiyoitem As New M0040
			naiyoitem.NAIYOCD = "0"
			naiyoitem.NAIYO = ""
			lstNaiyo.Insert(0, naiyoitem)
			ViewBag.NaiyouList = lstNaiyo

			Dim M0020 = db.M0020.OrderBy(Function(f) f.HYOJJN)
			Dim lstCATCD = M0020.ToList
			Dim item As New M0020
			item.CATCD = "0"
			item.CATNM = ""
			lstCATCD.Insert(0, item)
			ViewBag.CATCD = New SelectList(lstCATCD, "CATCD", "CATNM")

			Dim M0010 = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.KARIANA = False AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			Dim lstUSERID = M0010.ToList
			Dim itemm0010 As New M0010
			itemm0010.USERID = "0"
			itemm0010.USERNM = ""
			lstUSERID.Insert(0, itemm0010)
			ViewBag.USERID = New SelectList(lstUSERID, "USERID", "USERNM")
			ViewBag.lstM0010 = lstUSERID

			Dim lstKarianacat = db.M0080.OrderBy(Function(m) m.ANNACATNM).ToList
			Dim anacatitem As New M0080
			anacatitem.ANNACATNO = "0"
			anacatitem.ANNACATNM = ""
			lstKarianacat.Insert(0, anacatitem)
			ViewBag.KarianacatList = lstKarianacat

			Return View(m0090)
		End Function

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

        ' GET: M0090/Delete/5
        Function Delete(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim m0090 As M0090 = db.M0090.Find(id)
			If IsNothing(m0090) Then
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
           
            Return View(m0090)
        End Function

        ' POST: M0090/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Decimal) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If


			Dim m0090 As M0090 = db.M0090.Find(id)
            db.M0090.Remove(m0090)
            db.SaveChanges()

			'ASI[21 Oct 2019]: Pass WEEKA & WEEKB param in Action
			Return RedirectToAction("Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
		 .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"), .Oajknst = Session("Oajknst"), .Oajkned = Session("Oajkned"), .Banguminm = Session("Banguminm"),
		 .Biko = Session("Biko"), .CATCD = Session("CATCD"), .USERID = Session("USERID"), .ANAID = Session("ANAID"), .PtnAna2 = Session("PtnAna2"), .PtnflgNo = Session("PtnflgNo"), .PtnflgYes = Session("PtnflgYes"),
		 .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"), .Ptn5 = Session("Ptn5"),
		 .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .WeekA = Session("WeekA"), .WeekB = Session("WeekB"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Renraku = Session("Renraku"), .TaishoNo = Session("TaishoNo"), .TaishoYes = Session("TaishoYes")})

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

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

	End Class

End Namespace
