using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using DataRecord;

namespace People
{
    class Event : Form
    {
        Event() : base("event", new string[] { "Id", "what", "when", "where", "note" })
        {
            AddRecord<int>("Id", 0);
            AddRecord<string>("what", "");
            AddRecord<DateTime>("when", DateTime.Now);
            AddRecord<string>("where", "");
            AddRecord<string>("note", "");
        }
    }
}
