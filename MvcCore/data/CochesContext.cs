using Microsoft.EntityFrameworkCore;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.data
{
    public class CochesContext : DbContext
    {
        public CochesContext(DbContextOptions<CochesContext> options) : base(options) { }

        public DbSet<Coche> Coches { get; set; }
    }
}
