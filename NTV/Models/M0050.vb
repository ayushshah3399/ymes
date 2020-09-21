Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0050")>
Partial Public Class M0050
    Public Sub New()
        M0010 = New HashSet(Of M0010)()
    End Sub

	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	<Display(Name:="レベルコード")>
	Public Property ACCESSLVLCD As Short

	<Required>
	<StringLength(30)>
	<Display(Name:="レベル")>
	Public Property HYOJNM As String

    Public Property HYOJJN As Short

    Public Property SYSTEM As Boolean

    Public Property KANRI As Boolean

    Public Property ANA As Boolean

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

    Public Overridable Property M0010 As ICollection(Of M0010)
End Class
