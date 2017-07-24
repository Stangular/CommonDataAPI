using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using myScheduleModels.Models.Interfaces;

using Microsoft.EntityFrameworkCore;
using DataRecord;

namespace CommonDataAPIMirror.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class DataController : Controller
    {
        IScheduleRepository _eventRepo;

        //  private readonly myScheduleContext _context;

        public DataController(IScheduleRepository eventRepo)
        {
            _eventRepo = eventRepo;

            //       _context = context;
        }

        private JwtSecurityToken getAuthToken(Microsoft.AspNetCore.Http.IHeaderDictionary header)
        {
            var jwt = new JwtSecurityTokenHandler();
            string auth = header["Authorization"];
            if( auth == null)
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
      //  [EnableCors("MyPolicy")]
        //     [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Get()
        {
            try
            {
                // Return _recordRepo.InitialResponse(getUserID(Request.Headers));
                return Ok(_eventRepo.UserSchedule(getUserID(Request.Headers)));
            }
            catch (System.Exception ex)
            {
                //  return Json(ex.Message);

            }

            return BadRequest("User Schedule not available");

        }

        // GET: api/ScheduledEvents/5
        [HttpGet("{id}")]
       // [EnableCors("MyPolicy")]
       // [Authorize(Policy = "RegisteredDataUser")]
        public JsonResult Get([FromRoute] int id) // 
        {
            // formName,List<string>lookupdata
            // Return _recordRepo.Response();
            return Json("");
        }

        // PUT: api/ScheduledEvents/5
        [HttpPut("{id}")]
      //  [EnableCors("MyPolicy")]
      //  [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult PutScheduledEvent([FromRoute] int id)
        {
            return BadRequest("Nothing to see here");
        }

        // POST: api/data
        [HttpPost]
     //   [EnableCors("MyPolicy")]
        //       [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Post([FromBody] Form form)
        {
            try
            {
                if (_eventRepo.AddFromForm(form, getUserID(Request.Headers)))
                {
                    return Ok();
                }
                else
                {
                }
            }
            catch (System.Exception ex)
            {
                LogException("Exception thrown while adding data");
            }

            return BadRequest("Data was not saved");
        }

        private void LogException(string message)
        {
            //if (null != _logger)
            //{
            //    _logger.LogError(message);
            //}
        }
        // DELETE: api/ScheduledEvents/5
        [HttpDelete("{id}")]
     //   [EnableCors("MyPolicy")]
     //   [Authorize(Policy = "RegisteredDataUser")]
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
