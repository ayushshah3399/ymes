@ModelType IEnumerable(Of NTV_SHIFT.M0090)
@Code
    ViewData("Title") = "業務一括登録"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim CataList = DirectCast(ViewBag.CataList, List(Of M0020))
    Dim UserList = DirectCast(ViewBag.UserList, List(Of M0010))
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
    Dim strPTN As String = ""
End Code

@*<h2>Index</h2>*@

<style>
    /*.table-bordered th {
        white-space: nowrap;
    }

    .table-bordered td {
        /*width:1%;*/
    /*min-width:100px;*/
    /*white-space: nowrap;
    }*/


    .table-scroll {
        width: 2155px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 360px;
        width: 2155px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .colLink {
        width: 50px;
    }

    .colGYOMYMD {
        width: 170px;
    }

    .colGYOMYMD1, .colGYOMYMD2 {
        width: 85px;
    }

    .colKSKJKNST {
        width: 96px;
    }

    .colKSKJKNST1, .colKSKJKNST2 {
        width: 45px;
    }

    .colAna {
        width: 150px;
    }

    .colKariAna {
        width: 140px;
    }


    .colOAJKNST {
        width: 100px;
    }

    .colOAJKNST1, .colOAJKNST2 {
        width: 50px;
    }


    .colCATNM {
        width: 110px;
        max-width: 110px;
    }


    .colPTNFLG {
        width: 90px;
    }


    .colBANGUMINM, .colNAIYO {
        width: 150px;
        max-width: 150px;
    }

    .colBASYO {
        width: 150px;
        max-width: 150px;
    }

    .colBANGUMITANTO {
        width: 130px;
        max-width: 130px;
    }

    .colUSERID {
        width: 120px;
    }

    .colBANGUMITANTO {
        width: 120px;
        max-width: 120px;
    }

    .colBANGUMIRENRK {
        width: 120px;
        max-width: 120px;
    }

    .colBIKO {
        width: 150px;
        max-width: 150px;
    }

    .colUPDTID {
        width: 150px;
        max-width: 200px;
    }


    .colTaisho {
        width: 100px;
    }

    .colChkDel {
        width: 90px;
    }

    .table-scroll td {
        word-wrap: break-word;
    }

    #Banguminm {
        font-size: 14px;
        width: 200px;
        position: absolute;
    }

    #selectBoxBangumi {
        font-size: 14px;
        width: 225px;
    }

       #Naiyo {
        font-size: 14px;
        width: 200px;
        position: absolute;
    }

     #selectBoxNaiyo {
        font-size: 14px;
        width: 225px;
    }

    body {
        font-size:12px;
    }

    #PtnflgNo, #PtnflgYes, #Ptn1, #Ptn2, #Ptn3, #Ptn4, #Ptn5, #Ptn6, #Ptn7, #WeekA, #WeekB, #PtnAna2, #TaishoYes, #TaishoNo {
        margin-top: 1px;
    }

</style>





<div>
    @Html.Partial("_MenuPartial", "7")
</div>

@Using Html.BeginForm("Index", "A0170", FormMethod.Get)

   
    @<p></p>

    @<div class="row">

        <div class="col-md-5">
            @*<h3>業務検索</h3>*@

            <p>
                @Html.ActionLink("新規登録", "Create")
                |  <button id="EnDisCondition" type="button" class="btn btn-info btn-sm">検索条件表示/非表示</button>
                |<button id="btnSearch" type="submit" class="btn btn-default btn-sm">検索</button>

            </p>
        </div>

        <div class="col-md-6 col-md-pull-1">
            <p style="padding-top:20px;font-size:21px;font-weight:bold">検索画面</p>
        </div>
    </div>


    @*内容: @Html.TextBox("SearchString") <br />*@

    @*メモ: @Html.TextBox("SearchString") <br />
        <input type="submit" value="検索" />*@

    @<div id="conditionrow" class="row">

        <div class="form-horizontal">

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">業務期間</label>
                <div class="col-md-5">
                    @Html.TextBox("Gyost", Nothing, New With {.class = "form-control input-sm datepicker imedisabled"})
                    ～
                    @Html.TextBox("Gyoend", Nothing, New With {.class = "form-control input-sm datepicker imedisabled"})
                    <div id="ErrorGyost" style="color:red"></div>
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">バターン設定</label>
                <div class="col-md-10" style="margin-bottom:5px;">

                    <label class="checkbox-inline">
                        @Html.CheckBox("PtnflgNo")  繰り返し登録なし
                    </label>

                    <label class="checkbox-inline">
                        @Html.CheckBox("PtnflgYes")  繰り返し登録あり
                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("Ptn1")  月曜

                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("Ptn2")  火曜

                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("Ptn3")  水曜

                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("Ptn4")  木曜

                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("Ptn5")  金曜

                    </label>
                    <label class="checkbox-inline">

                        @Html.CheckBox("Ptn6") 土曜
                    </label>
                    <label class="checkbox-inline">

                        @Html.CheckBox("Ptn7") 日曜
                    </label>

                    @*ASI[21 Oct 2019]: Added below 2 checkBox A週 & B週.*@
                    <label class="checkbox-inline">
                        @Html.checkBox("WeekA") A週
                    </label>

                    <label class="checkbox-inline">
                        @Html.checkBox("WeekB") B週
                    </label>


                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">拘束時間</label>
                <div class="col-md-5">
                    @Html.TextBox("Kskjknst", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    ～
                    @Html.TextBox("Kskjkned", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    <div id="ErrorKskjknst" style="color:red"></div>
                </div>


            </div>
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">カテゴリー</label>
                <div class="col-md-5">
                    @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                </div>
            </div>


        </div>





        <div class="form-horizontal">

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">番組名</label>
                <div class="col-md-5">
                    @Html.TextBox("Banguminm", Nothing, New With {.class = "form-control input-sm"})
                    <select class="form-control input-sm selectBox" id="selectBoxBangumi">
                        @If Bangumilst IsNot Nothing Then
                            @For Each item In Bangumilst
                                @<option>@item.BANGUMINM </option>
                            Next
                             End If
                    </select>
                </div>
            </div>


            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">OA時間</label>
                <div class="col-md-5">
                    @Html.TextBox("Oajknst", Nothing, New With {.class = "form-control input-sm time imedisabled"})

                    ～
                    @Html.TextBox("Oajkned", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    <div id="ErrorOAJKNST" style="color:red"></div>
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label"> 内容</label>
                <div class="col-md-5">
                    @Html.TextBox("Naiyo", Nothing, New With {.class = "form-control input-sm"})
                    <select class="form-control input-sm selectBox" id="selectBoxNaiyo">
                        @If NaiyouList IsNot Nothing Then
                            @For Each item In NaiyouList
                                @<option>@item.NAIYO </option>
                            Next
                             End If
                    </select>
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label"> 場所</label>
                <div class="col-md-5">
                    @Html.TextBox("Basyo", Nothing, New With {.class = "form-control input-sm"})

                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">アナウンサー</label>
                <div class="col-md-3">
                    @Html.DropDownList("ANAID", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-10" style="margin-bottom:5px;">
                    <label class="checkbox-inline">
                        @Html.CheckBox("PtnAna2")  仮アナ
                    </label>
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">番組担当者</label>
                <div class="col-md-5">
                    @Html.TextBox("Bangumitanto", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">連絡先</label>
                <div class="col-md-5">
                    @Html.TextBox("Renraku", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">備考</label>
                <div class="col-md-5">
                    @Html.TextBox("Biko", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">最終更新者</label>
                <div class="col-md-5">
                    @Html.DropDownList("USERID", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})

                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label"> 一括登録対象</label>
                <div class="col-md-10">

                    <label class="checkbox-inline">
                        @Html.CheckBox("TaishoNo")  対象外
                    </label>

                    <label class="checkbox-inline">
                        @Html.CheckBox("TaishoYes")  対象
                    </label>

                </div>
            </div>

        </div>

    </div>

End Using
@Using (Html.BeginForm())

    @<p>
        @*@Html.ActionLink("CSV出力", "DownloadCsv", "B0020", Nothing, htmlAttributes:=New With {.name = "DownloadCsv", .class = "btn btn-success btn-sm"})*@
        <button id="btnDownloadCsv" type="submit" class="btn btn-success btn-sm" name="button" value="downloadcsv">CSV出力</button>
    </p>

@<p></p>
    @<table class="tablecontainer">
        <tr>
            <td>
                <table id="tblSearchResult" class="table table-bordered table-hover table-scroll">
                    <thead>
                        <tr>

                            <th class="colLink">

                            </th>
                            <th class="colLink">

                            </th>
                            <th style="padding-top:6px; padding-bottom:6px;" class="colChkDel">
                                <button id="btnDelete" type="submit" name="button" value="delete" class="btn btn-danger btn-xs">一括削除</button>
                            </th>
                            <th class="colGYOMYMD">
                                業務期間
                            </th>

                            <th class="colPTNFLG">
                                @Html.DisplayNameFor(Function(model) model.PTNFLG)
                            </th>
                            <th class="colKSKJKNST">
                                @*@Html.DisplayNameFor(Function(model) model.KSKJKNST)*@
                                拘束時間
                            </th>

                            <th class="colCATNM">
                                @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
                            </th>
                            <th class="colBANGUMINM">
                                @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                            </th>
                            <th class="colOAJKNST">
                                @*@Html.DisplayNameFor(Function(model) model.OAJKNST)*@
                                OA時間
                            </th>

                            <th class="colNAIYO">
                                @Html.DisplayNameFor(Function(model) model.NAIYO)
                            </th>
                            <th class="colBASYO">
                                @Html.DisplayNameFor(Function(model) model.BASYO)
                            </th>
                            @*<th class="colUSERID">
                                    @Html.DisplayNameFor(Function(model) model.USERID)
                                </th>*@
                            <th class="colAna">
                                アナウンサー
                            </th>
                            <th class="colKariAna">
                                仮アナカテゴリー
                            </th>
                            <th class="colBANGUMITANTO">
                                @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                            </th>
                            <th class="colBANGUMIRENRK">
                                @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                            </th>

                            <th class="colBIKO">
                                @Html.DisplayNameFor(Function(model) model.BIKO)
                            </th>
                            <th class="colUPDTID">
                                @Html.DisplayNameFor(Function(model) model.UPDTID)
                            </th>
                            <th class="colTaisho">
                                一括登録対象
                            </th>
                           

                        </tr>

                    </thead>

                    <tbody>

                        @If Model IsNot Nothing Then

                            @For i As Integer = 0 To Model.Count - 1
                                    Dim key As String = String.Format("lstM0090[{0}].", i)
                                    Dim item = Model(i)

                                @Html.Hidden(key + "IKKATUNO", item.IKKATUNO)

                                @<tr>
                                    <td class="colLink">
                                        @Html.ActionLink("修正", "Edit", New With {.id = item.IKKATUNO})
                                    </td>
                                     <td class="colLink">
                                         @*@Html.ActionLink("修正", "Edit", New With {.id = item.IKKATUNO}) |*@
                                         @* @Html.ActionLink("参照", "Details", New With {.id = item.IKKATUNO}) |*@
                                         @Html.ActionLink("削除", "Delete", New With {.id = item.IKKATUNO})
                                     </td>
                                     <td align="center" class="colChkDel">
                                         @Html.CheckBox(key + "FLGDEL", item.FLGDEL)
                                     </td>

                                    <td class="colGYOMYMD1">
                                        @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
                                        @Html.Hidden(key + "GYOMYMD", item.GYOMYMD)
                                    </td>
                                    <td class="colGYOMYMD2">

                                        @If item.GYOMYMD <> item.GYOMYMDED Then
                                            @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
                                            @Html.Hidden(key + "GYOMYMDED", item.GYOMYMDED)
                                        End If
                                    </td>
                                    <td class="colPTNFLG">
                                        @If item.PTNFLG = False Then
                                            @Html.DisplayFor(Function(modelItem) item.PTNFLG)
                                        Else

                                            @*@strPTN = Html.DisplayFor(Function(modelItem) item.PTN1)*@
                                            @Html.DisplayFor(Function(modelItem) item.PTN1)

                                            @If item.PTN1 = True And item.PTN2 = True Then
                                                @<label>,</label>
                                            End If

                                            @Html.DisplayFor(Function(modelItem) item.PTN2)

                                            @If (item.PTN1 = True Or item.PTN2 = True) And item.PTN3 = True Then
                                                @<label>,</label>
                                            End If
                                            @Html.DisplayFor(Function(modelItem) item.PTN3)

                                            @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True) And item.PTN4 = True Then
                                                @<label>,</label>
                                            End If

                                            @Html.DisplayFor(Function(modelItem) item.PTN4)

                                            @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True) And item.PTN5 = True Then
                                                @<label>,</label>
                                            End If
                                            @Html.DisplayFor(Function(modelItem) item.PTN5)

                                            @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True) And item.PTN6 = True Then
                                                @<label>,</label>
                                            End If
                                            @Html.DisplayFor(Function(modelItem) item.PTN6)

                                            @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True Or item.PTN6 = True) And item.PTN7 = True Then
                                                @<label>,</label>
                                            End If
                                            @Html.DisplayFor(Function(modelItem) item.PTN7)

                                            @*ASI[21 Oct 2019]:dispaly WeekA or WeekB in grid*@
                                            @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True Or item.PTN6 = True Or item.PTN7 = True) And item.WEEKA = True Then
	                                            @<label>,</label>
                                            End If
                                            @Html.DisplayFor(Function(modelItem) item.WEEKA)

                                            @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True Or item.PTN6 = True Or item.PTN7 = True Or item.WEEKA = True) And item.WEEKB = True Then
	                                            @<label>,</label>
                                            End If
                                            @Html.DisplayFor(Function(modelItem) item.WEEKB)

                                        End If
                                    </td>
                                     @Html.Hidden(key + "PTNFLG", item.PTNFLG)
                                     @Html.Hidden(key + "PTN1", item.PTN1)
                                     @Html.Hidden(key + "PTN2", item.PTN2)
                                     @Html.Hidden(key + "PTN3", item.PTN3)
                                     @Html.Hidden(key + "PTN4", item.PTN4)
                                     @Html.Hidden(key + "PTN5", item.PTN5)
                                     @Html.Hidden(key + "PTN6", item.PTN6)
                                     @Html.Hidden(key + "PTN7", item.PTN7)

                                     @*ASI[21 Oct 2019]:put WeekA and WeekB in hidden*@
                                     @Html.Hidden(key + "WEEKA", item.WEEKA)
                                     @Html.Hidden(key + "WEEKB", item.WEEKB)
                                    
                                    <td class="colKSKJKNST1">
                                        @*@Html.DisplayFor(Function(modelItem) item.KSKJKNST)*@
                                        @Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(2, 2)
                                        @Html.Hidden(key + "KSKJKNST", item.KSKJKNST)

                                    </td>
                                    <td class="colKSKJKNST2">
                                        @*@Html.DisplayFor(Function(modelItem) item.KSKJKNED)*@
                                        @Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(2, 2)

                                        @Html.Hidden(key + "KSKJKNED", item.KSKJKNED)
                                    </td>
                                    <td class="colCATNM">
                                        @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                                        @Html.Hidden(key + "M0020.CATNM", item.M0020.CATNM)
                                    </td>
                                    <td class="colBANGUMINM">
                                        @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                        @Html.Hidden(key + "BANGUMINM", item.BANGUMINM)
                                    </td>

                                    @*@Html.DisplayFor(Function(modelItem) item.OAJKNST)*@
                                    @If item.OAJKNST IsNot Nothing Then
                                        @<td class="colOAJKNST1">
                                            @Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(2, 2)

                                        </td>
                                    Else
                                        @<td class="colOAJKNST1"></td>
                                    End If
                                    @Html.Hidden(key + "OAJKNST", item.OAJKNST)

                                    @If item.OAJKNED IsNot Nothing Then
                                        @<td class="colOAJKNST2">
                                            @Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(2, 2)

                                        </td>
                                    Else
                                        @<td class="colOAJKNST2"></td>
                                    End If
                                    @Html.Hidden(key + "OAJKNED", item.OAJKNED)



                                    <td class="colNAIYO">
                                        @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                        @Html.Hidden(key + "NAIYO", item.NAIYO)

                                    </td>

                                    <td class="colBASYO">
                                        @Html.DisplayFor(Function(modelItem) item.BASYO)
                                        @Html.Hidden(key + "BASYO", item.BASYO)

                                    </td>
                                    @*<td class="colUSERID">
                                            @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                                        </td>*@

                                    <td class="colAna">
                                        @For j = 0 To item.M0110.Count - 1
                                                Dim k = j


                                                If k > 0 Then
                                            @Html.Encode("，")
                                        End If

                                            @Html.DisplayFor(Function(modelItem) item.M0110(k).M0010.USERNM)
                                            Dim keyM0110 As String = String.Format("M0110[{0}].", k)
                                            @Html.Hidden(key + keyM0110 + "M0010.USERNM", item.M0110(k).M0010.USERNM)
                                        Next

                                    </td>

                                    <td class="colKariAna">
                                        @If item.M0120 IsNot Nothing Then
                                            @For j = 0 To item.M0120.Count - 1
                                                    Dim k = j



                                                    If k > 0 Then
                                                @Html.Encode("，")
                                            End If

                                                @Html.DisplayFor(Function(modelItem) item.M0120(k).ANNACATNM)
                                                Dim keM0120 As String = String.Format("M0120[{0}].", k)
                                                @Html.Hidden(key + keM0120 + "ANNACATNM", item.M0120(k).ANNACATNM)
                                            Next
                                    End If


                                    </td>

                                    <td class="colBANGUMITANTO">
                                        @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                                        @Html.Hidden(key + "BANGUMITANTO", item.BANGUMITANTO)

                                    </td>
                                    <td class="colBANGUMIRENRK">
                                        @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
                                        @Html.Hidden(key + "BANGUMIRENRK", item.BANGUMIRENRK)

                                    </td>

                                    <td class="colBIKO">
                                        @Html.DisplayFor(Function(modelItem) item.BIKO)
                                        @Html.Hidden(key + "BIKO", item.BIKO)

                                    </td>
                                    <td class="colUPDTID">
                                        @Html.DisplayFor(Function(modelItem) item.UPDTID)
                                        @Html.Hidden(key + "UPDTID", item.UPDTID)

                                    </td>

                                    <td class="colTaisho">
                                        @Html.DisplayFor(Function(modelItem) item.IKTTAISHO)
                                        @Html.Hidden(key + "IKTTAISHO", item.IKTTAISHO)

                                    </td>
                                    
                                </tr>
                            Next
           End If

                    </tbody>


                </table>

            </td>
        </tr>

    </table>

End Using




<script type="text/javascript">

    $(document).ready(function () {

        var table = document.getElementById("tblSearchResult");
        var rows = table.getElementsByTagName("tr");
        if (rows.length > 1) {
            $("#conditionrow").hide();
        }
    });


    $('#btnDelete').on('click', function (e) {

        var len = $("#tblSearchResult tbody").children().length;
        if (len == 0) {
            return false
        }

        var tblSearchResult = document.getElementById("tblSearchResult");
        var rows = tblSearchResult.getElementsByTagName("tr");
        var chk = false;
        for (var i = 1; i < rows.length  ; i += 1) {
            chk = $('#tblSearchResult tr:eq(' + i + ') td.colChkDel').find('input:first').is(':checked')
            if (chk) {
                checked = chk;
                break;
            }
        }

        if (chk == false) {
            alert("一件も選択されていません。")
            return false
        }

        var result = confirm("選択されているデータを削除します。よろしいですか?")

        if (result == false) {
            return false
        }

    });

    $('#btnDownloadCsv').on('click', function (e) {

        var len = $("#tblSearchResult tbody").children().length;
        if (len == 0) {
            alert("対象データが一件も存在しません。検索を行ってください。");
            return false
        }

    });

    //$('#EnDisColMsgBox').on('click', function (e) {

    //    if ($("#conditoinrow").is(':hidden')) {
    //    //$("#ColMsgBox").removeClass("invisible");
    //    $("#conditoinrow").show();

    //    }
    //    else {
    //        //$("#ColMsgBox").last().addClass("invisible");
    //        $("#conditoinrow").hide();
    //    }
    //});

    //function rowinvisible() {

    //    $("#conditoinrow").hide();

    //}

    //$('#btnsearch').on('click', function (e) {


    //    //$("#ColMsgBox").last().addClass("invisible");
    //    $("#conditoinrow").hide();

    //});


    $("#selectBoxBangumi").change(function () {

        var val = this.value
        $('#Banguminm').val(val)

    });

    $("#selectBoxNaiyo").change(function () {

        var val = this.value
        $('#Naiyo').val(val)

    });


    $('#btnSearch').on('click', function (e) {
      
        var gyomymd = $('#Gyost').val();
        var gyomymded = $('#Gyoend').val();
        var errflg = '';

        if (gyomymd == '' && gyomymded != '') {
            $("#ErrorGyost").text("業務期間の終了日のみの指定はできません。 ");
            errflg = '1'
        }
        else if (gyomymd != '' && gyomymded != '' && gyomymd > gyomymded) {
            $("#ErrorGyost").text("業務期間-開始と終了の前後関係が誤っています。 ");
            errflg = '1'
        }
        else {
            $("#ErrorGyost").text("")
        }

        var kskjknst = $('#Kskjknst').val();
        var kskjkned = $('#Kskjkned').val();


        var OAJKNST = $('#Oajknst').val();
        var OAJKNED = $('#Oajkned').val();

        if (kskjknst == '' && kskjkned != '') {
            $("#ErrorKskjknst").text("拘束時間の終了時間のみの指定はできません。 ");
            errflg = '1'
        }
        else {
            $("#ErrorKskjknst").text("")
        }

        if (OAJKNST == '' && OAJKNED != '') {
            $("#ErrorOAJKNST").text("OA時間の終了時間のみの指定はできません。 ");
            errflg = '1'
        }
        else {
            $("#ErrorOAJKNST").text("")
        }

        if (errflg != '') {
            return false
        }

    });
</script>