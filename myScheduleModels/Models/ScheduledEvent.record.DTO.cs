using System;
using DataRecord;

namespace myScheduleModels.Models
{
    public partial class ScheduledEvent : IRecordSource
    {
     //   public int Id { get; set; }
        public void ToRecord(Form form)
        {
            form.SetValue<int>("ID", Id);
            form.SetValue<string>("purpose", Purpose);
            form.SetValue<DateTime>("beginat", BeginAt.ToLocalTime());
            form.SetValue<int>("duration", Duration);
            form.SetValue<int>("locationid", LocationId);
        }

        public void FromRecord(Form form, int index)
        {
         //   Id = form.GetValue<int>("ID", index, -1);
            Purpose = form.GetValue<string>("purpose",index,"");
            BeginAt = form.GetValue<DateTime>("beginat", index, DateTime.Now);
            Duration = (int)form.GetValue<int>("duration", index, -1);
            LocationId = form.GetValue<int>("locationid", index,-1);
            if( LocationId < 0)
            {
                Location = new Location();
                Location.FromRecord(form, index);
            }
        }
    }
}
