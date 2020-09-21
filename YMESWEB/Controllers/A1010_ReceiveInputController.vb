Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
	Public Class A1010_ReceiveInputController
		Inherits Controller

		Dim db As New Model1
		Dim StrPlantCode As String = Nothing
		Dim Appid As String = My.Settings.Item("ApplicationID")
		Dim CompCd As String = My.Settings.Item("CompCd")

		'GET: A1010_ReceiveInput
		Function Index(ByVal PassHeaderText As String, ByVal GOWOINPUT As String, ByVal BolDirectGotoWO As String) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			'If Pres Back Button From brower Then Need To Clear Binded Value Also
			ModelState.Clear()
			Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)

			'If User Click Previous Button Then Pass Directly Value 
			If PassHeaderText IsNot Nothing Then
				Dim dt_d_mes0150 As New d_mes0150
				dt_d_mes0150.header_text = PassHeaderText

				'When Click Back Button Then It model become Null
				If BolDirectGotoWO IsNot Nothing Then
					dt_d_mes0150.BolDirectGotoWO = BolDirectGotoWO
				End If

				TempData("d_mes0150") = dt_d_mes0150

			ElseIf GOWOINPUT IsNot Nothing AndAlso GOWOINPUT = "1" Then
				Dim dt_d_mes0150 As New d_mes0150
				dt_d_mes0150.header_text = "--------------"
				dt_d_mes0150.BolDirectGotoWO = True
				TempData("d_mes0150") = dt_d_mes0150

			End If

			ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
			TempData.Keep("d_mes0150")
			Return View(TempData("d_mes0150"))
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
				'Divide Two Numbers
				Dim intIdxDash As String = d_mes0150.barcode.IndexOf("-")
				Dim Strparampo_no As String = d_mes0150.barcode.Substring(0, intIdxDash)
				Dim Strparampo_sub_no As String = d_mes0150.barcode.Substring(intIdxDash + 1)

				Dim objd_sap0050 = (From t In db.d_sap0050 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.del_flag = "0" AndAlso t.plant_code = StrPlantCode).ToList

                If objd_sap0050.Count > 0 Then

                    '承認方針がNULLではない場合、
                    '承認フラグが「1:未承認」の場合、受入登録不可
                    If objd_sap0050(0).appd_policy IsNot Nothing AndAlso objd_sap0050(0).appd_flag = "1" Then
                        TempData("d_mes0150") = d_mes0150
                        TempData("errorDataNotFound") = LangResources.MSG_A1010_31_Ponotapproved
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(d_mes0150)
                    End If

                    Dim StrItemCode As String = objd_sap0050(0).item_code
                    Dim objm_item0010 = (From m In db.m_item0010 Where m.item_code = StrItemCode AndAlso m.plant_code = StrPlantCode).ToList

                    '● 基準単価有無のチェック	(PONO 入力時チェック)
                    '(品目プラントマスタ.標準原価 / 品目プラントマスタ.価格単位) が 0.001 以下の場合、受入登録不可
                    If objm_item0010 IsNot Nothing AndAlso objm_item0010.Count > 0 Then
                        If objm_item0010(0).standard_cost Is Nothing OrElse
                            objm_item0010(0).price_unit Is Nothing OrElse
                            objm_item0010(0).standard_cost = 0 OrElse
                            objm_item0010(0).price_unit = 0 OrElse
                            objm_item0010(0).standard_cost / objm_item0010(0).price_unit <= 0.001 Then
                            TempData("d_mes0150") = d_mes0150
                            TempData("errorDataNotFound") = LangResources.MSG_A1010_32_StandardCostless
                            ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                            Return View(d_mes0150)
                        End If
                    End If

                    If Not (objd_sap0050(0).acc_set_cat = "K" OrElse objd_sap0050(0).acc_set_cat = "A") AndAlso
                    objd_sap0050(0).in_loc_code = "" Then
                        TempData("d_mes0150") = d_mes0150
                        TempData("errorDataNotFound") = LangResources.MSG_A1010_30_CannotEnterLocationCodenull
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(d_mes0150)
                    End If

                    d_mes0150.po_no = Strparampo_no
                    d_mes0150.po_sub_no = Strparampo_sub_no
                    d_mes0150.ItemCode = objd_sap0050(0).item_code

                    'when acc_set_cat is K or A, no stock manage(sap0050 stock_odr_qty is 0). so set order qty and order unit to stock qty and stock unit to avoid stock_odr_qty 0 check
                    If objd_sap0050(0).acc_set_cat = "K" OrElse objd_sap0050(0).acc_set_cat = "A" Then
                        objd_sap0050(0).stock_odr_qty = objd_sap0050(0).odr_qty
                        objd_sap0050(0).stock_unit_code = objd_sap0050(0).unit_code
                    End If

                    'order And Stock Order Qty Is Zero Then Error
                    If objd_sap0050(0).odr_qty = 0 Then
                        TempData("d_mes0150") = d_mes0150
                        TempData("errorDataNotFound") = LangResources.MSG_A1010_21_Odr_QtyIsZero
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(d_mes0150)
                    ElseIf objd_sap0050(0).stock_odr_qty = 0 Then
                        TempData("d_mes0150") = d_mes0150
                        TempData("errorDataNotFound") = LangResources.MSG_A1010_22_Stock_Odr_QtyIsZero
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(d_mes0150)
                    ElseIf objd_sap0050(0).stock_odr_qty < 0 Then
                        TempData("d_mes0150") = d_mes0150
                        TempData("errorDataNotFound") = LangResources.MSG_A1010_30_CannotEnter_ReturnPO
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(d_mes0150)
                    End If

                    'Ratio Of Qty And Stock Qty
                    Dim RatioQty As Decimal = objd_sap0050(0).stock_odr_qty / objd_sap0050(0).odr_qty

                    'Set Ratio as HiddenRatio to Use 
                    d_mes0150.HiddenDec_RatioQty = RatioQty

                    '2019/07/05 Take Item name In Sap0050
                    'Dim objm_item0010 = (From m In db.m_item0010 Where m.item_code = StrItemCode AndAlso m.plant_code = StrPlantCode).ToList

                    'If objm_item0010.Count > 0 Then
                    '	d_mes0150.Itemname = objm_item0010(0).item_name
                    'End If

                     d_mes0150.Itemname = objd_sap0050(0).item_name
                    d_mes0150.receive_date = DateTime.Today

                    Dim Obj_d_mes0150 = (From m In db.d_mes0150 Where m.po_no = Strparampo_no AndAlso m.po_sub_no = Strparampo_sub_no AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).ToList
                    Dim Obj_SumRecievrQty As Decimal = 0
                    If Obj_d_mes0150.Count > 0 Then
                        Obj_SumRecievrQty = (From m In db.d_mes0150 Where m.po_no = Strparampo_no AndAlso m.po_sub_no = Strparampo_sub_no AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).Sum(Function(F) F.receive_qty)
                    End If


                    Dim IntOrderqty As Decimal = 0

                    '② 過剰納入許容範囲がNULLではない場合、
                    '例: 発注数 = 500 、過剰納入許容範囲 = 20%
                    '割合計算 →    500 x 0.2 = 100
                    '受入数 > 600(500 + 100) の場合、受入登録不可
                    '受入数 > 発注数 の場合、受入登録不可
                    Dim bolmax_DelExist As Boolean = False

                    If objd_sap0050(0).un_over_deliv_type Is Nothing AndAlso
                       objd_sap0050(0).max_delivery Is Nothing Then

                        IntOrderqty = objd_sap0050(0).odr_qty - Obj_SumRecievrQty

                    ElseIf objd_sap0050(0).max_delivery IsNot Nothing Then

                        If Obj_SumRecievrQty >= objd_sap0050(0).odr_qty Then
                            IntOrderqty = objd_sap0050(0).odr_qty + ((objd_sap0050(0).odr_qty * objd_sap0050(0).max_delivery) / 100) - Obj_SumRecievrQty
                            bolmax_DelExist = True
                        Else
                            IntOrderqty = objd_sap0050(0).odr_qty - Obj_SumRecievrQty
                        End If

                        'Can input anything
                        'No Check For this Case
                    Else

                        If Obj_SumRecievrQty >= objd_sap0050(0).odr_qty Then
                            'Assign max
                            IntOrderqty = 9999999999999.998
                            bolmax_DelExist = True
                        Else
                            IntOrderqty = objd_sap0050(0).odr_qty - Obj_SumRecievrQty
                        End If

                    End If

                    'If Order Is Sucessfully Completed Then NO Need To Do Further Process
                    If IntOrderqty <= 0 Then
                        TempData("errorDataNotFound") = LangResources.MSG_A1010_13_OrderCompleted
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(d_mes0150)
                    End If

                    d_mes0150.str_receive_qty = IntOrderqty.ToString("#,##0.###")
					d_mes0150.hidden_receive_qty = IntOrderqty

					Dim Dec_TotalStockQty As Decimal = Math.Round(IntOrderqty * RatioQty, 3)
					d_mes0150.str_QtyPerUnit = Dec_TotalStockQty.ToString("#,##0.###")
					d_mes0150.stock_unit_code = objd_sap0050(0).stock_unit_code

					Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList
					Dim InputQty As Decimal = 0
					Dim Dec_OrginalHiddenInputqtyfromDB As Decimal = 0

                    'If packin Qty Not Found than Set to Null
                    d_mes0150.HiddenInputqty = ""

                    If objm_proc0020.Count > 0 Then

                        'if Packing Qty Greter Then Order then OrderQty = Packing Qty
                        If Not objm_proc0020(0).pack_in_qty Is Nothing Then

                            Dec_OrginalHiddenInputqtyfromDB = objm_proc0020(0).pack_in_qty

                            'If Packing Qty is there then Set in Case of bolmax_DelExist
                            If bolmax_DelExist = False AndAlso Dec_TotalStockQty < objm_proc0020(0).pack_in_qty Then
                                InputQty = Dec_TotalStockQty
                            Else
                                InputQty = objm_proc0020(0).pack_in_qty
                            End If

                            'if packing Qty is There No One can Input more then This
                            d_mes0150.HiddenInputqty = objm_proc0020(0).pack_in_qty

                        Else
                            InputQty = Dec_TotalStockQty

                        End If

                    Else
                        InputQty = Dec_TotalStockQty
                    End If

                    d_mes0150.OrginalHiddenInputqtyfromDB = IIf(Dec_OrginalHiddenInputqtyfromDB = 0, Nothing, Dec_OrginalHiddenInputqtyfromDB.ToString("#,##0.###"))

                    '② 過剰納入許容範囲がNULLではない場合、
                    '例: 発注数 = 500 、過剰納入許容範囲 = 20%
                    '割合計算 →    500 x 0.2 = 100
                    '受入数 > 600(500 + 100) の場合、受入登録不可
                    '受入数 > 発注数 の場合、受入登録不可
                    'Set Null If Qty Is More Than Order
                    If bolmax_DelExist = True Then

                        d_mes0150.str_receive_qty = ""
                        d_mes0150.DivideTerm = ""
                        d_mes0150.RemainingQty = ""
                        d_mes0150.Fraction_stock_unit_code = ""
                        d_mes0150.PackingCount = ""
                        If objm_proc0020 IsNot Nothing AndAlso objm_proc0020.Count > 0 AndAlso objm_proc0020(0).pack_in_qty IsNot Nothing Then
                            d_mes0150.Inputqty = InputQty.ToString("#,##0.###")
                        Else
                            d_mes0150.Inputqty = ""
                        End If

                    Else

                        d_mes0150.Inputqty = InputQty.ToString("#,##0.###")

                        Dim Quotient As Decimal = Math.Floor(Dec_TotalStockQty / InputQty)
                        Dim Remainder As Decimal = Dec_TotalStockQty Mod InputQty

                        d_mes0150.DivideTerm = Quotient.ToString("#,##0.###")

                        If Remainder <> 0 Then
                            d_mes0150.RemainingQty = Remainder.ToString("#,##0.###")
                            d_mes0150.Fraction_stock_unit_code = objd_sap0050(0).stock_unit_code
                            d_mes0150.PackingCount = 1
                        End If

                    End If

                    d_mes0150.unit_code = objd_sap0050(0).unit_code

                    Dim Stocktype As String = Nothing
                    d_mes0150.Stock_type = objd_sap0050(0).stock_type
					If objd_sap0050(0).stock_type = "1" Then
						Stocktype = LangResources.A1010_12_Yes
					Else
						Stocktype = LangResources.A1010_12_No
					End If

					d_mes0150.Show_Stock_type = Stocktype
					d_mes0150.acc_set_cat = objd_sap0050(0).acc_set_cat
					TempData("d_mes0150") = d_mes0150
					Return RedirectToAction("Create", "A1010_ReceiveInput")

				Else

					'Data Is Not Regsiterd in master
					TempData("errorDataNotFound") = LangResources.MSG_A1010_14_BarcodeNotFound
					ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
					Return View(d_mes0150)

				End If

			Else

				'Data Is Not Regsiterd in master
				TempData("errorDataNotFound") = LangResources.MSG_A1010_14_BarcodeNotFound
				ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
				Return View(d_mes0150)

			End If

		End Function

		'GET: A1010_ReceiveInput
		Function HeaderText() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			'If Pres Back Button From brower Then Need To Clear Binded Value Also
			ModelState.Clear()
			Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
			TempData.Clear()

			Dim s0010 = (From t In db.s0010 Where t.appid = Appid AndAlso t.compcd = CompCd).ToList
			If s0010 IsNot Nothing AndAlso s0010.Count > 0 Then

				ViewData!header_qr_type = s0010(0).header_qr_type
				'POヘッダー入力区分 1:対象 2:対象外
				'If 2:対象外 Then No Need to Show this Form
				If s0010(0).po_header_input_type = "2" Then
					Return RedirectToAction("Index", "A1010_ReceiveInput")
				End If

			End If

			ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
			Return View()
		End Function

		<HttpPost()>
		Function HeaderText(ByVal d_mes0150 As d_mes0150) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

			Dim STRHeaderText As String = d_mes0150.header_text

			Dim s0010 = (From t In db.s0010 Where t.appid = Appid AndAlso t.compcd = CompCd).ToList
			If s0010 IsNot Nothing AndAlso s0010.Count > 0 Then
				If s0010(0).header_qr_type = "1" AndAlso STRHeaderText.Length > 25 Then
					TempData("errorDataNotFound") = LangResources.MSG_A1010_29_KBN1_PleaseEnterLessthen25
					ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
					Return View(d_mes0150)
				End If
			End If

			Dim StrHeader = db.FN_A1010_GETHEADER(STRHeaderText, CompCd, Appid)

			'if input Non Numeric In Date Then Display error
			If StrHeader = "ERRORFALSE" Then

				TempData("errorDataNotFound") = LangResources.MSG_A1010_28_HeaderTextWrongFormat
				Return View(d_mes0150)

			Else

				d_mes0150.header_text = StrHeader

			End If

			TempData("d_mes0150") = d_mes0150
			Return RedirectToAction("Index", "A1010_ReceiveInput")

		End Function

		' GET: A1010_ReceiveInput/Details/5
		Function Details(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' GET: A1010_ReceiveInput/Create
		Function Create() As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			If Session("LoginUserid") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			End If

			ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
			If TempData("d_mes0150").acc_set_cat = "K" OrElse TempData("d_mes0150").acc_set_cat = "A" Then
				ViewData!acc_set_cat = False
			Else
				ViewData!acc_set_cat = True
			End If

			'TempData.Keep()
			TempData.Keep("d_mes0150")
			Return View(TempData("d_mes0150"))
		End Function

		'POST: A1010_ReceiveInput/Create
		<HttpPost()>
		Function Create(<Bind(Include:="PLANT_CODE, SLIP_NO, PO_NO, PO_SUB_NO, PO_RECEIVE_SEQ, RECEIVE_DATE, POST_DATE, WORK_USER, RECEIVE_QTY, HIDDEN_RECEIVE_QTY,HIDDENDEC_RATIOQTY,ORGINALHIDDENINPUTQTYFROMDB, STR_RECEIVE_QTY,UNIT_CODE, STOCK_RECEIVE_QTY, STR_QTYPERUNIT, STOCK_UNIT_CODE, FRACTION_STOCK_UNIT_CODE, SAP_FLAG, SAP_SEND_DATE, DELETE_FLG, BARCODE, INPUTQTY, DIVIDETERM, REMAININGQTY, ITEMCODE, ITEMNAME, STOCK_TYPE, PACKINGCOUNT,SHOW_STOCK_TYPE, HIDDENINPUTQTY,HEADER_TEXT,ACC_SET_CAT, INSTID, INSTDT, INSTTERM, INSTPRGNM, UPDTID, UPDTDT, UPDTTERM, UPDTPRGNM, BOLDIRECTGOTOWO")> ByVal objd_mes0150 As d_mes0150, ByVal btnRegister As String) As ActionResult

			'If Session Is Nothing Or
			'Somehow Session Lost
			'Or SomeOne Open Form Directly By Changing URL
			'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
			If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
				Return RedirectToAction("Index", "Login")
			Else
				StrPlantCode = Session("StrPlantCode")
			End If

            If TempData("d_mes0150").acc_set_cat = "K" OrElse TempData("d_mes0150").acc_set_cat = "A" Then
                ViewData!acc_set_cat = False
            Else
                ViewData!acc_set_cat = True
            End If
            TempData.Keep("d_mes0150")
            'Date Check
            If Not objd_mes0150.receive_date Is Nothing Then
				Dim Recieve As Date = objd_mes0150.receive_date
				Dim Todate As Date = Date.Now
				If Recieve > Todate Then
					TempData("DateShouldbeLessThenToday") = LangResources.MSG_A1010_25_CannotinputFuturedate
					'If update Click Then Error Message
					If btnRegister = 3 Then
						ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
						Return View(objd_mes0150)
					End If
				End If
			Else
				'if Click Register then Will Return Error
				Return View(objd_mes0150)
			End If

			If btnRegister = 4 Then
				ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
				Return View(objd_mes0150)
			End If

			'If Input . or Some alphabet Or Non numeric Value then Error
			'Need to Calculate at This Place Because
			'If Qty From Database Then Set From database and If Not then Set to Null
			If Not objd_mes0150.str_receive_qty Is Nothing AndAlso IsNumeric(objd_mes0150.str_receive_qty) = False Then

				'get Paking Qty
				Dim StrItemCode = objd_mes0150.ItemCode

				Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList

				Dim InputQty As Decimal = 0

				If objm_proc0020.Count > 0 Then

					If Not objm_proc0020(0).pack_in_qty Is Nothing Then
						InputQty = objm_proc0020(0).pack_in_qty
					Else
						InputQty = 0
					End If

				Else
					InputQty = 0
				End If

				objd_mes0150.Inputqty = IIf(InputQty = 0, Nothing, InputQty.ToString("#,##0.###"))
				objd_mes0150.DivideTerm = ""
				objd_mes0150.RemainingQty = ""
				objd_mes0150.PackingCount = ""
				objd_mes0150.str_receive_qty = ""
				objd_mes0150.str_QtyPerUnit = ""
				objd_mes0150.Fraction_stock_unit_code = ""

				TempData("QtyShouldBeLess") = LangResources.MSG_A1010_25_InvalidNumber
				ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
				Return View(objd_mes0150)
			End If

			Dim Cnvrttodecimal As Decimal = Decimal.Parse(IIf(objd_mes0150.str_receive_qty Is Nothing, 0, objd_mes0150.str_receive_qty))

			'Click Update button
			If btnRegister = 3 Then

				'If Database Error Is There Then It Will Display
				If ModelState.IsValid = False Then
					ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
					Return View(objd_mes0150)
				End If

                'Store Po_No And Po_Sub_No
                Dim Strparampo_no_For_Check As String = objd_mes0150.po_no
                Dim Strparampo_sub_no_For_Check As String = objd_mes0150.po_sub_no
                'Get Data From D_Sap0050
                Dim objd_sap0050_For_Check = (From t In db.d_sap0050 Where t.po_no = Strparampo_no_For_Check AndAlso t.po_sub_no = Strparampo_sub_no_For_Check AndAlso t.plant_code = StrPlantCode).ToList

                If objd_sap0050_For_Check IsNot Nothing AndAlso objd_sap0050_For_Check.Count > 0 Then

                    Dim Obj_d_mes0150 = (From m In db.d_mes0150 Where m.po_no = Strparampo_no_For_Check AndAlso m.po_sub_no = Strparampo_sub_no_For_Check AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).ToList
                    Dim Obj_SumRecievrQty As Decimal = 0
                    If Obj_d_mes0150.Count > 0 Then
                        Obj_SumRecievrQty = (From m In db.d_mes0150 Where m.po_no = Strparampo_no_For_Check AndAlso m.po_sub_no = Strparampo_sub_no_For_Check AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).Sum(Function(F) F.receive_qty)
                    End If

                    '① 過剰納入無制限許容区分、過剰納入許容範囲の両方がNULLの場合、
                    '受入数 > 発注数 の場合、受入登録不可
                    If objd_sap0050_For_Check(0).un_over_deliv_type Is Nothing AndAlso objd_sap0050_For_Check(0).max_delivery Is Nothing Then
                        If Obj_SumRecievrQty + Cnvrttodecimal > objd_sap0050_For_Check(0).odr_qty Then
                            TempData("QtyShouldBeLess") = LangResources.MSG_A1010_15_ReceiveQtyIsGreterThenOrder
                            ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                            Return View(objd_mes0150)
                            '② 過剰納入許容範囲がNULLではない場合、
                            '例: 発注数 = 500 、過剰納入許容範囲 = 20%
                            '割合計算 →    500 x 0.2 = 100
                            '受入数 > 600(500 + 100) の場合、受入登録不可
                            '受入数 > 発注数 の場合、受入登録不可
                        End If
                    ElseIf objd_sap0050_For_Check(0).max_delivery IsNot Nothing Then
                        If Obj_SumRecievrQty + Cnvrttodecimal > objd_sap0050_For_Check(0).odr_qty + ((objd_sap0050_For_Check(0).odr_qty * objd_sap0050_For_Check(0).max_delivery) / 100) Then
                            TempData("QtyShouldBeLess") = LangResources.MSG_A1010_15_ReceiveQtyIsGreterThenOrder
                            ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                            Return View(objd_mes0150)
                        End If
                    End If

                Else

                    'Data Is Not Regsiterd in master
                    TempData("errorDataNotFound") = LangResources.MSG_A1010_14_BarcodeNotFound
                    ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                    Return RedirectToAction("Index", "A1010_ReceiveInput")
                End If

                '2019/11/14 Closing Date Check
                Dim DateFormat_Original As String = Session("DateFormat_Original")
                    Dim dt_date As Date = objd_mes0150.receive_date
                    Dim Str_date As String = dt_date.ToString(DateFormat_Original)
                    'Check Closing Date Status
                    Dim check_closing_date = db.fn_check_closing_date(Str_date, DateFormat_Original)
                    If check_closing_date = False Then
                        TempData("DateShouldbeLessThenToday") = LangResources.MSG_Comm_CheckClosingDate
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(objd_mes0150)
                    End If

                    '2019/10/22If Location Null Then Error Check
                    If objd_sap0050_For_Check IsNot Nothing AndAlso objd_sap0050_For_Check.Count > 0 Then

                        If Not (objd_mes0150.acc_set_cat = "K" OrElse objd_mes0150.acc_set_cat = "A") AndAlso
                    objd_sap0050_For_Check(0).in_loc_code = "" Then
                            TempData("errorDataNotFound") = LangResources.MSG_A1010_30_CannotEnterLocationCodenull
                            ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                            TempData("d_mes0150") = objd_mes0150
                            Return RedirectToAction("Index", "A1010_ReceiveInput")
                        End If

                    Else

                        'Data Is Not Regsiterd in master
                        TempData("errorDataNotFound") = LangResources.MSG_A1010_14_BarcodeNotFound
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return RedirectToAction("Index", "A1010_ReceiveInput")

                    End If

                    'Need to Calculate Again Because if user Change Reiving qty And Direct Click Enter
                    'In this Case Need to Calculate and Then
                    ModelState.Clear()
                    If Cnvrttodecimal = 0 Then

                        'get Paking Qty
                        Dim StrItemCode = objd_mes0150.ItemCode
                        Dim RecieveQty = Cnvrttodecimal

                        Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList

                        Dim InputQty As Decimal = 0

                        If objm_proc0020.Count > 0 Then

                            If Not objm_proc0020(0).pack_in_qty Is Nothing Then
                                InputQty = objm_proc0020(0).pack_in_qty
                            Else
                                InputQty = RecieveQty
                            End If

                        Else
                            InputQty = RecieveQty
                        End If

                        objd_mes0150.Inputqty = IIf(InputQty = 0, Nothing, InputQty.ToString("#,##0.###"))
                        objd_mes0150.DivideTerm = ""
                        objd_mes0150.RemainingQty = ""
                        objd_mes0150.PackingCount = ""
                        objd_mes0150.str_receive_qty = ""
                        objd_mes0150.str_QtyPerUnit = ""
                        objd_mes0150.Fraction_stock_unit_code = ""
                        ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                        Return View(objd_mes0150)

                        'If User Change Reciving Qty And Click Register Directly And Value is Not Zero
                    Else

                        'Store Po_No And Po_Sub_No
                        Dim Strparampo_no As String = objd_mes0150.po_no
                        Dim Strparampo_sub_no As String = objd_mes0150.po_sub_no

                        Dim objd_sap0050 = (From t In db.d_sap0050 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.del_flag = "0" AndAlso t.plant_code = StrPlantCode).ToList

                        Dim StrOrderQty As Decimal = objd_sap0050(0).odr_qty
                        Dim RecieveQty As Decimal = Cnvrttodecimal

                        'logic Move Above
                        ''For Concurrency Purpose Need To Check 
                        'Dim Obj_d_mes0150 = (From m In db.d_mes0150 Where m.po_no = Strparampo_no AndAlso m.po_sub_no = Strparampo_sub_no AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).ToList
                        'Dim Obj_SumRecievrQty As Decimal = 0
                        'If Obj_d_mes0150.Count > 0 Then
                        '	Obj_SumRecievrQty = (From m In db.d_mes0150 Where m.po_no = Strparampo_no AndAlso m.po_sub_no = Strparampo_sub_no AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).Sum(Function(F) F.receive_qty)
                        'End If

                        'If objd_sap0050(0).odr_qty < Obj_SumRecievrQty + RecieveQty Then
                        '	TempData("errorDataNotFound") = LangResources.MSG_Comm_Concurrency

                        '	'Dim drs0010 = (From t In db.s0010 Where t.appid = Appid AndAlso t.compcd = CompCd).ToList
                        '	'If drs0010 IsNot Nothing AndAlso drs0010.Count > 0 Then

                        '	'	'POヘッダー入力区分 1:対象 2:対象外
                        '	'	'If 2:対象外 Then No Need to Show this Form
                        '	'	If drs0010(0).po_header_input_type = "1" Then
                        '	'		Return RedirectToAction("HeaderText", "A1010_ReceiveInput")
                        '	'	Else
                        '	'		Return RedirectToAction("Index", "A1010_ReceiveInput")
                        '	'	End If

                        '	'End If
                        '	TempData("d_mes0150") = objd_mes0150
                        '	Return RedirectToAction("Index", "A1010_ReceiveInput")

                        'End If

                        If Not (objd_mes0150.acc_set_cat = "A" OrElse objd_mes0150.acc_set_cat = "K") Then

                            'Ratio Of Qty And Stock Qty
                            Dim RatioQty As Decimal = objd_sap0050(0).stock_odr_qty / objd_sap0050(0).odr_qty
                            Dim Dec_TotalStockQty As Decimal = RecieveQty * RatioQty

                            Dim Inputqty As Decimal = objd_mes0150.Inputqty
                            Dim Quotient As Decimal = Math.Floor(Dec_TotalStockQty / Inputqty)
                            Dim Remainder As Decimal = Dec_TotalStockQty Mod Inputqty

                            'objd_mes0150.stock_receive_qty = RecieveQty.ToString("#,##0.###")
                            objd_mes0150.Inputqty = Inputqty.ToString("#,##0.###")
                            objd_mes0150.DivideTerm = Quotient.ToString("#,##0.###")

                            If Remainder <> 0 Then

                                objd_mes0150.RemainingQty = Remainder.ToString("#,##0.###")
                                objd_mes0150.PackingCount = 1
                                objd_mes0150.Fraction_stock_unit_code = objd_sap0050(0).stock_unit_code

                            Else

                                objd_mes0150.RemainingQty = Nothing
                                objd_mes0150.PackingCount = Nothing
                                objd_mes0150.Fraction_stock_unit_code = Nothing

                            End If

                            objd_mes0150.str_QtyPerUnit = Dec_TotalStockQty.ToString("#,##0.###")
                            objd_mes0150.stock_unit_code = objd_sap0050(0).stock_unit_code

                        Else
                            'when acc_set_cat is K or A, no stock manage(sap0050 stock_odr_qty is 0). so set order qty and order unit to stock qty and stock unit
                            objd_mes0150.str_QtyPerUnit = RecieveQty.ToString("#,##0.###")
                            objd_mes0150.stock_unit_code = objd_sap0050(0).unit_code
                        End If

                    End If

                    Dim Strinspect_label_no As String = ""
                    Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
                    Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
                    Dim Tras As DbContextTransaction = db.Database.BeginTransaction
                    Try

                        'For Client Info
                        Dim cnt = db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

                        'Store Po_No And Po_Sub_No
                        Dim Strparampo_no As String = objd_mes0150.po_no
                        Dim Strparampo_sub_no As String = objd_mes0150.po_sub_no

                        'Get Data From D_Sap0050
                        Dim objd_sap0050 = (From t In db.d_sap0050 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.plant_code = StrPlantCode).ToList

                        'Create MES0150
                        'For Insert New Record
                        Dim InsertNewRecord As New d_mes0150

                        InsertNewRecord.plant_code = objd_sap0050(0).plant_code

                        'It Will Use Everwhere So Take Variable For Take Same Number
                        'auto Genrate Number
                        Dim StrSlipno As String = db.GetNewNo(db.Database.Connection, "SLIP_NO")
                        InsertNewRecord.slip_no = StrSlipno
                        InsertNewRecord.po_no = Strparampo_no
                        InsertNewRecord.po_sub_no = Strparampo_sub_no

                        Dim IntRecieveCnt As Integer = 0
                        'Get Data From d_mes0150
                        Dim obj_d_mes0150 = (From t In db.d_mes0150 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.delete_flg = "0" AndAlso t.plant_code = StrPlantCode).ToList

                        IntRecieveCnt = obj_d_mes0150.Count + 1

                        InsertNewRecord.po_receive_seq = IntRecieveCnt
                        InsertNewRecord.receive_date = objd_mes0150.receive_date
                        InsertNewRecord.post_date = objd_mes0150.receive_date
                        InsertNewRecord.work_user = Session("LoginUserid")
                        InsertNewRecord.receive_qty = Cnvrttodecimal
                        InsertNewRecord.unit_code = objd_mes0150.unit_code
                        InsertNewRecord.stock_receive_qty = objd_mes0150.str_QtyPerUnit
                        InsertNewRecord.stock_unit_code = objd_mes0150.stock_unit_code
                        InsertNewRecord.sap_flag = "0"
                        InsertNewRecord.sap_send_date = Nothing
                        InsertNewRecord.delete_flg = "0"

                        If objd_mes0150.BolDirectGotoWO Is Nothing OrElse
                        (objd_mes0150.BolDirectGotoWO IsNot Nothing AndAlso objd_mes0150.BolDirectGotoWO <> True) Then
                            InsertNewRecord.header_text = objd_mes0150.header_text
                        End If

                        InsertNewRecord.acc_set_cat = objd_mes0150.acc_set_cat

                        db.d_mes0150.Add(InsertNewRecord)

                        'Check If Data Has No Item Means Kbn Of Sap0050 has value of K&A Then No Need to Regiser
                        If Not (objd_mes0150.acc_set_cat = "K" OrElse objd_mes0150.acc_set_cat = "A") Then

                            'If user didnt logout then it will acotomatically login
                            Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

                            If objd_mes0150.Stock_type = "1" Then

                                'Create MES0050
                                'Condition For Create MES0050
                                Dim InsertNewRecordForMes0050 As New d_mes0050

                                InsertNewRecordForMes0050.plant_code = objd_sap0050(0).plant_code
                                ' It Will Use In MES040 So Take Variable
                                Strinspect_label_no = db.GetNewNo(db.Database.Connection, "INSPECT_LABEL_NO")
                                InsertNewRecordForMes0050.inspect_label_no = Strinspect_label_no

                                InsertNewRecordForMes0050.slip_no = StrSlipno
                                InsertNewRecordForMes0050.item_code = objd_mes0150.ItemCode
                                InsertNewRecordForMes0050.inspect_qty = objd_mes0150.str_QtyPerUnit
                                InsertNewRecordForMes0050.prev_inspect_qty = objd_mes0150.str_QtyPerUnit
                                InsertNewRecordForMes0050.unit_code = objd_mes0150.stock_unit_code
                                InsertNewRecordForMes0050.src_inspect_label_no = ""
                                InsertNewRecordForMes0050.print_datetime = Date.Now
                                InsertNewRecordForMes0050.work_user = Session("LoginUserid")
                                InsertNewRecordForMes0050.post_date = Date.Now
                                InsertNewRecordForMes0050.inspected_flg = "0"
                                InsertNewRecordForMes0050.delete_flg = "0"
                                'Add To Table Object
                                db.d_mes0050.Add(InsertNewRecordForMes0050)

                                'Create MES0040
                                'For Insert New Record In d_work0020
                                Dim InsertNewRecordFord_work0020 As New d_work0020

                                InsertNewRecordFord_work0020.instterm_ip = Ipaddress
                                InsertNewRecordFord_work0020.inspect_label_no = Strinspect_label_no
                                InsertNewRecordFord_work0020.item_code = objd_mes0150.ItemCode
                                Dim StrItemCode As String = objd_mes0150.ItemCode
                                Dim objm_item0010 = (From m In db.m_item0010 Where m.item_code = StrItemCode AndAlso m.plant_code = StrPlantCode).ToList

                                If objm_item0010.Count > 0 Then
                                    InsertNewRecordFord_work0020.item_name = objm_item0010(0).item_name
                                End If
                                InsertNewRecordFord_work0020.label_qty = objd_mes0150.str_QtyPerUnit
                                InsertNewRecordFord_work0020.unit_code = objd_mes0150.stock_unit_code
                                InsertNewRecordFord_work0020.both_po_no = objd_mes0150.po_no & "-" & objd_mes0150.po_sub_no
                                InsertNewRecordFord_work0020.receive_date = objd_mes0150.receive_date
                                InsertNewRecordFord_work0020.plant_code = objd_sap0050(0).plant_code

                                'Add To Table Object
                                db.d_work0020.Add(InsertNewRecordFord_work0020)

                            End If

                            'Condition For Create MES0040
                            Dim CountSheetQty As Integer = 0
                            Dim IntPackingCount As Integer = 0
                            If Not objd_mes0150.PackingCount Is Nothing Then
                                IntPackingCount = objd_mes0150.PackingCount
                            End If

                            'Get Count Of Qty
                            CountSheetQty = objd_mes0150.DivideTerm

                            Dim seq As Decimal = 0
                            'This Is Main Box packing Record
                            If CountSheetQty > 0 Then

                                For i As Integer = 0 To CountSheetQty - 1

                                    'Create MES0040
                                    'For Insert New Record In Mes0040
                                    Dim InsertNewRecordForMes0040 As New d_mes0040

                                    InsertNewRecordForMes0040.plant_code = objd_sap0050(0).plant_code

                                    'Same Record Should Be In Work 0010
                                    Dim strLABEL_NO_D_MES0040 As String = db.GetNewNo(db.Database.Connection, "LABEL_NO_D_MES0040")
                                    InsertNewRecordForMes0040.label_no = strLABEL_NO_D_MES0040
                                    InsertNewRecordForMes0040.slip_no = StrSlipno

                                    Dim StrItemCode = objd_mes0150.ItemCode
                                    InsertNewRecordForMes0040.item_code = StrItemCode

                                    Dim Strlocation_code = objd_sap0050(0).in_loc_code
                                    InsertNewRecordForMes0040.location_code = Strlocation_code

                                    'Take Shelf Code From  品目棚番マスタ	 M_PROC0060 With ItemKey And Location_Code
                                    'Get Data From D_Sap0050
                                    Dim objm_proc0060 = (From t In db.m_proc0060 Where t.item_code = StrItemCode AndAlso t.location_code = Strlocation_code AndAlso t.plant_code = StrPlantCode).ToList

                                    Dim SreShelfno As String = ""
                                    If objm_proc0060.Count > 0 Then
                                        SreShelfno = objm_proc0060(0).shelf_no
                                    End If
                                    InsertNewRecordForMes0040.shelf_no = SreShelfno

                                    '1:Yes--2:No
                                    If objd_mes0150.Stock_type = "1" Then

                                        InsertNewRecordForMes0040.inspect_qty = objd_mes0150.Inputqty
                                        InsertNewRecordForMes0040.prev_inspect_qty = objd_mes0150.Inputqty

                                    Else

                                        InsertNewRecordForMes0040.stock_qty = objd_mes0150.Inputqty

                                    End If

                                    InsertNewRecordForMes0040.unit_code = objd_mes0150.stock_unit_code
                                    InsertNewRecordForMes0040.print_datetime = Date.Now
                                    InsertNewRecordForMes0040.inspect_label_no = Strinspect_label_no
                                    InsertNewRecordForMes0040.src_inspect_label_no = ""
                                    InsertNewRecordForMes0040.transfer_flg = "0"
                                    InsertNewRecordForMes0040.delete_flg = "0"

                                    'Add To Table Object
                                    db.d_mes0040.Add(InsertNewRecordForMes0040)

                                    'Create d_work0010
                                    'Condition For Create d_work0010
                                    Dim InsertNewRecordFord_work0010 As New d_work0010

                                    InsertNewRecordFord_work0010.instterm_ip = Ipaddress
                                    InsertNewRecordFord_work0010.label_no = strLABEL_NO_D_MES0040
                                    InsertNewRecordFord_work0010.item_code = objd_mes0150.ItemCode

                                    Dim objm_item0010 = (From m In db.m_item0010 Where m.item_code = StrItemCode AndAlso m.plant_code = StrPlantCode).ToList
                                    If objm_item0010.Count > 0 Then
                                        InsertNewRecordFord_work0010.item_name = objm_item0010(0).item_name
                                    End If

                                    InsertNewRecordFord_work0010.label_qty = objd_mes0150.Inputqty
                                    InsertNewRecordFord_work0010.unit_code = objd_mes0150.stock_unit_code
                                    InsertNewRecordFord_work0010.both_po_no = objd_mes0150.po_no & "-" & objd_mes0150.po_sub_no
                                    InsertNewRecordFord_work0010.receive_date = objd_mes0150.receive_date
                                    InsertNewRecordFord_work0010.location_code = objd_sap0050(0).in_loc_code

                                    'Take Shelf Code From  品目棚番マスタ	 M_PROC0060 With ItemKey And Location_Code
                                    InsertNewRecordFord_work0010.shelf_no = SreShelfno

                                    Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList
                                    'From M0020 Master, トレース区分	TRACE_TYPE 1:対象 2:非対象
                                    If objm_proc0020.Count > 0 Then
                                        InsertNewRecordFord_work0010.trace_type = objm_proc0020(0).trace_type
                                    Else
                                        InsertNewRecordFord_work0010.trace_type = "1"
                                    End If
                                    InsertNewRecordFord_work0010.stock_type = objd_mes0150.Stock_type
                                    InsertNewRecordFord_work0010.plant_code = objd_sap0050(0).plant_code

                                    'Add StockStatus Condistion Same As
                                    If objd_mes0150.Stock_type = "1" Then
                                        'Inspection
                                        InsertNewRecordFord_work0010.stockstts_type = "2"
                                    Else
                                        'Stock
                                        InsertNewRecordFord_work0010.stockstts_type = "1"
                                    End If
                                    InsertNewRecordFord_work0010.seq = i + 1
                                    seq = i + 1
                                    'Add To Table Object
                                    db.d_work0010.Add(InsertNewRecordFord_work0010)

                                Next

                            End If

                            'Exyta Qty Record
                            If IntPackingCount > 0 Then

                                'Create MES0040
                                'For Insert New Record In Mes0040
                                Dim InsertNewRecordForMes0040 As New d_mes0040

                                InsertNewRecordForMes0040.plant_code = objd_sap0050(0).plant_code
                                Dim StrLABEL_NO_D_MES0040 As String = db.GetNewNo(db.Database.Connection, "LABEL_NO_D_MES0040")
                                InsertNewRecordForMes0040.label_no = StrLABEL_NO_D_MES0040
                                InsertNewRecordForMes0040.slip_no = StrSlipno
                                InsertNewRecordForMes0040.item_code = objd_mes0150.ItemCode
                                InsertNewRecordForMes0040.location_code = objd_sap0050(0).in_loc_code

                                Dim StrItemCode = objd_mes0150.ItemCode
                                Dim Strlocation_code = objd_sap0050(0).in_loc_code

                                'Take Shelf Code From  品目棚番マスタ	 M_PROC0060 With ItemKey And Location_Code
                                'Get Data From D_Sap0050
                                Dim objm_proc0060 = (From t In db.m_proc0060 Where t.item_code = StrItemCode AndAlso t.location_code = Strlocation_code AndAlso t.plant_code = StrPlantCode).ToList

                                Dim SreShelfno As String = ""
                                If objm_proc0060.Count > 0 Then
                                    SreShelfno = objm_proc0060(0).shelf_no
                                End If
                                InsertNewRecordForMes0040.shelf_no = SreShelfno

                                '1:Yes--2:No
                                If objd_mes0150.Stock_type = "1" Then
                                    InsertNewRecordForMes0040.inspect_qty = objd_mes0150.RemainingQty
                                    InsertNewRecordForMes0040.prev_inspect_qty = objd_mes0150.RemainingQty
                                Else
                                    InsertNewRecordForMes0040.stock_qty = objd_mes0150.RemainingQty
                                End If

                                InsertNewRecordForMes0040.unit_code = objd_mes0150.stock_unit_code
                                InsertNewRecordForMes0040.print_datetime = Date.Now
                                InsertNewRecordForMes0040.inspect_label_no = Strinspect_label_no
                                InsertNewRecordForMes0040.src_inspect_label_no = ""
                                InsertNewRecordForMes0040.transfer_flg = "0"
                                InsertNewRecordForMes0040.delete_flg = "0"

                                'Add To Table Object
                                db.d_mes0040.Add(InsertNewRecordForMes0040)

                                'Create d_work0010
                                'Condition For Create d_work0010
                                Dim InsertNewRecordFord_work0010 As New d_work0010

                                InsertNewRecordFord_work0010.instterm_ip = Ipaddress
                                InsertNewRecordFord_work0010.label_no = StrLABEL_NO_D_MES0040
                                InsertNewRecordFord_work0010.item_code = objd_mes0150.ItemCode

                                Dim objm_item0010 = (From m In db.m_item0010 Where m.item_code = StrItemCode AndAlso m.plant_code = StrPlantCode).ToList
                                If objm_item0010.Count > 0 Then
                                    InsertNewRecordFord_work0010.item_name = objm_item0010(0).item_name
                                End If

                                InsertNewRecordFord_work0010.label_qty = objd_mes0150.RemainingQty
                                InsertNewRecordFord_work0010.unit_code = objd_mes0150.stock_unit_code
                                InsertNewRecordFord_work0010.both_po_no = objd_mes0150.po_no & "-" & objd_mes0150.po_sub_no
                                InsertNewRecordFord_work0010.receive_date = objd_mes0150.receive_date
                                InsertNewRecordFord_work0010.location_code = objd_sap0050(0).in_loc_code

                                'Take Shelf Code From  品目棚番マスタ	 M_PROC0060 With ItemKey And Location_Code
                                InsertNewRecordFord_work0010.shelf_no = SreShelfno

                                Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList
                                'From M0020 Master
                                InsertNewRecordFord_work0010.trace_type = objm_proc0020(0).trace_type
                                InsertNewRecordFord_work0010.stock_type = objd_mes0150.Stock_type
                                InsertNewRecordFord_work0010.plant_code = objd_sap0050(0).plant_code

                                'Add StockStatus Condistion Same As
                                If objd_mes0150.Stock_type = "1" Then
                                    'Inspection
                                    InsertNewRecordFord_work0010.stockstts_type = "2"
                                Else
                                    'Stock
                                    InsertNewRecordFord_work0010.stockstts_type = "1"
                                End If
                                InsertNewRecordFord_work0010.seq = seq + 1
                                'Add To Table Object
                                db.d_work0010.Add(InsertNewRecordFord_work0010)

                            End If

                        End If

                        'Save To Database
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

                    'Throw ex

                Finally
                        Tras.Dispose()

                    End Try

                    'Dim s0010 = (From t In db.s0010 Where t.appid = Appid AndAlso t.compcd = CompCd).ToList
                    'If s0010 IsNot Nothing AndAlso s0010.Count > 0 Then

                    '	'POヘッダー入力区分 1:対象 2:対象外
                    '	'If 2:対象外 Then No Need to Show this Form
                    '	If s0010(0).po_header_input_type = "1" Then
                    '		Return RedirectToAction("HeaderText", "A1010_ReceiveInput")
                    '	Else
                    '		Return RedirectToAction("Index", "A1010_ReceiveInput")
                    '	End If

                    'End If

                    TempData("d_mes0150") = objd_mes0150
                    Return RedirectToAction("Index", "A1010_ReceiveInput")

                    'Qty caluculation
                Else

                    ModelState.Clear()
				If Cnvrttodecimal = 0 Then

					'get Paking Qty
					Dim StrItemCode = objd_mes0150.ItemCode
					Dim RecieveQty = Cnvrttodecimal

					Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList

					Dim InputQty As Decimal = 0

					If objm_proc0020.Count > 0 Then

						If Not objm_proc0020(0).pack_in_qty Is Nothing Then
							InputQty = objm_proc0020(0).pack_in_qty
						Else
							InputQty = RecieveQty
						End If

					Else
						InputQty = RecieveQty
					End If

					objd_mes0150.Inputqty = IIf(InputQty = 0, Nothing, InputQty.ToString("#,##0.###"))
					objd_mes0150.DivideTerm = ""
					objd_mes0150.RemainingQty = ""
					objd_mes0150.PackingCount = ""
					objd_mes0150.str_receive_qty = ""
					objd_mes0150.str_QtyPerUnit = ""
					objd_mes0150.Fraction_stock_unit_code = ""

				Else

					Dim BolisError As Boolean = False

					'Store Po_No And Po_Sub_No
					Dim Strparampo_no As String = objd_mes0150.po_no
					Dim Strparampo_sub_no As String = objd_mes0150.po_sub_no

					Dim objd_sap0050 = (From t In db.d_sap0050 Where t.po_no = Strparampo_no AndAlso t.po_sub_no = Strparampo_sub_no AndAlso t.del_flag = "0" AndAlso t.plant_code = StrPlantCode).ToList

					Dim StrOrderQty As Decimal = objd_sap0050(0).odr_qty
					Dim StrRecQty As Decimal = Cnvrttodecimal
					'Ratio Of Qty And Stock Qty
					Dim RatioQty As Decimal = (objd_sap0050(0).stock_odr_qty / objd_sap0050(0).odr_qty)

					' Error Case
					If StrRecQty > StrOrderQty Then
						TempData("QtyShouldBeLess") = LangResources.MSG_A1010_15_ReceiveQtyIsGreterThenOrder
						BolisError = True

					Else

						Dim Obj_d_mes0150 = (From m In db.d_mes0150 Where m.po_no = Strparampo_no AndAlso m.po_sub_no = Strparampo_sub_no AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).ToList
						Dim Obj_SumRecievrQty As Decimal = 0

						If Obj_d_mes0150.Count > 0 Then
							Obj_SumRecievrQty = (From m In db.d_mes0150 Where m.po_no = Strparampo_no AndAlso m.po_sub_no = Strparampo_sub_no AndAlso m.delete_flg = "0" AndAlso m.plant_code = StrPlantCode).Sum(Function(F) F.receive_qty)
						End If

						If StrOrderQty < Obj_SumRecievrQty + StrRecQty Then
							TempData("QtyShouldBeLess") = LangResources.MSG_A1010_15_ReceiveQtyIsGreterThenOrder
							BolisError = True
						End If

					End If

					'get Paking Qty
					Dim StrItemCode = objd_mes0150.ItemCode
					Dim RecieveQty = Cnvrttodecimal
					Dim Dec_TotalStockQty As Decimal = Math.Round(RecieveQty * RatioQty, 3)

					'get packet Qty
					Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList
					Dim InputQty As Decimal = 0
					If objm_proc0020.Count > 0 Then
						If Not objm_proc0020(0).pack_in_qty Is Nothing Then
							InputQty = objm_proc0020(0).pack_in_qty
						Else
							InputQty = Dec_TotalStockQty
						End If
					Else
						InputQty = Dec_TotalStockQty
					End If

					'If Package Qty From Table Is larger then Inputed Recieved qty Then Set Inputqty Same As Recieve
					If Dec_TotalStockQty < InputQty Then

						objd_mes0150.Inputqty = Dec_TotalStockQty.ToString("#,##0.###")
						objd_mes0150.DivideTerm = 1
						objd_mes0150.RemainingQty = ""
						objd_mes0150.PackingCount = ""
						objd_mes0150.str_QtyPerUnit = Dec_TotalStockQty.ToString("#,##0.###")
						objd_mes0150.stock_unit_code = objd_sap0050(0).stock_unit_code
						objd_mes0150.Fraction_stock_unit_code = ""

					Else

						If BolisError = False Then

							Dim Quotient As Decimal = Math.Floor(Dec_TotalStockQty / InputQty)
							Dim Remainder As Decimal = Dec_TotalStockQty Mod InputQty

							objd_mes0150.str_receive_qty = RecieveQty.ToString("#,##0.###")
							objd_mes0150.Inputqty = InputQty.ToString("#,##0.###")
							objd_mes0150.DivideTerm = Quotient.ToString("#,##0.###")
							objd_mes0150.str_QtyPerUnit = Dec_TotalStockQty.ToString("#,##0.###")
							objd_mes0150.stock_unit_code = objd_sap0050(0).stock_unit_code

							If Remainder <> 0 Then

								objd_mes0150.RemainingQty = Remainder.ToString("#,##0.###")
								objd_mes0150.PackingCount = 1
								objd_mes0150.Fraction_stock_unit_code = objd_sap0050(0).stock_unit_code

							Else

								objd_mes0150.RemainingQty = ""
								objd_mes0150.PackingCount = ""
								objd_mes0150.Fraction_stock_unit_code = ""

							End If

						End If

					End If

				End If
				ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
				Return View(objd_mes0150)

			End If
		End Function

		' GET: A1010_ReceiveInput/Edit/5
		Function Edit(ByVal id As Integer) As ActionResult

			Return View()
		End Function

		' POST: A1010_ReceiveInput/Edit/5
		<HttpPost()>
		Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
			Try
				' TODO: Add update logic here

				Return RedirectToAction("Index")
			Catch
				Return View()
			End Try
		End Function

		' GET: A1010_ReceiveInput/Delete/5
		Function Delete(ByVal id As Integer) As ActionResult
			Return View()
		End Function

		' POST: A1010_ReceiveInput/Delete/5
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