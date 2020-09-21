@ModelType NTV_SHIFT.M0020
@Code
    ViewData("Title") = "カテゴリー設定"
    Dim strDisable As String = ""
End Code

<div class="container">
    <div class="row">
        <div class="col-sm-8">
            <h3>修正</h3>
           @Using (Html.BeginForm("Edit", "A0120", routeValues:=Nothing, method:=FormMethod.Post, htmlAttributes:=New With {.id = "myForm"}))
                @Html.AntiForgeryToken()

                @<div class="form-horizontal">
                    
                    <hr />
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                     @Html.HiddenFor(Function(model) model.CATCD)
                    @*<div class="form-group">
                    @Html.LabelFor(Function(model) model.CATCD, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.CATCD, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
                    </div>
                </div>*@

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.CATNM, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.CATNM, New With {.htmlAttributes = New With {.class = "form-control input-sm"}})
                            @Html.ValidationMessageFor(Function(model) model.CATNM, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.HYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.HYOJ)</label>
                                 @Html.ValidationMessageFor(Function(model) model.HYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                     @*@If Model.HYOJ = True Then
                     strDisable = "true"
                     Else
                     strDisable = "false"
                     End If*@

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.HYOJJN, htmlAttributes:=New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            @Html.EditorFor(Function(model) model.HYOJJN, New With {.htmlAttributes = New With {.class = "form-control input-sm", .style = "width:80px;"}})
                            @Html.ValidationMessageFor(Function(model) model.HYOJJN, "", New With {.class = "text-danger"})
                        </div>
                    </div>


                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.OATIMEHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.OATIMEHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.OATIMEHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.BANGUMIHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.BANGUMIHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.BANGUMIHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.KKNHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.KKNHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.KKNHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.KSKHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.KSKHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.KSKHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.ANAHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.ANAHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.ANAHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.BASYOHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.BASYOHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.BASYOHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.BIKOHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.BIKOHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.BIKOHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.NAIYOHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.NAIYOHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.NAIYOHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>
            
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.TANTOHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.TANTOHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.TANTOHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.RENRKHYOJ, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.RENRKHYOJ) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.RENRKHYOJ, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>
                              

                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.SYUCHO, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.SYUCHO) (ONは表示、OFFは非表示)</label>
                                 @Html.ValidationMessageFor(Function(model) model.SYUCHO, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                    @*<div class="form-group">
                    @Html.LabelFor(Function(model) model.STATUS, htmlAttributes:= New With { .class = "control-label col-md-2" })
                    <div class="col-md-10">
                        <div class="checkbox">
                            @Html.EditorFor(Function(model) model.STATUS)
                            @Html.ValidationMessageFor(Function(model) model.STATUS, "", New With { .class = "text-danger" })
                        </div>
                    </div>
                </div>
*@
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.ALERTFLG, htmlAttributes:=New With {.class = "control-label col-md-3"})
                         <div class="col-md-9">
                             <div class="checkbox">
                                 <label>@Html.EditorFor(Function(model) model.ALERTFLG) (ONは送信する、OFFは送信しない)</label>
                                 @Html.ValidationMessageFor(Function(model) model.ALERTFLG, "", New With {.class = "text-danger"})
                             </div>
                         </div>
                     </div>

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-9">
                            <input id="btnMasterUpd" type="submit" value="更新" class="btn btn-default" />
                        </div>
                    </div>
                </div>
            End Using

            <div>
                @Html.ActionLink("一覧に戻る", "Index", Nothing, htmlAttributes:=New With {.id = "btnEditModoru"})
            </div>

            @Section Scripts
                @Scripts.Render("~/bundles/jqueryval")
            End Section


        </div>
        <div class="col-sm-3" style="padding-top:70px">
            @Html.Partial("_M0020Partial", ViewData.Item("List"))
        </div>

        </div>
</div>

<script>
    $(document).ready(function () {
        if ($("#HYOJ").is(':checked')) {

            document.getElementById("HYOJJN").disabled = false
            //var defaultval = "";
            //$('#HYOJJN').val(defaultval);
        }
        else {
            document.getElementById("HYOJJN").disabled = true
            //var defaultval = "999";
            //$('#HYOJJN').val(defaultval);
        }
    });


    $('#HYOJ').on('click', function (e) {

        //document.getElementById("myText").disabled = true
        if ($("#HYOJ").is(':checked')) {

            document.getElementById("HYOJJN").disabled = false
            //var defaultval = "";
            //$('#HYOJJN').val(defaultval);
        }
        else {
            document.getElementById("HYOJJN").disabled = true
            //var defaultval = "999";
            //$('#HYOJJN').val(defaultval);
        }

        
    });


    //修正モードで画面開いて戻るボタン押すと、確認メッセージ出ないように修正。
    //画面上のコントロールの値が変えられたら、戻るの時確認メッセージ出す
    $("#myForm :input").change(function () {
       
            $("#myForm").data("MSG", true);       
       
    });

    </script>

     
       