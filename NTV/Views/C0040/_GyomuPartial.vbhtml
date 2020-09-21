@ModelType NTV_SHIFT.D0050
@Code
   
    Dim Bangumilst = DirectCast(ViewBag.BangumiList, List(Of M0030))
    Dim NaiyouList = DirectCast(ViewBag.NaiyouList, List(Of M0040))
End Code

<style>
    #comboField {
        position: relative;
    }

    #BANGUMINM {
        font-size: 14px;
        width: 200px;
        position: absolute;
       
    }

    #selectBoxBangumi {
 font-size: 14px;
 width: 225px;
 
}

    
     #NAIYO {
        font-size: 14px;
        width: 200px;
        position: absolute;
       
    }

     #selectBoxNaiyo {
 font-size: 14px;
 width: 225px;
 
}
     
</style>


    @Using (Html.BeginForm("CreateD0050", "C0040", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm1"}))
    @Html.AntiForgeryToken()

    @Html.Hidden("name", ViewData("name"))
    @Html.Hidden("id", ViewData("id"))
    @Html.Hidden("searchdt", ViewData("searchdt"))
@Html.HiddenFor(Function(model) model.CONFIRMMSG)
    @<div class="form-horizontal" style="background-color:whitesmoke">
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
         <div class="form-group">
             <div class="col-sm-offset-1 col-md-11">
                 <h5><b>業務申請</b></h5>
             </div>
         </div>
        
        <div class="form-group">
            <div class="form-inline">
                @Html.LabelFor(Function(model) model.GYOMYMD, "業務期間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                <div class="col-md-9">
                    @Html.TextBox("GYOMYMD", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})                  
                    
                    <label class="control-label">～</label>
                    @Html.TextBox("GYOMYMDED", Nothing, "{0:yyyy/MM/dd}", htmlAttributes:=New With {.class = "form-control input-sm datepicker imedisabled"})                    
                   
                    <div id="ErrorGYOMYMD"></div>
                </div>
             </div>
            <div class=" col-md-offset-3 col-md-9">
               @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})
                @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})
            </div>
            </div>

        <div class="form-group">
            <div class="form-inline">
                @Html.LabelFor(Function(model) model.KSKJKNST, "拘束期間", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                <div class="col-md-9">
                    @Html.EditorFor(Function(model) model.KSKJKNST, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                  
                    <label class="control-label">～</label>
                    @Html.EditorFor(Function(model) model.KSKJKNED, New With {.htmlAttributes = New With {.class = "form-control input-sm time imedisabled"}})
                  
                    <div id="ErrorKSKJKNST"></div>
                </div>
            </div>
            <div class=" col-md-offset-3 col-md-9">
                @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With {.class = "text-danger"})
                @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.CATCD, "カテゴリー", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
            <div class="col-md-9">
                @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control input-sm"})
             
                <div id="ErrorCatcd"></div>
            </div>
            <div class=" col-md-offset-3 col-md-9">
                @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
            </div>
        </div>

         <div class="form-group ">

             @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
             <div class="col-md-9" id="comboField">
                 @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
               
                 @If Bangumilst IsNot Nothing Then
                     @<select class="form-control input-sm selectBox" id="selectBoxBangumi">
                         @For Each item In Bangumilst
                             @<option>@item.BANGUMINM</option>
                         Next
                     </select>
                 End If
                
                 <div id="ErrorBANGUMINM"></div>
             </div>
             <div class=" col-md-offset-3 col-md-9">
                 @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
             </div>
         </div>
         @*<div class="form-group ">
            <div class="col-md-9 col-md-push-3">
                @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
            </div>
         </div>*@
    
         <div class="form-group ">
             <div class="form-inline">
                 @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                 <div class="col-md-9" id="comboField">
                     @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control input-sm", .value = ViewData("NAIYO")}})

                     @If NaiyouList IsNot Nothing Then
                         @<select class="form-control input-sm selectBox" id="selectBoxNaiyo">
                             @For Each item In NaiyouList
                                 @<option>@item.NAIYO</option>
                             Next
                         </select>
                     End If
                    
                     <div id="ErrorNAIYO"></div>
                 </div>

             </div>
             <div class=" col-md-offset-3 col-md-9">
                 @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
             </div>
         </div>
         @*<div class="form-group ">
             <div class="col-md-9 col-md-push-3">
                 @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                 @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
             </div>
         </div>*@

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BASYO, htmlAttributes:=New With {.class = "control-label col-md-3"})
            <div class="col-md-9">
                @Html.EditorFor(Function(model) model.BASYO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
               
                <div id="ErrorBASYO"></div>
            </div>
            <div class=" col-md-offset-3 col-md-9">
                @Html.ValidationMessageFor(Function(model) model.BASYO, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMITANTO, htmlAttributes:=New With {.class = "control-label col-md-3"})
            <div class="col-md-9">
                @Html.EditorFor(Function(model) model.BANGUMITANTO, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
               
            </div>
            <div class=" col-md-offset-3 col-md-9">
                @Html.ValidationMessageFor(Function(model) model.BANGUMITANTO, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMIRENRK, "連絡先", htmlAttributes:=New With {.class = "control-label col-md-3"})
            <div class="col-md-9">
                @Html.EditorFor(Function(model) model.BANGUMIRENRK, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
               
            </div>
            <div class=" col-md-offset-3 col-md-9">
                @Html.ValidationMessageFor(Function(model) model.BANGUMIRENRK, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.GYOMMEMO, "備考", htmlAttributes:=New With {.class = "control-label col-md-3"})
            <div class="col-md-9">
                @Html.EditorFor(Function(model) model.GYOMMEMO, New With {.htmlAttributes = New With {.class = "form-control  input-sm"}})
               
            </div>
            <div class=" col-md-offset-3 col-md-9">
                @Html.ValidationMessageFor(Function(model) model.GYOMMEMO, "", New With {.class = "text-danger"})
            </div>
        </div>
    

         <div id="divWarning" class="text-warning" style="height:0px">
             @Html.Hidden("warning", ViewData("warning"))
         </div>
              
         <div class="form-group">
             <div class="col-md-offset-3 col-md-9">
                 <input id="updateD0050" type="submit" value="登録" class="btn btn-default" />

             </div>
         </div>

    </div>
    
End Using



<script>
    var myApp = myApp || {};
    myApp.Urls = myApp.Urls || {};
    myApp.Urls.baseUrl = '@Url.Content("~")';
</script>

<script type="text/javascript">

    $("#selectBoxBangumi").change(function () {

        var val = this.value
        $('#BANGUMINM').val(val)

    });

    $("#selectBoxNaiyo").change(function () {

        var val = this.value
        $('#NAIYO').val(val)

    });

    //$(document).ready(function () {
       
    //    //document.getElementById("GYOMYMD").value = "";
    //    //document.getElementById("GYOMYMDED").value = "";
    //});

    $(document).ready(function () {
        //var div = document.getElementById('checkbox');
        //if ($('#chkPattern').prop("checked")) {
        //    div.style.visibility = 'visible';
        //} else {
        //    div.style.visibility = 'hidden';
        //}
        //alert('reach')

        
            
    })

    //$(document).on('focus', 'input', function () {
    //    alert('reach')
    //    $('.datepicker').datepicker({
    //        language: 'ja',
    //        format: 'yyyy/mm/dd',
    //        autoclose: true,
    //        //clearBtn: true,
    //        //keyboardNavigation: false,
    //        todayHighlight: true
    //    });
    //});

    $(function () {
        $('#updateD0050').click(function (e) {

            var gyomymd = $('#GYOMYMD').val();
            var gyomymded = $('#GYOMYMDED').val();
            var id = $('#id').val();
            var jknst = $('#KSKJKNST').val();
            var jkned = $('#KSKJKNED').val();
                                 
            e.preventDefault();
            var url = myApp.Urls.baseUrl + 'C0040/CheckGyomuShinsei/?gyomymd=' + gyomymd + '&gyomymded=' + gyomymded + '&id=' + id + '&jknst=' + jknst + '&jkned=' + jkned ;

            $("#divWarning").load(url, function (response, status, xhr) {
               
                if (status == "success") {

                    var msgwarning = jQuery.trim($('#warning').val());
                    
                    if (msgwarning == 2) {
                        alert("公休展開していない月の申請です。デスクにご連絡ください。")                       
                        return false;

                    } else if (msgwarning == 3) {
                        alert("その時間帯は時間休が登録されています。")
                        return false;
                    }
                    else {
                        
                        var result = confirm("業務申請を行います。よろしいですか?")

                        if (result == false) {
                            return false
                        }

                        var msg = '';
                        var err = '';
                        $('div span[data-valmsg-for="GYOMYMD"]').text("");
                        $('div span[data-valmsg-for="GYOMYMDED"]').text("");
                        $('div span[data-valmsg-for="KSKJKNST"]').text("");
                        $('div span[data-valmsg-for="KSKJKNED"]').text("");
                        $('div span[data-valmsg-for="BANGUMINM"]').text("");
                        $('div span[data-valmsg-for="NAIYO"]').text("");
                        $('div span[data-valmsg-for="BASYO"]').text("");
                        $('div span[data-valmsg-for="CATCD"]').text("");

                        $('div span[data-valmsg-for="BANGUMITANTO"]').text("");
                        $('div span[data-valmsg-for="BANGUMIRENRK"]').text("");
                        $('div span[data-valmsg-for="GYOMMEMO"]').text("");



                        var kskjknst = $('#KSKJKNST').val();
                        var kskjkned = $('#KSKJKNED').val();

                        var banguminm = $('#BANGUMINM').val();
                        var catcd = $('#CATCD').val();


                        var naiyo = $('#NAIYO').val();
                        var basyo = $('#BASYO').val();
                        var bangumitanto = $('#BANGUMITANTO').val();
                        var renraku = $('#BANGUMIRENRK').val();
                        var memo = $('#GYOMMEMO').val();

                        if (catcd == '0') {
                            err = '1';
                            $('div span[data-valmsg-for="CATCD"]').text("カテゴリーが必要です。");
                        }


                        if (banguminm == '') {
                            err = '1';
                            $('div span[data-valmsg-for="BANGUMINM"]').text("番組名が必要です。");

                        }
                        else {
                            if (getByteCount(banguminm) > 40) {
                                err = '1';
                                $('div span[data-valmsg-for="BANGUMINM"]').text("文字数がオーバーしています。");

                            }

                        }

                        if (gyomymd == '') {
                            err = '1';
                            $('div span[data-valmsg-for="GYOMYMD"]').text('業務期間-開始が必要です。');
                        }
                        else if (gyomymded != '' && gyomymd > gyomymded) {
                            err = '1';
                            $('div span[data-valmsg-for="GYOMYMD"]').text('業務期間-開始と終了の前後関係が誤っています。');
                        }

                        if (kskjknst == '') {
                            err = '1';
                            $('div span[data-valmsg-for="KSKJKNST"]').text("拘束時間-開始が必要です。");
                        }
                        else if (kskjknst != '' && pad(kskjknst, 5) > '36:00') {
                            err = '1';
                            $('div span[data-valmsg-for="KSKJKNST"]').text("拘束時間-開始が36時を超えています。");
                        }

                        if (kskjkned == '') {
                            err = '1';
                            $('div span[data-valmsg-for="KSKJKNED"]').text("拘束時間-終了が必要です。");
                        }
                        else if (kskjkned != '' && pad(kskjkned, 5) > '36:00') {
                            err = '1';
                            $('div span[data-valmsg-for="KSKJKNED"]').text("拘束時間-終了が36時を超えています。");
                        }


                        if (gyomymd != '' & kskjknst != '' && kskjkned != '' && pad(kskjknst, 5) <= '36:00' && pad(kskjkned, 5) <= '36:00') {
                            var gyomymdedtemp = gyomymd;

                            if (gyomymded != '' && gyomymd != gyomymded) {
                                gyomymdedtemp = gyomymded;
                            }

                            var jtjknst = getjtjkn(gyomymd, pad(kskjknst, 5));
                            var jtjkned = getjtjkn(gyomymdedtemp, pad(kskjkned, 5));

                            if (gyomymd <= gyomymdedtemp && jtjknst > jtjkned) {
                                err = '1';
                                $('div span[data-valmsg-for="KSKJKNST"]').text("拘束時間-開始と終了の前後関係が誤っています。");
                            }
                        }


                        if (naiyo != '') {
                            if (getByteCount(naiyo) > 40) {
                                err = '1';
                                $('div span[data-valmsg-for="NAIYO"]').text("文字数がオーバーしています。");

                            }
                        }

                        if (basyo != '') {
                            if (getByteCount(basyo) > 40) {
                                err = '1';
                                $('div span[data-valmsg-for="BASYO"]').text("文字数がオーバーしています。");

                            }
                        }

                        if (bangumitanto != '') {
                            if (getByteCount(bangumitanto) > 30) {
                                err = '1';
                                $('div span[data-valmsg-for="BANGUMITANTO"]').text("文字数がオーバーしています。");

                            }
                        }


                        if (renraku != '') {
                            if (getByteCount(renraku) > 30) {
                                err = '1';
                                $('div span[data-valmsg-for="BANGUMIRENRK"]').text("文字数がオーバーしています。");

                            }
                        }


                        if (memo != '') {
                            if (getByteCount(memo) > 30) {
                                err = '1';
                                $('div span[data-valmsg-for="GYOMMEMO"]').text("文字数がオーバーしています。");

                            }
                        }


                        if (err == '1') {

                            return false
                        } else {

                            if (gyomymded == '') {
                                document.getElementById("GYOMYMDED").value = gyomymd
                            }

                        }

                        e.preventDefault();
                        var url = myApp.Urls.baseUrl + 'C0040/SearchGYOMU/?gyomymd=' + gyomymd + '&gyomymded=' + gyomymded + '&kskjknst=' + kskjknst + '&kskjkned=' + kskjkned + '&id=' + id;

                        $("#divWarning").load(url, function (response, status, xhr) {

                            if (status == "success") {

                                var msgwarning = jQuery.trim($('#warning').val());

                                if (msgwarning.length > 0) {
                                    if (msgwarning == '1') {
                                        var result = confirm("申請の業務と重複するシフトがありますがよろしいですか？");
                                        if (result == true) {
                                            $('#CONFIRMMSG').val(true);
                                            $("#myForm1").submit();
                                        }
                                        else {
                                            $('#CONFIRMMSG').val(false);
                                            return false;
                                        }
                                    }
                                    else {
                                        alert("公休展開していない月の申請です。デスクにご連絡ください。")
                                        return false;
                                    }

                                }
                                else {
                                    $("#myForm1").submit();
                                }
                            }
                        })
                    }
                   
                }
            })

                     

           
        });

    });

    function pad(str, max) {
        str = str.toString();
        return str.length < max ? pad("0" + str, max) : str;
    }

    function getjtjkn(dt, time) {

        var HH = time.substr(0, 2);
        var MM = time.substr(3, 2);

        if (HH >= 24) {
            HH = HH - 24;
            HH = pad(HH, 2)

            var dt = new Date(dt);
            var yy = dt.getFullYear();
            var mm = dt.getMonth() + 1;
            var dd = dt.getDate() + 1;

            dt = yy + "/" + pad(mm, 2) + "/" + pad(dd, 2);
        }

        var jtjkn = dt + " " + HH + ":" + MM;

        return jtjkn;
    }


    function getByteCount(str) {
        var b = str.match(/[^\x00-\xff]/g);
        return (str.length + (!b ? 0 : b.length));
    }
</script>

