using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;

namespace Task_3
{
    static class JsonUtils
    {
        private static readonly string _filePath = "notes.json";

        public static string LastError { get; }

        public static void CreateNote<T>(T note)
        {
            var list = GetNotes<T>();
            list.Add(note);
            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        private static List<T> GetNotes<T>()
        {
            List<T> list = new List<T>();
            var text = "";
            try
            {
                text = File.ReadAllText(_filePath);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            if (!string.IsNullOrWhiteSpace(text))
            {
                JArray.Parse(text).
                       ToList().
                       ForEach(j => list.Add(
                           JsonConvert.DeserializeObject<T>(j.ToString()))
                       );
            }
            return list;
        }


    }
}
