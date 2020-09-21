@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "スポーツシフト登録(仮登録)"
    Dim listFreeItems = DirectCast(ViewBag.FreeItemList, List(Of String))
    Dim listAnaItems = DirectCast(ViewBag.AnaItemList, List(Of String))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))

    Dim strKey As String = ""
    Dim conterI As Integer = 0
    Dim conterJ As Integer = 0
End Code

<style>
    .comboField {
        position: relative;
    }

    .inputBox {
        font-size: 14px;
        width: 195px;
        position: absolute;
    }

    .selectBox {
        font-size: 14px;
        width: 220px;
    }
</style>

<div class="col-md-12">
    <div class="row" style="padding-top: 10px">
        <div class="col-md-10" style="height:58px" >
            <h3 style="line-height: 0.2;">新規</h3>
            <hr />
        </div>
        <ul class="nav nav-pills navbar-right" style="padding-right: 15px">
            <li>@Html.ActionLink("戻る", "Index", "A0220")</li>
        </ul>
    </div>
</div>


<div class="row">
    <div class="col-md-12">
        @Using (Html.BeginForm("Create", "A0220", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
            @Html.AntiForgeryToken()
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            @Html.Partial("_SuccessMsg")
            @<div class="col-md-7">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("業務期間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                            <div class="col-md-9">
                                @Html.TextBox("GYOMYMD", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                                ～
                                @Html.TextBox("GYOMYMDED", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                            </div>
                            <div class=" col-md-offset-3 col-md-9">
                                @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("拘束時間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                            <div class="col-md-9">
                                @Html.EditorFor(Function(model) model.KSKJKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                                ～
                                @Html.EditorFor(Function(model) model.KSKJKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                            </div>
                            <div class=" col-md-offset-3 col-md-9">
                                @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("スポーツカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                            <div class="col-md-9">
                                @Html.DropDownList("SPORTCATCD", New SelectList(ViewBag.SportCatNmList, "SPORTCATCD", "SPORTCATNM", Model.SPORTCATCD), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:220px;"})
                                @Html.ValidationMessageFor(Function(model) model.SPORTCATCD, "", New With {.class = "text-danger"})
                                @Html.HiddenFor(Function(model) model.SPORTCATCD, "")
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("スポーツサブカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                            <div class="col-md-9">
                                @Html.DropDownList("SPORTSUBCATCD", New SelectList(ViewBag.SportSubCatNmList, "SPORTSUBCATCD", "SPORTSUBCATNM", Model.SPORTSUBCATCD), htmlAttributes:=New With {.class = "form-control input-sm", .id = "idSPORTSUBCATCD", .style = "width:220px;"})
                                @Html.ValidationMessageFor(Function(model) model.SPORTSUBCATCD, "", New With {.class = "text-danger"})
                                @Html.HiddenFor(Function(model) model.SPORTSUBCATCD, "")
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.CATCD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                            @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group ">
                        @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9 comboField">
                            @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm", .style = "width:220px;"}})
                            @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("OA時間", htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                @Html.EditorFor(Function(model) model.OAJKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                                ～
                                @Html.EditorFor(Function(model) model.OAJKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                            </div>
                            <div class=" col-md-offset-3 col-md-9">
                                @Html.ValidationMessageFor(Function(model) model.OAJKNST, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.OAJKNED, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("試合時間", htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                @Html.EditorFor(Function(model) model.SAIJKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                                ～
                                @Html.EditorFor(Function(model) model.SAIJKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                            </div>
                            <div class=" col-md-offset-3 col-md-9">
                                @Html.ValidationMessageFor(Function(model) model.SAIJKNST, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.SAIJKNED, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>

                    <div class="form-group ">
                        @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9 comboField">
                            @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox"}})
                            <select class="form-control input-sm selectBox" id="selectBoxNaiyo">
                                @If NaiyouList IsNot Nothing Then
                                    @For Each item In NaiyouList
                                        @<option>@item.NAIYO</option>
Next
                End If
                            </select>
                            @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
                        </div>
                    </div>



                    <div class="form-group">
                        @Html.Label("場所", htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.BASYO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @Html.ValidationMessageFor(Function(model) model.BASYO, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.BANGUMITANTO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.BANGUMITANTO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @Html.ValidationMessageFor(Function(model) model.BANGUMITANTO, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.BANGUMIRENRK, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.BANGUMIRENRK, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @Html.ValidationMessageFor(Function(model) model.BANGUMIRENRK, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.Label("備考", htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.BIKO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @Html.ValidationMessageFor(Function(model) model.BIKO, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-9">
                            @Html.HiddenFor(Function(model) model.CATCD, "")
                        </div>
                    </div>

                    @For Each item In listFreeItems
                        strKey = String.Format("FreeTxtBxList[{0}]", conterI)
                        @If item IsNot Nothing Then
                            @<div class="form-group">
                                @Html.Label(item, htmlAttributes:=New With {.class = "control-label col-md-3"})
                                <div class="col-md-9">
                                    @Html.TextBox(strKey, "", htmlAttributes:=New With {.class = "form-control input-sm"})
                                    @Html.ValidationMessage(strKey, New With {.class = "text-danger"})
                                </div>
                            </div>Else
                            @Html.Hidden(strKey, "", htmlAttributes:=New With {.class = "form-control input-sm"})
End If              conterI = conterI + 1
                    Next

                    <div class="form-group" id="divPattern">
                        @Html.LabelFor(Function(model) model.PATTERN, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            <div class="checkbox">
                                <label>@Html.EditorFor(Function(model) model.PATTERN, New With {.htmlAttributes = New With {.id = "chkPattern"}}) 繰り返し登録</label>
                                @Html.ValidationMessageFor(Function(model) model.PATTERN, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>

                    <p></p>
                    <div class="form-group">
                        <div class="form-inline">
                            <div class="col-md-offset-3 col-md-9" style="padding-top :12px">
                                <input type="submit" value="登録" id="btnCreate" class="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            @<div class="col-md-5">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("担当アナ", htmlAttributes:=New With {.class = "control-label col-md-3", .style = "text-align:left;"})
                        </div>
                    </div>

                    @For Each item In listAnaItems
                        strKey = String.Format("D0022[{0}].", conterJ)
                        @If item IsNot Nothing Then
                            @<div class="form-group">
                                <div class="form-inline">
                                    @Html.Label(item, htmlAttributes:=New With {.class = "control-label col-md-3"})
                                    <div class="col-md-9">
                                        @Html.DropDownList(strKey & "USERID", New SelectList(ViewBag.lstM0010, "USERID", "USERNM", ViewBag.lstM0010(1).USERID), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:200px"})
                                        @Html.Hidden(strKey & "COLIDX", conterJ + 1, htmlAttributes:=New With {.class = "form-control input-sm"})
                                    </div>
                                </div>
                            </div>Else
                            @Html.Hidden(strKey & "COLIDX", "0", htmlAttributes:=New With {.class = "form-control input-sm"})
End If              conterJ = conterJ + 1
                    Next
                </div>
            </div>

            @*@<p></p>
                @<div class="row">
                    <div class="col-md-12">
                        <div class="col-md-7">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <div class="col-md-offset-3 col-md-9" style="padding-top :12px">
                                            <input type="submit" value="登録" id="btnCreate" class="btn btn-default" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>*@
        End Using
    </div>
</div>

<script>
    $(document).ready(function () {

        $('#SPORTCATCD').prop('disabled', true)
        $('#idSPORTSUBCATCD').prop('disabled', true)

         if ('@TempData("CompleteMsg")' != "") {
             $('#mySuccessModal').modal('show');
        }

    });

    //内容リストで選択した時
    $("#selectBoxNaiyo").change(function () {
        var val = this.value
        $('#NAIYO').val(val)
    });

    $('#btnCreate').on('click', function (e) {


        var result = confirm("業務登録を行います。よろしいですか？");
        var chkPattern = $("#chkPattern").val();



        if (result == false) {
            return false;
        }
    });

</script>
