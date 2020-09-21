Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.D0070")>
Partial Public Class D0070

	<Key>
	<Column(TypeName:="numeric")>
	<Display(Name:="変更履歴コード")>
	Public Property HENKORRKCD As Decimal

	<Required>
	<StringLength(4)>
	<Display(Name:="変更内容")>
	Public Property HENKONAIYO As String

	<Display(Name:="修正者")>
	Public Property USERID As Short

    <Display(Name:="修正日時")>
    Public Property SYUSEIYMD As Date?

    <Display(Name:="業務期間")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMD As Date?

    <Display(Name:="業務期間-終了")>
     <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMDED As Date?

	<Required>
	<StringLength(4)>
	<Display(Name:="拘束時間")>
	Public Property KSKJKNST As String

	<Required>
	<StringLength(4)>
	<Display(Name:="拘束時間-終了")>
	Public Property KSKJKNED As String

	<Display(Name:="カテゴリー")>
	Public Property CATCD As Short

	<StringLength(40)>
	<Display(Name:="番組名")>
	Public Property BANGUMINM As String

	<StringLength(4)>
	<Display(Name:="OA時間")>
	Public Property OAJKNST As String

	<StringLength(4)>
	<Display(Name:="OA時間-終了")>
	Public Property OAJKNED As String

	<StringLength(40)>
	<Display(Name:="内容")>
	Public Property NAIYO As String

	<StringLength(40)>
	<Display(Name:="場所")>
	Public Property BASYO As String

	<StringLength(30)>
	<Display(Name:="備考")>
	Public Property BIKO As String

	<StringLength(30)>
	<Display(Name:="番組担当者")>
	Public Property BANGUMITANTO As String

	<StringLength(30)>
	<Display(Name:="連絡先")>
	Public Property BANGUMIRENRK As String

	<StringLength(65)>
	<Display(Name:="担当アナウンサー")>
	Public Property TNTNM As String

    <UIHint("IKTFLG")> _
    <Display(Name:="業務一括登録フラグ")>
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

	<Display(Name:="スポーツカテゴリ")>
	Public Property SPORTCATCD As Short?

	<Display(Name:="スポーツサブカテゴリ")>
	Public Property SPORTSUBCATCD As Short?

	<StringLength(5)>
	<Display(Name:="試合時間")>
	<TimeMaxValue(ErrorMessage:="{0}が36時を超えています。")>
	Public Property SAIJKNST As String

	<StringLength(5)>
	<Display(Name:="試合時間-終了")>
	<TimeMaxValue(ErrorMessage:="{0}が36時を超えています。")>
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
