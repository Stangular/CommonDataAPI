using System;
using System.Collections.Generic;
using System.Text;
using myScheduleModels.Models;
using DataRecord;

namespace myScheduleModels.Models
{
    public partial class Lists : IRecordSource
    {
        public void ToRecord(Form form)
        {
            form.SetValue<string>("ListName", ListName);
            form.SetValue<int>("Value", Value);
            form.SetValue<string>("Label", Label);
        }

        public void FromRecord(Form form, int index)
        {
            ListName = form.GetValue<string>("ListName", index, "");
            Value = form.GetValue<int>("Value", index, -1);
            Label = form.GetValue<string>("Label", index, "");
        }
    }
}
