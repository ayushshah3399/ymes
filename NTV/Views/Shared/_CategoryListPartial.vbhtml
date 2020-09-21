@ModelType IEnumerable(Of NTV_SHIFT.M0060)

    


<div class="well well-sm">
    @*<table style="text-align:center; width:100%;">
        <tr>
            <th style="text-align:center">凡例</th>
        </tr>
        <tr>
            <td>
                <table class="table-bordered" style="text-align:center; width:100%;border-collapse:separate;">

                    <tr>
                        <td id="td-cat-1-kin">勤務</td>
                        <td id="td-cat-2-de">休出</td>
                    </tr>
                    <tr>
                        <td id="td-cat-3-cho">出張</td>
                        <td id="td-cat-4-kou">公休</td>
                    </tr>
                    <tr>
                        <td id="td-cat-5-furi">振休</td>
                        <td id="td-cat-6-dai">代休</td>
                    </tr>
                    <tr>
                        <td id="td-cat-7-ji">時間休</td>
                        <td id="td-cat-8-kyo">強休</td>
                    </tr>
                    <tr>
                        <td id="td-cat-9-jikyo">時強休</td>
                        <td id="td-cat-10-24cho">24超休</td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>*@

    <table style="text-align:center; width:100%;">
        <tr>
            <th style="text-align:center">凡例</th>
        </tr>
        <tr>
            <td>
                <table class="table-bordered" style="text-align:center; width:100%;border-collapse:separate;">

                    <tr>
                       @For i As Integer = 0 To Model.Count - 1
                           Dim item = Model(i)

                            @If i = 0 Then
                                @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                                </td>


                            ElseIf i = 1 Then

                               @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                                </td>



                            End If

                        Next
                    </tr>

                    <tr>
                        @For i As Integer = 0 To Model.Count - 1
                            Dim item = Model(i)

                            If i = 2 Then
                           @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>

                            ElseIf i = 3 Then
                           @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>


                            End If

                        Next
                    </tr>
                    <tr>
                       @For i As Integer = 0 To Model.Count - 1
                           Dim item = Model(i)

                           If i = 4 Then
                                  @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                        @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                                    </td>

                           ElseIf i = 5 Then
                               @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                                </td>

                            End If

                        Next
                    </tr>


                    <tr>
                       @For i As Integer = 0 To Model.Count - 1
                           Dim item = Model(i)

                           If i = 6 Then
                           @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>


                           ElseIf i = 7 Then
                           @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>


                            End If


                        Next
                    </tr>

                    <tr>
                       @For i As Integer = 0 To Model.Count - 1
                           Dim item = Model(i)

                           If i = 8 Then
                              @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>

                           ElseIf i = 9 Then
                            @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>


                            End If


                       Next
                          
                           
                    </tr>

                    @*ASI[11 Nov 2019] START: added desing entry for leave type 時強休,24時超休出,24時超公休出 *@
                    <tr>
                       @For i As Integer = 0 To Model.Count - 1
                           Dim item = Model(i)

                           If i = 10 Then
                              @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>

                           ElseIf i = 11 Then
                            @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>


                            End If


                       Next
                          
                           
                    </tr>
                    <tr>
                       @For i As Integer = 0 To Model.Count - 1
                           Dim item = Model(i)

                           If i = 12 Then
                              @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>
                            ElseIf i = 13 Then
                            @<td style="background-color:#@item.BACKCOLOR; border-color:#@item.WAKUCOLOR; color:#@item.FONTCOLOR ">

                                @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                            </td>
                            End If
                       Next
                    </tr>
                    @*ASI[11 Nov 2019] END*@

                    <tr>

                        <td style="background-color:white; border-color:black; color:gray ">

                            未展開
                        </td>
                        </tr>
                </table>

            </td>
        </tr>
    </table>
</div>

