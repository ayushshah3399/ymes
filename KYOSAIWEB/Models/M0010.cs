namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.M0010")]
    public partial class M0010
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M0010()
        {
            D3010 = new HashSet<D3010>();
            M00101 = new HashSet<M0010>();
            M001011 = new HashSet<M0010>();
        }

        [Key]
        [StringLength(20)]
        public string HMNO { get; set; }

        [Required]
        [StringLength(40)]
        public string HMNM { get; set; }

        [Required]
        [StringLength(20)]
        public string HMRNM { get; set; }

        [Required]
        [StringLength(1)]
        public string HMKBN { get; set; }

        [StringLength(6)]
        public string CUSTCD { get; set; }

        [StringLength(20)]
        public string CUSTHMNO { get; set; }

        [StringLength(20)]
        public string DRAWNO { get; set; }

        [StringLength(6)]
        public string SUPPCD { get; set; }

        [StringLength(2)]
        public string SECTIONCD { get; set; }

        [Required]
        [StringLength(5)]
        public string PROCECD { get; set; }

        [StringLength(5)]
        public string MCCD { get; set; }

        [StringLength(4)]
        public string CHARGECD { get; set; }

        [Required]
        [StringLength(1)]
        public string STOCKKBN { get; set; }

        [StringLength(20)]
        public string STOCKHMNO { get; set; }

        [StringLength(3)]
        public string STOCKCD { get; set; }

        [Required]
        [StringLength(1)]
        public string LOTKBN { get; set; }

        public DateTime YMDFR { get; set; }

        public DateTime YMDTO { get; set; }

        public int? PACKQTY { get; set; }

        [Required]
        [StringLength(2)]
        public string UNIT { get; set; }

        public decimal? WEIGHT { get; set; }

        [StringLength(2)]
        public string WEUNIT { get; set; }

        [StringLength(2)]
        public string WEDENUNIT { get; set; }

        [StringLength(40)]
        public string REMARK { get; set; }

        [Required]
        [StringLength(1)]
        public string ARRGKBN { get; set; }

        [StringLength(5)]
        public string KSCD { get; set; }

        public decimal? YIELD { get; set; }

        [Required]
        [StringLength(1)]
        public string TPATARN { get; set; }

        [Required]
        [StringLength(1)]
        public string OFFSETKBN { get; set; }

        [StringLength(6)]
        public string OFFCUSTCD { get; set; }

        [Required]
        [StringLength(1)]
        public string USEKBN { get; set; }

        [StringLength(40)]
        public string INVOICENM { get; set; }

        [StringLength(3)]
        public string TAXTYPE { get; set; }

        [StringLength(1)]
        public string MASPROKBN { get; set; }

        [StringLength(2)]
        public string CLASSCD { get; set; }

        [Required]
        [StringLength(1)]
        public string DECIKBN { get; set; }

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

        [StringLength(3)]
        public string KYOTENCD { get; set; }

        [StringLength(20)]
        public string ZUNO { get; set; }

        public byte? ZVER { get; set; }

        [StringLength(18)]
        public string SEHMNO { get; set; }

        [StringLength(2)]
        public string MEICD { get; set; }

        [Required]
        [StringLength(1)]
        public string SISAKUKBN { get; set; }

        [StringLength(5)]
        public string CEREALCD { get; set; }

        [Required]
        [StringLength(1)]
        public string SUPSTS { get; set; }

        public DateTime? CHGDT { get; set; }

        [StringLength(3)]
        public string HINSYUCD { get; set; }

        [StringLength(3)]
        public string MAKERCD { get; set; }

        [Required]
        [StringLength(1)]
        public string TYPE { get; set; }

        [Required]
        [StringLength(1)]
        public string NISUGATA { get; set; }

        [StringLength(10)]
        public string EYOKYU { get; set; }

        [StringLength(20)]
        public string HAKKONO { get; set; }

        public DateTime? TKGNDT { get; set; }

        public DateTime? JKGNDT { get; set; }

        public DateTime? MKGNDT { get; set; }

        [StringLength(20)]
        public string TANA { get; set; }

        [StringLength(50)]
        public string OKURINOTE { get; set; }

        [StringLength(50)]
        public string CHGNOTE { get; set; }

        [StringLength(4)]
        public string EITANCD { get; set; }

        [Required]
        [StringLength(1)]
        public string PRNKBN { get; set; }

        [StringLength(4)]
        public string SEITANCD { get; set; }

        [StringLength(4)]
        public string SITANCD { get; set; }

        public decimal? TKZIQTY { get; set; }

        public decimal? SIZIQTY { get; set; }

        [StringLength(20)]
        public string BETUHMCD { get; set; }

        [Required]
        [StringLength(1)]
        public string ZAIKOKBN { get; set; }

        public DateTime? ZUBANDT { get; set; }

        [StringLength(40)]
        public string SIYO { get; set; }

        [StringLength(10)]
        public string ESTNO { get; set; }

        public DateTime? ESTDT { get; set; }

        [StringLength(20)]
        public string SERIES { get; set; }

        [StringLength(20)]
        public string PACKNM { get; set; }

        [StringLength(10)]
        public string TOKUSEI { get; set; }

        public decimal? EP { get; set; }

        [StringLength(2)]
        public string EPUNIT { get; set; }

        public decimal? RS { get; set; }

        [StringLength(2)]
        public string RSUNIT { get; set; }

        public decimal? VOL { get; set; }

        public decimal? ES { get; set; }

        [StringLength(2)]
        public string ESUNIT { get; set; }

        public decimal? TR { get; set; }

        [StringLength(2)]
        public string TRUNIT { get; set; }

        [StringLength(3)]
        public string BUNCD { get; set; }

        [StringLength(3)]
        public string KUNICD { get; set; }

        [StringLength(2)]
        public string WUNIT { get; set; }

        public byte? KIKAN { get; set; }

        [StringLength(50)]
        public string PMEMO { get; set; }

        [Required]
        [StringLength(1)]
        public string DEKBN { get; set; }

        public DateTime? ZJUDT { get; set; }

        public DateTime? ZSHIPDT { get; set; }

        [StringLength(20)]
        public string SITEICD { get; set; }

        public byte? JUCNT { get; set; }

        public decimal? ITEMSIZE { get; set; }

        [StringLength(1)]
        public string SEYOKYU { get; set; }

        [StringLength(1)]
        public string AZKBN { get; set; }

        [StringLength(40)]
        public string SHINNM { get; set; }

        [StringLength(20)]
        public string BUNO { get; set; }

        [StringLength(20)]
        public string ASSYNO { get; set; }

        [StringLength(2)]
        public string PHASE { get; set; }

        [StringLength(1)]
        public string SMTKBN { get; set; }

        [StringLength(1)]
        public string LILLEIETM { get; set; }

        public byte? LILLEPITCH { get; set; }

        [Required]
        [StringLength(1)]
        public string LONGDELIVERY { get; set; }

        [Required]
        [StringLength(1)]
        public string QUOCALCOMIT { get; set; }

        public decimal? ITEMWEIGHT { get; set; }

        [StringLength(20)]
        public string SPECNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<D3010> D3010 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M0010> M00101 { get; set; }

        public virtual M0010 M00102 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M0010> M001011 { get; set; }

        public virtual M0010 M00103 { get; set; }

        public virtual MC0030 MC0030 { get; set; }
    }
}
