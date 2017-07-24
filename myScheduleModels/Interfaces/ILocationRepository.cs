using DataRecord;

namespace myScheduleModels.Models.Interfaces
{
    public interface ILocationRepository
    {
        Form UserLocations(string userID);
    }
}
