Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0040")>
Partial Public Class M0040
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
      <Display(Name:="���e�R�[�h")>
    Public Property NAIYOCD As Short

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
    <ByteLength(40)>
      <Display(Name:="���e��")>
    Public Property NAIYO As String

  

    'Public Overridable Property M0090 As ICollection(Of M0090)
End Class
