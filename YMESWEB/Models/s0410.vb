Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0410")>
Partial Public Class s0410
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    Public Property sessionid As Decimal

    <Required>
    <StringLength(30)>
    Public Property userid As String

    <Required>
    <StringLength(48)>
    Public Property machine As String

    <StringLength(30)>
    Public Property osuser As String

    <StringLength(30)>
    Public Property terminal As String

    <StringLength(48)>
    Public Property program As String

    Public Property logintime As Date?

    Public Property logouttime As Date?
End Class
