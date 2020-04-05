using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HD.MeteringPayment.Service
{
    public static class GpConfig
    {
        public static String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnection1"].ConnectionString; 
    }
}