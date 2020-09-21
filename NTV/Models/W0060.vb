Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("TeLAS.W0060")>
Public Class W0060

	<Key>
	<Column(Order:=0)>
	Public Property NENGETU As Integer

	<Key>
	<Column(Order:=1)>
	Public Property HI As Short

	<Display(Name:="公休フラグ")>
	Public Property KOUKYU As Boolean


End Class
