﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactVerifierLibrary.Contact
{
    public class ContactEntity
    {
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public int Location { get; set; }
        public int CommuncationFrequency { get; set; }
    }
}
