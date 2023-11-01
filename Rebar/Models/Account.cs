namespace Rebar.Models
{
    public class Account
    {
        public List<Order> orders { get; } = new List<Order>();
        public int CostAllOrders { get; set; }  
        

    }
}
