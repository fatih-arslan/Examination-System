using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class AnsweredQuestion : Question
    {
        public int CorrectlyAnswered { get; set; }
        public int ChosenAnswer { get; set; }
        public DateTime DateOfAnswer { get; set; }
        public int UserID { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
