using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace WebCore.UnitTest
{
    public class UnitTest1
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder();

            return builder.Build();
        }
        int Add(int x, int y)
        {
            return x + y;
        }
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Fact]
        public void Test1()
        {
            Console.WriteLine("Test");
            var config = GetConfig();
            var con = config.GetConnectionString("DefaultConnection");
            Console.WriteLine(con);

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            Console.WriteLine(configuration.GetConnectionString("ConnectionStrings"));

            //GetAllTables(con);
        }

       

    }
}
