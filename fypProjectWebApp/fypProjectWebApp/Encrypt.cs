﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace fypProjectWebApp
{
    public static class Encrypt
    {
        public static string Hash(string val)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(val))
                );
        } 
    }
}