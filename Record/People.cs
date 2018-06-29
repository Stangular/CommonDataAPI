using System;
using DataAccess;
using System.Collections.Generic;

using DataRecord;
using System.Linq;

namespace People
{
    public class People : Form
    {
        public People() : base("person", new string[] { "Id", "LastName", "FullGivenName", "CommonName", "note" })
        {
            AddRecord<int>("Id", 0);
            AddRecord<string>("LastName", "");
            AddRecord<string>("FullGivenName", "");
            AddRecord<string>("CommonName", "");
            AddRecord<string>("note", "");
        }
    }
}
