namespace DAL.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public int StatusId { get; set; }
        public string? AdminComment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Room Room { get; set; }
        public Status Status { get; set; }
    }
}
