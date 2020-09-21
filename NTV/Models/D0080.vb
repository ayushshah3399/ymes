Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0080")>
Partial Public Class D0080
    <Key>
    <Column(TypeName:="numeric")>
    Public Property DNGNNO As Decimal

    Public Property USERID As Short

    <Required>
    <ByteLength(256)>
    Public Property MESSAGE As String

    Public Property DATAFLG As Byte

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
     Public Property TOROKUYMD As Date


    <StringLength(64)>
    Public Property INSTID As String

    <Column(TypeName:="datetime2")>
    Public Property INSTDT As Date


    <StringLength(50)>
    Public Property INSTTERM As String


    <StringLength(50)>
    Public Property INSTPRGNM As String


    <StringLength(64)>
    Public Property UPDTID As String

    <Column(TypeName:="datetime2")>
    Public Property UPDTDT As Date


    <StringLength(50)>
    Public Property UPDTTERM As String


    <StringLength(50)>
    Public Property UPDTPRGNM As String

    Public Overridable Property M0010 As M0010
End Class
