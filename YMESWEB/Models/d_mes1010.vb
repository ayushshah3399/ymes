Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes1010")>
Partial Public Class d_mes1010
    <Required>
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <Column(TypeName:="date")>
    Public Property stocktake_date As Date

    <StringLength(40)>
    Public Property memo As String

    <Required>
    <StringLength(1)>
    Public Property stocktake_status As String

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
