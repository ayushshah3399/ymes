Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.S0010")>
Partial Public Class S0010
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property APPID As Short

    <Required>
    <StringLength(50)>
    Public Property APPNM As String

    Public Property SHHYOJDAYCNT As Short

    <StringLength(100)>
    Public Property KOKYUTENKAIPATH As String

    <StringLength(30)>
    Public Property CCADDRESS As String

	<StringLength(64)>
	Public Property INSTID As String

    <Column(TypeName:="datetime2")>
    Public Property INSTDT As Date

	<StringLength(50)>
	Public Property INSTTERM As String

	<StringLength(50)>
	Public Property INSTPRGNM As String

	<StringLength(64)>
	Public Property UPDTID As String

    <Column(TypeName:="datetime2")>
    Public Property UPDTDT As Date

	<StringLength(50)>
	Public Property UPDTTERM As String

	<StringLength(50)>
	Public Property UPDTPRGNM As String

	<Column(TypeName:="datetime")>
	Public Property ABWEEKSTARTDT As Date


End Class
