using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myScheduleModels.Models
{

    public partial class Lists
    {
        [Key]
        public int ListId { get; set; }
        public string ListName { get; set; }
        public int Value { get; set; }
        public string Label { get; set; }
    }
}
