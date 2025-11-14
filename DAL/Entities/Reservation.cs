namespace DAL.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int? UserId { get; set; }
        public int RoomId { get; set; }
        public int BedId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string? AdminComment { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDateTime { get; set; } = DateTime.UtcNow;

        public Room Room { get; set; }
        public Bed Bed { get; set; }
        public Status Status { get; set; }
    }
}
