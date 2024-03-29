﻿namespace Recipe.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public ICollection<Ingredients> Ingredients { get; set;}

        public ICollection<Category> Categories { get; set;}

        public User User { get; set; }

    }
}
