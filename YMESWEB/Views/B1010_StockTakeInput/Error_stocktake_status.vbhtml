@Code
    ViewData("Title") = "Error_stocktake_status"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br /><br /><br /><br /><br />
<center><h2 style="color:green"><b>@LangResources.MSG_B1010_04_TarauroshiNotStart</b></h2></center>
<br /><br />
@*This is For Login Button*@
<div class="form-group">
    <center>
        <div class="col-md-2">
            <button id="BtnOk" name="BtnOk" type="submit" class="btn btn-primary text-center" style=" font-size:20px" onclick="location.href='@Url.Action("index", "Menu")'">@LangResources.Common_BacktoMenu</button>
        </div>
    </center>
</div>