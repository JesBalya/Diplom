using Diplom.ViewModels.Account;
using System.Security.Claims;
using System.Threading.Tasks;
using Diplom.Services.Implementations;

namespace Diplom.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}



