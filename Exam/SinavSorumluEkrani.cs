using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exam
{
    public partial class SinavSorumluEkrani : Form
    {
        public static int c = 0;
        public static List<TextBox> answerChoices = new List<TextBox>();
        public SinavSorumluEkrani()
        {
            InitializeComponent();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBoxQ.ImageLocation = openFileDialog1.FileName;
            textBoxPPath.Text = openFileDialog1.FileName;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            answerChoices = new List<TextBox> { textBoxA1, textBoxA2, textBoxA3, textBoxA4, textBoxA1Path, textBoxA2Path, textBoxA3Path, textBoxA4Path };
            if (textBoxUnit.Text == String.Empty || textBoxSection.Text == String.Empty || textBoxQNo.Text == String.Empty)
            {
                MessageBox.Show("Ünite, bölüm, ve soru numarası değeri girmelisiniz");
            }
            else if (richTextBoxQText1.Text == String.Empty && richTextBoxQText2.Text == String.Empty)
            {
                MessageBox.Show("Soru en az bir metin içermelidir");
            }                       
            else if(textBoxA1.Text == String.Empty || textBoxA2.Text == String.Empty || textBoxA3.Text == String.Empty || textBoxA4.Text == String.Empty)
            {         
                foreach(TextBox tb in answerChoices)
                {
                    if(tb.Text != String.Empty)
                    {
                        c++;
                    }
                }
                if(c < 4)
                {
                    MessageBox.Show("Soru 4 şıklı olmalıdır");
                }
                else if (comboBoxRA.Text == String.Empty)
                {
                    MessageBox.Show("Doğru cevap seçmelisiniz");
                }
                else
                {
                    int _UnitID = int.Parse(textBoxUnit.Text);
                    int _SectionID = int.Parse(textBoxSection.Text);
                    int _QuestionNumber = int.Parse(textBoxQNo.Text);
                    string _QuestionText1 = richTextBoxQText1.Text;
                    string _QuestionText2 = richTextBoxQText2.Text;
                    string _PicturePath = textBoxPPath.Text;
                    int _RightAnswer = int.Parse(comboBoxRA.SelectedItem.ToString());
                    string _Answer1 = textBoxA1.Text;
                    string _Answer2 = textBoxA2.Text;
                    string _Answer3 = textBoxA3.Text;
                    string _Answer4 = textBoxA4.Text;
                    string _A1P = textBoxA1Path.Text;
                    string _A2P = textBoxA2Path.Text;
                    string _A3P = textBoxA3Path.Text;
                    string _A4P = textBoxA4Path.Text;
                    Question q = QuestionOps.GetQuestion(_UnitID, _SectionID, _QuestionNumber);
                    if (q == null)
                    {
                        q = new Question
                        {
                            UnitID = _UnitID,
                            SectionID = _SectionID,
                            QuestionNumber = _QuestionNumber,
                            QuestionText1 = _QuestionText1,
                            QuestionText2 = _QuestionText2,
                            PicturePath = _PicturePath,
                            RightAnswer = _RightAnswer,
                            Answer1 = _Answer1,
                            Answer2 = _Answer2,
                            Answer3 = _Answer3,
                            Answer4 = _Answer4,
                            A1PicturePath = _A1P,
                            A2PicturePath = _A2P,
                            A3PicturePath = _A3P,
                            A4PicturePath = _A4P
                        };
                        QuestionOps.AddQuestion(q);
                        MessageBox.Show("Soru eklendi");
                    }
                    else if (comboBoxRA.Text == String.Empty)
                    {
                        MessageBox.Show("Doğru cevap seçmelisiniz");
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show($"{_UnitID}. ünite, {_SectionID}. bölüm, {_QuestionNumber}. " +
                            $"soru zaten eklenmiş, soru değiştirilsin mi?", "Soru zaten var", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Question q2 = new Question
                            {
                                UnitID = _UnitID,
                                SectionID = _SectionID,
                                QuestionNumber = _QuestionNumber,
                                QuestionText1 = _QuestionText1,
                                QuestionText2 = _QuestionText2,
                                PicturePath = _PicturePath,
                                Answer1 = _Answer1,
                                Answer2 = _Answer2,
                                Answer3 = _Answer3,
                                Answer4 = _Answer4,
                                A1PicturePath = _A1P,
                                A2PicturePath = _A2P,
                                A3PicturePath = _A3P,
                                A4PicturePath = _A4P,
                                RightAnswer = _RightAnswer,
                            };
                            QuestionOps.UpdateQuestion(q2, q);
                            MessageBox.Show("Soru değiştirildi");
                        }
                    }
                }
            }            
            else
            {
                int _UnitID = int.Parse(textBoxUnit.Text);
                int _SectionID = int.Parse(textBoxSection.Text);
                int _QuestionNumber = int.Parse(textBoxQNo.Text);
                string _QuestionText1 = richTextBoxQText1.Text;
                string _QuestionText2 = richTextBoxQText2.Text;
                string _PicturePath = textBoxPPath.Text;
                int _RightAnswer = int.Parse(comboBoxRA.SelectedItem.ToString());
                string _Answer1 = textBoxA1.Text;
                string _Answer2 = textBoxA2.Text;
                string _Answer3 = textBoxA3.Text;
                string _Answer4 = textBoxA4.Text;
                string _A1P = textBoxA1Path.Text;
                string _A2P = textBoxA2Path.Text;
                string _A3P = textBoxA3Path.Text;
                string _A4P = textBoxA4Path.Text;
                Question q = QuestionOps.GetQuestion(_UnitID, _SectionID, _QuestionNumber);
                if (q == null)
                {
                    q = new Question
                    {
                        UnitID = _UnitID,
                        SectionID = _SectionID,
                        QuestionNumber = _QuestionNumber,
                        QuestionText1 = _QuestionText1,
                        QuestionText2 = _QuestionText2,
                        PicturePath = _PicturePath,
                        RightAnswer = _RightAnswer,
                        Answer1 = _Answer1,
                        Answer2 = _Answer2,
                        Answer3 = _Answer3,
                        Answer4 = _Answer4,
                        A1PicturePath = _A1P,
                        A2PicturePath = _A2P,
                        A3PicturePath = _A3P,
                        A4PicturePath = _A4P
                    };
                    QuestionOps.AddQuestion(q);
                    MessageBox.Show("Soru eklendi");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show($"{_UnitID}. ünite, {_SectionID}. bölüm, {_QuestionNumber}. " +
                        $"soru zaten eklenmiş, soru değiştirilsin mi?", "Soru zaten var", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Question q2 = new Question
                        {
                            UnitID = _UnitID,
                            SectionID = _SectionID,
                            QuestionNumber = _QuestionNumber,
                            QuestionText1 = _QuestionText1,
                            QuestionText2 = _QuestionText2,
                            PicturePath = _PicturePath,
                            Answer1 = _Answer1,
                            Answer2 = _Answer2,
                            Answer3 = _Answer3,
                            Answer4 = _Answer4,
                            A1PicturePath = _A1P,
                            A2PicturePath = _A2P,
                            A3PicturePath = _A3P,
                            A4PicturePath = _A4P,
                            RightAnswer = _RightAnswer,
                        };
                        QuestionOps.UpdateQuestion(q2, q);
                        MessageBox.Show("Soru değiştirildi");
                    }
                }
            }            
        }

        private void btnSoruEkle_Click(object sender, EventArgs e)
        {            
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "pnlSoruEkle" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            pictureBoxA1.ImageLocation = openFileDialog2.FileName;
            textBoxA1Path.Text = openFileDialog2.FileName;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
            pictureBoxA2.ImageLocation = openFileDialog3.FileName;
            textBoxA2Path.Text = openFileDialog3.FileName;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog4.ShowDialog();
            pictureBoxA3.ImageLocation = openFileDialog4.FileName;
            textBoxA3Path.Text = openFileDialog4.FileName;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openFileDialog5.ShowDialog();
            pictureBoxA4.ImageLocation = openFileDialog5.FileName;
            textBoxA4Path.Text = openFileDialog5.FileName;
        }

        private void SinavSorumluEkrani_Load(object sender, EventArgs e)
        {
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;            
        }

        private void btnGetQ_Click(object sender, EventArgs e)
        {           
            int UnitID = int.Parse(textBoxGetUnit.Text);
            int SectionID = int.Parse(textBoxGetSection.Text);
            int QuestionNumber = int.Parse(textBoxGetQno.Text);
            Question q = QuestionOps.GetQuestion(UnitID, SectionID, QuestionNumber);
            if(q == null)
            {
                MessageBox.Show("Kayıtlı soru bulunamadı");
            }
            else
            {                
                if(q.PicturePath != "..." && q.PicturePath != String.Empty)
                {
                    pictureBoxGetQPic.Image = Image.FromFile(q.PicturePath);
                    richTextBoxGetQText1.Text = q.QuestionText1;
                    richTextBoxGetQText2.Text = q.QuestionText2;
                    richTextBoxFullQtext.Text = null;
                    richTextBoxFullQtext.Visible = false;
                }
                else 
                { 
                    pictureBoxGetQPic.Image = null;
                    richTextBoxFullQtext.Text = q.QuestionText1 + "\n" + q.QuestionText2;
                    richTextBoxFullQtext.Visible = true;
                    richTextBoxGetQText1.Text = null;
                    richTextBoxGetQText2.Text = null;

                }
                textBoxGetA1.Text = q.Answer1;
                textBoxGetA2.Text = q.Answer2;
                textBoxGetA3.Text = q.Answer3;
                textBoxGetA4.Text = q.Answer4;
                if(q.A1PicturePath != String.Empty)
                {
                    pictureBoxA1Pic.Image = Image.FromFile(q.A1PicturePath);
                }
                else { pictureBoxA1Pic.Image = null; }
                if (q.A2PicturePath != String.Empty)
                {
                    pictureBoxA2Pic.Image = Image.FromFile(q.A2PicturePath);
                }
                else { pictureBoxA2Pic.Image = null; }
                if (q.A3PicturePath != String.Empty)
                {
                    pictureBoxA3Pic.Image = Image.FromFile(q.A3PicturePath);
                }
                else { pictureBoxA3Pic.Image = null; }
                if (q.A4PicturePath != String.Empty)
                {
                    pictureBoxA4Pic.Image = Image.FromFile(q.A4PicturePath);
                }
                else { pictureBoxA4Pic.Image = null; }
                textBoxGetRA.Text = q.RightAnswer.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SinavSorumluEkrani se = new SinavSorumluEkrani();
            foreach (Panel p in se.Controls)
            {
                if (p.Name == "panelGetQ")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }            
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            
            foreach (Control c in pnlSoruEkle.Controls)
            {
                if(c is TextBox || c is RichTextBox)
                {
                    c.Text = String.Empty;                    
                }
                else if(c is PictureBox)
                {
                    ((PictureBox)c).Image = Image.FromFile("C:/Users/fatih/Desktop/ART GALLERY/qt3.jpg");
                }                
                else if(c is ComboBox)
                {
                    ((ComboBox)c).SelectedItem = null;
                }
            }              
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SinavSorumluEkrani se = new SinavSorumluEkrani();
            foreach(Panel p in se.Controls)
            {
                if(p.Name == "panelUpdate")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }            
        }

        private void buttonGetQ_Click(object sender, EventArgs e)
        {
            foreach(Panel p in this.Controls)
            {
                if(p.Name == "panelGetQ" || p.Name == "panel1")
                {
                    p.Visible=true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        private void buttonUpdateQ_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddQPic_Click(object sender, EventArgs e)
        {
            openFileDialog6.ShowDialog();
            pictureBoxQPicture.ImageLocation = openFileDialog6.FileName;
            textBoxQPicturePath.Text = openFileDialog6.FileName;            
        }

        private void buttonAddA1Pic_Click(object sender, EventArgs e)
        {
            openFileDialog7.ShowDialog();
            pictureBoxAnswer1.ImageLocation = openFileDialog7.FileName;
            textBoxAnswer1Picture.Text = openFileDialog7.FileName;
        }

        private void buttonAddA2Pic_Click(object sender, EventArgs e)
        {
            openFileDialog8.ShowDialog();
            pictureBoxAnswer2.ImageLocation = openFileDialog8.FileName;
            textBoxAnswer2Picture.Text = openFileDialog8.FileName;
        }

        private void buttonAddA3Pic_Click(object sender, EventArgs e)
        {
            openFileDialog9.ShowDialog();
            pictureBoxAnswer3.ImageLocation = openFileDialog9.FileName;
            textBoxAnswer3Picture.Text = openFileDialog9.FileName;
        }

        private void buttonAddA4Pic_Click(object sender, EventArgs e)
        {
            openFileDialog10.ShowDialog();
            pictureBoxAnswer4.ImageLocation = openFileDialog10.FileName;
            textBoxAnswer4Picture.Text = openFileDialog10.FileName;
        }

        private void buttonUpdateQ_Click_1(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelUpdate" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        private void buttonGetQuestion_Click(object sender, EventArgs e)
        {
            int Unit = int.Parse(textBoxQUnit.Text);
            int Section = int.Parse(textBoxQSection.Text);
            int QuestionNumber = int.Parse(textBoxQNumber.Text);
            Question q = QuestionOps.GetQuestion(Unit, Section, QuestionNumber);
            if (q == null)
            {
                MessageBox.Show("Kayıtlı soru bulunamadı");
            }
            else
            {
                richTextBoxQuestionText1.Text = q.QuestionText1;
                richTextBoxQuestionText2.Text = q.QuestionText2;
                textBoxQPicturePath.Text = q.PicturePath;
                if(q.PicturePath != "..." && q.PicturePath != String.Empty)
                {
                    pictureBoxQPicture.Image = Image.FromFile(q.PicturePath);
                }                
                textBoxAnswer1.Text = q.Answer1;
                textBoxAnswer2.Text = q.Answer2;
                textBoxAnswer3.Text = q.Answer3;
                textBoxAnswer4.Text = q.Answer4;
                textBoxAnswer1Picture.Text = q.A1PicturePath;
                textBoxAnswer2Picture.Text = q.A2PicturePath;
                textBoxAnswer3Picture.Text = q.A3PicturePath;
                textBoxAnswer4Picture.Text = q.A4PicturePath;
                if(q.A1PicturePath != String.Empty)
                {
                    pictureBoxAnswer1.Image = Image.FromFile(q.A1PicturePath);
                }                
                if (q.A2PicturePath != String.Empty)
                {
                    pictureBoxAnswer2.Image = Image.FromFile(q.A2PicturePath);
                }                
                if (q.A3PicturePath != String.Empty)
                {
                    pictureBoxAnswer3.Image = Image.FromFile(q.A3PicturePath);
                }                
                if (q.A4PicturePath != String.Empty)
                {
                    pictureBoxAnswer4.Image = Image.FromFile(q.A4PicturePath);
                }                
                comboBoxRigthAnswer.SelectedItem = q.RightAnswer.ToString();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in panelUpdate.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = String.Empty;
                }
                else if (c is PictureBox)
                {
                    ((PictureBox)c).Image = Image.FromFile("C:/Users/fatih/Desktop/ART GALLERY/qt3.jpg");
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedItem = null;
                }
            }
        }

        private void buttonUpdateQuestion_Click(object sender, EventArgs e)
        {
            int Unit = int.Parse(textBoxQUnit.Text);
            int Section = int.Parse(textBoxQSection.Text);
            int QuestionNumber = int.Parse(textBoxQNumber.Text);
            Question oldQ = QuestionOps.GetQuestion(Unit, Section, QuestionNumber);
            
            Question q = new Question();
            q.QuestionText1 = richTextBoxQuestionText1.Text;
            q.QuestionText2 = richTextBoxQuestionText2.Text;
            q.PicturePath = textBoxQPicturePath.Text;
            q.Answer1 = textBoxAnswer1.Text;
            q.Answer2 = textBoxAnswer2.Text;
            q.Answer3 = textBoxAnswer3.Text;
            q.Answer4 = textBoxAnswer4.Text;
            q.A1PicturePath = textBoxAnswer1Picture.Text;
            q.A2PicturePath = textBoxAnswer2Picture.Text;
            q.A3PicturePath = textBoxAnswer3Picture.Text;
            q.A4PicturePath = textBoxAnswer4Picture.Text;
            q.RightAnswer = int.Parse(comboBoxRigthAnswer.SelectedItem.ToString());
            QuestionOps.UpdateQuestion(q, oldQ);
            MessageBox.Show("Soru güncellendi");
        }

        private void buttonGetResult_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text;
            DateTime date;
            if (DateTime.TryParse(textBoxDate.Text, out date))
            {
                date = DateTime.Parse(textBoxDate.Text);
                string result = UserOps.GetExamResult(userName, date);
                if (result != null)
                {
                    labelResult.Visible = true;
                    label27.Visible = true;
                    labelResult.Text = result;
                }
                else
                {
                    MessageBox.Show("Sonuç bulunamadı");
                }
            }
            else
            {
                MessageBox.Show("Tarih geçersiz");
            }            
        }

        private void panelExamResults_Paint(object sender, PaintEventArgs e)
        {
            dataGridViewResults.DataSource = UserOps.GetAllResults();
        }

        private void buttonResults_Click(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelExamResults" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
