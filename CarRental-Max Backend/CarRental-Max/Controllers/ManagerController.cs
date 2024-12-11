using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
           _managerService = managerService;
        }


    }
}

