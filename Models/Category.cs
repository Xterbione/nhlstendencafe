using System.ComponentModel.DataAnnotations;


namespace nhl_stenden_cafe.Pages.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required, MinLength(2), MaxLength(128)]
        public string Name { get; set; }
    }
}