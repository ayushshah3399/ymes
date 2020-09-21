Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class C0040

	<Key>
	<Display(Name:="業務番号")>
	Public Property GYOMNO As Decimal

	Public Property PGYOMNO As Decimal

	<Display(Name:="業務日付")>
	Public Property GYOMDT As String

	<Display(Name:="日")>
	Public Property HI As String

	<Display(Name:="曜日")>
	Public Property YOBI As String


	<Display(Name:="確認")>
	Public Property KAKUNIN As String

	<Display(Name:="開始")>
	Public Property STTIME As String

	<Display(Name:="終了")>
	Public Property EDTIME As String

	<Display(Name:="カテゴリー")>
	Public Property CATCD As String

	<Display(Name:="番組名")>
	Public Property BANGUMINM As String

	<Display(Name:="内容")>
	Public Property NAIYO As String

	<Display(Name:="場所")>
	Public Property BASHO As String

	<Display(Name:="備考")>
	Public Property MEMO As String

	<Display(Name:="個人メモ")>
	<StringLength(60)>
	Public Property MYMEMO As String

	Public Property CHILDMEMO As String

	Public Property TITLEKBN As String

	Public Property DATAKBN As String

	Public Property USERID As Short

	Public Property STTIMEupdt As String

	Public Property EDTIMEupdt As String

	Public Property KKNST As Date

	Public Property KKNED As Date

	Public Property KYUKCD As String

	Public Property CATNM As String

	<StringLength(7)>
	<Display(Name:="セル背景色")>
	Public Property BACKCOLOR As String

	<StringLength(7)>
	<Display(Name:="セル枠色")>
	Public Property WAKUCOLOR As String

	<StringLength(7)>
	<Display(Name:="文字色")>
	Public Property FONTCOLOR As String

	Public Property KYUSHUTSU As String

	Public Property OVER As String

	<StringLength(7)>
	<Display(Name:="セル背景色")>
	Public Property ROWBACKCOLOR As String

	<StringLength(7)>
	<Display(Name:="セル枠色")>
	Public Property ROWWAKUCOLOR As String

	<StringLength(7)>
	<Display(Name:="文字色")>
	Public Property ROWFONTCOLOR As String

	Public Property MYMEMOFLG As String

	'ASI[08 Nov 2019]: Add this property to decide is there deskmemo exist for date or not.
	Public Property DESKMEMOEXISTFLG As Boolean

	'ASI[05 Dec 2019]: Add this property to decide top bottom color in A0240 Module.
	Public Property TOPWAKU As String
	Public Property BOTTOMWAKU As String
	Public Property BOTTOMBLACKWAKU As String

	Public Property SPORT_OYAFLG As String

	Public Property RNZK As Boolean

	<Column(TypeName:="datetime")>
	Public Property JTJKNED As Date

	'ASI[02 Jan 2020] : Added SPORTFLG
	Public Property SPORTFLG As Boolean

	Public Overridable Property C0050 As ICollection(Of C0050)

	Public Overridable Property D0010 As D0010
	Public Overridable Property D0060 As D0060
	Public Overridable Property D0050 As D0050
	Public Overridable Property M0020 As M0020
	Public Overridable Property M0010 As M0010

End Class
