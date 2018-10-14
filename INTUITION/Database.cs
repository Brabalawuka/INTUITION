using System;
using System.Data;
using System.IO;

/*
 * class Database(FILEPATH)
 * automatically creates a CSV with headers if file does not exist
 * 
 * string getAttributeById(int ID, string ATTRIBUTE)
 * title
 * description
 * venue
 * date
 * time
 * regrequired
 * detail
 * lon
 * lat
 * image
 * 
 * int createEvent(strings ...)
 * returns id
 */


class Database
{
    private string filePath;
    private DataTable dt;
    //private TextFieldParser parser;

    // CONSTRUCTORS

    public Database(string _s)
    {
        dt = new DataTable();
        filePath = _s;

        

        if (!File.Exists(_s))
        {
            File.WriteAllText(_s, "ID,title, description, venue, date, time, regrequired, detail, lon, lat, image\r\n");
        }

        using (StreamReader sr = new StreamReader(_s))
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {

                    string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
        }


    }


    string getData(int row, int column)
    {
        return (string)dt.Rows[row][column];
    }

    int getRowById(int id)
    {
        for(int i=0;i<dt.Rows.Count;i++)
        {
            if(int.Parse(dt.Rows[i][0].ToString())==id)
            {
                return i;
            }
        }
        return -1;
    }

    void exceptionMessage()
    {
        Console.WriteLine("ERROR: ID does not exist in Database");
    }

    /*
     * Methods
     */

    public string getAttributeById(int id, string att)
    {
        int choice=-1;
        switch (att)
        {
            case "title":
                choice = 1;
                break;
            case "description":
                choice = 2;
                break;
            case "venue":
                choice = 3;
                break;
            case "date":
                choice = 4;
                break;
            case "time":
                choice = 5;
                break;
            case "regrequired":
                choice = 6;
                break;
            case "detail":
                choice = 7;
                break;
            case "lon":
                choice = 8;
                break;
            case "lat":
                choice = 9;
                break;
            case "image":
                choice = 10;
                break;
            default:
                break;
        }

        try
        {
            return getData(getRowById(id), choice);
        }
        catch (IndexOutOfRangeException)
        {
            exceptionMessage();
            return null;
        }

    }


    public int createEvent(string title, string description, string venue, string date, string time, string regrequired, string detail, string lon, string lat, string image)
    {
        int numberOfRows = dt.Rows.Count;
        int lastID;
        DataRow dr = dt.NewRow();

        if (numberOfRows == 0)
        {
            lastID = 0;
        }
        else
        {
            lastID = int.Parse(getData(numberOfRows - 1, 0));
        }

        string[] s = { title, description, venue, date, time, regrequired, detail, lon, lat, image };

        
        
        File.AppendAllText(filePath, (lastID + 1).ToString() + ",");
        dr[0] = lastID + 1;
        

        for (int i = 0; i < s.Length; i++)
        {
            /*
             *  Fix double quotation comma problem
             *  "the ""hbgbg"" thij"
             * 
             */

            dr[i+1] = s[i];

            if (s[i].Contains(",") && !s[i].Contains("\""))
            {
                File.AppendAllText(filePath, "\"" + s[i] + "\"");
            }
            else if (s[i].Contains("\""))
            {
                s[i] = s[i].Replace("\"", "\"\"");
                File.AppendAllText(filePath, "\"" + s[i] + "\"");
            }
            else
            {
                File.AppendAllText(filePath, s[i]);
            }
            if (i < (s.Length - 1))
            {
                File.AppendAllText(filePath, ",");
            }

            

        }
        File.AppendAllText(filePath, "\r\n");

        dt.Rows.Add(dr);

        return lastID + 1;

    }



}