using System.Collections.Generic;
using System.Linq;
using myScheduleModels.Models.Interfaces;
using DataRecord;

namespace myScheduleModels.Models
{
    public class LocationRepo : ILocationRepository
    {
        Form locationForm = new Form("locations");
        private readonly myScheduleContext _appDbContext;

        public LocationRepo(myScheduleContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Form UserLocations(string userID)
        {
            locationForm.Clear();
            var q = from e in _appDbContext.ScheduledEvent
                    join p in _appDbContext.Location on e.LocationId equals p.Id
                    where e.ScheduleUser == userID
                    select p;
            var values = q.ToList();
            values.ForEach(p => p.ToRecord(locationForm));
            locationForm.RecordCount = values.Count;
            return locationForm;
        }
    }
}
