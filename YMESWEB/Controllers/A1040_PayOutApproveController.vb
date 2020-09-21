Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1040_PayOutApproveController
		Inherits Controller

		Dim Db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1040_PayOutApprove
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
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
				Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso t.pic_status = "2").ToList

				If objd_mes0060.Count > 0 Then

					Return RedirectToAction("Create", "A1040_PayOutApprove", d_mes0100)

				Else

					'Display Error
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
					ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
					Return View()

				End If

			Else

				'Display Error
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1040_03_NoDatainMes0100
				ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
				Return View()

			End If

		End Function

		' GET: A1040_PayOutApprove/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: A1040_PayOutApprove/Create
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
				Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso t.pic_status = "2").ToList

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

					ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
					Return View(d_mes0100)

				Else

					'Display Error
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
					ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
					Return RedirectToAction("Index", "A1040_PayOutApprove", d_mes0100)

				End If

			Else

				'Display Error
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1040_03_NoDatainMes0100
				ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
				Return RedirectToAction("Index", "A1040_PayOutApprove", d_mes0100)

			End If

			ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
			Return View(d_mes0100)
		End Function

		' POST: A1040_PayOutApprove/Create
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
			Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso t.pic_status = "2").ToList

			If objd_mes0060.Count > 0 Then

				'Concurrency Error
				If objd_mes0100.updtdt.ToString("yyyy/mm/dd hh:mm:ss") <> objd_mes0060(0).updtdt.ToString("yyyy/mm/dd hh:mm:ss") Then
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_Comm_Concurrency
					ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
					Return RedirectToAction("Index", "A1040_PayOutApprove")
				End If

			Else

				'No Data Means Concurrency
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_Comm_Concurrency
				ViewData!ID = LangResources.A1040_01_Fn_ConfirmSendingItem
				Return RedirectToAction("Index", "A1040_PayOutApprove")

			End If

			Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
			Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
			Dim Tras As DbContextTransaction = Db.Database.BeginTransaction

			Try

				'For Client Info
				Dim cnt = Db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

				objd_mes0060(0).pic_status = "3"

				'ただ、請求制で作成したピッキングリストで、払出して、払出承認した時に、不足リストは作らないようにしてください。
				'Means in web when doing 払出承認 at the time, If ピッキング基本情報D_MES0060.ピッキング入力区PIC_INPUT_TYPE = '5:請求制' then no need to create 不足リスト
				If objd_mes0060(0).pic_input_type <> "5" Then

					'If Picking List Order is Not Completed Then Need to Create New Picking Number
					'MES0070-->Using Picking No-->All record pic_with_status should be 2
					'If pic_with_status = 2 means Order is Not Completed.
					'So need to create new picking list

					'Get Uncompleted order data from objd_mes0070
					Dim objd_mes0070 = (From t In Db.d_mes0070 Where t.picking_no = StrPickingNo AndAlso t.pic_with_status = "1").ToList

					'If Uncompleted order record Is there then
					If objd_mes0070.Count > 0 Then

						'New one record Insert to Mes0060
						Dim InsertNewRecord As New d_mes0060

						InsertNewRecord.plant_code = objd_mes0060(0).plant_code
						Dim StrFusokuPickingNum = Db.GetNewNo(Db.Database.Connection, "PICKINGNO")
						InsertNewRecord.picking_no = StrFusokuPickingNum
						InsertNewRecord.in_loc_code = objd_mes0060(0).in_loc_code
						InsertNewRecord.issue_loc_code = objd_mes0060(0).issue_loc_code
						InsertNewRecord.workctr_code = objd_mes0060(0).workctr_code
						InsertNewRecord.shelfgrp_code = objd_mes0060(0).shelfgrp_code
						InsertNewRecord.instruction_date = objd_mes0060(0).instruction_date
						InsertNewRecord.instruction_time = objd_mes0060(0).instruction_time
						InsertNewRecord.pic_input_type = objd_mes0060(0).pic_input_type
                        InsertNewRecord.pic_type = "2"
                        InsertNewRecord.pic_status = "0"
						InsertNewRecord.print_datetime = Date.Now
						InsertNewRecord.ref_picking_no = objd_mes0060(0).picking_no
						Db.d_mes0060.Add(InsertNewRecord)

						'All record should be created in details Mes0070
						For i As Integer = 0 To objd_mes0070.Count - 1

							'3:過小完了
							objd_mes0070(i).pic_with_status = "3"

							'Create Fusoku
							Dim InsertnewrecordMes0070 As New d_mes0070

							InsertnewrecordMes0070.picking_no = StrFusokuPickingNum
							InsertnewrecordMes0070.cld_item_code = objd_mes0070(i).cld_item_code
							'Iif Because cant set Nothing Object
							'InsertnewrecordMes0070.shelf_no = IIf(objd_mes0070(i).shelf_no Is Nothing, Nothing, objd_mes0070(i).shelf_no)
							'Remaning Qty
							InsertnewrecordMes0070.qty = objd_mes0070(i).qty - IIf(objd_mes0070(i).payout_qty Is Nothing, Nothing, objd_mes0070(i).payout_qty)
							InsertnewrecordMes0070.unit_code = objd_mes0070(i).unit_code
							InsertnewrecordMes0070.pic_with_status = "1"
							InsertnewrecordMes0070.payout_qty = Nothing
							Db.d_mes0070.Add(InsertnewrecordMes0070)

						Next

					End If

				End If

				'Save To Database
				Db.Configuration.ValidateOnSaveEnabled = False
					Db.SaveChanges()
					Db.Configuration.ValidateOnSaveEnabled = True
					Tras.Commit()

					Return RedirectToAction("Index", "A1040_PayOutApprove")

			Catch ex As Exception

				Tras.Rollback()
				Return View()

			Finally
				Tras.Dispose()

			End Try

		End Function

		' GET: A1040_PayOutApprove/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1040_PayOutApprove/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1040_PayOutApprove/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1040_PayOutApprove/Delete/5
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