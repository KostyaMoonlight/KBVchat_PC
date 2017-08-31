using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("userinfo")]
    public class UserInfo
    {
        [Key, Column("iduser")]
        public int Id { get; set; }

        [Column("nick")]
        public string Nickname { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("middlename")]
        public string MiddleName { get; set; }

        [Column("thirdname")]
        public string ThirdName { get; set; }

        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

    }
}
