Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1060_ReprintItemLabelController
		Inherits Controller

		Dim Db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1060_ReprintItemLabel
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
			Return View()
		End Function

		'Post Method
		<HttpPost>
		Function Index(<Bind(Include:="PLANT_CODE,LABEL_NO,SLIP_NO,ITEM_CODE,LOCATION_CODE,SHELF_NO,OK_QTY,INSPECT_QTY,STOCK_QTY,KEEP_QTY,STR_INSPECT_QTY,STR_STOCK_QTY,STR_KEEP_QTY,NG_QTY,UNIT_CODE,PRINT_DATETIME,INSPECT_LABEL_NO,DELETE_FLAG,SHELFGRP_CODE,LISTLBLNO,OBJ_A1020_LABELINFO,HIDDENSHELFNO,HIDEENLABELNO,A1060_ITEMNAME,A1060_DISPLAYLBL_LABEL_NO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal objd_mes0040 As d_mes0040, ByVal btnRegister As String) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			'Retrive Data logic From Database 
			If btnRegister = 1 Then

				Dim StrlabelNo As String = objd_mes0040.label_no

				Dim objd_mes0040Cnt = (From t In Db.d_mes0040 Where t.label_no = StrlabelNo AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList

				If objd_mes0040Cnt.Count > 0 Then

					ModelState.Clear()
					objd_mes0040.label_no = Nothing
					objd_mes0040.A1060_DisplayLbl_label_no = StrlabelNo
					objd_mes0040.item_code = objd_mes0040Cnt(0).item_code
					Dim StrItemCode As String = objd_mes0040Cnt(0).item_code
					Dim objm_item0010 = (From t In Db.m_item0010 Where t.item_code = StrItemCode).ToList
					If objm_item0010.Count > 0 Then
						objd_mes0040.A1060_Itemname = objm_item0010(0).item_name
					End If
					objd_mes0040.location_code = objd_mes0040Cnt(0).location_code
					objd_mes0040.shelf_no = objd_mes0040Cnt(0).shelf_no
					objd_mes0040.str_stock_qty = objd_mes0040Cnt(0).stock_qty.ToString("#,##0.###")
					objd_mes0040.unit_code = objd_mes0040Cnt(0).unit_code
					objd_mes0040.str_inspect_qty = objd_mes0040Cnt(0).inspect_qty.ToString("#,##0.###")
					objd_mes0040.str_keep_qty = objd_mes0040Cnt(0).keep_qty.ToString("#,##0.###")
					'For Concurrency
					objd_mes0040.updtdt = objd_mes0040Cnt(0).updtdt

				Else

					'No Data Error
					TempData("NoDatainMes0040_A1060") = LangResources.MSG_A1030_07_NoDatainMes0040
					'Clear Data after error
					ModelState.Clear()
					objd_mes0040.label_no = Nothing
					objd_mes0040.A1060_DisplayLbl_label_no = Nothing
					objd_mes0040.item_code = Nothing
					objd_mes0040.A1060_Itemname = Nothing
					objd_mes0040.location_code = Nothing
					objd_mes0040.shelf_no = Nothing
					objd_mes0040.str_stock_qty = Nothing
					objd_mes0040.unit_code = Nothing
					objd_mes0040.str_inspect_qty = Nothing
					objd_mes0040.str_keep_qty = Nothing
					objd_mes0040.updtdt = Nothing

				End If

				ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
				Return View(objd_mes0040)

			Else

				'Display label
				Dim Strlabelno As String = objd_mes0040.A1060_DisplayLbl_label_no

				'Direct Click On Print.
				If Strlabelno Is Nothing Then

					'No Data Error
					TempData("NoDatainMes0040_A1060") = LangResources.MSG_A1020_03_LblEmptyLabelNo
					ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
					Return View(objd_mes0040)

				End If

				Dim objd_mes0040Cnt = (From t In Db.d_mes0040 Where t.label_no = Strlabelno AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList

				'check Concurrency
				If objd_mes0040Cnt.Count > 0 Then

					If objd_mes0040.updtdt.ToString("yyyy/mm/dd hh:mm:ss") <> objd_mes0040Cnt(0).updtdt.ToString("yyyy/mm/dd hh:mm:ss") Then

						'Concurrency Error
						TempData("NoDatainMes0040_A1060") = LangResources.MSG_Comm_Concurrency
						ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
						Return RedirectToAction("Index", "A1060_ReprintItemLabel")

					End If

				Else

					'No Data Means Concurrency Error
					TempData("NoDatainMes0040_A1060") = LangResources.MSG_Comm_Concurrency
					ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
					Return RedirectToAction("Index", "A1060_ReprintItemLabel")

				End If

				'This is for SetClientInfo
				Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
				Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
				Dim Tras As DbContextTransaction = Db.Database.BeginTransaction

				Try

					'Insert Logic
					'Create d_work0010
					'Condition For Create d_work0010
					Dim InsertNewRecordFord_work0010 As New d_work0010

					'Get Ip
					Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

					If objd_mes0040Cnt.Count > 0 Then

						InsertNewRecordFord_work0010.instterm_ip = Ipaddress
						InsertNewRecordFord_work0010.label_no = Strlabelno

						Dim StrItemCd As String = objd_mes0040.item_code
						InsertNewRecordFord_work0010.item_code = StrItemCd
						InsertNewRecordFord_work0010.item_name = objd_mes0040.A1060_Itemname
						InsertNewRecordFord_work0010.label_qty = objd_mes0040Cnt(0).stock_qty + objd_mes0040Cnt(0).inspect_qty + objd_mes0040Cnt(0).keep_qty
						InsertNewRecordFord_work0010.unit_code = objd_mes0040.unit_code

						Dim strslipno As String = objd_mes0040Cnt(0).slip_no
						Dim objd_mes0150 = (From t In Db.d_mes0150 Where t.slip_no = strslipno AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList
						Dim Str_Po_Sub_PO_no As String = Nothing
						If objd_mes0150.Count > 0 Then
							Str_Po_Sub_PO_no = objd_mes0150(0).po_no & "-" & objd_mes0150(0).po_sub_no
							InsertNewRecordFord_work0010.receive_date = objd_mes0150(0).receive_date
						Else
							InsertNewRecordFord_work0010.receive_date = objd_mes0040Cnt(0).print_datetime
						End If

						InsertNewRecordFord_work0010.both_po_no = Str_Po_Sub_PO_no

						InsertNewRecordFord_work0010.location_code = objd_mes0040.location_code
						InsertNewRecordFord_work0010.shelf_no = objd_mes0040Cnt(0).shelf_no

						Dim objm_proc0020 = (From t In Db.m_item0020 Where t.item_code = StrItemCd AndAlso t.plant_code = StrPlantCode).ToList
						'From M0020 Master
						InsertNewRecordFord_work0010.trace_type = objm_proc0020(0).trace_type

						'If Not exists Then, Set 2: no inspection.
						'If exists Then, Set Sap0050 stock type
						If Str_Po_Sub_PO_no IsNot Nothing Then

							Dim LV_Pono = objd_mes0150(0).po_no
							Dim LV_Sub_Pono = objd_mes0150(0).po_sub_no

							'If Not exists Then, Set 2: no inspection.
							'If exists Then, Set Sap0050 stock type
							Dim objd_sap0050 = (From t In Db.d_sap0050 Where t.po_no = LV_Pono AndAlso t.po_sub_no = LV_Sub_Pono AndAlso t.plant_code = StrPlantCode).ToList
							If objd_sap0050.Count > 0 Then
								InsertNewRecordFord_work0010.stock_type = objd_sap0050(0).stock_type
							Else
								InsertNewRecordFord_work0010.stock_type = "2"
							End If

						Else
							'If Not exists Then, Set 2: no inspection.
							InsertNewRecordFord_work0010.stock_type = "2"

						End If

						InsertNewRecordFord_work0010.plant_code = objd_mes0040Cnt(0).plant_code

						If objd_mes0040Cnt(0).stock_qty <> 0 Then
							InsertNewRecordFord_work0010.stockstts_type = "1"
						ElseIf objd_mes0040Cnt(0).inspect_qty <> 0 Then
							InsertNewRecordFord_work0010.stockstts_type = "2"
						ElseIf objd_mes0040Cnt(0).keep_qty <> 0 Then
							InsertNewRecordFord_work0010.stockstts_type = "3"
						Else
							InsertNewRecordFord_work0010.stockstts_type = "1"
						End If

						'Add To Table Object
						Db.d_work0010.Add(InsertNewRecordFord_work0010)

						'For Client Info
						Dim cnt = Db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

						'Save To Database
						Db.Configuration.ValidateOnSaveEnabled = False
						Db.SaveChanges()
						Db.Configuration.ValidateOnSaveEnabled = True
						Tras.Commit()

						Return RedirectToAction("Index", "A1060_ReprintItemLabel")

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

					ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
					Return View()

				Finally
					Tras.Dispose()

				End Try

			End If

			ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
			Return View(objd_mes0040)

		End Function

		' GET: A1060_ReprintItemLabel/Details/5
		Function Details() As ActionResult
			ViewData!ID = LangResources.A1060_01_Fn_RePrint_ID_tag
			Return View()
		End Function

		' GET: A1060_ReprintItemLabel/Create
		Function Create() As ActionResult
			Return View()
		End Function

		' POST: A1060_ReprintItemLabel/Create
		<HttpPost()>
		Function Create(ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add insert logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1060_ReprintItemLabel/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1060_ReprintItemLabel/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1060_ReprintItemLabel/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1060_ReprintItemLabel/Delete/5
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