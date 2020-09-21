@ModelType NTV_SHIFT.M0150
@Code
    ViewData("Title") = "スポーツシフト登録(仮登録)"
End Code

<style>
    .comboField {
        position: relative;
    }

    .inputBox {
        font-size: 14px;
        width: 195px;
        position: absolute;
    }

    .selectBox {
        font-size: 14px;
        width: 220px;
    }

</style>

<div class="col-md-12">
    <div class="row" style="padding-top:14px;">
        <div class="col-md-10">
            <h3 style="line-height: 0.2;">スポーツカテゴリを指定してください。</h3>
        </div>
        <ul class="nav nav-pills navbar-right" style="padding-right:15px">
            @If Session("UrlReferrer") IsNot Nothing Then
                    @<li><a href="@Session("UrlReferrer")">戻る</a></li>
End If
        </ul>
    </div>
    <hr style="margin-top:6px;" />
</div>


<div class="row">        
    <div class="col-md-12">
        @Using (Html.BeginForm("Index", "A0220", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
            @Html.AntiForgeryToken()
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                
            @<div class="col-md-7">
                <div class="form-horizontal">
                    <div class="form-group">
                            @Html.Label("スポーツカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                           <div class="col-md-9">
                                @Html.DropDownList("SPORTCATCD", New SelectList(ViewBag.SportCatNmList, "SPORTCATCD", "SPORTCATNM", ""), htmlAttributes:=New With {.class = "form-control input-sm", .style="width:220px;"})
                                @Html.ValidationMessageFor(Function(model) model.SPORTCATCD, "", New With {.class = "text-danger"})
                            </div>
                    </div>

                    <!--<div class="form-group ">
                        @Html.LabelFor(Function(model) model.SPORTCATCD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9 comboField">
                            @Html.EditorFor(Function(model) model.SPORTCATCD, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox"}})
                            <select class="form-control input-sm selectBox" id="selectBoxSportCatCd">
                                @If ViewBag.SportCatNmList IsNot Nothing Then
                                    @For Each item In ViewBag.SportCatNmList
                                        @<option>@item.SPORTCATCD</option>
                                    Next
                                 End If
                            </select>
                            @Html.ValidationMessageFor(Function(model) model.SPORTCATCD, "", New With {.class = "text-danger"})
                        </div>
                    </div>-->

                    <div class="form-group">
                            @Html.Label("スポーツサブカテゴリ", htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                                <div class="col-md-9">
                                @Html.DropDownList("SPORTSUBCATCD", New SelectList(ViewBag.SportSubCatNmList, "SPORTSUBCATCD", "SPORTSUBCATNM", ""), htmlAttributes:=New With {.class = "form-control input-sm", .style="width:220px;"})
                                @Html.ValidationMessageFor(Function(model) model.SPORTSUBCATCD, "", New With {.class = "text-danger"})
                            </div>
                    </div>

                    <!--<div class="form-group ">
                        @Html.LabelFor(Function(model) model.SPORTSUBCATCD, htmlAttributes:=New With {.class = "control-label col-md-3 text-warning"})
                        <div class="col-md-9 comboField">
                            @Html.EditorFor(Function(model) model.SPORTSUBCATCD, New With {.htmlAttributes = New With {.class = "form-control input-sm inputBox"}})
                            <select class="form-control input-sm selectBox" id="selectBoxSportSubCatCd">
                                @If ViewBag.SportSubCatNmList IsNot Nothing Then
                                    @For Each item In ViewBag.SportSubCatNmList
                                        @<option>@item.SPORTSUBCATCD</option>
                                    Next
                                 End If
                            </select>
                            @Html.ValidationMessageFor(Function(model) model.SPORTSUBCATCD, "", New With {.class = "text-danger"})
                        </div>
                    </div>-->
                </div>
            </div>

            @<p></p>
            @<div class="row">
                <div class="col-md-12">
                    <div class="col-md-7">                        
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="col-md-offset-3 col-md-9" style="padding-top :12px">
                                        <input type="submit" value="次へ" id="btnNext" class="btn btn-default" />
                                    </div>                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
        End Using
    </div>
</div>

<script>
    $("#SPORTCATCD").change(function () {
        var val = this.value
        $('#SPORTCATCD').val(val)

        /*Remove Options from SportSubCatCd before fetching new list corresponding to selected SportCatCd*/
        $("#SPORTSUBCATCD").find('option').remove();

        /*Write AJAX call to fetch list of SportSubCatCd corresponding to selected SportCatCd*/
        $.ajax({
            url: "@Url.Action("getSportSubCatCdList", "A0220")",
            async: false,
            type: "POST",
            data: { screenSportCatCd: val },
            dataType: 'json',
            cache: false,
            success: function (node) {
                if (node.success) {
                    if (node.sportSubCatCdList !== undefined) {
                        /*As List not blank, set this list of SportSubCatCd to dropdown*/
                        $("#SPORTSUBCATCD").append("<option></option>");
                        $.each(node.sportSubCatCdList, function () {
                            $("#SPORTSUBCATCD").append(
                                $('<option/>', {
                                    value: this.SPORTSUBCATCD,
                                    text: this.SPORTSUBCATNM
                                })
                            );
                        });

                    }
                }
            },
            error: function (node) {
                alert(node.responseText);
            }
        });

    });
    
    $("#SPORTSUBCATCD").change(function () {
        var val = this.value
        $('#SPORTSUBCATCD').val(val)
    });

    $('#btnNext').on('click', function (f) {

        var err = '';
        var sportcatcd = $('#SPORTCATCD :selected').text();
        var sportsubcatcd = $('#SPORTSUBCATCD :selected').text();

        //If selected in value in of list then remove previous Error Message
        if (sportcatcd != '') {
            err = '';
            $('div span[data-valmsg-for="SPORTCATCD"]').text("");
        }
        if (sportsubcatcd != '') {
            err = '';
            $('div span[data-valmsg-for="SPORTSUBCATCD"]').text("");
        }

        if (sportcatcd == '') {
            err = '1';
            $('div span[data-valmsg-for="SPORTCATCD"]').text("スポーツカテゴリーコードが必要です。");
        }

        if (sportsubcatcd == '') {
            err = '1';
            $('div span[data-valmsg-for="SPORTSUBCATCD"]').text("スポーツサブカテゴリーコードが必要です。");
        }

        if (err == '1') {
            return false;
        }

        return true;
    });

</script>