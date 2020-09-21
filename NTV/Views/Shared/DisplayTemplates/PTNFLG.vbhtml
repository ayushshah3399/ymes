@If Model = True Then
    @Html.Encode("パターンあり")
Else
    @Html.Encode("パターンなし")
End If

