Imports System.Web.Mvc
Imports MES_WEB.My.Resources

Namespace Controllers
	Public Class PswChangeController
		Inherits Controller

		'Object Of the Database Model
		Dim Db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: PswChange
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'If Session("LoginUserid") Is Nothing Then
			'	Return RedirectToAction("Index", "Login")
			'End If

			ViewData!ID = LangResources.L1_20_Fn_ChangePassword
			Return View()
		End Function

		<HttpPost>
		Function Index(ByVal PswChnageLoinId As String, ByVal OldPassword As String, ByVal NewPassword As String, ByVal ConfirmPassword As String) As ActionResult

			'Create Object of model1
			Dim db As New Model1
			Dim s0050 = (From t In db.s0050 Where t.userid = PswChnageLoinId).ToList

			If s0050.Count > 0 AndAlso s0050(0).pass = db.Encryptsyspass(OldPassword) Then

				'Upadate Password after Incrypt
				s0050(0).pass = db.Encryptsyspass(NewPassword)
				db.Configuration.ValidateOnSaveEnabled = False
				db.SaveChanges()
				db.Configuration.ValidateOnSaveEnabled = True

				'Redirect to Password Submit Page
				Return RedirectToAction("Index", "PswChangedSucess")

			Else

				'Display Error Message That Login Id and Passwords Are Not Matched.
				TempData("LoinId") = PswChnageLoinId
				TempData("LoginErrMsg") = LangResources.MSG_L1_21_InvalidUserPsw
				ViewData!ID = LangResources.L1_20_Fn_ChangePassword
				Return View()

			End If

		End Function

		' GET: PswChange/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: PswChange/Create
		Function Create() As ActionResult
			Return View()
		End Function

		' POST: PswChange/Create
		<HttpPost()>
		Function Create(ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add insert logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: PswChange/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: PswChange/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: PswChange/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: PswChange/Delete/5
		<HttpPost()>
		Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add delete logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function
	End Class
End Namespace