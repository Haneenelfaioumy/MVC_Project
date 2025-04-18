namespace Demo.Presentation.ViewModels.RoleViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; } = default!;
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
