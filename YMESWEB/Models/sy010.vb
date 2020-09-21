Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.sy010")>
Partial Public Class sy010
    <Key>
    <Column(Order:=0)>
    <StringLength(15)>
    Public Property ipaddress As String

    <Key>
    <Column(Order:=1)>
    Public Property logintime As Date

    Public Property logouttime As Date?

    <Required>
    <StringLength(50)>
    Public Property sessionid As String

    <Required>
    <StringLength(20)>
    Public Property userid As String

    <Required>
    <StringLength(32)>
    Public Property usernm As String

    <Required>
    <StringLength(6)>
    Public Property lcid As String

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
