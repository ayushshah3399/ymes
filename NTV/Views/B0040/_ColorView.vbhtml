@ModelType IEnumerable(Of NTV_SHIFT.M0060)
@Code
    Dim needCol As Integer = 0
End Code

<style>
    .table-bordered.table-condensed tr {
        border: 1px solid black;
    }

    .table-bordered.table-condensed td {
        border: 1px solid black;
    }

    /*table-test tbody{
        display: block;
    }*/

    /*.colRyaku {
        width: 120px;
    }

    .colName {
        width: 350px;
    }*/
</style>


<table class="table table-bordered table-condensed">
    <tbody>
        <tr>
            @For i As Integer = 0 To Model.Count - 1
                Dim item = Model(i)

                If i < Model.Count / 2 Then
					@<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

						@Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
					</td>

					@<td class="colName">
						@Html.DisplayFor(Function(modelItem) item.KYUKNM)
					</td>
				End If

                @*If i = 0 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 1 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 2 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 3 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 4 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If*@

            Next
        </tr>


        <tr>
            @For i As Integer = 0 To Model.Count - 1
                Dim item = Model(i)
                needCol = 10 - Model.Count

                If i >= Model.Count / 2 Then
					@<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

						@Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
					</td>

					@<td class="colName">
						@Html.DisplayFor(Function(modelItem) item.KYUKNM)
					</td>
				End If

                @*If i = 5 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 6 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 7 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 8 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If

                If i = 9 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If


                If i = 10 Then
                @<td class="colRyaku" style="background-color:#@item.BACKCOLOR;  border: 2px solid #@item.WAKUCOLOR; color:#@item.FONTCOLOR;">

                    @Html.DisplayFor(Function(modelItem) item.KYUKRYKNM)
                </td>

                @<td class="colName">
                    @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                </td>
                End If*@
            Next

        </tr>
    </tbody>
</table>



