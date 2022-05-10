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
    public partial class GirisEkrani : Form
    {
        public static User user;
        public static Student student;
        public GirisEkrani()
        {
            InitializeComponent();
        }
                
        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        
        private void btnGiris_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text;
            if(UserOps.GetUser(userName) is Student)
            {
                student = (Student)UserOps.GetUser(userName);
            }
            else
            {
                user = UserOps.GetUser(userName);
            }
            SinavSorumluEkrani se = new SinavSorumluEkrani();
            OgrenciEkrani oe = new OgrenciEkrani();
            AdminEkrani admin = new AdminEkrani();
            //admin.Show();            
            oe.Show();
            //se.Show();
            this.Close();
        }
    }
}
