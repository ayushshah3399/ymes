Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0240")>
Partial Public Class s0240
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    <StringLength(12)>
    Public Property logno As String

    <Key>
    <Column(Order:=2)>
    Public Property rowno As Decimal

    <Required>
    <StringLength(1)>
    Public Property errkbn As String

    <Key>
    <Column(Order:=3)>
    Public Property errid As Decimal

    <StringLength(100)>
    Public Property coltitle As String

    <Key>
    <Column(Order:=4)>
    <StringLength(100)>
    Public Property columnm As String

    <StringLength(100)>
    Public Property columcom As String

    <StringLength(100)>
    Public Property errmsg As String

    <StringLength(300)>
    Public Property value As String

    <Required>
    <StringLength(64)>
    Public Property instid As String

    Public Property instdt As Date

    <Required>
    <StringLength(64)>
    Public Property updtid As String

    Public Property updtdt As Date

    <Required>
    <StringLength(50)>
    Public Property instterm As String

    <Required>
    <StringLength(50)>
    Public Property instprgnm As String

    <Required>
    <StringLength(50)>
    Public Property updtterm As String

    <Required>
    <StringLength(50)>
    Public Property updtprgnm As String
End Class
