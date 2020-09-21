@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "業務登録"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
    Dim M0060Kyukde = DirectCast(ViewBag.KyukDe, M0060)
    Dim strKey As String = ""
    Dim strKey1 As String = ""
    Dim strId As String = ""
    Dim KarianacatList = DirectCast(ViewBag.KarianacatList, List(Of M0080))

    Dim M0060KyukHouDe = DirectCast(ViewBag.KyukHouDe, M0060)

    Dim sportShiftFlg As Boolean = model.SPORTFLG
    Dim listFreeItems = DirectCast(ViewBag.FreeItemList, List(Of String))
    Dim listAnaItems = DirectCast(ViewBag.AnaItemList, List(Of String))
    Dim listitemvalue = DirectCast(ViewBag.listitemvalue, List(Of String))
    Dim Form_name = ViewData("Form_name")

    Dim conterI As Integer = 0
    Dim conterJ As Integer = 0

    Dim lsM0150AnaCol = ViewBag.lsM0150AnaCol
End Code
 
<style>
    .comboField {
        position: relative;
    }

    .inputBox {
        font-size: 14px;
        width: 200px;
        position: absolute;
    }

    .selectBox {
        font-size: 14px;
        width: 225px;
    }

    .inputBoxAna {
        font-size: 14px;
        width: 100px;
        position: absolute;
    }

    .inputBoxAnaCol {
        font-size: 14px;
        width: 120px;
        position: absolute;
    }

    .selectBoxAna {
        font-size: 14px;
        width: 120px;
        height:33px;
    }

    .selectBoxAnaCol {
        font-size: 14px;
        width: 120px;
        height:33px;
    }

</style>

@Using (Html.BeginForm("Edit", "B0020", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
    @Html.AntiForgeryToken()

    @<div class="row">
        
    <div class="col-md-1 col-md-push-11">
        <div style="padding-top:10px;">
            <ul class="nav nav-pills ">
                @If Session("B0020EditRtnUrl" & Model.GYOMNO) IsNot Nothing Then
                    @<li><a href="@Session("B0020EditRtnUrl" & Model.GYOMNO).ToString" id="btnModoru">戻る</a></li>
                Else
                    @<li>@Html.ActionLink("戻る", "Index", "C0030", Nothing, htmlAttributes:=New With {.id = "btnModoru"})</li>
                End If
            </ul>
        </div>
    </div>

    @If ViewBag.AddTitle <> Nothing Then
        @<div class="col-md-10 col-md-pull-1">
            <div class="row">
                <div class="col-md-5">
                    <h3>
                        修正 @ViewBag.AddTitle
                    </h3>
                </div>
                <div class="col-md-5">
                    <div style="margin-top:23px;">
                        <input type="button" value="下書保存" id="btnCreateShita" class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModalShitaHina" data-fmtkbn="1" />
                        &nbsp | &nbsp
                        <input type="button" value="雛形保存" id="btnCreateHina" class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModalShitaHina" data-fmtkbn="2" />
                        &nbsp | &nbsp
                        @If Session("UrlReferrer") IsNot Nothing AndAlso Session("UrlReferrer").ToString.Contains("C0030") Then
                            @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnEditModoru"})
                        Else
                            @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
                                .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
                                .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
                                .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
                                .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk"), .OAJKNST = Session("OAJKNST"), .OAJKNED = Session("OAJKNED"), .Biko = Session("Biko")}, htmlAttributes:=New With {.id = "btnEditModoru"})
                        End If
                    </div>
                </div>
            </div>
        </div>
    Else
        @<div class="col-md-6 col-md-pull-1">
            <div class="row">
                <div class="col-md-3">
                    <h3>修正</h3>
                </div>
                <div class="col-md-5">
                    <div style="margin-top:23px;">
                        <input type="button" value="下書保存" id="btnCreateShita" class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModalShitaHina" data-fmtkbn="1" />
                        &nbsp | &nbsp
                        <input type="button" value="雛形保存" id="btnCreateHina" class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModalShitaHina" data-fmtkbn="2" />
                        &nbsp | &nbsp
                        @If Session("UrlReferrer") IsNot Nothing AndAlso Session("UrlReferrer").ToString.Contains("C0030") Then
                            @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnEditModoru"})
                        Else
                            @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
                            .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
                            .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
                            .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
                            .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk"), .OAJKNST = Session("OAJKNST"), .OAJKNED = Session("OAJKNED"), .Biko = Session("Biko")}, htmlAttributes:=New With {.id = "btnEditModoru"})
                        End If
                    </div>
                </div>
            </div>
        </div>

    End If

</div>
    
   
    @<div class="row">
    
        <div class="col-md-6">

            <div class="form-horizontal">

                <hr style="margin-top:1px;" />

                <div id="divSummaryError" class="text-danger" style="visibility:hidden">
                </div>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @Html.Hidden("Form_name", ViewBag.Form_name)
                @Html.Hidden("ACUSERID", ViewBag.ACUSERID)
                @Html.HiddenFor(Function(model) model.GYOMNO)
                @Html.HiddenFor(Function(model) model.JTJKNST)
                @Html.HiddenFor(Function(model) model.JTJKNED)
                @Html.HiddenFor(Function(model) model.PGYOMNO)
                @Html.HiddenFor(Function(model) model.IKTFLG)
                @Html.HiddenFor(Function(model) model.IKTUSERID)
                @Html.HiddenFor(Function(model) model.IKKATUNO)
                @Html.HiddenFor(Function(model) model.RNZK)
                @Html.HiddenFor(Function(model) model.FMTKBN)
                @Html.HiddenFor(Function(model) model.HINAMEMO)
                @Html.HiddenFor(Function(model) model.DATAKBN)
                @Html.HiddenFor(Function(model) model.ANAIDLIST)
                @Html.HiddenFor(Function(model) model.KARIANALIST)
                @Html.HiddenFor(Function(model) model.YOINUSERID)
                @Html.HiddenFor(Function(model) model.YOINUSERNM)
                @Html.HiddenFor(Function(model) model.YOINUSERID)
                @Html.HiddenFor(Function(model) model.YOINUSERNM)
                @Html.HiddenFor(Function(model) model.YOINIDYES)
                @Html.HiddenFor(Function(model) model.SPORT_OYAFLG)
                @Html.HiddenFor(Function(model) model.SPORTFLG)
                @Html.HiddenFor(Function(model) model.SPORTCATCD)
                @Html.HiddenFor(Function(model) model.SPORTSUBCATCD)
                @Html.Hidden("showkoho", ViewBag.ShowKoho)

                <div class="form-group">
                    <div class="form-inline">
                        @Html.Label("業務期間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9">
                            @Html.TextBox("GYOMYMD", Model.GYOMYMD, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                            ～
                            @Html.TextBox("GYOMYMDED", Model.GYOMYMDED, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})
                        </div>
                    </div>
                    <div class=" col-md-offset-3 col-md-9">
                        @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})
                        @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})
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

                <!-- ASI[06 Dec 2019] スポーツカテゴリ, スポーツサブカテゴリ added -->
                <div id="sportCatCd_SubCatCdDIV">
                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("スポーツカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                            <div class="col-md-9">
                                @Html.DropDownList("SPORTCATCDDD", New SelectList(ViewBag.SportCatNmList, "SPORTCATCD", "SPORTCATNM", Model.SPORTCATCD), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:220px;"})
                                @Html.ValidationMessageFor(Function(model) model.SPORTCATCD, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-inline">
                            @Html.Label("スポーツサブカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                            <div class="col-md-9">
                                @Html.DropDownList("SPORTSUBCATCDDD", New SelectList(ViewBag.SportSubCatNmList, "SPORTSUBCATCD", "SPORTSUBCATNM", Model.SPORTSUBCATCD), htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:220px;"})
                                @Html.ValidationMessageFor(Function(model) model.SPORTSUBCATCD, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                    </div>
                </div>

                @*<div class="form-group">
            <div class="form-inline">
                @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-3"})
                <div class="col-md-9">
                    @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm", .list = "Bangumi"}})
                    @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
                    @If Bangumilst IsNot Nothing Then
                    @<datalist id="Bangumi">
                        @For Each item In Bangumilst
                    @<option>@Html.Encode(item.BANGUMINM) </option>
                            Next
                    </datalist>
                    End If
                </div>
            </div>
        </div>*@

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.CATCD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                    <div class="col-md-9">
                        @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                        @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
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
                <!-- ASI[21 Dec 2019] 試合時間 added -->
                <div id="SAIJKNDIV">
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
                </div>
                @*<div class="form-group">
            <div class="form-inline">
                @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                <div class="col-md-9">
                    @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control input-sm", .value = ViewData("NAIYO"), .list = "Naiyo"}})
                    @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
                    @If NaiyouList IsNot Nothing Then
                    @<datalist id="Naiyo">
                        @For Each item In NaiyouList
                    @<option>@Html.Encode(item.NAIYO) </option>
                            Next
                    </datalist>
                    End If
                </div>
            </div>
        </div>*@

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

                <!-- ASI[06 Dec 2019]: Desing added for FURI -->
                <div id="furiDIV">
                    @For Each item In listFreeItems
            strKey = String.Format("FreeTxtBxList[{0}]", conterI)
            @If item IsNot Nothing Then
            @<div class="form-group">
                @Html.Label(item, htmlAttributes:=New With {.class = "control-label col-md-3"})
                <div class="col-md-9">
                    @Html.TextBox(strKey, listitemvalue(conterI), htmlAttributes:=New With {.class = "form-control input-sm"})
                    @Html.ValidationMessage(strKey, New With {.class = "text-danger"})
                </div>
            </div>
Else
            @Html.Hidden(strKey, "", htmlAttributes:=New With {.class = "form-control input-sm"})
End If
conterI = conterI + 1
Next
                </div>

                <div id="divHidden" style="visibility:hidden">
                    @Html.HiddenFor(Function(model) model.GYOMYMD)
                    @Html.HiddenFor(Function(model) model.GYOMYMDED)
                    @Html.HiddenFor(Function(model) model.KSKJKNST)
                    @Html.HiddenFor(Function(model) model.KSKJKNED)
                    @Html.HiddenFor(Function(model) model.SPORT_OYAFLG)
                    @Html.HiddenFor(Function(model) model.SPORTFLG)
                </div>

                <div id="divSuccess" class="text-success" style="visibility:hidden">
                    @Html.Hidden("success", TempData("success"))
                </div>

                <p></p>

                <div class="form-group">
                    <div class="col-md-offset-3 col-md-7">
                        <input type="button" id="btnSearchKoho" value="候補検索" class="btn btn-info btn-sm " />
                        &nbsp | &nbsp
                        <input type="button" id="btnReEdit" value="再編集" class="btn btn-primary btn-sm " />
                        &nbsp | &nbsp
                        <input id="btnUpdate" type="submit" value="更新" class="btn btn-default" />
                    </div>
                    <div class="col-md-2" style="padding-top:6px;">
                        @Html.ActionLink("削除", "Delete", routeValues:=New With {.id = Model.GYOMNO}, htmlAttributes:=New With {.class = "btn btn-danger btn-sm"})
                    </div>
                </div>
            </div>

            @*<p></p>
            <div class="row">
                <div class="col-sm-10">
                    <input type="button" value="下書保存" id="btnCreateShita" class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModalShitaHina" data-fmtkbn="1" />
                    &nbsp &nbsp
                    <input type="button" value="雛形保存" id="btnCreateHina" class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModalShitaHina" data-fmtkbn="2" />
                </div>
                <div class="col-sm-2">
                    @Html.ActionLink("削除", "Delete", routeValues:=New With {.id = Model.GYOMNO}, htmlAttributes:=New With {.class = "btn btn-danger btn-sm"})
                </div>
            </div>*@

                @*<p></p>
    <div>
        @If Session("UrlReferrer") IsNot Nothing AndAlso Session("UrlReferrer").ToString.Contains("C0030") Then
            @Html.ActionLink("一覧に戻る", "Index")
        Else
            @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
            .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
            .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
            .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
            .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk")})
            End If
    </div>*@

        </div>

        <div class="col-md-2">
            <table class="table table-bordered" id="myTable">
                <tr>
                    <th colspan="3">
                アナウンサー
                    </th>
                </tr>

                @If Model IsNot Nothing AndAlso Model.D0020 IsNot Nothing AndAlso Model.D0020.Count > 0 Then
                @For i = 0 To Model.D0020.Count - 1
                Dim j = i
                strKey = String.Format("D0020[{0}].", i)
                strId = String.Format("D0020_{0}__", i)
                @<tr>
                     @Html.Raw("<td>")
                        @Html.ValidationMessage(strKey & "USERID", New With {.class = "text-danger", .title = "アナウンサーの休暇、または業務情報がが変更されています。候補検索を行い、確認してください。"})

                        @If Model.D0020(i).M0010 IsNot Nothing Then
                            @(Model.D0020(i).M0010.USERNM)
                            @Html.Hidden(strKey & "USERNM", Model.D0020(i).M0010.USERNM)
                        Else
                            @(Model.D0020(i).USERNM)
                            @Html.Hidden(strKey & "USERNM", Model.D0020(i).USERNM)
                        End If

                        @Html.Hidden(strKey & "USERID", Model.D0020(i).USERID)
                        @Html.Hidden(strKey & "YOINIDYES", Model.D0020(i).YOINIDYES)
                        @*@Html.Hidden(strKey & "YOINID", Model.D0020(i).YOINID)
                        @Html.Hidden(strKey & "DESKMEMO", Model.D0020(i).DESKMEMO)
                        @Html.Hidden(strKey & "MARKKYST", Model.D0020(i).MARKKYST)
                        @Html.Hidden(strKey & "MARKSYTK", Model.D0020(i).MARKSYTK)
                        @Html.Hidden(strKey & "ROWIDX", Model.D0020(i).ROWIDX)*@
                    @Html.Raw("</td>")

                    @If sportShiftFlg = "True" Then
                        @<td>
                        @Html.Hidden(strKey & "COLNM", Model.D0020(i).COLNM)
                        <select class="form-control input-sm selectBoxAnaCol" onchange="AnnaColNmSelect(this, '@(strId & "COLNM")')">
                            @If lsM0150AnaCol IsNot Nothing Then

                                @For Each item In lsM0150AnaCol
                                    If item.COLNAME = Model.D0020(i).COLNM                                        @<option value="@item.COLNAME" selected>@item.COLVALUE</option>
                                    Else
                                        @<option value="@item.COLNAME">@item.COLVALUE</option>
                                    End If
                                Next
                            End If
                        </select>
                        @Html.ValidationMessage(strKey & "COLNM", New With {.class = "text-danger"})
                    </td>
                    End If
                    
                    <td>
                        <a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>
                    </td>
                </tr>
                    Next
                End If
            </table>

            @*@If KarianacatList IsNot Nothing Then
                        @<datalist id="Karianacat">
                            @For Each item In KarianacatList
                                    @<option>@Html.Encode(item.ANNACATNM) </option>
                            Next
                </datalist>
            End If*@

            @*<table class="table table-bordered" id="catTable">
                <tr>
                    <th colspan="2" >
                仮アナカテゴリー
                    </th>
                </tr>
                @If Model IsNot Nothing AndAlso Model.D0021 IsNot Nothing AndAlso Model.D0021.Count > 0 Then
                @For i = 0 To Model.D0021.Count - 1
                strKey = String.Format("D0021[{0}].", i)
                @<tr>
                    <td>
                        @Html.TextBox(strKey & "ANNACATNM", Model.D0021(i).ANNACATNM, htmlAttributes:=New With {.class = "form-control input-sm", .list = "Karianacat"})
                        @Html.Hidden(strKey & "SEQ", Model.D0021(i).SEQ)
                    </td>
                    <td>
                        <a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>
                    </td>
                </tr>
                    Next

                End If
            </table>*@

                <select id="KarianacatList" style="visibility:hidden;">
                    @If KarianacatList IsNot Nothing Then
                        @For Each item In KarianacatList
                            @<option>@item.ANNACATNM</option>
                        Next
                    End If
                </select>

                @*ASI[18 Dec 2019]: If Delete アナウンサー and then again add from 候補者一覧 list.*@
                                    @*so taken this Hidden Select control to provide DropDownList*@
                <select id="selectLsM0150AnaCol" style="visibility:hidden;">
                    @If lsM0150AnaCol IsNot Nothing Then
                        @For Each item In lsM0150AnaCol
                            @<option value="@item.COLNAME">@item.COLVALUE</option>
                        Next
                    End If
                </select>
               
            <table class="table table-bordered" id="catTable">
                <tr>
                    <th colspan="3" >
                        仮アナカテゴリー
                    </th>
                </tr>
                @If Model IsNot Nothing AndAlso Model.D0021 IsNot Nothing AndAlso Model.D0021.Count > 0 Then
                    @For i = 0 To Model.D0021.Count - 1
                        strKey = String.Format("D0021[{0}].", i)
                        strId = String.Format("D0021_{0}__", i)
                        @<tr>
                            <td>
                                @Html.TextBox(strKey & "ANNACATNM", Model.D0021(i).ANNACATNM, htmlAttributes:=New With {.class = "form-control input-sm inputBoxAna"})
                                @Html.Hidden(strKey & "SEQ", Model.D0021(i).SEQ)
                                <select class="form-control input-sm selectBoxAna" onchange="AnnacatSelect(this, '@(strId & "ANNACATNM")')">
                                    @If KarianacatList IsNot Nothing Then
                                        @For Each item In KarianacatList
                                            @<option>@item.ANNACATNM</option>
                                        Next
                                            End If
                                </select>
                                @Html.ValidationMessage(strKey & "ANNACATNM", New With {.class = "text-danger"})
                            </td>
                             @If sportShiftFlg = "True"  Then
                                @<td>
                                    @Html.Hidden(strKey & "COLNM", Model.D0021(i).COLNM)
                                    <select class="form-control input-sm selectBoxAnaCol" onchange="AnnaColNmSelect(this, '@(strId & "COLNM")')">
                                        @If lsM0150AnaCol IsNot Nothing Then
                            
                                            @For Each item In lsM0150AnaCol
                                                If item.COLNAME = Model.D0021(i).COLNM
                                                    @<option value="@item.COLNAME" selected>@item.COLVALUE</option>
                                                Else
                                                    @<option value="@item.COLNAME">@item.COLVALUE</option>
                                                End If
                                            Next
                                        End If
                                    </select>
                                    @Html.ValidationMessage(strKey & "COLNM", New With {.class = "text-danger"})
                                </td>
                            End If
                            <td>
                                <a href="#" id="del_btn_ana" class="btn btn-danger btn-xs">削除</a>
                            </td>
                        </tr>
                    Next
                End If
            </table>

            <table id="tblRefAnalist" class="table table-bordered">
                <tr>
                    <th colspan="2" style="background-color:#d2f4fa">
                        アナウンサー(呼出済)
                    </th>
                </tr>
                @If Model IsNot Nothing AndAlso Model.RefAnalist IsNot Nothing Then
                    @For i = 0 To Model.RefAnalist.Count - 1

                        strKey = String.Format("RefAnalist[{0}]", i)
                        strKey1 = String.Format("RefCatAnalist[{0}]", i)
                        @<tr>
                        <td>
                            @Model.RefAnalist(i)
                            @Html.Hidden(strKey, Model.RefAnalist(i))
                        </td>
                        @if sportShiftFlg = "True" AndAlso Model.RefCatAnalist IsNot Nothing Then
                            @<td>
                                @Model.RefCatAnalist(i)
                                @Html.Hidden(strKey1, Model.RefCatAnalist(i))
                            </td>
End If
                    </tr>
Next
                End If

            </table>

            <table id="tblRefKariAnalist" class="table table-bordered">
                <tr>
                    <th colspan="2" style="background-color:#d2f4fa">
                        仮アナカテゴリー(呼出済)
                    </th>
                </tr>
                @If Model IsNot Nothing AndAlso Model.RefKariAnalist IsNot Nothing Then
                    @For i = 0 To Model.RefKariAnalist.Count - 1

                        strKey = String.Format("RefKariAnalist[{0}]", i)
                        strKey1 = String.Format("RefCatKariAnalist[{0}]", i)

                        @<tr>
                        <td>
                            @Model.RefKariAnalist(i)
                            @Html.Hidden(strKey, Model.RefKariAnalist(i))
                        </td>
                        @if sportShiftFlg = "True" Then
                            @<td>
                                @Model.RefCatKariAnalist(i)
                                @Html.Hidden(strKey1, Model.RefCatKariAnalist(i))
                            </td>
End If
                    </tr>
Next
                End If
            </table>
        </div>

        <div class="col-sm-4" style="padding-left:50px;">
            <h4 style="margin-top:-1px;"><b>候補者一覧</b></h4>
            <font style="background-color:#@M0060Kyukde.BACKCOLOR; color:#@M0060Kyukde.FONTCOLOR; border: 1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>&nbsp;&nbsp;&nbsp;公休出　
            <font style="background-color:#@M0060KyukHouDe.BACKCOLOR; color:#@M0060KyukHouDe.FONTCOLOR; border: 1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>&nbsp;&nbsp;&nbsp;法休出　
            <font style="background-color: yellow;">※</font>&nbsp;&nbsp;&nbsp;OT45h超え
            <div>
                <font style="color: red;">赤</font>&nbsp;&nbsp;&nbsp;OT+休日100h超え&nbsp;/&nbsp;総240h超え&nbsp;/&nbsp;月間休日4日以下
            </div>
            <div id="divKoho">
                @If ViewBag.Showkoho = True Then
                    @Html.Partial("_D0010_AnalistPartial")
                End If
            </div>
            <label style="font-size:15px;color:orange;padding-top:10px;visibility:hidden;" id="lblInfo">処理中です。しばらくお待ち下さい。。。</label>
        </div>
    </div>

    @Html.Partial("_MsgDialog")

    @Html.Partial("_CreateShitaHinaDialog")

End Using

<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>

<script type="text/javascript" src="~/Scripts/B0020-6.js"></script>

<script>

    var Form_name = '@Form_name';
    var ResetYoinUserAjax_url= '@Url.Action("ResetYoinUserData", "B0020")';

    $(document).ready(function () {

        //ASI[06 Dec 2019]: ReadOnly both cd  when screen load
        $('#SPORTCATCDDD').prop('disabled', true)
        $('#SPORTSUBCATCDDD').prop('disabled', true)

        $('#GYOMYMD').prop('disabled', true)
        $('#GYOMYMDED').prop('disabled', true)
        $('#KSKJKNST').prop('disabled', true)
        $('#KSKJKNED').prop('disabled', true)

        var gyomymd = $('#GYOMYMD').val().substr(0,10);
        $('#GYOMYMD').val(gyomymd);

        if (Form_name == 'A0240' || Form_name == 'A0230') {
            $('#KSKJKNST').prop('disabled', false)
            $('#KSKJKNED').prop('disabled', false)
        }

        var gyomymded = $('#GYOMYMDED').val().substr(0, 10);
        $('#GYOMYMDED').val(gyomymded);

        //修正モードで画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
        //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
        $("#myForm :input").change(function () {
            $("#myForm").data("MSG", true);

        });
    });

    //仮アナカテゴリーリストで選択した時
    function AnnacatSelect(select, inputid) {
        var input = document.getElementById(inputid);
        var val = select.value;
        input.value = val;
    }

    //ASI[12 Dec 2019]
    function AnnaColNmSelect(select, inputid) {
        var input = document.getElementById(inputid);
        var val = select.value;
        input.value = val;
    }

    //番組リストで選択した時
    $("#selectBoxBangumi").change(function () {
        var val = this.value
        $('#BANGUMINM').val(val)
    });

    //内容リストで選択した時
    $("#selectBoxNaiyo").change(function () {
        var val = this.value
        $('#NAIYO').val(val)
    });

     $('#btnUpdate').on('click', function (e) {

         //ASI[06 Dec 2019]: Editable both cd for send to server side
         //$('#SPORTCATCDDD').prop('disabled', false)
         //$('#SPORTSUBCATCDDD').prop('disabled', false)

         $('#FMTKBN').val(0);

        var result = confirm("更新します。よろしいですか？");

        if (result == false) {
            return false;
        }
    });
     //alert('@sportShiftFlg');

     var sportShiftFlg = '@sportShiftFlg';
     /*ASI[06 Dec 2019] : If SPORTFLG TRUE then Display SPORTCATCD & SPORTSUBCATCD & FURI otherwise Do Not */
     if ('@sportShiftFlg' == 'True') {
         $('#sportCatCd_SubCatCdDIV').show();
         $('#furiDIV').show();
         $('#SAIJKNDIV').show();
     }
     else {
         $('#sportCatCd_SubCatCdDIV').hide();
         $('#furiDIV').hide();
         $('#SAIJKNDIV').hide();
     }

</script>