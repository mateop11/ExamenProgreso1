using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExamenProgreso1.Models;

namespace ExamenProgreso1.Data
{
    public class ExamenProgreso1Context : DbContext
    {
        public ExamenProgreso1Context (DbContextOptions<ExamenProgreso1Context> options)
            : base(options)
        {
        }

        public DbSet<ExamenProgreso1.Models.Celular> Celular { get; set; } = default!;
        public DbSet<ExamenProgreso1.Models.MPillajo> MPillajo { get; set; } = default!;
    }
}
