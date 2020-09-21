@ModelType IEnumerable(Of NTV_SHIFT.D0110)
@Code
    ViewData("Title") = "デスクメモ"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
    Dim info = ViewBag.SortingPagingInfo
End Code

<style>
    .table-scroll {
        width: 1845px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-scroll tbody {
        height: 360px;
        width: 1845px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .colLink {
        width: 45px;
    }

    .colDeskno {
        width: 100px;
        max-width: 120px;
        word-wrap: break-word;
    }

    .colKakuninnm {
        width: 60px;
        max-width: 60px;
        word-wrap: break-word;
    }

    .colInputdt {
        width: 85px;
    }

    .colDesknm {
        width: 90px;
        max-width: 110px;
        word-wrap: break-word;
    }

    .colCATNM {
        width: 110px;
        max-width: 110px;
        word-wrap: break-word;
    }

    .colBanguminm, .colNaiyo, .colBasyo {
        width: 130px;
        max-width: 160px;
        word-wrap: break-word;
    }

    .colShiftdt {
        width: 180px;
    }

    .colAna {
        width: 150px;
        max-width: 180px;
        word-wrap: break-word;
    }

    .colBangumitanto {
        width: 110px;
        max-width: 110px;
        word-wrap: break-word;
    }

    .colBangumirenrk {
        width: 130px;
        max-width: 130px;
        word-wrap: break-word;
    }

    .colDeskmemo {
        width: 280px;
        max-width: 280px;
        word-wrap: break-word;
    }

    .colLink2 {
        width: 50px;
    }

    #CondBanguminm {
        font-size: 14px;
        width: 200px;
        position: absolute;
    }

    #selectBoxBangumi {
        font-size: 14px;
        width: 225px;
    }

      #CondNaiyo {
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
</style>


@Using Html.BeginForm("Index", "A0200", FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})


    @Html.Hidden("SortField", info.SortField)
    @Html.Hidden("SortDirection", info.SortDirection)
    @<div class="row">
        <div class="col-md-1 col-md-push-11">
            <p>
                <ul class="nav nav-pills ">
                    @*<li><a href="#">印刷</a></li>
                        <li><a href="#">最新情報</a></li>*@
                @If Session("UrlReferrer") IsNot Nothing Then
                    @<li><a href="@Session("UrlReferrer")">戻る</a></li>
End If
                </ul>
            </p>
        </div>

        <div class="col-md-4 col-md-pull-1">
            @*<h3>デスクメモ</h3>*@
            <p style="padding-top:20px;">
                @Html.ActionLink("新規登録", "Create")
                |  <button id="EnDisCondition" type="button" class="btn btn-info btn-sm">検索条件表示/非表示</button>
                | <button id="btnSearch" type="submit" class="btn btn-default btn-sm">検索</button>
            </p>
        </div>

        <div class="col-md-7 col-md-pull-1">
            <p style="padding-top:20px;font-size:21px;font-weight:bold">検索画面</p>
        </div>
    </div>

    @<div id="conditionrow" class="row">

        <div class="form-horizontal">
            <div class="form-group div_condition">
                <label class="col-md-2 control-label">デスクメモNo</label>
                <div class="col-md-10">
                    @Html.TextBox("CondDeskno", Nothing, New With {.class = "form-control input-sm", .style = "width:160px;"})
                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label">確認</label>
                <div class="col-md-10" style="margin-bottom:5px;">
                    <label class="checkbox-inline">
                        @Html.CheckBox("CondKakunin1")済
                    </label>
                    <label class="checkbox-inline">
                        @Html.CheckBox("CondKakunin2")済以外
                    </label>
                </div>
            </div>

            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">更新日</label>
                <div class="col-md-10">
                    @Html.TextBox("CondInstst", Nothing, New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:130px;"})
                    ～
                    @Html.TextBox("CondInsted", Nothing, New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:130px;"})
                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label">入力者</label>
                <div class="col-md-10">
                    @Html.DropDownList("CondDeskid", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:130px;"})
                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label">番組名</label>
                <div class="col-md-10">
                    @Html.TextBox("CondBanguminm", Nothing, New With {.class = "form-control input-sm"})
                    <select class="form-control input-sm selectBox" id="selectBoxBangumi">
                        @If Bangumilst IsNot Nothing Then
                            @For Each item In Bangumilst
                                @<option>@item.BANGUMINM</option>
                            Next
                             End If
                    </select>

                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label">内容</label>
                <div class="col-md-10">
                    @Html.TextBox("CondNaiyo", Nothing, New With {.class = "form-control input-sm"})
                    <select class="form-control input-sm selectBox" id="selectBoxNaiyo">
                        @If NaiyouList IsNot Nothing Then
                            @For Each item In NaiyouList
                                @<option>@item.NAIYO </option>
                            Next
                      End If
                    </select>
                </div>
            </div>
            
            <div class="form-group div_condition">
                <label class="col-md-2 control-label">場所</label>
                <div class="col-md-10">
                    @Html.TextBox("CondBasyo", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>
            <div class="form-group form-inline div_condition">
                <label class="col-md-2 control-label">シフト日</label>
                <div class="col-md-10">
                    @Html.TextBox("CondShiftst", Nothing, New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:130px;"})
                    ～
                    @Html.TextBox("CondShifted", Nothing, New With {.class = "form-control input-sm datepicker imedisabled", .style = "width:130px;"})
                    <div id="ErrorShiftdt" style="color:red"></div>
                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label"> 担当アナ</label>
                <div class="col-md-10">
                    @Html.DropDownList("CondAnaid", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:130px;"})
                </div>
            </div>
            <div class="form-group div_condition">
                <label class="col-md-2 control-label">カテゴリー</label>
                <div class="col-md-10">
                    @Html.DropDownList("CondCatcd", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                </div>
            </div>
               
            <div class="form-group div_condition">
                <label class="col-md-2 control-label">番組担当者</label>
                <div class="col-md-10">
                    @Html.TextBox("CondBangumitanto", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>

            <div class="form-group div_condition">
                <label class="col-md-2 control-label">連絡先</label>
                <div class="col-md-10">
                    @Html.TextBox("CondBangumirenrk", Nothing, New With {.class = "form-control input-sm"})
                </div>
            </div>

        </div>

    </div>

End Using

<p></p>
<table class="tablecontainer">
    <tr>
        <td>
            <table id="tblSearchResult" class="table table-bordered table-hover table-scroll">
                <thead>
                    <tr>
                        <th class="colLink"></th>
                        <th class="colLink"></th>  
                        <th class="colLink2"></th>                      
                      
                        <th class="colKakuninnm">
                            <a href="#" data-sortfield="KAKUNINNM"
                               class="header">確認</a>
                        </th>

                        <th class="colInputdt">
                            <a href="#" data-sortfield="UPDTDT"
                               class="header">@Html.DisplayNameFor(Function(model) model.UPDTDT)</a>
                        </th>
                        <th class="colDesknm">

                            <a href="#" data-sortfield="USERID"
                               class="header">@Html.DisplayNameFor(Function(model) model.USERID)</a>
                        </th>
                        <th class="colDeskmemo">

                            <a href="#" data-sortfield="DESKMEMO"
                               class="header">@Html.DisplayNameFor(Function(model) model.DESKMEMO)</a>
                        </th>
                        <th class="colBanguminm">

                            <a href="#" data-sortfield="BANGUMINM"
                               class="header">@Html.DisplayNameFor(Function(model) model.BANGUMINM)</a>
                        </th>
                        
                        <th class="colAna">

                            <a href="#" data-sortfield="ANA"
                               class="header">担当アナ</a>
                        </th>
                        <th class="colNaiyo">

                            <a href="#" data-sortfield="NAIYO"
                               class="header">@Html.DisplayNameFor(Function(model) model.NAIYO)</a>
                        </th>
                       
                        <th class="colShiftdt">

                            <a href="#" data-sortfield="SHIFTDT"
                               class="header">シフト日と拘束時間</a>
                        </th>
                        <th class="colBasyo">

                            <a href="#" data-sortfield="BASYO"
                               class="header">@Html.DisplayNameFor(Function(model) model.BASYO)</a>
                        </th>              
                        <th class="colCATNM">

                            <a href="#" data-sortfield="CATNM"
                               class="header">@Html.DisplayNameFor(Function(model) model.M0020.CATNM)</a>
                        </th>

                        <th class="colBangumitanto">

                            <a href="#" data-sortfield="BANGUMITANTO"
                               class="header">@Html.DisplayNameFor(Function(model) model.BANGUMITANTO)</a>
                        </th>

                        <th class="colBangumirenrk">

                            <a href="#" data-sortfield="BANGUMIRENRK"
                               class="header">@Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)</a>
                        </th>
                        <th class="colDeskno">
                            <a href="#" data-sortfield="DESKNO"
                               class="header">@Html.DisplayNameFor(Function(model) model.DESKNO)</a>
                        </th>                     
                    </tr>

                </thead>
                <tbody>
                    @If Model IsNot Nothing Then
                        @For Each item In Model
                            @<tr>
                                <td class="colLink">
                                    @Html.ActionLink("修正", "Edit", New With {.id = item.DESKNO})
                                </td>
                                 <td class="colLink">
                                     @Html.ActionLink("削除", "Delete", New With {.id = item.DESKNO})
                                 </td>
                                 <td class="colLink2">
                                     @Html.ActionLink("参照", "Details", New With {.id = item.DESKNO})
                                 </td>                             
                                <td class="colKakuninnm">
                                    @Html.DisplayFor(Function(modelItem) item.M0100.KAKUNINNM)
                                </td>
                                <td class="colInputdt">
                                    @Html.DisplayFor(Function(modelItem) item.UPDTDT)
                                </td>
                                 <td class="colDesknm">
                                     @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                                 </td>
                                 <td class="colDeskmemo">
                                     @Html.DisplayFor(Function(modelItem) item.DESKMEMO)
                                 </td>                               
                                 <td class="colBanguminm">
                                     @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                 </td>                                
                              
                                 <td class="colAna">
                                     @For i = 0 To item.D0130.Count - 1
                                     If i > 0 Then
                                         @Html.Encode("，")
                                     End If
                                     Dim j = i
                                         @Html.DisplayFor(Function(modelItem) item.D0130(j).M0010.USERNM)
                                     Next
                                 </td>
                                 <td class="colNaiyo">
                                     @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                 </td>
                                 <td class="colShiftdt">
                                     @For i = 0 To item.D0120.Count - 1
                                     If i > 0 Then
                                             @Html.Encode("，")
                                     End If
                                     Dim j = i
                                         @Html.DisplayFor(Function(modelItem) item.D0120(j).SHIFTYMDST)
                                     If item.D0120(i).SHIFTYMDED IsNot Nothing AndAlso item.D0120(i).SHIFTYMDST <> item.D0120(i).SHIFTYMDED Then
                                         @Html.Encode("～")
                                         @Html.DisplayFor(Function(modelItem) item.D0120(j).SHIFTYMDED)
                                     End If

                                     If item.D0120(i).KSKJKNST IsNot Nothing OrElse item.D0120(i).KSKJKNED IsNot Nothing Then
                                         @Html.Encode("　")
                                     If item.D0120(i).KSKJKNST IsNot Nothing Then
                                         @Html.Encode(item.D0120(i).KSKJKNST.ToString.Substring(0, 2) & ":" & item.D0120(i).KSKJKNST.ToString.Substring(2, 2))
                                     End If

                                         @Html.Encode("～")
                                     If item.D0120(i).KSKJKNED IsNot Nothing Then
                                         @Html.Encode(item.D0120(i).KSKJKNED.ToString.Substring(0, 2) & ":" & item.D0120(i).KSKJKNED.ToString.Substring(2, 2))
                                     End If
                                     End If

                                     Next
                                 </td>
                                 <td class="colBasyo">
                                     @Html.DisplayFor(Function(modelItem) item.BASYO)
                                 </td>
                                <td class="colCATNM">
                                    @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                                </td>
                                 <td class="colBangumitanto">
                                     @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                                 </td>
                                 <td class="colBangumirenrk">
                                     @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
                                 </td>
                                 <td class="colDeskno">
                                     @Html.DisplayFor(Function(modelItem) item.DESKNO)
                                 </td>
                               
                            </tr>
                        Next
    End If
                </tbody>
            </table>

        </td>
    </tr>
</table>

<script>
    $(document).ready(function () {

        var table = document.getElementById("tblSearchResult");
        var rows = table.getElementsByTagName("tr");
        if (rows.length > 1) {
            $("#conditionrow").hide();
        }

        $(".header").click(function (evt) {

            var table = document.getElementById("tblSearchResult");
            var rows = table.getElementsByTagName("tr");

            if (rows.length < 3) {
                return
            }

            var sortfield = $(evt.target).data("sortfield");

            if ($("#SortField").val() == sortfield) {
                if ($("#SortDirection").val() == "ascending") {
                    $("#SortDirection").val("descending");
                }
                else {
                    $("#SortDirection").val("ascending");
                }
            }
            else {
                $("#SortField").val(sortfield);
                $("#SortDirection").val("ascending");
            }
            evt.preventDefault();

            $("#myForm").submit();
        });
    });

    $('#btnSearch').on('click', function (e) {

        $("#SortField").val('New');

        var Shiftst = $('#CondShiftst').val();
        var Shifted = $('#CondShifted').val();
        var errflg = '';

        if (Shiftst == '' && Shifted != '') {
            $("#ErrorShiftdt").text("シフトの終了日のみの指定はできません。 ");
            errflg = '1'
        }
        else if (Shiftst != '' && Shifted != '' && Shiftst > Shifted) {
            $("#ErrorShiftdt").text("シフト日-開始と終了の前後関係が誤っています。 ");
            errflg = '1'
        }
        else {
            $("#ErrorShiftdt").text("")
        }


        if (errflg != '') {
            return false
        }


    });


    $("#selectBoxBangumi").change(function () {

        var val = this.value
        $('#CondBanguminm').val(val)

    });

    //内容リストで選択した時
    $("#selectBoxNaiyo").change(function () {
        var val = this.value
        $('#CondNaiyo').val(val)
    });
</script>

