@*@Model DateTime*@

@*@Html.TextBox("", String.Format("{0:d}", Model.ToShortDateString()), New With {.class = "datefield"})*@
    
@Html.TextBoxFor(Function(model) model, New With {.class = "datepicker"})

    @*<script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker();
        });

        //$(function () {
        //    $('.datepicker').datepicker();
        //});
    </script>*@