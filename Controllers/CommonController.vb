Imports System.Web.Mvc

Namespace Controllers
	Public Class CommonController
		Inherits Controller

		Private db As New Model1
		Private Days As String() = {"日", "月", "火", "水", "木", "金", "土"}
		' GET: Common
		'sportdata : sportdata include or not
		Function GetPersonalShiftData(ByVal intDays As Integer, ByVal ParaDt As Date, ByVal id As String, ByVal sportdata As String) As List(Of C0040)
			Dim SearchDt As Date = Nothing
			Dim intId As Integer = 0
			If id IsNot Nothing Then
				intId = Integer.Parse(id)
			End If
			Dim listdata As New List(Of C0040)

			For i = 0 To intDays
				Dim bolNOKokyu As Boolean = False

				Dim data As New C0040

				SearchDt = ParaDt.AddDays(i).ToString("yyyy/MM/dd")

				'ASI[02 Aug 2019]: set DESKMEMOEXISTFLG property to decide is there deskmemo exist for date or not.
				'Based on value of that flag display link of DeskMemo in _個人シフト screen
				Dim DESKMEMOEXISTFLG As Boolean
				Dim HI_Date As String
				HI_Date = SearchDt.ToString("yyyy/MM/dd")
				Dim deskMemoCount = (From d1 In db.D0120 Join d2 In db.D0130
								On d1.DESKNO Equals d2.DESKNO
									 Join d3 In db.D0110 On d2.DESKNO Equals d3.DESKNO
									 Where d2.USERID = id And d3.KAKUNINID <> 5 And
									(HI_Date >= d1.SHIFTYMDST And HI_Date <= d1.SHIFTYMDED
									) Select d1.DESKNO).Count

				If deskMemoCount > 0 Then
					DESKMEMOEXISTFLG = True
				Else
					DESKMEMOEXISTFLG = False
				End If

				'If loginUserSystem = False AndAlso loginUserKanri = False Then
				Dim strKouKyudt As String = SearchDt.ToString("yyyyMMdd")
				strKouKyudt = strKouKyudt.Substring(0, 6)
				Dim intKokyudt As Integer = Integer.Parse(strKouKyudt)
				Dim d0030Day = db.D0030.Find(intId, intKokyudt)
				If d0030Day Is Nothing Then
					bolNOKokyu = True
				End If

				'End If

				'個人メモ
				Dim dateSearch As DateTime = DateTime.Parse(SearchDt)
				Dim intUserid As Integer = Integer.Parse(intId)
				Dim d0140Search = db.D0140.Find(intUserid, dateSearch)
				If d0140Search IsNot Nothing Then
					data.MYMEMO = d0140Search.USERMEMO
				End If


				Dim strKyushutsu As String = ""

				If bolNOKokyu = False Then
					Dim bolDataHave As Boolean = False
					Dim bolKyushutsu As Boolean = False
					'Dim bolHIYOBI As Boolean = False
					Dim rowbackcolor As String = ""
					Dim rowwakucolor As String = ""
					Dim rowfontcolor As String = ""

					'休暇
					Dim strSearchDate As String = SearchDt.ToString.Replace("/", "")
					Dim d0040data = From m In db.D0040 Select m
					'd0040data = d0040data.Where(Function(m) m.NENGETU = strSearchDate.Substring(0, 6) And m.HI = strSearchDate.Substring(6, 2) And {"6", "7", "8", "9"}.Contains(m.KYUKCD))
					d0040data = d0040data.Where(Function(m) m.NENGETU = strSearchDate.Substring(0, 6) And m.HI = strSearchDate.Substring(6, 2) And m.USERID = id And m.KYUKCD <> "1")
					d0040data = d0040data.OrderBy(Function(f) f.JKNST)
					For Each item In d0040data
						Dim datad0040 As New C0040

						If item.KYUKCD = "2" Or item.KYUKCD = "10" Or item.KYUKCD = "13" Or item.KYUKCD = "14" Then
							bolKyushutsu = True
							If item.KYUKCD = "2" Then
								strKyushutsu = "1"
							ElseIf item.KYUKCD = "10" Then
								strKyushutsu = "2"
							ElseIf item.KYUKCD = "13" Then
								strKyushutsu = "3"
							Else
								strKyushutsu = "4"
							End If

							Dim m0060 = db.M0060.Find(item.KYUKCD)

							rowbackcolor = m0060.BACKCOLOR
							rowwakucolor = m0060.WAKUCOLOR
							rowfontcolor = m0060.FONTCOLOR

							data.ROWBACKCOLOR = rowbackcolor
							data.ROWWAKUCOLOR = rowwakucolor
							data.ROWFONTCOLOR = rowfontcolor
							data.BANGUMINM = item.M0060.KYUKNM

						End If

						If item.KYUKCD <> "7" AndAlso item.KYUKCD <> "9" Then
							If item.KYUKCD <> "2" AndAlso item.KYUKCD <> "10" AndAlso item.KYUKCD <> "13" AndAlso item.KYUKCD <> "14" Then
								bolDataHave = True
								'休暇日毎

								data.GYOMDT = SearchDt.ToString("yyyy/MM/dd")
								'ASI[08 Nov 2019]
								data.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

								data.HI = SearchDt.ToString("yyyy/MM/dd")
								data.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"


								'data.GYOMDT = SearchDt

								If item.KYUKCD <> "1" Then
									data.BANGUMINM = item.M0060.KYUKNM
								Else
									data.BANGUMINM = ""
								End If

								data.TITLEKBN = "1"
								data.USERID = id
								data.DATAKBN = "3"
								data.STTIMEupdt = item.JKNST
								data.EDTIMEupdt = item.JKNED
                                'data.MYMEMO = item.GYOMMEMO
                                data.MEMO = item.BIKO
                                If item.KANRIMEMO IsNot Nothing Then
                                    data.MEMO = data.MEMO & " (" & item.KANRIMEMO & ")"
                                End If

								Dim m0060 = db.M0060.Find(item.KYUKCD)
								data.BACKCOLOR = m0060.BACKCOLOR
								data.WAKUCOLOR = m0060.WAKUCOLOR
								data.FONTCOLOR = m0060.FONTCOLOR

								If bolKyushutsu = True Then
									data.KYUSHUTSU = strKyushutsu
									data.ROWBACKCOLOR = rowbackcolor
									data.ROWWAKUCOLOR = rowwakucolor
									data.ROWFONTCOLOR = rowfontcolor
								End If

								'data.MYMEMOFLG = "1"
								'個人メモ
								If d0140Search IsNot Nothing Then
									data.MYMEMO = d0140Search.USERMEMO
								End If


								listdata.Add(data)
							End If

						Else
							bolDataHave = True
							'休暇時間ごと
							datad0040.GYOMDT = SearchDt
							datad0040.GYOMNO = item.KYUKCD
							datad0040.KAKUNIN = ""
							datad0040.STTIME = item.JKNST.Substring(0, 2) & ":" & item.JKNST.Substring(2, 2)
							datad0040.EDTIME = item.JKNED.Substring(0, 2) & ":" & item.JKNED.Substring(2, 2)

							datad0040.BANGUMINM = item.M0060.KYUKNM
							'datad0040.MYMEMO = item.GYOMMEMO
							datad0040.DATAKBN = "3"
							data.TITLEKBN = "0"
							datad0040.USERID = id
							datad0040.STTIMEupdt = item.JKNST
							datad0040.EDTIMEupdt = item.JKNED

							Dim m0060 = db.M0060.Find(item.KYUKCD)
							datad0040.BACKCOLOR = m0060.BACKCOLOR
							datad0040.WAKUCOLOR = m0060.WAKUCOLOR
							datad0040.FONTCOLOR = m0060.FONTCOLOR

							'ASI[08 Nov 2019]
							datad0040.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

							datad0040.HI = SearchDt.ToString("yyyy/MM/dd")
                            datad0040.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

                            datad0040.MEMO = item.BIKO
                            If item.KANRIMEMO IsNot Nothing Then
                                datad0040.MEMO = datad0040.MEMO & " (" & item.KANRIMEMO & ")"
                            End If

							If bolKyushutsu = True Then
								datad0040.KYUSHUTSU = strKyushutsu
								datad0040.ROWBACKCOLOR = rowbackcolor
								datad0040.ROWWAKUCOLOR = rowwakucolor
								datad0040.ROWFONTCOLOR = rowfontcolor
							End If

							'datad0040.MYMEMOFLG = "1"

							'個人メモ 
							If d0140Search IsNot Nothing Then
								datad0040.MYMEMO = d0140Search.USERMEMO
							End If

							listdata.Add(datad0040)
						End If

					Next

					'業務ー業務Noで探す
					Dim d0010 = From m In db.D0010 Select m

					d0010 = d0010.Where(Function(m) m.GYOMYMD = (SearchDt))
					d0010 = d0010.Where(Function(d1) db.D0020.Where(Function(m) m.USERID = id).Select(Function(t2) t2.GYOMNO).Contains(d1.GYOMNO))

					d0010 = d0010.OrderBy(Function(f) f.KSKJKNST)

					For Each item In d0010
						Dim datad0010 As New C0040
						bolDataHave = True
						datad0010.GYOMNO = item.GYOMNO
						datad0010.GYOMDT = item.GYOMYMD

						'ASI[27 Dec 2019]:[START] Commented code of fetching STTIME EDTIME fetchign from D0010, bcz now it is coming from D0020
						'datad0010.STTIME = item.KSKJKNST.Substring(0, 2) & ":" & item.KSKJKNST.Substring(2, 2)

						'If (item.RNZK = "1" AndAlso item.PGYOMNO Is Nothing AndAlso item.SPORTFLG = False) OrElse
						'	(item.RNZK = "1" AndAlso item.SPORT_OYAFLG = True AndAlso item.SPORTFLG = True) Then
						'	datad0010.EDTIME = "24:00"
						'Else
						'	datad0010.EDTIME = item.KSKJKNED.Substring(0, 2) & ":" & item.KSKJKNED.Substring(2, 2)
						'End If

						datad0010.RNZK = item.RNZK
						datad0010.SPORTFLG = item.SPORTFLG
						'[END]

						datad0010.CATCD = item.CATCD
						datad0010.BANGUMINM = item.BANGUMINM
						datad0010.NAIYO = item.NAIYO
						datad0010.BASHO = item.BASYO
						datad0010.MEMO = item.BIKO
						datad0010.DATAKBN = "1"
						'data.MYMEMO = 
						datad0010.USERID = id
						datad0010.TITLEKBN = "0"
						datad0010.CATNM = item.M0020.CATNM

						Dim m0020 = db.M0020.Find(item.CATCD)
						If m0020.SYUCHO = True Then
							datad0010.KYUKCD = 3
							Dim m0060 = db.M0060.Find(3)
							datad0010.BACKCOLOR = m0060.BACKCOLOR
							datad0010.WAKUCOLOR = m0060.WAKUCOLOR
							datad0010.FONTCOLOR = m0060.FONTCOLOR
						End If


						'担当アナテーブルの個人メモを取得
						Dim d0020 = From m In db.D0020 Select m
						d0020 = db.D0020.Where(Function(m) m.GYOMNO = (item.GYOMNO) And m.USERID = id)
						If d0020 IsNot Nothing Then
							For Each itemD0020 In d0020
								'datad0010.MYMEMO = itemD0020.SHIFTMEMO

								'ASI[27 Dec 2019] :[START] Fetch data from D0020 and display on C0040 index screen
								datad0010.STTIME = itemD0020.KSKJKNST.Substring(0, 2) & ":" & itemD0020.KSKJKNST.Substring(2, 2)

								If (item.RNZK = "1" AndAlso item.PGYOMNO Is Nothing AndAlso item.SPORTFLG = False) OrElse
									(item.RNZK = "1" AndAlso item.SPORT_OYAFLG = True AndAlso item.SPORTFLG = True) Then
									datad0010.EDTIME = "24:00"
								Else
									datad0010.EDTIME = itemD0020.KSKJKNED.Substring(0, 2) & ":" & itemD0020.KSKJKNED.Substring(2, 2)
								End If
								datad0010.JTJKNED = itemD0020.JTJKNED

								If itemD0020.CHK = False Then
									datad0010.KAKUNIN = "0"
								Else
									datad0010.KAKUNIN = "1"
								End If

							Next

						End If

						'ASI[08 Nov 2019]
						datad0010.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

						datad0010.HI = SearchDt.ToString("yyyy/MM/dd")
						datad0010.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"



						If bolKyushutsu = True Then
							datad0010.KYUSHUTSU = strKyushutsu
							datad0010.ROWBACKCOLOR = rowbackcolor
							datad0010.ROWWAKUCOLOR = rowwakucolor
							datad0010.ROWFONTCOLOR = rowfontcolor
						End If

						'datad0010.MYMEMOFLG = "1"

						'個人メモ
						If d0140Search IsNot Nothing Then
							datad0010.MYMEMO = d0140Search.USERMEMO
						End If

						datad0010.PGYOMNO = If(item.PGYOMNO IsNot Nothing, item.PGYOMNO, 0)
						datad0010.SPORT_OYAFLG = item.SPORT_OYAFLG


						listdata.Add(datad0010)
					Next

					'業務親業務Noで探す
					Dim d0010P = From m In db.D0010 Where m.SPORTFLG = False Select m

					d0010P = d0010P.Where(Function(m) m.GYOMYMD = (SearchDt))
					d0010P = d0010P.Where(Function(d1) db.D0020.Where(Function(m) m.USERID = id).Select(Function(t2) t2.GYOMNO).Contains(d1.PGYOMNO))

					d0010P = d0010P.OrderBy(Function(f) f.KSKJKNST)

					For Each item In d0010P
						bolDataHave = True
						Dim datad0010 As New C0040
						datad0010.GYOMNO = item.GYOMNO
						datad0010.PGYOMNO = item.PGYOMNO
						datad0010.GYOMDT = item.GYOMYMD

						'ASI[27 Dec 2019]:[START] Commented code of fetching STTIME EDTIME fetchign from D0010, bcz now it is coming from D0020
						datad0010.STTIME = item.KSKJKNST.Substring(0, 2) & ":" & item.KSKJKNST.Substring(2, 2)

						If item.RNZK = "1" And item.PGYOMNO Is Nothing Then
							datad0010.EDTIME = "24:00"
						Else
							datad0010.EDTIME = item.KSKJKNED.Substring(0, 2) & ":" & item.KSKJKNED.Substring(2, 2)
						End If

						datad0010.RNZK = item.RNZK
						datad0010.SPORTFLG = item.SPORTFLG
						'[END]

						datad0010.CATCD = item.CATCD
						Dim m0020 = db.M0020.Find(item.CATCD)
						If m0020.SYUCHO = True Then
							datad0010.KYUKCD = 3
							Dim m0060 = db.M0060.Find(3)
							datad0010.BACKCOLOR = m0060.BACKCOLOR
							datad0010.WAKUCOLOR = m0060.WAKUCOLOR
							datad0010.FONTCOLOR = m0060.FONTCOLOR
						End If


						datad0010.BANGUMINM = item.BANGUMINM
						datad0010.NAIYO = item.NAIYO
						datad0010.BASHO = item.BASYO
						datad0010.MEMO = item.BIKO
						datad0010.DATAKBN = "1"
						'data.MYMEMO = 
						datad0010.USERID = id
						datad0010.TITLEKBN = "0"
						datad0010.CATNM = item.M0020.CATNM

						'担当アナテーブルの個人メモを取得
						Dim d0020 = From m In db.D0020 Select m
						d0020 = db.D0020.Where(Function(m) m.GYOMNO = (item.PGYOMNO) And m.USERID = id)
						If d0020 IsNot Nothing Then
							For Each itemD0020 In d0020
								'datad0010.MYMEMO = itemD0020.SHIFTMEMO

								'ASI[27 Dec 2019] :[START] Fetch data from D0020 and display on C0040 index screen
								'datad0010.STTIME = itemD0020.KSKJKNST.Substring(0, 2) & ":" & itemD0020.KSKJKNST.Substring(2, 2)
								If item.JTJKNED.ToString("HH:mm") <> "00:00" Then
									If item.RNZK = "1" And item.PGYOMNO Is Nothing Then
										datad0010.EDTIME = "24:00"
									Else
										datad0010.EDTIME = itemD0020.KSKJKNED.Substring(0, 2) & ":" & itemD0020.KSKJKNED.Substring(2, 2)
									End If
								End If
								'datad0010.JTJKNED = itemD0020.JTJKNED
								'[END]

								If itemD0020.CHK = False Then
									datad0010.KAKUNIN = "0"
								Else
									datad0010.KAKUNIN = "1"
								End If

							Next

						End If

						If bolKyushutsu = True Then
							datad0010.KYUSHUTSU = strKyushutsu
							datad0010.ROWBACKCOLOR = rowbackcolor
							datad0010.ROWWAKUCOLOR = rowwakucolor
							datad0010.ROWFONTCOLOR = rowfontcolor
						End If

						'ASI[08 Nov 2019]
						datad0010.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

						datad0010.HI = SearchDt.ToString("yyyy/MM/dd")
						datad0010.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

						'個人メモ  
						If d0140Search IsNot Nothing Then
							datad0010.MYMEMO = d0140Search.USERMEMO
						End If

						datad0010.SPORT_OYAFLG = item.SPORT_OYAFLG
						datad0010.JTJKNED = item.JTJKNED

						listdata.Add(datad0010)
					Next

					'ASI[29 Nov 2019] Sport Cat Data
					If sportdata = "1" Then
						Dim d00102 = From m In db.D0010 Select m

						d00102 = d00102.Where(Function(m) m.GYOMYMD = (SearchDt) AndAlso m.OYAGYOMFLG = True AndAlso m.SPORTFLG = True)
						d00102 = d00102.Where(Function(d1) db.M0160.Where(Function(m) m.USERID = id).Select(Function(t2) t2.SPORTCATCD).Contains(d1.SPORTCATCD))
						d00102 = d00102.Where(Function(d1) (db.D0022.Select(Function(m) m.GYOMNO).Contains(d1.GYOMNO)) OrElse ((From m4 In db.D0010 Join d2 In db.D0021 On m4.GYOMNO Equals d2.GYOMNO Where d1.GYOMNO = m4.PGYOMNO And m4.OYAGYOMFLG = False Select m4.PGYOMNO).Contains(d1.GYOMNO)) OrElse ((From m5 In db.D0010 Join d20 In db.D0021 On m5.GYOMNO Equals d20.GYOMNO Where d1.PGYOMNO = m5.PGYOMNO And m5.OYAGYOMFLG = False AndAlso m5.PGYOMNO IsNot Nothing Select m5.PGYOMNO).Contains(d1.PGYOMNO)))
						d00102 = d00102.Where(Function(d1) Not (From m4 In db.D0010 Join d2 In db.D0020 On m4.GYOMNO Equals d2.GYOMNO Where d1.GYOMNO = m4.PGYOMNO And m4.OYAGYOMFLG = False AndAlso d2.USERID = id Select m4.PGYOMNO).Contains(d1.GYOMNO))
						d00102 = d00102.Where(Function(d1) Not (From m5 In db.D0010 Join d20 In db.D0020 On m5.GYOMNO Equals d20.GYOMNO Where d1.PGYOMNO = m5.PGYOMNO And m5.OYAGYOMFLG = False AndAlso m5.PGYOMNO IsNot Nothing AndAlso d20.USERID = id Select m5.PGYOMNO).Contains(d1.PGYOMNO))
						d00102 = d00102.OrderBy(Function(f) f.KSKJKNST)

						For Each item In d00102
							Dim datad0010 As New C0040
							bolDataHave = True
							datad0010.GYOMNO = item.GYOMNO
							datad0010.GYOMDT = item.GYOMYMD
							datad0010.KAKUNIN = "仮"

							datad0010.STTIME = item.KSKJKNST.Substring(0, 2) & ":" & item.KSKJKNST.Substring(2, 2)

							If item.RNZK = "1" And item.PGYOMNO Is Nothing Then
								datad0010.EDTIME = "24:00"
							Else
								datad0010.EDTIME = item.KSKJKNED.Substring(0, 2) & ":" & item.KSKJKNED.Substring(2, 2)
							End If

							datad0010.CATCD = item.CATCD
							datad0010.BANGUMINM = item.BANGUMINM
							datad0010.NAIYO = item.NAIYO
							datad0010.BASHO = item.BASYO
							datad0010.MEMO = item.BIKO
							datad0010.DATAKBN = "0"
							'data.MYMEMO = 
							datad0010.USERID = id
							datad0010.TITLEKBN = "0"
							datad0010.CATNM = item.M0020.CATNM

							'Dim m0020 = db.M0020.Find(item.CATCD)
							'If m0020.SYUCHO = True Then
							'	datad0010.KYUKCD = 3
							'	Dim m0060 = db.M0060.Find(3)
							'	datad0010.BACKCOLOR = m0060.BACKCOLOR
							'	datad0010.WAKUCOLOR = m0060.WAKUCOLOR
							'	datad0010.FONTCOLOR = m0060.FONTCOLOR
							'End If

							datad0010.BACKCOLOR = "FFFFFF"
							datad0010.FONTCOLOR = "A9A9A9"

							'ASI[08 Nov 2019]
							datad0010.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

							datad0010.HI = SearchDt.ToString("yyyy/MM/dd")
							datad0010.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

							If bolKyushutsu = True Then
								datad0010.KYUSHUTSU = strKyushutsu
								datad0010.ROWBACKCOLOR = rowbackcolor
								datad0010.ROWWAKUCOLOR = rowwakucolor
								datad0010.ROWFONTCOLOR = rowfontcolor
							End If

							'datad0010.MYMEMOFLG = "1"

							'個人メモ
							If d0140Search IsNot Nothing Then
								datad0010.MYMEMO = d0140Search.USERMEMO
							End If

							listdata.Add(datad0010)
						Next
					End If

					'業務申請
					Dim d0050data = From m In db.D0050 Select m
					d0050data = d0050data.Where(Function(m) m.GYOMYMD <= (SearchDt) And SearchDt <= m.GYOMYMDED And m.SHONINFLG = "0" And m.USERID = id)
					d0050data = d0050data.OrderBy(Function(f) f.KSKJKNST)
					For Each item In d0050data
						Dim datad0050 As New C0040
						bolDataHave = True
						datad0050.GYOMNO = item.GYOMSNSNO
						datad0050.GYOMDT = item.GYOMYMD
						datad0050.KAKUNIN = "申請中"

						datad0050.STTIME = item.KSKJKNST.Substring(0, 2) & ":" & item.KSKJKNST.Substring(2, 2)
						datad0050.EDTIME = item.KSKJKNED.Substring(0, 2) & ":" & item.KSKJKNED.Substring(2, 2)
						datad0050.CATCD = item.CATCD
						datad0050.BANGUMINM = item.BANGUMINM
						datad0050.NAIYO = item.NAIYO
						datad0050.BASHO = item.BASYO

						datad0050.MEMO = item.GYOMMEMO
						datad0050.CATNM = item.M0020.CATNM

						datad0050.DATAKBN = "2"
						datad0050.USERID = id

						If bolKyushutsu = True Then
							datad0050.KYUSHUTSU = strKyushutsu
							datad0050.ROWBACKCOLOR = rowbackcolor
							datad0050.ROWWAKUCOLOR = rowwakucolor
							datad0050.ROWFONTCOLOR = rowfontcolor
						End If

						'ASI[08 Nov 2019]
						datad0050.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

						datad0050.HI = SearchDt.ToString("yyyy/MM/dd")
						datad0050.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

						If d0140Search IsNot Nothing Then
							datad0050.MYMEMO = d0140Search.USERMEMO
						End If

						listdata.Add(datad0050)
					Next


					'休暇申請
					Dim d0060data = From m In db.D0060 Select m
					'd0060data = d0060data.Where(Function(m) m.KKNST = (SearchDt) And m.SHONINFLG = "False")
					d0060data = d0060data.Where(Function(m) m.KKNST <= SearchDt And SearchDt <= m.KKNED And m.SHONINFLG = "0" And m.USERID = id)
					d0060data = d0060data.OrderBy(Function(f) f.JKNST)
					For Each item In d0060data
						Dim datad0060 As New C0040
						If item.KYUKCD <> "7" AndAlso item.KYUKCD <> "9" Then
							bolDataHave = True
							'休暇申請日毎

							datad0060.GYOMDT = SearchDt.ToString("yyyy/MM/dd")

							'ASI[08 Nov 2019]
							datad0060.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

							datad0060.HI = SearchDt.ToString("yyyy/MM/dd")
							datad0060.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

							datad0060.GYOMDT = SearchDt
							datad0060.GYOMNO = item.KYUKSNSCD
							datad0060.KAKUNIN = "申請中"
							'data.BANGUMINM = "休暇申請中"
							datad0060.BANGUMINM = item.M0060.KYUKNM
                            'data.TITLEKBN = "1"

                            datad0060.MEMO = item.GYOMMEMO
                            If item.KANRIMEMO IsNot Nothing Then
                                datad0060.MEMO = datad0060.MEMO & " (" & item.KANRIMEMO & ")"
                            End If

                            datad0060.DATAKBN = "4"

							datad0060.USERID = id
							datad0060.STTIMEupdt = item.JKNST
							datad0060.EDTIMEupdt = item.JKNED
							datad0060.KKNST = item.KKNST
							datad0060.KKNED = item.KKNED

							'Dim m0060 = db.M0060.Find(item.KYUKCD)
							'data.BACKCOLOR = m0060.BACKCOLOR
							'data.WAKUCOLOR = m0060.WAKUCOLOR
							'data.FONTCOLOR = m0060.FONTCOLOR

							If bolKyushutsu = True Then
								datad0060.KYUSHUTSU = strKyushutsu
								datad0060.ROWBACKCOLOR = rowbackcolor
								datad0060.ROWWAKUCOLOR = rowwakucolor
								datad0060.ROWFONTCOLOR = rowfontcolor
							End If

							'個人メモ
							If d0140Search IsNot Nothing Then
								datad0060.MYMEMO = d0140Search.USERMEMO
							End If

							listdata.Add(datad0060)
						Else
							bolDataHave = True
							'休暇申請時間ごと
							datad0060.GYOMDT = SearchDt
							datad0060.GYOMNO = item.KYUKSNSCD
							'datad0060.KAKUNIN = "0"
							datad0060.KAKUNIN = "申請中"
							datad0060.STTIME = item.JKNST.Substring(0, 2) & ":" & item.JKNST.Substring(2, 2)
							datad0060.EDTIME = item.JKNED.Substring(0, 2) & ":" & item.JKNED.Substring(2, 2)

							datad0060.BANGUMINM = item.M0060.KYUKNM

                            datad0060.MEMO = item.GYOMMEMO
                            If item.KANRIMEMO IsNot Nothing Then
                                datad0060.MEMO = datad0060.MEMO & " (" & item.KANRIMEMO & ")"
                            End If

                            datad0060.DATAKBN = "4"
							datad0060.USERID = id
							data.TITLEKBN = "0"
							datad0060.STTIMEupdt = item.JKNST
							datad0060.EDTIMEupdt = item.JKNED
							datad0060.KKNST = item.KKNST
							datad0060.KKNED = item.KKNED

							'Dim m0060 = db.M0060.Find(item.KYUKCD)
							'datad0060.BACKCOLOR = m0060.BACKCOLOR
							'datad0060.WAKUCOLOR = m0060.WAKUCOLOR
							'datad0060.FONTCOLOR = m0060.FONTCOLOR

							If bolKyushutsu = True Then
								datad0060.KYUSHUTSU = strKyushutsu
								datad0060.ROWBACKCOLOR = rowbackcolor
								datad0060.ROWWAKUCOLOR = rowwakucolor
								datad0060.ROWFONTCOLOR = rowfontcolor
							End If

							'ASI[08 Nov 2019]
							datad0060.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

							datad0060.HI = SearchDt.ToString("yyyy/MM/dd")
							datad0060.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"


							'個人メモ      
							If d0140Search IsNot Nothing Then
								datad0060.MYMEMO = d0140Search.USERMEMO
							End If

							listdata.Add(datad0060)
						End If


					Next

					If bolDataHave = False Then

						If bolKyushutsu = True Then
							data.KYUSHUTSU = strKyushutsu
							data.ROWBACKCOLOR = rowbackcolor
							data.ROWWAKUCOLOR = rowwakucolor
							data.ROWFONTCOLOR = rowfontcolor
						End If
						data.GYOMDT = SearchDt.ToString("yyyy/MM/dd")
						'ASI[08 Nov 2019]
						data.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG
						data.HI = SearchDt.ToString("yyyy/MM/dd")
						data.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"
						data.DATAKBN = "5"
						data.USERID = id
						listdata.Add(data)


					End If
				Else
					data.GYOMDT = SearchDt.ToString("yyyy/MM/dd")
					'ASI[08 Nov 2019]
					data.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG
					data.HI = SearchDt.ToString("yyyy/MM/dd")
					data.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"
					data.DATAKBN = "5"
					data.USERID = id
					data.TITLEKBN = "1"
					data.BANGUMINM = "公休展開されていません。"
					data.MYMEMOFLG = "0"
					listdata.Add(data)
				End If


			Next

			listdata = listdata.OrderBy(Function(f) f.HI).ThenBy(Function(f) f.STTIME).ToList

			For Each item In listdata
				item.HI = Date.Parse(item.HI).ToString("MM/dd")
			Next
			Dim strHI As String = ""
			Dim strYobi As String = ""
			For Each item In listdata
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
			Return listdata
		End Function

		' GET: Common
		'Announcer Data : Get data of one month
		Function GetPersonalShiftData_OneMonth(ByVal intDays As Integer, ByVal ParaDt As Date, ByVal lstAnaId As List(Of String), ByVal sportdata As String) As List(Of C0040)
			Dim SearchDt As Date = Nothing
			Dim intId As Integer = 0
			Dim listdata As New List(Of C0040)
			Dim strIds As String() = lstAnaId.ToArray
			Dim dtStartDt As Date = ParaDt.AddDays(0).ToString("yyyy/MM/dd")
			Dim dtEndDt As Date = ParaDt.AddDays(intDays).ToString("yyyy/MM/dd")
			Dim strStartDt As String = dtStartDt.ToString("yyyy/MM/dd")
			Dim strEndDt As String = dtEndDt.ToString("yyyy/MM/dd")
			Dim id As String = ""
			Dim strStartDt_WOS As String = strStartDt.ToString.Replace("/", "")
			Dim strEndDt_WOS As String = ParaDt.AddDays(intDays - 1).ToString("yyyy/MM/dd").Replace("/", "")
			Dim strStartHI As String = strStartDt_WOS.Substring(6, 2)
			Dim strEndHI As String = strEndDt_WOS.Substring(6, 2)
			Dim intStartDt_YM As Integer = Integer.Parse(strStartDt_WOS.Substring(0, 6))

			Dim lstDeskMemo_DayCount = (From d1 In db.D0120 Join d2 In db.D0130 On d1.DESKNO Equals d2.DESKNO
										Join d3 In db.D0110 On d2.DESKNO Equals d3.DESKNO
										Where strIds.Contains(d2.USERID) And d3.KAKUNINID <> 5 And
										(d1.SHIFTYMDST >= strStartDt And d1.SHIFTYMDED <= strEndDt
										) Select d1.DESKNO, d2.USERID, d1.SHIFTYMDST, d1.SHIFTYMDED).ToList

			'休暇
			Dim lstD0040Data = (From m In db.D0040 Where m.NENGETU = strStartDt_WOS.Substring(0, 6) And m.HI <= strEndHI And
								m.HI >= strStartHI And strIds.Contains(m.USERID) And m.KYUKCD <> "1"
								Select m).ToList

			'業務ー業務Noで探す
			Dim lstD0010_D0020 = (From m In db.D0010 Join d2 In db.D0020 On m.GYOMNO Equals d2.GYOMNO
								  Where m.GYOMYMD >= strStartDt AndAlso m.GYOMYMD <= strEndDt AndAlso strIds.Contains(d2.USERID)
								  Select m, d2).ToList

			'業務親業務Noで探す
			Dim lstD0010_D0020P = (From d10 In db.D0010 Join d20 In db.D0020 On d10.PGYOMNO Equals d20.GYOMNO
								   Where d10.GYOMYMD >= strStartDt AndAlso d10.GYOMYMD <= strEndDt AndAlso
									d10.SPORTFLG = False AndAlso strIds.Contains(d20.USERID)
								   Select d10, d20).ToList

			'業務申請
			Dim lstD0050Data = (From m In db.D0050 Where m.GYOMYMD >= strStartDt And m.GYOMYMDED <= strEndDt And
							m.SHONINFLG = "0" And strIds.Contains(m.USERID) Select m).ToList

			'休暇申請
			Dim lstD0060Data = (From m In db.D0060 Where m.KKNST >= strStartDt And m.KKNED <= strEndDt And
							m.SHONINFLG = "0" And strIds.Contains(m.USERID) Select m).ToList

			'カテゴリーテーブル
			Dim lstM0020Data = (From m In db.M0020 Select m).ToList

			'休暇コードテーブル
			Dim lstM0060Data = (From m In db.M0060 Select m).ToList

			'公休展開管理テーブル
			Dim lstD0030Data = (From m In db.D0030 Where strIds.Contains(m.USERID) And m.NENGETU = intStartDt_YM).ToList

			'個人メモ
			Dim lstD0140Data = (From m In db.D0140 Where strIds.Contains(m.USERID) And m.YMD >= dtStartDt And m.YMD <= dtStartDt).ToList

			For j = 0 To strIds.Count - 1
				intId = Integer.Parse(strIds(j))
				id = strIds(j)

				For i = 0 To intDays
					Dim bolNOKokyu As Boolean = False

					Dim data As New C0040
					SearchDt = ParaDt.AddDays(i).ToString("yyyy/MM/dd")

					Dim DESKMEMOEXISTFLG As Boolean
					Dim HI_Date As String
					HI_Date = SearchDt.ToString("yyyy/MM/dd")

					'ASI[02 Aug 2019]: set DESKMEMOEXISTFLG property to decide is there deskmemo exist for date or not.
					'Based on value of that flag display link of DeskMemo in _個人シフト screen
					Dim deskMemoCount = lstDeskMemo_DayCount.Where(Function(m) m.USERID = id And HI_Date >= m.SHIFTYMDST And HI_Date <= m.SHIFTYMDED).Count

					If deskMemoCount > 0 Then
						DESKMEMOEXISTFLG = True
					Else
						DESKMEMOEXISTFLG = False
					End If

					'If loginUserSystem = False AndAlso loginUserKanri = False Then
					Dim strKouKyudt As String = SearchDt.ToString("yyyyMMdd")
					strKouKyudt = strKouKyudt.Substring(0, 6)
					Dim intKokyudt As Integer = Integer.Parse(strKouKyudt)

					Dim d0030Day = lstD0030Data.Where(Function(f) f.USERID = intId And f.NENGETU = intKokyudt).SingleOrDefault

					If d0030Day Is Nothing Then
						bolNOKokyu = True
					End If

					'個人メモ
					Dim dateSearch As DateTime = DateTime.Parse(SearchDt)
					Dim intUserid As Integer = Integer.Parse(intId)
					Dim d0140Search = lstD0140Data.Where(Function(m) m.USERID = intUserid And m.YMD = dateSearch).SingleOrDefault

					If d0140Search IsNot Nothing Then
						data.MYMEMO = d0140Search.USERMEMO
					End If

					Dim strKyushutsu As String = ""

					If bolNOKokyu = False Then
						Dim bolDataHave As Boolean = False
						Dim bolKyushutsu As Boolean = False
						Dim rowbackcolor As String = ""
						Dim rowwakucolor As String = ""
						Dim rowfontcolor As String = ""

						'休暇
						Dim strSearchDate As String = SearchDt.ToString.Replace("/", "")
						Dim d0040data = lstD0040Data.Where(Function(m) m.HI = strSearchDate.Substring(6, 2) And m.USERID = id).OrderBy(Function(f) f.JKNST)

						For Each item In d0040data
							Dim datad0040 As New C0040

							If item.KYUKCD = "2" Or item.KYUKCD = "10" Or item.KYUKCD = "13" Or item.KYUKCD = "14" Then
								bolKyushutsu = True
								If item.KYUKCD = "2" Then
									strKyushutsu = "1"
								ElseIf item.KYUKCD = "10" Then
									strKyushutsu = "2"
								ElseIf item.KYUKCD = "13" Then
									strKyushutsu = "3"
								Else
									strKyushutsu = "4"
								End If

								Dim m0060 = lstM0060Data.Where(Function(f) f.KYUKCD = item.KYUKCD).SingleOrDefault

								rowbackcolor = m0060.BACKCOLOR
								rowwakucolor = m0060.WAKUCOLOR
								rowfontcolor = m0060.FONTCOLOR

								data.ROWBACKCOLOR = rowbackcolor
								data.ROWWAKUCOLOR = rowwakucolor
								data.ROWFONTCOLOR = rowfontcolor
								data.BANGUMINM = item.M0060.KYUKNM

							End If

							If item.KYUKCD <> "7" AndAlso item.KYUKCD <> "9" Then
								If item.KYUKCD <> "2" AndAlso item.KYUKCD <> "10" AndAlso item.KYUKCD <> "13" AndAlso item.KYUKCD <> "14" Then
									bolDataHave = True
									'休暇日毎
									data.GYOMDT = SearchDt.ToString("yyyy/MM/dd")
									'ASI[08 Nov 2019]
									data.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

									data.HI = SearchDt.ToString("yyyy/MM/dd")
									data.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

									If item.KYUKCD <> "1" Then
										data.BANGUMINM = item.M0060.KYUKNM
									Else
										data.BANGUMINM = ""
									End If

									data.TITLEKBN = "1"
									data.USERID = id
									data.DATAKBN = "3"
									data.STTIMEupdt = item.JKNST
                                    data.EDTIMEupdt = item.JKNED

                                    data.MEMO = item.BIKO
                                    If item.KANRIMEMO IsNot Nothing Then
                                        data.MEMO = data.MEMO & " (" & item.KANRIMEMO & ")"
                                    End If

                                    Dim m0060 = lstM0060Data.Where(Function(f) f.KYUKCD = item.KYUKCD).SingleOrDefault

                                    data.BACKCOLOR = m0060.BACKCOLOR
									data.WAKUCOLOR = m0060.WAKUCOLOR
									data.FONTCOLOR = m0060.FONTCOLOR

									If bolKyushutsu = True Then
										data.KYUSHUTSU = strKyushutsu
										data.ROWBACKCOLOR = rowbackcolor
										data.ROWWAKUCOLOR = rowwakucolor
										data.ROWFONTCOLOR = rowfontcolor
									End If

									'個人メモ
									If d0140Search IsNot Nothing Then
										data.MYMEMO = d0140Search.USERMEMO
									End If
									listdata.Add(data)
								End If

							Else
								bolDataHave = True
								'休暇時間ごと
								datad0040.GYOMDT = SearchDt
								datad0040.GYOMNO = item.KYUKCD
								datad0040.KAKUNIN = ""
								datad0040.STTIME = item.JKNST.Substring(0, 2) & ":" & item.JKNST.Substring(2, 2)
								datad0040.EDTIME = item.JKNED.Substring(0, 2) & ":" & item.JKNED.Substring(2, 2)

								datad0040.BANGUMINM = item.M0060.KYUKNM
								datad0040.DATAKBN = "3"
								data.TITLEKBN = "0"
								datad0040.USERID = id
								datad0040.STTIMEupdt = item.JKNST
								datad0040.EDTIMEupdt = item.JKNED

								Dim m0060 = lstM0060Data.Where(Function(f) f.KYUKCD = item.KYUKCD).SingleOrDefault

								datad0040.BACKCOLOR = m0060.BACKCOLOR
								datad0040.WAKUCOLOR = m0060.WAKUCOLOR
								datad0040.FONTCOLOR = m0060.FONTCOLOR

								'ASI[08 Nov 2019]
								datad0040.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

								datad0040.HI = SearchDt.ToString("yyyy/MM/dd")
                                datad0040.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

                                datad0040.MEMO = item.BIKO
                                If item.KANRIMEMO IsNot Nothing Then
                                    datad0040.MEMO = datad0040.MEMO & " (" & item.KANRIMEMO & ")"
                                End If

								If bolKyushutsu = True Then
									datad0040.KYUSHUTSU = strKyushutsu
									datad0040.ROWBACKCOLOR = rowbackcolor
									datad0040.ROWWAKUCOLOR = rowwakucolor
									datad0040.ROWFONTCOLOR = rowfontcolor
								End If

								'個人メモ 
								If d0140Search IsNot Nothing Then
									datad0040.MYMEMO = d0140Search.USERMEMO
								End If

								listdata.Add(datad0040)
							End If

						Next

						'業務ー業務Noで探す
						Dim d0010 = lstD0010_D0020.Where(Function(f) f.m.GYOMYMD = (SearchDt) AndAlso f.d2.USERID = id).OrderBy(Function(f) f.m.KSKJKNST)

						For Each item1 In d0010
							Dim item = item1.m
							Dim itemD0020 = item1.d2
							Dim datad0010 As New C0040
							bolDataHave = True
							datad0010.GYOMNO = item.GYOMNO
							datad0010.GYOMDT = item.GYOMYMD

							'ASI[27 Dec 2019]:[START] Commented code of fetching STTIME EDTIME fetchign from D0010, bcz now it is coming from D0020
							'datad0010.STTIME = item.KSKJKNST.Substring(0, 2) & ":" & item.KSKJKNST.Substring(2, 2)

							'If (item.RNZK = "1" AndAlso item.PGYOMNO Is Nothing AndAlso item.SPORTFLG = False) OrElse
							'	(item.RNZK = "1" AndAlso item.SPORT_OYAFLG = True AndAlso item.SPORTFLG = True) Then
							'	datad0010.EDTIME = "24:00"
							'Else
							'	datad0010.EDTIME = item.KSKJKNED.Substring(0, 2) & ":" & item.KSKJKNED.Substring(2, 2)
							'End If

							datad0010.RNZK = item.RNZK
							datad0010.SPORTFLG = item.SPORTFLG
							'[END]

							datad0010.CATCD = item.CATCD
							datad0010.BANGUMINM = item.BANGUMINM
							datad0010.NAIYO = item.NAIYO
							datad0010.BASHO = item.BASYO
							datad0010.MEMO = item.BIKO
							datad0010.DATAKBN = "1"
							datad0010.USERID = id
							datad0010.TITLEKBN = "0"
							datad0010.CATNM = item.M0020.CATNM

							Dim m0020 = lstM0020Data.Where(Function(f) f.CATCD = item.CATCD).SingleOrDefault

							If m0020.SYUCHO = True Then
								datad0010.KYUKCD = 3
								Dim m0060 = lstM0060Data.Where(Function(f) f.KYUKCD = 3).SingleOrDefault

								datad0010.BACKCOLOR = m0060.BACKCOLOR
								datad0010.WAKUCOLOR = m0060.WAKUCOLOR
								datad0010.FONTCOLOR = m0060.FONTCOLOR
							End If


							'担当アナテーブルの個人メモを取得
							'ASI[27 Dec 2019] :[START] Fetch data from D0020 and display on C0040 index screen
							datad0010.STTIME = itemD0020.KSKJKNST.Substring(0, 2) & ":" & itemD0020.KSKJKNST.Substring(2, 2)

							If (item.RNZK = "1" AndAlso item.PGYOMNO Is Nothing AndAlso item.SPORTFLG = False) OrElse
								(item.RNZK = "1" AndAlso item.SPORT_OYAFLG = True AndAlso item.SPORTFLG = True) Then
								datad0010.EDTIME = "24:00"
							Else
								datad0010.EDTIME = itemD0020.KSKJKNED.Substring(0, 2) & ":" & itemD0020.KSKJKNED.Substring(2, 2)
							End If
							datad0010.JTJKNED = itemD0020.JTJKNED

							If itemD0020.CHK = False Then
								datad0010.KAKUNIN = "0"
							Else
								datad0010.KAKUNIN = "1"
							End If

							'ASI[08 Nov 2019]
							datad0010.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

							datad0010.HI = SearchDt.ToString("yyyy/MM/dd")
							datad0010.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

							If bolKyushutsu = True Then
								datad0010.KYUSHUTSU = strKyushutsu
								datad0010.ROWBACKCOLOR = rowbackcolor
								datad0010.ROWWAKUCOLOR = rowwakucolor
								datad0010.ROWFONTCOLOR = rowfontcolor
							End If

							'個人メモ
							If d0140Search IsNot Nothing Then
								datad0010.MYMEMO = d0140Search.USERMEMO
							End If

							datad0010.PGYOMNO = If(item.PGYOMNO IsNot Nothing, item.PGYOMNO, 0)
							datad0010.SPORT_OYAFLG = item.SPORT_OYAFLG

							listdata.Add(datad0010)
						Next


						'業務親業務Noで探す
						Dim d0010P = lstD0010_D0020P.Where(Function(f) f.d10.GYOMYMD = (SearchDt) And f.d20.USERID = id).OrderBy(Function(f) f.d10.KSKJKNST)

						For Each item1 In d0010P
							bolDataHave = True
							Dim datad0010 As New C0040
							Dim item = item1.d10
							Dim itemD0020 = item1.d20
							datad0010.GYOMNO = item.GYOMNO
							datad0010.PGYOMNO = item.PGYOMNO
							datad0010.GYOMDT = item.GYOMYMD

							'ASI[27 Dec 2019]:[START] Commented code of fetching STTIME EDTIME fetchign from D0010, bcz now it is coming from D0020
							datad0010.STTIME = item.KSKJKNST.Substring(0, 2) & ":" & item.KSKJKNST.Substring(2, 2)

							If item.RNZK = "1" And item.PGYOMNO Is Nothing Then
								datad0010.EDTIME = "24:00"
							Else
								datad0010.EDTIME = item.KSKJKNED.Substring(0, 2) & ":" & item.KSKJKNED.Substring(2, 2)
							End If

							datad0010.RNZK = item.RNZK
							datad0010.SPORTFLG = item.SPORTFLG
							'[END]

							datad0010.CATCD = item.CATCD

							Dim m0020 = lstM0020Data.Where(Function(f) f.CATCD = item.CATCD).SingleOrDefault

							If m0020.SYUCHO = True Then
								datad0010.KYUKCD = 3
								Dim m0060 = lstM0060Data.Where(Function(f) f.KYUKCD = 3).SingleOrDefault
								datad0010.BACKCOLOR = m0060.BACKCOLOR
								datad0010.WAKUCOLOR = m0060.WAKUCOLOR
								datad0010.FONTCOLOR = m0060.FONTCOLOR
							End If

							datad0010.BANGUMINM = item.BANGUMINM
							datad0010.NAIYO = item.NAIYO
							datad0010.BASHO = item.BASYO
							datad0010.MEMO = item.BIKO
							datad0010.DATAKBN = "1"
							datad0010.USERID = id
							datad0010.TITLEKBN = "0"
							datad0010.CATNM = item.M0020.CATNM

							'担当アナテーブルの個人メモを取得

							'ASI[27 Dec 2019] :[START] Fetch data from D0020 and display on C0040 index screen
							'datad0010.STTIME = itemD0020.KSKJKNST.Substring(0, 2) & ":" & itemD0020.KSKJKNST.Substring(2, 2)
							If item.JTJKNED.ToString("HH:mm") <> "00:00" Then
								If item.RNZK = "1" And item.PGYOMNO Is Nothing Then
									datad0010.EDTIME = "24:00"
								Else
									datad0010.EDTIME = itemD0020.KSKJKNED.Substring(0, 2) & ":" & itemD0020.KSKJKNED.Substring(2, 2)
								End If
							End If

							If itemD0020.CHK = False Then
								datad0010.KAKUNIN = "0"
							Else
								datad0010.KAKUNIN = "1"
							End If

							If bolKyushutsu = True Then
								datad0010.KYUSHUTSU = strKyushutsu
								datad0010.ROWBACKCOLOR = rowbackcolor
								datad0010.ROWWAKUCOLOR = rowwakucolor
								datad0010.ROWFONTCOLOR = rowfontcolor
							End If

							'ASI[08 Nov 2019]
							datad0010.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

							datad0010.HI = SearchDt.ToString("yyyy/MM/dd")
							datad0010.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

							'個人メモ  
							If d0140Search IsNot Nothing Then
								datad0010.MYMEMO = d0140Search.USERMEMO
							End If

							datad0010.SPORT_OYAFLG = item.SPORT_OYAFLG
							datad0010.JTJKNED = item.JTJKNED

							listdata.Add(datad0010)
						Next

						'業務申請
						Dim d0050data = lstD0050Data.Where(Function(m) m.GYOMYMD <= (SearchDt) And SearchDt <= m.GYOMYMDED And m.USERID = id).OrderBy(Function(f) f.KSKJKNST)

						For Each item In d0050data
							Dim datad0050 As New C0040
							bolDataHave = True
							datad0050.GYOMNO = item.GYOMSNSNO
							datad0050.GYOMDT = item.GYOMYMD
							datad0050.KAKUNIN = "申請中"

							datad0050.STTIME = item.KSKJKNST.Substring(0, 2) & ":" & item.KSKJKNST.Substring(2, 2)
							datad0050.EDTIME = item.KSKJKNED.Substring(0, 2) & ":" & item.KSKJKNED.Substring(2, 2)
							datad0050.CATCD = item.CATCD
							datad0050.BANGUMINM = item.BANGUMINM
							datad0050.NAIYO = item.NAIYO
							datad0050.BASHO = item.BASYO

							datad0050.MEMO = item.GYOMMEMO
							datad0050.CATNM = item.M0020.CATNM

							datad0050.DATAKBN = "2"
							datad0050.USERID = id

							If bolKyushutsu = True Then
								datad0050.KYUSHUTSU = strKyushutsu
								datad0050.ROWBACKCOLOR = rowbackcolor
								datad0050.ROWWAKUCOLOR = rowwakucolor
								datad0050.ROWFONTCOLOR = rowfontcolor
							End If

							'ASI[08 Nov 2019]
							datad0050.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

							datad0050.HI = SearchDt.ToString("yyyy/MM/dd")
							datad0050.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

							If d0140Search IsNot Nothing Then
								datad0050.MYMEMO = d0140Search.USERMEMO
							End If

							listdata.Add(datad0050)
						Next

						'休暇申請
						Dim d0060data = lstD0060Data.Where(Function(m) m.KKNST <= SearchDt And SearchDt <= m.KKNED And m.USERID = id).OrderBy(Function(f) f.JKNST)

						For Each item In d0060data
							Dim datad0060 As New C0040
							If item.KYUKCD <> "7" AndAlso item.KYUKCD <> "9" Then
								bolDataHave = True
								'休暇申請日毎
								datad0060.GYOMDT = SearchDt.ToString("yyyy/MM/dd")

								'ASI[08 Nov 2019]
								datad0060.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

								datad0060.HI = SearchDt.ToString("yyyy/MM/dd")
								datad0060.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

								datad0060.GYOMDT = SearchDt
								datad0060.GYOMNO = item.KYUKSNSCD
								datad0060.KAKUNIN = "申請中"
                                datad0060.BANGUMINM = item.M0060.KYUKNM
                                datad0060.MEMO = item.GYOMMEMO
                                If item.KANRIMEMO IsNot Nothing Then
                                    datad0060.MEMO = datad0060.MEMO & " (" & item.KANRIMEMO & ")"
                                End If
								datad0060.DATAKBN = "4"
								datad0060.USERID = id
								datad0060.STTIMEupdt = item.JKNST
								datad0060.EDTIMEupdt = item.JKNED
								datad0060.KKNST = item.KKNST
								datad0060.KKNED = item.KKNED

								If bolKyushutsu = True Then
									datad0060.KYUSHUTSU = strKyushutsu
									datad0060.ROWBACKCOLOR = rowbackcolor
									datad0060.ROWWAKUCOLOR = rowwakucolor
									datad0060.ROWFONTCOLOR = rowfontcolor
								End If

								'個人メモ
								If d0140Search IsNot Nothing Then
									datad0060.MYMEMO = d0140Search.USERMEMO
								End If

								listdata.Add(datad0060)
							Else
								bolDataHave = True
								'休暇申請時間ごと
								datad0060.GYOMDT = SearchDt
								datad0060.GYOMNO = item.KYUKSNSCD
								datad0060.KAKUNIN = "申請中"
								datad0060.STTIME = item.JKNST.Substring(0, 2) & ":" & item.JKNST.Substring(2, 2)
								datad0060.EDTIME = item.JKNED.Substring(0, 2) & ":" & item.JKNED.Substring(2, 2)
                                datad0060.BANGUMINM = item.M0060.KYUKNM
                                datad0060.MEMO = item.GYOMMEMO
                                If item.KANRIMEMO IsNot Nothing Then
                                    datad0060.MEMO = datad0060.MEMO & " (" & item.KANRIMEMO & ")"
                                End If
                                datad0060.DATAKBN = "4"
								datad0060.USERID = id
								data.TITLEKBN = "0"
								datad0060.STTIMEupdt = item.JKNST
								datad0060.EDTIMEupdt = item.JKNED
								datad0060.KKNST = item.KKNST
								datad0060.KKNED = item.KKNED

								If bolKyushutsu = True Then
									datad0060.KYUSHUTSU = strKyushutsu
									datad0060.ROWBACKCOLOR = rowbackcolor
									datad0060.ROWWAKUCOLOR = rowwakucolor
									datad0060.ROWFONTCOLOR = rowfontcolor
								End If

								'ASI[08 Nov 2019]
								datad0060.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG

								datad0060.HI = SearchDt.ToString("yyyy/MM/dd")
								datad0060.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"

								'個人メモ      
								If d0140Search IsNot Nothing Then
									datad0060.MYMEMO = d0140Search.USERMEMO
								End If

								listdata.Add(datad0060)
							End If
						Next

						If bolDataHave = False Then

							If bolKyushutsu = True Then
								data.KYUSHUTSU = strKyushutsu
								data.ROWBACKCOLOR = rowbackcolor
								data.ROWWAKUCOLOR = rowwakucolor
								data.ROWFONTCOLOR = rowfontcolor
							End If
							data.GYOMDT = SearchDt.ToString("yyyy/MM/dd")
							'ASI[08 Nov 2019]
							data.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG
							data.HI = SearchDt.ToString("yyyy/MM/dd")
							data.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"
							data.DATAKBN = "5"
							data.USERID = id
							listdata.Add(data)
						End If
					Else
						data.GYOMDT = SearchDt.ToString("yyyy/MM/dd")
						'ASI[08 Nov 2019]
						data.DESKMEMOEXISTFLG = DESKMEMOEXISTFLG
						data.HI = SearchDt.ToString("yyyy/MM/dd")
						data.YOBI = "(" & Days(SearchDt.DayOfWeek) & ")"
						data.DATAKBN = "5"
						data.USERID = id
						data.TITLEKBN = "1"
						data.BANGUMINM = "公休展開されていません。"
						data.MYMEMOFLG = "0"
						listdata.Add(data)
					End If
				Next
			Next
			listdata = listdata.OrderBy(Function(f) f.HI).ThenBy(Function(f) f.STTIME).ToList

			Return listdata
		End Function
	End Class
End Namespace