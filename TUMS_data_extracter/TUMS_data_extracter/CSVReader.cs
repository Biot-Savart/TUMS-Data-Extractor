using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSV
{
    class CSVReader
    {

        StreamReader reader;

        private char separator;

        private char quotechar;

        private int skipLines;



        // The default separator to use if none is supplied to the constructor.

        public static char DEFAULT_SEPARATOR = ',';


        // The default quote character to use if none is supplied to the constructor.

        public static char DEFAULT_QUOTE_CHARACTER = '"';


        // The default line to start reading.

        public static int DEFAULT_SKIP_LINES = 0;

        /// <summary>
        /// Constructs CSVReader using a comma for the separator.
        /// </summary>
        /// <param name="filename"></param>

        public CSVReader(string filename)
        {
            this.reader = File.OpenText(filename);
            this.separator = DEFAULT_SEPARATOR;
            this.quotechar = DEFAULT_QUOTE_CHARACTER;
            this.skipLines = DEFAULT_SKIP_LINES;
        }

        /// <summary>
        /// Constructs CSVReader with supplied separator.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="separator"></param>

        public CSVReader(string filename, char separator)
        {
            this.reader = File.OpenText(filename);
            this.separator = separator;
            this.quotechar = DEFAULT_QUOTE_CHARACTER;
            this.skipLines = DEFAULT_SKIP_LINES;
        }

        /// <summary>
        /// Constructs CSVReader with supplied separator and quote char.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="separator"></param>
        /// <param name="quotechar"></param>

        public CSVReader(string filename, char separator, char quotechar)
        {
            this.reader = File.OpenText(filename);
            this.separator = separator;
            this.quotechar = quotechar;
            this.skipLines = DEFAULT_SKIP_LINES;
        }

        /// <summary>
        /// Constructs CSVReader with supplied separator, quote char and number of skiplines.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="separator"></param>
        /// <param name="quotechar"></param>
        /// <param name="line"></param>

        public CSVReader(string filename, char separator, char quotechar, int skiplines)
        {
            this.reader = File.OpenText(filename);
            this.separator = separator;
            this.quotechar = quotechar;
            this.skipLines = skiplines;

            for (int x = 0; x <= skipLines; x++)
            {
                reader.ReadLine();
            }
        }

        /// <summary>
        /// Constructs CSVReader number of skiplines.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="skiplines"></param>

        public CSVReader(string filename, int skiplines)
        {
            this.reader = File.OpenText(filename);
            this.separator = DEFAULT_SEPARATOR;
            this.quotechar = DEFAULT_QUOTE_CHARACTER;
            this.skipLines = DEFAULT_SKIP_LINES;

            for (int x = 0; x <= skipLines; x++)
            {
                reader.ReadLine();
            }
        }


        /// <summary>
        /// Reads the next line of the csv file and breaks it up into its different values
        /// </summary>
        /// <returns>string[]</returns>

        public string[] read()
        {
            bool inquotes = false;              //variable that is set when a value is in qoute characters           
            int word_num = 0;

            string input = reader.ReadLine();   //reads the next line of the file   

            char[] chars; //= input.ToCharArray();         //convert the string into an array of caracters
            string[] data = null; //= new string[chars.Length];   //create string array of max possible length
            try
            {
                chars = input.ToCharArray();         //convert the string into an array of caracters

                if (chars.Length == 0)
                    return new string[0];

                data = new string[chars.Length];   //create string array of max possible length

                for (int x = 0; x < chars.Length; x++)
                {
                    if (chars[x] == quotechar && chars[x + 1] != quotechar)              //check for quote characters
                    {
                        if (inquotes == false)
                        {
                            inquotes = true;
                            x++;
                        }
                        else
                        {
                            inquotes = false;
                            x++;
                        }
                    }

                    if (inquotes == false)
                    {
                        if (chars.Length < x + 1)       //check if done then break
                            break;

                        if (chars[x] == separator)      //check for separator outside quotes
                        {
                            word_num++;                 //go to next string
                            x++;
                        }

                        if (chars[x] != quotechar)      //check if next character is quote character
                        {
                            data[word_num] = data[word_num] + chars[x];     //add character to string
                        }
                        else
                        {
                            inquotes = true;
                        }
                    }
                    else
                    {    
                        data[word_num] = data[word_num] + chars[x];         //just add characters if in quotes
                    }
                }

                string[] parsed = new string[word_num + 1];       //create string array that will hold the values

                for (int x = 0; x <= word_num; x++)                //populate the array
                {
                    parsed[x] = data[x];
                }

                word_num = 0;

                return parsed;               //return the array
            }
            catch
            {
                // return null;                //if nothing read return null

                try
                {
                    string[] parsed = new string[word_num];       //create string array that will hold the values

                    for (int x = 0; x <= word_num - 1; x++)                //populate the array
                    {
                        parsed[x] = data[x];
                    }

                    word_num = 0;

                    if (parsed.Length > 0)
                        return parsed;
                    else
                        return null;
                }
                catch { return null; }
            }

        }

        public void CloseFile()
        {
            reader.Close();
        }

    }
}
