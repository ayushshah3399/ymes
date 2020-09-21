Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("TeLAS.M0140")>
Partial Public Class M0140
	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.Identity)>
	Public Property SPORTSUBCATCD As Short

	<Required(ErrorMessage:="{0}���K�v�ł��B")>
	<Display(Name:="�X�|�[�c�T�u�J�e�S���[��")>
	<ByteLength(20)>
	Public Property SPORTSUBCATNM As String

	<Required(ErrorMessage:="{0}���K�v�ł��B")>
	<Display(Name:="�\����")>
	Public Property HYOJJN As Short

	<Display(Name:="�\��")>
	<UIHint("HYOJ")>
	Public Property HYOJ As Boolean

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
	Public Property SPORTCATCD As String

	<NotMapped>
	Public Property SELECTEDINDEX As String

	<NotMapped>
	Public Property USERSETTINGSTATUS As String = ""

    <NotMapped>
    Public Property M0150LIST = {(New With {.COLTYPE = "", .HYOJJN = 0, .COLNAME = "", .COLVALUE = 0, .HYOJ = False})}.ToList

    'Public Property M0150LIST = {(New With {.COLNAME = "", .COLVALUE = 0})}.Take(0).ToList

    <NotMapped>
	Public Property D0010LIST As List(Of D0010)

End Class
