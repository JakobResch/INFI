using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1Website_Vogt.Models.DB;

using System.Data.Common;
using _1Website_Vogt.Models;

namespace _1Website_Vogt.Controllers {
    public class BestellungsController : Controller {

        private RepositoryUserDB repo = new RepositoryUserDB();
        public async Task<IActionResult> Index() {
            try {
                await repo.ConnectAsync();
                return View(await repo.GetAllUsersAsync());
            } catch (DbException) {
                return View("_Message", new Message("Datenbankfehler", "die Benutzer konnten nicht geladen" +
                    "werden, Versuchen sie es später erneut."));
            }
            finally {
                await repo.DisconnectAsync();
            }
        }

        [HttpGet]
        public IActionResult Registration() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User userDataFromForm) {
            if (userDataFromForm == null) {
                return RedirectToAction("Registration");
            }

            ValidateRegistrationData(userDataFromForm);
            if (ModelState.IsValid) {
                try {
                    await repo.ConnectAsync();
                    if (await repo.InsertAsync(userDataFromForm)) {
                        return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgespeichert"));
                    } else {
                        return View("_Message", new Message("Registrierung", "Ihre Daten NICHT wurden erfolgreich abgespeichert", "Bitte versuchen sie es später erneut!"));
                    }
                } catch (DbException ex) {
                    return View("_Message", new Message("Registrierung", "Datenbankfehler!" + ex.Message, "Bitte versuchen sie es später erneut!"));
                }
                finally {
                    await repo.DisconnectAsync();
                }


            }
            return View(userDataFromForm);
        }
        private void ValidateRegistrationData(User user) {
            if (user == null) {
                return;
            }
            if (user.Vorname == null || (user.Vorname.Trim().Length < 4)) {
                ModelState.AddModelError("Vorname", "Der Vorname muss mind. 4 Zeichen lang sein!");
            }
            if (user.Nachname == null || user.Nachname.Trim().Length < 4) {
                ModelState.AddModelError("Nachname", "Der Nachname muss mind. 4 Zeichen lang sein");
            }
            if (user.Ort == null) {
                ModelState.AddModelError("Ort", "Kein Ort wurde eingetragen");
            }
            if (user.Hausnummer == null) {
                ModelState.AddModelError("Hausnummer", "Es wurde keine Hausnummer eingegeben");
            }
            if (user.Postleitzahl == null) {
                ModelState.AddModelError("Postleitzahl", "Es wurde keine Postleitzahl angegeben");
            }
            if (user.Email == null) {
                ModelState.AddModelError("Email", "Es wurde keine Email-Adresse angegeben");
            }
            if (user.Zahlung == null) {
                ModelState.AddModelError("Zahlung", "Es wurde keine Zahlungsart angegeben");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Bestellung(Gericht gerichtData) {
            try {
                await repo.ConnectAsync();
                if (await repo.InsertAsync(gerichtData)) {
                    return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgespeichert"));
                } else {
                    return View("_Message", new Message("Registrierung", "Ihre Daten NICHT wurden erfolgreich abgespeichert", "Bitte versuchen sie es später erneut!"));
                }
            } catch (DbException ex) {
                return View("_Message", new Message("Registrierung", "Datenbankfehler!" + ex.Message, "Bitte versuchen sie es später erneut!"));
            }
            finally {
                await repo.DisconnectAsync();
            }

            return View(gerichtData);
        }


        public IActionResult Bestellbestaetigung(User user) {
            try {
                repo.ConnectAsync();
                return View(repo.GetUser(user.UserID));
            } catch (DbException) {
                return View("_Message", new Message("Datenbankfehler", "die Benutzer konnten nicht geladen" +
                    "werden, Versuchen sie es später erneut."));
            }
            finally {
                repo.DisconnectAsync();
            }

        }

        public IActionResult Formular() {
            return View();
        }
    }
}