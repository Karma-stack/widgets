using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace widgets.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string review { get; set; }
        public string TelNum { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
