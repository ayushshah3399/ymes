Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.s0100")>
Partial Public Class s0100
    <Key>
    <Column(Order:=0)>
    <StringLength(30)>
    Public Property appid As String

    <Key>
    <Column(Order:=1)>
    <StringLength(20)>
    Public Property notyp As String

    <Required>
    <StringLength(20)>
    Public Property notypnm As String

    Public Property nosize As Decimal

    <Required>
    <StringLength(1)>
    Public Property headusekbn As String

    <StringLength(14)>
    Public Property koteihead As String

    <Key>
    <Column(Order:=2)>
    <StringLength(1)>
    Public Property yearkbn As String

    <Key>
    <Column(Order:=3)>
    <StringLength(4)>
    Public Property yearno As String

    <StringLength(7)>
    Public Property startym As String

    <Required>
    <StringLength(1)>
    Public Property headsiftkbn As String

    Public Property headsiftval As Decimal?

    <StringLength(4)>
    Public Property maxheadval As String

    <StringLength(4)>
    Public Property minheadval As String

    <StringLength(4)>
    Public Property nexthead As String

    Public Property maxno As Decimal

    Public Property minno As Decimal

    Public Property shiftno As Decimal

    Public Property nextno As Decimal

    <Required>
    <StringLength(1)>
    Public Property loopkbn As String

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
End Class
