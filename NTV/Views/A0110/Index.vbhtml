@ModelType IEnumerable(Of NTV_SHIFT.M0010)
@Code
    ViewData("Title") = "ユーザー設定"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div>
    @Html.Partial("_MenuPartial", "1")
</div>

<style>
    .table-scroll {
        width: 1489px; /*ASI[26 Nov 2019]: After added SPORTCATEGORY in Table, increased it 1375 to 1443*/
    }


    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-scroll tbody {
        height: 360px;
        width: 1489px; /*ASI[26 Nov 2019]: After added SPORTCATEGORY in Table, increased it 1375 to 1443*/
        overflow-y: auto;
        overflow-x: hidden;
    }

    .col1 {
        width: 50px;
    }

    .usernm {
        width: 110px;
        max-width:110px;
        word-wrap:break-word;
    }

    .loginid {
        width: 110px;
        max-width:110px;
        word-wrap:break-word;
    }

    .usersex, .hyoj {
        width: 50px;
    }

    .kokyu1, .kokyu2, .kokyu3, .kokyu4, .kokyu5, .kokyu6, .kokyu7, .kokyu8 {
        width: 40px;
        text-align:center;
    }

    .accesslevel {
        width: 115px;
        max-width:115px;
        word-wrap:break-word;
   }

    .mailaddress, .keitaiaddress {
        width: 125px;
        max-width:125px;
        word-wrap:break-word;
    }

    .kokyutenkai {
        width: 90px;
    }

    .kariana {
        width: 70px;
    }

    .collast {
        width: 90px;
    }

      body {
        font-size: 12px;
    }

    .sportcategory {
        width: 115px;
        max-width:115px;
        word-wrap:break-word;
    }
      
</style>

<p>
    <ul class="nav nav-pills">
        <li>@Html.ActionLink("新規作成", "Create")</li>
        <li>@Html.ActionLink("表示順設定", "EditHYOJJN")</li>
    </ul>
</p>

<table class="tablecontainer">
    <tr>
        <td>
            <table class="table table-bordered table-scroll table-hover">
                <thead>
                    <tr>
                        <th class="col1"></th>
                        <th class="usernm">
                            @Html.DisplayNameFor(Function(model) model.USERNM)
                        </th>
                        <th class="loginid">
                            @Html.DisplayNameFor(Function(model) model.LOGINID)
                        </th>
                        <th class="usersex">
                            @Html.DisplayNameFor(Function(model) model.USERSEX)
                        </th>
                        <th class="kokyu1">
                            @Html.DisplayNameFor(Function(model) model.KOKYU1)
                        </th>
                        <th class="kokyu2">
                            @Html.DisplayNameFor(Function(model) model.KOKYU2)
                        </th>
                        <th class="kokyu3">
                            @Html.DisplayNameFor(Function(model) model.KOKYU3)
                        </th>
                        <th class="kokyu4">
                            @Html.DisplayNameFor(Function(model) model.KOKYU4)
                        </th>
                        <th class="kokyu5">
                            @Html.DisplayNameFor(Function(model) model.KOKYU5)
                        </th>
                        <th class="kokyu6">
                            @Html.DisplayNameFor(Function(model) model.KOKYU6)
                        </th>
                        <th class="kokyu7">
                            @Html.DisplayNameFor(Function(model) model.KOKYU7)
                        </th>
                        <th class="hyoj">
                            @Html.DisplayNameFor(Function(model) model.HYOJ)
                        </th>
                        <th class="accesslevel">
                            @Html.DisplayNameFor(Function(model) model.M0050.HYOJNM)
                        </th>
                        <th class="mailaddress">
                            @Html.DisplayNameFor(Function(model) model.MAILLADDESS)
                        </th>
                        <th class="keitaiaddress">
                            @Html.DisplayNameFor(Function(model) model.KEITAIADDESS)
                        </th>
                        <th class="kokyutenkai">
                            @Html.DisplayNameFor(Function(model) model.KOKYUTENKAI)
                        </th>
                        <th class="kokyutenkai">
                            @Html.DisplayNameFor(Function(model) model.KOKYUTENKAIALL)
                        </th>
                        <th class="kariana">
                            @Html.DisplayNameFor(Function(model) model.KARIANA)
                        </th>
                         
                        <!--ASI[26 Nov 2019]: Added for SPORTCategory-->
                         <th class="sportcategory">
                            @Html.DisplayNameFor(Function(model) model.SportCatNmComaSeperatedString)
                        </th>
                        
                        <th class="collast"></th>
                    </tr>

                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td class="col1">
                                @Html.ActionLink("修正", "Edit", New With {.id = item.USERID})
                            </td>
                            <td class="usernm">
                                @Html.DisplayFor(Function(modelItem) item.USERNM)
                            </td>
                            <td class="loginid">
                                @Html.DisplayFor(Function(modelItem) item.LOGINID)
                            </td>
                            <td class="usersex">
                                @Html.DisplayFor(Function(modelItem) item.USERSEX)
                            </td>
                            <td class="kokyu1">
                                @Html.DisplayFor(Function(modelItem) item.KYU1)
                            </td>
                            <td class="kokyu2">
                                @Html.DisplayFor(Function(modelItem) item.KYU2)
                            </td>
                            <td class="kokyu3">
                                @Html.DisplayFor(Function(modelItem) item.KYU3)
                            </td>
                            <td class="kokyu4">
                                @Html.DisplayFor(Function(modelItem) item.KYU4)
                            </td>
                            <td class="kokyu5">
                                @Html.DisplayFor(Function(modelItem) item.KYU5)
                            </td>
                            <td class="kokyu6">
                                @Html.DisplayFor(Function(modelItem) item.KYU6)
                            </td>
                            <td class="kokyu7">
                                @Html.DisplayFor(Function(modelItem) item.KYU7)
                            </td>
                            <td class="hyoj">
                                @Html.DisplayFor(Function(modelItem) item.HYOJ)
                            </td>
                            <td class="accesslevel">
                                @Html.DisplayFor(Function(modelItem) item.M0050.HYOJNM)
                            </td>
                            <td class="mailaddress">
                                @Html.DisplayFor(Function(modelItem) item.MAILLADDESS)
                            </td>
                            <td class="keitaiaddress">
                                @Html.DisplayFor(Function(modelItem) item.KEITAIADDESS)
                            </td>
                            <td class="kokyutenkai">
                                @Html.DisplayFor(Function(modelItem) item.KOKYUTENKAI)
                            </td>
                             <td class="kokyutenkai">
                                 @Html.DisplayFor(Function(modelItem) item.KOKYUTENKAIALL)
                             </td>
                            <td class="kariana">
                                @Html.DisplayFor(Function(modelItem) item.KARIANA)
                            </td>

                            <!--ASI[26 Nov 2019]: Added for SPORTCategory-->
                            <td class="sportcategory">
                                @Html.DisplayFor(Function(modelItem) item.SportCatNmComaSeperatedString)
                            </td>

                            <td class="collast">
                                @Html.ActionLink("参照", "Details", New With {.id = item.USERID}) |
                                @Html.ActionLink("削除", "Delete", New With {.id = item.USERID})
                            </td>
                        </tr>
                    Next

                </tbody>

            </table>

        </td>
    </tr>

</table>