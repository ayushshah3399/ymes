@ModelType MES_WEB.d_mes0100
@Code
	ViewData("Title") = "Create"
	Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
        <div class="col-12" style="background-color:pink;max-width:353px" id="ContainerID_1">

            @Using Html.BeginForm("Create", "A1035_PayOutCancel", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*LabelNo TextBox*@
                <div class="form-group form-group-Custom focus">

                    @Html.LabelFor(Function(model) model.TextBox_lable_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.TextBox_lable_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .autocomplete = "off", .maxlength = 16}})

                </div>
                <div id="NoDataMes" name="NoDataMes" style="color:red;Font-Size:15px">@TempData("NoDataMes")</div>

                @*LabelNo Lable*@
                <div class="form-group form-group-Custom">

                    @Html.LabelFor(Function(model) model.label_no, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.label_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})

                </div>

                @*ItemCode Lable*@
                <div class="form-group form-group-Custom">

                    @Html.LabelFor(Function(model) model.item_code, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.item_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})

                </div>

                @*ItemName Lable*@
                <div class="form-group form-group-Custom">

                    @Html.LabelFor(Function(model) model.A1030_Itemname, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.A1030_Itemname, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})

                </div>

                @*str_qty*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.str_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    <div class=" input-group">
                        @Html.EditorFor(Function(model) model.str_qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "text-align: right;max-width:145px"}})
                        @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:50px"}})
                        @Html.ValidationMessageFor(Function(model) model.str_qty, "", New With {.class = "text-danger"})
                    </div>

                </div>

                @*This is For Change Button*@
                <div Class="form-group form-group-Custom">
                    <center>
                        <Button id="btnPrevious" name="btnPrevious" value="1" type="reset" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "A1035_PayOutCancel")'">@LangResources.Common_Previous</Button>
                        <button id="btnRegister" name="btnRegister" type="submit" value="2" class="btn btn-danger Button-Custom" style="margin-left:20px">@LangResources.Common_BtnCancleFlg</button>
                    </center>
                    <div id="CheckBeforeupdate" style="color:red">@TempData("CheckBeforeupdate")</div>
                    @Html.EditorFor(Function(model) model.updtdt, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                </div>

                <div class="form-group form-group-Custom">

                    @Html.LabelFor(Function(model) model.picking_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})
                    @Html.LabelFor(Function(model) model.shelfgrp_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})

                    <div class="input-group">
                        @Html.EditorFor(Function(model) model.picking_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                        @Html.EditorFor(Function(model) model.shelfgrp_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                    </div>

                </div>

                <div class="form-group form-group-Custom">

                    @Html.LabelFor(Function(model) model.loc_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})
                    @Html.LabelFor(Function(model) model.in_loc_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "min-width:140px"})

                    <div class="input-group">
                        @Html.EditorFor(Function(model) model.loc_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                        @Html.EditorFor(Function(model) model.in_loc_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:140px"}})
                    </div>

                </div>

                </text>
            End Using
        </div>
	</div>
</div>

@*Javascript Validation*@
<script>
    @* This is validation and will get data from master *@
    $('#TextBox_lable_no').keypress(function (e) {
        if (e.keyCode == 13) {
            @* Validation *@

            $('#btnRegister').val("1")

        };

    });

    @* When Focus out Fill Data To Other Control *@
    $('#TextBox_lable_no').focusout(function (e) {

        var lable_no = $('#TextBox_lable_no').val();

        if (lable_no != "") {

            $('#btnRegister').val("1");
            $('#btnRegister').click();

        };

    });

</script>