using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }


        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
