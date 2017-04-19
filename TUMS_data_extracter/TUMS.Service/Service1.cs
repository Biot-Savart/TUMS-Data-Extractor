using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TUMS_extractor;


namespace TUMS.Service
{
    public partial class Service1 : ServiceBase
    {       
        private DateTime preDate;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 3600000; // 1 hour
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Run);
            timer.Start();           
        }

        protected override void OnStop()
        {
        }

        private void Run(object sender, System.Timers.ElapsedEventArgs args)
        {
            if (DateTime.Now.Hour % Properties.Settings.Default.ExecutionIntervalHours == 0)        //every 2 hours
            {
                DoTums();
                preDate = DateTime.Now;
            }
        }

        private void DoTums()
        {
            string accountNumber = "accountNumber";
            string meterNumber = "meterNumber";
            string meterName = "demo";

            //DateTime sDate = new DateTime(2016,2,8);
            DateTime sDate = new DateTime(2017, 4, 1);
            DateTime eDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour);

            var sMills = sDate.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            var eMills = eDate.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            string filename = "C:/TUMS/" + meterName + "kWh - " + sDate.Year + sDate.Month + sDate.Day + "-" + eDate.Year + eDate.Month + eDate.Day + ".csv";

            string url = "https://tums.mdmhosting.com/selfcare-ws/api/meter/" + accountNumber + "_" + meterNumber + "/csv/consumption&auth=cnlub215YnVyZ2g6U000MzI1?fromDate=" + sMills + "&toDate=" + eMills + "&fileName=test.csv";


            System.Net.WebClient client = new WebClient();
            client.DownloadFile(url, filename);

            var data = TUMS_extractor.FileType.TUMS.GetData(filename, "YYYY/MM/DD");
           
        }

    }
}
