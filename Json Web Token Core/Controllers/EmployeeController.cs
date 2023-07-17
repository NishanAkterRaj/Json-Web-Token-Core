using Json_Web_Token_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Json_Web_Token_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenticated with JWT";
        }
        [Authorize]
        [HttpPost]
        public string AddUser(Users user)
        {
            return "User added With User name" + user.Username;
        }
    }
}
