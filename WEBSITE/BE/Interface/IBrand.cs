using BE.Models;

namespace BE.Interface
{
    public interface IBrand
    {
        Task<IEnumerable<Nhanhieu>> GetAllBrands();
    }
}
