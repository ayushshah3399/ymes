Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0070")>
Partial Public Class d_mes0070
	<Key>
	<Column(Order:=0)>
	<StringLength(12)>
	Public Property picking_no As String

	<Key>
	<Column(Order:=1)>
	<StringLength(18)>
	Public Property cld_item_code As String

	'<StringLength(10)>
	'Public Property shelf_no As String

	Public Property qty As Decimal

	<StringLength(3)>
	Public Property unit_code As String

	<Required>
	<StringLength(1)>
	Public Property pic_with_status As String

	Public Property payout_qty As Decimal?

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
