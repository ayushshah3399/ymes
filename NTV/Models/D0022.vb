Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0022")>
Partial Public Class D0022
	<Key>
	<Column(Order:=0, TypeName:="numeric")>
	Public Property GYOMNO As Decimal

	<Key>
	<Column(Order:=1)>
	<StringLength(20)>
	Public Property COLNM As String

	Public Property USERID As Short

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
	Public Property COLIDX As Short

End Class
