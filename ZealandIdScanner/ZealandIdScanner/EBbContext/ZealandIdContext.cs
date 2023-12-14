using System.Collections.Generic;
using ZealandIdScanner.Models;
using Microsoft.EntityFrameworkCore;

namespace ZealandIdScanner.EBbContext
{
    public class ZealandIdContext : DbContext
    {
        public ZealandIdContext(DbContextOptions<ZealandIdContext> options) : base(options)
        {
        }

        // DbSet properties and other configurations...     
        public DbSet<Sensors> Sensorer { get; set; }
        public DbSet<Lokaler> lokaler { get; set; }
    }
}
