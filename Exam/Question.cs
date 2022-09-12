using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int UnitID { get; set; }
        public int SectionID { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText1 { get; set; }
        public string PicturePath { get; set; }
        public string QuestionText2 { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string A1PicturePath { get; set; }
        public string A2PicturePath { get; set; }
        public string A3PicturePath { get; set; }
        public string A4PicturePath { get; set; }
        public int RightAnswer { get; set; }

        public override string ToString()
        {
            return QuestionText1;
        }
    }
}
