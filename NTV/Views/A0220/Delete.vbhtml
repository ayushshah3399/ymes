@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "スポーツシフト登録(仮登録)"
    Dim listFreeItems = DirectCast(ViewBag.FreeItemList, List(Of String))
    Dim listAnaItems As List(Of String()) = DirectCast(ViewBag.AnaItemList, List(Of String()))
    Dim listitemvalue = DirectCast(ViewBag.listitemvalue, List(Of String))
    Dim strKey As String = ""
    Dim conterI As Integer = 0
    Dim conterJ As Integer = 0
    Dim ListItemsName As Dictionary(Of Integer, String) = ViewBag.lstUSERID
    Dim lastForm As String = ViewBag.lastForm
End Code
<div class="row">
    <div class="col-md-2  col-md-push-11" style="padding-top:10px;">
        <ul class="nav nav-pills ">
            @If Session("A0220DeleteRtnUrl") IsNot Nothing Then
                @<li><a href="@Session("A0220DeleteRtnUrl").ToString">戻る</a></li>
End If
        </ul>
    </div>

    <div class="col-md-10 col-md-pull-2">
        <h3>削除</h3>
    </div>
</div>

<h3>削除してもよろしいですか？</h3>
<div>
    @*<h4>業務登録</h4>*@
    <hr />
    @Using (Html.BeginForm("Delete", "A0220", routeValues:=Model, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.Hidden("lastForm", lastForm, New With {.id = "lastForm"})

        @<div>
            <div Class="col-md-7">
                <div Class="form-horizontal">
                    <dl Class="dl-horizontal">

                        @Html.HiddenFor(Function(model) model.GYOMNO, htmlAttributes:=New With {.id = "GYOMNO"})
                        @Html.HiddenFor(Function(model) model.GYOMYMD, htmlAttributes:=New With {.id = "GYOMYMD"})
                        @Html.HiddenFor(Function(model) model.SPORTCATCD, htmlAttributes:=New With {.id = "SPORTCATCD"})
                        <dt>
                            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.GYOMYMD)
                            ～
                            @Html.DisplayFor(Function(model) model.GYOMYMDED)
                        </dd>

                        @*<dt>
                                @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
                            </dt>*@

                        @*<dd>
                                @Html.DisplayFor(Function(model) model.GYOMYMDED)
                            </dd>*@


                        <dt>
                            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(0, 2)
                            @Html.Encode(":")
                            @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(2, 2)

                            @Html.Encode("～")

                            @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(0, 2)
                            @Html.Encode(":")
                            @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(2, 2)
                        </dd>



                        <dt>
                            @Html.DisplayNameFor(Function(model) model.SPORTCATCD)
                        </dt>

                        <dd>
                            @Html.Label(ViewBag.SPORTCATNM.ToString, htmlAttributes:=New With {.style = "font-weight: normal;"})
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.SPORTSUBCATCD)
                        </dt>

                        <dd>
                            @Html.Label(ViewBag.SPORTSUBCATNM.ToString, htmlAttributes:=New With {.style = "font-weight: normal;"})
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.M0020.CATNM)
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.BANGUMINM)
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.OAJKNST)
                        </dt>

                        <dd>
                            @If Model.OAJKNST IsNot Nothing Then
                                @Html.DisplayFor(Function(model) model.OAJKNST).ToString.Substring(0, 2)
                                @Html.Encode(":")
                                @Html.DisplayFor(Function(model) model.OAJKNST).ToString.Substring(2, 2)
End If

                            @If Model.OAJKNED IsNot Nothing Then
                                @Html.Encode("～")

                                @Html.DisplayFor(Function(model) model.OAJKNED).ToString.Substring(0, 2)
                                @Html.Encode(":")
                                @Html.DisplayFor(Function(model) model.OAJKNED).ToString.Substring(2, 2)
End If
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.SAIJKNST)
                        </dt>

                        <dd>
                            @If Model.SAIJKNST IsNot Nothing Then
                                @Html.DisplayFor(Function(model) model.SAIJKNST).ToString.Substring(0, 2)
                                @Html.Encode(":")
                                @Html.DisplayFor(Function(model) model.SAIJKNST).ToString.Substring(2, 2)
End If

                            @If Model.SAIJKNST IsNot Nothing Then
                                @Html.Encode("～")

                                @Html.DisplayFor(Function(model) model.SAIJKNST).ToString.Substring(0, 2)
                                @Html.Encode(":")
                                @Html.DisplayFor(Function(model) model.SAIJKNST).ToString.Substring(2, 2)
End If
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.NAIYO)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.NAIYO)
                        </dd>



                        <dt>
                            @Html.DisplayNameFor(Function(model) model.BASYO)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.BASYO)
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.BANGUMITANTO)
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.BANGUMIRENRK)
                        </dd>

                        <dt>
                            @Html.DisplayNameFor(Function(model) model.BIKO)
                        </dt>

                        <dd>
                            @Html.DisplayFor(Function(model) model.BIKO)
                        </dd>

                        @For Each item In listFreeItems
                            @If item IsNot Nothing AndAlso listitemvalue(conterJ) IsNot Nothing Then


                                @<dt>
                                    @Html.Label(item, htmlAttributes:=New With {.class = "control-label", .style = "text-align:right;"})
                                </dt>

                                @<dd>
                                    @Html.Label(listitemvalue(conterJ), htmlAttributes:=New With {.class = "control-label", .style = "font-weight: normal;"})
                                </dd>
End If
                            conterJ = conterJ + 1
                        Next


                    </dl>

                    <div class="form-actions no-color" style="width:400px">
                        <input id="btnDelete" name="btnDelete" type="submit" value="削除" class="btn btn-primary" /> |

                        @Html.ActionLink("戻る", "Edit", "A0220", routeValues:=New With {.id = Model.GYOMNO, .lastForm = lastForm}, htmlAttributes:=New With {.style = "text-decoration:blink;"})
                    </div>

                </div>

            </div>

            <div Class="col-md-5">
                <div Class="form-horizontal">
                    @Html.Label("担当アナ", htmlAttributes:=New With {.class = "control-label"})

                    <dl Class="dl-horizontal">
                        @For Each item In listAnaItems
                            @If item IsNot Nothing Then


                                @<dt>
                                    @Html.Label(item(0), htmlAttributes:=New With {.class = "control-label", .style = "text-align:right;"})
                                </dt>

                                @<dd>
                                    @Html.Label(ListItemsName.Item(Convert.ToInt64(item(1))), htmlAttributes:=New With {.Class = "control-label", .style = "font-weight: normal;"})
                                </dd>
End If
                                    conterI = conterI + 1
                                Next
                    </dl>
                </div>
            </div>

        </div>

    End Using

</div>

<script>
    $('#btnDelete').click(function (e) {

        var result = confirm("削除します。よろしいですか？");
        //var gymno = $('#GYOMNO').val();
        //$('#btnDelete').val(gymno);
        if (result == false) {
            return false;
        }
    });
</script>

