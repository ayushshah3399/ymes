Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0050")>
Partial Public Class s0050
    Public Sub New()
        s0060 = New HashSet(Of s0060)()
    End Sub

    <Key>
    <StringLength(20)>
    Public Property userid As String

    <Required>
    <StringLength(32)>
    Public Property usernm As String

    <StringLength(96)>
    Public Property pass As String

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

    Public Property pswupdtdt As Date?

    <StringLength(1)>
    Public Property loginhist As String

    Public Overridable Property s0060 As ICollection(Of s0060)
End Class
