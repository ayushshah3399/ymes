Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes1020")>
Partial Public Class d_mes1020
	'<Required>
	<StringLength(4)>
    Public Property plant_code As String

	<Key>
	<Column(Order:=0, TypeName:="date")>
	<Display(Name:="B1010_02_Stocktake_date", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property stocktake_date As Date

	<Key>
	<Column(Order:=1)>
	<StringLength(16)>
	<Display(Name:="B1010_05_labelNo", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property label_no As String

	'<Required>
	<StringLength(18)>
	<Display(Name:="A1010_04_ItemCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property item_code As String

	'<Required>
	<StringLength(4)>
	<Display(Name:="B1010_10_LocationCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property location_code As String

	<StringLength(10)>
	<Display(Name:="B1010_11_ShelfNo", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property shelf_no As String

	<NotMapped>
	<StringLength(10)>
	<Display(Name:="B1010_09_LabelStatus", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Label_Status As String

	<Display(Name:="B1010_08_StockQty", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property stock_qty As Decimal

	<NotMapped>
	<Display(Name:="B1010_08_StockQty", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Str_stock_qty As String

	Public Property inspect_qty As Decimal

    Public Property keep_qty As Decimal

    <StringLength(4)>
    Public Property input_location_code As String

    <StringLength(10)>
    Public Property input_shelf_no As String

    Public Property input_stock_qty As Decimal?

    Public Property input_inspect_qty As Decimal?

    Public Property input_keep_qty As Decimal?

	'<Required>
	<StringLength(3)>
    Public Property unit_code As String

	'<Required>
	<StringLength(1)>
	Public Property input_flg As String

	'<Required>
	<StringLength(1)>
	Public Property stockstts_type As String

	<StringLength(1)>
	Public Property inputdata_type As String

	Public Property input_cnt As Decimal?

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
