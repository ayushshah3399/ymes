namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.M1050")]
    public partial class M1050
    {
        [Key]
        [StringLength(6)]
        public string DELICD { get; set; }

        [Required]
        [StringLength(40)]
        public string DELINM { get; set; }

        [Required]
        [StringLength(20)]
        public string DELIRNM { get; set; }

        [StringLength(6)]
        public string BCCD { get; set; }

        [Required]
        [StringLength(3)]
        public string STOCKCD { get; set; }

        [StringLength(6)]
        public string CALTYP { get; set; }

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
