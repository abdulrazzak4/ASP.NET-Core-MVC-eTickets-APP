using System.ComponentModel.DataAnnotations;
using webApp.Data.Base;

namespace webApp.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Logo")]
        [Required(ErrorMessage ="Cinema Logo is required")]
        public string? Logo { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage ="Cinema Name is required")]

        public string? Name { get; set; }
        [Required(ErrorMessage ="Cinema Description is required")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        //relationships
        public List<Movie>? Movies { get; set; }
    }
}