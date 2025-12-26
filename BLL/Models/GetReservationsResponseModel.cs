namespace BLL.Models
{
    public class GetReservationsResponseModel
    {
        public int ReservationId { get; set; }
        public string ReservationStartDate { get; set; }
        public string ReservationStatusName { get; set; }
        public string CreatedAt { get; set; }
    }
}
