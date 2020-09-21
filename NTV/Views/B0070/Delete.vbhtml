@ModelType NTV_SHIFT.D0090
@Code
    ViewData("Title") = "雛形登録"
End Code

<h2>削除</h2>

<h3>削除してもよろしいですか？</h3>
<div>
    @*<h4>D0090</h4>*@
    <hr />
    <dl class="dl-horizontal">

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMINM)
        </dd>

        <dt>
            担当アナ
            @*@Html.DisplayNameFor(Function(model) model.M0010.LOGINID)*@
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0010.USERNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.HINAMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HINAMEMO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTNFLG)
        </dt>

        <dd>
            @If Model.PTNFLG = False Then
                @Html.DisplayFor(Function(model) model.PTNFLG)
            Else
                @If Model.PTN1 Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.PTN1)@Html.Encode("］")
                End If

                @If Model.PTN2 Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.PTN2)@Html.Encode("］")
                End If

                @If Model.PTN3 Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.PTN3)@Html.Encode("］")
                End If

                @If Model.PTN4 Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.PTN4)@Html.Encode("］")
                End If

                @If Model.PTN5 Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.PTN5)@Html.Encode("］")
                End If

                @If Model.PTN6 Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.PTN6)@Html.Encode("］")
                End If

                @If Model.PTN7 Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.PTN7)@Html.Encode("］")
                End If
                
                @*ASI[21 Oct 2019]:dispaly WeekA or WeekB *@
                @If Model.WEEKA Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.WEEKA)@Html.Encode("］")
                End If
                
                @If Model.WEEKB Then
                    @Html.Encode("［")@Html.DisplayFor(Function(model) model.WEEKB)@Html.Encode("］")
                End If

            End If
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </dt>

        <dd>
            @If Model.KSKJKNST IsNot Nothing Then
                @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(0, 2)
                @Html.Encode(":")
                @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(2, 2)
            End If

            @If Model.KSKJKNED IsNot Nothing Then
                @Html.Encode("～")

                @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(0, 2)
                @Html.Encode(":")
                @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(2, 2)
            End If  
        </dd>
            
        <dt>
            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0020.CATNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.NAIYO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NAIYO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BASYO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BASYO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTDT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTDT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMYMD)
            ～
            @Html.DisplayFor(Function(model) model.GYOMYMDED)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OAJKNST)
        </dt>

        <dd>
            @If Model.OAJKNST IsNot Nothing Then
                @Html.DisplayFor(Function(model) model.OAJKNST).ToString.Substring(0, 2)
                @Html.Encode(":")
                @Html.DisplayFor(Function(model) model.OAJKNST).ToString.Substring(2, 2)
            End If

            @If Model.OAJKNED IsNot Nothing Then
                @Html.Encode("～")

                @Html.DisplayFor(Function(model) model.OAJKNED).ToString.Substring(0, 2)
                @Html.Encode(":")
                @Html.DisplayFor(Function(model) model.OAJKNED).ToString.Substring(2, 2)
            End If
            
        </dd>
    
        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMITANTO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMITANTO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMIRENRK)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMIRENRK)
        </dd>


        <dt>
            @Html.DisplayNameFor(Function(model) model.BIKO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BIKO)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.DATAKBN)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DATAKBN)
        </dd>
    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="削除" class="btn btn-default" /> |
            @Html.ActionLink("一覧に戻る", "Index")
        </div>
    End Using
</div>
