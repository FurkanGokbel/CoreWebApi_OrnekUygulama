using HotelFinderEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelFinder.DataAccess
{
   public class HotelDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-32DSDLJ\\SQLEXPRESS; database=HotelDb; integrated security=true;");

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
