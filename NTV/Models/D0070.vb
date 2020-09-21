Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0070")>
Partial Public Class D0070

	<Key>
	<Column(TypeName:="numeric")>
	<Display(Name:="�ύX�����R�[�h")>
	Public Property HENKORRKCD As Decimal

	<Required>
	<StringLength(4)>
	<Display(Name:="�ύX���e")>
	Public Property HENKONAIYO As String

	<Display(Name:="�C����")>
	Public Property USERID As Short

    <Display(Name:="�C������")>
    Public Property SYUSEIYMD As Date?

    <Display(Name:="�Ɩ�����")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMD As Date?

    <Display(Name:="�Ɩ�����-�I��")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMDED As Date?

	<Required>
	<StringLength(4)>
	<Display(Name:="�S������")>
	Public Property KSKJKNST As String

	<Required>
	<StringLength(4)>
	<Display(Name:="�S������-�I��")>
	Public Property KSKJKNED As String

	<Display(Name:="�J�e�S���[")>
	Public Property CATCD As Short

	<StringLength(40)>
	<Display(Name:="�ԑg��")>
	Public Property BANGUMINM As String

	<StringLength(4)>
	<Display(Name:="OA����")>
	Public Property OAJKNST As String

	<StringLength(4)>
	<Display(Name:="OA����-�I��")>
	Public Property OAJKNED As String

	<StringLength(40)>
	<Display(Name:="���e")>
	Public Property NAIYO As String

	<StringLength(40)>
	<Display(Name:="�ꏊ")>
	Public Property BASYO As String

	<StringLength(30)>
	<Display(Name:="���l")>
	Public Property BIKO As String

	<StringLength(30)>
	<Display(Name:="�ԑg�S����")>
	Public Property BANGUMITANTO As String

	<StringLength(30)>
	<Display(Name:="�A����")>
	Public Property BANGUMIRENRK As String

	<StringLength(65)>
	<Display(Name:="�S���A�i�E���T�[")>
	Public Property TNTNM As String

    <UIHint("IKTFLG")> _
    <Display(Name:="�Ɩ��ꊇ�o�^�t���O")>
    Public Property IKTFLG As Boolean?

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

	<Column(TypeName:="numeric")>
	Public Property GYOMNO As Decimal?

	<Display(Name:="�X�|�[�c�J�e�S��")>
	Public Property SPORTCATCD As Short?

	<Display(Name:="�X�|�[�c�T�u�J�e�S��")>
	Public Property SPORTSUBCATCD As Short?

	<StringLength(5)>
	<Display(Name:="��������")>
	<TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
	Public Property SAIJKNST As String

	<StringLength(5)>
	<Display(Name:="��������-�I��")>
	<TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
	Public Property SAIJKNED As String

	<StringLength(20)>
	Public Property COL01 As String

	<StringLength(20)>
	Public Property COL02 As String

	<StringLength(20)>
	Public Property COL03 As String

	<StringLength(20)>
	Public Property COL04 As String

	<StringLength(20)>
	Public Property COL05 As String

	<StringLength(20)>
	Public Property COL06 As String

	<StringLength(20)>
	Public Property COL07 As String

	<StringLength(20)>
	Public Property COL08 As String

	<StringLength(20)>
	Public Property COL09 As String

	<StringLength(20)>
	Public Property COL10 As String

	<StringLength(20)>
	Public Property COL11 As String

	<StringLength(20)>
	Public Property COL12 As String

	<StringLength(20)>
	Public Property COL13 As String

	<StringLength(20)>
	Public Property COL14 As String

	<StringLength(20)>
	Public Property COL15 As String

	<StringLength(20)>
	Public Property COL16 As String

	<StringLength(20)>
	Public Property COL17 As String

	<StringLength(20)>
	Public Property COL18 As String

	<StringLength(20)>
	Public Property COL19 As String

	<StringLength(20)>
	Public Property COL20 As String

	<StringLength(20)>
	Public Property COL21 As String

	<StringLength(20)>
	Public Property COL22 As String

	<StringLength(20)>
	Public Property COL23 As String

	<StringLength(20)>
	Public Property COL24 As String

	<StringLength(20)>
	Public Property COL25 As String

	<StringLength(20)>
	Public Property COL26 As String

	<StringLength(20)>
	Public Property COL27 As String

	<StringLength(20)>
	Public Property COL28 As String

	<StringLength(20)>
	Public Property COL29 As String

	<StringLength(20)>
	Public Property COL30 As String

	<StringLength(20)>
	Public Property COL31 As String

	<StringLength(20)>
	Public Property COL32 As String

	<StringLength(20)>
	Public Property COL33 As String

	<StringLength(20)>
	Public Property COL34 As String

	<StringLength(20)>
	Public Property COL35 As String

	<StringLength(20)>
	Public Property COL36 As String

	<StringLength(20)>
	Public Property COL37 As String

	<StringLength(20)>
	Public Property COL38 As String

	<StringLength(20)>
	Public Property COL39 As String

	<StringLength(20)>
	Public Property COL40 As String

	<StringLength(20)>
	Public Property COL41 As String

	<StringLength(20)>
	Public Property COL42 As String

	<StringLength(20)>
	Public Property COL43 As String

	<StringLength(20)>
	Public Property COL44 As String

	<StringLength(20)>
	Public Property COL45 As String

	<StringLength(20)>
	Public Property COL46 As String

	<StringLength(20)>
	Public Property COL47 As String

	<StringLength(20)>
	Public Property COL48 As String

	<StringLength(20)>
	Public Property COL49 As String

	<StringLength(20)>
	Public Property COL50 As String

	Public Overridable Property M0010 As M0010

	Public Overridable Property M0020 As M0020

	Public Overridable Property M0130 As M0130

	Public Overridable Property M0140 As M0140
End Class
