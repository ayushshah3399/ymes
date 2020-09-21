@ModelType IEnumerable(Of NTV_SHIFT.M0010)


<style>
    .table-scroll td {
        border: 1px solid black;
        color: blue;
    }

    .table-scroll th {
        border: 1px solid black;
        text-align: center;
    }


    table.table-scroll tbody,
    table.table-scroll thead {
        display: block;
    }



    table.table-scroll tbody {
        height: 650px;
        width: 800px;
        overflow-y: auto;
        overflow-x: hidden;
    }


    .table-scroll td {
        width: 21px;
    }

        .table-scroll td.td_head {
            width: 101px;
        }

     .table-scroll td.td-blank  {
            width : 5px;
            background-color:black;
        }

   
}
</style>

<table class="table-scroll">
    <thead>
        <tr>
            <th style="text-align:right; column-width:100px">日付</th>
            <th style="column-width:20px">1</th>
            <th style="column-width:20px">2</th>
            <th style="column-width:20px">3</th>
            <th style="column-width:20px">4</th>
            <th style="column-width:20px">5</th>
            <th style="column-width:20px">6</th>
            <th style="column-width:20px">7</th>
            <th style="column-width:20px">8</th>
            <th style="column-width:20px">9</th>
            <th style="column-width:20px">10</th>
            <th style="column-width:20px">11</th>
            <th style="column-width:20px">12</th>
            <th style="column-width:20px">13</th>
            <th style="column-width:20px">14</th>
            <th style="column-width:20px">15</th>

            <th style="column-width:4px; background-color:black; color:black;">.</th>
            <th style="column-width:20px">16</th>
            <th style="column-width:20px">17</th>
            <th style="column-width:20px">18</th>
            <th style="column-width:20px">19</th>
            <th style="column-width:20px">20</th>
            <th style="column-width:20px">21</th>
            <th style="column-width:20px">22</th>
            <th style="column-width:20px">23</th>
            <th style="column-width:20px">24</th>
            <th style="column-width:20px">25</th>
            <th style="column-width:20px">26</th>
            <th style="column-width:20px">27</th>
            <th style="column-width:20px">28</th>
            <th style="column-width:20px">29</th>
            <th style="column-width:20px">30</th>
            <th style="column-width:20px">31</th>
        </tr>

        <tr>
            <th style="text-align:right">曜日</th>
            <th>日</th>
            <th>月</th>
            <th>火</th>
            <th>水</th>
            <th>木</th>
            <th>金</th>
            <th>土</th>
            <th>日</th>
            <th>月</th>
            <th>火</th>
            <th>水</th>
            <th>木</th>
            <th>金</th>
            <th>土</th>
            <th>日</th>
            <th style="column-width:4px; background-color:black">.</th>
            <th>月</th>
            <th>火</th>
            <th>水</th>
            <th>木</th>
            <th>金</th>
            <th>土</th>
            <th>日</th>
            <th>月</th>
            <th>火</th>
            <th>水</th>
            <th>木</th>
            <th>金</th>
            <th>土</th>
            <th>日</th>
            <th>月</th>
            <th>火</th>


        </tr>

    </thead>

    <tbody>
@For Each item In Model
    @If item.USERSEX = False Then
        @If item.USERID.ToString.EndsWith("1") Then
            @<tr>
                <td class="td_head">
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td id="td-cat-2-de">出</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td class="td-blank"></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
            </tr>
        ElseIf item.USERID.ToString.EndsWith("2") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-2-de">出</td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td id="td-cat-2-de">出</td>
                <td id="td-cat-2-de">出</td>
                <td id="td-cat-2-de">出</td>
                <td></td>

                <td></td>
                <td class="td-blank"></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>

                <td id="td-cat-2-de">出</td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>

            </tr>
        ElseIf item.USERID.ToString.EndsWith("3") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>

                <td></td>
                <td></td>
                <td class="td-blank"></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>

            </tr>
        ElseIf item.USERID.ToString.EndsWith("4") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td class="td-blank"></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>

            </tr>

        ElseIf item.USERID.ToString.EndsWith("5") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>

                <td></td>
                <td class="td-blank"></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>


            </tr>
        ElseIf item.USERID.ToString.EndsWith("6") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-6-dai">代</td>
                <td></td>
                <td id="td-cat-4-kou">公</td>

                <td id="td-cat-4-kou">公</td>
                <td class="td-blank"></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
            </tr>


        ElseIf item.USERID.ToString.EndsWith("7") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td class="td-blank"></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>


            </tr>

        ElseIf item.USERID.ToString.EndsWith("8") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td class="td-blank"></td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
            </tr>

        ElseIf item.USERID.ToString.EndsWith("9") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td class="td-blank"></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>


            </tr>
        ElseIf item.USERID.ToString.EndsWith("0") Then

            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td class="td-blank"></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>


            </tr>



        Else

            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.USERNM)
                </td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td class="td-blank"></td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td id="td-cat-4-kou">公</td>
                <td id="td-cat-4-kou">公</td>
                <td></td>
            </tr>


        End If
    End If


Next
        @For Each item In Model
        @If item.USERSEX = True Then
        @If item.USERID.ToString.EndsWith("1") Then
        @<tr>
            <td class="td_head">
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td id="td-cat-2-de">出</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td class="td-blank"></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
        </tr>
                ElseIf item.USERID.ToString.EndsWith("2") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-2-de">出</td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td id="td-cat-2-de">出</td>
            <td id="td-cat-2-de">出</td>
            <td id="td-cat-2-de">出</td>
            <td></td>

            <td></td>
            <td class="td-blank"></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>

            <td id="td-cat-2-de">出</td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>

        </tr>
                ElseIf item.USERID.ToString.EndsWith("3") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>

            <td></td>
            <td></td>
            <td class="td-blank"></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>

        </tr>
                ElseIf item.USERID.ToString.EndsWith("4") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td class="td-blank"></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>

        </tr>

                ElseIf item.USERID.ToString.EndsWith("5") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>

            <td></td>
            <td class="td-blank"></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>


        </tr>
                ElseIf item.USERID.ToString.EndsWith("6") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-6-dai">代</td>
            <td></td>
            <td id="td-cat-4-kou">公</td>

            <td id="td-cat-4-kou">公</td>
            <td class="td-blank"></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
        </tr>


                ElseIf item.USERID.ToString.EndsWith("7") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td class="td-blank"></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>


        </tr>

                ElseIf item.USERID.ToString.EndsWith("8") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td class="td-blank"></td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
        </tr>

                ElseIf item.USERID.ToString.EndsWith("9") Then
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td class="td-blank"></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>


        </tr>
                ElseIf item.USERID.ToString.EndsWith("0") Then

        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td class="td-blank"></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>


        </tr>



                Else

        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.USERNM)
            </td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td class="td-blank"></td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td id="td-cat-4-kou">公</td>
            <td id="td-cat-4-kou">公</td>
            <td></td>
        </tr>


                End If
            End If


        Next
    </tbody>


</table>
