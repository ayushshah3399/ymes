@*@ModelType IEnumerable(Of NTV_SHIFT.W0010)*@
@code
    Dim lstFutekigonashi = DirectCast(ViewBag.Futekinashi, List(Of W0010))
    Dim lstChokakinmu = DirectCast(ViewBag.Chokakinmu, List(Of W0010))
    Dim lstZengo10ji = DirectCast(ViewBag.Zengo10ji, List(Of W0010))
    Dim lstJikankyu = DirectCast(ViewBag.Jikankyu, List(Of W0010))
    Dim lstTouJikankyu = DirectCast(ViewBag.TouJikankyu, List(Of W0010))
    Dim lstKoukyu = DirectCast(ViewBag.Koukyu, List(Of W0010))
    Dim lstDaikyu = DirectCast(ViewBag.Daikyu, List(Of W0010))
    Dim lstFurikyu = DirectCast(ViewBag.Furikyu, List(Of W0010))
    Dim lstKyokyu = DirectCast(ViewBag.Kyokyu, List(Of W0010))
    Dim lstJikyokyu = DirectCast(ViewBag.Jikyokyu, List(Of W0010))
    Dim lstTouJikyokyu = DirectCast(ViewBag.TouJikyokyu, List(Of W0010))
    Dim lstkoekyude = DirectCast(ViewBag.koekyude, List(Of W0010))
    Dim lstWBooking = DirectCast(ViewBag.WBooking, List(Of W0010))
    Dim M0060Kyukde = DirectCast(ViewBag.KyukDe, M0060)
    Dim M0060KyukHouDe = DirectCast(ViewBag.KyukHouDe, M0060)

    @*ASI[17 Oct 2019]: get list of employee under these 3 newly added leave from ViewBag*@
    Dim lstHoukyu = DirectCast(ViewBag.Houkyu, List(Of W0010))
    Dim lstFurikaehoukyu = DirectCast(ViewBag.Furikaehoukyu, List(Of W0010))
    Dim lstKoehoukyu24 = DirectCast(ViewBag.Koehoukyu24, List(Of W0010))
End Code


@*@If Model IsNot Nothing Then*@
    <div class="panel panel-default" style="overflow-y:auto; padding-top:5px; padding-bottom:5px; padding-left:15px; max-height:450px; max-width:420px;  ">

        <table id="tblKoho" class="tbllayout"  >

            <tr><th colspan="3">不適合要因なし</th></tr>

            @For Each item In lstFutekigonashi
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)

                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                    <td>
                        @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                        @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>　
                        End If
                        @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                        End If
                    </td>
                    <td>
                        @If item.MARKSYTK = True Then
                            @<font style="background-color: yellow;">※</font>
                        End If
                    </td>
                    <td>
                        <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                            選択
                        </button> |

                        @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})
  
                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">超過勤務</th></tr>
            @For Each item In lstChokakinmu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font> 
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                        <td>
                            <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                                選択
                            </button> |

                        @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">前後日10時間未満</th> </tr>
            @For Each item In lstZengo10ji
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                              <td>
                                  <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                                      選択
                                  </button> |

                         @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">時間休</th></tr>
            @For Each item In lstJikankyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                     <td>
                         <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                             選択
                         </button> |

                         @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">当日時間休あり</th></tr>
            @For Each item In lstTouJikankyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       @Html.HiddenFor(Function(modelItem) item.USERID)
                       @Html.HiddenFor(Function(modelItem) item.YOINID)
                       
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3"> 公休</th></tr>
            @For Each item In lstKoukyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       @Html.HiddenFor(Function(modelItem) item.USERID)
                       @Html.HiddenFor(Function(modelItem) item.YOINID)
                       
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">法休</th></tr>
            @For Each item In lstHoukyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       @Html.HiddenFor(Function(modelItem) item.USERID)
                       @Html.HiddenFor(Function(modelItem) item.YOINID)
                       
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">代休</th></tr>
            @For Each item In lstDaikyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">振公休</th></tr>
            @For Each item In lstFurikyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">振法休</th></tr>
            @For Each item In lstFurikaehoukyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       @Html.HiddenFor(Function(modelItem) item.USERID)
                       @Html.HiddenFor(Function(modelItem) item.YOINID)
                      
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr colspan="3"> <th>強休 </th> </tr>
            @For Each item In lstKyokyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3"> 時間強休    </th></tr>
            @For Each item In lstJikyokyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">当日時間強休あり</th></tr>
            @For Each item In lstTouJikyokyu
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">24時超え公休出</th></tr>
            @For Each item In lstkoekyude
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>

            Next

            <tr><th colspan="3">24時超え法休出</th></tr>
            @For Each item In lstKoehoukyu24
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       @Html.HiddenFor(Function(modelItem) item.USERID)
                       @Html.HiddenFor(Function(modelItem) item.YOINID)
                       
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                   <td>
                       <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                           選択
                       </button> |

                       @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

            <tr><th colspan="3">Wブッキング</th></tr>
            @For Each item In lstWBooking
                @<tr class="yoinid_@item.YOINID">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        @Html.HiddenFor(Function(modelItem) item.USERID)
                        @Html.HiddenFor(Function(modelItem) item.YOINID)
                        
                        @*ASI[23 Oct 2019]: Added Condition of MARKJYUYO*@
                        @if item.MARKJYUYO = True Then
						    @<font style="color:red">@Html.DisplayFor(Function(modelItem) item.M00101.USERNM)</font>
					    Else
						    @Html.DisplayFor(Function(modelItem) item.M00101.USERNM)
					    End If
                    </td>
                     <td>
                         @If item.SPORTSHIFT = True Then
                            @<font style="background-color:blue; color:white;">ス</font>
                        End If
                         @If item.MARKKYST = True Then
                            @<font style="background-color:#@M0060kyukde.backcolor; color:#@M0060Kyukde.FONTCOLOR; border:1px solid #@M0060Kyukde.WAKUCOLOR">公出</font>
                         End If
                         @If item.MARKHOUKYST = True Then
                            @<font style="background-color:#@M0060KyukHouDe.backcolor; color:#@M0060KyukHouDe.FONTCOLOR; border:1px solid #@M0060KyukHouDe.WAKUCOLOR">法出</font>
                         End If
                     </td>
                     <td>
                         @If item.MARKSYTK = True Then
                             @<font style="background-color: yellow;">※</font>
                         End If
                     </td>
                  <td>
                      <button class="btn btn-success btn-xs" data-toggle="modal" data-target="#myModal" data-userid="@item.USERID" data-usernm="@item.M00101.USERNM" data-kariana="@item.M00101.KARIANA">
                          選択
                      </button> |

                      @Html.ActionLink("シフト", "Index", "C0040", routeValues:=New With {.name = item.M00101.USERNM, .id = item.USERID, .stdt = ViewBag.GYOMYMD, .formname = "B0020"}, htmlAttributes:=New With {.target = "_blank"})

                        @If item.DESKMEMO = True Then
                            @Html.Encode(" | ")
                            @Html.ActionLink("デスクメモ", "Index", "A0200", routeValues:=New With {.CondAnaid = item.USERID, .CondShiftst = ViewBag.Gyomymd, .CondShifted = ViewBag.Gyomymded, .CondKakunin1="false", .CondKakunin2="true"}, htmlAttributes:=New With {.target = "_blank"})
                        End If
                    </td>
                </tr>
            Next

        </table>
    
    </div>

@*End If*@



