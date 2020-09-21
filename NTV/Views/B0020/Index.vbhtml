@ModelType IEnumerable(Of NTV_SHIFT.D0010)
@Code
    ViewData("Title") = "業務検索"
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
End Code

<style>
    /*.table-bordered th {
        white-space: nowrap;
    }*/

    /*.table-bordered td {

        white-space: nowrap;
    }*/   

  
    

    .colLink {
        width: 50px;
        max-width: 50px;
    }

    .colGYOMYMD {
        width: 170px;
        max-width: 170px;
    }

    .colGYOMYMD1, .colGYOMYMD2 {
        width: 85px;
        max-width: 85px;
    }

    .colKSKJKNST {
        width: 100px;
        max-width: 100px;
    }

    .colKSKJKNST1, .colKSKJKNST2 {
        width: 50px;
        max-width: 50px;
    }

    .colOAJKNST, .colSAIJKNST {
        width: 100px;
        max-width: 100px;
    }

    .colOAJKNST1, .colOAJKNST2, .colSAIJKNST1, .colSAIJKNST2 {
        width: 50px;
        max-width: 50px;
    }

    .colCATNM {
        width: 110px;
        max-width: 110px;
        word-wrap: break-word;
    }

    .colSPORTCATCD {
        width: 120px;
        max-width: 120px;
        word-wrap: break-word;
    }

    .colSPORTSUBCATCD {
        width: 140px;
        max-width: 140px;
        word-wrap: break-word;
    }

    .colBANGUMINM, .colNAIYO {
        width: 150px;
        max-width: 150px;
        word-wrap: break-word;
    }

    .colBASYO {
        width: 150px;
        max-width: 150px;
        word-wrap: break-word;
    }

    .colBANGUMITANTO {
        width: 110px;
        max-width: 110px;
        word-wrap: break-word;
    }

    .colBANGUMIRENRK {
        width: 130px;
        max-width: 130px;
        word-wrap: break-word;
    }

    .colBIKO {
        width: 150px;
        max-width: 130px;
        word-wrap: break-word;
    }

    .colAna {
        width: 160px;
        max-width: 160px;
        word-wrap: break-word;
    }

    .colKariAna {
        width: 140px;
        max-width: 140px;
        word-wrap: break-word;
    }

    .colLink2 {
        width: 50px;
        max-width: 50px;
    }

    .colChkDel {
        width: 90px;
        max-width: 90px;
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
        font-size: 12px;
    }

    #PtnflgNo, #Ptn1, #Ptn2, #Ptn3, #Ptn4, #Ptn5, #Ptn6, #Ptn7, #PtnAnaflgNo, #PtnAna1, #PtnAna2 {
        margin-top: 1px;
    }

    .myHeaderDiv {
        max-width: 1890px;
        overflow-x: hidden;
        overflow-y: scroll;
        z-index: 1;
        padding-left: 0px;
        height:39px;
        max-height:39px;
    }

    .myDiv {
        max-width: 1890px;
        height: 400px;
        overflow-y: scroll;
        overflow-x: scroll;
        position: relative;
        padding-left: 0px;
    }

    #tblSearchResult.table {
        position: relative;
        border-collapse: collapse;
        margin-bottom: 0px;
    }
</style>

@*<div id="divMessage" class="text-warning" style="visibility:hidden">
        @Html.Hidden("message", TempData("message"))
    </div>*@

<div id="divWarning" class="text-warning" style="visibility:hidden">
    @Html.Hidden("warning", TempData("warning"))
</div>

@Using Html.BeginForm("Index", "B0020", FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})
    @<div class="row">
        <div class="col-md-1 col-md-push-11">
            <p>
                <ul class="nav nav-pills ">
                    @*<li><a href="#">印刷</a></li>
                        <li><a href="#">最新情報</a></li>*@
                    <li>@Html.ActionLink("戻る", "Index", "C0050")</li>
                </ul>
            </p>
        </div>

        <div class="col-md-4 col-md-pull-1">
            @*<h3>業務検索</h3>*@

            <p style="padding-top:20px;">
                @Html.ActionLink("新規登録", "Create")
                | <button id="EnDisCondition" type="button" class="btn btn-info btn-sm">検索条件表示/非表示</button>
                | <button id="btnSearch" type="submit" class="btn btn-default btn-sm" @ViewData("disabled")>検索</button>
            </p>
        </div>

        <div class="col-md-7 col-md-pull-2">
            <p style="padding-top:20px;font-size:21px;font-weight:bold">
                <label style="font-size:15px;text-align:left; color:@ViewData("color"); padding-right:20px;" id="lblInfo">処理中です。しばらくお待ち下さい。。。</label>
                検索画面
            </p>
        </div>
    </div>


    @<div id="conditionrow" class="row" style="margin-left:0px;margin-right:0px;">
        <div class="form-horizontal">
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">業務期間</label>
                <div class="col-md-10">
                    @Html.TextBox("Gyost", Nothing, New With {.class = "form-control input-sm datepicker imedisabled"})
                    ～
                    @Html.TextBox("Gyoend", Nothing, New With {.class = "form-control input-sm datepicker imedisabled"})
                    <div id="ErrorGyost" style="color:red"></div>
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">曜日</label>
                <div class="col-md-10" style="margin-bottom:5px;">
                    <label class="checkbox-inline">
                        @Html.CheckBox("PtnflgNo")  すべて
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
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">拘束時間</label>
                <div class="col-md-10">
                    @Html.TextBox("Kskjknst", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    ～
                    @Html.TextBox("Kskjkned", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    <div id="ErrorKskjknst" style="color:red"></div>
                </div>
            </div>
            <div class="form-group div_condition">
                <label class="col-md-2 control-label">番組名</label>
                <div class="col-md-10">
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

            <div class="form-group div_condition">
                <label class="col-md-2 control-label"> 内容</label>
                <div class="col-md-10">
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
                <label class="col-md-2 control-label">アナウンサー</label>
                <div class="col-md-10">
                    @Html.DropDownList("ANAID", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-10" style="margin-bottom:5px;">
                    <label class="checkbox-inline">
                        @Html.CheckBox("PtnAnaflgNo")  すべて
                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("PtnAna1")  担当割当て済み

                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("PtnAna2")  仮アナ
                    </label>
                </div>
            </div>
            <div class="form-group div_condition">
                <label class="col-md-2 control-label"> 場所</label>
                <div class="col-md-10">
                    @Html.TextBox("Basyo", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label">備考</label>
                <div class="col-md-10">
                    @Html.TextBox("Biko", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>
            <div class="form-group div_condition">
                <label class="col-md-2 control-label">番組担当者</label>
                <div class="col-md-10">
                    @Html.TextBox("Bangumitanto", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label">連続先</label>
                <div class="col-md-10">
                    @Html.TextBox("Bangumirenrk", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">カテゴリー</label>
                <div class="col-md-10">
                    @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                </div>
            </div>
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">スポーツカテゴリ</label>
                <div class="col-md-10">
                    @Html.DropDownList("SPORTCATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:220px;"})
                </div>
            </div>
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">スポーツサブカテゴリ</label>
                <div class="col-md-10">
                    @Html.DropDownList("SPORTSUBCATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:220px;"})
                </div>
            </div>
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">OA時間</label>
                <div class="col-md-10">
                    @Html.TextBox("OAJKNST", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    ～
                    @Html.TextBox("OAJKNED", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    <div id="ErrorOAJKNST" style="color:red"></div>
                </div>
            </div>
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">試合時間</label>
                <div class="col-md-10">
                    @Html.TextBox("SAIJKNST", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    ～
                    @Html.TextBox("SAIJKNED", Nothing, New With {.class = "form-control input-sm time imedisabled"})
                    <div id="ErrorSAIJKNST" style="color:red"></div>
                </div>
            </div>
        </div>
    </div>

End Using

@Using (Html.BeginForm())
    @<p>
        @*@Html.ActionLink("CSV出力", "DownloadCsv", "B0020", Nothing, htmlAttributes:=New With {.name = "DownloadCsv", .class = "btn btn-success btn-sm"})*@
        <button id="btnDownloadCsv" type="submit" style="margin:0 0 10.5px" class="btn btn-success btn-sm" name="button" value="downloadcsv" @ViewData("disabled")>CSV出力</button>
    </p>

    @<p></p>


    @<div Class="row" style="max-width: 1890px;padding-left:0px;padding-right:0px;margin:0 0 10.5px;border:1px solid #ecf0f1;">
        <div id="headerDiv" class="myHeaderDiv col-md-12">
            <table id="tbl-header" class="table table-bordered table-hover" style="margin-bottom:0px;table-layout:fixed;">
                <thead>
                    <tr>
                        <th Class="colLink"> </th>
                        <th Class="colLink"> </th>
                        <th Class="colLink2"> </th>
                        <th style="padding-top:6px; padding-bottom:6px;" Class="colChkDel">
                            <Button id="btnDelete" type="submit" name="button" value="delete" Class="btn btn-danger btn-xs">一括削除</Button>
                        </th>
                        <th colspan="2" Class="colGYOMYMD">
                            業務期間
                        </th>
                        <th colspan="2" Class="colKSKJKNST">
                            拘束時間
                        </th>
                        <th Class="colBANGUMINM">
                            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                        </th>
                        <th class="colNAIYO">
                            @Html.DisplayNameFor(Function(model) model.NAIYO)
                        </th>
                        <th class="colAna">
                            アナウンサー <font color="blue">確認済</font>
                        </th>
                        <th class="colKariAna">
                            仮アナカテゴリー
                        </th>
                        <th class="colBASYO">
                            @Html.DisplayNameFor(Function(model) model.BASYO)
                        </th>
                        <th class="colBIKO">
                            @Html.DisplayNameFor(Function(model) model.BIKO)
                        </th>
                        <th class="colBANGUMITANTO">
                            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                        </th>
                        <th class="colBANGUMIRENRK">
                            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                        </th>
                        <th class="colCATNM">
                            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
                        </th>
                        <th class="colSPORTCATCD">
                            @Html.DisplayNameFor(Function(model) model.SPORTCATCD)
                        </th>
                        <th class="colSPORTSUBCATCD">
                            @Html.DisplayNameFor(Function(model) model.SPORTSUBCATCD)
                        </th>
                        <th colspan="2" class="colOAJKNST">
                            OA時間
                        </th>
                        <th colspan="2" class="colSAIJKNST">
                            @Html.DisplayNameFor(Function(model) model.SAIJKNST)
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div id="dataDiv" class="myDiv col-sm-12">
            <table id="tblSearchResult" class="table table-bordered table-hover" style="margin-bottom:0px;table-layout:fixed;">
                <tbody id="myTableBody">
                    @If Model IsNot Nothing Then

                        @For i As Integer = 0 To Model.Count - 1
                            Dim key As String = String.Format("lstd0010[{0}].", i)
                            Dim item = Model(i)

                            Dim firstrowid As String = ""
                            If i = 0 OrElse i Mod 50 = 0 Then
                                firstrowid = "firstrow"
                            End If
                            @<tr class=@firstrowid>
                                <td class="colLink">
                                    @Html.Hidden(key + "GYOMNO", item.GYOMNO)
                                    @Html.ActionLink("修正", "Edit", New With {.id = item.GYOMNO})
                                </td>
                                <td class="colLink2">
                                    @Html.ActionLink("複写", "Copy", New With {.id = item.GYOMNO})
                                </td>
                                <td class="colLink">
                                    @Html.ActionLink("削除", "Delete", New With {.id = item.GYOMNO})
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
                                <td class="colKSKJKNST1">
                                    @Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(2, 2)
                                    @Html.Hidden(key + "KSKJKNST", item.KSKJKNST)
                                </td>
                                <td class="colKSKJKNST2">
                                    @Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(2, 2)
                                    @Html.Hidden(key + "KSKJKNED", item.KSKJKNED)
                                </td>
                                <td class="colBANGUMINM">
                                    @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                    @Html.Hidden(key + "BANGUMINM", item.BANGUMINM)
                                </td>

                                <td class="colNAIYO">
                                    @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                    @Html.Hidden(key + "NAIYO", item.NAIYO)
                                </td>
                                <td class="colAna">
                                    @For j = 0 To item.D0020.Count - 1
                                        Dim k = j

                                        Dim keyD0020 As String = String.Format("D0020[{0}].", k)
                                        @Html.Hidden(key + keyD0020 + "M0010.USERNM", item.D0020(k).M0010.USERNM)
If k > 0 Then
                                            @Html.Encode("，") End If

                                        If item.D0020(j).CHK = "1" Then
                                            @<font color="blue">
                                                @Html.DisplayFor(Function(modelItem) item.D0020(k).M0010.USERNM)
                                            </font> Else
                                            @Html.DisplayFor(Function(modelItem) item.D0020(k).M0010.USERNM)
End If


                                    Next
                                    @*@If item.PGYOMNO IsNot Nothing Then
                                             For Each pitem In Model
                                                If item.PGYOMNO = pitem.GYOMNO Then
                                                   For j = 0 To pitem.D0020.Count - 1
                                                       Dim k = j

                                                        If k > 0 Then
                                                            @Html.Encode(", ")
                                                        End If

                                                        If pitem.D0020(j).CHK = "1" Then
                                                            @<font color="blue">
                                                                @Html.DisplayFor(Function(modelItem) pitem.D0020(k).M0010.USERNM)
                                                            </font>
                                                        Else
                                                            @Html.DisplayFor(Function(modelItem) pitem.D0020(k).M0010.USERNM)
                                                        End If

                                                    Next
                                                    Exit For
                                                End If
                                            Next
                                        End If*@

                                </td>

                                <td class="colKariAna">
                                    @For j = 0 To item.D0021.Count - 1
                                        Dim k = j

                                        Dim keyD0021 As String = String.Format("D0021[{0}].", k)
                                        @Html.Hidden(key + keyD0021 + "ANNACATNM", item.D0021(k).ANNACATNM)
If k > 0 Then
                                            @Html.Encode("，") End If

                                        @Html.DisplayFor(Function(modelItem) item.D0021(k).ANNACATNM)
Next
                                </td>
                                <td class="colBASYO">
                                    @Html.DisplayFor(Function(modelItem) item.BASYO)
                                    @Html.Hidden(key + "BASYO", item.BASYO)
                                </td>
                                <td class="colBIKO">
                                    @Html.DisplayFor(Function(modelItem) item.BIKO)
                                    @Html.Hidden(key + "BIKO", item.BIKO)
                                </td>
                                <td class="colBANGUMITANTO">
                                    @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                                    @Html.Hidden(key + "BANGUMITANTO", item.BANGUMITANTO)
                                </td>
                                <td class="colBANGUMIRENRK">
                                    @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
                                    @Html.Hidden(key + "BANGUMIRENRK", item.BANGUMIRENRK)
                                </td>
                                <td class="colCATNM">
                                    @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                                    @Html.Hidden(key + "M0020.CATNM", item.M0020.CATNM)
                                </td>
                                @If item.M0130 IsNot Nothing Then
                                    @<td class="colSPORTCATCD">
                                        @Html.DisplayFor(Function(modelItem) item.M0130.SPORTCATNM)
                                        @Html.Hidden(key + "M0130.SPORTCATNM", item.M0130.SPORTCATNM)
                                    </td> Else
                                    @<td class="colSPORTCATCD"></td>
                                End if
                                @If item.M0140 IsNot Nothing Then
                                    @<td class="colSPORTSUBCATCD">
                                        @Html.DisplayFor(Function(modelItem) item.M0140.SPORTSUBCATNM)
                                        @Html.Hidden(key + "M0140.SPORTSUBCATNM", item.M0140.SPORTSUBCATNM)
                                    </td> Else
                                    @<td class="colSPORTSUBCATCD"></td>
                                End If

                                @If item.OAJKNST IsNot Nothing Then
                                    @<td class="colOAJKNST1">
                                        @Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(2, 2)

                                    </td> Else
                                    @<td class="colOAJKNST1"></td>
                                End If
                                @Html.Hidden(key + "OAJKNST", item.OAJKNST)

                                @If item.OAJKNED IsNot Nothing Then
                                    @<td class="colOAJKNST2">
                                        @Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(2, 2)

                                    </td> Else
                                    @<td class="colOAJKNST2"></td>
                                End If
                                @Html.Hidden(key + "OAJKNED", item.OAJKNED)

                                @If item.SAIJKNST IsNot Nothing Then
                                    @<td class="colSAIJKNST1">
                                        @Html.DisplayFor(Function(modelItem) item.SAIJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.SAIJKNST).ToString.Substring(2, 2)

                                    </td> Else
                                    @<td class="colSAIJKNST1"></td>
                                End If
                                @Html.Hidden(key + "SAIJKNST", item.SAIJKNST)

                                @If item.SAIJKNED IsNot Nothing Then
                                    @<td class="colSAIJKNST2">
                                        @Html.DisplayFor(Function(modelItem) item.SAIJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.SAIJKNED).ToString.Substring(2, 2)

                                    </td> Else
                                    @<td class="colSAIJKNST2"></td>
                                End If
                                @Html.Hidden(key + "SAIJKNED", item.SAIJKNED)


                            </tr>Next

                    End If

                </tbody>
            </table>
        </div>
    </div>
      
End Using

        <div class="container">
            <div class="row" style="margin-top:-20px;">
                <div class="col-md-12  col-md-pull-1 text-center">
                    <ul class="pagination" id="myPager">
                        @*<li><a href="#" id="NoOne" style="visibility:visible">1</a></li>*@
                    </ul>
                </div>

            </div>
        </div>



        <script type="text/javascript">

    $("#SPORTCATCD").change(function () {
        var val = this.value
        $('#SPORTCATCD').val(val)

        /*Remove Options from SportSubCatCd before fetching new list corresponding to selected SportCatCd*/
        $("#SPORTSUBCATCD").find('option').remove();

        /*Write AJAX call to fetch list of SportSubCatCd corresponding to selected SportCatCd*/
        $.ajax({
            url: "@Url.Action("getSportSubCatCdList", "B0020")",
            async: false,
            type: "POST",
            data: { screenSportCatCd: val },
            dataType: 'json',
            cache: false,
            success: function (node) {
                if (node.success) {
                    if (node.sportSubCatCdList !== undefined) {
                        /*As List not blank, set this list of SportSubCatCd to dropdown*/
                        $("#SPORTSUBCATCD").append("<option></option>");
                        $.each(node.sportSubCatCdList, function () {
                            $("#SPORTSUBCATCD").append(
                                $('<option/>', {
                                    value: this.SPORTSUBCATCD,
                                    text: this.SPORTSUBCATNM
                                })
                            );
                        });

                    }
                }
            },
            error: function (node) {
                alert(node.responseText);
            }
        });

    });

    $("#SPORTSUBCATCD").change(function () {
        var val = this.value
        $('#SPORTSUBCATCD').val(val)
    });


    $(document).ready(function () {

        var table = document.getElementById("tblSearchResult");
        var rows = table.getElementsByTagName("tr");
        if (rows.length > 1) {
            $("#conditionrow").hide();
        }

        //var msg = jQuery.trim($('#message').val());
        //if (msg.length > 0) {
        //    alert(msg);
        //}

        var msgwarning = jQuery.trim($('#warning').val());

        if (msgwarning.length > 0) {
            var result = confirm(msgwarning);
            if (result == true) {

                var myurl = document.location.href;
                if (myurl.indexOf('?') == -1) {
                    myurl = myurl + '?';
                }
                else {
                    myurl = myurl + '&';
                }
                document.location.href = myurl + "confirmmsg=1"
            }
        }

        //ページ分けて表示
        $('#myTableBody').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 50 });

    });

    $('#EnDisColMsgBox').on('click', function (e) {

        if ($("#conditoinrow").is(':hidden')) {
            //$("#ColMsgBox").removeClass("invisible");
            $("#conditoinrow").show();

        }
        else {
            //$("#ColMsgBox").last().addClass("invisible");
            $("#conditoinrow").hide();
        }
    });

    $(document).on('click', '#btnSearch', function (e) {


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


        var OAJKNST = $('#OAJKNST').val();
        var OAJKNED = $('#OAJKNED').val();

        var SAIJKNST = $('#SAIJKNST').val();
        var SAIJKNED = $('#SAIJKNED').val();

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

        if (SAIJKNST == '' && SAIJKNED != '') {
            $("#ErrorSAIJKNST").text("試合時間の終了時間のみの指定はできません。 ");
            errflg = '1'
        }
        else {
            $("#ErrorSAIJKNST").text("")
        }

        if (errflg != '') {
            return false
        }

        $("#lblInfo").text("処理中です。しばらくお待ち下さい。。。")
        document.getElementById('lblInfo').style.color = 'orange';
        $('#btnSearch').attr("disabled", "disabled");
        $('#btnDownloadCsv').attr("disabled", "disabled");
        setTimeout(function () {
            $("#myForm").submit();
        }, 100);
    });

    $('#btnDownloadCsv').on('click', function (e) {

        var len = $("#tblSearchResult tbody").children().length;
        if (len == 0) {
            alert("対象データが一件も存在しません。検索を行ってください。");
            return false
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
        for (var i = 0; i < rows.length  ; i += 1) {
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

        var result = confirm("選択されているデータを削除します。よろしいですか?\n\n※担当アナへの連絡メールは送信されません。")

        if (result == false) {
            return false
        }

    });

    $('#PtnflgNo').on('click', function (e) {

        $("#Ptn1").prop("checked", this.checked);
        $("#Ptn2").prop("checked", this.checked);
        $("#Ptn3").prop("checked", this.checked);
        $("#Ptn4").prop("checked", this.checked);
        $("#Ptn5").prop("checked", this.checked);
        $("#Ptn6").prop("checked", this.checked);
        $("#Ptn7").prop("checked", this.checked);

    });

    $('#Ptn1').on('click', function (e) {
        if (this.checked && $("#Ptn2").prop("checked") && $("#Ptn3").prop("checked") && $("#Ptn4").prop("checked") && $("#Ptn5").prop("checked") && $("#Ptn6").prop("checked") && $("#Ptn7").prop("checked")) {
            $("#PtnflgNo").prop("checked", true);
        }
        else {
            $("#PtnflgNo").prop("checked", false);
        }
    });

    $('#Ptn2').on('click', function (e) {
        if (this.checked && $("#Ptn1").prop("checked") && $("#Ptn3").prop("checked") && $("#Ptn4").prop("checked") && $("#Ptn5").prop("checked") && $("#Ptn6").prop("checked") && $("#Ptn7").prop("checked")) {
            $("#PtnflgNo").prop("checked", true);
        }
        else {
            $("#PtnflgNo").prop("checked", false);
        }
    });

    $('#Ptn3').on('click', function (e) {
        if (this.checked && $("#Ptn1").prop("checked") && $("#Ptn2").prop("checked") && $("#Ptn4").prop("checked") && $("#Ptn5").prop("checked") && $("#Ptn6").prop("checked") && $("#Ptn7").prop("checked")) {
            $("#PtnflgNo").prop("checked", true);
        }
        else {
            $("#PtnflgNo").prop("checked", false);
        }
    });

    $('#Ptn4').on('click', function (e) {
        if (this.checked && $("#Ptn1").prop("checked") && $("#Ptn2").prop("checked") && $("#Ptn3").prop("checked") && $("#Ptn5").prop("checked") && $("#Ptn6").prop("checked") && $("#Ptn7").prop("checked")) {
            $("#PtnflgNo").prop("checked", true);
        }
        else {
            $("#PtnflgNo").prop("checked", false);
        }
    });

    $('#Ptn5').on('click', function (e) {
        if (this.checked && $("#Ptn1").prop("checked") && $("#Ptn2").prop("checked") && $("#Ptn3").prop("checked") && $("#Ptn4").prop("checked") && $("#Ptn6").prop("checked") && $("#Ptn7").prop("checked")) {
            $("#PtnflgNo").prop("checked", true);
        }
        else {
            $("#PtnflgNo").prop("checked", false);
        }
    });

    $('#Ptn6').on('click', function (e) {
        if (this.checked && $("#Ptn1").prop("checked") && $("#Ptn2").prop("checked") && $("#Ptn3").prop("checked") && $("#Ptn4").prop("checked") && $("#Ptn5").prop("checked") && $("#Ptn7").prop("checked")) {
            $("#PtnflgNo").prop("checked", true);
        }
        else {
            $("#PtnflgNo").prop("checked", false);
        }
    });

    $('#Ptn7').on('click', function (e) {
        if (this.checked && $("#Ptn1").prop("checked") && $("#Ptn2").prop("checked") && $("#Ptn3").prop("checked") && $("#Ptn4").prop("checked") && $("#Ptn5").prop("checked") && $("#Ptn7").prop("checked")) {
            $("#PtnflgNo").prop("checked", true);
        }
        else {
            $("#PtnflgNo").prop("checked", false);
        }
    });


    $('#PtnAnaflgNo').on('click', function (e) {

        $("#PtnAna1").prop("checked", this.checked);
        $("#PtnAna2").prop("checked", this.checked);

    });

    $('#PtnAna1').on('click', function (e) {
        if (this.checked && $("#PtnAna2").prop("checked")) {
            $("#PtnAnaflgNo").prop("checked", true);
        }
        else {
            $("#PtnAnaflgNo").prop("checked", false);
        }
    });

    $('#PtnAna2').on('click', function (e) {
        if (this.checked && $("#PtnAna1").prop("checked")) {
            $("#PtnAnaflgNo").prop("checked", true);
        }
        else {
            $("#PtnAnaflgNo").prop("checked", false);
        }
    });

    $("#selectBoxBangumi").change(function () {

        var val = this.value
        $('#Banguminm').val(val)

    });

    //内容リストで選択した時
    $("#selectBoxNaiyo").change(function () {
        var val = this.value
        $('#Naiyo').val(val)
    });

    //Scroll
    $("#dataDiv").scroll(function () {
        $("#headerDiv").scrollLeft($("#dataDiv").scrollLeft());
    });

    //ページ分けて表示
    $.fn.pageMe = function (opts) {
        var $this = this,
            defaults = {
                perPage: 7,
                showPrevNext: false,
                numbersPerPage: 5,
                hidePageNumbers: false
            },
            settings = $.extend(defaults, opts);

        var listElement = $this;
        var perPage = settings.perPage;
        var children = listElement.children();
        var pager = $('.pagination');

        if (typeof settings.childSelector != "undefined") {
            children = listElement.find(settings.childSelector);
        }

        if (typeof settings.pagerSelector != "undefined") {
            pager = $(settings.pagerSelector);
        }

        var numItems = children.size();
        var numPages = Math.ceil(numItems / perPage);

        pager.data("curr", 0);

        if (settings.showPrevNext) {
            $('<li><a href="#" class="first_link">＜＜最初</a></li>').appendTo(pager);
            //$('<li><a href="#" class="prev_link">前へ</a></li>').appendTo(pager);
        }

        var curr = 0;
        while (numPages > curr && (settings.hidePageNumbers == false)) {
            $('<li><a href="#" class="page_link">' + (curr + 1) + '</a></li>').appendTo(pager);
            curr++;
        }

        if (settings.numbersPerPage > 1) {
            $('.page_link').hide();
            $('.page_link').slice(pager.data("curr"), settings.numbersPerPage).show();
            //$('.page_link').slice(pager.data("curr"), pager.data("curr") + 1).show();
        }

        if (settings.showPrevNext) {
            if (numPages > settings.numbersPerPage) {
                //$('<li><a href="#" class="next_link">次へ</a></li>').appendTo(pager);
                $('<li><a href="#" class="last_link">最後＞＞</a></li>').appendTo(pager);
            }
        }

        pager.find('.page_link:first').addClass('active');
        pager.find('.prev_link').hide();
        pager.find('.first_link').hide();
        if (numPages <= 1) {
            pager.find('.next_link').hide();
            pager.find('.last_link').hide();
        }
        pager.children().eq(1).addClass("active");

        children.hide();
        children.slice(0, perPage).show();

        pager.find('li .page_link').click(function () {
            var clickedPage = $(this).html().valueOf() - 1;
            goTo(clickedPage, perPage);
            return false;
        });
        pager.find('li .prev_link').click(function () {
            previous();
            return false;
        });
        pager.find('li .next_link').click(function () {
            next();
            return false;
        });

        pager.find('li .first_link').click(function () {
            first();
            return false;
        });

        pager.find('li .last_link').click(function () {
            last();
            return false;
        });

        function previous() {
            var goToPage = parseInt(pager.data("curr")) - 1;
            goTo(goToPage);
        }

        function next() {
            goToPage = parseInt(pager.data("curr")) + 1;
            goTo(goToPage);
        }

        function first() {
            var goToPage = 0;
            goTo(goToPage);
        }

        function last() {
            var goToPage = numPages - 1;
            goTo(goToPage);
        }

        function goTo(page) {
            var startAt = page * perPage,
                endOn = startAt + perPage;

            children.css('display', 'none').slice(startAt, endOn).show();

            pager.data("curr", page);

            if (numPages > settings.numbersPerPage) {
                if (page >= 1) {
                    pager.find('.prev_link').show();
                    pager.find('.first_link').show();
                }
                else {
                    pager.find('.prev_link').hide();
                    pager.find('.first_link').hide();
                }

                if (page < (numPages - 1)) {
                    pager.find('.next_link').show();
                    pager.find('.last_link').show();
                }
                else {
                    pager.find('.next_link').hide();
                    pager.find('.last_link').hide();
                }

                if (page == 0) {
                    $('.page_link').hide();
                    $('.page_link').slice(page, settings.numbersPerPage + page).show();
                }
                else {
                    if (settings.numbersPerPage > 1) {
                        $('.page_link').hide();

                        if (page >= 2) {
                            $('.page_link').slice(page, settings.numbersPerPage - 2 + page).show();
                            $('.page_link').slice(page - 2, page).show();
                        }
                        else {
                            $('.page_link').slice(page, settings.numbersPerPage - 1 + page).show();
                            $('.page_link').slice(page - 1, page).show();
                        }
                    }
                }

                var total = settings.numbersPerPage + page

                if (total > numPages) {
                    var extrashow = total - numPages
                    $('.page_link').slice(page - extrashow, page).show();
                }


            }


            pager.children().removeClass("active");
            pager.children().eq(page + 1).addClass("active");

            //ページをクリックする時、そのページの一番目の行にフォーカスする
            var row = $('.firstrow');
            row.find('td:first').focus();

        }
    };

    //検索終わったら、処理中メッセージをクリア、検索ボタン使用可能にする
    jQuery(window).load(function () {

        setTimeout(function () {
            document.getElementById('lblInfo').style.color = 'white';
            $('#btnSearch').removeAttr("disabled");
            $('#btnDownloadCsv').removeAttr("disabled");
        }, 1000);
    });

        </script>
