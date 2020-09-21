Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0110")>
Partial Public Class M0110
    <Key>
    <Column(Order:=0, TypeName:="numeric")>
    Public Property IKKATUNO As Decimal

    <Key>
    <Column(Order:=1)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property USERID As Short

    Public Property CHK As Boolean

    <StringLength(30)>
    Public Property SHIFTMEMO As String


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

    Public Overridable Property M0090 As M0090

    Public Overridable Property M0010 As M0010

End Class
