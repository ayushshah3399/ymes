@Imports System.Web.Mvc
@Code
    ViewData("Title") = "_LoginPartial"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "Login", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<text>

                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                    @*This is For Set Version Lable*@
                    <div id="LblVersion" class="control-label" style="text-align:right;color:black;font-size:13px"><b>Version: &nbsp; @Session("AssembliVersion")</b></div>

                    @*Laungage Combo Box*@
                    <div class="form-group float-right">
                        @*This is For Set lanugage Combobox*@
                        @Html.DropDownList("language", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .onchange = "Fakeforlang.click()", .style = "font-size:13px;height:auto"})
                    </div>

                    @*Break After Combo box*@
                    <br /><br />

                    @*This is for Userid Textbox*@
                    <div class="form-group focus ">
                        <label class="control-label input-sm" style="font-size:15px;width:auto">@LangResources.L1_02_UserID</label>
                        @Html.TextBox("LoinId", TempData("LoinId"), htmlAttributes:=New With {.class = "form-control input-sm", .autocomplete = "off", .style = "ime-mode: disabled;max-width:1000px;font-size:15px", .placeholder = LangResources.L1_09_UserIdPlaceHolder, .maxlength = 20})
                        @Html.ValidationMessage("LoinId", htmlAttributes:=New With {.class = "text-danger"})
                        <span id="errorLoinId" style="color:red;font-size:15px"></span>
                    </div>

                    @*This is For Password Textbox*@
                    <div class="form-group">
                        <label class="control-label input-sm" style="font-size:15px;width:auto">@LangResources.L1_03_Password</label>
                        @Html.Password("Password", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .autocomplete = "off", .style = "max-width:1000px;font-size:15px", .placeholder = LangResources.L1_10_PasswordPlaceHolder, .maxlength = 24, .onpaste = "return false", .oncopy = "return false", .oncontextmenu = "return false"})
                        <span id="errorPassword" style="color:red;font-size:15px"></span>
                    </div>

                    @*This is For Login Button*@
                    <div class="form-group">
                        <p id="errorLogin" style="color:red;font-size:15px">@TempData("LoginErrMsg")</p>
                        <center>
                            <button id="btnLogin" name="btnLogin" type="submit" style="font-size:15px" Value="2" class="btn btn-primary text-center"><b>@LangResources.L1_01_Login</b></button>
                        </center>
                    </div>

                    @*This is For  Forget PassWord Link*@
                    @*This is password change link redrirect uppar submit button*@
                    <div class="form-group float-right">
                        <a id="PswChange" name="PswChange" class="form-group-sm" style="font-size:15px" href="@Url.Action("Index", "PswChange")" onclick="SetUserid();">@LangResources.L1_07_ChangePassword </a>
                    </div>

                    @*This is For  Hidden label to Get Value in Jquery*@
                    @*It is Submit Button type So it will actomatically called controller*@
                    <div class="form-group-sm hidden">
                        <div id="LblEmptyUserID" class="control-label invisible">@LangResources.MSG_L1_14_UserIdEmpty</div>
                        <div id="LblEmptyPsw" class="control-label invisible">@LangResources.MSG_L1_15_PasswordEmpty</div>
                    </div>

                </text>
            End Using
        </div>
    </div>
</div>
@* Jquery Validation *@
<script>

	@* This is validation and will get data from master *@
	$('#language').change(function (e) {

		$("#btnLogin").val("1");
		$("#btnLogin").click();

	});

	@*This is For Validation Of Both TextBox*@
	$('#btnLogin').on('click', function (e) {

		@* Validation *@
		var LoinId = $('#LoinId').val();
		var Password = $('#Password').val();
		var LblEmptyUserID = $('#LblEmptyUserID').text();
		var LblEmptyPsw = $('#LblEmptyPsw').text();
		var btnLogin = $("#btnLogin").val();

		if (btnLogin == '2'){
			var errflg = '';

			@* Error Message Become Null *@
			$("#errorLogin").text("");
			$("#errorLoinId").text("");
			$("#errorPassword").text("");

			@* Error Message If Login Is Null *@
			if (LoinId == '') {
				$("#errorLoinId").text(LblEmptyUserID);
				errflg = '1';
			}

			@* Error Message If Password Is Null *@
			if (Password == '') {
				$("#errorPassword").text(LblEmptyPsw);
				errflg = '1';
			}

			@* Return Flase if Error Occur *@
			if (errflg != '') {
				return false
			}
		}
    });

    @*var SetUserid = function ()
    {
        alert('A');
        var LoinId = $('#LoinId').val();
        @TempData['LoinId'] = LoinId;
        return true;
       }*@

</script>