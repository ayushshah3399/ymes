@If Model = True Then
    @Html.Encode("無効")
Else
    @Html.Encode("有効")
End If

