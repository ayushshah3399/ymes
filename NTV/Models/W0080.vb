Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0080")>
Partial Public Class W0080
    <Key>
    <Column(Order:=0)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property ACUSERID As Short

    <Key>
    <Column(Order:=1)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property USERID As Short

    <Key>
    <Column(Order:=2)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property NENGETU As Integer

    <Key>
    <Column(Order:=3)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property HI As Short

    <Key>
    <Column(Order:=4)>
    <StringLength(4)>
    Public Property JKNST As String

    <Required>
    <StringLength(4)>
    Public Property JKNED As String

    Public Property KYUKCD As Short

    <Column(TypeName:="date")>
    Public Property KYUKYMD As Date

    Public Property KYUKDAY As Short

	Public Property KYUK24KOE As Boolean

	Public Overridable Property M0010 As M0010

	Public Overridable Property M0060 As M0060

End Class
