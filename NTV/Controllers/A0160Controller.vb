Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports NTV_SHIFT

Namespace Controllers
    Public Class A0160Controller
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

        ' GET: D0070
        Function Index(ByVal searchdt As String, ByVal Shiftdt As System.Nullable(Of Boolean), ByVal info As SortingPagingInfo) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
                Return ReturnLoginPartial()
            End If
            ViewData!LoginUsernm = Session("LoginUsernm")

            If CheckAccessLvl() = False Then
                Return View("ErrorAccesslvl")
            End If

            ViewBag.LoginUserSystem = Session("LoginUserSystem")
            ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")
            ViewBag.LoginUserKanri = Session("LoginUserKanri")

            '一括業務メニューを表示可能な条件追加
            Dim intUserid As Integer = Integer.Parse(loginUserId)
            Dim m0010KOKYU = db.M0010.Find(intUserid)
            ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
            ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL

            If String.IsNullOrEmpty(searchdt) Then
                searchdt = Today.ToString("yyyy/MM/dd")
            End If

            ViewData("searchdt") = searchdt

			Dim d0070 = db.D0070.Include(Function(d) d.M0010).Include(Function(d) d.M0020).Include(Function(d) d.M0130).Include(Function(d) d.M0140)

			If Shiftdt.HasValue = True Then
                If Shiftdt.Value = True Then
                    d0070 = d0070.Where(Function(m) (searchdt) <= m.GYOMYMDED AndAlso (searchdt) >= m.GYOMYMD).OrderByDescending(Function(f) f.GYOMYMD).ThenByDescending(Function(f) f.KSKJKNST)
                ElseIf Shiftdt.Value = False Then
                    'd0070 = d0070.Where(Function(m) m.SYUSEIYMD.ToString.Substring(0, 10) = (searchdt))
                    'd0070 = d0070.Where(Function(m) Date.Parse(m.SYUSEIYMD).ToString("yyyy/MM/dd") = (searchdt))
                    Dim searchTimeSt As DateTime = DateTime.Parse(searchdt & " 00:00")
                    Dim dtEndDate As Date = Date.Parse(searchdt).AddDays(1)
                    Dim strEndDate As String = dtEndDate.ToString("yyyy/MM/dd")
                    Dim SearchTimeEd As DateTime = DateTime.Parse(strEndDate & " 00:00")
                    d0070 = d0070.Where(Function(m) (searchTimeSt) <= m.SYUSEIYMD AndAlso (SearchTimeEd) > m.SYUSEIYMD).OrderByDescending(Function(f) f.SYUSEIYMD)

                End If
            Else
                d0070 = d0070.Where(Function(m) (searchdt) <= m.GYOMYMDED AndAlso (searchdt) >= m.GYOMYMD).OrderByDescending(Function(f) f.GYOMYMD).ThenByDescending(Function(f) f.KSKJKNST)
            End If

            Dim lstd0070 = d0070.ToList

            If info IsNot Nothing AndAlso info.SortField IsNot Nothing Then
                Select Case info.SortField

                    Case "HENKONAIYO"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.HENKONAIYO), lstd0070.OrderByDescending(Function(c) c.HENKONAIYO))).ToList

                    Case "USERNM"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.M0010.USERNM), lstd0070.OrderByDescending(Function(c) c.M0010.USERNM))).ToList

                    Case "SYUSEIYMD"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.SYUSEIYMD), lstd0070.OrderByDescending(Function(c) c.SYUSEIYMD))).ToList

                    Case "GYOMYMD"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.GYOMYMD), lstd0070.OrderByDescending(Function(c) c.GYOMYMD))).ToList

                    Case "KSKJKNST"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.KSKJKNST), lstd0070.OrderByDescending(Function(c) c.KSKJKNST))).ToList

                    Case "CATNM"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.M0020.CATNM), lstd0070.OrderByDescending(Function(c) c.M0020.CATNM))).ToList

                    Case "BANGUMINM"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.BANGUMINM), lstd0070.OrderByDescending(Function(c) c.BANGUMINM))).ToList

                    Case "OAJKNST"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.OAJKNST), lstd0070.OrderByDescending(Function(c) c.OAJKNST))).ToList

                    Case "NAIYO"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.NAIYO), lstd0070.OrderByDescending(Function(c) c.NAIYO))).ToList

                    Case "BASYO"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.BASYO), lstd0070.OrderByDescending(Function(c) c.BASYO))).ToList

                    Case "TNTNM"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.TNTNM), lstd0070.OrderByDescending(Function(c) c.TNTNM))).ToList

                    Case "BANGUMITANTO"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.BANGUMITANTO), lstd0070.OrderByDescending(Function(c) c.BANGUMITANTO))).ToList

                    Case "BANGUMIRENRK"

                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.BANGUMIRENRK), lstd0070.OrderByDescending(Function(c) c.BANGUMIRENRK))).ToList

                    Case "BIKO"
                        lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.BIKO), lstd0070.OrderByDescending(Function(c) c.BIKO))).ToList

                    Case "IKTFLG"
						lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.IKTFLG), lstd0070.OrderByDescending(Function(c) c.IKTFLG))).ToList

					Case "SPORTCATCD"
						lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) If(c.M0130 IsNot Nothing, c.M0130.SPORTCATNM, "")), lstd0070.OrderByDescending(Function(c) If(c.M0130 IsNot Nothing, c.M0130.SPORTCATNM, "")))).ToList

					Case "SPORTSUBCATCD"
						lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) If(c.M0140 IsNot Nothing, c.M0140.SPORTSUBCATNM, "")), lstd0070.OrderByDescending(Function(c) If(c.M0140 IsNot Nothing, c.M0140.SPORTSUBCATNM, "")))).ToList

					Case "SAIJKNST"
						lstd0070 = (If(info.SortDirection = "ascending", lstd0070.OrderBy(Function(c) c.SAIJKNST), lstd0070.OrderByDescending(Function(c) c.SAIJKNST))).ToList

				End Select

                ViewBag.SortingPagingInfo = info
            Else
                Dim infoNew As New SortingPagingInfo()
                info.SortField = "New"
                info.SortDirection = "ascending"
                ViewBag.SortingPagingInfo = infoNew
            End If

            Return View(lstd0070)
        End Function

		'POST: D0010
		<HttpPost()>
		Function Index(<Bind(Include:="HENKORRKCD,HENKONAIYO,USERID,SYUSEIYMD,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,TNTNM,IKTFLG,D0070,M0010,M0020,M0130,M0140")> ByVal lstd0070 As List(Of D0070), ByVal button As String) As ActionResult

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
			ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

			Return DownloadCsv(lstd0070)

		End Function


		Function DownloadCsv(ByVal lstd0070 As List(Of D0070)) As ActionResult

            Dim builder As New StringBuilder()
			Dim strRecord As String = "変更内容,修正者,修正日時,業務期間,業務期間-終了,拘束時間-開始,拘束時間-終了,カテゴリー名,番組名,OA時間-開始,OA時間-終了,内容,場所,担当アナウンサー,番組担当者,連絡先,備考,業務一括登録フラグ,スポーツカテゴリ,スポーツサブカテゴリ,試合時間-開始,試合時間-終了"
			builder.AppendLine(strRecord)

            'ENKORRKCD, HENKONAIYO, USERID, SYUSEIYMD, GYOMYMD, GYOMYMDED, KSKJKNST, KSKJKNED, CATCD, BANGUMINM, OAJKNST, OAJKNED, NAIYO, BASYO, BIKO, BANGUMITANTO, BANGUMIRENRK, TNTNM

            For Each d0070 As D0070 In lstd0070

                If d0070.HENKONAIYO IsNot Nothing Then
                    strRecord = d0070.HENKONAIYO
                End If
                strRecord = strRecord & ","

                If d0070.M0010 IsNot Nothing AndAlso d0070.M0010.USERNM IsNot Nothing Then
                    strRecord = strRecord & d0070.M0010.USERNM
                End If
                strRecord = strRecord & ","

                If d0070.SYUSEIYMD IsNot Nothing Then
                    strRecord = strRecord & d0070.SYUSEIYMD
                End If
                strRecord = strRecord & ","

                strRecord = strRecord & Date.Parse(d0070.GYOMYMD).ToString("yyyy/MM/dd") & ","

                If d0070.GYOMYMDED IsNot Nothing Then
                    strRecord = strRecord & Date.Parse(d0070.GYOMYMDED).ToString("yyyy/MM/dd")
                End If

                strRecord = strRecord & "," & d0070.KSKJKNST.ToString.Substring(0, 2) & ":" & d0070.KSKJKNST.ToString.Substring(2, 2) & "," &
                     d0070.KSKJKNED.ToString.Substring(0, 2) & ":" & d0070.KSKJKNED.ToString.Substring(2, 2) & ","


                If d0070.M0020 IsNot Nothing AndAlso d0070.M0020.CATNM IsNot Nothing Then
                    strRecord = strRecord & d0070.M0020.CATNM
                End If
                strRecord = strRecord & ","

                If d0070.BANGUMINM IsNot Nothing Then
                    strRecord = strRecord & d0070.BANGUMINM
                End If
                strRecord = strRecord & ","

                If d0070.OAJKNST IsNot Nothing Then
                    strRecord = strRecord & d0070.OAJKNST.ToString.Substring(0, 2) & ":" & d0070.OAJKNST.ToString.Substring(2, 2)
                End If
                strRecord = strRecord & ","

                If d0070.OAJKNED IsNot Nothing Then
                    strRecord = strRecord & d0070.OAJKNED.ToString.Substring(0, 2) & ":" & d0070.OAJKNED.ToString.Substring(2, 2)
                End If
                strRecord = strRecord & ","

                If d0070.NAIYO IsNot Nothing Then
                    strRecord = strRecord & d0070.NAIYO
                End If
                strRecord = strRecord & ","

                If d0070.BASYO IsNot Nothing Then
                    strRecord = strRecord & d0070.BASYO
                End If
                strRecord = strRecord & ","


                Dim strAna As String = ""
                If d0070.TNTNM IsNot Nothing Then
                    If d0070.TNTNM.Contains(",") Then
                        Dim Users As String() = d0070.TNTNM.Split(New Char() {","c})
                        For Each struser In Users
                            If String.IsNullOrEmpty(strAna) = False Then
                                strAna = strAna & "，"           '全角カンマ
                            End If
                            strAna = strAna & struser
                        Next
                    Else
                        strAna = d0070.TNTNM
                    End If
                End If
               
                strRecord = strRecord & strAna & ","


                If d0070.BANGUMITANTO IsNot Nothing Then
                    strRecord = strRecord & d0070.BANGUMITANTO
                End If
                strRecord = strRecord & ","

                If d0070.BANGUMIRENRK IsNot Nothing Then
                    strRecord = strRecord & d0070.BANGUMIRENRK
                End If
                strRecord = strRecord & ","
                If d0070.BIKO IsNot Nothing Then
                    strRecord = strRecord & d0070.BIKO
                End If

                Dim strIktflg As String = ""
                strRecord = strRecord & ","
                If d0070.IKTFLG IsNot Nothing Then
                    If d0070.IKTFLG = True Then
                        strIktflg = "一括登録"
                    Else
                        strIktflg = ""
                    End If
                    strRecord = strRecord & strIktflg
                End If

				strRecord = strRecord & ","
				If d0070.M0130 IsNot Nothing AndAlso d0070.M0130.SPORTCATNM IsNot Nothing Then
					strRecord = strRecord & d0070.M0130.SPORTCATNM
				End If

				strRecord = strRecord & ","
				If d0070.M0140 IsNot Nothing AndAlso d0070.M0140.SPORTSUBCATNM IsNot Nothing Then
					strRecord = strRecord & d0070.M0140.SPORTSUBCATNM
				End If

				strRecord = strRecord & ","
				If d0070.SAIJKNST IsNot Nothing Then
					strRecord = strRecord & d0070.SAIJKNST.ToString.Substring(0, 2) & ":" & d0070.SAIJKNST.ToString.Substring(2, 2)
				End If

				strRecord = strRecord & ","
				If d0070.SAIJKNED IsNot Nothing Then
					strRecord = strRecord & d0070.SAIJKNED.ToString.Substring(0, 2) & ":" & d0070.SAIJKNED.ToString.Substring(2, 2)
				End If

				builder.AppendLine(strRecord)
            Next

            ' 生成された文字列を「text/csv」形式（Shift_JIS）で出力
            Return File(System.Text.Encoding.GetEncoding("shift_jis").GetBytes(builder.ToString), "text/csv", "gyomuhenkoudata.csv")
        End Function


        ' GET: D0070/Details/5
        Function Details(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0070 As D0070 = db.D0070.Find(id)
			If IsNothing(d0070) Then
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

            Return View(d0070)
        End Function

        ' GET: D0070/Create
        Function Create() As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID")
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM")
            Return View()
        End Function

        ' POST: D0070/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="HENKORRKCD,HENKONAIYO,USERID,SYUSEIYMD,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,TNTNM,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0070 As D0070) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

            If ModelState.IsValid Then
                db.D0070.Add(d0070)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0070.USERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0070.CATCD)
            Return View(d0070)
        End Function

        ' GET: D0070/Edit/5
        Function Edit(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0070 As D0070 = db.D0070.Find(id)
			If IsNothing(d0070) Then
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

            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0070.USERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0070.CATCD)
            Return View(d0070)
        End Function

        ' POST: D0070/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="HENKORRKCD,HENKONAIYO,USERID,SYUSEIYMD,GYOMYMD,GYOMYMDED,KSKJKNST,KSKJKNED,CATCD,BANGUMINM,OAJKNST,OAJKNED,NAIYO,BASYO,BIKO,BANGUMITANTO,BANGUMIRENRK,TNTNM,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal d0070 As D0070) As ActionResult

            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If
            ViewData!LoginUsernm = Session("LoginUsernm")

			If CheckAccessLvl() = False Then
				Return View("ErrorAccesslvl")
			End If

            If ModelState.IsValid Then
                db.Entry(d0070).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.USERID = New SelectList(db.M0010, "USERID", "LOGINID", d0070.USERID)
            ViewBag.CATCD = New SelectList(db.M0020, "CATCD", "CATNM", d0070.CATCD)
            Return View(d0070)
        End Function

        ' GET: D0070/Delete/5
        Function Delete(ByVal id As Decimal) As ActionResult

			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim d0070 As D0070 = db.D0070.Find(id)
			If IsNothing(d0070) Then
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

            Return View(d0070)
        End Function

        ' POST: D0070/Delete/5
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

            Dim d0070 As D0070 = db.D0070.Find(id)
            db.D0070.Remove(d0070)
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
