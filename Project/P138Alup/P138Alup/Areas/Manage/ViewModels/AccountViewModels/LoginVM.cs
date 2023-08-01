using System.ComponentModel.DataAnnotations;

namespace P138Alup.Areas.Manage.ViewModels.AccountViewModels
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
