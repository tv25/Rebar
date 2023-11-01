using MongoDB.Bson.Serialization.Attributes;

namespace Rebar.Models
{
    public enum Sizes
    {
        S,M,L
    }
    public record Shakes(Shake shake, Sizes size, int price);
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int TotalPrice { get; set; }
        public string ClientName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderEndDate { get; set; }
        public List <Shakes> SelectedShakes { get; set; }
        public List<Discount>? Discounts { get; set; }= new List<Discount>();


    }
}
