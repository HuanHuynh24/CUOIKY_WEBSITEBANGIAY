namespace BE.Models
{
    public class ChitietSanphamWithReviews
    {
        public Chitietsanpham ChitietSanpham { get; set; }
        public List<Hinhanh> hinhanhs { get; set; }
        public List<ReviewDTO> Reviews { get; set; }


    }
}
