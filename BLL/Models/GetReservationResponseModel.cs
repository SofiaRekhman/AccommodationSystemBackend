namespace BLL.Models
{
    public class GetReservationResponseModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int RoomNumber { get; set; }
        public int BedNumber { get; set; }
        public string ReservationStartDate { get; set; }
        public string ReservationEndDate { get; set; }
        public string ReservationStatusName { get; set; }
    }
}

