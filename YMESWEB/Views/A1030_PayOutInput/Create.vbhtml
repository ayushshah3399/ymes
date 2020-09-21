@ModelType MES_WEB.d_mes0100
@Code
    ViewData("Title") = "Create"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Create", "A1030_PayOutInput", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*LabelNo TextBox*@
                <div class="form-group form-group-Custom focus">
                    @Html.LabelFor(Function(model) model.TextBox_lable_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.TextBox_lable_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .autocomplete = "off", .maxlength = 16}})
                </div>
                <div id="NoDataMes" name="NoDataMes" style="color:red;Font-Size:15px">@TempData("NoDataMes")</div>

                @*LabelNo Lable*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.label_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.label_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                </div>

                @*ItemCode Lable*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.item_code, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.item_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                </div>

                @*ItemName Lable*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.A1030_Itemname, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.A1030_Itemname, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                </div>

                @*str_qty*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.str_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    <div class=" input-group ">
                        @Html.EditorFor(Function(model) model.str_qty, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom SelectOnFocus FormatLostFocus NumericValidation PasteOnNumeric", .style = "text-align: right;max-width:120px;border-top-right-radius:0.25rem;border-bottom-right-radius:0.25rem;", .autocomplete = "off", .maxlength = 17}})
                        @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:50px;margin-left:5px"}})
                    </div>
                    @Html.ValidationMessageFor(Function(model) model.str_qty, "", New With {.class = "text-danger"})
                    <div id="QtyGreterThenOrder" name="QtyGreterThenOrder" style="color:red;Font-Size:15px"></div>
                </div>

                @*This is For Change Button*@
                <div Class="form-group form-group-Custom">
                    <center>
                        <Button id="btnPrevious" name="btnPrevious" value="1" type="reset" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "A1030_PayOutInput")'">@LangResources.Common_Previous</Button>
                        <button id="btnRegister" name="btnRegister" type="submit" value="2" class="btn btn-primary Button-Custom" style="margin-left:20px">@LangResources.Common_Register</button>
                    </center>
                    <div id="CheckBeforeupdate" style="color:red">@TempData("CheckBeforeupdate")</div>
                    @Html.EditorFor(Function(model) model.A1030_Hidden_Qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    @Html.EditorFor(Function(model) model.updtdt, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    <div id="LblQtyCanbeMaximum" class="control-label control-label-Custom invisible" hidden="hidden">@LangResources.MSG_A1030_18_TransferQtyExceedLabelQty</div>
                </div>

                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.picking_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})
                    @Html.LabelFor(Function(model) model.shelfgrp_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})
                    <div class="input-group">
                        @Html.EditorFor(Function(model) model.picking_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                        @Html.EditorFor(Function(model) model.shelfgrp_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                    </div>
                </div>

                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.loc_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})
                    @Html.LabelFor(Function(model) model.in_loc_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})
                    <div class="input-group">
                        @Html.EditorFor(Function(model) model.loc_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                        @Html.EditorFor(Function(model) model.in_loc_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                    </div>
                </div>

                @*This is For  Hidden label to Get Value in Jquery*@
                @*It is Submit Button type So it will actomatically called controller*@
                <div class="form-group-sm hidden">
                    <div id="LblEmptyLabelNo" class="control-label invisible">@LangResources.MSG_A1020_03_LblEmptyLabelNo</div>
                    <div id="EmptyQty" class="control-label invisible">@LangResources.MSG_A1030_13_EmptyQty</div>
                    <div id="MSG_Comm_MaxDecQty" Class="control-label invisible">@LangResources.MSG_Comm_MaxDecQty</div>
                    <div id="MSG_Comm_NumerRange" Class="control-label invisible">@LangResources.MSG_Comm_NumerRange</div>
                    <div id="CanNotEnterZero" Class="control-label invisible">@LangResources.MSG_A1010_23_CanNotEnterZero</div>
                </div>

                </text>
            End Using
        </div>
    </div>
</div>

@* Jquery Validation *@
<script>

    @* This is validation and will get data from master *@
    $('#TextBox_lable_no').keypress(function (e) {
        if (e.keyCode == 13) {
            @* Validation *@
            $('#btnRegister').val("1");
        };
    });

    @* When Focus out Fill Data To Other Control *@
    $('#TextBox_lable_no').focusout(function (e) {
        var lable_no = $('#TextBox_lable_no').val();
        if (lable_no != "") {
            $('#btnRegister').val("1");
            $('#btnRegister').click();
        };
    });

    @* This is validation and will get data from master *@
    $('#str_qty').blur(function (e) {

        var txtreceive_qty = $('#str_qty').val();
        var MSG_Comm_MaxDecQty = $('#MSG_Comm_MaxDecQty').text();
        var MSG_Comm_NumerRange = $('#MSG_Comm_NumerRange').text();
        $("#QtyGreterThenOrder").text("");

        if (txtreceive_qty > 9999999999999.999) {
            //MSG_Comm_MaxDecQty
            $("#QtyGreterThenOrder").text(MSG_Comm_NumerRange);
            return false;
        }

        //txtInputqty more then 4 digit after dot
        if (txtreceive_qty.includes(".") == true && txtreceive_qty.substring(txtreceive_qty.indexOf("."), txtreceive_qty.length).length > 4) {

            //MSG_Comm_MaxDecQty
            $("#QtyGreterThenOrder").text(MSG_Comm_MaxDecQty);
            return false;

        }

        //Change Qty And Direct Click Update
        //Validation
        if (txtreceive_qty != '') {
            var A1030_Hidden_Qty = $('#A1030_Hidden_Qty').val();
            var LblQtyCanbeMaximum = $('#LblQtyCanbeMaximum').text();
            $("#QtyGreterThenOrder").text("");

            if (parseFloat(txtreceive_qty) > parseFloat(A1030_Hidden_Qty)) {
                $("#QtyGreterThenOrder").text(LblQtyCanbeMaximum);
                return false
            }
            return;
        }

    });

    @* This is validation and will get data from master *@
    $('#str_qty').keydown(function (e) {

        if (e.keyCode == 13) {  // 13 Is Enter key

            var txtreceive_qty = $('#str_qty').val();
            var MSG_Comm_MaxDecQty = $('#MSG_Comm_MaxDecQty').text();
            var MSG_Comm_NumerRange = $('#MSG_Comm_NumerRange').text();
            $("#QtyGreterThenOrder").text("");

            if (txtreceive_qty > 9999999999999.999) {
                //MSG_Comm_MaxDecQty
                $("#QtyGreterThenOrder").text(MSG_Comm_NumerRange);
                return false;
            }

            //txtInputqty more then 4 digit after dot
            if (txtreceive_qty.includes(".") == true && txtreceive_qty.substring(txtreceive_qty.indexOf("."), txtreceive_qty.length).length > 4) {

                //MSG_Comm_MaxDecQty
                $("#QtyGreterThenOrder").text(MSG_Comm_MaxDecQty);
                return false;

            }

            //Change Qty And Direct Click Update
            //Validation
            if (txtreceive_qty != '') {
                var A1030_Hidden_Qty = $('#A1030_Hidden_Qty').val();
                var LblQtyCanbeMaximum = $('#LblQtyCanbeMaximum').text();
                $("#QtyGreterThenOrder").text("");

                if (parseFloat(txtreceive_qty) > parseFloat(A1030_Hidden_Qty)) {
                    $("#QtyGreterThenOrder").text(LblQtyCanbeMaximum);
                    return false
                }
                return;
            }

        }

    });

    @* btnRegister-- > It will set null the textbox *@
    $('#btnRegister').click(function (e) {

        var TextBox_lable_no = $('#TextBox_lable_no').val();
        var lable_no = $('#label_no').val();
        var txtreceive_qty = $('#str_qty').val();
        var LblEmptyLabelNo = $('#LblEmptyLabelNo').text();
        var EmptyQty = $('#EmptyQty').text();
        $("#NoDataMes").text("");
        $("#QtyGreterThenOrder").text("");
        var MSG_Comm_MaxDecQty = $('#MSG_Comm_MaxDecQty').text();
        var MSG_Comm_NumerRange = $('#MSG_Comm_NumerRange').text();
        var CanNotEnterZero = $('#CanNotEnterZero').text();
        
        //Check When Update Button Click
        var ButtonVal = this.value;

        if (ButtonVal == "2") {

            //Click Register Button But NO LabeL Data is there
            if (TextBox_lable_no == '' && lable_no == '') {
                $("#NoDataMes").text(LblEmptyLabelNo);
                return false;
            }

            //If Qty Is Null Then Display Error
            if (lable_no != '' && txtreceive_qty == '') {
                $("#QtyGreterThenOrder").text(EmptyQty);
                return false;
            }

            if (txtreceive_qty > 9999999999999.999) {
                //MSG_Comm_MaxDecQty
                $("#QtyGreterThenOrder").text(MSG_Comm_NumerRange);
                return false;
            }

            //txtInputqty more then 4 digit after dot
            if (txtreceive_qty.includes(".") == true && txtreceive_qty.substring(txtreceive_qty.indexOf("."), txtreceive_qty.length).length > 4) {
                 //MSG_Comm_MaxDecQty
                $("#QtyGreterThenOrder").text(MSG_Comm_MaxDecQty);
                return false;
            }

            //If 0 then Disaply Error
            if (Number(txtreceive_qty).valueOf() == '0') {
                $('#QtyGreterThenOrder').text(CanNotEnterZero)
                return false;
            }

            //Change Qty And Direct Click Update
            //Validation
            if (txtreceive_qty != '') {
                var A1030_Hidden_Qty = $('#A1030_Hidden_Qty').val();
                var LblQtyCanbeMaximum = $('#LblQtyCanbeMaximum').text();

                if (parseFloat(txtreceive_qty) > parseFloat(A1030_Hidden_Qty)) {

                    $("#QtyGreterThenOrder").text(LblQtyCanbeMaximum);
                    return false

                }
                return;
            }

        }

    });

</script>