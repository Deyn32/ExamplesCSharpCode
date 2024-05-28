using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZooFerma.Models.Dto;

namespace ZooFerma.Models.Dao
{
    [Table(name: "clients")]
    public class Client
    {
        public Client()
        {
            email = "";
            phone = "";
            name = "";
        }

        public Client(ClientRequestDto dto)
        {
            client_id = dto.client_id;
            email = dto.email;
            phone = dto.phone;
            name = dto.name;
            user_id = dto.user_id;
        }

        [Key]
        public long client_id { get; set; }
        public string? email { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public Guid user_id { get; set; }
    }
}
