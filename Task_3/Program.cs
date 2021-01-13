using System;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

namespace Task_3
{
    class Program
    {
        static void SearchNotes()
        {
            Console.WriteLine("Search notes");
            Console.Write("Enter filter: ");
        }

        static void ViewNotes()
        {
            Console.WriteLine("View notes");
        }

        static void CreateNote()
        {
            Console.WriteLine("Create note");
            Console.WriteLine("Enter text: ");
            var text = Console.ReadLine().Replace("  ", " ").Normalize();
            Console.WriteLine(text);
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Can't create empty note.");
                return;
            }
            var note = new Note(text, DateTime.Now);
            JsonUtils.CreateNote(note);
        }

        static void DeleteNote()
        {
            Console.WriteLine("Delete note");
        }

        static void Menu()
        {
            do
            {
                Console.WriteLine("Menu:\n1. Search notes\n2. View notes\n3. Create note\n4. Delete note\n5. Exit");
                Console.Write("Enter menu number: ");
                var key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case '1': SearchNotes();
                        break;
                    case '2': ViewNotes();
                        break;
                    case '3': CreateNote();
                        break;
                    case '4': DeleteNote();
                        break;
                    case '5': return;
                    default: Console.WriteLine("Invalid menu number. Try again");
                        break;
                }
            } while (true);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 3. Notes by Andrey Basystyi");
            Menu();
        }
    }
}
