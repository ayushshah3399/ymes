Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0010")>
Partial Public Class s0010
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    <StringLength(1)>
    Public Property compcd As String

    <Required>
    <StringLength(40)>
    Public Property compnm As String

    <Required>
    <StringLength(20)>
    Public Property comprnm As String

    <StringLength(6)>
    Public Property caltyp As String

    <StringLength(30)>
    Public Property appnm As String

    <StringLength(100)>
    Public Property hoozinocx As String

    <StringLength(100)>
    Public Property hoozinocx64 As String

    <StringLength(60)>
    Public Property smtpserver As String

    Public Property smtpportno As Decimal?

    <StringLength(60)>
    Public Property mailuserid As String

    <StringLength(20)>
    Public Property mailuserpass As String

    <StringLength(100)>
    Public Property errmailadrs As String

    <StringLength(10)>
    Public Property zip As String

    <StringLength(30)>
    Public Property add1 As String

    <StringLength(30)>
    Public Property add2 As String

    <StringLength(30)>
    Public Property add3 As String

    <StringLength(20)>
    Public Property tel As String

    <StringLength(20)>
    Public Property fax As String

    <StringLength(3)>
    Public Property ddeacccd As String

    <StringLength(3)>
    Public Property ddeauacccd As String

    <StringLength(3)>
    Public Property dcracccd As String

    <StringLength(3)>
    Public Property dcrauacccd As String

    <StringLength(255)>
    Public Property ceosfile As String

    <StringLength(5)>
    Public Property mprocecd As String

    <StringLength(2)>
    Public Property msectioncd As String

    <StringLength(3)>
    Public Property geacccd As String

    <StringLength(3)>
    Public Property teacccd As String

    <Required>
    <StringLength(64)>
    Public Property instid As String

    Public Property instdt As Date

    <Required>
    <StringLength(50)>
    Public Property instterm As String

    <Required>
    <StringLength(50)>
    Public Property instprgnm As String

    <Required>
    <StringLength(64)>
    Public Property updtid As String

    Public Property updtdt As Date

    <Required>
    <StringLength(50)>
    Public Property updtterm As String

    <Required>
    <StringLength(50)>
    Public Property updtprgnm As String

    <StringLength(40)>
    Public Property compengnm As String

    <StringLength(30)>
    Public Property addeng1 As String

    <StringLength(30)>
    Public Property addeng2 As String

    <StringLength(30)>
    Public Property addeng3 As String

    <StringLength(20)>
    Public Property teleng As String

    <StringLength(20)>
    Public Property faxeng As String

    <StringLength(13)>
    Public Property mynum As String

    <StringLength(100)>
    Public Property aplanexepath As String

    <Required>
    <StringLength(1)>
    Public Property lotsaibankbn As String

    <Required>
    <StringLength(1)>
    Public Property lothikitekbn As String

    <StringLength(100)>
    Public Property mailadrs As String

    <StringLength(3)>
    Public Property taxtype As String

    <StringLength(5)>
    Public Property respprocecd As String

    <Required>
    <StringLength(1)>
    Public Property pswchkkbn As String

    Public Property pswvalday As Decimal?

    Public Property pswvalmsgday As Decimal?

    <Required>
    <StringLength(1)>
    Public Property usepricekbn As String

    <StringLength(3)>
    Public Property gdeacccd As String

    <StringLength(3)>
    Public Property gdeauacccd As String

    <StringLength(3)>
    Public Property gcracccd As String

    <StringLength(3)>
    Public Property gcrauacccd As String

    <StringLength(2)>
    Public Property ddesectioncd As String

    <StringLength(2)>
    Public Property dcrsectioncd As String

    <StringLength(3)>
    Public Property fueacccd As String

    <StringLength(3)>
    Public Property furacccd As String

    <StringLength(3)>
    Public Property koeacccd As String

    <StringLength(3)>
    Public Property koracccd As String

    <StringLength(3)>
    Public Property teeacccd As String

    <StringLength(3)>
    Public Property teracccd As String

    <StringLength(3)>
    Public Property soeacccd As String

    <StringLength(3)>
    Public Property soracccd As String

    <Required>
    <StringLength(1)>
    Public Property amohasukbn As String

    <StringLength(100)>
    Public Property docrefpath As String

    <Required>
    <StringLength(1)>
    Public Property acclinkbn As String

    <StringLength(10)>
    Public Property webappnm As String

	<StringLength(10)>
	Public Property reportdatefmt As String

	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Required>
	<StringLength(1)>
	Public Property label_reprint_type As String

	<StringLength(40)>
	Public Property po_header_input_type As String

	<StringLength(40)>
	Public Property header_qr_type As String

End Class
