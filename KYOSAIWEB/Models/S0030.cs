namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.S0030")]
    public partial class S0030
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string APPID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string CALTYP { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime YMD { get; set; }

        [Required]
        [StringLength(1)]
        public string WKKBN { get; set; }

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
