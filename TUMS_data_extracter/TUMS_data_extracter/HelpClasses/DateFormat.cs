using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TUMS_extractor.HelpClasses
{
    class DateFormat
    {
        public static DateTime formatDateTime(string date, string time, string dateFormat)
        {
            string[] dateParts = date.Split('/');



            if (dateFormat.Equals("YYYY/MM/DD"))
            {
                if (time.Substring(0, 3) == " 24")
                {
                    time = "00" + time.Substring(3, time.Length - 3);

                    DateTime d = DateTime.Parse(date + " " + time);
                    d = d.AddDays(1);

                    return d;
                }
                else
                    return DateTime.Parse(date + " " + time);
            }

            else if (dateFormat.Equals("DD/MM/YYYY"))
                return DateTime.Parse(dateParts[2] + "/" + dateParts[1] + "/" + dateParts[0] + " " + time);

            else if (dateFormat.Equals("MM/DD/YYYY"))
                return DateTime.Parse(dateParts[2] + "/" + dateParts[0] + "/" + dateParts[1] + " " + time);

            else if (dateFormat.Equals("DD-Month-YY"))
            {
                DateFormat df = new DateFormat();
                DateTime dateTime = DateTime.Parse(df.getDate(date) + " " + df.getTime(time) + " " + time.Split(' ')[1]);

                return dateTime;
            }

            return new DateTime();
        }

        private string getMonth(string monthName)
        {
            string month = "";

            if ("January".Contains(monthName))
            {
                month = "01";
            }
            if ("February".Contains(monthName))
            {
                month = "02";
            }
            if ("March".Contains(monthName))
            {
                month = "03";
            }
            if ("April".Contains(monthName))
            {
                month = "04";
            }
            if ("May".Contains(monthName))
            {
                month = "05";
            }
            if ("June".Contains(monthName))
            {
                month = "06";
            }
            if ("July".Contains(monthName))
            {
                month = "07";
            }
            if ("August".Contains(monthName))
            {
                month = "08";
            }
            if ("September".Contains(monthName))
            {
                month = "09";
            }
            if ("October".Contains(monthName))
            {
                month = "10";
            }
            if ("November".Contains(monthName))
            {
                month = "11";
            }
            if ("December".Contains(monthName))
            {
                month = "12";
            }

            return month;
        }

        private string getTime(string timeString)
        {
            string[] time = timeString.Split(' ')[0].Split(':');

            return time[0] + ":" + time[1] + ":" + "00";
        }

        private string getDate(string dateString)
        {
            string[] date = dateString.Split('-');

            int year = Int16.Parse(date[2]) + 2000;

            string day = date[0];

            string month = getMonth(date[1]);

            return year.ToString() + "/" + month + "/" + day;            
        }
    }
}
