Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes1130")>
Partial Public Class d_mes1130
	'<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<Column(Order:=0, TypeName:="date")>
	<Display(Name:="B1010_02_Stocktake_date", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property stocktake_date As Date

	<Key>
	<Column(Order:=1)>
	<StringLength(4)>
	<Display(Name:="B1010_10_LocationCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property location_code As String

	<Required>
	<StringLength(1)>
	Public Property st_input_type As String

	'<Required>
	<StringLength(64)>
	Public Property instid As String

	Public Property instdt As Date

	'<Required>
	<StringLength(50)>
	Public Property instterm As String

	'<Required>
	<StringLength(50)>
	Public Property instprgnm As String

	'<Required>
	<StringLength(64)>
	Public Property updtid As String

	Public Property updtdt As Date

	'<Required>
	<StringLength(50)>
	Public Property updtterm As String

	'<Required>
	<StringLength(50)>
	Public Property updtprgnm As String
End Class
