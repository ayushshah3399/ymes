1st @ModelType MES_WEB.m_proc0070
@Code
    ViewData("Title") = "Create"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim strKey As String = ""
End Code
End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Create", "C1010_OneByOneJessiki", FormMethod.Post, New With {.class = "Form-Horizontal",.id="C1010Form"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*ITEM CODE , ITEM NAME , BACK BUTTON*@
            <div Class="form-group form-group-Custom ">
                @Html.Hidden("MAN_STAT_CD", Model.man_stat_cd)

                @*ITEM CODE HEADER AND LABEL*@
                <div class="input-group" style=" width :323px;">
                    <div class="input-group" style=" width :173px; height :53px">
                        <div id="lblitem_code" Class="control-label control-label-Custom" style=" width :150px; height :0px">@LangResources.C1010_04_ItemCode</div>
                        @Html.EditorFor(Function(model) model.item_code, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .style = "width:170px"}})
                    </div>
                    <button id="btnbacktomenu_A1010" name="btnbacktomenu_A1010" type="button" style=" width :150px" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "C1010_OneByOneJessiki", New RouteValueDictionary(New With {.man_stat_cd = Model.man_stat_cd, .man_stat_cd_List = Model.man_stat_cd_List}))'">@LangResources.Common_Previous</button>
                </div>

                @*ITEM CODE HEADER AND LABEL*@
                <div id="lblitem_name" Class="control-label control-label-Custom">@LangResources.C1010_06_ItemName</div>
                @Html.EditorFor(Function(model) model.item_name, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom ", .readonly = "", .style = "width:322px"}})
                <br />

                @*SERIAL NO LABEL AND EDIT TEXT*@
                <div id="lblserial_no" Class="control-label control-label-Custom">@LangResources.C1010_07_SerialNumber</div>
                @Html.EditorFor(Function(model) model.serial_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.C1010_07_SerialNumber_PlaceHolder, .autocomplete = "off", .maxlength = 15}})

                @*ERROR MESSAGE OF SERIAL NO*@
                <div id="MSG_C1010_03_EmptySerial_no" style="color:red;font-size:15px" />
                <div id="error_serial_no_NODataNotFound" style="color:red;font-size:15px">@TempData("error_serial_no_NODataNotFound")</div>
                <input type="hidden" name="workctr_code" id="id" value=@Model.workctr_code   readonly>
                <br />

            </div>

                <div Class="form-group form-group-Custom">
                    <div id="lbltableheader" Class="control-label control-label-Custom" style=" font-weight: bold">@LangResources.C1010_08_SerialNoTableHeader</div>
                    <Table Class="table table-striped table-fixed table-sm table-bordered table-margin">

                        <tbody style="height: 230px;overflow-y: auto;width: 100%;display: block;border:ridge;border-width:thin;background-color:lightyellow;">

                            <tr style="background-color:aqua">
                                <th>
                                    @Html.Label(LangResources.C1010_09_number, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "font-weight: bold;width:40px"})
                                </th>
                                <th>
                                    @Html.Label(LangResources.C1010_010_serial_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "font-weight: bold;width:130px"})
                                </th>
                                <th>
                                    @Html.Label(LangResources.C1010_09_register_time, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "font-weight: bold;width:115px"})
                                </th>
                            </tr>

                            @if Not Model Is Nothing AndAlso Not Model.obj_C1010_SerialNoInfo Is Nothing AndAlso Model.obj_C1010_SerialNoInfo.Count > 0 Then

                                @For i = 0 To Model.obj_C1010_SerialNoInfo.Count - 1
                                    'strKey = String.Format("obj_C1010_SerialNoInfo2[{0}].", i)
                                    Dim strKeynumber = String.Format("obj_C1010_SerialNoInfo[{0}].number", i)
                                    Dim strKeyserial_no = String.Format("obj_C1010_SerialNoInfo[{0}].serial_no", i)
                                    Dim strKeyregister_time = String.Format("obj_C1010_SerialNoInfo[{0}].register_time", i)
                                    @<tr style="display: block;border:ridge;border-width:thin">

                                         <td style="border:none">

                                             @Html.Label(Model.obj_C1010_SerialNoInfo(i).number, htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "font-weight: bold;width:40px"})
                                            @* @Html.Hidden(strKey & "NUMBER", Model.obj_C1010_SerialNoInfo.ToArray()(i).number)*@
                                             <input type="hidden" name=@strKeynumber id="id" value=@Model.obj_C1010_SerialNoInfo.ToArray()(i).number readonly>

                                         </td>
                                         <td style="border:none">

                                             @Html.Label(Model.obj_C1010_SerialNoInfo(i).serial_no.PadRight(20), htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "font-weight: bold; width:130px"})
                                             @*@Html.Hidden(strKey & "SERIAL_NO", Model.obj_C1010_SerialNoInfo(i).serial_no)*@
                                             <input type="hidden" name=@strKeyserial_no id="id" value=@Model.obj_C1010_SerialNoInfo.ToArray()(i).serial_no readonly>
                                         </td>
                                        <td style="border:none">

                                            @Html.Label(Model.obj_C1010_SerialNoInfo(i).register_time.PadRight(20), htmlAttributes:=New With {.class = "control-label control-label-Custom", .style = "font-weight: bold;width:115px"})
                                           @* @Html.Hidden(strKey & "REGISTER_TIME", time)*@
                                            <input type="hidden" name=@strKeyregister_time id="id" value=@Model.obj_C1010_SerialNoInfo.ToArray()(i).register_time readonly>
                                        </td>


                                    </tr>

                                Next

                            End If

                        </tbody>
                    </Table>
                </div>

                @*This is hidden button for Submit part*@
                <div Class="form-group form-group-Custom">
                    <Button id="btnhidden" name="btnhidden" type="submit" value="Create" Class="btn btn-primary" hidden="hidden"></Button>
                </div>

                </text>
            End Using
        </div>
    </div>
</div>

<script>

    $(document).ready(function () {


        if ('@TempData("error_serial_no_NODataNotFound")' == "") {
            $('#serial_no').val("");
        }
        $('#serial_no').select();
        $('#serial_no').focus();

    });



    @* This is validation and will get data from master *@
    $('#serial_no').keydown(function (e) {
        var serialno = $('#serial_no').val();
        if (e.keyCode == 13) {

            @* Return Flase if Error Occur *@
            if (serialno != '') {
                //$("#C1010Form").submit();
                //$('#btnhidden').click();
            } else {
                $('#MSG_C1010_03_EmptySerial_no').text('@LangResources.MSG_C1010_03_EmptySerial_no');
                return false;
            }

        };

    });

</script>

