using System;
using System.IO;

public class TxtWriter
{
    private static string  path = @"c:\temp\MyTest.txt";

    public static void Main()
    {
        
        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            StreamWriter sw = File.CreateText(path);
        }

        // This text is always added, making the file longer over time
        // if it is not deleted.

        }

    public void addLine(String str) 
        {
            using (StreamWriter sw = File.AppendText(path))
                sw.WriteLine(str);
        }
}