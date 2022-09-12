using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class OgrenciEkrani : Form
    {
        public static int PracticeUnit;
        public OgrenciEkrani()
        {
            InitializeComponent();
        }

        private void OgrenciEkrani_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;                                                 
        }

        private void btnExam_Click(object sender, EventArgs e)
        {

        }

        private void buttonPrepExam_Click(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelPrepExam" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }        

        private void btnExam_Click_1(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelExam" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }

        private void buttonStartExam_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            if (UserOps.GetExamResult(GirisEkrani.student.UserName, DateTime.Today) != null)
            {
                MessageBox.Show("Günlük sınav hakkınız bulunmamaktadır");
            }
            else
            {
                SinavEkrani se = new SinavEkrani();
                se.Show();
            }            
        }

        private void buttonsettings_Click(object sender, EventArgs e)
        {
            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelSettings" || p.Name == "panel1")
                {
                    p.Visible = true;
                }
                else
                {
                    p.Visible = false;
                }
            }
        }                                     

        private void buttonSaveRep_Click(object sender, EventArgs e)
        {
            Student s = GirisEkrani.student;
            string rep = "";
            foreach (TextBox tb in panelExamSetting.Controls.OfType<TextBox>())
            {
                rep += tb.Text + " ";
            }
            rep = rep.Trim();
            s.QuestionRepetition = rep;
            GirisEkrani.student = s;
            MessageBox.Show(UserOps.AddNewRepetition(GirisEkrani.student, rep));
        }

        private void textBoxR1_TextChanged(object sender, EventArgs e)
        {
            textBoxR1.Text = textBoxR1.Text;
            buttonSaveRep.Enabled = true;
        }

        private void buttonAccountSetting_Click(object sender, EventArgs e)
        {
            panelUserInfo.Visible = true;
            panelExamSetting.Visible = false;
            panelPasswordChange.Visible = false;
            textBoxUserName.Text = GirisEkrani.student.UserName;
            textBoxName.Text = GirisEkrani.student.Name;
            textBoxSurname.Text = GirisEkrani.student.Surname;
            textBoxEMail.Text = GirisEkrani.student.Email;
        }

        private void buttonPasswordChange_Click(object sender, EventArgs e)
        {
            panelUserInfo.Visible = false;
            panelExamSetting.Visible = false;
            panelPasswordChange.Visible = true;
        }

        private void buttonChangeRep_Click(object sender, EventArgs e)
        {
            panelUserInfo.Visible = false;
            panelExamSetting.Visible = true;
            panelPasswordChange.Visible = false;
            string r = GirisEkrani.student.QuestionRepetition;
            string[] s = r.Split(' ');
            if (panelExamSetting.Visible == true)
            {
                int i = 0;
                foreach (TextBox tb in panelExamSetting.Controls.OfType<TextBox>())
                {                    
                    tb.Text = s[i];                                        
                    i++;
                }
            }
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            Student s = GirisEkrani.student;           
            s.UserName = textBoxUserName.Text;
            s.Name = textBoxName.Text;
            s.Surname = textBoxSurname.Text;
            s.Email = textBoxEMail.Text;
            UserOps.UpdateUser(GirisEkrani.student, s);
            GirisEkrani.student = s;
            MessageBox.Show("Değişiklikler kaydedildi");
        }

        private void buttonSavePassword_Click(object sender, EventArgs e)
        {
            if(textBoxCurrentPassword.Text == GirisEkrani.student.Password)
            {
                Student s = GirisEkrani.student;
                s.Password = textBoxNewPassword.Text;
                UserOps.UpdateUser(GirisEkrani.student, s);
                GirisEkrani.student = s;
                MessageBox.Show("Şifre değiştirildi");
            }
            else
            {
                MessageBox.Show("Şifre yanlış");
            }
        }
       
        private void buttonStartPractice_Click(object sender, EventArgs e)
        {
            PracticeUnit = int.Parse(comboBox1.SelectedItem.ToString());
            Alistirma a = new Alistirma();
            a.Show();           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            List<int> units = new List<int>();
            DataTable dt = UserOps.GetReport1(GirisEkrani.student.UserID);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    units.Add(int.Parse(dt.Rows[i]["UnitID"].ToString()));
                    chart1.Series["soru"].Points.AddXY(dt.Rows[i]["UnitID"], dt.Rows[i]["Corrects"]);
                }
                labelUnitMostCorrect.Text = dt.Rows[0]["UnitID"].ToString();
                labelUnitLeastCorrect.Text = dt.Rows[dt.Rows.Count - 1]["UnitID"].ToString();
                string unitsNoCorrect = "";
                if (units.Count == 7)
                {
                    unitsNoCorrect = "Yok";
                }
                for (int i = 1; i < 8; i++)
                {
                    if (!units.Contains(i))
                    {
                        unitsNoCorrect += i.ToString() + " ";
                    }
                }
                labelNoCorrect.Text = unitsNoCorrect;
            }

            List<int> units2 = new List<int>();
            DataTable dt2 = UserOps.GetReport2(GirisEkrani.student.UserID);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    units2.Add(int.Parse(dt2.Rows[i]["UnitID"].ToString()));
                    chart2.Series["soru"].Points.AddXY(dt2.Rows[i]["UnitID"], dt2.Rows[i]["Questions"]);
                }
                labelMostQ.Text = dt2.Rows[0]["UnitID"].ToString();
                labelLeastQ.Text = dt2.Rows[dt2.Rows.Count - 1]["UnitID"].ToString();
                string unitsNoQ = "";
                if (units2.Count == 7)
                {
                    unitsNoQ = "Yok";
                }
                for (int i = 1; i < 8; i++)
                {
                    if (!units2.Contains(i))
                    {
                        unitsNoQ += i.ToString() + " ";
                    }
                }
                labelNoQ.Text = unitsNoQ;
            }

            foreach (Panel p in this.Controls)
            {
                if (p.Name == "panelReport" || p.Name == "panel1")
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
            PrintDialog p1 = new PrintDialog();
            PrintDocument p2 = new PrintDocument();

            p2.DocumentName = "Print Document";
            p1.Document = p2;
            p1.AllowSelection = true;
            p1.AllowSomePages = true;   
            if(p1.ShowDialog() == DialogResult.OK)
            {
                p2.PrintPage += P2_PrintPage;
                p2.Print();
            }
            
        }

        private void P2_PrintPage(object sender, PrintPageEventArgs e)
        {
            string text = labelMostCorrect1.Text + " " + labelUnitMostCorrect.Text + "\n";
            text += labelLeastCorrect1.Text + " " + labelUnitLeastCorrect.Text + "\n";
            text += labelNoCorrect1.Text + " " + labelNoCorrect.Text + "\n";
            text += labelMostQ1.Text + " " + labelMostQ.Text + "\n";
            text += labelLeastQ1.Text + " " + labelLeastQ.Text + "\n";
            text += labelNoQ1.Text + " " + labelNoQ.Text + "\n";
            Font font = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);
            e.Graphics.DrawString(text, font, brush,10,20);

        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            buttonSaveChanges.Enabled = true;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonSaveChanges.Enabled = true;
        }

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            buttonSaveChanges.Enabled = true;
        }

        private void textBoxEMail_TextChanged(object sender, EventArgs e)
        {
            buttonSaveChanges.Enabled = true;
        }

        private void textBoxCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            buttonSavePassword.Enabled = true;
        }

        private void textBoxNewPassword_TextChanged(object sender, EventArgs e)
        {
            buttonSavePassword.Enabled=true;
        }

        private void textBoxR2_TextChanged(object sender, EventArgs e)
        {
            buttonSaveRep.Enabled = true;
        }

        private void textBoxR3_TextChanged(object sender, EventArgs e)
        {
            buttonSaveRep.Enabled=true;
        }

        private void textBoxR4_TextChanged(object sender, EventArgs e)
        {
            buttonSaveRep.Enabled = true;
        }

        private void textBoxR5_TextChanged(object sender, EventArgs e)
        {
            buttonSaveRep.Enabled = true;
        }

        private void textBoxR6_TextChanged(object sender, EventArgs e)
        {
            buttonSaveRep.Enabled = true;
        }
    }
}
