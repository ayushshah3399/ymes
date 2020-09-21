Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace Controllers
	Public Class C0050Controller
		Inherits Controller

		Private db As New Model1

		Function ReturnLoginPartial() As ActionResult
			ViewData!ID = "Login"
			Return PartialView("_LoginPartial")
		End Function

		Function Index(ByVal id As String, ByVal msgShow As String, ByVal Searchdt As String, ByVal cndradio As String, ByVal cndman As String, ByVal cndwoman As String, ByVal viewkosoku1 As String, ByVal viewkosoku2 As String, ByVal cnduserid As String) As ActionResult
			Dim cnd_SearchDt As String = Searchdt
			Dim cnd_ViewAna As String = cndradio
			Dim cnd_Man As String = cndman
			Dim cnd_Woman As String = cndwoman
			Dim vw_kosoku1 As String = viewkosoku1
			Dim vw_kosoku2 As String = viewkosoku2
			Dim strSQL As String = ""

			Dim loginUserId As String = Session("LoginUserid")
			If loginUserId = Nothing Then
				Return ReturnLoginPartial()
			End If

			'ASI [2020 Jan 23] Check login user is desk chief
			If (Session("LoginUserACCESSLVLCD") = 3) AndAlso Session("LoginUserDeskChief") = 1 Then
				ViewData("LoginUserDeskChief") = 1
			End If

			If String.IsNullOrEmpty(id) Then
				id = loginUserId
			End If

			'伝言板表示・非表示
			If msgShow IsNot Nothing Then
				Session("msgShow") = msgShow
			End If

			'業務登録、修正で使用するSession値の初期化
			For intIdx As Integer = Session.Keys.Count - 1 To 0 Step -1
				Dim strKey As String = Session.Keys(intIdx).ToString
				If strKey.Contains("B0020") OrElse strKey.Contains("D0010") Then
					Session.RemoveAt(intIdx)
				End If
			Next

			'以下、初回読み込み時のときは引数がNothingなので初期値をセット
			If String.IsNullOrEmpty(cnd_SearchDt) = True Then
				cnd_SearchDt = Today.ToString("yyyy/MM/dd")
			End If
			If String.IsNullOrEmpty(cnd_ViewAna) = True Then
				cnd_ViewAna = "All"
			End If
			If String.IsNullOrEmpty(cnd_Man) = True Then
				cnd_Man = "1"
			End If
			If String.IsNullOrEmpty(cnd_Woman) = True Then
				cnd_Woman = "1"
			End If
			If String.IsNullOrEmpty(vw_kosoku1) = True Then
				vw_kosoku1 = "0"
			End If
			If String.IsNullOrEmpty(vw_kosoku2) = True Then
				vw_kosoku2 = "0"
			End If

			ViewData("frompage") = "C0050"
			ViewData("LoginUsernm") = Session("LoginUsernm")
			ViewData("LoginUserACCESSLVLCD") = Session("LoginUserACCESSLVLCD")
			ViewData("searchdt") = cnd_SearchDt
			ViewData("userid") = loginUserId
			ViewData("name") = Session("LoginUsernm")

			'以下、条件として使用しているコントロール用
			ViewData("cndradio") = cnd_ViewAna  'Only:勤務日のアナのみ All:すべてのアナ
			ViewData("cndman") = cnd_Man  '0:チェックOff 1:チェックOn
			ViewData("cndwoman") = cnd_Woman '0:チェックOff 1:チェックOn
			ViewData("cnduserid") = cnduserid

			'担当アナを選択されていたらアナ名をcndusernmに渡す
			Dim strSelectUserNm As String = ""
			If String.IsNullOrEmpty(cnduserid) = False Then
				Dim lstSelectUserID As String() = cnduserid.Split(","c)
				For intAry As Integer = 0 To lstSelectUserID.Count - 1
					Dim intID As Integer = Integer.Parse(lstSelectUserID(intAry))
					Dim dtM0010 = db.M0010.Where(Function(m) m.USERID = intID).ToList
					If String.IsNullOrEmpty(strSelectUserNm) = False Then
						strSelectUserNm &= ", "
					End If
					strSelectUserNm &= dtM0010(0).USERNM.ToString
				Next
			End If
			ViewData("cndusernm") = strSelectUserNm

			'拘束時刻
			ViewData("kosoku1") = vw_kosoku1
			ViewData("kosoku2") = vw_kosoku2

			Dim loginUserKanri As Boolean = Session("LoginUserKanri")
			Dim loginUserSystem As Boolean = Session("LoginUserSystem")

			If (loginUserKanri OrElse loginUserSystem) Then
				ViewData("Kanri") = "1"     '管理職
			Else
				ViewData("Kanri") = "0"
			End If

			If loginUserSystem Then
				ViewData("System") = "1"    'システム管理者
			Else
				ViewData("System") = "0"
			End If

			'以下、ViewBagに保存
			ViewBag.LoginUserACCESSLVLCD = Session("LoginUserACCESSLVLCD")

			'ユーザー情報から公休展開個人実行と公休展開全員実行のフラグを取得
			Dim intUserid As Integer = Integer.Parse(loginUserId)
			Dim m0010KOKYU = db.M0010.Find(intUserid)
			ViewBag.KOKYUTENKAI = m0010KOKYU.KOKYUTENKAI
			ViewBag.KOKYUTENKAIALL = m0010KOKYU.KOKYUTENKAIALL

			'業務申請で未処理のものがある場合、申請ありを表示するため
			Dim d0050 = db.D0050.Where(Function(m) m.SHONINFLG = False)
			If d0050.Count > 0 Then
				ViewBag.GYOMFLG = "1"
			End If

			'休暇申請で未処理のものがある場合、申請ありを表示するため
			Dim d0060 = db.D0060.Where(Function(m) m.SHONINFLG = False)
			If d0060.Count > 0 Then
				ViewBag.KYUKFLG = "1"
			End If

			'★★★ 伝言板表示に使用する伝言板情報を「Message」「MessageList」に追加する ★★★
			Dim item As New D0080
			item.USERID = id
			item.TOROKUYMD = Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
			item.DATAFLG = "0"
			ViewData.Add("Message", item)

			Dim d0080 = db.D0080.Where(Function(m) m.DATAFLG = "0")
			d0080 = d0080.OrderByDescending(Function(f) f.DNGNNO)
			ViewData.Add("MessageList", d0080.ToList())

			'★★★ 休暇凡例リスト（_CategoryListPartial）で使用する休暇コードテーブルを「ColorList」に追加する ★★★
			Dim M0060 = db.M0060.OrderBy(Function(f) f.HYOJJN)
			M0060 = M0060.Where(Function(m) m.TNTHYOHYOJ = True)
			ViewData.Add("ColorList", M0060.ToList())

			'★★★ アナウンサー一覧に表示するユーザーを「UserList」に追加する ★★★
			Dim lstUSERID = db.M0010.Where(Function(m) m.HYOJ = True AndAlso m.STATUS = True AndAlso m.M0050.ANA = True).OrderBy(Function(m) m.USERSEX).ThenBy(Function(m) m.HYOJJN).ToList
			ViewData.Add("UserList", lstUSERID)

            '★★★ 指定日のアナウンサー一覧の色設定情報を取得して「UserColor」に追加する（_UserListParitalに渡す） ★★★
            strSQL = "SELECT FUNC.* FROM TeLAS.FN_C0050_USERCOLOR(@av_ymd) AS FUNC ORDER BY USERID"

            '指定日を引数で渡す
            Dim sqlpara1 As New SqlParameter("av_ymd", SqlDbType.VarChar, 10)
			sqlpara1.Value = cnd_SearchDt

			Dim lstUserList As New List(Of M_UserList)
			lstUserList = db.Database.SqlQuery(Of M_UserList)(strSQL, sqlpara1).ToList

			'取得したデータの1件目「勤務」から文字色を「KinmuColor」にセット
			If lstUserList(0).USERID = -1 Then
				ViewData.Add("KinmuColor", lstUserList(0).FONTCOLOR)
				lstUserList.RemoveAt(0) 'このデータはもういらないから削除
			End If
			ViewData.Add("UserColor", lstUserList)

            '★★★ 指定日のスケジュールを取得してメインデータとしてViewで返す ★★★
            strSQL = "SELECT FUNC.* FROM TeLAS.FN_C0050_MAINDATA(@av_ymd,@p_LoginUserACCESSLVLCD) AS FUNC{0} ORDER BY FUNC.HYOJJN, FUNC.USERSEX, FUNC.USERNM, FUNC.TM"
            Dim strWhere As String = ""
			Dim strWhereAna As String = ""
			Dim strWhereSex As String = ""

			'条件：勤務アナのみ
			If cnd_ViewAna = "Only" Then
				strWhereAna = "NOT FUNC.KYUKCD IN (4,5,6,8,11,12)"
			ElseIf cnd_ViewAna = "In" Then
				strWhereAna = "FUNC.USERID IN (" & cnduserid & ")"
			End If
			'条件：男性のみ、または女性のみ
			If cnd_Man = "1" AndAlso cnd_Woman = "0" Then
				strWhereSex = "FUNC.USERSEX = '0'"
			ElseIf cnd_Man = "0" AndAlso cnd_Woman = "1" Then
				strWhereSex = "FUNC.USERSEX = '1'"
			End If
			If String.IsNullOrEmpty(strWhereAna) = False AndAlso String.IsNullOrEmpty(strWhereSex) = False Then
				strWhere = " WHERE " & strWhereAna & " AND " & strWhereSex
			ElseIf String.IsNullOrEmpty(strWhereAna) = False OrElse String.IsNullOrEmpty(strWhereSex) = False Then
				strWhere = " WHERE " & strWhereAna & strWhereSex
			End If

			'条件があれば追加
			strSQL = String.Format(strSQL, New String() {strWhere})

			'指定日を引数で渡す
			Dim sqlpara2 As New SqlParameter("av_ymd", SqlDbType.VarChar, 10)
			sqlpara2.Value = cnd_SearchDt

            Dim sqlpara3 As New SqlParameter("p_LoginUserACCESSLVLCD", SqlDbType.SmallInt)
            sqlpara3.Value = ViewBag.LoginUserACCESSLVLCD

            Dim lstC0050FUNC As New List(Of M_C0050)
            lstC0050FUNC = db.Database.SqlQuery(Of M_C0050)(strSQL, sqlpara2, sqlpara3).ToList

            Return View(lstC0050FUNC)
		End Function

		<HttpPost(), ActionName("Delete")>
		<ValidateAntiForgeryToken()>
		Function DeleteConfirmed(ByVal DNGNNO As Decimal) As ActionResult
			Dim d0080 As D0080 = db.D0080.Find(DNGNNO)
			db.D0080.Remove(d0080)
			db.SaveChanges()
			Return RedirectToAction("Index")
		End Function
	End Class
End Namespace