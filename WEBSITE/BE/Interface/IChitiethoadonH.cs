using Microsoft.AspNetCore.Mvc;

namespace BE.Interface
{
    public interface IChitiethoadonH 
    {
        Task<String> addProduct(String Iduser, String idctsp);
        Task<bool> kTracthd(string idUser, string idsp);
        Task<IEnumerable<object>> getGiohang(string idUser);
    }
}
