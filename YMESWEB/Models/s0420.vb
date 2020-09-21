Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0420")>
Partial Public Class s0420
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    Public Property sessionid As Decimal

    <Key>
    <Column(Order:=2)>
    <StringLength(20)>
    Public Property userid As String

    <Key>
    <Column(Order:=3)>
    <StringLength(48)>
    Public Property machine As String

    <Key>
    <Column(Order:=4)>
    <StringLength(100)>
    Public Property windowid As String

    <Key>
    <Column(Order:=5)>
    Public Property windowseq As Decimal

    <StringLength(80)>
    Public Property menunm As String

    <StringLength(12)>
    Public Property version As String

    <Required>
    <StringLength(1)>
    Public Property state As String

    Public Property opentime As Date?

    Public Property closetime As Date?
End Class
