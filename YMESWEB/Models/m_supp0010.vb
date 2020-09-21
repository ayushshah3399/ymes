Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.m_supp0010")>
Partial Public Class m_supp0010
	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(10)>
	Public Property supplier_code As String

	<StringLength(3)>
	Public Property country_code As String

	<Required>
	<StringLength(40)>
	Public Property supplier_name As String

	<Required>
	<StringLength(64)>
	Public Property instid As String

	Public Property instdt As Date

	<Required>
	<StringLength(50)>
	Public Property instterm As String

	<Required>
	<StringLength(50)>
	Public Property instprgnm As String

	<Required>
	<StringLength(64)>
	Public Property updtid As String

	Public Property updtdt As Date

	<Required>
	<StringLength(50)>
	Public Property updtterm As String

	<Required>
	<StringLength(50)>
	Public Property updtprgnm As String
End Class
