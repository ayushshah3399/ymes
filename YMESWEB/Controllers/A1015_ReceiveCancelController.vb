Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1015_ReceiveCancelController
		Inherits Controller

		Dim db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1015_ReceiveCancel
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
			Return View()
		End Function

		<HttpPost()>
		Function Index(ByVal d_mes0150 As d_mes0150) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			'barcode format PoNo-PoSubNo. so it must contain char (-) and minlength is 3(eg. X-X).
			If d_mes0150.barcode.Contains("-") AndAlso d_mes0150.barcode.Length > 2 Then

				Dim intIdxDash As String = d_mes0150.barcode.IndexOf("-")
				Dim Strparampo_no As String = d_mes0150.barcode.Substring(0, intIdxDash)
				Dim Strparampo_sub_no As String = d_mes0150.barcode.Substring(intIdxDash + 1)

				Dim objd_mes0150 = (From t In db.d_mes0150 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode Order By t.po_receive_seq Descending).ToList
				Dim objd_sap0050 = (From t In db.d_sap0050 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.plant_code = StrPlantCode).ToList

				'Data is there or not
				If objd_mes0150.Count > 0 Then

					If objd_sap0050.Count > 0 Then

						If Not (objd_sap0050(0).acc_set_cat = "A" OrElse objd_sap0050(0).acc_set_cat = "K") Then

							'検査結果の登録した後も、取り消しをしたいという要望なので、その制御はできないです。
							'検査有りの受入取消の場合、その受入に紐づくラベル情報の在庫が、Inspectionにある
							'検査無しの受入取消の場合、その受入に紐づくラベル情報の在庫がUnrestrictedにある
							'かチェックをして、なければ受入取消できないとして欲しいです。
							'Checking Case
							Dim strSlipNo As String = objd_mes0150(0).slip_no
							Dim strLoc_code As String = objd_sap0050(0).in_loc_code
							Dim objd_mes0040lst = (From t In db.d_mes0040 Where t.slip_no = strSlipNo AndAlso t.location_code = strLoc_code AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList

							If objd_sap0050(0).stock_type = "1" Then

								If objd_mes0040lst.Count > 0 Then
									Dim objd_mes0040inspectQty = objd_mes0040lst.Sum(Function(F) F.inspect_qty)

									If objd_mes0040inspectQty <> objd_mes0150(0).stock_receive_qty Then
										TempData("errorDataNotFound") = LangResources.MSG_A1015_05_CannotDelete_Kensa
										ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
										Return View()
									End If

								Else

									TempData("errorDataNotFound") = LangResources.MSG_A1015_05_CannotDelete_Kensa
									ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
									Return View()

								End If

								'No Kensa
							Else

								If objd_mes0040lst.Count > 0 Then

									Dim objd_mes0040stock_qty = objd_mes0040lst.Sum(Function(F) F.stock_qty)

									If objd_mes0040stock_qty <> objd_mes0150(0).stock_receive_qty Then
										TempData("errorDataNotFound") = LangResources.MSG_A1015_06_CannotDelete_NoKensa
										ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
										Return View()
									End If

								Else

									TempData("errorDataNotFound") = LangResources.MSG_A1015_06_CannotDelete_NoKensa
									ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
									Return View()

								End If

							End If

						End If

						d_mes0150.delete_result_date = objd_mes0150(0).receive_date
						d_mes0150.po_no = Strparampo_no
						d_mes0150.po_sub_no = Strparampo_sub_no
						d_mes0150.ItemCode = objd_sap0050(0).item_code
						'Dim StrItemname As String = objd_sap0050(0).item_code
						'Dim objm_item0010 = (From t In db.m_item0010 Where t.item_code = StrItemname AndAlso t.plant_code = StrPlantCode).ToList

						''Gey Item Name
						'If objm_item0010.Count > 0 Then
						'	d_mes0150.Itemname = objm_item0010(0).item_name
						'End If

						d_mes0150.Itemname = objd_sap0050(0).item_name
						d_mes0150.str_receive_qty = objd_mes0150(0).receive_qty.ToString("#,##0.###")
						d_mes0150.unit_code = objd_mes0150(0).unit_code
						d_mes0150.po_receive_seq = objd_mes0150(0).po_receive_seq
						d_mes0150.receive_date = objd_mes0150(0).receive_date

						TempData("d_mes0150") = d_mes0150
						Return RedirectToAction("Create", "A1015_ReceiveCancel")

					Else
						'Not Registeref in Sap0050
						TempData("errorDataNotFound") = LangResources.MSG_A1015_03_BarcodeNotFound
						ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
						Return View()
					End If

				Else
					'Not Registeref in Mes0150
					TempData("errorDataNotFound") = LangResources.MSG_A1015_03_BarcodeNotFound
					ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
					Return View()
				End If

			Else
				'Input barcode Is Less Then 16 Digit
				TempData("errorDataNotFound") = LangResources.MSG_A1015_03_BarcodeNotFound
				ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
				Return View()

			End If

			'Catch ex As Exception
			'	Return View()
			'End Try

		End Function

		' GET: A1015_ReceiveCancel/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: A1015_ReceiveCancel/Create
		Function Create() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
			Return View(TempData("d_mes0150"))
		End Function

		' POST: A1015_ReceiveCancel/Create
		<HttpPost()>
		Function Create(<Bind(Include:="PLANT_CODE, SLIP_NO, ITEMCODE ,ITEMNAME ,INPUTQTY ,INPUTQTY ,STR_RECEIVE_QTY ,DELETE_RESULT_DATE ,PO_NO, PO_SUB_NO, PO_RECEIVE_SEQ, RECEIVE_DATE, POST_DATE, WORK_USER, RECEIVE_QTY, UNIT_CODE, STOCK_RECEIVE_QTY, STOCK_UNIT_CODE, SAP_FLAG, SAP_SEND_DATE, DELETE_FLG, INSTID, INSTDT, INSTTERM, INSTPRGNM, UPDTID, UPDTDT, UPDTTERM, UPDTPRGNM")> ByVal objd_mes0150 As d_mes0150, ByVal btnRegister As String) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			'If Database Error Is There Then It Will Display
			If ModelState.IsValid = False Then
				ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
				Return View(objd_mes0150)
			End If

			'Date Check
			If Not objd_mes0150.delete_result_date Is Nothing Then
				Dim delete_result_date As Date = objd_mes0150.delete_result_date
				Dim Todate As Date = Date.Now
				If delete_result_date > Todate Then
					TempData("DateShouldbeLessThenToday") = LangResources.MSG_A1010_25_CannotinputFuturedate
					ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
					Return View(objd_mes0150)
				End If
			Else
				'if Click Register then Will Return Error
				TempData("DateShouldbeLessThenToday") = LangResources.MSG_A1015_04_Result_Date_field_is_required
				Return View(objd_mes0150)
			End If

            '2019/11/14 Closing Date Check
            Dim DateFormat_Original As String = Session("DateFormat_Original")
            Dim dt_date As Date = objd_mes0150.delete_result_date
            Dim Str_date As String = dt_date.ToString(DateFormat_Original)
            'Check Closing Date Status
            Dim check_closing_date = db.fn_check_closing_date(Str_date, DateFormat_Original)
            If check_closing_date = False Then
                TempData("DateShouldbeLessThenToday") = LangResources.MSG_Comm_CheckClosingDate
                ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                Return View(objd_mes0150)
            End If

            Dim Strparampo_no = objd_mes0150.po_no
            Dim Strparampo_sub_no = objd_mes0150.po_sub_no
			Dim decPo_receive_seq = objd_mes0150.po_receive_seq
			Dim objd_mes0150_1 = (From t In db.d_mes0150 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.po_receive_seq = decPo_receive_seq AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList
			Dim objd_sap0050 = (From t In db.d_sap0050 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.plant_code = StrPlantCode).ToList
            Dim Obj_SumRecievrQty As Decimal = 0

            'Data is there or not
            If objd_mes0150_1.Count > 0 Then

                If objd_sap0050.Count > 0 Then

                    Obj_SumRecievrQty = (From m In db.d_mes0150 Where m.po_no = Strparampo_no AndAlso m.po_sub_no = Strparampo_sub_no AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).Sum(Function(F) F.receive_qty)

                    '● 請求済数量のチェック(更新時チェック)
                    '取消受入数 > 発注数 - 請求済数 の場合、受入取消不可
                    If objd_mes0150_1(0).receive_qty > Obj_SumRecievrQty - IIf(objd_sap0050(0).billed_ord_qty Is Nothing, 0, objd_sap0050(0).billed_ord_qty) Then
                        TempData("notenoughdeleteqty") = LangResources.MSG_A1020_16_notenoughdeleteqty
                        ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                        Return View(objd_mes0150)
                    End If

                    If Not (objd_sap0050(0).acc_set_cat = "A" OrElse objd_sap0050(0).acc_set_cat = "K") Then

                        '検査結果の登録した後も、取り消しをしたいという要望なので、その制御はできないです。
                        '検査有りの受入取消の場合、その受入に紐づくラベル情報の在庫が、Inspectionにある
                        '検査無しの受入取消の場合、その受入に紐づくラベル情報の在庫がUnrestrictedにある
                        'かチェックをして、なければ受入取消できないとして欲しいです。
                        'Checking Case

                        Dim strSlipNo As String = objd_mes0150_1(0).slip_no
                        Dim strLoc_code As String = objd_sap0050(0).in_loc_code
                        Dim objd_mes0040lst = (From t In db.d_mes0040 Where t.slip_no = strSlipNo AndAlso t.location_code = strLoc_code AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList

                        If objd_sap0050(0).stock_type = "1" Then

                            If objd_mes0040lst.Count > 0 Then
                                Dim objd_mes0040inspectQty = objd_mes0040lst.Sum(Function(F) F.inspect_qty)

                                If objd_mes0040inspectQty <> objd_mes0150_1(0).stock_receive_qty Then
                                    TempData("errorDataNotFound") = LangResources.MSG_A1015_05_CannotDelete_Kensa
                                    ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                                    Return RedirectToAction("Index", "A1015_ReceiveCancel")
                                End If

                            Else

                                TempData("errorDataNotFound") = LangResources.MSG_A1015_05_CannotDelete_Kensa
                                ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                                Return RedirectToAction("Index", "A1015_ReceiveCancel")

                            End If

                            'No Kensa
                        Else

                            If objd_mes0040lst.Count > 0 Then

                                Dim objd_mes0040stock_qty = objd_mes0040lst.Sum(Function(F) F.stock_qty)

                                If objd_mes0040stock_qty <> objd_mes0150_1(0).stock_receive_qty Then
                                    TempData("errorDataNotFound") = LangResources.MSG_A1015_06_CannotDelete_NoKensa
                                    ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                                    Return RedirectToAction("Index", "A1015_ReceiveCancel")
                                End If

                            Else

                                TempData("errorDataNotFound") = LangResources.MSG_A1015_06_CannotDelete_NoKensa
                                ViewData!ID = LangResources.A1015_01_Fn_CancelReceiving
                                Return RedirectToAction("Index", "A1015_ReceiveCancel")

                            End If

                        End If

                    End If

                Else

                    'Concunrrency Error because No data in Sap0050
                    TempData("errorDataNotFound") = LangResources.MSG_Comm_Concurrency
					Return RedirectToAction("Index", "A1015_ReceiveCancel")

				End If

				'Concunrrency
			Else
				TempData("errorDataNotFound") = LangResources.MSG_Comm_Concurrency
				Return RedirectToAction("Index", "A1015_ReceiveCancel")
			End If

			Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
			Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
			Dim Tras As DbContextTransaction = db.Database.BeginTransaction
			Try

				Dim cnt = db.Database.ExecuteSqlCommand("SELECT TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")
				'Dim obj_d_mes0150 = (From t In db.d_mes0150 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode Order By t.po_receive_seq Descending).ToList

				'Delete Flag
				objd_mes0150_1(0).delete_flg = "1"
				objd_mes0150_1(0).delete_result_date = objd_mes0150.delete_result_date

				Dim StrSlipno As String = objd_mes0150_1(0).slip_no

				If Not (objd_sap0050(0).acc_set_cat = "A" OrElse objd_sap0050(0).acc_set_cat = "K") Then

					'Delete MES0040 Record
					Dim obj_d_mes0050 = (From t In db.d_mes0050 Where t.slip_no = StrSlipno AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList
					If obj_d_mes0050.Count > 0 Then
						For i As Integer = 0 To obj_d_mes0050.Count - 1
							obj_d_mes0050(i).delete_flg = "1"
						Next
					End If

					'Delete MES0050 Record
					Dim obj_d_mes0040 = (From t In db.d_mes0040 Where t.slip_no = StrSlipno AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList
					If obj_d_mes0040.Count > 0 Then
						For i As Integer = 0 To obj_d_mes0040.Count - 1
							obj_d_mes0040(i).delete_flg = "1"
						Next
					End If

				End If

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

			Return RedirectToAction("Index", "A1015_ReceiveCancel")
		End Function


		' GET: A1015_ReceiveCancel/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1015_ReceiveCancel/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1015_ReceiveCancel/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1015_ReceiveCancel/Delete/5
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