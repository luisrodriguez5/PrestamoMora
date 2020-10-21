using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoMora.Entidades
{
    public class Moras 
    {
        [Key]
        public int MoraId { get; set; }

        [Required(ErrorMessage = "Prestamo es requerido")]
        public int PretamoId { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }


        [ForeignKey("MoraId")]
        public virtual List<MorasDetalle> MorasDetalles { get; set; }



        public Moras()
        {
            MoraId = 0;
            PretamoId = 0;
            Total = 0;
            Fecha = DateTime.Now;
            MorasDetalles = new List<MorasDetalle>();
        }
        
    }
}