using DevExpress.XtraEditors;
using Erp.CommonData;
using GP.DistributedServices.Seedwork.ErrorHandlers;
using HD.MeteringPayment.UITest; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HD.MeteringPayment.UITest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            GpTraceSource.Listeners.Add(new ConsoleTraceListener(false));
            GpTraceSource.Switch.Level = SourceLevels.All;
            SW.Reset();
            SW.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through     
            // our handler.     
            System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event.      
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            SW.Stop();
            GpTraceSource.TraceInformation("配置时间差:{0}毫秒", SW.ElapsedMilliseconds);
            Application.Run(new AppForm());

        }
        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            String error = "";
            Exception ex = t.Exception;
            //多种异常，判断属于哪一类
            FaultException<ApplicationServiceError> myException1;
            FaultException<Hondee.Common.HDException.ApplicationServiceError> myException2;
            Hondee.Common.HDException.BusinessException myException3;
            myException1 = ex as FaultException<ApplicationServiceError>;
            myException2 = ex as FaultException<Hondee.Common.HDException.ApplicationServiceError>;
            if (myException2 == null)
                myException2 = ex.InnerException as FaultException<Hondee.Common.HDException.ApplicationServiceError>;
            myException3 = ex as Hondee.Common.HDException.BusinessException;
            if (myException2 != null)
            {
                error = myException2.Detail.ErrorMessage;
            }
            if (myException3 != null)
            {
                error = myException3.BusMessage;
            }

#if Debug
            if (myException1 != null)
            {
                    error=myException1.ToString();
            } 
            if (String.IsNullOrEmpty(error))
                error = ex.ToString();


#else
            if (myException1 != null)
            {
                error = myException1.Message;
            }
            if (String.IsNullOrEmpty(error))
                error = ex.Message;

#endif
            XtraMessageBox.Show(error);
        }
        public static Stopwatch SW = new Stopwatch();
        public static TraceSource GpTraceSource = new TraceSource("TraceSourceApp");
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            String error = ""; 
            //多种异常，判断属于哪一类
            FaultException<ApplicationServiceError> myException1;
            FaultException<Hondee.Common.HDException.ApplicationServiceError> myException2;
            Hondee.Common.HDException.BusinessException myException3;
            myException1 = ex as FaultException<ApplicationServiceError>;
            myException2 = ex as FaultException<Hondee.Common.HDException.ApplicationServiceError>;
            if (myException2 == null)
                myException2 = ex.InnerException as FaultException<Hondee.Common.HDException.ApplicationServiceError>;
            myException3 = ex as Hondee.Common.HDException.BusinessException;
            if (myException2 != null)
            {
                error = myException2.Detail.ErrorMessage;
            }
            if (myException3 != null)
            {
                error = myException3.BusMessage;
            }

#if Debug
            if (myException1 != null)
            {
                    error=myException1.ToString();
            } 
            if (String.IsNullOrEmpty(error))
                error = ex.ToString();


#else
            if (myException1 != null)
            {
                error = myException1.Message;
            }
            if (String.IsNullOrEmpty(error))
                error = ex.Message;

#endif
            XtraMessageBox.Show(error);
        }
    }
}
