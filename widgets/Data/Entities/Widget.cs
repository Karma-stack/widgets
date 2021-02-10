using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace widgets.Data.Entities
{
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Yandex { get; set; }
        public string Google { get; set; }
        public string TwoGIS {get;set;}
        public int CountConversion { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Users Users { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
