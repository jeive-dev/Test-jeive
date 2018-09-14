using jeive.Include;
using Microsoft.AspNetCore.Mvc;
using static jeive.Include.Variable;

namespace jeive.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class SystemController : Controller
    {
        private readonly IMethod bc;

        public SystemController(IMethod x)
        {
            bc = x;
        }

        [HttpPost]
        public IActionResult Config([FromBody] Config set)
        {
            if(set == null )
            {
                return BadRequest();
            }
            else
            {
                bc.Add(set);
            }
            return null;
        }


    }
}