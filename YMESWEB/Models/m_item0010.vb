Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.m_item0010")>
Partial Public Class m_item0010
	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(18)>
	Public Property item_code As String

	<Required>
	<StringLength(4)>
	Public Property lang_key As String

	<Required>
	<StringLength(40)>
	Public Property item_name As String

	<Required>
	<StringLength(2)>
	Public Property special_prc_type As String

	<Required>
	<StringLength(4)>
	Public Property eval_class_code As String

    <StringLength(4)>
    Public Property proc_def_loc As String

    <StringLength(4)>
    Public Property inspect_flag As String

    Public Property standard_cost As Decimal?

    Public Property price_unit As Decimal?

    '<StringLength(4)>
    'Public Property loc_code As String

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
