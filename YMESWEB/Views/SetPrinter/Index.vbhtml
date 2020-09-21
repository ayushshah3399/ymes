@ModelType MES_WEB.sy060
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim strKey As String = ""
End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "SetPrinter", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                @*This is For List Of  Printer  from database*@
                @If Not Model Is Nothing AndAlso Not Model.obj_model_PrintSetting Is Nothing AndAlso Model.obj_model_PrintSetting.Count > 0 Then
                    @*This is Header For Choose Printer*@
                    @<Center><h3><b>@LangResources.MSG_P1_02_PleaseChoosePrinter</b></h3></Center>
                Else
                    @*This is Header For Data For Printer*@
                    @<Center><h3><b>@LangResources.MSG_P1_03_NoDataFound</b></h3></Center>
                End If

                @*This is For List Of  Printer  from database*@
                @If Not Model Is Nothing AndAlso Not Model.obj_model_PrintSetting Is Nothing AndAlso Model.obj_model_PrintSetting.Count > 0 Then

                    @<div class="list-group-item-text" style="overflow-y:scroll;height:332px;border:ridge;border-width:thin;">
                        @For i = 0 To Model.obj_model_PrintSetting.Count - 1
                            strKey = String.Format("obj_model_PrintSetting[{0}].", i)

                            @if Model.obj_model_PrintSetting(i).PrinterId = Model.HiddenlblForPrinterid Then
                                'HighLight The Selected Printer
                                @<button type="button" id=@Model.obj_model_PrintSetting(i).PrinterId name="button1" Class="list-group-item list-group-item-action active" value=@Model.obj_model_PrintSetting(i).PrinterId style="font-size:15px;border-width:thin"><b>@Model.obj_model_PrintSetting(i).PrinterName</b></button>
                            Else
                                @<button type="button" id=@Model.obj_model_PrintSetting(i).PrinterId name="button1" Class="list-group-item list-group-item-action" value=@Model.obj_model_PrintSetting(i).PrinterId style="font-size:15px;border-width:thin"><b>@Model.obj_model_PrintSetting(i).PrinterName</b></button>
                            End If

                        Next
                    </div>

                End If

                @*This is For Change Button*@
                <div Class="form-group form-group-Custom" style="margin-top:10px">
                    <center>
                        <Button id="btnPrevious" name="btnPrevious" value="1" type="reset" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</Button>

                        @*This is For List Of  Printer  from database*@
                        @If Not Model Is Nothing AndAlso Not Model.obj_model_PrintSetting Is Nothing AndAlso Model.obj_model_PrintSetting.Count > 0 Then
                            @<Button id="btnRegister" name="btnRegister" type="submit" value="2" Class="btn btn-primary Button-Custom" style="margin-left:20px">@LangResources.Common_Register</Button>
                        End If

                    </center>
                    @Html.HiddenFor(Function(model) model.HiddenlblForPrinterid)
                </div>

                </text>
            End Using
        </div>
    </div>
</div>
@* Jquery Validation *@
<Script>

@* This is validation and will get data from master *@
$('button[name="button1"]').click(function () {

    //Get Currrent Seleted Button Id
    var btn = (this.id)

    //Set The Value To The hidden label
    $('#HiddenlblForPrinterid').val(btn)

    $('button[name="button1"]').removeClass('active').addClass('inactive');
    $(this).removeClass('inactive').addClass('active');

});

</Script>
