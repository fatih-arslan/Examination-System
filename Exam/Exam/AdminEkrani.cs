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
    public partial class AdminEkrani : Form
    {
        User oldUser = new User();
        public AdminEkrani()
        {
            InitializeComponent();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelUsers" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        public void LoadUsers()
        {
            DataTable dt = UserOps.GetAllUsers();
            dataGridView1.DataSource = dt;
        }
       

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            UserOps.RemoveUser(userID);
            LoadUsers();
            
        }

        private void AdminEkrani_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            LoadUsers();
            LoadQuestions();
        }

        private void btnGetUser_Click(object sender, EventArgs e)
        {
            string un = textBoxUserName.Text;
            oldUser = UserOps.GetUser(un);
            textBoxNewUserName.Text = oldUser.UserName;
            textBoxName.Text = oldUser.Name;
            textBoxSurName.Text = oldUser.Surname;
            textBoxMail.Text = oldUser.Email;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            User newUser = new User();
            newUser.UserID = oldUser.UserID;
            newUser.UserName = textBoxNewUserName.Text;
            newUser.Name = textBoxName.Text;
            newUser.Surname = textBoxSurName.Text;
            newUser.Email = textBoxMail.Text;
            newUser.Password = oldUser.Password;
            newUser.UserTypeID = oldUser.UserTypeID;
            UserOps.UpdateUser(oldUser, newUser);

        }

        private void textBoxNewUserName_TextChanged(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;
        }

        private void textBoxSurName_TextChanged(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;
        }

        private void textBoxMail_TextChanged(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;
        }

        private void buttonGetQ_Click(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelUpdateUser" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        public void LoadQuestions()
        {
            DataTable dt = QuestionOps.GetAllQuestions();
            dataGridView2.DataSource = dt;
        }

        private void btnDeleteQ_Click(object sender, EventArgs e)
        {
            int questionID = int.Parse(dataGridView2.CurrentRow.Cells["QuestionID"].Value.ToString());
            QuestionOps.RemoveQuestion(questionID);
            LoadQuestions();
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
                if (q.PicturePath != "...")
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
                if (q.A1PicturePath != String.Empty)
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGetQuestions_Click(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelQuestions" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        private void buttonResults_Click(object sender, EventArgs e)
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
    }
}
