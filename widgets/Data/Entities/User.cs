﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace widgets.Data.Entities
{
    public class User: IdentityUser<int>
    {
        public string Username { get; set; }
        public List<Widget> Widgets { get; set; }
    }
}
