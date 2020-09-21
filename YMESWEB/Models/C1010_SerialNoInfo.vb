Imports System.ComponentModel.DataAnnotations

Public Class C1010_SerialNoInfo
    Public Property number As String
    Public Property serial_no As String

    <DisplayFormat(DataFormatString:="HH:MM", ApplyFormatInEditMode:=True)>
    Public Property register_time As String

End Class
