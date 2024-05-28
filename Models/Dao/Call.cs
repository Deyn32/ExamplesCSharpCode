using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZooFerma.Models.Dto;

namespace ZooFerma.Models.Dao
{
    [Table(name: "calls")]
    public class Call
    {
        public Call() 
        {
            note = "";
        }

        public Call(CallRequestDto dto)
        {
            call_id = dto.call_id;
            user_id = dto.user_id;
            client_id = dto.client_id;
            note = dto.note != null ? dto.note : "";
        }

        [Key]
        public long call_id { get; set; }
        public Guid user_id { get; set; }
        public long client_id { get; set; }
        public string? note { get; set; }
    }
}
