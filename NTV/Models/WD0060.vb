Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.WD0060")>
Partial Public Class WD0060
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

    <StringLength(4)>
    Public Property JKNED As String

    Public Property KYUKCD As Short

    Public Overridable Property M0060 As M0060

    Public Overridable Property M0010 As M0010



End Class
