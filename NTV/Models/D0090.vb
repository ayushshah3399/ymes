Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports System.ComponentModel

<Table("TeLAS.D0090")>
Partial Public Class D0090
    Public Sub New()
        D0100 = New HashSet(Of D0100)()
    End Sub

    <Key>
    <Column(TypeName:="numeric")>
    <Display(Name:="���`�ԍ�")>
    Public Property HINANO As Decimal

    <Display(Name:="����")>
   <StringLength(30)>
    Public Property HINAMEMO As String

    <Display(Name:="����")>
     <DefaultValue(2)>
    Public Property FMTKBN As Short

	<Display(Name:="�敪")>
	<UIHint("DATAKBN")> _
	Public Property DATAKBN As Short

	<Display(Name:="�Ɩ�����")>
	<DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}")> _
	Public Property GYOMYMD As Date?

	<Display(Name:="�Ɩ�����")>
	<DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}")> _
	Public Property GYOMYMDED As Date?

    <StringLength(4)>
    <Display(Name:="�S������")>
    Public Property KSKJKNST As String
      
    <StringLength(4)>
    <Display(Name:="�S������")>
    Public Property KSKJKNED As String

    <Display(Name:="�ú�ذ")>
    Public Property CATCD As Short

    <StringLength(40)>
     <Display(Name:="�ԑg��")>
    Public Property BANGUMINM As String

    <StringLength(4)>
        <Display(Name:="OA����")>
    Public Property OAJKNST As String

    <StringLength(4)>
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

    <Display(Name:="�p�^�[���o�^")>
      <UIHint("PTNFLG")> _
    Public Property PTNFLG As Boolean?

    <Display(Name:="��")>
    <UIHint("PTN1")> _
    Public Property PTN1 As Boolean

    <Display(Name:="��")>
    <UIHint("PTN2")> _
    Public Property PTN2 As Boolean

    <Display(Name:="��")>
    <UIHint("PTN3")> _
    Public Property PTN3 As Boolean

    <Display(Name:="��")>
    <UIHint("PTN4")> _
    Public Property PTN4 As Boolean

    <Display(Name:="��")>
    <UIHint("PTN5")> _
    Public Property PTN5 As Boolean

    <Display(Name:="�y")>
     <UIHint("PTN6")> _
    Public Property PTN6 As Boolean

    <Display(Name:="��")>
	<UIHint("PTN7")>
	Public Property PTN7 As Boolean

	'ASI[21 Oct 2019] : added WEEKA and WEEKB
	<Display(Name:="A�T")>
	<UIHint("WEEKA")>
	Public Property WEEKA As Boolean

	<Display(Name:="B�T")>
	<UIHint("WEEKB")>
	Public Property WEEKB As Boolean

	<UIHint("SIYOFLG")>
    Public Property SIYOFLG As Boolean

    Public Property SIYOUSERID As Short?

    Public Property SIYODATE As Date?

	Public Property STATUS As Short?

	<NotMapped>
	Public Property FLGDEL As Boolean

    <StringLength(64)>
      <Display(Name:="�o�^��")>
    Public Property INSTID As String

	<Column(TypeName:="datetime2")>
	  <Display(Name:="�ۑ�����")>
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
  

    Public Overridable Property M0010 As M0010

    Public Overridable Property M0020 As M0020

	Public Overridable Property D0100 As ICollection(Of D0100)

	Public Overridable Property D0101 As ICollection(Of D0101)

End Class
