@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "スポーツシフト登録(仮登録)"
    Dim listFreeItems = DirectCast(ViewBag.FreeItemList, List(Of String))
    Dim listAnaItems = ViewBag.AnaItemList
    Dim listitemvalue = DirectCast(ViewBag.listitemvalue, List(Of String))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
    Dim lstM0010 As List(Of M0010) = ViewBag.lstM0010
    Dim lstmsterm0010 As List(Of M0010) = ViewBag.lstmsterm0010


    Dim strKey As String = ""
    Dim conterI As Integer = 0
    Dim conterJ As Integer = 0
    Dim conterK As Integer = 0

    Dim listAnad0022 As List(Of String()) = ViewBag.listAnad0022
    Dim listAnad0020 As List(Of String()) = ViewBag.listAnad0020
    Dim listAnad0021 As List(Of String()) = ViewBag.listAnad0021
    Dim lastForm As String = ViewBag.lastForm
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
    <div class="row" style="padding-top: 10px;">
        <div class="col-md-10" style="height:58px">
            <h3 style="line-height: 0.2;">修正</h3>
            <hr />
        </div>
        <ul class="nav nav-pills navbar-right" style="padding-right: 15px">
            <li><a href="@Session("A0220EditRtnUrl" & Model.GYOMNO).ToString" id="modorubtn">戻る</a></li>
        </ul>
    </div>
</div>


<div class="row">
    <div class="col-md-12">
        @Using (Html.BeginForm("Edit", "A0220", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.Hidden("lastForm", lastForm, New With {.id = "lastForm"})

        @<div class="col-md-7">
            <div class="form-horizontal">

                @*業務期間コントロール*@
                <div class="form-group">
                    <div class="form-inline">
                        @Html.Label("業務期間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.HiddenFor(Function(model) model.GYOMYMD, htmlAttributes:=New With {.id = "GYOMYMD"})
                            @Html.HiddenFor(Function(model) model.GYOMYMDED, htmlAttributes:=New With {.id = "GYOMYMDED"})
                            @Html.TextBox("GYOMYMD", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .id = "GYOMYMDID"})
                            ～
                            @Html.TextBox("GYOMYMDED", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .id = "GYOMYMDEDID"})
                        </div>
                        <div class=" col-md-offset-3 col-md-9">
                            @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})
                            @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                </div>

                @*拘束時間コントロール*@
                <div class="form-group">
                    <div class="form-inline">
                        @Html.Label("拘束時間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.HiddenFor(Function(model) model.KSKJKNST, htmlAttributes:=New With {.id = "KSKJKNST"})
                            @Html.HiddenFor(Function(model) model.KSKJKNED, htmlAttributes:=New With {.id = "KSKJKNED"})
                            @Html.EditorFor(Function(model) model.KSKJKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled", .id = "KSKJKNSTID"}})
                            ～
                            @Html.EditorFor(Function(model) model.KSKJKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled", .id = "KSKJKNEDID"}})
                        </div>
                        <div class=" col-md-offset-3 col-md-9">
                            @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With {.class = "text-danger"})
                            @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                </div>



                @*スポーツカテゴリコントロール*@
                <div class="form-group">
                    <div class="form-inline">
                        @Html.HiddenFor(Function(model) model.SPORTCATCD)
                        @Html.Label("スポーツカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.DropDownList("SPORTCATCD", New SelectList(ViewBag.SportCatNmList, "SPORTCATCD", "SPORTCATNM", Model.SPORTCATCD), htmlAttributes:=New With {.class = "form-control input-sm", .id = "SPORTCATCDDD", .style = "width:220px;"})
                            @Html.ValidationMessageFor(Function(model) model.SPORTCATCD, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                </div>

                @*スポーツサブカテゴリコントロール*@
                <div class="form-group">
                    <div class="form-inline">
                        @Html.HiddenFor(Function(model) model.SPORTSUBCATCD)
                        @Html.Label("スポーツサブカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.DropDownList("SPORTSUBCATCD", New SelectList(ViewBag.SportSubCatNmList, "SPORTSUBCATCD", "SPORTSUBCATNM", Model.SPORTSUBCATCD), htmlAttributes:=New With {.class = "form-control input-sm", .id = "SPORTSUBCATCDDD", .style = "width:220px;"})
                            @Html.ValidationMessageFor(Function(model) model.SPORTSUBCATCD, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                </div>

                @*カテゴリーコントロール*@
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.CATCD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                    <div class="col-md-9">
                        @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                        @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
                    </div>
                </div>

                @*番組名コントロール*@
                <div class="form-group ">
                    @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                    <div class="col-md-9 comboField">
                        @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm", .style = "width:220px;"}})
                        @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
                    </div>
                </div>

                @*OA時間コントロール*@
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

                @*試合時間コントロール*@
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



                @*場所コントロール*@
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

                @*備考コントロール*@
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

                @*動的コントロール*@
                @For Each item In listFreeItems
                    strKey = String.Format("FreeTxtBxList[{0}]", conterI)
                @If item IsNot Nothing Then
                @<div class="form-group">
                    @Html.Label(item, htmlAttributes:=New With {.class = "control-label col-md-3"})
                    <div class="col-md-9">
                        @Html.TextBox(strKey, listitemvalue(conterI), htmlAttributes:=New With {.class = "form-control input-sm"})
                        @Html.ValidationMessage(strKey, New With {.class = "text-danger"})
                    </div>
                </div>Else
            @Html.Hidden(strKey, "", htmlAttributes:=New With {.class = "form-control input-sm"})
End If              conterI = conterI + 1
                Next
                <div class="form-group">
                    <div class="form-inline">
                        <div class="col-md-offset-3 col-md-9" style="padding-top :12px">
                            <input type="submit" value="更新" id="btnCreate" class="btn btn-default" />
                            @Html.ActionLink("削除", "Delete", routeValues:=New With {.AV_GYOMNO = Model.GYOMNO, .lastForm = lastForm}, htmlAttributes:=New With {.class = "btn btn-danger btn-sm", .style = "margin-left:290px", .id = "btnDelete"})
                        </div>
                    </div>
                </div>
            </div>
        </div>

        @<div class="col-md-5">

            @*動的コントロール*@
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="form-inline">
                        @Html.Label("担当アナ", htmlAttributes:=New With {.class = "control-label col-md-3", .style = "text-align:left;"})
                    </div>
                </div>
                @For Each item In listAnaItems
                    If item IsNot Nothing Then
                        If item.TBLNM.ToString = "D0020" Then
                            strKey = String.Format("D0022[{0}].", (Convert.ToInt64(item.COLINDEX.ToString) - 1).ToString)
                        @<div class="form-group">
                            <div class="form-inline">
                                @Html.Label(item.COLNAME.ToString, htmlAttributes:=New With {.class = "control-label col-md-3", .style = "padding-top:8px;"})
                                @Html.Label(lstmsterm0010.Where(Function(f) f.USERID = item.ANNCATNAME.ToString).Select(Function(f) f.USERNM)(0).ToString, htmlAttributes:=New With {.class = "control-label col-md-3", .style = "text-align:left;padding-top:7px;padding-bottom:7px;"})
                                @Html.Hidden(strKey & "COLIDX", "0", htmlAttributes:=New With {.class = "form-control input-sm"})
                            </div>
                        </div> ElseIf item.TBLNM.ToString = "D0021" Then
                            strKey = String.Format("D0022[{0}].", (Convert.ToInt64(item.COLINDEX.ToString) - 1).ToString)
                    @<div class="form-group">
                        <div class="form-inline">
                            @Html.Label(item.COLNAME.ToString, htmlAttributes:=New With {.class = "control-label col-md-3", .style = "padding-top:8px;"})
                            @Html.Label(item.ANNCATNAME.ToString, htmlAttributes:=New With {.class = "control-label col-md-3", .style = "text-align:left;padding-top:7px;padding-bottom:7px;"})
                            @Html.Hidden(strKey & "COLIDX", "0", htmlAttributes:=New With {.class = "form-control input-sm"})
                        </div>
                    </div>ElseIf item.TBLNM.ToString = "D0022" Then
                            strKey = String.Format("D0022[{0}].", (Convert.ToInt64(item.COLINDEX.ToString) - 1).ToString)
                @<div class="form-group">
                    <div class="form-inline">
                        @Html.Label(item.COLNAME.ToString, htmlAttributes:=New With {.class = "control-label col-md-3", .style = "padding-top:8px;"})
                        <div class="col-md-9">
                            @Html.DropDownList(strKey & "USERID", New SelectList(ViewBag.lstM0010, "USERID", "USERNM", item.ANNCATNAME.ToString), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:200px"})
                            @Html.Hidden(strKey & "COLIDX", item.COLINDEX.ToString, htmlAttributes:=New With {.class = "form-control input-sm"})
                        </div>
                    </div>
                </div>ElseIf item.TBLNM.ToString = "ANAITEM" Then
                            strKey = String.Format("D0022[{0}].", (Convert.ToInt64(item.COLINDEX.ToString) - 1).ToString)
            @If item.COLNAME.ToString <> "" Then
            @<div class="form-group">
                <div class="form-inline">
                    @Html.Label(item.COLNAME.ToString, htmlAttributes:=New With {.class = "control-label col-md-3", .style = "padding-top:8px;"})
                    <div class="col-md-9">
                        @Html.DropDownList(strKey & "USERID", New SelectList(ViewBag.lstM0010, "USERID", "USERNM", ViewBag.lstM0010(0).USERID), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:200px"})
                        @Html.Hidden(strKey & "COLIDX", (Convert.ToInt64(item.COLINDEX.ToString)), htmlAttributes:=New With {.class = "form-control input-sm"})
                    </div>
                </div>
            </div>
Else
        @Html.Hidden(strKey & "COLIDX", "0", htmlAttributes:=New With {.class = "form-control input-sm"})
End If
                        End If
                    End If
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
                                <div class="col-md-offset-3 col-md-9" style="padding-top :12px" >
                                    <input type="submit" value="更新" id="btnCreate" class="btn btn-default" />
                                    @Html.ActionLink("削除", "Delete", routeValues:=New With {.AV_GYOMNO = Model.GYOMNO, .lastForm = lastForm}, htmlAttributes:=New With {.class = "btn btn-danger btn-sm", .style = "margin-left:290px", .id = "btnDelete"})
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>*@

    @<div Class="form-group">
        <div Class="col-md-9">
            @Html.HiddenFor(Function(model) model.GYOMNO, "")
        </div>
    </div>

        End Using
    </div>
</div>

<script>
$("form").submit(function () {
$('#GYOMYMD').val($('#GYOMYMDID').val());
$('#GYOMYMDED').val($('#GYOMYMDEDID').val());
$('#KSKJKNST').val($('#KSKJKNSTID').val());
$('#KSKJKNED').val($('#KSKJKNEDID').val());
});

$(document).ready(function () {

$('#SPORTCATCDDD').prop('disabled', true);
$('#SPORTSUBCATCDDD').prop('disabled', true);

    if (@listAnad0020.Count  != 0 || @listAnad0021.Count != 0) {
    $("#btnDelete").attr("disabled", true);
    $('#btnDelete').css('pointer-events', 'none');

    $('#GYOMYMDID').prop('disabled', true);
    $('#GYOMYMDEDID').prop('disabled', true);
    $('#KSKJKNSTID').prop('disabled', true);
    $('#KSKJKNEDID').prop('disabled', true);
}

});

//内容リストで選択した時
$("#selectBoxNaiyo").change(function () {
    var val = this.value;
    $('#NAIYO').val(val);
});

$(function () {
$('#btnCreate').click(function () {
    var message = $('#MESSAGE').val();

    var result = confirm("更新します。よろしいですか?");

    if (result == false) {
        return false;
    }
});
});


</script>
