Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_work0020")>
Partial Public Class d_work0020
    <Key>
    <Column(Order:=0)>
    <StringLength(40)>
    Public Property instterm_ip As String

    <Key>
    <Column(Order:=1)>
    <StringLength(16)>
    Public Property inspect_label_no As String

    <Key>
    <Column(Order:=2)>
    <StringLength(18)>
    Public Property item_code As String

    <Key>
    <Column(Order:=3)>
    <StringLength(40)>
    Public Property item_name As String

    <Key>
    <Column(Order:=4)>
    Public Property label_qty As Decimal

    <Key>
    <Column(Order:=5)>
    <StringLength(3)>
    Public Property unit_code As String

    <Key>
    <Column(Order:=6)>
    <StringLength(18)>
    Public Property both_po_no As String

    <Key>
    <Column(Order:=7, TypeName:="date")>
    Public Property receive_date As Date

    <Key>
    <Column(Order:=8)>
    <StringLength(40)>
    Public Property plant_code As String

    <Key>
    <Column(Order:=9)>
    Public Property seq As Decimal = 1
End Class
