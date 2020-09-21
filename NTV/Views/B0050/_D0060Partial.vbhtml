@ModelType IEnumerable(Of NTV_SHIFT.D0060)
@Code
   
    'ViewData("Title") = "休日設定"
    Dim strLastMonthDate As String = DateTime.Now.AddMonths(-1).ToString("yyyy/MM")

    Dim neededWideCell As Boolean = false
    For i As Integer = 0 To Model.Count - 1
        Dim item = Model(i)
        If item.DESKMEMO = True Then
            neededWideCell = true
        End If
    Next

End Code

<style>
    /*ASI[11 Nov 2019]: all mytable, mytable2 and related css class used to make table and tbody width setting.
        if no desk memo exist then no need to display ! btn, so set width short otherwise set width large.
    */
      .mytable {
        width: 810px;
    }

      .mytable2 {
        width: 834px;
    }

    table.mytable tbody,
    table.mytable2 tbody,
    table.mytable thead,
    table.mytable2 thead{
        display: block;
    }

    table.mytable tbody {
        height: 120px;
        width: 810px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    table.mytable2 tbody {
        height: 120px;
        width: 834px;
        overflow-y: auto;
        overflow-x: hidden;
    }

    .colbtn1 {
        width: 85px;
    }

     .colbtn {
        width: 60px;
    }
       

    .colKKNST {
        width: 200px;
    }

    .colKKNST1, .colKKNST2 {
        width: 100px;
    }

    .colJKNST {
        width: 110px;
    }

    .colJKNST1, .colJKNST2 {
        width: 55px;
    }


      .colKYUKCD {
        width: 60px;
    }

    .colMemo {
        width: 150px;
        max-width: 150px;
        word-wrap: break-word;
    }

        .CellCommentPartial {
        display: none;
        position: relative;
        /*z-index: 50;*/
        border: 1px;
        background-color: white;
        border-style: solid;
        border-width: 1px;
        border-color: black;
        /*padding: 3px;*/
        color: black;
        top: 5px;
        left: 1px;
        width: 130px;
         word-wrap:break-word;
        height: auto;
        text-align: left;
    }

    .colMemo:hover span.CellCommentPartial {
        display: block;
    }

      
 </style>

@Using (Html.BeginForm("UpdateD0060", "B0050", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myFormPartial"}))
    @Html.AntiForgeryToken()
    @<div class="form-horizontal">

         <div class="row">
             @Html.Hidden("name", ViewData("name"))
             @Html.Hidden("userid", ViewData("id"))
             @Html.Hidden("showdate", ViewData("searchdt"))
             @*<label style="font-size:23px;">休暇申請 : 女子アナ３さん</label>*@
             <label style="font-size:18px;"> 休暇申請 : @ViewData("name")さん</label>
             &nbsp;<input id="updateD0060" type="submit" value="更新" class="btn btn-default btn-xs" />
             @*ASI[18 Oct 2019]: add nbsp and Lable for Error of Desk memo Exist*@
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <label id="Error" style="color:red; font-size:15px"></label>
             <table class="table table-bordered mytable" id="myTableData" style="font-size:13px">
                 <thead>
                     <tr>
                         @If neededWideCell Then
                            @<th class="colbtn1"></th>
                         Else
                            @<th class="colbtn"></th>
                         End If
                            
                         <th class="colKKNST">
                             @Html.DisplayNameFor(Function(model) model.KKNST)
                         </th>

                         <th class="colJKNST">
                             @Html.DisplayNameFor(Function(model) model.JKNST)
                         </th>

                         <th class="colKYUKCD">
                             種類
                         </th>
                         <th class="colMemo">
                             備考
                         </th>
                         <th class="colMemo">
                             管理者メモ
                         </th>
                         <th class="colbtn"></th>
                     </tr>
                 </thead>
                 <tbody>
                     @For i As Integer = 0 To Model.Count - 1

                             Dim item = Model(i)

                         @<tr>
                             @*ASI[11 Nov 2019]: width setting of td, based on desk memo existance for that date*@
                             @If neededWideCell Then
                                @<td class="colbtn1">

                                 @If item.SHONINFLG = "0" Then
                                         '<button id="shonin" class="btn btn-success btn-xs " onclick="Shonin('@(item.KYUKSNSCD)')">承認</button>
                                         '<input id="shonin" type="submit" value="承認" class="btn btn-default" />
                                    @<input type="button" class="btn btn-success btn-xs shonin" data-id="@(item.KYUKSNSCD)" value="承認" />
                                    @If item.DESKMEMO = True Then
                                    @Html.Encode(" | ")
                                    @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = item.GYOMSTDT, .CondShifted = item.GYOMEDDT, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank",.class="btn btn-danger btn-xs"})
                                    End If
                                 ElseIf item.SHONINFLG = "1" Then
                                     @<label style="color:green">承認済み</label>
                                 End If


                             </td>
                             Else
                                 @<td class="colbtn">

                                     @If item.SHONINFLG = "0" Then
                                             '<button id="shonin" class="btn btn-success btn-xs " onclick="Shonin('@(item.KYUKSNSCD)')">承認</button>
                                             '<input id="shonin" type="submit" value="承認" class="btn btn-default" />
                                        @<input type="button" class="btn btn-success btn-xs shonin" data-id="@(item.KYUKSNSCD)" value="承認" />
                                        @If item.DESKMEMO = True Then
                                        @Html.Encode(" | ")
                                        @Html.ActionLink("!", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = item.GYOMSTDT, .CondShifted = item.GYOMEDDT, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank",.class="btn btn-danger btn-xs"})
                                        End If
                                     ElseIf item.SHONINFLG = "1" Then
                                         @<label style="color:green">承認済み</label>
                                     End If


                                 </td>
                             End If

                             
                             <td class="colKKNST1">
                                 @Html.DisplayFor(Function(modelItem) item.KKNST)
                             </td>
                             <td class="colKKNST2">
                                 @Html.DisplayFor(Function(modelItem) item.KKNED)
                             </td>
                             <td class="colJKNST1">
                                 @Html.DisplayFor(Function(modelItem) item.JKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.JKNST).ToString.Substring(2, 2)

                             </td>
                             <td class="colJKNST2">

                                 @Html.DisplayFor(Function(modelItem) item.JKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.JKNED).ToString.Substring(2, 2)


                             </td>
                             <td class="colKYUKCD">
                                 @Html.DisplayFor(Function(modelItem) item.M0060.KYUKNM)
                             </td>

                             @Html.Hidden("lstD0060[" & i & "].KYUKSNSCD", item.KYUKSNSCD)

                             @*ASI[25 Dec 2019] : make GYOMMENO display only and Added KANRIMEMO text editor*@
                             <td class="colMemo">
                                 @Html.DisplayFor(Function(modelItem) item.GYOMMEMO)
                             </td>

                             <td class="colMemo">
                                 @Html.TextBox("lstD0060[" & i & "].KANRIMEMO", item.KANRIMEMO, htmlAttributes:=New With {.style = "width : 130px;", .id = "lstD0060_" & i & "__KANRIMEMO"})
                                 @If item.KANRIMEMO IsNot Nothing Then
                                     @<span class="CellCommentPartial" id="spanClass">@item.KANRIMEMO</span>
                                End If
                             </td>

                             <td class="colbtn">

                                 @If item.SHONINFLG = "0" Then
                                         '<button id="kyaka" class="btn btn-danger btn-xs " onclick="Kyaka('@(item.KYUKSNSCD)')">却下</button>
                                     @<input type="button" class="btn btn-danger btn-xs kyaka" data-id="@(item.KYUKSNSCD)" value="却下" />
                                 ElseIf item.SHONINFLG = "2" Then
                                     @<label style="color:red">却下済み</label>

                                 End If



                             </td>

                         </tr>
                     Next
                 <tr style="border-color:white">
                     <td colspan="8" style="border-color:white"></td>
                 </tr>
                 <tr style="border-color:white">
                     <td colspan="8" style="border-color:white"></td>
                 </tr>
                 </tbody>
             </table>


         </div>
    </div>
    
     

end using

@*<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>
<script type="text/javascript" src="~/Scripts/B0050.js"></script>*@

<script type="text/javascript">

    $(document).ready(function () {

        /*ASI[11 Nov 2019]: If desk memo is not exist then no need to display ! btn, so set width short otherwise set width large.
        neededWideCell variable declared in top of page.*/
        if ('@neededWideCell' == 'True'){
           var tbl = $("#myTableData");
            tbl.removeClass("mytable");
            tbl.addClass("mytable2");

        }

        $('.shonin').click(function () {
            //alert('reach')
            var value = $(this).attr("data-id");
            Shonin(value, this); //ASI[18 Oct 2019]: Pass 'this' as a parameter in Shohin function
        });


        $('.kyaka').click(function () {
            //alert('reach')
            var value = $(this).attr("data-id");
            Kyaka(value, this);
        });

    });

    function Shonin(value, btnObj) {
        //ASI[18 Oct 2019] START:To display Error Msg if DeskMemo Exist in between mentioned date range
        //ASI[07 Nov 2019] : logic change to display deskmemo button 
        /*var stopExecutionFlag = false;
        var kknst = btnObj.parentElement.parentElement.getElementsByTagName('td')[1].innerText;
        var kkned = btnObj.parentElement.parentElement.getElementsByTagName('td')[2].innerText;
        var jknst_timeString = btnObj.parentElement.parentElement.getElementsByTagName('td')[3].innerText;
        var jkned_timeString = btnObj.parentElement.parentElement.getElementsByTagName('td')[4].innerText;

        var jknst_timeDecimal = parseInt(jknst_timeString.split(':')[0] + jknst_timeString.split(':')[1]) + 0.01 ;
        var jkned_timeDecimal = parseInt(jkned_timeString.split(':')[0] + jkned_timeString.split(':')[1]) - 0.01 ;

        var userid = $('#userid').val();
        var name = $('#name').val();
        var showdate = $('#showdate').val();

        $.ajax({
            url: "@Url.Action("CheckDeskMemoExist", "B0050")",
            async: false,
            type: "POST",
            dataType: 'json',
            data: { kknst: kknst, kkned: kkned, jknst: jknst_timeDecimal, jkned: jkned_timeDecimal, name: name, userid: userid, showdate: showdate },
            cache: false,
            success: function (response) {
                if (response.DeskMemoExist) {
                    stopExecutionFlag = true;
                    $('#Error').text("デスクメモが登録されているため、承認できません");
                }
                else {
                    $('#Error').text("");
                }
            }
        });
        if (stopExecutionFlag == true) {
            return false
        }*/
        //ASI[18 Oct 2019] END

        var userid = $('#userid').val();
        var name = $('#name').val();
        var showdate = $('#showdate').val();
        var kanrimemo = $(btnObj).parent().parent().find("input[id$='__KANRIMEMO']")[0].value;

        $.ajax({
            url: "@Url.Action("CheckD00040", "B0050")",
                async: false,
                type: "POST",
            dataType: 'json',
            data: { id: value},

            cache: false,
            success: function (node) {
                if (node.success) {

                    if (node.text!= undefined) {
                        var result = confirm(node.text)
                        if (result == false) {
                            return false
                        }
                        $.ajax({
                            url: "@Url.Action("UpdateD0060Shonin", "B0050")",
                            async: false,
                            type: "POST",
                            dataType: 'json',
                            data: { id: value, name: name, userid: userid, showdate: showdate, kanrimemo: kanrimemo },
                            cache: false,
                            success: function (response) {
                                //alert(response.Url)
                                setTimeout(function () { document.location.href = response.Url; }, 250);
                                //document.location.href = response.Url;

                            },
                            error: function (response) {
                                alert("エラー:  " + response);
                            }
                        });
                    }



                //document.location.reload();
            } else {
                    alert(node.text);
        }
    }

    });

    }


    function Kyaka(value, btnObj) {

        var userid = $('#userid').val();
        var name = $('#name').val();
        var showdate = $('#showdate').val();
        var kanrimemo = $(btnObj).parent().parent().find("input[id$='__KANRIMEMO']")[0].value;

        var result = confirm("この申請を却下にします。よろしいですか?")

        if (result == false) {
            return false
        }



        $.ajax({
            url: "@Url.Action("UpdateD0060Kyaka", "B0050")",
            async: false,
            type: "POST",
        dataType: 'json',
        data: { kyuid: value, name: name, userid: userid, showdate: showdate, kanrimemo: kanrimemo },
        cache: false,
        success: function (response) {
            //alert(response.Url)
            //window.location.href = response.Url;
            //document.location.href = response.Url;
            setTimeout(function () { document.location.href = response.Url; }, 250);

        }
    });

    }



    $(function () {
        $('#updateD0060').click(function (e) {

            var table = document.getElementById('myTableData'),
             rows = table.getElementsByTagName('tr');
            $("#Error").text("")
            var strErr = '';
                
            var result = confirm("休暇申請を更新します。よろしいですか?")

            if (result == false) {
                return false
            }

            for (var i = 0; i < rows.length; i++) {

                var memo = "#lstD0060_" + i + "__KANRIMEMO"
                var memoval = $(memo).val();

                if (memoval != undefined) {

                    if (getByteCount(memoval) > 60) {
                        if (strErr != '') {
                            strErr = strErr + ',' + (i + 1);
                        }
                        else {
                            strErr = i + 1;
                        
                        }
                       
                       
                    }

                 
                }

            }
      
            if (strErr != '') {
                $("#Error").text(strErr + "行目の備考の文字数がオーバーしています。");
                document.getElementById('Error').style.color = 'red';
                return false
            }

            $("#myFormPartial").submit();
         

        });

    });

    function getByteCount(str) {
        var b = str.match(/[^\x00-\xff]/g);
        return (str.length + (!b ? 0 : b.length));
    }

    $("#myFormPartial :input").change(function () {
        var inputVal = $(this).val();     
        if (inputVal != '') {
            $("#myForm").data("MSG", true);
        }
        else {
            $("#myForm").data("MSG", false);
        }

    });
    </script>


    @*<script type="text/javascript">


        function Shonin(value) {


            $.ajax({
                url: "@Url.Action("CheckD00040", "B0050")",
                type: "POST",
                dataType: 'json',
                data: { id: value},

            cache: false,
            success: function (node) {
                if (node.success) {

                    if (node.text!== undefined) {
                        var result = confirm(node.text)
                        if (result == false) {
                            return false
                        }

                    }


                    $.ajax({
                        url: "@Url.Action("UpdateD0060Shonin", "B0050")",
                        type: "POST",
                    data: { id: value },
                    cache: false,
                    success: function (response) {
                        alert(response.Url)
                        window.location.href = response.Url;

                    }
                });

                    //document.location.reload();
                } else {
                    alert(node.text);
                }
            }

        });

        }


        function Kyaka(value) {

            var result = confirm("この申請を却下にします。よろしいですか?")

            if (result == false) {
                return false
            }

            $.ajax({
                url: "@Url.Action("UpdateD0060Kyaka", "B0050")",
                type: "POST",
            dataType: 'json',
            data: { kyuid: value },
            cache: false,
            success: function (node) {
                if (node.success) {
                    document.location.reload();
                } else {
                    alert("Fail");
                }
            }
            });

        }

    </script>*@