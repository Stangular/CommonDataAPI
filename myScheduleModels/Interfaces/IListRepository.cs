using System.Collections.Generic;
using DataRecord;

namespace myScheduleModels.Models.Interfaces
{
    public interface IListRepository
    {
        Form List(List<string> listNames);
    }
}
