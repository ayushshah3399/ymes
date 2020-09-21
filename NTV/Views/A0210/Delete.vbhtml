@ModelType NTV_SHIFT.M0130

@Code
    ViewData("Title") = "スポーツカテゴリー設定"
    Dim strKey As String = ""
    Dim rowIndex As Integer = 0
End Code

<style>
    .mytable {
        width: 486px;
    }

    table.mytable tbody,
    table.mytable thead {
        display: block;
    }

    table.mytable tbody {
        height: 220px;
        width: 436px;
        overflow-y: auto;
        overflow-x: hidden;
    }

</style>

<h3>削除</h3>

<h3>削除してもよろしいですか？</h3>

<div >
    @Using (Html.BeginForm("Delete", "A0210", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
        @Html.Hidden("id", Model.SPORTCATCD)
    <h3></h3>

    <hr />
    <div>
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(Function(model) model.SPORTCATNM)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.SPORTCATNM)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.HYOJJN)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.HYOJJN)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.HYOJ)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.HYOJ)
            </dd>

        </dl>

    </div>

    <div style="padding-top:10px">
        <div class="col-md-1"></div>
        @*<div class="form-horizontal">*@
        &nbsp; <label id="Error"></label>
        <table class="tablecontainer">
            <tr>
                <td>
                    <table class="table table table-bordered table-scroll table-hover ">
                        <thead>
                            <tr>
                                <th style="width:200px">
                                    @Html.DisplayNameFor(Function(model) model.M0140(0).SPORTSUBCATNM)
                                </th>
                                <th style="width:80px">
                                    @Html.DisplayNameFor(Function(model) model.HYOJJN)
                                </th>
                                <th style="width:60px">
                                    @Html.DisplayNameFor(Function(model) model.HYOJ)
                                </th>
                            </tr>
                        </thead>

                        <tbody>
                            @For Each item In Model.M0140

                                @<tr>

                                    <td style="width:200px;max-width:200px;">
                                        @Html.DisplayFor(Function(modelItem) item.SPORTSUBCATNM)
                                    </td>
                                    <td style="width:80px;max-width:60px;">
                                        @Html.DisplayFor(Function(modelItem) item.HYOJJN)
                                    </td>
                                    <td style="width:60px;max-width:100px;">
                                        @Html.DisplayFor(Function(modelItem) item.HYOJ)
                                    </td>
                                </tr>
                            Next
                        </tbody>

                    </table>

                </td>
            </tr>

        </table>
        @*</div>*@

    </div>
    <div class="form-actions no-color">
        <input id = "btnDeleteCat" type="submit" value="削除" class="btn btn-default" /> |
        @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = ""})
    </div>
</div>
    End Using
</div>


<script>

    $('#btnDeleteCat').click(function () {
        var sportcatcd = $('#id').val()
        var canDelete = IsRowDeletable(sportcatcd);
        if (!canDelete)
            return false;   
    });

    function IsRowDeletable(sportcatcd) {
        var rtn = true;
        var catPresentIn = ""

        //Call on server to check cat. is in use
        $.ajax({
            url: "@Url.Action("CheckSubCatExistsInD0010", "A0210")",
            async: false,
            type: "POST",
            data: { sportcat: sportcatcd,sportsubcat:"" },
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

    function detailLinkSubmit() {
        document.getElementById("opType").value = 1;
        document.forms[0].submit();
        return false;
    }
    //画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す

    //$('#HYOJJN').val('');
    $("#myForm :input").change(function () {
        var inputVal = $(this).val();

        if (inputVal != '') {
            $("#myForm").data("MSG", true);
        }
        else {
            $("#myForm").data("MSG", false);
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
        //$ncell.find('#SPORTCATNM2').val('');

        //$table.append($nrow);

        //var table = document.getElementById("categoryTable");
        //var rows = table.getElementsByTagName("tr");

        //var $table = $('#categoryTable');
        //var $nrow = $table.find('tr:eq(1)').clone();
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

        var $span = $ncell1.find('div').find('span');
        $span.text("");
        $span.attr("data-valmsg-for", "M0140[" + index + "].SPORTSUBCATNM")

        $table.append($nrow);

        //$("#detail_link").bind('click', null, detailLinkSubmit);

        //$($nrow[0].getElementsByTagName('a').detail_link).click(function (e) {
        //    onDetailLinkBtnClick(e, this);
        //});

        /*var RowCnt = table.rows.length;
        if (RowCnt > 2) {
            $("#myForm").data("MSG", true);
        }
        else {
            $("#myForm").data("MSG", false);
        }*/
    });

    //スポーツカテゴリ名2 Remove Button Click
    $("#categoryTable").on('click', '#btn_sportCat2Delete', function () {

        var table = document.getElementById("categoryTable");
        var rows = table.getElementsByTagName("tr");

        //if (rows.length != 2) {
        //    $(this).closest('tr').remove();
        //}

        if (rows.length != 2) {
            $(this).closest('tr').remove();

            //行削除後、リストのIndexを振り直す
            for (var i = 0; i < rows.length - 1; i += 1) {
                //var $ncell = $('#categoryTable tr:eq(' + i + ') td:first');
                var $ncell1 = $('#categoryTable tr:eq(' + (i + 1) + ') td:eq(1)')
                var $ncell2 = $('#categoryTable tr:eq(' + (i + 1) + ') td:eq(2)')
                var $ncell3 = $('#categoryTable tr:eq(' + (i + 1) + ') td:eq(3)')

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

            }
        }

        //$("#myForm").data("MSG", true);

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

    $(function () {
        $('#btnMasterUpd').click(function () {

            var result = confirm("更新します。よろしいですか?")

            if (result == false) {
                return false
            }
        });
    });

</script>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section

