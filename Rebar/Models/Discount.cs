namespace Rebar.Models
{
    public enum Discounts
    {
        A,B, C
    }
    public class Discount
    {
        public Discounts Name { get; set; }
        public float Percent { get;  }
        
    }
    
}
