Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0040")>
Partial Public Class d_mes0040
	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(16)>
	<Display(Name:="A1020_08_LabelNo", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property label_no As String

	<Required>
	<StringLength(15)>
	Public Property slip_no As String

	<Required>
	<StringLength(18)>
	<Display(Name:="A1010_04_ItemCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property item_code As String

	<Required>
	<StringLength(4)>
	<Display(Name:="A1060_04_LocationCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property location_code As String

	<StringLength(10)>
	<Display(Name:="A1020_11_Shelf", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property shelf_no As String

	<Display(Name:="A1060_05_StockQty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property stock_qty As Decimal

	'Used In A1060
	<NotMapped>
	<Display(Name:="A1060_05_StockQty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property str_stock_qty As String

	<Display(Name:="A1060_02_InspectionQty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property inspect_qty As Decimal

	'Used In A1060
	<NotMapped>
	<Display(Name:="A1060_02_InspectionQty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property str_inspect_qty As String

	<Display(Name:="A1060_03_KeepQty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property keep_qty As Decimal

	'Used In A1060
	<NotMapped>
	<Display(Name:="A1060_03_KeepQty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property str_keep_qty As String

	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property prev_inspect_qty As Decimal

	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property ok_qty As Decimal

	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property ng_qty As Decimal

	<Required>
	<StringLength(3)>
	Public Property unit_code As String

	Public Property print_datetime As Date

	<StringLength(16)>
	<Display(Name:="A1070_03_inspect_label_no", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property inspect_label_no As String

	<StringLength(16)>
	Public Property src_inspect_label_no As String

	<Required>
	<StringLength(1)>
	Public Property transfer_flg As String

	<Required>
	<StringLength(1)>
	Public Property delete_flg As String

	<NotMapped>
	Public Property shelfgrp_code As String

	<NotMapped>
	Public Property listlblno As String()

	<NotMapped>
	Public Property HiddenshelfNo As String

	<NotMapped>
	<Display(Name:="A1020_08_LabelNo", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property A1060_DisplayLbl_label_no As String

	<NotMapped>
	<Display(Name:="A1010_05_ItemName", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property A1060_Itemname As String

	<NotMapped>
	Public Property HiddenLabelfNo As String

	'<Required>
	'<DefaultSettingValue(DefaultSettingValueAttribute(True))>
	'<DefaultSettingValueAttribute("5")>
	<StringLength(1)>
	Public Property sttime_input_flg As String = "0"

	<NotMapped>
	<Display(Name:="A1070_02_Po_Sub_Po_NO", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Po_Sub_Po_NO As String

	<NotMapped>
	<Display(Name:="A1070_03_Supplier_Name", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Supplier_Name As String

	<NotMapped>
	<Column(TypeName:="date")>
	<Display(Name:="A1010_09_Receivedate", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Receive_Date As Date?

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

	<NotMapped>
	Public Overridable Property obj_A1020_Labelinfo As ICollection(Of A1020_Labelinfo)
End Class
