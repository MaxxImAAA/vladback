namespace API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeProduct { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }

        public List<CartProduct> CartProducts { get; set; }
    }
}
