using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using widgets.Data.Entities;

namespace widgets.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string review { get; set; }
        public string TelNum { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Widget Widget { get; set; }
    }
}
