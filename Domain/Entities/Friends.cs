using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("friends")]
    public class Friends
    {
        [Key, Column("idfirst", Order = 1)]
        public int IdFirst { get; set; }

        [Key, Column("idsecond", Order = 2)]
        public int IdSecond { get; set; }

        [Column("isconfirmed")]
        public bool IsConfirmed { get; set; }

    }
}
