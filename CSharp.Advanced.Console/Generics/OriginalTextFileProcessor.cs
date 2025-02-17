using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.Generics
{
    public static class GenericTextFileProcessor
    {
        public static void SaveToTextFile<T,U>(List<T> data, string filePath) where T : class, new() where U: class, new()
        { }
        public static void SaveToTextFile<T>(List<T> data, string filePath) where T : class, new() 
        {   
            if(data is null || data.Count == 0) throw new ArgumentNullException(nameof(data));  

            List<string> lines = new List<string>();
            StringBuilder header = new StringBuilder();
            StringBuilder line = new StringBuilder();
            var properties = data.GetType().GetProperties();
            foreach ( var property in properties) 
            {
                header.Append(property.Name);
                header.Append(',');
            }
            lines.Add(header.ToString().Substring(0,header.Length -1 ));
            foreach (var row in data) 
            {
                line = new StringBuilder();
                foreach (var col in properties)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }
                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }
            System.IO.File.WriteAllLines(filePath, lines);
        }
        public static List<T> loadDataFromTextFile<T>(string filePath) where T : class, new()
        {
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            List<T> data = new List<T>();
            T entry = new T();
            if(lines.Count < 2) throw new IndexOutOfRangeException(nameof(lines));  
            var properties = entry.GetType().GetProperties();
            var headers = lines[0].Split(",");
            lines.RemoveAt(0 );
            foreach (var row in lines)
            {
                entry = new T();
                var vals = row.Split(",");
                for (var i =0; i< headers.Length;i++)
                {
                    foreach (var property in properties) 
                    {
                        if (property.Name == headers[i]) 
                        {
                            property.SetValue(entry, Convert.ChangeType(vals[i],property.PropertyType));  
                        }
                    }
                }
                data.Add(entry);
            }


            return data;
        }
    }

    public class LogEntry 
    {
    
    }

    public class Person 
    {
    }
}
