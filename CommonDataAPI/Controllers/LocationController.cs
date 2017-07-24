using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using DataRecord;
using Microsoft.AspNetCore.Cors;
using myScheduleModels.Models.Interfaces;

namespace CommonDataAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Location")]
    public class LocationController : Controller
    {
        ILocationRepository _locRepo;
        private ILogger<DataController> _logger;

        //  private readonly myScheduleContext _context;

        public LocationController(ILocationRepository locRepo,
                ILogger<DataController> logger)
        {
            _locRepo = locRepo;
            _logger = logger;

            //       _context = context;
        }
        
        private JwtSecurityToken getAuthToken(Microsoft.AspNetCore.Http.IHeaderDictionary header)
        {
            var jwt = new JwtSecurityTokenHandler();
            string auth = header["Authorization"];
            if (auth.IndexOf("bearer") == 0)
            {
                auth = auth.Substring(7).Trim();// remove 'bearer '...
            }
            return jwt.ReadJwtToken(auth);
        }

        private string getUserID(Microsoft.AspNetCore.Http.IHeaderDictionary header)
        {
            string userID = "";
            try
            {
                var token = getAuthToken(header);
                userID = token.Payload.First(p => p.Key == "sub").Value.ToString();
            }
            catch (System.Exception ex)
            {
                // TODO: log error...
            }
            return userID;
        }
        [HttpGet]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_locRepo.UserLocations(getUserID(Request.Headers)) );
            }
            catch (System.Exception ex)
            {
                //  return Json(ex.Message);

            }
            return BadRequest("Method not active");
        }

        // GET: api/List/5
        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Get([FromRoute] int id) // 
        {
  
            return BadRequest("Method not active");

        }

        // POST: api/List
        [HttpPost]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Post([FromBody]string value)
        {
            return BadRequest("Method not active");
        }

        // PUT: api/List/5
        [HttpPut("{id}")]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return BadRequest("Method not active");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Delete(int id)
        {
            return BadRequest("Method not active");
        }
    }
}
