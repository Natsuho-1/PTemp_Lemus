using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTemp_Lemus.Models
{
    [Table(name:"c_Estado")]
    public class EstadoModel
    {
        [Key]
        public int idEstado { get; set; }
        public string nombreEstado { get; set; }
    }
}