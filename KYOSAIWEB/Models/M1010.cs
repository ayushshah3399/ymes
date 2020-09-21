namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.M1010")]
    public partial class M1010
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M1010()
        {
            M10101 = new HashSet<M1010>();
        }

        [Key]
        [StringLength(6)]
        public string BCCD { get; set; }

        [StringLength(50)]
        public string BCKNM { get; set; }

        [Required]
        [StringLength(40)]
        public string BCNM { get; set; }

        [Required]
        [StringLength(20)]
        public string BCRNM { get; set; }

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

        [Required]
        [StringLength(6)]
        public string CALTYP { get; set; }

        [StringLength(40)]
        public string REMARK { get; set; }

        [Required]
        [StringLength(1)]
        public string USEKBN { get; set; }

        [Required]
        [StringLength(3)]
        public string CURRENCYCD { get; set; }

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

        [StringLength(40)]
        public string KYOTENNM { get; set; }

        [StringLength(1)]
        public string HONSYAKBN { get; set; }

        [StringLength(6)]
        public string HONSYACD { get; set; }

        [StringLength(20)]
        public string TEL2 { get; set; }

        [StringLength(50)]
        public string BUSYONM { get; set; }

        [StringLength(30)]
        public string EIYAKU { get; set; }

        [StringLength(30)]
        public string EINM { get; set; }

        [StringLength(30)]
        public string EIMAIL { get; set; }

        [StringLength(20)]
        public string EITEL { get; set; }

        [StringLength(1)]
        public string EIODR { get; set; }

        [StringLength(1)]
        public string EIDLV { get; set; }

        [StringLength(1)]
        public string EIEST { get; set; }

        [StringLength(1)]
        public string EIENV { get; set; }

        [StringLength(30)]
        public string AS1YAKU { get; set; }

        [StringLength(30)]
        public string AS1NM { get; set; }

        [StringLength(30)]
        public string AS1MAIL { get; set; }

        [StringLength(20)]
        public string AS1TEL { get; set; }

        [StringLength(1)]
        public string AS1ODR { get; set; }

        [StringLength(1)]
        public string AS1DLV { get; set; }

        [StringLength(1)]
        public string AS1EST { get; set; }

        [StringLength(1)]
        public string AS1ENV { get; set; }

        [StringLength(30)]
        public string AS2YAKU { get; set; }

        [StringLength(30)]
        public string AS2NM { get; set; }

        [StringLength(30)]
        public string AS2MAIL { get; set; }

        [StringLength(20)]
        public string AS2TEL { get; set; }

        [StringLength(1)]
        public string AS2ODR { get; set; }

        [StringLength(1)]
        public string AS2DLV { get; set; }

        [StringLength(1)]
        public string AS2EST { get; set; }

        [StringLength(1)]
        public string AS2ENV { get; set; }

        [StringLength(20)]
        public string ODRFAX { get; set; }

        [StringLength(1)]
        public string ODRKBN { get; set; }

        [StringLength(20)]
        public string DELIFAX { get; set; }

        [StringLength(1)]
        public string DELIKBN { get; set; }

        [StringLength(20)]
        public string ACCFAX { get; set; }

        [StringLength(1)]
        public string ACCKBN { get; set; }

        [StringLength(1)]
        public string PAYCOND { get; set; }

        [StringLength(40)]
        public string PAYCONDNM { get; set; }

        [StringLength(1)]
        public string TRANSNAIYO { get; set; }

        public DateTime? CONDT { get; set; }

        public DateTime? OBTDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M1010> M10101 { get; set; }

        public virtual M1010 M10102 { get; set; }
    }
}
