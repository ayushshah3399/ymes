@ModelType MES_WEB.d_mes0150
@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
		<div class="col-12" style="background-color:pink;max-width:353px" id="ContainerID_1">

			@Using Html.BeginForm("Index", "A1015_ReceiveCancel", FormMethod.Post, New With {.class = "Form-Horizontal"})
				@Html.AntiForgeryToken()
				@<Text>

				@Html.ValidationSummary(True, "", New With {.class = "text-danger"})

				@*Back To Menu Button*@
				<button id="btnbacktomenu_A1015" name="btnbacktomenu_A1015" type="button" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>

				<br>

				@*Barcode TextBox*@
				<div class="form-group form-group-Custom focus">
					<div id="lblbarcode" class="control-label control-label-Custom">@LangResources.A1010_16_POBarCode</div>
					@Html.EditorFor(Function(model) model.barcode, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.A1010_16_POBarCode, .autocomplete = "off", .maxlength = 18}})
					@Html.ValidationMessageFor(Function(model) model.barcode, "", New With {.class = "text-danger"})
				</div>
				<div id="errorDataNotFound" style="color:red;font-size:15px">@TempData("errorDataNotFound")</div>

				@*This is hidden button for Submit part*@
				<div class="form-group form-group-Custom">
					<button id="btnhidden" name="btnhidden" type="submit" value="Create" class="btn btn-primary" hidden="hidden"></button>
					<div id="LblTxtBarcodeEmpty" Class="control-label invisible" hidden="hidden">@LangResources.MSG_A1010_17_TxtBarcodeEmpty</div>
				</div>

				</text>
            End Using
		</div>
	</div>
</div>

@*Javascript Validation*@
<script>

	@*This is validation and will get data from master*@
	$('#barcode').keypress(function (e)
	{
		if (e.keyCode == 13)
		{
			@*Validation *@
			var txtbarcode = $('#barcode').val();
			var ObjLblTxtBarcodeEmpty = $('#LblTxtBarcodeEmpty').text();
			var errflg = '';

			@* Error Message Become Null *@
			$("#errorDataNotFound").text("");

			if (txtbarcode == '')
			{

				$("#errorDataNotFound").text(ObjLblTxtBarcodeEmpty);
				errflg = '1';

			}

			@* Return Flase if Error Occur *@
			if (errflg != '') {
				return false
			}

		};

	});

</script>