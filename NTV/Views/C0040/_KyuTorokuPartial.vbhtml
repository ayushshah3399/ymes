﻿@ModelType NTV_SHIFT.D0060

@code 
    Dim strVisibility As String = "hidden"
    @*If ViewBag.KYUKCDforPartialView IsNot Nothing Then
         Dim KYUKCDList = DirectCast(ViewBag.KYUKCDforPartialView, List(Of M0060))
    End If*@
End Code

@Using (Html.BeginForm("CreateD0040", "C0040", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "kyukTorokuForm"}))
    @Html.AntiForgeryToken()

@<div class="form-horizontal" style="background-color:whitesmoke">
     <div class="form-group">
         <div class=" col-md-11 col-md-offset-1">
             <h5><b>時間休登録</b></h5>
         </div>
     </div>
     @Html.Hidden("name", ViewData("name"))
     @Html.Hidden("id", ViewData("id"))
     @Html.Hidden("searchdt", ViewData("searchdt"))


    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
  
  
     <div class="form-group">
         <div class="form-inline">
             @Html.LabelFor(Function(model) model.KKNST, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
             <div class="col-md-9">
                 @Html.TextBox("KKNST", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .id = "torokuKKNST"})              
                
                
                 <label class="control-label">～</label>
                 @Html.TextBox("KKNED", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled", .id = "torokuKKNED"})              
                
                
                 <div id="ErrorKKNST"></div>
             </div>
         </div>
         <div class=" col-md-offset-3 col-md-9">
             <span class="field-validation-valid text-danger" id="torokuKKNST" data-valmsg-replace="true" data-valmsg-for="torokuKKNST"></span>
             <span class="field-validation-valid text-danger" id="torokuKKNED" data-valmsg-replace="true" data-valmsg-for="torokuKKNED"></span>
         </div>
     </div>
       
     <div class="form-group">
         <div class="form-inline">
             @Html.LabelFor(Function(model) model.JKNST, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
             <div class="col-md-9">
                 @Html.EditorFor(Function(model) model.JKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm timecustom imedisabled", .id = "torokuJKNST"}})
                
                 <label class="control-label">～</label>
                 @Html.EditorFor(Function(model) model.JKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm timecustom imedisabled", .id = "torokuJKNED"}})
                
                 <div id="Error"></div>
                 <div id="Error24Over1"></div>
                
             </div>
         </div>
         <div class=" col-md-offset-3 col-md-9">
             <span class="field-validation-valid text-danger" id="torokuJKNST" data-valmsg-replace="true" data-valmsg-for="torokuJKNST"></span>
             <span class="field-validation-valid text-danger" id="torokuJKNED" data-valmsg-replace="true" data-valmsg-for="torokuJKNED"></span>
         </div>
     </div>

    <!--<div class="form-group">
        @Html.LabelFor(Function(model) model.KYUKCD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
        <div class="col-md-9">
            @*@Html.DropDownList("KYUKCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})*@
            @Html.DropDownList("KYUKCD", New SelectList(ViewBag.KYUKCDforPartialView, "KYUKCD", "KYUKNM", ""), htmlAttributes:=New With {.class = "form-control input-sm", .id="torokuKYUKCD"})
            <div id="Errorkyucd"></div>
        </div>
        <div class=" col-md-offset-3 col-md-9">
            <span class="field-validation-valid text-danger" id="torokuKYUKCD" data-valmsg-replace="true" data-valmsg-for="torokuKYUKCD"></span>
        </div>
    </div>-->
     @Html.Hidden("KYUKCD", 7)

    <div class="form-group">
        @Html.LabelFor(Function(model) model.GYOMMEMO, htmlAttributes:=New With {.class = "control-label col-md-3"})
        <div class="col-md-9">
            @Html.EditorFor(Function(model) model.GYOMMEMO, New With {.htmlAttributes = New With {.class = "form-control input-sm", .id = "torokuKYUGYOMMEMO"}})
            
        </div>
        <div class=" col-md-offset-3 col-md-9">            
            <span class="field-validation-valid text-danger" id="torokuKYUGYOMMEMO" data-valmsg-replace="true" data-valmsg-for="torokuKYUGYOMMEMO"></span>
        </div>
    </div>
     
     <div class="form-group" id="divPattern">
         @Html.LabelFor(Function(model) model.PATTERN, htmlAttributes:=New With {.class = "control-label col-md-3"})
         <div class="col-md-9">
             <div class="checkbox">
                 <label>@Html.EditorFor(Function(model) model.PATTERN, New With {.htmlAttributes = New With {.id = "chkPattern1"}}) 繰り返し登録</label>
                 @Html.ValidationMessageFor(Function(model) model.PATTERN, "", New With {.class = "text-danger"})
             </div>
         </div>
     </div>

     @If Model IsNot Nothing AndAlso Model.PATTERN = True Then
             strVisibility = "visible"
         End If

     <div class="form-group" id="pcheckbox" style="visibility:@strVisibility">
         <div class="col-md-offset-3 col-md-9">
             <lable class="checkbox-inline">
                 @Html.EditorFor(Function(model) model.MON)月曜
             </lable>
             <label class="checkbox-inline">
                 @Html.EditorFor(Function(model) model.TUE)火曜
             </label>
             <label class="checkbox-inline">
                 @Html.EditorFor(Function(model) model.WED)水曜
             </label>
             <lable class="checkbox-inline">
                 @Html.EditorFor(Function(model) model.TUR)木曜
             </lable>
             <label class="checkbox-inline">
                 @Html.EditorFor(Function(model) model.FRI)金曜
             </label>
             <label class="checkbox-inline">
                 @Html.EditorFor(Function(model) model.SAT)土曜
             </label>
             <label class="checkbox-inline">
                 @Html.EditorFor(Function(model) model.SUN)日曜
             </label>
         </div>
     </div>
     <div class="form-group">
         <div class="col-md-offset-3 col-md-9">
             <span class="field-validation-valid text-danger" id="patternMon" ></span>
             
         </div>
     </div>

     <div id="divWarning" class="text-warning" style="height:0px">
         @Html.Hidden("warning", ViewData("warning"))
     </div>
      
     <div class="form-group">
         <div class="col-md-offset-3 col-md-9">
             <input id="updateKyuToroku" type="submit" value="登録" class="btn btn-default" />
             
         </div>
     </div>
     

</div>


End Using
<script type="text/javascript">
   
    ////繰り返し登録チェックの時
    //function ShowDaysChkbox(checkboxElem) {
    //    var div = document.getElementById('checkbox');
    //    if (checkboxElem.checked) {
    //        div.style.visibility = 'visible';
    //    } else {
    //        div.style.visibility = 'hidden';
    //    }
    //}

    $(document).ready(function () {

        document.getElementById("torokuKKNST").value = "";
        document.getElementById("torokuKKNED").value = "";

        $('#chkPattern1').change(function () {
            var div = document.getElementById('pcheckbox');
            if (this.checked) {
                div.style.visibility = 'visible';
            }
            else {
                div.style.visibility = 'hidden';
            }
        });
    });


   

    function myCreateFunction() {
       
        var msg = '';
        //var kyukacd = $('#torokuKYUKCD').val();
        var kskjknst = $('#torokuJKNST').val();
        var kskjkned = $('#torokuJKNED').val();
       
        //if (kyukacd == '7' || kyukacd == '9') {
            if (kskjknst == '' || kskjkned == '') {

                msg = '拘束時間';
            }
        //}


        if (msg != '') {
            alert('以下の項目を入力してください。\n' + msg);
            return false
        }
    }

    $(function () {

        $('#updateKyuToroku').click(function (e) {

            var gyomymd = $('#torokuKKNST').val();
            var gyomymded = $('#torokuKKNED').val();
            var id = $('#id').val();
            var jknst = $('#torokuJKNST').val();
            var jkned = $('#torokuJKNED').val();
			//var torokuKYUKCD = $('#torokuKYUKCD').val();
            var pattern = $('#divPattern #chkPattern1').prop("checked");
            var mon = $('#pcheckbox #MON').prop("checked");
            var tue = $('#pcheckbox #TUE').prop("checked");
            var wed = $('#pcheckbox #WED').prop("checked");
            var tur = $('#pcheckbox #TUR').prop("checked");
            var fri = $('#pcheckbox #FRI').prop("checked");
            var sat = $('#pcheckbox #SAT').prop("checked");
            var sun = $('#pcheckbox #SUN').prop("checked");

            e.preventDefault();
            var url = myApp.Urls.baseUrl + 'C0040/SearchKYUTOROKU/?gyomymd=' + gyomymd + '&gyomymded=' + gyomymded + '&id=' + id + '&jknst=' + jknst + '&jkned=' + jkned +
                '&pattern=' + pattern + '&mon=' + mon + '&tue=' + tue + '&wed=' + wed + '&tur=' + tur + '&fri=' + fri + '&sat=' + sat + '&sun=' + sun  ;

            $("#divWarning").load(url, function (response, status, xhr) {

                if (status == "success") {

                    var msgwarning = jQuery.trim($('#warning').val());

                    if (msgwarning == 2) {

                        alert("公休展開していない月の申請です。デスクにご連絡ください。")
                        return false;
                    }
					else if (msgwarning == 3) {
                        alert("その時間帯は業務が登録されています。")						
                        return false;
                    }
                    else if (msgwarning == 4) {
                        alert("この日の時間休登録はデスクに問い合わせて下さい。");
                        return false;
                    }
                    else if (msgwarning == 5) {
                        alert("休日に対して、時間休登録できません。");
                        return false;
                    }
                    else if (msgwarning == 6) {
                        alert("他の時間休、又は時強休と重複するため、登録できません。");
                        return false;
                    }
                    else {
                        var result = confirm("時間休登録を行います。よろしいですか？")

                        if (result == false) {
                            return false
                        }
                        var err = '';
                        var kskjknst = $('#torokuJKNST').val();
                        var kskjkned = $('#torokuJKNED').val();
                        //var kyukacd = $('#torokuKYUKCD :selected').text();
                        //var kyukacdcode = $('#torokuKYUKCD').val();                                              

                        $('div span[data-valmsg-for="torokuKKNST"]').text("");
                        $('div span[data-valmsg-for="torokuKKNED"]').text("");
                        $('div span[data-valmsg-for="torokuJKNST"]').text("");
                        $('div span[data-valmsg-for="torokuJKNED"]').text("");
                        //$('div span[data-valmsg-for="torokuKYUKCD"]').text("");
                        $('div span[data-valmsg-for="torokuKYUGYOMMEMO"]').text("");
                        document.getElementById('patternMon').innerHTML = "";

                        var memo = document.getElementById("torokuKYUGYOMMEMO").value;

                        //if (kyukacd == '') {
                        //    err = '1';
                        //    $('div span[data-valmsg-for="torokuKYUKCD"]').text("休暇種類が必要です。");
                        //}


                        if (memo != '') {
                            if (getByteCount(memo) > 30) {
                                err = '1';
                                $('div span[data-valmsg-for="torokuKYUGYOMMEMO"]').text("文字数がオーバーしています。");

                            }
                        }

                        if (gyomymd == '') {
                            err = '1';
                            $('div span[data-valmsg-for="torokuKKNST"]').text('期間-開始が必要です。');
                        }
                        else if (gyomymded != '' && gyomymd > gyomymded) {
                            err = '1';
                            $('div span[data-valmsg-for="torokuKKNED"]').text('期間-開始と終了の前後関係が誤っています。');
                        }

                        
                        if (kskjknst == '') {
                            err = '1';
                            $('div span[data-valmsg-for="torokuJKNST"]').text("時間-開始が必要です。");
                        }
                        else if (kskjknst != '' && pad(kskjknst, 5) > '24:00') {
                            err = '1';
                            $('div span[data-valmsg-for="torokuJKNST"]').text("時間-開始が24時を超えています。");
                        }

                        if (kskjkned == '') {
                            err = '1';
                            $('div span[data-valmsg-for="torokuJKNED"]').text("時間-終了が必要です。");
                        }
                        else if (kskjkned != '' && pad(kskjkned, 5) > '24:00') {
                            err = '1';
                            $('div span[data-valmsg-for="torokuJKNED"]').text("時間-終了が24時を超えています。");
                        }
                        


                        //if (gyomymd != '' & kskjknst != '' && kskjkned != '' && pad(kskjknst, 5) <= '36:00' && pad(kskjkned, 5) <= '36:00') {
                        //    var gyomymdedtemp = gyomymd;

                        //    if (pattern == false) {
                        //        //繰り返し登録なしの場合
                        //        if (gyomymded != '' && gyomymd != gyomymded) {
                        //            gyomymdedtemp = gyomymded;
                        //        }
                        //    }
                        //    else {
                        //        //繰り返し登録の場合
                        //        //開始時間 > 終了時間の場合、開始日+1
                        //        if (pad(kskjknst, 5) > pad(kskjkned, 5)) {
                        //            var dt = new Date(gyomymd);
                        //            var yy = dt.getFullYear();
                        //            var mm = dt.getMonth() + 1;
                        //            var dd = dt.getDate() + 1;
                        //            gyomymdedtemp = yy + "/" + pad(mm, 2) + "/" + pad(dd, 2);
                        //        }
                        //    }

                        //    var jtjknst = getjtjkn(gyomymd, pad(kskjknst, 5));
                        //    var jtjkned = getjtjkn(gyomymdedtemp, pad(kskjkned, 5));

                        //    if (gyomymd <= gyomymdedtemp && jtjknst > jtjkned) {
                        //        err = '1';
                        //        $('div span[data-valmsg-for="KSKJKNST"]').text("拘束時間-開始と終了の前後関係が誤っています。");
                        //    }
                        //}


                        if (pattern && gyomymded == '') {
                            err = '1';
                            $('div span[data-valmsg-for="torokuKKNST"]').text("繰り返し登録の場合、業務期間-終了が必要です。");
                        }
                        else if (pattern && gyomymd == gyomymded) {
                            err = '1';
                            $('div span[data-valmsg-for="torokuKKNST"]').text("繰り返し登録の場合、業務期間-開始と終了に同じ日付は指定できません。");
                        }


                        if (pattern && mon == false && tue == false && wed == false && tur == false && fri == false && sat == false && sun == false) {
                            err = '1';
                            document.getElementById('patternMon').innerHTML = "繰り返し登録の場合、曜日指定が必要です。";
                        }
                        else {
                            document.getElementById('patternMon').innerHTML = "";
                        }

                        var exist = '';

                        if (pattern && gyomymded != '' && (mon || tue || wed || tur || fri || sat || sun)) {
                            var gyomymdst = gyomymd;
                            while (gyomymdst <= gyomymded) {
                                var dtgyomymdst = new Date(gyomymdst);
                                var weekday = dtgyomymdst.getDay();

                                if (sun && weekday == 0) { exist = '1'; break; }
                                if (mon && weekday == 1) { exist = '1'; break; }
                                if (tue && weekday == 2) { exist = '1'; break; }
                                if (wed && weekday == 3) { exist = '1'; break; }
                                if (tur && weekday == 4) { exist = '1'; break; }
                                if (fri && weekday == 5) { exist = '1'; break; }
                                if (sat && weekday == 6) { exist = '1'; break; }

                                var yy = dtgyomymdst.getFullYear();
                                var mm = dtgyomymdst.getMonth() + 1;
                                var dd = dtgyomymdst.getDate() + 1;

                                gyomymdst = yy + "/" + pad(mm, 2) + "/" + pad(dd, 2);
                            }

                            if (exist == '') {
                                err = '1';

                                document.getElementById('patternMon').innerHTML ="業務期間内に指定の曜日が存在しません。";
                            }
                        }
                        


                        if (err != '') {

                            return false
                        }
                        else {

                            /*if (kyukacdcode == '7' || kyukacdcode == '9') {*/

                                timestart = kskjknst.split(':');
                                var second = 0
                                var time1 = parseInt(timestart[1])
                                var second1 = (timestart[0] * 60);
                                second1 += time1;


                                timeend = kskjkned.split(':');
                                var second = 0
                                var time2 = parseInt(timeend[1])
                                var second2 = (timeend[0] * 60);
                                second2 += time2;

                                var timedifference = second2 - second1

                                if (timedifference < 15) {
                                    alert("開始時間と終了時間の間隔は最低15分以上である必要があります。")
                                    return false
                                }

                            //}

                            $("#kyukTorokuForm").submit();
                        
                        }



                    }
                }
            })

          


        })

    })


    function pad(str, max) {
        str = str.toString();
        return str.length < max ? pad("0" + str, max) : str;
    }

    //$(function () {
    //    $('#update').click(function () {

    //        var result = confirm("休暇申請を行います。よろしいですか?")

    //        if (result == false) {
    //            return false
    //        }

    //        var msg = '';
    //        var kyukacd = $('#KYUKCD').val();
           
    //        var kskjknst = $('#JKNST').val();
    //        var kskjkned = $('#JKNED').val();
    //        var kknst = $('#KKNST').val();
    //        var kkned= $('#KKNED').val();
       
    //        var kyucd = $('#KYUKCD').val();
    //        if (kyucd == '0') {
    //            $("#Errorkyucd").text("休暇種類が必要です。 ");
    //            document.getElementById('Errorkyucd').style.color = 'red';
    //            var msg = 'Errorkyucd';

    //        }
    //        else {
    //            $("#Errorkyucd").text("")
    //        }


    //        if (kknst == '' ) {
    //            $("#ErrorKKNST").text("期間が必要です。 ");
    //            document.getElementById('ErrorKKNST').style.color = 'red';
    //            var msg = 'ErrorKKNST';
           
    //        }
    //        else {
    //            $("#ErrorKKNST").text("")
    //        }

    //        if (kyukacd == '7' || kyukacd == '9') {
    //            if (kskjknst == '' || kskjkned == '') {                   
    //                $("#Error").text("時間が必要です。 ");
    //                document.getElementById('Error').style.color = 'red';
    //                var msg = 'Error';
                                 
                
    //            }
    //            else {
    //                $("#Error").text("")
    //                var pad = "00000"
    //                var start = pad.substring(0, pad.length - kskjknst.length) + kskjknst
    //                var end = pad.substring(0, pad.length - kskjkned.length) + kskjkned
                   
    //                if (kskjknst != '' && start > '24:00') {
    //                    $("#Error24Over1").text("時間が24時を超えています。");
    //                    document.getElementById('Error24Over1').style.color = 'red';
    //                    return false
    //                }
    //                else {
    //                    $("#Error24Over1").text("")
    //                }

    //                if (kskjkned != '' && end > '24:00') {
    //                    $("#Error24Over1").text("時間が24時を超えています。");
    //                    document.getElementById('Error24Over1').style.color = 'red';
    //                    return false
    //                }
    //                else {
    //                    $("#Error24Over1").text("")
    //                }
                   
    //            }

    //            }
              

    //        if (msg != '') {
                
    //            return false
    //        } else {


              
    //            if (kknst != '' && kkned != '') {
    //                if (kknst > kkned) {
    //                    $("#ErrorKKNST").text("期間-開始と終了の前後関係が誤っています。");
    //                    document.getElementById('ErrorKKNST').style.color = 'red';
    //                    return false
    //                }
    //                else {
    //                    $("#ErrorKKNST").text("")
    //                }
    //            }


    //            if (kkned == '' && (kyucd == '7' || kyucd == '9')) {
    //                var pad = "00000"
    //                var start = pad.substring(0, pad.length - kskjknst.length) + kskjknst
    //                var end = pad.substring(0, pad.length - kskjkned.length) + kskjkned


    //                if (start > end) {
    //                    $("#Error").text("時間-開始と終了の前後関係が誤っています。");
    //                    document.getElementById('Error').style.color = 'red';
    //                    return false

    //                }
    //                else {
    //                    $("#Error").text("")
    //                }
    //            }

    //            if (kyukacd == '7' || kyukacd == '9') {

    //                 timestart = kskjknst.split(':');
    //                var second = 0
    //                var time1 = parseInt(timestart[1])
    //                var second1 = (timestart[0] * 60);
    //                second1 += time1;


    //                timeend = kskjkned.split(':');
    //                var second = 0
    //                var time2 = parseInt(timeend[1])
    //                var second2 = (timeend[0] * 60);
    //                second2 += time2;

    //                var timedifference = second2 - second1

    //                if (timedifference < 15) {
    //                    alert("開始時間と終了時間の間隔は最低15分以上である必要があります。")
    //                    return false
    //                }

    //            }


    //            if (kkned == '') {
    //                document.getElementById("KKNED").value = kknst
    //            }
    //        }

    //    });

    //    });
       
    function getByteCount(str) {
        var b = str.match(/[^\x00-\xff]/g);
        return (str.length + (!b ? 0 : b.length));
    }

</script>
