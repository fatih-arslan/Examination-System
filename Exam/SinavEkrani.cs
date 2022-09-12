using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class SinavEkrani : Form
    {
        List<int> unansweredQuestions = new List<int>();// Öğrencinin daha önce cevaplamadığı sorular
        List<int> dailyQuestions = new List<int>();//Cevaplanmamış soruların içinden seçilecek rastgele 10 soru
        int chosen = 0; //İşaretlenen cevabı tutacak değişken       
        List<int> answeredQuestions = new List<int>(); // Daha önce doğru cevaplanmış ve bu sınavda tekrar edilmesi gereken sorular
        DateTime endOfExam; //sınavın bitiş zamanı
        public static int score; //doğru cevap sayısını tutacak değişken
        public static AnsweredQuestion[] results; //Bu sınavda cevaplanan soruları tutacak liste
        public static List<int> examQuestions = new List<int>(); //Bu sınavda sorulacak tüm soruları içeren liste (dailyQuestions + answeredQuestions)
        int currentQuestion; //Ekranda olan sorunun numarası
        public SinavEkrani()
        {
            InitializeComponent();
        }

        private void SinavEkrani_Load(object sender, EventArgs e)
        {            
            WindowState = FormWindowState.Maximized;          
            currentQuestion = 0;    
            string[] s = GirisEkrani.student.QuestionRepetition.Split(' ');// Veritabanından öğrencinin soru tekrar sıklığı tercihini getirme
            List<DateTime> dates = new List<DateTime>(); 
            for(int i = 0; i < s.Length; i++)
            {
                dates.Add(DateTime.Today.AddDays(-int.Parse(s[i])));
            }            
            answeredQuestions = QuestionOps.GetQuestionsWithDate(dates, GirisEkrani.student.UserID); //O gün tekrar sorulması gereken geçmiş tarihli soruları getirme           
            
            //O gün sorulacak rastgele 10 yeni soru seçme
            unansweredQuestions = QuestionOps.GetUnansweredQuestions(GirisEkrani.student);
            if (unansweredQuestions.Count > 0)
            {
                Random random = new Random();
                int c = Math.Min(unansweredQuestions.Count, 10);
                while(dailyQuestions.Count < c)
                {
                    int n = random.Next(unansweredQuestions.Count);
                    if (!dailyQuestions.Contains(unansweredQuestions[n]))
                    {
                        dailyQuestions.Add(unansweredQuestions[n]);
                    }
                }                
            }
            
            examQuestions.AddRange(dailyQuestions);
            examQuestions.AddRange(answeredQuestions);
            results = new AnsweredQuestion[examQuestions.Count];            

            labelRemainingQuestions.Text = $"{(currentQuestion + 1)}/{examQuestions.Count}";
            double RemainingTime = examQuestions.Count;
            Question question = QuestionOps.GetQuestionByID(examQuestions[currentQuestion]);
            if(currentQuestion < dailyQuestions.Count)
            {
                labelExtraQuestions.Text = "Yeni soru";
            }
            else
            {
                labelExtraQuestions.Text = QuestionOps.GetAnsweredQuestionByID(examQuestions[currentQuestion]).DateOfAnswer.ToString();
            }

            //Soruyu ekrana getirme
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
            if(question.A1PicturePath != String.Empty)
            {
                pictureBoxA1Pic.Image = Image.FromFile(question.A1PicturePath);
            }
            else { pictureBoxA1Pic.Image = null; }
            if(question.A2PicturePath != String.Empty)
            {
                pictureBoxA2Pic.Image = Image.FromFile(question.A2PicturePath);
            }
            else { pictureBoxA2Pic.Image = null; }
            if(question.A3PicturePath != String.Empty)
            {
                pictureBoxA3Pic.Image = Image.FromFile(question.A3PicturePath);
            }
            else { pictureBoxA3Pic.Image = null; }
            if(question.A4PicturePath != String.Empty)
            {
                pictureBoxA4Pic.Image = Image.FromFile(question.A4PicturePath);
            }
            else { pictureBoxA4Pic.Image = null; }

            endOfExam = DateTime.Now.AddMinutes(RemainingTime);
            timer1 = new Timer { Interval = 1000, Enabled = true };
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1_Tick(null, null);
        }

        private void buttonNextQ_Click(object sender, EventArgs e)
        {
            //isaretlenmis soruyu results listesine ekleme            
            int unit = int.Parse(labelUnit.Text.Replace(".", ""));
            int section = int.Parse(labelSection.Text.Replace(".", ""));
            int questionNumber = int.Parse(labelQnumber.Text.Replace(".", ""));
            Question answeredQuestion = QuestionOps.GetQuestion(unit, section, questionNumber);
            int ca = 0;
            if (chosen == answeredQuestion.RightAnswer)
            {
                ca = 1;
            }
            AnsweredQuestion ansQ = QuestionOps.GetAnsweredQuestionByID(answeredQuestion.QuestionID);
            if (ansQ == null)
            {
                ansQ = new AnsweredQuestion
                {
                    QuestionID = answeredQuestion.QuestionID,
                    RightAnswer = answeredQuestion.RightAnswer,
                    ChosenAnswer = chosen,
                    DateOfAnswer = DateTime.Today,
                    UserID = GirisEkrani.student.UserID,
                    CorrectlyAnswered = 0,
                };
            }
            else
            {
                ansQ.RightAnswer = answeredQuestion.RightAnswer;
                ansQ.ChosenAnswer = chosen;
                ansQ.DateOfAnswer = DateTime.Today;
                ansQ.UserID = GirisEkrani.student.UserID;
            }
            ansQ.CorrectlyAnswered = (ca == 0) ? 0 : (ansQ.CorrectlyAnswered + ca);
            results[currentQuestion] = ansQ;          

            //Yeni soru getirme
            if(currentQuestion < examQuestions.Count - 1)
            {
                foreach (RadioButton rb in this.Controls.OfType<RadioButton>())// İşaretlenmiş şıktan işareti kaldırma
                {
                    rb.Checked = false;
                }
                chosen = 0;

                currentQuestion++;
                labelN.Text = (currentQuestion + 1).ToString() + ")";
                labelRemainingQuestions.Text = $"{(currentQuestion + 1)}/{examQuestions.Count}";
                Question nextQuestion = new Question();
                
                nextQuestion = QuestionOps.GetQuestionByID(examQuestions[currentQuestion]);
                if (currentQuestion < dailyQuestions.Count)
                {
                    labelExtraQuestions.Text = "Yeni soru";
                }
                else
                {
                    labelExtraQuestions.Text = QuestionOps.GetAnsweredQuestionByID(examQuestions[currentQuestion]).DateOfAnswer.ToString("dd/MM/yyyy") + 
                        " tarihinde doğru cevaplanmış soru";
                }

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
                
                if(results[currentQuestion] != null)// Bir sonraki soru sınav esnasında daha önce görüntülenip işaretlenmişse işaretlenmiş cevabı getirme
                {
                    int a = results[currentQuestion].ChosenAnswer;
                    switch (a)
                    {
                        case 1:
                            radioButton1.Checked = true;
                            break;
                        case 2:
                            radioButton2.Checked = true;
                            break;
                        case 3:
                            radioButton3.Checked = true;
                            break;
                        case 4:
                            radioButton4.Checked = true;
                            break;
                        default:
                            foreach (RadioButton rb in this.Controls.OfType<RadioButton>()) { rb.Checked = false; }
                            break;
                    }
                }
            }  
            buttonEndExam.Enabled = currentQuestion == examQuestions.Count -1 ? true : false;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            SinavEkrani se = new SinavEkrani();
            TimeSpan ts = endOfExam.Subtract(DateTime.Now);
            labelRemainingTime.Text = ts.ToString(@"mm\:ss");
            if (ts.TotalMilliseconds < 0)
            {
                ((Timer)sender).Enabled = false;
                MessageBox.Show("Süreniz bitti");                               
                Sonuc s = new Sonuc();
                s.Show();
                this.Close();
            }
        }

        private void SinavEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void buttonPreQ_Click(object sender, EventArgs e)
        {            
            if (currentQuestion > 0)
            {
                
                int unit = int.Parse(labelUnit.Text.Replace(".", ""));
                int section = int.Parse(labelSection.Text.Replace(".", ""));
                int questionNumber = int.Parse(labelQnumber.Text.Replace(".", ""));
                Question answeredQuestion = QuestionOps.GetQuestion(unit, section, questionNumber);
                int ca = 0;
                if (chosen == answeredQuestion.RightAnswer)
                {
                    ca = 1;
                }
                AnsweredQuestion ansQ = QuestionOps.GetAnsweredQuestionByID(answeredQuestion.QuestionID);
                if (ansQ == null)
                {
                    ansQ = new AnsweredQuestion
                    {
                        QuestionID = answeredQuestion.QuestionID,
                        RightAnswer = answeredQuestion.RightAnswer,
                        ChosenAnswer = chosen,
                        DateOfAnswer = DateTime.Today,
                        UserID = GirisEkrani.student.UserID,
                        CorrectlyAnswered = 0,
                    };
                }
                else
                {
                    ansQ.RightAnswer = answeredQuestion.RightAnswer;
                    ansQ.ChosenAnswer = chosen;
                    ansQ.DateOfAnswer = DateTime.Today;
                    ansQ.UserID = GirisEkrani.student.UserID;
                }
                ansQ.CorrectlyAnswered = (ca == 0) ? 0 : (ansQ.CorrectlyAnswered + ca);
                results[currentQuestion] = ansQ;
               
                currentQuestion--;
                int chosenAnswer = results[currentQuestion].ChosenAnswer;
                Question previousQuestion = new Question();
                previousQuestion = QuestionOps.GetQuestionByID(examQuestions[currentQuestion]);
                labelN.Text = (currentQuestion + 1).ToString() + ")";
                labelRemainingQuestions.Text = $"{(currentQuestion + 1)}/{examQuestions.Count}";
                if (currentQuestion < dailyQuestions.Count)
                {
                    labelExtraQuestions.Text = "Yeni soru";
                }
                else
                {
                    labelExtraQuestions.Text = QuestionOps.GetAnsweredQuestionByID(examQuestions[currentQuestion]).DateOfAnswer.ToString("dd/MM/yyyy") + 
                        " tarihinde doğru cevaplanmış soru";
                }

                labelUnit.Text = previousQuestion.UnitID.ToString() + ".";
                labelSection.Text = previousQuestion.SectionID.ToString() + ".";
                labelQnumber.Text = previousQuestion.QuestionNumber.ToString() + ".";

                if (previousQuestion.PicturePath != "..." && previousQuestion.PicturePath != String.Empty && previousQuestion.PicturePath != null)
                {
                    pictureBoxQPic.Image = Image.FromFile(previousQuestion.PicturePath);
                    richTextBoxQText1.Text = previousQuestion.QuestionText1;
                    richTextBoxQText2.Text = previousQuestion.QuestionText2;
                    richTextBoxFullText.Text = null;
                    richTextBoxFullText.Visible = false;
                }
                else
                {
                    pictureBoxQPic.Image = null;
                    richTextBoxFullText.Text = previousQuestion.QuestionText1 + "\n" + previousQuestion.QuestionText2;
                    richTextBoxFullText.Visible = true;
                    richTextBoxQText1.Text = null;
                    richTextBoxQText2.Text = null;
                }

                textBoxA1.Text = previousQuestion.Answer1;
                textBoxA2.Text = previousQuestion.Answer2;
                textBoxA3.Text = previousQuestion.Answer3;
                textBoxA4.Text = previousQuestion.Answer4;
                if (previousQuestion.A1PicturePath != null && previousQuestion.A1PicturePath != String.Empty)
                {
                    pictureBoxA1Pic.Image = Image.FromFile(previousQuestion.A1PicturePath);
                }
                else { pictureBoxA1Pic.Image = null; }
                if (previousQuestion.A2PicturePath != null && previousQuestion.A2PicturePath != String.Empty)
                {
                    pictureBoxA2Pic.Image = Image.FromFile(previousQuestion.A2PicturePath);
                }
                else { pictureBoxA2Pic.Image = null; }
                if (previousQuestion.A3PicturePath != null && previousQuestion.A3PicturePath != String.Empty)
                {
                    pictureBoxA3Pic.Image = Image.FromFile(previousQuestion.A3PicturePath);
                }
                else { pictureBoxA3Pic.Image = null; }
                if (previousQuestion.A4PicturePath != null && previousQuestion.A4PicturePath != String.Empty)
                {
                    pictureBoxA4Pic.Image = Image.FromFile(previousQuestion.A4PicturePath);
                }
                else { pictureBoxA4Pic.Image = null; }

                switch (chosenAnswer)
                {
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 2:
                        radioButton2.Checked = true;
                        break;
                    case 3:
                        radioButton3.Checked = true;
                        break;
                    case 4:
                        radioButton4.Checked = true;
                        break;
                    default:
                        foreach(RadioButton rb in this.Controls.OfType<RadioButton>()) { rb.Checked = false; }
                        break;
                }
            }  
            buttonEndExam.Enabled = false;
        }

        private void buttonEndExam_Click(object sender, EventArgs e)
        {
            int unit = int.Parse(labelUnit.Text.Replace(".", ""));// Son soruyu results listesine ekleme
            int section = int.Parse(labelSection.Text.Replace(".", ""));
            int questionNumber = int.Parse(labelQnumber.Text.Replace(".", ""));
            Question answeredQuestion = QuestionOps.GetQuestion(unit, section, questionNumber);
            int ca = (chosen == answeredQuestion.RightAnswer) ? 1 : 0;
            AnsweredQuestion ansQ = QuestionOps.GetAnsweredQuestionByID(answeredQuestion.QuestionID);
            if (ansQ == null)
            {
                ansQ = new AnsweredQuestion
                {
                    QuestionID = answeredQuestion.QuestionID,
                    RightAnswer = answeredQuestion.RightAnswer,
                    ChosenAnswer = chosen,
                    DateOfAnswer = DateTime.Today,
                    UserID = GirisEkrani.student.UserID,
                    CorrectlyAnswered = 0,
                };
            }
            else
            {
                ansQ.RightAnswer = answeredQuestion.RightAnswer;
                ansQ.ChosenAnswer = chosen;
                ansQ.DateOfAnswer = DateTime.Today;
                ansQ.UserID = GirisEkrani.student.UserID;
            }
            ansQ.CorrectlyAnswered = (ca == 0) ? 0 : (ansQ.CorrectlyAnswered + ca);            
            results[currentQuestion] = ansQ;

            foreach (AnsweredQuestion q in results)
            {
                QuestionOps.AddAnsweredQuestion(q);                
            }
            score = 0;
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].ChosenAnswer == results[i].RightAnswer)
                {
                    score++;
                }
            }
            UserOps.AddExamResult(GirisEkrani.student, $"{score}/{examQuestions.Count}", DateTime.Today);
            timer1.Stop();
            Sonuc s = new Sonuc();
            s.Show();
            dailyQuestions.Clear();
            answeredQuestions.Clear();
            unansweredQuestions.Clear();
            examQuestions.Clear();
            this.Close();
        }
    }
}
