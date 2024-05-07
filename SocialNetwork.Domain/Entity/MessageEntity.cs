using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Entity
{
    public class MessageEntity
    {
        /// <summary>
        /// Displays the message ID
        /// </summary>
        [Comment("ID Message")]
        public int Id { get; set; }

        /// <summary>
        /// Displays the ID of the sender
        /// </summary>
        [Comment("Sender ID")]
        public int FromUserId { get; set; }
        public UserEntity FromUser { get; set; }

        /// <summary>
        /// Displays the recipient ID
        /// </summary>
        [Comment("Receiver ID")]
        public int ToUserId { get; set; }
        public UserEntity ToUser { get; set; }

        /// <summary>
        /// Displays the subject of the message
        /// </summary>
        [Comment("Post subject")]
        [StringLength(50)]
        public string Header { get; set; }

        /// <summary>
        /// Displays message text
        /// </summary>
        [Comment("Message text")]
        [StringLength(200)]
        public string Body { get; set; }

        /// <summary>
        /// Displays whether the message has been read
        /// </summary>
        [Comment("Whether the message has been read")]
        public bool IsReading { get; set; }

        /// <summary>
        /// Displays the date the message was sent
        /// </summary>
        [Comment("Date the message was sent")]
        public DateTime DateOfMessage { get; set; }
    }
}
