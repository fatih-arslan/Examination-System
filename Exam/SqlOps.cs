using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Exam
{
    public class SqlOps
    {
        public static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SUPB5RF;Initial Catalog=ExamDatabase;Integrated Security=True");
        public static void CheckConnection(SqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
    }
}
