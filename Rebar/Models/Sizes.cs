namespace Rebar.Models
{
    public enum AllSizes
    {
        S, M, L
    }
    public struct SizeAndPrice
    {
        public AllSizes Size { get; }
        public float Price { get; set; }

        public SizeAndPrice(AllSizes size, float price)
        {
            Size = size;
            Price = price;
        }
    }

}
