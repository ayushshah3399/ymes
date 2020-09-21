Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0030")>
Partial Public Class W0030
	<Key>
	<Column(Order:=0)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property ACUSERID As Short

	<Key>
	<Column(Order:=1)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property YOINID As Short

	<Key>
	<Column(Order:=2)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property USERID As Short

	<Key>
	<Column(Order:=3, TypeName:="numeric")>
	Public Property GYOMNO As Decimal

	<Display(Name:="�Ɩ�����")>
	<Column(TypeName:="date")>
	 <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
	Public Property GYOMYMD As Date

	<Column(TypeName:="date")>
	 <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
	Public Property GYOMYMDED As Date?

	<Display(Name:="�S������")>
	<Required>
	<StringLength(4)>
	Public Property KSKJKNST As String

	<Required>
	<StringLength(4)>
	Public Property KSKJKNED As String

	Public Property JTJKNST As Date

	Public Property JTJKNED As Date

	<Display(Name:="�J�e�S���[")>
	Public Property CATCD As Short

	<Display(Name:="�ԑg��")>
	<StringLength(40)>
	Public Property BANGUMINM As String

	<Display(Name:="OA����")>
	<StringLength(4)>
	Public Property OAJKNST As String

	<StringLength(4)>
	Public Property OAJKNED As String

	<Display(Name:="���e")>
	<StringLength(40)>
	Public Property NAIYO As String

	<Display(Name:="�ꏊ")>
	<StringLength(40)>
	Public Property BASYO As String

	<Display(Name:="���l")>
	<StringLength(30)>
	Public Property BIKO As String

	<Display(Name:="�ԑg�S����")>
	<StringLength(30)>
	Public Property BANGUMITANTO As String

	<Display(Name:="�A����")>
	<StringLength(30)>
	Public Property BANGUMIRENRK As String

	Public Property RNZK As Boolean

	<Column(TypeName:="numeric")>
	Public Property PGYOMNO As Decimal?

	Public Property IKTFLG As Boolean?

	Public Property IKTUSERID As Short?

	<Column(TypeName:="numeric")>
	Public Property IKKATUNO As Decimal?

	Public Overridable Property W0010 As W0010

	Public Overridable Property M0020 As M0020

End Class
