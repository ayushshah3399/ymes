Imports System.ComponentModel.DataAnnotations

Public Class A0230

	Public Property ITEMNM As String

	Public Property CATCD As Short = -1

	Public Property SEARCHDT As Date

	Public Property COLORSTATUS As Short = -1

	Public Property LINKCOLOR As String

	Public Property ITEMCD As String

	Public Property COL_TYPE As String

	Public Property BACKCOLOR As String

	Public Property FONTCOLOR As String

	Public Property KYUSHUTSU As String

	Public Property TITLEKBN As String

	Public Property KAKUNIN As String

	Public Property BANGUMINM As String

	Public Property WAKUCOLOR As String

	Public Property COL_NAME As String

	Public Property SPORTSUBCATCD As Short?

	Public Property GYOMNO As Decimal

	'ASI[05 Dec 2019]: Add this property to decide top bottom color in A0240 Module.
	Public Property TOPWAKU As String
	Public Property BOTTOMWAKU As String
	Public Property BOTTOMBLACKWAKU As String
	Public Property ROWWAKUCOLOR As String
	Public Property FIX_GYOMNO As String
    Public Property DESKMEMOEXISTFLG As Boolean
    Public Property GYOMDT As String
    Public Property HYOJ2 As Boolean
    Public Property HYOJJN As Short?
    Public Property RNZK As Boolean
	Public Property PGYOMNO As Decimal
	Public Property DESKMEMOBOTTOMWAKU As String
	Public Property DESKMEMOTOPWAKU As String
	Public Property BGCOLOR As String
	Public Property AnaLIST As List(Of A0240ANALIST)
	Public Property Desk_Chief_Cat As Boolean

End Class
