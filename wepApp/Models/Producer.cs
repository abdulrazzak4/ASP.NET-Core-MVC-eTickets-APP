using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webApp.Data.Base;

namespace webApp.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        [Required(ErrorMessage ="ProfilePictureURL is required")]
        public string? ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage ="Full Name is required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Full Name must between 3 and 50 chars")]
        public string? FullName { get; set; }
        [Display(Name = "Biography")] 
        [Required(ErrorMessage ="Biography is required")]
        public string? Bio { get; set; }
        //relationships
        public List<Movie>? Movies { get; set; }
    }
}