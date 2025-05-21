namespace HW_Backend.Models.DTO
{
    public class RentedMovieDTO
    {
        public int UserID { get; set; }
        public int MovieID { get; set; }
        public DateTime RentStart { get; set; }
        public DateTime RentEnd { get; set; }
        public double TotalPrice { get; set; }
    }
}
