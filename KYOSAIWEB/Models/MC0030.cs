namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.MC0030")]
    public partial class MC0030
    {
        [Key]
        [StringLength(3)]
        public string MAKERCD { get; set; }

        [Required]
        [StringLength(60)]
        public string MAKERNM { get; set; }

        [Required]
        [StringLength(30)]
        public string MAKERLOGO { get; set; }

        [Required]
        [StringLength(30)]
        public string MAKERIPNM { get; set; }

        [Required]
        [StringLength(20)]
        public string SAIYOUNM { get; set; }

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
    }
}
