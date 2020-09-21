Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0190")>
Partial Public Class d_mes0190
    '<Required>
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <StringLength(15)>
    Public Property work_result_no As String

    <StringLength(20)>
    Public Property serial_no As String

    <Range(0, 9999999999.999, ErrorMessage:="please Enter Lesss")>
    Public Property qty As Decimal

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
