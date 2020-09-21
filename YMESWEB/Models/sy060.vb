Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.sy060")>
Partial Public Class sy060
	<Key>
	<Column(Order:=0)>
	<StringLength(15)>
	Public Property ipaddress As String

	<Column(Order:=1)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property printer_id As Integer

	<NotMapped>
	Public Overridable Property obj_model_PrintSetting As ICollection(Of PrintSetting)

	<NotMapped>
	Public Property HiddenlblForPrinterid As String

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
End Class
