@ModelType NTV_SHIFT.M0150
@Code
    ViewData("Title") = "詳細設定"
End Code

<style>
      .mytable {
        width: 1018px;
    }

    table.mytable tbody,
    table.mytable thead {
        display: block;
    }

    table.mytable tbody {
        height: 533px;
        width: 1018px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    hr {
        margin-top: 10px;
        margin-bottom: 2px;
        border: 0;
        border-top: 1px solid #ecf0f1;
    }

    #resetRadio {
        padding: 0px 9px;
        font-size: 13px;
        line-height: -0.5;
        border-radius: 3px;
        border-width: 0px;
    }

    .col-md-offset-custom {
    margin-left: 43.33333333333333%;
  }

    .col1,.col2,.col3,.col4,.col5,.col6,.col7,.col8,.col9,.co20 {
        width: 100px;
    }
    .col4_col6{
        width:300px;
    }
    .col7_col9{
        width:300px;
    }
    .editable-text{
        width:80px;
        border-width:2px;
        border:2px solid #dce4ec;
        border-radius:4px;
        height:24px;
    }
    .col_type{
        width:80px;
        border:2px solid #dce4ec;
        border-radius:4px;
        height:24px;
    }

    .pd-top-10{
        padding-top:10px;
    }

    .col-md-2 {
        width: 19.666666666666664%;
    }

 </style>

<div class="container">
   @Using (Html.BeginForm("CreateM0130", "A0210", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()
       @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

    @<div class="form-horizontal">

    <ul Class="nav nav-pills navbar-right">
        @if Session("Mode") = "新規" Then
            @<li>@Html.ActionLink("戻る", "Create", Nothing, htmlAttributes:=New With {.id = "btnNewModoru"})</li>
        Else
            @<li>@Html.ActionLink("戻る", "Edit", New With {.id = Model.SPORTCATCD}, htmlAttributes:=New With {.id = "btnNewModoru"})</li>
        End If
    </ul>

    <h3>詳細設定</h3>

    <hr />
    @Html.Hidden("IsDataChanged", If(Session("IsDataChanged") = Nothing, False, Session("IsDataChanged")))
    @Html.Hidden("IsDataChangedCreate", If(Session("IsDataChangedCreate") = Nothing, False, Session("IsDataChangedCreate")))
    @Html.Hidden("FREEZE_LSTCOLNM")
    @Html.HiddenFor(Function(model) model.SELECTINDEX)
    @Html.HiddenFor(Function(model) model.SPORTCATNM)
    @Html.HiddenFor(Function(model) model.SPORTSUBCATNM)
    @Html.HiddenFor(Function(model) model.SPORTCATCD)
    @Html.HiddenFor(Function(model) model.SPORTSUBCATCD)

    <div class="col-md-offset-1 col-md-12">
        <div class="form-group">
            <label class="control-label col-md-2" style="text-align:left;">スポーツカテゴリー名</label>
            <div class="pd-top-10">@Model.SPORTCATNM</div>

        </div>
        <div class="form-group">
            <label class="control-label col-md-2" style="text-align:left;">スポーツサブカテゴリー名</label>
            <div class="pd-top-10">@Model.SPORTSUBCATNM</div>
        </div>

        <div class="form-group">
            <table class="table table-bordered mytable" id="categoryDetailTable" style="font-size:13px">
                <thead>
                    <tr>
                        <th class="col1"></th>
                        <th class="col2">項目名</th>
                        <th class="col3">アナ/フリー</th>
                        <th class="col4_col6" colspan="3">全体スポーツシフト表</th>
                        <th class="col7_col9" colspan="3">種目別スポーツシフト表</th>
                        <th class="col7_co20" colspan="3">最終固定項目</th>
                    </tr>
                    <tr>
                        <th class="col1"></th>
                        <th class="col2"></th>
                        <th class="col3">入力</th>
                        <th class="col4">表示</th>
                        <th class="col5">表示名</th>
                        <th class="col6">表示順</th>
                        <th class="col7">表示</th>
                        <th class="col8">表示名</th>
                        <th class="col9">表示順</th>
                        <th class="co20"><input id="resetRadio" type="button" class="btn btn-success btn-sm btnForward" value="初期設定" onclick="ResetRadio()"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="col1"><b>番組名</b></td>
                        <td class="col2"></td>
                        <td class="col3"></td>
                        <td class="col4" style="text-align:center;">

                            @If Model.BANGUMIHYOJ1 <> Nothing AndAlso Model.BANGUMIHYOJ1 <> "0" Then
                                @Html.CheckBox("BANGUMIHYOJ1", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("BANGUMIHYOJ1", False, htmlAttributes:=New With {.value = ""})
End If
                        </td>
                        <td class="col5">
                            @Html.EditorFor(Function(model) model.BANGUMIHYOJNM1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BANGUMIHYOJNM1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col6">
                            @Html.EditorFor(Function(model) model.BANGUMIHYOJJN1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BANGUMIHYOJJN1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col7" style="text-align:center;">

                            @If Model.BANGUMIHYOJ2 <> Nothing AndAlso Model.BANGUMIHYOJ2 <> "0" Then
                                @Html.CheckBox("BANGUMIHYOJ2", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("BANGUMIHYOJ2", False, htmlAttributes:=New With {.value = ""})
End If
                        </td>
                        <td class="col8">
                            @Html.EditorFor(Function(model) model.BANGUMIHYOJNM2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BANGUMIHYOJNM2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col9">
                            @Html.EditorFor(Function(model) model.BANGUMIHYOJJN2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BANGUMIHYOJJN2, "", New With {.class = "text-danger"})</div>
</td>                   <td class="co20">
                             @If Model.FREEZE_LSTCOLNM = "BANGUMINM" Then
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "BANGUMINM", htmlAttributes:=New With {.style = "margin-left:30px;", .checked = "checked"})
Else
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "BANGUMINM", htmlAttributes:=New With {.style = "margin-left:30px;"})
End If
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><b>拘束時間</b></td>
                        <td class="col2"></td>
                        <td class="col3"></td>
                        <td class="col4" style="text-align:center;">

                            @If Model.KSKJKNHYOJ1 <> Nothing AndAlso Model.KSKJKNHYOJ1 <> "0" Then
                                @Html.CheckBox("KSKJKNHYOJ1", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("KSKJKNHYOJ1", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col5">
                            @Html.EditorFor(Function(model) model.KSKJKNHYOJNM1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.KSKJKNHYOJNM1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col6">
                            @Html.EditorFor(Function(model) model.KSKJKNHYOJJN1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.KSKJKNHYOJJN1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col7" style="text-align:center;">

                            @If Model.KSKJKNHYOJ2 <> Nothing AndAlso Model.KSKJKNHYOJ2 <> "0" Then
                                @Html.CheckBox("KSKJKNHYOJ2", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("KSKJKNHYOJ2", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col8">
                            @Html.EditorFor(Function(model) model.KSKJKNHYOJNM2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.KSKJKNHYOJNM2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col9">
                            @Html.EditorFor(Function(model) model.KSKJKNHYOJJN2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.KSKJKNHYOJJN2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="co20">
                             @If Model.FREEZE_LSTCOLNM = "KSKJKNST" Then
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "KSKJKNST", htmlAttributes:=New With {.style = "margin-left:30px;", .checked = "checked"})
Else
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "KSKJKNST", htmlAttributes:=New With {.style = "margin-left:30px;"})
                             End If
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><b>OA時間</b></td>
                        <td class="col2"></td>
                        <td class="col3"></td>
                        <td class="col4" style="text-align:center;">

                            @If Model.OAJKNHYOJ1 <> Nothing AndAlso Model.OAJKNHYOJ1 <> "0" Then
                                @Html.CheckBox("OAJKNHYOJ1", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("OAJKNHYOJ1", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col5">
                            @Html.EditorFor(Function(model) model.OAJKNHYOJNM1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.OAJKNHYOJNM1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col6">
                            @Html.EditorFor(Function(model) model.OAJKNHYOJJN1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.OAJKNHYOJJN1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col7" style="text-align:center;">

                            @If Model.OAJKNHYOJ2 <> Nothing AndAlso Model.OAJKNHYOJ2 <> "0" Then
                                @Html.CheckBox("OAJKNHYOJ2", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("OAJKNHYOJ2", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col8">
                            @Html.EditorFor(Function(model) model.OAJKNHYOJNM2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.OAJKNHYOJNM2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col9">
                            @Html.EditorFor(Function(model) model.OAJKNHYOJJN2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.OAJKNHYOJJN2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="co20">
                            @If Model.FREEZE_LSTCOLNM = "OAJKNST" Then
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "OAJKNST", htmlAttributes:=New With {.style = "margin-left:30px;", .checked = "checked"})
Else
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "OAJKNST", htmlAttributes:=New With {.style = "margin-left:30px;"})
End If
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><b>試合時間</b></td>
                        <td class="col2"></td>
                        <td class="col3"></td>
                        <td class="col4" style="text-align:center;">

                            @If Model.SAIKNHYOJ1 <> Nothing AndAlso Model.SAIKNHYOJ1 <> "0" Then
                                @Html.CheckBox("SAIKNHYOJ1", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("SAIKNHYOJ1", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col5">
                            @Html.EditorFor(Function(model) model.SAIKNHYOJNM1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.SAIKNHYOJNM1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col6">
                            @Html.EditorFor(Function(model) model.SAIKNHYOJJN1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.SAIKNHYOJJN1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col7" style="text-align:center;">

                            @If Model.SAIKNHYOJ2 <> Nothing AndAlso Model.SAIKNHYOJ2 <> "0" Then
                                @Html.CheckBox("SAIKNHYOJ2", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("SAIKNHYOJ2", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col8">
                            @Html.EditorFor(Function(model) model.SAIKNHYOJNM2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.SAIKNHYOJNM2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col9">
                            @Html.EditorFor(Function(model) model.SAIKNHYOJJN2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.SAIKNHYOJJN2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="co20">
                            @If Model.FREEZE_LSTCOLNM = "SAIJKNST" Then
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "SAIJKNST", htmlAttributes:=New With {.style = "margin-left:30px;", .checked = "checked"})
Else
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "SAIJKNST", htmlAttributes:=New With {.style = "margin-left:30px;"})
End If
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><b>場所</b></td>
                        <td class="col2"></td>
                        <td class="col3"></td>
                        <td class="col4" style="text-align:center;">

                            @If Model.BASYOHYOJ1 <> Nothing AndAlso Model.BASYOHYOJ1 <> "0" Then
                                @Html.CheckBox("BASYOHYOJ1", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("BASYOHYOJ1", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col5">
                            @Html.EditorFor(Function(model) model.BASYOHYOJNM1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BASYOHYOJNM1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col6">
                            @Html.EditorFor(Function(model) model.BASYOHYOJJN1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BASYOHYOJJN1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col7" style="text-align:center;">

                            @If Model.BASYOHYOJ2 <> Nothing AndAlso Model.BASYOHYOJ2 <> "0" Then
                                @Html.CheckBox("BASYOHYOJ2", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("BASYOHYOJ2", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col8">
                            @Html.EditorFor(Function(model) model.BASYOHYOJNM2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BASYOHYOJNM2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col9">
                            @Html.EditorFor(Function(model) model.BASYOHYOJJN2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BASYOHYOJJN2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="co20">
                            @If Model.FREEZE_LSTCOLNM = "BASYO" Then
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "BASYO", htmlAttributes:=New With {.style = "margin-left:30px;", .checked = "checked"})
Else
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "BASYO", htmlAttributes:=New With {.style = "margin-left:30px;"})
End If
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"><b>備考</b></td>
                        <td class="col2"></td>
                        <td class="col3"></td>
                        <td class="col4" style="text-align:center;">


                            @If Model.BIKOHYOJ1 <> Nothing AndAlso Model.BIKOHYOJ1 <> "0" Then

                                @Html.CheckBox("BIKOHYOJ1", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("BIKOHYOJ1", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col5">
                            @Html.EditorFor(Function(model) model.BIKOHYOJNM1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BIKOHYOJNM1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col6">
                            @Html.EditorFor(Function(model) model.BIKOHYOJJN1, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BIKOHYOJJN1, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col7" style="text-align:center;">


                            @If Model.BIKOHYOJ2 <> Nothing AndAlso Model.BIKOHYOJ2 <> "0" Then
                                @Html.CheckBox("BIKOHYOJ2", True, htmlAttributes:=New With {.value = ""})
Else
                                @Html.CheckBox("BIKOHYOJ2", False, htmlAttributes:=New With {.value = ""})
End If

                        </td>
                        <td class="col8">
                            @Html.EditorFor(Function(model) model.BIKOHYOJNM2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BIKOHYOJNM2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="col9">
                            @Html.EditorFor(Function(model) model.BIKOHYOJJN2, New With {.htmlAttributes = New With {.class = "editable-text"}})
                            <div>@Html.ValidationMessageFor(Function(model) model.BIKOHYOJJN2, "", New With {.class = "text-danger"})</div>
</td>
                        <td class="co20">
                            @If Model.FREEZE_LSTCOLNM = "BIKO" Then
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "BIKO", htmlAttributes:=New With {.style = "margin-left:30px;", .checked = "checked"})
Else
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", "BIKO", htmlAttributes:=New With {.style = "margin-left:30px;"})
End If
                        </td>
                    </tr>
                    @For i = 1 To 50
                        Dim COL = "COL" & i.ToString("00")
                        @<tr>
                            <td class="col1"><b>項目 @(i) </b></td>
                            <td class="col2">
                                @Html.Editor(COL, New With {.htmlAttributes = New With {.class = "editable-text"}})
                                <div>@Html.ValidationMessage(COL, New With {.Class = "text-danger"})</div>
                            </td>
                            <td class="col3">
                                @Code
Dim COLTYPE = {
                                                                                                                                                                                               New SelectListItem() With {.Value = "0", .Text = ""},
                                                                                                                                                                                               New SelectListItem() With {.Value = "1", .Text = "フリー"},
                                                                                                                                                                                               New SelectListItem() With {.Value = "2", .Text = "アナ"}
                                                                                                                                                                                           }
                                End Code
                                @Html.DropDownList(COL & "_TYPE", New SelectList(COLTYPE, "Value", "Text", ""), htmlAttributes:=New With {.class = "col_type"})
                                <div>@Html.ValidationMessage(COL & "_TYPE", New With {.class = "text-danger"})</div>
                            </td>
                            <td class="col4" style="text-align:center;">@Html.Editor(COL & "_HYOJ1")</td>
                            <td class="col5">
                                @Html.Editor(COL & "_HYOJNM1", New With {.htmlAttributes = New With {.class = "editable-text"}})
                                <div>@Html.ValidationMessage(COL & "_HYOJNM1", New With {.Class = "text-danger"})</div>
                            </td>
                            <td class="col6">
                                @Html.Editor(COL & "_HYOJJN1", New With {.htmlAttributes = New With {.class = "editable-text"}})
                                <div>@Html.ValidationMessage(COL & "_HYOJJN1", New With {.Class = "text-danger"})</div>
                            </td>
                            <td class="col7" style="text-align:center;">@Html.Editor(COL & "_HYOJ2")</td>
                            <td class="col8">
                                @Html.Editor(COL & "_HYOJNM2", New With {.htmlAttributes = New With {.class = "editable-text"}})
                                <div>@Html.ValidationMessage(COL & "_HYOJNM2", New With {.Class = "text-danger"})</div>
                            </td>
                            <td class="col9">
                                @Html.Editor(COL & "_HYOJJN2", New With {.htmlAttributes = New With {.class = "editable-text"}})
                                <div>@Html.ValidationMessage(COL & "_HYOJJN2", New With {.Class = "text-danger"})</div>
                            </td>
                            <td class="co20">
                                @If Model.FREEZE_LSTCOLNM = COL Then
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", COL, htmlAttributes:=New With {.style = "margin-left:30px;", .checked = "checked"})
Else
                                 @Html.RadioButton("FREEZE_LSTCOL_NO", COL, htmlAttributes:=New With {.style = "margin-left:30px;"})
End If
                            </td>
                        </tr> Next
                </tbody>
            </table>
        </div>
        <div class="form-group">
            <div class="col-md-offset-custom col-md-10">
                <input id="btnMasterUpdate" type="submit" value="OK" class="btn btn-default" />
            </div>
        </div>
    </div>
</div>
                                    End Using
</div>
   
<script>
    //$('#btnMasterUpd').on('click', function (e) {
    //    return false;
    //});

    $(function () {
        $('#btnMasterUpdate').click(function () {

            //BANGUMIHYOJ1
            if (document.getElementById("BANGUMIHYOJ1").checked == true) {
                document.getElementById("BANGUMIHYOJ1").value = "1";

            } 

            if (document.getElementById("BANGUMIHYOJ2").checked == true) {
                document.getElementById("BANGUMIHYOJ2").value = "1";

            } 

            //KSKJKNHYOJ1
            if (document.getElementById("KSKJKNHYOJ1").checked == true) {
                document.getElementById("KSKJKNHYOJ1").value = "2";

            } else {
                document.getElementById("KSKJKNHYOJ1").value = "0";
            }

            if (document.getElementById("KSKJKNHYOJ2").checked == true) {
                document.getElementById("KSKJKNHYOJ2").value = "2";

            } else {
                document.getElementById("KSKJKNHYOJ2").value = "0";
            }

            //OAJKNHYOJ1
            if (document.getElementById("OAJKNHYOJ1").checked == true) {
                document.getElementById("OAJKNHYOJ1").value = "3";

            } else {
                document.getElementById("OAJKNHYOJ1").value = "0";
            }

            if (document.getElementById("OAJKNHYOJ2").checked == true) {
                document.getElementById("OAJKNHYOJ2").value = "3";

            } else {
                document.getElementById("OAJKNHYOJ2").value = "0";
            }

            //SAIKNHYOJ1
            if (document.getElementById("SAIKNHYOJ1").checked == true) {
                document.getElementById("SAIKNHYOJ1").value = "4";

            } else {
                document.getElementById("SAIKNHYOJ1").value = "0";
            }

            if (document.getElementById("SAIKNHYOJ2").checked == true) {
                document.getElementById("SAIKNHYOJ2").value = "4";

            } else {
                document.getElementById("SAIKNHYOJ2").value = "0";
            }

             //BASYOHYOJ1
            if (document.getElementById("BASYOHYOJ1").checked == true) {
                document.getElementById("BASYOHYOJ1").value = "5";

            } else {
                document.getElementById("BASYOHYOJ1").value = "0";
            }

            if (document.getElementById("BASYOHYOJ2").checked == true) {
                document.getElementById("BASYOHYOJ2").value = "5";

            } else {
                document.getElementById("BASYOHYOJ2").value = "0";
            }

            //BIKOHYOJ1
            if (document.getElementById("BIKOHYOJ1").checked == true) {
                document.getElementById("BIKOHYOJ1").value = "6";

            } else {
                document.getElementById("BIKOHYOJ1").value = "0";
            }

            if (document.getElementById("BIKOHYOJ2").checked == true) {
                document.getElementById("BIKOHYOJ2").value = "6";

            } else {
                document.getElementById("BIKOHYOJ2").value = "0";
            }
        });
    });

    $("#myForm :input").change(function (e) {
        var inputVal = $(this).val();
        var oldValue = e.target.defaultValue;

        if (e.target.type == "checkbox" || e.target.type == "radio") {
            if (e.target.defaultChecked != e.target.checked) {
                $("#myForm").data("MSG", true);
                document.getElementById("IsDataChanged").value = true;
            } else {
                $("#myForm").data("MSG", false);
                document.getElementById("IsDataChanged").value = false;
            }
        }
        else {
            if (oldValue != inputVal) {
                $("#myForm").data("MSG", true);
                document.getElementById("IsDataChanged").value = true;
            }
            else {
                $("#myForm").data("MSG", false);
                document.getElementById("IsDataChanged").value = false;
            }
        }
    });

    $('input[type=radio][name=FREEZE_LSTCOL_NO]').change(function() {
        document.getElementById("FREEZE_LSTCOLNM").value = this.value;
    });

    function ResetRadio() {
        //Since value changes so make it a change
        if ($('input[type=radio][name=FREEZE_LSTCOL_NO]:checked').length > 0) {
            $("#myForm").data("MSG", true);
            document.getElementById("IsDataChanged").value = true;
            $('input[type=radio][name=FREEZE_LSTCOL_NO]:checked').removeAttr('checked');
            document.getElementById("FREEZE_LSTCOLNM").value = "";
        }
    }

</script>
 
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
