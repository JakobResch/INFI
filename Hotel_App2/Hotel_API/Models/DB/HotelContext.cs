using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Hotel_Klasse;

namespace Hotel_API.Models.DB {
    public class HotelContext : DbContext {
        public DbSet<Room> Rooms { get; set; }


        // in dieser Methode wird der DB-Server und die zugehörigen Daten angegeben
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // für den Pomelo-MySQL-Treiber
            string connectionString = "Server=localhost;database=hotel;user=root";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
