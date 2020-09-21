Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0120")>
Partial Public Class d_mes0120
    '<Required>
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <StringLength(15)>
    Public Property work_result_no As String

    <StringLength(15)>
    Public Property work_order_no As String

    <Range(0, 9999999999, ErrorMessage:="please Enter Lesss")>
    Public Property work_order_seq As Decimal

    <StringLength(18)>
    Public Property item_code As String

    <StringLength(10)>
    Public Property man_stat_cd As String

    Public Property result_date As Date

    <Range(0, 9999999999.999, ErrorMessage:="please Enter Lesss")>
    Public Property qty As Decimal

    '<Required>
    <StringLength(3)>
    Public Property unit_code As String

    <Range(0, 9999999999, ErrorMessage:="please Enter Lesss")>
    Public Property label_no_of_print As String

    <Range(0, 9999999999.999, ErrorMessage:="please Enter Lesss")>
    Public Property defect_qty As String

    <StringLength(1)>
    Public Property input_type As String

    <StringLength(1)>
    Public Property act_result_type As String

    <StringLength(5)>
    Public Property dest_location_code As String

    <StringLength(1)>
    Public Property stock_update_type As String

    <StringLength(5)>
    Public Property reason_code As String

    <StringLength(3)>
    Public Property sap_move_type As String

    <StringLength(8)>
    Public Property m_stat_worktime As String

    <StringLength(8)>
    Public Property total_worktime As String

    <StringLength(1)>
    Public Property sap_if_status As String

    <StringLength(12)>
    Public Property prod_individual_id As String

    <StringLength(1)>
    Public Property stock_updt_type As String

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
