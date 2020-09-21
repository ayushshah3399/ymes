Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class SetPrinterController
		Inherits Controller

		'Object Of the Database Model
		Dim Db As New Model1

		' GET: SetPrinter
		Function Index() As ActionResult

			'If Sesson Is Nothing It Means 
			'May Session is Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			'Get Data From Database
			Dim objsy050 = Db.sy050.OrderBy(Function(m) m.printer_name).ToList()
			Dim ListOfCollectionForPrintSetting As New List(Of PrintSetting)

			For i As Integer = 0 To objsy050.Count - 1
				Dim ObjOfProperty As New PrintSetting
				ObjOfProperty.PrinterId = objsy050(i).printer_id
				ObjOfProperty.PrinterName = objsy050(i).printer_name
				ListOfCollectionForPrintSetting.Add(ObjOfProperty)
			Next

			Dim ObjSy060 As New sy060
			ObjSy060.obj_model_PrintSetting = ListOfCollectionForPrintSetting

			Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")
			Dim obj_sy060 = (From t In Db.sy060 Where t.ipaddress = Ipaddress).ToList

			If obj_sy060.Count > 0 Then
				ObjSy060.HiddenlblForPrinterid = obj_sy060(0).printer_id
			End If

			'Change Lable of Print
			ViewData!ID = LangResources.P1_01_Fn_SetPrinter
			Return View(ObjSy060)

		End Function

		'Post Method
		<HttpPost>
		Function Index(ByVal objsy060 As sy060, ByVal btnRegister As String) As ActionResult

			'Selected Printer
			Dim PrinterId = objsy060.HiddenlblForPrinterid

			Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
			Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
			Dim Tras As DbContextTransaction = Db.Database.BeginTransaction
			Try

				Dim cnt = Db.Database.ExecuteSqlCommand("SELECT TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

				Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")
				Dim obj_sy060 = (From t In Db.sy060 Where t.ipaddress = Ipaddress).ToList
				'Check New Record Is there or not
				If obj_sy060.Count > 0 Then
					obj_sy060(0).printer_id = PrinterId
				Else
					Dim InsertNewRecord As New sy060
					InsertNewRecord.ipaddress = Ipaddress
					InsertNewRecord.printer_id = PrinterId
					Db.sy060.Add(InsertNewRecord)
				End If

				Db.Configuration.ValidateOnSaveEnabled = False
				Db.SaveChanges()
				Db.Configuration.ValidateOnSaveEnabled = True
				Tras.Commit()

				Return RedirectToAction("Index", "Menu")

			Catch ex As Exception
				Tras.Rollback()

                '倉庫棚卸中のため、登録できません。
                '工程棚卸中のため、登録できません。
                '完成・不良倉庫棚卸中のため、登録できません。
                '棚卸中のため、登録できません。
                Dim ObjTransactionLockStockController As New TransactionLockStockController
                Dim ObjException = ObjTransactionLockStockController.Check_Transaction_lock_Exception(ex)

                If ObjException IsNot Nothing Then
                    Return ObjException
                End If

                'Get stack trace for the exception with source file information
                Dim st = New StackTrace(ex, True)
				'Get the top stack frame
				Dim frame = st.GetFrame(st.GetFrames.Count - 1)
				'Get the line number from the stack frame
				Dim line = frame.GetFileLineNumber()

				TempData("Line") = line
				Throw ex

			Finally
				Tras.Dispose()

			End Try

		End Function

		'GET: SetPrinter/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: SetPrinter/Create
		Function Create() As ActionResult
			Return View()
		End Function

		' POST: SetPrinter/Create
		<HttpPost()>
		Function Create(ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add insert logic here
				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: SetPrinter/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: SetPrinter/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: SetPrinter/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: SetPrinter/Delete/5
		<HttpPost()>
		Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add delete logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		'Dispose The DB To Avoid Error related max Connection open
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				Db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

	End Class
End Namespace