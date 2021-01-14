using System;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Task_3
{
    class Program
    {
        static void SearchNotes()
        {
            Console.WriteLine("Search notes");
            Console.Write("Enter filter: ");
            var filter = Console.ReadLine();
            var filteredNotes = NoteUtils.GetNotes<Note>().Where(x => x.Id.ToString().Contains(filter)
                                                                   || x.Text.Contains(filter)
                                                                   || x.CreatedOn.ToString().Contains(filter));
            if (filteredNotes == null || filteredNotes.Count() == 0)
            {
                throw new ArgumentException();
            }
            foreach (var note in filteredNotes)
            {
                Console.WriteLine(note.ToString(false));
                Console.WriteLine("__________________________________");
            }
        }

        static void ViewNotes()
        {
            Console.WriteLine("View notes");

            Console.Write("Enter id: ");
            var id = int.Parse(Console.ReadLine());
            var viewNote = NoteUtils.GetNoteById(id);
            Console.WriteLine(viewNote.ToString(true));
        }

        static void CreateNote()
        {
            Console.WriteLine("Create note");
            Console.WriteLine("Enter text: ");
            var text = Console.ReadLine().Replace("  ", " ").Normalize();

            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Can't create empty note.");
                return;
            }
            var note = new Note(text, DateTime.Now);
            NoteUtils.CreateNote(note);
        }

        static void DeleteNote()
        {
            Console.WriteLine("Delete note");
            Console.Write("Enter id: ");
            var id = int.Parse(Console.ReadLine());
            var noteToRemove = NoteUtils.GetNoteById(id);
            Console.WriteLine(noteToRemove.ToString(true));
            Console.Write("If you sure and wanna delete this note, enter (y): ");
            if (Console.ReadKey().KeyChar == 'y')
            {
                NoteUtils.DeleteNote(id);
            }
            Console.WriteLine();
        }

        static void Menu()
        {
            do
            {
                Console.WriteLine("- - - - - - - - - - - - - - - - - ");
                Console.WriteLine("Menu\n1. Search notes\n2. View notes\n3. Create note\n4. Delete note\n5. Exit");
                Console.Write("Enter menu number: ");
                var key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine("- - - - - - - - - - - - - - - - - ");
                try
                {
                    switch (key)
                    {
                        case '1':
                            SearchNotes();
                            break;
                        case '2':
                            ViewNotes();
                            break;
                        case '3':
                            CreateNote();
                            break;
                        case '4':
                            DeleteNote();
                            break;
                        case '5': Console.WriteLine("Bye!");
                            return;
                        default:
                            Console.WriteLine("Invalid menu number. Try again");
                            break;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Empty notes list. Try to add note.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid id value.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Note not found.");
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
