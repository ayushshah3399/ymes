@ModelType IEnumerable(Of NTV_SHIFT.D0080)

@For Each item In Model
    @Using (Html.BeginForm("Delete", "C0050", Nothing, FormMethod.Post))
        @Html.AntiForgeryToken()
        @<div class="panel panel-success">
           
           @If ViewData("Kanri") = "1" Then
                  @<input id="DeleteButton" type="submit" class="close btn-xs DeleteButton" value="X" style="color:black" />
		   End If
             
           
            <div class="panel-heading">
                
                @Html.Hidden("DNGNNO", item.DNGNNO)
                @Html.DisplayFor(Function(modelItem) item.TOROKUYMD)
                <br />@Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
            </div>
             <div class="panel-body" style="word-wrap:break-word;">
                 @Html.DisplayFor(Function(modelItem) item.MESSAGE)
             </div>

        </div>
	End Using

Next

<script>
   

    $(function () {
        $('.DeleteButton').click(function () {
       

                var result = confirm("削除します。よろしいですか?")

                if (result == false) {
                    return false
                }
        
        });

    });
</script>













