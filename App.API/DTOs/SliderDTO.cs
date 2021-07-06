namespace App.API.DTOs
{
    public class SliderDTO : BaseLookupDTO
    {
        public string ImageUrl { get; set; }
        public string ButtonUrl { get; set; }
        public string ButtonText { get; set; }
        public string ButtonTextAr { get; set; }
        public bool IsActive { get; set; }
    }
}
