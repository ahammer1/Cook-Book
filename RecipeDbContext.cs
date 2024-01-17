using Microsoft.EntityFrameworkCore;
using Recipe.Models;
using System;

public class RecipeDbContext : DbContext
{
	public DbSet<Item> Items { get; set; }
	

	public RecipeDbContext(DbContextOptions<RecipeDbContext> context) : base(context)
    {

}
}
