using Recipe.Models;

namespace Recipe.DTOs
{
    public class CreateItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }

        public User User { get; set; }

        public List<IngredientDTO> Ingredients { get; set; }
    }
}
