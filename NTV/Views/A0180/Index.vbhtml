@ModelType IEnumerable(Of NTV_SHIFT.D0150)
@Code
    ViewData("Title") = "休暇申請履歴"
    Dim info = ViewBag.SortingPagingInfo
End Code

<div>
    @Html.Partial("_MenuPartial", "8")
</div>

<style>
    /*.table-bordered th {
        white-space: nowrap;
    }

    .table-bordered td {
        white-space: nowrap;
    }*/

    .table-scroll {
        width: 1135px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 400px;
        width: 1135px;
        overflow-y: auto;
        overflow-x: auto;
    }




    .colShonin {
        width: 50px;
    }

    .colShoninSha {
        width: 200px;
    }

    .colShoninHi {
        width: 200px;
    }

    .colDate {
        width: 170px;
    }

    .colDate1, .colDate2 {
        width: 85px;
    }

    .colTime {
        width:  96px;
    }

    .colTime1, .colTime2 {
        width: 45px;
    }

    .colUserid {
        width: 150px;
    }

        .colKYUKNM {
        width: 100px;
    }

   
    .colGYOMMEMO {
        width: 150px;
    }

  
     body {
   font-size:12px;
   
}
</style>

<div class="container-fluid">
    @*<div class="row">
        <div class="col-md-12 col-md-push-5" style="padding-top:10px">
            <label style="font-size:15px;">※8日以前の休暇申請の履歴はありません。</label>
        </div>

    </div>*@

    <div class="row">
        @Using Html.BeginForm("Index", "A0180", FormMethod.Get, htmlAttributes:=New With {.id = "myForm"})
    @Html.Hidden("SortField", info.SortField)
    @Html.Hidden("SortDirection", info.SortDirection)
    @Html.Hidden("viewdatadate", ViewData("searchdt"))
            
            @<p>

                 <div class="col-md-4" style="padding-top:5px;">

                     <label class="radio-inline">
                         @Html.RadioButton("Kyuka", "True", True)
                         <label>休暇日で検索&nbsp;</label>
                     </label>
                     <label class="radio-inline">
                         @Html.RadioButton("Kyuka", "False")
                         <label>承認日付で検索&nbsp;</label>
                     </label>

                 </div>
                <ul class="nav nav-pills ">

                   @* <li><a href="#" onclick="SetDate(-1)">前日</a></li>*@
                    <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(-1)">前日</button></li>
                    <li>
                        <div class="input-group">
                            <input id="searchdt" name="searchdt" type="text" class="form-control input-sm date imedisabled" value=@ViewData("searchdt") onchange="KeyUpFunction()" style="width:120px;font-size:small;">

                        </div>
                    </li>
                    @*<li><a href="#" onclick="SetDate(1)">翌日</a></li>*@
                    <li><button type="submit" class="btn btn-success btn-sm" style="background:white; color:green" onclick="SetDate(1)">翌日</button></li>
                    <li><button type="submit" class="btn btn-success btn-sm">表示</button></li>
                    <li><label style="font-size:15px; padding-left:20px; padding-top:5px;">※8日以前の休暇申請の履歴はありません。</label></li>
</ul>

            </p>

        End Using
    </div>


    <div class="row">

        <table class="tablecontainer">
            <tr>
                <td>
                    <table id="tblSearchResult"  class="table table-bordered table-hover table-scroll">
                        <thead>
                            <tr>

                                <th class="colShonin">
                                    <a href="#" data-sortfield="HENKONAIYO"
                                       class="header">確認</a>
                                </th>
                                <th class="colShoninSha">
                                    <a href="#" data-sortfield="USERNM"
                                       class="header">承認者</a>
                                </th>
                                <th class="colShoninHi">
                                    <a href="#" data-sortfield="UPDTDT"
                                       class="header">承認日付</a>
                                </th>

                                <th class="colDate">
                                    <a href="#" data-sortfield="KKNST"
                                       class="header">期間</a>
                                </th>
                                <th class="colTime">
                                    <a href="#" data-sortfield="JKNST"
                                       class="header">時間</a>
                                </th>

                                <th class="colUserid">
                                    <a href="#" data-sortfield="SHINSEIUSER"
                                       class="header">申請者</a>
                                </th>
                                <th class="colKYUKNM">
                                    <a href="#" data-sortfield="KYUKNM"
                                       class="header">@Html.DisplayNameFor(Function(model) model.KYUKNM)</a>
                                </th>

                                <th class="colGYOMMEMO">
                                    <a href="#" data-sortfield="GYOMMEMO"
                                       class="header">@Html.DisplayNameFor(Function(model) model.GYOMMEMO)</a>
                                </th>

                            </tr>
                        </thead>

                        <tbody>

                            @For Each item In Model
                                @<tr>

                                    <td class="colShonin">
                                        @Html.DisplayFor(Function(modelItem) item.HENKONAIYO)
                                    </td>

                                    <td class="colShoninSha">
                                        @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                                    </td>

                                    <td class="colShoninHi">
                                        @Html.DisplayFor(Function(modelItem) item.UPDTDT)
                                    </td>
                                    <td class="colDate1">
                                        @Html.DisplayFor(Function(modelItem) item.KKNST)
                                    </td>
                                    <td class="colDate2">
                                        @Html.DisplayFor(Function(modelItem) item.KKNED)
                                    </td>
                                    <td class="colTime1">
                                        @*@Html.DisplayFor(Function(modelItem) item.JKNST)*@
                                        @Html.DisplayFor(Function(modelItem) item.JKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.JKNST).ToString.Substring(2, 2)
                                    </td>
                                    <td class="colTime2">
                                        @*@Html.DisplayFor(Function(modelItem) item.JKNED)*@
                                        @Html.DisplayFor(Function(modelItem) item.JKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.JKNED).ToString.Substring(2, 2)
                                    </td>
                                    <td class="colUserid">
                                        @Html.DisplayFor(Function(modelItem) item.SHINSEIUSER)
                                    </td>
                                    <td class="colKYUKNM">
                                        @Html.DisplayFor(Function(modelItem) item.KYUKNM)
                                    </td>
                                    <td class="colGYOMMEMO">
                                        @Html.DisplayFor(Function(modelItem) item.GYOMMEMO)
                                    </td>



                                </tr>
                            Next
                        </tbody>
                    </table>

                </td>
            </tr>

        </table>

    </div>

    </div>
  

    <script type="text/javascript">

        function SetDate(days) {
            var curdates = $('#searchdt').val().split('/');
            var newdate = new Date(curdates[0], curdates[1] - 1, curdates[2]);
            newdate.setDate(newdate.getDate() + days);
            var formattedNewDate = newdate.getFullYear() + '/' + ('0' + (newdate.getMonth() + 1)).slice(-2) + '/' + ('0' + newdate.getDate()).slice(-2);
            $('#searchdt').val(formattedNewDate);
        }


        $(document).ready(function () {
         $(".header").click(function (evt) {

                var table = document.getElementById("tblSearchResult");
                var rows = table.getElementsByTagName("tr");

                if (rows.length < 3) {
                    return
                }

                var sortfield = $(evt.target).data("sortfield");

                if ($("#SortField").val() == sortfield) {
                    if ($("#SortDirection").val() == "ascending") {
                        $("#SortDirection").val("descending");
                    }
                    else {
                        $("#SortDirection").val("ascending");
                    }
                }
                else {
                    $("#SortField").val(sortfield);
                    $("#SortDirection").val("ascending");
                }
                evt.preventDefault();

                $("#myForm").submit();
            });
        });


        function KeyUpFunction() {

            var searchdt = $('#searchdt').val()
            var viewdate = $('#viewdatadate').val()

            if (searchdt != "") {

                if (searchdt.length == 10) {

                    if (searchdt != viewdate) {
                     
                        $("#myForm").submit();
                    }

                }
            }

        }


    </script>
