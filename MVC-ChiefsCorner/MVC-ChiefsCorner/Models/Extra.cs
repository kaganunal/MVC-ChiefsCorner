using System.ComponentModel.DataAnnotations;

namespace MVC_ChiefsCorner.Models
{
    public class Extra
    {
        //Menülere ek olarak, müşterilerin sipariş edebileceği ekstra malzemeleri tutan tablo. Bu tabloda her bir ekstra, bir ad ve fiyat içerir. Ayrıca her ekstra, siparişlerde de kullanılabilir. Bu yüzden Extra sınıfında, ICollection<OrderExtra> OrderExtras özelliği de yer alır.
        public int Id { get; set; }

        [Required(ErrorMessage = "Extra name is required")]
        [StringLength(50, ErrorMessage = "Extra name cannot be longer than 50 characters")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<OrderExtra> OrderExtras { get; set; }
        public virtual ICollection<MenuExtra> MenuExtras { get; set; }
    }
}
