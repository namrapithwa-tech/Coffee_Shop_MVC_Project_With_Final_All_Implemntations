using System.ComponentModel.DataAnnotations;

namespace DataTables.Models.Coffee
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [StringLength(100, ErrorMessage = "User Name cannot be longer than 100 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [MinLength(10, ErrorMessage = "Mobile number should be 10 digits")]
        [RegularExpression("^\\d+$",ErrorMessage ="Mobile number does not contains alphabet")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }
    }
    public class UserDropDownModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
