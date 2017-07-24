using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace myScheduleModels.Models
{
 
    public partial class ScheduledEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime BeginAt { get; set; }
        public int Duration { get; set; }
        public string Purpose { get; set; }
        public int History { get; set; }
        public string ScheduleUser { get; set; }

        public virtual Location Location { get; set; }
    }
}
