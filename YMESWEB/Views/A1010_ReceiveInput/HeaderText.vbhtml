@ModelType MES_WEB.d_mes0150
@Code
    ViewData("Title") = "HeaderText"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim header_qr_type = ViewData("header_qr_type")
End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("HeaderText", "A1010_ReceiveInput", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                <div Class="input-group">
                    @*This is for back to menu button*@
                    <button id="btnbacktomenu_A1010" name="btnbacktomenu_A1010" type="button" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>
                    <Button id="GOWOINPUT" name="GOWOINPUT" type="button" Class="btn btn-secondary Button-Custom ml-auto" style="width:140px;" onclick="location.href='@Url.Action("Index", "A1010_ReceiveInput", New RouteValueDictionary(New With {.GOWOINPUT = "1"}))'">@LangResources.A1010_30_GotoWO</Button>
                </div>               

                @if header_qr_type = "1" Then

                    @*This is for header_text textbox*@
                    @<div Class="form-group form-group-Custom focus">
                        <div id="lblheader_text" Class="control-label control-label-Custom">@LangResources.A1010_20_HeaderText</div>
                        @Html.EditorFor(Function(model) model.header_text, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.A1010_20_HeaderText, .autocomplete = "off", .maxlength = 26}})
                        @*@Html.ValidationMessageFor(Function(model) model.header_text, "", New With {.class = "text-danger"})*@
                    </div>

                Else

                    @<div class="form-group form-group-Custom focus">
                        <div id="lblheader_text" class="control-label control-label-Custom">@LangResources.A1010_20_HeaderText</div>
                        @Html.EditorFor(Function(model) model.header_text, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.A1010_20_HeaderText, .autocomplete = "off"}})
                        @*@Html.ValidationMessageFor(Function(model) model.header_text, "", New With {.class = "text-danger"})*@
                    </div>

                End If
                <div id="errorDataNotFound" style="color:red;font-size:15px">@TempData("errorDataNotFound")</div>

                @*This is hidden button for Submit part*@
                <div class="form-group form-group-Custom">
                    <button id="btnhidden" name="btnhidden" type="submit" class="btn btn-primary" hidden="hidden"></button>
                    <div id="LblTxtheadertextEmpty" Class="control-label invisible">@LangResources.MSG_A1010_27_HeaderTextempty</div>
                </div>

                </text>
            End Using
        </div>
    </div>
</div>

@*Javascript Validation*@
<script>

    $(document).ready(function () {
        var errorDataNotFound = $('#errorDataNotFound').text();
        if (errorDataNotFound != '') {
            $('#header_text').select();
        };
    });
    
	@*This is validation and will get data from master*@
    $('#header_text').keypress(function (e)
	{
		if (e.keyCode == 13)
		{
			@*Validation *@
            var txtbarcode = $('#header_text').val();
            var ObjLblTxtheadertextEmpty = $('#LblTxtheadertextEmpty').text();
			var errflg = '';

			@* Error Message Become Null *@
			$("#errorDataNotFound").text("");

			if (txtbarcode == '')
			{

                $("#errorDataNotFound").text(ObjLblTxtheadertextEmpty);
				errflg = '1';

			}

			@* Return Flase if Error Occur *@
			if (errflg != '') {
				return false
			}

		};

	});

</script>