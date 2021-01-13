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
        public int Id { get; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("text")]
        public string Text { get; }

        [JsonProperty("dateTime")]
        public DateTime CreatedOn { get; }

        public Note(string text, DateTime createdOn)
        {
            Text = text;
            CreatedOn = createdOn;
            Title = Text.Substring(0, Text.Length > 32 ? 32 : Text.Length);
        }
    }
}
