@ModelType MES_WEB.d_mes0100
@Code
	ViewData("Title") = "Create"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim strKey As String = ""
End Code

<br />
<div class="container-fluid">
	<div class="row justify-content-center">
        <div class="col-12" style="background-color:pink;max-width:353px" id="ContainerID_1">

            @Using Html.BeginForm("Create", "A1055_PayOutGetApproveCancle", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<text>
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                    @*This is For Picking Number And Storage Bin Number*@
                    <div class="form-group form-group-Custom">

                        @*label*@
                        @Html.LabelFor(Function(model) model.picking_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:150px"})
                        @Html.LabelFor(Function(model) model.shelfgrp_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:150px"})

                        @*Value*@
                        <div class="input-group">
                            @Html.EditorFor(Function(model) model.picking_no, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "width:150px"}})
                            @Html.EditorFor(Function(model) model.shelfgrp_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "width:150px"}})
                        </div>

                    </div>

                    @*This is For Location Code And in Location Code*@
                    <div class="form-group form-group-Custom">

                        @*Lable*@
                        @Html.LabelFor(Function(model) model.loc_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:150px"})
                        @Html.LabelFor(Function(model) model.in_loc_code, htmlAttributes:=New With {.class = "control-label control-label-Custom", .Style = "width:150px"})

                        @*value*@
                        <div class="input-group">
                            @Html.EditorFor(Function(model) model.loc_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "width:150px"}})
                            @Html.EditorFor(Function(model) model.in_loc_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "width:150px"}})
                        </div>

                    </div>

                    @*This is For Change Button*@
                    <center>
                        <Button id="btnPrevious" name="btnPrevious" value="1" type="reset" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "A1055_PayOutGetApproveCancle")'">@LangResources.Common_Previous</Button>
                        <button id="btnRegister" name="btnRegister" type="submit" value="2" class="btn btn-danger Button-Custom" style="margin-left:20px">@LangResources.Common_BtnCancleFlg</button>
                    </center>
                    @Html.EditorFor(Function(model) model.A1030_Hidden_Qty, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})
                    @Html.EditorFor(Function(model) model.updtdt, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .Hidden = "Hidden"}})

                    <div Class="form-group form-group-Custom">
                        <Table Class="table table-striped table-fixed table-sm table-bordered table-margin">
                            <thead style="width: 100%">
                                <tr>
                                    <th scope="col" colspan="2" Style="font-size:15px;background-color:aqua">@LangResources.A1040_02_RegLabelInfo</th>
                                </tr>
                            </thead>
                            <tbody style="height: 230px;overflow-y: auto;width: 100%;display: block;border:ridge;border-width:thin;background-color:lightyellow;">

                                @if Not Model Is Nothing AndAlso Not Model.obj_A1040_TableInfo Is Nothing AndAlso Model.obj_A1040_TableInfo.Count > 0 Then

                                    @For i = 0 To Model.obj_A1040_TableInfo.Count - 1
                                        strKey = String.Format("obj_A1040_TableInfo[{0}].", i)

                                        @if Model.obj_A1040_TableInfo(i).ItemCodeFlag = "1" Then

                                            @<tr style="display: block;border:ridge;border-width:thin">

                                                <td style="display: block;border:none">

                                                    @Html.Label(strKey & "TABLELABELINFO", Model.obj_A1040_TableInfo(i).TableLabelInfo.PadRight(20), htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "font-weight: bold"})
                                                    @Html.Hidden(strKey & "TABLELABELINFO", Model.obj_A1040_TableInfo(i).TableLabelInfo)

                                                </td>

                                                @For j = i + 1 To Model.obj_A1040_TableInfo.Count - 1
                                                    strKey = String.Format("obj_A1040_TableInfo[{0}].", i)

                                                    @if Model.obj_A1040_TableInfo(j).ItemCodeFlag = "2" Then

                                                        @<td style="display: block;border:none">

                                                            @Html.Label(strKey & "TABLELABELINFO", Model.obj_A1040_TableInfo(j).TableLabelInfo.PadRight(20), htmlAttributes:=New With {.Class = "control-label control-label-Custom", .Style = "min-width:170px"})
                                                            @Html.Hidden(strKey & "TABLELABELINFO", Model.obj_A1040_TableInfo(j).TableLabelInfo)
                                                            @Html.Label(strKey & "TABLELABELINFO", Model.obj_A1040_TableInfo(j).TableQty, htmlAttributes:=New With {.Class = "control-label control-label-Custom"})
                                                            @Html.Hidden(strKey & "TABLELABELINFO", Model.obj_A1040_TableInfo(j).TableQty)
                                                        </td>

                                                    Else

                                                        Exit For

                                                    End If

                                                Next

                                            </tr>

                                        End If

                                    Next
                                End If

                            </tbody>
                        </Table>
                    </div>

                </text>
            End Using
        </div>
	</div>
</div>
