using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQCourse
{
    public class Answer
    {
        public Answer(string text)
        {
            Text = text;
            IsSelected = false;
        }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}
