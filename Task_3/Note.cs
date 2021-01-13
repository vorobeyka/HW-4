using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace Task_3
{
    class Note : INote
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("dateTime")]
        public DateTime CreatedOn { get; set; }

        public Note() { }

        public Note(string text, DateTime createdOn)
        {
            Text = text;
            CreatedOn = createdOn;
            Title = Text.Substring(0, Text.Length > 32 ? 32 : Text.Length);
            try
            {
                Id = JsonUtils.GetNotes<Note>().Max(x => x.Id) + 1;
            }
            catch (Exception)
            {
                Id = 1;
            }
        }

        public string ToString(bool textFlag)
        {
            if (textFlag)
            {
                return $"Id: {Id}\nTitle: {Title}\nNote: {Text}\nCreated on: {CreatedOn}";
            }
            return $"Id: {Id}\nTitle: {Title}\nCreated on: {CreatedOn}";
        }
    }
}
