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
        public GirisEkrani()
        {
            InitializeComponent();
        }
                
        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOgrGiris_Click(object sender, EventArgs e)
        {
            SinavSorumluEkrani se = new SinavSorumluEkrani();
            OgrenciEkrani oe = new OgrenciEkrani();
            oe.Show();
            //se.Show();
            this.Close();
        }
    }
}
