using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("group")]
    public class Group
    {
        [Key,Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("admin")]
        public int? IdAdmin { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public User Admin { get; set; }

        public virtual ICollection<UsersGroup> UsersGroups { get; set; }

    }
}
