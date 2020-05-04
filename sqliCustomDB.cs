using System;
using System.Data;
using System.Data.SqlClient;

namespace CxAudit_Demo
{
    public partial class sqliCustomDB : System.Web.UI.Page
    {
        static string username = String.Empty;
        static int age = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string ID = Request.QueryString[0];
                getName(ID);
            }
            catch { }
        }

        private void getName(string ID)
        {
            string username = "No name";
            XyzDBSqlConnection conn = new XyzDBSqlConnection("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=" + Constants.DB_PASSWORD + ";");
            XyzDBSqlCommand cmd = new XyzDBSqlCommand();
            XyzDBSqlDataReader reader;

            cmd.CommandText = "SELECT NAME FROM Users WHERE ID = " + ID;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.XyzDBxecuteMethod();
            if (reader.HasRows)
            {
                username = reader["NAME"].ToString();
                age = getAge(username);
            }

            message.Text = myCustomSanitizer("Welcome " + username);
            conn.Close();
        }        

        protected void submit_Click(object sender, EventArgs e)
        {
            getName(name.Text);
        }
    }
}
