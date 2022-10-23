using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsUser")]
    public class UserDTO : BaseModel
    {
        [Key]
        [Column("IDUser")]
        public Guid IDUser { get; set; }
        [Column("IDRole")]
        public int IDRole { get; set; }
        [Column("Username")]
        public string Username { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Email")]
        public string Email { get; set; }
    }
}
