Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports System.Data.SqlClient

<Table("TeLAS.D0020")>
Partial Public Class D0020

	<Key>
	<Column(Order:=0, TypeName:="numeric")>
	Public Property GYOMNO As Decimal

	<Key>
	<Column(Order:=1)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property USERID As Short

	Public Property CHK As Boolean

	<StringLength(30)>
	Public Property SHIFTMEMO As String

	Public Property SOUSIN As Boolean

	'ASI[12 Dec 2010] : Added as decided in new Update flow
	<StringLength(20)>
	Public Property COLNM As String

	<NotMapped>
	<StringLength(12)>
	Public Property USERNM As String

	<NotMapped>
	Public Property YOINIDYES As String

	'<NotMapped>
	'Public Property YOINID As Short

	'<NotMapped>
	'Public Property DESKMEMO As Boolean?

	'<NotMapped>
	'Public Property MARKKYST As Boolean?

	'<NotMapped>
	'Public Property MARKSYTK As Boolean?

	'<NotMapped>
	'Public Property ROWIDX As Short

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


	Public Overridable Property D0010 As D0010

    Public Overridable Property M0010 As M0010

    '<Required(ErrorMessage:="{0}���K�v�ł��B")>
    <Display(Name:="�Ɩ�����-�J�n")>
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")>
    Public Property GYOMYMD As Date?


    <Display(Name:="�Ɩ�����-�I��")>
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")>
    Public Property GYOMYMDED As Date?

    '<Required(ErrorMessage:="{0}���K�v�ł��B")>
    <StringLength(5)>
    <Display(Name:="�S������-�J�n")>
    <TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
    Public Property KSKJKNST As String

    '<Required(ErrorMessage:="{0}���K�v�ł��B")>
    <StringLength(5)>
    <Display(Name:="�S������-�I��")>
    <TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
    Public Property KSKJKNED As String

    '<Required>
    <Column(TypeName:="datetime")>
    <Display(Name:="���J�n����")>
    Public Property JTJKNST As Date?

    '<Required>
    <Column(TypeName:="datetime")>
    <Display(Name:="���I������")>
    Public Property JTJKNED As Date?


End Class
