using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Entity
{
    public class UserEntity
    {
        /// <summary>
        /// Displays the user ID
        /// </summary>
        [Comment("ID User")]
        public int Id { get; set; }

        /// <summary>
        /// Displays the user login
        /// </summary>
        [Comment("User login")]
        [StringLength(50)]
        public string Login { get; set; }

        /// <summary>
        /// Displays the user password
        /// </summary>
        [Comment("User password")]
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Displays the user surname
        /// </summary>
        [Comment("User surname")]
        [StringLength(50)]
        public string Surname { get; set; }

        /// <summary>
        /// Displays the user name
        /// </summary>
        [Comment("User name")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Displays the user middle name
        /// </summary>
        [Comment("User middle name")]
        [StringLength(50)]
        public string Middlename { get; set; }

        /// <summary>
        /// Displays a list of messages sent by the user
        /// </summary>
        [Comment("List of messages sent by the user")]
        public List<MessageEntity> SentMessages { get; set; }

        /// <summary>
        /// Displays a list of messages received by the user
        /// </summary>
        [Comment("List of messages received  by the user")]
        public List<MessageEntity> ReceivedMessages { get; set; }
    }
}
