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
    public partial class KayitEkrani : Form
    {
        public KayitEkrani()
        {
            InitializeComponent();
        }

        private void buttonOgrPanel_Click(object sender, EventArgs e)
        {
            this.pnlSrmKyt.Visible = false;
            this.pnlOgrKyt.Visible = true;
        }

        private void buttonSrmPanel_Click(object sender, EventArgs e)
        {
            this.pnlOgrKyt.Visible = false;
            this.pnlSrmKyt.Visible = true;
        }

        private void KayitEkrani_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnSrmKayit_Click(object sender, EventArgs e)
        {
            if(textBoxSrmUN.Text != String.Empty && textBoxSrmAd.Text != String.Empty && textBoxSrmSoyad.Text != String.Empty &&
                textBoxSrmMail.Text != String.Empty && textBoxSrmSifre.Text != String.Empty)
            {                
                User user = new User
                {
                    UserName = textBoxSrmUN.Text,
                    Name = textBoxSrmAd.Text,
                    Surname = textBoxSrmSoyad.Text,
                    Email = textBoxSrmMail.Text,
                    Password = textBoxSrmSifre.Text,
                    UserTypeID = 2
                };
                MessageBox.Show(UserOps.AddUser(user));
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }

        private void btnOgrKayit_Click(object sender, EventArgs e)
        {
            if (textBoxOgrUN.Text != String.Empty && textBoxOgrAd.Text != String.Empty && textBoxOgrSoyad.Text != String.Empty &&
                textBoxOgrMail.Text != String.Empty && textBoxOgrSifre.Text != String.Empty)
            {
                Student student = new Student
                {
                    UserName = textBoxOgrUN.Text,
                    Name = textBoxOgrAd.Text,
                    Surname = textBoxOgrSoyad.Text,
                    Email = textBoxOgrMail.Text,
                    Password = textBoxOgrSifre.Text,
                    UserTypeID = 1
                };
                MessageBox.Show(UserOps.AddUser(student));
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }
    }
}
