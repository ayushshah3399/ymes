@ModelType IEnumerable(Of NTV_SHIFT.D0050)
@Code
    ViewData("Title") = "業務承認"
    Layout = "~/Views/Shared/_Layout.vbhtml"
 
End Code



<style>
    .table-scroll {
        width: 1415px;
    }

    table.table-scroll tbody,
    table.table-scroll thead {
        display: inline-block;
    }

    table.table-scroll tbody {
        height: 450px;
        width: 1415px;
        overflow-y: auto;
        overflow-x: auto;
    }


    .colSelect, .colDelete {
        width: 50px;
    }

    .colUSERID {
        width: 80px;
    }


    .colGYOMYMD {
        width: 170px;
    }

    .colGYOMYMD1, .colGYOMYMD2 {
        width: 85px;
    }

    .colKSKJKNST {
        width: 96px;
    }

    .colKSKJKNST1, .colKSKJKNST2 {
        width: 45px;
    }



    .colCATNM {
        width: 110px;
    }

    .colBANGUMINM, .colNAIYO {
        width: 150px;
    }

    .colBASYO {
        width: 150px;
    }



    .colBANGUMITANTO {
        width: 110px;
    }

    .colBANGUMIRENRK {
        width: 130px;
    }

    .colGYOMMEMO {
        width: 150px;
    }
              
    body {
   font-size:12px;
   
}
</style>


<div class="container-fluid">
@Using Html.BeginForm("Index", "B0030", routeValues:=Nothing, method:=FormMethod.Get, htmlAttributes:=New With {.id = "B0030Index"})
    @<div class="row" style="padding-top:10px">
        <div class="col-md-4 col-md-push-8">
            <ul class="nav nav-pills ">
                <li><a href="#" onclick="$(this).closest('form').submit()">最新情報</a></li>
                <li><a href="#" id="EnDisColMsgBox1">伝言板表示/非表示</a></li>
                <li>@Html.ActionLink("戻る", "Index", "C0050")</li>
            </ul>
        </div>


        @*<div class="col-md-6 col-md-pull-6">
            <h4>業務承認</h4>
        </div>*@
    </div>
@Html.Hidden("msgShow", Session("B0030msgShow"))
End Using
     <div class="row" style="padding-top:10px">

         <div class="col-md-10">

             <table class="tablecontainer">
                 <tr>
                     <td>
                         <table class="table table-bordered table-hover table-scroll">
                             <thead>
                                 <tr>
                                     <th class="colSelect">
                                         選択
                                     </th>
                                     <th class="colDelete">

                                     </th>
                                     <th class="colUSERID">
                                         申請者
                                     </th>
                                     <th class="colCATNM">
                                         @Html.DisplayNameFor(Function(model) model.CATCD)
                                     </th>
                                     <th class="colBANGUMINM">
                                         @Html.DisplayNameFor(Function(model) model.BANGUMINM)
                                     </th>
                                     <th colspan="2" class="colGYOMYMD">
                                         @Html.DisplayNameFor(Function(model) model.GYOMYMD)
                                     </th>

                                     <th colspan="2" class="colKSKJKNST">
                                         @Html.DisplayNameFor(Function(model) model.KSKJKNST)
                                     </th>

                                     <th class="colNAIYO">
                                         @Html.DisplayNameFor(Function(model) model.NAIYO)
                                     </th>
                                     <th class="colBASYO">
                                         @Html.DisplayNameFor(Function(model) model.BASYO)
                                     </th>
                                     <th class="colBANGUMITANTO">
                                         @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
                                     </th>
                                     <th class="colBANGUMIRENRK">
                                         @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
                                     </th>
                                     <th class="colGYOMMEMO">
                                         @Html.DisplayNameFor(Function(model) model.GYOMMEMO)
                                     </th>

                                 </tr>
                             </thead>
                             <tbody>
                                 @For Each item In Model

                                     @<tr>
                                         <td class="colSelect">
                                             @*@Html.ActionLink("選択", "Create", "B0020")*@
                                             @Html.ActionLink("選択", "Create", "B0020", routeValues:=New With {.gyomsnsno = item.GYOMSNSNO}, htmlAttributes:=New With {.class = "btnSelect", .data_myid = item.GYOMSNSNO})

                                         </td>
                                         <td class="colDelete">

                                             @Html.ActionLink("削除", "Delete", New With {.id = item.GYOMSNSNO})
                                         </td>
                                         <td class="colUSERID">
                                             @Html.DisplayFor(Function(modelItem) item.M0010.USERNM)
                                         </td>
                                         <td class="colCATNM">
                                             @Html.DisplayFor(Function(modelItem) item.M0020.CATNM)
                                         </td>
                                         <td class="colBANGUMINM">
                                             @Html.DisplayFor(Function(modelItem) item.BANGUMINM)
                                         </td>
                                         <td class="colGYOMYMD1">
                                             @Html.DisplayFor(Function(modelItem) item.GYOMYMD)
                                         </td>
                                         <td class="colGYOMYMD2">
                                             @Html.DisplayFor(Function(modelItem) item.GYOMYMDED)
                                         </td>
                                         <td class="colKSKJKNST1">
                                             @Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNST).ToString.Substring(2, 2)
                                         </td>
                                         <td class="colKSKJKNST2">
                                             @Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(0, 2):@Html.DisplayFor(Function(modelItem) item.KSKJKNED).ToString.Substring(2, 2)
                                         </td>

                                         <td class="colNAIYO">
                                             @Html.DisplayFor(Function(modelItem) item.NAIYO)
                                         </td>
                                         <td class="colBASYO">
                                             @Html.DisplayFor(Function(modelItem) item.BASYO)
                                         </td>
                                         <td class="colBANGUMITANTO">
                                             @Html.DisplayFor(Function(modelItem) item.BANGUMITANTO)
                                         </td>
                                         <td class="colBANGUMIRENRK">
                                             @Html.DisplayFor(Function(modelItem) item.BANGUMIRENRK)
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

         <!-- 伝言板 -->
         @If Session("B0030msgShow") = "hide" Then
             @<div Class="col-md-2 col-md-offset-8 affix " id="ColMsgBox" style="background-color:lavender;width:320px;height:450px; overflow-y:scroll;display:none;">
                 @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                 @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
             </div>
         Else
             @<div Class="col-md-2 col-md-offset-8 affix " id="ColMsgBox" style="background-color:lavender;width:320px;height:450px; overflow-y:scroll;">
                 @Html.Partial("_D0080Partial", ViewData.Item("Message"))
                 @Html.Partial("ShowMessage", ViewData.Item("MessageList"))
             </div>
         End If

     </div>

</div>



<script>
    
    $('.btnSelect').click(function (e) {
           
         var value = $(this).attr("data-myid");
        $.ajax({
            url: "@Url.Action("CheckD0050", "B0030")",
            async: false,
            type: "POST",
            dataType: 'json',
            data: { id: value },

            cache: false,
            success: function (node) {
            if (node.success) {
               
            } else {                               
                alert(node.text);
                e.preventDefault();
                
            }
        }


    });
    });

    $('#EnDisColMsgBox1').on('click', function (e) {
       
        if ($("#ColMsgBox").is(':hidden')) {
            
            $("#ColMsgBox").show(); 
            $("#msgShow").val("show");
        }
        else {
          
            $("#ColMsgBox").hide();
            $("#msgShow").val("hide");
        }
        $("body").css("cursor", "progress");

        //非表示にしたら、ログオフするまで非表示にするため、submitしController側で現在の設定を保存している
        $("#B0030Index").submit();

    });


    $(document).ready(function () {
      
        //伝言板非表示で検索する時は伝言板非表示のままにしたいため       
        if ($("#msgShow").val() == 'hide') {
            $("#ColMsgBox").hide();
        }
           
        else {
            $("#ColMsgBox").show();
        }

    });

</script>



