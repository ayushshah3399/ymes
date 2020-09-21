@ModelType IEnumerable(Of NTV_SHIFT.M0010)

@Code
	Dim bolDataHave = False
	Dim fontColor As String = ""
	Dim backColor As String = ""
	Dim wakuColor As String = ""
	Dim bolTenkai As Boolean = False
	Dim strCellColor As String = ""
End Code

<style>
	table.table-bordered.table-scroll tbody,
	table.table-scroll thead {
		display: block;
	}

	table.table-bordered.table-scroll tbody {
		height: 542px;
		width: 209px;
		overflow-y: auto;
		overflow-x: hidden;
	}

	.colusernm {
		width: 100px;
	}

	.hyojjn {
		width: 90px;
	}
</style>


@If ViewData("frompage") = "C0030" Then
	Dim dtSearch As Date = Nothing
	Date.TryParse(ViewData("searchdt").ToString, dtSearch)
	'dtSearch = dtSearch.AddDays(-1)
	@<div class="row" style="overflow-y:auto; max-height:400px;">
		<div class="col-sm-6 userlist-col-1">
			<ul class="list-group">
				@For Each item In Model
					'色の初期化
					backColor = ""
					wakuColor = ""
					fontColor = ""
					@If item.USERSEX = False Then
						bolDataHave = False
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = True
								'勤務、休出、２４時間超えの時は枠色を取得、それ以外の休暇には背景色を取得
								'ASI[23 Oct 2019]: Added 法休出、24時超法休出 [KYUKCD 13,14] in below condition
								@If itemColor.KYUKCD <> "2" AndAlso itemColor.KYUKCD <> "10" AndAlso itemColor.KYUKCD <> "13" AndAlso itemColor.KYUKCD <> "14" Then
									backColor = "#" & itemColor.M0060.BACKCOLOR
								Else
									@If itemColor.KYUKCD <> "1" Then
										wakuColor = "#" & itemColor.M0060.wakucolor
									End If

								End If

								'フォントの色取得
								@If itemColor.M0060.FONTCOLOR IsNot Nothing Then
									fontColor = "#" & itemColor.M0060.FONTCOLOR
								Else
									fontColor = "#" & ViewData.Item("KinmuColor")
								End If

							End If

						Next
						'休日データあったら、それに合わせて色付ける
						If bolDataHave = True Then
							If wakuColor = "" Then
								@<li class="list-group-item userlist-item" style="background-color: @backColor ;  border-collapse:separate; margin-bottom:0px; padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="background-color: @backColor ; border:2px solid @wakuColor; border-collapse:separate; margin-bottom:0px; padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							End If

						End If


						'休日ない時、公休展開していないユーザーはグレー、それ以外は普通の色
						If bolDataHave = False Then

							fontColor = "#" & ViewData.Item("KinmuColor")

							bolTenkai = False
							@For Each itemT In ViewData.Item("TenkaiList")
								If itemT.USERID = item.USERID Then
									bolTenkai = True
									Exit For

								End If
							Next
							If bolTenkai = True Then
								@<li class="list-group-item userlist-item" style="border-collapse:separate; margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="border-collapse:separate; border:2px solid black; margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:gray"})
								</li>
							End If
						End If
					End If

				Next
			</ul>
		</div>
		<div class="col-sm-6 userlist-col-2">
			<ul class="list-group">
				@For Each item In Model
					'色の初期化
					backColor = ""
					wakuColor = ""
					fontColor = ""
					@If item.USERSEX = True Then
						bolDataHave = False
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = True
								'勤務、休出、２４時間超えの時は枠色を取得、それ以外の休暇には背景色を取得
								@If itemColor.KYUKCD <> "2" AndAlso itemColor.KYUKCD <> "10" Then
									backColor = "#" & itemColor.M0060.BACKCOLOR
								Else
									@If itemColor.KYUKCD <> "1" Then
										wakuColor = "#" & itemColor.M0060.wakucolor
									End If

								End If

								'フォントの色取得
								@If itemColor.M0060.FONTCOLOR IsNot Nothing Then
									fontColor = "#" & itemColor.M0060.FONTCOLOR
								Else
									fontColor = "#" & ViewData.Item("KinmuColor")
								End If
							End If
						Next

						'休日データあったら、それに合わせて色付ける
						If bolDataHave = True Then
							If wakuColor = "" Then
								@<li class="list-group-item userlist-item" style="background-color:@backColor;   border-collapse:separate;  margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="background-color:@backColor;  border:2px solid @wakuColor;  border-collapse:separate;  margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							End If

						End If

						'休日ない時、公休展開していないユーザーはグレー、それ以外は普通の色
						If bolDataHave = False Then

							fontColor = "#" & ViewData.Item("KinmuColor")

							bolTenkai = False
							@For Each itemT In ViewData.Item("TenkaiList")
								If itemT.USERID = item.USERID Then
									bolTenkai = True

									Exit For

								End If
							Next
							If bolTenkai = True Then
								@<li class="list-group-item userlist-item" style="border-collapse:separate; margin-bottom:0px; padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="border-collapse:separate ; border:2px solid black;  margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:gray"})
								</li>
							End If

						End If
					End If
				Next
			</ul>
		</div>
	</div>

ElseIf ViewData("frompage") = "B0040" Then
	@<div class="row" style="overflow-y:auto; max-height:390px;">
		<div class="col-sm-6 userlist-col-1">
			<ul class="list-group">

				@For Each item In Model

					@If item.USERSEX = False Then

						bolDataHave = False
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = True

								@<li class="list-group-item userlist-item">
									@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt"), .formname = "B0040"}, htmlAttributes:=New With {.style = "color:blue;"})
								</li>
								Exit For

							End If
						Next
						If bolDataHave = False Then
							@<li class="list-group-item userlist-item">
								@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt"), .formname = "B0040"}, htmlAttributes:=New With {.style = "color:black;"})
							</li>
						End If
					End If

				Next
			</ul>
		</div>
		<div class="col-sm-6 userlist-col-2">
			<ul class="list-group">

				@For Each item In Model

					@If item.USERSEX = True Then
						bolDataHave = False
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = True

								@<li class="list-group-item userlist-item">
									@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt")}, htmlAttributes:=New With {.style = "color:blue;"})
								</li>
								Exit For

							End If
						Next
						If bolDataHave = False Then
							@<li class="list-group-item userlist-item">
								@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt")}, htmlAttributes:=New With {.style = "color:black;"})
							</li>
						End If
					End If

				Next

			</ul>
		</div>
	</div>

ElseIf ViewData("frompage") = "B0050" Then
	@<div class="row" style="overflow-y:auto; max-height:390px;">
		<div class="col-sm-6 userlist-col-1">
			<ul class="list-group">

				@For Each item In Model
					@If item.MAILLADDESS = "1" Then
						strCellColor = "FF0000"
					Else
						strCellColor = ""
					End If
					@If item.USERSEX = False Then

						bolDataHave = False
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = True

								@<li class="list-group-item userlist-item" style="background-color:#@strCellColor">
									@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt")}, htmlAttributes:=New With {.style = "color:blue;"})
								</li>
								Exit For

							End If
						Next
						If bolDataHave = False Then
							@<li class="list-group-item userlist-item" style="background-color:#@strCellColor">
								@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt")}, htmlAttributes:=New With {.style = "color:black;"})
							</li>
						End If
					End If

				Next
			</ul>
		</div>
		<div class="col-sm-6 userlist-col-2">
			<ul class="list-group">

				@For Each item In Model
					@If item.MAILLADDESS = "1" Then
						strCellColor = "FF0000"
					Else
						strCellColor = ""
					End If
					@If item.USERSEX = True Then
						bolDataHave = False
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = True

								@<li class="list-group-item userlist-item" style="background-color:#@strCellColor">
									@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt")}, htmlAttributes:=New With {.style = "color:blue;"})
								</li>
								Exit For

							End If
						Next
						If bolDataHave = False Then
							@<li class="list-group-item userlist-item" style="background-color:#@strCellColor">
								@Html.ActionLink(item.USERNM, "Index", "B0050", routeValues:=New With {.name = item.USERNM.ToString, .userid = item.USERID, .showdate = ViewData("searchdt")}, htmlAttributes:=New With {.style = "color:black;"})
							</li>
						End If
					End If

				Next

			</ul>
		</div>
	</div>

ElseIf ViewData("frompage") = "A0110" Then

	'ユーザー設定の新規と修正の場合
	@<div class="row">
		<div class="col-sm-6 userlist-col-1" style="width:219px;">
			<table class="table table-bordered table-scroll" style="text-align:center">
				<thead>
					<tr>
						<th class="colusernm">
							@Html.DisplayNameFor(Function(model) model.USERNM)
						</th>
						<th class="hyojjn">
							@Html.DisplayNameFor(Function(model) model.HYOJJN)
						</th>
					</tr>
				</thead>
				<tbody>
					@For Each item In Model
						@If item.USERSEX = False Then
							@<tr>
								<td class="colusernm" style="padding:4px;">
									@item.USERNM
								</td>
								<td class="hyojjn" style="padding:4px;">
									@item.HYOJJN
								</td>
							</tr>
						End If
					Next
				</tbody>

			</table>
		</div>

		<div class="col-sm-6 userlist-col-2" style="width:219px;margin-left:10px;">
			<table class="table table-bordered table-scroll" style="text-align:center">
				<thead>
					<tr>
						<th class="colusernm">
							@Html.DisplayNameFor(Function(model) model.USERNM)
						</th>
						<th class="hyojjn">
							@Html.DisplayNameFor(Function(model) model.HYOJJN)
						</th>
					</tr>
				</thead>
				<tbody>
					@For Each item In Model
						@If item.USERSEX = True Then
							@<tr>
								<td class="colusernm" style="padding:4px;">
									@item.USERNM
								</td>
								<td class="hyojjn" style="padding:4px;">
									@item.HYOJJN
								</td>
							</tr>
						End If
					Next
				</tbody>

			</table>
		</div>
	</div>

ElseIf ViewData("frompage") = "C0050" Then
	Dim dtSearch As Date = Nothing
	Date.TryParse(ViewData("searchdt").ToString, dtSearch)

	@<div class="row" style="overflow-y:auto; max-height:400px;">
		<div class="col-sm-6 userlist-col-1">
			<ul class="list-group">
				@For Each item In Model
					'男性は左側
					@If item.USERSEX = False Then
						'変数の初期化
						backColor = ""
						wakuColor = ""
						fontColor = "#" & ViewData.Item("KinmuColor")
						bolDataHave = False
						bolTenkai = False
						'ユーザーの色情報を取得する
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = (itemColor.KYUKCD <> -1)  'True:休暇設定あり
								bolTenkai = (itemColor.TENKAI = "1")   'True:公休展開

								'勤務、休出、２４時間超えの時は枠色を取得、それ以外の休暇には背景色を取得
								'ASI[23 Oct 2019]: Added 法休出、24時超法休出 [KYUKCD 13,14] in below condition
								@If itemColor.KYUKCD <> 2 AndAlso itemColor.KYUKCD <> 10 AndAlso itemColor.KYUKCD <> 13 AndAlso itemColor.KYUKCD <> 14 Then
									backColor = "#" & itemColor.BACKCOLOR
								Else
									@If itemColor.KYUKCD <> 1 Then
										wakuColor = "#" & itemColor.WAKUCOLOR
									End If
								End If

								'フォントの色取得
								@If itemColor.FONTCOLOR <> "" Then
									fontColor = "#" & itemColor.FONTCOLOR
								End If

								'出張の場合は背景色を「出張」で設定されている背景色にする
								@if itemColor.SHUCHO = "1" Then
									backColor = "#" & itemColor.BACKCOLOR
								End If

								Exit For
							End If
						Next

						'休日データあったら、それに合わせて色付ける
						If bolDataHave = True Then
							If wakuColor = "" Then
								@<li class="list-group-item userlist-item" style="background-color: @backColor ;  border-collapse:separate; margin-bottom:0px; padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="background-color: @backColor ; border:2px solid @wakuColor; border-collapse:separate; margin-bottom:0px; padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							End If
						Else
							'休日ない時、公休展開していないユーザーはグレー、それ以外は普通の色
							If bolTenkai = True Then
								@<li class="list-group-item userlist-item" style="border-collapse:separate; margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="border-collapse:separate; border:2px solid black; margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:gray"})
								</li>
							End If
						End If
					End If
				Next
			</ul>
		</div>
		<div class="col-sm-6 userlist-col-2">
			<ul class="list-group">
				@For Each item In Model
					'女性は右側
					@If item.USERSEX = True Then
						'色の初期化
						backColor = ""
						wakuColor = ""
						fontColor = "#" & ViewData.Item("KinmuColor")
						bolDataHave = False
						bolTenkai = False
						'ユーザーの色情報を取得する
						@For Each itemColor In ViewData.Item("UserColor")
							If itemColor.USERID = item.USERID Then
								bolDataHave = (itemColor.KYUKCD <> -1)  'True:休暇設定あり
								bolTenkai = (itemColor.TENKAI = "1")   'True:公休展開

								'勤務、休出、２４時間超えの時は枠色を取得、それ以外の休暇には背景色を取得
								@If itemColor.KYUKCD <> 2 AndAlso itemColor.KYUKCD <> 10 Then
									backColor = "#" & itemColor.BACKCOLOR
								Else
									@If itemColor.KYUKCD <> "1" Then
										wakuColor = "#" & itemColor.WAKUCOLOR
									End If
								End If

								'フォントの色取得
								@If itemColor.FONTCOLOR <> "" Then
									fontColor = "#" & itemColor.FONTCOLOR
								End If

								'出張の場合は背景色を「出張」で設定されている背景色にする
								@if itemColor.SHUCHO = "1" Then
									backColor = "#" & itemColor.BACKCOLOR
								End If

								Exit For
							End If
						Next

						'休日データあったら、それに合わせて色付ける
						If bolDataHave = True Then
							If wakuColor = "" Then
								@<li class="list-group-item userlist-item" style="background-color:@backColor;   border-collapse:separate;  margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="background-color:@backColor;  border:2px solid @wakuColor;  border-collapse:separate;  margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							End If
						Else
							'休日ない時、公休展開していないユーザーはグレー、それ以外は普通の色
							If bolTenkai = True Then
								@<li class="list-group-item userlist-item" style="border-collapse:separate; margin-bottom:0px; padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:" & fontColor})
								</li>
							Else
								@<li class="list-group-item userlist-item" style="border-collapse:separate ; border:2px solid black;  margin-bottom:0px;padding:0px; ">
									@Html.ActionLink(item.USERNM, "Index", "C0040", routeValues:=New With {.name = item.USERNM.ToString, .id = item.USERID, .stdt = dtSearch.ToString}, htmlAttributes:=New With {.style = "color:gray"})
								</li>
							End If
						End If
					End If
				Next
			</ul>
		</div>
	</div>

Else
	@<div class="row" style="overflow-y:auto; max-height:550px;">
		<div class="col-sm-6 userlist-col-1">
			<ul class="list-group">
				@For Each item In Model
					@If item.USERSEX = False Then
						@<li class="list-group-item userlist-item">
							@item.USERNM
						</li>
					End If
				Next
			</ul>
		</div>
		<div class="col-sm-6 userlist-col-2">
			<ul class="list-group">
				@For Each item In Model
					@If item.USERSEX = True Then
						@<li class="list-group-item userlist-item">
							@item.USERNM
						</li>
					End If
				Next
			</ul>
		</div>
	</div>
End If












