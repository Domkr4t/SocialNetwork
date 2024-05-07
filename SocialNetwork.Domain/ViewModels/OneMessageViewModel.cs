namespace SocialNetwork.Domain.ViewModels
{
    public class OneMessageViewModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public bool IsReading { get; set; }
        public string DateOfMessage { get; set; }
    }
}
