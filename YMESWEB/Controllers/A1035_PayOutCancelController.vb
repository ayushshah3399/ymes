Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1035_PayOutCancelController
		Inherits Controller

		Dim db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1035_PayOutCancel
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1035_01_CancelSendItembyPL
			Return View()
		End Function

		'Post
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

			'Get Picking Number Details From MES0060. If Not There Then Display Error
			Dim objd_mes0100 = (From t In db.d_mes0100 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode).ToList

			If objd_mes0100.Count > 0 Then

				'Get Picking Number Details From MES0060. If Not There Then Display Error
				Dim objd_mes0060 = (From t In db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso t.pic_status = "2").ToList

				If objd_mes0060.Count > 0 Then

					d_mes0100.picking_no = StrPickingNo
					d_mes0100.shelfgrp_code = objd_mes0060(0).shelfgrp_code
					d_mes0100.loc_code = objd_mes0060(0).issue_loc_code
					d_mes0100.in_loc_code = objd_mes0060(0).in_loc_code

					Return RedirectToAction("Create", "A1035_PayOutCancel", d_mes0100)

                Else

					'Display Error
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1035_02_NoDatainMes0100
					ViewData!ID = LangResources.A1035_01_CancelSendItembyPL
					Return View()

				End If

			Else

				TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1035_02_NoDatainMes0100
				ViewData!ID = LangResources.A1035_01_CancelSendItembyPL
				Return View()

			End If

		End Function

		' GET: A1035_PayOutCancel/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: A1035_PayOutCancel/Create
		Function Create(ByVal d_mes0100 As d_mes0100) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1035_01_CancelSendItembyPL
			Return View(d_mes0100)
		End Function

		' POST: A1030_PayOutInput/Create
		<HttpPost()>
		Function Create(<Bind(Include:="PLANT_CODE,PAYOUT_NO,PICKING_NO,LABEL_NO,LABEL_TYPE,ITEM_CODE,QTY,STR_QTY,UNIT_CODE,WORK_USER,DEL_FLAG,SHELFGRP_CODE,IN_LOC_CODE,LOC_CODE,TXTBOX_PICKING_NO,TEXTBOX_LABLE_NO,A1030_ITEMNAME, INSTID, INSTDT, INSTTERM, INSTPRGNM, UPDTID, UPDTDT, UPDTTERM, UPDTPRGNM")> ByVal objd_mes0100 As d_mes0100, ByVal btnRegister As String) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			Dim Strinspect_label_no As String = ""
			Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
			Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
			Dim StrpickingNo As String = objd_mes0100.picking_no

			'Lable No Click Event
			If btnRegister = 1 Then
				Dim StrInputlabelno As String = objd_mes0100.TextBox_lable_no
				'To Display Updated Value On The View
				ModelState.Clear()

				'Get Data From MES0030 And Get Delete Flag 0 Record only 
				Dim obj_d_mes0100 = (From t In db.d_mes0100 Where t.picking_no = StrpickingNo AndAlso t.label_no = StrInputlabelno AndAlso t.del_flag = 0 AndAlso t.plant_code = StrPlantCode).ToList

				If obj_d_mes0100.Count > 0 Then

					Dim StrItemCode As String = obj_d_mes0100(0).item_code

					'Set label N0
					objd_mes0100.label_no = StrInputlabelno
					objd_mes0100.item_code = StrItemCode

					'ItemName From Master
					Dim objm_item0010 = (From t In db.m_item0010 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList

					If objm_item0010.Count > 0 Then
						objd_mes0100.A1030_Itemname = objm_item0010(0).item_name
					End If

					'get Sum of Qty
					'Delete all records 
					Dim Sum_obj_d_mes0100 = (From t In db.d_mes0100 Where t.picking_no = StrpickingNo AndAlso t.label_no = StrInputlabelno AndAlso t.del_flag = 0 AndAlso t.plant_code = StrPlantCode).Sum(Function(ByVal t) t.qty)

					'Set Qty From Master
					objd_mes0100.str_qty = Sum_obj_d_mes0100.ToString("#,##0.###")
					objd_mes0100.unit_code = obj_d_mes0100(0).unit_code
					objd_mes0100.TextBox_lable_no = Nothing

					'Everytime MES0070 Will Update So Use Concurrency
					objd_mes0100.updtdt = obj_d_mes0100(0).updtdt

				Else

					'Display Error Record Not Found
					TempData("NoDataMes") = LangResources.MSG_A1035_02_NoDatainMes0100

				End If

				ViewData!ID = LangResources.A1035_01_CancelSendItembyPL
				Return View(objd_mes0100)

				'Click Register
			Else

				Dim StrInputlabelno As String = objd_mes0100.label_no

				'Get Data From obj_d_mes0100
				Dim obj_d_mes0100 = (From t In db.d_mes0100 Where t.picking_no = StrpickingNo AndAlso t.label_no = StrInputlabelno AndAlso t.del_flag = 0 AndAlso t.plant_code = StrPlantCode).ToList

				'Get Label Type
				Dim Strlbltype As String = ""
				If obj_d_mes0100.Count > 0 Then

					'Concurrency Error
					If objd_mes0100.updtdt.ToString("yyyy/mm/dd hh:mm:ss") <> obj_d_mes0100(0).updtdt.ToString("yyyy/mm/dd hh:mm:ss") Then
						TempData("errorPicking_NODataNotFound") = LangResources.MSG_Comm_Concurrency
						ViewData!ID = LangResources.A1035_01_CancelSendItembyPL
						objd_mes0100.label_no = Nothing
						objd_mes0100.item_code = Nothing
						objd_mes0100.A1030_Itemname = Nothing
						objd_mes0100.str_qty = Nothing
						objd_mes0100.unit_code = Nothing
						Return RedirectToAction("Index", "A1035_PayOutCancel")
					End If

					Strlbltype = obj_d_mes0100(0).label_type
				Else
					'Concurrency
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_Comm_Concurrency
					Return RedirectToAction("Index", "A1035_PayOutCancel")
				End If

                '2019/11/14 Closing Date Check
                '"実績日はどこから？ピッキング払出実績情報のINSTDT?"
                Dim DateFormat_Original As String = Session("DateFormat_Original")
                'Check Closing Date Status
                Dim check_closing_date = db.fn_check_closing_date(obj_d_mes0100(0).instdt.ToString(DateFormat_Original), DateFormat_Original)
                If check_closing_date = False Then
                    TempData("CheckBeforeupdate") = LangResources.MSG_Comm_CheckClosingDate
                    ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                    Return View(objd_mes0100)
                End If

                Dim ConverfromDecimal As Decimal = Decimal.Parse(objd_mes0100.str_qty)

                Dim Tras As DbContextTransaction = db.Database.BeginTransaction

				Try

					'For Client Info
					Dim cnt = db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

					'When Kbn Is 2
					If Strlbltype = "2" Then

						Dim objd_mes0040 = (From t In db.d_mes0040 Where t.label_no = StrInputlabelno AndAlso t.plant_code = StrPlantCode).ToList

						'Update Qty In Mes0040
						objd_mes0040(0).stock_qty = objd_mes0040(0).stock_qty + ConverfromDecimal

						'Update Qty In MES0100
						For i As Integer = 0 To obj_d_mes0100.Count - 1
							obj_d_mes0100(i).del_flag = "1"
						Next

						'Minus Payout Qty From From MES0070
						Dim objd_mes0070 = (From t In db.d_mes0070 Where t.picking_no = StrpickingNo AndAlso t.cld_item_code = objd_mes0100.item_code).ToList

						Dim decCur_payout_qty As Decimal = 0
						Decimal.TryParse(objd_mes0070(0).payout_qty, decCur_payout_qty)
						objd_mes0070(0).payout_qty = decCur_payout_qty - ConverfromDecimal

						If objd_mes0070(0).pic_with_status = "2" AndAlso objd_mes0070(0).payout_qty < objd_mes0070(0).qty Then
							objd_mes0070(0).pic_with_status = "1"
						End If

						'If all order is cancled then need to change Kbn in mes0060
						't.lable_no <> StrInputlabelno
						'Becuse obj_d_mes0100(i).del_flag = "1" will not Affect to database
						Dim objCheckPartiallyCompleted_d_mes0100 = (From t In db.d_mes0100 Where t.picking_no = StrpickingNo AndAlso t.label_no <> StrInputlabelno AndAlso t.del_flag = 0 AndAlso t.plant_code = StrPlantCode).ToList
						If objCheckPartiallyCompleted_d_mes0100.Count = 0 Then
							'Get Picking Number Details From MES0060. If Not There Then Display Error
							'no order means No Partially Completed.
							Dim objd_mes0060 = (From t In db.d_mes0060 Where t.picking_no = StrpickingNo AndAlso t.plant_code = StrPlantCode).ToList
							objd_mes0060(0).pic_status = "1"
						End If

						'Save To Database
						db.Configuration.ValidateOnSaveEnabled = False
						db.SaveChanges()
						db.Configuration.ValidateOnSaveEnabled = True
						Tras.Commit()

					Else

						Return View()

					End If

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

			End If

			Return RedirectToAction("Index", "A1035_PayOutCancel")

		End Function

		' GET: A1035_PayOutCancel/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1035_PayOutCancel/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1035_PayOutCancel/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1035_PayOutCancel/Delete/5
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
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

	End Class
End Namespace