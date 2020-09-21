Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0100")>
Partial Public Class M0100
	Public Sub New()
		D0110 = New HashSet(Of D0110)()
	End Sub

	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property KAKUNINID As Short

	<Required>
	<StringLength(20)>
	Public Property KAKUNINNM As String

	Public Property KOHOHYOJ As Boolean

	<Required>
	<StringLength(64)>
	Public Property INSTID As String

	<Column(TypeName:="datetime2")>
	Public Property INSTDT As Date

	<Required>
	<StringLength(50)>
	Public Property INSTTERM As String

	<Required>
	<StringLength(50)>
	Public Property INSTPRGNM As String

	<Required>
	<StringLength(64)>
	Public Property UPDTID As String

	<Column(TypeName:="datetime2")>
	Public Property UPDTDT As Date

	<Required>
	<StringLength(50)>
	Public Property UPDTTERM As String

	<Required>
	<StringLength(50)>
	Public Property UPDTPRGNM As String

	Public Overridable Property D0110 As ICollection(Of D0110)
End Class
