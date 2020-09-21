Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0080")>
Partial Public Class s0080
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    <StringLength(20)>
    Public Property userid As String

    <Key>
    <Column(Order:=2)>
    <StringLength(50)>
    Public Property menunm As String

    <Required>
    <StringLength(1)>
    Public Property usekbn As String

    <Required>
    <StringLength(1)>
    Public Property editkbn1 As String

    <Required>
    <StringLength(1)>
    Public Property editkbn2 As String

    <Required>
    <StringLength(1)>
    Public Property editkbn3 As String

    <Required>
    <StringLength(1)>
    Public Property editkbn4 As String

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
