using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1Website_Vogt.Models {
    public class User {
        private int userID;
        public int UserID {
            get { return this.userID; }
            set {
                if (value >= 0) {
                    this.userID = value;
                }
            }
        }

        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Ort { get; set; }
        public string Strasse { get; set; }
        public int Hausnummer { get; set; }
        public int Postleitzahl { get; set; }
        public string Email { get; set; }
        public Zahlungsmethode Zahlung { get; set; }


    }
}