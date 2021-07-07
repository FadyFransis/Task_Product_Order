namespace App.API.DTOs
{
    public class AreaDTO : BaseNameDTO
    {
        public long CityID { get; set; }
        public string CityName { get; set; }
        public string CityNameAr { get; set; }
        public decimal DeliveryFees { get; set; }
    }
}
