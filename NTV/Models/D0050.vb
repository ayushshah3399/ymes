Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0050")>
Partial Public Class D0050
    <Key>
    <Column(TypeName:="numeric")>
     <Display(Name:="�Ɩ��\���R�[�h")>
    Public Property GYOMSNSNO As Decimal

    <Display(Name:="���[�U�[�h�c")>
    Public Property USERID As Short

    <Display(Name:="�Ɩ�����")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMD As Date?

    <Display(Name:="�Ɩ�����")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMDED As Date?

    <Required>
    <StringLength(5)>
    <Display(Name:="�S������")>
    Public Property KSKJKNST As String

    <Required>
    <StringLength(5)>
    <Display(Name:="�S������-�I��")>
    Public Property KSKJKNED As String

    <Display(Name:="�J�e�S���[")>
    Public Property CATCD As Short

    <Required>
    <StringLength(40)>
      <Display(Name:="�ԑg��")>
    Public Property BANGUMINM As String

    <StringLength(40)>
    <Display(Name:="���e")>
      Public Property NAIYO As String

    <StringLength(40)>
    <Display(Name:="�ꏊ")>
    Public Property BASYO As String

    <StringLength(30)>
      <Display(Name:="���l")>
    Public Property GYOMMEMO As String

    <StringLength(30)>
      <Display(Name:="�ԑg�S����")>
    Public Property BANGUMITANTO As String

    <StringLength(30)>
      <Display(Name:="�A����")>
    Public Property BANGUMIRENRK As String

    Public Property SHONINFLG As String


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
       Public Property CONFIRMMSG As Boolean?


    Public Overridable Property M0010 As M0010

    Public Overridable Property M0020 As M0020
End Class
