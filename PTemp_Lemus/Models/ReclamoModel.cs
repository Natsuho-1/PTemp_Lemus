using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTemp_Lemus.Models
{
    [Table(name:"t_Reclamo")]
    public class ReclamoModel
    {
        [Key]
        public int idReclamo { get; set; }
        [Required]
        public string nombreProveedor { get; set; }
        [Required]
        public string direccionProveedor { get; set; }
        [Required]
        public string detalleReclamo { get; set; }
        public string telefonoProveedor { get; set; }
        public decimal montoReclamo { get; set; }
        [Required]
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaRevision { get; set; }
        public int idEmpleado { get; set; }
        public int idConsumidor { get; set; }
        public int idEstado {  get; set; }
        [Required]
        public bool activo { get; set; }
    }
}