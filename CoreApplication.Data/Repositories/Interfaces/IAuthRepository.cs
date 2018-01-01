using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginUser> Register(LoginUser user,string password);
        
        Task<LoginUser> Login(string userName,string password);

        Task<bool> UserExists(string username);
    }
}   