using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        
    }
}
