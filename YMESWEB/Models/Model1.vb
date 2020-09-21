Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq
Imports Npgsql
Imports NpgsqlTypes

Partial Public Class Model1
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=Model1")
    End Sub

    Public Overridable Property d_mes0030 As DbSet(Of d_mes0030)
    Public Overridable Property d_mes0040 As DbSet(Of d_mes0040)
    Public Overridable Property d_mes0050 As DbSet(Of d_mes0050)
    Public Overridable Property d_mes0060 As DbSet(Of d_mes0060)
    Public Overridable Property d_mes0070 As DbSet(Of d_mes0070)
    Public Overridable Property d_mes0100 As DbSet(Of d_mes0100)
    Public Overridable Property d_mes0110 As DbSet(Of d_mes0110)
    Public Overridable Property d_mes0120 As DbSet(Of d_mes0120)
    Public Overridable Property d_mes0150 As DbSet(Of d_mes0150)
    Public Overridable Property d_mes0190 As DbSet(Of d_mes0190)
    Public Overridable Property d_mes0250 As DbSet(Of d_mes0250)
    Public Overridable Property d_mes0280 As DbSet(Of d_mes0280)
    Public Overridable Property d_mes0290 As DbSet(Of d_mes0290)
    Public Overridable Property d_mes1010 As DbSet(Of d_mes1010)
    Public Overridable Property d_mes1020 As DbSet(Of d_mes1020)
    Public Overridable Property d_mes1120 As DbSet(Of d_mes1120)
    Public Overridable Property d_mes1130 As DbSet(Of d_mes1130)
	Public Overridable Property d_sap0050 As DbSet(Of d_sap0050)
    Public Overridable Property m_item0010 As DbSet(Of m_item0010)
    Public Overridable Property m_proc0020 As DbSet(Of m_proc0020)
    Public Overridable Property m_proc0040 As DbSet(Of m_proc0040)
    Public Overridable Property m_proc0050 As DbSet(Of m_proc0050)
    Public Overridable Property m_proc0070 As DbSet(Of m_proc0070)
    Public Overridable Property m_supp0010 As DbSet(Of m_supp0010)
    Public Overridable Property s0010 As DbSet(Of s0010)
    Public Overridable Property s0020 As DbSet(Of s0020)
    Public Overridable Property s0030 As DbSet(Of s0030)
    Public Overridable Property s0040 As DbSet(Of s0040)
    Public Overridable Property s0050 As DbSet(Of s0050)
    Public Overridable Property s0060 As DbSet(Of s0060)
    Public Overridable Property s0070 As DbSet(Of s0070)
    Public Overridable Property s0080 As DbSet(Of s0080)
    Public Overridable Property s0090 As DbSet(Of s0090)
    Public Overridable Property s0100 As DbSet(Of s0100)
    Public Overridable Property s0110 As DbSet(Of s0110)
    Public Overridable Property s0120 As DbSet(Of s0120)
    Public Overridable Property s0130 As DbSet(Of s0130)
    Public Overridable Property s0140 As DbSet(Of s0140)
    Public Overridable Property s0210 As DbSet(Of s0210)
    Public Overridable Property s0220 As DbSet(Of s0220)
    Public Overridable Property s0230 As DbSet(Of s0230)
    Public Overridable Property s0240 As DbSet(Of s0240)
    Public Overridable Property s0410 As DbSet(Of s0410)
    Public Overridable Property s0420 As DbSet(Of s0420)
    Public Overridable Property sy010 As DbSet(Of sy010)
    Public Overridable Property sy020 As DbSet(Of sy020)
    Public Overridable Property sy030 As DbSet(Of sy030)
    Public Overridable Property sy040 As DbSet(Of sy040)
    Public Overridable Property d_work0010 As DbSet(Of d_work0010)
    Public Overridable Property d_work0020 As DbSet(Of d_work0020)
    Public Overridable Property d_work0030 As DbSet(Of d_work0030)
    Public Overridable Property m_proc0060 As DbSet(Of m_proc0060)
    Public Overridable Property m_item0020 As DbSet(Of m_item0020)
    Public Overridable Property m_proc0030 As DbSet(Of m_proc0030)
    Public Overridable Property sy050 As DbSet(Of sy050)
    Public Overridable Property sy060 As DbSet(Of sy060)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of d_mes0030)() _
            .Property(Function(e) e.qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0030)() _
            .Property(Function(e) e.label_cur_status) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.stock_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.inspect_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.keep_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.prev_inspect_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.ok_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.ng_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.transfer_flg) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0040)() _
            .Property(Function(e) e.delete_flg) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0050)() _
            .Property(Function(e) e.inspect_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0050)() _
            .Property(Function(e) e.prev_inspect_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0050)() _
            .Property(Function(e) e.inspected_flg) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0050)() _
            .Property(Function(e) e.delete_flg) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0150)() _
            .Property(Function(e) e.po_receive_seq) _
            .HasPrecision(2, 0)

        modelBuilder.Entity(Of d_mes0150)() _
            .Property(Function(e) e.receive_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0150)() _
            .Property(Function(e) e.stock_receive_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0150)() _
            .Property(Function(e) e.sap_flag) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0150)() _
            .Property(Function(e) e.delete_flg) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_sap0050)() _
            .Property(Function(e) e.odr_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_sap0050)() _
            .Property(Function(e) e.billed_ord_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_sap0050)() _
            .Property(Function(e) e.stock_odr_qty) _
            .HasPrecision(13, 3)

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.net_price) _
        '	.HasPrecision(7, 0)

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.price_unit) _
        '	.HasPrecision(6, 0)

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.ekko_delete_flag) _
        '	.IsFixedLength()

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.ekko_detail_delete_flag) _
        '	.IsFixedLength()

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.comp_type) _
        '	.IsFixedLength()

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.over_delivery_type) _
        '	.IsFixedLength()

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.max_delivery) _
        '	.HasPrecision(3, 0)

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.min_delivery) _
        '	.HasPrecision(3, 0)

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.drct_idrct_type) _
        '	.IsFixedLength()

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.import_type) _
        '	.IsFixedLength()

        'modelBuilder.Entity(Of d_sap0050)() _
        '	.Property(Function(e) e.short_deli_type) _
        '	.IsFixedLength()

        modelBuilder.Entity(Of d_sap0050)() _
            .Property(Function(e) e.del_flag) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_sap0050)() _
            .Property(Function(e) e.appd_flag) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_sap0050)() _
            .Property(Function(e) e.stock_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of m_proc0050)() _
            .Property(Function(e) e.shelf_seq) _
            .HasPrecision(6, 0)

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.compcd) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.smtpportno) _
            .HasPrecision(5, 0)

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.lotsaibankbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.lothikitekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.pswchkkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.pswvalday) _
            .HasPrecision(4, 0)

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.pswvalmsgday) _
            .HasPrecision(4, 0)

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.usepricekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.amohasukbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0010)() _
            .Property(Function(e) e.acclinkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0020)() _
            .Property(Function(e) e.aplankbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0030)() _
            .Property(Function(e) e.wkkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0040)() _
            .Property(Function(e) e.authkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0040)() _
            .Property(Function(e) e.pswchkkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0050)() _
            .Property(Function(e) e.loginhist) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0050)() _
            .HasMany(Function(e) e.s0060) _
            .WithRequired(Function(e) e.s0050) _
            .WillCascadeOnDelete(False)

        modelBuilder.Entity(Of s0060)() _
            .Property(Function(e) e.appusekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0070)() _
            .Property(Function(e) e.usekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0070)() _
            .Property(Function(e) e.editkbn1) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0070)() _
            .Property(Function(e) e.editkbn2) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0070)() _
            .Property(Function(e) e.editkbn3) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0070)() _
            .Property(Function(e) e.editkbn4) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0080)() _
            .Property(Function(e) e.usekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0080)() _
            .Property(Function(e) e.editkbn1) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0080)() _
            .Property(Function(e) e.editkbn2) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0080)() _
            .Property(Function(e) e.editkbn3) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0080)() _
            .Property(Function(e) e.editkbn4) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0090)() _
            .Property(Function(e) e.prnkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.nosize) _
            .HasPrecision(2, 0)

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.headusekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.yearkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.headsiftkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.headsiftval) _
            .HasPrecision(1, 0)

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.maxno) _
            .HasPrecision(15, 0)

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.minno) _
            .HasPrecision(15, 0)

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.shiftno) _
            .HasPrecision(15, 0)

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.nextno) _
            .HasPrecision(15, 0)

        modelBuilder.Entity(Of s0100)() _
            .Property(Function(e) e.loopkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0120)() _
            .Property(Function(e) e.usekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0130)() _
            .Property(Function(e) e.usekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0140)() _
            .Property(Function(e) e.dateseparator) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0140)() _
            .Property(Function(e) e.showorder) _
            .HasPrecision(2, 0)

        modelBuilder.Entity(Of s0220)() _
            .Property(Function(e) e.state) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0230)() _
            .Property(Function(e) e.seq) _
            .HasPrecision(3, 0)

        modelBuilder.Entity(Of s0230)() _
            .Property(Function(e) e.state) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0240)() _
            .Property(Function(e) e.rowno) _
            .HasPrecision(8, 0)

        modelBuilder.Entity(Of s0240)() _
            .Property(Function(e) e.errkbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of s0240)() _
            .Property(Function(e) e.errid) _
            .HasPrecision(6, 0)

        modelBuilder.Entity(Of s0410)() _
            .Property(Function(e) e.sessionid) _
            .HasPrecision(19, 4)

        modelBuilder.Entity(Of s0420)() _
            .Property(Function(e) e.sessionid) _
            .HasPrecision(19, 4)

        modelBuilder.Entity(Of s0420)() _
            .Property(Function(e) e.windowseq) _
            .HasPrecision(19, 4)

        modelBuilder.Entity(Of s0420)() _
            .Property(Function(e) e.state) _
            .IsFixedLength()

        modelBuilder.Entity(Of sy030)() _
            .Property(Function(e) e.usekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of sy040)() _
            .Property(Function(e) e.usekbn) _
            .IsFixedLength()

        modelBuilder.Entity(Of m_item0020)() _
            .Property(Function(e) e.trace_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of m_item0020)() _
            .Property(Function(e) e.pack_in_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_work0010)() _
            .Property(Function(e) e.label_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_work0010)() _
            .Property(Function(e) e.trace_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_work0010)() _
            .Property(Function(e) e.stock_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_work0020)() _
            .Property(Function(e) e.label_qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0060)() _
            .Property(Function(e) e.pic_input_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0060)() _
            .Property(Function(e) e.pic_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0060)() _
            .Property(Function(e) e.pic_status) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0100)() _
            .Property(Function(e) e.label_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0100)() _
            .Property(Function(e) e.qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0100)() _
            .Property(Function(e) e.del_flag) _
            .IsFixedLength()

        modelBuilder.Entity(Of m_proc0030)() _
            .Property(Function(e) e.location_type) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0070)() _
            .Property(Function(e) e.qty) _
            .HasPrecision(13, 3)

        modelBuilder.Entity(Of d_mes0070)() _
            .Property(Function(e) e.pic_with_status) _
            .IsFixedLength()

        modelBuilder.Entity(Of d_mes0070)() _
            .Property(Function(e) e.payout_qty) _
            .HasPrecision(13, 3)

    End Sub
    ''' <summary>
    ''' For Encreption Of Password
    ''' </summary>
    ''' <param name="Password"></param>
    ''' <returns></returns>
    Public Function Encryptsyspass(ByVal Password As String)

        Dim strSQL As String = "SELECT telas.fn_sys_desencrypt('ACTY1234','" & Password & "')"
        Dim PgSqlConn As NpgsqlConnection = New NpgsqlConnection(My.Settings.Item("ConnectionString"))
        Dim PgSqlComm As New NpgsqlCommand

        PgSqlConn.Open()

        With PgSqlComm
            .Connection = PgSqlConn
            .CommandType = CommandType.Text
            .CommandText = strSQL
            .Parameters.Clear()
            .ExecuteNonQuery()
        End With

        Dim PgSqlreader = PgSqlComm.ExecuteScalar

        PgSqlConn.Close()

        Return PgSqlreader

    End Function

    ''' <summary>
    ''' �w�肳�ꂽNOTYP�̍̔Ԓl��擾����
    ''' </summary>
    Public Function GetNewNo(ByVal PgSqlConn As NpgsqlConnection, ByVal strNOTYP As String) As String

        Dim strRet As String = ""

        Try

            Dim strSQL As String = "SELECT TELAS.pr_sys_getnextno('TeLAS','" & strNOTYP & "')"
            'Dim PgSqlConn As NpgsqlConnection = New NpgsqlConnection(My.Settings.Item("ConnectionString"))
            Dim PgSqlComm As New NpgsqlCommand
            'PgSqlConn.Open()

            With PgSqlComm
                .Connection = PgSqlConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
                .Parameters.Clear()
            End With

            Dim PgSqlreader = PgSqlComm.ExecuteScalar

            'PgSqlConn.Close()

            strRet = PgSqlreader

        Catch ex As Exception
            Throw ex
        End Try

        Return strRet

    End Function

    ''' <summary>
    ''' Closing Date
    ''' </summary>
    Public Function fn_check_closing_date(ByVal Jissekki_Date As String, ByVal Format As String) As String

        Dim strSQL As String = "SELECT TELAS.fn_check_closing_date('TeLAS','1', to_date('" & Jissekki_Date & "','" & Format & "'))"
        Dim PgSqlConn As NpgsqlConnection = New NpgsqlConnection(My.Settings.Item("ConnectionString"))
        Dim PgSqlComm As New NpgsqlCommand

        PgSqlConn.Open()

        With PgSqlComm
            .Connection = PgSqlConn
            .CommandType = CommandType.Text
            .CommandText = strSQL
            .Parameters.Clear()
            .ExecuteNonQuery()
        End With

        Dim PgSqlreader = PgSqlComm.ExecuteScalar

        PgSqlConn.Close()

        Return PgSqlreader

    End Function

    ''' <summary>
    ''' To Get Header Text
    ''' </summary>
    ''' <param name="AV_HEADER_TEXT"></param>
    ''' <param name="AV_COMP_CODE"></param>
    ''' <param name="AV_APPID"></param>
    ''' <returns></returns>
    Public Function FN_A1010_GETHEADER(ByVal AV_HEADER_TEXT As String, ByVal AV_COMP_CODE As String, ByVal AV_APPID As String)

        Dim strSQL As String = "SELECT telas.FN_A1010_GETHEADER('" & AV_HEADER_TEXT & "','" & AV_COMP_CODE & "','" & AV_APPID & "')"
        Dim PgSqlConn As NpgsqlConnection = New NpgsqlConnection(My.Settings.Item("ConnectionString"))
        Dim PgSqlComm As New NpgsqlCommand

        PgSqlConn.Open()

        With PgSqlComm
            .Connection = PgSqlConn
            .CommandType = CommandType.Text
            .CommandText = strSQL
            .Parameters.Clear()
            .ExecuteNonQuery()
        End With

        Dim PgSqlreader = PgSqlComm.ExecuteScalar

        PgSqlConn.Close()

        Return PgSqlreader

    End Function

End Class
