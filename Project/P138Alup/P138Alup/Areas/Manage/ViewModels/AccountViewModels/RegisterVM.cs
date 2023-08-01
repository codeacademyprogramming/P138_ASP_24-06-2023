using System.ComponentModel.DataAnnotations;

namespace P138Alup.Areas.Manage.ViewModels.AccountViewModels
{
    public class RegisterVM
    {
        [StringLength(100)]
        public string Name { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
