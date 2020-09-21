@ModelType IEnumerable(Of NTV_SHIFT.D0010)
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="container" >
    <div class="row">

        <div class="col-xs-2" style="background-color:lightyellow">
            <div><label>アナウンサー一覧</label></div>
            <div>@Html.Partial("_UserListParital", ViewData.Item("UserList"))</div>
        </div>

        <div class="col-xs-10">

            <div class="container">
                <div class="row" >
                    <div class="col-xs-8">
                        <ul class="nav nav-pills">
                            <li><a href="#">本日</a></li>
                            <li><a href="#">前日</a></li>
                            <li><input class="form-control datepicker" type="text"></li>
                            <li><a href="#">表示</a></li>
                            <li><a href="#">翌日</a></li>
                        </ul>
                    </div>
                    <div class="col-xs-4">
                        <ul class="nav nav-pills navbar-right">
                            <li><a href="#">印刷</a></li>
                            <li><a href="#">最新情報</a></li>
                            <li><a href="#">戻る</a></li>
                        </ul>
                    </div>
                </div>

                <div class="row" >
                    <ul class="nav nav-pills">
                        <li>@Html.ActionLink("管理メニュー", "Index", "A0100")</li>
                        <li><a href="#">休日表</a></li>
                        <li><a href="#">休日設定</a></li>
                        <li><a href="#">業務登録</a></li>
                        <li><a href="#">業務情報</a></li>
                        <li><a href="#">業務承認</a></li>
                        <li><a href="#">デスクメモ</a></li>
                    </ul>
                </div>

                @*<br />*@
   
                <div class="row">

                    <div class="col-xs-9" >

                        <ul class="breadcrumb">
                            <li><a href="#">テスク</a></li>
                            <li><a href="#">地デジ</a></li>
                            <li><a href="#">NN24</a></li>
                            <li><a href="#">BS</a></li>
                        </ul>

                        <div class="panel">
                            <div class="panel-body">
                                <label>デスク  </label>

                                <table class="table">
                                    <tr>
                                        <th></th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            @Html.ActionLink("修正", "Edit", "")
                                        </td>
                                        <td>テスト番組1</td>
                                        <td>2017/1/13</td>
                                        <td> 2018/1/13</td>
                                        <td>◎◎◎◎◎◎◎、○○○○○○○</td>

                                    </tr>
                                    <tr>
                                        <td>
                                            @Html.ActionLink("修正", "Edit", "")
                                        </td>
                                        <td>テスト番組2</td>
                                        <td>2017/1/13</td>
                                        <td> 2018/1/13</td>
                                        <td>◎◎◎◎◎◎◎、○○○○○○○</td>
                                    </tr>
                                </table>

                                <label>地デジ</label>
                                <table class="table">
                                    <tr>
                                        <th></th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.OAJKNST)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.OAJKNED)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BASYO)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                                        </th>
                                        <th>
                                            @Html.DisplayNameFor(Function(model) model.BIKO)
                                        </th>

                                    </tr>
                                    <tr>
                                        <td>
                                            @Html.ActionLink("修正", "Edit", "")
                                        </td>
                                        <td>10:00</td>
                                        <td>11:00</td>
                                        <td>ニュースA</td>
                                        <td>09:00</td>
                                        <td>12:00</td>
                                        <td>◎◎◎◎◎◎◎、○○○○○○○</td>
                                        <td>スタジオ</td>
                                        <td>◇◇◇◇◇◇</td>
                                        <td>12-34568</td>
                                        <td></td>
                                    </tr>

                                </table>

                                <label>NN24</label>
                                <p>該当データがありません。</p>

                                <br />
                                <label>BS</label>
                                <p>該当データがありません。</p>
                            </div>
                        </div>

                    </div>

                    <div class="col-xs-3" style="background-color:lavender">
                        @Html.Partial("_MessagePartial")
                    </div>

                </div>

            </div>

        </div>

    </div>
</div>

@*<div class="body-content">
    <div class="row">
        @*<div class="col-lg-2">
            <div class="pull-left">
                <div><label>アナウンサー一覧</label></div>
                <div>
                    @Html.Partial("_UserListParital", ViewData.Item("UserList"))
                </div>
            </div>

        </div>

        <div class="col-lg-10">
            <div class="row">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8">
                            <ul class="nav nav-pills">
                                <li><a href="#">本日</a></li>
                                <li><a href="#">前日</a></li>
                                <li><input class="form-control datepicker" type="text"></li>
                                <li><a href="#">表示</a></li>
                                <li><a href="#">翌日</a></li>
                            </ul>
                        </div>
                        <div class="col-lg-4 text-right">
                            <ul class="nav nav-pills navbar-right">
                                <li><a href="#">印刷</a></li>
                                <li><a href="#">最新情報</a></li>
                                <li><a href="#">戻る</a></li>
                            </ul>
                        </div>
                    </div>

                    <ul class="nav nav-pills">
                        <li>@Html.ActionLink("管理メニュー", "Index", "A0100")</li>
                        <li><a href="#">休日表</a></li>
                        <li><a href="#">休日設定</a></li>
                        <li><a href="#">業務登録</a></li>
                        <li><a href="#">業務情報</a></li>
                        <li><a href="#">業務承認</a></li>
                        <li><a href="#">デスクメモ</a></li>
                    </ul>

                    <br />

                    <ul class="breadcrumb">
                        <li><a href="#">テスク</a></li>
                        <li><a href="#">他デジ</a></li>
                        <li><a href="#">BS</a></li>
                        <li><a href="#">NN24</a></li>
                    </ul>

                </div>
            </div>
            <div class="row">

                <div class="col_lg_8">

                    <div>
                        <label>デスク</label>
                        @*<table class="table">
                            <tr>
                                <th>
                                    @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                                </th>
                                <th>
                                    @Html.DisplayNameFor(Function(model) model.GYOMYMD)
                                </th>
                                @*<th>
                                    @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
                                </th>
                                <th>
                                    @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                                </th>*@

@*<th>
        @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.M0090.IKKATUMEMO)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.KSKJKNST)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.KSKJKNED)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.JTJKNST)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.JTJKNED)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.OAJKNST)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.OAJKNED)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.NAIYO)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.BASYO)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.BIKO)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.RNZK)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.PGYOMNO)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.IKTFLG)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.IKTUSERID)
    </th>*@

@*<th></th>
    </tr>*@

@*@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
        </td>*@
@*<td>
        @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
    </td>
    <td>
        @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
    </td>*@

@*<td>
                @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.M0090.IKKATUMEMO)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.KSKJKNST)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.KSKJKNED)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.JTJKNST)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.JTJKNED)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.OAJKNST)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.OAJKNED)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.NAIYO)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.BASYO)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.BIKO)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.RNZK)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.PGYOMNO)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.IKTFLG)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.IKTUSERID)
            </td>@*

                    <td>
                        @Html.ActionLink("修正", "Edit", New With {.id = item.GYOMNO}) |
                        @Html.ActionLink("参照", "Details", New With {.id = item.GYOMNO}) |
                        @Html.ActionLink("削除", "Delete", New With {.id = item.GYOMNO})
                    </td>
                </tr>
            Next

        </table>

    </div>

    <div>
        <label>他デジ</label>*@
@*<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.OAJKNST)
        </th>
        @*<th>
            @Html.DisplayNameFor(Function(model) model.OAJKNED)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BASYO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BIKO)
        </th>*@


@*                                      <th>
            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
        </th>
                                                <th>
        @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.M0090.IKKATUMEMO)
    </th>

    <th>
        @Html.DisplayNameFor(Function(model) model.JTJKNST)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.JTJKNED)
    </th>

    <th>
        @Html.DisplayNameFor(Function(model) model.NAIYO)
    </th>


    <th>
        @Html.DisplayNameFor(Function(model) model.RNZK)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.PGYOMNO)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.IKTFLG)
    </th>
    <th>
        @Html.DisplayNameFor(Function(model) model.IKTUSERID)
    </th>

            <th></th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OAJKNST)
                </td>
                @*<td>
                    @Html.DisplayFor(Function(modelItem) item.OAJKNED)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.KSKJKNST)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.KSKJKNED)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.BASYO)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.BIKO)
                </td>*@

@* <td>
                                    @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
                                </td>
                                <td>
                                    @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
                                </td>
                                                            <td>
                                        @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.M0090.IKKATUMEMO)
                                    </td>

                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.JTJKNST)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.JTJKNED)
                                    </td>

                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.RNZK)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.PGYOMNO)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.IKTFLG)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(modelItem) item.IKTUSERID)
                                    </td>

                                        <td>
                                            @Html.ActionLink("修正", "Edit", New With {.id = item.GYOMNO}) |
                                            @Html.ActionLink("参照", "Details", New With {.id = item.GYOMNO}) |
                                            @Html.ActionLink("削除", "Delete", New With {.id = item.GYOMNO})
                                        </td>
                                    </tr>
                                Next

                            </table>

                        </div>

                    </div>

                    <div class="col_lg_2">
                        <div class="pull-right">
                            @Html.Partial("_MessagePartial")

                        </div>
                    </div>

                </div>

                </div>

        </div>

    </div>*@
