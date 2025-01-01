using System.ComponentModel.DataAnnotations;

namespace DataTables.Models.Coffee
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(100, ErrorMessage = "Customer Name cannot be longer than 100 characters")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Home Address is required")]
        [StringLength(100, ErrorMessage = "Home Address cannot be longer than 100 characters")]
        public string HomeAddress { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [MinLength(10, ErrorMessage = "Mobile number should be 10 digits")]
        [RegularExpression("^\\d+$", ErrorMessage = "Mobile number does not contains alphabet")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "GST NO is required")]
        [StringLength(15, ErrorMessage = "GST NO cannot be longer than 15 characters")]
        public string GSTNO { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [StringLength(100, ErrorMessage = "City Name cannot be longer than 100 characters")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Pin Code is required")]
        [StringLength(15, ErrorMessage = "Pin Code cannot be longer than 15 characters")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Net Amount is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Net Amount must be between 0.01 and 999999.99")]
        public decimal NetAmount { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserID { get; set; }
    }
    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}
