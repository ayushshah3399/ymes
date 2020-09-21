Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.m_item0020")>
Partial Public Class m_item0020
	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(18)>
	Public Property item_code As String

	<Required>
	<StringLength(1)>
	Public Property trace_type As String

	<StringLength(1)>
	Public Property label_prt_type As String

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

	Public Property pack_in_qty As Decimal?
End Class
