Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0010")>
<CustomValidation(GetType(M0010), "ValidateHYOJJN")>
<CustomValidation(GetType(M0010), "ValidateKokyuAndHokyu")>
Partial Public Class M0010
	Public Sub New()
		D0020 = New HashSet(Of D0020)()
		D0030 = New HashSet(Of D0030)()
		D0050 = New HashSet(Of D0050)()
		D0060 = New HashSet(Of D0060)()
		D0070 = New HashSet(Of D0070)()
		D0080 = New HashSet(Of D0080)()
		D0090 = New HashSet(Of D0090)()
		W0010 = New HashSet(Of W0010)()
		W00101 = New HashSet(Of W0010)()
		W0050 = New HashSet(Of W0050)()
		W00501 = New HashSet(Of W0050)()
	End Sub

	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.Identity)>
	<Display(Name:="ユーザーID")>
	Public Property USERID As Short

	<Required(ErrorMessage:="{0}が必要です。")>
	<ByteLength(12)>
	<Display(Name:="ユーザーID")>
	Public Property LOGINID As String

	<Required(ErrorMessage:="{0}が必要です。")>
	<MinLength(5, ErrorMessage:="パスワードは５文字以上12文字以下を設定してください。")>
	<MaxLength(12, ErrorMessage:="パスワードは５文字以上12文字以下を設定してください。")>
	<Display(Name:="パスワード")>
	<DataType(DataType.Password)>
	Public Property USERPWD As String

	<NotMapped>
	<Required(ErrorMessage:="{0}が必要です。")>
	<Compare("USERPWD", ErrorMessage:="{1}と一致していません。")>
	<Display(Name:="パスワード確認")>
	<DataType(DataType.Password)>
	Public Property USERPWDCONFRIM As String

	'<NotMapped>
	'<Display(Name:="旧パスワード")>
	'<DataType(DataType.Password)>
	'Public Property USERPWDOLD As String

	<Required(ErrorMessage:="{0}が必要です。")>
	<ByteLength(12)>
	<Display(Name:="氏名")>
	Public Property USERNM As String


	<Display(Name:="性別")>
	<UIHint("USERSEX")>
	Public Property USERSEX As Boolean

	<Display(Name:="月")>
	Public Property KOKYU1 As Boolean

	<Display(Name:="火")>
	Public Property KOKYU2 As Boolean

	<Display(Name:="水")>
	Public Property KOKYU3 As Boolean

	<Display(Name:="木")>
	Public Property KOKYU4 As Boolean

	<Display(Name:="金")>
	Public Property KOKYU5 As Boolean

	<Display(Name:="土")>
	Public Property KOKYU6 As Boolean

	<Display(Name:="日")>
	Public Property KOKYU7 As Boolean

	<Display(Name:="表示順")>
	Public Property HYOJJN As Short?

	<Display(Name:="表示")>
	<DefaultSettingValue("True")>
	Public Property HYOJ As Boolean

	<Display(Name:="ステータス")>
	Public Property STATUS As Boolean

	<Display(Name:="レベル")>
	Public Property ACCESSLVLCD As Short

	<Required(ErrorMessage:="{0}が必要です。")>
	<MaxLength(128)>
	<Display(Name:="メールアドレス")>
	<DataType(DataType.EmailAddress)>
	Public Property MAILLADDESS As String

	<MaxLength(128)>
	<Display(Name:="携帯メールアドレス")>
	Public Property KEITAIADDESS As String

	<Display(Name:="公休展開個人実行")>
	Public Property KOKYUTENKAI As Boolean

	<Display(Name:="公休展開全員実行")>
	Public Property KOKYUTENKAIALL As Boolean

	<Display(Name:="仮アナ")>
	Public Property KARIANA As Boolean

	<Display(Name:="月")>
	Public Property HOKYU1 As Boolean

	<Display(Name:="火")>
	Public Property HOKYU2 As Boolean

	<Display(Name:="水")>
	Public Property HOKYU3 As Boolean

	<Display(Name:="木")>
	Public Property HOKYU4 As Boolean

	<Display(Name:="金")>
	Public Property HOKYU5 As Boolean

	<Display(Name:="土")>
	Public Property HOKYU6 As Boolean

	<Display(Name:="日")>
	Public Property HOKYU7 As Boolean


	<NotMapped>
	Public Property CONFIRMMSG As Boolean?

	<NotMapped>
	<Display(Name:="月")>
	Public Property KYU1 As String

	<NotMapped>
	<Display(Name:="火")>
	Public Property KYU2 As String

	<NotMapped>
	<Display(Name:="水")>
	Public Property KYU3 As String

	<NotMapped>
	<Display(Name:="木")>
	Public Property KYU4 As String

	<NotMapped>
	<Display(Name:="金")>
	Public Property KYU5 As String

	<NotMapped>
	<Display(Name:="土")>
	Public Property KYU6 As String

	<NotMapped>
	<Display(Name:="日")>
	Public Property KYU7 As String

	Public Overridable Property D0020 As ICollection(Of D0020)

	Public Overridable Property D0030 As ICollection(Of D0030)

	Public Overridable Property D0050 As ICollection(Of D0050)

	Public Overridable Property D0060 As ICollection(Of D0060)

	Public Overridable Property D0070 As ICollection(Of D0070)

	Public Overridable Property D0080 As ICollection(Of D0080)

	Public Overridable Property D0090 As ICollection(Of D0090)

	Public Overridable Property M0050 As M0050

	Public Overridable Property W0010 As ICollection(Of W0010)

	Public Overridable Property W00101 As ICollection(Of W0010)

	Public Overridable Property W0050 As ICollection(Of W0050)

	Public Overridable Property W00501 As ICollection(Of W0050)

	<NotMapped>
	<Display(Name:="スポーツカテゴリ")>
	Public Property SportCatCdComaSeperatedString As String

	<NotMapped>
	<Display(Name:="スポーツカテゴリ")>
	Public Property SportCatNmComaSeperatedString As String

	<NotMapped>
	Public Property ChiefFlgsComaSeperatedString As String

	Public Shared Function ValidateHYOJJN(ByVal m0010 As M0010) As ValidationResult
		If m0010 IsNot Nothing Then
			Using db As New Model1
				Dim item = db.M0010.Where(Function(m) m.USERID <> m0010.USERID And m.USERSEX = m0010.USERSEX And m.HYOJJN = m0010.HYOJJN).ToList
				If item IsNot Nothing AndAlso item.Count > 0 Then
					Return New ValidationResult("表示順が重複しています。", New String() {"HYOJJN"})
				End If
			End Using
		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function ValidateKokyuAndHokyu(ByVal m0010 As M0010) As ValidationResult
		If m0010 IsNot Nothing Then
			If (m0010.KOKYU1 = True And m0010.HOKYU1 = True) OrElse
					(m0010.KOKYU2 = True And m0010.HOKYU2 = True) OrElse
					(m0010.KOKYU3 = True And m0010.HOKYU3 = True) OrElse
					(m0010.KOKYU4 = True And m0010.HOKYU4 = True) OrElse
					(m0010.KOKYU5 = True And m0010.HOKYU5 = True) OrElse
					(m0010.KOKYU6 = True And m0010.HOKYU6 = True) OrElse
				(m0010.KOKYU7 = True And m0010.HOKYU7 = True) Then
				Return New ValidationResult("法休と公休が重複しています。", New String() {"KOKYU1"})
			End If
		End If

		Return ValidationResult.Success
	End Function
End Class
