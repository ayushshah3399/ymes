Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<CustomValidation(GetType(D0120), "ValidateSHIFTYMD")>
<Table("TeLAS.D0120")>
Partial Public Class D0120
	<Key>
	<Column(Order:=0)>
	Public Property DESKNO As String

	<Key>
	<Column(Order:=1)>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property EDA As Short

	<DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
	Public Property SHIFTYMDST As Date?

	<DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
	Public Property SHIFTYMDED As Date?

	<StringLength(5)>
	Public Property KSKJKNST As String

	<StringLength(5)>
	Public Property KSKJKNED As String

	<Column(TypeName:="datetime")>
	Public Property JTJKNST As Date?

	<Column(TypeName:="datetime")>
	Public Property JTJKNED As Date?

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

	Public Overridable Property D0110 As D0110

	Public Shared Function ValidateSHIFTYMD(ByVal d0120 As D0120) As ValidationResult
		If d0120 IsNot Nothing Then

			If d0120.SHIFTYMDST Is Nothing AndAlso d0120.SHIFTYMDED IsNot Nothing Then
				Return New ValidationResult("�V�t�g�I�������ݒ肳��Ă���ꍇ�A�J�n���͕K�v�ł��B", New String() {"SHIFTYMDED"})
			End If

			'�V�t�g���t�̑O��֌W
			If d0120.SHIFTYMDST IsNot Nothing AndAlso d0120.SHIFTYMDED IsNot Nothing Then
				If d0120.SHIFTYMDST > d0120.SHIFTYMDED Then
					Return New ValidationResult("�V�t�g��-�J�n�ƏI���̑O��֌W������Ă��܂��B", New String() {"SHIFTYMDED"})
				End If
			End If

			'�V�t�g�����J�n���̂݁A�܂��́A�J�n���ƏI�����������Ƃ��͎��Ԃ̑O��֌W�̃`�F�b�N
			If d0120.SHIFTYMDST IsNot Nothing AndAlso d0120.KSKJKNST IsNot Nothing AndAlso d0120.KSKJKNED IsNot Nothing Then
				If d0120.SHIFTYMDED Is Nothing OrElse d0120.SHIFTYMDST = d0120.SHIFTYMDED Then
					Dim strKSKJKNST As String = ChangeToHHMM(d0120.KSKJKNST)
					Dim strKSKJKNED As String = ChangeToHHMM(d0120.KSKJKNED)

					'�����ԂőO��֌W�`�F�b�N
					Dim jtjknst As Date = GetJtjkn(d0120.SHIFTYMDST, strKSKJKNST)
					Dim jtjkned As Date = GetJtjkn(d0120.SHIFTYMDST, strKSKJKNED)

					If jtjknst > jtjkned Then
						Return New ValidationResult("�S������-�J�n�ƏI���̑O��֌W������Ă��܂��B", New String() {"SHIFTYMDED"})
					End If
				End If
			End If
			
		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function ChangeToHHMM(ByVal strTime As String)

		If String.IsNullOrEmpty(strTime) = False Then
			Dim strHH As String = ""
			Dim strMM As String = ""

			If strTime.Contains(":") Then
				Dim strs As String() = strTime.PadLeft(5, "0").Split(":")
				strHH = strs(0).PadLeft(2, "0")
				strMM = strs(1).PadLeft(2, "0")
				strTime = strHH & strMM
			Else
				If strTime.Length <= 2 Then
					strHH = strTime.PadLeft(2, "0")
					strMM = "00"
					strTime = strHH & strMM
				End If
			End If
		End If

		Return strTime
	End Function

	Public Shared Function GetJtjkn(ByVal dt As Date, ByVal time As String) As Date
		Dim dtRtn As Date = Nothing
		Dim strHH As String = ""
		Dim strMM As String = ""

		If time.Contains(":") Then
			Dim strs As String() = time.Split(":")
			strHH = strs(0).PadLeft(2, "0")
			strMM = strs(1).PadLeft(2, "0")
		Else
			If time.Length <= 2 Then
				strHH = time.PadLeft(2, "0")
				strMM = "00"
			Else
				strHH = time.Substring(0, 2)
				strMM = time.Substring(2, 2)
			End If
		End If

		'36:00�܂œo�^�\�Ȃ̂ŁA�����Ԃ��Q�S���Ԑ��x�ɕύX����
		If strHH >= "24" Then
			Dim intHH As Integer = Integer.Parse(strHH) - 24
			strHH = intHH.ToString.PadLeft(2, "0")
			dt = dt.AddDays(1)
		End If

		dtRtn = Date.Parse(dt.ToString("yyyy/MM/dd") & " " & strHH & ":" & strMM)

		Return dtRtn
	End Function

End Class
