Imports System.ComponentModel.DataAnnotations

Public Class M_UserList

	<Key>
	<Display(Name:="ユーザーID")>
	Public Property USERID As Short

	<ByteLength(12)>
	<Display(Name:="氏名")>
	Public Property USERNM As String

	Public Property USERSEX As Boolean

	Public Property HYOJJN As Short

	Public Property KYUKCD As Short

	Public Property KYUKNM As String

	Public Property BACKCOLOR As String

	Public Property WAKUCOLOR As String

	Public Property FONTCOLOR As String

	Public Property SHUCHO As String

	Public Property TENKAI As String

End Class
