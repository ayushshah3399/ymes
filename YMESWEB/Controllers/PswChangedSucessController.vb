Imports System.Web.Mvc
Imports MES_WEB.My.Resources

Namespace Controllers
    Public Class PswChangedSucessController
        Inherits Controller

		' GET: PswChangedSucess
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			''Or SomeOne Open Form Directly By Changing URL
			'If Session("LoginUserid") Is Nothing Then
			'	Return RedirectToAction("Index", "Login")
			'End If

			ViewData!ID = LangResources.L1_20_Fn_ChangePassword
			Return View()
		End Function
	End Class
End Namespace