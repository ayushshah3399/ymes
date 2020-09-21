Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0050")>
Partial Public Class W0050
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

	<Key>
	<Column(Order:=3)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property USERID As Short

    Public Property CHK As Boolean

    <StringLength(30)>
    Public Property SHIFTMEMO As String

	Public Property SOUSIN As Boolean

	Public Property DELFLG As Boolean

	<NotMapped>
	Public Property MAILSOUSIAN As Boolean

	<NotMapped>
	Public Property SENDKEITAI As Boolean

	Public Overridable Property M0010 As M0010

	Public Overridable Property M00101 As M0010

	Public Overridable Property W0040 As W0040

End Class
