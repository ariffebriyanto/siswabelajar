using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrModule")]
    public class ModuleDTO : BaseModel
    {
        [Key]
        [Column("IDModule")]
        public Guid IDModule { get; set; }
        [Column("IDRole")]
        public int IDRole { get; set; }
        [Column("ModuleName")]
        public string ModuleName { get; set; }
        [Column("ModuleLevel")]
        public int ModuleLevel { get; set; }
        [Column("Route")]
        public string Route { get; set; }
    }
}
