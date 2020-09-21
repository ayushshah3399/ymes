@ModelType NTV_SHIFT.D0040
@Code
    ViewData("Title") = "時間休削除"
End Code


<div class="row">
    <div class="col-md-2" style="padding-top:10px;">
        <ul class="nav nav-pills ">
            @If Session("C0040DeleteRtnUrl" & Model.USERID) IsNot Nothing Then
                    @<li><a href="@Session("C0040DeleteRtnUrl" & Model.USERID).ToString" id="btnModoru">戻る</a></li>
                Else
                    @<li>@Html.ActionLink("戻る", "Index", "C0040", Nothing, htmlAttributes:=New With {.id = "btnModoru"})</li>
                End If

           <!-- @If Session("C0040DeleteRtnUrl") IsNot Nothing Then
                @<li><a href="@Session("C0040DeleteRtnUrl").ToString" >戻る</a></li>
            Else
                @<li>@Html.ActionLink("戻る", "Index", "C0040")</li>
            End If-->
        </ul>
    </div>

    <div class="col-md-10 col-md-pull-2">
        <h3>削除</h3>
    </div>
</div>

<h3>削除してもよろしいですか？</h3>
<div>
    <hr />
    <dl class="dl-horizontal">       
        
        <dt>
            @Html.label("期間")
        </dt>
        <dd>
            @ViewBag.YasumiHi  @Html.Encode("～") @ViewBag.YasumiHi
        </dd>


        <dt>
            @Html.label("時間")
        </dt>
        <dd>           
            @Html.DisplayFor(Function(model) model.JKNST).ToString.Substring(0, 2)
            @Html.Encode(":")
            @Html.DisplayFor(Function(model) model.JKNST).ToString.Substring(2, 2)

            @Html.Encode("～")

            @Html.DisplayFor(Function(model) model.JKNED).ToString.Substring(0, 2)
            @Html.Encode(":")
            @Html.DisplayFor(Function(model) model.JKNED).ToString.Substring(2, 2)
             @Html.Hidden("JKNST", model.JKNST)
        </dd>


        <dt>
            @Html.label("休暇の種類")
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.M0060.KYUKNM)
        </dd>

        <dt>
            @Html.label("備考")
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.BIKO)
        </dd>        

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
             @Html.Hidden("nengetsu", ViewBag.NENGETU)
             @Html.Hidden("HI", ViewBag.HI)
            <input id="btnDelete" type="submit" value="削除" class="btn btn-default" /> |

            @If Session("C0040DeleteRtnUrl" & Model.USERID) IsNot Nothing Then
                @<a href="@Session("C0040DeleteRtnUrl" & Model.USERID).ToString" id="btnModoru">個人シフトに戻る</a>
            Else
                @Html.ActionLink("個人シフトに戻る", "Index", "C0040", Nothing, htmlAttributes:=New With {.id = "btnModoru"})
            End If

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