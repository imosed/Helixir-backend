using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Helixir.Controllers.Resources;
using Helixir.Data;
using Helixir.Models;
using Microsoft.AspNetCore.Mvc;

namespace Helixir.Controllers
{
    [Route("/api/[controller]/")]
    public class DrinksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly HelixirContext _context;

        public DrinksController(HelixirContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DrinkResource> GetDrinkById(int id)
        {
            var drink = _context.Drinks.Find(id);
            return _mapper.Map<Drink, DrinkResource>(drink);
        }

        [HttpGet]
        [Route("list")]
        [Route("list/{filter}")]
        public ActionResult<List<DrinkScore>> GetDrinkList(string filter)
        {
            var drinksScores = _context.Drinks.Join(
                _context.Scores,
                d => d.ScoreId,
                s => s.DrinkId,
                (d, s) => new DrinkScore
                {
                    Id = d.Id,
                    Name = d.Name,
                    Points = s.Points
                }
            ).ToList();
            
            var drink = filter != null
                ? drinksScores.Where(d => d.Name.ToLower().Split().Any(w => w.StartsWith(filter.ToLower())))
                : drinksScores.OrderBy(d => d.Points);
            return new ActionResult<List<DrinkScore>>(drink.ToList());
        }

        [HttpGet]
        [Route("scoreup/{id}")]
        public ActionResult<bool> ScoreUp(int id)
        {
            var score = _context.Scores.Single(s => s.DrinkId == id);
            _context.Scores.Update(score).Entity.Points += 1;
            return _context.SaveChanges() > 0;
        }
        
        [HttpPost]
        [Route("add")]
        public ActionResult<bool> AddDrink([FromBody] Drink recipe)
        {
            var drink = new Drink
            {
                Name = recipe.Name,
                Instructions = recipe.Instructions
            };
            _context.Add(drink);
            _context.AddRange(recipe.Volumes);
            
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}