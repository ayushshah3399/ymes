@If Model = True Then
    @Html.Encode("出張")
Else
    @Html.Encode("非出張")
End If

