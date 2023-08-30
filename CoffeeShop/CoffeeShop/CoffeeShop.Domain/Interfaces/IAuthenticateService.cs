using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces
{
    public interface IAuthenticateService
    {
        ObjectResponse<UserResponse> Login(LoginRequest request);
        ObjectResponse<UserResponse> Register(RegisterRequest request);
        Task<ObjectResponse<UserResponse>> Register(UserRequest request);
        ObjectListResponse<UserResponse> Filter(UserFilter filter);
        Task<BaseResponse> CreateNewRole(RoleRequest request);
        ObjectResponse<UserResponse> GetUserById(string id);
    }
}
