using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend_aflevering_2.Models;

namespace Backend_aflevering_2.Data
{
    public class Backend_aflevering_2Context : DbContext
    {
        public Backend_aflevering_2Context (DbContextOptions<Backend_aflevering_2Context> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expense { get; set; } = default!;

        public DbSet<Job> Job { get; set; } = default!;

        public DbSet<Model> Model { get; set; } = default!;
    }
}
