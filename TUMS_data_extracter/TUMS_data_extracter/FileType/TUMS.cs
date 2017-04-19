using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSV;
using TUMS_extractor.HelpClasses;


namespace TUMS_extractor.FileType
{
    public class TUMS
    {       


        public TUMS() { }

        public static List<SingleValueDataPoint> GetData(string filename, string dateFormat)
        {
            CSVReader reader = new CSVReader(filename,';');

            string[] line;                      
            int dateCol = 0;
            int timeCol = 0;     
            int kWhcol = 0;
            int interval = 15;

            List<SingleValueDataPoint> kWData = new List<SingleValueDataPoint>();             

            int row = 0;
            while ((line = reader.read()) != null)
            {
                if (row == 0)   //Header Row
                {
                    for (int col = 0; col < line.Length; col++)
                    {
                        if (line[col].Contains("Date"))
                        {
                            dateCol = col;
                        }
                        else if (line[col].Contains("Time"))
                        {
                            timeCol = col;
                        }  
                        else if (line[col].Contains("Consumption kWh"))
                        {
                            kWhcol = col;
                        }                                             
                    }
                }
                else if (row > 0)   //Data Rows
                {                          

                    DateTime pointDateTime = DateFormat.formatDateTime(line[dateCol], line[timeCol] ,dateFormat);               
                    
                        try
                        {   
                            SingleValueDataPoint point = new SingleValueDataPoint();                          
                            point.TimeStamp = pointDateTime;                         

                            
                                point.Value = Double.Parse(line[kWhcol]) * (60 / interval);
                                kWData.Add(point);
                                                 
                            
                            
                        }   //try
                        catch { }
                   
                }   //if

                if (line[0] != "sep=")
                    row++;
            }   //while           
           
            reader.CloseFile();

            return kWData;
        }

        private SingleValueDataPoint[] populateSinksData(List<SingleValueDataPoint> data)
        {
            SingleValueDataPoint[] sinkData = new SingleValueDataPoint[data.Count];

            int row = 0;

            foreach (SingleValueDataPoint point in data)
            {
                sinkData[row] = point;
                row++;
            }

            return sinkData;
        }             


        
    }

    public class SingleValueDataPoint
    {
        public DateTime TimeStamp { get; set; }

        public double Value { get; set; }
    }

}
