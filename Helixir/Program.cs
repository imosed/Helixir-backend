using System;
using System.Collections.Generic;
using System.Linq;
using Helixir.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Helixir
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<HelixirContext>();
                    context.Database.EnsureCreated();

                    if (!context.Ingredients.Any())
                    {
                        var ingredients = new List<Ingredient>
                        {
                            new Ingredient {Name = "Vodka", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Dark Rum", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "White Rum", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Spiced Rum", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Coconut Rum", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Tequila", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Gin", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Irish Cream", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Chocolate Liquor", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Cola", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Whole Milk", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Heavy Cream", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Scotch", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Rye Whiskey", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Irish Whiskey", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Canadian Whisky", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Bourbon", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Brandy", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Sake", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Absinthe", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Everclear", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Vermouth", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Mead", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Triple Sec", Alcoholic = true, Spice = false},
                            new Ingredient {Name = "Sweet & Sour", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Pineapple Juice", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Grenadine", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Fruit Punch", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Olive Juice", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Lemon Juice", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Lime Juice", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Orange Juice", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Chocolate Syrup", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Club Soda", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Green Tea", Alcoholic = false, Spice = false},
                            new Ingredient {Name = "Matcha", Alcoholic = false, Spice = true},
                            new Ingredient {Name = "Nutmeg", Alcoholic = false, Spice = true},
                            new Ingredient {Name = "Cinnamon", Alcoholic = false, Spice = true},
                            new Ingredient {Name = "Eggnog", Alcoholic = false, Spice = false}
                        };
                        ingredients.ForEach(i => context.Ingredients.Add(i));
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError("An error occurred: " + ex);
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}