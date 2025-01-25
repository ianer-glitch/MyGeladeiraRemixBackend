namespace Fridge.Domain.Items.Get;

public interface IGetItemsOut
{
        public Guid Id { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }    
        public string Icon { get; set; }
}
