using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Enum
{
    /// <summary>
    /// Enumeration used to display message status (Unread, Read, All)
    /// </summary>
    public enum MessageStatus
    {
        [Display(Name = "Unread")]
        Unread = 0,

        [Display(Name = "Read")]
        IsRead = 1,

        [Display(Name = "All")]
        All = 2,
    }
}
