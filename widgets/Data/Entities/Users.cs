using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace widgets.Data.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Widget> Widgets { get; set; }
    }
}
