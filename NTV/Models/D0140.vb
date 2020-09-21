Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0140")>
Partial Public Class D0140
	<Key>
	<Column(Order:=0)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property USERID As Short

	<Key>
	<Column(Order:=1)>
	Public Property YMD As Date

    <StringLength(60)>
    Public Property USERMEMO As String


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
End Class
