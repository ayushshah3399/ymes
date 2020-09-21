Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0040")>
Partial Public Class W0040
    Public Sub New()
		W0050 = New HashSet(Of W0050)()
	End Sub

	<Key>
	<Column(Order:=0)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property ACUSERID As Short

	<Key>
	<Column(Order:=1)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property SHORIKBN As Short

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

	Public Property PTNFLG As Boolean

	Public Property PTN1 As Boolean

	Public Property PTN2 As Boolean

	Public Property PTN3 As Boolean

	Public Property PTN4 As Boolean

	Public Property PTN5 As Boolean

	Public Property PTN6 As Boolean

	Public Property PTN7 As Boolean

	Public Property UPDTDT As Date

	<NotMapped>
	Public Property MAILSUBJECT As String

	<NotMapped>
	Public Property MAILBODYHEAD As String

	<NotMapped>
	Public Property MAILBODY As String

	<NotMapped>
	Public Property MAIAPPURL As String

	<NotMapped>
	<Display(Name:="コメント")>
	<StringLength(1000)>
	<DataType(DataType.MultilineText)>
	Public Property MAILNOTE As String

	<NotMapped>
	<DataType(DataType.Url)>
	<Display(name:="システムでシフト表を参照")>
	Public Property MAILAPPURL As String

	Public Overridable Property M0020 As M0020

	Public Overridable Property W0050 As ICollection(Of W0050)

	<NotMapped>
	Public Property ToMailList As List(Of Short)

	<NotMapped>
	Public Property CcMailList As List(Of Short)

End Class
