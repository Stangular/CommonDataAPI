using DataRecord;

namespace myScheduleModels.Models.Interfaces
{
    public interface IScheduleRepository
    {
        bool AddFromForm(Form form, string userID);
        Form UserSchedule(string userID);
    }
}
