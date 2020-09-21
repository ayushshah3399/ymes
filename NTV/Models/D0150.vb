Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0150")>
Partial Public Class D0150
    <Key>
    <Column(TypeName:="numeric")>
    Public Property HENKORRKCD As Decimal

    <Required>
    <StringLength(4)>
    Public Property HENKONAIYO As String

    Public Property USERID As Short

    Public Property SYUSEIYMD As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
      Public Property KKNST As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
      Public Property KKNED As Date

    <Required>
    <StringLength(4)>
    Public Property JKNST As String

    <Required>
    <StringLength(4)>
    Public Property JKNED As String

    <Required>
    <StringLength(65)>
    Public Property SHINSEIUSER As String

    <Required>
    <Display(Name:="休暇名称")>
    <StringLength(12)>
    Public Property KYUKNM As String

    <StringLength(60)>
    <Display(Name:="備考")>
    Public Property GYOMMEMO As String


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
