Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0060")>
Partial Public Class D0060
    <Key>
    <Column(TypeName:="numeric")>
    Public Property KYUKSNSCD As Decimal

    Public Property USERID As Short

	<Display(Name:="‹x‰É‚ÌŽí—Þ")>
	Public Property KYUKCD As Short

    <Display(Name:="ŠúŠÔ")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property KKNST As Date?

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property KKNED As Date?

    <StringLength(5)>
    <Display(Name:="ŽžŠÔ")>
    Public Property JKNST As String

    <StringLength(5)>
    Public Property JKNED As String

    <StringLength(30)>
    <Display(Name:="”õl")>
    <DataType(DataType.MultilineText)>
    Public Property GYOMMEMO As String

	<UIHint("SHONINFLG")>
	Public Property SHONINFLG As String

	<StringLength(60)>
	<Display(Name:="ŠÇ—ŽÒƒƒ‚")>
	<DataType(DataType.MultilineText)>
	Public Property KANRIMEMO As String


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

    <NotMapped>
   <Display(Name:="ƒpƒ^[ƒ“‚ÌÝ’è")>
    Public Property PATTERN As Boolean

    <NotMapped>
    <Display(Name:="ŒŽ—j")>
    Public Property MON As Boolean

    <NotMapped>
    <Display(Name:="‰Î—j")>
    Public Property TUE As Boolean

    <NotMapped>
    <Display(Name:="…—j")>
    Public Property WED As Boolean

    <NotMapped>
    <Display(Name:="–Ø—j")>
    Public Property TUR As Boolean

    <NotMapped>
    <Display(Name:="‹à—j")>
    Public Property FRI As Boolean

    <NotMapped>
    <Display(Name:="“y—j")>
    Public Property SAT As Boolean

    <NotMapped>
	<Display(Name:="“ú—j")>
	Public Property SUN As Boolean

	<NotMapped>
	Public Property DESKMEMO As Boolean
	<NotMapped>
	Public Property GYOMSTDT As String
	<NotMapped>
	Public Property GYOMEDDT As String
	Public Overridable Property M0010 As M0010

    Public Overridable Property M0060 As M0060
End Class
