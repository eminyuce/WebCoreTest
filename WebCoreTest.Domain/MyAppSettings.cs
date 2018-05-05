using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebCoreTest.Domain.Entities;
using WebCoreTest.Domain.Helpers;

namespace WebCoreTest.Domain
{
    public class MyAppSettings
    {
        private IConfiguration Configuration { get; }
        public MyAppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string ConnectionString
        {
            get
            {
                return Configuration.GetConnectionString("DefaultConnection");
            }
        }
    }
}
