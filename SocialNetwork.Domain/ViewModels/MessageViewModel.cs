namespace SocialNetwork.Domain.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string FromUserLogin { get; set; }
        public string Header { get; set; }
        public bool IsReading { get; set; }
        public string DateOfMessage { get; set; }
    }
}
