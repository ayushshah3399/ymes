namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("TELAS.S0050")]
    public partial class S0050
    {
        [Key]
        [Required(ErrorMessage = "�K�{����")]
        [AllowHtml]
        [StringLength(20, ErrorMessage = "���[�U�[ID�́A��20�����ȉ��łȂ���΂Ȃ�܂���B")]
        public string USERID { get; set; }

        [StringLength(32)]
        public string USERNM { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "�K�{����")]
        [DataType(DataType.Password)]
        [StringLength(96, ErrorMessage = "�p�X���[�h�́A��20�����ȉ��łȂ���΂Ȃ�܂���B")]
        public string PASS { get; set; }

        public DateTime? PSWUPDTDT { get; set; }

        [StringLength(1)]
        public string LOGINHIST { get; set; }

        [StringLength(6)]
        public string SUPPCD { get; set; }

        [StringLength(4)]
        public string CHARGECD { get; set; }
    }
}
