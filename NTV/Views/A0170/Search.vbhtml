@ModelType NTV_SHIFT.M0090
@Code
    ViewData("Title") = "Search"
End Code



@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>業務一括登録マスタ</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})


        @*<div class="form-group">
            @Html.LabelFor(Function(model) model.IKKATUMEMO, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.IKKATUMEMO, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.IKKATUMEMO, "", New With {.class = "text-danger"})
            </div>
        </div>*@

        <div class="form-group">
            <div class="form-inline">
                @Html.LabelFor(Function(model) model.GYOMYMD, htmlAttributes:=New With {.class = "control-label col-md-2"})

                <div class="col-md-10">


                    @Html.EditorFor(Function(model) model.GYOMYMD, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})

                    ～
                    @*@Html.LabelFor(Function(model) model.GYOMYMDED, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.GYOMYMD, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.GYOMYMD, "", New With {.class = "text-danger"})
                </div>

            </div>
        </div>

        <div class="form-group">
            <div class="form-inline">
                @Html.LabelFor(Function(model) model.GYOMYMDED, htmlAttributes:=New With {.class = "control-label col-md-2"})

                <div class="col-md-10">


                    @Html.EditorFor(Function(model) model.GYOMYMDED, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})

                    ～
                    @*@Html.LabelFor(Function(model) model.GYOMYMDED, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.GYOMYMDED, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.GYOMYMDED, "", New With {.class = "text-danger"})
                </div>

            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.PTN1, "パターン設定", htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">

                <label class="checkbox-inline">
                    繰り返し
                    @*@Html.LabelFor(Function(model) model.PTN1, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.PTN1)
                    @Html.ValidationMessageFor(Function(model) model.PTN1, "", New With {.class = "text-danger"})
                </label>

                <label class="checkbox-inline">
                    月
                    @*@Html.LabelFor(Function(model) model.PTN1, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.PTN1)
                    @Html.ValidationMessageFor(Function(model) model.PTN1, "", New With {.class = "text-danger"})
                </label>

                <label class="checkbox-inline">
                    火
                    @*@Html.LabelFor(Function(model) model.PTN2, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.PTN2)
                    @Html.ValidationMessageFor(Function(model) model.PTN2, "", New With {.class = "text-danger"})
                </label>

                <label class="checkbox-inline">
                    水
                    @Html.EditorFor(Function(model) model.PTN3)
                    @Html.ValidationMessageFor(Function(model) model.PTN3, "", New With {.class = "text-danger"})

                </label>

                <label class="checkbox-inline">
                    木
                    @Html.EditorFor(Function(model) model.PTN4)
                    @Html.ValidationMessageFor(Function(model) model.PTN4, "", New With {.class = "text-danger"})
                </label>

                <label class="checkbox-inline">
                    金
                    @Html.EditorFor(Function(model) model.PTN5)
                    @Html.ValidationMessageFor(Function(model) model.PTN5, "", New With {.class = "text-danger"})
                </label>

                <label class="checkbox-inline">
                    土
                    @Html.EditorFor(Function(model) model.PTN6)
                    @Html.ValidationMessageFor(Function(model) model.PTN6, "", New With {.class = "text-danger"})
                </label>

                <label class="checkbox-inline">
                    日
                    @Html.EditorFor(Function(model) model.PTN7)
                    @Html.ValidationMessageFor(Function(model) model.PTN7, "", New With {.class = "text-danger"})
                </label>

            </div>
        </div>

        <div class="form-group">
            <div class="form-inline">

                @Html.LabelFor(Function(model) model.KSKJKNST, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.KSKJKNST, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With {.class = "text-danger"})



                    ～
                    @*@Html.LabelFor(Function(model) model.KSKJKNED, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.KSKJKNST, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.KSKJKNST, "", New With {.class = "text-danger"})

                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="form-inline">

                @Html.LabelFor(Function(model) model.KSKJKNED, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.KSKJKNED, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With {.class = "text-danger"})



                    ～
                    @*@Html.LabelFor(Function(model) model.KSKJKNED, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.KSKJKNED, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.KSKJKNED, "", New With {.class = "text-danger"})

                </div>
            </div>
        </div>

        <div class="form-group">

            @Html.LabelFor(Function(model) model.CATCD, "カテゴリー", htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownList("CATCD", Nothing, htmlAttributes:=New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.CATCD, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMINM, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @*@Html.DropDownList("BANGUMICD", Nothing, htmlAttributes:=New With {.class = "form-control"})*@
                @*@Html.ValidationMessageFor(Function(model) model.M0030.BANGUMICD, "", New With {.class = "text-danger"})*@
                @Html.EditorFor(Function(model) model.BANGUMINM, New With {.htmlAttributes = New With {.class = "form-control"}})
               @Html.ValidationMessageFor(Function(model) model.BANGUMINM, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <div class="form-inline">


                @Html.LabelFor(Function(model) model.OAJKNST, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.OAJKNST, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.OAJKNST, "", New With {.class = "text-danger"})



                    ～
                    @*@Html.LabelFor(Function(model) model.OAJKNED, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.OAJKNST, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.OAJKNST, "", New With {.class = "text-danger"})

                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="form-inline">


                @Html.LabelFor(Function(model) model.OAJKNED, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.OAJKNED, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.OAJKNED, "", New With {.class = "text-danger"})



                    ～
                    @*@Html.LabelFor(Function(model) model.OAJKNED, htmlAttributes:=New With {.class = "control-label col-md-2"})*@

                    @Html.EditorFor(Function(model) model.OAJKNED, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.OAJKNED, "", New With {.class = "text-danger"})

                </div>
            </div>
        </div>



        <div class="form-group">
            @Html.LabelFor(Function(model) model.NAIYO, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @*@Html.DropDownList("NAIYOCD", Nothing, htmlAttributes:=New With {.class = "form-control"})*@
                @*@Html.ValidationMessageFor(Function(model) model.M0040.NAIYOCD, "", New With {.class = "text-danger"})*@

                 @Html.EditorFor(Function(model) model.NAIYO, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.NAIYO, "", New With {.class = "text-danger"})
            </div>
        </div>

        @*<div class="form-group">
                @Html.LabelFor(Function(model) model.BASYOHYOJ, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    <div class="checkbox">
                        @Html.EditorFor(Function(model) model.BASYOHYOJ)
                        @Html.ValidationMessageFor(Function(model) model.BASYOHYOJ, "", New With {.class = "text-danger"})
                    </div>
                </div>
            </div>*@

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BASYO, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BASYO, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.BASYO, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.USERID, "アナウンサー", htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownList("USERID", Nothing, htmlAttributes:=New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.USERID, "", New With {.class = "text-danger"})
            </div>
        </div>



        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMITANTO, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BANGUMITANTO, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.BANGUMITANTO, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BANGUMIRENRK, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BANGUMIRENRK, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.BANGUMIRENRK, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BIKO, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.BIKO, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.BIKO, "", New With {.class = "text-danger"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(model) model.IKKATUNO, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.IKKATUNO, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.IKKATUNO, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.UPDTID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.UPDTID, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.UPDTID, "", New With {.class = "text-danger"})
            </div>
        </div>

        @*<div class="form-group">
                @Html.LabelFor(Function(model) model.PTNFLG, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    <div class="checkbox">
                        @Html.EditorFor(Function(model) model.PTNFLG)
                        @Html.ValidationMessageFor(Function(model) model.PTNFLG, "", New With {.class = "text-danger"})
                    </div>
                </div>
            </div>*@




        @*<div class="form-group">
                @Html.LabelFor(Function(model) model.INSTID, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.INSTID, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.INSTID, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.INSTDT, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.INSTDT, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.INSTDT, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.INSTTERM, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.INSTTERM, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.INSTTERM, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.INSTPRGNM, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.INSTPRGNM, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.INSTPRGNM, "", New With {.class = "text-danger"})
                </div>
            </div>


            <div class="form-group">
                @Html.LabelFor(Function(model) model.UPDTDT, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.UPDTDT, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.UPDTDT, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.UPDTTERM, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.UPDTTERM, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.UPDTTERM, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.UPDTPRGNM, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.UPDTPRGNM, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.UPDTPRGNM, "", New With {.class = "text-danger"})
                </div>
            </div>*@


        <div class="form-group">
            <div class="form-inline">


                <div class="col-md-offset-2 col-md-10">
                    @Html.ActionLink("検索", "Index")
                    &nbsp &nbsp
                    @Html.ActionLink("業務登録画面", "Create")
                </div>
            </div>
        </div>

        @*<div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="登録" class="btn btn-default" />
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="業務登録画面" class="btn btn-default" />
                </div>
            </div>*@
    </div>



End Using
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
        </th>

        <th>
            パターン設定
            @*@Html.DisplayNameFor(Function(model) model.PTNFLG)*@
        </th>

        <th>
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.KSKJKNED)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </th>

       
        <th>
            @Html.DisplayNameFor(Function(model) model.OAJKNST)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.OAJKNED)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.NAIYO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BASYO)
        </th>
        <th>

            @Html.DisplayNameFor(Function(model) model.M0010.LOGINID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
        </th>
        @*<th>
                @Html.DisplayNameFor(Function(model) model.IKKATUMEMO)
            </th>*@


        <th>
            @Html.DisplayNameFor(Function(model) model.BIKO)
        </th>

      
        @*<th>
                @Html.DisplayNameFor(Function(model) model.PTN1)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PTN2)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PTN3)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PTN4)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PTN5)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PTN6)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PTN7)
            </th>*@
        @*<th>
                @Html.DisplayNameFor(Function(model) model.INSTID)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.INSTDT)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.INSTTERM)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.INSTPRGNM)
            </th>*@
        <th>
            @Html.DisplayNameFor(Function(model) model.UPDTID)
        </th>
        @*<th>
                @Html.DisplayNameFor(Function(model) model.UPDTDT)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.UPDTTERM)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.UPDTPRGNM)
            </th>
            <th></th>*@
    </tr>

   
  
</table>
