using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    internal class UserOps
    {
        public static User GetUser(string userName)
        {
            User user = new User();
            SqlCommand command1 = new SqlCommand("Select * from Users where UserName = @Username", SqlOps.connection);
            command1.Parameters.AddWithValue("@UserName", userName);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                user.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                user.UserName = dt.Rows[0]["UserName"].ToString();
                user.Name = dt.Rows[0]["Name"].ToString();
                user.Surname = dt.Rows[0]["SurName"].ToString();
                user.Email = dt.Rows[0]["Mail"].ToString();
                user.Password = dt.Rows[0]["Password"].ToString();
                user.UserTypeID = int.Parse(dt.Rows[0]["UserTypeID"].ToString());
                if(user.UserTypeID == 1)
                {
                    Student student = new Student();
                    student.UserID = user.UserID;
                    student.UserName = user.UserName;
                    student.Name = user.Name;
                    student.Surname = user.Surname;
                    student.Email = user.Email;
                    student.Password = user.Password;
                    student.UserTypeID = user.UserTypeID;
                    SqlCommand command2 = new SqlCommand("Select * from QuestionRepetitions where UserID = @UserID",SqlOps.connection);
                    command2.Parameters.AddWithValue("@UserID", student.UserID);
                    SqlDataAdapter da2 = new SqlDataAdapter(command2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if(dt2.Rows.Count > 0)
                    {                        
                        student.QuestionRepetition = dt2.Rows[0]["Repetition"].ToString();
                    }
                    else
                    {
                        student.QuestionRepetition = Student.Repetition;
                    }
                    user = student;
                }
            }
            else
            {
                user = null;
            }
            return user;
        }

        public static User GetUserByEmail(string email)
        {
            User user = new User();
            SqlCommand commandGet = new SqlCommand("Select * from Users where Mail = @Mail", SqlOps.connection);
            commandGet.Parameters.AddWithValue("@Mail", email);
            SqlDataAdapter da = new SqlDataAdapter(commandGet);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                user.UserName = dt.Rows[0]["UserName"].ToString();
                user = GetUser(user.UserName);
            }
            else
            {
                user = null;
            }
            return user;
        }
        public static string AddUser(User user)
        {
            string result = "Kayıt tamamlandı";
            User u = UserOps.GetUser(user.UserName);
            User u2 = UserOps.GetUserByEmail(user.Email);
            if(u != null)
            {
                result = "Bu kullanıcı adı alınmış, lütfen başka bir kullanıcı adı seçin";
            }
            else if(u2 != null)
            {
                result = "Bu mail adresi kullanılıyor, lütfen başka bir mail adresi girin";
            }
            else
            {                
                SqlCommand command1 = new SqlCommand("Insert into Users (UserName, Name, Surname, Mail, Password, UserTypeID) values" +
                    "(@UserName, @Name, @Surname, @Mail, @Password, @UserTypeID)", SqlOps.connection);
                command1.Parameters.AddWithValue("@UserName", user.UserName);
                command1.Parameters.AddWithValue("@Name", user.Name);
                command1.Parameters.AddWithValue("@Surname", user.Surname);
                command1.Parameters.AddWithValue("@Mail", user.Email);
                command1.Parameters.AddWithValue("@Password", user.Password);
                command1.Parameters.AddWithValue("@UserTypeID", user.UserTypeID);
                SqlOps.CheckConnection(SqlOps.connection);
                command1.ExecuteNonQuery();                
            }
            return result;
        }        

        public static string AddNewRepetition(Student student, string rep)
        {            
            string result = "Değişiklikler kaydedildi";
            if(rep == "1 7 30 90 180 365")
            {
                result = "Değişiklik bulunamadı";
                return result;
            }
            string[] s = rep.Split(' '); 
            HashSet<string> h = s.ToHashSet();            
            if(h.Count < s.Length)
            {
                result = "6 farklı tekrar günü olmak zorundadır";
                return result;
            }
            for(int i = 0; i < s.Length; i++)
            {
                if(int.Parse(s[i]) <= 0 || int.Parse(s[i]) > 365)
                {
                    result = "Gün değerleri 1 ve 365 arasında olmak zorundadır";
                    return result;
                }
            }            
            if(result == "Değişiklikler kaydedildi")
            {
                SqlCommand command1 = new SqlCommand("select * from QuestionRepetitions where UserID = @UserID", SqlOps.connection);
                command1.Parameters.AddWithValue("@UserID", student.UserID);
                SqlDataAdapter da = new SqlDataAdapter(command1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    SqlCommand command2 = new SqlCommand("Update QuestionRepetitions set Repetition = @Repetition where " +
                        "UserID = @UserID",SqlOps.connection);
                    command2.Parameters.AddWithValue("@UserID", student.UserID);
                    command2.Parameters.AddWithValue("@Repetition", rep);
                    SqlOps.CheckConnection(SqlOps.connection);
                    command2.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand command3 = new SqlCommand("Insert into QuestionRepetitions (UserID, Repetition)" +
                        "values (@UserID, @Repetition)", SqlOps.connection);
                    SqlOps.CheckConnection(SqlOps.connection);
                    command3.Parameters.AddWithValue("@UserID", student.UserID);
                    command3.Parameters.AddWithValue("@Repetition", rep);
                    command3.ExecuteNonQuery();
                }                
            }
            return result;
        }

        public static void UpdateUser(User oldUser, User newUser)
        {
            SqlCommand command1 = new SqlCommand("Update Users set UserName = @UserName, Name = @Name, Surname = @Surname," +
                "Mail = @Mail, Password = @Password where UserID = @UserID",SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            command1.Parameters.AddWithValue("@UserName", newUser.UserName);
            command1.Parameters.AddWithValue("@Name", newUser.Name);
            command1.Parameters.AddWithValue("@Surname", newUser.Surname);
            command1.Parameters.AddWithValue("@Mail", newUser.Email);
            command1.Parameters.AddWithValue("@Password", newUser.Password);
            command1.Parameters.AddWithValue("@UserID", oldUser.UserID);
            command1.ExecuteNonQuery();
        }

        public static void AddExamResult(Student student, string result, DateTime date)
        {
            SqlCommand command1 = new SqlCommand("Insert into ExamResults (UserID, UserName, Result, ExamDate) " +
                "values (@UserID, @UserName, @Result, @ExamDate)", SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            command1.Parameters.AddWithValue("@UserID", student.UserID.ToString());
            command1.Parameters.AddWithValue("@UserName", student.UserName);
            command1.Parameters.AddWithValue("@Result", result);
            command1.Parameters.AddWithValue("@ExamDate", date);
            command1.ExecuteNonQuery();
        }

        public static string GetExamResult(string UserName, DateTime date)
        {
            string result = null;
            SqlCommand command1 = new SqlCommand("Select UserName as KullanıcıAdı, Result as Sonuc, ExamDate as Tarih from ExamResults" +
                " where UserName = @UserName and ExamDate = @ExamDate", SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            command1.Parameters.AddWithValue("@UserName", UserName);
            command1.Parameters.AddWithValue("@ExamDate", date);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["Sonuc"].ToString();
            }
            return result;
        }

        public static DataTable GetAllResults()
        {
            SqlCommand command1 = new SqlCommand("Select UserName as KullanıcıAdı, Result as Sonuc, ExamDate as Tarih " +
                "from ExamResults order by ExamDate",SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataTable GetAllUsers()
        {
            SqlCommand command1 = new SqlCommand("select Users.UserID as ID, Users.UserName as KullanıcıAdı, Users.Name as İsim, " +
                "Users.Surname as Soyisim, Users.Mail as EPosta, UserTypes.UserTypeName " +
                "as KullanıcıTipi from Users inner join UserTypes on Users.UserTypeID = UserTypes.UserTypeID", SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static void RemoveUser(int userID)
        {
            SqlCommand command1 = new SqlCommand("Delete from Users where UserID = @UserID", SqlOps.connection);
            command1.Parameters.AddWithValue("@UserID", userID);
            command1.ExecuteNonQuery();
        }

        public static DataTable GetReport1(int userID)
        {
            SqlCommand command1 = new SqlCommand("select count(Questions.QuestionID) as Corrects, Questions.UnitID from Questions inner join " +
                "AnsweredQuestions on Questions.QuestionID = AnsweredQuestions.QuestionID where AnsweredQuestions.CorrectlyAnswered > 0 " +
                "and AnsweredQuestions.UserID = @UserID group by Questions.UnitID order by corrects desc", SqlOps.connection);
            command1.Parameters.AddWithValue("@UserID", userID);
            SqlOps.CheckConnection(SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataTable GetReport2(int userID)
        {
            SqlCommand command1 = new SqlCommand("Select count(AnsweredQuestions.QuestionID) as Questions, Questions.UnitID " +
                "from Questions inner join AnsweredQuestions on Questions.QuestionID = " +
                "AnsweredQuestions.QuestionID where AnsweredQuestions.UserID = @UserID group by UnitID", SqlOps.connection);
            SqlOps.CheckConnection (SqlOps.connection);
            command1.Parameters.AddWithValue("@UserID", userID);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
