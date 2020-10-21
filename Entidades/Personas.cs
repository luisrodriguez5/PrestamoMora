using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoMora.Entidades
{
    public class Personas
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Favor ingrese el Nombre...")] 
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Favor ingrese la Cedula...")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Favor ingrese el Telefono...")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Favor ingrese la Direccion...")]
        public string Direccion { get; set; }

        public decimal Balance { get; set; }

        public DateTime FechaNacimiento { get; set; }

        

        public Personas()
        {
            Id = 0;
            Nombre = string.Empty;
            Cedula = string.Empty;
            Telefono = string.Empty;
            Direccion = string.Empty;
            Balance = 0;
            FechaNacimiento = DateTime.Now;
            
        }
        
    }
}