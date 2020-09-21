@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br /><br /><br /><br /><br />
<center><h2 style="color:red"><b>@TempData("ErrExceptionMsg")</b></h2></center>
<br /><br />
@*This is For Login Button*@
<div class="form-group">
    <center>
        <div class="col-md-2">
            <button id="BtnOk" name="BtnOk" type="submit" class="btn btn-primary text-center" style=" font-size:20px" onclick="location.href='@Url.Action("index", "Login")'">@LangResources.Common_BacktoMenu</button>
        </div>
    </center>
</div>