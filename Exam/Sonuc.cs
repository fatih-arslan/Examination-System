using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class Sonuc : Form
    {
        public Sonuc()
        {
            InitializeComponent();
        }

        private void Sonuc_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            AnsweredQuestion[] results = SinavEkrani.results;
            int n = 1;            
            string r = $"Sınav sonucunuz: {SinavEkrani.score}/{SinavEkrani.examQuestions.Count}\n";
            r += n.ToString() + ")" + " ";
            for(int i = 0; i < SinavEkrani.examQuestions.Count; i++)
            {
                if(results[i].ChosenAnswer == 0 || results[i] == null)
                {
                    r += "Boş\n";
                }
                else if (results[i].CorrectlyAnswered > 0)
                {
                    r += "Doğru\n";
                }
                else
                {
                    r += "Yanlış\n";
                }
                n++;
                if(i != results.Length - 1)
                {
                    r += (n.ToString()) + ")" + " ";
                }                
            }            
            richTextBoxResults.Text = r;
            Array.Clear(SinavEkrani.results, 0, SinavEkrani.results.Length);
        }
    }
}
