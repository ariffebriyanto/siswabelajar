using Model.DTO.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsRole")]
    public class RoleDTO : BaseModel
    {
        [Key]
        [Column("IDRole")]
        public int IDRole { get; set; }
        [Column("RoleName")]
        public string RoleName { get; set; }
        [Column("RoleLevel")]
        public int RoleLevel { get; set; }
    }
}
