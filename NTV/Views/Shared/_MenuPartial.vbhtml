
<div class="row">
    <div class="col-md-3" style="padding-top:10px;">
        <h4><b>管理メニュー</b></h4>
    </div>
    <div class="col-md-9 text-right">
        <p>
            <ul class="nav nav-pills navbar-right">

                <li>@Html.ActionLink("ガントチャートに戻る", "Index", "C0050")</li>
            </ul>
        </p>
    </div>
</div>

<ul class="nav nav-pills">

    @If ViewBag.LoginUserACCESSLVLCD <> "3" OrElse (ViewBag.LoginUserACCESSLVLCD = "3" AndAlso Session("LoginUserDeskChief") = 1) Then
        @If Model = "1" Then
            @<li class="active"> @Html.ActionLink("ユーザー設定", "Index", "A0110") </li>
        Else
            @<li>  @Html.ActionLink("ユーザー設定", "Index", "A0110")</li>
        End If
    End If

    @If ViewBag.LoginUserACCESSLVLCD <> "3" Then
        @If Model = "2" Then
            @<li class="active">@Html.ActionLink("カテゴリー設定", "Index", "A0120")</li>
        Else
            @<li>@Html.ActionLink("カテゴリー設定", "Index", "A0120")</li>
        End If

    End If

    @If Model = "3" Then
        @<li class="active">@Html.ActionLink("番組設定", "Index", "A0130")</li>
    Else
        @<li>@Html.ActionLink("番組設定", "Index", "A0130")</li>
    End If

    @If ViewBag.LoginUserACCESSLVLCD <> "3" Then

        @If Model = "4" Then
            @<li class="active">@Html.ActionLink("内容設定", "Index", "A0140")</li>
        Else
            @<li>@Html.ActionLink("内容設定", "Index", "A0140")</li>
        End If

    End If

    @If (ViewBag.LoginUserSystem = True OrElse ViewBag.LoginUserKanri = True) AndAlso ViewBag.LoginUserACCESSLVLCD <> "3" Then
        @If Model = "5" Then
            @<li class="active">@Html.ActionLink("休暇コード設定", "Index", "A0150")</li>
        Else
            @<li>@Html.ActionLink("休暇コード設定", "Index", "A0150")</li>
        End If
    End If

    @If Model = "6" Then
        @<li class="active">@Html.ActionLink("業務変更履歴", "Index", "A0160")</li>
    Else
        @<li>@Html.ActionLink("業務変更履歴", "Index", "A0160")</li>
    End If

    @If Model = "8" Then
        @<li class="active">@Html.ActionLink("休暇申請履歴", "Index", "A0180")</li>
    Else
        @<li>@Html.ActionLink("休暇申請履歴", "Index", "A0180")</li>
    End If

    @If ViewBag.KOKYUTENKAI = "1" OrElse ViewBag.KOKYUTENKAIALL = "1" Then
        @If Model = "7" Then
            @<li class="active">@Html.ActionLink("業務一括登録", "Index", "A0170")</li>
        Else
            @<li>@Html.ActionLink("業務一括登録", "Index", "A0170")</li>
        End If
    End If

    @If ViewBag.LoginUserACCESSLVLCD <> "3" OrElse (ViewBag.LoginUserACCESSLVLCD = "3" AndAlso Session("LoginUserDeskChief") = 1) Then
        @If Model = "9" Then
            @<li class="active">@Html.ActionLink("スポーツカテゴリー設定", "Index", "A0210")</li>
        Else
            @<li>@Html.ActionLink("スポーツカテゴリー設定", "Index", "A0210")</li>
        End If
    End If

</ul>

