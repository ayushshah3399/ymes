Imports System.Web.Mvc

Namespace Controllers
    Public Class LoginController
        Inherits Controller

        ' GET: Login
		Function Index() As ActionResult

            '既にログインされている時は担当表へ
            Dim loginUserId As String = Session("LoginUserid")
            If loginUserId IsNot Nothing Then
                Dim LoginUserKanri As String = Session("LoginUserKanri")
                Dim LoginUserSystem As String = Session("LoginUserSystem")
                Dim LoginUserAna As String = Session("LoginUserAna")

                '2020/01/13 一般ユーザーなら個人シフト画面へ遷移して、それ以外はスケジュール表へ遷移する
                If LoginUserKanri = False AndAlso LoginUserSystem = False AndAlso LoginUserAna = True Then
                    Return RedirectToAction("Index", "C0040")
                End If
                Return RedirectToAction("Index", "C0050")
            End If

            ViewData!ID = "Login"

			Return PartialView("_LoginPartial")
		End Function

		' Post: Login
		<HttpPost>
		Function Index(ByVal LoinId As String, ByVal Password As String, ByVal changepsw As String) As ActionResult

			Dim db As New Model1

			Dim m0010 = (From t In db.M0010 Where t.LOGINID = LoinId And t.STATUS = True).ToList

			'ログイン
			If m0010.Count > 0 AndAlso m0010(0).USERPWD = Password Then
				Session("LoginUserid") = m0010(0).USERID
				Session("LoginLoginid") = m0010(0).LOGINID
				Session("LoginUsernm") = m0010(0).USERNM
				Session("LoginUserKanri") = m0010(0).M0050.KANRI
				Session("LoginUserSystem") = m0010(0).M0050.SYSTEM
				Session("LoginUserAna") = m0010(0).M0050.ANA
                Session("LoginUserACCESSLVLCD") = m0010(0).ACCESSLVLCD

				Dim loginUserId As String = Session("LoginUserid")

				'ASI [2020 Jan 23] Check login user is desk chief
				Session("LoginUserDeskChief") = 0
				If (Session("LoginUserACCESSLVLCD") = 3) Then
					Dim m0160UID_ChiefFlgDataCnt = (From m In db.M0160 Where m.USERID = loginUserId And m.CHIEFFLG = True).Count
					If m0160UID_ChiefFlgDataCnt > 0 Then
						Session("LoginUserDeskChief") = 1
					End If
				End If

				'パスワード変更
				If changepsw = "1" Then
					Return RedirectToAction("ChangePassword", "A0110", routeValues:=New With {.id = m0010(0).USERID})
				Else
					'ログイン
					If Request.UrlReferrer IsNot Nothing Then
						If Not Request.UrlReferrer.LocalPath = "/" AndAlso Not Request.UrlReferrer.LocalPath.Contains("Login") Then
							Return Redirect(Request.UrlReferrer.ToString)
						End If
					End If

					'2020/01/13 一般ユーザーなら個人シフト画面へ遷移して、それ以外はスケジュール表へ遷移する
					If m0010(0).M0050.KANRI = False AndAlso m0010(0).M0050.SYSTEM = False AndAlso m0010(0).M0050.ANA = True Then
						Return RedirectToAction("Index", "C0040")
					End If

					Return RedirectToAction("Index", "C0050")
				End If

			Else
				TempData("LoinId") = LoinId
				TempData("LoginErrMsg") = "ユーザーID、又はパスワードが正しくありません。"
				If Request.UrlReferrer IsNot Nothing Then
					If Not Request.UrlReferrer.LocalPath = "/" AndAlso Not Request.UrlReferrer.LocalPath.Contains("Login") Then
						Return Redirect(Request.UrlReferrer.ToString)
					End If
				End If
			End If

			ViewData!ID = "Login"
			Return PartialView("_LoginPartial")
		End Function

		'View 無し
		' GET: Logout
		Function Logout() As ActionResult

			Session.RemoveAll()

			Return RedirectToAction("Index")
		End Function

	End Class

End Namespace