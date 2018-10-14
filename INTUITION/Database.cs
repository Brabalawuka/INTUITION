using System;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;

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

namespace Testingonly
{
    class Testingonly
    {
        static void Main(string[] args)
        {

            // This opens/creates a CSV in the filepath
            Database test = new Database(@"D:\sheet.csv");

            // Creates an event in this database and gets the ID
            int createdEventId = test.createEvent("Event Title","Some description","A venue","11/10","1:30","1","Details","1.35","1.2","http://asdf.com");

            // Outputs the ID along with example attributes
            Console.WriteLine("ID: " + createdEventId);
            Console.WriteLine("Title: " + test.getAttributeById(createdEventId,"title")); // gets title
            Console.WriteLine("Description: " + test.getAttributeById(createdEventId, "description")); // gets description

        }
    }
}


class Database
{
    private string filePath;
    private DataTable dt;
    private TextFieldParser parser;

    // CONSTRUCTORS

    public Database(string _s)
    {
        dt = new DataTable();
        filePath = _s;

        

        if (!File.Exists(_s))
        {
            File.WriteAllText(_s, "ID,title, description, venue, date, time, regrequired, detail, lon, lat, image\r\n");
        }

        parser = new TextFieldParser(_s);

        parser.HasFieldsEnclosedInQuotes = true;
        parser.SetDelimiters(",");

        string[] fields;

        int i = 0;
        int j = 0;

        while (!parser.EndOfData)
        {
            fields = parser.ReadFields();

            if (j != 0)    // TO HELP IGNORE HEADERS
            {
                DataRow dr = dt.NewRow();

                foreach (string field in fields)
                {
                    //Console.WriteLine(i+" : "+field);
                    dr[i] = field;
                    i++;
                }
                dt.Rows.Add(dr);

            }
            else
            {   // MUST ADD HEADERS TO DATATABLE FOR IT TO WORK!!!!!!!!
                foreach (string field in fields)
                {
                    dt.Columns.Add(field);
                }
            }
            i = 0;
            j++;
        }

        parser.Close();



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