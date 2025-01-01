using System.ComponentModel.DataAnnotations;

namespace DataTables.Models.Coffee
{
    public class OrderModel
    {
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Order Date is required")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Customer ID is required")]
        public int CustomerID { get; set; }

        [StringLength(100, ErrorMessage = "Payment Mode cannot be longer than 100 characters")]
        public string PaymentMode { get; set; }

        [Required(ErrorMessage = "Total Amount cannot be Empty")]
        [Range(0.01, 999999.99, ErrorMessage = "Total Amount must be between 0.01 and 999999.99")]
        public decimal? TotalAmount { get; set; }

        [Required(ErrorMessage = "Shipping Address is required")]
        [StringLength(100, ErrorMessage = "Shipping Address cannot be longer than 100 characters")]
        public string ShippingAddress { get; set;}

        [Required(ErrorMessage = "Order Number is required")]
        [StringLength(8, ErrorMessage = "Order Number cannot be longer than 8 characters")]
        [RegularExpression("^ORD\\d{5}$", ErrorMessage ="Order Number Must be Start with ORD and Contains 5 digits")]
        public string OrderNumber { get; set; }
        public int UserID { get; set; }
    }
    public class OrderDropDownModel
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
    }
}
