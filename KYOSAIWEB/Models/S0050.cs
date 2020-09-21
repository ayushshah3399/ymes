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
        [Required(ErrorMessage = "必須項目")]
        [AllowHtml]
        [StringLength(20, ErrorMessage = "ユーザーIDは、は20文字以下でなければなりません。")]
        public string USERID { get; set; }

        [StringLength(32)]
        public string USERNM { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "必須項目")]
        [DataType(DataType.Password)]
        [StringLength(96, ErrorMessage = "パスワードは、は20文字以下でなければなりません。")]
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
