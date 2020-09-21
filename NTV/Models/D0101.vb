Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0101")>
Partial Public Class D0101
    <Key>
    <Column(Order:=0, TypeName:="numeric")>
    Public Property HINANO As Decimal

    <Key>
    <Column(Order:=1)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property SEQ As Short

    <Required>
    <StringLength(12)>
    Public Property ANNACATNM As String

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

    Public Overridable Property D0090 As D0090
End Class
