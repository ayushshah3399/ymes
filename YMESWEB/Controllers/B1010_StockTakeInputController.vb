Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
    Public Class B1010_StockTakeInputController
        Inherits Controller

        Dim db As New Model1
        Dim StrPlantCode As String = Nothing

        ' GET: B1010_StockTakeInput
        Function Index(ByVal d_mes1020 As d_mes1020) As ActionResult

            'If Session Is Nothing Or
            'Somehow Session Lost
            'Or SomeOne Open Form Directly By Changing URL
            'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
            If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                Return RedirectToAction("Index", "Login")
            Else
                StrPlantCode = Session("StrPlantCode")
            End If

            'For check If no Data Then Error
            Dim Obj_d_mes1010 = (From t In db.d_mes1010 Where t.plant_code = StrPlantCode).ToList

            If Obj_d_mes1010.Count > 0 Then
                Dim Obj_d_mes1010_First = db.d_mes1010.OrderByDescending(Function(m) m.stocktake_date).First()

                If Obj_d_mes1010_First.stocktake_status <> "1" Then
                    'need to Show Error if there is no data
                    Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
                Else
                    d_mes1020.stocktake_date = Obj_d_mes1010_First.stocktake_date

                    'For Drop Down Box
                    Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                          Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = d_mes1020.stocktake_date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                          Order By t.location_code Ascending
                                          Select t).ToList
                    If Obj_m_proc0030.Count > 0 Then
                        If Request.Cookies("location_code") IsNot Nothing Then

                            'Set To TextBox
                            d_mes1020.location_code = Request.Cookies("location_code").Values.ToString
                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030

                        Else

                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030

                        End If
                    End If

                End If

            Else
                'need to Show Error if there is no data
                Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
            End If

            'For Displaay On The Screen
            ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
            Return View(d_mes1020)

        End Function

        ' POST: B1010_StockTakeInput/Create
        <HttpPost()>
        Function Index(<Bind(Include:="PLANT_CODE,STOCKTAKE_DATE,LABEL_NO,ITEM_CODE,LOCATION_CODE,SHELF_NO,STOCK_QTY,INSPECT_QTY,KEEP_QTY,INPUT_LOCATION_CODE,INPUT_SHELF_NO,INPUT_STOCK_QTY,INPUT_INSPECT_QTY,INPUT_KEEP_QTY,UNIT_CODE,INPUT_FLG, INSTID, INSTDT, INSTTERM, INSTPRGNM, UPDTID, UPDTDT, UPDTTERM, UPDTPRGNM")> ByVal objd_mes1020 As d_mes1020, ByVal btnRegister As String) As ActionResult

            'If Session Is Nothing Or
            'Somehow Session Lost
            'Or SomeOne Open Form Directly By Changing URL
            'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
            If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                Return RedirectToAction("Index", "Login")
            Else
                StrPlantCode = Session("StrPlantCode")
            End If

            'Check Again if Status Change Then Error
            Dim Obj_d_mes1010 = (From t In db.d_mes1010 Where t.plant_code = StrPlantCode).ToList
            Dim Obj_d_mes1010_First = Nothing
            If Obj_d_mes1010.Count > 0 Then
                Obj_d_mes1010_First = db.d_mes1010.OrderByDescending(Function(m) m.stocktake_date).First()

                If Obj_d_mes1010_First.stocktake_status <> "1" Then
                    'need to Show Error if there is no data
                    Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
                End If
            Else
                'need to Show Error if there is no data
                Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
            End If

            'Store the Date For Compare
            Dim Stock_take_Date As Date = Obj_d_mes1010_First.stocktake_date
            'If Database Error Is There Then It Will Display
            If ModelState.IsValid = False Then

                'For Drop Down Box
                Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                      Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                      Order By t.location_code Ascending
                                      Select t).ToList
                If Obj_m_proc0030.Count > 0 Then
                    Dim m_proc0030item As New m_proc0030
                    m_proc0030item.location_code = ""
                    Obj_m_proc0030.Insert(0, m_proc0030item)
                    ViewBag.Location_code = Obj_m_proc0030
                End If

                ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                Return View(objd_mes1020)
            End If

            'Check Location Code Exist or Not
            If btnRegister = "2" Then

                'Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" AndAlso t.location_code = objd_mes1020.location_code).ToList
                Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                             d_mes1130 In db.d_mes1130
                                      Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1" AndAlso
                                              t.location_code = objd_mes1020.location_code
                                      Order By t.location_code Ascending Select t).ToList

                If Obj_m_proc0030.Count = 0 Then
                    TempData("B1010_Emptylocation_code") = LangResources.MSG_B1030_10_LocationCodeExist
                    'Else
                    '	Response.Cookies("location_code").Value = objd_mes1020.location_code
                End If
                'Drop Down Menu
                Dim Obj_m_proc0030_1 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                        Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                        Order By t.location_code Ascending
                                        Select t).ToList
                If Obj_m_proc0030_1.Count > 0 Then
                    Dim m_proc0030item As New m_proc0030
                    m_proc0030item.location_code = ""
                    Obj_m_proc0030_1.Insert(0, m_proc0030item)
                    ViewBag.Location_code = Obj_m_proc0030_1
                End If
                ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                Return View(objd_mes1020)
            End If

            'If label Not Nothing Then Process
            If objd_mes1020.label_no IsNot Nothing Then

                'Label No 
                Dim StrLabel As String = objd_mes1020.label_no

                Dim Obj_d_mes0040 = (From t In db.d_mes0040 Where t.plant_code = StrPlantCode AndAlso t.label_no = StrLabel AndAlso t.delete_flg = "0").ToList

                ModelState.Clear()

                If Obj_d_mes0040.Count > 0 Then

                    'Check Error
                    If Obj_d_mes0040(0).location_code <> objd_mes1020.location_code Then

                        'For Drop Down Box
                        Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                              Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                              Order By t.location_code Ascending
                                              Select t).ToList
                        If Obj_m_proc0030.Count > 0 Then
                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030
                        End If

                        TempData("B1010_TxtLableError") = LangResources.MSG_B1030_09_LocationCodeDiff
                        ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                        Return View(objd_mes1020)
                    End If

                    If Obj_d_mes0040(0).sttime_input_flg = "1" Then

                        'Error Msg Display From Spec
                        '棚卸時発行のラベルの場合、数量変更として登録してください
                        'For Drop Down Box
                        Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                              Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                              Order By t.location_code Ascending
                                              Select t).ToList
                        If Obj_m_proc0030.Count > 0 Then

                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030

                        End If
                        TempData("B1010_TxtLableError") = LangResources.MSG_B1010_03_TanauroshiIssuedLabel
                        ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                        Return View(objd_mes1020)

                    Else

                        'Cannot Input the ID Tag that is inserted after Stocktake started.
                        'If mes0040.instdt > mes1010.instdt And mes0040.sttime_input_flag <> 1 Then,
                        If Obj_d_mes0040(0).instdt > Obj_d_mes1010_First.instdt Then

                            '2019/06/13 Change Added. 
                            Dim Strdate = Date.Parse(objd_mes1020.stocktake_date)
                            Dim Obj_d_mes1020 = (From t In db.d_mes1020 Where t.plant_code = StrPlantCode AndAlso t.label_no = StrLabel AndAlso t.stocktake_date = Strdate).ToList

                            If Obj_d_mes1020.Count = 0 Then

                                'For Drop Down Box
                                Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                                      Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                                      Order By t.location_code Ascending
                                                      Select t).ToList
                                If Obj_m_proc0030.Count > 0 Then

                                    Dim m_proc0030item As New m_proc0030
                                    m_proc0030item.location_code = ""
                                    Obj_m_proc0030.Insert(0, m_proc0030item)
                                    ViewBag.Location_code = Obj_m_proc0030

                                End If

                                objd_mes1020.Str_stock_qty = Nothing
                                objd_mes1020.unit_code = Nothing
                                objd_mes1020.Label_Status = Nothing
                                objd_mes1020.shelf_no = Nothing
                                objd_mes1020.item_code = Nothing
                                TempData("B1010_TxtLableError") = LangResources.MSG_B1030_08_CannotEnterIDTag
                                ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                                Return View(objd_mes1020)

                            End If

                        End If

                        'Set Value to The Controlls
                        If btnRegister = "1" Then

                            ModelState.Clear()

                            objd_mes1020.unit_code = Obj_d_mes0040(0).unit_code

                            Dim Str_Label_Status As String = Nothing
                            If Obj_d_mes0040(0).stock_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_12_available
                                objd_mes1020.Str_stock_qty = Obj_d_mes0040(0).stock_qty.ToString("##0.###")

                            ElseIf Obj_d_mes0040(0).inspect_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_13_Inspection
                                objd_mes1020.Str_stock_qty = Obj_d_mes0040(0).inspect_qty.ToString("##0.###")

                            ElseIf Obj_d_mes0040(0).keep_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_14_pending
                                objd_mes1020.Str_stock_qty = Obj_d_mes0040(0).keep_qty.ToString("##0.###")

                            Else
                                Str_Label_Status = LangResources.B1010_12_available
                                objd_mes1020.Str_stock_qty = 0
                            End If

                            objd_mes1020.Label_Status = Str_Label_Status
                            objd_mes1020.shelf_no = Obj_d_mes0040(0).shelf_no
                            objd_mes1020.item_code = Obj_d_mes0040(0).item_code

                            Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" Order By t.location_code Ascending).ToList
                            If Obj_m_proc0030.Count > 0 Then

                                Dim m_proc0030item As New m_proc0030
                                m_proc0030item.location_code = ""
                                Obj_m_proc0030.Insert(0, m_proc0030item)
                                ViewBag.Location_code = Obj_m_proc0030

                            End If

                            TempData("SetFocusOnSubmit") = "1"
                            ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                            Return View(objd_mes1020)

                        End If

                        Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
                        Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
                        Dim Tras As DbContextTransaction = db.Database.BeginTransaction

                        Try

                            'For Client Info
                            Dim cnt = db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

                            'Main Logic Start Here
                            'Check if record is already there Then update OtherWise Insert
                            Dim Strdate = Date.Parse(objd_mes1020.stocktake_date)
                            Dim Obj_d_mes1020 = (From t In db.d_mes1020 Where t.plant_code = StrPlantCode AndAlso t.label_no = StrLabel AndAlso t.stocktake_date = Strdate).ToList

                            'Variable For Display Qty
                            Dim DisplayQty As Decimal = 0
                            Dim DisplayStatus As String = Nothing

                            'Update Case
                            If Obj_d_mes1020.Count > 0 Then

                                Obj_d_mes1020(0).input_location_code = Obj_d_mes1020(0).location_code
                                Obj_d_mes1020(0).input_shelf_no = Obj_d_mes1020(0).shelf_no
                                Obj_d_mes1020(0).input_stock_qty = Obj_d_mes1020(0).stock_qty
                                Obj_d_mes1020(0).input_inspect_qty = Obj_d_mes1020(0).inspect_qty
                                Obj_d_mes1020(0).input_keep_qty = Obj_d_mes1020(0).keep_qty
                                Obj_d_mes1020(0).input_flg = "1"

                                If Obj_d_mes1020(0).input_cnt Is Nothing Then
                                    Obj_d_mes1020(0).input_cnt = 1
                                Else
                                    Obj_d_mes1020(0).input_cnt = Obj_d_mes1020(0).input_cnt + 1
                                End If

                                'mesd1020 入力データ・タイプ　inputdata_type　0:   1:As It 2:Changed 3: Changed Zero を追加 初期値０
                                Obj_d_mes1020(0).inputdata_type = "1"

                                If Obj_d_mes1020(0).stockstts_type = "1" Then
                                    DisplayQty = Obj_d_mes1020(0).stock_qty
                                    DisplayStatus = LangResources.B1010_12_available
                                ElseIf Obj_d_mes1020(0).stockstts_type = "2" Then
                                    DisplayQty = Obj_d_mes1020(0).inspect_qty
                                    DisplayStatus = LangResources.B1010_13_Inspection
                                ElseIf Obj_d_mes1020(0).stockstts_type = "3" Then
                                    DisplayQty = Obj_d_mes1020(0).keep_qty
                                    DisplayStatus = LangResources.B1010_14_pending
                                Else
                                    DisplayQty = Obj_d_mes1020(0).stock_qty
                                    DisplayStatus = LangResources.B1010_12_available
                                End If

                            Else
                                'Insert Case

                                'Condition For Create d_mes1020
                                Dim InsertNewRecordFord_mes1020 As New d_mes1020

                                InsertNewRecordFord_mes1020.plant_code = StrPlantCode
                                InsertNewRecordFord_mes1020.stocktake_date = Strdate
                                InsertNewRecordFord_mes1020.label_no = Obj_d_mes0040(0).label_no
                                InsertNewRecordFord_mes1020.item_code = Obj_d_mes0040(0).item_code
                                InsertNewRecordFord_mes1020.location_code = Obj_d_mes0040(0).location_code
                                InsertNewRecordFord_mes1020.shelf_no = Obj_d_mes0040(0).shelf_no
                                InsertNewRecordFord_mes1020.stock_qty = 0
                                InsertNewRecordFord_mes1020.inspect_qty = 0
                                InsertNewRecordFord_mes1020.keep_qty = 0
                                InsertNewRecordFord_mes1020.input_location_code = Obj_d_mes0040(0).location_code
                                InsertNewRecordFord_mes1020.input_shelf_no = Obj_d_mes0040(0).shelf_no
                                InsertNewRecordFord_mes1020.input_stock_qty = Obj_d_mes0040(0).stock_qty
                                InsertNewRecordFord_mes1020.input_inspect_qty = Obj_d_mes0040(0).inspect_qty
                                InsertNewRecordFord_mes1020.input_keep_qty = Obj_d_mes0040(0).keep_qty
                                InsertNewRecordFord_mes1020.unit_code = Obj_d_mes0040(0).unit_code
                                InsertNewRecordFord_mes1020.input_flg = "1"
                                InsertNewRecordFord_mes1020.input_cnt = 1

                                'mesd1020 入力データ・タイプ　inputdata_type　0:   1:As It 2:Changed 3: Changed Zero を追加 初期値０
                                InsertNewRecordFord_mes1020.inputdata_type = "1"
                                '※1	ラベルの	利用可能在庫数 <> 0 の場合、ステータス= 利用可能		
                                '検査中在庫 <> 0 の場合、ステータス＝ 検査中		
                                '保留在庫 <> 0 の場合、ステータス＝ 保留		
                                'その他の場合、ステータス＝ 利用可能	

                                If Obj_d_mes0040(0).stock_qty <> 0 Then
                                    InsertNewRecordFord_mes1020.stockstts_type = "1"
                                    DisplayQty = Obj_d_mes0040(0).stock_qty
                                    DisplayStatus = LangResources.B1010_12_available

                                ElseIf Obj_d_mes0040(0).inspect_qty <> 0 Then
                                    InsertNewRecordFord_mes1020.stockstts_type = "2"
                                    DisplayQty = Obj_d_mes0040(0).inspect_qty
                                    DisplayStatus = LangResources.B1010_13_Inspection

                                ElseIf Obj_d_mes0040(0).keep_qty <> 0 Then
                                    InsertNewRecordFord_mes1020.stockstts_type = "3"
                                    DisplayQty = Obj_d_mes0040(0).stock_qty
                                    DisplayStatus = LangResources.B1010_14_pending

                                Else
                                    InsertNewRecordFord_mes1020.stockstts_type = "1"
                                    DisplayQty = Obj_d_mes0040(0).keep_qty
                                    DisplayStatus = LangResources.B1010_12_available

                                End If

                                'Add To Table Object
                                db.d_mes1020.Add(InsertNewRecordFord_mes1020)

                            End If

                            ''If user didnt logout then it will acotomatically login
                            'Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

                            ''Condition For Create d_mes1020
                            'Dim InsertNewRecordFord_work0030 As New d_work0030

                            'InsertNewRecordFord_work0030.plant_code = StrPlantCode
                            'InsertNewRecordFord_work0030.instterm_ip = Ipaddress
                            'InsertNewRecordFord_work0030.label_no = Obj_d_mes0040(0).label_no
                            'InsertNewRecordFord_work0030.item_code = Obj_d_mes0040(0).item_code
                            ''Confirmation With jinway San
                            'InsertNewRecordFord_work0030.qty = Obj_d_mes0040(0).stock_qty + Obj_d_mes0040(0).inspect_qty + Obj_d_mes0040(0).keep_qty
                            'InsertNewRecordFord_work0030.unit_code = Obj_d_mes0040(0).unit_code
                            'InsertNewRecordFord_work0030.stocktake_date = Strdate
                            'InsertNewRecordFord_work0030.label_type = "1"

                            ''Add To Table Object
                            'db.d_work0030.Add(InsertNewRecordFord_work0030)

                            'Save To Database
                            db.Configuration.ValidateOnSaveEnabled = False
                            db.SaveChanges()
                            db.Configuration.ValidateOnSaveEnabled = True
                            Tras.Commit()

                            Response.Cookies.Add(New HttpCookie("location_code") With {.Value = objd_mes1020.location_code})

                            'labelno updated successfully →　lableno itemcode status qty Updated
                            TempData("B1010_Completed") = Obj_d_mes0040(0).label_no & " " & Obj_d_mes0040(0).item_code & " " & DisplayStatus & " " & DisplayQty.ToString("##0.###") & " " & LangResources.MSG_Comm_UpdateSucess
                            Return RedirectToAction("Index", "B1010_StockTakeInput")

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

                Else

                    Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" Order By t.location_code Ascending).ToList
                    If Obj_m_proc0030.Count > 0 Then
                        Dim m_proc0030item As New m_proc0030
                        m_proc0030item.location_code = ""
                        Obj_m_proc0030.Insert(0, m_proc0030item)
                        ViewBag.Location_code = Obj_m_proc0030

                    End If

                    objd_mes1020.Str_stock_qty = Nothing
                    objd_mes1020.unit_code = Nothing
                    objd_mes1020.Label_Status = Nothing
                    objd_mes1020.shelf_no = Nothing
                    objd_mes1020.item_code = Nothing

                    'NO Data
                    TempData("B1010_TxtLableError") = LangResources.MSG_B1010_01_NoDatainMes0040
                    Return View(objd_mes1020)

                End If

            Else

                'Show Error
                'safe Side write this Code
                'Model State.IsValid= Flase Will Show Error
                TempData("B1010_TxtLableError") = LangResources.MSG_B1010_02_EmptyLabel
                Return RedirectToAction("Index", "B1010_StockTakeInput")

            End If

        End Function

        ' GET: B1010_StockTakeInput/Details/5
        Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' GET: B1010_StockTakeInput/Create
        Function Create(ByVal d_mes1020 As d_mes1020) As ActionResult

            'If Session Is Nothing Or
            'Somehow Session Lost
            'Or SomeOne Open Form Directly By Changing URL
            'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
            If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                Return RedirectToAction("Index", "Login")
            Else
                StrPlantCode = Session("StrPlantCode")
            End If

            'For check If no Data Then Error
            Dim Obj_d_mes1010 = (From t In db.d_mes1010 Where t.plant_code = StrPlantCode).ToList

            If Obj_d_mes1010.Count > 0 Then
                Dim Obj_d_mes1010_First = db.d_mes1010.OrderByDescending(Function(m) m.stocktake_date).First()

                If Obj_d_mes1010_First.stocktake_status <> "1" Then
                    'need to Show Error if there is no data
                    Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
                Else
                    d_mes1020.stocktake_date = Obj_d_mes1010_First.stocktake_date

                    'For Drop Down Box
                    Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                          Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = d_mes1020.stocktake_date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                          Order By t.location_code Ascending
                                          Select t).ToList
                    If Obj_m_proc0030.Count > 0 Then
                        If Request.Cookies("location_code") IsNot Nothing Then
                            'Set To TextBox
                            d_mes1020.location_code = Request.Cookies("location_code").Values.ToString
                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030
                        Else
                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030
                        End If
                    End If

                End If

            Else
                'need to Show Error if there is no data
                Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
                'd_mes1020.stocktake_date = Date.Now
            End If

            'For Displaay On The Screen
            TempData("SetFocus") = "1"
            ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
            Return View(d_mes1020)

        End Function

        ' POST: B1010_StockTakeInput/Create
        <HttpPost()>
        Function Create(<Bind(Include:="PLANT_CODE,STOCKTAKE_DATE,LABEL_NO,ITEM_CODE,LOCATION_CODE,SHELF_NO,STOCK_QTY,STR_STOCK_QTY,INSPECT_QTY,KEEP_QTY,INPUT_LOCATION_CODE,INPUT_SHELF_NO,INPUT_STOCK_QTY,INPUT_INSPECT_QTY,INPUT_KEEP_QTY,UNIT_CODE,INPUT_FLG, LABEL_STATUS, STOCKSTTS_TYPE, INSTID, INSTDT, INSTTERM, INSTPRGNM, UPDTID, UPDTDT, UPDTTERM, UPDTPRGNM")> ByVal objd_mes1020 As d_mes1020, ByVal btnRegister As String) As ActionResult

            'If Session Is Nothing Or
            'Somehow Session Lost
            'Or SomeOne Open Form Directly By Changing URL
            'Session("StrPlantCode") Need to use Safeside Because it's Complusary in all Data retriving condition.
            If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                Return RedirectToAction("Index", "Login")
            Else
                StrPlantCode = Session("StrPlantCode")
            End If

            'Check Again if Status Change Then Error
            Dim Obj_d_mes1010 = (From t In db.d_mes1010 Where t.plant_code = StrPlantCode).ToList
            Dim Obj_d_mes1010_First = Nothing
            If Obj_d_mes1010.Count > 0 Then
                Obj_d_mes1010_First = db.d_mes1010.OrderByDescending(Function(m) m.stocktake_date).First()

                If Obj_d_mes1010_First.stocktake_status <> "1" Then
                    'need to Show Error if there is no data
                    Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
                End If
            Else
                'need to Show Error if there is no data
                Return RedirectToAction("Error_stocktake_status", "B1010_StockTakeInput")
            End If

            Dim Stock_take_Date As Date = Obj_d_mes1010_First.stocktake_date
            'Check Location Code Exist or Not
            If btnRegister = "2" Then

                'Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" AndAlso t.location_code = objd_mes1020.location_code).ToList
                Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                      Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1" AndAlso
                                              t.location_code = objd_mes1020.location_code
                                      Order By t.location_code Ascending
                                      Select t).ToList

                If Obj_m_proc0030.Count = 0 Then
                    TempData("B1010_Emptylocation_code") = LangResources.MSG_B1030_10_LocationCodeExist
                    'Else
                    '	Response.Cookies("location_code").Value = objd_mes1020.location_code
                End If
                'Drop Down Menu
                'For Drop Down Box
                Dim Obj_m_proc0030_1 = (From t In db.m_proc0030,
                                              d_mes1130 In db.d_mes1130
                                        Where t.plant_code = StrPlantCode AndAlso
                                              t.location_type = "2" AndAlso
                                              d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                              d_mes1130.location_code = t.location_code AndAlso
                                              d_mes1130.st_input_type = "1"
                                        Order By t.location_code Ascending
                                        Select t).ToList
                If Obj_m_proc0030_1.Count > 0 Then
                    Dim m_proc0030item As New m_proc0030
                    m_proc0030item.location_code = ""
                    Obj_m_proc0030_1.Insert(0, m_proc0030item)
                    ViewBag.Location_code = Obj_m_proc0030_1
                End If
                ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                Return View(objd_mes1020)
            End If

            'If label Not Nothing Then Process
            If objd_mes1020.label_no IsNot Nothing Then

                'Label No
                Dim StrLabel As String = objd_mes1020.label_no

                Dim Obj_d_mes0040 = (From t In db.d_mes0040 Where t.plant_code = StrPlantCode AndAlso t.label_no = StrLabel AndAlso t.delete_flg = "0").ToList

                If Obj_d_mes0040.Count > 0 Then

                    'Check Error
                    If Obj_d_mes0040(0).location_code <> objd_mes1020.location_code Then

                        Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" Order By t.location_code Ascending).ToList
                        If Obj_m_proc0030.Count > 0 Then
                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030

                        End If

                        TempData("B1010_TxtLableError") = LangResources.MSG_B1030_09_LocationCodeDiff
                        objd_mes1020.Str_stock_qty = Nothing
                        objd_mes1020.unit_code = Nothing
                        objd_mes1020.Label_Status = Nothing
                        objd_mes1020.shelf_no = Nothing
                        objd_mes1020.item_code = Nothing
                        ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                        Return View(objd_mes1020)
                    End If

                    'Set Value to The Controlls
                    If btnRegister = "1" Then

                        ModelState.Clear()

                        'Tanauroshi Hakko  Data
                        If Obj_d_mes0040(0).sttime_input_flg = "1" Then

                            objd_mes1020.Str_stock_qty = 0

                            objd_mes1020.unit_code = Obj_d_mes0040(0).unit_code

                            Dim Str_Label_Status As String = Nothing
                            If Obj_d_mes0040(0).stock_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_12_available

                            ElseIf Obj_d_mes0040(0).inspect_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_13_Inspection

                            ElseIf Obj_d_mes0040(0).keep_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_14_pending

                            Else
                                Str_Label_Status = LangResources.B1010_12_available
                            End If

                            objd_mes1020.Label_Status = Str_Label_Status
                            objd_mes1020.location_code = Obj_d_mes0040(0).location_code
                            objd_mes1020.shelf_no = Obj_d_mes0040(0).shelf_no
                            objd_mes1020.item_code = Obj_d_mes0040(0).item_code

                            'No Tanauroshi Hakko
                        Else

                            'Cannot Input the ID Tag that is inserted after Stocktake started.
                            'If mes0040.instdt > mes1010.instdt And mes0040.sttime_input_flag <> 1 Then,
                            If Obj_d_mes0040(0).instdt > Obj_d_mes1010_First.instdt Then

                                Dim Strdate = Date.Parse(objd_mes1020.stocktake_date)
                                Dim Obj_d_mes1020 = (From t In db.d_mes1020 Where t.plant_code = StrPlantCode AndAlso t.label_no = StrLabel AndAlso t.stocktake_date = Strdate).ToList

                                If Obj_d_mes1020.Count = 0 Then

                                    Dim Obj_m_proc0030_c = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" Order By t.location_code Ascending).ToList
                                    If Obj_m_proc0030_c.Count > 0 Then
                                        Dim m_proc0030item As New m_proc0030
                                        m_proc0030item.location_code = ""
                                        Obj_m_proc0030_c.Insert(0, m_proc0030item)
                                        ViewBag.Location_code = Obj_m_proc0030_c

                                    End If

                                    TempData("B1010_TxtLableError") = LangResources.MSG_B1030_08_CannotEnterIDTag
                                    objd_mes1020.Str_stock_qty = Nothing
                                    objd_mes1020.unit_code = Nothing
                                    objd_mes1020.Label_Status = Nothing
                                    objd_mes1020.shelf_no = Nothing
                                    objd_mes1020.item_code = Nothing
                                    ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                                    Return View(objd_mes1020)

                                End If

                            End If

                            objd_mes1020.unit_code = Obj_d_mes0040(0).unit_code

                            Dim Str_Label_Status As String = Nothing
                            If Obj_d_mes0040(0).stock_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_12_available
                                objd_mes1020.Str_stock_qty = Obj_d_mes0040(0).stock_qty.ToString("##0.###")

                            ElseIf Obj_d_mes0040(0).inspect_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_13_Inspection
                                objd_mes1020.Str_stock_qty = Obj_d_mes0040(0).inspect_qty.ToString("##0.###")

                            ElseIf Obj_d_mes0040(0).keep_qty <> 0 Then
                                Str_Label_Status = LangResources.B1010_14_pending
                                objd_mes1020.Str_stock_qty = Obj_d_mes0040(0).keep_qty.ToString("##0.###")

                            Else
                                Str_Label_Status = LangResources.B1010_12_available
                                objd_mes1020.Str_stock_qty = 0
                            End If

                            objd_mes1020.Label_Status = Str_Label_Status
                            objd_mes1020.shelf_no = Obj_d_mes0040(0).shelf_no
                            objd_mes1020.item_code = Obj_d_mes0040(0).item_code

                        End If

                        Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" Order By t.location_code Ascending).ToList
                        If Obj_m_proc0030.Count > 0 Then
                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030

                        End If

                        'For Displaay On The Screen
                        TempData("SetFocus") = "2"
                        ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                        Return View(objd_mes1020)

                    End If

                    ' If Somehow Wrong Word Entered so safe side Need to check
                    If Not objd_mes1020.Str_stock_qty Is Nothing AndAlso IsNumeric(objd_mes1020.Str_stock_qty) = False Then
                        TempData("SetFocus") = "2"

                        Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" Order By t.location_code Ascending).ToList
                        If Obj_m_proc0030.Count > 0 Then

                            Dim m_proc0030item As New m_proc0030
                            m_proc0030item.location_code = ""
                            Obj_m_proc0030.Insert(0, m_proc0030item)
                            ViewBag.Location_code = Obj_m_proc0030

                        End If

                        TempData("B1010_Txtstock_qtyError") = LangResources.MSG_B1010_05_InvalidNumber
                        ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                        Return View(objd_mes1020)
                    End If

                    'Check Qty Cases
                    Dim DecConvrtqty As Decimal = Decimal.Parse(IIf(objd_mes1020.Str_stock_qty Is Nothing, 0, objd_mes1020.Str_stock_qty))

                    'if Zero then Display error
                    If DecConvrtqty = 0 Then

                        'TanauroshiHakko label
                        If Obj_d_mes0040(0).sttime_input_flg = "1" Then
                            TempData("SetFocus") = "2"

                            Dim Obj_m_proc0030 = (From t In db.m_proc0030 Where t.plant_code = StrPlantCode AndAlso t.location_type = "2" Order By t.location_code Ascending).ToList
                            If Obj_m_proc0030.Count > 0 Then

                                Dim m_proc0030item As New m_proc0030
                                m_proc0030item.location_code = ""
                                Obj_m_proc0030.Insert(0, m_proc0030item)
                                ViewBag.Location_code = Obj_m_proc0030

                            End If

                            TempData("B1010_Txtstock_qtyError") = LangResources.MSG_B1010_03_TanauroshiIssuedLabel
                            ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                            Return View(objd_mes1020)
                        Else
                            TempData("SetFocus") = "2"

                            'For Drop Down Box
                            Dim Obj_m_proc0030 = (From t In db.m_proc0030,
                                                    d_mes1130 In db.d_mes1130
                                                  Where t.plant_code = StrPlantCode AndAlso
                                                    t.location_type = "2" AndAlso
                                                    d_mes1130.stocktake_date = Stock_take_Date AndAlso
                                                    d_mes1130.location_code = t.location_code AndAlso
                                                    d_mes1130.st_input_type = "1"
                                                  Order By t.location_code Ascending
                                                  Select t).ToList
                            If Obj_m_proc0030.Count > 0 Then

                                Dim m_proc0030item As New m_proc0030
                                m_proc0030item.location_code = ""
                                Obj_m_proc0030.Insert(0, m_proc0030item)
                                ViewBag.Location_code = Obj_m_proc0030

                            End If

                            TempData("B1010_Txtstock_qtyError") = LangResources.MSG_B1010_06_CanNotEnterZero
                            ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                            Return View(objd_mes1020)
                        End If

                    End If

                    If btnRegister = 3 Then

                        Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
                        Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress
                        Dim Tras As DbContextTransaction = db.Database.BeginTransaction

                        Try

                            'For Client Info
                            Dim cnt = db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

                            'Main Logic Start Here
                            'Check if record is already there Then update OtherWise Insert
                            Dim Strdate = Date.Parse(objd_mes1020.stocktake_date)
                            Dim Obj_d_mes1020 = (From t In db.d_mes1020 Where t.plant_code = StrPlantCode AndAlso t.label_no = StrLabel AndAlso t.stocktake_date = Strdate).ToList

                            Dim Str_Label_Status As String = Nothing
                            'Update Case
                            If Obj_d_mes1020.Count > 0 Then

                                Obj_d_mes1020(0).input_location_code = Obj_d_mes1020(0).location_code
                                Obj_d_mes1020(0).input_shelf_no = Obj_d_mes1020(0).shelf_no

                                '入力卸利用可能評価在庫←ステータス = 利用可能の場合、入力数量。その他、0
                                '入力品質検査中在庫←ステータス = 検査中の場合、入力数量。その他、0
                                '入力保留在庫←ステータス = 保留の場合、入力数量。その他、0	
                                If Obj_d_mes0040(0).stock_qty <> 0 Then
                                    Obj_d_mes1020(0).input_stock_qty = DecConvrtqty
                                    Obj_d_mes1020(0).input_inspect_qty = 0
                                    Obj_d_mes1020(0).input_keep_qty = 0
                                    Str_Label_Status = LangResources.B1010_12_available
                                ElseIf Obj_d_mes0040(0).inspect_qty <> 0 Then
                                    Obj_d_mes1020(0).input_stock_qty = 0
                                    Obj_d_mes1020(0).input_inspect_qty = DecConvrtqty
                                    Obj_d_mes1020(0).input_keep_qty = 0
                                    Str_Label_Status = LangResources.B1010_13_Inspection
                                ElseIf Obj_d_mes0040(0).keep_qty <> 0 Then
                                    Obj_d_mes1020(0).input_stock_qty = 0
                                    Obj_d_mes1020(0).input_inspect_qty = 0
                                    Obj_d_mes1020(0).input_keep_qty = DecConvrtqty
                                    Str_Label_Status = LangResources.B1010_14_pending
                                Else
                                    Obj_d_mes1020(0).input_stock_qty = DecConvrtqty
                                    Obj_d_mes1020(0).input_inspect_qty = 0
                                    Obj_d_mes1020(0).input_keep_qty = 0
                                    Str_Label_Status = LangResources.B1010_12_available
                                End If

                                Obj_d_mes1020(0).input_flg = "1"
                                If Obj_d_mes1020(0).input_cnt Is Nothing Then
                                    Obj_d_mes1020(0).input_cnt = 1
                                Else
                                    Obj_d_mes1020(0).input_cnt = Obj_d_mes1020(0).input_cnt + 1
                                End If

                                'mesd1020 入力データ・タイプ　inputdata_type　0:   1:As It 2:Changed 3: Changed Zero を追加 初期値０
                                Obj_d_mes1020(0).inputdata_type = "2"

                            Else

                                'Insert Case
                                'Condition For Create d_mes1020
                                Dim InsertNewRecordFord_mes1020 As New d_mes1020

                                InsertNewRecordFord_mes1020.plant_code = StrPlantCode
                                InsertNewRecordFord_mes1020.stocktake_date = Strdate
                                InsertNewRecordFord_mes1020.label_no = Obj_d_mes0040(0).label_no
                                InsertNewRecordFord_mes1020.item_code = Obj_d_mes0040(0).item_code
                                InsertNewRecordFord_mes1020.location_code = Obj_d_mes0040(0).location_code
                                InsertNewRecordFord_mes1020.shelf_no = Obj_d_mes0040(0).shelf_no
                                InsertNewRecordFord_mes1020.stock_qty = 0
                                InsertNewRecordFord_mes1020.inspect_qty = 0
                                InsertNewRecordFord_mes1020.keep_qty = 0
                                InsertNewRecordFord_mes1020.input_location_code = Obj_d_mes0040(0).location_code
                                InsertNewRecordFord_mes1020.input_shelf_no = Obj_d_mes0040(0).shelf_no
                                '入力卸利用可能評価在庫	←ステータス = 利用可能の場合、入力数量。その他、0
                                '入力品質検査中在庫←ステータス = 検査中の場合、入力数量。その他、0
                                '入力保留在庫←ステータス = 保留の場合、入力数量。その他、0	
                                If Obj_d_mes0040(0).stock_qty <> 0 Then
                                    InsertNewRecordFord_mes1020.input_stock_qty = DecConvrtqty
                                    InsertNewRecordFord_mes1020.input_inspect_qty = 0
                                    InsertNewRecordFord_mes1020.input_keep_qty = 0
                                    InsertNewRecordFord_mes1020.stockstts_type = "1"
                                    Str_Label_Status = LangResources.B1010_12_available
                                ElseIf Obj_d_mes0040(0).inspect_qty <> 0 Then
                                    InsertNewRecordFord_mes1020.input_stock_qty = 0
                                    InsertNewRecordFord_mes1020.input_inspect_qty = DecConvrtqty
                                    InsertNewRecordFord_mes1020.input_keep_qty = 0
                                    InsertNewRecordFord_mes1020.stockstts_type = "2"
                                    Str_Label_Status = LangResources.B1010_13_Inspection
                                ElseIf Obj_d_mes0040(0).keep_qty <> 0 Then
                                    InsertNewRecordFord_mes1020.input_stock_qty = 0
                                    InsertNewRecordFord_mes1020.input_inspect_qty = 0
                                    InsertNewRecordFord_mes1020.input_keep_qty = DecConvrtqty
                                    InsertNewRecordFord_mes1020.stockstts_type = "3"
                                    Str_Label_Status = LangResources.B1010_14_pending
                                Else
                                    InsertNewRecordFord_mes1020.input_stock_qty = DecConvrtqty
                                    InsertNewRecordFord_mes1020.input_inspect_qty = 0
                                    InsertNewRecordFord_mes1020.input_keep_qty = 0
                                    InsertNewRecordFord_mes1020.stockstts_type = "1"
                                    Str_Label_Status = LangResources.B1010_12_available
                                End If

                                InsertNewRecordFord_mes1020.unit_code = Obj_d_mes0040(0).unit_code
                                InsertNewRecordFord_mes1020.input_flg = "1"
                                InsertNewRecordFord_mes1020.input_cnt = 1

                                'mesd1020 入力データ・タイプ　inputdata_type　0:   1:As It 2:Changed 3: Changed Zero を追加 初期値０
                                InsertNewRecordFord_mes1020.inputdata_type = "2"

                                'Add To Table Object
                                db.d_mes1020.Add(InsertNewRecordFord_mes1020)

                            End If

                            Dim objd_mes1120 = (From m In db.d_mes1120 Where m.stocktake_date = Strdate AndAlso m.plant_code = StrPlantCode).ToList

                            'Web棚卸登録 変更
                            '・差異反映なしの時、ID Tag、Changed、As Isラベル印刷しない
                            If objd_mes1120 IsNot Nothing AndAlso objd_mes1120.Count > 0 AndAlso objd_mes1120(0).stmat_stock_updt_type = "1" Then

                                '印刷
                                'If user didnt logout then it will acotomatically login
                                Dim Ipaddress = Request.ServerVariables("REMOTE_ADDR")

                                'Condition For Create d_mes1020
                                Dim InsertNewRecordFord_work0030 As New d_work0030

                                InsertNewRecordFord_work0030.plant_code = StrPlantCode
                                InsertNewRecordFord_work0030.instterm_ip = Ipaddress
                                InsertNewRecordFord_work0030.label_no = Obj_d_mes0040(0).label_no
                                InsertNewRecordFord_work0030.item_code = Obj_d_mes0040(0).item_code
                                InsertNewRecordFord_work0030.qty = DecConvrtqty
                                InsertNewRecordFord_work0030.unit_code = Obj_d_mes0040(0).unit_code
                                InsertNewRecordFord_work0030.stocktake_date = Strdate
                                'when update Changed form, label_type = 1 when qty Is Not changed . label_type = 2 when qty Is changed.
                                If Obj_d_mes0040(0).stock_qty + Obj_d_mes0040(0).inspect_qty + Obj_d_mes0040(0).keep_qty = DecConvrtqty Then
                                    InsertNewRecordFord_work0030.label_type = "1"
                                Else
                                    InsertNewRecordFord_work0030.label_type = "2"

                                    'Create d_work0010
                                    'Condition For Create d_work0010
                                    Dim InsertNewRecordFord_work0010 As New d_work0010

                                    Dim StrItemCode = Obj_d_mes0040(0).item_code
                                    InsertNewRecordFord_work0010.instterm_ip = Ipaddress
                                    InsertNewRecordFord_work0010.label_no = Obj_d_mes0040(0).label_no
                                    InsertNewRecordFord_work0010.item_code = StrItemCode

                                    Dim objm_item0010 = (From m In db.m_item0010 Where m.item_code = StrItemCode AndAlso m.plant_code = StrPlantCode).ToList

                                    If objm_item0010.Count > 0 Then
                                        InsertNewRecordFord_work0010.item_name = objm_item0010(0).item_name
                                    End If

                                    InsertNewRecordFord_work0010.label_qty = DecConvrtqty
                                    InsertNewRecordFord_work0010.unit_code = Obj_d_mes0040(0).unit_code

                                    Dim strslipno As String = Obj_d_mes0040(0).slip_no
                                    Dim objd_mes0150 = (From t In db.d_mes0150 Where t.slip_no = strslipno AndAlso t.delete_flg = 0 AndAlso t.plant_code = StrPlantCode).ToList
                                    Dim Str_Po_Sub_PO_no As String = Nothing
                                    If objd_mes0150.Count > 0 Then
                                        Str_Po_Sub_PO_no = objd_mes0150(0).po_no & "-" & objd_mes0150(0).po_sub_no
                                        InsertNewRecordFord_work0010.receive_date = objd_mes0150(0).receive_date
                                    Else
                                        InsertNewRecordFord_work0010.receive_date = Obj_d_mes0040(0).print_datetime
                                    End If

                                    InsertNewRecordFord_work0010.both_po_no = Str_Po_Sub_PO_no
                                    InsertNewRecordFord_work0010.location_code = Obj_d_mes0040(0).location_code

                                    'Take Shelf Code From  品目棚番マスタ	 M_PROC0060 With ItemKey And Location_Code
                                    InsertNewRecordFord_work0010.shelf_no = Obj_d_mes0040(0).shelf_no


                                    Dim objm_proc0020 = (From t In db.m_item0020 Where t.item_code = StrItemCode AndAlso t.plant_code = StrPlantCode).ToList
                                    'From M0020 Master, トレース区分	TRACE_TYPE 1:対象 2:非対象
                                    If objm_proc0020.Count > 0 Then
                                        InsertNewRecordFord_work0010.trace_type = objm_proc0020(0).trace_type
                                    Else
                                        InsertNewRecordFord_work0010.trace_type = "1"
                                    End If

                                    'stock_type
                                    If Str_Po_Sub_PO_no IsNot Nothing Then
                                        Dim po_no = objd_mes0150(0).po_no
                                        Dim po_sub_no = objd_mes0150(0).po_sub_no
                                        Dim objd_sap0030 = (From t In db.d_sap0050 Where t.po_no = po_no AndAlso t.po_sub_no = po_sub_no AndAlso t.plant_code = StrPlantCode).ToList
                                        If objd_sap0030.Count > 0 Then
                                            InsertNewRecordFord_work0010.stock_type = objd_sap0030(0).stock_type
                                        Else
                                            InsertNewRecordFord_work0010.stock_type = "1"
                                        End If
                                    Else
                                        InsertNewRecordFord_work0010.stock_type = "1"
                                    End If

                                    InsertNewRecordFord_work0010.plant_code = StrPlantCode

                                    'Condition added
                                    If Obj_d_mes0040(0).stock_qty <> 0 Then
                                        InsertNewRecordFord_work0010.stockstts_type = "1"
                                        Str_Label_Status = LangResources.B1010_12_available
                                    ElseIf Obj_d_mes0040(0).inspect_qty <> 0 Then
                                        InsertNewRecordFord_work0010.stockstts_type = "2"
                                        Str_Label_Status = LangResources.B1010_13_Inspection
                                    ElseIf Obj_d_mes0040(0).keep_qty <> 0 Then
                                        InsertNewRecordFord_work0010.stockstts_type = "3"
                                        Str_Label_Status = LangResources.B1010_14_pending
                                    Else
                                        InsertNewRecordFord_work0010.stockstts_type = "1"
                                        Str_Label_Status = LangResources.B1010_12_available
                                    End If

                                    'Add To Table Object
                                    db.d_work0010.Add(InsertNewRecordFord_work0010)

                                End If

                                'Add To Table Object
                                db.d_work0030.Add(InsertNewRecordFord_work0030)

                            End If

                            'Save To Database
                            db.Configuration.ValidateOnSaveEnabled = False
                            db.SaveChanges()
                            db.Configuration.ValidateOnSaveEnabled = True
                            Tras.Commit()

                            Response.Cookies.Add(New HttpCookie("location_code") With {.Value = objd_mes1020.location_code})

                            TempData("B1010_Completed") = Obj_d_mes0040(0).label_no & " " & Obj_d_mes0040(0).item_code & " " & Str_Label_Status & " " & DecConvrtqty.ToString("##0.###") & " " & LangResources.MSG_Comm_UpdateSucess
                            Return RedirectToAction("Index", "B1010_StockTakeInput")

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

                Else

                    'NO Data
                    ModelState.Clear()
                    TempData("B1010_TxtLableError") = LangResources.MSG_B1010_01_NoDatainMes0040
                    objd_mes1020.Str_stock_qty = Nothing
                    objd_mes1020.unit_code = Nothing
                    objd_mes1020.Label_Status = Nothing
                    objd_mes1020.shelf_no = Nothing
                    objd_mes1020.item_code = Nothing
                    ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
                    Return View(objd_mes1020)

                End If

            End If

        End Function

        '' GET: B1010_StockTakeInput/Error_stocktake_status/5
        Function Error_stocktake_status() As ActionResult
            ViewData!ID = LangResources.B1010_01_Fn_StockTakeInput
            Return View()
        End Function

        ' GET: B1010_StockTakeInput/Edit/5
        Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: B1010_StockTakeInput/Edit/5
        <HttpPost()>
        Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add update logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        ' GET: B1010_StockTakeInput/Delete/5
        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: B1010_StockTakeInput/Delete/5
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