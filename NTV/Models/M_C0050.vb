Imports System.ComponentModel.DataAnnotations

Public Class M_C0050

	<Key>
	<Display(Name:="ユーザーID")>
	Public Property USERID As Short

	<ByteLength(12)>
	<Display(Name:="氏名")>
	Public Property USERNM As String

	Public Property USERSEX As String

	Public Property HYOJJN As Short

	Public Property USERBDCL As String

	Public Property ERRKBN As Short

	Public Property DESKNO As String

	Public Property GYOMNO As String

	Public Property BANGUMINM As String

	Public Property SHIFTTM As Short

	Public Property SHIFTSPAN As Short

	Public Property KYUKTM As Short

	Public Property KYUKSPAN As Short

	Public Property KYUKRNM As String

	Public Property KYUKBKCL As String

	Public Property KYUKFTCL As String

	Public Property DESKTM As Short

	Public Property DESKSPAN As Short

	Public Property SYUCHO As Short

	Public Property TM As Short

	Public Property SPAN As Short

End Class
