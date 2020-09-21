Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("telas.m_proc0040")>
Partial Public Class m_proc0040
	<Required>
	<StringLength(4)>
	Public Property plant_code As String

	<Key>
	<StringLength(6)>
    Public Property shelfgrp_code As String

    <StringLength(40)>
    Public Property shelfgrp_name As String

    <Required>
    <StringLength(4)>
    Public Property location_code As String

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
