@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "Menu", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_1010_ReceiveInput") = "btn_1010_ReceiveInput" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 受入登録 Button*@
                        @<Button id="ReceiveInput" name="ReceiveInput" type="Submit" value="1010" Class="btn btn-primary Menu-Button2-Custom" style="float: left;">@LangResources.A1010_01_Fn_RegisterReceiving</Button>
                    End If

                    @If ViewData.Item("btn_1015_ReceiveCancel") = "btn_1015_ReceiveCancel" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 受入取消 Button*@
                        @<button id="ReceiveCancel" name="ReceiveCancel" type="Submit" value="1015" class="btn btn-primary Menu-Button2-Custom" style="background-color:pink;border-color:pink;float: right">@LangResources.A1015_01_Fn_CancelReceiving</button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_1020_ShelfMove") = "btn_1020_ShelfMove" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 棚移動登録 Button*@
                        @<Button id="ShelfMove" type="Submit" name="ShelfMove" value="1020" Class="btn btn-primary Menu-Button2-Custom" style="float: left;">@LangResources.A1020_01_Fn_LocateStorageBin</Button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_1030_PayOutInput") = "btn_1030_PayOutInput" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 材料払出登録 Button*@
                        @<button id="PayOutInput" type="Submit" name="PayOutInput" value="1030" class="btn btn-primary Menu-Button2-Custom">@LangResources.A1030_01_Fn_SendItembyPickingList</button>
                    End If

                    @If ViewData.Item("btn_1035_PayOutCancel") = "btn_1035_PayOutCancel" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 棚移動登録 Button*@
                        @<Button id="PayOutCancel" type="Submit" name="PayOutCancel" value="1035" Class="btn btn-primary Menu-Button2-Custom" style="background-color:pink;border-color:pink;float: right">@LangResources.A1035_01_CancelSendItembyPL</Button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_1040_PayOutApprove") = "btn_1040_PayOutApprove" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 材料払出承認 Button*@
                        @<button id="PayOutApprove" type="Submit" name="PayOutApprove" value="1040" class="btn btn-primary Menu-Button2-Custom ">@LangResources.A1040_01_Fn_ConfirmSendingItem</button>
                    End If

                    @If ViewData.Item("btn_A1045_PayOutApproveCancle") = "btn_A1045_PayOutApproveCancle" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 材料払出承認 Button*@
                        @<button id="PayOutApproveCancle" type="Submit" name="PayOutApproveCancle" value="1045" Class="btn btn-primary Menu-Button2-Custom" style="background-color:pink;border-color:pink;float: right">@LangResources.A1045_01_Fn_CancelConfirm_Sending</button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_1050_PayOutGetApprove") = "btn_1050_PayOutGetApprove" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 材料受取承認 Button*@
                        @<button id="PayOutGetApprove" type="Submit" name="PayOutGetApprove" value="1050" Class="btn btn-primary Menu-Button2-Custom ">@LangResources.A1050_01_Fn_ConfirmReceivingItem</button>
                    End If

                    @If ViewData.Item("btn_A1055_PayOutGetApproveCancle") = "btn_A1055_PayOutGetApproveCancle" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 材料受取承認 Button*@
                        @<button id="PayOutGetApproveCancle" type="Submit" name="PayOutGetApproveCancle" value="1055" Class="btn btn-primary Menu-Button2-Custom" style="background-color:pink;border-color:pink;float: right">@LangResources.A1055_01_Fn_CancelConfirm_Receiving</button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_1060_ReprintItemLabel") = "btn_1060_ReprintItemLabel" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 現品票ラベル再発行 Button*@
                        @<button id="ReprintItemLabel" type="Submit" name="ReprintItemLabel" value="1060" Class="btn btn-primary Menu-Button2-Custom ">@LangResources.A1060_01_Fn_RePrint_ID_tag</button>
                    End If


                    @If ViewData.Item("btn_1070_IDTagInquiry") = "btn_1070_IDTagInquiry" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 現品票ラベル再発行 Button*@
                        @<button id="IDTagInquiry" type="Submit" name="IDTagInquiry" value="1070" Class="btn btn-primary Menu-Button2-Custom " style="float: right">@LangResources.A1070_01_IDTagInquiry</button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_B1010_StockTakeInput") = "btn_B1010_StockTakeInput" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 現品票ラベル再発行 Button*@
                        @<button id="StockTakeInput" type="Submit" name="StockTakeInput" value="B1010" Class="btn btn-primary Menu-Button2-Custom ">@LangResources.B1010_01_Fn_StockTakeInput</button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    @If ViewData.Item("btn_C1010_OneByOneJessiki") = "btn_C1010_OneByOneJessiki" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 現品票ラベル再発行 Button*@
                        @<button id="OneByOneJessiki" type="Submit" name="OneByOneJessiki" value="C1010" Class="btn btn-primary Menu-Button2-Custom ">@LangResources.C1010_01_Fn_OneByOneJessiki</button>
                    End If

                    @If ViewData.Item("btn_C1015_OneByOneJessikiCancel") = "btn_C1015_OneByOneJessikiCancel" OrElse ViewData.Item("Groupcd") = "AD" Then
                        @*This is For 現品票ラベル再発行 Button*@
                        @<button id="OneByOneJessikiCancel" type="Submit" name="OneByOneJessikiCancel" value="C1015" Class="btn btn-primary Menu-Button2-Custom" style="background-color:pink;border-color:pink;float: right">@LangResources.C1015_01_Fn_OneByOneJessiki</button>
                    End If

                </div>

                <div class="input-group input-Group-Custom">

                    <center>
                        @*This is For 現品票ラベル再発行 Button*@
                        <button id="SetPrinter" type="Submit" name="SetPrinter" value="SetPrinter" Class="btn btn-primary Menu-Button2-Custom" style="background-color:lightslategrey;border-color:lightslategrey;">@LangResources.P1_01_Fn_SetPrinter</button>
                    </center>

                </div>

                @*This is For Set Version Lable*@
                <div id="LblVersion" class="control-label" style="text-align:right;color:black;margin-top:10px"><b>Version: &nbsp; @Session("AssembliVersion")</b></div>

                </text>
            End Using
        </div>
    </div>
</div>