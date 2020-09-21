Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1070_IDTagInquiryController
		Inherits Controller

		Dim Db As New Model1
		Dim StrPlantCode As String = Nothing

		' GET: A1070_IDTagInquiry
		Function Index() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1070_01_IDTagInquiry
			Return View()

		End Function

		'Post Method
		<HttpPost>
		Function Index(ByVal objd_mes0040 As d_mes0040, ByVal btnRegister As String) As ActionResult

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
					objd_mes0040.inspect_label_no = objd_mes0040Cnt(0).inspect_label_no

					Dim Str_Slip_No As String = objd_mes0040Cnt(0).slip_no

					Dim objd_d_mes0150 = (From t In Db.d_mes0150 Where t.slip_no = Str_Slip_No AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList

					If objd_d_mes0150.Count > 0 Then
						objd_mes0040.Po_Sub_Po_NO = objd_d_mes0150(0).po_no & "-" & objd_d_mes0150(0).po_sub_no
						objd_mes0040.Receive_Date = objd_d_mes0150(0).receive_date

						Dim po_no = objd_d_mes0150(0).po_no
						Dim po_sub_no = objd_d_mes0150(0).po_sub_no

						'Get Supplier Code
						Dim objd_d_sap0050 = (From t In Db.d_sap0050 Where t.po_no = po_no AndAlso t.po_sub_no = po_sub_no AndAlso t.plant_code = StrPlantCode).ToList

						If objd_d_sap0050 IsNot Nothing AndAlso objd_d_sap0050.Count > 0 Then

							'Get Supplier Name From 仕入先マスタ	
							Dim Supplier_Code As String = objd_d_sap0050(0).supplier_code
							Dim objd_m_supp0010 = (From t In Db.m_supp0010 Where t.supplier_code = Supplier_Code AndAlso t.plant_code = StrPlantCode).ToList
							If objd_m_supp0010.Count > 0 Then
								objd_mes0040.Supplier_Name = objd_m_supp0010(0).supplier_name
							End If

						End If

					End If

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
					objd_mes0040.inspect_label_no = Nothing
					objd_mes0040.Po_Sub_Po_NO = Nothing
					objd_mes0040.Receive_Date = Nothing
					objd_mes0040.Supplier_Name = Nothing

				End If

                ViewData!ID = LangResources.A1070_01_IDTagInquiry
                Return View(objd_mes0040)

			End If

            ViewData!ID = LangResources.A1070_01_IDTagInquiry
            Return View(objd_mes0040)

		End Function


		' GET: A1070_IDTagInquiry/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: A1070_IDTagInquiry/Create
		Function Create() As ActionResult
			Return View()
		End Function

		' POST: A1070_IDTagInquiry/Create
		<HttpPost()>
		Function Create(ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add insert logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1070_IDTagInquiry/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1070_IDTagInquiry/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1070_IDTagInquiry/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1070_IDTagInquiry/Delete/5
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