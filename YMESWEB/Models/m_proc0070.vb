Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.m_proc0070")>
Partial Public Class m_proc0070
    '<Required>
    <StringLength(4)>
    Public Property plant_code As String

    <StringLength(5)>
    Public Property scm_type As String

    <Key>
    <StringLength(10)>
    Public Property man_stat_cd As String

    <StringLength(40)>
    Public Property man_stat_name As String

    <StringLength(5)>
    Public Property workctr_code As String

    <Range(0, 9999, ErrorMessage:="please Enter Less")>
    Public Property seq As Decimal

    <StringLength(10)>
    Public Property line_code As String

    <StringLength(1)>
    Public Property point_type As String

    <StringLength(1)>
    Public Property sap_if_type As String

    <StringLength(1)>
    Public Property ser_set_type As String

    '<Required>
    <StringLength(1)>
    Public Property ser_lab_prt_type As String

    <StringLength(20)>
    Public Property ser_no As String

    <DisplayFormat(DataFormatString:="{0:#,##0}", ApplyFormatInEditMode:=True)>
    <Range(0, 99, ErrorMessage:="please Enter Less")>
    Public Property exser_num_from As String

    <DisplayFormat(DataFormatString:="{0:#,##0}", ApplyFormatInEditMode:=True)>
    <Range(0, 99, ErrorMessage:="please Enter Less")>
    Public Property exser_num_to As String

    <StringLength(1)>
    Public Property work_result_type As String

    <StringLength(1)>
    Public Property work_result_tran_type As String

    <NotMapped>
    <StringLength(25)>
    <Display(Name:="A1010_21_StaticHeaderText", ResourceType:=GetType(My.Resources.LangResources))>
    Public Property header_text As String

    <NotMapped>
    Public Property BolDirectGotoWO As String

    <NotMapped>
    Public Property item_code As String

    <NotMapped>
    Public Property item_name As String

    <NotMapped>
    Public Property serial_no As String

    <NotMapped>
    Public Overridable Property obj_C1010_SerialNoInfo As ICollection(Of C1010_SerialNoInfo)

    <NotMapped>
    Public Property man_stat_cd_List() As List(Of String)

    <StringLength(64)>
    Public Property instid As String

    Public Property instdt As Date

    '<Required>
    <StringLength(50)>
    Public Property instterm As String

    '<Required>
    <StringLength(50)>
    Public Property instprgnm As String

    '<Required>
    <StringLength(64)>
    Public Property updtid As String

    Public Property updtdt As Date

    '<Required>
    <StringLength(50)>
    Public Property updtterm As String

    '<Required>
    <StringLength(50)>
    Public Property updtprgnm As String
End Class
