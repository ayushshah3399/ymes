Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0060")>
Partial Public Class d_mes0060
	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(15)>
	Public Property picking_no As String

	<Required>
	<StringLength(10)>
	Public Property in_loc_code As String

	<Required>
	<StringLength(4)>
	Public Property issue_loc_code As String

	<StringLength(5)>
	Public Property workctr_code As String

	<StringLength(6)>
	Public Property shelfgrp_code As String

	<Column(TypeName:="date")>
	Public Property instruction_date As Date

	<Required>
	<StringLength(5)>
	Public Property instruction_time As String

	<Required>
	<StringLength(1)>
	Public Property pic_input_type As String

	<Required>
	<StringLength(1)>
	Public Property pic_type As String

	<Required>
	<StringLength(1)>
	Public Property pic_status As String

	Public Property print_datetime As Date?

	<StringLength(15)>
	Public Property ref_picking_no As String

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
