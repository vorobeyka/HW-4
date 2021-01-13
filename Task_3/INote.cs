using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
