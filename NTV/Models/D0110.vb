Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0110")>
Partial Public Class D0110
	Public Sub New()
		D0120 = New HashSet(Of D0120)()
		D0130 = New HashSet(Of D0130)()
	End Sub

	<Key>
	<Display(Name:="�f�X�N����No")>
	Public Property DESKNO As String

	<Required>
	<Display(Name:="�m�F")>
	Public Property KAKUNINID As Short

	<Required>
	<Display(Name:="���͎�")>
	<Range(1, 32767, ErrorMessage:="{0}���K�v�ł��B")>
	Public Property USERID As Short

	<Display(Name:="�J�e�S���[")>
	Public Property CATCD As Short?

    <Display(Name:="�ԑg��")>
    <ByteLength(40)>
    Public Property BANGUMINM As String

    <ByteLength(40)>
    <Display(Name:="���e")>
    Public Property NAIYO As String

	<ByteLength(30)>
	<Display(Name:="�ԑg�S����")>
	Public Property BANGUMITANTO As String

	<ByteLength(30)>
	<Display(Name:="�A����")>
	Public Property BANGUMIRENRK As String

    <Display(Name:="�f�X�N����")>
    <ByteLength(1000)>
    <DataType(DataType.MultilineText)>
    Public Property DESKMEMO As String

    <Display(Name:="���͓�")>
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")>
    <Column(TypeName:="date")>
    Public Property INPUTDT As Date

    <ByteLength(40)>
  <Display(Name:="�ꏊ")>
    Public Property BASYO As String

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

    <Display(Name:="�X�V��")>
  <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")>
  <Column(TypeName:="datetime2")>
    Public Property UPDTDT As Date

	<StringLength(50)>
	Public Property UPDTTERM As String

	<StringLength(50)>
	Public Property UPDTPRGNM As String

	Public Overridable Property M0010 As M0010

	Public Overridable Property M0020 As M0020

	Public Overridable Property M0100 As M0100

	Public Overridable Property D0120 As ICollection(Of D0120)

	Public Overridable Property D0130 As ICollection(Of D0130)
End Class
