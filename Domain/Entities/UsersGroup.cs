using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("usersgroup")]
    public class UsersGroup
    {
        [Key, Column("id_user", Order = 1)]
        public int IdUser { get; set; }

        [Key, Column("id_group", Order = 2)]
        public int IdGroup { get; set; }


        public Group Group { get; set; }

        public User User { get; set; }
    }
}
