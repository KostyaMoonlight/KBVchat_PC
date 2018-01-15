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
        [Key, Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("last_time_access")]
        public DateTime LastTimeAccess { get; set; }

        [Column("unread_messages")]
        public int UnreadMessages { get; set; }

        [Column("is_online")]
        public int IsOnline { get; set; }

        [Column("balance")]
        public double Balance { get; set; }

        [Column("card")]
        public string Card { get; set; }

        [Column("card_date")]
        public string CardDate { get; set; }

        [Column("card_svv")]
        public string CardSVV { get; set; }

        [Column("room_id")]
        public int? RoomId { get; set; }

        [Column("nick")]
        public string Nickname { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("middle_name")]
        public string MiddleName { get; set; }

        [Column("third_name")]
        public string ThirdName { get; set; }

        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

        public Room Room { get; set; }

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
                    x.Email,
                    x.IsOnline,
                    x.LastTimeAccess,
                    x.Nickname,
                    x.Password,
                    x.UnreadMessages,
                    x.Balance,
                    x.Card,
                    x.CardDate,
                    x.CardSVV,
                    x.RoomId
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
