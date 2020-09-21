Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0010")>
Partial Public Class W0010
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

	<StringLength(8)>
	Public Property YOINYMD As String

	<Column(TypeName:="numeric")>
	Public Property YOINGYOMNO As Decimal?

	Public Property MARKKYST As Boolean?

	Public Property MARKSYTK As Boolean?

	Public Property DESKMEMO As Boolean?

	Public Overridable Property M0010 As M0010

	Public Overridable Property M00101 As M0010

	Public Overridable Property W0020 As ICollection(Of W0020)

	Public Overridable Property W0030 As ICollection(Of W0030)

	'ASI [15 Oct 2019]
	Public Property MARKHOUKYST As Boolean?

	'ASI [23 Oct 2019]
	Public Property MARKJYUYO As Boolean?

	'ASI[05 Dec 2019]
	Public Property SPORTSHIFT As Boolean?

	'ASI[26 Dec 2019]
	Public Property CHK As Boolean

End Class
