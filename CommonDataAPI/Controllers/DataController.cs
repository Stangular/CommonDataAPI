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
    [Route("api/data")]
    public class DataController : Controller
    {
        IScheduleRepository _eventRepo;
        private ILogger<DataController> _logger;

        //  private readonly myScheduleContext _context;

        public DataController(IScheduleRepository eventRepo,
                ILogger<DataController> logger)
        {
            _eventRepo = eventRepo;
            _logger = logger;

            //       _context = context;
        }

        private JwtSecurityToken getAuthToken(Microsoft.AspNetCore.Http.IHeaderDictionary header)
        {
            var jwt = new JwtSecurityTokenHandler();
            string auth = header["Authorization"];
            if (auth == null)
            {
                return null;
            }
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
                if (token != null)
                {
                    userID = token.Payload.First(p => p.Key == "sub").Value.ToString();
                }
            }
            catch (System.Exception ex)
            {
                // TODO: log error...
            }
            return userID;
        }

        // GET: api/ScheduledEvents
        [HttpGet]
        [EnableCors("MyPolicy")]
   //     [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Get()
        {
            _logger.LogInformation("mySchedule Controller initial Get...");
            try
            {
                // Return _recordRepo.InitialResponse(getUserID(Request.Headers));
                return Ok(_eventRepo.UserSchedule(getUserID(Request.Headers)));
            }
            catch(System.Exception ex)
            {
               //  return Json(ex.Message);

            }

            return BadRequest("User Schedule not available");

        }

        // GET: api/ScheduledEvents/5
        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public JsonResult Get([FromRoute] int id) // 
        {
            // formName,List<string>lookupdata
            // Return _recordRepo.Response();
            return Json("");
        }

       // PUT: api/ScheduledEvents/5
        [HttpPut("{id}")]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult PutScheduledEvent([FromRoute] int id)
        {
            return BadRequest("Nothing to see here");
        }

        // POST: api/data
        [HttpPost]
        [EnableCors("MyPolicy")]
 //       [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Post([FromBody] Form form)
        {
            _logger.LogInformation("Saving form");
            try
            {
                if (_eventRepo.AddFromForm(form, getUserID(Request.Headers)))
                {
                    return Ok();
                }
                else
                {
                    _logger.LogInformation("Add Scheduled Event failed...");
                }
            }
            catch(System.Exception ex)
            {
                LogException("Exception thrown while adding data");
            }

            return BadRequest("Data was not saved");
        }

        private void LogException(string message)
        {
            if (null != _logger)
            {
                _logger.LogError(message);
            }
        }
        // DELETE: api/ScheduledEvents/5
        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        [Authorize(Policy = "RegisteredDataUser")]
        public JsonResult DeleteScheduledEvent([FromRoute] int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var scheduledEvent = await _context.ScheduledEvent.SingleOrDefaultAsync(m => m.Id == id);
            //if (scheduledEvent == null)
            //{
            //    return NotFound();
            //}

            //_context.ScheduledEvent.Remove(scheduledEvent);
            //await _context.SaveChangesAsync();
            return Json("");
            //return Ok();
        }
    }
}
