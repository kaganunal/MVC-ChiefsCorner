namespace MVC_ChiefsCorner.Models
{
    public class Order
    {
        //Müşterilerin siparişlerini tutan tablo. Her siparişin, bir tarih-saat bilgisi ve bir kullanıcıya ait bilgi bulunur. Bu yüzden Order sınıfında, UserId özelliği ile kullanıcıya referans verilirken, AppUser sınıfından bir User nesnesi de User özelliği ile tutulur. Ayrıca her siparişin birden çok menü ve ekstra malzemesi de olabilir. Bu yüzden Order sınıfında, ICollection<OrderMenu> OrderMenus ve ICollection<OrderExtra> OrderExtras özellikleri de yer alır.
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }
        public decimal OrderTotal { get; set; } = 0;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<OrderMenu> OrderMenus { get; set; }


        public Order()
        {

        }
        public void CalculateOrderTotal()
        {
            decimal total = 0;

            foreach (var orderMenu in OrderMenus)
            {
                total += orderMenu.Menu.Price * orderMenu.Quantity;
            }

            OrderTotal = total;
        }

        public void CalculateQuantity()
        {
            Quantity = OrderMenus?.Sum(om => om.Quantity) ?? 0;
        }

    }

    public enum OrderStatus
    {
        Pending = 0,
        Completed = 1,
    }

}
