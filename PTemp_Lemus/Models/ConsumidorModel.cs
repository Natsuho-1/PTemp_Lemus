using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTemp_Lemus.Models
{
    [Table(name:"c_Consumidor")]
    public class ConsumidorModel
    {
        [Key]
        public int idConsumidor { get; set; }
        public string nombreConsumidor { get; set; }
        public string apellidoConsumidor { get; set; }
        public string direccion {  get; set; }
        public string correoElectronico { get; set; }
        public string duiConsumidor { get; set; }
        public bool activo { get; set; }
    }
}