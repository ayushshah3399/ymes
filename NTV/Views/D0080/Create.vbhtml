@ModelType NTV_SHIFT.D0080
@Code
    ViewData("Title") = "Create"
End Code

<h5>伝言板</h5>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">

        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @*<div class="form-group">
                @Html.LabelFor(Function(model) model.DNGNNO, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.DNGNNO, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.DNGNNO, "", New With { .class = "text-danger" })
                </div>
            </div>*@


        <div class="form-group">

            <div class="col-md-3">
                <textarea class="form-control" id="MESSAGE" name="MESSAGE" rows="3"></textarea>
                <span class="help-block">伝言は2週間で消えます。</span>

                <input type="submit" value="送信" class="btn btn-primary" />

            </div>

        </div>
        <div class="form-group">


            @*<div class="col-md-3">
                    @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
                </div>*@
        </div>
         @Html.ValidationMessageFor(Function(model) model.MESSAGE, "", New With {.class = "text-danger"})

    </div>
End Using

