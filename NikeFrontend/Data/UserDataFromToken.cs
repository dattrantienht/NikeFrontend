﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFrontend.Data
{

    public class UserDataFromTokenRoot
    {
        public UserDataFromToken data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }

    public class UserDataFromToken
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public List<Role> roles { get; set; }
    }
    public class Role
    {
        public string id { get; set; }
        public string name { get; set; }
        public string normalizedName { get; set; }
        public string concurrencyStamp { get; set; }
    }
    public class StringToken
    {
        public string Token { get; set; }
    }

}
