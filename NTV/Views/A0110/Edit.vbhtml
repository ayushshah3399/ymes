@ModelType NTV_SHIFT.M0010
@Code
    ViewData("Title") = "ユーザー設定"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .modal-sm-cat{width:400px}    
</style>

<div class="container">
    <div class="row">
        <div class="col-md-7">

            <h3>修正</h3>

            @Using (Html.BeginForm("Edit", "A0110", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
                @Html.AntiForgeryToken()

                @<div class="form-horizontal">
                   
                    <hr />
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                     @Html.HiddenFor(Function(model) model.USERID)
            
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.USERNM, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.USERNM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @Html.ValidationMessageFor(Function(model) model.USERNM, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.LOGINID, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.LOGINID, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @Html.ValidationMessageFor(Function(model) model.LOGINID, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.USERPWD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.USERPWD, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @*@Html.PasswordFor(Function(model) model.USERPWD, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                            @Html.ValidationMessageFor(Function(model) model.USERPWD, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.USERPWDCONFRIM, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                         <div class="col-md-9">
                             @Html.EditorFor(Function(model) model.USERPWDCONFRIM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                             @*@Html.PasswordFor(Function(model) model.USERPWD, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                             @Html.ValidationMessageFor(Function(model) model.USERPWDCONFRIM, "", New With {.class = "text-danger"})
                         </div>
                     </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.USERSEX, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            <label class="radio-inline">
                                @Html.RadioButtonFor(Function(model) model.USERSEX, "False")
                                男
                            </label>
                            <label class="radio-inline">
                                @Html.RadioButtonFor(Function(model) model.USERSEX, "True")
                                女
                            </label>
                            @Html.ValidationMessageFor(Function(model) model.USERSEX, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                     <div class="form-group">
                        @Html.LabelFor(Function(model) model.KOKYU1, "公休", htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            <lable class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.KOKYU1)月
                            </lable>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.KOKYU2)火
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.KOKYU3)水
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.KOKYU4)木
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.KOKYU5)金
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.KOKYU6)土
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.KOKYU7)日
                            </label>
                            <div>
                                @Html.ValidationMessageFor(Function(model) model.KOKYU1, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.KOKYU2, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.KOKYU3, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.KOKYU4, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.KOKYU5, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.KOKYU6, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.KOKYU7, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>

                      <!-- Havan[14 Oct 2019] : Added code for leagal Holidays -->
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.HOKYU1, "法休", htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">

                            <lable class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.HOKYU1)月
                            </lable>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.HOKYU2)火
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.HOKYU3)水
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.HOKYU4)木
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.HOKYU5)金
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.HOKYU6)土
                            </label>
                            <label class="checkbox-inline">
                                @Html.EditorFor(Function(model) model.HOKYU7)日
                            </label>
                            <div>
                                @Html.ValidationMessageFor(Function(model) model.HOKYU1, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.HOKYU2, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.HOKYU3, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.HOKYU4, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.HOKYU5, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.HOKYU6, "", New With {.class = "text-danger"})
                                @Html.ValidationMessageFor(Function(model) model.HOKYU7, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                      
                    </div>
                  
                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.HYOJJN, htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                @Html.EditorFor(Function(model) model.HYOJJN, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                                @Html.ValidationMessageFor(Function(model) model.HYOJJN, "", New With {.class = "text-danger"})
                            </div>
                        </div>

                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.HYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                <div class="checkbox">
                                    <label>@Html.EditorFor(Function(model) model.HYOJ)</label>
                                    @Html.ValidationMessageFor(Function(model) model.HYOJ, "", New With {.class = "text-danger"})
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.ACCESSLVLCD, "レベル", htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                @Html.DropDownList("ACCESSLVLCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                                @Html.ValidationMessageFor(Function(model) model.ACCESSLVLCD, "", New With {.class = "text-danger"})
                            </div>
                        </div>

                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.MAILLADDESS, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                            <div class="col-md-9">
                                @Html.EditorFor(Function(model) model.MAILLADDESS, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                                @Html.ValidationMessageFor(Function(model) model.MAILLADDESS, "", New With {.class = "text-danger"})
                            </div>
                        </div>

                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.KEITAIADDESS, htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                @Html.EditorFor(Function(model) model.KEITAIADDESS, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                                @Html.ValidationMessageFor(Function(model) model.KEITAIADDESS, "", New With {.class = "text-danger"})
                            </div>
                        </div>

                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.KOKYUTENKAI, htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                <div class="checkbox">
                                    <label>@Html.EditorFor(Function(model) model.KOKYUTENKAI)</label>
                                    @Html.ValidationMessageFor(Function(model) model.KOKYUTENKAI, "", New With {.class = "text-danger"})
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.KOKYUTENKAIALL, htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                <div class="checkbox">
                                    <label>@Html.EditorFor(Function(model) model.KOKYUTENKAIALL)</label>
                                    @Html.HiddenFor(Function(model) model.KOKYUTENKAIALL)
                                    @Html.ValidationMessageFor(Function(model) model.KOKYUTENKAIALL, "", New With {.class = "text-danger"})
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            @Html.LabelFor(Function(model) model.KARIANA, htmlAttributes:=New With {.class = "control-label col-md-3"})
                            <div class="col-md-9">
                                <div class="checkbox">
                                    <label>@Html.EditorFor(Function(model) model.KARIANA)</label>
                                    @Html.ValidationMessageFor(Function(model) model.KARIANA, "", New With {.class = "text-danger"})
                                </div>
                            </div>
                        </div>

                         <!--ASI[25 Nov 2019]: START-->
                        <div class="form-group">
                             <label class = "control-label col-md-3">スポーツカテゴリ</label>
                             <div class="col-md-6">
                                 <!--<textarea id="SportCatCdComaSeperatedString" name="SportCatCdComaSeperatedString" cols="50" hidden =true ></textarea>
                                 <textarea id="SportCatNmComaSeperatedString" name="SportCatNmComaSeperatedString" cols="50" readonly style="border:none;padding-top:10px;" ></textarea>--> 
                                 @Html.Hidden("ChiefFlgsComaSeperatedString", Model.ChiefFlgsComaSeperatedString, New With {.class = "form-control"})  
                                 @Html.Hidden("SportCatCdComaSeperatedString", Model.SportCatCdComaSeperatedString,  New With {.class = "form-control" })  
                                 @Html.TextAreaFor( Function(model) model.SportCatNmComaSeperatedString,  New With { .cols="50",.readonly="true", .style="border:none;padding-top:10px;"})
                             </div>
                             <div class="col-md-3" >
                                 <button type="button" class="btn btn-info col-md-offset-0" data-toggle="modal" data-target="#sportCategoryDialog" data-kbn="1">スポーツ班設定</button>
                             </div>                         
                         </div>
                        <!--ASI[25 Nov 2019]: END-->

                        <div class="form-group">
                            <div class="col-md-offset-3 col-md-10">
                                <input id="btnMasterUpd" type="submit" value="更新" class="btn btn-default" />
                            </div>
                        </div>
                    </div>
                                
            End Using

            <div>
                @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnEditModoru"})
            </div>

        </div>

        <div class="col-md-5">
            <p>
                <ul class="nav nav-pills">
                    <li>@Html.ActionLink("表示順設定", "EditHYOJJN")</li>
                </ul>
            </p>

            @Html.Partial("_UserListParital", ViewData.Item("List"))
        </div>
    </div>

</div>

<div class="modal fade" id="sportCategoryDialog" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-sm-cat" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">アナウンサー選択</h4>
                <input type="text" class="hidden" id="kbn" />
            </div>
            <div class="modal-body">

            </div>
            <div class="modal-footer">
                <button id="btnselectcategory" type="button" class="btn btn-default" data-dismiss="modal" style="float: left;" onclick="btnselectcategoryClick()">選択</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal">閉じる</button>
            </div>
        </div>
    </div>
</div>

<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>

<script>

    $(document).ready(function () {
        var val = $("#ACCESSLVLCD").val();

        if (val == 1 || val == 2 || val == 3) {
            $('#KOKYUTENKAI').prop('disabled', false);
            $('#KOKYUTENKAIALL').prop('disabled', false);
        }
        else {
            $('#KOKYUTENKAI').prop('disabled', true);
            $('#KOKYUTENKAI').prop('checked', false);;
            $('#KOKYUTENKAI[type=hidden]').val(false);

            $('#KOKYUTENKAIALL').prop('disabled', true);
            $('#KOKYUTENKAIALL').prop('checked', false);;
            $('#KOKYUTENKAIALL[type=hidden]').val(false);
        }
    });

    $("#ACCESSLVLCD").change(function () {
        var val = this.value
        if (val == 1 || val == 2 || val == 3) {
            $('#KOKYUTENKAI').prop('disabled', false);
            $('#KOKYUTENKAIALL').prop('disabled', false);
        }
        else {
            $('#KOKYUTENKAI').prop('disabled', true);
            $('#KOKYUTENKAI').prop('checked', false);;
            $('#KOKYUTENKAI[type=hidden]').val(false);

            $('#KOKYUTENKAIALL').prop('disabled', true);
            $('#KOKYUTENKAIALL').prop('checked', false);;
            $('#KOKYUTENKAIALL[type=hidden]').val(false);
        }
    });


    //修正モードで画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
    $("#myForm :input").change(function () {

        $("#myForm").data("MSG", true);

    });

    /**ASI[22 Nov 2019]: Sport-category dialog **/
    //スポーツカテゴリ 選択ダイアログ
    $('#sportCategoryDialog').on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.modal-body').load(myApp.Urls.baseUrl + 'A0110/SearchSportCategory');
        modal.find('.modal-title').text('スポーツカテゴリ');
    });

    /**ASI[25 Nov 2019]: Sport-category dialog **/
    $('#sportCategoryDialog').on('shown.bs.modal', function (e) {
        var sportCatCdTxtArea = $('#SportCatCdComaSeperatedString').val();
        if (sportCatCdTxtArea != "") {
            var catCdArr = sportCatCdTxtArea.split(',');
            var ChiefFlgsComaSeperatedString = $('#ChiefFlgsComaSeperatedString').val();
            var chiefFlgArr = ChiefFlgsComaSeperatedString.split(',');
            for (i = 0; i < catCdArr.length; i++) {
                var id = catCdArr[i];
                id = "_" + id;
                $("#tblSportCat tr td input[id^='CHK1'][id$='" + id + "']")[0].checked = true;
                var chiefFlgValue = chiefFlgArr[i];
                if (chiefFlgValue == "1")
                    $("#tblSportCat tr td input[id^='CHK2'][id$='" + id + "']")[0].checked = true;
                var usertype = '@Session("LoginUserACCESSLVLCD")'
                if (usertype != "3") {
                    $("#tblSportCat tr td input[id^='CHK2'][id$='" + id + "']").removeAttr("disabled");
                } else {
                    if (chiefFlgValue == "1") {
                        $("#tblSportCat tr td input[id^='CHK1'][id$='" + id + "']").attr("disabled", true);
                    }
                }
            }
        }
    });
    /**ASI[25 Nov 2019]: Sport-category dialog **/
    function btnselectcategoryClick() {
        var CheckedCatCheckBoxes = $("#tblSportCat tr td input[id^='CHK1_']:checked");
        var allSportCatCds = '';
        var allChiefFlgs = '';
        var sportCatNAME = '';
        CheckedCatCheckBoxes.each(function () {
            var id = $(this)[0].id.substr(5);
            id = "_" + id;
            var chiefcount = $("#tblSportCat tr td input[id^='CHK2'][id$='" + id + "']:checked").length;
            if (chiefcount > 0) {
                allSportCatCds = allSportCatCds + $(this)[0].getAttribute('code') + ',';
                allChiefFlgs = allChiefFlgs + "1" + ',';
                sportCatNAME = sportCatNAME + $(this)[0].getAttribute('value') + "(チーフ)" + ', ';
            } else {
                allSportCatCds = allSportCatCds + $(this)[0].getAttribute('code') + ',';
                allChiefFlgs = allChiefFlgs + "0" + ',';
                sportCatNAME = sportCatNAME + $(this)[0].getAttribute('value') + ', ';
            }
        });
        allSportCatCds = allSportCatCds.substr(0, allSportCatCds.length - 1);
        sportCatNAME = sportCatNAME.substr(0, sportCatNAME.length - 2);

        //$('#sportCategory_val').val(sportCatNAME);
        $('#SportCatCdComaSeperatedString').val(allSportCatCds);
        $('#SportCatNmComaSeperatedString').val(sportCatNAME);
        $('#ChiefFlgsComaSeperatedString').val(allChiefFlgs);
    };

    function CatcdCheckboxCliked(cb) {
        var checkbox2id = cb.id.replace('CHK1', 'CHK2');
        var usertype = '@Session("LoginUserACCESSLVLCD")'
        if (usertype != "3") {
            if (cb.checked) {
                $("#tblSportCat tr td input[id$='" + checkbox2id + "']").removeAttr("disabled");
            } else {
                $("#tblSportCat tr td input[id$='" + checkbox2id + "']").attr("disabled", true);
            }
        }
        if (!cb.checked) {
            $("#tblSportCat tr td input[id$='" + checkbox2id + "']")[0].checked = false;
        }
    }

</script>