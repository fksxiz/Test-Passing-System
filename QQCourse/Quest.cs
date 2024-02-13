using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQCourse
{
    public class Quest
    {
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public List<bool> CorrectAnswers { get; set; }
    }
}
