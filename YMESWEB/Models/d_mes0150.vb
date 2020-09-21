Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports MES_WEB.My.Resources

<Table("telas.d_mes0150")>
Partial Public Class d_mes0150
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(15)>
	Public Property slip_no As String

	<Required>
	<StringLength(12)>
	<Display(Name:="A1010_02_Pono", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property po_no As String

	<Required>
	<StringLength(5)>
	<Display(Name:="A1010_03_Po_Sub_No", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property po_sub_no As String

	<Display(Name:="A1015_02_Po_Receive_Seq", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property po_receive_seq As Decimal

	<Required>
	<Column(TypeName:="date")>
	<Display(Name:="A1010_09_Receivedate", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property receive_date As Date?

	<Column(TypeName:="date")>
	Public Property post_date As Date

	'<Required>
	<StringLength(20)>
	Public Property work_user As String

	<Display(Name:="A1010_08_Receive_Qty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property receive_qty As Decimal

	'<Required>
	<NotMapped>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	<Display(Name:="A1010_08_Receive_Qty", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property str_receive_qty As String

	<NotMapped>
	<Display(Name:="A1010_08_Receive_Qty", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property hidden_receive_qty As String

	<Required>
	<StringLength(3)>
	Public Property unit_code As String

	Public Property stock_receive_qty As Decimal

	<NotMapped>
	<Display(Name:="A1010_18_Stock_Receive_Qty", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property str_QtyPerUnit As String

	'Store Ratio in View Side In A1010 
	<NotMapped>
	Public Property HiddenDec_RatioQty As String

	'<Required>
	<StringLength(3)>
	Public Property stock_unit_code As String

	'Extra Because Sometime Need to Display Nothing
	<NotMapped>
	<StringLength(3)>
	Public Property Fraction_stock_unit_code As String

	'<Required>
	<StringLength(1)>
	Public Property sap_flag As String

	Public Property sap_send_date As Date?

	'<Required>
	<StringLength(1)>
	Public Property delete_flg As String

	'<Required>
	<Column(TypeName:="date")>
	<Display(Name:="A1015_03_delete_result_date", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property delete_result_date As Date?

	<StringLength(25)>
	<Display(Name:="A1010_21_StaticHeaderText", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property header_text As String

	<NotMapped>
	<StringLength(18)>
	Public Property barcode As String

	'<Required>
	<NotMapped>
	<Display(Name:="A1010_06_QtyPerCarton", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property Inputqty As String

	<NotMapped>
	<Display(Name:="A1010_07_Sheet", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property DivideTerm As String

	<NotMapped>
	<Display(Name:="A1010_10_RemainingQty", ResourceType:=GetType(My.Resources.LangResources))>
	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property RemainingQty As String

	<NotMapped>
	<StringLength(18)>
	<Display(Name:="A1010_04_ItemCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property ItemCode As String

	<NotMapped>
	<Display(Name:="A1010_05_ItemName", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Itemname As String

	<NotMapped>
	<Display(Name:="A1010_11_Stock_type", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Stock_type As String

	<NotMapped>
	<Display(Name:="A1010_11_Stock_type", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property Show_Stock_type As String

	<NotMapped>
	Public Property PackingCount As String

	<NotMapped>
	Public Property HiddenInputqty As String

	'original picking Qty From Master Table
	<NotMapped>
	Public Property OrginalHiddenInputqtyfromDB As String

	<NotMapped>
	Public Property RecieveRecordCount As Integer

	<NotMapped>
	Public Property acc_set_cat As String

	<NotMapped>
	Public Property BolDirectGotoWO As String

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

	<NotMapped>
	Public Overridable Property d_sap0050 As d_sap0050

End Class
