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
            SqlCommand command1 = new SqlCommand("Insert into Questions (QuestionText, SectionID, UnitID, PicturePath, RightAnswer) values " +
            "(@QuestionText, @SectionID, @UnitID, @PicturePath, @RightAnswer)", SqlOps.connection);
            command1.Parameters.AddWithValue("@QuestionText", richTextBoxQText.Text);
            command1.Parameters.AddWithValue("@SectionID", textBoxSection.Text);
            command1.Parameters.AddWithValue("@UnitID", textBoxUnit.Text);
            command1.Parameters.AddWithValue("@PicturePath", textBoxPPath.Text);
            command1.Parameters.AddWithValue("@RightAnswer", textBoxRA.Text);
            SqlOps.CheckConnection(SqlOps.connection);
            command1.ExecuteNonQuery();
            SqlCommand commandGet = new SqlCommand("select top 1 QuestionID from Questions order by QuestionID desc", SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(commandGet);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int QuestionID = Convert.ToInt32(dt.Rows[0]["QuestionID"].ToString());
            SqlCommand command2 = new SqlCommand("Insert into Answers (QuestionID, Answer1, Answer2, Answer3, Answer4, A1PicturePath, A2PicturePath, A3PicturePath, A4PicturePath) values " +
            "(@Q, @a1, @a2, @a3, @a4, @a1p, @a2p, @a3p, @a4p)", SqlOps.connection);
            command2.Parameters.AddWithValue("@Q", QuestionID);
            command2.Parameters.AddWithValue("@a1", textBoxA1.Text);
            command2.Parameters.AddWithValue("@a2", textBoxA2.Text);
            command2.Parameters.AddWithValue("@a3", textBoxA3.Text);
            command2.Parameters.AddWithValue("@a4", textBoxA4.Text);
            command2.Parameters.AddWithValue("@a1p", textBoxA1Path.Text);
            command2.Parameters.AddWithValue("@a2p", textBoxA2Path.Text);
            command2.Parameters.AddWithValue("@a3p", textBoxA3Path.Text);
            command2.Parameters.AddWithValue("@a4p", textBoxA4Path.Text);
            SqlOps.CheckConnection(SqlOps.connection);
            command2.ExecuteNonQuery();
            MessageBox.Show("Soru eklendi");                                    
        }

        private void btnSoruEkle_Click(object sender, EventArgs e)
        {
            pnlSoruEkle.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            pictureBoxA1.ImageLocation = openFileDialog2.FileName;
            textBoxA1Path.Text = openFileDialog1.FileName;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
            pictureBoxA2.ImageLocation = openFileDialog3.FileName;
            textBoxA2Path.Text = openFileDialog1.FileName;
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
    }
}
