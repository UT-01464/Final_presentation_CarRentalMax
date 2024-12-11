using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class ManagerRepository:IManagerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ManagerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }


    }
}
