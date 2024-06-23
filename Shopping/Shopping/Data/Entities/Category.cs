using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Ctegoría")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
    }
}
