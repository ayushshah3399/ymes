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
    <Display(Name:="ｶﾃｺﾞﾘｰｺｰﾄﾞ")>
    Public Property CATCD As Short

    <Required(ErrorMessage:="{0}が必要です。")>
   <Display(Name:="カテゴリー名")>
    <ByteLength(10)>
    Public Property CATNM As String
    <Display(Name:="表示順")>
    Public Property HYOJJN As Short

    <Display(Name:="表示")>
        <UIHint("HYOJ")> _
    Public Property HYOJ As Boolean
    <Display(Name:="OA時間")>
     <UIHint("OATIMEHYOJ")> _
    Public Property OATIMEHYOJ As Boolean
    <Display(Name:="番組名")>
     <UIHint("BANGUMIHYOJ")> _
    Public Property BANGUMIHYOJ As Boolean
    <Display(Name:="拘束時間")>
        <UIHint("KSKHYOJ")> _
    Public Property KSKHYOJ As Boolean
    <Display(Name:="担当アナ")>
        <UIHint("ANAHYOJ")> _
    Public Property ANAHYOJ As Boolean
    <Display(Name:="場所")>
        <UIHint("BASYOHYOJ")> _
    Public Property BASYOHYOJ As Boolean
    <Display(Name:="期間")>
        <UIHint("KKNHYOJ")> _
    Public Property KKNHYOJ As Boolean
    <Display(Name:="備考")>
        <UIHint("BIKOHYOJ")> _
    Public Property BIKOHYOJ As Boolean
    <Display(Name:="番組担当者")>
        <UIHint("TANTOHYOJ")> _
    Public Property TANTOHYOJ As Boolean
    <Display(Name:="連絡先")>
        <UIHint("RENRKHYOJ")> _
    Public Property RENRKHYOJ As Boolean
    <Display(Name:="出張")>
        <UIHint("SYUCHO")> _
    Public Property SYUCHO As Boolean
    <Display(Name:="ステータス")>
        <UIHint("STATUS")> _
    Public Property STATUS As Boolean
    <Display(Name:="アラートメール")>
    <UIHint("ALERTFLG")> _
    Public Property ALERTFLG As Boolean

    <Display(Name:="内容")>
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
