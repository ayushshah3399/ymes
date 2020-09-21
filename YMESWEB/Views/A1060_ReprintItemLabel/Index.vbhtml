@ModelType MES_WEB.d_mes0040
@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "A1060_ReprintItemLabel", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<text>
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                    @*Label Number Textbox*@
                    <div class="form-group form-group-Custom focus">
                        @Html.LabelFor(Function(model) model.label_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                        @Html.EditorFor(Function(model) model.label_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.A1020_08_LabelNo, .autocomplete = "off", .maxlength = 16}})
                        @Html.ValidationMessageFor(Function(model) model.label_no, "", New With {.class = "text-danger"})
                    </div>
                    <div id="NoDatainMes0040_A1060" name="NoDatainMes0040_A1060" style="color:red;Font-Size:15px">@TempData("NoDatainMes0040_A1060")</div>

                    @*Label Label number*@
                    <div class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.A1060_DisplayLbl_label_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                        @Html.EditorFor(Function(model) model.A1060_DisplayLbl_label_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                    </div>

                    @*item_code Lable*@
                    <div class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.item_code, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                        @Html.EditorFor(Function(model) model.item_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                    </div>

                    @*ItemName Lable*@
                    <div class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.A1060_Itemname, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                        @Html.EditorFor(Function(model) model.A1060_Itemname, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                    </div>

                    @*location_code--shelf_no*@
                    <div class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.location_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:150px"})
                        @Html.LabelFor(Function(model) model.shelf_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:150px"})
                        <div class="input-group">
                            @Html.EditorFor(Function(model) model.location_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:154px"}})
                            @Html.EditorFor(Function(model) model.shelf_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:150px"}})
                        </div>
                    </div>

                    @*stock_qty--inspect_qty--unit_code*@
                    <div class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.str_stock_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:150px"})
                        @Html.LabelFor(Function(model) model.str_inspect_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:150px"})
                        <div class="input-group">
                            @Html.EditorFor(Function(model) model.str_stock_qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align:right;max-width:105px"}})
                            @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:44px;margin-left:5px"}})
                            @Html.EditorFor(Function(model) model.str_inspect_qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align:right;max-width:105px"}})
                            @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:40px;margin-left:5px"}})
                        </div>
                    </div>

                    @*keep_qty--unit_code*@
                    <div class="form-group form-group-Custom">
                        @Html.LabelFor(Function(model) model.str_keep_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                        <div class="input-group">
                            @Html.EditorFor(Function(model) model.str_keep_qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align:right;width:105px"}})
                            @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "width:44px;margin-left:5px"}})
                        </div>
                    </div>

                    @*button*@
                    <div class="form-group form-group-Custom">
                        <center>
                            <button type="button" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>
                            <button id="btnRegister" name="btnRegister" type="submit" value="2" Class="btn btn-primary  Button-Custom" style="margin-left:20px">@LangResources.Common_BtnPrint</button>
                        </center>
                        @Html.EditorFor(Function(model) model.updtdt, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    </div>

                    @*This is For  Hidden label to Get Value in Jquery*@
                    <div id="LblEmptyLabel" Class="control-label control-label-Custom invisible" hidden="hidden">@LangResources.MSG_A1020_03_LblEmptyLabelNo</div>

                </text>
            End Using
        </div>
		</div>
</div>
<script>

	@* This is validation and will get data from master *@
	$('#label_no').keypress(function (e) {
		if (e.keyCode == 13) {
			@* Validation *@

			$('#btnRegister').val("1")

		};

    });

    @* This is validation and will get data from master *@
    $('#label_no').focusout(function (e) {
        var label_no = $('#label_no').val();
        if (label_no != '') {
            @* Validation *@

            $('#btnRegister').val("1");
            $('#btnRegister').click();

        };

    });

	@* This is validation and will get data from master *@
	$('#btnRegister').click(function (e) {
        var errflg = '';
		@* Error Message Become Null *@
		$("#NoDatainMes0040_A1060").text("");
		var label_no = $('#label_no').val();
		var btnRegister = $('#btnRegister').val();
		var LblEmptyLabel = $('#LblEmptyLabel').text();
		if (btnRegister == '1' && label_no == '') {

			$('#NoDatainMes0040_A1060').text(LblEmptyLabel)
			errflg = '1';
		}

		@* Return Flase if Error Occur *@
			if (errflg != '') {
                return false;
		}

	});

</script>