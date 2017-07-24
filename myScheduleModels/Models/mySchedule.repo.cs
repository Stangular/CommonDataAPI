using System.Linq;
using DataRecord;
using Microsoft.Extensions.Logging;
using myScheduleModels.Models.Interfaces;

namespace myScheduleModels.Models
{
    public class myScheduleRepo : IScheduleRepository
    {
        LocationRepo _locations;
        Form scheduleForm = new Form("mySchedule");
        //Form listForm = new Form("lists");
        //Form locationForm = new Form("locations");

        private readonly myScheduleContext _appDbContext;
        private ILogger<myScheduleContext> _logger;

        public myScheduleRepo(myScheduleContext appDbContext,
                ILogger<myScheduleContext> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _locations = new LocationRepo(appDbContext);
        }

        private ScheduledEvent getEvent(int id)
        {
            return _appDbContext.ScheduledEvent.FirstOrDefault(e => e.Id == id);
        }

        public bool AddFromForm(Form form, string userID)
        {
            bool result = false;
            ScheduledEvent sevent;
            try
            {
                _logger.LogInformation("Adding from form");
                for (int i = 0; i < form.RecordCount; i = i + 1)
                {
                    sevent = getEvent(form.GetValue<int>("id", i, -1));
                    if (sevent == null)
                    {
                        sevent = new ScheduledEvent();
                        sevent.ScheduleUser = userID;
                        _appDbContext.ScheduledEvent.Add(sevent);
                    }
                    sevent.FromRecord(form, i);
                }
                result = ( _appDbContext.SaveChanges() > 0 );
                if( result )
                {
                    _logger.LogInformation("Finished AddFromForm");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                result = false;/// log error...
            }
            finally
            {
                _logger.LogInformation("Finished AddFromForm");
            }
            return result;
        }

        public Form UserSchedule(string userID)
        {
            scheduleForm.Clear();
            var events = _appDbContext.ScheduledEvent
                       .Where(e => e.ScheduleUser == userID).ToList();  // .Include(L => L.Location)
            events.ForEach(e => e.ToRecord(scheduleForm));
            scheduleForm.RecordCount = events.Count;
            scheduleForm.AddForm(_locations.UserLocations(userID));
            return scheduleForm;
        }

        //public Form List(List<string> listNames)
        //{
        //    listForm.Clear();
        //    _appDbContext.Lists.Where(i => listNames.Contains(i.ListName))
        //               .ToList().ForEach(e => e.ToRecord(listForm));
        //    return listForm;
        //}

        //public Form UserLocations(string userID)
        //{
        //    locationForm.Clear();
        //    var q = from e in _appDbContext.ScheduledEvent
        //            join p in _appDbContext.Location on e.LocationId equals p.Id
        //            where e.ScheduleUser == userID
        //            select p;
        //    q.ToList().ForEach(p=>p.ToRecord(locationForm));
        //    return locationForm;
        //}
    }
}
