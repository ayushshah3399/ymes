@If Model = True Then
    @Html.Encode("表示")
Else
    @Html.Encode("非表示")
End If