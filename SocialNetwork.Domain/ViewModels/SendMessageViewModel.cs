namespace SocialNetwork.Domain.ViewModels
{
    public class SendMessageViewModel
    {
        public int FromUserId { get; set; }
        public string Login { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
