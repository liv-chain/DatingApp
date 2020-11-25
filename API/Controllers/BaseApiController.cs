using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
namespace API.Controllers
{
    [ApiController]
    [EnableCors]    
    [Route("api/[controller]")] 
    public class BaseApiController : ControllerBase
    {
        
    }
}