using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CAR_RENTAL_MS_III.Services
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IAuthRepository authRepository,IConfiguration configuration, ApplicationDbContext context)
        { 
            _authRepository = authRepository;
            _configuration = configuration;
            _context = context;
        }



        


    }
}
