using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webApp.Data;
using webApp.Data.Base;

namespace webApp.Models
{
    public class Movie:IEntityBase
    {
     [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; } 
        [Display(Name = "Price")]
        public double Price { get; set; }   
        [Display(Name = "ImageURL")]
        public string? ImageURL { get; set; }
        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }
        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }
        [Display(Name = "MovieCategory")]
        public MovieCategory MovieCategory { get; set; } 
        //relationships
        public List<Actor_Movie>? Actors_Movies { get; set; }
        //Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema? Cinema { get; set; }
        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer? Producer { get; set; }

    }
}