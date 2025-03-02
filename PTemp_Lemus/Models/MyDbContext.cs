using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PTemp_Lemus.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext() : base("name=DBTemp_Lemus")
        {

        }
        public DbSet <EmpleadoModel> EmpleadoModel { get; set; }
        public DbSet<ReclamoModel> ReclamoModel { get; set; }
    }
}