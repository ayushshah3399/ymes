Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0210")>
Partial Public Class s0210
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    <StringLength(30)>
    Public Property schema As String

    <Key>
    <Column(Order:=2)>
    <StringLength(30)>
    Public Property tabnm As String

    <Key>
    <Column(Order:=3)>
    <StringLength(30)>
    Public Property colnm As String

    <Key>
    <Column(Order:=4)>
    <StringLength(30)>
    Public Property prgid As String

    <StringLength(30)>
    Public Property colfmt As String

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
