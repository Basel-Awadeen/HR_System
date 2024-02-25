﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class UserClaimModel
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
