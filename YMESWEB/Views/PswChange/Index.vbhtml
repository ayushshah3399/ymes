@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "PswChange", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*This is for Userid Textbox*@
                <div class="form-group focus">
                    <div class="control-label input-sm" style="font-size:15px;width:auto">@LangResources.L1_02_UserID</div>
                    @Html.TextBox("PswChnageLoinId", TempData("LoinId"), htmlAttributes:=New With {.class = "form-control input-sm", .autocomplete = "off", .style = "ime-mode: disabled;max-width:1000px;font-size:15px", .placeholder = LangResources.L1_09_UserIdPlaceHolder, .maxlength = 20})
                    @Html.ValidationMessage("PswChnageLoinId", htmlAttributes:=New With {.class = "text-danger"})
                    <div id="errorLoinId_pswchange" style="color:red;font-size:15px"></div>
                </div>

                @*This is For Password Textbox*@
                <div class="form-group">
                    <div id="lbloldpsw" class="control-label input-sm" style="font-size:15px;width:auto">@LangResources.L1_04_OldPassword</div>
                    @Html.Password("OldPassword", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .autocomplete = "off", .style = "max-width:1000px;font-size:15px", .placeholder = LangResources.L1_11_OldPasswordPlaceHolder, .maxlength = 24, .onpaste = "return false", .oncopy = "return false", .oncontextmenu = "return false"})
                    <div id="OlderrorPassword" style="color:red;font-size:15px"></div>
                </div>

                @*This is For Password Textbox*@
                <div class="form-group">
                    <div id="LblnewPsw" class="control-label input-sm" style="font-size:15px;width:auto">@LangResources.L1_05_NewPassword</div>
                    @Html.Password("NewPassword", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .autocomplete = "off", .style = "max-width:1000px;font-size:15px", .placeholder = LangResources.L1_12_NewPasswordPlaceHolder, .maxlength = 24, .onpaste = "return false", .oncopy = "return false", .oncontextmenu = "return false"})
                    <div id="NewerrorPassword" style="color:red;font-size:15px"></div>
                </div>

                @*This is For Password Textbox*@
                <div class="form-group">
                    <div id="lblconfirmpsw" class="control-label input-sm" style="font-size:15px;width:auto">@LangResources.L1_06_ConfirmNewPassword</div>
                    @Html.Password("ConfirmPassword", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .autocomplete = "off", .style = "max-width:1000px;font-size:15px", .placeholder = LangResources.L1_13_ConfirmNewPasswordPlaceHolder, .maxlength = 24, .onpaste = "return false", .oncopy = "return false", .oncontextmenu = "return false"})
                    <div id="ConfirmNewerrorPassword" style="color:red;font-size:15px"></div>
                </div>

                @*This is For Change Button*@
                <div class="form-group">
                    <p id="ChangepswErrorlbl" style="color:red;font-size:15px">@TempData("LoginErrMsg")</p>
                    <Center>
                        <Button id="btnback" name="btnback" value="1" type="button" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Login")'">@LangResources.Common_Previous</Button>
                        <button id="btnChangePsw" name="btnChangePsw" type="submit" class="btn btn-primary" style="font-size :15px;width:120px;margin-left:5px;"><b>@LangResources.L1_08_Change</b></button>
                    </Center>
                </div>

                @*This is For  Hidden label to Get Value in Jquery*@
                <div Class="form-group hidden">

                    @*It is Submit Button type So it will actomatically called controller*@
                    <div id="LblEmptyUserID" Class="control-label" hidden="hidden">@LangResources.MSG_L1_14_UserIdEmpty</div>
                    <div id="LblOldEmptyPsw" Class="control-label" hidden="hidden">@LangResources.MSG_L1_16_OldPasswordEmpty</div>
                    <div id="LblNewEmptyUserID" Class="control-label" hidden="hidden">@LangResources.MSG_L1_17_NewPasswordEmpty</div>
                    <div id="LblConfirmNewEmptyPsw" Class="control-label" hidden="hidden">@LangResources.MSG_L1_18_ConfirmNewPasswordEmpty</div>
                    <div id="LblNewandConfirmPswDiffrent" Class="control-label" hidden="hidden">@LangResources.MSG_L1_23_NewAndConfirmPswDiffrent</div>
                    <div id="LblOldAndNewPswSame" Class="control-label" hidden="hidden">@LangResources.MSG_L1_22_OldAndNewPswSame</div>

                </div>

                </text>
            End Using
        </div>
	</div>
</div>
@* Jquery Validation *@
<script>

	@*This is For Validation Of Both TextBox*@
	$('#btnChangePsw').on('click', function (e) {

		@* Validation *@
		var PswChnageLoinId = $('#PswChnageLoinId').val();
		var Password = $('#OldPassword').val();
		var NewPassword = $('#NewPassword').val();
		var ConfirmNewPassword = $('#ConfirmPassword').val();
		var LblEmptyUserID = $('#LblEmptyUserID').text();
		var LblOldEmptyPsw = $('#LblOldEmptyPsw').text();
		var LblNewEmptyUserID = $('#LblNewEmptyUserID').text();
		var LblConfirmNewEmptyPsw = $('#LblConfirmNewEmptyPsw').text();
		var LblNewandConfirmPswDiffrent = $('#LblNewandConfirmPswDiffrent').text();
		var LblOldAndNewPswSame = $('#LblOldAndNewPswSame').text();
		var errflg = '';

		@* Error Message Become Null *@
		$("#errorLogin").text("");
		$("#errorLoinId_pswchange").text("");
		$("#OlderrorPassword").text("");
		$("#NewerrorPassword").text("");
		$("#ConfirmNewerrorPassword").text("");
		$("#ChangepswErrorlbl").text("");

		@* Error Message If Login Is Null *@
		if (PswChnageLoinId == '') {
			$("#errorLoinId_pswchange").text(LblEmptyUserID);
			errflg = '1';
		}

		@* Error Message If Password Is Null *@
		if (Password == '') {
			$("#OlderrorPassword").text(LblOldEmptyPsw);
			errflg = '1';
		}

		@* Error Message If Newpassword Is Null *@
		if (NewPassword == '') {
			$("#NewerrorPassword").text(LblNewEmptyUserID);
			errflg = '1';
		}

		@* Error Message If ConfirmNewpassword Is Null *@
		if (ConfirmNewPassword == '') {
			$("#ConfirmNewerrorPassword").text(LblConfirmNewEmptyPsw);
			errflg = '1';
		}

		@*If New password and confirm password is not same then display error message*@
		if (PswChnageLoinId != '' && Password != '' && NewPassword != '' && ConfirmNewPassword != '' && NewPassword != ConfirmNewPassword) {
			$("#ConfirmNewerrorPassword").text(LblNewandConfirmPswDiffrent);
			errflg = '1';
		}

		@* If password and confirm password is same then display error message *@
		if (PswChnageLoinId != '' && Password != '' && NewPassword != '' && ConfirmNewPassword != '' && NewPassword == Password) {
			$("#ConfirmNewerrorPassword").text(LblOldAndNewPswSame);
			errflg = '1';
		}

		@* Return Flase if Error Occur *@
		if (errflg != '') {
			return false
		}

	});

</script>