namespace NLayer.Core.DTOs
{
    public class ProductUpdateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
    }
}
