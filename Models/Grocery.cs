namespace Recipe.Models
{
    public class Grocery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ICollection<Ingredients> Ingredients { get; set;}

    }
}
