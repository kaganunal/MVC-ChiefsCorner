namespace MVC_ChiefsCorner.Models
{
    public class Order
    {
        //Müşterilerin siparişlerini tutan tablo. Her siparişin, bir tarih-saat bilgisi ve bir kullanıcıya ait bilgi bulunur. Bu yüzden Order sınıfında, UserId özelliği ile kullanıcıya referans verilirken, AppUser sınıfından bir User nesnesi de User özelliği ile tutulur. Ayrıca her siparişin birden çok menü ve ekstra malzemesi de olabilir. Bu yüzden Order sınıfında, ICollection<OrderMenu> OrderMenus ve ICollection<OrderExtra> OrderExtras özellikleri de yer alır.
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<OrderMenu> OrderMenus { get; set; }
        public virtual ICollection<OrderExtra> OrderExtras { get; set; }

    }
}
