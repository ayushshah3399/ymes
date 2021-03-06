Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0220")>
Partial Public Class s0220
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    <StringLength(12)>
    Public Property logno As String

    <Required>
    <StringLength(30)>
    Public Property prgid As String

    <StringLength(50)>
    Public Property menunm As String

    <Required>
    <StringLength(255)>
    Public Property filenm As String

    <Required>
    <StringLength(30)>
    Public Property tablenm As String

    Public Property startdt As Date

    Public Property enddt As Date?

    <Required>
    <StringLength(1)>
    Public Property state As String

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
