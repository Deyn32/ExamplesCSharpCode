using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZooFerma.Models.Dto;

namespace ZooFerma.Models.Dao
{
    [Table(name: "sessions")]
    public class Session
    {
        public Session()
        {
            status = false;
            link = "";
        }

        public Session(SessionRequestDto dto)
        {
            session_id = dto.session_id;
            user_id = dto.user_id;
            datestart = dto.datestart;
            dateend = dto.dateend;
            link = dto.link;
            status = dto.status;
        }

        [Key]
        public long session_id { get; set; }
        public Guid user_id { get; set; }
        public DateTime datestart { get; set; }
        public DateTime? dateend { get; set; }
        public string link { get; set; }
        public bool status { get; set; }
    }
}
