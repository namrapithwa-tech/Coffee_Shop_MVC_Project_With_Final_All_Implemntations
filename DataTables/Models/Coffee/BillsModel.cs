using System.ComponentModel.DataAnnotations;

namespace DataTables.Models.Coffee
{
    public class BillsModel
    {
        public int BillID { get; set; }

        [Required(ErrorMessage = "Bill Number is required")]
        [StringLength(100, ErrorMessage = "Bill Number cannot be longer than 100 characters")]
        [RegularExpression("^B\\d{3}$", ErrorMessage = "Bill Number Must be Start with B and Contains 3 digits")]
        public string BillNumber { get; set; }

        [Required(ErrorMessage = "Bill Date is required")]
        [DataType(DataType.Date)]
        public DateTime BillDate { get; set; }

        [Required(ErrorMessage = "Order ID is required")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Total Amount must be between 0.01 and 999999.99")]
        public decimal TotalAmount { get; set; }

        [Range(0, 999999.99, ErrorMessage = "Discount must be between 0 and 999999.99")]
        [Required(ErrorMessage = "Discount should not be Empty")]
        public decimal? Discount { get; set; }

        [Required(ErrorMessage = "Net Amount is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Net Amount must be between 0.01 and 999999.99")]
        public decimal NetAmount { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserID { get; set; }
    }
}
