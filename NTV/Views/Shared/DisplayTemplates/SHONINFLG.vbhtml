@If Model = "0" Then
    @Html.Encode("未処理")
ElseIf Model = "1" Then
    @Html.Encode("承認")
ElseIf Model = "2" Then
@Html.Encode("却下")
End If




