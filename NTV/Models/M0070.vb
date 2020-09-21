Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0070")>
Partial Public Class M0070
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property MAILSEQ As Short

    Public Property KIKAN As Boolean

    Public Property SOUSIN As Boolean

    Public Property KAKUNIN As Boolean

    Public Property MAILSOUSIAN As Boolean

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
End Class
