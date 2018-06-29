using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataRecord;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
//using DataRecord;

namespace CommonDataAPIMirror.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class DataController : Controller
    {
        // IScheduleRepository _eventRepo;
  //      ISource _source;
        IConfigurationRoot _config;
        ISource _masterForm;
        //  private readonly myScheduleContext _context;

        public DataController(ISource form )
        {
            _masterForm = form;
            //_repo = repo;
            //_repo.Initialize();
            //_source = source;
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
            return Ok(_masterForm.Get("lists",0,0));


            //   return BadRequest("No data results available");

        }

        // GET: api/ScheduledEvents/5
        [HttpGet("{formName}")]
        [EnableCors("MyPolicy")]
        // [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Get(string formName) // 
        {
            // formName,List<string>lookupdata
            // Return _recordRepo.Response();
            return Ok(_masterForm.Get(formName, 0, 0));

        }

        // GET: api/ScheduledEvents/5
        [HttpGet("GetForm")]
        [EnableCors("MyPolicy")]
        // [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult GetForm([FromRoute] PagingFormModel formPage) // 
        {
            // formName,List<string>lookupdata
            // Return _recordRepo.Response();
            return Ok(_masterForm.Get(formPage.FormName,formPage.PageNumber,formPage.PageLength));
        }

        // PUT: api/ScheduledEvents/5
        [HttpPut("{id}")]
        [EnableCors("MyPolicy")]
        //  [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult PutScheduledEvent([FromRoute] int id)
        {
            return BadRequest("Nothing to see here");
        }

        // POST: api/data
        [HttpPost]
        [EnableCors("MyPolicy")]
        //       [Authorize(Policy = "RegisteredDataUser")]
        public IActionResult Post([FromBody] string form)
        {
            //try
            //{
            //    if (_eventRepo.AddFromForm(form, getUserID(Request.Headers)))
            //    {
            //        return Ok();
            //    }
            //    else
            //    {
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    LogException("Exception thrown while adding data");
            //}

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
        [EnableCors("MyPolicy")]
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
