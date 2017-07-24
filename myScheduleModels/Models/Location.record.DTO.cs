using System;
using System.Collections.Generic;
using System.Text;
using myScheduleModels.Models;
using DataRecord;

namespace myScheduleModels.Models
{
    public partial class Location : IRecordSource
    {
    //    public int Id { get; set; }
        public void ToRecord(Form form)
        {
            form.SetValue<int>("ID", Id);
            form.SetValue<string>("Address1", Address);
            form.SetValue<string>("Path", Path);
            form.SetValue<string>("Description", Description);
            form.SetValue<decimal>("Latitude", Lat.HasValue ? Lat.Value : 0.0M);
            form.SetValue<decimal>("Longitude", Lon.HasValue ? Lon.Value : 0.0M);
        }

        public void FromRecord(Form form, int index)
        {
       //     Id = form.GetValue<int>("ID", index, -1);
            Address = form.GetValue<string>("Address1", index, "");
            Path = form.GetValue<string>("Path", index, "");
            Description = form.GetValue<string>("Description", index, "");
            Lat = form.GetValue<decimal>("Latitude", index, 0.0M);
            Lon = form.GetValue<decimal>("Longitude", index, 0.0M);
        }
    }
}
