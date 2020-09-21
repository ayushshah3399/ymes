Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1030_PayOutInputController
		Inherits Controller

		Dim Db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1030_PayOutInput
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
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
			Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = StrPickingNo AndAlso t.plant_code = StrPlantCode AndAlso (t.pic_status = "0" OrElse t.pic_status = "1" OrElse t.pic_status = "2")).ToList

			If objd_mes0060.Count > 0 Then

				Dim issue_loc_code = objd_mes0060(0).issue_loc_code
				'Get Picking Number Details From MES0060. If Not There Then Display Error
				Dim objm_proc0030 = (From t In Db.m_proc0030 Where t.location_code = issue_loc_code AndAlso t.plant_code = StrPlantCode).ToList

				If objm_proc0030.Count > 0 Then

					Dim Strlocationtype As String = objm_proc0030(0).location_type

					If Strlocationtype <> "2" Then

						'Display Error
						TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_16_NoSokodata
						ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
						Return View()

					End If

					d_mes0100.picking_no = StrPickingNo
					d_mes0100.shelfgrp_code = objd_mes0060(0).shelfgrp_code
					d_mes0100.loc_code = issue_loc_code
					d_mes0100.in_loc_code = objd_mes0060(0).in_loc_code
					Return RedirectToAction("Create", "A1030_PayOutInput", d_mes0100)

				Else

					'Display Error
					TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_17_LocationCodeNotRegister
					ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
					Return View()

				End If

			Else
				'Display Error
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
				ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
				Return View()
			End If

		End Function

		' GET: A1030_PayOutInput/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: A1030_PayOutInput/Create
		Function Create(ByVal d_mes0100 As d_mes0100) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
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
			'Get Picking Number Details From MES0060. If Not There Then Display Error
			Dim objm_proc0030 = (From t In Db.m_proc0030 Where t.location_code = objd_mes0100.loc_code AndAlso t.plant_code = StrPlantCode).ToList

			Dim Strlocationtype As String = ""

			If objm_proc0030.Count > 0 Then
				Strlocationtype = objm_proc0030(0).location_type
			Else
				'Display Error
				TempData("errorPicking_NODataNotFound") = LangResources.MSG_A1030_17_LocationCodeNotRegister
				ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
				Return RedirectToAction("Index", "A1030_PayOutInput")
			End If

			ModelState.Clear()
			Dim Cnvrttodecimal As Decimal = Decimal.Parse(IIf(objd_mes0100.str_qty Is Nothing, 0, objd_mes0100.str_qty))
			Dim Errorflag As Boolean = False

			'For Insert into Worktable for label Print
			Dim Str_Strplantcode As String = Nothing
			Dim Str_Shelfcode As String = Nothing
			Dim Str_SlipNo As String = Nothing

			'Click label No 
			If btnRegister = 1 OrElse btnRegister = 2 Then

				Dim StrInputlabelno As String = objd_mes0100.TextBox_lable_no
				Dim StrpickingNo As String = objd_mes0100.picking_no

				'btnRegister = 1
				If Not StrInputlabelno Is Nothing Then

					If Strlocationtype = "2" Then

						'When Kbn Is 2 takes from MES0040
						'Get Data From Mes0040
						Dim dt_objd_mes0040 = (From t In Db.d_mes0040 Where t.label_no = StrInputlabelno AndAlso t.plant_code = StrPlantCode).ToList

						If dt_objd_mes0040.Count > 0 Then

							'Get Data From Mes0040 notDeleted
							Dim objd_mes0040 = (From t In dt_objd_mes0040 Where t.label_no = StrInputlabelno AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList

							If objd_mes0040.Count > 0 Then

								'Check For One Picking List Should Contains One label
								Dim objd_d_mes0100 = (From t In Db.d_mes0100 Where t.picking_no = StrpickingNo AndAlso t.label_no = StrInputlabelno AndAlso t.del_flag = "0" AndAlso t.plant_code = StrPlantCode).ToList

								If objd_d_mes0100.Count > 0 Then

									'Check For One Picking List Should Contains One label
									TempData("NoDataMes") = LangResources.MSG_A1030_19_LabelIsAlready
									ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
									objd_mes0100.label_no = Nothing
									objd_mes0100.item_code = Nothing
									objd_mes0100.A1030_Itemname = Nothing
									objd_mes0100.str_qty = Nothing
									objd_mes0100.unit_code = Nothing
									Errorflag = True
									Return View(objd_mes0100)

								End If

								Dim StrItemCode As String = objd_mes0040(0).item_code
								Dim objd_mes0070 = (From t In Db.d_mes0070 Where t.picking_no = StrpickingNo AndAlso t.cld_item_code = StrItemCode).ToList

								If objd_mes0070.Count > 0 Then

									Dim CheckCount = (From t In Db.d_mes0100 Where t.picking_no = StrpickingNo AndAlso t.item_code = StrItemCode AndAlso t.del_flag = "0" AndAlso t.plant_code = StrPlantCode).ToList

									Dim SumOfRecivedQty As Decimal = 0
									'Neeed To Put Condition
									'If No records Then It Become Error So Need to Put this Condition
									If CheckCount.Count > 0 Then
										SumOfRecivedQty = (From t In Db.d_mes0100 Where t.picking_no = StrpickingNo AndAlso t.item_code = StrItemCode AndAlso t.del_flag = "0" AndAlso t.plant_code = StrPlantCode).Sum(Function(F) F.qty)
									End If

									Dim StrMes0070Qty As Decimal = 0
									Dim StrMes0040Qty As Decimal = 0
									Dim DecFinalMes0100InsertQty As Decimal = 0

									'Fill Qty From Mes0040
									StrMes0040Qty = objd_mes0040(0).stock_qty

									'Fill Qty From Mes0070
									If objd_mes0070.Count > 0 Then

										'Remaining Picking Qty 
										StrMes0070Qty = objd_mes0070(0).qty - SumOfRecivedQty

										'Check Picking Complete or not for inputed item
										'2019/08/05 
										'transfer qty > picking qty then error right?
										'plz remove this check.
										'If StrMes0070Qty <= 0 Then
										'	TempData("NoDataMes") = LangResources.MSG_A1030_02_OrderCompleted
										'	ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
										'	objd_mes0100.label_no = Nothing
										'	objd_mes0100.item_code = Nothing
										'	objd_mes0100.A1030_Itemname = Nothing
										'	objd_mes0100.str_qty = Nothing
										'	objd_mes0100.unit_code = Nothing
										'	Errorflag = True
										'	Return View(objd_mes0100)
										'End If

										'check label's stock qty
										If objd_mes0040(0).stock_qty <= 0 Then
											TempData("NoDataMes") = LangResources.MSG_A1030_15_LabelStockQtyZero
											ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
											objd_mes0100.label_no = Nothing
											objd_mes0100.item_code = Nothing
											objd_mes0100.A1030_Itemname = Nothing
											objd_mes0100.str_qty = Nothing
											objd_mes0100.unit_code = Nothing
											Errorflag = True
											Return View(objd_mes0100)
										End If

										If StrMes0040Qty > StrMes0070Qty Then
											DecFinalMes0100InsertQty = StrMes0070Qty
										Else
											DecFinalMes0100InsertQty = StrMes0040Qty
										End If

										'Everytime MES0070 Will Update So Use Concurrency
										objd_mes0100.updtdt = objd_mes0070(0).updtdt

									Else
										'No Related Record In Mes0070
										'Accorind To Spec No Need For This Condition because Now Onward if No Record in MES0070 Then Error Message Will Display
										DecFinalMes0100InsertQty = StrMes0040Qty
									End If

									'check same location or not
									If objd_mes0040(0).location_code <> objd_mes0100.loc_code Then
										TempData("NoDataMes") = LangResources.MSG_A1030_14_LocCodeDifferent
										ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
										objd_mes0100.label_no = Nothing
										objd_mes0100.item_code = Nothing
										objd_mes0100.A1030_Itemname = Nothing
										objd_mes0100.str_qty = Nothing
										objd_mes0100.unit_code = Nothing
										Errorflag = True
										Return View(objd_mes0100)
									End If

									'Set label N0
									objd_mes0100.label_no = StrInputlabelno
									objd_mes0100.item_code = StrItemCode

									Dim objm_item0010 = (From t In Db.m_item0010 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList

									If objm_item0010.Count > 0 Then
										objd_mes0100.A1030_Itemname = objm_item0010(0).item_name
									End If

									'When  Qty < - Then
									If StrMes0070Qty <= 0 Then
										objd_mes0100.str_qty = Nothing
									Else
										objd_mes0100.str_qty = DecFinalMes0100InsertQty.ToString("#,##0.###")
									End If

									'objd_mes0100.A1030_Hidden_Qty = DecFinalMes0100InsertQty
									objd_mes0100.A1030_Hidden_Qty = StrMes0040Qty
									objd_mes0100.unit_code = objd_mes0040(0).unit_code
									objd_mes0100.updtdt = objd_mes0070(0).updtdt
									objd_mes0100.TextBox_lable_no = Nothing

								Else

									'Display Error Record Not Found Not In MES0070
									TempData("NoDataMes") = LangResources.MSG_A1030_06_NoDatainMes0070
									objd_mes0100.label_no = Nothing
									objd_mes0100.item_code = Nothing
									objd_mes0100.A1030_Itemname = Nothing
									objd_mes0100.str_qty = Nothing
									objd_mes0100.unit_code = Nothing
									Errorflag = True

								End If

							Else

								'Error record is Deleted
								TempData("NoDataMes") = LangResources.MSG_A1030_16_DataDeletedinMes0040
								objd_mes0100.label_no = Nothing
								objd_mes0100.item_code = Nothing
								objd_mes0100.A1030_Itemname = Nothing
								objd_mes0100.str_qty = Nothing
								objd_mes0100.unit_code = Nothing
								Errorflag = True

							End If

						Else

							'Error Data Not Exist
							TempData("NoDataMes") = LangResources.MSG_A1030_07_NoDatainMes0040
							objd_mes0100.label_no = Nothing
							objd_mes0100.item_code = Nothing
							objd_mes0100.A1030_Itemname = Nothing
							objd_mes0100.str_qty = Nothing
							objd_mes0100.unit_code = Nothing
							Errorflag = True

						End If

					Else

						ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
						Return View(objd_mes0100)

					End If

				End If

				'btnRegister = 2 Then
				'Update Logc Start From Here
				If btnRegister = 1 OrElse Errorflag = True Then

					ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
					Return View(objd_mes0100)

					'Update Logic
				Else

					'For concorrency Mes0070 Updtdt will Use.
					'If Database Error Is There Then It Will Display
					If ModelState.IsValid = False Then
						Return View(objd_mes0100)
					End If

					'Check Concurrency
					Dim StrItemCode As String = objd_mes0100.item_code
					Dim Updt_StrPickingNo As String = objd_mes0100.picking_no
					Dim objd_mes0070 = (From t In Db.d_mes0070 Where t.picking_no = Updt_StrPickingNo AndAlso t.cld_item_code = StrItemCode).ToList

					'Concurrency Error
					If objd_mes0100.updtdt.ToString("yyyy/mm/dd hh:mm:ss") <> objd_mes0070(0).updtdt.ToString("yyyy/mm/dd hh:mm:ss") Then
						TempData("errorPicking_NODataNotFound") = LangResources.MSG_Comm_Concurrency
						ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
						objd_mes0100.label_no = Nothing
						objd_mes0100.item_code = Nothing
						objd_mes0100.A1030_Itemname = Nothing
						objd_mes0100.str_qty = Nothing
						objd_mes0100.unit_code = Nothing
						Return RedirectToAction("Index", "A1030_PayOutInput")
					End If

                    '2019/11/14 Closing Date Check
                    Dim DateFormat_Original As String = Session("DateFormat_Original")
                    'Check Closing Date Status
                    Dim check_closing_date = Db.fn_check_closing_date(Date.Now.ToString(DateFormat_Original), DateFormat_Original)
                    If check_closing_date = False Then
                        TempData("CheckBeforeupdate") = LangResources.MSG_Comm_CheckClosingDate
                        ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                        Return View(objd_mes0100)
                    End If

                    Dim Updt_StrInputlabelno As String = objd_mes0100.label_no

                    Dim Tras As DbContextTransaction = Db.Database.BeginTransaction

					Try

						'For Client Info
						Dim cnt = Db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

						'When Kbn Is 2 
						If Strlocationtype = "2" Then

							Dim dt_objd_mes0040 = (From t In Db.d_mes0040 Where t.label_no = Updt_StrInputlabelno AndAlso t.plant_code = StrPlantCode).ToList

							If dt_objd_mes0040.Count > 0 Then

								'Get Data From Mes0040 notDeleted
								Dim objd_mes0040 = (From t In dt_objd_mes0040 Where t.label_no = Updt_StrInputlabelno AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList

								If objd_mes0040.Count > 0 Then

									Dim InsertNewRecord As New d_mes0100

									InsertNewRecord.plant_code = objd_mes0040(0).plant_code
									InsertNewRecord.payout_no = Db.GetNewNo(Db.Database.Connection, "PAYOUT_NO")
									InsertNewRecord.picking_no = objd_mes0100.picking_no
									InsertNewRecord.label_no = Updt_StrInputlabelno
									InsertNewRecord.label_type = Strlocationtype
									InsertNewRecord.item_code = objd_mes0100.item_code

									'When User Enter Label No and Direct Click On Register Button
									'At That Time Cnvrttodecimal Become 0
									'The Reason Of Taking A1030_Hidden_Qty is
									'A1030_Hidden_Qty is Decimal type
									'If I take str_qty Then Need to Convert So i Take Hidden
									If Cnvrttodecimal = 0 Then
										'Cnvrttodecimal = objd_mes0100.A1030_Hidden_Qty
										Cnvrttodecimal = Convert.ToDecimal(objd_mes0100.str_qty)
									End If

									InsertNewRecord.qty = Cnvrttodecimal
									InsertNewRecord.unit_code = objd_mes0100.unit_code
									InsertNewRecord.work_user = Session("LoginUserid")
									InsertNewRecord.del_flag = "0"

									Db.d_mes0100.Add(InsertNewRecord)

									'For Insert into Work0010
									Str_Shelfcode = objd_mes0040(0).shelf_no
									Str_Strplantcode = objd_mes0040(0).plant_code

								Else

									'Deleted Record
									TempData("NoDataMes") = LangResources.MSG_A1030_16_DataDeletedinMes0040
									objd_mes0100.label_no = Nothing
									objd_mes0100.item_code = Nothing
									objd_mes0100.A1030_Itemname = Nothing
									objd_mes0100.str_qty = Nothing
									objd_mes0100.unit_code = Nothing
									ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
									Return View(objd_mes0100)

								End If

								'After This Need to Upadate Common MES0060,MES0070
								'下Coding

							Else
								'Error Already Check So No Need To Check 
								ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
								Return View(objd_mes0100)

							End If

						Else

							'Error Already Check So No Need To Check 
							ViewData!ID = LangResources.A1030_01_Fn_SendItembyPickingList
							Return View(objd_mes0100)

						End If

						'上のCoding
						'Common Logic For Update

						'Get Picking Number Details From MES0060. If Not There Then Display Error
						Dim objd_mes0060 = (From t In Db.d_mes0060 Where t.picking_no = Updt_StrPickingNo AndAlso t.plant_code = StrPlantCode).ToList

						If objd_mes0060.Count > 0 AndAlso (objd_mes0060(0).pic_status = "0" OrElse objd_mes0060(0).pic_status = "1") Then

							objd_mes0060(0).pic_status = "2"

						End If

						'Upadte Targer Table is HeaderInfo MES0070
						'input qty
						Dim payoutqty = Cnvrttodecimal

						If Not objd_mes0070(0).payout_qty Is Nothing Then
							payoutqty = objd_mes0070(0).payout_qty + payoutqty
						End If

						' Add InPut Qty + Mes0070 Qty
						objd_mes0070(0).payout_qty = payoutqty

						'This objd_mes0070(0).qty Is From table
						If objd_mes0070.Count > 0 AndAlso (objd_mes0070(0).qty <= payoutqty AndAlso objd_mes0070(0).pic_with_status = "1") Then
							objd_mes0070(0).pic_with_status = "2"
						End If

						'Print Label 
						If Strlocationtype = "2" Then

                            Dim Count As Integer = 1

                            Dim objd_mes0040 = (From t In Db.d_mes0040 Where t.label_no = Updt_StrInputlabelno AndAlso t.plant_code = StrPlantCode).ToList
                            Dim Date_recievedate As Date = objd_mes0040(0).print_datetime

                            If objd_mes0040.Count > 0 And Cnvrttodecimal < objd_mes0040(0).stock_qty Then

								Dim objm_item0020 = (From t In Db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList

								If (objm_item0020.Count > 0 AndAlso objm_item0020(0).label_prt_type <> 0) OrElse objm_item0020.Count = 0 Then

									'If user didnt logout then it will acotomatically login
									Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

									'Create d_work0010
									'Condition For Create d_work0010
									Dim InsertNewRecordFord_work0010 As New d_work0010

									InsertNewRecordFord_work0010.instterm_ip = Ipaddress
									InsertNewRecordFord_work0010.label_no = objd_mes0100.label_no
									InsertNewRecordFord_work0010.item_code = objd_mes0100.item_code
									InsertNewRecordFord_work0010.item_name = objd_mes0100.A1030_Itemname
									InsertNewRecordFord_work0010.label_qty = Cnvrttodecimal
									InsertNewRecordFord_work0010.unit_code = objd_mes0100.unit_code

                                    Str_SlipNo = objd_mes0040(0).slip_no

                                    'Get Data  From Mes0150
                                    Dim objd_mes0150 = (From t In Db.d_mes0150 Where t.slip_no = Str_SlipNo AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList
									Dim Str_Po_Sub_PO_no As String = Nothing
									If objd_mes0150.Count > 0 Then
                                        Str_Po_Sub_PO_no = objd_mes0150(0).po_no & "-" & objd_mes0150(0).po_sub_no
                                        Date_recievedate = objd_mes0150(0).receive_date
                                    End If

									InsertNewRecordFord_work0010.both_po_no = Str_Po_Sub_PO_no
                                    InsertNewRecordFord_work0010.receive_date = Date_recievedate
                                    InsertNewRecordFord_work0010.location_code = objd_mes0100.loc_code

                                    'Take Shelf Code from MEs0040
                                    InsertNewRecordFord_work0010.shelf_no = Str_Shelfcode

									Dim objm_proc0020 = (From t In Db.m_item0020 Where t.item_code = StrItemCode).ToList
									'From M0020 Master, トレース区分	TRACE_TYPE 1:対象 2:非対象
									If objm_proc0020.Count > 0 Then
										InsertNewRecordFord_work0010.trace_type = objm_proc0020(0).trace_type
									Else
										InsertNewRecordFord_work0010.trace_type = "1"
									End If

									InsertNewRecordFord_work0010.stock_type = "1"
									InsertNewRecordFord_work0010.plant_code = Str_Strplantcode

                                    InsertNewRecordFord_work0010.stockstts_type = "1"

                                    InsertNewRecordFord_work0010.seq = Count
                                    Count = Count + 1

                                    'Add To Table Object
                                    Db.d_work0010.Add(InsertNewRecordFord_work0010)

								End If

							End If

							'Update Qty In Mes0040
							objd_mes0040(0).stock_qty = objd_mes0040(0).stock_qty - Cnvrttodecimal

							'対応方法：	システム情報マスタに発行フラグ項目を追加。
							'H列で「○」のところは、システム情報の発行フラグによって発行する/しないになる。
							'「☓」は、フラグと関係なく、現状のまま。
							Dim Appid As String = My.Settings.Item("ApplicationID")
							Dim CompCd As String = My.Settings.Item("CompCd")

							Dim s0010 = (From t In Db.s0010 Where t.appid = Appid AndAlso t.compcd = CompCd).ToList
							If s0010.Count > 0 AndAlso s0010(0).label_reprint_type = "1" Then

								'If qty is not Zero Then Print
								If (objd_mes0040(0).stock_qty) <> 0 Then

									'If user didnt logout then it will acotomatically login
									Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

									'Create d_work0010
									'Condition For Create d_work0010
									Dim InsertNewRecordFord_work0010 As New d_work0010

									InsertNewRecordFord_work0010.instterm_ip = Ipaddress
									InsertNewRecordFord_work0010.label_no = objd_mes0100.label_no
									InsertNewRecordFord_work0010.item_code = objd_mes0100.item_code
									InsertNewRecordFord_work0010.item_name = objd_mes0100.A1030_Itemname
									InsertNewRecordFord_work0010.label_qty = objd_mes0040(0).stock_qty
									InsertNewRecordFord_work0010.unit_code = objd_mes0100.unit_code

                                    Str_SlipNo = objd_mes0040(0).slip_no

                                    'Get Data  From Mes0150
                                    Dim objd_mes0150 = (From t In Db.d_mes0150 Where t.slip_no = Str_SlipNo AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList
									Dim Str_Po_Sub_PO_no As String = Nothing
									If objd_mes0150.Count > 0 Then
										Str_Po_Sub_PO_no = objd_mes0150(0).po_no & "-" & objd_mes0150(0).po_sub_no
                                        Date_recievedate = objd_mes0150(0).receive_date
                                    End If

                                    InsertNewRecordFord_work0010.both_po_no = Str_Po_Sub_PO_no
                                    InsertNewRecordFord_work0010.receive_date = Date_recievedate
                                    InsertNewRecordFord_work0010.location_code = objd_mes0100.loc_code

									'Take Shelf Code from MEs0040
									InsertNewRecordFord_work0010.shelf_no = Str_Shelfcode

									Dim objm_proc0020 = (From t In Db.m_item0020 Where t.item_code = StrItemCode).ToList
									'From M0020 Master, トレース区分	TRACE_TYPE 1:対象 2:非対象
									If objm_proc0020.Count > 0 Then
										InsertNewRecordFord_work0010.trace_type = objm_proc0020(0).trace_type
									Else
										InsertNewRecordFord_work0010.trace_type = "1"
									End If

									InsertNewRecordFord_work0010.stock_type = "1"
									InsertNewRecordFord_work0010.plant_code = Str_Strplantcode

                                    InsertNewRecordFord_work0010.stockstts_type = "1"
                                    InsertNewRecordFord_work0010.seq = Count

                                    'Add To Table Object
                                    Db.d_work0010.Add(InsertNewRecordFord_work0010)

								End If

							End If

						End If

						'Save To Database
						Db.Configuration.ValidateOnSaveEnabled = False
						Db.SaveChanges()
						Db.Configuration.ValidateOnSaveEnabled = True
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

					objd_mes0100.label_no = Nothing
					objd_mes0100.TextBox_lable_no = Nothing
					objd_mes0100.item_code = Nothing
					objd_mes0100.A1030_Itemname = Nothing
					objd_mes0100.str_qty = Nothing
					objd_mes0100.unit_code = Nothing

					Return View(objd_mes0100)

				End If

			End If

			Return View(objd_mes0100)
		End Function

		' GET: A1030_PayOutInput/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1030_PayOutInput/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1030_PayOutInput/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1030_PayOutInput/Delete/5
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