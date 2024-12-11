using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICustomerService _customerService;

        public AuthenticationController(IAuthService authService, ICustomerService customerService)
        {
            _authService = authService;
            _customerService = customerService;
        }



    }
}
