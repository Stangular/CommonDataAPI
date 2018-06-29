using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using DataAccess;
using DataRecord;

namespace Services
{
    public class MasterForm : ISource
    {
        List<Form> _forms = new List<Form>();
        List<IDataSource> _sources = new List<IDataSource>();
        IMemoryCache _cache;
        public MasterForm(IConfigurationRoot configuration, IMemoryCache cache, string formName = "Main")
        {
            _sources.Add(new Source(configuration));
            _cache = cache;
            _forms.Add(new ListForm());
        }

        public IForm Get(string name, int pageNumber, int pageLength)
        {
            var form = _forms.FirstOrDefault(f => f.FormName.ToLower() == name.ToLower());
            if (form == null)
            {
                form = new Form(name, new string[] { "" });
                _forms.Add(form);
            }
            _sources[0].Query(form);
            return form;
        }

        IDataStore ISource.GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        IDataStore ISource.GetFromCache(string name)
        {
            throw new NotImplementedException();
        }

        Task<IDataStore> ISource.GetFromCacheAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
