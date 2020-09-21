@Code
	ViewData("Title") = "Index"
	ViewData.Item("ID") = LangResources.MSG_Comm_Error
	Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<!DOCTYPE html>
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width" />
	<title>Error</title>
</head>
<body>
	<div class="container-fluid">
		<div class="row justify-content-center">
			<div class="col-12 col-sm-12 col-md-12">
				<p style="padding-top:10px;">
					<h4 class="text-danger">@Model.Exception.Message</h4>
				</p>
                <p>
                    <b>Controller Name : </b>@Model.ControllerName
                    <br />
                    <b>Action Name : </b> @Model.ActionName
                    <br />
                    <b>Line No : </b>@TempData("Line")
                </p>
				<p>
					<h6>InnerException :</h6>
					@If Model.Exception.InnerException IsNot Nothing Then
						@Html.Raw(Model.Exception.InnerException.ToString.Replace(vbCrLf, "<br />"))
					End If
				</p>
				<p>
					<h6>StackTrace :</h6>
					@If Model.Exception.Stacktrace IsNot Nothing Then
						@Html.Raw(Model.Exception.Stacktrace.ToString.Replace(vbCrLf, "<br />"))
					End If
				</p>
			</div>
		</div>
	</div>
</body>
</html>
