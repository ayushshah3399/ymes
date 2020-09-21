﻿@ModelType MES_WEB.d_mes0100
@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
        <div class="col-12" style="background-color:pink;max-width:353px" id="ContainerID_1">

            @Using Html.BeginForm("Index", "A1035_PayOutCancel", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*This is Back Button*@
                <div class="form-group form-group-Custom">
                    <button type="button" Class="btn btn-secondary" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>
                </div>

                @*This is For picking number textbox*@
                <div class="form-group form-group-Custom focus">
                    <div class="control-label">@LangResources.A1030_03_Picking_NO</div>
                    @Html.EditorFor(Function(model) model.TxtBox_picking_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.A1030_03_Picking_NO, .autocomplete = "off", .maxlength = 15}})
                    @Html.ValidationMessageFor(Function(model) model.TxtBox_picking_no, "", New With {.class = "text-danger"})
                </div>
                <div id="errorPicking_NODataNotFound" style="color:red;font-size:15px">@TempData("errorPicking_NODataNotFound")</div>

                <div class="form-group form-group-Custom">
                    <button id="btnhidden" name="btnhidden" type="submit" value="Create" class="btn btn-primary" hidden="hidden"></button>
                    <div id="LblTxtEmpty_Picking_NO" Class="control-label invisible" hidden="hidden">@LangResources.MSG_A1030_04_Empty_Picking_NO</div>
                </div>

                </text>
            End Using
        </div>
	</div>
</div>

@*Javascript Validation*@
<script>

	@*This is validation and will get data from master*@
	$('#TxtBox_picking_no').keypress(function (e)
	{
		if (e.keyCode == 13)
		{
			@*Validation *@
			var txtpicking_no = $('#TxtBox_picking_no').val();
			var ObjLblTxtpicking_noEmpty = $('#LblTxtEmpty_Picking_NO').text();
			var errflg = '';

			@* Error Message Become Null *@
			$("#errorPicking_NODataNotFound").text("");

			if (txtpicking_no == '')
			{

				$("#errorPicking_NODataNotFound").text(ObjLblTxtpicking_noEmpty);
				errflg = '1';

			}

			@* Return Flase if Error Occur *@
			if (errflg != '') {
				return false
			}

		};

	});

</script>