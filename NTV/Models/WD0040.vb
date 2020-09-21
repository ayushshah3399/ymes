Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.WD0040")>
Partial Public Class WD0040
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
    Public Property KYUKCD As Short

    <StringLength(10)>
    Public Property HI1 As String

    <StringLength(10)>
    Public Property HI2 As String

    <StringLength(10)>
    Public Property HI3 As String

    <StringLength(10)>
    Public Property HI4 As String

    <StringLength(10)>
    Public Property HI5 As String

    <StringLength(10)>
    Public Property HI6 As String

    <StringLength(10)>
    Public Property HI7 As String

    <StringLength(10)>
    Public Property HI8 As String

    <StringLength(10)>
    Public Property HI9 As String

    <StringLength(10)>
    Public Property HI10 As String

    <StringLength(10)>
    Public Property HI11 As String

    <StringLength(10)>
    Public Property HI12 As String

    <StringLength(10)>
    Public Property HI13 As String

    <StringLength(10)>
    Public Property HI14 As String

    <StringLength(10)>
    Public Property HI15 As String

    <StringLength(10)>
    Public Property HI16 As String

    <StringLength(10)>
    Public Property HI17 As String

    <StringLength(10)>
    Public Property HI18 As String

    <StringLength(10)>
    Public Property HI19 As String

    <StringLength(10)>
    Public Property HI20 As String

    <StringLength(10)>
    Public Property HI21 As String

    <StringLength(10)>
    Public Property HI22 As String

    <StringLength(10)>
    Public Property HI23 As String

    <StringLength(10)>
    Public Property HI24 As String

    <StringLength(10)>
    Public Property HI25 As String

    <StringLength(10)>
    Public Property HI26 As String

    <StringLength(10)>
    Public Property HI27 As String

    <StringLength(10)>
    Public Property HI28 As String

    <StringLength(10)>
    Public Property HI29 As String

    <StringLength(10)>
    Public Property HI30 As String

    <StringLength(10)>
    Public Property HI31 As String

    Public Overridable Property D0040 As ICollection(Of D0040)

End Class
