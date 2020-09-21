@ModelType IndexViewModel
@Code
    ViewBag.Title = "管理"
End Code

<h2>@ViewBag.Title.</h2>

<p class="text-success">@ViewBag.StatusMessage</p>
<div>
    <h4>アカウント設定の変更</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>パスワード:</dt>
        <dd>
            [
            @If Model.HasPassword Then
                @Html.ActionLink("パスワードの変更", "ChangePassword")
            Else
                @Html.ActionLink("作成", "SetPassword")
            End If
            ]
        </dd>
        <dt>外部ログイン:</dt>
        <dd>
            @Model.Logins.Count [
            @Html.ActionLink("管理", "ManageLogins") ]
        </dd>
        @*
            電話番号を 2 要素認証システムの第 2 認証要素としてお使いいただけます。
             
             SMS を使用する 2 要素認証をサポートするようにこの ASP.NET アプリケーションを設定する方法の詳細については、
                <a href="http://go.microsoft.com/fwlink/?LinkId=403804">こちらの記事</a>をご覧ください。
             
             2 要素認証の設定が完了したら、次のブロックのコメントを解除してください。
        *@
        @* 
            <dt>電話番号:</dt>
            <dd>
                @(If(Model.PhoneNumber, "なし")) [
                @If (Model.PhoneNumber <> Nothing) Then
                    @Html.ActionLink("変更", "AddPhoneNumber")
                    @: &nbsp;|&nbsp;
                    @Html.ActionLink("削除", "RemovePhoneNumber")
                Else
                    @Html.ActionLink("追加", "AddPhoneNumber")
                End If
                ]
            </dd>
        *@
        <dt>2 要素認証:</dt>
        <dd>
            <p>
                構成済みの 2 要素認証プロバイダーはありません。2 要素認証をサポートするようにこの ASP.NET アプリケーションを設定する方法の
                詳細については、<a href="http://go.microsoft.com/fwlink/?LinkId=403804">こちらの記事</a>をご覧ください。
            </p>
            @*
                @If Model.TwoFactor Then
                    @Using Html.BeginForm("DisableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      有効
                      <input type="submit" value="無効にする" class="btn btn-link" />
                      </text>
                    End Using
                Else
                    @Using Html.BeginForm("EnableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      無効
                      <input type="submit" value="有効にする" class="btn btn-link" />
                      </text>
                    End Using
                End If
	     *@
        </dd>
    </dl>
</div>
