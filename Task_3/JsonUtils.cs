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

        public static Note GetNoteById(int id)
        {
            var notes = GetNotes<Note>();
            if (notes == null || notes.Count == 0)
            {
                throw new ArgumentException();
            }
            return notes.Find(x => x.Id == id) ?? throw new Exception();
        }

        public static void CreateNote<T>(T note)
        {
            var list = GetNotes<T>();
            list.Add(note);
            WriteJson(list);
        }

        public static void DeleteNote(int id)
        {
            var list = GetNotes<Note>();
            list.Remove(list.Find(x => x.Id == id));
            WriteJson(list);
        }

        private static void WriteJson<T>(List<T> list)
        {
            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public static List<T> GetNotes<T>()
        {
            List<T> list = new List<T>();
            var text = "";
            try
            {
                text = File.ReadAllText(_filePath);
            }
            catch (Exception) { }

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
