Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0090")>
Partial Public Class W0090
    <Key>
    <Column(Order:=0)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property ACUSERID As Short

    <Key>
    <Column(Order:=1, TypeName:="numeric")>
    Public Property GYOMNO As Decimal

	<Key>
	<Column(Order:=2)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property USERID As Short

	Public Property KYUKCD As Short

	Public Overridable Property M0010 As M0010

	Public Overridable Property M0060 As M0060

End Class
