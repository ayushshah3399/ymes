Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0020")>
Partial Public Class M0020
    Public Sub New()
        D0010 = New HashSet(Of D0010)()
        D0050 = New HashSet(Of D0050)()
        D0070 = New HashSet(Of D0070)()
        D0090 = New HashSet(Of D0090)()
		D0110 = New HashSet(Of D0110)()
		M0090 = New HashSet(Of M0090)()
    End Sub

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    <Display(Name:="�ú�ذ����")>
    Public Property CATCD As Short

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
   <Display(Name:="�J�e�S���[��")>
    <ByteLength(10)>
    Public Property CATNM As String
    <Display(Name:="�\����")>
    Public Property HYOJJN As Short

    <Display(Name:="�\��")>
        <UIHint("HYOJ")> _
    Public Property HYOJ As Boolean
    <Display(Name:="OA����")>
     <UIHint("OATIMEHYOJ")> _
    Public Property OATIMEHYOJ As Boolean
    <Display(Name:="�ԑg��")>
     <UIHint("BANGUMIHYOJ")> _
    Public Property BANGUMIHYOJ As Boolean
    <Display(Name:="�S������")>
        <UIHint("KSKHYOJ")> _
    Public Property KSKHYOJ As Boolean
    <Display(Name:="�S���A�i")>
        <UIHint("ANAHYOJ")> _
    Public Property ANAHYOJ As Boolean
    <Display(Name:="�ꏊ")>
        <UIHint("BASYOHYOJ")> _
    Public Property BASYOHYOJ As Boolean
    <Display(Name:="����")>
        <UIHint("KKNHYOJ")> _
    Public Property KKNHYOJ As Boolean
    <Display(Name:="���l")>
        <UIHint("BIKOHYOJ")> _
    Public Property BIKOHYOJ As Boolean
    <Display(Name:="�ԑg�S����")>
        <UIHint("TANTOHYOJ")> _
    Public Property TANTOHYOJ As Boolean
    <Display(Name:="�A����")>
        <UIHint("RENRKHYOJ")> _
    Public Property RENRKHYOJ As Boolean
    <Display(Name:="�o��")>
        <UIHint("SYUCHO")> _
    Public Property SYUCHO As Boolean
    <Display(Name:="�X�e�[�^�X")>
        <UIHint("STATUS")> _
    Public Property STATUS As Boolean
    <Display(Name:="�A���[�g���[��")>
    <UIHint("ALERTFLG")> _
    Public Property ALERTFLG As Boolean

    <Display(Name:="���e")>
    <UIHint("NAIYOHYOJ")> _
    Public Property NAIYOHYOJ As Boolean

    Public Overridable Property D0010 As ICollection(Of D0010)

    Public Overridable Property D0050 As ICollection(Of D0050)

    Public Overridable Property D0070 As ICollection(Of D0070)

    Public Overridable Property D0090 As ICollection(Of D0090)

	Public Overridable Property D0110 As ICollection(Of D0110)

	Public Overridable Property M0090 As ICollection(Of M0090)

	Public Overridable Property W0070 As ICollection(Of W0070)

End Class
