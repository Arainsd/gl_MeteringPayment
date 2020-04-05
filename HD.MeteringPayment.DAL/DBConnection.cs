
using System;
using System.Collections.Generic;

using System.Linq;


namespace HD.MeteringPayment.DAL
{
    /// <summary>
    /// 读取服务配置文件的连接字符串
    /// </summary>
    public static class DBConnection
    {
        public static String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

    }
}
