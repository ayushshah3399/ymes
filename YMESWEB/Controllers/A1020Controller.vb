Imports System.Web.Mvc
Imports System.Data.Entity
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1020Controller
		Inherits Controller

		'Create Object of model1
		Dim db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1020
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1020_01_Fn_LocateStorageBin
			Return View()
		End Function

		<HttpPost>
		Function Index(<Bind(Include:="PLANT_CODE,LABEL_NO,SLIP_NO,ITEM_CODE,LOCATION_CODE,SHELF_NO,OK_QTY,INSPECT_QTY,NG_QTY,UNIT_CODE,PRINT_DATETIME,INSPECT_LABEL_NO,DELETE_FLAG,SHELFGRP_CODE,LISTLBLNO,OBJ_A1020_LABELINFO,HIDDENSHELFNO,HIDEENLABELNO,INSTID,INSTDT,INSTTERM,INSTPRGNM,UPDTID,UPDTDT,UPDTTERM,UPDTPRGNM")> ByVal objd_mes0040 As d_mes0040, ByVal btnRegister As String, ByVal lblfakeshelf_no As String, ByVal lblfakelabel_no As String, ByVal btnbacktomenu As String) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			Dim txtShelf As String = objd_mes0040.shelf_no
			ViewData("Enable") = "True"
			Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
			Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress

			'shelf_no Button Click Then
			If btnRegister = "1" Then

				Dim objm_proc0050Cnt = (From t In db.m_proc0050 Where t.shelf_no = txtShelf AndAlso t.plant_code = StrPlantCode).ToList

				'Check if record is not exist then error
				If objm_proc0050Cnt.Count > 0 Then
					objd_mes0040.shelfgrp_code = objm_proc0050Cnt(0).shelfgrp_code
					Dim strshelfgrp_code As String = objm_proc0050Cnt(0).shelfgrp_code
					Dim objm_proc0040Cnt = (From t In db.m_proc0040 Where t.shelfgrp_code = strshelfgrp_code AndAlso t.plant_code = StrPlantCode).ToList

					'Check if record is not exist then error
					If objm_proc0040Cnt.Count > 0 Then
						objd_mes0040.location_code = objm_proc0040Cnt(0).location_code
					Else
						TempData("errortxtShelf_Empty") = LangResources.MSG_A1020_09__NoDatainPROC0040
						ViewData("Enable") = "False"
					End If
				Else
					TempData("errortxtShelf_Empty") = LangResources.MSG_A1020_10__NoDatainPROC0050
					ViewData("Enable") = "False"

				End If

				ViewData!ID = LangResources.A1020_01_Fn_LocateStorageBin
				Return View(objd_mes0040)

			End If

			'label_no Button Click Then
			If btnRegister = "2" OrElse btnRegister = "3" Then

				'Set Again GroupCode And location Code
				Dim objm_proc0050Cnt = (From t In db.m_proc0050 Where t.shelf_no = txtShelf AndAlso t.plant_code = StrPlantCode).ToList
				objd_mes0040.shelfgrp_code = objm_proc0050Cnt(0).shelfgrp_code
				Dim strshelfgrp_code As String = objm_proc0050Cnt(0).shelfgrp_code
				Dim objm_proc0040Cnt = (From t In db.m_proc0040 Where t.shelfgrp_code = strshelfgrp_code AndAlso t.plant_code = StrPlantCode).ToList
				objd_mes0040.location_code = objm_proc0040Cnt(0).location_code

				'First Time No Data in Model
				If Not objd_mes0040.label_no Is Nothing Then

					Dim Str_label_no As String = objd_mes0040.label_no
					Dim objd_mes0040Cnt = (From m In db.d_mes0040 Where m.label_no = Str_label_no AndAlso m.plant_code = StrPlantCode).ToList

					'If Label No is Not Register Then
					If objd_mes0040Cnt.Count > 0 Then
						Dim objd_Delete_mes0040Cnt = (From t In db.d_mes0040 Where t.label_no = Str_label_no AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList

						'If Record Is Deleted Then Display Record is Deleted
						If objd_Delete_mes0040Cnt.Count > 0 Then

							'If Location Code Is same Then
							If objd_Delete_mes0040Cnt(0).location_code = objd_mes0040.location_code Then

								Dim Lst_obj_A1020_Labelinfo As New List(Of A1020_Labelinfo)

								'For 1st Input Data It Will Nothing
								If Not objd_mes0040.obj_A1020_Labelinfo Is Nothing Then

									If objd_mes0040.obj_A1020_Labelinfo.Where(Function(x) x.label_no = objd_mes0040.label_no).Count > 0 Then
										TempData("errortxtLabelNo_Empty") = LangResources.MSG_A1020_02_LabelNoAlreadyexist
										ModelState.Clear()
										objd_mes0040.label_no = ""
										ViewData!ID = LangResources.A1020_01_Fn_LocateStorageBin
										Return View(objd_mes0040)
									End If

									For Each item As A1020_Labelinfo In objd_mes0040.obj_A1020_Labelinfo
										Lst_obj_A1020_Labelinfo.Add(item)
									Next
								End If

								Dim objLabelinfo As New A1020_Labelinfo
								objLabelinfo.label_no = objd_mes0040.label_no
								Lst_obj_A1020_Labelinfo.Add(objLabelinfo)

								objd_mes0040.obj_A1020_Labelinfo = Lst_obj_A1020_Labelinfo
								objLabelinfo = Nothing

							Else
								TempData("errortxtLabelNo_Empty") = LangResources.MSG_A1020_05_LocationCodenotmatched
							End If
						Else
							TempData("errortxtLabelNo_Empty") = LangResources.MSG_A1020_06_DataDeletedinMes0040
						End If

						'No Record Reated Label No
					Else
						TempData("errortxtLabelNo_Empty") = LangResources.MSG_A1020_07_NoDatainMes0040
					End If

				End If

				If TempData("errortxtLabelNo_Empty") Is Nothing Then
					ModelState.Clear()
					objd_mes0040.label_no = ""
				End If

				'Update Button Click Then
				If btnRegister = "3" AndAlso TempData("errortxtLabelNo_Empty") Is Nothing Then

					Dim ErrorlabelList As New List(Of String)

					'For 1st Input Data It Will Nothing
					If Not objd_mes0040.obj_A1020_Labelinfo Is Nothing Then

						Dim Tras As DbContextTransaction = db.Database.BeginTransaction

						Try

							'If user didnt logout then it will acotomatically login
							Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

                            Dim Count As Integer = 1
                            For Each item As A1020_Labelinfo In objd_mes0040.obj_A1020_Labelinfo
								Dim StrlblNo As String = item.label_no
								Dim objd_Update_mes0040Cnt = (From t In db.d_mes0040 Where t.label_no = StrlblNo AndAlso t.plant_code = StrPlantCode).ToList

								'if Location Code Same then Update
								If objd_Update_mes0040Cnt.Count > 0 AndAlso
									objd_Update_mes0040Cnt(0).location_code = objd_mes0040.location_code Then
									objd_Update_mes0040Cnt(0).shelf_no = txtShelf

									'2019/06/25倉庫ラベル情報に更新を行う機能一覧
									'Print Each Record 
									'Create d_work0010

									Dim Appid As String = My.Settings.Item("ApplicationID")
									Dim CompCd As String = My.Settings.Item("CompCd")

									Dim s0010 = (From t In db.s0010 Where t.appid = Appid AndAlso t.compcd = CompCd).ToList
									If s0010.Count > 0 AndAlso s0010(0).label_reprint_type = "1" Then

										Dim InsertNewRecordFord_work0010 As New d_work0010

										InsertNewRecordFord_work0010.instterm_ip = Ipaddress
										InsertNewRecordFord_work0010.label_no = StrlblNo

										Dim StrItemCd As String = objd_Update_mes0040Cnt(0).item_code
										InsertNewRecordFord_work0010.item_code = StrItemCd
										Dim objm_item0010 = (From m In db.m_item0010 Where m.item_code = StrItemCd AndAlso m.plant_code = StrPlantCode).ToList
										'Get Itemname From Item Code
										If objm_item0010.Count > 0 Then
											InsertNewRecordFord_work0010.item_name = objm_item0010(0).item_name
										End If
										InsertNewRecordFord_work0010.label_qty = objd_Update_mes0040Cnt(0).stock_qty + objd_Update_mes0040Cnt(0).inspect_qty + objd_Update_mes0040Cnt(0).keep_qty
										InsertNewRecordFord_work0010.unit_code = objd_Update_mes0040Cnt(0).unit_code

										Dim strslipno As String = objd_Update_mes0040Cnt(0).slip_no
										Dim objd_mes0150 = (From t In db.d_mes0150 Where t.slip_no = strslipno AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList
										Dim Str_Po_Sub_PO_no As String = Nothing
										If objd_mes0150.Count > 0 Then
											Str_Po_Sub_PO_no = objd_mes0150(0).po_no & "-" & objd_mes0150(0).po_sub_no
											InsertNewRecordFord_work0010.receive_date = objd_mes0150(0).receive_date
										Else
											InsertNewRecordFord_work0010.receive_date = objd_Update_mes0040Cnt(0).print_datetime
										End If

										InsertNewRecordFord_work0010.both_po_no = Str_Po_Sub_PO_no

										InsertNewRecordFord_work0010.location_code = objd_Update_mes0040Cnt(0).location_code
										InsertNewRecordFord_work0010.shelf_no = txtShelf

										Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCd AndAlso t.plant_code = StrPlantCode).ToList
										'From M0020 Master
										InsertNewRecordFord_work0010.trace_type = objm_proc0020(0).trace_type

										'If Not exists Then, Set 2: no inspection.
										'If exists Then, Set Sap0050 stock type
										If Str_Po_Sub_PO_no IsNot Nothing Then

											Dim LV_Pono = objd_mes0150(0).po_no
											Dim LV_Sub_Pono = objd_mes0150(0).po_sub_no

											'If Not exists Then, Set 2: no inspection.
											'If exists Then, Set Sap0050 stock type
											Dim objd_sap0050 = (From t In db.d_sap0050 Where t.po_no = LV_Pono AndAlso t.po_sub_no = LV_Sub_Pono AndAlso t.plant_code = StrPlantCode).ToList
											If objd_sap0050.Count > 0 Then
												InsertNewRecordFord_work0010.stock_type = objd_sap0050(0).stock_type
											Else
												InsertNewRecordFord_work0010.stock_type = "2"
											End If

										Else
											'If Not exists Then, Set 2: no inspection.
											InsertNewRecordFord_work0010.stock_type = "2"

										End If

										InsertNewRecordFord_work0010.plant_code = objd_Update_mes0040Cnt(0).plant_code

										If objd_Update_mes0040Cnt(0).stock_qty <> 0 Then
											InsertNewRecordFord_work0010.stockstts_type = "1"
										ElseIf objd_Update_mes0040Cnt(0).inspect_qty <> 0 Then
											InsertNewRecordFord_work0010.stockstts_type = "2"
										ElseIf objd_Update_mes0040Cnt(0).keep_qty <> 0 Then
											InsertNewRecordFord_work0010.stockstts_type = "3"
										Else
											InsertNewRecordFord_work0010.stockstts_type = "1"
										End If

                                        InsertNewRecordFord_work0010.seq = Count
                                        'Add To Table Object
                                        db.d_work0010.Add(InsertNewRecordFord_work0010)

									End If

								Else
									ErrorlabelList.Add(StrlblNo)
								End If

                                Count = Count + 1
                            Next

							'For Client Info
							Dim cnt = db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

							db.Configuration.ValidateOnSaveEnabled = False
							db.SaveChanges()
							db.Configuration.ValidateOnSaveEnabled = True
							Tras.Commit()

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

					Else
						'Return
						TempData("errortxtLabelNo_Empty") = LangResources.MSG_A1020_14__ListIsEmptyCannotUpdate
						ViewData!ID = LangResources.A1020_01_Fn_LocateStorageBin
						Return View(objd_mes0040)
					End If

					TempData("ErrorLabel") = ErrorlabelList
                    Return RedirectToAction("Index", "A1020", ViewBag.ErrorLabel)

                Else

					'Return if btnRegister='2'
					ViewData!ID = LangResources.A1020_01_Fn_LocateStorageBin
					Return View(objd_mes0040)

				End If

			End If

			ViewData!ID = LangResources.A1020_01_Fn_LocateStorageBin
			Return View()

		End Function

		' GET: A1020/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: A1020/Create
		Function Create() As ActionResult
			Return View()
		End Function

		' POST: A1020/Create
		<HttpPost()>
		Function Create(ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add insert logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1020/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1020/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1020/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1020/Delete/5
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