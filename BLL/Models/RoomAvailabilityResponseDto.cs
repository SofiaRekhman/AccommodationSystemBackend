using DAL.Entities;

namespace BLL.Models
{
    public class RoomAvailabilityResponseDto
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public List<Bed> AvailableBeds { get; set; } = new List<Bed>();
    }
}
