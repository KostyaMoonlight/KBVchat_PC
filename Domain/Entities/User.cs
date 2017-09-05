using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Users")]
    public class User
    {
        [Key, Column("iduser")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("lasttimeaccess")]
        public DateTime LastTimeAccess { get; set; }

        [Column("unreadmessages")]
        public int UnreadMessages { get; set; }

        [Column("isonline")]
        public int IsOnline { get; set; }


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


        public virtual ICollection<Friend> FirstFriend { get; set; }

        public virtual ICollection<Friend> SecondFriend { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Group> GroupAdmins { get; set; }

        public virtual ICollection<UsersGroup> UserGroups { get; set; }

    }

    public class UserEntityTypeConfiguration
        : EntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration()
        {
            Map(m =>
            {
                m.Properties(x => new
                {
                    x.Id,
                    x.Login,
                    x.Email,
                    x.IsOnline,
                    x.LastTimeAccess,
                    x.Nickname,
                    x.Password,
                    x.UnreadMessages,

                });
                m.ToTable("Users");
            })
            .Map(m =>
            {
                m.Properties(x => new
                {
                    x.Birthdate,
                    x.FirstName,
                    x.MiddleName,
                    x.Phone,
                    x.ThirdName
                });
                m.ToTable("UserInfo");
            });
        }
    }
}
