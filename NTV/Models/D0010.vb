Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports System.Data.SqlClient
Imports System.ComponentModel

<Table("TeLAS.D0010")>
<CustomValidation(GetType(D0010), "ValidateD0020Ana")>
<CustomValidation(GetType(D0010), "ValidateGyomymd")>
<CustomValidation(GetType(D0010), "ValidateGyomymded")>
<CustomValidation(GetType(D0010), "ValidateKskjkn")>
<CustomValidation(GetType(D0010), "ValidateOAjkn")>
<CustomValidation(GetType(D0010), "ValidateSAIjkn")>
<CustomValidation(GetType(D0010), "ValidatePattern")>
<CustomValidation(GetType(D0010), "ValidateEachItemAttribute")>
Partial Public Class D0010

	Public Sub New()
		D0020 = New HashSet(Of D0020)()
	End Sub

	<Key>
	<Column(TypeName:="numeric")>
	<Display(Name:="業務番号")>
	<DatabaseGenerated(DatabaseGeneratedOption.None)>
	Public Property GYOMNO As Decimal

	<Required(ErrorMessage:="{0}が必要です。")>
	<Display(Name:="業務期間-開始")>
	<DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")>
	Public Property GYOMYMD As Date?


	<Display(Name:="業務期間-終了")>
	<DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:yyyy/MM/dd}")>
	Public Property GYOMYMDED As Date?

	<Required(ErrorMessage:="{0}が必要です。")>
	<StringLength(5)>
	<Display(Name:="拘束時間-開始")>
	<TimeMaxValue(ErrorMessage:="{0}が36時を超えています。")>
	Public Property KSKJKNST As String

	<Required(ErrorMessage:="{0}が必要です。")>
	<StringLength(5)>
	<Display(Name:="拘束時間-終了")>
	<TimeMaxValue(ErrorMessage:="{0}が36時を超えています。")>
	Public Property KSKJKNED As String

	<Required>
	<Column(TypeName:="datetime")>
	<Display(Name:="実開始時間")>
	Public Property JTJKNST As Date

	<Required>
	<Column(TypeName:="datetime")>
	<Display(Name:="実終了時間")>
	Public Property JTJKNED As Date

	<Required>
	<Display(Name:="カテゴリー")>
	<Range(1, 32767, ErrorMessage:="{0}が必要です。")>
	Public Property CATCD As Short

	<Required(ErrorMessage:="{0}が必要です。")>
	<ByteLength(40)>
	<Display(Name:="番組名")>
	Public Property BANGUMINM As String

	<StringLength(5)>
	<Display(Name:="OA時間")>
	<TimeMaxValue(ErrorMessage:="{0}が36時を超えています。")>
	Public Property OAJKNST As String

	<StringLength(5)>
	<Display(Name:="OA時間-終了")>
	<TimeMaxValue(ErrorMessage:="{0}が36時を超えています。")>
	Public Property OAJKNED As String

	<ByteLength(40)>
	<Display(Name:="内容")>
	Public Property NAIYO As String

	<ByteLength(40)>
	<Display(Name:="場所")>
	Public Property BASYO As String

	<ByteLength(30)>
	<Display(Name:="備考")>
	<DataType(DataType.MultilineText)>
	Public Property BIKO As String

	<ByteLength(30)>
	<Display(Name:="番組担当者")>
	Public Property BANGUMITANTO As String

	<ByteLength(30)>
	<Display(Name:="連絡先")>
	Public Property BANGUMIRENRK As String

	<Display(Name:="連番業務フラグ")>
	Public Property RNZK As Boolean

	<Column(TypeName:="numeric")>
	Public Property PGYOMNO As Decimal?

	<Display(Name:="業務一括登録フラグ")>
	Public Property IKTFLG As Boolean?

	<Display(Name:="一括登録実地ユーザーID")>
	Public Property IKTUSERID As Short?

	<Column(TypeName:="numeric")>
	<Display(Name:="一括登録番号")>
	Public Property IKKATUNO As Decimal?

	<NotMapped>
    <Display(Name:="パターンの設定")>
    Public Property PATTERN As Boolean

    <NotMapped>
    Public Property INDIVIDUAL As Boolean

    <NotMapped>
	<Display(Name:="月曜")>
	Public Property MON As Boolean

	<NotMapped>
	<Display(Name:="火曜")>
	Public Property TUE As Boolean

	<NotMapped>
	<Display(Name:="水曜")>
	Public Property WED As Boolean

	<NotMapped>
	<Display(Name:="木曜")>
	Public Property TUR As Boolean

	<NotMapped>
	<Display(Name:="金曜")>
	Public Property FRI As Boolean

	<NotMapped>
	<Display(Name:="土曜")>
	Public Property SAT As Boolean

	<NotMapped>
	<Display(Name:="日曜")>
	Public Property SUN As Boolean

	'ASI[24 Oct 2019] : Added WEEK and WEEKB properties
	<NotMapped>
	<Display(Name:="A週")>
	Public Property WEEKA As Boolean

	<NotMapped>
	<Display(Name:="B週")>
	Public Property WEEKB As Boolean

	<NotMapped>
	Public Property FLGDEL As Boolean

	<NotMapped>
	Public Property ACUSERID As Short

	<NotMapped>
	Public Property DESKNO As String

	<NotMapped>
	Public Property DESKMEMO As String

	<NotMapped>
	<Display(Name:="業務申請コード")>
	Public Property GYOMSNSNO As Decimal?

	<NotMapped>
	<Column(TypeName:="numeric")>
	Public Property HINANO As Decimal?

	<NotMapped>
	Public Property RefAnalist As List(Of String)

	<NotMapped>
	Public Property RefCatAnalist As List(Of String)

	<NotMapped>
	Public Property RefKariAnalist As List(Of String)

	<NotMapped>
	Public Property RefCatKariAnalist As List(Of String)

	'1:下書、2:雛形
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
	Public Property CONFIRMMSG As Boolean?

	<NotMapped>
	Public Property YOINUSERID As Short?

	<NotMapped>
	Public Property YOINUSERNM As String

	<NotMapped>
	Public Property YOINIDYES As String

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


	Public Overridable Property M0020 As M0020

	Public Overridable Property M0090 As M0090

	Public Overridable Property M0130 As M0130

	Public Overridable Property M0140 As M0140

	Public Overridable Property D0020 As ICollection(Of D0020)

	Public Overridable Property D0021 As ICollection(Of D0021)

	'ASI[12 Nov 2019]:Added SOUSHINFLG, SPORTFLG, OYAGYOMFLG, SPORTCATCD, SPORTSUBCATCD, SAIJKNST, SAIJKNED properties

	'<Display(Name:="メール送信フラグ")>
	'Public Property SOUSHINFLG As Boolean?

	Public Property SPORTFLG As Boolean

	Public Property OYAGYOMFLG As Boolean

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

	<NotMapped>
	Public Overridable Property M0150 As M0150

	<Display(Name:="項目1")>
	<ByteLength(20)>
	Public Property COL01 As String

	<Display(Name:="項目2")>
	<ByteLength(20)>
	Public Property COL02 As String

	<Display(Name:="項目3")>
	<ByteLength(20)>
	Public Property COL03 As String

	<Display(Name:="項目4")>
	<ByteLength(20)>
	Public Property COL04 As String

	<Display(Name:="項目5")>
	<ByteLength(20)>
	Public Property COL05 As String

	<Display(Name:="項目6")>
	<ByteLength(20)>
	Public Property COL06 As String

	<Display(Name:="項目7")>
	<ByteLength(20)>
	Public Property COL07 As String

	<Display(Name:="項目8")>
	<ByteLength(20)>
	Public Property COL08 As String

	<Display(Name:="項目9")>
	<ByteLength(20)>
	Public Property COL09 As String

	<Display(Name:="項目10")>
	<ByteLength(20)>
	Public Property COL10 As String

	<Display(Name:="項目11")>
	<ByteLength(20)>
	Public Property COL11 As String

	<Display(Name:="項目12")>
	<ByteLength(20)>
	Public Property COL12 As String

	<Display(Name:="項目13")>
	<ByteLength(20)>
	Public Property COL13 As String

	<Display(Name:="項目14")>
	<ByteLength(20)>
	Public Property COL14 As String

	<Display(Name:="項目15")>
	<ByteLength(20)>
	Public Property COL15 As String

	<Display(Name:="項目16")>
	<ByteLength(20)>
	Public Property COL16 As String

	<Display(Name:="項目17")>
	<ByteLength(20)>
	Public Property COL17 As String

	<Display(Name:="項目18")>
	<ByteLength(20)>
	Public Property COL18 As String

	<Display(Name:="項目19")>
	<ByteLength(20)>
	Public Property COL19 As String

	<Display(Name:="項目20")>
	<ByteLength(20)>
	Public Property COL20 As String

	<Display(Name:="項目21")>
	<ByteLength(20)>
	Public Property COL21 As String

	<Display(Name:="項目22")>
	<ByteLength(20)>
	Public Property COL22 As String

	<Display(Name:="項目23")>
	<ByteLength(20)>
	Public Property COL23 As String

	<Display(Name:="項目24")>
	<ByteLength(20)>
	Public Property COL24 As String

	<Display(Name:="項目25")>
	<ByteLength(20)>
	Public Property COL25 As String

	Public Property SPORT_OYAFLG As Boolean

	<ByteLength(20)>
	<Display(Name:="項目26")>
	Public Property COL26 As String

	<ByteLength(20)>
	<Display(Name:="項目27")>
	Public Property COL27 As String

	<ByteLength(20)>
	<Display(Name:="項目28")>
	Public Property COL28 As String

	<ByteLength(20)>
	<Display(Name:="項目29")>
	Public Property COL29 As String

	<ByteLength(20)>
	<Display(Name:="項目30")>
	Public Property COL30 As String

	<ByteLength(20)>
	<Display(Name:="項目31")>
	Public Property COL31 As String

	<ByteLength(20)>
	<Display(Name:="項目32")>
	Public Property COL32 As String

	<ByteLength(20)>
	<Display(Name:="項目33")>
	Public Property COL33 As String

	<ByteLength(20)>
	<Display(Name:="項目34")>
	Public Property COL34 As String

	<ByteLength(20)>
	<Display(Name:="項目35")>
	Public Property COL35 As String

	<ByteLength(20)>
	<Display(Name:="項目36")>
	Public Property COL36 As String

	<ByteLength(20)>
	<Display(Name:="項目37")>
	Public Property COL37 As String

	<ByteLength(20)>
	<Display(Name:="項目38")>
	Public Property COL38 As String

	<ByteLength(20)>
	<Display(Name:="項目39")>
	Public Property COL39 As String

	<ByteLength(20)>
	<Display(Name:="項目40")>
	Public Property COL40 As String

	<ByteLength(20)>
	<Display(Name:="項目41")>
	Public Property COL41 As String

	<ByteLength(20)>
	<Display(Name:="項目42")>
	Public Property COL42 As String

	<ByteLength(20)>
	<Display(Name:="項目43")>
	Public Property COL43 As String

	<ByteLength(20)>
	<Display(Name:="項目44")>
	Public Property COL44 As String

	<ByteLength(20)>
	<Display(Name:="項目45")>
	Public Property COL45 As String

	<ByteLength(20)>
	<Display(Name:="項目46")>
	Public Property COL46 As String

	<ByteLength(20)>
	<Display(Name:="項目47")>
	Public Property COL47 As String

	<ByteLength(20)>
	<Display(Name:="項目48")>
	Public Property COL48 As String

	<ByteLength(20)>
	<Display(Name:="項目49")>
	Public Property COL49 As String

	<ByteLength(20)>
	<Display(Name:="項目50")>
	Public Property COL50 As String

	Public Overridable Property D0022 As ICollection(Of D0022)

	<NotMapped>
	Public Property FreeTxtBxList As List(Of String)

	'Public Shared Function ValidateCATCD(ByVal catcd As Short) As ValidationResult
	'	If catcd = 0 Then
	'		Return New ValidationResult("カテゴリーが必要です。")
	'	End If

	'	Return ValidationResult.Success
	'End Function

	Public Shared Function ValidateEachItemAttribute(ByVal d0010 As D0010) As ValidationResult
		If d0010 IsNot Nothing Then
			If d0010.FreeTxtBxList IsNot Nothing Then
				Dim CounterI As Integer = 0
				For Each item In d0010.FreeTxtBxList
					If System.Text.Encoding.GetEncoding("shift-jis").GetByteCount(item) > 20 Then
						Return New ValidationResult("文字数がオーバーしています。", New String() {String.Format("FreeTxtBxList[{0}]", CounterI)})
					End If
					CounterI = CounterI + 1
				Next
			End If
		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function ValidateGyomymd(ByVal d0010 As D0010) As ValidationResult
		If d0010 IsNot Nothing Then
			If d0010.GYOMYMD IsNot Nothing AndAlso d0010.GYOMYMDED IsNot Nothing Then
				If d0010.GYOMYMD > d0010.GYOMYMDED Then
					Return New ValidationResult("業務期間-開始と終了の前後関係が誤っています。", New String() {"GYOMYMD"})
				End If

				If d0010.PATTERN AndAlso d0010.GYOMYMD = d0010.GYOMYMDED Then
					Return New ValidationResult("繰り返し登録の場合、業務期間-開始と終了に同じ日付は指定できません。", New String() {"GYOMYMDED"})
				End If
			End If
		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function ValidateGyomymded(ByVal d0010 As D0010) As ValidationResult
		If d0010 IsNot Nothing Then

            If d0010.PATTERN AndAlso d0010.GYOMYMDED Is Nothing Then
                Return New ValidationResult("繰り返し登録の場合、業務期間-終了が必要です。", New String() {"GYOMYMDED"})
            End If

        End If

		Return ValidationResult.Success
	End Function


	Public Shared Function ValidateKskjkn(ByVal d0010 As D0010) As ValidationResult
		If d0010 IsNot Nothing Then

			Dim dtGYOMYMDED As Date = d0010.GYOMYMD
			Dim strKSKJKNST As String = ChangeToHHMM(d0010.KSKJKNST)
			Dim strKSKJKNED As String = ChangeToHHMM(d0010.KSKJKNED)

			'繰り返し登録無しの場合
			If d0010.PATTERN = False Then
				If d0010.GYOMYMDED IsNot Nothing Then
					dtGYOMYMDED = d0010.GYOMYMDED
				End If
			Else
				'繰り返し登録の場合
				'開始時間 > 終了時間の場合、開始日+1
				If strKSKJKNST > strKSKJKNED Then
					dtGYOMYMDED = Date.Parse(d0010.GYOMYMD).AddDays(1)
				End If
			End If

			'実時間で前後関係チェック
			Dim jtjknst As Date = GetJtjkn(d0010.GYOMYMD, strKSKJKNST)
			Dim jtjkned As Date = GetJtjkn(dtGYOMYMDED, strKSKJKNED)

			If d0010.GYOMYMD <= dtGYOMYMDED AndAlso jtjknst > jtjkned Then
				Return New ValidationResult("拘束時間-開始と終了の前後関係が誤っています。", New String() {"KSKJKNST"})
			End If

            'Sport Category Check Only
            If d0010.SPORTCATCD IsNot Nothing AndAlso d0010.SPORTCATCD <> 0 Then

				'Sport Category Check Only
				'Check If Pattern CheckBox Is True Then Cannot Enter Start Time More Then End Time.
				If d0010.PATTERN AndAlso strKSKJKNST > strKSKJKNED Then
					Return New ValidationResult("繰り返し登録の場合、業務期間-終了が必要です。", New String() {"KSKJKNST"})
				End If

				Dim intKSKJKDiff As Integer = Convert.ToInt16(strKSKJKNED) - Convert.ToInt16(strKSKJKNST)

				'Sport Category Check Only
				'Check If textbox Is then Cannot Input More Then 24
				If d0010.PATTERN AndAlso intKSKJKDiff > 2400 Then
					Return New ValidationResult("繰り返し登録の場合、拘束時間の総時間が24時間以内しか入力できません。", New String() {"KSKJKNST"})
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

		'36:00まで登録可能なので、実時間を２４時間制度に変更する
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
	Public Shared Function ValidateOAjkn(ByVal d0010 As D0010) As ValidationResult
		If d0010 IsNot Nothing Then
			If d0010.OAJKNST IsNot Nothing AndAlso d0010.OAJKNED IsNot Nothing AndAlso d0010.OAJKNST.PadLeft(5, "0") > d0010.OAJKNED.PadLeft(5, "0") Then
				Return New ValidationResult("OA時間-開始と終了の前後関係が誤っています。", New String() {"OAJKNST"})
			End If
		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function ValidateSAIjkn(ByVal d0010 As D0010) As ValidationResult
		If d0010 IsNot Nothing Then
			If d0010.SAIJKNST IsNot Nothing AndAlso d0010.SAIJKNED IsNot Nothing AndAlso d0010.SAIJKNST.PadLeft(5, "0") > d0010.SAIJKNED.PadLeft(5, "0") Then
				Return New ValidationResult("試合時間-開始と終了の前後関係が誤っています。", New String() {"SAIJKNST"})
			End If
		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function ValidatePattern(ByVal d0010 As D0010) As ValidationResult
		If d0010 IsNot Nothing Then

			If d0010.PATTERN Then

				If d0010.MON = False AndAlso d0010.TUE = False AndAlso d0010.WED = False AndAlso d0010.TUR = False AndAlso
								d0010.FRI = False AndAlso d0010.SAT = False AndAlso d0010.SUN = False Then
					Return New ValidationResult("繰り返し登録の場合、曜日指定が必要です。", New String() {"MON"})
				End If

				If d0010.MON OrElse d0010.TUE OrElse d0010.WED OrElse d0010.TUR OrElse d0010.FRI OrElse d0010.SAT OrElse d0010.SUN Then

					If d0010.GYOMYMDED IsNot Nothing AndAlso d0010.GYOMYMD <= d0010.GYOMYMDED Then
						Dim dtYMDFr As Date = d0010.GYOMYMD
						Dim bolExist As Boolean = False

						While dtYMDFr <= d0010.GYOMYMDED
							If d0010.MON = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Monday Then
								bolExist = True
								Exit While
							ElseIf d0010.TUE = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Tuesday Then
								bolExist = True
								Exit While
							ElseIf d0010.WED = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Wednesday Then
								bolExist = True
								Exit While
							ElseIf d0010.TUR = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Thursday Then
								bolExist = True
								Exit While
							ElseIf d0010.FRI = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Friday Then
								bolExist = True
								Exit While
							ElseIf d0010.SAT = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Saturday Then
								bolExist = True
								Exit While
							ElseIf d0010.SUN = True AndAlso dtYMDFr.DayOfWeek = DayOfWeek.Sunday Then
								bolExist = True
								Exit While
							End If

							dtYMDFr = dtYMDFr.AddDays(1)
						End While

						If bolExist = False Then
							Return New ValidationResult("業務期間内に指定の曜日が存在しません。")
						End If
					End If

				End If

				If (d0010.MON AndAlso d0010.TUE) OrElse (d0010.TUE AndAlso d0010.WED) OrElse (d0010.WED AndAlso d0010.TUR) OrElse (d0010.TUR AndAlso d0010.FRI) OrElse
					   (d0010.FRI AndAlso d0010.SAT) OrElse (d0010.SAT AndAlso d0010.SUN) OrElse (d0010.SUN AndAlso d0010.MON) Then

					Dim dtymd As Date = d0010.GYOMYMD
					Dim dtymded As Date = Nothing
					Dim strKSKJKNST As String = ChangeToHHMM(d0010.KSKJKNST)
					Dim strKSKJKNED As String = ChangeToHHMM(d0010.KSKJKNED)

					'開始時間 > 終了時間の場合、開始日+1
					If strKSKJKNST > strKSKJKNED Then
						dtymded = dtymd.AddDays(1)
					Else
						'開始時間 <= 終了時間
						dtymded = dtymd
					End If

					Dim dtJTJKNST As Date = GetJtjkn(dtymd, strKSKJKNST)
					Dim dtJTJKNED As Date = GetJtjkn(dtymded, strKSKJKNED)

					If DateDiff(DateInterval.Minute, dtJTJKNST, dtJTJKNED) > 1440 Then
						Return New ValidationResult("パターン曜日が連続している場合、拘束時間の総時間が24時間以内しか入力できません。", New String() {"KSKJKNST"})
					End If
				End If

			End If

		End If

		Return ValidationResult.Success
	End Function

	Public Shared Function ValidateD0020Ana(ByVal d0010 As D0010) As ValidationResult

		If d0010 IsNot Nothing Then

			If d0010.FMTKBN = 1 OrElse d0010.FMTKBN = 2 Then
				Return ValidationResult.Success
			End If

			If (d0010.D0020 Is Nothing OrElse d0010.D0020.Count = 0) AndAlso (d0010.D0021 Is Nothing OrElse d0010.D0021.Count = 0) AndAlso (d0010.D0022 Is Nothing OrElse d0010.D0022.Count = 0) Then
				Return New ValidationResult("担当のアナウンサーか、又は仮アナカテゴリーを設定してください。")
			End If

			If (d0010.D0020 Is Nothing OrElse d0010.D0020.Count = 0) AndAlso d0010.D0021 IsNot Nothing AndAlso d0010.D0021.Count > 0 Then
				Dim bolExist As Boolean = False
				For Each item In d0010.D0021
					If String.IsNullOrEmpty(item.ANNACATNM) = False Then
						bolExist = True
					End If
				Next
				If bolExist = False Then
					Return New ValidationResult("担当のアナウンサーか、又は仮アナカテゴリーを設定してください。")
				End If
			End If

			'If d0010.D0020 IsNot Nothing AndAlso d0010.D0020.Count > 0 Then

			'	Dim db As New Model1

			'	Dim lstw0010bf = (From t In db.W0010 Where t.ACUSERID = d0010.ACUSERID).ToList
			'	Dim lstw0020bf = (From t In db.W0020 Where t.ACUSERID = d0010.ACUSERID).ToList
			'	Dim lstw0030bf = (From t In db.W0030 Where t.ACUSERID = d0010.ACUSERID).ToList

			'	Dim strGYOMYMDED As String = d0010.GYOMYMD
			'	If d0010.GYOMYMDED IsNot Nothing Then
			'		strGYOMYMDED = d0010.GYOMYMDED
			'	End If

			'	Dim strGyomno As String = ""
			'	If d0010.GYOMNO <> 0 Then
			'		strGyomno = d0010.GYOMNO
			'	End If

			'	ExecProc(db, d0010.ACUSERID, strGyomno, d0010.GYOMYMD, strGYOMYMDED, d0010.KSKJKNST, d0010.KSKJKNED, d0010.PATTERN, d0010.MON, d0010.TUE, d0010.WED, d0010.TUR, d0010.FRI, d0010.SAT, d0010.SUN)

			'	Dim strErrMsg As String = "!!!"

			'	For i As Integer = 0 To d0010.D0020.Count - 1
			'		Dim item = d0010.D0020(i)

			'		'修正の時、追加のユーザーのみチェックする
			'		If String.IsNullOrEmpty(strGyomno) = False AndAlso strGyomno <> "0" Then
			'			Dim d0020 = db.D0020.Find(d0010.GYOMNO, item.USERID)
			'			If d0020 IsNot Nothing Then
			'				Continue For
			'			End If
			'		End If

			'		If lstw0010bf.Count = 0 Then

			'		End If

			'		Dim lstuserw0010bf = (From t In lstw0010bf Where t.USERID = item.USERID).ToList
			'		Dim lstuserw0010aft = (From t In db.W0010 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList

			'		If lstuserw0010bf.Count <> lstuserw0010aft.Count Then
			'			Return New ValidationResult(strErrMsg, New String() {"D0020[" & i & "].USERID"})
			'		End If

			'		For Each W0010bf In lstuserw0010bf
			'			Dim bolFound As Boolean = False
			'			For Each w0010aft In lstuserw0010aft
			'				If W0010bf.YOINID = w0010aft.YOINID Then
			'					bolFound = True
			'					Exit For
			'				End If
			'			Next
			'			If bolFound = False Then
			'				Return New ValidationResult(strErrMsg, New String() {"D0020[" & i & "].USERID"})
			'			End If
			'		Next

			'		Dim lstuserw0020bf = (From t In lstw0020bf Where t.USERID = item.USERID).ToList
			'		Dim lstuserw0020aft = (From t In db.W0020 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList

			'		If lstuserw0020bf.Count <> lstuserw0020aft.Count Then
			'			Return New ValidationResult(strErrMsg, New String() {"D0020[" & i & "].USERID"})
			'		End If

			'		For Each W0020bf In lstuserw0020bf
			'			Dim bolFound As Boolean = False
			'			For Each w0020aft In lstuserw0020aft
			'				If W0020bf.YOINID = w0020aft.YOINID AndAlso W0020bf.KYUKYMD = w0020aft.KYUKYMD AndAlso W0020bf.JKNST = w0020aft.JKNST AndAlso W0020bf.JKNED = w0020aft.JKNED Then
			'					bolFound = True
			'					Exit For
			'				End If
			'			Next
			'			If bolFound = False Then
			'				Return New ValidationResult(strErrMsg, New String() {"D0020[" & i & "].USERID"})
			'			End If
			'		Next

			'		Dim lstuserw0030bf = (From t In lstw0030bf Where t.USERID = item.USERID).ToList
			'		Dim lstuserw0030aft = (From t In db.W0030 Where t.ACUSERID = d0010.ACUSERID And t.USERID = item.USERID).ToList

			'		If lstuserw0030bf.Count <> lstuserw0030aft.Count Then
			'			Return New ValidationResult(strErrMsg, New String() {"D0020[" & i & "].USERID"})
			'		End If

			'		For Each W0030bf In lstuserw0030bf
			'			Dim bolFound As Boolean = False
			'			For Each w0030aft In lstuserw0030aft
			'				If W0030bf.YOINID = w0030aft.YOINID AndAlso W0030bf.GYOMNO = w0030aft.GYOMNO AndAlso
			'					W0030bf.GYOMYMD = w0030aft.GYOMYMD AndAlso W0030bf.GYOMYMDED = w0030aft.GYOMYMDED AndAlso
			'					W0030bf.KSKJKNST = w0030aft.KSKJKNST AndAlso W0030bf.KSKJKNED = w0030aft.KSKJKNED AndAlso
			'					W0030bf.CATCD = w0030aft.CATCD AndAlso W0030bf.BANGUMINM = w0030aft.BANGUMINM AndAlso
			'					W0030bf.OAJKNST = w0030aft.OAJKNST AndAlso W0030bf.OAJKNED = w0030aft.OAJKNED AndAlso
			'					W0030bf.NAIYO = w0030aft.NAIYO AndAlso W0030bf.BASYO = w0030aft.BASYO AndAlso W0030bf.BIKO = w0030aft.BIKO AndAlso
			'					W0030bf.BANGUMITANTO = w0030aft.BANGUMITANTO AndAlso W0030bf.BANGUMIRENRK = w0030aft.BANGUMIRENRK Then

			'					bolFound = True
			'					Exit For
			'				End If
			'			Next
			'			If bolFound = False Then
			'				Return New ValidationResult(strErrMsg, New String() {"D0020[" & i & "].USERID"})
			'			End If
			'		Next

			'	Next

			'End If

		End If

		Return ValidationResult.Success
	End Function

	'Public Shared Function ExecProc(ByVal db As Model1, ByVal acuserid As String, ByVal gyomno As String, ByVal gyomymd As String, ByVal gyomymded As String, ByVal kskjknst As String, ByVal kskjkned As String,
	'				ByVal pattern As String, ByVal mon As String, ByVal tue As String, ByVal wed As String, ByVal tur As String, ByVal fri As String, ByVal sat As String, ByVal sun As String) As Boolean

	'	Dim sqlpara1 As New SqlParameter("asi_acuserid", SqlDbType.SmallInt)
	'	sqlpara1.Value = acuserid

	'	Dim sqlpara2 As New SqlParameter("av_gyomno", SqlDbType.VarChar, 12)
	'	If gyomno = "0" Then
	'		'新規登録の場合
	'		gyomno = ""
	'	End If
	'	sqlpara2.Value = gyomno

	'	Dim sqlpara3 As New SqlParameter("av_gyomymd", SqlDbType.VarChar, 10)
	'	sqlpara3.Value = gyomymd

	'	Dim sqlpara4 As New SqlParameter("av_gyomymded", SqlDbType.VarChar, 10)
	'	sqlpara4.Value = gyomymded

	'	If kskjknst.Contains(":") = False Then
	'		kskjknst = kskjknst.Substring(0, 2) & ":" & kskjknst.Substring(2, 2)
	'	Else
	'		kskjknst = kskjknst.PadLeft(5, "0")
	'	End If

	'	Dim sqlpara5 As New SqlParameter("av_kskjknst", SqlDbType.VarChar, 5)
	'	sqlpara5.Value = kskjknst

	'	If kskjkned.Contains(":") = False Then
	'		kskjkned = kskjkned.Substring(0, 2) & ":" & kskjkned.Substring(2, 2)
	'	Else
	'		kskjkned = kskjkned.PadLeft(5, "0")
	'	End If

	'	Dim sqlpara6 As New SqlParameter("av_kskjkned", SqlDbType.VarChar, 5)
	'	sqlpara6.Value = kskjkned

	'	Dim sqlpara7 As New SqlParameter("ab_pattern", SqlDbType.Bit)
	'	sqlpara7.Value = 0

	'	If pattern = "True" Then
	'		sqlpara7.Value = 1
	'	End If

	'	Dim sqlpara8 As New SqlParameter("ab_mon", SqlDbType.Bit)
	'	sqlpara8.Value = 0

	'	Dim sqlpara9 As New SqlParameter("ab_tue", SqlDbType.Bit)
	'	sqlpara9.Value = 0

	'	Dim sqlpara10 As New SqlParameter("ab_wed", SqlDbType.Bit)
	'	sqlpara10.Value = 0

	'	Dim sqlpara11 As New SqlParameter("ab_tur", SqlDbType.Bit)
	'	sqlpara11.Value = 0

	'	Dim sqlpara12 As New SqlParameter("ab_fri", SqlDbType.Bit)
	'	sqlpara12.Value = 0

	'	Dim sqlpara13 As New SqlParameter("ab_sat", SqlDbType.Bit)
	'	sqlpara13.Value = 0

	'	Dim sqlpara14 As New SqlParameter("ab_sun", SqlDbType.Bit)
	'	sqlpara14.Value = 0

	'	If mon = "True" Then
	'		sqlpara8.Value = 1
	'	End If
	'	If tue = "True" Then
	'		sqlpara9.Value = 1
	'	End If
	'	If wed = "True" Then
	'		sqlpara10.Value = 1
	'	End If
	'	If tur = "True" Then
	'		sqlpara11.Value = 1
	'	End If
	'	If fri = "True" Then
	'		sqlpara12.Value = 1
	'	End If
	'	If sat = "True" Then
	'		sqlpara13.Value = 1
	'	End If
	'	If sun = "True" Then
	'		sqlpara14.Value = 1
	'	End If

	'	Dim cnt = db.Database.ExecuteSqlCommand("EXEC TeLAS.pr_insert_w0010 @asi_acuserid, @av_gyomno, @av_gyomymd, @av_gyomymded, @av_kskjknst, @av_kskjkned, @ab_pattern, @ab_mon, @ab_tue, @ab_wed,@ab_tur,@ab_fri, @ab_sat,@ab_sun",
	'						sqlpara1, sqlpara2, sqlpara3, sqlpara4, sqlpara5, sqlpara6, sqlpara7, sqlpara8, sqlpara9, sqlpara10, sqlpara11, sqlpara12, sqlpara13, sqlpara14)


	'	Return True
	'End Function



End Class

Public Class TimeMaxValueAttribute
	Inherits ValidationAttribute

	Public Property Value As String

	Public Sub TimeMaxValueAttribute()
		Me.Value = "36:00"
	End Sub

	Public Overrides Function IsValid(value As Object) As Boolean
		If value IsNot Nothing Then
			Dim strValue As String = value.ToString.PadLeft(5, "0")
			If strValue > "36:00" Then
				Return False
			End If
		End If

		Return True
	End Function
End Class


Public Class ByteLengthAttribute
	Inherits StringLengthAttribute
	Public Sub New(maximumLength As Integer)
		MyBase.New(maximumLength)
	End Sub

	Public Overrides Function IsValid(value As Object) As Boolean
		If value IsNot Nothing Then
			Dim strValue As String = value.ToString
			Dim intLength As Integer = MyBase.MaximumLength / 2
			If System.Text.Encoding.GetEncoding("shift-jis").GetByteCount(strValue) > MyBase.MaximumLength Then
				'MyBase.ErrorMessage = "{0}は全角" & intLength & "文字まで入力できます。"
				MyBase.ErrorMessage = "文字数がオーバーしています。"
				Return False
			End If
		End If

		Return True
	End Function
End Class

