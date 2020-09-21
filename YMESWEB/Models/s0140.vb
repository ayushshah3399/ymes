Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0140")>
Partial Public Class s0140
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    <StringLength(6)>
    Public Property lcid As String

    <Required>
    <StringLength(20)>
    Public Property lcnm As String

    <Required>
    <StringLength(20)>
    Public Property datefmt As String

    <Required>
    <StringLength(1)>
    Public Property dateseparator As String

    Public Property showorder As Decimal?

    <StringLength(50)>
    Public Property memo As String

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
