Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.W0020")>
Partial Public Class W0020
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
	<Column(Order:=3)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property NENGETU As Integer

	<Key>
	<Column(Order:=4)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property HI As Short

	<Key>
	<Column(Order:=5)>
	<StringLength(4)>
	 Public Property JKNST As String

	<StringLength(4)>
	Public Property JKNED As String

	<DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
 Public Property KYUKYMD As Date

    Public Property KYUKDAY As Short

    Public Property KYUKCD As Short?

	Public Overridable Property W0010 As W0010

End Class
