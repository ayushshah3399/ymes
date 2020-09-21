Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes0030")>
Partial Public Class d_mes0030
    <Required>
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <StringLength(16)>
    Public Property label_no As String

    <Required>
    <StringLength(12)>
    Public Property work_result_no As String

    <Required>
    <StringLength(18)>
    Public Property item_code As String

    Public Property qty As Decimal

    Public Property print_datetime As Date?

    <Required>
    <StringLength(1)>
    Public Property label_cur_status As String

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
