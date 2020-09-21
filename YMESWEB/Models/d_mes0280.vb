Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports MES_WEB.My.Resources

<Table("telas.d_mes0280")>
Partial Public Class d_mes0280
    <StringLength(4)>
    Public Property plant_code As String

    <StringLength(15)>
    Public Property work_order_no As String

    <Key>
    <StringLength(12)>
    Public Property prod_individual_id As String

    <DisplayFormat(DataFormatString:="{0:#,###0.###}", ApplyFormatInEditMode:=True)>
    Public Property qty As Decimal

    <StringLength(1)>
    Public Property act_result_status As String

    <StringLength(64)>
    Public Property instid As String

    Public Property instdt As Date

    <StringLength(50)>
    Public Property instterm As String

    <StringLength(50)>
    Public Property instprgnm As String

    <StringLength(64)>
    Public Property updtid As String

    Public Property updtdt As Date

    <StringLength(50)>
    Public Property updtterm As String

    <StringLength(50)>
    Public Property updtprgnm As String

End Class
