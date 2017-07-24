using System;
using System.Collections.Generic;

namespace myScheduleModels.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public int PreviousId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
