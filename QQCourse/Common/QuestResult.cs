using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQCourse
{
    public class QuestResult
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public List<TextS> SelectedAnswers { get; set; }
        public List<TextS> CorrectAnswers { get; set; }

        public QuestResult(string text, bool IsCorrect, List<TextS> SelectedAns, List<TextS> CorrectAns)
        {
            Text = text;
            this.IsCorrect = IsCorrect;
            SelectedAnswers = SelectedAns;
            CorrectAnswers = CorrectAns;
        }
    }
}
