namespace Hotel_API.Models
{
    public class Room {

        public int roomNum { get; set; }
        public int bedNum { get; set; }

        public bool kitchen { get; set; }

        public bool balcony { get; set; }

        public bool terrace { get; set; }
        public decimal pricePerNight { get; set; }


    }

}
