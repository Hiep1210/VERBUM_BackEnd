using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verbum_service_domain.Common
{
    public static class SystemConfig
    {
        //public const string ConnectionString = "Host=localhost;Database=abc;Username=postgres;Password=123";
        public const string CONNECTION_STRING = "Host=localhost;Database=verbum_db;Username=postgres;Password=123456";
        public const int ACCESS_TOKEN_LIFE = 1; //hour
        public const int REFRESH_TOKEN_LIFE = 1; //month
    }
}
