namespace DAL.Entities
{
    public class Bed
    {
        public int BedId { get; set; }
        public int RoomId { get; set; }
        public bool IsOccupied { get; set; }

        // Navigation
        public Room Room { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
