using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class StateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
