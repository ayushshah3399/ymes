namespace KYOSAI_WEB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<S0050> S0050 { get; set; }
        public virtual DbSet<D3010> D3010 { get; set; }
        public virtual DbSet<M0010> M0010 { get; set; }
        public virtual DbSet<M1010> M1010 { get; set; }
        public virtual DbSet<M1050> M1050 { get; set; }
        public virtual DbSet<M5010> M5010 { get; set; }
        public virtual DbSet<MC0030> MC0030 { get; set; }
        public virtual DbSet<D4020> D4020 { get; set; }
        public virtual DbSet<DC0060> DC0060 { get; set; }
        public virtual DbSet<MC0060> MC0060 { get; set; }
        public virtual DbSet<MS080> MS080 { get; set; }
        public virtual DbSet<S0030> S0030 { get; set; }
        public virtual DbSet<S0010> S0010 { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<S0050>()
                .Property(e => e.USERID)
                .IsUnicode(false);

            modelBuilder.Entity<S0050>()
                .Property(e => e.USERNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0050>()
                .Property(e => e.PASS)
                .IsUnicode(false);

            modelBuilder.Entity<S0050>()
                .Property(e => e.LOGINHIST)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0050>()
                .Property(e => e.SUPPCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0050>()
                .Property(e => e.CHARGECD)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.ORDERKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.ORDERNO)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.SUPPCD)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.DELICD)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.HMNO)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.ORDERQTY)
                .HasPrecision(11, 3);

            modelBuilder.Entity<D3010>()
                .Property(e => e.ORDERUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.PRICEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.PRICE)
                .HasPrecision(10, 2);

            modelBuilder.Entity<D3010>()
                .Property(e => e.PRICEUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.AMOUNT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<D3010>()
                .Property(e => e.TAXKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.STATE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.REMARK)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.PRTKBN1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.PRTKBN2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.DECIKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.REPLANKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.SORDERNO)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.EXCEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.RATE)
                .HasPrecision(7, 2);

            modelBuilder.Entity<D3010>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.KYOTENCD)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.JITANCD)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.JORDERNO)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.SZCMT)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.BCCMT)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.SHEETNO)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.ADJREQKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.MORDERNO)
                .IsUnicode(false);

            modelBuilder.Entity<D3010>()
                .Property(e => e.GAITNKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.HMNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.HMNM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.HMRNM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.HMKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.CUSTCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.CUSTHMNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.DRAWNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SUPPCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SECTIONCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.PROCECD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.MCCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.CHARGECD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.STOCKKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.STOCKHMNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.STOCKCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.LOTKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.UNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.WEIGHT)
                .HasPrecision(7, 3);

            modelBuilder.Entity<M0010>()
                .Property(e => e.WEUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.WEDENUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.REMARK)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ARRGKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.KSCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.YIELD)
                .HasPrecision(3, 2);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TPATARN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.OFFSETKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.OFFCUSTCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.USEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.INVOICENM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TAXTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.MASPROKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.CLASSCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.DECIKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.KYOTENCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ZUNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SEHMNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.MEICD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SISAKUKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.CEREALCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SUPSTS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.HINSYUCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.MAKERCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TYPE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.NISUGATA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.EYOKYU)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.HAKKONO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TANA)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.OKURINOTE)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.CHGNOTE)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.EITANCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.PRNKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SEITANCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SITANCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TKZIQTY)
                .HasPrecision(11, 3);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SIZIQTY)
                .HasPrecision(11, 3);

            modelBuilder.Entity<M0010>()
                .Property(e => e.BETUHMCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ZAIKOKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SIYO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ESTNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SERIES)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.PACKNM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TOKUSEI)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.EP)
                .HasPrecision(8, 2);

            modelBuilder.Entity<M0010>()
                .Property(e => e.EPUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.RS)
                .HasPrecision(8, 2);

            modelBuilder.Entity<M0010>()
                .Property(e => e.RSUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.VOL)
                .HasPrecision(8, 2);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ES)
                .HasPrecision(8, 2);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ESUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TR)
                .HasPrecision(8, 2);

            modelBuilder.Entity<M0010>()
                .Property(e => e.TRUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.BUNCD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.KUNICD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.WUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.PMEMO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.DEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SITEICD)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ITEMSIZE)
                .HasPrecision(8, 2);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SEYOKYU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.AZKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SHINNM)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.BUNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ASSYNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.PHASE)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SMTKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.LILLEIETM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.LONGDELIVERY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.QUOCALCOMIT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .Property(e => e.ITEMWEIGHT)
                .HasPrecision(11, 3);

            modelBuilder.Entity<M0010>()
                .Property(e => e.SPECNO)
                .IsUnicode(false);

            modelBuilder.Entity<M0010>()
                .HasMany(e => e.M00101)
                .WithOptional(e => e.M00102)
                .HasForeignKey(e => e.STOCKHMNO);

            modelBuilder.Entity<M0010>()
                .HasMany(e => e.M001011)
                .WithOptional(e => e.M00103)
                .HasForeignKey(e => e.BETUHMCD);

            modelBuilder.Entity<M1010>()
                .Property(e => e.BCCD)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.BCKNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.BCNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.BCRNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ZIP)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ADD1)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ADD2)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ADD3)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.TEL)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.FAX)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.CALTYP)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.REMARK)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.USEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.CURRENCYCD)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.KYOTENNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.HONSYAKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.HONSYACD)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.TEL2)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.BUSYONM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EIYAKU)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EINM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EIMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EITEL)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EIODR)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EIDLV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EIEST)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.EIENV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1YAKU)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1NM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1MAIL)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1TEL)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1ODR)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1DLV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1EST)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS1ENV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2YAKU)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2NM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2MAIL)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2TEL)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2ODR)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2DLV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2EST)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.AS2ENV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ODRFAX)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ODRKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.DELIFAX)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.DELIKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ACCFAX)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.ACCKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.PAYCOND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.PAYCONDNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .Property(e => e.TRANSNAIYO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M1010>()
                .HasMany(e => e.M10101)
                .WithOptional(e => e.M10102)
                .HasForeignKey(e => e.HONSYACD);

            modelBuilder.Entity<M1050>()
                .Property(e => e.DELICD)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.DELINM)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.DELIRNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.BCCD)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.STOCKCD)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.CALTYP)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M1050>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.CHARGECD)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.CHARGENM)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.MEMO)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.RETIREKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.SECTIONCD)
                .IsUnicode(false);

            modelBuilder.Entity<M5010>()
                .Property(e => e.MAILADRS)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.MAKERCD)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.MAKERNM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.MAKERLOGO)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.MAKERIPNM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.SAIYOUNM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0030>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);
            modelBuilder.Entity<D4020>()
                .Property(e => e.DATAKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.ORDERNO)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.SUPPCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.HMNO)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.ACTUALQTY)
                .HasPrecision(11, 3);

            modelBuilder.Entity<D4020>()
                .Property(e => e.ACTUALUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.PRICE)
                .HasPrecision(10, 2);

            modelBuilder.Entity<D4020>()
                .Property(e => e.PRICEUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.AMOUNT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<D4020>()
                .Property(e => e.PRICEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.TAXAMOUNT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<D4020>()
                .Property(e => e.TAXKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.ADDUPKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.APPLY)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.CHECKNO)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.DEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.DEAUACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.CRACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.CRAUACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.DESECTIONCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.CRSECTIONCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.LOTNO)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.MAKERLOTNO)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.STOCKCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.STOCKQTY)
                .HasPrecision(11, 3);

            modelBuilder.Entity<D4020>()
                .Property(e => e.STOCKUNIT)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.PRTKBN1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.REMARK)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.REPSUPPCD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.BADREACD)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.UORDERNO)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.TAXTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.RATE)
                .HasPrecision(7, 2);

            modelBuilder.Entity<D4020>()
                .Property(e => e.REMARK2)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.RSHCHARGE)
                .IsUnicode(false);

            modelBuilder.Entity<D4020>()
                .Property(e => e.RSHQTY)
                .HasPrecision(11, 3);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.ORDERNO)
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.DELISCHEQTY)
                .HasPrecision(11, 3);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.RECEIVEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.UKECHARGECD)
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<DC0060>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.JITANCD)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.JITANNM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.CHARGECD)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<MC0060>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.UNIT)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.UNITNM)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.HASUKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<MS080>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.APPID)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.CALTYP)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.WKKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<S0030>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.APPID)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.COMPCD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.COMPNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.COMPRNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.CALTYP)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.APPNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ZIP)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ADD1)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ADD2)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ADD3)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TEL)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.FAX)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.HOOZINOCX)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.HOOZINOCX64)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.SMTPSERVER)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.MAILUSERID)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.MAILUSERPASS)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.MAILADRS)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ERRMAILADRS)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DOCREFPATH)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.MYNUM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.COMPENGNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ADDENG1)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ADDENG2)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ADDENG3)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TELENG)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.FAXENG)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.LOTSAIBANKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.LOTHIKITEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.RESPPROCECD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.USEPRICEKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.AMOHASUKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ACCLINKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DDEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DDEAUACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DCRACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DCRAUACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DDESECTIONCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DCRSECTIONCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.GDEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.GDEAUACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.GCRACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.GCRAUACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.MPROCECD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.MSECTIONCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.GEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TAXTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.FUEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.FURACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.KOEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.KORACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TEEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TERACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.SOEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.SORACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.GAIDEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ZAIDEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BUDEACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.GAICRACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ZAICRACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BUCRACCCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.CEOSFILE)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.PSWCHKKBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TRADERCD)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.TRADERNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BANKNO)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BANKNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BRANCHNO)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BRANCHNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DEPOSITS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ACCNO)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.INSTID)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.INSTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.INSTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.UPDTID)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.UPDTTERM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.UPDTPRGNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BANKNMZEN)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.BRANCHNMZEN)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.ACCNM)
                .IsUnicode(false);

            modelBuilder.Entity<S0010>()
                .Property(e => e.DESTCD)
                .IsUnicode(false);
        }
    }
}
