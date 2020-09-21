@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div >
    @Html.Partial("_MenuPartial", "")
</div>

@*<h2>管理メニュー</h2>
<ul class="nav nav-pills">
    <li>@Html.ActionLink("ユーザー設定", "Index", "M0010")</li>
    <li>@Html.ActionLink("カテゴリー設定", "Index", "M0010")</li>
    <li>@Html.ActionLink("番組設定", "Index", "M0010")</li>
    <li>@Html.ActionLink("内容設定", "Index", "M0010")</li>
    <li>@Html.ActionLink("変更履歴", "Index", "M0010")</li>
</ul>*@

<!-- 
<a href="#" class="btn btn-primary">ユーザー設定</a>
<a href="#" class="btn btn-primary">カテゴリー設定</a>
<a href="#" class="btn btn-primary">番組設定</a>
<a href="#" class="btn btn-primary">内容設定</a>
<a href="#" class="btn btn-primary">変更履歴</a>
-->

<br />
<div class="text-center">
    <h5><b>メニューを選択してください。</b></h5>
    </div>
<br />
<br />
<br />
<br />

@*<ul class="nav nav-pills">

    <li class="active"><a href="#">ユーザー設定</a></li>
    <li><a href="#">カテゴリー設定</a></li>
    <li><a href="#">番組設定</a></li>
    <li><a href="#">内容設定</a></li>
    <li><a href="#">変更履歴</a></li>

</ul>*@
