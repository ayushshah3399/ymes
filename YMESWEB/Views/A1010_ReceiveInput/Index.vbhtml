@ModelType MES_WEB.d_mes0150
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "A1010_ReceiveInput", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<Text>

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                <button id="btnbacktomenu_A1010" name="btnbacktomenu_A1010" type="button" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>
                <br />
                @if Model IsNot Nothing Then

                    @if Model.header_text IsNot Nothing AndAlso Model.header_text <> "" Then

                        @*This is for back to menu button*@
                        @<div Class="form-group form-group-Custom">
                            @Html.LabelFor(Function(model) model.header_text, htmlAttributes:=New With {.class = "control-label control-label-Custom"})
                            @Html.EditorFor(Function(model) model.header_text, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = ""}})
                        </div>

                    End If

                End If

                @*This is for barcode textbox*@
                <div Class="form-group form-group-Custom focus">
                    <div id="lblbarcode" Class="control-label control-label-Custom">@LangResources.A1010_16_POBarCode</div>
                    @Html.EditorFor(Function(model) model.barcode, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .placeholder = LangResources.A1010_16_POBarCode, .autocomplete = "off", .maxlength = 18}})
                    @Html.ValidationMessageFor(Function(model) model.barcode, "", New With {.class = "text-danger"})
                </div>
                <div id="errorDataNotFound" style="color:red;font-size:15px">@TempData("errorDataNotFound")</div>

                @*This is hidden button for Submit part*@
                <div Class="form-group form-group-Custom">
                    <Button id="btnhidden" name="btnhidden" type="submit" value="Create" Class="btn btn-primary" hidden="hidden"></Button>
                    <div id="LblTxtBarcodeEmpty" Class="control-label invisible">@LangResources.MSG_A1010_17_TxtBarcodeEmpty</div>
                    @Html.EditorFor(Function(model) model.BolDirectGotoWO, New With {.htmlAttributes = New With {.class = "form-control-plaintext form-control-plaintext-Custom", .readonly = "", .hidden = "hidden"}})
                </div>

                </text>
            End Using
        </div>
    </div>
</div>

@*Javascript Validation*@
<script>

    $(document).ready(function () {
        var BolDirectGotoWO = $('#BolDirectGotoWO').val();
        if (BolDirectGotoWO == 'True') {
            $('#header_text').css("color","black");
        }
    });

    @*This is validation and will get data from master*@
    $('#barcode').keypress(function (e)
    {
        if (e.keyCode == 13)
        {
             @*Validation *@
            var txtbarcode = $('#barcode').val();
            var ObjLblTxtBarcodeEmpty = $('#LblTxtBarcodeEmpty').text();
            var errflg = '';

            @* Error Message Become Null *@
            $("#errorDataNotFound").text("");

            if (txtbarcode == '')
            {

                $("#errorDataNotFound").text(ObjLblTxtBarcodeEmpty);
                errflg = '1';

            }

            @* Return Flase if Error Occur *@
            if (errflg != '') {
                return false
            }
            else {
                $('#btnhidden').click();
            }

        };

    });

</script>
