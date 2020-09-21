@ModelType MES_WEB.d_mes0040
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim strKey As String = ""
    Dim ErrorlabelList = TempData("ErrorLabel")

End Code

<br />
<div class="container-fluid">
    <div class="row justify-content-center">
        <div class="col-12" style="max-width:353px">

            @Using Html.BeginForm("Index", "A1020", FormMethod.Post, New With {.class = "Form-Horizontal"})
                @Html.AntiForgeryToken()
                @<text>

                    @*This is For メニューへ Button*@
                    <div Class="form-group form-group-Custom">
                        <button type="button" value="1" Class="btn btn-secondary Button-Custom" onclick="location.href='@Url.Action("Index", "Menu")'">@LangResources.Common_BacktoMenu</button>
                    </div>

                    @*This is for 棚番 Textbox*@
                    <div class="form-group form-group-Custom focus">
                        @*This is For LblValShelfgroup Textbox*@
                        <div id="LblShelf" class="control-label control-label-Custom">@LangResources.A1020_11_Shelf</div>

                        @*To disable textbox*@
                        @if Model Is Nothing OrElse TempData("errortxtShelf_Empty") <> "" Then

                            @Html.EditorFor(Function(model) model.shelf_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .autocomplete = "off", .Placeholder = LangResources.A1020_11_Shelf, .style = "max-width:1000px;font-size:15px;ime-mode: disabled", .maxlength = 10}})
                        Else

                            @Html.EditorFor(Function(model) model.shelf_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .autocomplete = "off", .disabled = "disabled", .Placeholder = LangResources.A1020_11_Shelf, .style = "max-width:1000px;font-size:15px;ime-mode: disabled", .maxlength = 10}})
                        End If
                        @Html.HiddenFor(Function(model) model.shelf_no)
                        @Html.ValidationMessageFor(Function(model) model.shelf_no, "", New With {.class = "text-danger"})
                        <div id="errortxtShelf_Empty" style="color:red">@TempData("errortxtShelf_Empty")</div>
                    </div>

                    @*This is For LblShelfgroup Textbox*@
                    <div class="form-group form-group-Custom">

                        <label id="LblShelfgroup" class="control-label control-label-Custom" Style="font-size:15px;width:150px">@LangResources.A1020_12_Shelfgroup</label>
                        <label id="LblStoragelocation" class="control-label control-label-Custom" Style="font-size:15px;width:70px">@LangResources.A1020_13_StorgeLocation</label>
                        <br />
                        @*This is For LblValShelfgroup Textbox*@
                        @if Model Is Nothing Then
                            @<label id="LblValShelfgroup" Class="control-label control-label-Custom"></label>
                            @<label id="LblValStoragelocation" Class="control-label control-label-Custom"></label>
                        Else
                            @<label id="LblValShelfgroup" Class="control-label control-label-Custom" style="color: blueviolet;font-size:15px;width:150px">@Model.shelfgrp_code</label>
                            @<label id="LblValStoragelocation" Class="control-label control-label-Custom" style="color: blueviolet;font-size:15px;width:70px">@Model.location_code</label>
                        End If

                    </div>

                    @*This is for 現品表ラベルNo Textbox*@
                    <div class="form-group form-group-Custom">
                        <div id="LblLabelNo" class="control-label control-label-Custom">@LangResources.A1020_08_LabelNo</div>

                        @if ViewData("Enable") = "True" Then
                            @Html.EditorFor(Function(model) model.label_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .autocomplete = "off", .Placeholder = LangResources.A1020_08_LabelNo, .style = "max-width:1000px;font-size:15px;ime-mode: disabled", .maxlength = 16}})
                        Else
                            @Html.EditorFor(Function(model) model.label_no, New With {.htmlAttributes = New With {.class = "form-control form-control-Custom", .autocomplete = "off", .Placeholder = LangResources.A1020_08_LabelNo, .style = "max-width:1000px;font-size:15px;ime-mode: disabled", .maxlength = 16, .disabled = "disabled"}})
                        End If

                        @Html.ValidationMessageFor(Function(model) model.label_no, "", New With {.class = "text-danger"})
                        <div id="errortxtLabelNo_Empty" style="color:red">@TempData("errortxtLabelNo_Empty")</div>
                    </div>

                    @*This is For Change Button*@
                    <div Class="form-group form-group-Custom">
                        <Center>
                            <Button id="btnClear" name="btnClear" value="1" type="reset" Class="btn btn-secondary Button-Custom" style="text-align:center;font-size :15px;width:120px;height:40px" onclick="location.href='@Url.Action("Index", "A1020")'">@LangResources.Common_Clear</Button>
                            <button id="btnRegister" name="btnRegister" type="submit" value="3" class="btn btn-primary Button-Custom" style="font-size :15px;width:120px;height:40px;margin-left:20px">@LangResources.Common_Register</button>
                        </Center>
                    </div>

                    <div Class="form-group form-group-Custom">
                        <Table Class="table table-striped table-fixed table-sm table-bordered" id="TableId_A1020" style="margin-bottom:0px">
                            <thead style="width: 100%">
                                <tr>
                                    <th scope="col" Style="font-size:15px;background-color:aqua">@LangResources.A1020_08_LabelNo</th>
                                </tr>
                            </thead>

                            @*If Height Greter then 132 Then Automatically Scroll Will There.*@
                            <tbody style="height: 132px;overflow-y: auto;width: 100%;display: block;">
                                @if Not Model Is Nothing AndAlso Not Model.obj_A1020_Labelinfo Is Nothing AndAlso Model.obj_A1020_Labelinfo.Count > 0 Then

                                    @For i = 0 To Model.obj_A1020_Labelinfo.Count - 1
                                        strKey = String.Format("obj_A1020_Labelinfo[{0}].", i)
                                        @<tr style="display: block;">
                                            <td style="display: block;">

                                                @Html.Label(strKey & "LABEL_NO", Model.obj_A1020_Labelinfo(i).label_no, htmlAttributes:=New With {.class = "control-label control-label-Custom", .id = "TableLableno", .Style = "font-size:15px"})
                                                @Html.Hidden(strKey & "LABEL_NO", Model.obj_A1020_Labelinfo(i).label_no)

                                            </td>
                                        </tr>
                                    Next
                                End If
                            </tbody>
                        </Table>
                    </div>

                    @If ErrorlabelList IsNot Nothing AndAlso ErrorlabelList.count > 0 Then
                        @<div id="ErrorConcurrency" style="color:red" Class="form-group form-group-Custom">@LangResources.MSG_A1020_15_LocationDiffrentBeforeUpdate</div>
                        @<ul  id ="ErrorlabelList" style="color:red">
                            @For Each item In ErrorlabelList
                                @<li>@item</li>
                            Next
                        </ul>
                    End If

                    @*This is For  Hidden label to Get Value in Jquery*@
                    <div Class="row">
                        <div id="LblEmptyShelfNO" Class="control-label control-label-Custom invisible" hidden="hidden">@LangResources.MSG_A1020_04_LblEmptyShelfNO</div>
                        <div id="LblEmptyLabelNo" Class="control-label control-label-Custom invisible" hidden="hidden">@LangResources.MSG_A1020_03_LblEmptyLabelNo</div>
                    </div>

                </text>
            End Using

        </div>
    </div>
</div>

@* Jquery Validation *@
<script>

    @* This is validation and will get data from master *@
    $('#shelf_no').focusout(function (e) {

            @* Validation *@
			var txtShelf = $('#shelf_no').val();
            var LblEmptyShelfNO = $('#LblEmptyShelfNO').text();
            var errflg = '';
             $('#HiddenshelfNo').val("")

            @* Error Message Become Null *@
        $("#errortxtShelf_Empty").text("");
        $('#ErrorConcurrency').text("");
        $('#ErrorlabelList').text("");

            if (txtShelf == '') {

                $("#errortxtShelf_Empty").text(LblEmptyShelfNO);
                $("#LblValShelfgroup").text("");
                $("#LblValStoragelocation").text("");
                errflg = '1';

            }

            @* Return Flase if Error Occur *@
			if (errflg != '') {
                return false
            }
            if (errflg == '') {

                $('#btnRegister').val("1");
                $('#btnRegister').click();

            }

    });

	@*This is validation and will get data from master*@
    $('#shelf_no').keypress(function (e) {
		if (e.keyCode == 13) {
			@* Validation *@
			var txtShelf = $('#shelf_no').val();
			var LblEmptyShelfNO = $('#LblEmptyShelfNO').text();
			var errflg = '';
			$('#HiddenshelfNo').val("")

			@* Error Message Become Null *@
			$("#errortxtShelf_Empty").text("");

			if (txtShelf == '')
			{

				$("#errortxtShelf_Empty").text(LblEmptyShelfNO);
				$("#LblValShelfgroup").text("");
				$("#LblValStoragelocation").text("");
				errflg = '1';

			}

			@* Return Flase if Error Occur *@
			if (errflg != '') {
				return false
			}
			if (errflg == '') {
				//$('#HiddenshelfNo').val("1")
				$('#btnRegister').val("1")

			}

		};

	});

	@* This is validation and will get data from master *@
	$('#label_no').keypress(function (e) {
		if (e.which === 13) {
			@* Validation *@
			var txtlabel_no = $('#label_no').val();
			var LblEmptyLabelNo = $('#LblEmptyLabelNo').text();
			var errflg = '';
			$('#HiddenshelfNo').val("")

			@* Error Message Become Null *@
			$("#errortxtLabelNo_Empty").text("");

			if (txtlabel_no == '') {

				$("#errortxtLabelNo_Empty").text(LblEmptyLabelNo);
				errflg = '1';

			}

			@* Return Flase if Error Occur *@
			if (errflg != '') {
				return false
			}
			if (errflg == '') {
				//$('#HiddenshelfNo').val("2")
				$('#btnRegister').val("2")
			}

		};

    });

    @* This is validation and will get data from master *@
    $('#label_no').focusout(function (e) {

            @* Validation *@
			var txtlabel_no = $('#label_no').val();
            var LblEmptyLabelNo = $('#LblEmptyLabelNo').text();
            var errflg = '';
            $('#HiddenshelfNo').val("")

            @* Error Message Become Null *@
            $("#errortxtLabelNo_Empty").text("");

            if (txtlabel_no == '') {

                $("#errortxtLabelNo_Empty").text(LblEmptyLabelNo);
                errflg = '1';

            }

            @* Return Flase if Error Occur *@
			if (errflg != '') {
                return false
            }
            if (errflg == '') {
                //$('#HiddenshelfNo').val("2")
                $('#btnRegister').val("2")
                $('#btnRegister').click();
            }

    });

	$('#myForm').keypress(function (e) {
		if (e.keyCode === 13) {

			e.preventDefault();
			return false;

		}
    });

    //On Click Need to Displayed Error Message
    $("#btnRegister").click(function () {

        //lable no
        var txtshelf_no = $('#shelf_no').val();
        var LblValShelfgroup = $('#LblValShelfgroup').val();
        //Message Empty Lable No
        var LblEmptyShelfNO = $('#LblEmptyShelfNO').text();

        @* Error Message Become Null *@
        $("#errortxtShelf_Empty").text("");
        var errflg = '';

        if (LblValShelfgroup == '' && txtshelf_no == '') {

            $("#errortxtShelf_Empty").text(LblEmptyShelfNO);
            errflg = '1';

        }

        @* Return Flase if Error Occur *@
			if (errflg != '') {
            return false
        }

    });

</script>