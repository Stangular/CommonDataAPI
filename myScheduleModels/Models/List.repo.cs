using System.Collections.Generic;
using System.Linq;
using myScheduleModels.Models.Interfaces;
using DataRecord;

namespace myScheduleModels.Models
{
    public class ListRepo : IListRepository
    {
        Form listForm = new Form("lists");
        private readonly myScheduleContext _appDbContext;

        public ListRepo(myScheduleContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Form List(List<string> listNames)
        {
            listForm.Clear();
            _appDbContext.Lists.Where(i => listNames.Contains(i.ListName))
                       .ToList().ForEach(e => e.ToRecord(listForm));
            return listForm;
        }
    }
}
