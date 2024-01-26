using Microsoft.EntityFrameworkCore;
using Recipe.Models;
using System;

public class RecipeDbContext : DbContext
{
	public DbSet<Item> Items { get; set; }
	public DbSet<Ingredients> Ingredients { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Grocery> Grocery { get; set; }

	public DbSet<Category> Category { get; set; }

	public RecipeDbContext(DbContextOptions<RecipeDbContext> context) : base(context)
    {

}
}
