Imports System.Data.Entity
Imports System.Web.Mvc
Imports MES_WEB.My.Resources
Imports Npgsql

Namespace Controllers
    Public Class C1015_OneByOneJessikiCancelController
        Inherits Controller

        Dim db As New Model1
        Dim StrPlantCode As String = Nothing

        ' GET: C1010_OneByOneJessiki
        Function Index(ByVal objm_proc0070 As m_proc0070) As ActionResult

            If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                Return RedirectToAction("Index", "Login")
            Else
                StrPlantCode = Session("StrPlantCode")
            End If
            ViewData!ID = LangResources.C1015_01_Fn_OneByOneJessiki

            Dim Obj_m_proc0070 = (From m_proc0070 In db.m_proc0070
                                  Where m_proc0070.plant_code = StrPlantCode AndAlso
                                      m_proc0070.ser_set_type = "3" AndAlso
                                      m_proc0070.work_result_type = "4"
                                  Select m_proc0070
                                  Order By m_proc0070.man_stat_cd,
                                      m_proc0070.workctr_code).Select(Function(x) x.man_stat_cd).ToList
            If Obj_m_proc0070.Count > 0 Then

                Obj_m_proc0070.Insert(0, "")
                objm_proc0070.man_stat_cd_List = Obj_m_proc0070

            End If
            If Request.Cookies("man_stat_cd") IsNot Nothing Then
                objm_proc0070.man_stat_cd = Request.Cookies("man_stat_cd").Values.ToString
            End If

            Return View(objm_proc0070)

        End Function

        <HttpPost()>
        Function Index(ByVal objm_proc0070 As m_proc0070, ByVal btnhidden As String) As ActionResult

            Try
                If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                    Return RedirectToAction("Index", "Login")
                Else
                    StrPlantCode = Session("StrPlantCode")
                End If
                ViewData!ID = LangResources.C1015_01_Fn_OneByOneJessiki
                Dim man_stat_cd_List = (From m_proc0070 In db.m_proc0070
                                        Where m_proc0070.plant_code = StrPlantCode AndAlso
                                            m_proc0070.ser_set_type = "3" AndAlso
                                      m_proc0070.work_result_type = "4"
                                        Select m_proc0070.man_stat_cd,
                                            m_proc0070.workctr_code).ToList
                objm_proc0070.man_stat_cd_List = man_stat_cd_List.Select(Function(x) x.man_stat_cd).ToList
                objm_proc0070.man_stat_cd_List.Insert(0, "")

                If {"2", "3"}.Contains(btnhidden) Then
                    Dim strman_stat_cd = objm_proc0070.man_stat_cd

                    Dim man_stat_cd_List_ALL_DETAILS = (From m_proc0070 In db.m_proc0070
                                                        Where m_proc0070.plant_code = StrPlantCode AndAlso
                                                            m_proc0070.man_stat_cd = strman_stat_cd
                                                        Select m_proc0070).ToList

                    If Not (man_stat_cd_List_ALL_DETAILS IsNot Nothing AndAlso man_stat_cd_List_ALL_DETAILS.Count > 0) Then
                        TempData("error_man_stat_code_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
                    ElseIf (man_stat_cd_List_ALL_DETAILS(0).ser_set_type <> "3") Then
                        TempData("error_man_stat_code_NODataNotFound") = LangResources.MSG_C1010_07_MStationCreationSetting
                    ElseIf (man_stat_cd_List_ALL_DETAILS(0).work_result_type <> "4") Then
                        TempData("error_man_stat_code_NODataNotFound") = LangResources.MSG_C1010_13_InputMStationWrongType
                    End If

                    If {"2"}.Contains(btnhidden) Then
                        Return View(objm_proc0070)
                    End If


                End If

                If {"3"}.Contains(btnhidden) Then

                    Dim m_item0010 = (From t In db.m_item0010 Where t.item_code = objm_proc0070.item_code AndAlso t.plant_code = StrPlantCode).ToList
                    If m_item0010 IsNot Nothing AndAlso m_item0010.Count > 0 Then
                        objm_proc0070.item_name = m_item0010(0).item_name
                    Else
                        'Display Error
                        TempData("errorItemCode_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
                        Return View(objm_proc0070)
                    End If

                    If TempData("error_man_stat_code_NODataNotFound") IsNot Nothing AndAlso TempData("error_man_stat_code_NODataNotFound") <> "" Then
                        Return View(objm_proc0070)
                    End If

                End If
                If Request.Cookies("man_stat_cd") Is Nothing Then
                    Response.Cookies.Add(New HttpCookie("man_stat_cd") With {.Value = objm_proc0070.man_stat_cd, .Expires = DateTime.MaxValue})
                Else
                    Response.Cookies("man_stat_cd").Value = objm_proc0070.man_stat_cd
                End If
                objm_proc0070.workctr_code = man_stat_cd_List.Where(Function(w) w.man_stat_cd = objm_proc0070.man_stat_cd)(0).workctr_code
                Return RedirectToAction("Create", "C1015_OneByOneJessikiCancel", objm_proc0070)

            Catch
                Return View()
            End Try

            Return View(objm_proc0070)
        End Function

        ' GET: C1010_OneByOneJessiki/Create
        Function Create(ByVal m_proc0070 As m_proc0070) As ActionResult

            If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                Return RedirectToAction("Index", "Login")
            Else
                StrPlantCode = Session("StrPlantCode")
            End If
            ViewData!ID = LangResources.C1015_01_Fn_OneByOneJessiki
            Return View(m_proc0070)

        End Function

        ' POST: C1010_OneByOneJessiki/Create
        <HttpPost()>
        Function Create(ByVal obj_m_proc0070 As m_proc0070, ByVal btnRegister As String, obj_C1010_SerialNoInfo2 As C1010_SerialNoInfo) As ActionResult

            If Session("LoginUserid") Is Nothing OrElse Session("StrPlantCode") Is Nothing Then
                Return RedirectToAction("Index", "Login")
            Else
                StrPlantCode = Session("StrPlantCode")
            End If
            ViewData!ID = LangResources.C1015_01_Fn_OneByOneJessiki

            '2020/06/16 Closing Date Check
            Dim DateFormat_Original As String = Session("DateFormat_Original")
            Dim dt_date As Date = Date.Now
            Dim Str_date As String = dt_date.ToString(DateFormat_Original)
            'Check Closing Date Status
            Dim check_closing_date = db.fn_check_closing_date(Str_date, DateFormat_Original)
            If check_closing_date = False Then
                TempData("error_serial_no_NODataNotFound") = LangResources.MSG_Comm_CheckClosingDate
                ViewData!ID = LangResources.A1010_01_Fn_RegisterReceiving
                Return View(obj_m_proc0070)
            End If

            Dim d_mes0190_result = (From d_mes0120 In db.d_mes0120,
                                             d_mes0190 In db.d_mes0190,
                                             d_mes0110 In db.d_mes0110
                                    Where d_mes0190.work_result_no = d_mes0120.work_result_no AndAlso
                                            d_mes0190.plant_code = d_mes0120.plant_code AndAlso
                                            d_mes0190.plant_code = StrPlantCode AndAlso
                                            d_mes0190.serial_no = obj_m_proc0070.serial_no AndAlso
                                             d_mes0110.work_order_no = d_mes0120.work_order_no
                                    Select d_mes0120.work_order_no,
                                            d_mes0120.unit_code,
                                            d_mes0120.man_stat_cd,
                                            d_mes0110.location_code,
                                            d_mes0110.act_result_status,
                                            d_mes0110.item_code,
                                            d_mes0120.prod_individual_id,
                                            d_mes0190.serial_no,
                                            d_mes0190.qty,
                                            d_mes0110.workctr_code,
                                            d_mes0120.work_result_no).ToList

            Dim update_d_mes0280 = Nothing
            If d_mes0190_result IsNot Nothing AndAlso d_mes0190_result.Count > 0 Then

                Dim prod_individualno = d_mes0190_result(0).prod_individual_id
                update_d_mes0280 = (From d_mes0280 In db.d_mes0280
                                    Where d_mes0280.prod_individual_id = prod_individualno
                                    Select d_mes0280).ToList

                If d_mes0190_result(0).item_code <> obj_m_proc0070.item_code Then
                    TempData("error_serial_no_NODataNotFound") = LangResources.MSG_C1010_06_DifferentItemCode
                    Return View(obj_m_proc0070)

                ElseIf d_mes0190_result(0).workctr_code <> obj_m_proc0070.workctr_code Then
                    TempData("error_serial_no_NODataNotFound") = LangResources.MSG_C1010_08_DifferentWorkCtr
                    Return View(obj_m_proc0070)

                ElseIf update_d_mes0280 IsNot Nothing AndAlso update_d_mes0280.Count > 0 AndAlso update_d_mes0280(0).act_result_status = "3" Then
                    TempData("error_serial_no_NODataNotFound") = LangResources.MSG_C1010_09_Prod_individual_no_zerokan
                    Return View(obj_m_proc0070)
                    'No Need to Because jisseki will perform on Last M Station Also
                    'ElseIf d_mes0190_result(0).act_result_status = "2" Then
                    '    TempData("error_serial_no_NODataNotFound") = LangResources.MSG_C1010_12_InputSerialNoPTComplete
                    '    Return View(obj_m_proc0070)

                Else

                    Dim linear_man_stat_cd = (From m_proc0070 In db.m_proc0070,
                                                 m_proc007001 In db.m_proc0070
                                              Where (m_proc007001.workctr_code = m_proc0070.workctr_code AndAlso
                                                    m_proc0070.man_stat_cd = obj_m_proc0070.man_stat_cd AndAlso
                                                    m_proc0070.plant_code = StrPlantCode AndAlso
                                                    m_proc0070.plant_code = m_proc007001.plant_code AndAlso
                                                    m_proc0070.point_type < m_proc007001.point_type) OrElse
                                                  (m_proc007001.workctr_code = m_proc0070.workctr_code AndAlso
                                                    m_proc0070.man_stat_cd = obj_m_proc0070.man_stat_cd AndAlso
                                                    m_proc0070.plant_code = StrPlantCode AndAlso
                                                    m_proc0070.plant_code = m_proc007001.plant_code AndAlso
                                                    m_proc0070.point_type = m_proc007001.point_type AndAlso
                                                    m_proc0070.seq > m_proc007001.seq)
                                              Select m_proc007001.man_stat_cd).ToList

                    For Each mstation As String In d_mes0190_result.Select(Function(x) x.man_stat_cd)
                        If linear_man_stat_cd.Contains(mstation) Then
                            TempData("error_serial_no_NODataNotFound") = LangResources.MSG_C1010_10_CompleteSerialNoAboveMstation
                            Return View(obj_m_proc0070)
                        End If
                    Next

                    If d_mes0190_result.Where(Function(x) x.man_stat_cd = obj_m_proc0070.man_stat_cd AndAlso x.serial_no = obj_m_proc0070.serial_no).Count > 0 Then

                        Dim RecordOfC1010_SerialNoInfo As New C1010_SerialNoInfo
                        RecordOfC1010_SerialNoInfo.serial_no = obj_m_proc0070.serial_no
                        RecordOfC1010_SerialNoInfo.register_time = Date.Now.ToString("HH:mm")

                        Dim TableForB1020 As New List(Of C1010_SerialNoInfo)

                        If obj_m_proc0070.obj_C1010_SerialNoInfo IsNot Nothing Then

                            RecordOfC1010_SerialNoInfo.number = obj_m_proc0070.obj_C1010_SerialNoInfo.Count + 1
                            TableForB1020.Add(RecordOfC1010_SerialNoInfo)

                            For Each item As C1010_SerialNoInfo In obj_m_proc0070.obj_C1010_SerialNoInfo
                                TableForB1020.Add(item)
                            Next
                        Else
                            RecordOfC1010_SerialNoInfo.number = 1
                            TableForB1020.Add(RecordOfC1010_SerialNoInfo)
                        End If

                        obj_m_proc0070.obj_C1010_SerialNoInfo = TableForB1020

                        Dim m_proc0070_result = (From m_proc0070 In db.m_proc0070
                                                 Where m_proc0070.man_stat_cd = obj_m_proc0070.man_stat_cd AndAlso
                                                       m_proc0070.plant_code = StrPlantCode
                                                 Select m_proc0070.man_stat_cd,
                                                     m_proc0070.sap_if_type).ToList

                        Dim Npgsqlpara1 As New NpgsqlParameter("av_clientinfo", NpgsqlTypes.NpgsqlDbType.Varchar, 128)
                        Npgsqlpara1.Value = Session("LoginUserid") & "," & Request.Browser.Browser & " " & Request.Browser.Version & "," & Request.UserHostAddress

                        Dim Tras As DbContextTransaction = db.Database.BeginTransaction
                        Try



                            'For Client Info
                            Dim cnt = db.Database.ExecuteSqlCommand("Select TeLAS.pr_set_clientinfo('" & Npgsqlpara1.Value & "')")

                            Dim workorderno = d_mes0190_result(0).work_order_no

                            'Create d_work0010
                            'Condition For Create d_work0010
                            Dim InsertNewRecordFord_mes0120 As New d_mes0120

                            InsertNewRecordFord_mes0120.plant_code = StrPlantCode
                            InsertNewRecordFord_mes0120.work_result_no = db.GetNewNo(db.Database.Connection, "WORK_RESULT_NO")
                            InsertNewRecordFord_mes0120.work_order_no = d_mes0190_result(0).work_order_no

                            InsertNewRecordFord_mes0120.work_order_seq = (From d_mes012002 In db.d_mes0120
                                                                          Where d_mes012002.work_order_no = workorderno AndAlso
                                                                              d_mes012002.man_stat_cd = obj_m_proc0070.man_stat_cd AndAlso
                                                                                d_mes012002.prod_individual_id = prod_individualno AndAlso
                                                                                d_mes012002.plant_code = StrPlantCode
                                                                          Select d_mes012002).Count() + 1
                            InsertNewRecordFord_mes0120.item_code = obj_m_proc0070.item_code
                            InsertNewRecordFord_mes0120.man_stat_cd = obj_m_proc0070.man_stat_cd
                            InsertNewRecordFord_mes0120.result_date = Date.Now
                            InsertNewRecordFord_mes0120.qty = d_mes0190_result(0).qty
                            InsertNewRecordFord_mes0120.unit_code = d_mes0190_result(0).unit_code
                            InsertNewRecordFord_mes0120.input_type = "2"
                            InsertNewRecordFord_mes0120.act_result_type = "4"
                            InsertNewRecordFord_mes0120.dest_location_code = d_mes0190_result(0).location_code
                            If m_proc0070_result(0).sap_if_type = "1" Then

                                If update_d_mes0280 IsNot Nothing AndAlso update_d_mes0280.Count > 0 Then
                                    update_d_mes0280(0).act_result_status = "1"
                                End If

                                InsertNewRecordFord_mes0120.stock_update_type = "0"
                                InsertNewRecordFord_mes0120.stock_updt_type = "1"
                            Else
                                InsertNewRecordFord_mes0120.stock_update_type = "9"
                                InsertNewRecordFord_mes0120.stock_updt_type = "2"
                            End If
                            InsertNewRecordFord_mes0120.sap_if_status = "0"
                            InsertNewRecordFord_mes0120.prod_individual_id = d_mes0190_result(0).prod_individual_id
                            db.d_mes0120.Add(InsertNewRecordFord_mes0120)

                            Dim work_result_no_to_delete = d_mes0190_result.Where(Function(x) x.man_stat_cd = obj_m_proc0070.man_stat_cd AndAlso x.serial_no = obj_m_proc0070.serial_no).Select(Function(x) x.work_result_no)


                            'Delete seiban Record If it is there
                            Dim d_mes0190_result_DELETE = (From t In db.d_mes0190 Where t.serial_no = obj_m_proc0070.serial_no AndAlso work_result_no_to_delete.Contains(t.work_result_no) AndAlso t.plant_code = StrPlantCode).ToList

                            'If records are there then 
                            If d_mes0190_result_DELETE IsNot Nothing AndAlso d_mes0190_result_DELETE.Count > 0 Then
                                db.d_mes0190.RemoveRange(d_mes0190_result_DELETE)
                            End If

                            'Delete seiban Record If it is there
                            Dim d_mes0290_result_DELETE = (From t In db.d_mes0290 Where t.serial_no = obj_m_proc0070.serial_no AndAlso t.plant_code = StrPlantCode AndAlso t.item_code = obj_m_proc0070.item_code).ToList

                            'If records are there then 
                            If d_mes0290_result_DELETE IsNot Nothing AndAlso d_mes0290_result_DELETE.Count > 0 Then
                                db.d_mes0290.RemoveRange(d_mes0290_result_DELETE)
                            End If

                            'Delete seiban Record If it is there
                            Dim d_mes0250_result_INSLABEL = (From t In db.d_mes0250 Where work_result_no_to_delete.Contains(t.work_result_no) AndAlso t.plant_code = StrPlantCode).ToList

                            'If records are there then 
                            If d_mes0250_result_INSLABEL IsNot Nothing AndAlso d_mes0250_result_INSLABEL.Count > 0 Then
                                For Each d_mes0250_result_INSLABELdr As d_mes0250 In d_mes0250_result_INSLABEL
                                    d_mes0250_result_INSLABELdr.delete_flag = "1"
                                Next
                            End If

                            'revert Back jisseki Status
                            Dim obj_mes0110 = (From t In db.d_mes0110 Where t.work_order_no = workorderno AndAlso t.plant_code = StrPlantCode).ToList

                            If obj_mes0110 IsNot Nothing AndAlso obj_mes0110.Count > 0 Then
                                If obj_mes0110(0).act_result_status = "2" Then
                                    obj_mes0110(0).act_result_status = "1"
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

                    Else
                        TempData("error_serial_no_NODataNotFound") = LangResources.MSG_C1010_11_NotInputSerialNo
                        Return View(obj_m_proc0070)

                    End If

                End If

            Else
                TempData("error_serial_no_NODataNotFound") = LangResources.MSG_A1030_05_NoDatainMes0060
                Return View(obj_m_proc0070)
            End If

            If Request.Cookies("man_stat_cd") Is Nothing Then
                Response.Cookies.Add(New HttpCookie("man_stat_cd") With {.Value = obj_m_proc0070.man_stat_cd})
            Else
                Response.Cookies("man_stat_cd").Value = obj_m_proc0070.man_stat_cd
            End If

            Return View(obj_m_proc0070)

        End Function


        ' GET: C1010_OneByOneJessiki/Details/5
        Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' GET: C1010_OneByOneJessiki/Edit/5
        Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: C1010_OneByOneJessiki/Edit/5
        <HttpPost()>
        Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add update logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        ' GET: C1010_OneByOneJessiki/Delete/5
        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: C1010_OneByOneJessiki/Delete/5
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