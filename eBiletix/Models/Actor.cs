using eBiletix.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eBiletix.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile picture is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        //relationships

        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
