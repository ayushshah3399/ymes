Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0100")>
Partial Public Class d_mes0100
	'<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(16)>
	Public Property payout_no As String

	<StringLength(15)>
	<Display(Name:="A1030_03_Picking_NO", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property picking_no As String

	<StringLength(16)>
	<Display(Name:="A1020_08_LabelNo", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property label_no As String

	'<Required>
	<StringLength(1)>
	Public Property label_type As String

	'<Required>
	<StringLength(18)>
	<Display(Name:="A1010_04_ItemCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property item_code As String

	<Display(Name:="A1030_12_qty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
    <Range(0, 9999999999999.998, ErrorMessage:="please Enter Lesss")>
    Public Property qty As Decimal

	<NotMapped>
	<Display(Name:="A1030_12_qty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
    <Range(0, 9999999999999.998, ErrorMessage:="please Enter Lesss")>
    Public Property str_qty As String

	'<Required>
	<StringLength(3)>
	Public Property unit_code As String

	<StringLength(20)>
	Public Property work_user As String

	'<Required>
	<StringLength(1)>
	Public Property del_flag As String

	<NotMapped>
	<StringLength(6)>
	<Display(Name:="A1020_12_Shelfgroup", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property shelfgrp_code As String

	<NotMapped>
	<StringLength(10)>
	<Display(Name:="A1030_11_LocationCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property in_loc_code As String

	<NotMapped>
	<StringLength(4)>
	<Display(Name:="A1030_10_IssueLocationCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property loc_code As String

	<NotMapped>
	<StringLength(15)>
	Public Property TxtBox_picking_no As String

	<NotMapped>
	<StringLength(16)>
	<Display(Name:="A1020_08_LabelNo", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property TextBox_lable_no As String

	<NotMapped>
	<Display(Name:="A1010_05_ItemName", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property A1030_Itemname As String

	<NotMapped>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property A1030_Hidden_Qty As String

	<NotMapped>
	Public Overridable Property obj_A1040_TableInfo As ICollection(Of A1040_TableInfo)

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
