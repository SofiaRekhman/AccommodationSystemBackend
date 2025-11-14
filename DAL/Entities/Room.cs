namespace DAL.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int DormitoryId { get; set; }
        public bool IsActive { get; set; }

        public Dormitory Dormitory { get; set; }
        public ICollection<Bed> Beds { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
