@ModelType IEnumerable(Of NTV_SHIFT.D0060)
@Code
    ViewData("Title") = "Index"
End Code

<div class="row">
    <div class="col-md-10">

        <ul class="nav nav-pills navbar-right">
            <li>@Html.ActionLink("休日設定", "Index", "B0050")</li>
        </ul>
    </div>
</div>

<div class="row">

    <div class="col-md-8">
        @*<label style="font-size:23px;">休暇申請 : 女子アナ３さん</label>*@
        <label style="font-size:23px;"> 休暇申請 : @Html.Encode(ViewData("name"))さん</label>
        <table class="table table-bordered">
            <tr>
                <th></th>
                <th colspan="2">
                    @Html.DisplayNameFor(Function(model) model.KKNST)
                </th>

                <th colspan="2">
                    @Html.DisplayNameFor(Function(model) model.JKNST)
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.KYUKCD)
                </th>
                <th></th>
            </tr>

            @For Each item In Model
                @<tr>
                     <td>

                         @Html.ActionLink("承認", "Delete", New With {.id = item.KYUKSNSCD})
                     </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.KKNST)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.KKNED)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.JKNST)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.JKNED)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.M0060.KYUKNM)
                    </td>
                     <td>

                         @Html.ActionLink("却下", "Delete", New With {.id = item.KYUKSNSCD})
                     </td>

                </tr>
            Next


        </table>
    </div>

</div>
