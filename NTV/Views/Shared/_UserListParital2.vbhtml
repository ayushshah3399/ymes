@ModelType IEnumerable(Of NTV_SHIFT.M0010)

<div class="container" style="margin:0px; padding:0px;">
    <div class="row">
        <div class="col-sm-1">
            <table class="table" style="background-color:aliceblue" border="1">
                @*<tr>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.USERNM)
                        </th>
                    </tr>*@

                @For Each item In Model
                    @If item.USERSEX = False Then
                        @<tr>
                            <td>
                                @Html.DisplayFor(Function(modelItem) item.USERNM)
                            </td>
                        </tr>
                    End If
                Next

            </table>

        </div>
        <div class="col-sm-1">
            <table class="table" style="background-color:aliceblue" border="1">
                @*<tr>

                        <th>
                            @Html.DisplayNameFor(Function(model) model.USERNM)
                        </th>

                    </tr>*@

                @For Each item In Model
                    @If item.USERSEX = True Then
                        @<tr>
                            <td>
                                @Html.DisplayFor(Function(modelItem) item.USERNM)
                            </td>
                        </tr>
                    End If


                Next

            </table>

        </div>
    </div>
</div>

















