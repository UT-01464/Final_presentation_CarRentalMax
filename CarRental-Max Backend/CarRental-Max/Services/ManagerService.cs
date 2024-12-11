
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models;

namespace CAR_RENTAL_MS_III.Services
{
    public class ManagerService:IManagerService
    {
        private readonly IManagerRepository _managerRepository;
       

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
           

        }

       
    }
}
