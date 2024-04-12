using Microsoft.AspNetCore.Http;
using Diplom.Models.Account;

namespace Diplom.Services.Interfaces
{
    public interface IBaseResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}
