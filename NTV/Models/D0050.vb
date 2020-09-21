Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0050")>
Partial Public Class D0050
    <Key>
    <Column(TypeName:="numeric")>
     <Display(Name:="業務申請コード")>
    Public Property GYOMSNSNO As Decimal

    <Display(Name:="ユーザーＩＤ")>
    Public Property USERID As Short

    <Display(Name:="業務期間")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMD As Date?

    <Display(Name:="業務期間")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMDED As Date?

    <Required>
    <StringLength(5)>
    <Display(Name:="拘束時間")>
    Public Property KSKJKNST As String

    <Required>
    <StringLength(5)>
    <Display(Name:="拘束時間-終了")>
    Public Property KSKJKNED As String

    <Display(Name:="カテゴリー")>
    Public Property CATCD As Short

    <Required>
    <StringLength(40)>
      <Display(Name:="番組名")>
    Public Property BANGUMINM As String

    <StringLength(40)>
    <Display(Name:="内容")>
      Public Property NAIYO As String

    <StringLength(40)>
    <Display(Name:="場所")>
    Public Property BASYO As String

    <StringLength(30)>
      <Display(Name:="備考")>
    Public Property GYOMMEMO As String

    <StringLength(30)>
      <Display(Name:="番組担当者")>
    Public Property BANGUMITANTO As String

    <StringLength(30)>
      <Display(Name:="連絡先")>
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
