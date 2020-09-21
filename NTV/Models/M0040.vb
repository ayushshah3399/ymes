Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0040")>
Partial Public Class M0040
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
      <Display(Name:="内容コード")>
    Public Property NAIYOCD As Short

    <Required(ErrorMessage:="{0}が必要です。")>
    <ByteLength(40)>
      <Display(Name:="内容名")>
    Public Property NAIYO As String

  

    'Public Overridable Property M0090 As ICollection(Of M0090)
End Class
