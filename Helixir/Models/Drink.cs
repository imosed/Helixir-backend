using System.Collections.Generic;

namespace Helixir.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        
        public ICollection<Volume> Volumes { set; get; }
        public int ScoreId { get; set; }
    }
}