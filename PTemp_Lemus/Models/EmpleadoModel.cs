using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PTemp_Lemus.Models
{
    [Table("c_Empleado")]
    public class EmpleadoModel
    {
        [Key]
        public int idEmpleado {  get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public bool activo { get; set; }
    }
}