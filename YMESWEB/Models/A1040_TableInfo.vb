Imports System.ComponentModel.DataAnnotations

Public Class A1040_TableInfo
	Public Property ItemCodeFlag As String
	Public Property TableLabelInfo As String

	<DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
	Public Property TableQty As String

End Class
