namespace DAL.Entities
{
    public class Dormitory
    { 
        public int DormitoryId { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public int TotalRooms { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
