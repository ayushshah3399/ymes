Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

'<CustomValidation(GetType(M0130), "ValidateMapping")>
<Table("TeLAS.M0130")>
Partial Public Class M0130

	Public Sub New()
		M0140 = New HashSet(Of M0140)()
		M0150 = New HashSet(Of M0150)()
	End Sub

	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.Identity)>
	Public Property SPORTCATCD As Short

	<Required(ErrorMessage:="{0}が必要です。")>
	<Display(Name:="スポーツカテゴリー名")>
	<ByteLength(20)>
	Public Property SPORTCATNM As String

	<Required(ErrorMessage:="{0}が必要です。")>
	<Display(Name:="表示順")>
	Public Property HYOJJN As Short?

	<Display(Name:="表示")>
	<UIHint("HYOJ")>
	Public Property HYOJ As Boolean

	<StringLength(64)>
	Public Property INSTID As String

	<Column(TypeName:="datetime2")>
	Public Property INSTDT As Date

	<StringLength(50)>
	Public Property INSTTERM As String

	<StringLength(50)>
	Public Property INSTPRGNM As String

	<StringLength(64)>
	Public Property UPDTID As String

	<Column(TypeName:="datetime2")>
	Public Property UPDTDT As Date

	<StringLength(50)>
	Public Property UPDTTERM As String

	<StringLength(50)>
	Public Property UPDTPRGNM As String

	<NotMapped>
	Public Property CHECKEDSTATUS As Boolean = True

	<NotMapped>
	Public Overridable Property M0140 As ICollection(Of M0140)

	<NotMapped>
	Public Overridable Property M0150 As ICollection(Of M0150)

	'Public Shared Function ValidateMapping(ByVal m0130 As M0130) As ValidationResult

	'	Dim opType() As String = HttpContext.Current.Request.Form.GetValues("opType")
	'	Dim operationType As String = opType(0)

	'	'If it is save opration
	'	If operationType = "2" Then

	'		'Get value from session
	'		If HttpContext.Current.Session("m0150") IsNot Nothing Then

	'			'Get value of M0150
	'			Dim m0150List As ICollection(Of M0150) = HttpContext.Current.Session("m0150")


	'			For i As Integer = 0 To m0130.M0140.Count - 1

	'				If m0130.M0140(i).SPORTSUBCATCD <> 0 Then
	'					Continue For
	'				End If

	'				Dim matchCount = 0

	'				For j As Integer = 0 To m0150List.Count - 1

	'					'Check matching record of M0140 and M0150
	'					If m0130.M0140(i).SELECTEDINDEX = m0150List(j).SELECTINDEX Then

	'						matchCount = 1
	'					End If

	'				Next

	'				If matchCount = 0 Then
	'					Return New ValidationResult("詳細設定が必要です。", New String() {String.Format("M0140[{0}].SELECTEDINDEX", i)})
	'				End If

	'			Next

	'		Else

	'			Dim FirstSelectedIndex As Integer = -1

	'			For i As Integer = 0 To m0130.M0140.Count - 1

	'				If m0130.M0140(i).SPORTSUBCATCD <> 0 Then
	'					Continue For
	'				Else
	'					FirstSelectedIndex = i
	'					Exit For
	'				End If

	'			Next

	'			If FirstSelectedIndex <> -1 Then
	'				Return New ValidationResult("詳細設定が必要です。", New String() {String.Format("M0140[{0}].SELECTEDINDEX", FirstSelectedIndex)})
	'			End If

	'		End If


	'		End If

	'	Return ValidationResult.Success
	'End Function

End Class
