@ModelType NTV_SHIFT.M0090
@Code
    ViewData("Title") = "業務一括登録"
End Code

<h3>削除</h3>

<h3>削除してもよろしいですか？</h3>
<div>
   
    <hr />
    <dl class="dl-horizontal">
        @*<dt>
            @Html.DisplayNameFor(Function(model) model.M0010.LOGINID)
        </dt>*@

        @*<dd>
            @Html.DisplayFor(Function(model) model.M0010.LOGINID)
        </dd>*@

        <dt>
            @Html.DisplayNameFor(Function(model) model.IKKATUMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IKKATUMEMO)
        </dd>

        <dt>
           業務期間
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMYMD)
            ～
            @Html.DisplayFor(Function(model) model.GYOMYMDED)
        </dd>
            

        <dt>
            @Html.DisplayNameFor(Function(model) model.PTNFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PTNFLG)
        </dd>

        @*<dd>
                      
            @Html.DisplayFor(Function(model) model.PTN1)
            @Html.DisplayNameFor(Function(model) model.PTN1)

            @Html.DisplayFor(Function(model) model.PTN2)
            @Html.DisplayNameFor(Function(model) model.PTN2)

            @Html.DisplayFor(Function(model) model.PTN3)
            @Html.DisplayNameFor(Function(model) model.PTN3)


            @Html.DisplayFor(Function(model) model.PTN4)
            @Html.DisplayNameFor(Function(model) model.PTN4)

            @Html.DisplayFor(Function(model) model.PTN5)
            @Html.DisplayNameFor(Function(model) model.PTN5)

            @Html.DisplayFor(Function(model) model.PTN6)
            @Html.DisplayNameFor(Function(model) model.PTN6)

            @Html.DisplayFor(Function(model) model.PTN7)
            @Html.DisplayNameFor(Function(model) model.PTN7)
        </dd>*@

       
        <dt>
            @Html.DisplayNameFor(Function(model) model.KSKJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(0, 2)
            @Html.Encode(":")
            @Html.DisplayFor(Function(model) model.KSKJKNST).ToString.Substring(2, 2)

            @Html.Encode("～")

            @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(0, 2)
            @Html.Encode(":")
            @Html.DisplayFor(Function(model) model.KSKJKNED).ToString.Substring(2, 2)
        </dd>
              

        <dt>
            @Html.DisplayNameFor(Function(model) model.M0020.CATNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0020.CATNM)
        </dd>
        
        
         <dt>
            @Html.DisplayNameFor(Function(model) model.BANGUMINM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BANGUMINM)
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
            @Html.DisplayNameFor(Function(model) model.NAIYO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NAIYO)
        </dd>

        @*<dt>
            アナウンサー
        </dt>*@

        @*<dd>
            @Html.DisplayFor(Function(model) model.M0010.USERNM)
        </dd>*@

        <dt>
            @Html.DisplayNameFor(Function(model) model.BASYO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BASYO)
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
            @Html.DisplayNameFor(Function(model) model.IKTTAISHO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IKTTAISHO)
        </dd>


    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="削除" class="btn btn-default" /> |
             @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
                     .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
                     .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
                     .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .Oajknst = Session("Oajknst"), .Oajkned = Session("Oajkned"),
                     .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Renraku = Session("Renraku"), .TaishoNo = Session("TaishoNo"), .TaishoYes = Session("TaishoYes")})


        </div>
    End Using
</div>
