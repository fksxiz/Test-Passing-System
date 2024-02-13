using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQCourse
{
    public class ETDOut
    {
        public ETDOut(string name,string desc, int minScore, TimeSpan ts) {
            Name = name;
            Desc = desc;
            this.minScore = minScore;
            timeToPass = ts;
        }
        public string Name { get; }
        public string Desc { get; }
        public int minScore { get; }
        public TimeSpan timeToPass { get; }
    }
}
