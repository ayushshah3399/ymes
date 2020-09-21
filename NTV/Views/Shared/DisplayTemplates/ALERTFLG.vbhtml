@If Model = True Then
    @Html.Encode("送信する")
Else
    @Html.Encode("送信しない")
End If
