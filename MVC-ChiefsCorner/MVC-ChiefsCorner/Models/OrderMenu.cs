namespace MVC_ChiefsCorner.Models
{
    public class OrderMenu
    {
        //Siparişlerdeki menülerin bilgilerini tutan tablo. Her bir siparişte birden çok menü olabilir ve her bir menü de siparişlerde birden çok defa kullanılabilir. Bu yüzden OrderMenu sınıfında, OrderId özelliği ile siparişe referans verilirken, MenuId özelliği ile de menüye referans verilir. Ayrıca menülerin boyutları da farklı olabilir. Bu yüzden Size özelliği de yer alır. 
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public Size Size { get; set; }
        //public virtual ICollection<OrderExtra> OrderExtras { get; set; }

        //OrderMenu class'ında OrderExtra ICollection'u, sipariş verilen menüye ekstra malzemeler eklemek için kullanılır. Yani bir siparişteki her bir menüye ekstra malzemeler eklenebilir ve bu bilgiler OrderExtra tablosunda tutulur. Örneğin, bir kişi büyük boy pizza siparişi verdiğinde, ekstra mantar, zeytin ve peynir eklemek isteyebilir. Bu durumda OrderExtra tablosu, siparişin hangi menüsüne (pizza) hangi ekstraların eklendiğini, kaç adet ekstra eklenildiğini vb. bilgileri tutar.
    }
}
