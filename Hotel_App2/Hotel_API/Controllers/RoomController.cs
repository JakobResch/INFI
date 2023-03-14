﻿using Hotel_API.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel_Klasse;

namespace Hotel_API.Controllers {
    [Route("api/room")]
    [ApiController]
    public class RoomController : Controller {

        private HotelContext HotelContext;

        // DI - Teil 2
        // Konstruktor - Injektion
        // da wir beim DI - Container ShopContext registriert haben
        // injiziert er uns die Instanz, da der Parameter des ctor's
        // ihm bekannt ist
        public RoomController(HotelContext hotelContext) {
            this.HotelContext = hotelContext;
        }




        [HttpGet]
        [Route("rooms")]
        public async Task<IActionResult> AllArticlesAsync() {
            //die Liste der Artikel werden nach JSON konvertiert
            return new JsonResult(this.HotelContext.Rooms);

        }

        [HttpGet]
        [Route("room/{id}")]
        public async Task<IActionResult> GetArticlesBIdAsync(int id) {
            var a = await this.HotelContext.Rooms.FindAsync(id);
            if (a != null) {
                return new JsonResult(a);
            }

            return new JsonResult(false);
        }


        // einen Artikel löschen         
        [HttpDelete]
        [Route("room/delete/{id}")]
        public async Task<IActionResult> DeleteArticleByIdAsync(int id) {

            var article = this.HotelContext.Rooms.Find(id);
            if (article != null) {
                this.HotelContext.Rooms.Remove(article);
                int result = await SaveToDbAsync();
                return new JsonResult(1 == result);
            }
            return new JsonResult(false);

        }

        // einen neuen Artikel hinzufügen 
        [HttpPost]
        [Route("room/add")]
        public async Task<IActionResult> InsertArticleByIdAsync(Room room) {

            if (room == null) {
                return new JsonResult(false);
            }

            await this.HotelContext.Rooms.AddAsync(room);
            int result = await SaveToDbAsync();
            return new JsonResult(result == 1);
        }


        // einen vorhandenen Artikel ändern
        [HttpPut]
        [Route("room/update/{id}")]
        public async Task<IActionResult> UpdateArticleByIdAsync(int id, Room room) {
            var a = this.HotelContext.Rooms.Find(id);
            if (a == null || room == null) {
                return new JsonResult(false);
            }
            a.RoomNum = room.RoomNum;
            a.bedNum = room.bedNum;
            a.kitchen = room.kitchen;
            a.balcony = room.balcony;
            a.terrace = room.terrace;
            a.pricePerNight = room.pricePerNight;
            this.HotelContext.Rooms.Update(a);
            int result = await SaveToDbAsync();
            return new JsonResult(result == 1);
        }

        private async Task<int> SaveToDbAsync() {
            try {
                return await this.HotelContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                // loggen
                Console.WriteLine("Datenbankfehler: konkurrenter Zugriff!");
                return 0;
            } catch (DbUpdateException) {
                // loggen
                Console.WriteLine("Datenbankfehler: Update fehlgeschlagen!");
                return 0;

            } catch (OperationCanceledException) {
                // loggen
                Console.WriteLine("Datenbankfehler: Operation abgebrochen!");
                return 0;
            }

        }
    }
}
    
