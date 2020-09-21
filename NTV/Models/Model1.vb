Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class Model1
	Inherits DbContext

	Public Sub New()
		MyBase.New("name=Model1")
	End Sub

	Public Overridable Property S0010 As DbSet(Of S0010)
	Public Overridable Property D0010 As DbSet(Of D0010)
	Public Overridable Property D0020 As DbSet(Of D0020)
	Public Overridable Property D0021 As DbSet(Of D0021)
	Public Overridable Property D0030 As DbSet(Of D0030)
	Public Overridable Property D0040 As DbSet(Of D0040)
	Public Overridable Property D0050 As DbSet(Of D0050)
	Public Overridable Property D0060 As DbSet(Of D0060)
	Public Overridable Property D0070 As DbSet(Of D0070)
	Public Overridable Property D0080 As DbSet(Of D0080)
	Public Overridable Property D0090 As DbSet(Of D0090)
	Public Overridable Property D0100 As DbSet(Of D0100)
	Public Overridable Property D0101 As DbSet(Of D0101)
	Public Overridable Property D0110 As DbSet(Of D0110)
	Public Overridable Property D0120 As DbSet(Of D0120)
	Public Overridable Property D0130 As DbSet(Of D0130)
	Public Overridable Property D0140 As DbSet(Of D0140)
	Public Overridable Property M0010 As DbSet(Of M0010)
	Public Overridable Property M0020 As DbSet(Of M0020)
	Public Overridable Property M0030 As DbSet(Of M0030)
	Public Overridable Property M0040 As DbSet(Of M0040)
	Public Overridable Property M0050 As DbSet(Of M0050)
	Public Overridable Property M0060 As DbSet(Of M0060)
	Public Overridable Property M0070 As DbSet(Of M0070)
	Public Overridable Property M0080 As DbSet(Of M0080)
	Public Overridable Property M0090 As DbSet(Of M0090)
	Public Overridable Property M0100 As DbSet(Of M0100)
	Public Overridable Property M0110 As DbSet(Of M0110)
	Public Overridable Property M0120 As DbSet(Of M0120)
	Public Overridable Property M0130 As DbSet(Of M0130)
	Public Overridable Property M0140 As DbSet(Of M0140)
	Public Overridable Property M0150 As DbSet(Of M0150)
	Public Overridable Property W0010 As DbSet(Of W0010)
	Public Overridable Property W0020 As DbSet(Of W0020)
	Public Overridable Property W0030 As DbSet(Of W0030)
	Public Overridable Property W0040 As DbSet(Of W0040)
	Public Overridable Property W0050 As DbSet(Of W0050)
	Public Overridable Property W0060 As DbSet(Of W0060)
	Public Overridable Property W0070 As DbSet(Of W0070)
	Public Overridable Property W0080 As DbSet(Of W0080)
	Public Overridable Property W0090 As DbSet(Of W0090)
	Public Overridable Property WD0040 As DbSet(Of WD0040)
	Public Overridable Property WD0050 As DbSet(Of WD0050)
	Public Overridable Property WD0060 As DbSet(Of WD0060)
	Public Overridable Property D0150 As DbSet(Of D0150)
	Public Overridable Property D0022 As DbSet(Of D0022)

	Public Overridable Property M0160 As DbSet(Of M0160)

	Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
		modelBuilder.Entity(Of S0010)() _
		 .Property(Function(e) e.APPNM) _
		 .IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
			.Property(Function(e) e.KOKYUTENKAIPATH) _
			.IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
		.Property(Function(e) e.CCADDRESS) _
		.IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of S0010)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.GYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.OAJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.OAJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.BASYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.BIKO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.PGYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.IKKATUNO) _
			.HasPrecision(9, 0)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.HasMany(Function(e) e.D0020) _
			.WithRequired(Function(e) e.D0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of D0010)() _
			.HasMany(Function(e) e.D0021) _
			.WithRequired(Function(e) e.D0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.SAIJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.SAIJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL01) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL02) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL03) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL04) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL05) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL06) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL07) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL08) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL09) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL10) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL11) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL12) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL13) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL14) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL15) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL16) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL17) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL18) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL19) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL20) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL21) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL22) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL23) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL24) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL25) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL26) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL27) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL28) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL29) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL30) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL31) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL32) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL33) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL34) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL35) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL36) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL37) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL38) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL39) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL40) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL41) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL42) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL43) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL44) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL45) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL46) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL47) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL48) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL49) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0010)() _
			.Property(Function(e) e.COL50) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.GYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.SHIFTMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.COLNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0020)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.GYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.ANNACATNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.COLNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0021)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0030)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0030)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0030)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0030)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0030)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0030)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0030)() _
			.HasMany(Function(e) e.D0040) _
			.WithRequired(Function(e) e.D0030) _
			.HasForeignKey(Function(e) New With {e.USERID, e.NENGETU}) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.JKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.JKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.GYOMMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
		.Property(Function(e) e.BIKO) _
		.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0040)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.GYOMSNSNO) _
			.HasPrecision(11, 0)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.BASYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.GYOMMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0050)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.KYUKSNSCD) _
			.HasPrecision(11, 0)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.JKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.JKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.GYOMMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.KANRIMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0060)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.HENKORRKCD) _
			.HasPrecision(13, 0)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.HENKONAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.OAJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.OAJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.BASYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.BIKO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.TNTNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.GYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.SAIJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.SAIJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL01) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL02) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL03) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL04) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL05) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL06) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL07) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL08) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL09) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL10) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL11) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL12) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL13) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL14) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL15) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL16) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL17) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL18) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL19) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL20) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL21) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL22) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL23) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL24) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL25) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL26) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL27) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL28) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL29) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL30) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL31) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL32) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL33) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL34) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL35) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL36) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL37) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL38) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL39) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL40) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL41) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL42) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL43) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL44) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL45) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL46) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL47) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL48) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL49) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0070)() _
			.Property(Function(e) e.COL50) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.DNGNNO) _
			.HasPrecision(11, 0)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.MESSAGE) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0080)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.HINANO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.HINAMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.OAJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.OAJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.BASYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.BIKO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0090)() _
			.HasMany(Function(e) e.D0100) _
			.WithRequired(Function(e) e.D0090) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of D0100)() _
			.Property(Function(e) e.HINANO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0100)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0100)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0100)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0100)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0100)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0100)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0101)() _
		  .Property(Function(e) e.HINANO) _
		  .HasPrecision(12, 0)

		modelBuilder.Entity(Of D0101)() _
			.Property(Function(e) e.ANNACATNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0101)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0101)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0101)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0101)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0101)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0101)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
		   .Property(Function(e) e.DESKNO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.DESKMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0110)() _
			.HasMany(Function(e) e.D0120) _
			.WithRequired(Function(e) e.D0110) _
			.WillCascadeOnDelete(True)

		modelBuilder.Entity(Of D0110)() _
			.HasMany(Function(e) e.D0130) _
			.WithRequired(Function(e) e.D0110) _
			.WillCascadeOnDelete(True)

		modelBuilder.Entity(Of D0110)() _
		 .Property(Function(e) e.BASYO) _
		 .IsUnicode(False)

		modelBuilder.Entity(Of D0120)() _
			.Property(Function(e) e.DESKNO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0120)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0120)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0120)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0120)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0120)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0120)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0130)() _
			.Property(Function(e) e.DESKNO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0130)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0130)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0130)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0130)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0130)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0130)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0140)() _
			.Property(Function(e) e.USERMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0140)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0140)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0140)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0140)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0140)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0140)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0010)() _
			.Property(Function(e) e.LOGINID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0010)() _
			.Property(Function(e) e.USERPWD) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0010)() _
			.Property(Function(e) e.USERNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0010)() _
			.Property(Function(e) e.MAILLADDESS) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0010)() _
			.Property(Function(e) e.KEITAIADDESS) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.D0020) _
			.WithRequired(Function(e) e.M0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.D0030) _
			.WithRequired(Function(e) e.M0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.D0050) _
			.WithRequired(Function(e) e.M0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.D0060) _
			.WithRequired(Function(e) e.M0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.D0070) _
			.WithRequired(Function(e) e.M0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.D0080) _
			.WithRequired(Function(e) e.M0010) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.D0090) _
			.WithOptional(Function(e) e.M0010) _
			.HasForeignKey(Function(e) e.SIYOUSERID)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.W0010) _
			.WithRequired(Function(e) e.M0010) _
			.HasForeignKey(Function(e) e.ACUSERID) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.W00101) _
			.WithRequired(Function(e) e.M00101) _
			.HasForeignKey(Function(e) e.USERID) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
		.HasMany(Function(e) e.W0050) _
		.WithRequired(Function(e) e.M0010) _
		.HasForeignKey(Function(e) e.ACUSERID) _
		.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0010)() _
			.HasMany(Function(e) e.W00501) _
			.WithRequired(Function(e) e.M00101) _
			.HasForeignKey(Function(e) e.USERID) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0020)() _
			.Property(Function(e) e.CATNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0020)() _
			.HasMany(Function(e) e.D0010) _
			.WithRequired(Function(e) e.M0020) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0020)() _
			.HasMany(Function(e) e.D0050) _
			.WithRequired(Function(e) e.M0020) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0020)() _
			.HasMany(Function(e) e.D0070) _
			.WithRequired(Function(e) e.M0020) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0020)() _
			.HasMany(Function(e) e.D0110) _
			.WithRequired(Function(e) e.M0020) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0030)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0030)() _
			.Property(Function(e) e.BANGUMIKN) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0040)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)


		modelBuilder.Entity(Of M0050)() _
			.Property(Function(e) e.HYOJNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0050)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0050)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0050)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0050)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0050)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0050)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0050)() _
			.HasMany(Function(e) e.M0010) _
			.WithRequired(Function(e) e.M0050) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.KYUKNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.KYUKRYKNM) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.BACKCOLOR) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.WAKUCOLOR) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.FONTCOLOR) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0060)() _
			.HasMany(Function(e) e.D0040) _
			.WithRequired(Function(e) e.M0060) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0060)() _
			.HasMany(Function(e) e.D0060) _
			.WithRequired(Function(e) e.M0060) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0070)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0070)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0070)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0070)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0070)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0070)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0080)() _
			.Property(Function(e) e.ANNACATNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0080)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0080)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0080)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0080)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0080)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0080)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.IKKATUNO) _
			.HasPrecision(9, 0)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.IKKATUMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.OAJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.OAJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.BIKO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0090)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
		   .Property(Function(e) e.KAKUNINNM) _
		   .IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0100)() _
			.HasMany(Function(e) e.D0110) _
			.WithRequired(Function(e) e.M0100) _
			.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of M0110)() _
		   .Property(Function(e) e.IKKATUNO) _
		   .HasPrecision(12, 0)

		modelBuilder.Entity(Of M0110)() _
			.Property(Function(e) e.SHIFTMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0110)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0110)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0110)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0110)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0110)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0110)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.IKKATUNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.ANNACATNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0120)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0130)() _
			.Property(Function(e) e.SPORTCATNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0130)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0130)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0130)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0130)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0130)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0130)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0140)() _
			.Property(Function(e) e.SPORTSUBCATNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0140)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0140)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0140)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0140)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0140)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0140)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BANGUMIHYOJ1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.KSKJKNHYOJ1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.OAJKNHYOJ1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.SAIKNHYOJ1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BASYOHYOJ1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BIKOHYOJ1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BANGUMIHYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.KSKJKNHYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.OAJKNHYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.SAIKNHYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BASYOHYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BIKOHYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BANGUMIHYOJ2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.KSKJKNHYOJ2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.OAJKNHYOJ2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.SAIKNHYOJ2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BASYOHYOJ2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BIKOHYOJ2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BANGUMIHYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.KSKJKNHYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.OAJKNHYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.SAIKNHYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BASYOHYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.BIKOHYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL01) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL02) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL03) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL04) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL05) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL06) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL07) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL08) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL09) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL10) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL11) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL12) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL13) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL14) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL15) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL16) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL17) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL18) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL19) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL20) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL21) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL22) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL23) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL24) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL25) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL01_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL02_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL03_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL04_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL05_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL06_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL07_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL08_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL09_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL10_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL11_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL12_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL13_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL14_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL15_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL16_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL17_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL18_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL19_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL20_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL21_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL22_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL23_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL24_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL25_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL01_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL02_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL03_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL04_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL05_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL06_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL07_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL08_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL09_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL10_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL11_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL12_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL13_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL14_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL15_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL16_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL17_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL18_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL19_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL20_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL21_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL22_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL23_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL24_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL25_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL01_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL02_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL03_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL04_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL05_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL06_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL07_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL08_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL09_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL10_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL11_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL12_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL13_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL14_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL15_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL16_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL17_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL18_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL19_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL20_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL21_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL22_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL23_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL24_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL25_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.FREEZE_LSTCOLNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL26) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL27) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL28) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL29) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL30) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL31) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL32) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL33) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL34) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL35) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL36) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL37) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL38) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL39) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL40) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL41) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL42) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL43) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL44) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL45) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL46) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL47) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL48) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL49) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL50) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL26_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL27_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL28_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL29_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL30_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL31_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL32_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL33_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL34_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL35_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL36_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL37_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL38_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL39_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL40_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL41_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL42_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL43_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL44_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL45_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL46_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL47_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL48_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL49_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL50_TYPE) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL26_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL27_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL28_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL29_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL30_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL31_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL32_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL33_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL34_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL35_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL36_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL37_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL38_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL39_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL40_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL41_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL42_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL43_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL44_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL45_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL46_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL47_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL48_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL49_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL50_HYOJNM1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL26_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL27_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL28_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL29_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL30_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL31_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL32_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL33_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL34_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL35_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL36_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL37_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL38_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL39_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL40_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL41_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL42_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL43_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL44_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL45_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL46_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL47_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL48_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL49_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.COL50_HYOJNM2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0150)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0010)() _
			.Property(Function(e) e.YOINYMD) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0010)() _
			.Property(Function(e) e.YOINGYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of W0010)() _
		   .HasMany(Function(e) e.W0020) _
		   .WithRequired(Function(e) e.W0010) _
		   .HasForeignKey(Function(e) New With {e.ACUSERID, e.YOINID, e.USERID})

		modelBuilder.Entity(Of W0010)() _
			.HasMany(Function(e) e.W0030) _
			.WithRequired(Function(e) e.W0010) _
			.HasForeignKey(Function(e) New With {e.ACUSERID, e.YOINID, e.USERID})

		modelBuilder.Entity(Of W0020)() _
			.Property(Function(e) e.JKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0020)() _
			.Property(Function(e) e.JKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
				   .Property(Function(e) e.GYOMNO) _
				   .HasPrecision(12, 0)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.OAJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.OAJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.BASYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.BIKO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.PGYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of W0030)() _
			.Property(Function(e) e.IKKATUNO) _
			.HasPrecision(9, 0)

		modelBuilder.Entity(Of W0040)() _
						  .Property(Function(e) e.GYOMNO) _
						  .HasPrecision(12, 0)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.OAJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.OAJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.BASYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.BIKO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0040)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		'modelBuilder.Entity(Of W0040)() _
		'	.HasMany(Function(e) e.W0050) _
		'	.WithRequired(Function(e) e.W0040) _
		'	.HasForeignKey(Function(e) New With {e.ACUSERID, e.SHORIKBN, e.GYOMNO})

		modelBuilder.Entity(Of W0050)() _
			.Property(Function(e) e.GYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of W0050)() _
			.Property(Function(e) e.SHIFTMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
	.Property(Function(e) e.GYOMNO) _
	.HasPrecision(12, 0)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.KSKJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.KSKJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.BANGUMINM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.OAJKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.OAJKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.NAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.BASYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.BIKO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.BANGUMITANTO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.BANGUMIRENRK) _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.PGYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of W0070)() _
			.Property(Function(e) e.IKKATUNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of W0080)() _
			.Property(Function(e) e.JKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0080)() _
			.Property(Function(e) e.JKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of W0090)() _
			.Property(Function(e) e.GYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI3) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI4) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI5) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI6) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI7) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI8) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI9) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI10) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI11) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI12) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI13) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI14) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI15) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI16) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI17) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI18) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI19) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI20) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI21) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI22) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI23) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI24) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI25) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI26) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI27) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI28) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI29) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI30) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0040)() _
			.Property(Function(e) e.HI31) _
			.IsUnicode(False)


		modelBuilder.Entity(Of WD0050)() _
			 .Property(Function(e) e.USERNM) _
			 .IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI1) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI2) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI3) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI4) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI5) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI6) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI7) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI8) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI9) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI10) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI11) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI12) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI13) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI14) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI15) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI16) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI17) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI18) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI19) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI20) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI21) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI22) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI23) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI24) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI25) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI26) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI27) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI28) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI29) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI30) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.HI31) _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI3) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI4) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI5) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI6) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI7) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI8) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI9) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI10) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI11) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI12) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI13) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI14) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI15) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI16) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI17) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI18) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI19) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI20) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI21) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI22) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI23) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI24) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI25) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI26) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI27) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI28) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI29) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI30) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.BACKCOLORHI31) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI3) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI4) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI5) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI6) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI7) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI8) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI9) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI10) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI11) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI12) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI13) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI14) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI15) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI16) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI17) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI18) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI19) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI20) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI21) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI22) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI23) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI24) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI25) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI26) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI27) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI28) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI29) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI30) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.WAKUCOLORHI31) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI1) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI2) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI3) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI4) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI5) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI6) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI7) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI8) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI9) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI10) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI11) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI12) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI13) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI14) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI15) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI16) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI17) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI18) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI19) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI20) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI21) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI22) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI23) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI24) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI25) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI26) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI27) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI28) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI29) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI30) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
			.Property(Function(e) e.FONTCOLORHI31) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0050)() _
		 .Property(Function(e) e.TENKAIFONTCOLOR) _
		 .IsFixedLength() _
		 .IsUnicode(False)

		modelBuilder.Entity(Of WD0060)() _
			.Property(Function(e) e.JKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of WD0060)() _
			.Property(Function(e) e.JKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
		   .Property(Function(e) e.HENKORRKCD) _
		   .HasPrecision(13, 0)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.HENKONAIYO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.JKNST) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.JKNED) _
			.IsFixedLength() _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.SHINSEIUSER) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.KYUKNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.GYOMMEMO) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0150)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.GYOMNO) _
			.HasPrecision(12, 0)

		modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.COLNM) _
			.IsUnicode(False)

        'modelBuilder.Entity(Of D0022)() _
        '	.Property(Function(e) e.FIX_GYOMNO) _
        '	.HasPrecision(12, 0)

        modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of D0022)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0160)() _
			.Property(Function(e) e.INSTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0160)() _
			.Property(Function(e) e.INSTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0160)() _
			.Property(Function(e) e.INSTPRGNM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0160)() _
			.Property(Function(e) e.UPDTID) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0160)() _
			.Property(Function(e) e.UPDTTERM) _
			.IsUnicode(False)

		modelBuilder.Entity(Of M0160)() _
			.Property(Function(e) e.UPDTPRGNM) _
			.IsUnicode(False)
	End Sub

	Public Property C0020 As System.Data.Entity.DbSet(Of C0040)

	Public Property M_C0050 As System.Data.Entity.DbSet(Of M_C0050)

	Public Property M_UserList As System.Data.Entity.DbSet(Of M_UserList)
End Class
