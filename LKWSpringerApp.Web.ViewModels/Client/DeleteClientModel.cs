namespace LKWSpringerApp.Web.ViewModels.Client
{
    public class DeleteClientModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int ClientNumber { get; set; }
    }
}
