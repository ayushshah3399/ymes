namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.S0010")]
    public partial class S0010
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string APPID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string COMPCD { get; set; }

        [Required]
        [StringLength(40)]
        public string COMPNM { get; set; }

        [Required]
        [StringLength(20)]
        public string COMPRNM { get; set; }

        [StringLength(6)]
        public string CALTYP { get; set; }

        [StringLength(30)]
        public string APPNM { get; set; }

        [StringLength(10)]
        public string ZIP { get; set; }

        [StringLength(30)]
        public string ADD1 { get; set; }

        [StringLength(30)]
        public string ADD2 { get; set; }

        [StringLength(30)]
        public string ADD3 { get; set; }

        [StringLength(20)]
        public string TEL { get; set; }

        [StringLength(20)]
        public string FAX { get; set; }

        [StringLength(100)]
        public string HOOZINOCX { get; set; }

        [StringLength(100)]
        public string HOOZINOCX64 { get; set; }

        [StringLength(60)]
        public string SMTPSERVER { get; set; }

        public short? SMTPPORTNO { get; set; }

        [StringLength(60)]
        public string MAILUSERID { get; set; }

        [StringLength(20)]
        public string MAILUSERPASS { get; set; }

        [StringLength(100)]
        public string MAILADRS { get; set; }

        [StringLength(100)]
        public string ERRMAILADRS { get; set; }

        [StringLength(100)]
        public string DOCREFPATH { get; set; }

        [StringLength(13)]
        public string MYNUM { get; set; }

        [StringLength(40)]
        public string COMPENGNM { get; set; }

        [StringLength(30)]
        public string ADDENG1 { get; set; }

        [StringLength(30)]
        public string ADDENG2 { get; set; }

        [StringLength(30)]
        public string ADDENG3 { get; set; }

        [StringLength(20)]
        public string TELENG { get; set; }

        [StringLength(20)]
        public string FAXENG { get; set; }

        [Required]
        [StringLength(1)]
        public string LOTSAIBANKBN { get; set; }

        [Required]
        [StringLength(1)]
        public string LOTHIKITEKBN { get; set; }

        [StringLength(5)]
        public string RESPPROCECD { get; set; }

        [Required]
        [StringLength(1)]
        public string USEPRICEKBN { get; set; }

        [Required]
        [StringLength(1)]
        public string AMOHASUKBN { get; set; }

        [Required]
        [StringLength(1)]
        public string ACCLINKBN { get; set; }

        public byte? STMONTH { get; set; }

        [StringLength(3)]
        public string DDEACCCD { get; set; }

        [StringLength(3)]
        public string DDEAUACCCD { get; set; }

        [StringLength(3)]
        public string DCRACCCD { get; set; }

        [StringLength(3)]
        public string DCRAUACCCD { get; set; }

        [StringLength(2)]
        public string DDESECTIONCD { get; set; }

        [StringLength(2)]
        public string DCRSECTIONCD { get; set; }

        [StringLength(3)]
        public string GDEACCCD { get; set; }

        [StringLength(3)]
        public string GDEAUACCCD { get; set; }

        [StringLength(3)]
        public string GCRACCCD { get; set; }

        [StringLength(3)]
        public string GCRAUACCCD { get; set; }

        [StringLength(5)]
        public string MPROCECD { get; set; }

        [StringLength(2)]
        public string MSECTIONCD { get; set; }

        [StringLength(3)]
        public string GEACCCD { get; set; }

        [StringLength(3)]
        public string TEACCCD { get; set; }

        [StringLength(3)]
        public string TAXTYPE { get; set; }

        [StringLength(3)]
        public string FUEACCCD { get; set; }

        [StringLength(3)]
        public string FURACCCD { get; set; }

        [StringLength(3)]
        public string KOEACCCD { get; set; }

        [StringLength(3)]
        public string KORACCCD { get; set; }

        [StringLength(3)]
        public string TEEACCCD { get; set; }

        [StringLength(3)]
        public string TERACCCD { get; set; }

        [StringLength(3)]
        public string SOEACCCD { get; set; }

        [StringLength(3)]
        public string SORACCCD { get; set; }

        [StringLength(3)]
        public string GAIDEACCCD { get; set; }

        [StringLength(3)]
        public string ZAIDEACCCD { get; set; }

        [StringLength(3)]
        public string BUDEACCCD { get; set; }

        [StringLength(3)]
        public string GAICRACCCD { get; set; }

        [StringLength(3)]
        public string ZAICRACCCD { get; set; }

        [StringLength(3)]
        public string BUCRACCCD { get; set; }

        [StringLength(255)]
        public string CEOSFILE { get; set; }

        [Required]
        [StringLength(1)]
        public string PSWCHKKBN { get; set; }

        public short? PSWVALDAY { get; set; }

        public short? PSWVALMSGDAY { get; set; }

        [StringLength(10)]
        public string TRADERCD { get; set; }

        [StringLength(40)]
        public string TRADERNM { get; set; }

        [StringLength(4)]
        public string BANKNO { get; set; }

        [StringLength(15)]
        public string BANKNM { get; set; }

        [StringLength(3)]
        public string BRANCHNO { get; set; }

        [StringLength(15)]
        public string BRANCHNM { get; set; }

        [Required]
        [StringLength(1)]
        public string DEPOSITS { get; set; }

        [StringLength(7)]
        public string ACCNO { get; set; }

        public int? MYBRANFEEM { get; set; }

        public int? MYBRANFEEL { get; set; }

        public int? OTHBRANFEEM { get; set; }

        public int? OTHBRANFEEL { get; set; }

        public int? OTHBANKFEEM { get; set; }

        public int? OTHBANKFEEL { get; set; }

        [Required]
        [StringLength(64)]
        public string INSTID { get; set; }

        public DateTime INSTDT { get; set; }

        [Required]
        [StringLength(50)]
        public string INSTTERM { get; set; }

        [Required]
        [StringLength(50)]
        public string INSTPRGNM { get; set; }

        [Required]
        [StringLength(64)]
        public string UPDTID { get; set; }

        public DateTime UPDTDT { get; set; }

        [Required]
        [StringLength(50)]
        public string UPDTTERM { get; set; }

        [Required]
        [StringLength(50)]
        public string UPDTPRGNM { get; set; }

        public byte? KIKAN { get; set; }

        public byte? ESTRITU { get; set; }

        [StringLength(60)]
        public string BANKNMZEN { get; set; }

        [StringLength(60)]
        public string BRANCHNMZEN { get; set; }

        [StringLength(60)]
        public string ACCNM { get; set; }

        [StringLength(6)]
        public string DESTCD { get; set; }
    }
}
