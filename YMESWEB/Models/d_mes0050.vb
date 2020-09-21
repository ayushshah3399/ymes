Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0050")>
Partial Public Class d_mes0050
    <Required>
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <StringLength(16)>
    Public Property inspect_label_no As String

    <Required>
    <StringLength(15)>
    Public Property slip_no As String

	<Required>
	<StringLength(18)>
	<Display(Name:="A1010_04_ItemCode", ResourceType:=GetType(My.Resources.LangResources))>
	Public Property item_code As String

    Public Property inspect_qty As Decimal

    Public Property prev_inspect_qty As Decimal

    <Required>
    <StringLength(3)>
    Public Property unit_code As String

    <StringLength(16)>
    Public Property src_inspect_label_no As String

    Public Property print_datetime As Date

    <StringLength(20)>
    Public Property work_user As String

    <Column(TypeName:="date")>
    Public Property post_date As Date?

    <Required>
    <StringLength(1)>
    Public Property inspected_flg As String

    <Required>
    <StringLength(1)>
    Public Property delete_flg As String

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
