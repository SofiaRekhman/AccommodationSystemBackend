namespace BLL.Models
{
    public class GetReservationResponseModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int RoomId { get; set; }
        public int BedId { get; set; }
        public string ReservationStartDate { get; set; }
        public string ReservationEndDate { get; set; }
    }
}

