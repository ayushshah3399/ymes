Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.m_proc0030")>
Partial Public Class m_proc0030
	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(4)>
    Public Property location_code As String

    <Required>
    <StringLength(1)>
    Public Property location_type As String
End Class
