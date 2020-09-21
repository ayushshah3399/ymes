@ModelType NTV_SHIFT.M0090
@Code
    ViewData("Title") = "業務一括登録"
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
    Dim KarianacatList = DirectCast(ViewBag.KarianacatList, List(Of M0080))
    Dim Analist = DirectCast(ViewBag.lstM0010, List(Of M0010))
    Dim strStyle As String = ""
    Dim strKey As String = ""
    Dim strID As String = ""
   
End Code

<style>
    #comboField {
        position: relative;
    }

    #BANGUMINM {
        font-size: 14px;
        width: 200px;
        position: absolute;
    }

    #selectBoxBangumi {
        font-size: 14px;
        width: 225px;
    }


    #NAIYO {
        font-size: 14px;
        width: 200px;
        position: absolute;
    }

    #selectBoxNaiyo {
        font-size: 14px;
        width: 225px;
    }

        .inputBoxAna {
        font-size: 14px;
        width: 120px;
        position: absolute;
    }

    .selectBoxAna {
        font-size: 14px;
        width: 145px;
        height: 33px;
    }
</style>

@Using (Html.BeginForm("Edit", "A0170", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
    @Html.AntiForgeryToken()

    @<div class="row">
    <div class="col-md-6">
        <div class="row">
            <div class="col-md-3">
                <h3 style="margin-top:15px;">修正</h3>
            </div>
            <div class="col-md-9">
                <div style="margin-top:15px;">

                    <input type="button" value="雛形保存" id="btnCreateHina" class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModalShitaHina" data-fmtkbn="2" />
                   
                    &nbsp | &nbsp
                    @*ASI[22 Oct 2019]:Added WeekA and WeekB param in action call*@
                    @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
                     .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
                     .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
                     .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .Oajknst = Session("Oajknst"), .Oajkned = Session("Oajkned"),
                     .WeekA = Session("WeekA"), .WeekB = Session("WeekB"),
                     .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Renraku = Session("Renraku"), .TaishoNo = Session("TaishoNo"), .TaishoYes = Session("TaishoYes")}, htmlAttributes:=New With {.id = "btnEditModoru"})

                </div>
            </div>
        </div>
    </div>
</div>
    
    @<div class="row">
         <div class="col-sm-9">
             <div class="form-horizontal">
                 <hr style="margin-top:-10px;" />
                 @Html.HiddenFor(Function(model) model.FMTKBN)
                 @Html.HiddenFor(Function(model) model.ANAIDLIST)
                 @Html.HiddenFor(Function(model) model.KARIANALIST)
                 @Html.HiddenFor(Function(model) model.HINAMEMO)
                 @Html.HiddenFor(Function(model) model.DATAKBN)



                 @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                 @*<div class="form-group">
                    @Html.LabelFor(Function(model) model.IKKATUNO, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.IKKATUNO, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.IKKATUNO, "", New With {.class = "text-danger"})
                    </div>
                </div>*@
                 @Html.HiddenFor(Function(model) model.IKKATUNO)
                 <div class="form-group">
                     @Html.LabelFor(Function(model) model.IKKATUMEMO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                     <div class="col-md-9">
                         @Html.EditorFor(Function(model) model.IKKATUMEMO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                         @Html.ValidationMessageFor(Function(model) model.IKKATUMEMO, "", New With {.class = "text-danger"})
                     </div>
                 </div>
                 <div class="form-group">
                     <div class="form-inline">
                         @Html.Label("業務期間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                         <div class="col-md-9">
                             @Html.TextBox("GYOMYMD", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                             ～
                             @Html.TextBox("GYOMYMDED", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                         </div>
                     </div>
                     <div class=" col-md-offset-3 col-md-9">
                         @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})
                         @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})
                     </div>
                 </div>


                 <div class="form-group">
                     <div class="form-inline">
                         @Html.LabelFor(Function(model) model.PTN1, "パターン設定", htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">

                             <label class="checkbox-inline">
                                 @Html.EditorFor(Function(model) model.PTNFLG) 繰り返し登録
                             </label>

                             @If Model.PTNFLG = True Then
                                     strStyle = "visibility:visible"
                                 Else
                                     strStyle = "visibility:hidden"
                                 End If

                             <label class="checkbox-inline" id="pin1" style=@strStyle>

                                 @*@Html.LabelFor(Function(model) model.PTN1, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                                 @Html.EditorFor(Function(model) model.PTN1)  月曜
                                 @*@Html.ValidationMessageFor(Function(model) model.PTN1, "", New With {.class = "text-danger"})*@

                             </label>

                             <label class="checkbox-inline" id="pin2" style=@strStyle>

                                 @*@Html.LabelFor(Function(model) model.PTN2, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                                 @Html.EditorFor(Function(model) model.PTN2) 火曜
                                 @Html.ValidationMessageFor(Function(model) model.PTN2, "", New With {.class = "text-danger"})
                             </label>

                             <label class="checkbox-inline" id="pin3" style=@strStyle>

                                 @Html.EditorFor(Function(model) model.PTN3) 水曜
                                 @Html.ValidationMessageFor(Function(model) model.PTN3, "", New With {.class = "text-danger"})

                             </label>

                             <label class="checkbox-inline" id="pin4" style=@strStyle>

                                 @Html.EditorFor(Function(model) model.PTN4) 木曜
                                 @Html.ValidationMessageFor(Function(model) model.PTN4, "", New With {.class = "text-danger"})
                             </label>

                             <label class="checkbox-inline" id="pin5" style=@strStyle>

                                 @Html.EditorFor(Function(model) model.PTN5) 金曜
                                 @Html.ValidationMessageFor(Function(model) model.PTN5, "", New With {.class = "text-danger"})
                             </label>

                             <label class="checkbox-inline" id="pin6" style=@strStyle>

                                 @Html.EditorFor(Function(model) model.PTN6) 土曜
                                 @Html.ValidationMessageFor(Function(model) model.PTN6, "", New With {.class = "text-danger"})
                             </label>

                             <label class="checkbox-inline" id="pin7" style=@strStyle>

                                 @Html.EditorFor(Function(model) model.PTN7) 日曜
                                 @Html.ValidationMessageFor(Function(model) model.PTN7, "", New With {.class = "text-danger"})
                             </label>

                            

                         </div>
                     </div>
                 </div>
                 <div class="form-group">
                     <div class="col-md-offset-3 col-md-9" id="weekAB">
                          @*ASI[21 Oct 2019]: Added below 2 checkBox A週 & B週.*@
                             <label class="checkbox-inline" id="weekA" >
                                 @Html.EditorFor(Function(model) model.WEEKA) A週
                                 @Html.ValidationMessageFor(Function(model) model.WEEKA, "", New With {.class = "text-danger"})
                             </label>

                             <label class="checkbox-inline" id="weekB" >
                                 @Html.EditorFor(Function(model) model.WEEKB) B週
                                 @Html.ValidationMessageFor(Function(model) model.WEEKB, "", New With {.class = "text-danger"})
                             </label>
                     </div>
                 </div>

                 <div class="form-group">
                     <div class="col-md-offset-3 col-md-9">
                         @Html.ValidationMessageFor(Function(model) model.PTN1, "", New With {.class = "text-danger"})
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
                     </div>
                     <div class=" col-md-offset-3 col-md-9">
                         @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With {.class = "text-danger"})
                         @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With {.class = "text-danger"})
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
                         @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox"}})

                         <select class="form-control input-sm selectBox" id="selectBoxBangumi">
                             @If Bangumilst IsNot Nothing Then
                                 @For Each item In Bangumilst
                                     @<option>@item.BANGUMINM</option>
                                 Next
                             End If
                         </select>
                         @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
                     </div>
                 </div>

                 <div class="form-group">
                     <div class="form-inline">
                         @Html.LabelFor(Function(model) model.OAJKNST, htmlAttributes:=New With {.class = "control-label col-md-3"})
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
                     @Html.LabelFor(Function(model) model.BASYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                     <div class="col-md-9">
                         @Html.EditorFor(Function(model) model.BASYO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                         @Html.ValidationMessageFor(Function(model) model.BASYO, "", New With {.class = "text-danger"})
                     </div>
                 </div>

                 @*<div class="form-group">
                    @Html.LabelFor(Function(model) model.USERID, "アナウンサー", htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9">
                        @Html.DropDownList("USERID", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                        @Html.ValidationMessageFor(Function(model) model.USERID, "", New With {.class = "text-danger"})
                    </div>
                </div>*@

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
                     @Html.LabelFor(Function(model) model.BIKO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                     <div class="col-md-9">
                         @Html.EditorFor(Function(model) model.BIKO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                         @Html.ValidationMessageFor(Function(model) model.BIKO, "", New With {.class = "text-danger"})
                     </div>
                 </div>

                 <div class="form-group">
                     @Html.LabelFor(Function(model) model.IKTTAISHO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                     <div class="col-md-9">
                         <label class="radio-inline">
                             @Html.RadioButtonFor(Function(model) model.IKTTAISHO, "False", New With {.checked = "true"})
                             対象外
                         </label>
                         <label class="radio-inline">
                             @Html.RadioButtonFor(Function(model) model.IKTTAISHO, "True")
                             対象
                         </label>
                         @Html.ValidationMessageFor(Function(model) model.IKTTAISHO, "", New With {.class = "text-danger"})
                     </div>
                 </div>

                 <div id="divSuccess" class="text-success" style="visibility:hidden">
                     @Html.Hidden("success", TempData("success"))
                 </div>

                 <div class="form-group">
                     <div class="form-inline">
                         <div class="col-md-offset-3 col-md-9">
                             <input id="update" type="submit" value="更新" class="btn btn-default" />
                             &nbsp | &nbsp
                             @*<input type="submit" value="業務登録画面" class="btn btn-default" />*@
                             @Html.ActionLink("業務登録", "Create", "B0020", routeValues:=New With {.ikkatuno = Model.IKKATUNO}, htmlAttributes:=New With {.class = "btn  btn-primary btngyomu"})



                         </div>
                     </div>
                 </div>




                 @*<div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="登録" class="btn btn-default" />
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="業務登録画面" class="btn btn-default" />
                    </div>
                </div>*@
             </div>
         </div>


         <div class="col-md-2 comboField">

             <div class="row">

                 <select id="Analist" style="visibility:hidden;">
                     @If Analist IsNot Nothing Then
                         @For Each item In Analist
                             @<option value=@item.USERID>@Html.Encode(item.USERNM)</option>
                         Next
                         End If
                 </select>
                 <table class="table table-bordered" id="myTable">
                     <tr>
                         <th bgcolor="MediumAquaMarine">
                             アナウンサー
                         </th>
                         <th>
                             <a href="#" id="add_btn_ana" class="btn btn-success btn-xs ">追加</a>
                         </th>
                     </tr>

                     @If Model IsNot Nothing AndAlso Model.M0110 IsNot Nothing AndAlso Model.M0110.Count > 0 Then
                         @For i = 0 To Model.M0110.Count - 1
                                 strKey = String.Format("M0110[{0}].", i)
                             @<tr>
                                 <td>
                                     @Html.DropDownList(strKey & "USERID", New SelectList(ViewBag.lstM0010, "USERID", "USERNM", Model.M0110(i).USERID), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:200px"})
                                     @Html.Hidden(strKey & "IKKATUNO", Model.M0110(i).IKKATUNO)

                                 </td>
                                 <td>
                                     <a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>
                                 </td>
                             </tr>
                         Next
                                                                                                                                     @*Else
                         @<tr>
                             <td>
                                 @Html.DropDownList("M0110[0].USERID", New SelectList(ViewBag.lstM0010, "USERID", "USERNM", ""), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:200px"})
                                 @Html.Hidden("M0110[0].IKKATUNO", "0")

                             </td>
                             <td>
                                 <a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>
                             </td>
                         </tr>*@
                     End If
                 </table>
                 </div>
           

             <div class="row">
                 <select id="KarianacatList" style="visibility:hidden;">
                     @If KarianacatList IsNot Nothing Then
                         @For Each item In KarianacatList
                             @<option>@Html.Encode(item.ANNACATNM)</option>
                         Next
                         End If
                 </select>

                 <table class="table table-bordered" id="catTable">
                     <tr>
                         <th bgcolor="MediumAquaMarine" style="width:200px">
                             仮アナカテゴリー
                         </th>
                         <th>
                             <a href="#" id="add_btn_kariana" class="btn btn-success btn-xs ">追加</a>
                         </th>
                     </tr>
                     @If Model IsNot Nothing AndAlso Model.M0120 IsNot Nothing AndAlso Model.M0120.Count > 0 Then
                         @For i = 0 To Model.M0120.Count - 1
                                 strKey = String.Format("M0120[{0}].", i)
                                 strID = String.Format("M0120_{0}__", i)
                             @<tr>
                                 <td>
                                     @Html.TextBox(strKey & "ANNACATNM", Model.M0120(i).ANNACATNM, htmlAttributes:=New With {.class = "form-control input-sm inputBoxAna"})
                                     @Html.Hidden(strKey & "SEQ", Model.M0120(i).SEQ)
                                     @*@Html.Hidden(strKey & "IKKATUNO", Model.M0120(i).IKKATUNO)*@
                                     <select class="form-control input-sm selectBoxAna" onchange="AnnacatSelect(this, '@(strId & "ANNACATNM")')">
                                         @If KarianacatList IsNot Nothing Then
                                             @For Each item In KarianacatList
                                                 @<option>@Html.Encode(item.ANNACATNM)</option>
                                             Next
                                                    End If
                                     </select>
                                     @Html.ValidationMessage(strKey & "ANNACATNM", New With {.class = "text-danger"})
                                 </td>
                                 <td>
                                     <a href="#" id="del_btn_kariana" class="btn btn-danger btn-xs">削除</a>
                                 </td>
                             </tr>
                         Next

                         @*Else
                strKey = String.Format("M0120[{0}].", 0)
                strID = String.Format("M0120_{0}__", 0)
                    @<tr>
                        <td>
                            @Html.TextBox(strKey & "ANNACATNM", Nothing, htmlAttributes:=New With {.class = "form-control input-sm inputBoxAna"})
                            @Html.Hidden(strKey & "SEQ", Nothing)
                            @Html.Hidden(strKey & "IKKATUNO", Nothing)
                            <select class="form-control input-sm selectBoxAna" onchange="AnnacatSelect(this, '@(strId & "ANNACATNM")')">
                                @If KarianacatList IsNot Nothing Then
                                    @For Each item In KarianacatList
                                        @<option>@Html.Encode(item.ANNACATNM)</option>
                                    Next
                                                End If
                            </select>

                        </td>
                        <td>
                            <a href="#" id="del_btn_kariana" class="btn btn-danger btn-sm">削除</a>
                        </td>
                    </tr>*@
                     End If
                 </table>
             </div>
             

         </div>

        </div>
        End Using

@Html.Partial("_CreateShitaHinaDialog")

<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>

<script type="text/javascript" src="~/Scripts/A0170-2.js"></script>



<script>

    //仮アナカテゴリーリストで選択した時
    function AnnacatSelect(select, inputid) {
        var input = document.getElementById(inputid);
        var val = select.value;
        input.value = val;
    }

    $("#selectBoxBangumi").change(function () {

        var val = this.value
        $('#BANGUMINM').val(val)

    });

    $("#selectBoxNaiyo").change(function () {

        var val = this.value
        $('#NAIYO').val(val)

    });


    $('#update').on('click', function (e) {

        $('#FMTKBN').val(0);

        var result = confirm("更新します。よろしいですか？")

        if (result == false) {
            return false
        }

        //if ($("#PTNFLG").is(':checked')) {
        //    if ($("#PTN1").is(':checked') || $("#PTN2").is(':checked') || $("#PTN3").is(':checked') || $("#PTN4").is(':checked') || $("#PTN5").is(':checked') || $("#PTN6").is(':checked') || $("#PTN7").is(':checked')) {

        //    }
        //    else {
        //        alert("繰り返し登録ONの場合、 月曜 ～ 日曜のいずれかをチェックする必要があります。")
        //        return false
        //    }
        //}
    });



    $("#myTable").on('click', '#add_btn_ana', function () {


        //var table = document.getElementById("myTable");
        //var rows = table.getElementsByTagName("tr");
        //var index = rows.length - 1;
        //var $table = $('#myTable');
        //var $nrow = $table.find('tr:eq(1)').clone();
        //var $ncell = $nrow.find('td:first');

        //var $select = $ncell.find('select');
        //$select.val(0);
        //$select.attr("name", "M0110[" + index + "].USERID");
        //$select.attr("id", "M0110" + index + "__USERID");

        //var $input = $ncell.find('input');
        //$input.val(0);
        //$input.attr("name", "M0110[" + rows.length + "].IKKATUNO");
        //$input.attr("id", "M0110" + rows.length + "__IKKATUNO");

        //$table.append($nrow);

        var table = document.getElementById("myTable");
        var rows = table.getElementsByTagName("tr");
        var rowcount = rows.length - 1;
        var row = table.insertRow();


        //alert(rowcount)
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);


        var cell1HTML = '<input id ="M0110_' + rowcount + '__IKKATUNO" name ="M0110[' + rowcount + '].IKKATUNO" type="hidden" value="0" ></input>\n' +
                '<select  id ="M0110_' + rowcount + '__USERID" name ="M0110[' + rowcount + '].USERID" class="form-control input-sm">\n';

        //$("#KarianacatList option").each(function () {
        //    cell1HTML = cell1HTML + '<option>' + $(this).text() + '</option>';  
        //});

        $("#Analist").find('option').each(function () {
            //alert($(this).text());
            cell1HTML = cell1HTML + '<option value=' + $(this).val() + '>' + $(this).text() + '</option>\n';
        });

        cell1HTML = cell1HTML + '</select>';
        cell1.innerHTML = cell1HTML;

        cell2.innerHTML = '<a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>';
    });

    $('#add_btn_kariana').click(function (event) {
        var table = document.getElementById("catTable");
        var rows = table.getElementsByTagName("tr");
        var rowcount = rows.length - 1;
        var row = table.insertRow();
               

        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);

        //cell1.innerHTML = '<input id ="M0120_' + rowcount + '__ANNACATNM" name ="M0120[' + rowcount + '].ANNACATNM" class="form-control input-sm" type="text" value="' + name + '" list="Karianacat" ></input>' +
        //                  '<input id ="M0120_' + rowcount + '__SEQ" name ="M0120[' + rowcount + '].SEQ" type="hidden" value="0" ></input>';

        var cell1HTML = '<input id ="M0120_' + rowcount + '__ANNACATNM" name ="M0120[' + rowcount + '].ANNACATNM" class="form-control input-sm inputBoxAna" type="text" value="' + name + '" ></input>\n' +
                        '<input id ="M0120_' + rowcount + '__SEQ" name ="M0120[' + rowcount + '].SEQ" type="hidden" value="0" ></input>\n' +
                    '<select class="form-control input-sm selectBoxAna" onchange="AnnacatSelect(this, \'M0120_' + rowcount + '__ANNACATNM\')">\n';

        //$("#KarianacatList option").each(function () {
        //    cell1HTML = cell1HTML + '<option>' + $(this).text() + '</option>';  
        //});

        $("#KarianacatList").find('option').each(function () {
            //alert($(this).text());
            cell1HTML = cell1HTML + '<option>' + $(this).text() + '</option>\n';
        });

        cell1HTML = cell1HTML + '</select>';
        cell1HTML = cell1HTML + '<span class="field-validation-error text-danger" data-valmsg-replace="true" data-valmsg-for="M0120[' + rowcount + '].ANNACATNM"></span>';
        cell1.innerHTML = cell1HTML;

        cell2.innerHTML = '<a href="#" id="del_btn_kariana" class="btn btn-danger btn-xs">削除</a>';

    });

           
    $("#myTable").on('click', '#del_btn_ana', function () {

        var table = document.getElementById("myTable");
        var rows = table.getElementsByTagName("tr");
                
               
        if (rows.length != 0) {
            $(this).closest('tr').remove();
                    
            var index = 0;
            //行削除後、リストのIndexを振り直す
            for (var i = 1; i < rows.length  ; i += 1) {
                var $ncell = $('#myTable tr:eq(' + i + ') td:first');
                      
                //alert(index)
                var $select = $ncell.find('select');                        
                $select.attr("name", "M0110[" + index + "].USERID")
                $select.attr("id", "M0110_" + index + "__USERID")
                       
                      
                       
                var $input = $ncell.find('input');
                $input.attr("name", "M0110[" + index + "].IKKATUNO")
                $input.attr("id", "M0110_" + index + "__IKKATUNO")

                //var $input1 = $('#myTable tr:eq(' + i + ') td:first').find('input');
                //$input1.attr("name", "M0110[" + index + "].IKKATUNO");
                //$input1.attr("id", "M0110_" + index + "__IKKATUNO");

                    

                index = index + 1;
            }
            //アナウンサーテーブルの行が削除されたら、データ変更されたので、戻るボタンの時メッセージ出す
            $("#myForm").data("MSG", true);
        }

    });

    $("#catTable").on('click', '#del_btn_kariana', function () {

        //alert('reach')
        var table = document.getElementById("catTable");
        var rows = table.getElementsByTagName("tr");
        if (rows.length != 0) {
            $(this).closest('tr').remove();

            //行削除後、リストのIndexを振り直す
            var index = 0;
            for (var i = 1; i < rows.length  ; i += 1) {
                var $input1 = $('#catTable tr:eq(' + i + ') td:first').find('input:first');
                $input1.attr("name", "M0120[" + index + "].ANNACATNM");
                $input1.attr("id", "M0120_" + index + "__ANNACATNM");

                var $input2 = $('#catTable tr:eq(' + i + ') td:first').find('input:last');
                $input2.attr("name", "M0120[" + index + "].SEQ");
                $input2.attr("id", "M0120_" + index + "__SEQ");

                index = index + 1;
            }

            //アナウンサーテーブルの行が削除されたら、データ変更されたので、戻るボタンの時メッセージ出す
            $("#myForm").data("MSG", true);
        }

    });




    function doalert(bol) {
        var chk1 = document.getElementById('pin1');
        var chk2 = document.getElementById('pin2');
        var chk3 = document.getElementById('pin3');
        var chk4 = document.getElementById('pin4');
        var chk5 = document.getElementById('pin5');
        var chk6 = document.getElementById('pin6');
        var chk7 = document.getElementById('pin7');

        if (bol = true) {
            chk1.style.visibility = 'visible';
            chk2.style.visibility = 'visible';
            chk3.style.visibility = 'visible';
            chk4.style.visibility = 'visible';
            chk5.style.visibility = 'visible';
            chk6.style.visibility = 'visible';
            chk7.style.visibility = 'visible';
        } else {

            chk1.style.visibility = 'hidden';
            chk2.style.visibility = 'hidden';
            chk3.style.visibility = 'hidden';
            chk4.style.visibility = 'hidden';
            chk5.style.visibility = 'hidden';
            chk6.style.visibility = 'hidden';
            chk7.style.visibility = 'hidden';
        }
    }

    $('#PTNFLG').on('click', function (e) {

        var chk1 = document.getElementById('pin1');
        var chk2 = document.getElementById('pin2');
        var chk3 = document.getElementById('pin3');
        var chk4 = document.getElementById('pin4');
        var chk5 = document.getElementById('pin5');
        var chk6 = document.getElementById('pin6');
        var chk7 = document.getElementById('pin7');

        //ASI[21 Oct 2019] : To Visible InVisible A週   B週 checkbox
        //var aShuu = document.getElementById('weekA');
        //var bShuu = document.getElementById('weekB');

        if ($("#PTNFLG").is(':checked')) {
            //$("#ColMsgBox").removeClass("invisible");
            $("#ColMsgBox").show();
            chk1.style.visibility = 'visible';
            chk2.style.visibility = 'visible';
            chk3.style.visibility = 'visible';
            chk4.style.visibility = 'visible';
            chk5.style.visibility = 'visible';
            chk6.style.visibility = 'visible';
            chk7.style.visibility = 'visible';

            //ASI[21 Oct 2019] : To Visible A週   B週 checkbox
            //aShuu.style.visibility = 'visible';
            //bShuu.style.visibility = 'visible';
            $('#weekAB').show();
        }
        else {
            //$("#ColMsgBox").last().addClass("invisible");
            chk1.style.visibility = 'hidden';
            chk2.style.visibility = 'hidden';
            chk3.style.visibility = 'hidden';
            chk4.style.visibility = 'hidden';
            chk5.style.visibility = 'hidden';
            chk6.style.visibility = 'hidden';
            chk7.style.visibility = 'hidden';

            //ASI[21 Oct 2019] : To InVisible A週   B週 checkbox
            //aShuu.style.visibility = 'hidden';
            //bShuu.style.visibility = 'hidden';
            $('#weekAB').hide();

        }
    });




    $(function () {
        $('#createB0020').click(function () {
            var bangumi = $("#BANGUMINM").val();
            var kskjst = $("#KSKJKNST").val();
            var kskjed = $("#KSKJKNED").val();
            var gyoymd = $("#GYOMYMD").val();

            if (gyoymd == '' || kskjst == '' || kskjed == '' || bangumi == '') {
                alert("必須項目が未入力です。")
                return false
            }

        });
    });


    $(function () {
        $('#createlink').click(function () {
            var bangumi = $("#BANGUMINM").val();
            var kskjst = $("#KSKJKNST").val();
            var kskjed = $("#KSKJKNED").val();
            var gyoymd = $("#GYOMYMD").val();

            if (gyoymd == '' || kskjst == '' || kskjed == '' || bangumi == '') {
                alert("必須項目が未入力です。")
                return false
            }

        });
    });

    $('#btnCreateHina').on('click', function (e) {
        // 書式に2:雛形を設定
        $('#FMTKBN').val(2)

        if (CheckValues() == false) {
            return false;
        }
    });

    function CheckValues() {

        $('div span[data-valmsg-for="IKKATUMEMO"]').text("");
        $('div span[data-valmsg-for="GYOMYMD"]').text("");
        $('div span[data-valmsg-for="GYOMYMDED"]').text("");
        $('div span[data-valmsg-for="KSKJKNST"]').text("");
        $('div span[data-valmsg-for="KSKJKNED"]').text("");
        $('div span[data-valmsg-for="OAJKNST"]').text("");
        $('div span[data-valmsg-for="OAJKNED"]').text("");
        $('div span[data-valmsg-for="PTNFLG"]').text("");
        $('div span[data-valmsg-for="BANGUMINM"]').text("");
        $('div span[data-valmsg-for="NAIYO"]').text("");
        $('div span[data-valmsg-for="BASYO"]').text("");
        $('div span[data-valmsg-for="CATCD"]').text("");
        $('div span[data-valmsg-for="BANGUMITANTO"]').text("");
        $('div span[data-valmsg-for="BANGUMIRENRK"]').text("");
        $('div span[data-valmsg-for="BIKO"]').text("");

        var catcd = $('#CATCD :selected').text();
        var err = '';

        if (catcd == '') {
            err = '1';
            $('div span[data-valmsg-for="CATCD"]').text("カテゴリーが必要です。");
        }
        else {
            $('div span[data-valmsg-for="CATCD"]').text("");
        }

        var banguminm = $('#BANGUMINM').val();
        var naiyo = $('#NAIYO').val();
        var basyo = $('#BASYO').val();
        var bangumitanto = $('#BANGUMITANTO').val();
        var renraku = $('#BANGUMIRENRK').val();
        var memo = $('#BIKO').val();

        var gyomymd = $('#GYOMYMD').val();
        var gyomymded = $('#GYOMYMDED').val();
        var kskjknst = $('#KSKJKNST').val();
        var kskjkned = $('#KSKJKNED').val();

        var oajknst = $('#OAJKNST').val();
        var oajkned = $('#OAJKNED').val();

        if (gyomymd != '' && gyomymded != '' && gyomymd > gyomymded) {
            err = '1';
            $('div span[data-valmsg-for="GYOMYMD"]').text('業務期間-開始と終了の前後関係が誤っています。');
        }

        if (kskjknst != '' && pad(kskjknst, 5) > '36:00') {
            err = '1';
            $('div span[data-valmsg-for="KSKJKNST"]').text("拘束時間-開始が36時を超えています。");
        }

        if (kskjkned != '' && pad(kskjkned, 5) > '36:00') {
            err = '1';
            $('div span[data-valmsg-for="KSKJKNED"]').text("拘束時間-終了が36時を超えています。");
        }

        if (oajknst != '' && pad(oajknst, 5) > '36:00') {
            err = '1';
            $('div span[data-valmsg-for="OAJKNST"]').text("OA時間-開始が36時を超えています。");
        }

        if (oajkned != '' && pad(oajkned, 5) > '36:00') {
            err = '1';
            $('div span[data-valmsg-for="OAJKNED"]').text("OA時間-終了が36時を超えています。");
        }

        if (oajknst != '' && oajkned != '' && pad(oajknst, 5) <= '36:00' && pad(oajkned, 5) <= '36:00' && pad(oajknst, 5) > pad(oajkned, 5)) {
            err = '1';
            $('div span[data-valmsg-for="OAJKNST"]').text("OA時間-開始と終了の前後関係が誤っています。");
        }

        if (gyomymd != '' & kskjknst != '' && kskjkned != '' && pad(kskjknst, 5) <= '36:00' && pad(kskjkned, 5) <= '36:00') {
            var gyomymdedtemp = gyomymd;

            if (gyomymded != '' && gyomymd != gyomymded) {
                gyomymdedtemp = gyomymded;
            }

            var jtjknst = getjtjkn(gyomymd, pad(kskjknst, 5));
            var jtjkned = getjtjkn(gyomymdedtemp, pad(kskjkned, 5));

            if (gyomymd <= gyomymdedtemp && jtjknst > jtjkned) {
                err = '1';
                $('div span[data-valmsg-for="KSKJKNST"]').text("拘束時間-開始と終了の前後関係が誤っています。");
            }
        }

   
        if (banguminm != '') {

            if (getByteCount(banguminm) > 40) {
                err = '1';
                $('div span[data-valmsg-for="BANGUMINM"]').text("文字数がオーバーしています。");

            }

        }


        if (naiyo != '') {
            if (getByteCount(naiyo) > 40) {
                err = '1';
                $('div span[data-valmsg-for="NAIYO"]').text("文字数がオーバーしています。");

            }
        }

        if (basyo != '') {
            if (getByteCount(basyo) > 40) {
                err = '1';
                $('div span[data-valmsg-for="BASYO"]').text("文字数がオーバーしています。");

            }
        }

        if (bangumitanto != '') {
            if (getByteCount(bangumitanto) > 30) {
                err = '1';
                $('div span[data-valmsg-for="BANGUMITANTO"]').text("文字数がオーバーしています。");

            }
        }


        if (renraku != '') {
            if (getByteCount(renraku) > 30) {
                err = '1';
                $('div span[data-valmsg-for="BANGUMIRENRK"]').text("文字数がオーバーしています。");

            }
        }


        if (memo != '') {
            if (getByteCount(memo) > 30) {
                err = '1';
                $('div span[data-valmsg-for="BIKO"]').text("文字数がオーバーしています。");

            }
        }

        $('#catTable tr').each(function () {
            var i = 1
            $(this).find('span').text("");
            var colValue = $(this).find('input').val();
            if (colValue != undefined && colValue != '') {
                if (getByteCount(colValue) > 12) {
                    err = '1';

                    $(this).find('span').text("文字数がオーバーしています。");
                }

            }

            var i = i + 1
        });


        if (err == '1') {
            return false;
        }

        return true;
    }


    $(document).ready(function () {
        //alert('reach')

        var msgsuccess = jQuery.trim($('#success').val());

        if (msgsuccess.length > 0) {
            alert(msgsuccess);
        }

        //修正モードで画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
        //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
        $("#myForm :input").change(function () {

            $("#myForm").data("MSG", true);


        });

        //ASI[08 Nov 2019] : To Visible A週   B週 checkbox
        if ($("#PTNFLG").is(':checked')) {
            $('#weekAB').show();
        }
        else {
            $('#weekAB').hide();
        }

    });


    function pad(str, max) {
        str = str.toString();
        return str.length < max ? pad("0" + str, max) : str;
    }

    function getjtjkn(dt, time) {

        var HH = time.substr(0, 2);
        var MM = time.substr(3, 2);

        if (HH >= 24) {
            HH = HH - 24;
            HH = pad(HH, 2)

            var dt = new Date(dt);
            var yy = dt.getFullYear();
            var mm = dt.getMonth() + 1;
            var dd = dt.getDate() + 1;

            dt = yy + "/" + pad(mm, 2) + "/" + pad(dd, 2);
        }

        var jtjkn = dt + " " + HH + ":" + MM;

        return jtjkn;
    }


    //バイト数取得
    function getByteCount(str) {
        var b = str.match(/[^\x00-\xff]/g);
        return (str.length + (!b ? 0 : b.length));
    }
</script>


