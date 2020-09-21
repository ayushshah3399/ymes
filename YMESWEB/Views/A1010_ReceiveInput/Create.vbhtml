@ModelType MES_WEB.d_mes0150
@Code
    ViewData("Title") = "Create"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim strDateFormat As String = Session("language_Frmt")
    Dim acc_set_cat = ViewData("acc_set_cat")

End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Create", "A1010_ReceiveInput", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*Receive_date*@
                <div Class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.receive_date, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:192px"})
                    @Html.LabelFor(Function(model) model.po_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    <div Class="input-group">
                        @Html.TextBoxFor(Function(model) model.receive_date, strDateFormat, New With {.Class = "form-control", .style = "max-width:100px;border-top-right-radius:0.25rem;border-bottom-right-radius:0.25rem", .maxlength = 10, .autocomplete = "off", .placeholder = LangResources.A1010_09_Receivedate})
                        @Html.EditorFor(Function(model) model.po_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:129px;margin-left:94px"}})
                    </div>
                    @Html.ValidationMessageFor(Function(model) model.receive_date, "", New With {.class = "text-danger form-group-Custom"})
                    <div id="DateShouldbeLessThenToday" style="color:red">@TempData("DateShouldbeLessThenToday")</div>
                    @*<div id="CheckBeforeupdate" style="color:red">@TempData("CheckBeforeupdate")</div>*@
                </div>

                @*str_receive_qty*@
                <div Class="form-group form-group-Custom focus">
                    @Html.LabelFor(Function(model) model.str_receive_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:185px"})
                    @Html.LabelFor(Function(model) model.str_QtyPerUnit, htmlAttributes:=New With {.class = "control-label control-label-Custom", .hidden = "hidden"})
                    @Html.LabelFor(Function(model) model.po_sub_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:70px;margin-left:8px"})
                    <div Class="input-group">
                        @Html.EditorFor(Function(model) model.str_receive_qty, New With {.htmlAttributes = New With {.class = "form-control SelectOnFocus FormatLostFocus NumericValidation PasteOnNumeric", .style = "text-align: right;max-width:120px;min-width:120px;border-top-right-radius:0.25rem;border-bottom-right-radius:0.25rem;", .autocomplete = "off", .maxlength = 17, .placeholder = LangResources.A1010_08_Receive_Qty}})
                        @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:50px;margin-left:5px"}})
                        @Html.EditorFor(Function(model) model.str_QtyPerUnit, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .style = "text-align: right;max-width:110px;", .hidden = "hidden"}})
                        @Html.EditorFor(Function(model) model.stock_unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:50px;margin-left:5px", .hidden = "hidden"}})
                        @Html.EditorFor(Function(model) model.po_sub_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:70px;margin-left:20px"}})
                    </div>
                    @Html.ValidationMessageFor(Function(model) model.str_receive_qty, "", New With {.class = "text-danger", .id = "Empty_str_receive_qty"})
                    <div id="QtyShouldBeLess" style="color:red">@TempData("QtyShouldBeLess")</div>
                </div>

                @* If acc_set_cat Value Is  *@
                @If acc_set_cat = True Then

                    @*Inputqty---DivideTerm*@
                    @<div Class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.Inputqty, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "width:192px"})
                        @Html.LabelFor(Function(model) model.DivideTerm, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "width:100px"})
                        <div Class=" input-group">
                            @Html.EditorFor(Function(model) model.Inputqty, New With {.htmlAttributes = New With {.class = "form-control SelectOnFocus FormatLostFocus NumericValidation PasteOnNumeric", .style = "text-align: right;max-width:120px;border-top-right-radius:0.25rem;border-bottom-right-radius:0.25rem;", .autocomplete = "off", .placeholder = LangResources.A1010_07_Sheet, .maxlength = 17}})
                            @Html.EditorFor(Function(model) model.stock_unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:50px;margin-left:5px"}})
                            <Label style="font-size :15px;margin-top:5px"><b> &nbsp; X &nbsp;</b></Label>
                            @Html.EditorFor(Function(model) model.DivideTerm, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align:right;width:50px"}})
                        </div>
                        @Html.ValidationMessageFor(Function(model) model.Inputqty, "", New With {.class = "text-danger", .id = "InputqtyNull"})
                        <div id="QtyShouldBeLessThenCartton" style="color:red"></div>
                    </div>

                    @*RemainingQty---PackingCount*@
                    @<div Class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.RemainingQty, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "width:120px;"})
                        <div Class=" input-group">
                            @Html.EditorFor(Function(model) model.RemainingQty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align:right;width:120px"}})
                            @Html.EditorFor(Function(model) model.Fraction_stock_unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:50px;margin-left:5px"}})
                            <Label style="font-size :15px;margin-top:5px"><b> &nbsp; X &nbsp;</b></Label>
                            @Html.EditorFor(Function(model) model.PackingCount, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align:right;width:50px"}})
                        </div>
                    </div>

                    @*Show_Stock_type---Stock_type*@
                    @<div Class="form-group form-group-Custom">
                        <div Class=" input-group">
                            @Html.LabelFor(Function(model) model.Show_Stock_type, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                            @Html.EditorFor(Function(model) model.Show_Stock_type, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "width:100px"}})
                            @Html.EditorFor(Function(model) model.Stock_type, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                        </div>
                    </div>

                End If

                @*btnClear---btnRegister*@
                <div>
                    <center>
                        <Button id="btnClear" name="btnClear" value="1" type="reset" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "A1010_ReceiveInput", New RouteValueDictionary(New With {.PassHeaderText = Model.header_text, .BolDirectGotoWO = Model.BolDirectGotoWO}))'">@LangResources.Common_Previous</Button>
                        <Button id="btnRegister" name="btnRegister" type="submit" value="3" Class="btn btn-primary Button-Custom" style="margin-left:20px">@LangResources.Common_Register</Button>
                    </center>
                    @Html.EditorFor(Function(model) model.HiddenInputqty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    @Html.EditorFor(Function(model) model.OrginalHiddenInputqtyfromDB, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    @Html.EditorFor(Function(model) model.hidden_receive_qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    @Html.EditorFor(Function(model) model.HiddenDec_RatioQty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    @Html.EditorFor(Function(model) model.acc_set_cat, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                </div>

                @*This is For Display Header Text *@
                @If Model IsNot Nothing AndAlso Model.header_text IsNot Nothing AndAlso Model.header_text <> "" Then
                    @<div Class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.header_text, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                        <div Class="input-group">
                            @Html.EditorFor(Function(model) model.header_text, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                        </div>
                        @Html.ValidationMessageFor(Function(model) model.header_text, "", New With {.class = "text-danger form-group-Custom"})
                    </div>
                End If

                @*Item Code*@
                <div Class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.ItemCode, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.ItemCode, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                </div>

                @*Item Name*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.Itemname, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.Itemname, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                </div>

                @*This is hidden button for Submit part*@
                <div class="form-group form-group-Custom">
                    <div id="InputQtyIsGreterThenCarttonQty" Class="control-label invisible">@LangResources.MSG_A1010_20_InputQtyIsGreterThenCarttonQty</div>
                    <div id="CanNotEnterZero" Class="control-label invisible">@LangResources.MSG_A1010_23_CanNotEnterZero</div>
                    <div id="InputqtyGreterThenInputrecieveQty" Class="control-label invisible">@LangResources.MSG_A1010_24_InputqtyGreterThenInputrecieveQty</div>
                    <div id="InvalidNumber" Class="control-label invisible">@LangResources.MSG_A1010_25_InvalidNumber</div>
                    <div id="ReceiveQtyIsGreterThenOrder" Class="control-label invisible">@LangResources.MSG_A1010_15_ReceiveQtyIsGreterThenOrder</div>
                    <div id="The_Reciving_field_is_required" Class="control-label invisible">@LangResources.MSG_A1010_25_The_Reciving_field_is_required</div>
                    <div id="The_Qty_Per_Carton_field_is_required" Class="control-label invisible">@LangResources.MSG_A1010_26_The_Qty_Per_Carton_field_is_required</div>
                    <div id="MSG_Comm_MaxDecQty" Class="control-label invisible">@LangResources.MSG_Comm_MaxDecQty</div>
                    <div id="MSG_Comm_NumerRange" Class="control-label invisible">@LangResources.MSG_Comm_NumerRange</div>
                    <div id="Var_acc_set_cat" Class="control-label invisible" hidden="hidden">@acc_set_cat</div>
                    @Html.EditorFor(Function(model) model.BolDirectGotoWO, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .hidden = "hidden"}})
                </div>

                </text>
            End Using
        </div>
    </div>
</div>

@* Jquery Validation *@
<script>

    $(document).ready(function () {
        var BolDirectGotoWO = $('#BolDirectGotoWO').val();
        if (BolDirectGotoWO == 'True') {
            $('#header_text').css("color", "black");
        }
    });

    //Focus Lost Is in Common Shared Layout
    //Blur Event Will Call after Focuslost
    $('#Inputqty').blur(function (e) {

        @* Validation *@
        var txtInputqty = $('#Inputqty').val().replace(/,/g, '');
        var HiddenInputqty = $('#HiddenInputqty').val().replace(/,/g, '');
        var str_receive_qty = $('#str_receive_qty').val().replace(/,/g, '');    /*OrderReciving*/
        var str_QtyPerUnit = $('#str_QtyPerUnit').val().replace(/,/g, '');      /*Stock Total Item*/
        var stock_unit_code = $('#stock_unit_code').val();

        $('#QtyShouldBeLessThenCartton').text("")    //Inputqty Validation Message
        $('#InputqtyNull').text("")                  //Mvc Return Errror Then Need to Set Null

        var InputQtyIsGreterThenCarttonQty = $('#InputQtyIsGreterThenCarttonQty').text();
        var InputqtyGreterThenInputrecieveQty = $('#InputqtyGreterThenInputrecieveQty').text();
        var InvalidNumber = $('#InvalidNumber').text();
        var MSG_Comm_MaxDecQty = $('#MSG_Comm_MaxDecQty').text();
        var MSG_Comm_NumerRange = $('#MSG_Comm_NumerRange').text();

        if ((txtInputqty == '') || (str_receive_qty == '') || (Number(str_receive_qty).valueOf() == '0')) {
            $('#DivideTerm').val("")
            $('#RemainingQty').val("")
            $('#PackingCount').val("")
        }
        else {

             //txtInputqty = 0
            if (parseFloat(txtInputqty) == 0) {
                $('#DivideTerm').val("")
                $('#RemainingQty').val("")
                $('#PackingCount').val("")
                $('#Inputqty').val("0")
                $('#Fraction_stock_unit_code').val("")
                return false;
            }

            //txtInputqty = 0
            if ($.isNumeric(txtInputqty) == false) {
                $('#QtyShouldBeLessThenCartton').text(InvalidNumber)
                $('#DivideTerm').val("")
                $('#RemainingQty').val("")
                $('#PackingCount').val("")
                $('#Inputqty').val("")
                $('#Fraction_stock_unit_code').val("")
                 return false;
            }

            //txtInputqty more then 9999999999999.999
            if (txtInputqty > 9999999999999.999) {

                //MSG_Comm_NumerRange
                $('#QtyShouldBeLessThenCartton').text(MSG_Comm_NumerRange)
                $('#DivideTerm').val("")
                $('#RemainingQty').val("")
                $('#PackingCount').val("")
                $('#Fraction_stock_unit_code').val("")
                return false;

            }

            //txtInputqty more then 4 digit after dot
            if (txtInputqty.includes(".") == true && txtInputqty.substring(txtInputqty.indexOf("."), txtInputqty.length).length > 4) {

                //MSG_Comm_MaxDecQty
                $('#QtyShouldBeLessThenCartton').text(MSG_Comm_MaxDecQty)
                $('#DivideTerm').val("")
                $('#RemainingQty').val("")
                $('#PackingCount').val("")
                $('#Fraction_stock_unit_code').val("")
                return false;

            }

                //Input Qty Should be Always Small Ot Equal Then Receving
            if (Number(str_receive_qty) < 9999999999999.999 && Number(str_QtyPerUnit) < Number(txtInputqty)) {
                    $('#QtyShouldBeLessThenCartton').text(InputqtyGreterThenInputrecieveQty)
                    return false;
                }

                if  (HiddenInputqty != "" && (Number(HiddenInputqty) < Number(txtInputqty))) {
                    $('#QtyShouldBeLessThenCartton').text(InputQtyIsGreterThenCarttonQty)
                    return false;
                }

                //No Error Then Calculate
                if ($('#QtyShouldBeLess').text() == ""){

                    var txtreceive_qty = $('#str_QtyPerUnit').val().replace(/,/g, '');
                    var quotient = Math.floor(txtreceive_qty / txtInputqty);
                    var remainder = Math.round((txtreceive_qty % txtInputqty) * 1000) / 1000;

                    $('#DivideTerm').val(String(Number(quotient).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                    $('#Inputqty').val(String(Number(txtInputqty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))

                    if (remainder!= '0') {
                        $('#RemainingQty').val(String(Number(remainder).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                        $('#Fraction_stock_unit_code').val(stock_unit_code)
                        $('#PackingCount').val("1")
                    }

                    else {
                        $('#RemainingQty').val("")
                        $('#PackingCount').val("")
                        $('#Fraction_stock_unit_code').val("")
                    }

                }

            }
            return;

    });

@* On Focus Loss event *@
    $('#str_receive_qty').blur(function (e) {

        var ReceiveQtyIsGreterThenOrder = $('#ReceiveQtyIsGreterThenOrder').text();
        var str_receive_qty = $('#str_receive_qty').val().replace(/,/g, '');
        var txtInputqty = $('#Inputqty').val().replace(/,/g, '');
        var hidden_receive_qty = $('#hidden_receive_qty').val().replace(/,/g, '');
        var HiddenInputqty = $('#HiddenInputqty').val().replace(/,/g, '');
        var OrginalHiddenInputqtyfromDB = $('#OrginalHiddenInputqtyfromDB').val().replace(/,/g, '');
        var HiddenDec_RatioQty = $('#HiddenDec_RatioQty').val().replace(/,/g, '');  //Calculation
        var InvalidNumber = $('#InvalidNumber').text();
        var MSG_Comm_MaxDecQty = $('#MSG_Comm_MaxDecQty').text();
        var MSG_Comm_NumerRange = $('#MSG_Comm_NumerRange').text();
        var acc_set_cat = $('#Var_acc_set_cat').text();
        var stock_unit_code = ""

        //This Condition is For... If No item Code from Sap Then Issue Occur
        if (acc_set_cat == 'True') {
             stock_unit_code = $('#stock_unit_code').val();
        }

        $('#QtyShouldBeLess').text("")
        $('#QtyShouldBeLessThenCartton').text("")
        $('#Empty_str_receive_qty').text("")

        //Code Start From here for Qty Calulcation Same as Controller
        if ((str_receive_qty == '') || (str_receive_qty == '0') || (Number(str_receive_qty).valueOf() == '0') || (txtInputqty == '0')) {

            //txtInputqty = 0
            if (parseFloat(txtInputqty) == 0) {
                $('#DivideTerm').val("")
                $('#RemainingQty').val("")
                $('#PackingCount').val("")
                $('#Fraction_stock_unit_code').val("")
                return false;
            }

            //If Null Then set Null
            if (str_receive_qty == '') {

                $('#str_receive_qty').val("")
                //If null then display Null
                if (OrginalHiddenInputqtyfromDB == '') {
                    $('#Inputqty').val("")
                }
                else {
                    $('#Inputqty').val(String(Number(OrginalHiddenInputqtyfromDB).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                }

            }

            else {
                $('#str_receive_qty').val("0")
                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {
                    $('#Inputqty').val("0")
                }

            }

            if (acc_set_cat == 'True') {
                $('#DivideTerm').val("")
                $('#RemainingQty').val("")
                $('#PackingCount').val("")
                $('#Fraction_stock_unit_code').val("")
            }

        }
        //Calculation
        else {

            if ($.isNumeric(str_receive_qty) == false) {
                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {
                    $('#Inputqty').val(String(Number(OrginalHiddenInputqtyfromDB).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                    $('#DivideTerm').val("")
                    $('#RemainingQty').val("")
                    $('#PackingCount').val("")
                    $('#Fraction_stock_unit_code').val("")
                }
                $('#QtyShouldBeLess').text(InvalidNumber)
                $('#str_receive_qty').val("")

                //Error Mukvani Baki Che.
                return false;
            }

            //txtInputqty more then 9999999999999.999
            if (str_receive_qty > 9999999999999.999) {

                //MSG_Comm_NumerRange
                $('#QtyShouldBeLess').text(MSG_Comm_NumerRange)
                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {
                    $('#DivideTerm').val("")
                    $('#RemainingQty').val("")
                    $('#PackingCount').val("")
                    $('#Fraction_stock_unit_code').val("")
                }
                //2019/11/26 Raj No Need
                //$('#QtyShouldBeLess').text(InvalidNumber)

                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {

                    if (OrginalHiddenInputqtyfromDB == '') {
                        $('#Inputqty').val("")
                    }
                    else {
                        $('#Inputqty').val(String(Number(OrginalHiddenInputqtyfromDB).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                    }

                }

                return false;

            }

            //str_receive_qty more then 4 digit after dot
            if (str_receive_qty.includes(".") == true && str_receive_qty.substring(str_receive_qty.indexOf("."), str_receive_qty.length).length > 4) {

                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {

                    //MSG_Comm_MaxDecQty
                    if (OrginalHiddenInputqtyfromDB == '') {
                        $('#Inputqty').val("")
                    }
                    else {
                        $('#Inputqty').val(String(Number(OrginalHiddenInputqtyfromDB).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                    }

                }

                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {
                    $('#DivideTerm').val("")
                    $('#RemainingQty').val("")
                    $('#PackingCount').val("")
                    $('#Fraction_stock_unit_code').val("")
                }

                $('#QtyShouldBeLess').text(MSG_Comm_MaxDecQty)
                return false;

            }

            //Store TotalStockQty From Ratio
            var Dec_TotalStockQty = (Number(str_receive_qty) * Number(HiddenDec_RatioQty)).toFixed(3);

            //Order Qty Should be  Less then Order Qty
            //2020/05/18 New Logic Added to Control side
            //if (Number(str_receive_qty) > Number(hidden_receive_qty)) {
            //    $('#QtyShouldBeLess').text(ReceiveQtyIsGreterThenOrder)

            //    //This Condition is For... If No item Code from Sap Then Issue Occur
            //    if (acc_set_cat == 'True') {
            //        $('#Inputqty').val("")
            //        $('#DivideTerm').val("")
            //        $('#RemainingQty').val("")
            //        $('#PackingCount').val("")
            //        $('#Fraction_stock_unit_code').val("")
            //    }

            //    return false;
            //}

            //Set Picking Qty
            //IF Null In Database Or Not In DataBase
            //Set Recieve Qty As By Default
            if (OrginalHiddenInputqtyfromDB == "") {

                $('#str_receive_qty').val(String(Number(str_receive_qty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))

                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {
                    //2020-05-20 No Need to Change Input Qty When Focusout From ReceiveQty
                    if (txtInputqty == "" || Number(Dec_TotalStockQty) < Number(txtInputqty)) {
                        $('#Inputqty').val(String(Number(Dec_TotalStockQty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                        $('#DivideTerm').val("1")
                        $('#RemainingQty').val("")
                        $('#PackingCount').val("")
                        $('#stock_unit_code').val(stock_unit_code)
                        $('#Fraction_stock_unit_code').val("")
                    }
                    else {

                        //Need to Calculate
                        var quotient = Math.floor(Dec_TotalStockQty / Number(txtInputqty));
                        var remainder = Math.round((Dec_TotalStockQty % Number(txtInputqty)) * 1000) / 1000;

                        $('#DivideTerm').val(String(Number(quotient).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                        $('#stock_unit_code').val(stock_unit_code)

                        if (remainder != '0') {
                            $('#RemainingQty').val(String(Number(remainder).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                            $('#Fraction_stock_unit_code').val(stock_unit_code)
                            $('#PackingCount').val("1")
                        }

                        else {
                            $('#RemainingQty').val("")
                            $('#PackingCount').val("")
                            $('#Fraction_stock_unit_code').val("")
                        }
                    }
                }

                $('#str_QtyPerUnit').val(Dec_TotalStockQty)

            }
            else {

                //For Example Picking_Qty From DB 10 > str_receive_qty 5
                if (Number(OrginalHiddenInputqtyfromDB) > Number(Dec_TotalStockQty)) {

                    $('#str_receive_qty').val(String(Number(str_receive_qty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))

                    //This Condition is For... If No item Code from Sap Then Issue Occur
                    if (acc_set_cat == 'True') {

                        //Calculate
                        if (txtInputqty != "" && Number(txtInputqty) < Number(Dec_TotalStockQty)) {

                            //Need to Calculate
                            var quotient = Math.floor(Dec_TotalStockQty / Number(txtInputqty));
                            var remainder = Math.round((Dec_TotalStockQty % Number(txtInputqty)) * 1000) / 1000;

                            $('#DivideTerm').val(String(Number(quotient).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                            $('#stock_unit_code').val(stock_unit_code)

                            if (remainder != '0') {
                                $('#RemainingQty').val(String(Number(remainder).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                                $('#Fraction_stock_unit_code').val(stock_unit_code)
                                $('#PackingCount').val("1")
                            }

                            else {
                                $('#RemainingQty').val("")
                                $('#PackingCount').val("")
                                $('#Fraction_stock_unit_code').val("")
                            }
                        }
                        //Set Values
                        else {
                            $('#Inputqty').val(String(Number(Dec_TotalStockQty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                            $('#DivideTerm').val("1")
                            $('#RemainingQty').val("")
                            $('#PackingCount').val("")
                            $('#stock_unit_code').val(stock_unit_code)
                            $('#Fraction_stock_unit_code').val("")
                        }

                    }

                    $('#str_QtyPerUnit').val(Dec_TotalStockQty)

                }
                 //For Example Picking_Qty From DB 10 < str_receive_qty 100
                else {

                    $('#str_QtyPerUnit').val(Dec_TotalStockQty)

                    //If Null then Set Default
                    if (txtInputqty == "") {
                        $('#Inputqty').val(String(Number(Dec_TotalStockQty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                        $('#DivideTerm').val("1")
                        $('#RemainingQty').val("")
                        $('#PackingCount').val("")
                        $('#stock_unit_code').val(stock_unit_code)
                        $('#Fraction_stock_unit_code').val("")
                        return
                    }

                    //2020/05/21 If inputed Qty Should not be set default in case of receive qty Change.
                    var quotient = Math.floor(Dec_TotalStockQty / txtInputqty);
                    var remainder = Math.round((Dec_TotalStockQty % txtInputqty) * 1000) / 1000;

                    $('#str_receive_qty').val(String(Number(str_receive_qty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))

                    //This Condition is For... If No item Code from Sap Then Issue Occur
                    if (acc_set_cat == 'True') {
                        $('#Inputqty').val(String(Number(txtInputqty).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                        $('#DivideTerm').val(String(Number(quotient).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                        $('#stock_unit_code').val(stock_unit_code)

                        if (remainder != '0') {
                            $('#RemainingQty').val(String(Number(remainder).valueOf()).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,'))
                            $('#Fraction_stock_unit_code').val(stock_unit_code)
                            $('#PackingCount').val("1")
                        }

                        else {
                            $('#RemainingQty').val("")
                            $('#PackingCount').val("")
                            $('#Fraction_stock_unit_code').val("")
                        }
                    }

                }

            }

        }

    });

@* btnRegister--> It will set null the textbox *@
    $('#btnRegister').click(function (e) {

        $('#QtyShouldBeLess').text("")
        $('#InputqtyNull').text("")     //Mvc Return Errror Then Need to Set Null
        var MSG_Comm_MaxDecQty = $('#MSG_Comm_MaxDecQty').text();
        var acc_set_cat = $('#Var_acc_set_cat').text();

        var ButtonVal = this.value;

        if (ButtonVal == "3") {

            //******* RecieveQty Check logic *******
            var ReceiveQtyIsGreterThenOrder = $('#ReceiveQtyIsGreterThenOrder').text();
            var str_receive_qty = $('#str_receive_qty').val().replace(/,/g, '');
            var hidden_receive_qty = $('#hidden_receive_qty').val().replace(/,/g, '');
            var The_Reciving_field_is_required = $('#The_Reciving_field_is_required').text();
            var The_Qty_Per_Carton_field_is_required = $('#The_Qty_Per_Carton_field_is_required').text();

            //This Condition is For... If No item Code from Sap Then Issue Occur
            if (acc_set_cat == 'True') {
                var txtInputqty = $('#Inputqty').val().replace(/,/g, '');
            }

            var CanNotEnterZero = $('#CanNotEnterZero').text();
            var MSG_Comm_NumerRange = $('#MSG_Comm_NumerRange').text();

            //Need to Add Condition
            //If Null then Disaply Error
            if (str_receive_qty == '') {
                $('#QtyShouldBeLess').text(The_Reciving_field_is_required)
                return false;

                //If 0 then Disaply Error
            } else if (Number(str_receive_qty).valueOf() == '0') {
                $('#QtyShouldBeLess').text(CanNotEnterZero)
                return false;

                //9999999999999.999
            } else if (str_receive_qty > 9999999999999.999) {
                $('#QtyShouldBeLess').text(MSG_Comm_NumerRange)
                return false;

                //str_receive_qty more then 4 digit after dot
            } else if (str_receive_qty.includes(".") == true && str_receive_qty.substring(str_receive_qty.indexOf("."), str_receive_qty.length).length > 4) {
                $('#QtyShouldBeLess').text(MSG_Comm_MaxDecQty)
                return false;
            }
            //2020/05/18 New logic added to client
            //Order Qty Should be  Less then Order Qty
            //else if (Number(str_receive_qty) > Number(hidden_receive_qty)) {
            //    $('#QtyShouldBeLess').text(ReceiveQtyIsGreterThenOrder)
            //    return false;
            //}
            else {

                $('#QtyShouldBeLessThenCartton').text("")

                //This Condition is For... If No item Code from Sap Then Issue Occur
                if (acc_set_cat == 'True') {

                    //If Null then Disaply Error
                    if (txtInputqty == '') {
                        $('#QtyShouldBeLessThenCartton').text(The_Qty_Per_Carton_field_is_required)
                        return false;

                        //If 0 then Disaply Error
                    } else if (Number(txtInputqty).valueOf() == '0') {
                        $('#QtyShouldBeLessThenCartton').text(CanNotEnterZero)
                        return false;

                        //9999999999999.999
                    } else if (txtInputqty > 9999999999999.999) {
                        $('#QtyShouldBeLessThenCartton').text(MSG_Comm_NumerRange)
                        return false;

                        //txtInputqty more then 4 digit after dot
                    } else if (txtInputqty.includes(".") == true && txtInputqty.substring(txtInputqty.indexOf("."), txtInputqty.length).length > 4) {
                        //MSG_Comm_MaxDecQty
                        $('#QtyShouldBeLessThenCartton').text(MSG_Comm_MaxDecQty)
                        return false;
                    }

                }

            }

            //This Condition is For... If No item Code from Sap Then Issue Occur
            if (acc_set_cat == 'True') {

                //******* InputQty Check logic *******
                var HiddenInputqty = $('#HiddenInputqty').val().replace(/,/g, '');
                var stock_unit_code = $('#stock_unit_code').val();
                $('#QtyShouldBeLessThenCartton').text("")
                var str_QtyPerUnit = $('#str_QtyPerUnit').val().replace(/,/g, '');/*Stock Total Item*/

                //Mvc Return Errror Then Need to Set Null
                $('#InputqtyNull').text("")
                var InputQtyIsGreterThenCarttonQty = $('#InputQtyIsGreterThenCarttonQty').text();
                var InputqtyGreterThenInputrecieveQty = $('#InputqtyGreterThenInputrecieveQty').text();
                var InvalidNumber = $('#InvalidNumber').text();

                if (txtInputqty == '') {
                    $('#DivideTerm').val("")
                    $('#RemainingQty').val("")
                    $('#PackingCount').val("")
                }
                else {

                    if (parseFloat(txtInputqty) == 0) {
                        $('#QtyShouldBeLessThenCartton').text(CanNotEnterZero)
                        $('#DivideTerm').val("")
                        $('#RemainingQty').val("")
                        $('#PackingCount').val("")
                        $('#Inputqty').val("")
                        $('#Fraction_stock_unit_code').val("")
                        return false;
                    }

                    if (txtInputqty == ".") {
                        $('#QtyShouldBeLessThenCartton').text(InvalidNumber)
                        $('#DivideTerm').val("")
                        $('#RemainingQty').val("")
                        $('#PackingCount').val("")
                        $('#Inputqty').val("")
                        $('#Fraction_stock_unit_code').val("")
                        return false;
                    }

                    //Input Qty Should be Always Small Ot Equal Then Receving
                    if (Number(str_QtyPerUnit) < Number(txtInputqty)) {
                        $('#QtyShouldBeLessThenCartton').text(InputqtyGreterThenInputrecieveQty)
                        return false;
                    }

                    if (HiddenInputqty != "" && (Number(HiddenInputqty) < Number(txtInputqty))) {
                        $('#QtyShouldBeLessThenCartton').text(InputQtyIsGreterThenCarttonQty)
                        return false;
                    }

                    var txtreceive_qty = $('#str_QtyPerUnit').val().replace(/,/g, '');
                    var quotient = Math.floor(txtreceive_qty / txtInputqty);
                    var remainder = Math.round((txtreceive_qty % txtInputqty) * 1000) / 1000;

                    $('#DivideTerm').val(quotient)

                    if (remainder != '0') {
                        $('#RemainingQty').val(remainder)
                        $('#Fraction_stock_unit_code').val(stock_unit_code)
                        $('#PackingCount').val("1")
                    }

                    else {
                        $('#RemainingQty').val("")
                        $('#PackingCount').val("")
                        $('#Fraction_stock_unit_code').val("")
                    }

                }
                return;

            }

        }

    });

    //Receive Date Validation
    //Enter button wil not work as submit button
    $('#receive_date').keypress(function (e) {
        if (e.keyCode == 13) {
            @* Validation *@
            $('#btnRegister').val("4")
            $(this).next('input').focus();
        }
    });

</script>
