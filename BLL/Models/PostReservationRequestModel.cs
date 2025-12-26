namespace BLL.Models
{
    public class PostReservationRequestModel
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int BedId { get; set; }
        public string ReservationReason { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
    }
}
