@ModelType IEnumerable(Of NTV_SHIFT.M0010)
@Code
    ViewData("Title") = "表示順修正"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim i As Integer = 0
    Dim key As String = ""
    Dim bolUSERSEX As Boolean
End Code

<style>
    table.table-bordered.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-bordered.table-scroll tbody {
        height: 500px;
        width: 358px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .colusernm {
        width: 150px;
    }

    .hyojjn {
        width: 190px;
    }
</style>

<div class="container">

    <hr />

    @Using (Html.BeginForm("EditHYOJJN", "A0110", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()

        @<div class="row">

            @For cnt As Integer = 1 To 2
                @If cnt = 2 Then
                    @<div class="col-md-1 ">
                    </div>
                End If
                @<div class="col-sm-6 col-md-4 " style="padding-top:20px;">
                    <div style="width:339px;">
                        <table class="table table-bordered table-hover table-scroll">
                            <thead>
                                <tr>
                                    <th class="colusernm">
                                        <label>アナウンサー名</label>
                                    </th>
                                    <th class="hyojjn">
                                        @Html.DisplayNameFor(Function(model) model.HYOJJN)
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each item In Model
                                    @If item.USERSEX = bolUSERSEX Then
                                    key = String.Format("list[{0}].", i)
                                        @Html.Hidden(key + "USERID", item.USERID)
                                        @Html.Hidden(key + "USERNM", item.USERNM)
                                        @Html.Hidden(key + "LOGINID", item.LOGINID)
                                        @Html.Hidden(key + "USERPWD", item.USERPWD)
                                        @Html.Hidden(key + "USERPWDCONFRIM", item.USERPWD)
                                        @Html.Hidden(key + "USERSEX", item.USERSEX)
                                        @Html.Hidden(key + "USERPWD", item.USERPWD)
                                        @Html.Hidden(key + "KOKYU1", item.KOKYU1)
                                        @Html.Hidden(key + "KOKYU2", item.KOKYU2)
                                        @Html.Hidden(key + "KOKYU3", item.KOKYU3)
                                        @Html.Hidden(key + "KOKYU4", item.KOKYU4)
                                        @Html.Hidden(key + "KOKYU5", item.KOKYU5)
                                        @Html.Hidden(key + "KOKYU6", item.KOKYU6)

                                        @*Havan[14 Oct 2019] : Added for Legal Holidays eg. HOKYU1,2,3,4,5,6,7 *@
                                        @Html.Hidden(key + "KOKYU1", item.HOKYU1)
                                        @Html.Hidden(key + "KOKYU2", item.HOKYU2)
                                        @Html.Hidden(key + "KOKYU3", item.HOKYU3)
                                        @Html.Hidden(key + "KOKYU4", item.HOKYU4)
                                        @Html.Hidden(key + "KOKYU5", item.HOKYU5)
                                        @Html.Hidden(key + "KOKYU6", item.HOKYU6)
                                        @Html.Hidden(key + "KOKYU6", item.HOKYU7)

                                        @Html.Hidden(key + "HYOJ", item.HYOJ)
                                        @Html.Hidden(key + "STATUS", item.STATUS)
                                        @Html.Hidden(key + "ACCESSLVLCD", item.ACCESSLVLCD)
                                        @Html.Hidden(key + "MAILLADDESS", item.MAILLADDESS)
                                        @Html.Hidden(key + "KEITAIADDESS", item.KEITAIADDESS)
                                        @Html.Hidden(key + "KOKYUTENKAI", item.KOKYUTENKAI)

                                        @<tr>
                                            <td class="colusernm">
                                                @Html.DisplayFor(Function(modelItem) item.USERNM)
                                            </td>
                                            <td class="hyojjn">
                                                @Html.TextBox(key + "HYOJJN", item.HYOJJN, htmlAttributes:=New With {.style = "width:100px;"})
                                                <div>@Html.ValidationMessage(key + "HYOJJN", New With {.class = "text-danger"})</div>
                                            </td>
                                        </tr>

                                    i = i + 1
                                    End If
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>

            bolUSERSEX = True

            Next

        </div>

        @<div class="form-group">
            <div class="col-md-offset-1 col-md-11">
                <input id="btnMasterUpd" type="submit" value="更新" class="btn btn-default" />
            </div>
        </div>

    End Using

    <div>
        @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnEditModoru"})

    </div>


</div>

<script>

    //修正モードで画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
    $('.table-scroll').on('change', 'input', function () {

        $("#myForm").data("MSG", true);

    });

</script>