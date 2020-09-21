Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0040")>
Partial Public Class D0040
    <Key>
    <Column(Order:=0)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property USERID As Short

    <Key>
    <Column(Order:=1)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property NENGETU As Integer

    <Key>
    <Column(Order:=2)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property HI As Short

    <Key>
    <Column(Order:=3)>
    <StringLength(4)>
    Public Property JKNST As String


    <Column(Order:=4)>
    <StringLength(4)>
    Public Property JKNED As String

    Public Property JTJKNST As Date

    Public Property JTJKNED As Date

    Public Property KYUKCD As Short

    <StringLength(30)>
    Public Property GYOMMEMO As String

    <StringLength(30)>
    <Display(Name:="”õl")>
    Public Property BIKO As String

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

    Public Overridable Property D0030 As D0030

    Public Overridable Property M0060 As M0060

    Public Overridable Property M0010 As M0010

	Public Overridable Property WD0040 As WD0040

	Public Property KANRIMEMO As String
End Class
