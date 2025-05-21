namespace HW_Backend.Models.DTO
{
    public class RentMovieUpdateDTO
    {
        public int RentingUserID { get; set; }
        public int RentToUserID { get; set; }
        public int MovieID { get; set; }
    }
}
