using System.ComponentModel.DataAnnotations;

namespace MVC_ChiefsCorner.Models
{
    public class MenuCategory
    {
        //Menülerin kategorilerini tutan tablo. Her kategori birden çok menüye sahip olabilir. Bu yüzden MenuCategory sınıfında, ICollection<Menu> Menus özelliği yer alır.
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
