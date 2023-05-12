using System.ComponentModel.DataAnnotations;

namespace MVC_ChiefsCorner.Models
{
    public class MenuExtra
    {
        //Menüler ve ekstra malzemeler arasındaki ilişkiyi tutan tablo. Her bir menüde birden çok ekstra malzeme olabilir ve her bir ekstra malzeme birden çok menüde kullanılabilir. Bu yüzden MenuExtra sınıfında, MenuId özelliği ile menüye referans verilirken, ExtraId özelliği ile de ekstra malzemeye referans verilir.
        public int Id { get; set; }
        [Required(ErrorMessage = "Menu is required")]
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        public int ExtraId { get; set; }
        public virtual Extra Extra { get; set; }
    }
}
