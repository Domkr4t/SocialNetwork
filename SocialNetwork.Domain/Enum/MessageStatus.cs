using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Enum
{
    /// <summary>
    /// Enumeration used to display message status (Unread, Read, All)
    /// </summary>
    public enum MessageStatus
    {
        [Display(Name = "Не прочитанные")]
        Unread = 0,

        [Display(Name = "Прочитанные")]
        IsRead = 1,

        [Display(Name = "Все")]
        All = 2,
    }
}
