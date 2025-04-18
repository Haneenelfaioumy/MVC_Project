using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.UsersViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string Email { get; set; } = null!;

        [Phone]
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        // Relationship Between Users And Roles Tables => Many To Many
    }
}
