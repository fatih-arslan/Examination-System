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
    public partial class Alistirma : Form
    {
        List<int> PracticeQuestions = new List<int>();
        int CurrentQ;
        int score;
        int chosen;
        public Alistirma()
        {
            InitializeComponent();
        }

        private void Alistirma_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            CurrentQ = 0;
            score = 0;
            PracticeQuestions = QuestionOps.GetPracticeQuestions(OgrenciEkrani.PracticeUnit);
            Question question = QuestionOps.GetQuestionByID(PracticeQuestions[CurrentQ]);
            labelN.Text = (CurrentQ + 1).ToString() + ")";
            labelUnit.Text = question.UnitID.ToString() + ".";
            labelSection.Text = question.SectionID.ToString() + ".";
            labelQnumber.Text = question.QuestionNumber.ToString() + ".";

            if (question.PicturePath != "..." && question.PicturePath != String.Empty)
            {
                pictureBoxQPic.Image = Image.FromFile(question.PicturePath);
                richTextBoxQText1.Text = question.QuestionText1;
                richTextBoxQText2.Text = question.QuestionText2;
                richTextBoxFullText.Text = null;
                richTextBoxFullText.Visible = false;
            }
            else
            {
                pictureBoxQPic.Image = null;
                richTextBoxFullText.Text = question.QuestionText1 + "\n" + question.QuestionText2;
                richTextBoxFullText.Visible = true;
                richTextBoxQText1.Text = null;
                richTextBoxQText2.Text = null;
            }

            textBoxA1.Text = question.Answer1;
            textBoxA2.Text = question.Answer2;
            textBoxA3.Text = question.Answer3;
            textBoxA4.Text = question.Answer4;
            if (question.A1PicturePath != String.Empty)
            {
                pictureBoxA1Pic.Image = Image.FromFile(question.A1PicturePath);
            }
            else { pictureBoxA1Pic.Image = null; }
            if (question.A2PicturePath != String.Empty)
            {
                pictureBoxA2Pic.Image = Image.FromFile(question.A2PicturePath);
            }
            else { pictureBoxA2Pic.Image = null; }
            if (question.A3PicturePath != String.Empty)
            {
                pictureBoxA3Pic.Image = Image.FromFile(question.A3PicturePath);
            }
            else { pictureBoxA3Pic.Image = null; }
            if (question.A4PicturePath != String.Empty)
            {
                pictureBoxA4Pic.Image = Image.FromFile(question.A4PicturePath);
            }
            else { pictureBoxA4Pic.Image = null; }

        }

        private void buttonNextQ_Click(object sender, EventArgs e)
        {
            CurrentQ++;
            int unit = int.Parse(labelUnit.Text.Replace(".", ""));
            int section = int.Parse(labelSection.Text.Replace(".", ""));
            int questionNumber = int.Parse(labelQnumber.Text.Replace(".", ""));
            Question answeredQuestion = QuestionOps.GetQuestion(unit, section, questionNumber);
            if(chosen == answeredQuestion.RightAnswer)
            {
                score++;
            }

            
            if(CurrentQ == PracticeQuestions.Count)
            {
                MessageBox.Show($"Sınav sonucunuz: {score}/{PracticeQuestions.Count}");
                this.Close();
            }
            else
            {
                if (CurrentQ == PracticeQuestions.Count - 1)
                {
                    buttonNextQ.Text = "Sınavı bitir";
                }
                labelN.Text = (CurrentQ + 1).ToString() + ")";//(int.Parse(labelN.Text.Replace(")", "")) + 1).ToString() + ")";
                Question nextQuestion = QuestionOps.GetQuestionByID(PracticeQuestions[CurrentQ]);

                labelUnit.Text = nextQuestion.UnitID.ToString() + ".";
                labelSection.Text = nextQuestion.SectionID.ToString() + ".";
                labelQnumber.Text = nextQuestion.QuestionNumber.ToString() + ".";

                if (nextQuestion.PicturePath != "..." && nextQuestion.PicturePath != String.Empty && nextQuestion.PicturePath != null)
                {
                    pictureBoxQPic.Image = Image.FromFile(nextQuestion.PicturePath);
                    richTextBoxQText1.Text = nextQuestion.QuestionText1;
                    richTextBoxQText2.Text = nextQuestion.QuestionText2;
                    richTextBoxFullText.Text = null;
                    richTextBoxFullText.Visible = false;
                }
                else
                {
                    pictureBoxQPic.Image = null;
                    richTextBoxFullText.Text = nextQuestion.QuestionText1 + "\n" + nextQuestion.QuestionText2;
                    richTextBoxFullText.Visible = true;
                    richTextBoxQText1.Text = null;
                    richTextBoxQText2.Text = null;
                }

                textBoxA1.Text = nextQuestion.Answer1;
                textBoxA2.Text = nextQuestion.Answer2;
                textBoxA3.Text = nextQuestion.Answer3;
                textBoxA4.Text = nextQuestion.Answer4;
                if (nextQuestion.A1PicturePath != null && nextQuestion.A1PicturePath != String.Empty)
                {
                    pictureBoxA1Pic.Image = Image.FromFile(nextQuestion.A1PicturePath);
                }
                else { pictureBoxA1Pic.Image = null; }
                if (nextQuestion.A2PicturePath != null && nextQuestion.A2PicturePath != String.Empty)
                {
                    pictureBoxA2Pic.Image = Image.FromFile(nextQuestion.A2PicturePath);
                }
                else { pictureBoxA2Pic.Image = null; }
                if (nextQuestion.A3PicturePath != null && nextQuestion.A3PicturePath != String.Empty)
                {
                    pictureBoxA3Pic.Image = Image.FromFile(nextQuestion.A3PicturePath);
                }
                else { pictureBoxA3Pic.Image = null; }
                if (nextQuestion.A4PicturePath != null && nextQuestion.A4PicturePath != String.Empty)
                {
                    pictureBoxA4Pic.Image = Image.FromFile(nextQuestion.A4PicturePath);
                }
                else { pictureBoxA4Pic.Image = null; }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 4;
        }

        private void buttonPreviousQ_Click(object sender, EventArgs e)
        {
            CurrentQ--;
            buttonNextQ.Text = "Sonraki soru";
            labelN.Text = (CurrentQ+1).ToString() + ")";
            Question nextQuestion = QuestionOps.GetQuestionByID(PracticeQuestions[CurrentQ]);
            labelUnit.Text = nextQuestion.UnitID.ToString() + ".";
            labelSection.Text = nextQuestion.SectionID.ToString() + ".";
            labelQnumber.Text = nextQuestion.QuestionNumber.ToString() + ".";

            if (nextQuestion.PicturePath != "..." && nextQuestion.PicturePath != String.Empty && nextQuestion.PicturePath != null)
            {
                pictureBoxQPic.Image = Image.FromFile(nextQuestion.PicturePath);
                richTextBoxQText1.Text = nextQuestion.QuestionText1;
                richTextBoxQText2.Text = nextQuestion.QuestionText2;
                richTextBoxFullText.Text = null;
                richTextBoxFullText.Visible = false;
            }
            else
            {
                pictureBoxQPic.Image = null;
                richTextBoxFullText.Text = nextQuestion.QuestionText1 + "\n" + nextQuestion.QuestionText2;
                richTextBoxFullText.Visible = true;
                richTextBoxQText1.Text = null;
                richTextBoxQText2.Text = null;
            }

            textBoxA1.Text = nextQuestion.Answer1;
            textBoxA2.Text = nextQuestion.Answer2;
            textBoxA3.Text = nextQuestion.Answer3;
            textBoxA4.Text = nextQuestion.Answer4;
            if (nextQuestion.A1PicturePath != null && nextQuestion.A1PicturePath != String.Empty)
            {
                pictureBoxA1Pic.Image = Image.FromFile(nextQuestion.A1PicturePath);
            }
            else { pictureBoxA1Pic.Image = null; }
            if (nextQuestion.A2PicturePath != null && nextQuestion.A2PicturePath != String.Empty)
            {
                pictureBoxA2Pic.Image = Image.FromFile(nextQuestion.A2PicturePath);
            }
            else { pictureBoxA2Pic.Image = null; }
            if (nextQuestion.A3PicturePath != null && nextQuestion.A3PicturePath != String.Empty)
            {
                pictureBoxA3Pic.Image = Image.FromFile(nextQuestion.A3PicturePath);
            }
            else { pictureBoxA3Pic.Image = null; }
            if (nextQuestion.A4PicturePath != null && nextQuestion.A4PicturePath != String.Empty)
            {
                pictureBoxA4Pic.Image = Image.FromFile(nextQuestion.A4PicturePath);
            }
            else { pictureBoxA4Pic.Image = null; }
        }
    }
}
