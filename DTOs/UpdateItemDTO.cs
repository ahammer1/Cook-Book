using Recipe.Models;

namespace Recipe.DTOs
{
    public class UpdateItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }

        public User User { get; set; }
    }
}
