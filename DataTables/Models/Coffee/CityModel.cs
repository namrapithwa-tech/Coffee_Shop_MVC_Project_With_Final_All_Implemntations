using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataTables.Models.Coffee
{
    public class CityModel
    {
        public int CityID { get; set; }

        [Required]
        [DisplayName("City Name")]
        public string CityName { get; set; }

        [Required]
        [DisplayName("Country Name")]
        public int CountryID { get; set; }

        [Required]
        [DisplayName("State ID")]
        public int StateID { get; set; }

        [Required]
        [DisplayName("State Name")]
        public string StateName { get; set; }

        [Required]
        [DisplayName("State Name")]
        public string CountryName { get; set; }

        [DisplayName("City Code")]
        public string CityCode { get; set; }
        public int? UserID { get; set; }
    }
}
