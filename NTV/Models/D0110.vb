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
	<Display(Name:="デスクメモNo")>
	Public Property DESKNO As String

	<Required>
	<Display(Name:="確認")>
	Public Property KAKUNINID As Short

	<Required>
	<Display(Name:="入力者")>
	<Range(1, 32767, ErrorMessage:="{0}が必要です。")>
	Public Property USERID As Short

	<Display(Name:="カテゴリー")>
	Public Property CATCD As Short?

    <Display(Name:="番組名")>
    <ByteLength(40)>
    Public Property BANGUMINM As String

    <ByteLength(40)>
    <Display(Name:="内容")>
    Public Property NAIYO As String

	<ByteLength(30)>
	<Display(Name:="番組担当者")>
	Public Property BANGUMITANTO As String

	<ByteLength(30)>
	<Display(Name:="連絡先")>
	Public Property BANGUMIRENRK As String

    <Display(Name:="デスクメモ")>
    <ByteLength(1000)>
    <DataType(DataType.MultilineText)>
    Public Property DESKMEMO As String

    <Display(Name:="入力日")>
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")>
    <Column(TypeName:="date")>
    Public Property INPUTDT As Date

    <ByteLength(40)>
  <Display(Name:="場所")>
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

    <Display(Name:="更新日")>
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
