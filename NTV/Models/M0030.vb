Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0030")>
Partial Public Class M0030
	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.Identity)>
	<Display(Name:="�ԑg�R�[�h")>
	   Public Property BANGUMICD As Short

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
    <ByteLength(40)>
    <Display(Name:="�ԑg��")>
    Public Property BANGUMINM As String

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
    <ByteLength(80)>
    <Display(Name:="�ԑg���J�i")>
    Public Property BANGUMIKN As String

   

    'Public Overridable Property M0090 As ICollection(Of M0090)

End Class
