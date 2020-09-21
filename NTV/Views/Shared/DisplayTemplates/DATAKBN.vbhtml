@*1:個人、2:共有*@

@If Model = "1" Then
    @Html.Encode("個人")
Else
    @Html.Encode("共有")
End If