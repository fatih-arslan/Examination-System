using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace Exam
{
    public partial class GirisEkrani : Form
    {
        public static User user;
        public static Student student = new Student();
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
            if(textBoxUserName.Text != String.Empty || textBoxPassword.Text != String.Empty)
            {
                string userName = textBoxUserName.Text;
                if(UserOps.GetUser(userName) != null && UserOps.GetUser(userName).Password == textBoxPassword.Text)
                {
                    if (UserOps.GetUser(userName) is Student)
                    {
                        student = (Student)UserOps.GetUser(userName);
                        user = null;
                    }
                    else
                    {
                        user = UserOps.GetUser(userName);
                        student = null;
                    }
                    if (user != null)
                    {
                        if (user.UserTypeID == 2)
                        {
                            SinavSorumluEkrani se = new SinavSorumluEkrani();
                            se.Show();
                            this.Close();
                        }
                        else if (user.UserTypeID == 3)
                        {
                            AdminEkrani admin = new AdminEkrani();
                            admin.Show();
                            this.Close();
                        }                        
                    }
                    else if (student != null)
                    {
                        OgrenciEkrani oe = new OgrenciEkrani();
                        oe.Show();
                        this.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı ya da şifre yanlış");
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun");
            }                      
        }

        private void lblOgrSifre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string userMail = Interaction.InputBox("Lütfen hesabınızda kayıtlı mail adresinizi girin", "Mail adresinizi girin", "", 70, 70);
            string newPassword = Membership.GeneratePassword(7, 0).ToString();
            User user = UserOps.GetUserByEmail(userMail);
            if(user != null)
            {
                User newUser = UserOps.GetUserByEmail(userMail);
                newUser.Password = newPassword;
                UserOps.UpdateUser(user, newUser);
                student = (Student)newUser;
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("ExamSystemApp@gmail.com");
                    mail.To.Add(userMail);
                    mail.Subject = "Yeni Şifreniz";
                    mail.Body = $"Exam App yeni şifreniz: {newPassword}";

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("ExamSystemApp@gmail.com", "fatiheren6805");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                    SmtpServer.Send(mail);
                    MessageBox.Show("Mail adresinize yeni şifreniz gönderildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Girdiğiniz mail adresiyle kayıtlı hesap bulunamadı");
            }
        }
    }
}
