using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TUMS_extractor;
using System.Threading;
using TUMS_extractor.FileType;
using static TUMS_extractor.FileType.TUMS;

namespace TUMS_data_extracter
{
    class Program
    {      

        static void Main(string[] args)
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

            var data = TUMS.GetData(filename, "YYYY/MM/DD");

        }

    }
}
