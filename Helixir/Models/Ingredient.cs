using System.Collections.Generic;
using Helixir.Models;

namespace Helixir.Data
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Alcoholic { get; set; }
        public bool Spice { get; set; }
        
        public ICollection<Volume> Volumes { get; set; }
    }
}