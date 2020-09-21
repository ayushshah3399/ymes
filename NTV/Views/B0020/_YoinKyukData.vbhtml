@ModelType IEnumerable(Of NTV_SHIFT.W0020)

<div class="modal-header">
    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
    <div>
        <label class="control-label">担当アナ : </label>
        <label class="control-label" id="lblusernm">@ViewBag.Usernm</label>
    </div>
    <h4 class="modal-title">@ViewBag.Title </h4>
</div>

<div class="modal-body">
    @Html.Hidden("yoinid", ViewBag.Yoinid)
    @Html.Hidden("lastyoinflg", ViewBag.LastYoinFlg)

    <div class="panel panel-default">

        <table class="tbllayout" style="margin:10px;">

            @For Each item In Model
                @<tr>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.KYUKYMD)

                        @If item.KYUKDAY = 1 Then
                            @Html.Encode("（日曜日）")

                        ElseIf item.KYUKDAY = 2 Then
                            @Html.Encode("（月曜日）")

                        ElseIf item.KYUKDAY = 3 Then
                            @Html.Encode("（火曜日）")

                        ElseIf item.KYUKDAY = 4 Then
                            @Html.Encode("（水曜日）")

                        ElseIf item.KYUKDAY = 5 Then
                            @Html.Encode("（木曜日）")

                        ElseIf item.KYUKDAY = 6 Then
                            @Html.Encode("（金曜日）")

                        ElseIf item.KYUKDAY = 7 Then
                            @Html.Encode("（土曜日）")
                        End If


                        @If item.YOINID = 3 Then
                            @Html.Encode("［時間休］")

                        ElseIf item.YOINID = 5 Then
                            @Html.Encode("［公休］")

                        ElseIf item.YOINID = 13 Then
                            @Html.Encode("［法休］")

                        ElseIf item.YOINID = 6 Then
                            @Html.Encode("［代休］")

                        ElseIf item.YOINID = 7 Then
                            @Html.Encode("［振公休］")

                        ElseIf item.YOINID = 14 Then
                            @Html.Encode("［振法休］")

                        ElseIf item.YOINID = 11 Then
                            '24時超え休出で公休、振休、代休、強休をメッセージ分けて出す
                            @If item.KYUKCD = 5 Then
                                @Html.Encode("［公休］")
                            ElseIf item.KYUKCD = 7 then
                                @Html.Encode("［振公休］")
                            ElseIf item.KYUKCD = 6 then
                                 @Html.Encode("［代休］")
                            Else
                                @Html.Encode("［強休］")
                            End If

                        ElseIf item.YOINID = 15 Then
                        '24時超法休出で法休、振法、代休、強休をメッセージ分けて出す
                            @If item.KYUKCD = 13 Then
                                @Html.Encode("［法休］")
                            ElseIf item.KYUKCD = 14 then
                                @Html.Encode("［振法休］")
                            ElseIf item.KYUKCD = 6 then
                                 @Html.Encode("［代休］")
                            Else
                                @Html.Encode("［強休］")
                            End If

                ElseIf item.YOINID = 0 Then
                        '不適合要因無しで翌日が時間休、時間強休のどれかだったらメッセージ出す
                            @If item.KYUKCD = 3 Then
                                @Html.Encode("［時間休］")

                            ElseIf item.KYUKCD = 9 Then
                                @Html.Encode("［時間強休］")                         
                                                
                            End If
                        End If

                        
                        @If item.YOINID = 3 OrElse item.YOINID = 4 OrElse item.KYUKCD = 3 OrElse item.KYUKCD = 9 Then
                            @Html.Encode("　　")
                            @item.JKNST.ToString.Substring(0, 2) @Html.Encode(":") @item.JKNST.ToString.Substring(2, 2)
                            @Html.Encode("～")
                            @item.JKNED.ToString.Substring(0, 2) @Html.Encode(":") @item.JKNED.ToString.Substring(2, 2)
                        End If
                    </td>



                </tr>
            Next

        </table>

    </div>

    <div class="message">
        @Html.Raw(ViewBag.Message.ToString.Replace(vbCrLf, "<br />"))
    </div>

</div>

@*<div>
        @Html.Raw(Html.Encode(ViewBag.Message).Replace(vbCrLf, "<br />"))
        @Html.Hidden("title", ViewBag.Title)
    </div>*@
