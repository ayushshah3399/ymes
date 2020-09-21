Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports MES_WEB.My.Resources

<Table("telas.d_mes0250")>
Partial Public Class d_mes0250
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <StringLength(16)>
    Public Property inspect_label_no As String


    <StringLength(15)>
    Public Property work_order_no As String

    <Required>
    <StringLength(15)>
    Public Property work_result_no As String

    <Required>
    <StringLength(18)>
    Public Property item_code As String

    <DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
    Public Property inspect_qty As Decimal


    <DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
    Public Property prev_inspect_qty As Decimal


    <DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
    Public Property ok_qty As Decimal


    <DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
    Public Property ng_qty As Decimal

    <StringLength(4)>
    Public Property ng_loc_code As String

    <Required>
    <StringLength(3)>
    Public Property unit_code As String

    <StringLength(16)>
    Public Property src_inspect_label_no As String

    Public Property print_datetime As Date


    <StringLength(20)>
    Public Property work_user As String


    Public Property post_date As Date

    <StringLength(1)>
    Public Property inspected_flg As String

    <StringLength(1)>
    Public Property delete_flag As String

    <StringLength(64)>
    Public Property instid As String

    Public Property instdt As Date

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
