@ModelType NTV_SHIFT.D0010
@Code
    ViewData("Title") = "業務登録"
End Code


<div class="row">
    <div class="col-md-2" style="padding-top:10px;">
        <ul class="nav nav-pills ">
            @If Session("B0020DeleteRtnUrl") IsNot Nothing Then
                @<li><a href="@Session("B0020DeleteRtnUrl").ToString" >戻る</a></li>
            Else
                @<li>@Html.ActionLink("戻る", "Index", "C0030")</li>
            End If
        </ul>
    </div>

    <div class="col-md-10 col-md-pull-2">
        <h3>削除</h3>
    </div>
</div>

<h3>削除してもよろしいですか？</h3>
<div>
    @*<h4>業務登録</h4>*@
    <hr />
    <dl class="dl-horizontal">

        <dt>
            @Html.DisplayNameFor(Function(model) model.GYOMYMD)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.GYOMYMD)
            ～
            @Html.DisplayFor(Function(model) model.GYOMYMDED)
        </dd>

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.GYOMYMDED)
        </dt>*@

        @*<dd>
            @Html.DisplayFor(Function(model) model.GYOMYMDED)
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

        @*<dt>
            @Html.DisplayNameFor(Function(model) model.M0090.IKKATUMEMO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M0090.IKKATUMEMO)
        </dd>
           


        <dt>
            @Html.DisplayNameFor(Function(model) model.JTJKNST)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JTJKNST)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JTJKNED)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JTJKNED)
        </dd>

      
        <dt>
            @Html.DisplayNameFor(Function(model) model.RNZK)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RNZK)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PGYOMNO)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PGYOMNO)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.IKTFLG)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IKTFLG)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.IKTUSERID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IKTUSERID)
        </dd>*@

        @*<dt>
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
            @Html.DisplayNameFor(Function(model) model.INSTTERM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTTERM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.INSTPRGNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.INSTPRGNM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTDT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTDT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTTERM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTTERM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UPDTPRGNM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UPDTPRGNM)
        </dd>*@

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input id="btnDelete" type="submit" value="削除" class="btn btn-default" /> |

             @Html.ActionLink("一覧に戻る", "Index", routeValues:=New With {.Gyost = Session("Gyost"), .Gyoend = Session("Gyoend"),
                    .PtnflgNo = Session("PtnflgNo"), .Ptn1 = Session("Ptn1"), .Ptn2 = Session("Ptn2"), .Ptn3 = Session("Ptn3"), .Ptn4 = Session("Ptn4"),
                    .Ptn5 = Session("Ptn5"), .Ptn6 = Session("Ptn6"), .Ptn7 = Session("Ptn7"), .Kskjknst = Session("Kskjknst"), .Kskjkned = Session("Kskjkned"),
                    .CATCD = Session("CATCD"), .ANAID = Session("ANAID"), .PtnAnaflgNo = Session("PtnAnaflgNo"), .PtnAna1 = Session("PtnAna1"), .PtnAna2 = Session("PtnAna2"),
                    .Banguminm = Session("Banguminm"), .Naiyo = Session("Naiyo"), .Basyo = Session("Basyo"), .Bangumitanto = Session("Bangumitanto"), .Bangumirenrk = Session("Bangumirenrk"), .OAJKNST = Session("OAJKNST"), .OAJKNED = Session("OAJKNED"), .Biko = Session("Biko")})
        </div>
    End Using
</div>

<script>
    $('#btnDelete').on('click', function (e) {

        var result = confirm("削除します。よろしいですか？")

        if (result == false) {
            return false
        }
    });
</script>