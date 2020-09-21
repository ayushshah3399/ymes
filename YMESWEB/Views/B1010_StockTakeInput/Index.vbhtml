1st @ModelType MES_WEB.d_mes1020
@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim strDateFormat As String = Session("language_Frmt")
	Dim location_codeList = ViewBag.Location_code
End Code

<style>
	.comboField {
		position: relative;
	}

	.inputBox {
		font-size: 14px;
		width: 200px;
		position: absolute;
	}

	.selectBox {
		font-size: 14px;
		width: 225px;
	}
</style>

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
		<div class="col-12" style="max-width:353px">

			@Using Html.BeginForm("Index", "B1010_StockTakeInput", FormMethod.Post, New With {.class = "Form-Horizontal"})
				@Html.AntiForgeryToken()
				@<Text>

				@Html.ValidationSummary(True, "", New With {.class = "text-danger"})

				@*Receive_date & back Button*@
				<div Class="form-group form-group-Custom">
					@Html.LabelFor(Function(model) model.location_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:130px"})
					@Html.LabelFor(Function(model) model.stocktake_date, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
					<div class=" input-group">

						<div class="comboField">
							@Html.EditorFor(Function(model) model.location_code, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox", .autocomplete = "off", .maxlength = 4, .style = "width:90px"}})
							<select class="form-control input-sm selectBox" id="selectBoxlocation_code" style=" width:115px;">
								@If location_codeList IsNot Nothing Then
									@For Each item In location_codeList
										@<option>@item.location_code</option>
									Next
								End If
							</select>
							@Html.ValidationMessageFor(Function(model) model.location_code, "", New With {.class = "text-danger"})
						</div>

						@Html.TextBoxFor(Function(model) model.stocktake_date, strDateFormat, New With {.Class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "Max-width:120px;Max-height:35px;padding-left:17px;"})
					</div>
					<div id="B1010_Emptylocation_code" style="color:red;font-size:15px">@TempData("B1010_Emptylocation_code")</div>
				</div>

				@*Laungage Combo Box*@
				@*<div class="form-group">*@
				@*<div id="lblMode" class="control-label control-label-Custom">@LangResources.B1010_14_Location_Code</div>*@
				@*This is For Set lanugage Combobox*@
				@* @Html.DropDownList("Location_code", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .style = "font-size:13px;height:auto;width:auto"})*@
				@*</div>*@

				@*btnNoChange & btnQtyChange Button*@
				<div>
					<div id="lblMode" class="control-label control-label-Custom">@LangResources.B1010_07_Mode</div>
					<center>
						<Button id="btnNoChange" name="btnNoChange" type="button" Class="btn btn-success focus Button-Custom">@LangResources.B1010_03_NoChange</Button>
						<Button id="btnQtyChange" name="btnQtyChange" type="button" value="3" Class="btn btn-primary Button-Custom" style="margin-left:20px" onclick="location.href='@Url.Action("Create", "B1010_StockTakeInput")'">@LangResources.B1010_04_QtyChange</Button>
					</center>
				</div>

				<div id="B1010_Completed" style="color:green;font-size:20px">@TempData("B1010_Completed")</div>

				@If TempData("SetFocusOnSubmit") = "1" Then

					@*This is for label_no textbox*@
					@<div class="form-group form-group-Custom">
						@Html.LabelFor(Function(model) model.label_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
						<div Class="input-group">
							@Html.EditorFor(Function(model) model.label_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .style = "width:50%;border-top-right-radius:0.25rem;border-bottom-right-radius:0.25rem;", .placeholder = LangResources.B1010_06_labelNo_PlaceHolder, .autocomplete = "off", .maxlength = 16}})
							<Button id="btnRegister" name="btnRegister" type="submit" value="3" Class="btn btn-primary Button-Custom float-right" style="margin-left:20px;margin-top:-2px">@LangResources.Common_Register</Button>

						</div>
						@Html.ValidationMessageFor(Function(model) model.label_no, "", New With {.class = "text-danger"})
					</div>

				Else

					@*This is for label_no textbox*@
					@<div class="form-group form-group-Custom">
						@Html.LabelFor(Function(model) model.label_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
						<div Class="input-group">
							@Html.EditorFor(Function(model) model.label_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .style = "width:50%;border-top-right-radius:0.25rem;border-bottom-right-radius:0.25rem;", .placeholder = LangResources.B1010_06_labelNo_PlaceHolder, .autocomplete = "off", .maxlength = 16}})
							<Button id="btnRegister" name="btnRegister" type="submit" value="3" Class="btn btn-primary Button-Custom float-right" style="margin-left:20px;margin-top:-2px">@LangResources.Common_Register</Button>

						</div>
						@Html.ValidationMessageFor(Function(model) model.label_no, "", New With {.class = "text-danger"})
					</div>

				End If

				<div id="B1010_TxtLableError" style="color:red;font-size:15px">@TempData("B1010_TxtLableError")</div>

				@*str_receive_qty*@
                <div Class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.Str_stock_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:185px"})
                    @Html.LabelFor(Function(model) model.Label_Status, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:100px"})
                    <div Class="input-group">
                        <div Class="input-group">
                            @Html.EditorFor(Function(model) model.Str_stock_qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align: right;max-width:120px;min-width:120px;font-size: 20px;"}})
                            @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:63px;margin-left:5px;;margin-top:3px;"}})
                            @Html.EditorFor(Function(model) model.Label_Status, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:100px;font-size: 20px;"}})

                        </div>
                        <div id="B1010_Txtstock_qtyError" style="color:red">@TempData("B1010_Txtstock_qtyError")</div>
                        @Html.ValidationMessageFor(Function(model) model.Str_stock_qty, "", New With {.class = "text-danger", .id = "Empty_str_receive_qty"})

                    </div>

                    @*item_code-shelf_no*@
                    <div Class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.item_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:218px"})
                        @Html.LabelFor(Function(model) model.shelf_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:100px"})

                        <div Class="input-group">
                            @Html.EditorFor(Function(model) model.item_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:220px"}})
                            @Html.EditorFor(Function(model) model.shelf_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:100px;"}})
                        </div>

                    </div>
                    <br />
                    @*This is for back to menu button*@
                    <button id="btnbacktomenu_B1010" name="btnbacktomenu_B1010" type="button" Class="btn btn-secondary ml-auto Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>

                    <div id="MSG_B1030_08_EmptyIDTag" Class="control-label invisible">@LangResources.MSG_B1030_08_EmptyIDTag</div>
                    <div id="MSG_B1030_10_EmptyLocationCode" Class="control-label invisible">@LangResources.MSG_B1030_10_EmptyLocationCode</div>
                    <div id="MSG_B1030_10_LocationCodeExist" Class="control-label invisible">@LangResources.MSG_B1030_10_LocationCodeExist</div>
                </text>
            End Using
                </div>
	</div>
</div>

@* Jquery Validation *@
<script>

    //DropDown Menu With TextBox
    $("#selectBoxlocation_code").change(function () {
        var val = this.value
        $('#location_code').val(val)
        $('#B1010_Emptylocation_code').text("");
        document.cookie = "location_code=" + val + ";path=/";
    });

    //DropDown Menu With TextBox
    $("#location_code").focusout(function () {

        var location_code = $('#location_code').val();

        if (location_code !== '') {
            $('#btnRegister').val("2");
            $('#btnRegister').click();
        }

    });

    $("#label_no").focusout(function () {

        $('#B1010_TxtLableError').text("");
        var label_no = $('#label_no').val();

        if (label_no !== '') {
            $('#btnRegister').val("1");
            $('#btnRegister').click();
        }
        else {
            $('#Str_stock_qty').val("");
            $('#Label_Status').val("");
            $('#unit_code').val("");
            $('#item_code').val("");
            $('#shelf_no').val("");
            return false;
        };

    });

    @*When Enter Need to Fill Data *@
    $('#label_no').keydown(function (e) {

        if (e.keyCode == 13) {  // 13 Is Enter key

            //To Prevent Default
            //e.preventDefault()

            $('#B1010_TxtLableError').text("");
            //$('#B1010_Completed').text("");
            var label_no = $('#label_no').val();

            if (label_no !== '') {
                $('#btnRegister').val("1");
                $('#btnRegister').click();
            }

        }

    });

    $('#btnRegister').click(function (e) {


        var MSG_B1030_10_EmptyLocationCode = $('#MSG_B1030_10_EmptyLocationCode').text();
        var MSG_B1030_10_LocationCodeExist = $('#MSG_B1030_10_LocationCodeExist').text();
        $('#B1010_Emptylocation_code').text("");
        var location_code = $('#location_code').val();
        var bolerrorflag = '1'

        if (location_code == '') {
			$('#label_no').val("");
	        $('#B1010_Emptylocation_code').text(MSG_B1030_10_EmptyLocationCode);
			bolerrorflag = '2';
			$('#location_code').focus();
        }

        var locationcodeexist = false;
        var selectBoxlocation_code = document.getElementById('selectBoxlocation_code').options;
        for (var i = 0; i < selectBoxlocation_code.length; i++) {
            if (selectBoxlocation_code[i].value === location_code) {               
                locationcodeexist = true;
            }
        }

        //Location Code not Exust Then Error
        if (locationcodeexist === false) {
            $('#B1010_Emptylocation_code').text(MSG_B1030_10_LocationCodeExist);
            bolerrorflag = '2';
            $('#location_code').focus();
        }

        var btnRegister = $('#btnRegister').val();
        if (btnRegister == '3') {

            var MSG_B1030_08_EmptyIDTag = $('#MSG_B1030_08_EmptyIDTag').text();
            var txtlabel_no = $('#label_no').val();

            $('#B1010_TxtLableError').text("");
            $('#B1010_Completed').text("");

            if (txtlabel_no == '') {
                $('#B1010_TxtLableError').text(MSG_B1030_08_EmptyIDTag);
				bolerrorflag = '2';
				$('#label_no').focus();
            }

        }

        if (bolerrorflag == '2') {
            return false;
        }

    });

	//set first focus to location code if it is null.
	$(document).ready(function () {
		var location_code = $('#location_code').val();
		if (location_code == '') {
			$('#location_code').focus();
		}
		else {
			var label_no = $('#label_no').val();
			var TxtLableError = $('#B1010_TxtLableError').html();
			if (label_no == '' || TxtLableError !== '' ) {
				$('#label_no').focus();

				if (label_no !== '') {
					$('#label_no').select();
				}
			}
			else {
				$('#btnRegister').focus();
			}
		}
	});
</script>
