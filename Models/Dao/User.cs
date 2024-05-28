using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZooFerma.Models.Dto;

namespace ZooFerma.Models.Dao
{
	[Table(name:"users")]
    public class User
    {

		public User() 
		{
			fio = "";
			email = "";
			password = "";
			phone = "";
		}

		public User(RegistrationDto dto) 
		{
			fio = dto.fio;
			email = dto.email;
			password = dto.password;
			phone = dto.phone;
		}

		[Key]
        public Guid? id { get; set; }
		public string fio { get; set; }
		public string email { get; set; }
		public string? password { get; set; }
		public string phone { get; set; }
		public DateTime? datestart { get; set; }
		public DateTime? dateend { get; set; }
		public bool status { get; set; }
		public int user_role {  get; set; }
    }
}
