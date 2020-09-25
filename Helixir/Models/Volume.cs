namespace Helixir.Models
{
    public class Volume
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int DrinkId { get; set; }
        public int Amount { get; set; }
    }
}