@ModelType NTV_SHIFT.M0130

@Code
    ViewData("Title") = "スポーツカテゴリー設定"
    Dim strKey As String = ""
    Dim strKeyLink As String = ""
    Dim rowIndex As Integer = 0
    Dim modeType = Session("Mode")
End Code

<style>
      .mytable {
        width: 680px;
    }

    table.mytable tbody,
    table.mytable thead {
        display: block;
    }

    table.mytable tbody {
        height: 220px;
        width: 680px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .colbtn {
        width: 60px;
    }
    .SPORTCATNM_header {
        width: 295px;
    }
    .SPORTCATNM {
        width: 295px;
    }
    .HYOJJN_header {
        width: 130px;
    }
    .HYOJJN {
        width: 130px;
    }
    .HYOJ_header {
        width: 70px;
    }
    .HYOJ {
        width: 70px;
    }
    .Link_header {
        width: 90px;
    }
    .collink {
        width: 90px;
    }

 </style>


<div class="container">
  @Using (Html.BeginForm("EditM0150", "A0210", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
            
           <h3>@modeType</h3>
           
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            @Html.Hidden("opType", "2")
            @Html.Hidden("m0150SelectedIndex", "0")
            @Html.Hidden("IsDataChanged", If(Session("IsDataChanged") = Nothing, False, Session("IsDataChanged")))
            @Html.Hidden("IsDataChangedCreate", If(Session("IsDataChangedCreate") = Nothing, False, Session("IsDataChangedCreate")))
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.SPORTCATNM, htmlAttributes:=New With {.class = "control-label col-md-3"})
                <div class="col-md-5">
                    @Html.HiddenFor(Function(model) model.SPORTCATCD)
                    @Html.EditorFor(Function(model) model.SPORTCATNM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                    @Html.ValidationMessageFor(Function(model) model.SPORTCATNM, "", New With {.class = "text-danger"})
                </div>
            </div>
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.HYOJJN, htmlAttributes:=New With {.class = "control-label col-md-3", .style = "margin-top:10px"})
                <div class="col-md-5" style="margin-bottom:5px;">
                    @Html.EditorFor(Function(model) model.HYOJJN, New With {.htmlAttributes = New With {.class = "form-control input-sm", .style = "margin-top:10px"}})
                    @Html.ValidationMessageFor(Function(model) model.HYOJJN, "", New With {.class = "text-danger"})
                </div>
            </div>
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.HYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                <div class="col-md-5" style="margin-bottom:5px;">
                     @If modeType = "新規" Then
                        @Html.CheckBox("HYOJ", True, New With {.class = "", .style = "margin-top:15px"})  
Else
                        @Html.CheckBox("HYOJ", Model.HYOJ, New With {.class = "", .style = "margin-top:15px"})  
End If
                    
                </div>
            </div>
            <div class="col-md-12" style="padding-top:10px">
                <div class="col-md-1"></div>
                <div class="col-md-11">
                    <div class="col-md-11">
                            <!--<div class="form-horizontal">-->
                                <div class="row">           
                                    &nbsp; <label id="Error"></label>
                                    <table class="table table-bordered mytable" id="categoryTable" style="font-size:13px">
                                        <thead>
                                            <tr>
                                                <th class="colbtn"><input id="btn_sportCat2Add" type="button" value="追加" class="btn btn-success btn-xs colbtn" /></th>
                                                <th class="SPORTCATNM_header">
                                                    @Html.DisplayName("スポーツサブカテゴリー名")
                                                </th>
                                                <th class="HYOJJN_header">
                                                    @Html.DisplayName("表示順")
                                                </th>
                                                <th class="HYOJ_header">
                                                    @Html.DisplayName("表示")
                                                </th>
                                                <th class="Link_header">
                                                   
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            @If Model IsNot Nothing AndAlso Model.M0140.Count > 0 Then
                                            @For i = 0 To Model.M0140.Count - 1
                                                strKey = String.Format("M0140[{0}].", i)
                                                @<tr>
                                                    <td class="colbtn"><input id="btn_sportCat2Delete" type="button" value="削除" class="btn btn-danger btn-xs colbtn" /></td>
                                                    <td class="SPORTCATNM">
                                                        @Html.Hidden(strKey & "SELECTEDINDEX", Model.M0140(i).SELECTEDINDEX)
                                                        @Html.Hidden(strKey & "SPORTSUBCATCD", Model.M0140(i).SPORTSUBCATCD)
                                                        @Html.TextBox(strKey & "SPORTSUBCATNM", Model.M0140(i).SPORTSUBCATNM, htmlAttributes:=New With {.class = "form-control input-sm"})
                                                        <div>@Html.ValidationMessage(strKey & "SPORTSUBCATNM", New With {.Class = "text-danger"})</div>
                                                    </td>
                                                    <td class="HYOJJN">
                                                        @Html.TextBox(strKey & "HYOJJN", Model.M0140(i).HYOJJN, htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:112px"})
                                                        <div>@Html.ValidationMessage(strKey & "HYOJJN", New With {.Class = "text-danger"})</div>
                                                    </td>
                                                    <td class="HYOJ">
                                                        @Html.CheckBox(strKey & "HYOJ", Model.M0140(i).HYOJ, New With {.class = "", .style = "margin-top:12px; margin-left: 20px;"})
                                                    </td>
                                                    <td class="collink">
                                                        @Html.ActionLink("詳細設定", "EditM0150", "A0210", Nothing, htmlAttributes:=New With {.id = "detail_link", .Class = "", .value = i})
                                                        @Html.Label(strKey & "USERSETTINGSTATUS", If(Model.M0140(i).USERSETTINGSTATUS = Nothing, " ", Model.M0140(i).USERSETTINGSTATUS), htmlAttributes:=New With {.style = "color:red"})
                                                        @Html.Hidden(strKey & "USERSETTINGSTATUS", Model.M0140(i).USERSETTINGSTATUS)
                                                        <div>@Html.ValidationMessage(strKey & "SELECTEDINDEX", New With {.Class = "text-danger"})</div>
                                                    </td>
                                                </tr>
Next
                                            Else

                                             @<tr>
                                                    <td class="colbtn"><input id="btn_sportCat2Delete" type="button" value="削除" class="btn btn-danger btn-xs colbtn" /></td>
                                                    <td class="SPORTCATNM">
                                                        @Html.Hidden("M0140[0].SELECTEDINDEX", "0")
                                                        @Html.Hidden("M0140[0].SPORTSUBCATCD", "0")
                                                        @Html.TextBox("M0140[0].SPORTSUBCATNM", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
                                                        <div>@Html.ValidationMessage("M0140[0].SPORTSUBCATNM", New With {.Class = "text-danger"})</div>
                                                    </td>
                                                    <td class="HYOJJN">
                                                        @Html.TextBox("M0140[0].HYOJJN", Nothing, htmlAttributes:=New With {.class = "form-control input-sm", .style = "width:112px"})
                                                        <div>@Html.ValidationMessage("M0140[0].HYOJJN", New With {.Class = "text-danger"})</div>
                                                    </td>
                                                    <td class="HYOJ">
                                                        @Html.CheckBox("M0140[0].HYOJ", True, New With {.class = "", .style = "margin-top:12px; margin-left: 20px;"})
                                                    </td>
                                                    <td class="collink">
                                                        @Html.ActionLink("詳細設定", "EditM0150", "A0210", Nothing, htmlAttributes:=New With {.id = "detail_link", .class = "", .value = "0"})
                                                        @Html.Label("M0140[0].USERSETTINGSTATUS", "未", htmlAttributes:=New With {.value = "未", .name = "M0140[0].USERSETTINGSTATUS", .id = "M0140_0__USERSETTINGSTATUS", .style = "color:red"})
                                                        @Html.Hidden("M0140[0].USERSETTINGSTATUS", "未")
                                                        <div>@Html.ValidationMessage("M0140[0].SELECTEDINDEX", New With {.Class = "text-danger"})</div>
                                                    </td>
                                                </tr>
End If
                                        </tbody>
                                    </table>
                                </div>
                        <!--</div>-->
                    </div>
                </div>
             </div>
            <div class="col-md-12">
                <div class="col-md-2"></div>
                <div class="col-md-10" style="margin-left: 33.33333333%;">
                    @if modeType = "修正" then
                         @<input id = "btnMasterUpdForm" type="submit" value="更新" Class="btn btn-default">
                     Else
                         @<input id = "btnMasterUpdForm" type="submit" value="登録" Class="btn btn-default">
                    End If  

                   
                </div>
            </div>
        </div>
  End Using
    <div>
        @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnReturnMain"})
    </div>
</div>


<script>

    function detailLinkSubmit() {
        document.getElementById("opType").value = 1;
        document.forms[0].submit();
        return false;
    }

    $('#btnMasterUpdForm').click(function () {

        document.getElementById("opType").value = 2;

        var result = confirm("更新します。よろしいですか?")

        if (result == false) {
            return false
        }
    });
    //画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す

    //$('#HYOJJN').val('');
    $("#myForm :input").change(function (e) {
        var inputVal = $(this).val();
        var oldValue = e.target.defaultValue;

        if (e.target.type == "checkbox") {
            if (e.target.defaultChecked != e.target.checked) {
                $("#myForm").data("MSG", true);
                document.getElementById("IsDataChangedCreate").value = true;
            } else {
                $("#myForm").data("MSG", false);
                document.getElementById("IsDataChangedCreate").value = false;
            }
        } else {
            if (oldValue != inputVal) {
                $("#myForm").data("MSG", true);
                document.getElementById("IsDataChangedCreate").value = true;
            } else {
                $("#myForm").data("MSG", false);
                document.getElementById("IsDataChangedCreate").value = false;
            }
        }

        var ID = this.id;
        if(ID.indexOf("M0140") == 0)
        {
            if (inputVal != '') {
                $('div span[data-valmsg-for="'+this.name+'"]').text("");
            }
        }
    });

    $('#btnReturnMain').click(function () {
        //登録画面だけ確認メッセージを出すため、登録画面かどうかの判断フラグ
        var dataChanged = '@Html.Encode(Session("IsDataChanged"))'
        var dataChangedCreate = '@Html.Encode(Session("IsDataChangedCreate"))'
        if (dataChanged == "True" || $("#myForm").data("MSG") || dataChangedCreate == "True") {

            var result = confirm("更新または登録ボタンを押さないと編集中のデータは登録されません。よろしいですか？")

            if (result == false) {
                return false
            }
        }
    });

    //スポーツカテゴリ名2 Add Button Click
    $('#btn_sportCat2Add').click(function (event) {

        var table = document.getElementById("categoryTable");
        var rows = table.getElementsByTagName("tr");
        var index = rows.length - 1;

        var $table = $('#categoryTable');
        var $nrow = $table.find('tr:eq(1)').clone();
        var $ncell1 = $nrow.find('td:eq(1)');
        var $ncell2 = $nrow.find('td:eq(2)');
        var $ncell3 = $nrow.find('td:eq(3)');
        var $ncell4 = $nrow.find('td:eq(4)');
        //$ncell.find('#SPORTCATNM2').val('');

        //$table.append($nrow);

        //var table = document.getElementById("categoryTable");
        //var rows = table.getElementsByTagName("tr");

        //var $table = $('#categoryTable');
        //var $nrow = $table.find('tr:eq(1)').clone();
        //var $ncell = $nrow.find('td:eq(1)');
        //var $ncell = $nrow.find('td:eq(1)');


        var $select = $ncell1.find('[name*="SELECTEDINDEX"]');

        var rowLength = rows.length - 1;

        var previousRow = $('#categoryTable tr:eq(' + rowLength + ')');

        var previousRowFirstCell = previousRow.find('td:eq(1)');

        var previousRowIndex = previousRowFirstCell.find('[name*="SELECTEDINDEX"]')

        var previousRowValue = previousRowIndex.val();

        var previousRowIntValue = parseInt(previousRowValue)

        var NextRowValue = previousRowIntValue + 1;

        //$select.val(NextRowValue);

        var $select = $ncell1.find('[name*="SPORTSUBCATNM"]');
        $select.val('');
        $select.attr("name", "M0140[" + index + "].SPORTSUBCATNM");
        $select.attr("id", "M0140_" + index + "__SPORTSUBCATNM");

        var $select1 = $ncell1.find('input[name*="SPORTSUBCATCD"]');
        $select1.val(0);
        $select1.attr("name", "M0140[" + index + "].SPORTSUBCATCD");
        $select1.attr("id", "M0140_" + index + "__SPORTSUBCATCD");

        var $select11 = $ncell1.find('input[name*="SELECTEDINDEX"]');
        $select11.val(NextRowValue);
        $select11.attr("name", "M0140[" + index + "].SELECTEDINDEX");
        $select11.attr("id", "M0140_" + index + "__SELECTEDINDEX");

        var $select2 = $ncell2.find('[name*="HYOJJN"]');
        $select2.val('');
        $select2.attr("name", "M0140[" + index + "].HYOJJN");
        $select2.attr("id", "M0140_" + index + "__HYOJJN");

        var $select3 = $ncell3.find('[name*="HYOJ"]');
        $select3.val(true);
        $select3.attr("name", "M0140[" + index + "].HYOJ");
        $select3.attr("id", "M0140_" + index + "__HYOJ");

        var $select4 = $ncell4.find('[for*="USERSETTINGSTATUS"]');
        $select4.html('未');
        $select4.attr("name", "M0140[" + index + "].USERSETTINGSTATUS");
        $select4.attr("id", "M0140_" + index + "__USERSETTINGSTATUS");

        var $select5 = $ncell4.find('input[name*="USERSETTINGSTATUS"]');
        $select5.val('未');
        $select5.attr("name", "M0140[" + index + "].USERSETTINGSTATUS");
        $select5.attr("id", "M0140_" + index + "__USERSETTINGSTATUS");

        var $span = $ncell1.find('div').find('span');
        $span.text("");
        $span.attr("data-valmsg-for", "M0140[" + index + "].SPORTSUBCATNM");

        var $span2 = $ncell2.find('div').find('span');
        $span2.text("");
        $span2.attr("data-valmsg-for", "M0140[" + index + "].HYOJJN");

        $table.append($nrow);

        //テーブル行追加されたら、データ変更されたとして、戻るボタンの時メッセージ出す
        var RowCnt = table.rows.length;
        if (RowCnt > 1) {
            $("#myForm").data("MSG", true);

        }
        else {
            $("#myForm").data("MSG", false);
        }
    });

    function IsRowDeletable(ths) {
        var thistr = $(ths).closest('tr');
        var thistd = thistr.find('td:eq(1)');
        var sportcatcd = $('#SPORTCATCD').val();
        var sportsubcatcd = thistd.find('input:eq(1)').val();
        var rtn = true;
        var catPresentIn = ""

        //Call on server to check cat. is in use
        if (sportsubcatcd != "0") {
            $.ajax({
                url: "@Url.Action("CheckSubCatExistsInD0010", "A0210")",
                async: false,
                type: "POST",
                data: { sportcat: sportcatcd,sportsubcat:sportsubcatcd },
                dataType: 'json',
                cache: false,
                success: function (node) {
                    if (node.success) {
                        if (node.catExists != "") {
                            catPresentIn = node.catExists
                            rtn = false;
                        }
                    }
                },
                error: function (node) {
                    alert(node.responseText);
                }
            });
        }

        //Set error to div
        if (!rtn) {
            //subcategory is present in D0010
            if (catPresentIn == "D0010")
                alert("業務登録されているため削除できません。");
            //subcategory is not present in D0010 but present in D0070
            else
                alert("業務変更履歴が存在するため、削除できません。");
        }

        return rtn;
    }

    //スポーツカテゴリ名2 Remove Button Click
    $("#categoryTable").on('click', '#btn_sportCat2Delete', function () {

        var table = document.getElementById("categoryTable");
        var rows = table.getElementsByTagName("tr");

        var canDelete = IsRowDeletable(this);
        if (!canDelete)
            return;

        if (rows.length != 2) {

            $(this).closest('tr').remove();

            //行削除後、リストのIndexを振り直す
            for (var i = 0; i < rows.length-1; i += 1) {
                //var $ncell = $('#categoryTable tr:eq(' + i + ') td:first');
                var $ncell1 = $('#categoryTable tr:eq(' + (i + 1) + ') td:eq(1)')
                var $ncell2 = $('#categoryTable tr:eq(' + (i + 1) + ') td:eq(2)')
                var $ncell3 = $('#categoryTable tr:eq(' + (i + 1) + ') td:eq(3)')
                var $ncell4 = $('#categoryTable tr:eq(' + (i + 1) + ') td:eq(4)')

                var $select = $ncell1.find('[name*="SPORTSUBCATNM"]');
                $select.attr("name", "M0140[" + i + "].SPORTSUBCATNM");
                $select.attr("id", "M0140_" + i + "__SPORTSUBCATNM");

                var $select1 = $ncell1.find('[name*="SPORTSUBCATCD"]');
                $select1.attr("name", "M0140[" + i + "].SPORTSUBCATCD");
                $select1.attr("id", "M0140_" + i + "__SPORTSUBCATCD");

                var $select11 = $ncell1.find('[name*="SELECTEDINDEX"]');
                $select11.attr("name", "M0140[" + i + "].SELECTEDINDEX");
                $select11.attr("id", "M0140_" + i + "__SELECTEDINDEX");

                var $select2 = $ncell2.find('[name*="HYOJJN"]');
                $select2.attr("name", "M0140[" + i + "].HYOJJN");
                $select2.attr("id", "M0140_" + i + "__HYOJJN");

                var $select3 = $ncell3.find('[name*="HYOJ"]');
                $select3.attr("name", "M0140[" + i + "].HYOJ");
                $select3.attr("id", "M0140_" + i + "__HYOJ");

                var $select4 = $ncell4.find('[for*="USERSETTINGSTATUS"]');
                $select4.attr("name", "M0140[" + i + "].USERSETTINGSTATUS");
                $select4.attr("id", "M0140_" + i + "__USERSETTINGSTATUS");

                var $select5 = $ncell4.find('input[name*="USERSETTINGSTATUS"]');
                $select5.attr("name", "M0140[" + i + "].USERSETTINGSTATUS");
                $select5.attr("id", "M0140_" + i + "__USERSETTINGSTATUS");

                $("#myForm").data("MSG", true);
            }
        } else {
                alert("最低限1つのサブカテゴリの設定が必要です");
        }
    });

    //$(function () {
    //    $("#detail_link").click(function () {
    //        document.forms[0].submit();
    //        return false;
    //    });
    //});

    //$(function () {
    //    $("#detail_link").click(function () {
    //        var ret = detailLinkSubmit();
    //        return ret;
    //    });
    //});

    $("#categoryTable").on('click', '#detail_link', function (e) {

        var row = $(this).closest('tr')

        //var rowIndex = row[0].rowIndex - 1;
        var nCell = row.find('td:eq(1)');

        var rowIndex = nCell.find('[name*="SELECTEDINDEX"]')

        var rowIndexValue = rowIndex.val();
        
        document.getElementById("m0150SelectedIndex").value = rowIndexValue;

        //document.getElementById("m0150SelectedIndex").value = rowIndex;

        var ret = detailLinkSubmit();

        return ret;
    });

   

    function onDetailLinkBtnClick(e, linkObj) {
        e.preventDefault();
        var href = linkObj.href;

        //code
        var sportcatcd1val = 1; //$('#SPORTCATCD1').val();
        var sportcatcd2val = 2; //'';
        var sportcatnm1val = $('#SPORTCATNM1').val();
        var sportcatnm2val = linkObj.parentElement.parentElement.getElementsByTagName('input')[1].value;

        href = href + "?" +
            "sportcatcd1=" + sportcatcd1val + "&" +
            "sportcatcd2=" + sportcatcd2val + "&" +
            "sportcatnm1=" + sportcatnm1val + "&" +
            "sportcatnm2=" + sportcatnm2val;

        location.href = href;
    }

    //$(function () {
    //    $('#btnMasterUpd').click(function () {

    //        var result = confirm("更新します。よろしいですか?")

    //        if (result == false) {
    //            return false
    //        }
    //    });
    //});

</script>   

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section

