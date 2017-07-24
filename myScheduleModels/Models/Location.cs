using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myScheduleModels.Models
{
    public partial class Location
    {
        public Location()
        {
            ScheduledEvent = new HashSet<ScheduledEvent>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Path { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ScheduledEvent> ScheduledEvent { get; set; }
    }
}
