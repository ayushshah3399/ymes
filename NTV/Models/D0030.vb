Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0030")>
Partial Public Class D0030
    Public Sub New()
        D0040 = New HashSet(Of D0040)()
    End Sub

    <Key>
    <Column(Order:=0)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property USERID As Short

    <Key>
    <Column(Order:=1)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property NENGETU As Integer

    <Required>
    <StringLength(64)>
    Public Property INSTID As String

    <Column(TypeName:="datetime2")>
    Public Property INSTDT As Date

    <Required>
    <StringLength(50)>
    Public Property INSTTERM As String

    <Required>
    <StringLength(50)>
    Public Property INSTPRGNM As String

    <Required>
    <StringLength(64)>
    Public Property UPDTID As String

    <Column(TypeName:="datetime2")>
    Public Property UPDTDT As Date

    <Required>
    <StringLength(50)>
    Public Property UPDTTERM As String

    <Required>
    <StringLength(50)>
    Public Property UPDTPRGNM As String

    Public Overridable Property M0010 As M0010

    Public Overridable Property D0040 As ICollection(Of D0040)
End Class
