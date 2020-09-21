Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0110")>
Partial Public Class d_mes0110
    '<Required>
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <StringLength(15)>
    Public Property work_order_no As String

    <StringLength(12)>
    Public Property order_no As String

    <StringLength(4)>
    Public Property order_type As String

    <StringLength(5)>
    Public Property workctr_code As String

    <StringLength(4)>
    Public Property location_code As String

    <StringLength(18)>
    Public Property item_code As String

    Public Property std_start_date As Date

    Public Property std_end_date As Date

    <Range(0, 9999999999.999, ErrorMessage:="please Enter Less")>
    Public Property plan_qty As Decimal

    '<Required>
    <StringLength(3)>
    Public Property unit_code As String


    <StringLength(1)>
    Public Property act_result_status As String


    <StringLength(15)>
    Public Property ref_work_order_no As String


    <StringLength(1)>
    Public Property work_type As String


    <StringLength(1)>
    Public Property print_status As String


    Public Property print_datetime As Date?

    <StringLength(1)>
    Public Property work_order_status As String

    <StringLength(20)>
    Public Property from_serial_no As String

    <StringLength(20)>
    Public Property to_serial_no As String

    <Range(0, 9999999999.999, ErrorMessage:="please Enter Less")>
    Public Property current_qty As Decimal

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
