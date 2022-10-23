using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.Base
{
    public class BaseModel
	{
		[Column("UserIn")]
		public string UserIn { get; private set; }
		[Column("DateIn")]
		public DateTime DateIn { get; private set; }
		[Column("UserUp")]
		public string UserUp { get; private set; }
		[Column("DateUp")]
		public DateTime? DateUp { get; private set; }
		[Column("StsRc")]
		public char StsRc { get; private set; }

		public void SetUserIn(string UserIn)
		{
			this.UserIn = UserIn;
		}
		public void SetDateIn(DateTime DateIn)
		{
			this.DateIn = DateIn;
		}
		public void SetUserUp(string UserUp)
		{
			this.UserUp = UserUp;
		}
		public void SetDateUp(DateTime? DateUp)
		{
			this.DateUp = DateUp;
		}
		public void SetStsRc(char StsRc)
		{
			this.StsRc = StsRc;
		}
	}
}