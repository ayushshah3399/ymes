Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_work0030")>
Partial Public Class d_work0030
    <Key>
    <Column(Order:=0)>
    <StringLength(40)>
    Public Property plant_code As String

    <Key>
    <Column(Order:=1)>
    <StringLength(40)>
    Public Property instterm_ip As String

    <Key>
    <Column(Order:=2)>
    <StringLength(16)>
    Public Property label_no As String

    <Key>
    <Column(Order:=3)>
    <StringLength(18)>
    Public Property item_code As String

    <Key>
    <Column(Order:=4)>
    Public Property qty As Decimal

    <Key>
    <Column(Order:=5)>
    <StringLength(3)>
    Public Property unit_code As String

    <Key>
    <Column(Order:=6, TypeName:="date")>
    Public Property stocktake_date As Date

    <Key>
    <Column(Order:=7)>
    <StringLength(1)>
    Public Property label_type As String

    <Key>
    <Column(Order:=8)>
    Public Property seq As Decimal = 1
End Class
