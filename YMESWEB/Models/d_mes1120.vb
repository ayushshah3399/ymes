Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.d_mes1120")>
Partial Public Class d_mes1120
    <StringLength(4)>
    Public Property plant_code As String

    <Key>
    <Column(Order:=0, TypeName:="date")>
    Public Property stocktake_date As Date

    <Required>
    Public Property st_bom_valid_date As Date

    <Required>
    <StringLength(1)>
    Public Property stmat_stock_updt_type As String

    <Required>
    <StringLength(1)>
    Public Property stpro_stock_updt_type As String

    <Required>
    <StringLength(1)>
    Public Property stfgd_stock_updt_type As String

    <Required>
    <StringLength(1)>
    Public Property st_complete_flg As String

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
