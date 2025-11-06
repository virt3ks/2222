using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2222
{
    public class AppDbContext : DbContext
    {

        private string _connectiontring => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Everything;Integrated Security=True;Connect Timeout=30;";

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectiontring);
        }
    }
}
