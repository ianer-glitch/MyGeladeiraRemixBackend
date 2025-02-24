namespace Fridge.Domain.Items.Get;

public interface IGetItemsOut
{
        public Guid Id { get; set; }
        public  string Color { get; set; }
        public  string Name { get; set; }
        public  string Icon { get; set; }
        public int MinimumQuantity { get; set; }
        public int Quantity { get; set; }
        public DateTime Expiration { get; set; }
        public double Weight { get; set; }
}
