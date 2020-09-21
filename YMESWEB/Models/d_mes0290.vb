Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0290")>
Partial Public Class d_mes0290
    '<Required>
    <StringLength(4)>
    Public Property plant_code As String

    <Key, Column(Order:=0)>
    <StringLength(20)>
    Public Property serial_no As String

    <Key, Column(Order:=1)>
    <StringLength(18)>
    Public Property item_code As String

    <StringLength(1)>
    Public Property shipped_flg As String

    <StringLength(1)>
    Public Property stockstts_type As String

    <Range(0, 9999999999.999, ErrorMessage:="please Enter Lesss")>
    Public Property qty As String

    <StringLength(4)>
    Public Property location_code As String

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
