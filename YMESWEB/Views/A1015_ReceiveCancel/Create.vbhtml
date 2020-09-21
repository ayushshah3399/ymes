@ModelType MES_WEB.d_mes0150
@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim strDateFormat As String = Session("language_Frmt")
End Code

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
        <div class="col-12" style="background-color:pink;max-width:353px" id="ContainerID_1">

            @Using Html.BeginForm("Create", "A1015_ReceiveCancel", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*Receive_date*@
                <div Class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.delete_result_date, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.TextBoxFor(Function(model) model.delete_result_date, strDateFormat, New With {.Class = "form-control", .style = "max-width:100px", .maxlength = 10, .autocomplete = "off", .placeholder = LangResources.A1010_09_Receivedate})
                </div>
                @Html.ValidationMessageFor(Function(model) model.delete_result_date, "", New With {.class = "text-danger form-group-Custom"})
                <div id="DateShouldbeLessThenToday" style="color:red">@TempData("DateShouldbeLessThenToday")</div>
                @*<div id="CheckBeforeupdate" style="color:red">@TempData("CheckBeforeupdate")</div>*@

                @*This is for Po Number,Sub-Po-Number,Receive Seq Label*@
                <div class="form-group form-group-Custom">

                    @Html.LabelFor(Function(model) model.po_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:160px;"})
                    @Html.LabelFor(Function(model) model.po_sub_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:70px"})
                    @Html.LabelFor(Function(model) model.po_receive_seq, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:35px"})

                    @*This is for Po Number,Sub-Po-Number,Receive Seq Value*@
                    <div class="input-group">
                        @Html.EditorFor(Function(model) model.po_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:160px"}})
                        @Html.EditorFor(Function(model) model.po_sub_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:70px"}})
                        @Html.EditorFor(Function(model) model.po_receive_seq, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "max-width:35px;text-align:right"}})
                    </div>

                </div>

                @*This is for Itemcode*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.ItemCode, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.ItemCode, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                </div>

                @*This is for ItemName*@
                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.Itemname, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.EditorFor(Function(model) model.Itemname, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                </div>

                @*receive_date*@
                <div Class="form-group form-group-Custom date">
                    @Html.LabelFor(Function(model) model.receive_date, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    @Html.TextBoxFor(Function(model) model.receive_date, strDateFormat, New With {.Class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""})

                </div>

                <div class="form-group form-group-Custom">
                    @Html.LabelFor(Function(model) model.str_receive_qty, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                    <div class=" input-group">
                        @Html.EditorFor(Function(model) model.str_receive_qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .style = "text-align:right;width:145px", .readonly = ""}})
                        @Html.EditorFor(Function(model) model.unit_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Style = "width:50px;margin-left:5px"}})
                    </div>
                    @Html.ValidationMessageFor(Function(model) model.str_receive_qty, "", New With {.class = "text-danger"})
                </div>
                <div id="notenoughdeleteqty" style="color:red">@TempData("notenoughdeleteqty")</div>

                @*This is For Change Button*@
                <div Class="form-group form-group-Custom">
                    <center>
                        <Button id="btnClear" name="btnClear" value="1" type="reset" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "A1015_ReceiveCancel")'">@LangResources.Common_Previous</Button>
                        <button id="btnCancleFlg" name="btnCancleFlg" type="submit" value="1" class="btn btn-danger Button-Custom" style="margin-left:20px">@LangResources.Common_BtnCancleFlg</button>
                    </center>
                </div>

                </text>
            End Using
        </div>
	</div>
</div>