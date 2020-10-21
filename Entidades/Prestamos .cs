using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PrestamoMora.Entidades
{
    public class Prestamos 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor ingresar el ID...")]
        public int PersonaId { get; set; } 

        [Required (ErrorMessage ="Favor ingresar el copcento")]
        public string Concepto { get; set; }

        [Required(ErrorMessage = "Favor colocar el Monto...")]
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public DateTime Fecha { get; set; }

        public Prestamos()
        {
            Id = 0;
            PersonaId = 0;
            Concepto = string.Empty;
            Monto = 0;
            Balance = 0;
            Fecha = DateTime.Now;
        }
        
    }
}