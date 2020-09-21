Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_sap0050")>
Partial Public Class d_sap0050
    <Required>
    <StringLength(4)>
    Public Property plant_code As String

	<Key>
    <Column(Order:=0)>
    <StringLength(12)>
    Public Property po_no As String

    <Key>
    <Column(Order:=1)>
    <StringLength(5)>
    Public Property po_sub_no As String

	'<Column(TypeName:="date")>
	'Public Property slip_date As Date?

	<StringLength(3)>
    Public Property order_type As String

	<Required>
	<StringLength(18)>
	<Display(Name:="A1010_04_ItemCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property item_code As String

	<Required>
	<StringLength(4)>
	Public Property in_loc_code As String

    Public Property odr_qty As Decimal

    <Required>
    <StringLength(3)>
    Public Property unit_code As String

    Public Property stock_odr_qty As Decimal

    <Required>
    <StringLength(3)>
    Public Property stock_unit_code As String

	'<Required>
	<StringLength(10)>
	Public Property supplier_code As String

    'Public Property net_price As Decimal?

    'Public Property price_unit As Decimal?

    '<StringLength(1)>
    'Public Property ekko_delete_flag As String

    '<StringLength(1)>
    'Public Property ekko_detail_delete_flag As String

    '<StringLength(1)>
    'Public Property comp_type As String

    '<Column(TypeName:="date")>
    'Public Property delivery_date As Date

    <StringLength(1)>
    Public Property un_over_deliv_type As String

    Public Property max_delivery As Decimal?

    'Public Property min_delivery As Decimal?

    '<StringLength(6)>
    'Public Property arrange_no As String

    '<StringLength(2)>
    'Public Property unload_loc_code As String

    '<StringLength(1)>
    'Public Property drct_idrct_type As String

    '<StringLength(10)>
    'Public Property account_code As String

    '<StringLength(1)>
    'Public Property import_type As String

    '<StringLength(1)>
    'Public Property short_deli_type As String

    <StringLength(1)>
    Public Property del_flag As String

	'<StringLength(30)>
	'Public Property po_text As String

	'<StringLength(10)>
	'Public Property memo As String

	<StringLength(1)>
	Public Property stock_type As String

	<StringLength(1)>
	Public Property acc_set_cat As String

    <StringLength(40)>
    Public Property item_name As String

    <StringLength(1)>
    Public Property appd_flag As String

    <StringLength(2)>
    Public Property appd_policy As String

    Public Property billed_ord_qty As Decimal?

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

	Public Overridable Property m_item0010 As m_item0010

End Class
