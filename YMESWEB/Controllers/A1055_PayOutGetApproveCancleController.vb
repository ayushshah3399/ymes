Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1055_PayOutGetApproveCancleController
		Inherits Controller

		Dim Db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1055_PayOutGetApproveCancle
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1055_01_Fn_CancelConfirm_Receiving
			Return View()

		End Function

		<HttpPost>
		Function Index(ByVal d_mes0100 As d_mes0100) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			Dim StrPickingNo As String = d_mes0100.TxtBox_picking_no

			Dim objd_mes0100 = (From t In Db.d_mes0100 Where t.picking_no = StrPickingNo AndAlso t.del_flag <> "1" AndAlso t.plant_code = StrPlantCode Order By t.item_code).ToList

			If objd_mes0100.Count > 0 Then
				'Get Picking Number Details From MES0060. If Not There Then Display Error
				Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso t.pic_status = "4").ToList

				If objd_mes0060.Count > 0 Then
					Return RedirectToAction("Create", "A1055_PayOutGetApproveCancle", d_mes0100)
				Else
					'Display Error
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
					ViewData!ID = LangResources.A1055_01_Fn_CancelConfirm_Receiving
					Return View()
				End If
			Else
				'Display Error
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1040_03_NoDatainMes0100
				ViewData!ID = LangResources.A1055_01_Fn_CancelConfirm_Receiving
				Return View()
			End If

		End Function

		' GET: A1055_PayOutGetApproveCancle/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function
		' GET: A1055_PayOutGetApproveCancle/Create
		Function Create(ByVal d_mes0100 As d_mes0100) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			ModelState.Clear()

			Dim StrPickingNo As String = d_mes0100.TxtBox_picking_no

			Dim objd_mes0100 = (From t In Db.d_mes0100 Where t.picking_no = StrPickingNo AndAlso t.del_flag <> "1" AndAlso t.plant_code = StrPlantCode Order By t.item_code).ToList

			If objd_mes0100.Count > 0 Then

				'Get Picking Number Details From MES0060. If Not There Then Display Error
				Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso t.pic_status = "4").ToList

				If objd_mes0060.Count > 0 Then

					d_mes0100.picking_no = StrPickingNo
					d_mes0100.shelfgrp_code = objd_mes0060(0).shelfgrp_code
					d_mes0100.loc_code = objd_mes0060(0).issue_loc_code
					d_mes0100.in_loc_code = objd_mes0060(0).in_loc_code

					Dim TableForA1040 As New List(Of A1040_TableInfo)

					For i As Integer = 0 To objd_mes0100.Count - 1

						Dim bolExits As Boolean = False
						For Each A1040_tb As A1040_TableInfo In TableForA1040
							If A1040_tb.TableLabelInfo = objd_mes0100(i).item_code Then
								bolExits = True
								Exit For
							End If
						Next

						If bolExits = False Then
							Dim RecordOfA1040 As New A1040_TableInfo
							RecordOfA1040.TableLabelInfo = objd_mes0100(i).item_code
							RecordOfA1040.ItemCodeFlag = "1"
							TableForA1040.Add(RecordOfA1040)
						End If

						If objd_mes0100(i).label_no IsNot Nothing Then
							Dim RecordOfA1040 As New A1040_TableInfo
							RecordOfA1040.TableLabelInfo = (objd_mes0100(i).label_no).PadRight(20)
							RecordOfA1040.ItemCodeFlag = "2"
							RecordOfA1040.TableQty = objd_mes0100(i).qty.ToString("#,##0.###")
							TableForA1040.Add(RecordOfA1040)
						End If

					Next

					d_mes0100.obj_A1040_TableInfo = TableForA1040

					'Everytime objd_mes0060 Will Update So Use Concurrency
					d_mes0100.updtdt = objd_mes0060(0).updtdt

					ViewData!ID = LangResources.A1055_01_Fn_CancelConfirm_Receiving
					Return View(d_mes0100)

				Else

					'Display Error
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
					ViewData!ID = LangResources.A1055_01_Fn_CancelConfirm_Receiving
					Return RedirectToAction("Index", "A1055_PayOutGetApproveCancle", d_mes0100)

				End If

			Else

				'Display Error
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1040_03_NoDatainMes0100
				ViewData!ID = LangResources.A1055_01_Fn_CancelConfirm_Receiving
				Return RedirectToAction("Index", "A1055_PayOutGetApproveCancle", d_mes0100)

			End If

			ViewData!ID = LangResources.A1055_01_Fn_CancelConfirm_Receiving
			Return View(d_mes0100)
		End Function

		' POST: A1055_PayOutGetApproveCancle/Create
		<HttpPost()>
		Function Create(<Bind(Include:="PLANT_CODE,PAYOUT_NO,PICKING_NO,LABEL_NO,LABEL_TYPE,ITEM_CODE,QTY,UNIT_CODE,WORK_USER,DEL_FLAG,SHELFGRP_CODE,IN_LOC_CODE,LOC_CODE,TXTBOX_PICKING_NO,TEXTBOX_LABLE_NO,A1030_ITEMNAME, INSTID, INSTDT, INSTTERM, INSTPRGNM, UPDTID, UPDTDT, UPDTTERM, UPDTPRGNM")> ByVal objd_mes0100 As d_mes0100, ByVal btnRegister As String) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			Dim StrPickingNo As String = objd_mes0100.picking_no

			'Get Picking Number Details From MES0060. If Not There Then Display Error
			Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso t.pic_status = "4").ToList

			If objd_mes0060.Count > 0 Then

				'Concurrency Error
				If objd_mes0100.updtdt.ToString("yyyy/mm/dd hh:mm:ss") <> objd_mes0060(0).updtdt.ToString("yyyy/mm/dd hh:mm:ss") Then
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_Comm_Concurrency
					ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
					Return RedirectToAction("Index", "A1055_PayOutGetApproveCancle")
				End If

			Else

				'No Data Means Concurrency
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_Comm_Concurrency
				ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
				Return RedirectToAction("Index", "A1055_PayOutGetApproveCancle")

			End If

			Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
			Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
			Dim Tras As DbContextTransaction = Db.Database.BeginTransaction

			Try

				'For Client Info
				Dim cnt = Db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

				objd_mes0060(0).pic_status = "3"

				'Save To Database
				Db.Configuration.ValidateOnSaveEnabled = False
				Db.SaveChanges()
				Db.Configuration.ValidateOnSaveEnabled = True
				Tras.Commit()

				Return RedirectToAction("Index", "A1055_PayOutGetApproveCancle")

			Catch ex As Exception

				Tras.Rollback()
				Return View()

			Finally
				Tras.Dispose()

			End Try

		End Function


		' GET: A1055_PayOutGetApproveCancle/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1055_PayOutGetApproveCancle/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1055_PayOutGetApproveCancle/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1055_PayOutGetApproveCancle/Delete/5
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