@ModelType ExternalLoginListViewModel
@Imports Microsoft.Owin.Security
@Code
    Dim loginProviders = Context.GetOwinContext().Authentication.GetExternalAuthenticationTypes()
End Code
<h4>別のサービスを使用してログインしてください。</h4>
<hr />
@If loginProviders.Count() = 0 Then
    @<div>
          <p>
              構成済みの外部認証サービスはありません。外部サービス経由のログインをサポートするようにこの ASP.NET アプリケーションを設定する方法の
              詳細については、<a href="http://go.microsoft.com/fwlink/?LinkId=403804">こちらの記事</a>をご覧ください。
          </p>
    </div>
Else
    @Using Html.BeginForm("ExternalLogin", "Account", New With {.ReturnUrl = Model.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
        @Html.AntiForgeryToken()
        @<div id="socialLoginList">
           <p>
               @For Each p As AuthenticationDescription In loginProviders
                   @<button type="submit" class="btn btn-default" id="@p.AuthenticationType" name="provider" value="@p.AuthenticationType" title="@p.Caption アカウントを使用してログイン">@p.AuthenticationType</button>
               Next
           </p>
        </div>
    End Using
End If
