@ModelType IEnumerable(Of NTV_SHIFT.W0010)
@Code
    ViewData("Title") = "Index"
End Code


@*0:不適合要因無し 1:前半休 2:後半休  3:公休 4:休暇［代休］ 5:Wﾌﾞｯｷﾝｸﾞ 6:超過勤務 7:前後日10時間未満 8:強休*@


<h4>候補者一覧</h4>

<font style="background-color: red;">出</font>　休出　<font style="background-color: yellow;">※</font> 月間休日4日以下
<table class="table">

    <tr>
        <th>
            不適合要因無し
        </th>
    </tr>


    @For Each item In Model

        @If item.YOINID = "0" Then
            @<tr>
                <td>
                    &nbsp;  &nbsp; @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                    <button class="btn btn-success btn-xs" onclick="myCreateFunction(@Html.DisplayFor(Function(modelItem) item.M0010.USERNM))">選択</button> |
                    <button class="btn btn-success btn-xs">シフト</button> |
                    <button class="btn btn-success btn-xs">デスク</button>
                </td>
            </tr>
        End If

    Next



    <tr>
        <th>
            前半休
        </th>
    </tr>

    @For Each item In Model

        @If item.YOINID = "1" Then
            @<tr>
                <td>
                    &nbsp;  &nbsp;  @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                    <button class="btn btn-success btn-xs" onclick="myCreateFunction('item.M0010.USERNM')">選択</button> |
                    <button class="btn btn-success btn-xs">シフト</button> |
                    <button class="btn btn-success btn-xs">デスク</button>
                </td>
            </tr>
        End If

    Next


    <tr>
        <th>
            後半休
        </th>
    </tr>

    @For Each item In Model

        @If item.YOINID = "2" Then
            @<tr>
                <td>
                    &nbsp;  &nbsp;  @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                    <button class="btn btn-success btn-xs" onclick="myCreateFunction(item.M0010.USERNM)">選択</button> |
                    <button class="btn btn-success btn-xs">シフト</button> |
                    <button class="btn btn-success btn-xs">デスク</button>
                </td>
            </tr>
        End If

    Next


    <tr>
        <th>
            公休
        </th>
    </tr>

    @For Each item In Model

        @If item.YOINID = "3" Then
            @<tr>
                <td>
                    &nbsp;  &nbsp;  @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                    <button class="btn btn-success btn-xs" onclick="myCreateFunction(item.M0010.USERNM)">選択</button> |
                    <button class="btn btn-success btn-xs">シフト</button> |
                    <button class="btn btn-success btn-xs">デスク</button>
                </td>
            </tr>
        End If

    Next

    <tr>
        <th>
            休暇［代休］
        </th>
    </tr>

    @For Each item In Model

        @If item.YOINID = "4" Then
            @<tr>
                <td>
                    &nbsp;  &nbsp;  @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                    <button class="btn btn-success btn-xs" onclick="myCreateFunction(item.M0010.USERNM)">選択</button> |
                    <button class="btn btn-success btn-xs">シフト</button> |
                    <button class="btn btn-success btn-xs">デスク</button>
                </td>
            </tr>
        End If

    Next

    @*0:不適合要因無し 1:前半休 2:後半休  3:公休 4:休暇［代休］ 5:Wﾌﾞｯｷﾝｸﾞ 6:超過勤務 7:前後日10時間未満 8:強休*@

<tr>
    <th>
        Wﾌﾞｯｷﾝｸﾞ
    </th>
</tr>

@For Each item In Model

    @If item.YOINID = "5" Then
        @<tr>
            <td>
                &nbsp;  &nbsp; @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                <button class="btn btn-success btn-xs" onclick="myCreateFunction(item.M0010.USERNM)">選択</button> |
                <button class="btn btn-success btn-xs">シフト</button> |
                <button class="btn btn-success btn-xs">デスク</button>
            </td>
        </tr>
    End If

Next
    
<tr>
    <th>
        超過勤務
    </th>
</tr>

@For Each item In Model

    @If item.YOINID = "6" Then
        @<tr>
            <td>
                &nbsp;  &nbsp;  @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                <button class="btn btn-success btn-xs" onclick="myCreateFunction(item.M0010.USERNM)">選択</button> |
                <button class="btn btn-success btn-xs">シフト</button> |
                <button class="btn btn-success btn-xs">デスク</button>
            </td>
        </tr>
    End If

Next
    
<tr>
    <th>
        前後日10時間未満
    </th>
</tr>

@For Each item In Model

    @If item.YOINID = "7" Then
        @<tr>
            <td>
                &nbsp;  &nbsp;  @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                <button class="btn btn-success btn-xs" onclick="myCreateFunction(item.M0010.USERNM)">選択</button> |
                <button class="btn btn-success btn-xs">シフト</button> |
                <button class="btn btn-success btn-xs">デスク</button>
            </td>
        </tr>
    End If

Next
    

<tr>
    <th>
        強休
    </th>
</tr>

@For Each item In Model

    @If item.YOINID = "8" Then
        @<tr>
            <td>
                &nbsp;  &nbsp;  @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                <button class="btn btn-success btn-xs" onclick="myCreateFunction(item.M0010.USERNM)">選択</button> |
                <button class="btn btn-success btn-xs">シフト</button> |
                <button class="btn btn-success btn-xs">デスク</button>
            </td>
        </tr>
    End If

Next

</table>



