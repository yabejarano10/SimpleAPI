﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models.Patron
{
    public class PatronIndexModel
    {
        public IEnumerable<PatronDetailModel> patrons { get; set; }
    }
}
