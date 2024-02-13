using QQCourse.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQCourse
{
    public class TestViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TestViewModel(Tests test)
        {
            Init(test);
        }

        private void Init(Tests test)
        {
            var questions = Core.Database.Questions.Where(q => q.TestId == test.Id);

            foreach (var quest in questions)
            {
                Quest buf = new Quest();
                buf.Text = quest.Question;
                var answers = Core.Database.Answers.Where(a => a.QuestId == quest.Id);
                List<Answer> answrs = new List<Answer>();
                List<bool> iscorrect = new List<bool>();
                foreach (var ans in answers)
                {
                    answrs.Add(new Answer(ans.Answer));
                    iscorrect.Add(ans.Correct == true);
                }
                buf.Answers = answrs;
                buf.CorrectAnswers = iscorrect;
                if (buf.Answers.Count>0)
                {
                    Questions.Add(buf);
                }
            }
        }

        public ObservableCollection<Quest> Questions { get; set; } = new ObservableCollection<Quest>();

        private List<List<bool>> selectedAnswers = new List<List<bool>>();
        public List<List<bool>> SelectedAnswers
        {
            get { return selectedAnswers; }
            set
            {
                selectedAnswers = value;
                OnPropertyChanged(nameof(SelectedAnswers));
            }
        }
    }
}
