using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OracleDTO
{
    [Table("PS_N_PERSONCAR_TBL")]
    public class PS_N_PERSONCAR_TBLDTO
    {
		[Column("EMPLID")]
		public string EMPLID { get; set; }
		[Column("EXTERNAL_SYSTEM_ID")]
		public string EXTERNAL_SYSTEM_ID { get; set; }
		[Column("N_STDNT_ID2")]
		public string N_STDNT_ID2 { get; set; }
		[Column("PHONE")]
		public string PHONE { get; set; }
		[Column("ACAD_CAREER")]
		public string ACAD_CAREER { get; set; }
		[Column("CUM_GPA")]
		public decimal CUM_GPA { get; set; }
		[Column("EMAIL_ADDR")]
		public string EMAIL_ADDR { get; set; }
		[Column("FIRST_NAME")]
		public string FIRST_NAME { get; set; }
		[Column("LAST_NAME")]
		public string LAST_NAME { get; set; }
		[Column("BIRTHDATE")]
		public DateTime BIRTHDATE { get; set; }
	}
}
