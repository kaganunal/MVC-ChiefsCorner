using System.ComponentModel.DataAnnotations;

namespace MVC_ChiefsCorner.Models
{
    public class Menu
    {
        //Restoran menüsündeki yemeklerin tutulduğu tablo.Her menü, bir kategoriye aittir.Bu yüzden Menu sınıfında, MenuCategoryId özelliği ile kategoriye referans verilir. Bununla birlikte, her bir menüde ekstra malzemeler de olabilir.Bu yüzden Menu sınıfında, ICollection<MenuExtra> MenuExtras özelliği de yer alır.
        public int Id { get; set; }
        [Required(ErrorMessage = "Menu name is required")]
        [StringLength(50, ErrorMessage = "Menu name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Photo")]
        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "Menu category is required")]
        [Display(Name = "Category")]
        public bool IsAvailable { get; set; } = true;

        public int PreparationTime { get; set; } = 15;

        public int MenuCategoryId { get; set; }
        public virtual MenuCategory? MenuCategory { get; set; }
        public virtual ICollection<OrderMenu>? OrderMenus { get; set; }
    }
}
