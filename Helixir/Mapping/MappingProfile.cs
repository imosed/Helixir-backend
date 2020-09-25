using AutoMapper;
using Helixir.Controllers.Resources;
using Helixir.Data;
using Helixir.Models;

namespace Helixir.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ingredient, IngredientResource>();
            CreateMap<Drink, DrinkResource>();
        }
    }
}