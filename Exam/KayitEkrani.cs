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
                try
                {
                    SqlCommand command1 = new SqlCommand("Insert into Users (UserName, Name, Surname, Mail, Password, UserTypeID) values" +
                    "(@UserName, @Name, @Surname, @Mail, @Password, @UserTypeID)", SqlOps.connection);
                    command1.Parameters.AddWithValue("@UserName", textBoxSrmUN.Text);
                    command1.Parameters.AddWithValue("@Name", textBoxSrmAd.Text);
                    command1.Parameters.AddWithValue("@Surname", textBoxSrmSoyad.Text);
                    command1.Parameters.AddWithValue("@Mail", textBoxSrmMail.Text);
                    command1.Parameters.AddWithValue("@Password", textBoxSrmSifre.Text);
                    command1.Parameters.AddWithValue("@UserTypeID", 2);
                    SqlOps.CheckConnection(SqlOps.connection);
                    command1.ExecuteNonQuery();
                    this.Close();
                    MessageBox.Show("Kayıt tamamlandı");
                }
                catch(Exception)
                {
                    MessageBox.Show("Bu kullanıcı adı alınmış, lütfen başka bir kullanıcı adı seçin ");
                }
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
                try
                {
                    SqlCommand command1 = new SqlCommand("Insert into Users (UserName, Name, Surname, Mail, Password, UserTypeID) values" +
                    "(@UserName, @Name, @Surname, @Mail, @Password, @UserTypeID)", SqlOps.connection);
                    command1.Parameters.AddWithValue("@UserName", textBoxOgrUN.Text);
                    command1.Parameters.AddWithValue("@Name", textBoxOgrAd.Text);
                    command1.Parameters.AddWithValue("@Surname", textBoxOgrSoyad.Text);
                    command1.Parameters.AddWithValue("@Mail", textBoxOgrMail.Text);
                    command1.Parameters.AddWithValue("@Password", textBoxOgrSifre.Text);
                    command1.Parameters.AddWithValue("@UserTypeID", 1);
                    SqlOps.CheckConnection(SqlOps.connection);
                    command1.ExecuteNonQuery();
                    this.Close();
                    MessageBox.Show("Kayıt tamamlandı");
                }
                catch (Exception)
                {
                    MessageBox.Show("Bu kullanıcı adı alınmış, lütfen başka bir kullanıcı adı seçin ");
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }
    }
}
