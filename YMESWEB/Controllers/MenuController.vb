Imports System.Web.Mvc
Imports MES_WEB.My.Resources

Namespace Controllers
	Public Class MenuController
		Inherits Controller

		'Object Of the Database Model
		Dim Db As New Model1
	
		' GET: Menu
		Function Index() As ActionResult

			ViewData!ID = LangResources.M1_01_Fn_Menu

			'If Sesson Is Nothing It Means 
			'May Session is Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData("LoginUsernm") = Session("LoginUsernm")
			ViewData("LoginUserid") = Session("LoginUserid")
			Dim LoginUserid As String = Session("LoginUserid")

			Dim objsy040Cnt = (From t In Db.sy040 Where t.userid = LoginUserid).ToList

			If objsy040Cnt.Count > 0 Then

				Dim objsy040 = (From t In Db.sy040 Where t.userid = LoginUserid AndAlso t.usekbn = "1").ToList
				'Check Customer Setting
				If objsy040.Count > 0 Then
					For i As Integer = 0 To objsy040.Count - 1
						ViewData(objsy040(i).menunm) = objsy040(i).menunm
					Next
				End If

			Else

				'If No Customer Setting then Check Group Code Setting
				'Get Group CD
				Dim objs0060 = (From t In Db.s0060 Where t.userid = LoginUserid).ToList
				Dim SreGroupcd As String = objs0060(0).grpcd

				'Display All The Forms
				If SreGroupcd = "AD" Then
					ViewData!Groupcd = "AD"
				Else
					Dim objsy030 = (From t In Db.sy030 Where t.usekbn = "1" AndAlso t.grpcd = SreGroupcd).ToList

					'Check Customer Setting
					If objsy030.Count > 0 Then
						For i As Integer = 0 To objsy030.Count - 1
							ViewData(objsy030(i).menunm) = objsy030(i).menunm
						Next
					End If
				End If

			End If

			Return View()
		End Function
        ' Post: Menu
        <HttpPost>
        Function Index(ByVal ReceiveInput As String, ByVal ReceiveCancel As String, ByVal ShelfMove As String, ByVal PayOutInput As String,
                       ByVal PayOutCancel As String,
                       ByVal PayOutApprove As String, ByVal PayOutApproveCancle As String,
                       ByVal PayOutGetApprove As String, ByVal PayOutGetApproveCancle As String,
                       ByVal ReprintItemLabel As String, ByVal IDTagInquiry As String,
                       ByVal SetPrinter As String,
                       ByVal StockTakeInput As String,
                       ByVal OneByOneJessiki As String,
                       ByVal OneByOneJessikiCancel As String) As ActionResult

            If ReceiveInput = "1010" Then

                Return RedirectToAction("HeaderText", "A1010_ReceiveInput")

            ElseIf ReceiveCancel = "1015" Then

                Return RedirectToAction("Index", "A1015_ReceiveCancel")

            ElseIf ShelfMove = "1020" Then

                Return RedirectToAction("Index", "A1020")

            ElseIf PayOutInput = "1030" Then

                Return RedirectToAction("Index", "A1030_PayOutInput")

            ElseIf PayOutCancel = "1035" Then

                Return RedirectToAction("Index", "A1035_PayOutCancel")

            ElseIf PayOutApprove = "1040" Then

                Return RedirectToAction("Index", "A1040_PayOutApprove")

            ElseIf PayOutApproveCancle = "1045" Then

                Return RedirectToAction("Index", "A1045_PayOutApproveCancle")

            ElseIf PayOutGetApprove = "1050" Then

                Return RedirectToAction("Index", "A1050_PayOutGetApprove")

            ElseIf PayOutGetApproveCancle = "1055" Then

                Return RedirectToAction("Index", "A1055_PayOutGetApproveCancle")

            ElseIf ReprintItemLabel = "1060" Then

                Return RedirectToAction("Index", "A1060_ReprintItemLabel")

            ElseIf IDTagInquiry = "1070" Then

                Return RedirectToAction("Index", "A1070_IDTagInquiry")

            ElseIf StockTakeInput = "B1010" Then

                Return RedirectToAction("Index", "B1010_StockTakeInput")

            ElseIf OneByOneJessiki = "C1010" Then

                Return RedirectToAction("Index", "C1010_OneByOneJessiki")

            ElseIf OneByOneJessikiCancel = "C1015" Then

                Return RedirectToAction("Index", "C1015_OneByOneJessikiCancel")

            ElseIf SetPrinter = "SetPrinter" Then

                Return RedirectToAction("Index", "SetPrinter")

            Else

                ViewData!ID = LangResources.M1_01_Fn_Menu
                Return View()

            End If

        End Function

        ' GET: Menu/Details/5
        Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: Menu/Create
		Function Create() As ActionResult
			Return View()
		End Function

		' POST: Menu/Create
		<HttpPost()>
		Function Create(ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add insert logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: Menu/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: Menu/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: Menu/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: Menu/Delete/5
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