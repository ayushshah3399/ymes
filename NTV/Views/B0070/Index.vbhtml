@ModelType IEnumerable(Of NTV_SHIFT.D0090)
@Code
    ViewData("Title") = "雛形一覧"
End Code

<style>
    .table-scroll {
        width: 2522px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }

    table.table-scroll tbody {
        height: 480px;
        width: 2522px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .colLink {
        width: 50px;
    }

    .colDATAKBN {
        width: 50px;
    }

    .colHINAMEMO {
        width: 130px;
        max-width:130px;
        word-wrap:break-word;
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

    .colCATNM {
        width: 110px;
        max-width:110px;
        word-wrap:break-word;
    }

    .colBANGUMINM, .colNAIYO, .colBASYO {
        width: 170px;
        max-width:170px;
        word-wrap:break-word;
   }

    .colAna {
        width: 170px;
        max-width:170px;
        word-wrap:break-word;
   }

    .colOAJKN {
        width: 95px;
    }

    .colOAJKNST, .colOAJKNED {
        width: 45px;
    }

    .colINSTDT {
        width: 140px;
        max-width:170px;
        word-wrap:break-word;
   }

    .colINSTID {
        width: 90px;
        max-width:90px;
        word-wrap:break-word;
   }

    .colPTNFLG {
        width: 110px;
        max-width:120px;
        word-wrap:break-word;
   }

    .colBANGUMITANTO {
        width: 130px;
        max-width:130px;
        word-wrap:break-word;
    }

    .colBANGUMIRENRK {
        width: 110px;
        max-width:130px;
        word-wrap:break-word;
   }

    .colBIKO {
        width: 160px;
        max-width:160px;
        word-wrap:break-word;
    }

    .colSIYOFLG {
        width: 70px;
    }

    .colChkDel {
        width: 90px;
    }

    body {
   font-size:12px;
   
}

</style>


@Using Html.BeginForm("Index", "B0070", FormMethod.Get)

    @<div class="row">
         <div class="col-md-2 col-md-push-11">
             <p>
                 <ul class="nav nav-pills ">
                     @*<li><a href="#">印刷</a></li>
                    <li><a href="#">最新情報</a></li>*@
                     @If ViewData("FormName") = "A0170" Then
                         @<li>@Html.ActionLink("戻る", "Create", "A0170")</li>
                     Else
                         @<li>@Html.ActionLink("戻る", "Create", "B0020")</li>
                     End If

                 </ul>
             </p>
         </div>

    <div class="col-md-10 col-md-pull-2">
        @*<h3>雛形一覧</h3>*@
        <p style="padding-top:20px;">
            <div class="form-inline">
                @Html.DropDownList("SIYOFLG", Nothing, htmlAttributes:=New With {.class = "form-control", .style = "width:160px; height:40px"})
                @*<select class="form-control" id="sel1" name="sel1" style="width:160px; height:40px">
                <option value="1" >全て表示</option>
                <option value="2">登録済み</option>
                <option value="3">未登録</option>
            </select>*@

                <button id="hyoji" type="submit" class="btn btn-success btn-sm">表示</button>
            </div>
        </p>
       
    </div>
</div>

    
End Using

@Using Html.BeginForm()
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
                        <th class="colDATAKBN">
                            @Html.DisplayNameFor(Function(model) model.DATAKBN)
                        </th>
                        <th class="colBANGUMINM">
                            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                        </th>
                        <th class="colAna">
                            担当アナ
                        </th>
                        <th class="colAna">
                            仮アナ
                        </th>
                        <th class="colHINAMEMO">
                            @Html.DisplayNameFor(Function(model) model.HINAMEMO)
                        </th>

                        <th class="colPTNFLG">
                            @Html.DisplayNameFor(Function(model) model.PTNFLG)
                        </th>
                        <th colspan="2" class="colKSKJKNST">
                            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
                        </th>

                        <th class="colCATNM">
                            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
                        </th>

                        <th class="colNAIYO">
                            @Html.DisplayNameFor(Function(model) model.NAIYO)
                        </th>

                        <th class="colBASYO">
                            @Html.DisplayNameFor(Function(model) model.BASYO)
                        </th>

                        <th class="colINSTID">
                            @Html.DisplayNameFor(Function(model) model.INSTID)
                        </th>
                        <th class="colINSTDT">
                            @Html.DisplayNameFor(Function(model) model.INSTDT)
                        </th>
                        <th colspan="2" class="colGYOMYMD">
                            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
                        </th>
                        <th colspan="2" class="colOAJKN">
                            @Html.DisplayNameFor(Function(model) model.OAJKNST)
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
                        <th class="colSIYOFLG">
                            登録済み
                        </th>
                      
                    </tr>

                </thead>

                <tbody>
                    @For i As Integer = 0 To Model.Count - 1
                            Dim key As String = String.Format("lstd0090[{0}].", i)
                            Dim item = Model(i)

                        @Html.Hidden(key + "HINANO", item.HINANO)

                        @<tr>
                            <td class="colLink">
                                @*@Html.ActionLink("選択", "Create", "B0020")*@
                                @If ViewData("FormName") = "A0170" Then
                                    @Html.ActionLink("選択", "Create", "A0170", routeValues:=New With {.hinano = item.HINANO}, htmlAttributes:=Nothing)
                                Else

                                    @Html.ActionLink("選択", "Create", "B0020", routeValues:=New With {.hinano = item.HINANO}, htmlAttributes:=Nothing)
                                End If

                            </td>
                             <td class="colLink">
                                 @Html.ActionLink("削除", "Delete", New With {.id = item.HINANO})
                             </td>
                             <td class="colChkDel" align="center">
                                 @Html.CheckBox(key + "FLGDEL", item.FLGDEL)
                             </td>
                             <td class="colDATAKBN">
                                 @Html.DisplayFor(Function(modelItem) item.DATAKBN)

                             </td>
                            <td class="colBANGUMINM">
                                @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                            </td>
                            <td class="colAna">
                                @For j = 0 To item.D0100.Count - 1
                                        If j > 0 Then
                                    @Html.Encode("，")
                                End If
                                Dim k = j
                                    @Html.DisplayFor(Function(modelItem) item.D0100(k).M0010.USERNM)
                                Next
                            </td>
                            <td class="colAna">
                                @For j = 0 To item.D0101.Count - 1
                                        If j > 0 Then
                                    @Html.Encode("，")
                                End If
                                Dim k = j
                                    @Html.DisplayFor(Function(modelItem) item.D0101(k).ANNACATNM)
                                Next
                            </td>
                            <td class="colHINAMEMO">
                                @Html.DisplayFor(Function(modelItem) item.HINAMEMO)
                            </td>
                            <td class="colPTNFLG">
                                @If item.PTNFLG = False Then
                                    @Html.DisplayFor(Function(modelItem) item.PTNFLG)
                                Else

                                    @Html.DisplayFor(Function(modelItem) item.PTN1)

                                    @If item.PTN1 = True And item.PTN2 = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.PTN2)

                                    @If (item.PTN1 = True Or item.PTN2 = True) And item.PTN3 = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.PTN3)

                                    @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True) And item.PTN4 = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.PTN4)

                                    @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True) And item.PTN5 = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.PTN5)

                                    @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True) And item.PTN6 = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.PTN6)

                                    @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True Or item.PTN6 = True) And item.PTN7 = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.PTN7)

                                    @*ASI[21 Oct 2019]: if WEEKA and WEEKB exist then display it on grid*@
                                    @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True Or item.PTN6 = True Or item.PTN7 = True) And item.WEEKA = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.WEEKA)

                                    @If (item.PTN1 = True Or item.PTN2 = True Or item.PTN3 = True Or item.PTN4 = True Or item.PTN5 = True Or item.PTN6 = True Or item.PTN7 = True Or item.WEEKA = True) And item.WEEKB = True Then
                                        @Html.Encode("，")
                                    End If

                                    @Html.DisplayFor(Function(modelItem) item.WEEKB)


                                End If
                            </td>

                            <td class="colKSKJKNST1">
                                @If item.KSKJKNST IsNot Nothing Then
                                    @Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(0, 2)
                                    @Html.Encode(":")
                                    @Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(2, 2)
                                End If
                            </td>

                            <td class="colKSKJKNST2">
                                @If item.KSKJKNED IsNot Nothing Then
                                    @Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(0, 2)
                                    @Html.Encode(":")
                                    @Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(2, 2)
                                End If
                            </td>

                            <td class="colCATNM">
                                @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                            </td>

                            <td class="colNAIYO">
                                @Html.DisplayFor(Function(modelItem) item.NAIYO)
                            </td>
                            <td class="colBASYO">
                                @Html.DisplayFor(Function(modelItem) item.BASYO)
                            </td>
                            <td class="colINSTID">
                                @Html.DisplayFor(Function(modelItem) item.INSTID)

                            </td>
                            <td class="colINSTDT">
                                @Html.DisplayFor(Function(modelItem) item.INSTDT)

                            </td>
                            <td class="colGYOMYMD1">
                                @Html.DisplayFor(Function(modelItem) item.GYOMYMD)

                            </td>
                            <td class="colGYOMYMD2">
                                @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)

                            </td>
                            <td class="colOAJKNST">
                                @If item.OAJKNST IsNot Nothing Then
                                    @Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(0, 2)
                                    @Html.Encode(":")
                                    @Html.DisplayFor(Function(modelItem) item.OAJKNST).ToString.Substring(2, 2)
                                End If
                            </td>
                            <td class="colOAJKNED">
                                @If item.OAJKNED IsNot Nothing Then
                                    @Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(0, 2)
                                    @Html.Encode(":")
                                    @Html.DisplayFor(Function(modelItem) item.OAJKNED).ToString.Substring(2, 2)
                                End If
                            </td>
                            <td class="colBANGUMITANTO">
                                @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)

                            </td>
                            <td class="colBANGUMIRENRK">
                                @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)

                            </td>
                            <td class="colBIKO">
                                @Html.DisplayFor(Function(modelItem) item.BIKO)

                            </td>                          
                            <td class="colSIYOFLG">
                                @Html.DisplayFor(Function(modelItem) item.SIYOFLG)
                                @*<input type="checkbox" name="query_myTextEditBox" style="text-align:center; vertical-align: middle;">*@
                            </td>
                            
                        </tr>
                    Next
                </tbody>

            </table>
        </td>
    </tr>
    </table>


End Using


<script>

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

    //$(function () {
    //    $('#hyoji').click(function () {
    //        var sel1 = document.getElementById("sel1");
    //      //alert(sel1.value)
    //        sessionStorage.setItem("sel1", sel1.value);

    //    });
    //});


    //var sel1 = document.getElementById("sel1");

    //sel1.value = sessionStorage.getItem("sel1");
    //sessionStorage.removeItem('sel1');


</script>




