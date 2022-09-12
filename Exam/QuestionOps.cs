using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    internal class QuestionOps
    {
        public static void AddQuestion(Question q)
        {
            SqlCommand command1 = new SqlCommand("Insert into Questions (UnitID, SectionID, QuestionNumber, QuestionText1, PicturePath, QuestionText2, RightAnswer) values " +
           "(@UnitID, @SectionID, @QuestionNumber, @QuestionText1, @PicturePath, @QuestionText2, @RightAnswer)", SqlOps.connection);
            command1.Parameters.AddWithValue("@UnitID", q.UnitID);
            command1.Parameters.AddWithValue("@SectionID", q.SectionID);
            command1.Parameters.AddWithValue("@QuestionNumber", q.QuestionNumber);
            command1.Parameters.AddWithValue("@QuestionText1", q.QuestionText1);
            command1.Parameters.AddWithValue("@PicturePath", q.PicturePath);
            command1.Parameters.AddWithValue("@QuestionText2", q.QuestionText2);
            command1.Parameters.AddWithValue("@RightAnswer", q.RightAnswer);
            SqlOps.CheckConnection(SqlOps.connection);
            command1.ExecuteNonQuery();
            SqlCommand commandGet = new SqlCommand("select top 1 QuestionID from Questions order by QuestionID desc", SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(commandGet);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int QuestionID = Convert.ToInt32(dt.Rows[0]["QuestionID"].ToString());
            SqlCommand command2 = new SqlCommand("Insert into Answers (QuestionID, Answer1, Answer2, Answer3, Answer4, A1PicturePath, A2PicturePath, A3PicturePath, A4PicturePath) values " +
            "(@QuestionID, @Answer1, @Answer2, @Answer3, @Answer4, @Answer1Path, @Answer2Path, @Answer3Path, @Answer4Path)", SqlOps.connection);
            command2.Parameters.AddWithValue("@QuestionID", QuestionID);
            command2.Parameters.AddWithValue("@Answer1", q.Answer1);
            command2.Parameters.AddWithValue("@Answer2", q.Answer2);
            command2.Parameters.AddWithValue("@Answer3", q.Answer3);
            command2.Parameters.AddWithValue("@Answer4", q.Answer4);
            command2.Parameters.AddWithValue("@Answer1Path", q.A1PicturePath);
            command2.Parameters.AddWithValue("@Answer2Path", q.A2PicturePath);
            command2.Parameters.AddWithValue("@Answer3Path", q.A3PicturePath);
            command2.Parameters.AddWithValue("@Answer4Path", q.A4PicturePath);
            SqlOps.CheckConnection(SqlOps.connection);
            command2.ExecuteNonQuery();
        }

        public static Question GetQuestionByID(int questionID)
        {
            Question question = new Question();
            SqlCommand command1 = new SqlCommand("Select * from Questions where QuestionID = @QuestionID", SqlOps.connection);
            command1.Parameters.AddWithValue("@QuestionID", questionID);            
            SqlOps.CheckConnection(SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                question.QuestionID = questionID;
                question.QuestionText1 = dt.Rows[0]["QuestionText1"].ToString();
                question.PicturePath = dt.Rows[0]["PicturePath"].ToString();
                question.QuestionText2 = dt.Rows[0]["QuestionText2"].ToString();
                question.RightAnswer = int.Parse(dt.Rows[0]["RightAnswer"].ToString());
                question.UnitID = int.Parse(dt.Rows[0]["UnitID"].ToString());
                question.QuestionNumber = int.Parse(dt.Rows[0]["QuestionNumber"].ToString());
                question.SectionID = int.Parse(dt.Rows[0]["SectionID"].ToString());

                SqlCommand command2 = new SqlCommand("Select * from Answers where QuestionID = @QuestionID", SqlOps.connection);
                command2.Parameters.AddWithValue("@QuestionID", question.QuestionID);
                SqlOps.CheckConnection(SqlOps.connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                question.Answer1 = dt2.Rows[0]["Answer1"].ToString();
                question.Answer2 = dt2.Rows[0]["Answer2"].ToString();
                question.Answer3 = dt2.Rows[0]["Answer3"].ToString();
                question.Answer4 = dt2.Rows[0]["Answer4"].ToString();
                question.A1PicturePath = dt2.Rows[0]["A1PicturePath"].ToString();
                question.A2PicturePath = dt2.Rows[0]["A2PicturePath"].ToString();
                question.A3PicturePath = dt2.Rows[0]["A3PicturePath"].ToString();
                question.A4PicturePath = dt2.Rows[0]["A4PicturePath"].ToString();
            }
            else
            {
                question = null;
            }
            return question;
        }

        public static Question GetQuestion(int Unit, int Section, int QuestionNumber)
        {
            Question question = new Question();
            SqlCommand command1 = new SqlCommand("Select * from Questions where UnitID = @UnitID and " +
                "SectionID = @SectionID and QuestionNumber = @QuestionNumber", SqlOps.connection);
            command1.Parameters.AddWithValue("@UnitID", Unit);
            command1.Parameters.AddWithValue("@SectionID", Section);
            command1.Parameters.AddWithValue("@QuestionNumber", QuestionNumber);
            SqlOps.CheckConnection(SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable(); 
            da.Fill(dt);            
            if (dt.Rows.Count > 0)
            {
                question.QuestionID = int.Parse(dt.Rows[0]["QuestionID"].ToString());
                question.QuestionText1 = dt.Rows[0]["QuestionText1"].ToString();
                question.PicturePath = dt.Rows[0]["PicturePath"].ToString();                
                question.QuestionText2 = dt.Rows[0]["QuestionText2"].ToString();
                question.RightAnswer = int.Parse(dt.Rows[0]["RightAnswer"].ToString());
                question.UnitID = Unit;
                question.QuestionNumber = QuestionNumber;
                question.SectionID = Section;

                SqlCommand command2 = new SqlCommand("Select * from Answers where QuestionID = @QuestionID", SqlOps.connection);
                command2.Parameters.AddWithValue("@QuestionID", question.QuestionID);
                SqlOps.CheckConnection(SqlOps.connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                question.Answer1 = dt2.Rows[0]["Answer1"].ToString();
                question.Answer2 = dt2.Rows[0]["Answer2"].ToString();
                question.Answer3 = dt2.Rows[0]["Answer3"].ToString();
                question.Answer4 = dt2.Rows[0]["Answer4"].ToString();
                question.A1PicturePath = dt2.Rows[0]["A1PicturePath"].ToString();
                question.A2PicturePath = dt2.Rows[0]["A2PicturePath"].ToString();
                question.A3PicturePath = dt2.Rows[0]["A3PicturePath"].ToString();
                question.A4PicturePath = dt2.Rows[0]["A4PicturePath"].ToString();
            }
            else
            {
                question = null;
            }            
            return question;
        }
       
        public static void UpdateQuestion(Question newQ, Question oldQ)
        {
            int Unit = oldQ.UnitID;
            int Section = oldQ.SectionID;
            int QuestionNumber = oldQ.QuestionNumber;
            SqlCommand command1 = new SqlCommand("Select * from Questions where UnitID = @UnitID and " +
                "SectionID = @SectionID and QuestionNumber = @QuestionNumber", SqlOps.connection);
            command1.Parameters.AddWithValue("@UnitID", Unit);
            command1.Parameters.AddWithValue("@SectionID", Section);
            command1.Parameters.AddWithValue("@QuestionNumber", QuestionNumber);            
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            SqlOps.CheckConnection(SqlOps.connection);
            da.Fill(dt);            
            int QuestionID = int.Parse(dt.Rows[0]["QuestionID"].ToString());
            
            SqlCommand commandUpdate = new SqlCommand("Update Questions set QuestionText1 = @QuestionText1," +
            "PicturePath = @PicturePath, QuestionText2 = @QuestionText2, RightAnswer = @RightAnswer where UnitID = @UnitID and " +
            "SectionID = @SectionID and QuestionNumber = @QuestionNumber" ,SqlOps.connection);
            commandUpdate.Parameters.AddWithValue("@QuestionText1", newQ.QuestionText1);
            commandUpdate.Parameters.AddWithValue("@PicturePath", newQ.PicturePath);
            commandUpdate.Parameters.AddWithValue("@QuestionText2", newQ.QuestionText2);
            commandUpdate.Parameters.AddWithValue("@RightAnswer", newQ.RightAnswer);           
            commandUpdate.Parameters.AddWithValue("@UnitID", oldQ.UnitID);           
            commandUpdate.Parameters.AddWithValue("@SectionID", oldQ.SectionID);           
            commandUpdate.Parameters.AddWithValue("@QuestionNumber", oldQ.QuestionNumber);
            SqlOps.CheckConnection(SqlOps.connection);
            commandUpdate.ExecuteNonQuery();

            
            SqlCommand commandUpdate2 = new SqlCommand("Update Answers set Answer1 = @Answer1, Answer2 = @Answer2, Answer3 = @Answer3," +
                "Answer4 = @answer4, A1PicturePath = @A1P, A2PicturePath = @A2P, A3PicturePath = @A3P, A4PicturePath = @A4P where QuestionID = @QuestionID" ,SqlOps.connection);
            commandUpdate2.Parameters.AddWithValue("@Answer1", newQ.Answer1);
            commandUpdate2.Parameters.AddWithValue("@Answer2", newQ.Answer2);
            commandUpdate2.Parameters.AddWithValue("@Answer3", newQ.Answer3);
            commandUpdate2.Parameters.AddWithValue("@Answer4", newQ.Answer4);
            commandUpdate2.Parameters.AddWithValue("@A1P", newQ.A1PicturePath);
            commandUpdate2.Parameters.AddWithValue("@A2P", newQ.A2PicturePath);
            commandUpdate2.Parameters.AddWithValue("@A3P", newQ.A3PicturePath);
            commandUpdate2.Parameters.AddWithValue("@A4P", newQ.A4PicturePath);
            commandUpdate2.Parameters.AddWithValue("@QuestionID", QuestionID);
            SqlOps.CheckConnection(SqlOps.connection);
            commandUpdate2.ExecuteNonQuery();
        }

        public static List<int> GetUnansweredQuestions(Student student)
        {            
            SqlCommand command3 = new SqlCommand("select QuestionID from Questions where QuestionID not in " +
                "(select QuestionID from AnsweredQuestions where UserID = @UserID and CorrectlyAnswered > 0 and CorrectlyAnswered < 6)", SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            command3.Parameters.AddWithValue("@UserID", student.UserID);
            command3.Parameters.AddWithValue("0", 0);
            command3.Parameters.AddWithValue("6", 6);
            SqlDataAdapter da3 = new SqlDataAdapter(command3);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            List<int> IDs = new List<int>();
                           
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                IDs.Add(int.Parse(dt3.Rows[i]["QuestionID"].ToString()));
            }                                                    
            return IDs;
        }

        public static void AddAnsweredQuestion(AnsweredQuestion ansQ)
        {
            SqlCommand command1 = new SqlCommand("Select * from AnsweredQuestions where QuestionID = @QuestionID and UserID = @UserID",SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            command1.Parameters.AddWithValue("@QuestionID", ansQ.QuestionID);
            command1.Parameters.AddWithValue("@UserID", ansQ.UserID);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count != 0)
            {
                SqlCommand command2 = new SqlCommand("Update AnsweredQuestions set ChosenAnswer = @ChosenAnswer," +
                    "DateOfAnswer =  @DateOfAnswer, CorrectlyAnswered = @CorrectlyAnswered where QuestionID = @QuestionID and UserID = @UserID", SqlOps.connection);
                command2.Parameters.AddWithValue("@ChosenAnswer", ansQ.ChosenAnswer);
                command2.Parameters.AddWithValue("@DateOfAnswer", ansQ.DateOfAnswer);
                command2.Parameters.AddWithValue("@CorrectlyAnswered", ansQ.CorrectlyAnswered);
                command2.Parameters.AddWithValue("@QuestionID", ansQ.QuestionID);
                command2.Parameters.AddWithValue("@UserID", ansQ.UserID);
                SqlOps.CheckConnection(SqlOps.connection);
                command2.ExecuteNonQuery();
            }
            else
            {
                SqlCommand command3 = new SqlCommand("Insert into AnsweredQuestions " +
                "(QuestionID, RightAnswer, ChosenAnswer, UserID, DateOfAnswer, CorrectlyAnswered) values " +
                "(@QuestionID, @RightAnswer, @ChosenAnswer, @UserID, @DateOfAnswer, @CorrectlyAnswered)", SqlOps.connection);
                command3.Parameters.AddWithValue("@QuestionID", ansQ.QuestionID);
                command3.Parameters.AddWithValue("@RightAnswer", ansQ.RightAnswer);
                command3.Parameters.AddWithValue("@ChosenAnswer", ansQ.ChosenAnswer);
                command3.Parameters.AddWithValue("@UserID", ansQ.UserID);
                command3.Parameters.AddWithValue("@DateOfAnswer", ansQ.DateOfAnswer);
                command3.Parameters.AddWithValue("@CorrectlyAnswered", ansQ.CorrectlyAnswered);
                SqlOps.CheckConnection(SqlOps.connection);
                command3.ExecuteNonQuery();
            }            
        }
       
        public static AnsweredQuestion GetAnsweredQuestionByID(int ID)
        {
            AnsweredQuestion tq = new AnsweredQuestion();
            SqlCommand command1 = new SqlCommand("Select * from AnsweredQuestions where QuestionID = @QuestionID", SqlOps.connection);
            command1.Parameters.AddWithValue("@QuestionID", ID);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                SqlCommand command2 = new SqlCommand("Select * from Questions where QuestionID = @QuestionID", SqlOps.connection);
                command2.Parameters.AddWithValue("@QuestionID", ID);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                SqlCommand command3 = new SqlCommand("Select * from Answers where QuestionID = @QuestionID", SqlOps.connection);
                command3.Parameters.AddWithValue("@QuestionID", ID);
                SqlDataAdapter da3 = new SqlDataAdapter(command3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                tq.QuestionID = ID;
                tq.UnitID = int.Parse(dt2.Rows[0]["UnitID"].ToString());
                tq.SectionID = int.Parse(dt2.Rows[0]["SectionID"].ToString());
                tq.QuestionNumber = int.Parse(dt2.Rows[0]["QuestionNumber"].ToString());
                tq.QuestionText1 = dt2.Rows[0]["QuestionText1"].ToString();
                tq.PicturePath = dt2.Rows[0]["PicturePath"].ToString();
                tq.QuestionText2 = dt2.Rows[0]["QuestionText2"].ToString();
                tq.RightAnswer = int.Parse(dt2.Rows[0]["RightAnswer"].ToString());
                tq.Answer1 = dt3.Rows[0]["Answer1"].ToString();
                tq.Answer2 = dt3.Rows[0]["Answer2"].ToString();
                tq.Answer3 = dt3.Rows[0]["Answer3"].ToString();
                tq.Answer4 = dt3.Rows[0]["Answer4"].ToString();
                tq.A1PicturePath = dt3.Rows[0]["A1PicturePath"].ToString();
                tq.A2PicturePath = dt3.Rows[0]["A2PicturePath"].ToString();
                tq.A3PicturePath = dt3.Rows[0]["A3PicturePath"].ToString();
                tq.A4PicturePath = dt3.Rows[0]["A4PicturePath"].ToString();
                tq.ChosenAnswer = int.Parse(dt.Rows[0]["ChosenAnswer"].ToString());
                tq.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                tq.DateOfAnswer = DateTime.Parse(dt.Rows[0]["DateOfAnswer"].ToString());
                tq.CorrectlyAnswered = int.Parse(dt.Rows[0]["CorrectlyAnswered"].ToString());
            }
            else
            {
                tq = null;
            }
            return tq;
        }

        public static List<int> GetQuestionsWithDate(List<DateTime> dates, int userID)
        {
            List<int> list = new List<int>();
            foreach(DateTime date in dates)
            {
                SqlCommand command1 = new SqlCommand("Select * from AnsweredQuestions where DateOfAnswer = @Date and UserID = @UserID and CorrectlyAnswered > 0 " +
                    "and CorrectlyAnswered < 6", SqlOps.connection);
                SqlOps.CheckConnection(SqlOps.connection);
                command1.Parameters.AddWithValue("@Date", date);
                command1.Parameters.AddWithValue("@UserID", userID);
                command1.Parameters.AddWithValue("0", 0);
                command1.Parameters.AddWithValue("6", 6);
                SqlDataAdapter da = new SqlDataAdapter(command1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int n = dt.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    int ID = int.Parse(dt.Rows[i]["QuestionID"].ToString());
                    list.Add(ID);
                }
            }                                    
            return list;
        }

        public static void RemoveAnsweredQuestion(Student student, AnsweredQuestion ansQ)
        {
            if(GetAnsweredQuestionByID(ansQ.QuestionID) != null)
            {
                SqlCommand command1 = new SqlCommand("Delete from AnsweredQuestions where UserID = @UserID and " +
                    "QuestionID = @QuestionID",SqlOps.connection);
                SqlOps.CheckConnection(SqlOps.connection);
                command1.Parameters.AddWithValue("@UserID", student.UserID);
                command1.Parameters.AddWithValue("@QuestionID", ansQ.QuestionID);
                command1.ExecuteNonQuery();
            }
        }

        public static List<int> GetPracticeQuestions(int unit)
        {
            List<int> list = new List<int>();
            SqlCommand command = new SqlCommand("Select QuestionID from Questions where UnitID = @UnitID order by SectionID, QuestionNumber", SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            command.Parameters.AddWithValue("@UnitID", unit);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(int.Parse(dt.Rows[i]["QuestionID"].ToString()));
                }
            }
            else
            {
                list = null;
            }
            return list;
        }

        public static DataTable GetAllQuestions()
        {
            SqlCommand command1 = new SqlCommand("Select * from Questions inner join Answers on " +
                "Questions.QuestionID = Answers.QuestionID",SqlOps.connection);
            SqlOps.CheckConnection(SqlOps.connection);
            SqlDataAdapter da = new SqlDataAdapter(command1);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            return dataTable;
        }

        public static void RemoveQuestion(int ID)
        {
            SqlCommand command1 = new SqlCommand("Delete from Questions where QuestionID = @QuestionID", SqlOps.connection);
            command1.Parameters.AddWithValue("@QuestionID", ID);
            SqlOps.CheckConnection(SqlOps.connection);
            command1.Parameters.AddWithValue("ID", ID);
            command1.ExecuteNonQuery();

            SqlCommand command2 = new SqlCommand("Delete from Answers where QuestionID = @QuestionID", SqlOps.connection);
            command2.Parameters.AddWithValue("@QuestionID", ID);
            SqlOps.CheckConnection(SqlOps.connection);
            command2.Parameters.AddWithValue("ID", ID); 
            command2.ExecuteNonQuery();
        }
    }
}
