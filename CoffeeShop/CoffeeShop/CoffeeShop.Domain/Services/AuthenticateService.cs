using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Interfaces;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Domain.Mappers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.Domain.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AuthenticateService> logger;
        private readonly IConfiguration configuration;
        private readonly UserStore<User> userStore;
        // private readonly UserStore<User> userStore;

        public AuthenticateService(UserManager<User> userManager,
            IConfiguration configuration, SignInManager<User> signInManager,
            ILogger<AuthenticateService> logger, UserStore<User> userStore, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
            this.configuration = configuration;
            this.userStore = userStore;
            this.roleManager = roleManager;
        }

        public async Task<BaseResponse> CreateNewRole(RoleRequest request)
        {
            var response = new BaseResponse();

            var isRoleExist = await roleManager.RoleExistsAsync(request.Name.Trim());

            if (!isRoleExist)
            {
                var res = await roleManager.CreateAsync(new IdentityRole(request.Name));

                if (!res.Succeeded)
                {
                    return new BaseResponse
                    {
                        StatusCode = ResponseStatus.Fail,
                        Message = "Failed to create role, please try again later."
                    };
                }

                response.StatusCode = ResponseStatus.Success;
            }
            else
            {
                response.StatusCode = ResponseStatus.Fail;
                response.Message = "This role already exist.";

            }

            return response;
        }

        public ObjectListResponse<UserResponse> Filter(UserFilter filter)
        {
            var response = new ObjectListResponse<UserResponse>();

            var userRes = new List<UserResponse>();

            var res = userManager.Users.ToList();

            foreach (var user in res)
            {

                userRes.Add(
                    new UserResponse
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
            }
            response.Data = userRes;
            response.StatusCode = Enums.ResponseStatus.Success;

            return response;
        }

        public ObjectResponse<UserResponse> GetUserById(string id)
        {
            ObjectResponse<UserResponse> response = new ObjectResponse<UserResponse>();

            try
            {

                var user = userManager.FindByIdAsync(id).Result;

                if (user == null)
                {
                    response.StatusCode = ResponseStatus.NoneExist;
                    response.Message = $"The user you're trying to access this resources doesn't exist.";
                    return response;
                }

                response.StatusCode = ResponseStatus.Success;
                var data = user.Map();
                data.Token = GenerateToken(user);
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.Fail;
                response.Message = "An exception has occured trying to sign in user.";
                logger.LogError(ex.Message);
            }

            return response;
        }

        public ObjectResponse<UserResponse> Login(LoginRequest request)
        {
            ObjectResponse<UserResponse> response = new ObjectResponse<UserResponse>();

            try
            {
                var result = signInManager
                    .PasswordSignInAsync(request.Username, request.Password, false, lockoutOnFailure: false)
                    .Result;

                if (!result.Succeeded)
                {
                    response.StatusCode = ResponseStatus.Fail;
                    response.Message = "Username and password does not match, please provide correct details.";
                    return response;
                }


                var user = userManager.FindByNameAsync(request.Username).Result;
                response.StatusCode = ResponseStatus.Success;
                var data = user.Map();
                data.Token = GenerateToken(user);
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.Fail;
                response.Message = "An exception has occured trying to sign in user.";
                logger.LogError(ex.Message);
            }

            return response;
        }

        public async Task<ObjectResponse<UserResponse>> Register(UserRequest request)
        {
            ObjectResponse<UserResponse> response = new ObjectResponse<UserResponse>();

            try
            {

                var user = new User
                {
                    FirstName = request.FirstName.Trim(),
                    LastName = request.LastName.Trim(),
                    UserName = request.Email.Trim(),
                    PhoneNumber = request.Phone.Trim(),
                    Email = request.Email.Trim(),
                    EmailConfirmed = true
                };

                var results = await userManager.CreateAsync(user, request.Password);

                if (!results.Succeeded)
                {

                    string message = "Failed to create user, please try again.";

                    if (results.Errors != null && results.Errors.Any())
                    {
                        StringBuilder stringBuilder = new StringBuilder();

                        foreach (var error in results.Errors)
                        {
                            stringBuilder.AppendLine(error.Description);
                        }

                        message = stringBuilder.ToString();
                    }

                    response.Message = message;
                    response.StatusCode = ResponseStatus.Fail;
                    return response;
                }

                var roles = roleManager.Roles.ToList();

                if (request.Roles == null || request.Roles.Count() == 0)
                {
                    var role = roles
                        .FirstOrDefault(
                        x => x.Name.Contains("Data",
                        StringComparison.InvariantCultureIgnoreCase));

                    var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                }
                else
                {
                    foreach (var roleId in request.Roles)
                    {
                        var role = roles.FirstOrDefault(x => x.Id == roleId);
                        var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                    }
                }


                response.StatusCode = ResponseStatus.Success;
                response.Message = "You have been registered successfully.";

            }
            catch (Exception ex)
            {
                response.StatusCode = ResponseStatus.Fail;
                response.Message = "An exception has occured trying to register user, please try again later.";
                logger.LogError(ex.Message);
            }

            return response;
        }

        public ObjectResponse<UserResponse> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        private string GenerateToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = configuration.GetValue<string>("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.UserName.ToString()),
                    new Claim("userNamePhone", user.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
