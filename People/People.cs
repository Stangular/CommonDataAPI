using System;
using DataAccess;
using System.Collections.Generic;

using DataRecord;
using System.Linq;

namespace People
{
    public class Person : Form
    {
        public Person() : base("person", new string[] { "Id", "LastName", "FullGivenName", "CommonName", "note" })
        {
            AddRecord<int>("Id", 0);
            AddRecord<string>("LastName", "");
            AddRecord<string>("FullGivenName", "");
            AddRecord<string>("CommonName", "");
            AddRecord<string>("note", "");

            // select * from person as p join personevent as e on e.PersonId = p.Id  where Id = id... 
        }


        public void AddValues()
        {
            for (i = 0; i < _fields.Count - 1; i = i + 1)
            {
                 _fields[i];

            }
        }
    }
    public class PersonEvent : Form
    {
        string parentForm = "";
        public PersonEvent() : base("personevent", new string[] { "PersonId", "EventId", "note"})
        {
            AddRecord<int>("PersonId", 0);
            AddRecord<int>("EventId", 0);
            AddRecord<string>("note", "");
        }
        // Event  PersonID  what when where note   


        //   Add event to person...    
        //     Insert into personevent fields 
        //   Add person to event...
        // add person id and eventid to PersonEvent...
    }

    public class EventPerson : Event
    {
        public PersonEvent() : base()
        {

        }
    }

    public class People
    {

        public People()
        {

        }
    }
}
