using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Key, Column("iduser")]
        public int Id { get; set; }

        [Key, Column("email")]
        public string Email { get; set; }

        [Key, Column("password")]
        public string Password { get; set; }

        [Key, Column("phone")]
        public string Phone { get; set; }

        [Key, Column("lasttimeaccess")]
        public DateTime LastTimeAccess { get; set; }

        [Key, Column("unreadmessages")]
        public int UnreadMessages { get; set; }

        [Key, Column("isonline")]
        public int IsOnline { get; set; }

        public ICollection<Friend> FirstFriend { get; set; }

        public ICollection<Friend> SecondFriend { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Group> GroupAdmins { get; set; }

        public ICollection<UsersGroup> UserGroups { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
