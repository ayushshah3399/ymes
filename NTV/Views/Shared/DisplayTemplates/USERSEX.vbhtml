
@*@Model boolean*@

@If Model = True Then
   @Html.Encode("女")
Else
  @Html.Encode("男")
End If



