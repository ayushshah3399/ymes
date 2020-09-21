Imports System.ComponentModel.DataAnnotations
<CustomValidation(GetType(C0050), "ValidateMemo")>
<CustomValidation(GetType(C0050), "ValidateJKNRange")>
Public Class C0050

	<Key>
	<Display(Name:="業務番号")>
	Public Property GYOMNO As Decimal

	Public Property PGYOMNO As Decimal

	<Display(Name:="業務日付")>
	Public Property GYOMDT As String

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
	Public Property MYMEMO As String

	Public Property CHILDMEMO As String

	Public Property TITLEKBN As String

	Public Property DATAKBN As String

	Public Property USERID As Short

	Public Property STTIMEupdt As String

	Public Property EDTIMEupdt As String

	Public Property KKNST As Date

	Public Property KKNED As Date

	Public Property Searchdt As String

	Public Property KYUKCD As String

	'ASI[02 Jan 2020] : Added SPORT_OYAFLG, RNZK, SPORTFLG
	Public Property SPORT_OYAFLG As String

	Public Property RNZK As Boolean

	Public Property SPORTFLG As Boolean

	Public Overridable Property C0040 As C0040

	Public Overridable Property WD0040 As WD0040

	Public Shared Function ValidateMemo(ByVal c0050 As C0050) As ValidationResult
		Dim lstError As New List(Of String)

		If c0050 IsNot Nothing Then

			If c0050.MYMEMO IsNot Nothing Then
				If System.Text.Encoding.GetEncoding("shift-jis").GetByteCount(c0050.MYMEMO.ToString) > 60 Then
					Return New ValidationResult("文字数がオーバーしています。", New String() {"MYMEMO"})
				End If
			End If

		End If

		Return ValidationResult.Success
	End Function

	'ASI[02 Jan 2020] : To check Start and End time range of same day single record
	Public Shared Function ValidateJKNRange(ByVal c0050 As C0050) As ValidationResult
		If c0050 IsNot Nothing Then
			If c0050.STTIME IsNot Nothing And c0050.EDTIME IsNot Nothing Then
				Dim jtjknst As Date = GetJtjkn(c0050.GYOMDT, c0050.STTIME)
				Dim jtjkned As Date = GetJtjkn(c0050.GYOMDT, c0050.EDTIME)

				If jtjknst > jtjkned Then
					Return New ValidationResult("開始と終了の前後関係が誤っています。", New String() {"STTIME"})
				End If
			End If

		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function GetJtjkn(ByVal dt As Date, ByVal time As String) As Date
		Dim dtRtn As Date = Nothing
		Dim strHH As String = ""
		Dim strMM As String = ""

		If time.Contains(":") Then
			Dim strs As String() = time.Split(":")
			strHH = strs(0).PadLeft(2, "0")
			strMM = strs(1).PadLeft(2, "0")
		Else
			If time.Length <= 2 Then
				strHH = time.PadLeft(2, "0")
				strMM = "00"
			ElseIf time.Length = 3 Then
				strHH = time.Substring(0, 1).PadLeft(2, "0")
				strMM = time.Substring(1, 2)
			Else
				strHH = time.Substring(0, 2)
				strMM = time.Substring(2, 2)
			End If
		End If

		'36:00まで登録可能なので、実時間を２４時間制度に変更する
		If strHH >= "24" Then
			Dim intHH As Integer = Integer.Parse(strHH) - 24
			strHH = intHH.ToString.PadLeft(2, "0")
			dt = dt.AddDays(1)
		End If

		dtRtn = Date.Parse(dt.ToString("yyyy/MM/dd") & " " & strHH & ":" & strMM)

		Return dtRtn
	End Function


End Class
