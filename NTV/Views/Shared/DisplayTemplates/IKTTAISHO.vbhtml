@If Model = True Then
    @Html.Encode("対象")
Else
    @Html.Encode("対象外")
End If

