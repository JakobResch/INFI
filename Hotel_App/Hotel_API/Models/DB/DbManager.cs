using Hotel_API.Models;
using Microsoft.EntityFrameworkCore;


namespace Hotel_API.Models.DB {
    public class DbManager : DbContext {


        public DbSet<Room> Rooms { get; set; }


        // in dieser Methode wird der DB-Server und die zugehörigen Daten angegeben
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // für den Pomelo-MySQL-Treiber
            string connectionString = "Server=localhost;database=hotel;user=root";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}

