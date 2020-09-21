1st @ModelType MES_WEB.m_proc0070
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"


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
        width: 260px;
    }
</style>

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "C1010_OneByOneJessiki", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                <button id="btnbacktomenu_A1010" name="btnbacktomenu_A1010" type="button" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>
                <br />

                @*This is for barcode textbox*@
                <div Class="form-group form-group-Custom">

                    @*MAN STAT CODE HEADER ,COMBO BOX*@
                    <div id="lblbarcode" Class="control-label control-label-Custom">@LangResources.C1010_02_MStation</div>
                    <div class=" input-group">
                        <div class="comboField">

                            @*MAN STAT CODE EDIT TEXT BOX*@
                            @Html.EditorFor(Function(model) model.man_stat_cd, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox", .autocomplete = "off", .maxlength = 10, .placeholder = LangResources.C1010_03_MStation_placeholder, .style = "width:125px"}})

                            <select class="form-control input-sm selectBox" id="selectBoxman_stat_cd" style=" width:145px;">
                                @If Model.man_stat_cd_List IsNot Nothing Then
                                    @For Each item As String In Model.man_stat_cd_List
                                        @<option>@item</option>
                                    Next
                                End If
                            </select>

                        </div>
                    </div>

                    @*MAN STAT CODE MESSAGE*@
                    <div id="error_man_stat_code_NODataNotFound" style="color:red;font-size:15px">@TempData("error_man_stat_code_NODataNotFound")</div>

                    @*ITEM CODE HEADER ,LABEL VALUE*@
                    <div id="lblbarcode" Class="control-label control-label-Custom">@LangResources.C1010_04_ItemCode</div>
                    @Html.EditorFor(Function(model) model.item_code, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.C1010_05_ItemCode_placeholder, .autocomplete = "off", .maxlength = 18}})

                    @*ITEM CODE MESSAGE*@
                    <div id="MSG_C1010_02_EmptyItemCode" style="color:red;font-size:15px" />
                    <div id="errorItemCode_NODataNotFound" style="color:red;font-size:15px">@TempData("errorItemCode_NODataNotFound")</div>
                </div>

                @*This is hidden button for Submit part*@
                <div Class="form-group form-group-Custom">
                    <Button id="btnhidden" name="btnhidden" type="submit" value="1" Class="btn btn-primary" hidden="hidden"></Button>
                    <div id="LblTxtBarcodeEmpty" Class="control-label invisible">@LangResources.MSG_A1010_17_TxtBarcodeEmpty</div>
                    @Html.EditorFor(Function(model) model.BolDirectGotoWO, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .hidden = "hidden"}})
                </div>

                </text>
            End Using
        </div>
    </div>
</div>

<script>

    $(window).unbind = function () {
        $('#btnbacktomenu_A1010').click();
    };

    $(document).ready(function () {

        if ($('#man_stat_cd').val() != "" && '@TempData("error_man_stat_code_NODataNotFound")' != "") {
            $('#man_stat_cd').focus();
            $('#man_stat_cd').select();
        } else if ($('#item_code').val() != "" && '@TempData("errorItemCode_NODataNotFound")' != "") {
            $('#item_code').focus();
            $('#item_code').select();
        } else if ($('#man_stat_cd').val() == "") {
            $('#man_stat_cd').focus();
        }
    });

    //DropDown Menu With TextBox
    $("#selectBoxman_stat_cd").change(function () {
        $('#man_stat_cd').val(this.value);
        $('#error_man_stat_code_NODataNotFound').text("");
    });

    //DropDown Menu With TextBox
    $("#man_stat_cd").focusout(function () {

        if ($('#man_stat_cd').val() != '') {
            $('#btnhidden').val("2");
            $('#btnhidden').click();
        }

    });

    @* This is validation and will get data from master *@
    $('#man_stat_cd').keypress(function (e) {

        if (e.keyCode == 13) {
            $('#item_code').focus();
            return false;
        };

    });

    @* This is validation and will get data from master *@
    $('#item_code').keydown(function (e) {

        if (e.keyCode == 13) {

            var man_stat_cd = $('#man_stat_cd').val();
            var item_code = $('#item_code').val();

            if (man_stat_cd == '') {
                $('#man_stat_cd').val("");
                $('#error_man_stat_code_NODataNotFound').text('@LangResources.MSG_C1010_01_EmptyManStatCode');
                $('#man_stat_cd').focus();
            }

            if (item_code == '') {
                $('#item_code').val("");
                $('#MSG_C1010_02_EmptyItemCode').text('@LangResources.MSG_C1010_02_EmptyItemCode');
                $('#item_code').focus();
            }

            if (man_stat_cd != '' && man_stat_cd != null && item_code != '' && item_code != null) {
                $('#btnhidden').val("3");
                $('#btnhidden').click();
                return true;

            } else {
                return false;
            }

        };

    });

    $('#btnhidden').click(function (e) {

        if ($('#man_stat_cd').val() == '') {
            $('#man_stat_cd').val("");
            $('#error_man_stat_code_NODataNotFound').text('@LangResources.MSG_C1010_01_EmptyManStatCode');
            $('#man_stat_cd').focus();
        }

        //return false;

    });

</script>

