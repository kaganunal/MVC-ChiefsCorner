namespace MVC_ChiefsCorner.Models
{
    public class OrderExtra
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderMenuId { get; set; }
        public int ExtraId { get; set; }
        public int Quantity { get; set; }

        public virtual OrderMenu OrderMenu { get; set; }
        public virtual Extra Extra { get; set; }
    }
}
