using BE.Models;

namespace BE.Interface
{
    public interface IColor
    {
        // 
        Task<IEnumerable<Color>> GetAllColors();
    }
}
