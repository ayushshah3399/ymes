Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0070")>
Partial Public Class W0070
    <Key>
    <Column(Order:=0)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property ACUSERID As Short

    <Key>
    <Column(Order:=1)>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property USERID As Short

    <Key>
    <Column(Order:=2, TypeName:="numeric")>
    Public Property GYOMNO As Decimal

    Public Property GYOMYMD As Date

    Public Property GYOMYMDED As Date?

    <Required>
    <StringLength(4)>
    Public Property KSKJKNST As String

    <Required>
    <StringLength(4)>
    Public Property KSKJKNED As String

    Public Property JTJKNST As Date

    Public Property JTJKNED As Date

    Public Property CATCD As Short

    <StringLength(40)>
    Public Property BANGUMINM As String

    <StringLength(4)>
    Public Property OAJKNST As String

    <StringLength(4)>
    Public Property OAJKNED As String

    <StringLength(40)>
    Public Property NAIYO As String

    <StringLength(40)>
    Public Property BASYO As String

    <StringLength(30)>
    Public Property BIKO As String

    <StringLength(30)>
    Public Property BANGUMITANTO As String

    <StringLength(30)>
    Public Property BANGUMIRENRK As String

    Public Property RNZK As Boolean

    <Column(TypeName:="numeric")>
    Public Property PGYOMNO As Decimal?

    Public Property IKTFLG As Boolean?

    Public Property IKTUSERID As Short?

	<Column(TypeName:="numeric")>
	Public Property IKKATUNO As Decimal?

	Public Overridable Property M0020 As M0020

End Class
