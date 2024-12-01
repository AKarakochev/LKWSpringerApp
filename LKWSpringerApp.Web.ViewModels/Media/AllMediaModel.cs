namespace LKWSpringerApp.Web.ViewModels.Media
{
    public class AllMediaModel
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public int MediaCount { get; set; }
    }
}
