namespace DAL.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
