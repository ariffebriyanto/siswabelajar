using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrBlastEmail")]
    public class BlastEmailDTO : BaseModel
    {
        [Key]
        [Column("IDBlastEmail")]
        public Guid IDBlastEmail { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("Subject")]
        public string Subject{ get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("BlastDateTime")]
        public DateTime BlastDateTime { get; set; }
    }
}
