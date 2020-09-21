Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial



<CustomValidation(GetType(M0090), "ValidateM0110Ana")>
<CustomValidation(GetType(M0090), "ValidateGyomymd")>
<CustomValidation(GetType(M0090), "ValidateGyomymded")>
<CustomValidation(GetType(M0090), "ValidateKskjkn")>
<CustomValidation(GetType(M0090), "ValidateOAjkn")>
<CustomValidation(GetType(M0090), "ValidatePattern")>
<Table("TeLAS.M0090")>
Partial Public Class M0090
    Private Const STR_MAXTIME As String = "36:00"

    Public Sub New()
        D0010 = New HashSet(Of D0010)()
    End Sub

    <Key>
    <Column(TypeName:="numeric")>
     <Display(Name:="�ꊇ�o�^�ԍ�")>
    Public Property IKKATUNO As Decimal

    <ByteLength(30)>
     <Display(Name:="����")>
    Public Property IKKATUMEMO As String

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
    <Display(Name:="�Ɩ�����-�J�n")>
   <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMD As Date?

    <Display(Name:="�Ɩ�����-�I��")>
   <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")> _
    Public Property GYOMYMDED As Date?

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
  <StringLength(5)>
    <Display(Name:="�S������-�J�n")>
     <TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
    Public Property KSKJKNST As String

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
    <StringLength(5)>
     <Display(Name:="�S������-�I��")>
     <TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
    Public Property KSKJKNED As String

    <Display(Name:="�J�e�S���[")>
    <Range(1, 32767, ErrorMessage:="�J�e�S���[���K�v�ł��B")>
    Public Property CATCD As Short

    <Required(ErrorMessage:="{0}���K�v�ł��B")>
    <ByteLength(40)>
     <Display(Name:="�ԑg��")>
    Public Property BANGUMINM As String


    <StringLength(5)>
     <Display(Name:="OA����-�J�n")>
     <TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
    Public Property OAJKNST As String


    <StringLength(5)>
     <Display(Name:="OA����-�I��")>
     <TimeMaxValue(ErrorMessage:="{0}��36���𒴂��Ă��܂��B")>
    Public Property OAJKNED As String


    <ByteLength(40)>
     <Display(Name:="���e")>
    Public Property NAIYO As String


    <ByteLength(40)>
    <Display(Name:="�ꏊ")>
    Public Property BASYO As String

    <Display(Name:="�A�i�E���T�[")>
    Public Property USERID As Short?

    <ByteLength(30)>
     <Display(Name:="���l")>
    Public Property BIKO As String

    <ByteLength(30)>
     <Display(Name:="�ԑg�S����")>
    Public Property BANGUMITANTO As String

    <ByteLength(30)>
     <Display(Name:="�A����")>
    Public Property BANGUMIRENRK As String

    <Display(Name:="�p�^�[���ݒ�")>
      <UIHint("PTNFLG")> _
    Public Property PTNFLG As Boolean

	<Display(Name:="���j")>
	<UIHint("PTN1")> _
    Public Property PTN1 As Boolean

    <Display(Name:="�Ηj")>
      <UIHint("PTN2")> _
    Public Property PTN2 As Boolean

    <Display(Name:="���j")>
      <UIHint("PTN3")> _
    Public Property PTN3 As Boolean

    <Display(Name:="�ؗj")>
      <UIHint("PTN4")> _
    Public Property PTN4 As Boolean

    <Display(Name:="���j")>
      <UIHint("PTN5")> _
    Public Property PTN5 As Boolean

    <Display(Name:="�y�j")>
      <UIHint("PTN6")> _
    Public Property PTN6 As Boolean

    <Display(Name:="���j")>
	<UIHint("PTN7")>
	Public Property PTN7 As Boolean

	'ASI[21 Oct 2019] : added WEEKA and WEEKB
	<Display(Name:="A�T")>
	<UIHint("WEEKA")>
	Public Property WEEKA As Boolean

	<Display(Name:="B�T")>
	<UIHint("WEEKB")>
	Public Property WEEKB As Boolean

	<Display(Name:="�Ɩ��ꊇ�o�^�Ώ�")>
     <UIHint("IKTTAISHO")> _
    Public Property IKTTAISHO As Boolean?

    <StringLength(64)>
     <Display(Name:="�ŏI�X�V��")>
    Public Property UPDTID As String

    <NotMapped>
   <Column(TypeName:="numeric")>
    Public Property HINANO As Decimal?

    '1:�����A2:���`
    <NotMapped>
    Public Property FMTKBN As Short?

    <NotMapped>
    <StringLength(20)>
    Public Property HINAMEMO As String

    <NotMapped>
    Public Property DATAKBN As Short?

    <NotMapped>
    Public Property ANAIDLIST As String

    <NotMapped>
    Public Property KARIANALIST As String

    <NotMapped>
    Public Property FLGDEL As Boolean


    Public Overridable Property D0010 As ICollection(Of D0010)

    'Public Overridable Property M0010 As M0010 

    Public Overridable Property M0020 As M0020

    Public Overridable Property M0110 As ICollection(Of M0110)

    Public Overridable Property M0120 As ICollection(Of M0120)

    Public Shared Function ValidatePattern(ByVal m0090 As M0090) As ValidationResult
        If m0090 IsNot Nothing Then

            If m0090.PTNFLG Then

				If m0090.PTN1 = False AndAlso m0090.PTN2 = False AndAlso m0090.PTN3 = False AndAlso m0090.PTN4 = False AndAlso
								m0090.PTN5 = False AndAlso m0090.PTN6 = False AndAlso m0090.PTN7 = False Then
					Return New ValidationResult("�J��Ԃ��o�^�̏ꍇ�A�j���w�肪�K�v�ł��B", New String() {"PTN1"})
				End If

				If (m0090.WEEKA AndAlso m0090.WEEKB) Then
                    Return New ValidationResult("A�T��B�T�𓯎��ɑI�����邱�Ƃ͂ł��܂���B", New String() {"PTN1"})
                End If

				If m0090.PTN1 OrElse m0090.PTN2 OrElse m0090.PTN3 OrElse m0090.PTN4 OrElse m0090.PTN5 OrElse m0090.PTN6 OrElse m0090.PTN7 Then

					If m0090.GYOMYMDED IsNot Nothing AndAlso m0090.GYOMYMD <= m0090.GYOMYMDED Then
						Dim dtYMDFr As Date = m0090.GYOMYMD
						Dim bolExist As Boolean = False

						While dtYMDFr <= m0090.GYOMYMDED
							If m0090.PTN1 = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Monday Then
								bolExist = True
								Exit While
							ElseIf m0090.PTN2 = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Tuesday Then
								bolExist = True
								Exit While
							ElseIf m0090.PTN3 = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Wednesday Then
								bolExist = True
								Exit While
							ElseIf m0090.PTN4 = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Thursday Then
								bolExist = True
								Exit While
							ElseIf m0090.PTN5 = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Friday Then
								bolExist = True
								Exit While
							ElseIf m0090.PTN6 = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Saturday Then
								bolExist = True
								Exit While
							ElseIf m0090.PTN7 = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Sunday Then
								bolExist = True
								Exit While

								'ASI[21 OCT 2019] : Added condition of WEEKA and WEEKB
								'ElseIf m0090.WEEKA = True Then
								'	bolExist = True
								'	Exit While
								'ElseIf m0090.WEEKB = True Then
								'	bolExist = True
								'	Exit While
							End If

							dtYMDFr = dtYMDFr.AddDays(1)
						End While

						If bolExist = False Then
							Return New ValidationResult("�Ɩ����ԓ��Ɏw��̗j�������݂��܂���B")
						End If
					End If

				End If

				If (m0090.PTN1 AndAlso m0090.PTN2) OrElse (m0090.PTN2 AndAlso m0090.PTN3) OrElse (m0090.PTN3 AndAlso m0090.PTN4) OrElse (m0090.PTN4 AndAlso m0090.PTN5) OrElse
			   (m0090.PTN5 AndAlso m0090.PTN6) OrElse (m0090.PTN6 AndAlso m0090.PTN7) OrElse (m0090.PTN7 AndAlso m0090.PTN1) Then

						Dim dtymd As Date = m0090.GYOMYMD
						Dim dtymded As Date = Nothing
						Dim strKSKJKNST As String = ChangeToHHMM(m0090.KSKJKNST)
						Dim strKSKJKNED As String = ChangeToHHMM(m0090.KSKJKNED)

						'�J�n���� > �I�����Ԃ̏ꍇ�A�J�n��+1
						If strKSKJKNST > strKSKJKNED Then
							dtymded = dtymd.AddDays(1)
						Else
							'�J�n���� <= �I������
							dtymded = dtymd
						End If

						Dim dtJTJKNST As Date = GetJtjkn(dtymd, strKSKJKNST)
						Dim dtJTJKNED As Date = GetJtjkn(dtymded, strKSKJKNED)

						If DateDiff(DateInterval.Minute, dtJTJKNST, dtJTJKNED) > 1440 Then
							Return New ValidationResult("�p�^�[���j�����A�����Ă���ꍇ�A�S�����Ԃ̑����Ԃ�24���Ԉȓ��������͂ł��܂���B", New String() {"KSKJKNST"})
						End If
					End If
				End If
			End If

        Return ValidationResult.Success

    End Function

    Public Shared Function ValidateGyomymd(ByVal m0090 As M0090) As ValidationResult
        If m0090 IsNot Nothing Then
            If m0090.GYOMYMD IsNot Nothing AndAlso m0090.GYOMYMDED IsNot Nothing Then
                If m0090.GYOMYMD > m0090.GYOMYMDED Then
                    Return New ValidationResult("�Ɩ�����-�J�n�ƏI���̑O��֌W������Ă��܂��B", New String() {"GYOMYMD"})
                End If

                If m0090.PTNFLG AndAlso m0090.GYOMYMD = m0090.GYOMYMDED Then
                    Return New ValidationResult("�J��Ԃ��o�^�̏ꍇ�A�Ɩ�����-�J�n�ƏI���ɓ������t�͎w��ł��܂���B", New String() {"GYOMYMDED"})
                End If
            End If
        End If

        Return ValidationResult.Success
    End Function

    Public Shared Function ValidateGyomymded(ByVal m0090 As M0090) As ValidationResult
        If m0090 IsNot Nothing Then
            If m0090.PTNFLG AndAlso m0090.GYOMYMDED Is Nothing Then
                Return New ValidationResult("�J��Ԃ��o�^�̏ꍇ�A�Ɩ�����-�I�����K�v�ł��B", New String() {"GYOMYMDED"})
            End If
        End If

        Return ValidationResult.Success
    End Function


    Public Shared Function ValidateOAjkn(ByVal m0090 As M0090) As ValidationResult
        If m0090 IsNot Nothing Then
            If m0090.OAJKNST IsNot Nothing AndAlso m0090.OAJKNED IsNot Nothing AndAlso m0090.OAJKNST.PadLeft(5, "0") > m0090.OAJKNED.PadLeft(5, "0") Then
                Return New ValidationResult("OA����-�J�n�ƏI���̑O��֌W������Ă��܂��B", New String() {"OAJKNST"})
            End If
        End If

        Return ValidationResult.Success
    End Function

    Public Shared Function ValidateKskjkn(ByVal m0090 As M0090) As ValidationResult
        If m0090 IsNot Nothing Then

            If m0090.KSKJKNST IsNot Nothing AndAlso m0090.KSKJKNED IsNot Nothing Then

                Dim dtGYOMYMDED As Date = m0090.GYOMYMD
                Dim strKSKJKNST As String = ChangeToHHMM(m0090.KSKJKNST)
                Dim strKSKJKNED As String = ChangeToHHMM(m0090.KSKJKNED)

                If m0090.GYOMYMDED IsNot Nothing Then
                    dtGYOMYMDED = m0090.GYOMYMDED
                End If

                '�����ԂőO��֌W�`�F�b�N
                Dim jtjknst As Date = GetJtjkn(m0090.GYOMYMD, strKSKJKNST)
                Dim jtjkned As Date = GetJtjkn(dtGYOMYMDED, strKSKJKNED)


                If m0090.GYOMYMD <= dtGYOMYMDED AndAlso jtjknst > jtjkned Then
                    Return New ValidationResult("�S������-�J�n�ƏI���̑O��֌W������Ă��܂��B", New String() {"KSKJKNST"})
                End If
            End If

        End If

        Return ValidationResult.Success
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

    Public Shared Function ValidateM0110Ana(ByVal M0090 As M0090) As ValidationResult

        If M0090 IsNot Nothing Then

            If M0090.FMTKBN = 1 OrElse M0090.FMTKBN = 2 Then
                Return ValidationResult.Success
            End If

            'If (M0090.M0110 Is Nothing OrElse M0090.M0110.Count = 0) AndAlso (M0090.M0110 Is Nothing OrElse M0090.M0110.Count = 0) Then
            '    Return New ValidationResult("�S���̃A�i�E���T�[���A���͉��A�i�J�e�S���[��ݒ肵�Ă��������B")
            'End If
            Dim bolAnaExist As Boolean = False
            Dim bolKariExist As Boolean = False
            If M0090.M0110 IsNot Nothing AndAlso M0090.M0110.Count > 0 Then

                For Each item In M0090.M0110
                    If item.USERID > 0 Then
                        bolAnaExist = True
                        Exit For
                    End If
                Next

            End If


            If M0090.M0120 IsNot Nothing AndAlso M0090.M0120.Count > 0 Then
                Dim bolExist As Boolean = False
                For Each item In M0090.M0120
                    If String.IsNullOrEmpty(item.ANNACATNM) = False Then
                        bolKariExist = True
                        Exit For
                    End If
                Next

            End If

            If bolAnaExist = False AndAlso bolKariExist = False Then
                Return New ValidationResult("�S���̃A�i�E���T�[���A���͉��A�i�J�e�S���[��ݒ肵�Ă��������B")
            End If

        End If

        Return ValidationResult.Success
    End Function

End Class


