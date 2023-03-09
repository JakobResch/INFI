using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1Website_Vogt.Models {
    public class Gericht {
        private int gerichtID;
        public int GerichtID {
            get { return this.gerichtID; }
            set {
                if (value >= 0) {
                    this.gerichtID = value;
                }
            }
        }

        public string gerichtName { get; set; }
        public double preis { get; set; }
    }
}
