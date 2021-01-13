using System;
using System.Collections.Generic;
using System.Text;

namespace Task_3
{
    interface INote
    {
        int Id { get; }
        string Title { get; }
        string Text { get; }
        DateTime CreatedOn { get; }
    }
}
