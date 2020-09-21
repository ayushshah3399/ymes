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
	Public Class WB0050Controller
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

		' GET: WB00501/Create
		Function Create(ByVal searchdt As String, ByVal userid As String) As ActionResult

			If IsNothing(searchdt) Then
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

			Dim strSearchDate As String = searchdt & "/01"
			Dim dtSearchDate As Date = Date.Parse(strSearchDate)
			Dim DaysInMonth As Integer = Date.DaysInMonth(dtSearchDate.Year, dtSearchDate.Month)
			ViewData("searchdt") = searchdt
			ViewData("Days") = DaysInMonth

			If String.IsNullOrEmpty(userid) = False Then
				ViewData("userid") = userid
				ViewData("name") = db.M0010.Find(Integer.Parse(userid)).USERNM
			End If

			Dim intNengetu As Integer = Integer.Parse(searchdt.Replace("/", ""))
			Dim lstW0060 As New List(Of W0060)

			For intHi As Integer = 1 To DaysInMonth
				Dim w0060 As New W0060
				w0060.NENGETU = intNengetu
				w0060.HI = intHi
				lstW0060.Add(w0060)
			Next

			If Request.UrlReferrer IsNot Nothing Then
				Dim strUrlReferrer As String = Request.UrlReferrer.AbsoluteUri		'休日設定画面から来た時アナ名が文字化けするので、Encodeされている元のUrlを取得
				If Not strUrlReferrer.Contains("WB0050/Create") Then
					Session("WB0050UrlReferrer") = strUrlReferrer
				End If
			End If

			Return View(lstW0060)
		End Function

		' POST: WB00501/Create
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Create(<Bind(Include:="NENGETU,HI,KOUKYU")> ByVal W0060 As List(Of W0060), ByVal searchdt As String, ByVal userid As String) As ActionResult

			If IsNothing(searchdt) Then
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

			Dim loginUserNM As String = Session("LoginUsernm")

			Dim intNengetu As Integer = W0060(0).NENGETU
			Dim intUserid As Integer = 0
			If String.IsNullOrEmpty(userid) = False Then
				intUserid = Integer.Parse(userid)
			End If

			'If ModelState.IsValid Then
			'	Dim lstD0030 As List(Of D0030) = Nothing
			'	If String.IsNullOrEmpty(userid) = False Then
			'		intUserid = Integer.Parse(userid)
			'		lstD0030 = db.D0030.Where(Function(d) d.NENGETU = intNengetu AndAlso d.USERID = intUserid).ToList
			'	Else
			'		lstD0030 = db.D0030.Where(Function(d) d.NENGETU = intNengetu).ToList
			'	End If

			'	If lstD0030.Count > 0 Then
			'		ModelState.AddModelError(String.Empty, "既に公休展開されています。")
			'	End If
			'End If

			If ModelState.IsValid Then

				'初期化する
				Dim lstW0060 = db.W0060
				For Each item In lstW0060
					db.W0060.Remove(item)
				Next

				db.SaveChanges()

				Dim sqlpara1 As New SqlParameter("av_clientinfo", SqlDbType.VarChar, 128)
				sqlpara1.Value = loginUserNM & "," & Request.UserHostAddress & "," & Request.Browser.Browser & " " & Request.Browser.Version

				Using tran As DbContextTransaction = db.Database.BeginTransaction
					Try
						Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_set_client_info @av_clientinfo", sqlpara1)

						'公休データを作成
						For Each item In W0060
							db.W0060.Add(item)
						Next

						db.SaveChanges()

						'タイムアウト時間を3分に設定
						db.Database.CommandTimeout = 180

						'公休展開処理()
						Exe_pr_b0050_kokyutenkai(loginUserId, intNengetu, intUserid)

						db.Database.CommandTimeout = Nothing

						tran.Commit()

					Catch ex As SqlException
						Throw ex
						tran.Rollback()
					End Try
				End Using

				Dim strTaishoUserNm As String = "全員"
				If intUserid > 0 Then
					Dim m0010taisho = db.M0010.Find(intUserid)
					strTaishoUserNm = m0010taisho.USERNM & "さん"
				End If

				Dim intloginUserId As Integer = Integer.Parse(loginUserId)
				Dim m0010 = db.M0010.Find(intloginUserId)
				Dim strNengetsu As String = String.Format("{0}年{1}月", intNengetu.ToString.Substring(0, 4), intNengetu.ToString.Substring(4, 2))
				Dim strNow = Now.ToString("yyyy/MM/dd HH:mm:ss")
				Dim s0010 = db.S0010.Find(1)

				Dim strSubject As String = String.Format("{0}分 公休展開処理結果【{1}】", strNengetsu, s0010.APPNM)
				Dim sbBody As New StringBuilder
				sbBody.AppendLine("● 登録結果は以下となります。")
				sbBody.AppendLine("   実地者 : " & loginUserNM & "さん")
				sbBody.AppendLine("   対象者 : " & strTaishoUserNm)
				sbBody.AppendLine("   対象年月 : " & strNengetsu)
				sbBody.AppendLine("※添付ファイルは論理矛盾のチェック結果です")

				Dim sbResult As New StringBuilder

				sbResult.AppendLine("============================================================")
				sbResult.AppendLine(String.Format(" 論理矛盾データチェック結果  {0}", strNow))
				sbResult.AppendLine("============================================================")
				sbResult.AppendLine()
				sbResult.AppendLine("■ Ｗブッキングチェック結果 --------------------------------")

				Dim lstwgyom = (From w0070 In db.W0070 Join d0020 In db.D0020 On w0070.GYOMNO Equals d0020.GYOMNO
								Join m2 In db.M0020 On w0070.CATCD Equals m2.CATCD Join m1 In db.M0010 On d0020.USERID Equals m1.USERID
								Where w0070.ACUSERID = intloginUserId
								Order By d0020.USERID, w0070.GYOMYMD, w0070.GYOMYMDED, w0070.KSKJKNST, w0070.KSKJKNED, w0070.CATCD
								Select AnaUserid = m1.USERID, m1.USERNM, w0070.GYOMYMD, w0070.GYOMYMDED, w0070.KSKJKNST, w0070.KSKJKNED, m2.CATNM, w0070.BANGUMINM, w0070.NAIYO).ToList


				If lstwgyom.Count = 0 Then
					sbResult.AppendLine("該当なし")
				Else
					Dim intAnaUserid As Integer = 0
					For Each dr In lstwgyom
						If intAnaUserid <> dr.AnaUserid Then
							intAnaUserid = dr.AnaUserid
							sbResult.AppendLine()
							sbResult.AppendLine("   担当アナ : " & dr.USERNM)
						End If

						sbResult.AppendLine("     業務期間   : " & dr.GYOMYMD & "～" & dr.GYOMYMDED)
						sbResult.AppendLine("     拘束時間   : " & dr.KSKJKNST.Substring(0, 2) & ":" & dr.KSKJKNST.Substring(2, 2) & "～" &
															  dr.KSKJKNED.Substring(0, 2) & ":" & dr.KSKJKNED.Substring(2, 2))
						sbResult.AppendLine("     カテゴリー : " & dr.CATNM)
						sbResult.AppendLine("     番組名     : " & dr.BANGUMINM)
						sbResult.AppendLine("     内容       : " & dr.NAIYO)
						sbResult.AppendLine()
					Next
				End If

				sbResult.AppendLine()

				sbResult.AppendLine("■ 休日業務チェック結果 ------------------------------------")

				Dim lstW0080 = db.W0080.Where(Function(w) w.ACUSERID = intloginUserId And w.KYUK24KOE = False).OrderBy(Function(f) f.USERID).
					ThenBy(Function(f) f.KYUKYMD).ThenBy(Function(f) f.JKNST).ThenBy(Function(f) f.JKNED).ToList()

				If lstW0080.Count = 0 Then
					sbResult.AppendLine("該当なし")
				Else
					Dim intAnaUserid As Integer = 0
					For Each w0080 In lstW0080
						If intAnaUserid <> w0080.USERID Then
							intAnaUserid = w0080.USERID
							sbResult.AppendLine()
							sbResult.AppendLine("   アナ : " & w0080.M0010.USERNM)
						End If
						sbResult.AppendLine("     休暇期間   : " & w0080.KYUKYMD)
						sbResult.AppendLine("     時間       : " & w0080.JKNST.Substring(0, 2) & ":" & w0080.JKNST.Substring(2, 2) & "～" &
															  w0080.JKNED.Substring(0, 2) & ":" & w0080.JKNED.Substring(2, 2))
						sbResult.AppendLine("     休暇の種類 : " & w0080.M0060.KYUKNM)
						sbResult.AppendLine()
					Next
				End If

				sbResult.AppendLine()

				sbResult.AppendLine("■ 削除アナウンサーチェック結果 ----------------------------")

				Dim lstW0090 = db.W0090.Where(Function(w) w.ACUSERID = intloginUserId).OrderBy(Function(f) f.USERID).ThenBy(Function(f) f.GYOMNO).ToList
				If lstW0090.Count = 0 Then
					sbResult.AppendLine("該当なし")
				Else
					Dim intAnaUserid As Integer = 0
					For Each w0090 In lstW0090
						If intAnaUserid <> w0090.USERID Then
							intAnaUserid = w0090.USERID
							sbResult.AppendLine()
							sbResult.AppendLine("   担当アナ : " & w0090.M0010.USERNM)
						End If
						Dim d0010 = db.D0010.Find(w0090.GYOMNO)
						sbResult.AppendLine("     業務期間   : " & d0010.GYOMYMD & "～" & d0010.GYOMYMDED)
						sbResult.AppendLine("     拘束時間   : " & d0010.KSKJKNST.Substring(0, 2) & ":" & d0010.KSKJKNST.Substring(2, 2) & "～" &
																 d0010.KSKJKNED.Substring(0, 2) & ":" & d0010.KSKJKNED.Substring(2, 2))
						sbResult.AppendLine("     カテゴリー : " & d0010.M0020.CATNM)
						sbResult.AppendLine("     番組名     : " & d0010.BANGUMINM)
						sbResult.AppendLine("     内容       : " & d0010.NAIYO)
						sbResult.AppendLine("     休暇の種類 : " & w0090.M0060.KYUKNM)
						sbResult.AppendLine()
					Next
				End If

				sbResult.AppendLine()
				sbResult.AppendLine("============================================================")
				sbResult.AppendLine(String.Format(" チェック終了                {0}", strNow))
				sbResult.AppendLine("============================================================")

				'txtファイルを書き込む（既に存在するときは上書きされる） 
				Dim strDirectory As String = s0010.KOKYUTENKAIPATH

				'フォルダー存在する時、2ヶ月より前に作成したファイルは削除する
				If Directory.Exists(strDirectory) Then

					Dim strFileNames As String() = Directory.GetFiles(strDirectory, "*.txt")
					Dim strDate2MonthAgo As String = Today.AddMonths(-2).ToString("yyyyMMdd")

					For Each strFileName As String In strFileNames

						Dim strfNameDate As String = strFileName.Substring(strFileName.LastIndexOf("_") + 1, 8)

						If strfNameDate < strDate2MonthAgo Then
							Try
								System.IO.File.Delete(strFileName)
							Catch ex As IOException
								'もし使用中のためエラーになる場合は無視。次回削除すれば良い。
							End Try
						End If
					Next

				Else
					'フォルダー存在しない場合、作成。
					Directory.CreateDirectory(strDirectory)
				End If

				Dim strPath As String = strDirectory & String.Format("\{0}分_公休展開処理結果", strNengetsu) & "_" & Now.ToString("yyyyMMddHHmm") & ".txt"
				System.IO.File.WriteAllText(strPath, sbResult.ToString, System.Text.Encoding.GetEncoding("shift_jis"))


				Dim lstToMailid As New List(Of String)
				If String.IsNullOrEmpty(m0010.MAILLADDESS) = False Then
					lstToMailid.Add(m0010.MAILLADDESS)
				End If

				If String.IsNullOrEmpty(m0010.KEITAIADDESS) = False Then
					lstToMailid.Add(m0010.KEITAIADDESS)
				End If

				'メール送信
				If lstToMailid.Count > 0 Then
					DoSendMail(lstToMailid.ToArray, strSubject, sbBody.ToString, , , , {strPath})
				End If

                '戻る
                If String.IsNullOrEmpty(Session("WB0050UrlReferrer")) = False Then
                    Return Redirect(Session("WB0050UrlReferrer"))
                Else
                    Return RedirectToAction("Index", "B0040", routeValues:=New With {.showdate = searchdt})
				End If
			End If

				Dim strSearchDate As String = searchdt & "/01"
				Dim dtSearchDate As Date = Date.Parse(strSearchDate)
				Dim DaysInMonth As Integer = Date.DaysInMonth(dtSearchDate.Year, dtSearchDate.Month)
				ViewData("searchdt") = searchdt
				ViewData("Days") = DaysInMonth
				ViewData("userid") = userid

				Return View(W0060)
		End Function

		Function Exe_pr_b0050_kokyutenkai(ByVal intAcuserid As Integer, ByVal intNengetu As Integer, ByVal intUserid As Integer) As Boolean

			Dim sqlpara1 As New SqlParameter("asi_acuserid", SqlDbType.SmallInt)
			sqlpara1.Value = intAcuserid

			Dim sqlpara2 As New SqlParameter("ai_nengetu", SqlDbType.Int)
			sqlpara2.Value = intNengetu

			Dim sqlpara3 As New SqlParameter("asi_userid", SqlDbType.SmallInt)
			sqlpara3.Value = intUserid

			Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_b0050_kokyutenkai @asi_acuserid, @ai_nengetu, @asi_userid", sqlpara1, sqlpara2, sqlpara3)

			Return True
		End Function


		Function DoSendMail(ByVal strToEmailIds As String(), _
Optional strSubject As String = Nothing, _
Optional strBody As String = Nothing, _
Optional strCcEmailIds As String() = Nothing, _
Optional strBccEmailIds As String() = Nothing, _
Optional bolHtmlBody As Boolean = False, _
Optional strAattchments As String() = Nothing, _
Optional strSMTPServer As String = "", _
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

		' GET: WB00501
		Function Index() As ActionResult
			Return View(db.W0060.ToList())
		End Function

		' GET: WB00501/Details/5
		Function Details(ByVal id As Short?) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim w0060 As W0060 = db.W0060.Find(id)
			If IsNothing(w0060) Then
				Return HttpNotFound()
			End If
			Return View(w0060)
		End Function


		' GET: WB00501/Edit/5
		Function Edit(ByVal id As Short?) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim w0060 As W0060 = db.W0060.Find(id)
			If IsNothing(w0060) Then
				Return HttpNotFound()
			End If
			Return View(w0060)
		End Function

		' POST: WB00501/Edit/5
		'過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
		'詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
		<HttpPost()>
		<ValidateAntiForgeryToken()>
		Function Edit(<Bind(Include:="KYUKCD,HI,STTIME,EDTIME")> ByVal wB0050 As W0060) As ActionResult
			If ModelState.IsValid Then
				db.Entry(wB0050).State = EntityState.Modified
				db.SaveChanges()
				Return RedirectToAction("Index")
			End If
			Return View(wB0050)
		End Function

		' GET: WB00501/Delete/5
		Function Delete(ByVal id As Short?) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim w0060 As W0060 = db.W0060.Find(id)
			If IsNothing(w0060) Then
				Return HttpNotFound()
			End If
			Return View(w0060)
		End Function

		' POST: WB00501/Delete/5
		<HttpPost()>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken()>
		Function DeleteConfirmed(ByVal id As Short) As ActionResult
			Dim w0060 As W0060 = db.W0060.Find(id)
			db.W0060.Remove(w0060)
			db.SaveChanges()
			Return RedirectToAction("Index")
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

	End Class
End Namespace
