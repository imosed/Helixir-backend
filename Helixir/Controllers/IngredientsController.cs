using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Helixir.Controllers.Resources;
using Helixir.Data;
using Microsoft.AspNetCore.Mvc;

namespace Helixir.Controllers
{
    [Route("/api/[controller]/")]
    public class IngredientsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly HelixirContext _context;
        public IngredientsController(HelixirContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        
        [HttpPost]
        [Route("add")]
        public ActionResult<bool> AddIngredient([FromBody] IngredientResource ingredient)
        {
            _context.Add(new Ingredient { Name = ingredient.Name, Alcoholic = ingredient.Alcoholic });
            var added = _context.SaveChanges();
            return added > 0;
        }
        
        [HttpGet]
        [Route("list")]
        [Route("list/{filter}")]
        public ActionResult<List<IngredientResource>> GetIngredients(string filter)
        {
            var ingredients = filter != null
                ? _context.Ingredients.ToList().Where(i => i.Name.ToLower().Split().Any(w => w.StartsWith(filter.ToLower())))
                : _context.Ingredients.OrderBy(i => i.Name);
            return _mapper.Map<List<Ingredient>, List<IngredientResource>>(ingredients.ToList());
        }
    }
}
