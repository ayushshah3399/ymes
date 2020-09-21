Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0060")>
	<CustomValidation(GetType(M0060), "ValidateHYOJJN")>
Partial Public Class M0060
	Public Sub New()
		D0040 = New HashSet(Of D0040)()
		D0060 = New HashSet(Of D0060)()
	End Sub

	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.Identity)>
	Public Property KYUKCD As Short

	<Required>
	<StringLength(12)>
	<Display(Name:="ãxâ…ñºèÃ")>
	Public Property KYUKNM As String

	<StringLength(6)>
		<Display(Name:="ãxâ…ó™èÃ")>
	Public Property KYUKRYKNM As String

	<StringLength(7)>
		<Display(Name:="ÉZÉãîwåiêF")>
	Public Property BACKCOLOR As String

	<StringLength(7)>
		<Display(Name:="ÉZÉãògêF")>
	Public Property WAKUCOLOR As String

	<StringLength(7)>
		<Display(Name:="ï∂éöêF")>
	Public Property FONTCOLOR As String

	<Display(Name:="ï\é¶èá")>
	Public Property HYOJJN As Short

	<Display(Name:="íSìñï\ï\é¶")>
	Public Property HYOJ As Boolean

	<Display(Name:="íSìñï\ñ}ó·ï\é¶")>
	Public Property TNTHYOHYOJ As Boolean

	<Display(Name:="ãxì˙ï\ï\é¶")>
	Public Property KYUJITUHYOJ As Boolean

	<Display(Name:="ãxâ…ê\êøï\é¶")>
	Public Property SHINSEIHYOJ As Boolean

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

	Public Overridable Property D0040 As ICollection(Of D0040)

	Public Overridable Property D0060 As ICollection(Of D0060)

	Public Shared Function ValidateHYOJJN(ByVal m0060 As M0060) As ValidationResult
		If m0060 IsNot Nothing Then
			Using db As New Model1
				Dim item = db.M0060.Where(Function(m) m.KYUKCD <> m0060.KYUKCD And m.HYOJJN = m0060.HYOJJN).ToList
				If item IsNot Nothing AndAlso item.Count > 0 Then
					Return New ValidationResult("ï\é¶èáÇ™èdï°ÇµÇƒÇ¢Ç‹Ç∑ÅB", New String() {"HYOJJN"})
				End If
			End Using
		End If

		Return ValidationResult.Success
	End Function

End Class
