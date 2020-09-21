@ModelType System.Web.Mvc.HandleErrorInfo

@Code
    ViewBag.Title = "エラー"
    ViewData!LoginUsernm = Session("LoginUsernm")
End Code

<h1 class="text-danger">エラー</h1>
<h2 class="text-danger">要求の処理中にエラーが発生しました。</h2>
<br />
<h4 class="text-danger">@Model.Exception.Message</h4>

@If Model.Exception.InnerException IsNot Nothing Then
    @<hr />

   @Html.Raw(Model.Exception.InnerException.ToString.Replace(vbCrLf, "<br />"))
End If
    

